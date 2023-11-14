using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;

namespace DoranApp.View.CekStok;

public partial class MutasiBarangControl : UserControl
{
    private Client _Client = new Client();
    public MutasiStokData _MutasiStokData = new MutasiStokData();

    public MutasiBarangControl()
    {
        InitializeComponent();
    }

    private void MutasiBarangControl_Load(object sender, EventArgs e)
    {
        comboMasterbarang.Focus();
        dataGridView1.EnableDoubleBuffered(true);
        dataGridView1.DataSource = _MutasiStokData.GetDataTable();
        dataGridView1.Columns[1].Width = 300;
        dataGridView1.Columns[2].Width = 200;
    }

    private async Task GetMutasiStok()
    {
        button2.Enabled = false;
        _MutasiStokData.SetQuery(new
        {
            KodeBarang = comboMasterbarang.SelectedValue,
            Kodegudang = comboMastergudang.SelectedValue
        });
        await _MutasiStokData.Refresh();
        button2.Enabled = true;
    }

    private void button2_Click_1(object sender, EventArgs e)
    {
        GetMutasiStok();
    }

    private async Task GetStok()
    {
        var data = await _MutasiStokData.GetStokByBarangAndGudang((int)comboMasterbarang.SelectedValue,
            (int)comboMastergudang.SelectedValue);
        label5.Text = data.StokSby == null ? "-" : $"Stok Gudang Atas + LT3 : {data.StokSby}";
        label4.Text = $"Stok : {data.Stok}";
    }

    private void button1_Click(object sender, EventArgs e)
    {
        GetStok();
    }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var pcs = 0;
        if (dataGridView1.Rows[e.RowIndex]?.Cells[4] != null)
        {
            pcs = Convert.ToInt32(dataGridView1.Rows[e.RowIndex]?.Cells[4].Value);
        }

        if (e.ColumnIndex == 1 && e.Value != null)
        {
            string cellValue = e.Value.ToString();
            if (cellValue.StartsWith("IN -") && pcs > 0)
            {
                // Set the row's back color to green
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                // Optionally, you can set the text color to white for better visibility
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < listBoxGudang.Items.Count; i++)
        {
            listBoxGudang.SetSelected(i, true);
        }
    }

    private async void button6_Click(object sender, EventArgs e)
    {
        button6.Enabled = false;
        List<int> selectedGudang = new List<int>();
        foreach (dynamic selectedItem in listBoxGudang.SelectedItems)
        {
            // Add the ID of each selected item to the list
            selectedGudang.Add(selectedItem.Kode);
        }

        try
        {
            var res = await _Client.Get_Stok_By_Barang_And_Gudang_MassAsync(
                body: new GetStokMassRequestDto
                {
                    KodeBarang = (int)comboMasterbarang.SelectedValue,
                    Kodegudang = selectedGudang
                }
            );
            listBoxStok.Items.Clear();
            var totalStok = 0;
            foreach (var item in res)
            {
                totalStok += item.TotalJumlah;
                listBoxStok.Items.Add(new ListBoxGudangItem
                {
                    Kode = item.KodeGudang,
                    Text = $"{item.NamaGudang} : {item.TotalJumlah}",
                    Total = item.TotalJumlah
                });
            }

            label6.Text = $"Total Stok : {totalStok.ToString("N0")}";
        }
        catch (ApiException ex)
        {
            Helper.ShowErrorMessageFromResponse(ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(ex);
        }

        button6.Enabled = true;
    }

    private void button7_Click(object sender, EventArgs e)
    {
        var text = "";
        var total = 0;
        foreach (ListBoxGudangItem selectedItem in listBoxStok.Items)
        {
            text += $"{selectedItem.Text}\n";
            total += selectedItem.Total;
        }

        text += $"Total stok : {total.ToString("N0")}\n";
        var formCetak = new CetakStokForm(text);
        formCetak.ShowDialog();
    }

    private async void button9_Click(object sender, EventArgs e)
    {
        if (comboMasterbarang.SelectedValue == null)
        {
            return;
        }

        MasterbarangOptionWithSnDto brg = (MasterbarangOptionWithSnDto)comboMasterbarang.SelectedItem;
        var conf = MessageBox.Show($"Anda Yakin Ingin Melakukan Non Aktif Barang \"{brg?.BrgNama}\"?", "Non Aktif Data",
            MessageBoxButtons.YesNo);

        if (conf == DialogResult.No)
        {
            return;
        }

        button9.Enabled = false;
        try
        {
            await _Client.Nonaktif_MasterbarangAsync(Convert.ToInt32(comboMasterbarang.SelectedValue));
            MessageBox.Show("Non Aktif Barang Berhasil");
        }
        catch (ApiException ex)
        {
            Helper.ShowErrorMessageFromResponse(ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(ex);
        }

        button9.Enabled = true;
    }

    private void button8_Click(object sender, EventArgs e)
    {
        //TODO
        MessageBox.Show("WIP");
    }
}

internal class ListBoxGudangItem
{
    public int Kode { get; set; }
    public string Text { get; set; }
    public int Total { get; set; }
}