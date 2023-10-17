using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Forms;
using DoranApp.Utils;
using Newtonsoft.Json;

namespace DoranApp
{
    public class Helper
    {
        public static void ShowErrorMessageFromResponse(ApiException apiException)
        {
            try
            {
                dynamic resp = JsonConvert.DeserializeObject<ExpandoObject>(apiException.Response);
                var hadMessage = ((IDictionary<string, object>)resp).ContainsKey("message");
                MessageBox.Show(hadMessage ? resp.message : apiException.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(apiException.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

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