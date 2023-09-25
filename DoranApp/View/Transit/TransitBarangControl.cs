using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;

namespace DoranApp.View;

public partial class TransitBarangControl : UserControl
{
    public TransitData _TransitData = new TransitData();
    public bool AnyDataProcessRun = false;
    public int? KodeEdit = null;

    public TransitBarangControl()
    {
        InitializeComponent();
    }

    public async Task GetTransitByNamaGudangTujuan()
    {
        if (AnyDataProcessRun)
        {
            return;
        }

        AnyDataProcessRun = true;
        ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
        try
        {
            _TransitData.SetQuery(new
            {
                NamaGudangTujuan = textBoxFilterNamaGudangTujuan.Text.Trim()
            });
            await _TransitData.Refresh();
        }
        catch (Exception ex)
        {
            ConsoleDump.Extensions.Dump(ex);
            MessageBox.Show(ex.Message);
        }

        ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        AnyDataProcessRun = false;
    }

    public async Task GetTransit()
    {
        if (AnyDataProcessRun)
        {
            return;
        }

        AnyDataProcessRun = true;
        ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
        try
        {
            _TransitData.SetQuery(new
            {
                MinDate = dateTimePickerFilterMin.Value.ToString("MM-dd-yyyy"),
                MaxDate = dateTimePickerFilterMax.Value.ToString("MM-dd-yyyy"),
                Kodegudang = comboBoxFilterGudang.SelectedValue?.ToString(),
                KodeGudangTujuan = comboBoxFilterGudangTujuan.SelectedValue?.ToString(),
                Kodepenyiap = comboBoxFilterPenyiap.SelectedValue?.ToString()
            });
            await _TransitData.Refresh();
        }
        catch (Exception ex)
        {
            ConsoleDump.Extensions.Dump(ex);
            MessageBox.Show(ex.Message);
        }

        ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        AnyDataProcessRun = false;
    }

    private void TransitBarangControl_Load(object sender, EventArgs e)
    {
        dataGridView1.DataSource = _TransitData.GetDataTable();
    }

    private void button4_Click(object sender, EventArgs e)
    {
        GetTransit();
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
        {
            return;
        }

        dataGridView2.Rows.Clear();
        try
        {
            var kodeh = (int)dataGridView1.SelectedRows[0].Cells["Kodet"].Value;

            var htransit = _TransitData.GetData().Where(x => x.KodeT == kodeh).FirstOrDefault();

            if (htransit == null)
            {
                return;
            }

            foreach (var d in htransit.Dtransit)
            {
                var index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells["Pcs"].Value = d.Jumlah;
                dataGridView2.Rows[index].Cells["NamaBarang"].Value = d.Masterbarang?.BrgNama;
                dataGridView2.Rows[index].Cells["Rak"].Value = d.Masterbarang?.Mastertipebarang?.Nama;
                dataGridView2.Rows[index].Cells["Penerima"].Value = d.Namapenerima;
                dataGridView2.Rows[index].Cells["SN"].Value = d.NmrSn;
                dataGridView2.Rows[index].Cells["SudahDicek"].Value = d.Sudahdicek;
                dataGridView2.Rows[index].Cells["Koded"].Value = d.Koded;
                dataGridView2.Rows[index].Cells["Kodet"].Value = d.Kodet;
            }
        }
        catch (Exception ex)
        {
            ConsoleDump.Extensions.Dump(ex);
        }
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        if (AnyDataProcessRun)
        {
            return;
        }

        AnyDataProcessRun = true;
        LoadingButtonHelper.SetLoadingState(buttonSimpan, true);
        try
        {
            var res = await _TransitData.CreateOrUpdate(null, new
            {
                TglTrans = datePickerTglTransit.Value.ToString("MM/dd/yyyy"),
                Kodegudang = comboBoxMastergudang.SelectedValue?.ToString(),
                KodeGudangTujuan = comboBoxMastergudangTujuan.SelectedValue?.ToString(),
                Kodepenyiap = comboBoxPenyiapOrder.SelectedValue?.ToString(),
                Keterangan = textBoxKeterangan.Text.Trim()
            });

            GetTransit();
            MessageBox.Show("\"Header Transit\" berhasil disimpan", "Sukses", MessageBoxButtons.OK);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        AnyDataProcessRun = false;
        LoadingButtonHelper.SetLoadingState(buttonSimpan, false);
    }

    private void button6_Click(object sender, EventArgs e)
    {
        GetTransitByNamaGudangTujuan();
    }
}