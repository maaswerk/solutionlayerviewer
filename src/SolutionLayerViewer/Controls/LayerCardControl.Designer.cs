using System.Drawing;
using System.Windows.Forms;
using SolutionLayerViewer.UI;

namespace SolutionLayerViewer.Controls
{
    partial class LayerCardControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private Panel accentStrip;
        private Label orderBadge;
        private Label solutionNameLabel;
        private Label statusBadge;
        private Label detailLabel;

        private void InitializeComponent()
        {
            this.accentStrip = new Panel();
            this.orderBadge = new Label();
            this.solutionNameLabel = new Label();
            this.statusBadge = new Label();
            this.detailLabel = new Label();
            this.SuspendLayout();

            //
            // accentStrip
            //
            this.accentStrip.Dock = DockStyle.Left;
            this.accentStrip.Width = 6;
            this.accentStrip.BackColor = Theme.BorderColor;

            //
            // orderBadge
            //
            this.orderBadge.Size = new Size(24, 24);
            this.orderBadge.Location = new Point(16, 10);
            this.orderBadge.TextAlign = ContentAlignment.MiddleCenter;
            this.orderBadge.Font = Theme.FontSmallBold;
            this.orderBadge.ForeColor = Color.White;
            this.orderBadge.BackColor = Theme.ManagedGray;

            //
            // solutionNameLabel
            //
            this.solutionNameLabel.Location = new Point(50, 8);
            this.solutionNameLabel.Size = new Size(220, 20);
            this.solutionNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.solutionNameLabel.Font = Theme.FontBold;
            this.solutionNameLabel.ForeColor = Theme.TextPrimary;
            this.solutionNameLabel.AutoEllipsis = true;

            //
            // statusBadge
            //
            this.statusBadge.Location = new Point(50, 30);
            this.statusBadge.Size = new Size(150, 18);
            this.statusBadge.TextAlign = ContentAlignment.MiddleCenter;
            this.statusBadge.Font = Theme.FontSmallBold;
            this.statusBadge.ForeColor = Color.White;
            this.statusBadge.BackColor = Theme.ManagedGray;

            //
            // detailLabel
            //
            this.detailLabel.Location = new Point(50, 51);
            this.detailLabel.Size = new Size(300, 16);
            this.detailLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.detailLabel.Font = Theme.FontSmall;
            this.detailLabel.ForeColor = Theme.TextMuted;
            this.detailLabel.AutoEllipsis = true;

            //
            // LayerCardControl
            //
            this.Controls.Add(this.detailLabel);
            this.Controls.Add(this.statusBadge);
            this.Controls.Add(this.solutionNameLabel);
            this.Controls.Add(this.orderBadge);
            this.Controls.Add(this.accentStrip);
            this.BackColor = Color.White;
            this.Height = 76;
            this.Margin = new Padding(0, 0, 0, 8);
            this.Padding = new Padding(1);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
