using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data.Laporan;
using DoranApp.DataGlobal;
using DoranApp.Models;

namespace DoranApp.View;

public partial class LaporanPenjualanBarangBySales : Form
{
    public enum TransaksiBySalesTipeGroup
    {
        GROUP_BY_SALES=0,
        GROUP_BY_CHANNEL=1,
    }
    
    public enum TransaksiBySalesShowMode
    {
        BY_OMZET = 0,
        BY_PCS = 1
    }
    
    private TransaksiBySalesTipeGroup _TipeGroup = TransaksiBySalesTipeGroup.GROUP_BY_SALES;

    private bool _FetchRun = false;
    private IDisposable _MastergudangSubscribe;
    private List<MastergudangOption> _MastergudangOptions = new List<MastergudangOption>();

    private IDisposable _MasterchannelsalesSubscribe;
    private List<MasterchannelsalesOption> _MasterchannelsalesOptions = new List<MasterchannelsalesOption>();
    private List<MastertimsalesOption> _MastertimsalesOptions = new List<MastertimsalesOption>();
    private List<SalesOption> _SalesOptions = new List<SalesOption>();

    private IDisposable _HkategoribarangSubscribe;
    private List<HkategoribarangOption> _HkategoribarangOptions = new List<HkategoribarangOption>();
    private List<DkategoribarangOption> _DkategoribarangOptions = new List<DkategoribarangOption>();

    private LaporanTransaksiBySalesData _laporanTransaksi = new LaporanTransaksiBySalesData();

    
    public LaporanPenjualanBarangBySales()
    {
        InitializeComponent();
    }
    
     private async Task SubscribeChannelSales()
        {
            _MasterchannelsalesSubscribe = FetchMasterchannelsalesOption.Subscribe(data =>
            {
                _MasterchannelsalesOptions = data.Prepend(new MasterchannelsalesOption
                {
                    Kode = null,
                    Nama = "Semua Channel Sales"
                }).ToList();
                _MastertimsalesOptions = data.SelectMany(e => e.Mastertimsales).Prepend(new MastertimsalesOption
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
                _HkategoribarangOptions = data.Prepend( new HkategoribarangOption
                {
                    Kodeh = null,
                    Nama = "Semua Brand"
                }).ToList();
                _DkategoribarangOptions = data.SelectMany(e => e.Dkategoribarang)
                .OrderBy(e => e.Nama)
                .Prepend(new DkategoribarangOption
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
            dataGridView1.DoubleBuffered(true);
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
                var jumlah = _laporanTransaksi.GetData().Sum(e => e.SumTotal).ToString("#,0");
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
            if (dataGridView1.Rows.Count > 0){
            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            }
            dataGridView1.ClearSelection();
        }
}