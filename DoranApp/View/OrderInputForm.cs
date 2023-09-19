using DoranApp.Data;
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
    public partial class OrderInputForm : Form
    {
        private MasterbarangData _masterbarang = new MasterbarangData();
        private SalesData _salesData = new SalesData();
        private MasterpelangganData _masterpelangganData = new MasterpelangganData();
        private MasterpengeluaranData _masterpengeluaranData = new MasterpengeluaranData();
        private MastergudangData _mastergudangData = new MastergudangData();
        private SetlevelhargaData _setlevelhargaData = new SetlevelhargaData();

     
        private List<MasterbarangOption> _masterbarangOptions = new List<MasterbarangOption>();
        private List<Masterpengeluaran> _ekspedisiOption = new List<Masterpengeluaran>();
        private List<SalesOption> _salesOptions = new List<SalesOption>();
        private List<MasterpelangganOption> _masterpelangganOptions = new List<MasterpelangganOption>();
        private List<Mastergudang> _mastergudangOptions = new List<Mastergudang>();
        private List<Setlevelharga> _setlevelhargaOptions = new List<Setlevelharga>();

        private OrderData _OrderData = new OrderData();
        
        private int? KodeEdit = null;
        
        private decimal SubTotal = 0;
        private decimal GrandTotal = 0;
        
        public bool FetchOrderRun = false;
        public long _laporanOrderLastPage = 0;
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
            set
            {
                toolStripTextBox1.Text = value > 0 ? value.ToString() : "1";
            }
        }

        public OrderInputForm()
        {
            InitializeComponent();
        }

        private void OrderInputForm_Load(object sender, EventArgs e)
        {
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
            dc.DisplayMember = "BrgNama";     // The DisplayMember
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
            _masterbarangOptions = (List<MasterbarangOption>)data.Response;
        }

        public async Task FetchSales()
        {
            try
            {
                var data = await _salesData.GetNameAndKodeOnly();
                _salesOptions = (List<SalesOption>)data.Response;
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
                _masterpelangganOptions = (List<MasterpelangganOption>)data.Response;
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
                } else
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
            try { 
                _OrderData.SetQuery(new {
                    Nama = textBoxFilterNama.Text.Trim(),
                    Sales = comboFilterSales.SelectedValue?.ToString(),
                    Dicetak = Helper.GetSelectedRadioButtonTag(groupBoxDicetak),
                    Level = Helper.GetSelectedRadioButtonTag(groupFilterLevel),
                    Online = Helper.GetSelectedRadioButtonTag(groupBoxFilterJenisTrans),
                    Status = Helper.GetSelectedRadioButtonTag(groupBoxFilterJenisTrans),
                    NamaCustomer = textBoxFilterNamaCust.Text.Trim(),
                    PageSize = comboPageSize.SelectedItem?.ToString() ?? "50",
                    Page = _laporanOrderPage <= 0 ? 1 : _laporanOrderPage,
                });
            
                await _OrderData.Refresh();
            } catch (Exception ex) { MessageBox.Show(ex.Message); }
            var paginationData = _OrderData.GetPaginationData();
            _laporanOrderPage = paginationData.Page;
            _laporanOrderLastPage = paginationData.TotalPage;
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
            if (dataGridView2.SelectedRows.Count <= 0 && dataGridView2.Rows.Count <= 0)
            {
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
                var dtrans = new List<dynamic>();
                foreach (var d in horder.dorder)
                {
                    
                    var index = dataGridView3.Rows.Add();
                    dataGridView3.Rows[index].Cells["Pcs"].Value = d.jumlah;
                    dataGridView3.Rows[index].Cells["NamaBarang"].Value = d.masterbarang?.brgNama;
                    dataGridView3.Rows[index].Cells["Harga"].Value = d.harga;
                    dataGridView3.Rows[index].Cells["Jumlah"].Value = d.jumlah;
                    dataGridView3.Rows[index].Cells["Keterangan"].Value = d.keterangan;

                }
            } catch (Exception ex)
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
            _laporanOrderPage = _laporanOrderLastPage;
            FetchOrder();
        }

        private void OrderInputForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            var keyPress = e.KeyChar.ToString();
            
            if ( dataGridView2.Focused)
            {
                ConsoleDump.Extensions.Dump(e.KeyChar);
                if (keyPress == "q" )
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
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculateSubtotal();
            CalculateTotal();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 2 || e.ColumnIndex == 3) // Replace with the actual column index
            {
               
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null && cell.Value.ToString().Length > 15)
                { ConsoleDump.Extensions.Dump(123);
                    cell.Value = Decimal.Parse(cell.Value.ToString().Substring(0, 15));
                }
            }
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
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBoxNumberOnly_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string text = textBox.Text;

            // Remove non-numeric characters
            textBox.Text = new string(text.Where(c => char.IsDigit(c) || c == '.').ToArray());
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // var isSelected = dataGridView1.SelectedRows.Count > 0 && dataGridView1.Rows.Count > 0;
            // buttonDeleteCart.Enabled = isSelected;
        }

        private void buttonDeleteCart_Click(object sender, EventArgs e)
        {
            ConsoleDump.Extensions.Dump(dataGridView1.CurrentCell);
            if (dataGridView1.CurrentCell != null  && !dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].IsNewRow)
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

        private async Task SaveOrder()
        {
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
                            jumlah = (int) Math.Floor((decimal)row.Cells[0]?.Value),
                            harga = (int) Math.Floor((decimal)row.Cells[2]?.Value),
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
                    ppn = checkBoxPPN.Checked ? (int) Math.Floor( textBoxPPN.UnformattedValue ): 0,
                    tipetempo = getTipeTempo(),
                    Tgltempo = datePickerJatuhTempo.Value.ToString("yyyy-MM-dd"),
                    Infopenting = textBoxInfoPenting.Text,
                    noSeriOnline = textBoxNoSeriOnline.Text.Trim(),
                    barcodeonline=textBoxBarcodeonline.Text.Trim(),
                    namaCust = textboxNamaCust.Text.Trim(),
                    nmrHp = textboxNmrHp.Text.Trim(),
                    Details = Details
                });
              
                MessageBox.Show("Transaksi berhasil disimpan");
                dataGridView1.Rows.Clear();
                buttonBatalUbah.Visible = false;
            }
            catch (Exception ex)
            {
                button9.Enabled = true;
                MessageBox.Show(ex.Message);
            }
            button9.Enabled = true;
            buttonBatalUbah.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SaveOrder();
        }

        private void comboTempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTempo();
        }
    }
}