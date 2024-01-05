using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleDump;
using DoranApp.Data;
using DoranApp.DataGlobal;
using DoranApp.Utils;
using Microsoft.Reporting.WinForms;
using OfficeOpenXml;

namespace DoranApp.View.LaporanTransaksiPenjualan
{
    public partial class LaporanTransaksiPenjualanForm : Form
    {
        private Client _Client = new Client();
        public long _laporanTransaksiLastPage = 0;
        private List<CommonResultDto> _lokasiKota = new List<CommonResultDto>();
        private MasterbarangData _masterbarang = new MasterbarangData();
        private List<MasterbarangOptionDto> _masterbarangOptions = new List<MasterbarangOptionDto>();
        private MastergudangData _mastergudangData = new MastergudangData();
        private List<Mastergudang> _mastergudangOptions = new List<Mastergudang>();
        private MasterpelangganData _masterpelangganData = new MasterpelangganData();
        private List<CommonResultDto> _masterpelangganOptions = new List<CommonResultDto>();
        private List<MastertimsalesOptionDto> _mastertimsales = new List<MastertimsalesOptionDto>();
        private MastertimsalesData _mastertimsalesData = new MastertimsalesData();
        private List<SalesOptionDto> _sales = new List<SalesOptionDto>();

        private LaporanTransaksiData _transaksiData = new LaporanTransaksiData();

        private Dictionary<string, IDisposable> DisposableList = new Dictionary<string, IDisposable>();
        private bool PrintNotaPpnRun = false;
        private DateTime? Tanggal1 = null;
        private DateTime? Tanggal2 = null;

        public LaporanTransaksiPenjualanForm()
        {
            InitializeComponent();
            var UTAMA_Akses = Session.GetUser().Akses;
            if (Session.GetUser().Akses?.ToLower() == "mastersuperadmin")
            {
                BTN_UpdateTransaksi.Visible = true;
                CBO_GantiHarga.Visible = true;
                CBO_GantiHarga.Enabled = false;
                CBO_GantiHarga.Checked = true;
            }

            if (UTAMA_Akses?.ToLower() == "supermasteraudit")
            {
                BTN_UpdateTransaksi.Visible = true;
                CBO_GantiHarga.Visible = true;
                CBO_GantiHarga.Enabled = true;
                CBO_GantiHarga.Checked = false;
            }

            if (UTAMA_Akses?.ToLower() == "bos" || UTAMA_Akses?.ToLower() == "auditor" ||
                UTAMA_Akses?.ToLower() == "supermasteraudit" || UTAMA_Akses?.ToLower() == "kepalaakuntan")
            {
                BTN_Jurnal.Visible = true;
                BTN_Valid.Visible = true;
                groupRadioValid.Visible = true;
                CBO_SemuaValid.Visible = true;

                // TODO: harus di set nama di C# nya, karena ini dari delphi #3
                // TAB_PPN.Enabled = true;
                // BTN_HapusPPN.Enabled = true;
                // PANEL_Auditor.Enabled = true;
                // PANEL_Auditor.Visible = true;
                // GROUP_PPNManual.Visible = true;

                if (UTAMA_Akses?.ToLower() == "kepalaakuntan")
                {
                    // TAB_PPN.Enabled = true;
                }
            }

            if (UTAMA_Akses?.ToLower() == "bos" || UTAMA_Akses?.ToLower() == "masteraudit" ||
                UTAMA_Akses?.ToLower() == "supermasteraudit" || UTAMA_Akses?.ToLower() == "managerbusiness" ||
                UTAMA_Akses?.ToLower() == "pm" || UTAMA_Akses?.ToLower() == "masteraudit")
            {
                // PANEL_SN.Visible = true;
            }

            if (UTAMA_Akses?.ToLower() == "kepalaakuntan")
            {
                // TAB_PPN.Enabled = true;
            }

            if (UTAMA_Akses?.ToLower() == "bos" || UTAMA_Akses?.ToLower() == "masteraudit" ||
                UTAMA_Akses?.ToLower() == "supermasteraudit")
            {
                // CBO_KomisiMinus.Visible = true;
            }

            if (UTAMA_Akses?.ToLower() == "bos" || UTAMA_Akses?.ToLower() == "masteradmin" ||
                UTAMA_Akses?.ToLower() == "masteradmin2" || UTAMA_Akses?.ToLower() == "mastersuperadmin" ||
                UTAMA_Akses?.ToLower() == "seketaris" || UTAMA_Akses?.ToLower() == "masteraudit" ||
                UTAMA_Akses?.ToLower() == "supermasteraudit")
            {
                // Group_JatuhTempo.Enabled = true;
                // BTN_Angsuran.Enabled = true;
                // BTN_SetLunas1.Enabled = true;
                // TAB_StokanNota.Enabled = true;
                // PANEL_Finance.Enabled = true;
                // PANEL_Finance.Visible = true;
            }

            if (UTAMA_Akses?.ToLower() == "kepalaakuntan")
            {
                // BTN_Angsuran.Enabled = true;
            }

            if (UTAMA_Akses?.ToLower() == "bos" || UTAMA_Akses?.ToLower() == "masteradmin" ||
                UTAMA_Akses?.ToLower() == "masteradmin2" || UTAMA_Akses?.ToLower() == "mastersuperadmin" ||
                UTAMA_Akses?.ToLower() == "masteraudit" || UTAMA_Akses?.ToLower() == "supermasteraudit" ||
                UTAMA_Akses?.ToLower() == "seketaris" || UTAMA_Akses?.ToLower() == "kepalaakuntan")
            {
                // GROUP_MasterAdmin.Visible = true;
            }

            if (UTAMA_Akses?.ToLower() == "bos")
            {
                // TAB_CetakForm.Enabled = true;
                // TAB_BagiKomisi.Enabled = true;
                // Group_Komisi.Enabled = true;
                // BTN_HapusPPN.Enabled = true;
                // Group_Footer.Visible = true;
                // TE_Komisi1.Visible = true;
                // TE_Komisi2.Visible = true;
                // LBL_Komisi.Visible = true;
                // LBL_KomisiStrip.Visible = true;
                CBO_Bos.Visible = true;
                // BTN_HapusTTMassal.Enabled = true;
                // GROUP_AdminGantiHarga.Visible = true;
            }
        }

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
            set { toolStripTextBox1.Text = value.ToString(); }
        }

        public async Task FetchMasterbarang()
        {
            try
            {
                var data = await _masterbarang.GetNameAndKodeOnly();
                var resp = (List<MasterbarangOptionDto>)data.Response;
                _masterbarangOptions = resp.Prepend(new MasterbarangOptionDto()
                {
                    BrgKode = null,
                    BrgNama = "Semua Barang"
                }).ToList();
            }
            catch (Exception ex)
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
                var m = resp.OrderBy(x => x.Nama).ToList();
                _mastertimsales = m.Select(x => new MastertimsalesOptionDto()
                {
                    Kode = x.Kode,
                    Nama = x.Nama,
                    Sales = x.Sales.Select(e => new SalesOptionDto()
                    {
                        Kode = e.Kode,
                        Nama = e.Nama,
                        Kodetimsales = e.Kodetimsales
                    }).ToList(),
                    Kodechannel = x.Kodechannel
                }).ToList();

                _sales.Clear();
                _sales = _mastertimsales?.SelectMany(x => x.Sales)
                    .OrderBy(e => e.Nama)
                    .ToList();

                _mastertimsales = _mastertimsales.Prepend(new MastertimsalesOptionDto()
                {
                    Kode = -1,
                    Nama = "Semua Tim Sales",
                }).ToList();

                _sales = _sales.Prepend(new SalesOptionDto()
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
                _masterpelangganOptions = (List<CommonResultDto>)data.Response;
                _masterpelangganOptions = _masterpelangganOptions
                    .Prepend(new CommonResultDto { Kode = null, Nama = "Semua Pelanggan" }).ToList();
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

        private dynamic GetLaporanTransaksiSearchQuery()
        {
            return new
            {
                page = _laporanTransaksiPage <= 0 ? 1 : _laporanTransaksiPage,
                pageSize = comboPageSize.SelectedItem?.ToString() ?? "50",
                //Filter 1
                minDate = datePickerFilterMin.Value,
                maxDate = datePickerFilterMax.Value,
                kodeSales = comboFilterSales.SelectedValue?.ToString(),
                KodePelanggan = comboFilterPelanggan.SelectedValue?.ToString(),
                kodeKota = comboFilterKota.SelectedValue?.ToString(),
                kodeProvinsi = comboFilterProvinsi.SelectedValue?.ToString(),
                Kodenota = textBoxFilterKodenota.Text.Trim(),
                kodeh = textBoxFilterKodeh.Text.Trim(),
                lunas = GetFilterLunas(),

                // Filter 2 
                InsertName = comboFilterAdmin.SelectedValue?.ToString(),
                Keterangan = textboxFilterKeterangan.Text,
                NamaPelanggan = textBoxNamaPelanggan.Text,
                KodeGudang = comboFilterGudang.SelectedValue?.ToString(),
                HargaMin = textboxFilterHargaMin.Text != "" ? textboxFilterHargaMin.UnformattedValue.ToString() : null,
                HargaMax = textboxFilterHargaMax.Text != "" ? textboxFilterHargaMax.UnformattedValue.ToString() : null,
                HargaTidakNol = checkBoxHargaTidakNol.Checked,
                NoSeriOnline = textboxFilterNoSeriOnline.Text,
                Barcodeonline = textboxFilterBarcode.Text,

                // Filter 3
                NamaBarang = textboxFilterNamabarang.Text,
                Jumlah = textboxFilterHargaMax.Text != "" ? textboxJumlahTrans.UnformattedValue.ToString() : null,
                NotaTitip = checkFilterTitipNota.Checked,
                Admingantiharga = checkboxFilterAdminGantiHarga.Checked,

                // Filter Jurnal Penjualan
                AkanDjJurnalkan = Helper.GetSelectedRadioButtonTag(groupBoxRadioJurnalPenjualan),
                Valid = Helper.GetSelectedRadioButtonTag(groupRadioValid)
            };
        }

        private async Task fetchLaporanTransaksi()
        {
            GC.Collect();
            toolStripLabel2.Visible = true;
            toolStrip1.Enabled = false;
            button1.Enabled = false;
            Tanggal1 = datePickerFilterMin.Value;
            Tanggal2 = datePickerFilterMax.Value;
            try
            {
                _transaksiData.SetQuery(GetLaporanTransaksiSearchQuery());
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

            var totalData = _transaksiData.GetTotalData();

            labelTotalTrans.Text = paginationData.TotalRow.ToString("N0");
            labelTotalOmzet.Text = totalData.Total.ToString("N0");
            labelTotalKomisi.Text = totalData.Komisi.ToString("N0");
            labelTotalDppFull.Text = totalData.DppFull.ToString("N0");
            labelTotalUntung.Text = totalData.Untung.ToString("N0");
            labelTotalUntungBelPotOL.Text = totalData.UntungbelumpotOl.ToString("N0");
            labelTotalPpnFull.Text = totalData.Ppn.ToString("N0");
            labelTotalTambahan.Text = totalData.TambahanLainnya.ToString("N0");
            labelTotalBiaya.Text = totalData.Jumlahbarangbiaya.ToString("N0");
            labelTotalDiskon.Text = totalData.Diskon.ToString("N0");

            labelTotalPpn.Text = totalData.Ppn.ToString("N0");
            labelTotalDpp.Text = totalData.Dpp.ToString("N0");
            labelTotalOmzetPpn.Text = totalData.TotalOmzetPPN.ToString("N0");

            totalData?.Dump();
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

        private async Task SubscribeUser()
        {
            DisposableList["userSubs"] = FetchMasteruserOption.Subscribe(data =>
            {
                comboFilterAdmin.DataSource = data.ToList().Prepend(new MasteruserOptionDto
                {
                    Kodeku = null,
                    Usernameku = "Semua Admin"
                }).ToList();
            });
            await FetchMasteruserOption.Run();
        }

        private async Task SubscribeGudang()
        {
            DisposableList["gudangSubs"] = FetchMastergudangOption.Subscribe(data =>
            {
                comboFilterGudang.DataSource = data.ToList().Prepend(new MastergudangOptionDto
                {
                    Kode = null,
                    Nama = "Semua Gudang"
                }).ToList();
            });
            await FetchMastergudangOption.Run();
        }

        private async Task SubscribeLokasiKota()
        {
            comboFilterKota.ValueMember = "Kode";
            comboFilterKota.DisplayMember = "Nama";
            // Subscribe directly without creating a new LocationObserver


            DisposableList["lokasiKotaSubs"] = FetchLokasiKota.Subscribe(data =>
            {
                _lokasiKota = data.Select(e => new CommonResultDto()
                {
                    Kode = e.Kode,
                    Nama = e.Nama
                }).ToList();
                if (_lokasiKota != null)
                {
                    _lokasiKota = _lokasiKota.Prepend(new CommonResultDto
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
            var lastMonth = DateTime.Now.AddMonths(-1);
            datePickerFilterMin.Value = new DateTime(lastMonth.Year, lastMonth.Month, 1);

            dataGridView1.EnableDoubleBuffered(true);
            dataGridView2.EnableDoubleBuffered(true);
            dataGridView1.DataSource = _transaksiData.GetDataTable();
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList()
                .ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            comboPageSize.SelectedIndex = 4;
            int[] numCols = new int[] { 2, 3 };
            foreach (var i in numCols)
            {
                dataGridView1.Columns[i].DefaultCellStyle.Format = "N0";
            }

            dataGridView1.Columns["Npwp"].Visible = false;

            FetchMastertimWithSales();
            FetchPelanggan();
            FetchMasterbarang();
            SubscribeLokasiKota();
            SubscribeUser();
            SubscribeGudang();
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
            if (_laporanTransaksiPage + 1 <= _laporanTransaksiLastPage)
            {
                _laporanTransaksiPage++;
            }

            fetchLaporanTransaksi();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _laporanTransaksiPage = 1;
            fetchLaporanTransaksi();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            _laporanTransaksiPage = _laporanTransaksiLastPage;
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
            }
            catch (Exception ex)
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

            foreach (var disposable in DisposableList)
            {
                disposable.Value.Dispose();
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
            if (e.Type == ScrollEventType.Last)
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
            if (dataGridView1.Focused)
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

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = dataGridView1.HitTest(e.X, e.Y);
                if (hti.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[hti.RowIndex].Selected = true;

                    contextMenuStrip1.Show(Cursor.Position); // Show the context menu at mouse cursor position
                }
            }
        }

        private void CopyCellData(string cellName)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                return;
            }

            var text = dataGridView1.SelectedRows[0].Cells[cellName].Value?.ToString();
            if (!String.IsNullOrEmpty(text))
            {
                Clipboard.SetText(text);
            }
        }

        private void copyNoNotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyCellData("No Nota");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CopyCellData("No Nota");
        }

        private void copyNoSeriOLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyCellData("Seri OL");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CopyCellData("Seri OL");
        }

        private void copyBarcodeOLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyCellData("Barcode OL");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CopyCellData("Barcode OL");
        }

        private void copyKodehToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyCellData("Kodeh");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CopyCellData("Kodeh");
        }

        private async void LaporanTransaksiPenjualan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                _laporanTransaksiPage = 1;
                await fetchLaporanTransaksi();
            }
        }

        public async Task<List<GetLaporanTransaksiPenjualanGroupTokoDto>> GetLaporanTransaksiPenjualanGroupToko()
        {
            List<GetLaporanTransaksiPenjualanGroupTokoDto> data =
                await _transaksiData.GetLaporanTransaksiPenjualanGroupToko(GetLaporanTransaksiSearchQuery());
            return data;
        }

        private async void BTN_TotalOmzetPenjualan_Click(object sender, EventArgs e)
        {
            var oldText = BTN_TotalOmzetPenjualan.Text;
            try
            {
                BTN_TotalOmzetPenjualan.Enabled = false;
                BTN_TotalOmzetPenjualan.Text = "Loading";
                List<GetLaporanTransaksiPenjualanGroupTokoDto> data = await GetLaporanTransaksiPenjualanGroupToko();
                var dialog = new ByTokoForm(data, datePickerFilterMin.Value, datePickerFilterMax.Value,
                    GetLaporanTransaksiPenjualanGroupToko);
                BTN_TotalOmzetPenjualan.Enabled = true;
                BTN_TotalOmzetPenjualan.Text = oldText;
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                ex?.Dump();
                Helper.ShowErrorMessage(ex);
                BTN_TotalOmzetPenjualan.Enabled = true;
            }
        }

        private async void BTN_TopKota_Click(object sender, EventArgs e)
        {
            var oldText = BTN_TopKota.Text;
            try
            {
                BTN_TopKota.Text = "Loading";
                BTN_TopKota.Enabled = false;
                List<LaporanTransaksiPenjualanGroupKotaDto> data =
                    await _transaksiData.GetLaporanTransaksiPenjualanGroupKota(GetLaporanTransaksiSearchQuery());
                var dialog = new ByKotaForm(data);
                BTN_TopKota.Text = oldText;
                BTN_TopKota.Enabled = true;
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                ex?.Dump();
                Helper.ShowErrorMessage(ex);
                BTN_TopKota.Enabled = true;
                BTN_TopKota.Text = oldText;
            }
        }

        private async void BTN_TopProvinsi_Click(object sender, EventArgs e)
        {
            var oldText = BTN_TopProvinsi.Text;
            try
            {
                BTN_TopProvinsi.Text = "Loading";
                BTN_TopProvinsi.Enabled = false;
                List<LaporanTransaksiPenjualanGroupProvinsiDto> data =
                    await _transaksiData.GetLaporanTransaksiPenjualanGroupProvinsi(GetLaporanTransaksiSearchQuery());
                var dialog = new ByProvinsiForm(data);
                BTN_TopProvinsi.Enabled = true;
                BTN_TopProvinsi.Text = oldText;
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                ex?.Dump();
                Helper.ShowErrorMessage(ex);
                BTN_TopProvinsi.Enabled = true;
                BTN_TopProvinsi.Text = oldText;
            }
        }

        private dynamic getSelectedHtrans()
        {
            if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
            {
                return null;
            }

            try
            {
                var kodeh = (long)dataGridView1.SelectedRows[0].Cells["Kodeh"].Value;
                return _transaksiData.GetData().Where(x => x.kodeH == kodeh).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
            {
                return;
            }

            if (PrintNotaPpnRun)
            {
                return;
            }

            PrintNotaPpnRun = true;
            var kodeh = (long)dataGridView1.SelectedRows[0].Cells["Kodeh"].Value;
            NotaTransaksiPpnResultDto htrans = null;

            try
            {
                htrans = await _Client.Get_Nota_Transaksi_PpnAsync((int)kodeh);
                MessageBox.Show(htrans.ToString());
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
                PrintNotaPpnRun = false;
                return;
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
                PrintNotaPpnRun = false;
                return;
            }

            try
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportEmbeddedResource =
                    "DoranApp.Report.NotaTransaksiPpn.rdlc"; // Path to your RDLC file

                var dt = new DataTable();
                dt.Columns.Add("no");
                dt.Columns.Add("jumlah");
                dt.Columns.Add("kodeBrg");
                dt.Columns.Add("brgNama");
                dt.Columns.Add("harga");
                dt.Columns.Add("subTotal");
                var no = 1;
                foreach (var dtrans in htrans.Detail)
                {
                    DataRow row = dt.NewRow();
                    row["no"] = no;
                    row["jumlah"] = dtrans.Pcs.ToString("N0");
                    row["kodeBrg"] = dtrans.Kodebarang;
                    row["brgNama"] = dtrans.Namabarang;
                    row["harga"] = dtrans.Hargabelumppn.ToString("N0");
                    row["subTotal"] = dtrans.Subtotalbelumppn.ToString("N0");
                    dt.Rows.Add(row);
                    no++;
                }

                // Set parameters (if any)
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("total", string.Format("{0:N0}", htrans.Jumlah)));
                parameters.Add(new ReportParameter("lainnya", string.Format("{0:N0}", htrans.Tambahanlainnya)));

                parameters.Add(new ReportParameter("tglTrans", htrans.Tanggal.ToString("dd/MM/yyyy")));
                parameters.Add(new ReportParameter("kodenota", (string)htrans.Kodenota ?? ""));
                var namaPelanggan = Regex.Replace(htrans.Nama ?? "", @"[\r\n\t]+", " ");
                parameters.Add(new ReportParameter("namaPelanggan", namaPelanggan.Trim()));
                parameters.Add(new ReportParameter("lokasiPelanggan", htrans.Lokasi));

                parameters.Add(new ReportParameter("namaSales", (string)htrans.Namasales ?? ""));
                parameters.Add(new ReportParameter("tipeTempo", htrans.Tipetempo));
                parameters.Add(new ReportParameter("tglTempo", htrans.Tgltempo.ToString("dd/MM/yyyy")));
                parameters.Add(new ReportParameter("kodenota", htrans.Kodenota ?? ""));
                parameters.Add(new ReportParameter("oleh", htrans.Oleh ?? ""));

                parameters.Add(new ReportParameter("ppn", string.Format("{0:N0}", htrans.Ppn)));
                parameters.Add(new ReportParameter("dpp", string.Format("{0:N0}", htrans.Dpp)));
                parameters.Add(new ReportParameter("diskon", string.Format("{0:N0}", htrans.Diskon)));
                parameters.Add(new ReportParameter("grandTotal", string.Format("{0:N0}", htrans.Jumlah)));

                reportViewer.LocalReport.SetParameters(parameters.ToArray());

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                // Refresh the report
                reportViewer.RefreshReport();

                var directPrint = new DirectPrint();


                directPrint.Export(reportViewer.LocalReport, "CustomSize", 21, 29).Print();

                //.Print("CustomSize", 970, 551);
            }
            catch (Exception ex)
            {
                ex.Dump();
                MessageBox.Show(ex.Message);
            }

            PrintNotaPpnRun = false;
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
            {
                return;
            }

            try
            {
                var kodeh = dataGridView1.SelectedRows[0].Cells["Kodeh"].Value?.ToString();
                await _Client.Add_Ppn_To_TransactionAsync(Convert.ToInt32(kodeh));
                MessageBox.Show("Berhasil disimpan");
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MemoryStream excelMemoryStream = ExportToExcel(dataGridView1);

            // Generate timestamp for the file name
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Save the Excel file to disk and open it
            if (excelMemoryStream != null)
            {
                using (excelMemoryStream)
                {
                    string fileName = $"Export_{timestamp}.xlsx";
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        excelMemoryStream.WriteTo(fileStream);
                    }
                }

                System.Diagnostics.Process.Start($"Export_{timestamp}.xlsx");
            }
        }

        private MemoryStream ExportToExcel(DataGridView dataGridView)
        {
            MemoryStream memoryStream = new MemoryStream();

            // Create a new Excel package for Excel 97-2003 format
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                // Create a new worksheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells["A1:H1"].Merge = true;
                worksheet.Cells["A1:H1"].Style.Font.Bold = true;

                worksheet.Cells[1, 1].Value =
                    $"Trans Penjualan Periode {Tanggal1?.ToString("dd/MM/yyyy")} - {Tanggal2?.ToString("dd/MM/yyyy")}";

                // Set headers for specific columns
                string[] headers = { "Nama Toko", "NPWP", "No Nota", "Tanggal", "DPP", "PPN", "LAINNYA", "JUMLAH" };
                double[] headerWidths = { 35, 20, 8, 10, 12, 10, 12, 12 };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[2, i + 1].Value = headers[i];
                    worksheet.Column(i + 1).Width = headerWidths[i];
                    worksheet.Cells[2, i + 1].Style.Font.Bold = true;
                }

                // Set cell values for the specific columns
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    worksheet.Cells[i + 3, 1].Value = dataGridView.Rows[i].Cells["Nama"].Value?.ToString();
                    worksheet.Cells[i + 3, 2].Value = dataGridView.Rows[i].Cells["Npwp"].Value?.ToString();
                    worksheet.Cells[i + 3, 3].Value = dataGridView.Rows[i].Cells["No Nota"].Value?.ToString();
                    worksheet.Cells[i + 3, 4].Value = dataGridView.Rows[i].Cells["Tanggal"].Value?.ToString();
                    worksheet.Cells[i + 3, 5].Value = dataGridView.Rows[i].Cells["DPP"].Value?.ToString();
                    worksheet.Cells[i + 3, 6].Value = dataGridView.Rows[i].Cells["Faktur Ppn"].Value?.ToString();
                    worksheet.Cells[i + 3, 7].Value = dataGridView.Rows[i].Cells["Lainnya"].Value?.ToString();
                    worksheet.Cells[i + 3, 8].Value = dataGridView.Rows[i].Cells["Jumlah"].Value?.ToString();

                    worksheet.Cells[i + 3, 5].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[i + 3, 6].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[i + 3, 7].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[i + 3, 8].Style.Numberformat.Format = "0";
                }

                // Auto-fit columns for better visibility
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Save the Excel package (no need to explicitly save to a file)
                excelPackage.Save();
            }

            return memoryStream;
        }

        private void onChangeComboNamaTempo2()
        {
            if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
            {
                return;
            }

            try
            {
                var kodeh = (long)dataGridView1.SelectedRows[0].Cells["Kodeh"].Value;
                var htrans = _transaksiData.GetData().Where(x => x.kodeH == kodeh).FirstOrDefault();
                if (htrans == null)
                {
                    return;
                }


                DATE_TglTempo.Value =
                    ((DateTime)htrans.tglTrans).AddDays(Helper.TipeTempoIndexToValue(COMBO_NamaTempo2.SelectedIndex));
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
            }
        }

        private void COMBO_NamaTempo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            onChangeComboNamaTempo2();
        }

        private void COMBO_NamaTempo2_Leave(object sender, EventArgs e)
        {
            onChangeComboNamaTempo2();
        }

        private async void button17_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
            {
                return;
            }

            if (COMBO_NamaTempo2.SelectedIndex < 0)
            {
                Helper.ShowError("Pilih tempo terlebih dahulu");
                COMBO_NamaTempo2.DroppedDown = true;
                return;
            }

            try
            {
                var kodeh = dataGridView1.SelectedRows[0].Cells["Kodeh"].Value?.ToString();
                if (kodeh == null)
                {
                    return;
                }

                var kodehs = new List<int>();
                kodehs.Add(Convert.ToInt32(kodeh));
                await _Client.Set_Tempo_To_TransactionAsync(new SetTempoToTransactionDto
                {
                    Kodeh = kodehs,
                    Tgltempo = DATE_TglTempo.Value
                });

                MessageBox.Show("Set Tgl Jatuh Tempo Berhasil");
                fetchLaporanTransaksi();
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
            catch (Exception ex)
            {
                ex.Dump();
                Helper.ShowErrorMessage(ex);
            }
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            if (COMBO_NamaTempo2.SelectedIndex < 0)
            {
                Helper.ShowError("Pilih tempo terlebih dahulu");
                COMBO_NamaTempo2.DroppedDown = true;
                return;
            }

            var kodehs = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Kodeh"].Value != null)
                {
                    kodehs.Add(Convert.ToInt32(row.Cells["Kodeh"].Value.ToString()));
                }
            }

            await _Client.Set_Tempo_To_TransactionAsync(new SetTempoToTransactionDto
            {
                Kodeh = kodehs,
                Tgltempo = DATE_TglTempo.Value
            });

            MessageBox.Show("Set isi Tgl Jatuh Tempo Berhasil");
            fetchLaporanTransaksi();
        }

        private async void BTN_HapusPPN_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
            {
                return;
            }

            var kodeh = (long)dataGridView1.SelectedRows[0].Cells["Kodeh"].Value;
            var htrans = _transaksiData.GetData().Where(x => x.kodeH == kodeh).FirstOrDefault();
            if (htrans == null)
            {
                return;
            }

            var confirm = MessageBox.Show($"Apakah anda yakin ingin menghapus PPN nota {htrans.kodenota}?",
                "Hapus PPN?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                await _Client.Delete_Ppn_To_TransactionAsync(Convert.ToInt32(kodeh));
                fetchLaporanTransaksi();
                MessageBox.Show($"Hapus PPN nota {htrans.kodenota} berhasil");
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
            }
        }

        private async void button15_Click(object sender, EventArgs e)
        {
            var kodehs = new List<int>();
            if (CBO_SetSemuaTglPPN.Checked)
            {
                if (MessageBox.Show("SET Semua Tgl PPN ?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Kodeh"].Value != null)
                    {
                        kodehs.Add(Convert.ToInt32(row.Cells["Kodeh"].Value.ToString()));
                    }
                }
            }
            else
            {
                if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
                {
                    return;
                }

                var kodeh = dataGridView1.SelectedRows[0].Cells["Kodeh"].Value?.ToString();
                kodehs.Add(Convert.ToInt32(kodeh));
            }

            try
            {
                await _Client.Set_Tanggal_Ppn_To_TransactionAsync(new SetTanggalPpnToTransactionDto()
                {
                    Kodeh = kodehs,
                    TglPPN = Date_TglPPN.Value
                });
                fetchLaporanTransaksi();
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
            }
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            var kodehs = new List<int>();
            if (CBO_UntukSemua.Checked)
            {
                if (MessageBox.Show("Lapor PPN Untuk Semua ?", "Konfirmasi", MessageBoxButtons.YesNo) ==
                    DialogResult.No)
                {
                    return;
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Kodeh"].Value != null)
                    {
                        kodehs.Add(Convert.ToInt32(row.Cells["Kodeh"].Value.ToString()));
                    }
                }
            }
            else
            {
                if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
                {
                    return;
                }

                var kodeh = dataGridView1.SelectedRows[0].Cells["Kodeh"].Value?.ToString();
                kodehs.Add(Convert.ToInt32(kodeh));
            }

            try
            {
                await _Client.Set_Lapor_Ppn_To_TransactionAsync(new SetTanggalLaporPpnToTransactionDto()
                {
                    Kodeh = kodehs,
                    TglLaporPPN = DATE_TglLapor.Value
                });
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
            }
        }
    }
}