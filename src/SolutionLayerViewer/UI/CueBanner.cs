using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SolutionLayerViewer.UI
{
    /// <summary>Sets the native "cue banner" (placeholder text) on a TextBox.</summary>
    internal static class CueBanner
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern System.IntPtr SendMessage(System.IntPtr hWnd, int msg, System.IntPtr wParam, string lParam);

        public static void Set(TextBox textBox, string text)
        {
            // Accessing .Handle forces the native window to be created if it isn't yet.
            SendMessage(textBox.Handle, EM_SETCUEBANNER, System.IntPtr.Zero, text);
        }
    }
}
