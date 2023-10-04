using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data.Laporan;
using DoranApp.DataGlobal;
using DoranApp.Utils;

namespace DoranApp.View;

public partial class LaporanPenjualanBarangBySales : Form
{
    public enum TransaksiBySalesShowMode
    {
        BY_OMZET = 0,
        BY_PCS = 1
    }

    public enum TransaksiBySalesTipeGroup
    {
        GROUP_BY_SALES = 0,
        GROUP_BY_CHANNEL = 1,
    }

    private List<DkategoribarangOptionDto> _DkategoribarangOptions = new List<DkategoribarangOptionDto>();

    private bool _FetchRun = false;
    private List<HkategoribarangOptionDto> _HkategoribarangOptions = new List<HkategoribarangOptionDto>();

    private IDisposable _HkategoribarangSubscribe;

    private LaporanTransaksiBySalesData _laporanTransaksi = new LaporanTransaksiBySalesData();
    private List<MasterchannelsalesOptionDto> _MasterchannelsalesOptions = new List<MasterchannelsalesOptionDto>();

    private IDisposable _MasterchannelsalesSubscribe;
    private List<MastergudangOptionDto> _MastergudangOptions = new List<MastergudangOptionDto>();
    private IDisposable _MastergudangSubscribe;
    private List<MastertimsalesOptionDto> _MastertimsalesOptions = new List<MastertimsalesOptionDto>();
    private List<SalesOptionDto> _SalesOptions = new List<SalesOptionDto>();

    private TransaksiBySalesTipeGroup _TipeGroup = TransaksiBySalesTipeGroup.GROUP_BY_SALES;


    public LaporanPenjualanBarangBySales()
    {
        InitializeComponent();
    }

    private async Task SubscribeChannelSales()
    {
        _MasterchannelsalesSubscribe = FetchMasterchannelsalesOption.Subscribe(data =>
        {
            _MasterchannelsalesOptions = data.Prepend(new MasterchannelsalesOptionDto()
            {
                Kode = null,
                Nama = "Semua Channel Sales"
            }).ToList();
            _MastertimsalesOptions = data.SelectMany(e => e.Mastertimsales).Prepend(new MastertimsalesOptionDto()
            {
                Kode = null,
                Nama = "Semua Tim Sales"
            }).ToList();

            comboFilterMasterchannelsales.DataSource = _MasterchannelsalesOptions;
            comboFilterMastertimsales.DataSource = _MastertimsalesOptions;
        });

        FetchMasterchannelsalesOption.Run();
    }

    private async Task SubscribeHkategoribarang()
    {
        _HkategoribarangSubscribe = FetchHkategoribarangOption.Subscribe(data =>
        {
            _HkategoribarangOptions = data.Prepend(new HkategoribarangOptionDto()
            {
                Kodeh = null,
                Nama = "Semua Brand"
            }).ToList();
            _DkategoribarangOptions = data.SelectMany(e => e.Dkategoribarang)
                .OrderBy(e => e.Nama)
                .Prepend(new DkategoribarangOptionDto()
                {
                    Koded = null,
                    Kodeh = null,
                    Nama = "Semua Sub Brand"
                })
                .ToList();
            comboFilterDkategoribarang.DataSource = _DkategoribarangOptions;
            comboFilterHkategoribarang.DataSource = _HkategoribarangOptions;
        });
        FetchHkategoribarangOption.Run();
    }

    private void LaporanPenjualanBarangBySales_Load(object sender, EventArgs e)
    {
        SubscribeHkategoribarang();
        SubscribeChannelSales();
        var bs = new BindingSource();
        var ds = _laporanTransaksi.GetDataTable();
        bs.DataSource = ds;
        bindingNavigator1.BindingSource = bs;
        dataGridView1.EnableDoubleBuffered(true);
        dataGridView1.DataSource = ds;
        dataGridView1.Columns[0].Width = 300;
        dataGridView1.Columns[1].DefaultCellStyle.Format = "N0";
        dataGridView1.Columns[2].DefaultCellStyle.Format = "0.00\\%";
        dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
    }

    private async Task FetchLaporan()
    {
        if (_FetchRun)
        {
            return;
        }

        _FetchRun = true;
        labelLoading.Visible = true;
        try
        {
            _laporanTransaksi.SetQuery(new
            {
                minDate = datePickerFilterMin.Value.ToString("MM/dd/yyyy"),
                maxDate = datePickerFilterMax.Value.ToString("MM/dd/yyyy"),
                NamaBarang = textBoxFilterNamaBarang.Text.Trim(),

                KodeTimSales = comboFilterMastertimsales.SelectedValue?.ToString(),
                KodeChannelSales = comboFilterMasterchannelsales.SelectedValue?.ToString(),
                KodeBrand = comboFilterHkategoribarang.SelectedValue?.ToString(),
                KodeKategori = comboFilterDkategoribarang.SelectedValue?.ToString(),
                JurnalPenjualan = Helper.GetSelectedRadioButtonTag(groupBoxJurnalPenjualan),
                ShowMode = Helper.GetSelectedRadioButtonTag(groupBoxShowMode),
                TipeGroup = _TipeGroup
            });
            await _laporanTransaksi.Refresh();
            var jumlah = _laporanTransaksi.GetData().Sum(e => e.SumTotal).ToString();
            labelJumlahSum.Text = $"Total: {jumlah}";
            var totalPersen = _laporanTransaksi.GetData().Sum(e => e.Persen).ToString();
            toolStripLabel2.Text = totalPersen;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        _FetchRun = false;
        labelLoading.Visible = false;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        this._TipeGroup = TransaksiBySalesTipeGroup.GROUP_BY_SALES;
        FetchLaporan();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        this._TipeGroup = TransaksiBySalesTipeGroup.GROUP_BY_CHANNEL;
        FetchLaporan();
    }

    private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
        if (dataGridView1.Rows.Count > 0)
        {
            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
        }

        dataGridView1.ClearSelection();
    }
}