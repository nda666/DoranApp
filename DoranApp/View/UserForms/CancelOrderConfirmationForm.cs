using System;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;

namespace DoranApp.View.UserForms;

public partial class CancelOrderConfirmationForm : Form
{
    private OrderData _OrderData;
    private int Kodecancel;
    private int Koded;
    private int Kodeh;

    public CancelOrderConfirmationForm(OrderData orderData, int kodeh, int koded, sbyte kodecancel)
    {
        this._OrderData = orderData;
        this.Koded = koded;
        this.Kodeh = kodeh;
        this.Kodecancel = kodecancel;
        InitializeComponent();
    }

    private async void button2_Click(object sender, EventArgs e)
    {
        if (textBoxSebab.Text.Trim() == "")
        {
            errorProvider1.SetError(textBoxSebab, "Sebab harus di isi");
            return;
        }

        buttonSimpan.Enabled = false;
        try
        {
            await _OrderData.CancelOrderDetail(Kodeh, new CancelOrderDetailDto
            {
                Keterangancancel = textBoxSebab.Text,
                Koded = Koded,
                Kodecancel = Kodecancel
            });
            DialogResult = DialogResult.OK;
        }
        catch (ApiException ex)
        {
            Helper.ShowErrorMessageFromResponse(ex);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        buttonSimpan.Enabled = true;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}