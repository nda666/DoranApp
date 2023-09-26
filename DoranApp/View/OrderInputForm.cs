using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Properties;
using DoranApp.Utils;

namespace DoranApp.View
{
    public partial class OrderInputForm : Form
    {
        private List<Masterpengeluaran> _ekspedisiOption = new List<Masterpengeluaran>();
        private MasterbarangData _masterbarang = new MasterbarangData();
        private List<MasterbarangOptionDto> _masterbarangOptions = new List<MasterbarangOptionDto>();
        private List<Mastergudang> _mastergudangOptions = new List<Mastergudang>();
        private MasterpelangganData _masterpelangganData = new MasterpelangganData();
        private List<CommonResultDto> _masterpelangganOptions = new List<CommonResultDto>();
        private MasterpengeluaranData _masterpengeluaranData = new MasterpengeluaranData();

        private OrderData _OrderData = new OrderData();
        private PenyiaporderData _PenyiapOrderData = new PenyiaporderData();

        private List<Penyiaporder> _PenyiaporderOptions = new List<Penyiaporder>();
        private SalesData _salesData = new SalesData();
        private List<CommonResultDto> _salesOptions = new List<CommonResultDto>();
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
            var clientManager = new WebSocketClientManager();
            clientManager.Subscribe("order", eventData =>
            {
                if (eventData != null)
                {
                    MessageBox.Show("123");
                }
            });

            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);
            dataGridView3.DoubleBuffered(true);
            comboPageSize.SelectedIndex = 0;
            comboJenisEkspedisi.SelectedIndex = 0;
            comboTempo.SelectedIndex = 0;
            FetchSales();
            FetchPelanggan();
            FetchLevelHarga();
            FetchEkspedisi();
            FetchPenyiapOrder();
            CreateDatagridview();
            dataGridView2.DataSource = _OrderData.GetDataTable();
            dataGridView2.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
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

        public async Task FetchSales()
        {
            try
            {
                var data = await _salesData.GetNameAndKodeOnly();
                _salesOptions = (List<CommonResultDto>)data.Response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            comboSales.ValueMember = "Kode";
            comboSales.DisplayMember = "Nama";
            comboSales.DataSource = _salesOptions;
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
            toolStrip1.Enabled = !FetchOrderRun;
            toolStripLabel2.Visible = FetchOrderRun;
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
                    NamaPelanggan = textBoxFilterNama.Text.Trim(),
                    Kodesales = comboFilterSales.SelectedValue?.ToString(),
                    Dicetak = Helper.GetSelectedRadioButtonTag(groupBoxDicetak),
                    LevelOrder = Helper.GetSelectedRadioButtonTag(groupFilterLevel),
                    SalesOl = Helper.GetSelectedRadioButtonTag(groupBoxFilterJenisTrans),
                    NamaCust = textBoxFilterNamaCust.Text.Trim(),
                    PageSize = comboPageSize.SelectedItem?.ToString() ?? "50",
                    Page = _laporanOrderPage <= 0 ? 1 : _laporanOrderPage,
                });
                await _OrderData.Refresh();
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
                var kodeh = (long)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;

                var horder = _OrderData.GetData().Where(x => x.kodeh == kodeh).FirstOrDefault();
                if (horder == null)
                {
                    return;
                }

                var dorder = new List<dynamic>();
                foreach (var d in horder.dorder)
                {
                    var index = dataGridView3.Rows.Add();
                    dataGridView3.Rows[index].Cells["Pcs"].Value = d.jumlah;
                    dataGridView3.Rows[index].Cells["NamaBarang"].Value = d.masterbarang?.brgNama;
                    dataGridView3.Rows[index].Cells["Harga"].Value = d.harga;
                    dataGridView3.Rows[index].Cells["Jumlah"].Value = d.jumlah;
                    dataGridView3.Rows[index].Cells["Keterangan"].Value = d.keterangan;
                }

                ConsoleDump.Extensions.Dump(comboSetPenyiap.SelectedValue, horder.kodepenyiap?.ToString());
                comboSetPenyiap.SelectedValue = (sbyte)horder.kodepenyiap;
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

        private async void buttonSetPenyiap_Click(object sender, EventArgs e)
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

            SaveRun = true;
            var confirm = MessageBox.Show(Resources.AskSetPenyiapOrder, "Konfirmasi", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (confirm == DialogResult.No)
            {
                SaveRun = false;
                return;
            }

            try
            {
                ConsoleDump.Extensions.Dump(comboSetPenyiap.SelectedValue?.ToString() ?? "NOPEE");
                var kodeh = (long)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
                var data = await _OrderData.SetPenyiapOrder(kodeh,
                    Convert.ToInt32(comboSetPenyiap.SelectedValue?.ToString() ?? "0"));
                await FetchOrder();
            }
            catch (Exception ex)
            {
                // ConsoleDump.Extensions.Dump(ex);
                MessageBox.Show(ex.Message, "Error");
            }

            SaveRun = false;
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
                var horder = _OrderData.GetData().Where(x => x.kodeh == kodeh).FirstOrDefault();
                if (horder == null)
                {
                    return;
                }

                KodeEdit = (int)horder.kodeh;
                EditHeaderOnly = !withDetail;
                comboPelanggan.SelectedValue = (int)horder.kodepelanggan;
                comboSales.SelectedValue = (int)horder.kodesales;
                datePickerOrder.Value = horder.tglorder;
                textboxNamaCust.Text = horder.namaCust;
                textboxNmrHp.Text = horder.nmrHp;
                textBoxBarcodeonline.Text = horder.barcodeonline;
                textBoxNoSeriOnline.Text = horder.noSeriOnline;
                textBoxKodeOrderApps.Text = horder.kodeorderapps?.ToString();
                textBoxInfoPenting.Text = horder.infopenting;
                comboTempo.SelectedValue = horder.tipetempo;
                comboEkspedisi.SelectedValue = horder.kodeexp;
                comboJenisEkspedisi.SelectedValue = horder.kirimmelalui;
                dataGridView1.Rows.Clear();

                checkBoxPPN.Checked = horder.ppn > 0;
                textBoxTotal.Text = Convert.ToString(horder.jumlah);
                if (withDetail)
                {
                    foreach (var d in horder.dorder)
                    {
                        DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                        row.Cells[0].Value = (decimal)d.jumlah;
                        row.Cells[1].Value = (int)d.kodebarang;
                        row.Cells[2].Value = (decimal)d.harga;
                        row.Cells[3].Value = d.harga * d.jumlah;
                        row.Cells[4].Value = d.keterangan;
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

        private void btnCetakTanpaKertas_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}