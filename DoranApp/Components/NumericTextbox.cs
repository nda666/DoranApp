using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DoranApp.Components
{
    public partial class NumericTextbox : TextBox
    {
        private const int WM_CHAR = 0x0102;
        public NumericTextbox()
        {
            InitializeComponent();
        }

        public NumericTextbox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CHAR)
            {
                char c = (char)m.WParam.ToInt32();

                // Allow digits, decimal point, and necessary control characters
                if (!char.IsDigit(c) && !char.IsControl(c))
                {
                    m.Result = IntPtr.Zero;
                    return;
                }
            }

            base.WndProc(ref m);
        }
    }
}
