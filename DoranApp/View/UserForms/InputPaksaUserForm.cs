using System;
using System.Windows.Forms;

namespace DoranApp.View.UserForms;

public partial class InputPaksaUserForm : Form
{
    public InputPaksaUserForm()
    {
        InitializeComponent();
    }

    public DialogResult CustomDialogResult { get; private set; }

    private void InputPaksaForm_Load(object sender, EventArgs e)
    {
    }

    private void buttonOk_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }


    private void InputPaksaUserForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (DialogResult == DialogResult.OK)
        {
            // e.Cancel = true; // Cancel the form closing event
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}