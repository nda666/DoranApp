using DoranApp.Utils;
using DoranApp.View.Pegawai;
using System;
using System.Windows.Forms;

namespace DoranApp.View.Pegawai
{
    public partial class PegawaiForm : Form
    {
        public PegawaiForm()
        {
            InitializeComponent();
        }

        private void PegawaiForm_Load(object sender, EventArgs e)
        {
            FormHelper.AddUserControlToTabPage<DivisiControl>(tabControl1, "Master Divisi");
            FormHelper.AddUserControlToTabPage<JabatanControl>(tabControl1, "Master Jabatan");
        }
    }
}
