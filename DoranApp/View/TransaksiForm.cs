using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleDump;
using DoranApp.Data;
using DoranApp.Exceptions;
using DoranApp.Utils;
using DoranApp.View.UserForms;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json.Linq;

namespace DoranApp.View
{
    public partial class TransaksiForm : Form
    {
        private decimal _biaya = 0;

        private Client _CLient = new Client();
        public long _laporanTransaksiLastPage = 0;
        private MasterbarangData _masterbarang = new MasterbarangData();
        private List<MasterbarangOptionWithSnDto> _masterbarangOptions = new List<MasterbarangOptionWithSnDto>();
        private List<MasterbarangOptionWithSnDto> _masterbarangShownyaOptions = new List<MasterbarangOptionWithSnDto>();
        private MastergudangData _mastergudangData = new MastergudangData();
        private List<Mastergudang> _mastergudangOptions = new List<Mastergudang>();
        private MasterpelangganData _masterpelangganData = new MasterpelangganData();
        private List<MasterpelangganNamaResultDto> _masterpelangganOptions = new List<MasterpelangganNamaResultDto>();
        private OrderData _OrderData = new OrderData();
        private SalesData _salesData = new SalesData();
        private List<SalesOptionDto> _salesOptions = new List<SalesOptionDto>();
        private SetlevelhargaData _setlevelhargaData = new SetlevelhargaData();
        private List<CommonResultDto> _setlevelhargaOptions = new List<CommonResultDto>();

        private decimal _subtotal = 0;

        private TransaksiData _transaksiData = new TransaksiData();
        private BindingList<dynamic> cart;

        private bool FetchLaporanTransaksi = false;
        private int? KodeEdit = null;

        private int? NoOrder = null;
        private bool preventEventCheckBoxShowAllItem = false;
        private bool PrintNotaPpnRun = false;

        private bool PrintNotaRun = false;

        protected bool SnCheckRun = false;

        public TransaksiForm()
        {
            InitializeComponent();
            var allowed = new[]
            {
                "bos", "kagud", "masterkagud", "seketaris", "masteradmin", "masteradmin2",
                "mastersuperadmin", "masteraudit", "supermasteraudit"
            };

            if (!allowed.Contains(Session.GetUser().Akses.ToLower().Trim()))
            {
                splitContainer1.Panel1Collapsed = true;
                button12.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
                checkBoxPrintFoto.Visible = false;
                checkBoxModeNotaOl.Visible = false;
                checkBox1.Visible = false;
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
            var data = await _masterbarang.GetNameAndKodeOnly();
            _masterbarangOptions = (List<MasterbarangOptionWithSnDto>)data.Response;
        }

        public async Task FetchSales()
        {
            try
            {
                _salesOptions = await _salesData.GetNameAndKodeOnly();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboSales.ValueMember = "Kode";
            comboSales.DisplayMember = "Nama";
            comboSales.DataSource = _salesOptions;
        }

        private async Task FetchGudang()
        {
            try
            {
                _mastergudangData.SetQuery(new
                {
                    aktif = true
                });
                await _mastergudangData.Refresh();
                _mastergudangOptions = _mastergudangData.GetData();
                comboGudang.ValueMember = "Kode";
                comboGudang.DisplayMember = "Nama";
                comboGudang.DataSource = _mastergudangOptions;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var filterOptions = new List<Mastergudang>(_mastergudangOptions).Prepend(new Mastergudang
            {
                Kode = -1,
                Nama = "SEMUA"
            }).ToList();


            comboFilterGudang.ValueMember = "Kode";
            comboFilterGudang.DisplayMember = "Nama";
            comboFilterGudang.DataSource = filterOptions;
        }

        private async Task FetchLevelHarga()
        {
            try
            {
                _setlevelhargaData.SetQuery(new
                {
                    modal = 0,
                    online = false
                });
                await _setlevelhargaData.Refresh();
                _setlevelhargaOptions = _setlevelhargaData.GetData();
                ConsoleDump.Extensions.DumpObject(_setlevelhargaOptions);
                comboHarga.ValueMember = "Kode";
                comboHarga.DisplayMember = "Nama";
                comboHarga.DataSource = _setlevelhargaOptions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Request harga error: {ex.StackTrace}");
            }
        }

        private async Task FetchPelanggan()
        {
            try
            {
                var data = await _masterpelangganData.GetOptions();
                _masterpelangganOptions = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboPelanggan.ValueMember = "Kode";
            comboPelanggan.DisplayMember = "Nama";
            comboPelanggan.DataSource = _masterpelangganOptions;
        }

        public async void CreateDatagridview()
        {
            await FetchMasterbarang();

            var dc = new DataGridViewComboBoxColumn();
            dc.ValueMember = "BrgKode";
            dc.DataPropertyName = "BrgKode"; // The ValueMember
            dc.DisplayMember = "BrgNama"; // The DisplayMember
            dc.HeaderText = "Item";
            dc.Name = "item";
            dc.DisplayStyleForCurrentCellOnly = false;
            dc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dc.AutoComplete = true;

            // dc.DataSource = _masterbarangOptions.ToList();
            foreach (var x in _masterbarangOptions)
            {
                // if (x.Shownya == 1){
                dc.Items.Add(x);
                // _masterbarangShownyaOptions.Add(x);
                // }
            }

            dataGridView1.Columns.Add("pcs", "Pcs");
            dataGridView1.Columns.Add(dc);
            dataGridView1.Columns.Add("harga", "Harga");
            dataGridView1.Columns.Add("jumlah", "Jumlah");
            dataGridView1.Columns.Add("ord", "ORD");
            dataGridView1.Columns.Add("sisa", "Sisa");
            dataGridView1.Columns.Add("sn", "SN");

            dataGridView1.Columns["pcs"].Width = 40;
            dataGridView1.Columns["item"].Width = 240;
            dataGridView1.Columns["ord"].Width = 40;
            dataGridView1.Columns["sisa"].Width = 40;
            dataGridView1.Columns["sn"].Width = 121;

            dataGridView1.Columns["pcs"].ValueType = typeof(int);
            dataGridView1.Columns["harga"].ValueType = typeof(int);
            dataGridView1.Columns["pcs"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["harga"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["jumlah"].DefaultCellStyle.Format = "N0";

            dataGridView1.Columns["jumlah"].ReadOnly = true;
            dataGridView1.Columns["ord"].ReadOnly = true;
            dataGridView1.Columns["sisa"].ReadOnly = true;


            dataGridView2.DataSource = null;
            dataGridView2.DataSource = _transaksiData.GetDataTable();
            dataGridView2.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView2.Columns[4].DefaultCellStyle.Format = "N0";

            dataGridView2.Columns[2].Width = 70;
            dataGridView2.Columns[3].Width = 150;
            dataGridView2.Columns[7].Width = 150;
            dataGridView2.Columns[6].Width = 50;
            dataGridView2.Columns[8].Width = 70;
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private async void TransaksiForm_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableDoubleBuffered(true);
            dataGridView2.EnableDoubleBuffered(true);
            dataGridView3.EnableDoubleBuffered(true);

            dataGridView3.Columns[0].DefaultCellStyle.Format = "N0";
            dataGridView3.Columns[2].DefaultCellStyle.Format = "N0";
            dataGridView3.Columns[3].DefaultCellStyle.Format = "N0";

            comboTempo.SelectedIndex = 1;

            FetchSales();
            FetchPelanggan();
            FetchGudang();
            FetchLevelHarga();
            CreateDatagridview();
        }


        private async void button3_Click(object sender, EventArgs e)
        {
            await FetchPelanggan();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await FetchMasterbarang();
        }

        private SaveTransaksiDto CreateSaveRequest(List<DetailTransaksi> Details)
        {
            var reqData = new SaveTransaksiDto()
            {
                NoOrder = NoOrder,
                KodeSales = Convert.ToInt32(comboSales.SelectedValue),
                Kodegudang = Convert.ToInt32(comboGudang.SelectedValue),
                TglTrans = datePickerTransaksi.Value,
                KodePelanggan = Convert.ToInt32(comboPelanggan.SelectedValue),
                Keterangan = textBoxKeterangan.Text,
                Infopenting = textBoxInfoPenting.Text,
                Tipetempo = getTipeTempo(),
                Tgltempo = comboTempo.SelectedIndex == 0 ? null : datePickerJatuhTempo.Value,
                Retur = "0",
                Jumlahbarangbiaya = (int)textBoxBiaya.UnformattedValue,
                TambahanLainnya = (int)textBoxOngkir.UnformattedValue,
                Diskon = (int)textBoxDiskon.UnformattedValue,
                Ppn = (int)textBoxPPN.UnformattedValue,
                Dpp = (int)textBoxDpp.UnformattedValue,
                Details = Details,
                Force = false
            };
            return reqData;
        }

        private UpdateTransaksiDto CreateUpdateRequest(List<DetailTransaksi> Details)
        {
            var reqData = new UpdateTransaksiDto()
            {
                NoOrder = NoOrder,
                KodeSales = Convert.ToInt32(comboSales.SelectedValue),
                Kodegudang = Convert.ToInt32(comboGudang.SelectedValue),
                TglTrans = datePickerTransaksi.Value,
                KodePelanggan = Convert.ToInt32(comboPelanggan.SelectedValue),
                Keterangan = textBoxKeterangan.Text,
                Infopenting = textBoxInfoPenting.Text,
                Tipetempo = getTipeTempo(),
                Tgltempo = comboTempo.SelectedIndex == 0 ? null : datePickerJatuhTempo.Value,
                Retur = "0",
                Jumlahbarangbiaya = (int)textBoxBiaya.UnformattedValue,
                TambahanLainnya = (int)textBoxOngkir.UnformattedValue,
                Diskon = (int)textBoxDiskon.UnformattedValue,
                Ppn = (int)textBoxPPN.UnformattedValue,
                Dpp = (int)textBoxDpp.UnformattedValue,
                Details = Details,
                Force = false,
                CancelOrder = checkBoxCancelOrder.Checked
            };
            return reqData;
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            dynamic reqData = new ExpandoObject();
            try
            {
                List<DetailTransaksi> Details = new List<DetailTransaksi>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells[0]?.Value == null)
                        {
                            MessageBox.Show("Ada Pcs yang belum di isi");
                            dataGridView1.CurrentCell = row.Cells[0];
                            dataGridView1.BeginEdit(true);
                            return;
                        }

                        if (row.Cells[2]?.Value == null)
                        {
                            MessageBox.Show("Ada Harga yang belum di isi");
                            dataGridView1.CurrentCell = row.Cells[2];
                            dataGridView1.BeginEdit(true);
                            return;
                        }

                        if (row.Cells[1]?.Value == null)
                        {
                            MessageBox.Show("Ada nama barang yang belum di isi");
                            dataGridView1.CurrentCell = row.Cells[1];
                            dataGridView1.BeginEdit(true);
                            return;
                        }

                        var checkMb = _masterbarangOptions.Find(e =>
                            e.BrgKode.HasValue && e.BrgKode.ToString() == row.Cells[1]?.Value.ToString());
                        if (checkMb.Sn && (row.Cells[6]?.Value?.ToString().Trim() ?? "") == "")
                        {
                            MessageBox.Show($"Isi terlebih dahulu nomor SN: \"{checkMb.BrgNama}\"");
                            dataGridView1.CurrentCell = row.Cells[6];
                            dataGridView1.BeginEdit(true);
                            return;
                        }

                        if (checkMb.Sn && row.Cells[0].Value?.ToString() != "1")
                        {
                            MessageBox.Show($"Item SN pcs nya harus 1");
                            dataGridView1.CurrentCell = row.Cells[0];
                            dataGridView1.BeginEdit(true);
                            return;
                        }

                        Details.Add(new DetailTransaksi
                        {
                            Kodebarang = Convert.ToInt32(row.Cells[1]?.Value),
                            Jumlah = Convert.ToInt32(row.Cells[0]?.Value),
                            Harga = Convert.ToInt32(row.Cells[2]?.Value),
                            Nmrsn = row.Cells[6]?.Value?.ToString() ?? "",
                        });
                    }
                }

                if (Details.Count == 0)
                {
                    MessageBox.Show("Isi terlebih dahulu item nya");
                    return;
                }

                var confirm = MessageBox.Show($"{(KodeEdit == null ? "Simpan" : "Ubah")} transaksi ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (confirm == DialogResult.No)
                {
                    button9.Enabled = true;
                    return;
                }

                buttonBatalUbah.Enabled = false;
                button9.Enabled = false;
                reqData = KodeEdit == null ? CreateSaveRequest(Details) : CreateUpdateRequest(Details);
                var res = await _transaksiData.CreateOrUpdate(KodeEdit == null ? null : KodeEdit.ToString(), reqData);
                fetchLaporanTransaksi();
                ResetForm();
                MessageBox.Show("Transaksi berhasil disimpan");
                dataGridView1.Rows.Clear();
                buttonBatalUbah.Visible = false;
            }
            catch (RestException ex)
            {
                if (ex?.ErrorType == "LIMIT_TRANSAKSI")
                {
                    reqData.Force = true;
                    var limitForm = new LimitTransaksiConfirmationForm(ex.Data?.limitMessage?.ToString(), KodeEdit,
                        _transaksiData, reqData);
                    var confirmLimit = limitForm.ShowDialog();
                    if (confirmLimit == DialogResult.OK)
                    {
                        fetchLaporanTransaksi();
                        ResetForm();
                        MessageBox.Show("Transaksi berhasil disimpan");
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                ConsoleDump.Extensions.Dump(ex);
                button9.Enabled = true;
                MessageBox.Show(ex.Message);
            }

            button9.Enabled = true;
            buttonBatalUbah.Enabled = true;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculateSubtotal();
            CalculateTotal();
            if (e.RowIndex == -1)
                return;

            //this.latestEditingControl
            Type t = dataGridView1.GetType();
            FieldInfo viewSetter = t.GetField("latestEditingControl",
                BindingFlags.Default | BindingFlags.NonPublic | BindingFlags.Instance);
            if (viewSetter == null) return;
            viewSetter.SetValue(dataGridView1, null);
        }

        private void CalculateSubtotal()
        {
            decimal biaya = 0;
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                if (row.Cells["pcs"].Value != null &&
                    row.Cells["pcs"].Value.ToString().Trim() != "" &&
                    row.Cells["harga"].Value != null &&
                    row.Cells["harga"].Value.ToString().Trim() != ""
                   ) // Skip the new row if present
                {
                    int pcs = Convert.ToInt32(row.Cells["pcs"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["harga"].Value);
                    decimal rowSubtotal = pcs * price;
                    row.Cells["jumlah"].Value = rowSubtotal;
                    if (row.Cells["item"].Value != null)
                    {
                        var brgKode = Convert.ToInt32(row.Cells["item"].Value);
                        var mb = _masterbarangOptions
                            .Where(e => e.BrgKode == brgKode)
                            .FirstOrDefault();
                        if (mb.JurnalBiaya)
                        {
                            biaya += rowSubtotal;
                        }
                        else
                        {
                            subtotal += rowSubtotal;
                        }
                    }
                }
                else
                {
                    row.Cells["jumlah"].Value = 0;
                    subtotal += 0;
                }
            }

            _subtotal = subtotal;
            _biaya = biaya;
            textBoxBiaya.Text = _biaya.ToString();
            CallculatePPN();
        }

        private void CalculateTotal()
        {
            textBoxTotal.Text = (_subtotal - textBoxDiskon.UnformattedValue + textBoxBiaya.UnformattedValue +
                                 textBoxOngkir.UnformattedValue).ToString();
        }

        private int getTipeTempo()
        {
            var subDay = 0;
            switch (comboTempo.SelectedIndex)
            {
                case 1:
                    subDay = 7;
                    break;
                case 2:
                    subDay = 14;
                    break;
                case 3:
                    subDay = 30;
                    break;
                case 4:
                    subDay = 60;
                    break;
                default:
                    subDay = 0;
                    break;
            }

            return subDay;
        }

        private void UpdateTempo()
        {
            if (comboTempo.SelectedIndex == 0)
            {
                datePickerJatuhTempo.Enabled = false;
                return;
            }

            datePickerJatuhTempo.Enabled = true;


            datePickerJatuhTempo.Value = datePickerTransaksi.Value.AddDays(getTipeTempo());
        }

        private void comboTempo_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateTempo();
        }

        private void datePickerTransaksi_ValueChanged(object sender, EventArgs e)
        {
            UpdateTempo();
        }

        private void CallculatePPN()
        {
            textBoxPPN.Enabled = checkBoxPPN.Checked;
            decimal ppn = 0;
            decimal dpp = 0;
            if (checkBoxPPN.Checked)
            {
                ppn = Math.Round(_subtotal - (_subtotal / (decimal)1.11));
                dpp = Math.Round(_subtotal - ppn);
            }

            textBoxPPN.Text = ((int)ppn).ToString();
            textBoxDpp.Text = ((int)dpp).ToString();
            CalculateTotal();
        }

        private void checkBoxPPN_CheckedChanged(object sender, EventArgs e)
        {
            CallculatePPN();
        }


        private void textBoxDiskon_Leave(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void textBoxBiaya_Leave(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void textBoxOngkir_Leave(object sender, EventArgs e)
        {
            CalculateTotal();
        }


        private void textBoxDpp_Leave_1(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private async void fetchLaporanTransaksi()
        {
            if (FetchLaporanTransaksi)
            {
                return;
            }

            splitContainer1.Cursor = Cursors.AppStarting;
            ButtonToggleHelper.DisableButtonsByTag(this, "groupbox");
            FetchLaporanTransaksi = true;
            toolStripLabel2.Visible = true;
            dataGridView3.Rows.Clear();
            toolStrip1.Enabled = false;
            button10.Enabled = false;
            try
            {
                _transaksiData.SetQuery(new
                {
                    page = _laporanTransaksiPage <= 0 ? 1 : _laporanTransaksiPage,
                    pageSize = comboPageSize.SelectedItem?.ToString() ?? "300",
                    Kodegudang = comboFilterGudang.SelectedValue.ToString(),
                    namaPelanggan = textBoxFilterNama.Text.Trim().ToString()
                    //MinDate = DateTime.Now.AddMonths(-3)
                });
                await _transaksiData.Refresh();
            }
            catch (Exception ex)
            {
                ConsoleDump.Extensions.Dump(ex.Message);
                MessageBox.Show(ex.Message);
            }

            // var paginationData = _transaksiData.GetPaginationData();
            // _laporanTransaksiPage = paginationData.Page;
            // _laporanTransaksiLastPage = paginationData.TotalPage;
            // toolStripLabel1.Text = $"dari {paginationData.TotalPage.ToString()}";
            toolStrip1.Enabled = true;
            button10.Enabled = true;
            toolStripLabel2.Visible = false;
            ButtonToggleHelper.EnableButtonsByTag(this, "groupbox");
            splitContainer1.Cursor = Cursors.Default;
            FetchLaporanTransaksi = false;
        }


        private async void button10_Click(object sender, EventArgs e)
        {
            fetchLaporanTransaksi();
        }

        private dynamic GetSelectedHtrans()
        {
            var kodeh = (long)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
            return _transaksiData.GetData().Where(x => x.kodeH == kodeh).FirstOrDefault();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0 && dataGridView2.Rows.Count <= 0)
            {
                return;
            }

            dataGridView3.Rows.Clear();
            var htrans = GetSelectedHtrans();
            if (htrans == null)
            {
                return;
            }

            var dtrans = new List<dynamic>();
            foreach (var d in htrans.dtrans)
            {
                var index = dataGridView3.Rows.Add();
                dataGridView3.Rows[index].Cells["Pcs"].Value = d.jumlah;
                dataGridView3.Rows[index].Cells["NamaBarang"].Value = d.masterbarang?.brgNama;
                dataGridView3.Rows[index].Cells["Harga"].Value = d.harga;
                dataGridView3.Rows[index].Cells["Jumlah"].Value = d.jumlah * d.harga;
                dataGridView3.Rows[index].Cells["KurangiStok"].Value = d.kuranginStok;
                dataGridView3.Rows[index].Cells["SN"].Value = d.nmrsn;
            }
        }

        private void dataGridView2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dataGridView2.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private async Task<List<HargaByLevelResult>> GetHarga()
        {
            List<int> kodeBarang = new List<int>();
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells[1]?.Value != null) kodeBarang.Add((int)row.Cells[1]?.Value);
                    }
                }

                List<HargaByLevelResult> result = await _setlevelhargaData.GetHargaByLevel(
                    kodeBarang.ToArray(),
                    Int32.Parse(comboHarga.SelectedValue.ToString()),
                    Int32.Parse(comboPelanggan.SelectedValue.ToString())
                );
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Count(row => row.Cells[1].Value != null) == 0)
            {
                return;
            }

            button7.Enabled = false;
            var harga = await this.GetHarga();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells[1]?.Value != null)
                    {
                        row.Cells[0].Value = Convert.ToInt32(row.Cells[0].Value) == 0 ? 1 : row.Cells[0].Value;
                        var found = harga.Find(x => x.Kodebarang.ToString() == row.Cells[1].Value.ToString());
                        if (found != null)
                        {
                            row.Cells[2].Value = found.Harga;
                            row.Cells[3].Value = Convert.ToInt32(row.Cells[0].Value) *
                                                 Convert.ToInt32(row.Cells[2].Value);
                        }
                    }
                }
            }

            button7.Enabled = true;
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Count(row => row.Cells[1].Value != null) == 0)
            {
                return;
            }

            button6.Cursor = Cursors.WaitCursor;
            button6.Enabled = false;
            var harga = await this.GetHarga();
            if (harga != null)
            {
                var str = "";
                foreach (var x in harga)
                {
                    str += $"{x.Namabarang}: {x.Harga.ToString("#,##0")}";
                }

                MessageBox.Show(str);
            }

            button6.Enabled = true;
            button6.Cursor = Cursors.Default;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _laporanTransaksiPage++;
            fetchLaporanTransaksi();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _laporanTransaksiPage--;
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

        private void toolStripTextBox1_Leave(object sender, EventArgs e)
        {
            fetchLaporanTransaksi();
        }

        private void ResetForm()
        {
            comboGudang.SelectedIndex = 0;
            comboSales.SelectedIndex = 0;
            comboPelanggan.SelectedIndex = 0;
            datePickerTransaksi.Value = DateTime.Now;
            textBoxInfoPenting.Text = "";
            comboTempo.SelectedIndex = 2;
            dataGridView1.Rows.Clear();
            textBoxOngkir.Text = "";
            textBoxBiaya.Text = "";
            textBoxDiskon.Text = "";
            textBoxDpp.Text = "";
            checkBoxPPN.Checked = false;
            textBoxTotal.Text = "";
            KodeEdit = null;
            NoOrder = null;
        }

        private void EnableUpdateForm()
        {
            if (dataGridView2.SelectedRows.Count <= 0 && dataGridView2.Rows.Count <= 0)
            {
                return;
            }

            ResetForm();
            var kodeh = (long)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
            var htrans = _transaksiData.GetData().Where(x => x.kodeH == kodeh).FirstOrDefault();
            if (htrans == null)
            {
                return;
            }

            KodeEdit = (int)htrans.kodeH;
            comboGudang.SelectedValue = (int)htrans.kodegudang;
            comboPelanggan.SelectedValue = (int)htrans.kodePelanggan;
            comboSales.SelectedValue = (int)htrans.kodeSales;
            datePickerTransaksi.Value = htrans.tglTrans;
            textBoxInfoPenting.Text = htrans.infopenting;
            comboTempo.SelectedValue = htrans.tipetempo;
            dataGridView1.Rows.Clear();

            textBoxOngkir.Text = Convert.ToString(htrans.tambahanLainnya);
            textBoxBiaya.Text = Convert.ToString(htrans.jumlahbarangbiaya);
            textBoxDiskon.Text = Convert.ToString(htrans.diskon);
            textBoxDpp.Text = Convert.ToString(htrans.dpp);
            checkBoxPPN.Checked = htrans.ppn > 0;
            textBoxTotal.Text = Convert.ToString(htrans.jumlah);

            foreach (var d in htrans.dtrans)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = d.jumlah;
                row.Cells[1].Value = (int)d.kodebarang;
                row.Cells[2].Value = d.harga;
                row.Cells[3].Value = d.harga * d.jumlah;
                row.Cells[6].Value = d.nmrsn;
                dataGridView1.Rows.Add(row);
            }

            buttonBatalUbah.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            EnableUpdateForm();
        }

        private void buttonBatalUbah_Click(object sender, EventArgs e)
        {
            KodeEdit = null;
            NoOrder = null;
            buttonBatalUbah.Visible = false;
            ResetForm();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b,
                    e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void comboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            _laporanTransaksiPage = 1;
            fetchLaporanTransaksi();
        }

        private void TransaksiForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            var keyPress = e.KeyChar.ToString();
            if (dataGridView2.Focused)
            {
                if (keyPress == "q")
                {
                    _laporanTransaksiPage--;
                }

                if (keyPress == "e")
                {
                    _laporanTransaksiPage++;
                }

                return;
            }
        }

        private void TransaksiForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView2.Focused)
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

        private void button15_Click(object sender, EventArgs e)
        {
            PrintNotaPpn();
        }

        private async void PrintNotaPpn()
        {
            if (dataGridView2.SelectedRows.Count <= 0 && dataGridView2.Rows.Count <= 0)
            {
                return;
            }

            if (PrintNotaPpnRun)
            {
                return;
            }

            PrintNotaPpnRun = true;
            var kodeh = (long)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
            NotaTransaksiPpnResultDto htrans = null;

            try
            {
                htrans = await _transaksiData.GetNotaPpn((int)kodeh);
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

                if (checkBoxPrintFoto.Checked)
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 21, 29).ShowPreview();
                }
                else
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 21, 29).Print();
                }
                //.Print("CustomSize", 970, 551);
            }
            catch (Exception ex)
            {
                ex.Dump();
                MessageBox.Show(ex.Message);
            }

            PrintNotaPpnRun = false;
        }

        private async void PrintNotaOl(int kodeh)
        {
            if (dataGridView2.SelectedRows.Count <= 0 && dataGridView2.Rows.Count <= 0)
            {
                return;
            }

            if (PrintNotaRun)
            {
                return;
            }

            PrintNotaRun = true;

            HtransResult htrans = null;

            try
            {
                htrans = await _transaksiData.GetByKodeh((int)kodeh);
                htrans.Dump();
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
                PrintNotaRun = false;
                return;
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
                PrintNotaRun = false;
                return;
            }


            try
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportEmbeddedResource =
                    "DoranApp.Report.NotaTransaksiOl.rdlc"; // Path to your RDLC file

                var dt = new DataTable();
                dt.Columns.Add("jumlah");
                dt.Columns.Add("brgNama");
                dt.Columns.Add("harga");
                dt.Columns.Add("subTotal");
                foreach (var dtrans in htrans.Dtrans)
                {
                    DataRow row = dt.NewRow();
                    row["jumlah"] = dtrans.Jumlah.ToString("N0");
                    row["brgNama"] = dtrans.Masterbarang?.BrgNama;
                    row["harga"] = dtrans.Harga.ToString("N0");
                    row["subTotal"] = (dtrans.Harga * dtrans.Jumlah).ToString("N0");
                    dt.Rows.Add(row);
                }

                // Set parameters (if any)
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("total", string.Format("{0:N0}", htrans.Jumlah)));
                parameters.Add(new ReportParameter("tglTrans", htrans.TglTrans.ToString("dd/MM/yyyy")));
                parameters.Add(new ReportParameter("kodenota", (string)htrans.Kodenota ?? ""));
                var namaPelanggan = Regex.Replace(htrans.Masterpelanggan?.Nama?.ToString() ?? "", @"[\r\n\t]+", " ");
                parameters.Add(new ReportParameter("namaPelanggan", namaPelanggan));
                parameters.Add(new ReportParameter("namaCust", htrans.NamaCust));
                parameters.Add(new ReportParameter("namaSales", (string)htrans.Sales?.Nama ?? ""));


                reportViewer.LocalReport.SetParameters(parameters.ToArray());

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                // Refresh the report
                reportViewer.RefreshReport();

                var directPrint = new DirectPrint();
                if (checkBoxPrintFoto.Checked)
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 8, 10).ShowPreview();
                }
                else
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 8, 10).Print();
                }
            }
            catch (Exception ex)
            {
                ex.Dump();
                MessageBox.Show(ex.Message);
            }

            PrintNotaRun = false;
        }

        private async void PrintNota(int kodeh)
        {
            if (dataGridView2.SelectedRows.Count <= 0 && dataGridView2.Rows.Count <= 0)
            {
                return;
            }

            if (PrintNotaRun)
            {
                return;
            }

            PrintNotaRun = true;

            HtransResult htrans = null;

            try
            {
                htrans = await _transaksiData.GetByKodeh((int)kodeh);
                htrans.Dump();
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
                PrintNotaRun = false;
                return;
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
                PrintNotaRun = false;
                return;
            }


            try
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportEmbeddedResource =
                    "DoranApp.Report.NotaTransaksi.rdlc"; // Path to your RDLC file

                var dt = new DataTable();
                dt.Columns.Add("no");
                dt.Columns.Add("jumlah");
                dt.Columns.Add("brgNama");
                dt.Columns.Add("harga");
                dt.Columns.Add("subTotal");
                var no = 1;
                foreach (var dtrans in htrans.Dtrans)
                {
                    DataRow row = dt.NewRow();
                    row["no"] = no;
                    row["jumlah"] = dtrans.Jumlah.ToString("N0");
                    row["brgNama"] = dtrans.Masterbarang?.BrgNama;
                    row["harga"] = dtrans.Harga.ToString("N0");
                    row["subTotal"] = (dtrans.Harga * dtrans.Jumlah).ToString("N0");
                    dt.Rows.Add(row);
                    no++;
                }

                // Set parameters (if any)
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("total", string.Format("{0:N0}", htrans.Jumlah)));
                parameters.Add(new ReportParameter("lainnya", string.Format("{0:N0}", htrans.TambahanLainnya)));
                parameters.Add(new ReportParameter("ppn", string.Format("{0:N0}", htrans.Ppn)));
                parameters.Add(new ReportParameter("tglTrans", htrans.TglTrans.ToString("dd/MM/yyyy")));
                parameters.Add(new ReportParameter("kodenota", (string)htrans.Kodenota ?? ""));
                var namaPelanggan = Regex.Replace(htrans.Masterpelanggan?.Nama?.ToString() ?? "", @"[\r\n\t]+", " ");
                parameters.Add(new ReportParameter("namaPelanggan", namaPelanggan));
                ConsoleDump.Extensions.Dump(htrans.Masterpelanggan?.Nama?.ToString());
                parameters.Add(new ReportParameter("namaSales", (string)htrans.Sales?.Nama ?? ""));
                parameters.Add(new ReportParameter("tipeTempo", Helper.TipeTempoToString((sbyte)htrans.Tipetempo)));
                parameters.Add(new ReportParameter("tglTempo", htrans.Tgltempo.ToString("dd/MM/yyyy")));
                parameters.Add(new ReportParameter("R", "1"));
                parameters.Add(new ReportParameter("N", "2"));

                if (!String.IsNullOrWhiteSpace((string)htrans.Kodenota))
                {
                    var barcode64String =
                        BarcodeGenerator.GenerateBase64Barcode((string)htrans.Kodenota, ZXing.BarcodeFormat.CODE_128,
                            400, 40);

                    parameters.Add(new ReportParameter("barcodeImage", barcode64String));
                }

                reportViewer.LocalReport.SetParameters(parameters.ToArray());

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DetailTransaksi", dt));
                // Refresh the report
                reportViewer.RefreshReport();

                var directPrint = new DirectPrint();
                if (checkBoxPrintFoto.Checked)
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 21, 14)
                        .ShowPreview();
                }
                else
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 21, 14)
                        .Print();
                }
                //.Print("CustomSize", 970, 551);
            }
            catch (Exception ex)
            {
                ex.Dump();
                MessageBox.Show(ex.Message);
            }

            PrintNotaRun = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 1)
            {
                return;
            }

            var htrans = GetSelectedHtrans();
            if (htrans == null)
            {
                MessageBox.Show("Transaksi tidak ditemukan");
                return;
            }

            if (checkBoxModeNotaOl.Checked &&
                htrans?.masterpelanggan?.lokasiKota?.lokasiProvinsi?.kode == Constants.KODE_PROV_ONLINE)
            {
                PrintNotaOl((int)htrans.kodeH);
            }
            else
            {
                PrintNota((int)htrans.kodeH);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void OrderToDatagridview(HorderResult order)
        {
            if (order == null)
            {
                MessageBox.Show("Order tidak ditemukan");
                return;
            }

            datePickerTransaksi.Value = order.Tglorder;
            comboSales.SelectedValue = order.Kodesales;
            comboPelanggan.SelectedValue = order.Kodepelanggan;
            textBoxInfoPenting.Text = order.Infopenting;
            comboTempo.SelectedIndex = Helper.TipeTempoValueToIndex(order.Tipetempo ?? -1);
            NoOrder = order.Kodeh;
            dataGridView1.Rows.Clear();
            if (order.Dorder != null)
            {
                foreach (var dorder in order.Dorder)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells["pcs"].Value = dorder.Jumlah;
                    dataGridView1.Rows[index].Cells["item"].Value = dorder.Kodebarang;
                    dataGridView1.Rows[index].Cells["harga"].Value = dorder.Harga;
                    dataGridView1.Rows[index].Cells["jumlah"].Value = dorder.Jumlah * dorder.Harga;
                }
            }

            CalculateSubtotal();
            CalculateTotal();
            buttonBatalUbah.Visible = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                var order = await _OrderData.FindUnprocesedOrderByKodeh(Convert.ToInt32(textBoxNoSo.Text));

                OrderToDatagridview(order);
            }
            catch (ApiException ex)
            {
                byte[] Errorbytes = Encoding.Default.GetBytes(ex.Response);
                var errorString = Encoding.UTF8.GetString(Errorbytes);
                var errors = JObject.Parse(errorString);
                if (errors["message"] != null)
                {
                    MessageBox.Show(errors["message"]?.ToString());
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            button1.Enabled = true;
        }

        private async Task FindOrderByMethod(sbyte method)
        {
            try
            {
                ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
                HorderResult? order = null;
                switch (method)
                {
                    case 0:
                        if (textBoxCariBySeriOnline.Text.Trim() == "")
                        {
                            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                            return;
                        }

                        order = await _OrderData.FindByNoSeriOnlineNotLunas(textBoxCariBySeriOnline.Text);
                        break;
                    case 1:
                        if (textBoxCariByBarcodeOnline.Text.Trim() == "")
                        {
                            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                            return;
                        }

                        order = await _OrderData.FindByNoBarcodeOnlineNotLunas(textBoxCariByBarcodeOnline.Text);
                        break;
                    case 2:
                        if (textBoxCariBy5DigitSeriOnline.Text.Trim().Length != 5)
                        {
                            MessageBox.Show("Harus berisi 5 karakter");
                            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                            return;
                        }

                        order = await _OrderData.FindByNoSeriOnlineNotLunas($"%{textBoxCariBy5DigitSeriOnline.Text}",
                            2);
                        break;
                    case 3:
                        if (textBoxCariBy5DigitBarcodeOnline.Text.Trim().Length != 5)
                        {
                            MessageBox.Show("Harus berisi 5 karakter");
                            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                            return;
                        }

                        order = await _OrderData.FindByNoBarcodeOnlineNotLunas(
                            $"%{textBoxCariBy5DigitBarcodeOnline.Text}", 2);
                        break;
                }

                if (order != null)
                {
                    OrderToDatagridview(order);
                }
                else
                {
                    MessageBox.Show("Orderan tidak ditemukan", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            FindOrderByMethod(0);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            FindOrderByMethod(1);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FindOrderByMethod(2);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FindOrderByMethod(3);
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            try
            {
                var kodeh = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Kodeh"].Value);
                var resp = await _transaksiData.CancelOrder(kodeh);
                checkBoxCancelOrder.Checked = true;
                EnableUpdateForm();
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        }

        private void comboPelanggan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPelanggan.SelectedValue == null)
            {
                return;
            }

            try
            {
                var selected = Convert.ToInt32(comboPelanggan.SelectedValue);
                var pelanggan = _masterpelangganOptions.Where(e => e.Kode == selected).FirstOrDefault();
                checkBoxPPN.Checked = pelanggan.DefaultPpn;
                pelanggan.Dump();
            }
            catch (Exception ex)
            {
#if DEBUG
                ex.Dump();
#endif
            }
        }

        public async void OnSnCheck(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dataGridView1.CurrentRow != null && !SnCheckRun)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                DataGridViewTextBoxEditingControl editingControl = sender as DataGridViewTextBoxEditingControl;
                if (editingControl.Text.Trim() == "")
                {
                    dataGridView1.CurrentRow.Cells[1].Value = null;
                    dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
                    dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
                    return;
                }

                SnCheckRun = true;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[6].Value?.ToString() == editingControl.Text &&
                        row.Index != dataGridView1.CurrentRow.Index && !row.IsNewRow)
                    {
                        MessageBox.Show($"SN Sudah terinput di transaksi ini");
                        await Task.Delay(100);
                        SnCheckRun = false;
                        return;
                    }
                }

                try
                {
                    var res = await _CLient.Get_Barangsn_By_SnAsync(editingControl.Text);
                    if (res.Status == 1)
                    {
                        MessageBox.Show($"Status SN Sudah Terjual di KODEH {res.Kodehjual}");
                        await Task.Delay(100);
                        SnCheckRun = false;
                        return;
                    }


                    dataGridView1.CurrentRow.Cells[0].Value = 1;
                    dataGridView1.CurrentRow.Cells[1].Value = res.Kodebarang;
                    dataGridView1.CurrentRow.Cells[0].ReadOnly = true;
                    dataGridView1.CurrentRow.Cells[1].ReadOnly = true;
                }
                catch (ApiException ex)
                {
                    dataGridView1.CurrentRow.Cells[1].Value = null;
                    dataGridView1.CurrentRow.Cells[0].ReadOnly = false;
                    dataGridView1.CurrentRow.Cells[1].ReadOnly = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                await Task.Delay(100);
                SnCheckRun = false;
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl && dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                DataGridViewTextBoxEditingControl editingControl = e.Control as DataGridViewTextBoxEditingControl;
                if (editingControl != null)
                {
                    editingControl.AcceptsReturn = false;
                    editingControl.KeyUp -= new KeyEventHandler(OnSnCheck);
                    editingControl.KeyUp += new KeyEventHandler
                        (OnSnCheck);
                }
            }

            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                var comboBox = e.Control as DataGridViewComboBoxEditingControl;
                if (comboBox != null)
                {
                    if (checkboxEnableSearch.Checked)
                    {
                        comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                        comboBox.AutoCompleteMode = AutoCompleteMode.Append;
                        // comboBox.Validated -= new EventHandler(combo_Validated);
                        // comboBox.Validated += new EventHandler(combo_Validated);
                    }

                    comboBox.SelectedIndexChanged -= new EventHandler(OnChangeComboItem);
                    comboBox.SelectedIndexChanged += new EventHandler(OnChangeComboItem);

                    comboBox.Enter -= new EventHandler(ctl_Enter);
                    comboBox.Enter += new EventHandler(ctl_Enter);
                }
            }
        }

        void OnChangeComboItem(object sender, EventArgs e)
        {
            try
            {
                MasterbarangOptionWithSnDto item = (MasterbarangOptionWithSnDto)((sender as ComboBox).SelectedItem);
                if (item == null)
                {
                    return;
                }

                var selectedRow = dataGridView1.CurrentCell.RowIndex;
                if (!item.Sn && dataGridView1.Rows[selectedRow].Cells[6]?.Value?.ToString() != "")
                {
                    dataGridView1.Rows[selectedRow].Cells[6].Value = "";
                }
            }
            catch (Exception ex)
            {
                ex.Dump();
            }
        }

        void ctl_Enter(object sender, EventArgs e)
        {
            (sender as ComboBox).DroppedDown = true;
        }

        public static object GetPropValue(object src, string propName)
        {
            if (src == null)
                return null;
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        void combo_Validated(object sender, EventArgs e)
        {
            Object selectedItem = ((ComboBox)sender).SelectedItem;
            DataGridViewComboBoxColumn col =
                (DataGridViewComboBoxColumn)dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex];
            if (!String.IsNullOrEmpty(col.ValueMember))
                dataGridView1.CurrentCell.Value = GetPropValue(selectedItem, col.ValueMember);
            else
                dataGridView1.CurrentCell.Value = selectedItem;
        }

        private void comboPelanggan_Enter(object sender, EventArgs e)
        {
            comboPelanggan.DroppedDown = true;
        }

        private void checkBoxShowAllItem_CheckedChanged(object sender, EventArgs e)
        {
            if (preventEventCheckBoxShowAllItem)
            {
                return;
            }


            preventEventCheckBoxShowAllItem = false;
            DataGridViewComboBoxColumn comboBoxColumn = dataGridView1.Columns["item"] as DataGridViewComboBoxColumn;

            if (comboBoxColumn == null)
            {
                return;
            }


            checkBoxShowAllItem.Enabled = false;
            if (checkBoxShowAllItem.Checked)
            {
                comboBoxColumn.Items.Clear();
                comboBoxColumn.Items.AddRange(_masterbarangOptions.ToArray());
            }
            else
            {
                if (!HadShownyaItem())
                {
                    comboBoxColumn.Items.Clear();
                    comboBoxColumn.Items.AddRange(_masterbarangShownyaOptions.ToArray());
                }
                else
                {
                    MessageBox.Show(
                        "Ada item dikeranjang saat ini menggukan filter semua item, kosongkan keranjang/selesaikan transaksi terlebih dahulu");
                    preventEventCheckBoxShowAllItem = true;
                    checkBoxShowAllItem.Checked = true;
                    preventEventCheckBoxShowAllItem = false;
                }
            }

            checkBoxShowAllItem.Enabled = true;
        }

        private bool HadShownyaItem()
        {
            var result = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[1].Value.Dump();
                if (row.Cells[1].Value != null)
                {
                    var founded = _masterbarangOptions.Where(e => e.BrgKode == Convert.ToInt32(row.Cells[1].Value))
                        .FirstOrDefault();
                    if (founded?.Shownya == 0)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private void TransaksiForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1 && e.Control)
            {
                //button14.Click();
            }

            if (e.KeyCode == Keys.F3)
            {
                fetchLaporanTransaksi();
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // if (dataGridView1.Rows.Count == 0)
            // {
            //     return;
            // }
            //
            // try
            // {
            //     var checkMb = _masterbarangOptions.Find(x =>
            //         x.BrgKode.HasValue &&
            //         x.BrgKode.ToString() == dataGridView1.Rows[e.RowIndex].Cells[1]?.Value.ToString());
            //     if (checkMb == null)
            //     {
            //         e.Cancel = true;
            //         return;
            //     }
            //
            //     if (e.ColumnIndex == 6)
            //     {
            //         e.Cancel = !checkMb.Sn;
            //     }
            // }
            // catch (Exception ex)
            // {
            // }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBoxGenerateKeyLimit.Text = Helper.DekripKeyLimitTagihan(textBoxGenerateToken.Text);
        }

        private void buttonCopyKeyLimit_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxGenerateKeyLimit.Text);
        }

        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("FORMRR");
            }
        }
    }

    internal class HargaByLevelResult
    {
        public int Harga { get; set; }
        public int Kodebarang { get; set; }
        public string? Namabarang { get; set; }
    }
}