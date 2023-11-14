using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleDump;
using DoranApp.Data;
using DoranApp.Exceptions;
using DoranApp.Properties;
using DoranApp.Utils;
using DoranApp.View.UserForms;
using Microsoft.Reporting.WinForms;

namespace DoranApp.View
{
    public partial class OrderInputForm : Form
    {
        private Client _Client = new Client();
        private List<Masterpengeluaran> _ekspedisiOption = new List<Masterpengeluaran>();
        private MasterbarangData _masterbarang = new MasterbarangData();
        private List<MasterbarangOptionDto> _masterbarangOptions = new List<MasterbarangOptionDto>();
        private MastergudangData _MastergudangData = new MastergudangData();
        private List<MastergudangOptionDto> _mastergudangOptions = new List<MastergudangOptionDto>();
        private MasterpelangganData _masterpelangganData = new MasterpelangganData();
        private List<CommonResultDto> _masterpelangganOptions = new List<CommonResultDto>();
        private MasterpengeluaranData _masterpengeluaranData = new MasterpengeluaranData();

        private OrderData _OrderData = new OrderData();
        private PenyiaporderData _PenyiapOrderData = new PenyiaporderData();

        private List<Penyiaporder> _PenyiaporderOptions = new List<Penyiaporder>();
        private SalesData _salesData = new SalesData();
        private List<SalesOptionDto> _salesOptions = new List<SalesOptionDto>();
        private SetlevelhargaData _setlevelhargaData = new SetlevelhargaData();
        private List<CommonResultDto> _setlevelhargaOptions = new List<CommonResultDto>();

        private bool EditHeaderOnly = false;

        public bool FetchOrderRun = false;
        private decimal GrandTotal = 0;

        private int? KodeEdit = null;

        public long LaporanOrderLastPage = 0;
        public bool SaveRun = false;

        private decimal SubTotal = 0;

        public OrderInputForm()
        {
            InitializeComponent();
            var kagud = new[] { "kagud", "masterkagud" };
            if (kagud.Contains(Session.GetUser().Akses.ToLower().Trim()))
            {
                splitContainer1.Panel1Collapsed = true;
                buttonUbahAtas.Visible = false;
                buttonUpdateFull.Visible = false;
                buttonCekAndSetSiap.Visible = false;
                btnBatalkanHeader.Visible = false;
                groupBox8.Visible = false;
            }
        }

        public long _laporanOrderPage
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
            set { toolStripTextBox1.Text = value > 0 ? value.ToString() : "1"; }
        }

        private void OrderInputForm_Load(object sender, EventArgs e)
        {
            datePickerOrder.Select();
            var clientManager = new WebSocketClientManager();
            clientManager.Subscribe("order", eventData =>
            {
                if (eventData != null)
                {
                }
            });

            dataGridView1.EnableDoubleBuffered(true);
            dataGridView2.EnableDoubleBuffered(true);
            dataGridView3.EnableDoubleBuffered(true);
            comboPageSize.SelectedIndex = 0;
            comboJenisEkspedisi.SelectedIndex = 0;
            comboTempo.SelectedIndex = 0;

            FetchSales();
            FetchPelanggan();
            FetchLevelHarga();
            FetchEkspedisi();
            FetchGudang();
            FetchPenyiapOrder();
            CreateDatagridview();
            dataGridView2.DataSource = _OrderData.GetDataTable();
            dataGridView2.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView2.Columns[3].DefaultCellStyle.Format = "N0";
            dataGridView2.Columns[6].DefaultCellStyle.Format = "N0";
            dataGridView3.Columns[2].DefaultCellStyle.Format = "N0";
            dataGridView3.Columns[3].DefaultCellStyle.Format = "N0";
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public async Task CreateDatagridview()
        {
            await FetchMasterbarang();
            var dc = new DataGridViewComboBoxColumn();
            dc.ValueMember = "BrgKode";
            dc.DataPropertyName = "BrgKode"; // The ValueMember
            dc.DisplayMember = "BrgNama"; // The DisplayMember
            dc.HeaderText = "Item";
            dc.Name = "item";
            dc.DisplayStyleForCurrentCellOnly = true;
            dc.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dc.FlatStyle = FlatStyle.Flat;
            dc.AutoComplete = true;

            foreach (var x in _masterbarangOptions)
            {
                dc.Items.Add(x);
            }

            dataGridView1.Columns.Add("pcs", "Pcs");
            dataGridView1.Columns.Add(dc);
            dataGridView1.Columns.Add("harga", "Harga");
            dataGridView1.Columns.Add("jumlah", "Jumlah");
            dataGridView1.Columns.Add("keterangan", "Keterangan");

            dataGridView1.Columns["pcs"].Width = 50;
            dataGridView1.Columns["item"].Width = 230;
            dataGridView1.Columns["keterangan"].Width = 110;
            dataGridView1.Columns["pcs"].ValueType = typeof(decimal);
            dataGridView1.Columns["harga"].ValueType = typeof(decimal);
            dataGridView1.Columns["jumlah"].ValueType = typeof(decimal);
            dataGridView1.Columns["pcs"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["harga"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["jumlah"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["jumlah"].ReadOnly = true;
        }

        public async Task FetchMasterbarang()
        {
            var data = await _masterbarang.GetNameAndKodeOnly();
            _masterbarangOptions = (List<MasterbarangOptionDto>)data.Response;
        }

        public async Task FetchPenyiapOrder()
        {
            try
            {
                _PenyiaporderOptions = await _PenyiapOrderData.GetActivePenyiapOrder();
                comboSetPenyiap.ValueMember = "Kode";
                comboSetPenyiap.DisplayMember = "Nama";
                comboSetPenyiap.DataSource = _PenyiaporderOptions;
            }
            catch (Exception ex)
            {
                ConsoleDump.Extensions.Dump(ex);
            }
        }

        public async Task FetchGudang()
        {
            try
            {
                _mastergudangOptions = await _MastergudangData.GetMastergudangOption();
                comboFilterGudang.DataSource = _mastergudangOptions.ToList();
            }
            catch (Exception ex)
            {
            }
        }

        public async Task FetchSales()
        {
            try
            {
                _salesOptions = await _salesData.GetNameAndKodeOnly();
                comboSales.ValueMember = "Kode";
                comboSales.DisplayMember = "Nama";
                comboSales.DataSource = _salesOptions.ToList();

                comboFilterSales.ValueMember = "Kode";
                comboFilterSales.DisplayMember = "Nama";
                comboFilterSales.DataSource = _salesOptions.ToList().Prepend(new SalesOptionDto
                {
                    Kode = null,
                    Nama = "SEMUA"
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                var data = await _masterpelangganData.GetNameAndKodeOnly();
                _masterpelangganOptions = (List<CommonResultDto>)data.Response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboPelanggan.ValueMember = "Kode";
            comboPelanggan.DisplayMember = "Nama";
            comboPelanggan.DataSource = _masterpelangganOptions;
        }

        private async Task FetchEkspedisi()
        {
            try
            {
                var data = await _masterpengeluaranData.GetEkspedisiOnly();
                _ekspedisiOption = (List<Masterpengeluaran>)data.Response;
                _ekspedisiOption.Prepend(new Masterpengeluaran
                {
                    Kode = 0,
                    Nama = "Belum Diisi"
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboEkspedisi.ValueMember = "Kode";
            comboEkspedisi.DisplayMember = "Nama";
            comboEkspedisi.DataSource = _ekspedisiOption;
        }

        private void CalculateSubtotal()
        {
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
                    decimal pcs = Convert.ToDecimal(row.Cells["pcs"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["harga"].Value);
                    decimal rowSubtotal = pcs * price;
                    row.Cells["jumlah"].Value = rowSubtotal;
                    subtotal += rowSubtotal;
                }
                else
                {
                    row.Cells["jumlah"].Value = 0;
                    subtotal += 0;
                }
            }

            SubTotal = subtotal;
        }

        private void CalculateTotal()
        {
            textBoxTotal.Text = SubTotal.ToString();
        }

        private void ToggleLoading()
        {
            FetchOrderRun = !FetchOrderRun;
            btnFilter.Enabled = !FetchOrderRun;
            miniToolStrip.Enabled = !FetchOrderRun;
            toolStripLabel2.Visible = FetchOrderRun;
            dataGridView2.Enabled = !FetchOrderRun;
            dataGridView3.Enabled = !FetchOrderRun;
            this.Cursor = FetchOrderRun ? Cursors.WaitCursor : Cursors.Default;
            if (FetchOrderRun)
            {
                ButtonToggleHelper.DisableButtonsByTag(this, "enableOnSelect");
                ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            }
            else
            {
                ButtonToggleHelper.EnableButtonsByTag(this, "enableOnSelect");
                ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
            }
        }

        private async Task FetchOrder()
        {
            if (FetchOrderRun == true)
            {
                return;
            }

            ToggleLoading();
            try
            {
                _OrderData.SetQuery(new
                {
                    Kodegudang = comboFilterGudang.SelectedValue?.ToString(),
                    NamaPelanggan = textBoxFilterNama.Text.Trim(),
                    Kodesales = comboFilterSales.SelectedValue?.ToString(),
                    Dicetak = Helper.GetSelectedRadioButtonTag(groupBoxDicetak),
                    LevelOrder = Helper.GetSelectedRadioButtonTag(groupFilterLevel),
                    SalesOl = Helper.GetSelectedRadioButtonTag(groupBoxFilterJenisTrans),
                    NamaCust = textBoxFilterNamaCust.Text,
                    NoSeriOnline = textBoxFilterNoSeriOnline.Text,
                    PageSize = comboPageSize.SelectedItem?.ToString() ?? "50",
                    Page = _laporanOrderPage <= 0 ? 1 : _laporanOrderPage,
                });
                await _OrderData.Refresh();
                dataGridView3.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var paginationData = _OrderData.GetPaginationData();
            _laporanOrderPage = paginationData.Page;
            LaporanOrderLastPage = paginationData.TotalPage;
            toolStripLabel1.Text = $"dari {paginationData.TotalPage.ToString()}";

            ToggleLoading();
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            _laporanOrderPage = 1;
            await FetchOrder();
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                dataGridView2.FirstDisplayedScrollingRowIndex = 0;
            }

            dataGridView2.ClearSelection();
        }

        private string GetStatusLunas(int lunas)
        {
            return (lunas == 0) ? "Belum" :
                (lunas == 1) ? "Kurang" :
                (lunas == 2) ? "BERES" : "CANCEL";
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0 || dataGridView2.Rows.Count <= 0)
            {
                ButtonToggleHelper.DisableButtonsByTag(this, "enableOnSelect");
                return;
            }

            dataGridView3.Rows.Clear();
            try
            {
                dataGridView2.SelectedRows[0].Cells["Kodeh"].Value.Dump();
                var kodeh = (int)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;

                var horder = _OrderData.GetData()?.Where(x => x.Kodeh == kodeh).FirstOrDefault();
                if (horder == null)
                {
                    return;
                }

                foreach (var d in horder.Dorder)
                {
                    var index = dataGridView3.Rows.Add();
                    dataGridView3.Rows[index].Cells["Pcs"].Value = d.Jumlah;
                    dataGridView3.Rows[index].Cells["NamaBarang"].Value = d.Masterbarang?.BrgNama;
                    dataGridView3.Rows[index].Cells["Harga"].Value = d.Harga;
                    dataGridView3.Rows[index].Cells["Krm"].Value = d.Jumlahdikirim;
                    dataGridView3.Rows[index].Cells["Status"].Value = GetStatusLunas(d.Lunas);
                    dataGridView3.Rows[index].Cells["BeresOrCancel"].Value = d.Keterangancancel;
                    dataGridView3.Rows[index].Cells["Keterangan"].Value = d.Keterangan;
                    dataGridView3.Rows[index].Cells["Koded"].Value = d.Koded;
                }

                comboSetPenyiap.SelectedValue = (sbyte)horder.Kodepenyiap;
                ButtonToggleHelper.EnableButtonsByTag(this, "enableOnSelect");
            }
            catch (Exception ex)
            {
                ConsoleDump.Extensions.Dump(ex);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _laporanOrderPage--;
            FetchOrder();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _laporanOrderPage = 1;
            FetchOrder();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _laporanOrderPage++;
            FetchOrder();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            _laporanOrderPage = LaporanOrderLastPage;
            FetchOrder();
        }

        private void OrderInputForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            var keyPress = e.KeyChar.ToString();

            if (dataGridView2.Focused)
            {
                ConsoleDump.Extensions.Dump(e.KeyChar);
                if (keyPress == "q")
                {
                    _laporanOrderPage--;
                }

                if (keyPress == "e")
                {
                    _laporanOrderPage++;
                }
            }
        }

        private void OrderInputForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView2.Focused)
            {
                if (e.KeyCode == Keys.Q)
                {
                    FetchOrder();
                }

                if (e.KeyCode == Keys.E)
                {
                    FetchOrder();
                }
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b,
                    e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculateSubtotal();
            CalculateTotal();
        }


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int[] restrictedCells = { 0, 2, 3 }; // Replace with the cell indices you want to restrict only allow number

            if (restrictedCells.Contains(dataGridView1.CurrentCell.ColumnIndex))
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(textBoxNumberOnly_KeyPress);
                    textBox.TextChanged += new EventHandler(textBoxNumberOnly_TextChanged);
                }
            }
        }

        private void textBoxNumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && (sender as TextBox)?.Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBoxNumberOnly_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text;

                // Remove non-numeric characters
                textBox.Text = new string(text.Where(c => char.IsDigit(c) || c == '.').ToArray());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // var isSelected = dataGridView1.SelectedRows.Count > 0 && dataGridView1.Rows.Count > 0;
            // buttonDeleteCart.Enabled = isSelected;
        }

        private void buttonDeleteCart_Click(object sender, EventArgs e)
        {
            ConsoleDump.Extensions.Dump(dataGridView1.CurrentCell);
            if (dataGridView1.CurrentCell != null && !dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].IsNewRow)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex;

                if (rowIndex >= 0)
                {
                    dataGridView1.Rows.RemoveAt(rowIndex);
                }
            }

            if (dataGridView1.Rows.Count <= 0)
            {
                dataGridView1.Rows.Add();
            }
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


            datePickerJatuhTempo.Value = datePickerOrder.Value.AddDays(getTipeTempo());
        }

        private async Task SaveHeader()
        {
            if (SaveRun)
            {
                return;
            }

            SaveRun = true;
            try
            {
                buttonBatalUbah.Enabled = false;
                button9.Enabled = false;
                var res = await _OrderData.UpdateHeader(KodeEdit.ToString(), new
                {
                    tglorder = datePickerOrder.Value,
                    keterangan = textBoxKeterangan.Text,
                    Kodepelanggan = comboPelanggan.SelectedValue,
                    kodesales = comboSales.SelectedValue,
                    kodeexp = comboEkspedisi.SelectedValue,
                    kirimmelalui = comboJenisEkspedisi.SelectedIndex,
                    ppn = checkBoxPPN.Checked ? (int)Math.Floor(textBoxPPN.UnformattedValue) : 0,
                    tipetempo = getTipeTempo(),
                    Tgltempo = datePickerJatuhTempo.Value.ToString("yyyy-MM-dd"),
                    Infopenting = textBoxInfoPenting.Text,
                    noSeriOnline = textBoxNoSeriOnline.Text.Trim(),
                    barcodeonline = textBoxBarcodeonline.Text.Trim(),
                    namaCust = textboxNamaCust.Text.Trim(),
                    nmrHp = textboxNmrHp.Text.Trim(),
                });

                MessageBox.Show("Transaksi berhasil disimpan");
                dataGridView1.Rows.Clear();
                buttonBatalUbah.Visible = false;
                ResetForm();
                FetchOrder();
            }
            catch (Exception ex)
            {
                button9.Enabled = true;
                MessageBox.Show(ex.Message);
            }

            button9.Enabled = true;
            buttonBatalUbah.Enabled = true;
            SaveRun = false;
        }

        private async Task SaveOrder()
        {
            if (SaveRun)
            {
                return;
            }

            SaveRun = true;
            try
            {
                List<dynamic> Details = new List<dynamic>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells[1]?.Value == null)
                        {
                            MessageBox.Show("Ada nama barang yang belum di isi");
                            dataGridView1.CurrentCell = row.Cells[1];
                            dataGridView1.BeginEdit(true);
                            return;
                        }

                        Details.Add(new
                        {
                            kodebarang = row.Cells[1]?.Value,
                            jumlah = (int)Math.Floor((decimal)row.Cells[0]?.Value),
                            harga = (int)Math.Floor((decimal)row.Cells[2]?.Value),
                            keterangan = row.Cells[4]?.Value ?? "",
                        });
                    }
                }

                if (Details.Count == 0)
                {
                    MessageBox.Show("Isi terlebih dahulu item nya");
                    return;
                }

                buttonBatalUbah.Enabled = false;
                button9.Enabled = false;
                var res = await _OrderData.CreateOrUpdate(KodeEdit == null ? null : KodeEdit.ToString(), new
                {
                    tglorder = datePickerOrder.Value,
                    keterangan = textBoxKeterangan.Text,
                    Kodepelanggan = comboPelanggan.SelectedValue,
                    kodesales = comboSales.SelectedValue,
                    kodeexp = comboEkspedisi.SelectedValue,
                    kirimmelalui = comboJenisEkspedisi.SelectedIndex,
                    ppn = checkBoxPPN.Checked ? (int)Math.Floor(textBoxPPN.UnformattedValue) : 0,
                    tipetempo = getTipeTempo(),
                    Tgltempo = datePickerJatuhTempo.Value.ToString("yyyy-MM-dd"),
                    Infopenting = textBoxInfoPenting.Text,
                    noSeriOnline = textBoxNoSeriOnline.Text.Trim(),
                    barcodeonline = textBoxBarcodeonline.Text.Trim(),
                    namaCust = textboxNamaCust.Text.Trim(),
                    nmrHp = textboxNmrHp.Text.Trim(),
                    Details = Details
                });

                MessageBox.Show("Transaksi berhasil disimpan");
                dataGridView1.Rows.Clear();
                buttonBatalUbah.Visible = false;
                ResetForm();
                FetchOrder();
            }
            catch (Exception ex)
            {
                button9.Enabled = true;
                MessageBox.Show(ex.Message);
            }

            button9.Enabled = true;
            buttonBatalUbah.Enabled = true;
            SaveRun = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboSales.SelectedValue == null)
            {
                MessageBox.Show("Pilih sales terlebih dahulu");
                comboSales.Select();
                return;
            }

            var checkSales = _salesOptions.Where(e => e.Kode == Convert.ToInt32(comboSales.SelectedValue))
                .FirstOrDefault();
            checkSales.Dump();
            if (checkSales == null)
            {
                MessageBox.Show("Pilih sales terlebih dahulu");
                comboSales.Select();
                return;
            }

            if (checkSales?.Salesol > 0 && textboxNamaCust.Text.Trim() == "")
            {
                MessageBox.Show("Tidak Bisa Input karena Nama Belum diisi");
                textboxNamaCust.Focus();
                return;
            }

            if (checkSales?.Salesol > 0 && textboxNmrHp.Text.Trim() == "")
            {
                MessageBox.Show("Tidak Bisa Input karena No HP Belum diisi");
                textboxNmrHp.Focus();
                return;
            }

            if (comboPelanggan.SelectedValue == null)
            {
                MessageBox.Show("Tidak Bisa Input karena Pelanggan belum diisi");
                comboPelanggan.Select();
                return;
            }

            var selectedPelanggan = Convert.ToInt32(comboPelanggan.SelectedValue);
            if (comboEkspedisi.SelectedValue == null
                && selectedPelanggan != 291 // JTCASH
                && selectedPelanggan != 2944 //JTCASH DORAN GARMIN
                && selectedPelanggan != 2945 //JTCASH DORAN GADGET
               )
            {
                MessageBox.Show(" Tidak Bisa Input karena Ekspedisi Harus diisi");
                comboEkspedisi.Select();
                return;
            }

            if (EditHeaderOnly)
            {
                SaveHeader();
            }
            else
            {
                SaveOrder();
            }
        }

        private void comboTempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTempo();
        }

        private void datePickerOrder_ValueChanged(object sender, EventArgs e)
        {
            UpdateTempo();
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

        private void ResetForm()
        {
            comboPelanggan.SelectedIndex = 0;
            comboSales.SelectedIndex = 0;
            datePickerOrder.Value = DateTime.Now;
            textboxNamaCust.Clear();
            textboxNmrHp.Clear();
            textBoxBarcodeonline.Clear();
            textBoxNoSeriOnline.Clear();
            textBoxKodeOrderApps.Clear();
            textBoxInfoPenting.Clear();
            comboTempo.SelectedIndex = 0;
            comboEkspedisi.SelectedIndex = 0;
            comboJenisEkspedisi.SelectedIndex = 0;
            dataGridView1.Rows.Clear();
            checkBoxPPN.Checked = false;
            textBoxTotal.Text = "";
            KodeEdit = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BaseUbah(true);
        }

        private void buttonBatalUbah_Click(object sender, EventArgs e)
        {
            ResetForm();
            buttonBatalUbah.Visible = false;
        }

        private async Task SetPenyiap(bool isRetry = false, string passwordPaksa = null)
        {
            if (comboSetPenyiap.SelectedValue == null)
            {
                MessageBox.Show(Resources.ErrorPilihPenyiapOrder, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dataGridView2.SelectedRows.Count <= 0 || dataGridView2.Rows.Count <= 0 || SaveRun)
            {
                return;
            }

            var kodeh = (int)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
            var horder = _OrderData.GetData()?.Where(x => x.Kodeh == kodeh).FirstOrDefault();
            if (horder == null)
            {
                return;
            }

            if (Convert.ToInt32(horder.Historynya) >= 4)
            {
                MessageBox.Show("Tidak bisa set penyiap karena Trans Online belum dicek");
                return;
            }

            SaveRun = true;
            if (!isRetry)
            {
                var confirm = MessageBox.Show(Resources.AskSetPenyiapOrder, "Konfirmasi", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (confirm == DialogResult.No)
                {
                    SaveRun = false;
                    return;
                }
            }

            try
            {
                var data = await _OrderData.SetPenyiapOrder(kodeh,
                    Convert.ToInt32(comboSetPenyiap.SelectedValue?.ToString() ?? "0"),
                    passwordPaksa);
                await FetchOrder();
                SaveRun = false;
            }
            catch (RestException ex)
            {
                if (ex.ErrorType == "LIMIT_TRANSAKSI")
                {
                    var inputPaksa = new InputPaksaUserForm();
                    inputPaksa.textboxMemo.Text = ex.Data?.limitMessage?.ToString();
                    if (ex.Data?.isPasswordError)
                    {
                        MessageBox.Show("Password tidak valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    var inputPaksaResult = inputPaksa.ShowDialog();
                    if (inputPaksaResult == DialogResult.OK)
                    {
                        SaveRun = false;
                        SetPenyiap(true, inputPaksa.textBoxPassword.Text);
                    }
                }
                else
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            catch (Exception ex)
            {
                // ConsoleDump.Extensions.Dump(ex);
                MessageBox.Show(ex.Message, "Error");
            }

            SaveRun = false;
        }

        private async void buttonSetPenyiap_Click(object sender, EventArgs e)
        {
            SetPenyiap();
        }

        private void BaseUbah(bool withDetail = false)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count <= 0 || dataGridView2.Rows.Count <= 0)
                {
                    return;
                }

                ResetForm();
                var kodeh = (long)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
                var horder = _OrderData.GetData().Where(x => x.Kodeh == kodeh).FirstOrDefault();
                if (horder == null)
                {
                    return;
                }

                KodeEdit = (int)horder.Kodeh;
                EditHeaderOnly = !withDetail;
                comboPelanggan.SelectedValue = (int)horder.Kodepelanggan;
                comboSales.SelectedValue = (int)horder.Kodesales;
                datePickerOrder.Value = horder.Tglorder;
                textboxNamaCust.Text = horder.NamaCust;
                textboxNmrHp.Text = horder.NmrHp;
                textBoxBarcodeonline.Text = horder.Barcodeonline;
                textBoxNoSeriOnline.Text = horder.NoSeriOnline;
                textBoxKodeOrderApps.Text = horder.Kodeorderapps.ToString();
                textBoxInfoPenting.Text = horder.Infopenting;
                comboTempo.SelectedValue = horder.Tipetempo;
                comboEkspedisi.SelectedValue = horder.Kodeexp;
                comboJenisEkspedisi.SelectedValue = horder.Kirimmelalui;
                dataGridView1.Rows.Clear();

                checkBoxPPN.Checked = horder.Ppn > 0;
                textBoxTotal.Text = Convert.ToString(horder.Jumlah);
                if (withDetail)
                {
                    foreach (var d in horder.Dorder)
                    {
                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        row.Cells[0].Value = (decimal)d.Jumlah;
                        row.Cells[1].Value = (int)d.Kodebarang;
                        row.Cells[2].Value = (decimal)d.Harga;
                        row.Cells[3].Value = d.Harga * d.Jumlah;
                        row.Cells[4].Value = d.Keterangan;
                        dataGridView1.Rows.Add(row);
                    }
                }

                buttonBatalUbah.Visible = true;
            }
            catch (Exception ex)
            {
                ConsoleDump.Extensions.DumpObject(ex);
            }
        }

        private void buttonUbahAtas_Click(object sender, EventArgs e)
        {
            BaseUbah();
        }

        private void OrderInputForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (EditHeaderOnly)
                {
                    SaveHeader();
                }
                else
                {
                    SaveOrder();
                }
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0 || dataGridView2.Rows.Count <= 0 || FetchOrderRun)
            {
                return;
            }

            FetchOrderRun = true;
            ButtonToggleHelper.DisableButtonsByTag(this, "enableOnSelect");
            ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            LoadingButtonHelper.SetLoadingState(button1, true);
            try
            {
                var kodeh = (int)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;

                var horder = _OrderData.GetData()?.Where(x => x.Kodeh == kodeh).FirstOrDefault();
                if (horder == null)
                {
                    ButtonToggleHelper.EnableButtonsByTag(this, "enableOnSelect");
                    ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                    LoadingButtonHelper.SetLoadingState(button1, false);
                    FetchOrderRun = false;
                    return;
                }

                var hargaNol = horder.Dorder.Where(e => e.Harga == 0).FirstOrDefault();


                if (hargaNol != null)
                {
                    var confirmHargaNol = MessageBox.Show(
                        "Ada Harga yang 0, Apakah tetap ingin memeriksa transaksi ini ?", "Konfirmasi",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmHargaNol == DialogResult.No)
                    {
                        LoadingButtonHelper.SetLoadingState(button1, false);
                        ButtonToggleHelper.EnableButtonsByTag(this, "enableOnSelect");
                        ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                        FetchOrderRun = false;
                        return;
                    }
                }

                await _OrderData.TimOnlineCek(kodeh);
                FetchOrderRun = false;
                await FetchOrder();
                if (dataGridView2.Rows.Count > 0)
                {
                    dataGridView2.Rows[0].Selected = true;
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

            FetchOrderRun = false;
            ButtonToggleHelper.EnableButtonsByTag(this, "enableOnSelect");
            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
            LoadingButtonHelper.SetLoadingState(button1, false);
        }

        private HorderResult GetHorderFromDgv2()
        {
            try
            {
                var kodeh = (int)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
                if (kodeh == null)
                {
                    return null;
                }

                return _OrderData.GetData()?.Where(x => x.Kodeh == kodeh).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private DorderResult GetDorderFromDgv3(List<DorderResult> dorder)
        {
            try
            {
                var koded = (int)dataGridView3.SelectedRows[0].Cells["Koded"].Value;
                if (koded == null)
                {
                    return null;
                }

                return dorder?.Where(x => x.Koded == koded)?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task RunCancelDetailOrder(sbyte kodeCancel, string confirmText, string sebabText = "")
        {
            var horder = GetHorderFromDgv2();
            if (horder == null || FetchOrderRun)
            {
                return;
            }

            var dorder = GetDorderFromDgv3(horder?.Dorder?.ToList());
            if (dorder == null || FetchOrderRun)
            {
                return;
            }

            var confirmForm = new CancelOrderConfirmationForm(_OrderData, horder.Kodeh, dorder.Koded, kodeCancel);
            confirmForm.Text = "Konfirmasi Batalkan Orderan";
            confirmForm.textBoxSebab.Text = sebabText;
            var confirm = confirmForm.ShowDialog();
            if (confirm == DialogResult.OK && dataGridView3.SelectedRows.Count > 0 && dataGridView3.Rows.Count > 0)
            {
            }
        }

        private async void btnBatalkan_Click(object sender, EventArgs e)
        {
            await RunCancelDetailOrder(3, "Konfirmasi Batalkan Orderan");
        }

        private async void btnLunasPaksa_Click(object sender, EventArgs e)
        {
            await RunCancelDetailOrder(5, "Konfirmasi Bereskan Orderan");
        }

        private async void btnPendingOrder_Click(object sender, EventArgs e)
        {
            await RunCancelDetailOrder(4, "Konfirmasi Anggap Sebagai Pending Order", "Tunggu Masuk");
        }

        private async void btnBatalkanHeader_Click(object sender, EventArgs e)
        {
            var kodeh = (int)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;

            var horder = _OrderData.GetData()?.Where(x => x.Kodeh == kodeh).FirstOrDefault();
            if (horder == null || FetchOrderRun)
            {
                return;
            }

            var confirm = MessageBox.Show("Apakah anda yakin ingin membatalkan header ini?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No)
            {
                return;
            }

            FetchOrderRun = true;
            ButtonToggleHelper.DisableButtonsByTag(this, "enableOnSelect");
            ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            LoadingButtonHelper.SetLoadingState(btnBatalkanHeader, true);
            await _OrderData.CancelOrderHeader(horder.Kodeh);
            FetchOrderRun = false;
            await FetchOrder();
            LoadingButtonHelper.SetLoadingState(btnBatalkanHeader, false);
            ButtonToggleHelper.EnableButtonsByTag(this, "enableOnSelect");
            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                await _Client.Set_Ada_OrderanAsync();
                MessageBox.Show("Berhasil");
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
            }

            button2.Enabled = true;
        }

        private async Task<PrintOrderResultDto?> GetPrintData()
        {
            if (dataGridView2.SelectedRows.Count <= 0 || dataGridView2.Rows.Count <= 0 || FetchOrderRun)
            {
                return null;
            }

            var kodeh = (int)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
            try
            {
                return await _Client.Print_OrderanAsync(kodeh);
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
            }

            return null;
        }

        private async Task PrintData(bool file)
        {
            var printData = await GetPrintData();
            if (printData == null)
            {
                return;
            }

            printData.Dump();
            if (printData.PrintOl == 1)
            {
                PrintNotaOl(printData, file);
            }
            else
            {
                PrintNota(printData, file);
            }
        }

        private void PrintNotaOl(PrintOrderResultDto printData, bool file)
        {
            var horderPrint = printData.Horder;
            try
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportEmbeddedResource =
                    "DoranApp.Report.NotaOrderanOl.rdlc"; // Path to your RDLC file

                var dt = new DataTable();
                dt.Columns.Add("pcs");
                dt.Columns.Add("nama");
                dt.Columns.Add("keterangan");
                foreach (var d in printData.Dorder)
                {
                    DataRow row = dt.NewRow();
                    row["pcs"] = d.Pcs;
                    row["nama"] = d.NamaBarang;
                    row["keterangan"] = d.KeteranganBrg;
                    dt.Rows.Add(row);
                }

                // Set parameters (if any)
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("tanggal", horderPrint.Tanggal));
                parameters.Add(new ReportParameter("kodeh", horderPrint.KodeH.ToString()));

                var namaPelanggan = Regex.Replace(horderPrint.Nama ?? "", @"[\r\n\t]+", " ");
                parameters.Add(new ReportParameter("namaPelanggan", namaPelanggan));
                parameters.Add(new ReportParameter("namaSales", horderPrint.NamaSales ?? ""));
                parameters.Add(new ReportParameter("infoPenting", horderPrint.InfoPenting ?? ""));
                parameters.Add(new ReportParameter("penyiapOrder", horderPrint.PenyiapOrder ?? ""));
                parameters.Add(new ReportParameter("tanggalInput", horderPrint.TanggalInput ?? ""));
                parameters.Add(new ReportParameter("tanggalCetak", horderPrint.TanggalCetak ?? ""));
                parameters.Add(new ReportParameter("keterangan", horderPrint.Keterangan ?? ""));
                parameters.Add(new ReportParameter("namaExp", horderPrint.NamaExp ?? ""));
                parameters.Add(new ReportParameter("melalui", horderPrint.Melalui ?? ""));
                parameters.Add(new ReportParameter("NR", printData.TotalBarangLabel ?? ""));

                reportViewer.LocalReport.SetParameters(parameters.ToArray());

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                // Refresh the report
                reportViewer.RefreshReport();

                var directPrint = new DirectPrint();
                if (file)
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 8, 11).ShowPreview();
                }
                else
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 8, 11).Print();
                }
            }
            catch (Exception ex)
            {
                ex.Dump();
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintNota(PrintOrderResultDto printData, bool file)
        {
            var horderPrint = printData.Horder;
            try
            {
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportEmbeddedResource =
                    "DoranApp.Report.NotaOrderan.rdlc"; // Path to your RDLC file

                var dt = new DataTable();
                dt.Columns.Add("pcs");
                dt.Columns.Add("nama");
                dt.Columns.Add("keterangan");
                foreach (var d in printData.Dorder)
                {
                    DataRow row = dt.NewRow();
                    row["pcs"] = d.Pcs;
                    row["nama"] = d.NamaBarang;
                    row["keterangan"] = d.KeteranganBrg;
                    dt.Rows.Add(row);
                }

                // Set parameters (if any)
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("tanggal", horderPrint.Tanggal));
                parameters.Add(new ReportParameter("kodeh", horderPrint.KodeH.ToString()));

                var namaPelanggan = Regex.Replace(horderPrint.Nama ?? "", @"[\r\n\t]+", " ");
                parameters.Add(new ReportParameter("namaPelanggan", namaPelanggan));
                parameters.Add(new ReportParameter("namaSales", horderPrint.NamaSales ?? ""));
                parameters.Add(new ReportParameter("infoPenting", horderPrint.InfoPenting ?? ""));
                parameters.Add(new ReportParameter("penyiapOrder", horderPrint.PenyiapOrder ?? ""));
                parameters.Add(new ReportParameter("tanggalInput", horderPrint.TanggalInput ?? ""));
                parameters.Add(new ReportParameter("tanggalCetak", horderPrint.TanggalCetak ?? ""));
                parameters.Add(new ReportParameter("keterangan", horderPrint.Keterangan ?? ""));
                parameters.Add(new ReportParameter("namaExp", horderPrint.NamaExp ?? ""));
                parameters.Add(new ReportParameter("melalui", horderPrint.Melalui ?? ""));
                parameters.Add(new ReportParameter("NR", printData.JumlahKirimanLabel ?? ""));


                reportViewer.LocalReport.SetParameters(parameters.ToArray());

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                // Refresh the report
                reportViewer.RefreshReport();

                var directPrint = new DirectPrint();
                if (file)
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 21, 14).ShowPreview();
                }
                else
                {
                    directPrint.Export(reportViewer.LocalReport, "CustomSize", 21, 14).Print();
                }
            }
            catch (Exception ex)
            {
                ex.Dump();
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnCetak_Click(object sender, EventArgs e)
        {
            PrintData(false);
        }

        private void btnCetakTanpaKertas_Click(object sender, EventArgs e)
        {
            PrintData(true);
        }

        private async void btnInfoDetail_Click(object sender, EventArgs e)
        {
            var horder = GetHorderFromDgv2();
            if (horder == null || FetchOrderRun)
            {
                return;
            }

            var dorder = GetDorderFromDgv3(horder?.Dorder?.ToList());
            if (dorder == null || FetchOrderRun)
            {
                return;
            }

            try
            {
                var res = await _Client.Info_OrderanAsync(horder.Kodeh, dorder.Koded);
                MessageBox.Show(res);
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