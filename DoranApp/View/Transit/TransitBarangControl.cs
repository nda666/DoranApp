using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;

namespace DoranApp.View;

public partial class TransitBarangControl : UserControl
{
    public TransitData _TransitData = new TransitData();

    public long _TransitLastPage = 0;
    public bool AnyDataProcessRun = false;
    public int? KodeEdit = null;
    public dynamic TransitDataQuery = new { };

    public TransitBarangControl()
    {
        InitializeComponent();
        dataGridView1.DoubleBuffered(true);
        dataGridView2.DoubleBuffered(true);
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
        comboPageSize.SelectedIndex = 0;
        dataGridView1.DataSource = _TransitData.GetDataTable();
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

        dataGridView2.Rows.Clear();
        try
        {
            var kodeh = (int)dataGridView1.SelectedRows[0].Cells["Kodet"].Value;

            var htransit = _TransitData.GetData().Where(x => x.KodeT == kodeh).FirstOrDefault();

            if (htransit == null)
            {
                return;
            }

            var Pcs = 0;
            var Varian = 0;
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
                Pcs += d.Jumlah ?? 0;
                Varian++;
            }

            labelVarian.Text = $"Varian : {Pcs.ToString()}";
            labelPcs.Text = $"PCS : {Pcs.ToString()}";
            buttonTambahData.Enabled = true;
        }
        catch (Exception ex)
        {
            ConsoleDump.Extensions.Dump(ex);
        }
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        if (AnyDataProcessRun)
        {
            return;
        }

        AnyDataProcessRun = true;
        LoadingButtonHelper.SetLoadingState(buttonSimpan, true);
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

            GetTransit();
            MessageBox.Show("\"Header Transit\" berhasil disimpan", "Sukses", MessageBoxButtons.OK);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        AnyDataProcessRun = false;
        LoadingButtonHelper.SetLoadingState(buttonSimpan, false);
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
            var result = await _TransitData.DeleteDetailByKoded(Kodet, koded.ToArray());
            var indexData = _TransitData.GetData().FindIndex(x => x.KodeT == result.KodeT);
            _TransitData.UpdateData(indexData, result);
            dataGridView1.ClearSelection();
            dataGridView1.Rows[selectedIndex].Selected = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        AnyDataProcessRun = false;
        buttonHapus.Enabled = true;
    }

    private void dataGridView2_SelectionChanged(object sender, EventArgs e)
    {
        if (dataGridView2.Rows.Count <= 0 && dataGridView2.SelectedRows.Count <= 0)
        {
            return;
        }

        buttonHapus.Enabled = dataGridView2.SelectedRows.Count >= 1;
        buttonUbah.Enabled = dataGridView2.SelectedRows.Count == 1;
    }
}