using System.Windows.Forms;

namespace DoranApp.Components;

public partial class MyDataGridView : DataGridView
{
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if ((keyData == Keys.Enter) &&
            (EditingControl != null) && (EditingControl is DataGridViewTextBoxEditingControl))
        {
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }
}