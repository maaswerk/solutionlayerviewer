using System.Drawing;
using SolutionLayerViewer.Models;
using SolutionLayerViewer.UI;

namespace SolutionLayerViewer.Controls
{
    /// <summary>One row of the "layer stack" view: a single solution layer touching a component.</summary>
    public partial class LayerCardControl : System.Windows.Forms.UserControl
    {
        public LayerCardControl()
        {
            InitializeComponent();

            this.KeepRounded(8);
            orderBadge.KeepRounded(12);
            statusBadge.KeepRounded(9);
        }

        public void SetLayer(SolutionLayerItem layer, bool isTopLayer)
        {
            orderBadge.Text = layer.Order.ToString();
            solutionNameLabel.Text = layer.SolutionName;
            detailLabel.Text = string.IsNullOrEmpty(layer.Publisher)
                ? $"v{layer.VersionText}"
                : $"v{layer.VersionText}  ·  {layer.Publisher}";

            if (!layer.IsManaged)
            {
                statusBadge.Text = "ACTIVE (Unmanaged)";
                statusBadge.BackColor = Theme.ActiveGreen;
            }
            else
            {
                statusBadge.Text = "Managed";
                statusBadge.BackColor = Theme.ManagedGray;
            }

            accentStrip.BackColor = isTopLayer ? Theme.Accent : Theme.BorderColor;
            orderBadge.BackColor = isTopLayer ? Theme.Accent : Theme.ManagedGray;
            BackColor = isTopLayer ? Color.FromArgb(240, 247, 255) : Color.White;
        }
    }
}
