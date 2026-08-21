using System.Drawing;
using System.Windows.Forms;
using SolutionLayerViewer.UI;

namespace SolutionLayerViewer
{
    partial class PluginControl
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

        private Panel headerPanel;
        private LayersLogoPanel logoPanel;
        private Label titleLabel;
        private Label subtitleLabel;
        private Label solutionLabel;
        private ComboBox solutionComboBox;
        private Button refreshButton;
        private TextBox filterTextBox;

        private Panel statusPanel;
        private Label statusLabel;

        private SplitContainer mainSplit;
        private Label componentsHeaderLabel;
        private DataGridView componentsGrid;
        private Label layersHeaderLabel;
        private FlowLayoutPanel layersFlowPanel;
        private Label emptyLayersLabel;

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            this.logoPanel = new LayersLogoPanel();
            this.titleLabel = new Label();
            this.subtitleLabel = new Label();
            this.solutionLabel = new Label();
            this.solutionComboBox = new ComboBox();
            this.refreshButton = new Button();
            this.filterTextBox = new TextBox();

            this.statusPanel = new Panel();
            this.statusLabel = new Label();

            this.mainSplit = new SplitContainer();
            this.componentsHeaderLabel = new Label();
            this.componentsGrid = new DataGridView();
            this.layersHeaderLabel = new Label();
            this.layersFlowPanel = new FlowLayoutPanel();
            this.emptyLayersLabel = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).BeginInit();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.componentsGrid)).BeginInit();
            this.SuspendLayout();

            //
            // headerPanel
            //
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 84;
            this.headerPanel.BackColor = Theme.HeaderBackground;
            this.headerPanel.Controls.Add(this.logoPanel);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Controls.Add(this.solutionLabel);
            this.headerPanel.Controls.Add(this.solutionComboBox);
            this.headerPanel.Controls.Add(this.refreshButton);
            this.headerPanel.Controls.Add(this.filterTextBox);

            //
            // logoPanel
            //
            this.logoPanel.Location = new Point(16, 8);

            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new Point(56, 8);
            this.titleLabel.Font = Theme.FontTitle;
            this.titleLabel.ForeColor = Color.White;
            this.titleLabel.Text = "Solution Layer Viewer";

            //
            // subtitleLabel
            //
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.Location = new Point(58, 32);
            this.subtitleLabel.Font = Theme.FontSmall;
            this.subtitleLabel.ForeColor = Color.FromArgb(220, 235, 250);
            this.subtitleLabel.Text = "See which solutions layer each component";

            //
            // solutionLabel
            //
            this.solutionLabel.AutoSize = true;
            this.solutionLabel.Location = new Point(16, 52);
            this.solutionLabel.Font = Theme.FontRegular;
            this.solutionLabel.ForeColor = Color.White;
            this.solutionLabel.Text = "Solution:";

            //
            // solutionComboBox
            //
            this.solutionComboBox.Location = new Point(78, 48);
            this.solutionComboBox.Size = new Size(300, 24);
            this.solutionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.solutionComboBox.FlatStyle = FlatStyle.Flat;

            //
            // refreshButton
            //
            this.refreshButton.Location = new Point(388, 47);
            this.refreshButton.Size = new Size(92, 26);
            this.refreshButton.Text = "⟳ Refresh";
            this.refreshButton.FlatStyle = FlatStyle.Flat;
            this.refreshButton.FlatAppearance.BorderColor = Theme.AccentDark;
            this.refreshButton.BackColor = Theme.AccentDark;
            this.refreshButton.ForeColor = Color.White;
            this.refreshButton.Cursor = Cursors.Hand;

            //
            // filterTextBox
            //
            this.filterTextBox.Location = new Point(496, 48);
            this.filterTextBox.Size = new Size(240, 24);
            this.filterTextBox.Font = Theme.FontRegular;

            //
            // statusPanel
            //
            this.statusPanel.Dock = DockStyle.Bottom;
            this.statusPanel.Height = 28;
            this.statusPanel.BackColor = Theme.PanelBackground;
            this.statusPanel.Controls.Add(this.statusLabel);

            //
            // statusLabel
            //
            this.statusLabel.Dock = DockStyle.Fill;
            this.statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.statusLabel.Padding = new Padding(12, 0, 0, 0);
            this.statusLabel.Font = Theme.FontSmall;
            this.statusLabel.ForeColor = Theme.TextMuted;
            this.statusLabel.Text = "Connect to an environment to load solutions.";

            //
            // mainSplit
            //
            this.mainSplit.Dock = DockStyle.Fill;
            this.mainSplit.Orientation = Orientation.Vertical;
            this.mainSplit.SplitterWidth = 6;
            this.mainSplit.BackColor = Theme.BorderColor;
            this.mainSplit.SplitterDistance = 460;

            //
            // mainSplit.Panel1 -> components
            //
            this.mainSplit.Panel1.Controls.Add(this.componentsGrid);
            this.mainSplit.Panel1.Controls.Add(this.componentsHeaderLabel);
            this.mainSplit.Panel1.BackColor = Theme.PageBackground;

            //
            // componentsHeaderLabel
            //
            this.componentsHeaderLabel.Dock = DockStyle.Top;
            this.componentsHeaderLabel.Height = 30;
            this.componentsHeaderLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.componentsHeaderLabel.Padding = new Padding(10, 0, 0, 0);
            this.componentsHeaderLabel.Font = Theme.FontBold;
            this.componentsHeaderLabel.ForeColor = Theme.TextPrimary;
            this.componentsHeaderLabel.BackColor = Theme.PanelBackground;
            this.componentsHeaderLabel.Text = "Components";

            //
            // componentsGrid
            //
            this.componentsGrid.Dock = DockStyle.Fill;
            this.componentsGrid.BackgroundColor = Theme.PageBackground;
            this.componentsGrid.BorderStyle = BorderStyle.None;
            this.componentsGrid.ReadOnly = true;
            this.componentsGrid.AllowUserToAddRows = false;
            this.componentsGrid.AllowUserToDeleteRows = false;
            this.componentsGrid.AllowUserToResizeRows = false;
            this.componentsGrid.RowHeadersVisible = false;
            this.componentsGrid.MultiSelect = false;
            this.componentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.componentsGrid.AutoGenerateColumns = false;
            this.componentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.componentsGrid.EnableHeadersVisualStyles = false;
            this.componentsGrid.GridColor = Theme.BorderColor;
            this.componentsGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.componentsGrid.RowTemplate.Height = 28;
            this.componentsGrid.Font = Theme.FontRegular;
            this.componentsGrid.ColumnHeadersDefaultCellStyle.BackColor = Theme.PanelBackground;
            this.componentsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextPrimary;
            this.componentsGrid.ColumnHeadersDefaultCellStyle.Font = Theme.FontBold;
            this.componentsGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.componentsGrid.ColumnHeadersHeight = 30;
            this.componentsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 230, 250);
            this.componentsGrid.DefaultCellStyle.SelectionForeColor = Theme.TextPrimary;
            this.componentsGrid.AlternatingRowsDefaultCellStyle.BackColor = Theme.PanelBackground;

            //
            // mainSplit.Panel2 -> layers
            //
            this.mainSplit.Panel2.Controls.Add(this.layersFlowPanel);
            this.mainSplit.Panel2.Controls.Add(this.layersHeaderLabel);
            this.mainSplit.Panel2.BackColor = Theme.PanelBackground;

            //
            // layersHeaderLabel
            //
            this.layersHeaderLabel.Dock = DockStyle.Top;
            this.layersHeaderLabel.Height = 30;
            this.layersHeaderLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.layersHeaderLabel.Padding = new Padding(10, 0, 0, 0);
            this.layersHeaderLabel.Font = Theme.FontBold;
            this.layersHeaderLabel.ForeColor = Theme.TextPrimary;
            this.layersHeaderLabel.BackColor = Theme.PanelBackground;
            this.layersHeaderLabel.Text = "Layer stack";

            //
            // layersFlowPanel
            //
            this.layersFlowPanel.Dock = DockStyle.Fill;
            this.layersFlowPanel.AutoScroll = true;
            this.layersFlowPanel.FlowDirection = FlowDirection.TopDown;
            this.layersFlowPanel.WrapContents = false;
            this.layersFlowPanel.BackColor = Theme.PanelBackground;
            this.layersFlowPanel.Padding = new Padding(10);
            this.layersFlowPanel.Controls.Add(this.emptyLayersLabel);

            //
            // emptyLayersLabel
            //
            this.emptyLayersLabel.AutoSize = true;
            this.emptyLayersLabel.Font = Theme.FontRegular;
            this.emptyLayersLabel.ForeColor = Theme.TextMuted;
            this.emptyLayersLabel.Margin = new Padding(6, 12, 6, 6);
            this.emptyLayersLabel.Text = "Select a component on the left to see its layer stack.";

            //
            // PluginControl
            //
            this.BackColor = Theme.PageBackground;
            this.Controls.Add(this.mainSplit);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.headerPanel);
            this.Name = "PluginControl";
            this.Size = new Size(900, 600);

            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplit)).EndInit();
            this.mainSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.componentsGrid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
