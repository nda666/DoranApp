using DoranApp.Data.Laporan;
using DoranApp.DataGlobal;
using DoranApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class LaporanPenjualanBarangByBarang : Form
    {
        private IDisposable _MasterchannelsalesSubscribe;
        private List<MasterchannelsalesOption> _MasterchannelsalesOptions = new List<MasterchannelsalesOption>();
        private List<MastertimsalesOption> _MastertimsalesOptions = new List<MastertimsalesOption>();
        private List<SalesOption> _SalesOptions = new List<SalesOption>();

        private IDisposable _HkategoribarangSubscribe;
        private List<HkategoribarangOption> _HkategoribarangOptions = new List<HkategoribarangOption>();
        private List<DkategoribarangOption> _DkategoribarangOptions = new List<DkategoribarangOption>();

        private IDisposable _LokasiProvinsiSubscribe;
        private List<LokasiKotaOption> _LokasiKota = new List<LokasiKotaOption>();
        private List<LokasiProvinsiOption> _LokasiProvinsi = new List<LokasiProvinsiOption>();

        private IDisposable _MasterpelangganSubscribe;
        private List<MasterpelangganOption> _Masterpelanggan = new List<MasterpelangganOption>();

        private LaporanTransaksiByBarangData _laporanTransaksi = new LaporanTransaksiByBarangData();

        public LaporanPenjualanBarangByBarang()
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
                    _LokasiProvinsi = data.Prepend(new LokasiProvinsiOption
                    {
                        Kode = null,
                        Nama = "Semua Kota"
                    }).ToList();
                }
                comboFilterProvinsi.DataSource = _LokasiProvinsi;
                _LokasiKota.Clear();
                _LokasiKota = _LokasiKota.Prepend(new LokasiKotaOption
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
                    _Masterpelanggan = data.Prepend(new MasterpelangganOption
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
                _SalesOptions = data.SelectMany(e => e.Mastertimsales.SelectMany(
                        e => e.Sales)
                    ).Prepend(new SalesOption
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
        private async void LaporanPenjualanBarangByBarang_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _laporanTransaksi.GetDataTable();
            SubscribeHkategoribarang();
            SubscribeLokasi();
            SubscribeChannelSales();
            SubscribeMasterpelanggan();
        }

        private void LaporanPenjualanBarangByBarang_FormClosing(object sender, FormClosingEventArgs e)
        {
            _LokasiProvinsiSubscribe?.Dispose();
            _MasterpelangganSubscribe?.Dispose();
            _LokasiProvinsiSubscribe?.Dispose();
            _HkategoribarangSubscribe?.Dispose();
        }

        private async Task FetchLaporan()
        {
            button1.Enabled = false;
            try
            {
                _laporanTransaksi.SetQuery(new
                {
                    minDate = datePickerFilterMin.Value,
                    maxDate = datePickerFilterMax.Value,
                    NamaBarang = textBoxFilterNamaBarang.Text.Trim(),
                    KodeKota = comboFilterLokasiKota.SelectedValue?.ToString(),
                    KodePelanggan = comboFilterPelanggan.SelectedValue?.ToString(),
                });
                await _laporanTransaksi.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            button1.Enabled = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await FetchLaporan();
        }
    }
}