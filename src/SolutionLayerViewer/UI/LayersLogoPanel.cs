using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SolutionLayerViewer.UI
{
    /// <summary>Small hand-drawn mark: three stacked layers, used as the plugin's header logo.</summary>
    internal sealed class LayersLogoPanel : Panel
    {
        public LayersLogoPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
            Size = new Size(30, 30);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            DrawLayer(e.Graphics, 0, 16, 90);
            DrawLayer(e.Graphics, 6, 8, 160);
            DrawLayer(e.Graphics, 12, 0, 255);
        }

        private void DrawLayer(Graphics g, int yOffset, int xOffset, int alpha)
        {
            var rect = new Rectangle(xOffset, yOffset, 22, 10);
            using (var path = RoundedRect(rect, 3))
            using (var brush = new SolidBrush(Color.FromArgb(alpha, Color.White)))
            {
                g.FillPath(brush, path);
            }
        }

        private static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            var d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
