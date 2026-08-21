using System.Drawing;

namespace SolutionLayerViewer.UI
{
    /// <summary>Shared color palette / fonts for the plugin's UI.</summary>
    internal static class Theme
    {
        public static readonly Color Accent = Color.FromArgb(0, 120, 212);
        public static readonly Color AccentDark = Color.FromArgb(0, 90, 158);
        public static readonly Color ActiveGreen = Color.FromArgb(16, 137, 62);
        public static readonly Color ManagedGray = Color.FromArgb(96, 104, 120);

        public static readonly Color HeaderBackground = Accent;
        public static readonly Color PageBackground = Color.White;
        public static readonly Color PanelBackground = Color.FromArgb(247, 248, 250);
        public static readonly Color BorderColor = Color.FromArgb(224, 226, 230);

        public static readonly Color TextPrimary = Color.FromArgb(32, 32, 32);
        public static readonly Color TextMuted = Color.FromArgb(110, 116, 128);

        public static readonly Font FontTitle = new Font("Segoe UI", 13F, FontStyle.Bold);
        public static readonly Font FontRegular = new Font("Segoe UI", 9F);
        public static readonly Font FontBold = new Font("Segoe UI", 9F, FontStyle.Bold);
        public static readonly Font FontSmall = new Font("Segoe UI", 8F);
        public static readonly Font FontSmallBold = new Font("Segoe UI", 8F, FontStyle.Bold);
    }
}
