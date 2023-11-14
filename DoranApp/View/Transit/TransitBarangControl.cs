using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Exceptions;
using DoranApp.Utils;

namespace DoranApp.View;

public partial class TransitBarangControl : UserControl
{
    public TransitData _TransitData = new TransitData();

    public long _TransitLastPage = 0;


    public bool AnyDataProcessRun = false;
    public Client client = new Client();
    public bool confirmSelectionChange = false;
    public bool dontUpdateChangeDgv = false;

    public bool IsEdit = false;
    public int? KodeEdit = null;

    public int? KodeTEdit = null;
    public int previousSelectedRow;
    public int previousSelectedRow2;
    public dynamic TransitDataQuery = new { };

    public TransitBarangControl()
    {
        InitializeComponent();
    }

    public long _TransitPage
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

    private async Task GetTransitRunner()
    {
        AnyDataProcessRun = true;
        ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
        toolStripLabel2.Visible = true;
        try
        {
            _TransitData.SetQuery(TransitDataQuery);
            await _TransitData.Refresh();
            var paginationData = _TransitData.GetPaginationData();
            _TransitPage = paginationData.Page;
            _TransitLastPage = paginationData.TotalPage;
            toolStripLabel1.Text = $"dari {paginationData.TotalPage.ToString()}";
        }
        catch (Exception ex)
        {
            ConsoleDump.Extensions.Dump(ex);
            MessageBox.Show(ex.Message);
        }

        toolStrip1.Enabled = true;
        toolStripLabel2.Visible = false;

        ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        AnyDataProcessRun = false;
    }

    public async Task GetTransitByNamaGudangTujuan()
    {
        if (AnyDataProcessRun)
        {
            return;
        }

        TransitDataQuery = new
        {
            Page = _TransitPage.ToString(),
            PageSize = comboPageSize.SelectedItem?.ToString() ?? "50",
            NamaGudangTujuan = textBoxFilterNamaGudangTujuan.Text.Trim()
        };
        await GetTransitRunner();
    }

    public async Task GetTransit()
    {
        if (AnyDataProcessRun)
        {
            return;
        }

        TransitDataQuery = new
        {
            Page = _TransitPage,
            PageSize = comboPageSize.SelectedItem?.ToString() ?? "50",
            MinDate = dateTimePickerFilterMin.Value.ToString("MM-dd-yyyy"),
            MaxDate = dateTimePickerFilterMax.Value.ToString("MM-dd-yyyy"),
            Kodegudang = comboBoxFilterGudang.SelectedValue?.ToString(),
            KodeGudangTujuan = comboBoxFilterGudangTujuan.SelectedValue?.ToString(),
            Kodepenyiap = comboBoxFilterPenyiap.SelectedValue?.ToString()
        };
        await GetTransitRunner();
    }

    private void TransitBarangControl_Load(object sender, EventArgs e)
    {
        dataGridView1.EnableDoubleBuffered(true);
        dataGridView2.EnableDoubleBuffered(true);

        comboPageSize.SelectedIndex = 0;
        dataGridView1.DataSource = _TransitData.GetDataTable();

        dataGridView1.Columns[0].Width = 70;
        dataGridView1.Columns[2].Width = 150;
        dataGridView1.Columns[4].Width = 70;
        dataGridView1.Columns[3].Width = 70;
        dataGridView1.Columns[6].Width = 200;
        dataGridView1.Columns[7].Width = 60;
        dataGridView1.Columns[7].Width = 60;
        dataGridView1.Columns[8].Width = 60;
        dataGridView1.Columns[9].Width = 50;
        dataGridView1.Columns[10].Width = 120;
        dataGridView1.Columns[11].Width = 120;
    }

    private void button4_Click(object sender, EventArgs e)
    {
        GetTransit();
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
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

        try
        {
            var kodeh = (int)dataGridView1.SelectedRows[0].Cells["KodeT"].Value;
            var htransit = _TransitData.GetData().Where(x => x.KodeT == kodeh).FirstOrDefault();

            if (htransit == null)
            {
                return;
            }

            var Pcs = 0;
            var Varian = 0;

            dataGridView2.Rows.Clear();
            foreach (var d in htransit.Dtransit)
            {
                var index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells["Pcs"].Value = d.Jumlah;
                dataGridView2.Rows[index].Cells["NamaBarang"].Value = d.Masterbarang?.BrgNama;
                dataGridView2.Rows[index].Cells["Rak"].Value = d.Masterbarang?.Mastertipebarang?.Nama;
                dataGridView2.Rows[index].Cells["Penerima"].Value = d.Namapenerima;
                dataGridView2.Rows[index].Cells["SN"].Value = d.NmrSn;
                dataGridView2.Rows[index].Cells["SudahDicek"].Value = d.Sudahdicek;
                dataGridView2.Rows[index].Cells["Koded"].Value = d.Koded;
                dataGridView2.Rows[index].Cells["Kodet"].Value = d.Kodet;
                dataGridView2.Rows[index].Cells["KodeBarang"].Value = d.Kodebarang;
                Pcs += d.Jumlah;
                Varian++;
            }

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

    private void SetLoading(bool loading)
    {
        tabControl1.Enabled = !loading;
        groupBox2.Enabled = !loading;
        groupBox3.Enabled = !loading;
        this.Cursor = loading ? Cursors.WaitCursor : Cursors.Default;
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        if (AnyDataProcessRun)
        {
            return;
        }

        AnyDataProcessRun = true;
        SetLoading(true);
        try
        {
            var res = await _TransitData.CreateOrUpdate(null, new
            {
                TglTrans = datePickerTglTransit.Value.ToString("MM/dd/yyyy"),
                Kodegudang = comboBoxMastergudang.SelectedValue?.ToString(),
                KodeGudangTujuan = comboBoxMastergudangTujuan.SelectedValue?.ToString(),
                Kodepenyiap = comboBoxPenyiapOrder.SelectedValue?.ToString(),
                Keterangan = textBoxKeterangan.Text.Trim()
            });

            await GetTransit();
            MessageBox.Show("\"Header Transit\" berhasil disimpan", "Sukses", MessageBoxButtons.OK);
        }
        catch (RestException ex)
        {
            Helper.ShowErrorMessageFromResponse(ex);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        AnyDataProcessRun = false;
        SetLoading(false);
    }

    private void button6_Click(object sender, EventArgs e)
    {
        GetTransitByNamaGudangTujuan();
    }

    private void ButtonNavigationAction()
    {
        if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
        {
            GetTransitByNamaGudangTujuan();
        }
        else
        {
            GetTransit();
        }
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
        _TransitPage--;
        ButtonNavigationAction();
    }

    private void toolStripButton3_Click(object sender, EventArgs e)
    {
        _TransitPage++;
        ButtonNavigationAction();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
        _TransitPage = 1;
        ButtonNavigationAction();
    }

    private void toolStripButton4_Click(object sender, EventArgs e)
    {
        _TransitPage = _TransitLastPage;
        ButtonNavigationAction();
    }

    private async void buttonHapus_Click(object sender, EventArgs e)
    {
        if (dataGridView2.Rows.Count <= 0 && dataGridView2.SelectedRows.Count <= 0)
        {
            return;
        }

        var confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus detail transit yang Anda pilih saat ini?",
            "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (confirm == DialogResult.No)
        {
            return;
        }

        dontUpdateChangeDgv = true;
        var selectedIndex = dataGridView1.SelectedRows[0].Index;
        var Kodet = (int)dataGridView1.SelectedRows[0].Cells["Kodet"].Value;
        var htransit = _TransitData.GetData().Where(x => x.KodeT == Kodet).FirstOrDefault();
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

        AnyDataProcessRun = true;
        buttonHapus.Enabled = false;
        try
        {
            var result = await _TransitData.DeleteDetail(Kodet, koded.ToArray());
            var indexData = _TransitData.GetData().FindIndex(x => x.KodeT == result.KodeT);
            _TransitData.UpdateDatatableAndData(x => (int)x["KodeT"] == result.KodeT, indexData, result);
            dataGridView1.ClearSelection();
            dataGridView1.Rows[selectedIndex].Selected = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        AnyDataProcessRun = false;
        buttonHapus.Enabled = true;
        dontUpdateChangeDgv = false;
    }

    private void DatagridDetailOnUpdateChangeSelection()
    {
        if (IsEdit == true && dataGridView2.SelectedRows.Count > 0)
        {
            try
            {
                var jumlah = dataGridView2.SelectedRows[0].Cells["Pcs"].Value?.ToString() ?? "";
                var kodeBarang = (int)dataGridView2.SelectedRows[0].Cells["KodeBarang"].Value;
                var SN = (string)dataGridView2.SelectedRows[0].Cells["SN"].Value;
                comboBoxTambahMasterbarang.SelectedValue = kodeBarang;
                textBoxSn.Text = SN;
                textBoxJumlah.Text = jumlah;
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(ex);
            }
        }
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

    private void EnableGroupBoxDetailTambah(bool enable)
    {
        IsEdit = false;
        confirmSelectionChange = enable;
        groupBoxTambahDetail.Enabled = enable;
        button1.Enabled = enable;
        groupBox6.Enabled = !enable;
        groupBox1.Enabled = !enable;
        if (enable)
        {
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

    private void EnableGroupBoxDetail(bool enable)
    {
        button7.Text = "UPDATE";
        confirmSelectionChange = enable;
        groupBoxTambahDetail.Enabled = enable;
        button1.Enabled = enable;
        groupBox6.Enabled = !enable;
        groupBox1.Enabled = !enable;
        if (enable)
        {
            button1.Text = "Selesai Ubah";
            ButtonToggleHelper.DisableButtonsByTag(this, "FilterAction1");
        }
        else
        {
            button1.Text = "Selesai Ubah";
            IsEdit = false;
            ButtonToggleHelper.EnableButtonsByTag(this, "FilterAction1");
            ButtonToggleHelper.EnableButtonsByTag(this, "detailButton");
        }
    }

    private void buttonUbah_Click(object sender, EventArgs e)
    {
        IsEdit = true;
        EnableGroupBoxDetail(true);
        DatagridDetailOnUpdateChangeSelection();
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
        EnableGroupBoxDetail(false);
    }

    private async Task InsertDetail()
    {
        if (comboBoxTambahMasterbarang.SelectedValue == null || String.IsNullOrWhiteSpace(textBoxJumlah.Text))
        {
            return;
        }

        var item = (MasterbarangOptionWithSnDto)comboBoxTambahMasterbarang.SelectedItem;
        if (item.Sn == true && String.IsNullOrEmpty(textBoxSn.Text))
        {
            MessageBox.Show("SN harus di isi secara manual terlebih dahulu", "error", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (item.Sn == false && !String.IsNullOrEmpty(textBoxSn.Text))
        {
            MessageBox.Show("Item yang Anda pilih harus nya tidak memiliki SN, Akan dihapus secara Otomatis", "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        var kodet = (int)dataGridView1.SelectedRows[0].Cells["Kodet"].Value;
        try
        {
            var selectedIndex = dataGridView1.SelectedRows[0].Index;
            var result = await client.Insert_Detail_TransitAsync(kodet, new UpdateDetailByKodedDto()
            {
                Jumlah = Convert.ToInt32(textBoxJumlah.Text),
                NmrSn = item.Sn == false ? "" : textBoxSn.Text,
                Kodebarang = Convert.ToInt32(comboBoxTambahMasterbarang.SelectedValue)
            });
            var indexData = _TransitData.GetData().FindIndex(x => x.KodeT == result.KodeT);
            _TransitData.UpdateDatatableAndData(x => (int)x["KodeT"] == result.KodeT, indexData, result);
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

        var item = (MasterbarangOptionWithSnDto)comboBoxTambahMasterbarang.SelectedItem;
        if (item.Sn == true && String.IsNullOrWhiteSpace(textBoxSn.Text))
        {
            MessageBox.Show("SN harus di isi secara manual terlebih dahulu", "error", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        var kodet = (int)dataGridView1.SelectedRows[0].Cells["Kodet"].Value;
        var koded = (int)dataGridView2.SelectedRows[0].Cells["Koded"].Value;
        try
        {
            var selectedIndex = dataGridView1.SelectedRows[0].Index;
            var result = await client.Update_Detail_TransitAsync(kodet, koded, new UpdateDetailByKodedDto()
            {
                Jumlah = Convert.ToInt32(textBoxJumlah.Text),
                NmrSn = textBoxSn.Text,
                Kodebarang = Convert.ToInt32(comboBoxTambahMasterbarang.SelectedValue)
            });
            var indexData = _TransitData.GetData().FindIndex(x => x.KodeT == result.KodeT);
            _TransitData.UpdateDatatableAndData(x => (int)x["KodeT"] == result.KodeT, indexData, result);
            dataGridView1.ClearSelection();
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

    private void buttonTambahData_Click(object sender, EventArgs e)
    {
        EnableGroupBoxDetailTambah(true);
    }

    private void button9_Click(object sender, EventArgs e)
    {
        if (dataGridView1.Rows.Count <= 0 && dataGridView1.SelectedRows.Count <= 0)
        {
            return;
        }

        buttonBatalUbahheader.Enabled = true;
        buttonSaveUpdate.Enabled = true;
        buttonSimpan.Enabled = false;
        try
        {
            var kodet = (int)dataGridView1.SelectedRows[0].Cells["KodeT"].Value;
            var htransit = _TransitData.GetData().Where(x => x.KodeT == kodet).FirstOrDefault();

            if (htransit.TglTrans.HasValue)
            {
                datePickerTglTransit.Value = (DateTime)htransit.TglTrans;
            }

            comboBoxMastergudang.SelectedValue = htransit.Kodegudang;
            comboBoxMastergudangTujuan.SelectedValue = htransit.KodeGudangTujuan;
            comboBoxPenyiapOrder.SelectedValue = htransit.Kodepenyiap;
            textBoxKeterangan.Text = htransit.Keterangan;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void buttonBatalUbahheader_Click(object sender, EventArgs e)
    {
        buttonBatalUbahheader.Enabled = false;
        buttonSaveUpdate.Enabled = false;
        buttonSimpan.Enabled = true;
    }

    private async void buttonSaveUpdate_Click(object sender, EventArgs e)
    {
        if (AnyDataProcessRun)
        {
            return;
        }

        AnyDataProcessRun = true;
        dontUpdateChangeDgv = true;
        buttonBatalUbahheader.Enabled = false;
        buttonSaveUpdate.Enabled = false;
        try
        {
            var kodet = (int)dataGridView1.SelectedRows[0].Cells["KodeT"].Value;
            var selectedIndex = dataGridView1.SelectedRows[0].Index;
            var result = await client.Update_TransitAsync(kodet, new SaveHeaderTransitDto()
            {
                TglTrans = datePickerTglTransit.Value,
                Kodegudang = Convert.ToInt32(comboBoxMastergudang.SelectedValue),
                KodeGudangTujuan = Convert.ToInt32(comboBoxMastergudangTujuan.SelectedValue),
                Kodepenyiap = Convert.ToInt32(comboBoxPenyiapOrder.SelectedValue),
                Keterangan = textBoxKeterangan.Text.Trim()
            });
            var indexData = _TransitData.GetData().FindIndex(x => x.KodeT == result.KodeT);
            _TransitData.UpdateDatatableAndData(x => (int)x["KodeT"] == result.KodeT, indexData, result);
            dataGridView1.ClearSelection();
            dataGridView1.Rows[selectedIndex].Selected = true;

            buttonBatalUbahheader.Enabled = false;
            buttonSaveUpdate.Enabled = false;
            buttonSimpan.Enabled = true;
            datePickerTglTransit.Value = DateTime.Now;
            comboBoxPenyiapOrder.SelectedIndex = 0;
            comboBoxMastergudang.SelectedIndex = 0;
            comboBoxMastergudangTujuan.SelectedIndex = 0;
            textBoxKeterangan.Clear();
        }
        catch (ApiException ex)
        {
            MessageBox.Show(ex.Message);
            buttonBatalUbahheader.Enabled = true;
            buttonSaveUpdate.Enabled = true;
        }

        dontUpdateChangeDgv = false;
        AnyDataProcessRun = false;
    }
}