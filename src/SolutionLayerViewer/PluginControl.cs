using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using SolutionLayerViewer.Models;
using XrmToolBox.Extensibility;

namespace SolutionLayerViewer
{
    [Export(typeof(IXrmToolBoxPlugin))]
    [ExportMetadata("Name", "Solution Layer Viewer")]
    [ExportMetadata("Description", "Lists the components of a solution and shows the solution layer stack for the selected component.")]
    public class PluginControl : PluginControlBase
    {
        private ComboBox _solutionComboBox;
        private DataGridView _componentsGrid;
        private DataGridView _layersGrid;

        public PluginControl()
        {
            InitializeComponent();
        }

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);
            LoadSolutions();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            var topPanel = new Panel { Dock = DockStyle.Top, Height = 36 };

            var solutionLabel = new Label { Text = "Solution:", AutoSize = true, Location = new Point(8, 11) };

            _solutionComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(70, 7),
                Width = 380,
                DisplayMember = nameof(SolutionListItem.FriendlyName)
            };
            _solutionComboBox.SelectedIndexChanged += (s, e) => LoadComponents();

            var refreshButton = new Button { Text = "Reload solutions", Location = new Point(460, 6), Width = 130 };
            refreshButton.Click += (s, e) => LoadSolutions();

            topPanel.Controls.Add(solutionLabel);
            topPanel.Controls.Add(_solutionComboBox);
            topPanel.Controls.Add(refreshButton);

            var splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 420
            };

            _componentsGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            _componentsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ComponentType", HeaderText = "Component type", DataPropertyName = nameof(SolutionComponentItem.ComponentType) });
            _componentsGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "ObjectId", HeaderText = "Object id", DataPropertyName = nameof(SolutionComponentItem.ObjectId) });
            _componentsGrid.SelectionChanged += (s, e) => LoadLayers();

            _layersGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            _layersGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Order", HeaderText = "#", DataPropertyName = nameof(SolutionLayerItem.Order), FillWeight = 10 });
            _layersGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "SolutionName", HeaderText = "Solution", DataPropertyName = nameof(SolutionLayerItem.SolutionName) });
            _layersGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "IsManaged", HeaderText = "Managed", DataPropertyName = nameof(SolutionLayerItem.IsManaged), FillWeight = 20 });
            _layersGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "VersionText", HeaderText = "Version", DataPropertyName = nameof(SolutionLayerItem.VersionText) });
            _layersGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Publisher", HeaderText = "Publisher", DataPropertyName = nameof(SolutionLayerItem.Publisher) });

            var layersLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 20,
                Text = "Layers for selected component",
                Font = new Font(Font, FontStyle.Bold)
            };

            var rightPanel = new Panel { Dock = DockStyle.Fill };
            rightPanel.Controls.Add(_layersGrid);
            rightPanel.Controls.Add(layersLabel);

            splitContainer.Panel1.Controls.Add(_componentsGrid);
            splitContainer.Panel2.Controls.Add(rightPanel);

            Controls.Add(splitContainer);
            Controls.Add(topPanel);

            ResumeLayout(false);
        }

        private void LoadSolutions()
        {
            if (Service == null)
            {
                return;
            }

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
                        return;
                    }

                    var solutions = ((IEnumerable<Entity>)args.Result)
                        .Select(e => new SolutionListItem(e))
                        .ToList();

                    _solutionComboBox.DataSource = solutions;
                    _componentsGrid.DataSource = null;
                    _layersGrid.DataSource = null;
                }
            });
        }

        private void LoadComponents()
        {
            if (Service == null || !(_solutionComboBox.SelectedItem is SolutionListItem solution))
            {
                return;
            }

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
                        return;
                    }

                    var components = ((IEnumerable<Entity>)args.Result)
                        .Select(e => new SolutionComponentItem(e))
                        .OrderBy(c => c.ComponentType)
                        .ToList();

                    _componentsGrid.DataSource = components;
                    _layersGrid.DataSource = null;
                }
            });
        }

        private void LoadLayers()
        {
            if (Service == null || !(_componentsGrid.CurrentRow?.DataBoundItem is SolutionComponentItem component))
            {
                _layersGrid.DataSource = null;
                return;
            }

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

                    _layersGrid.DataSource = layers;
                }
            });
        }
    }
}
