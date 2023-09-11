using DoranApp.Data;
using DoranApp.DataGlobal;
using DoranApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class LaporanTransaksiPenjualan : Form
    {
        private IDisposable _LokasiKotaSubscribe;
        private MasterbarangData _masterbarang = new MasterbarangData();
        private MastertimsalesData _mastertimsalesData = new MastertimsalesData();
        private MasterpelangganData _masterpelangganData = new MasterpelangganData();
        private MastergudangData _mastergudangData = new MastergudangData();

        private LaporanTransaksiData _transaksiData = new LaporanTransaksiData();
        private List<MasterbarangOption> _masterbarangOptions = new List<MasterbarangOption>();
        private List<Mastertimsales> _mastertimsales = new List<Mastertimsales>();
        private List<Sales> _sales = new List<Sales>();
        private List<MasterpelangganOption> _masterpelangganOptions = new List<MasterpelangganOption>();
        private List<Mastergudang> _mastergudangOptions = new List<Mastergudang>();

        private List<LokasiKota> _lokasiKota = new List<LokasiKota>();

        public long _laporanTransaksiLastPage = 0;
        public long _laporanTransaksiPage
        {
            get
            {
                long page;
                if (!long.TryParse(toolStripTextBox1.Text, out page) || toolStripTextBox1.Text == "")
                {
                    page = 1;
                }
                return page;
            }
            set
            {
                toolStripTextBox1.Text = value.ToString();
            }
        }

        public LaporanTransaksiPenjualan()
        {
            InitializeComponent();
        }

        public async Task FetchMasterbarang()
        {
            try
            {
                var data = await _masterbarang.GetNameAndKodeOnly();
                var resp = (List<MasterbarangOption>)data.Response;
                _masterbarangOptions = resp.Prepend(new MasterbarangOption
                {
                    brgKode = null,
                    brgNama = "Semua Barang"
                }).ToList();
                
            } catch (Exception ex)
            {

            }
            comboFilterBarang.ValueMember = "brgKode";
            comboFilterBarang.DisplayMember = "brgNama";
            comboFilterBarang.DataSource = _masterbarangOptions;
        }

        public async Task FetchMastertimWithSales()
        {
            try
            {
                var resp = await _mastertimsalesData.GetWithSales();
                _mastertimsales = resp.OrderBy(x => x.Nama).ToList();
                _sales.Clear();
                _sales = _mastertimsales?.SelectMany(x => x.Sales)
                    .OrderBy(e => e.Nama)
                    .ToList();
                _mastertimsales = _mastertimsales.Prepend(new Mastertimsales
                {
                    Kode = -1,
                    Nama = "Semua Tim Sales",

                }).ToList();
                _sales = _sales.Prepend(new Sales
                {
                    Kode = null,
                    Nama = "Semua Sales"
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboFilterMastertimsales.ValueMember = "Kode";
            comboFilterMastertimsales.DisplayMember = "Nama";
            comboFilterMastertimsales.DataSource = _mastertimsales;

            comboFilterSales.ValueMember = "Kode";
            comboFilterSales.DisplayMember = "Nama";
            comboFilterSales.DataSource = _sales;
        }

        //private async Task FetchGudang()
        //{
        //    try
        //    {
        //        _mastergudangData.SetQuery(new
        //        {
        //            aktif = true
        //        });
        //        await _mastergudangData.Refresh();
        //        _mastergudangOptions = _mastergudangData.GetData();
        //        comboGudang.ValueMember = "Kode";
        //        comboGudang.DisplayMember = "Nama";
        //        comboGudang.DataSource = _mastergudangOptions;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //    var filterOptions = new List<Mastergudang>(_mastergudangOptions).Prepend(new Mastergudang
        //    {
        //        Kode = -1,
        //        Nama = "SEMUA"
        //    }).ToList();


        //    comboFilterGudang.ValueMember = "Kode";
        //    comboFilterGudang.DisplayMember = "Nama";
        //    comboFilterGudang.DataSource = filterOptions;
        //}

        //private async Task FetchLevelHarga()
        //{
        //    try
        //    {
        //        _setlevelhargaData.SetQuery(new
        //        {
        //            modal = 0,
        //            online = false
        //        });
        //        await _setlevelhargaData.Refresh();
        //        _setlevelhargaOptions = _setlevelhargaData.GetData();
        //        ConsoleDump.Extensions.DumpObject(_setlevelhargaOptions);
        //        comboHarga.ValueMember = "Kode";
        //        comboHarga.DisplayMember = "Nama";
        //        comboHarga.DataSource = _setlevelhargaOptions;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Request harga error: {ex.StackTrace}");
        //    }
        //}

        private async Task FetchPelanggan()
        {
            button11.Enabled = false;
            try
            {
                var data = await _masterpelangganData.GetNameAndKodeOnly();
                _masterpelangganOptions = (List<MasterpelangganOption>)data.Response;
                _masterpelangganOptions = _masterpelangganOptions.Prepend(new MasterpelangganOption { Kode = null, Nama = "Semua Pelanggan" }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboFilterPelanggan.ValueMember = "Kode";
            comboFilterPelanggan.DisplayMember = "Nama";
            comboFilterPelanggan.DataSource = _masterpelangganOptions;
            button11.Enabled = true;
        }
        private async Task fetchLaporanTransaksi()
        {
            GC.Collect();
            toolStripLabel2.Visible = true;
            toolStrip1.Enabled = false;
            button1.Enabled = false;
            try
            {
                _transaksiData.SetQuery(new
                {
                    page = _laporanTransaksiPage <= 0 ? 1 : _laporanTransaksiPage,
                    pageSize = comboPageSize.SelectedItem?.ToString() ?? "50",
                    minDate = datePickerFilterMin.Value,
                    maxDate = datePickerFilterMax.Value,
                    kodeSales = comboFilterSales.SelectedValue?.ToString(),
                    KodePelanggan = comboFilterPelanggan.SelectedValue?.ToString(),
                    kodeKota = comboFilterKota.SelectedValue?.ToString(),
                    kodeProvinsi = comboFilterProvinsi.SelectedValue?.ToString(),
                    Kodenota = textBoxFilterKodenota.Text.Trim(),
                    kodeh = textBoxFilterKodeh.Text.Trim(),
                    lunas = GetFilterLunas()

                });
                await _transaksiData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var paginationData = _transaksiData.GetPaginationData();
            _laporanTransaksiPage = paginationData.Page;
            _laporanTransaksiLastPage = paginationData.TotalPage;
            toolStripLabel1.Text = $"dari {paginationData.TotalPage.ToString()}";
            toolStrip1.Enabled = true;
            button1.Enabled = true;
            toolStripLabel2.Visible = false;
        }

        private string? GetFilterLunas()
        {
            if (radioFilterLunasBelum.Checked)
            {
                return "0";
            }
            if (radioFilterLunasSudah.Checked)
            {
                return "1";
            }
            return null;
        }

        private async Task SubscribeLokasiKota()
        {
            comboFilterKota.ValueMember = "Kode";
            comboFilterKota.DisplayMember = "Nama";
            // Subscribe directly without creating a new LocationObserver


            _LokasiKotaSubscribe = FetchLokasiKota.Subscribe(data =>
            {
                _lokasiKota = data;
                if (_lokasiKota != null) {
                    _lokasiKota = data.Prepend(new LokasiKota
                {
                    Kode = null,
                    Nama = "Semua Kota"
                }).ToList();
               
                }
                comboFilterKota.DataSource = _lokasiKota;
            });
            // Trigger a fetch
            await FetchLokasiKota.Run();
        }

        private void LaporanTransaksiPenjualan_Load(object sender, EventArgs e)
        {
            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);
            dataGridView1.DataSource = _transaksiData.GetDataTable();
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            comboPageSize.SelectedIndex = 0;
            int[] numCols = new int[] { 2, 3 };
            foreach (var i in numCols)
            {
                dataGridView1.Columns[i].DefaultCellStyle.Format = "N0";
            }
            FetchMastertimWithSales();
            FetchPelanggan();
            FetchMasterbarang();
            SubscribeLokasiKota();
            //FetchGudang();
            //FetchLevelHarga();
            //CreateDatagridview();
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            await FetchPelanggan();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            _laporanTransaksiPage = 1;
            await fetchLaporanTransaksi();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _laporanTransaksiPage--;
            fetchLaporanTransaksi();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _laporanTransaksiPage++;
            fetchLaporanTransaksi();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _laporanTransaksiPage = 1;
            fetchLaporanTransaksi();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            _laporanTransaksiPage = _laporanTransaksiLastPage ;
            fetchLaporanTransaksi();
        }


        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
            {
                return;
            }
            dataGridView2.Rows.Clear();
            try
            {
                var kodeh = (long)dataGridView1.SelectedRows[0].Cells["Kodeh"].Value;
                var htrans = _transaksiData.GetData().Where(x => x.kodeH == kodeh).FirstOrDefault();
                if (htrans == null)
                {
                    return;
                }
                var dtrans = new List<dynamic>();
                foreach (var d in htrans.dtrans)
                {
                    var index = dataGridView2.Rows.Add();
                    dataGridView2.Rows[index].Cells["Pcs"].Value = d.jumlah;
                    dataGridView2.Rows[index].Cells["NamaBarang"].Value = d.masterbarang?.brgNama;
                    dataGridView2.Rows[index].Cells["Harga"].Value = d.harga;
                    dataGridView2.Rows[index].Cells["Jumlah"].Value = d.jumlah * d.harga;
                    dataGridView2.Rows[index].Cells["KurangiStok"].Value = d.kuranginStok;
                    dataGridView2.Rows[index].Cells["SN"].Value = d.SN;

                }
            } catch (Exception ex)
            {

            }
            
        }

        private void LaporanTransaksiPenjualan_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataGridView1.ClearSelection();
            if (dataGridView1.DataSource != null)
            {
                dataGridView1.DataSource = null;
            }
            dataGridView1.Dispose();
            GC.Collect();
        }

        private void toolStripTextBox1_Leave(object sender, EventArgs e)
        {
            fetchLaporanTransaksi();
        }

        private void comboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            _laporanTransaksiPage = 1;
            fetchLaporanTransaksi();
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            ConsoleDump.Extensions.Dump(e.Type);
            int count = dataGridView1.Rows[dataGridView1.RowCount - 1].Height;
            int display = dataGridView1.Rows.Count - dataGridView1.DisplayedRowCount(false);
            if ( e.Type == ScrollEventType.Last)
            {

                MessageBox.Show("123");
                if (dataGridView1.FirstDisplayedScrollingRowIndex + 8 >= dataGridView1.Rows.Count - 1)
                {
                }
            }
        }

        

        private void LaporanTransaksiPenjualan_KeyPress(object sender, KeyPressEventArgs e)
        {
            var keyPress = e.KeyChar.ToString();
            if ( dataGridView1.Focused)
            {
                if (keyPress == "q")
                {
                    _laporanTransaksiPage--;
                }

                if (keyPress == "e")
                {
                    _laporanTransaksiPage++;
                }
            }
        }

        private void LaporanTransaksiPenjualan_KeyUp(object sender, KeyEventArgs e)
        {
           
            if (dataGridView1.Focused)
            {
                if (e.KeyCode == Keys.Q)
                {
                    fetchLaporanTransaksi();
                }

                if (e.KeyCode == Keys.E)
                {
                    fetchLaporanTransaksi();
                }
            }
        }
    }
}
