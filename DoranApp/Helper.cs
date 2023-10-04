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

        public static int TipeTempoValueToIndex(int tipeTempo)
        {
            var subDay = -1;
            switch (tipeTempo)
            {
                case 0:
                    subDay = 0;
                    break;
                case 7:
                    subDay = 1;
                    break;
                case 14:
                    subDay = 2;
                    break;
                case 30:
                    subDay = 3;
                    break;
                case 60:
                    subDay = 4;
                    break;
                default:
                    break;
            }

            return subDay;
        }
    }
}