using System;
using System.Windows.Forms;

namespace DoranApp.View.CekStok;

public partial class CetakStokForm : Form
{
    public CetakStokForm(string stokText)
    {
        InitializeComponent();
        richTextBox1.Text = stokText;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        this.Close();
    }
}