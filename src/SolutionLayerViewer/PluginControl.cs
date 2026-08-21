using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SolutionLayerViewer.Controls;
using SolutionLayerViewer.Models;
using SolutionLayerViewer.UI;
using XrmToolBox.Extensibility;

namespace SolutionLayerViewer
{
    public partial class PluginControl : PluginControlBase
    {
        private List<SolutionComponentItem> _allComponents = new List<SolutionComponentItem>();

        public PluginControl()
        {
            InitializeComponent();
            ConfigureGrid();
            WireEvents();
            CueBanner.Set(filterTextBox, "Filter components…");
        }

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);
            LoadSolutions();
        }

        private void ConfigureGrid()
        {
            componentsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ComponentType",
                HeaderText = "Component type",
                DataPropertyName = nameof(SolutionComponentItem.ComponentType),
                FillWeight = 55
            });
            componentsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ObjectId",
                HeaderText = "Object id",
                DataPropertyName = nameof(SolutionComponentItem.ObjectId),
                FillWeight = 45
            });
        }

        private void WireEvents()
        {
            solutionComboBox.SelectedIndexChanged += (s, e) => LoadComponents();
            refreshButton.Click += (s, e) => LoadSolutions();
            filterTextBox.TextChanged += (s, e) => ApplyFilter();
            componentsGrid.SelectionChanged += (s, e) => LoadLayers();
        }

        private void SetStatus(string text)
        {
            statusLabel.Text = text;
        }

        private void LoadSolutions()
        {
            if (Service == null)
            {
                return;
            }

            SetStatus("Loading solutions…");

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading solutions...",
                Work = (worker, args) =>
                {
                    var query = new QueryExpression("solution")
                    {
                        ColumnSet = new ColumnSet("solutionid", "friendlyname", "uniquename"),
                        Criteria = new FilterExpression
                        {
                            Conditions = { new ConditionExpression("isvisible", ConditionOperator.Equal, true) }
                        },
                        Orders = { new OrderExpression("friendlyname", OrderType.Ascending) }
                    };

                    args.Result = Service.RetrieveMultiple(query).Entities;
                },
                PostWorkCallBack = args =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(this, args.Error.Message, "Error loading solutions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SetStatus("Failed to load solutions.");
                        return;
                    }

                    var solutions = ((IEnumerable<Entity>)args.Result)
                        .Select(e => new SolutionListItem(e))
                        .ToList();

                    solutionComboBox.DataSource = solutions;
                    _allComponents.Clear();
                    componentsGrid.DataSource = null;
                    ClearLayers("Select a component on the left to see its layer stack.");
                    SetStatus($"{solutions.Count} solution(s) loaded.");
                }
            });
        }

        private void LoadComponents()
        {
            if (Service == null || !(solutionComboBox.SelectedItem is SolutionListItem solution))
            {
                return;
            }

            SetStatus($"Loading components of '{solution.FriendlyName}'…");

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Loading components of '{solution.FriendlyName}'...",
                Work = (worker, args) =>
                {
                    var query = new QueryExpression("solutioncomponent")
                    {
                        ColumnSet = new ColumnSet("componenttype", "objectid"),
                        Criteria = new FilterExpression
                        {
                            Conditions = { new ConditionExpression("solutionid", ConditionOperator.Equal, solution.SolutionId) }
                        }
                    };

                    args.Result = Service.RetrieveMultiple(query).Entities;
                },
                PostWorkCallBack = args =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(this, args.Error.Message, "Error loading components", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SetStatus("Failed to load components.");
                        return;
                    }

                    _allComponents = ((IEnumerable<Entity>)args.Result)
                        .Select(e => new SolutionComponentItem(e))
                        .OrderBy(c => c.ComponentType)
                        .ToList();

                    filterTextBox.Text = string.Empty;
                    ApplyFilter();
                    ClearLayers("Select a component on the left to see its layer stack.");
                    SetStatus($"{_allComponents.Count} component(s) in '{solution.FriendlyName}'.");
                }
            });
        }

        private void ApplyFilter()
        {
            var filter = filterTextBox.Text?.Trim();

            var filtered = string.IsNullOrEmpty(filter)
                ? _allComponents
                : _allComponents
                    .Where(c => c.ComponentType.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0
                                || c.ObjectId.ToString().IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

            componentsGrid.DataSource = null;
            componentsGrid.DataSource = filtered;
        }

        private void LoadLayers()
        {
            if (Service == null || !(componentsGrid.CurrentRow?.DataBoundItem is SolutionComponentItem component))
            {
                ClearLayers("Select a component on the left to see its layer stack.");
                return;
            }

            SetStatus("Loading layers…");

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading layers...",
                Work = (worker, args) =>
                {
                    var query = new QueryExpression("solutioncomponent")
                    {
                        ColumnSet = new ColumnSet(false),
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("objectid", ConditionOperator.Equal, component.ObjectId),
                                new ConditionExpression("componenttype", ConditionOperator.Equal, component.ComponentTypeCode)
                            }
                        }
                    };

                    var solutionLink = query.AddLink("solution", "solutionid", "solutionid");
                    solutionLink.EntityAlias = "sol";
                    solutionLink.Columns = new ColumnSet("friendlyname", "version", "ismanaged", "publisherid");
                    solutionLink.LinkCriteria.AddCondition("isvisible", ConditionOperator.Equal, true);

                    args.Result = Service.RetrieveMultiple(query).Entities;
                },
                PostWorkCallBack = args =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(this, args.Error.Message, "Error loading layers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SetStatus("Failed to load layers.");
                        return;
                    }

                    // Heuristic ordering: the unmanaged ("Active") layer always wins over
                    // managed layers; among managed layers the one with the highest version
                    // wins. This approximates, but is not guaranteed to exactly match, the
                    // ordering shown in the Power Platform maker portal's "Solution layers"
                    // dialog, since Microsoft does not publish the exact algorithm.
                    var layers = ((IEnumerable<Entity>)args.Result)
                        .Select(e => new SolutionLayerItem(e))
                        .OrderBy(l => l.IsManaged)
                        .ThenByDescending(l => l.Version)
                        .ToList();

                    for (var i = 0; i < layers.Count; i++)
                    {
                        layers[i].Order = i + 1;
                    }

                    ShowLayers(component, layers);
                    SetStatus($"{layers.Count} layer(s) for {component.ComponentType} {component.ObjectId}.");
                }
            });
        }

        private void ShowLayers(SolutionComponentItem component, List<SolutionLayerItem> layers)
        {
            layersFlowPanel.SuspendLayout();
            layersFlowPanel.Controls.Clear();

            if (layers.Count == 0)
            {
                layersFlowPanel.Controls.Add(new System.Windows.Forms.Label
                {
                    AutoSize = true,
                    Font = Theme.FontRegular,
                    ForeColor = Theme.TextMuted,
                    Margin = new Padding(6, 12, 6, 6),
                    Text = $"No layers found for this {component.ComponentType.ToLowerInvariant()}."
                });
            }
            else
            {
                foreach (var layer in layers)
                {
                    var card = new LayerCardControl { Width = Math.Max(200, layersFlowPanel.ClientSize.Width - 24) };
                    card.SetLayer(layer, layer.Order == 1);
                    layersFlowPanel.Controls.Add(card);
                }

                layersFlowPanel.Resize -= LayersFlowPanel_Resize;
                layersFlowPanel.Resize += LayersFlowPanel_Resize;
            }

            layersFlowPanel.ResumeLayout(true);
        }

        private void LayersFlowPanel_Resize(object sender, EventArgs e)
        {
            var width = Math.Max(200, layersFlowPanel.ClientSize.Width - 24);
            foreach (LayerCardControl card in layersFlowPanel.Controls.OfType<LayerCardControl>())
            {
                card.Width = width;
            }
        }

        private void ClearLayers(string message)
        {
            layersFlowPanel.Controls.Clear();
            layersFlowPanel.Controls.Add(new System.Windows.Forms.Label
            {
                AutoSize = true,
                Font = Theme.FontRegular,
                ForeColor = Theme.TextMuted,
                Margin = new Padding(6, 12, 6, 6),
                Text = message
            });
        }
    }
}
