using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.BASE_API_URL))
            {
                textBox1.Text = Properties.Settings.Default.BASE_API_URL;
                textBox1.Select(textBox1.Text.Length, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BASE_API_URL = textBox1.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Setting berhasil disimpan");
            this.Close();
        }
    }
}
