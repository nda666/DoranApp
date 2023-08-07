using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Utils
{
    internal class FormHelper
    {
        public static void AddUserControlToTabPage<T>(TabControl tabControl, string title = "") where T : UserControl, new()
        {
            var salesControl = new T();
            salesControl.Dock = DockStyle.Fill;
            TabPage myTabPage = new TabPage();//Create new tabpage
            myTabPage.Text = title;
            myTabPage.Controls.Add(salesControl);
            tabControl.TabPages.Add(myTabPage);
        }
    }
}
