using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SolutionLayerViewer.UI
{
    /// <summary>Gives a control rounded corners by clipping it to a rounded-rectangle region.</summary>
    internal static class RoundedControlExtensions
    {
        public static void ApplyRoundedRegion(this Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
            {
                return;
            }

            var diameter = radius * 2;
            var rect = new Rectangle(0, 0, control.Width, control.Height);

            using (var path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();

                control.Region?.Dispose();
                control.Region = new Region(path);
            }
        }

        /// <summary>Wires ApplyRoundedRegion to run on every resize, so the control stays rounded as it grows/shrinks.</summary>
        public static void KeepRounded(this Control control, int radius)
        {
            control.Resize += (s, e) => control.ApplyRoundedRegion(radius);
            control.ApplyRoundedRegion(radius);
        }
    }
}
