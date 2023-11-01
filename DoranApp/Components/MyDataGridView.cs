using System.Windows.Forms;

namespace DoranApp.Components;

public partial class MyDataGridView : DataGridView
{
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
        if ((keyData == Keys.Enter) &&
            (EditingControl != null) &&
            (CurrentCell.RowIndex == RowCount - 1)) {

            SendKeys.Send("{TAB}");
            return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }
}