using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;

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
}