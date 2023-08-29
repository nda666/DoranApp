using System.Windows.Forms;

namespace DoranApp.Utils
{
    internal class ButtonToggleHelper
    {
        public static void EnableButtonsByTag(Control control, string tag)
        {
            SetButtonsEnabledByTag(control, tag, true);
        }

        // Disable buttons with a specific tag within a control and its children
        public static void DisableButtonsByTag(Control control, string tag)
        {
            SetButtonsEnabledByTag(control, tag, false);
        }

        private static void SetButtonsEnabledByTag(Control control, string tag, bool enabled)
        {
            foreach (Control c in control.Controls)
            {
                if (c is Button button && button.Tag?.ToString() == tag)
                {
                    button.Enabled = enabled;
                }

                // Recursively process child controls if the current control has children
                if (c.HasChildren)
                {
                    SetButtonsEnabledByTag(c, tag, enabled);
                }
            }
        }
    }
}