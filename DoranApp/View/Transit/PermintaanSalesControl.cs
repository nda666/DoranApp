using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;

namespace DoranApp.View.Transit;

public partial class PermintaanSalesControl : UserControl
{
    public bool _AnyDataProcessRun = false;

    public dynamic _DataQuery;
    public long _LastPage = 0;

    public PermintaanSalesData _PermintaanSalesData = new PermintaanSalesData();
    public bool confirmSelectionChange = false;
    public bool dontUpdateChangeDgv = false;
    public bool IsEdit = false;
    public int previousSelectedRow;
    public int previousSelectedRow2;

    public PermintaanSalesControl()
    {
        InitializeComponent();
    }

    public long _Page
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

    private void PermintaanSalesControl_Load(object sender, EventArgs e)
    {
        dataGridView1.DataSource = _PermintaanSalesData.GetDataTable();
        dataGridView1.Columns.Cast<DataGridViewColumn>().ToList()
            .ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
        dataGridView1.EnableDoubleBuffered(true);
        dataGridView2.EnableDoubleBuffered(true);

        dataGridView1.Columns["Tujuan"].Width = 200;
        dataGridView1.Columns["Tanggal"].Width = 80;
        dataGridView1.Columns["Sales"].Width = 150;
        dataGridView1.Columns["Keterangan"].Width = 250;
        dataGridView1.Columns["Lunas"].Width = 40;
        dataGridView1.Columns["Penyiap"].Width = 60;
        dataGridView1.Columns["Oleh"].Width = 60;

        comboGudangAsal.ValueMember = "Key";
        comboGudangAsal.DisplayMember = "Value";
        comboFilterGudang.ValueMember = "Key";
        comboFilterGudang.DisplayMember = "Value";

        Dictionary<int, string> dataGudangAsal = new Dictionary<int, string>();
        dataGudangAsal.Add(1, "SBY");
        comboGudangAsal.DataSource = dataGudangAsal.ToList();
        comboFilterGudang.DataSource = dataGudangAsal.ToList();
    }

    private void buttonFilterNama_Click(object sender, EventArgs e)
    {
        GetPermintaanSales();
    }

    public async Task GetPermintaanSales()
    {
        if (_AnyDataProcessRun)
        {
            return;
        }

        _DataQuery = new
        {
            Page = _Page.ToString(),
            PageSize = comboPageSize.SelectedItem?.ToString() ?? "50",
            NamaGudangTujuan = textBoxFilterNamaGudangTujuan.Text.Trim(),
            StokSales = true,
            Kodegudang = comboFilterGudang.SelectedValue,
            Namagudang = textBoxFilterNamaGudangTujuan.Text,
            KodeBarang = comboFilterBarang.SelectedValue?.ToString()
        };
        this.Text = "Loading";
        ButtonToggleHelper.DisableButtonsByTag(this, "groupbox");
        this.UseWaitCursor = true;
        _AnyDataProcessRun = true;
        toolStripLabel2.Visible = true;
        try
        {
            _PermintaanSalesData.SetQuery(_DataQuery);
            await _PermintaanSalesData.Refresh();
            var paginationData = _PermintaanSalesData.GetPaginationData();
            _Page = paginationData.Page;
            _LastPage = paginationData.TotalPage;
            toolStripLabel1.Text = $"dari {paginationData.TotalPage.ToString()}";
            dataGridView2.Rows.Clear();
        }
        catch (Exception ex)
        {
            ConsoleDump.Extensions.Dump(ex);
            MessageBox.Show(ex.Message);
        }

        miniToolStrip.Enabled = true;
        toolStripLabel2.Visible = false;
        _AnyDataProcessRun = false;
        ButtonToggleHelper.EnableButtonsByTag(this, "groupbox");
        this.UseWaitCursor = false;
    }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.Value != null && e.Value is DateTime)
        {
            e.Value = ((DateTime)e.Value).ToString("d/M/yyyy");
            e.FormattingApplied = true;
        }
    }

    private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
        if (dataGridView1.Rows.Count > 0)
        {
            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
        }

        dataGridView1.ClearSelection();
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
        if (_AnyDataProcessRun)
        {
            return;
        }

        if (dataGridView1.SelectedRows.Count <= 0 && dataGridView1.Rows.Count <= 0)
        {
            buttonTambahData.Enabled = false;
            return;
        }

        if (confirmSelectionChange && previousSelectedRow >= 0 && !dontUpdateChangeDgv)
        {
            DialogResult result =
                MessageBox.Show(
                    "Anda sedang dalam mode \"Tambah/Ubah Detail Transit\" pada transit yang sedang Anda pilih. Apakah ingin keluar dari mode ini?",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    previousSelectedRow2 = dataGridView2.SelectedRows[0].Index;
                }

                confirmSelectionChange = false; // Set this to false to prevent further prompts
                dontUpdateChangeDgv = true;
                dataGridView1.ClearSelection(); // Clear the selection

                try
                {
                    dataGridView1.Rows[previousSelectedRow].Selected = true; // Reselect the previous row
                    dataGridView2.Rows[previousSelectedRow2].Selected = true;
                }
                catch (Exception ex)
                {
                }

                confirmSelectionChange = true; // Set it back to true for future selection changes
                dontUpdateChangeDgv = false;
                return;
            }
            else
            {
                previousSelectedRow = dataGridView1.SelectedRows[0].Index; // Update the previous selected row
                EnableGroupBoxDetail(false);
            }
        }
        else
        {
            if (!dontUpdateChangeDgv)
            {
                previousSelectedRow = dataGridView1.SelectedRows[0].Index;
                confirmSelectionChange = false;
            }
        }

        dataGridView2.Rows.Clear();
        try
        {
            var kodeh = (int)dataGridView1.SelectedRows[0].Cells["Kodeh"].Value;

            var horder = _PermintaanSalesData.GetData()?.Where(x => x.Kodeh == kodeh).FirstOrDefault();
            if (horder == null)
            {
                return;
            }

            var Pcs = horder.Dorder.Sum(e => e.Jumlah);
            var Varian = horder.Dorder.GroupBy(e => e.Kodebarang).Count();
            dataGridView2.Rows.Clear();
            foreach (var d in horder.Dorder)
            {
                var index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells["Pcs"].Value = d.Jumlah;
                dataGridView2.Rows[index].Cells["NamaBarang"].Value = d.Masterbarang?.BrgNama;
                dataGridView2.Rows[index].Cells["Keterangan"].Value = d.Keterangan;
                dataGridView2.Rows[index].Cells["Kodeh"].Value = d.Kodeh;
                dataGridView2.Rows[index].Cells["Koded"].Value = d.Koded;
                dataGridView2.Rows[index].Cells["Rak"].Value = d.Masterbarang?.Mastertipebarang?.Nama;
                dataGridView2.Rows[index].Cells["KodehTrans"].Value = d.KodehTrans;
                dataGridView2.Rows[index].Cells["KodedTrans"].Value = d.KodedTrans;
                dataGridView2.Rows[index].Cells["Keterangan"].Value = d.Keterangan;
                dataGridView2.Rows[index].Cells["KodeBarang"].Value = d.Kodebarang;
            }

            DataGridViewColumn columnKoded = dataGridView2.Columns["Koded"];
            dataGridView2.Sort(columnKoded, ListSortDirection.Ascending);
            dataGridView2.ClearSelection();
            labelVarian.Text = $"Varian : {Varian.ToString()}";
            labelPcs.Text = $"PCS : {Pcs.ToString()}";

            if (!dontUpdateChangeDgv)
            {
                buttonTambahData.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ConsoleDump.Extensions.Dump(ex);
        }
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
        _Page -= 1;
        GetPermintaanSales();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
        _Page = 1;
        GetPermintaanSales();
    }

    private void toolStripButton3_Click(object sender, EventArgs e)
    {
        _Page += 1;
        GetPermintaanSales();
    }

    private void toolStripButton4_Click(object sender, EventArgs e)
    {
        _Page = _LastPage;
        GetPermintaanSales();
    }

    private void buttonSimpan_Click(object sender, EventArgs e)
    {
        SavePermintaanSales();
    }

    private bool ValidateSavePermintaanSales()
    {
        var valid = true;

        errorProvider1.SetError(comboGudangAsal,
            comboGudangAsal.SelectedValue == null ? "Gudang asal harus diisi." : null);
        valid = !valid ? false : comboGudangAsal.SelectedValue != null;

        errorProvider1.SetError(comboGudangTujuan,
            comboGudangTujuan.SelectedValue == null ? "Gudang tujuan harus diisi." : null);
        valid = !valid ? false : comboGudangTujuan.SelectedValue != null;

        errorProvider1.SetError(comboSales, comboSales.SelectedValue == null ? "Sales harus diisi." : null);
        valid = !valid ? false : comboSales.SelectedValue != null;

        errorProvider1.SetError(comboPenyiapOrder,
            comboPenyiapOrder.SelectedValue == null ? "Penyiap harus diisi." : null);
        valid = !valid ? false : comboPenyiapOrder.SelectedValue != null;

        return valid;
    }

    private void SetLoading(bool loading)
    {
        tabControl1.Enabled = loading;
        groupBox2.Enabled = loading;
        groupBox3.Enabled = loading;
        this.Cursor = loading ? Cursors.WaitCursor : Cursors.Default;
    }

    private async Task SavePermintaanSales()
    {
        var valid = ValidateSavePermintaanSales();
        if (!valid)
        {
            return;
        }

        var confirm = MessageBox.Show("Simpan Persiapan Stok Sales ?", "Simpan?", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        if (confirm == DialogResult.No)
        {
            return;
        }

        SetLoading(true);
        try
        {
            var dataPost = new SavePermintaanSalesDto
            {
                Tglorder = datePickerTglTransit.Value,
                Keterangan = textBoxKeterangan.Text,
                Kodegudang = Convert.ToInt32(comboGudangAsal.SelectedValue),
                Kodepelanggan = Convert.ToInt32(comboGudangTujuan.SelectedValue),
                Kodesales = Convert.ToInt32(comboSales.SelectedValue),
            };
            await _PermintaanSalesData.CreatePermintaanSales(dataPost);
        }
        catch (ApiException ex)
        {
            Helper.ShowErrorMessageFromResponse(ex);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        SetLoading(false);
    }

    private void buttonTambahData_Click(object sender, EventArgs e)
    {
        EnableGroupBoxDetailTambah(true);
    }

    private void buttonUbah_Click(object sender, EventArgs e)
    {
        IsEdit = true;
        EnableGroupBoxDetail(true);
        DatagridDetailOnUpdateChangeSelection();
    }


    private void EnableGroupBoxDetail(bool enable)
    {
        button7.Text = "UPDATE";
        confirmSelectionChange = enable;
        groupBoxTambahDetail.Enabled = enable;
        button1.Enabled = enable;
        buttonTambahData.Enabled = !enable;
        buttonHapus.Enabled = !enable;
        groupBox6.Enabled = !enable;
        groupBox1.Enabled = !enable;
        if (enable)
        {
            comboBoxTambahMasterbarang.Select();
            button1.Text = "Selesai Ubah";
            IsEdit = true;
            ButtonToggleHelper.DisableButtonsByTag(this, "FilterAction1");
        }
        else
        {
            button1.Text = "Selesai Tambah";
            IsEdit = false;
            ButtonToggleHelper.EnableButtonsByTag(this, "FilterAction1");
            ButtonToggleHelper.EnableButtonsByTag(this, "detailButton");
        }
    }

    private void EnableGroupBoxDetailTambah(bool enable)
    {
        IsEdit = false;
        confirmSelectionChange = enable;
        groupBoxTambahDetail.Enabled = enable;
        groupBox6.Enabled = !enable;
        groupBox1.Enabled = !enable;
        button1.Enabled = enable;
        if (enable)
        {
            comboBoxTambahMasterbarang.Select();
            button1.Text = "Selesai Tambah";
            button7.Text = "TAMBAH";
            ButtonToggleHelper.DisableButtonsByTag(this, "FilterAction1");
            ButtonToggleHelper.DisableButtonsByTag(this, "detailButton");
        }
        else
        {
            ButtonToggleHelper.EnableButtonsByTag(this, "FilterAction1");
            ButtonToggleHelper.EnableButtonsByTag(this, "detailButton");
        }
    }

    private void DatagridDetailOnUpdateChangeSelection()
    {
        if (IsEdit == true && dataGridView2.SelectedRows.Count > 0)
        {
            try
            {
                var jumlah = dataGridView2.SelectedRows[0].Cells["Pcs"].Value?.ToString() ?? "";
                var kodeBarang = (int)dataGridView2.SelectedRows[0].Cells["KodeBarang"].Value;
                var keterangan = (string)dataGridView2.SelectedRows[0].Cells["Keterangan"].Value;
                comboBoxTambahMasterbarang.SelectedValue = kodeBarang;
                textBoxDetailKeterangan.Text = keterangan;
                textBoxJumlah.Text = jumlah;
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
            }
        }
    }

    private async void buttonHapus_Click(object sender, EventArgs e)
    {
        if (dataGridView2.Rows.Count <= 0 && dataGridView2.SelectedRows.Count <= 0)
        {
            return;
        }

        var confirm = MessageBox.Show(
            $"Apakah Anda yakin ingin menghapus \"{dataGridView2.SelectedRows.Count} DETAIL TRANSIT\" yang Anda pilih saat ini?",
            "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (confirm == DialogResult.No)
        {
            return;
        }

        dontUpdateChangeDgv = true;
        var selectedIndex = dataGridView1.SelectedRows[0].Index;
        var Kodeh = (int)dataGridView1.SelectedRows[0].Cells["Kodeh"].Value;
        var htransit = _PermintaanSalesData.GetData().Where(x => x.Kodeh == Kodeh).FirstOrDefault();
        if (htransit == null)
        {
            MessageBox.Show("Pilih detail transit yang ingin Anda hapus terlebih dahulu");
            return;
        }

        var koded = new List<int>();
        for (var i = 0; i < dataGridView2.SelectedRows.Count; i++)
        {
            var kodedFound = dataGridView2.SelectedRows[i].Cells["Koded"].Value;
            if (kodedFound != null)
            {
                koded.Add(Convert.ToInt32(kodedFound));
            }
        }

        _AnyDataProcessRun = true;
        buttonHapus.Enabled = false;
        try
        {
            var result = await _PermintaanSalesData.DeleteDetail(Kodeh, koded.ToArray());
            var indexData = _PermintaanSalesData.GetData().FindIndex(x => x.Kodeh == result.Kodeh);
            _PermintaanSalesData.UpdateDatatableAndData(x => (int)x["Kodeh"] == result.Kodeh, indexData, result);
            dataGridView1.ClearSelection();
            dataGridView1.Rows[selectedIndex].Selected = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        _AnyDataProcessRun = false;
        buttonHapus.Enabled = true;
        dontUpdateChangeDgv = false;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        EnableGroupBoxDetailTambah(false);
    }

    private async Task InsertDetail()
    {
        if (comboBoxTambahMasterbarang.SelectedValue == null || String.IsNullOrWhiteSpace(textBoxJumlah.Text))
        {
            return;
        }

        var Kodeh = (int)dataGridView1.SelectedRows[0].Cells["Kodeh"].Value;
        try
        {
            var selectedIndex = dataGridView1.SelectedRows[0].Index;
            var result = await _PermintaanSalesData.InsertDetail(Kodeh, new InsertDetailPermintaanSalesDto
            {
                Jumlah = Convert.ToInt32(textBoxJumlah.Text),
                Kodebarang = Convert.ToInt32(comboBoxTambahMasterbarang.SelectedValue),
                Keterangan = textBoxDetailKeterangan.Text.Trim()
            });

            var indexData = _PermintaanSalesData.GetData().FindIndex(x => x.Kodeh == result.Kodeh);
            _PermintaanSalesData.UpdateDatatableAndData(x => (int)x["Kodeh"] == result.Kodeh, indexData, result);
            dataGridView1.ClearSelection();
            dataGridView1.Rows[selectedIndex].Selected = true;
        }
        catch (ApiException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private async Task UpdateDetail()
    {
        if (comboBoxTambahMasterbarang.SelectedValue == null || String.IsNullOrWhiteSpace(textBoxJumlah.Text))
        {
            return;
        }

        var kodeh = (int)dataGridView1.SelectedRows[0].Cells["Kodeh"].Value;
        var koded = (int)dataGridView2.SelectedRows[0].Cells["Koded"].Value;
        try
        {
            var selectedIndex = dataGridView1.SelectedRows[0].Index;
            var result = await _PermintaanSalesData.UpdateDetail(kodeh, koded, new InsertDetailPermintaanSalesDto()
            {
                Jumlah = Convert.ToInt32(textBoxJumlah.Text),
                Kodebarang = Convert.ToInt32(comboBoxTambahMasterbarang.SelectedValue),
                Keterangan = textBoxDetailKeterangan.Text.Trim()
            });
            var indexData = _PermintaanSalesData.GetData().FindIndex(x => x.Kodeh == result.Kodeh);
            _PermintaanSalesData.UpdateDatatableAndData(x => (int)x["Kodeh"] == result.Kodeh, indexData, result);
            // dataGridView1.ClearSelection();
            dataGridView1.Rows[selectedIndex].Selected = true;
        }
        catch (ApiException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    private async void button7_Click(object sender, EventArgs e)
    {
        dontUpdateChangeDgv = true;
        this.Enabled = false;
        if (button7.Text == "UPDATE")
        {
            await UpdateDetail();
        }
        else
        {
            await InsertDetail();
        }

        this.Enabled = true;
        dontUpdateChangeDgv = false;
    }

    private void dataGridView2_SelectionChanged(object sender, EventArgs e)
    {
        if (dataGridView2.Rows.Count <= 0 && dataGridView2.SelectedRows.Count <= 0)
        {
            return;
        }

        if (!IsEdit)
        {
            buttonHapus.Enabled = dataGridView2.SelectedRows.Count >= 1;
            buttonUbah.Enabled = dataGridView2.SelectedRows.Count == 1;
        }

        DatagridDetailOnUpdateChangeSelection();
    }
}