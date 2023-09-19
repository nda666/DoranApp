using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace DoranApp.Components
{
    public partial class CurrencyTextBox : TextBox
    {
        private decimal unformattedValue = 0;

        public CurrencyTextBox()
        {
            this.KeyPress += NumericTextBox_KeyPress;
            this.TextChanged += NumericTextBox_TextChanged;
            this.Leave += NumericTextBox_Leave;
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && this.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void NumericTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Remove non-numeric characters
                string value = new string(textBox.Text.Where(char.IsDigit).ToArray());

                if (!string.IsNullOrWhiteSpace(value))
                {
                    unformattedValue = decimal.Parse(value);
                    textBox.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:N0}", unformattedValue);
                    textBox.Select(textBox.Text.Length, 0);
                }
            }
        }

        private void NumericTextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (String.IsNullOrWhiteSpace(textBox.Text))
                {
                    unformattedValue = 0;
                }
                else
                {
                    unformattedValue = decimal.Parse(textBox.Text.Replace(CultureInfo.CurrentUICulture.NumberFormat.CurrencyGroupSeparator, ""));
                }
                textBox.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:N0}", unformattedValue);
            }
        }

        // Property to get the unformatted value as decimal
        public decimal UnformattedValue
        {
            get
            {
                return unformattedValue;
            }
        }
    }
}