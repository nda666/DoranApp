using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;

namespace DoranApp.View.CekStok;

public partial class MutasiBarangControl : UserControl
{
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
        if (e.ColumnIndex == 1 && e.Value != null)
        {
            string cellValue = e.Value.ToString();
            if (cellValue.StartsWith("IN -"))
            {
                // Set the row's back color to green
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                // Optionally, you can set the text color to white for better visibility
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }
    }
}