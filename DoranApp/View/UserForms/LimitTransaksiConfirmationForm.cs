using System;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Exceptions;
using DoranApp.Utils;

namespace DoranApp.View.UserForms;

public partial class LimitTransaksiConfirmationForm : Form
{
    private int? _KodeEdit = null;
    private dynamic _ReqData;
    private TransaksiData _TransaksiData;

    public LimitTransaksiConfirmationForm(string limitMessage, int? kodeEdit, TransaksiData transaksiData,
        dynamic reqData)
    {
        InitializeComponent();
        label1.Text = limitMessage;
        _ReqData = reqData;
        _TransaksiData = transaksiData;
        _KodeEdit = kodeEdit;
        textBox1.Text = Helper.GetKeyLimitTagihan();

#if DEBUG
        textBox2.Text = Helper.DekripKeyLimitTagihan(textBox1.Text);
        Console.WriteLine($"Kode Enkrip: {Helper.DekripKeyLimitTagihan(textBox1.Text)}");
#endif
    }

    private async void button2_Click(object sender, EventArgs e)
    {
        if (textBox2.Text.Trim() == "")
        {
            toolTip1.ShowAlways = true;
            toolTip1.ToolTipIcon = ToolTipIcon.Error;
            toolTip1.ShowAlways = true;
            toolTip1.ToolTipTitle = "Error";
            toolTip1.Show("Key limit tidak sesuai", this, textBox2.Left, textBox2.Top - 10);
            return;
        }

        if (textBox2.Text != Helper.DekripKeyLimitTagihan(textBox1.Text))
        {
            // errorProvider1.SetError(textBox2, "Key limit tidak sesuai");

            toolTip1.ShowAlways = true;
            toolTip1.ToolTipIcon = ToolTipIcon.Error;
            toolTip1.ShowAlways = true;
            toolTip1.ToolTipTitle = "Error";
            toolTip1.Show("Key limit tidak sesuai", this, textBox2.Left, textBox2.Top - 10);
            return;
        }

        ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
        try
        {
            var res = await _TransaksiData.CreateOrUpdate(_KodeEdit == null ? null : _KodeEdit.ToString(), _ReqData);
            DialogResult = DialogResult.OK;
        }
        catch (RestException ex)
        {
            if (ex.ErrorType != "LIMIT_TRANSAKSI")
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                label1.Text = ex.Data?.limitMessage;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
    }

    private void button3_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }

    private void textBox2_KeyDown(object sender, KeyEventArgs e)
    {
        toolTip1.Hide(textBox2);
    }


    private void LimitTransaksiConfirmationForm_Load(object sender, EventArgs e)
    {
    }
}