using System;
using System.Windows.Forms;

namespace DoranApp.View.UserForms;

public partial class ApplicationUpdateForm : Form
{
    public ApplicationUpdateForm(string message, bool mandatory)
    {
        InitializeComponent();
        label1.Text = message;
        button2.Visible = !mandatory;
    }

    private void ApplicationUpdateForm_Load(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Yes;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
    }
}