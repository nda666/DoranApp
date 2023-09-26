using System.Windows.Forms;

namespace DoranApp
{
    public class Helper
    {
        public static string GetSelectedRadioButtonTag(GroupBox groupBox)
        {
            foreach (Control control in groupBox.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    return radioButton.Tag?.ToString();
                }
            }

            return null; // No radio button selected
        }

        public static string TipeTempoToString(sbyte tipeTempo)
        {
            switch (tipeTempo)
            {
                case > 0:
                    return $"{tipeTempo.ToString()} Hari";
                default:
                    return "Cash";
            }
        }
    }
}