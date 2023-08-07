using DoranApp.View.Pegawai;
using System;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class PegawaiForm : Form
    {
        public PegawaiForm()
        {
            InitializeComponent();
        }

        private void PegawaiForm_Load(object sender, EventArgs e)
        {
            var myUserControl = new DivisiControl();
            myUserControl.Dock = DockStyle.Fill;
            var myTabPage = new TabPage();//Create new tabpage
            myTabPage.Controls.Add(myUserControl);
            myTabPage.Text = "Master Divisi";
            tabControl1.TabPages.Add(myTabPage);
        }
    }
}
