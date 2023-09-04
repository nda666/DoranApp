using System;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<UserForm>();
        }

        private void HomeForm_Resize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<RolesForm>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((_Container)MdiParent)._stopSync = false;
            ((_Container)MdiParent)._runSync = false;
            ((_Container)MdiParent).syncDb();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<TransaksiForm>();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<OrderInputForm>();
        }
    }
}