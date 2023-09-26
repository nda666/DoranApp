﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;
using Microsoft.Reporting.WinForms;

namespace DoranApp.View
{
    public partial class TransaksiForm : Form
    {
        public long _laporanTransaksiLastPage = 0;
        private MasterbarangData _masterbarang = new MasterbarangData();
        private List<MasterbarangOptionDto> _masterbarangOptions = new List<MasterbarangOptionDto>();
        private MastergudangData _mastergudangData = new MastergudangData();
        private List<Mastergudang> _mastergudangOptions = new List<Mastergudang>();
        private MasterpelangganData _masterpelangganData = new MasterpelangganData();
        private List<CommonResultDto> _masterpelangganOptions = new List<CommonResultDto>();
        private SalesData _salesData = new SalesData();
        private List<CommonResultDto> _salesOptions = new List<CommonResultDto>();
        private SetlevelhargaData _setlevelhargaData = new SetlevelhargaData();
        private List<CommonResultDto> _setlevelhargaOptions = new List<CommonResultDto>();

        private decimal _subtotal = 0;

        private TransaksiData _transaksiData = new TransaksiData();
        private BindingList<dynamic> cart;
        private int? KodeEdit = null;

        public TransaksiForm()
        {
            InitializeComponent();
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
            _masterbarangOptions = (List<MasterbarangOptionDto>)data.Response;
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

        public async void CreateDatagridview()
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
            dataGridView2.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private async void TransaksiForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);
            dataGridView3.DoubleBuffered(true);
            comboTempo.SelectedIndex = 1;
            FetchSales();
            FetchPelanggan();
            FetchGudang();
            FetchLevelHarga();
            CreateDatagridview();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await FetchPelanggan();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await FetchMasterbarang();
        }

        private async void button9_Click(object sender, EventArgs e)
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
                            jumlah = row.Cells[0]?.Value,
                            harga = row.Cells[2]?.Value,
                            nmrsn = row.Cells[6]?.Value ?? "",
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
                var res = await _transaksiData.CreateOrUpdate(KodeEdit == null ? null : KodeEdit.ToString(), new
                {
                    KodeSales = comboSales.SelectedValue,
                    KodeGudang = comboGudang.SelectedValue,
                    TglTrans = datePickerTransaksi.Value,
                    KodePelanggan = comboPelanggan.SelectedValue,
                    Keterangan = textBoxKeterangan.Text,
                    Infopenting = textBoxInfoPenting.Text,
                    Tipetempo = getTipeTempo(),
                    Tgltempo = comboTempo.SelectedIndex == 0 ? "" : datePickerJatuhTempo.Value.ToString(),
                    Retur = 0,
                    Jumlahbarangbiaya = textBoxBiaya.UnformattedValue,
                    TambahanLainnya = textBoxOngkir.UnformattedValue,
                    Diskon = textBoxDiskon.UnformattedValue,
                    Ppn = textBoxPPN.UnformattedValue,
                    Dpp = textBoxDpp.UnformattedValue,
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CalculateSubtotal();
            CalculateTotal();
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
                    int pcs = Convert.ToInt32(row.Cells["pcs"].Value);
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

            _subtotal = subtotal;
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

        private void checkBoxPPN_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPPN.Enabled = checkBoxPPN.Checked;
            decimal ppn = 0;
            if (checkBoxPPN.Checked)
            {
                ppn = _subtotal - (_subtotal / (decimal)1.11);
            }

            textBoxPPN.Text = ((int)ppn).ToString();
            CalculateTotal();
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
            toolStripLabel2.Visible = true;
            dataGridView3.Rows.Clear();
            toolStrip1.Enabled = false;
            button10.Enabled = false;
            try
            {
                _transaksiData.SetQuery(new
                {
                    page = _laporanTransaksiPage <= 0 ? 1 : _laporanTransaksiPage,
                    pageSize = comboPageSize.SelectedItem?.ToString() ?? "50",
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

            var paginationData = _transaksiData.GetPaginationData();
            _laporanTransaksiPage = paginationData.Page;
            _laporanTransaksiLastPage = paginationData.TotalPage;
            toolStripLabel1.Text = $"dari {paginationData.TotalPage.ToString()}";
            toolStrip1.Enabled = true;
            button10.Enabled = true;
            toolStripLabel2.Visible = false;
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            fetchLaporanTransaksi();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0 && dataGridView2.Rows.Count <= 0)
            {
                return;
            }

            dataGridView3.Rows.Clear();
            var kodeh = (long)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
            var htrans = _transaksiData.GetData().Where(x => x.kodeH == kodeh).FirstOrDefault();
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
                dataGridView3.Rows[index].Cells["SN"].Value = d.SN;
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
        }

        private void button12_Click(object sender, EventArgs e)
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

        private void buttonBatalUbah_Click(object sender, EventArgs e)
        {
            KodeEdit = null;
            buttonBatalUbah.Visible = false;
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
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0 && dataGridView2.Rows.Count <= 0)
            {
                return;
            }

            var kodeh = (long)dataGridView2.SelectedRows[0].Cells["Kodeh"].Value;
            var htrans = _transaksiData.GetData().Where(x => x.kodeH == kodeh).FirstOrDefault();
            ConsoleDump.Extensions.Dump(htrans);
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
            foreach (var dtrans in htrans.dtrans)
            {
                DataRow row = dt.NewRow();
                row["no"] = no;
                row["jumlah"] = dtrans.jumlah;
                row["brgNama"] = dtrans.masterbarang?.brgNama;
                row["harga"] = dtrans.harga;
                row["subTotal"] = dtrans.harga * dtrans.jumlah;
                dt.Rows.Add(row);
                no++;
            }

            // Set parameters (if any)
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("total", string.Format("{0:N0}", htrans.jumlah)));
            parameters.Add(new ReportParameter("lainnya", string.Format("{0:N0}", htrans.tambahanLainnya)));
            parameters.Add(new ReportParameter("ppn", string.Format("{0:N0}", htrans.ppn)));
            parameters.Add(new ReportParameter("tglTrans", htrans.tglTrans?.ToString("dd/MM/yyyy")));
            parameters.Add(new ReportParameter("kodenota", htrans.kodenota));
            parameters.Add(new ReportParameter("namaPelanggan", htrans.masterpelanggan?.nama));
            parameters.Add(new ReportParameter("namaSales", htrans.sales?.nama));
            parameters.Add(new ReportParameter("tipeTempo", Helper.TipeTempoToString((sbyte)htrans.tipetempo)));
            parameters.Add(new ReportParameter("tglTempo", htrans.tgltempo?.ToString("dd/MM/yyyy")));
            parameters.Add(new ReportParameter("R", "0"));
            parameters.Add(new ReportParameter("N", "0"));

            if (!String.IsNullOrWhiteSpace(htrans.kodenota))
            {
                var barcode64String =
                    BarcodeGenerator.GenerateBase64Barcode(htrans.kodenota, ZXing.BarcodeFormat.CODE_128, 400, 40);

                parameters.Add(new ReportParameter("barcodeImage", barcode64String));
            }

            reportViewer.LocalReport.SetParameters(parameters.ToArray());

            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DetailTransaksi", dt));
            // Refresh the report
            reportViewer.RefreshReport();

            var directPrint = new DirectPrint();
            float widthInInches = 24 / 2.54f; // Convert 24 cm to inches
            float heightInInches = 14 / 2.54f; // Convert 14 cm to inches
            directPrint.Export(reportViewer.LocalReport).Print("CustomSize", 970, 551);
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }

    internal class HargaByLevelResult
    {
        public int Harga { get; set; }
        public int Kodebarang { get; set; }
        public string? Namabarang { get; set; }
    }
}