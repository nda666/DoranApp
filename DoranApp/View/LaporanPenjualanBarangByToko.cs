using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data.Laporan;
using DoranApp.DataGlobal;
using DoranApp.Utils;

namespace DoranApp.View
{
    public partial class LaporanPenjualanBarangByToko : Form
    {
        public enum TransaksiByTokoTipeGroup
        {
            GROUP_BY_TOKO = 0,
            GROUP_BY_KOTA = 1,
        }

        private List<DkategoribarangOptionDto> _DkategoribarangOptions = new List<DkategoribarangOptionDto>();
        private bool _FetchRun = false;
        private List<HkategoribarangOptionDto> _HkategoribarangOptions = new List<HkategoribarangOptionDto>();

        private IDisposable _HkategoribarangSubscribe;

        private LaporanTransaksiByTokoData _laporanTransaksi = new LaporanTransaksiByTokoData();
        private List<LokasiKota> _LokasiKota = new List<LokasiKota>();
        private List<LokasiProvinsi> _LokasiProvinsi = new List<LokasiProvinsi>();

        private IDisposable _LokasiProvinsiSubscribe;
        private List<MasterchannelsalesOptionDto> _MasterchannelsalesOptions = new List<MasterchannelsalesOptionDto>();

        private IDisposable _MasterchannelsalesSubscribe;
        private List<MastergudangOptionDto> _MastergudangOptions = new List<MastergudangOptionDto>();
        private IDisposable _MastergudangSubscribe;
        private List<CommonResultDto> _Masterpelanggan = new List<CommonResultDto>();

        private IDisposable _MasterpelangganSubscribe;
        private List<MastertimsalesOptionDto> _MastertimsalesOptions = new List<MastertimsalesOptionDto>();
        private List<SalesOptionDto> _SalesOptions = new List<SalesOptionDto>();

        private TransaksiByTokoTipeGroup _TipeGroup = TransaksiByTokoTipeGroup.GROUP_BY_TOKO;

        public LaporanPenjualanBarangByToko()
        {
            InitializeComponent();
        }

        private async Task SubscribeLokasi()
        {
            comboFilterLokasiKota.ValueMember = "Kode";
            comboFilterLokasiKota.DisplayMember = "Nama";

            comboFilterProvinsi.ValueMember = "Kode";
            comboFilterProvinsi.DisplayMember = "Nama";
            // Subscribe directly without creating a new LocationObserver
            _LokasiProvinsiSubscribe = FetchLokasiProvinsiOption.Subscribe(data =>
            {
                _LokasiProvinsi = data;
                if (_LokasiProvinsi != null)
                {
                    _LokasiProvinsi = data.Prepend(new LokasiProvinsi
                    {
                        Kode = null,
                        Nama = "Semua Kota"
                    }).ToList();
                }

                comboFilterProvinsi.DataSource = _LokasiProvinsi;
                _LokasiKota.Clear();
                _LokasiKota = _LokasiKota.Prepend(new LokasiKota
                {
                    Kode = null,
                    Nama = "Semua Kota"
                }).ToList();
                foreach (var e in _LokasiProvinsi)
                {
                    _LokasiKota.AddRange(e.LokasiKota);
                }

                comboFilterLokasiKota.DataSource = _LokasiKota;
            });
            // Trigger a fetch
            await FetchLokasiProvinsiOption.Run();
        }

        private async Task SubscribeMasterpelanggan()
        {
            comboFilterPelanggan.ValueMember = "Kode";
            comboFilterPelanggan.DisplayMember = "Nama";
            // Subscribe directly without creating a new LocationObserver

            _MasterpelangganSubscribe = FetchMasterpelangganOption.Subscribe(data =>
            {
                _Masterpelanggan = data;
                if (_Masterpelanggan != null)
                {
                    _Masterpelanggan = data.Prepend(new CommonResultDto()
                    {
                        Kode = null,
                        Nama = "Semua Pelanggan"
                    }).ToList();
                }

                comboFilterPelanggan.DataSource = _Masterpelanggan;
            });
            // Trigger a fetch
            await FetchMasterpelangganOption.Run();
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
                _SalesOptions = data.SelectMany(e => e.Mastertimsales.SelectMany(
                    e => e.Sales)
                ).Prepend(new SalesOptionDto()
                {
                    Kode = null,
                    Nama = "Semua Sales"
                }).ToList();
                comboFilterMasterchannelsales.DataSource = _MasterchannelsalesOptions;
                comboFilterMastertimsales.DataSource = _MastertimsalesOptions;
                comboFilterSales.DataSource = _SalesOptions;
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

        private async Task SubscribeMastergudang()
        {
            FetchMastergudangOption.Subscribe(data =>
            {
                _MastergudangOptions = data.Prepend(new MastergudangOptionDto()
                {
                    Kode = null,
                    Nama = "Semua Gudang"
                }).ToList();
                comboMastergudang.DataSource = _MastergudangOptions;
            });
            FetchMastergudangOption.Run();
        }

        private async void LaporanPenjualanBarangByToko_Load(object sender, EventArgs e)
        {
            var bs = new BindingSource();
            bs.DataSource = _laporanTransaksi.GetDataTable();
            bindingNavigator1.BindingSource = bs;
            dataGridView1.DataSource = bs.DataSource;
            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Columns[1].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "N0";

            SubscribeMastergudang();
            SubscribeHkategoribarang();
            SubscribeLokasi();
            SubscribeChannelSales();
            SubscribeMasterpelanggan();
        }

        private void LaporanPenjualanBarangByToko_FormClosing(object sender, FormClosingEventArgs e)
        {
            _LokasiProvinsiSubscribe?.Dispose();
            _MasterpelangganSubscribe?.Dispose();
            _LokasiProvinsiSubscribe?.Dispose();
            _HkategoribarangSubscribe?.Dispose();
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
                    minDate = datePickerFilterMin.Value,
                    maxDate = datePickerFilterMax.Value,
                    NamaBarang = textBoxFilterNamaBarang.Text.Trim(),
                    KodeKota = comboFilterLokasiKota.SelectedValue?.ToString(),
                    KodePelanggan = comboFilterPelanggan.SelectedValue?.ToString(),
                    KodeSales = comboFilterSales.SelectedValue?.ToString(),
                    KodeTimSales = comboFilterMastertimsales.SelectedValue?.ToString(),
                    KodeChannelSales = comboFilterMasterchannelsales.SelectedValue?.ToString(),
                    KodeBrand = comboFilterHkategoribarang.SelectedValue?.ToString(),
                    KodeKategori = comboFilterDkategoribarang.SelectedValue?.ToString(),
                    KodeGudang = comboMastergudang.SelectedValue?.ToString(),
                    TipeGroup = _TipeGroup
                });
                await _laporanTransaksi.Refresh();
                var jumlah = _laporanTransaksi.GetData().Sum(e => e.Jumlah).ToString();
                labelJumlahSum.Text = $"Total: {jumlah}";
                var total = _laporanTransaksi.GetData().Sum(e => e.SumTotal).ToString();
                labelTotalOmzet.Text = $"Total Omzet: {total}";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _FetchRun = false;
            labelLoading.Visible = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            _TipeGroup = TransaksiByTokoTipeGroup.GROUP_BY_TOKO;
            await FetchLaporan();
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            _TipeGroup = TransaksiByTokoTipeGroup.GROUP_BY_KOTA;
            await FetchLaporan();
        }
    }
}