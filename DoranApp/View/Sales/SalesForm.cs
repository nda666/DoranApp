using System;
using System.Windows.Forms;
using DoranApp.Utils;

namespace DoranApp.View
{
    public partial class SalesForm : Form
    {
        public SalesForm()
        {
            InitializeComponent();
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            FormHelper.AddUserControlToTabPage<SalesControl>(tabControl1, "Master Sales");
            FormHelper.AddUserControlToTabPage<SalesChannelControl>(tabControl1, "Master Channel Sales");
            FormHelper.AddUserControlToTabPage<SalesTeamControl>(tabControl1, "Master Tim Sales");
        }
    }
}