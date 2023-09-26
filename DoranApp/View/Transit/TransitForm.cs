using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DoranApp.DataGlobal;
using DoranApp.Utils;

namespace DoranApp.View.Transit;

public partial class TransitForm : Form
{
    public IDisposable FetchMasterbarangOptionWithSnSubs;

    public IDisposable FetchMastergudangBoleTransitOptionSubs;

    public IDisposable FetchMastergudangOptionSubs;
    public IDisposable FetchPenyiaporderSubs;
    public List<MasterbarangOptionDto> MasterbarangOptionWithSnOptions = new List<MasterbarangOptionDto>();
    public List<CommonResultDto> MastergudangOptions = new List<CommonResultDto>();
    public List<CommonResultDto> MastergudangTujuanOptions = new List<CommonResultDto>();
    public List<Penyiaporder> PenyiapOrderOptions = new List<Penyiaporder>();
    public PermintaanSalesControl permintaanSalesControl;

    public TransitBarangControl transitBarangControl;

    public TransitForm()
    {
        InitializeComponent();
    }


    public async void SubscribeMasterbarangOptionWithSn()
    {
        FetchMasterbarangOptionWithSnSubs = FetchMasterbarangOptionWithSn.Subscribe(x =>
        {
            transitBarangControl.comboBoxTambahMasterbarang.DataSource = x.ToList();
        });
        try
        {
            await FetchMasterbarangOptionWithSn.Run();
        }
        catch (Exception ex)
        {
            var result = MessageBox.Show("Terjadi kesalahan saat request master barang. " + ex.Message + ". Coba lagi?",
                "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (result == DialogResult.Yes)
            {
                SubscribeMasterbarangOptionWithSn();
            }
            else
            {
                this.Close();
            }
        }
    }

    public async void SubscribeMastergudangBoleTransitOption()
    {
        FetchMastergudangBoleTransitOptionSubs = FetchMastergudangBoleTransitOption.Subscribe(x =>
        {
            transitBarangControl.comboBoxMastergudangTujuan.DataSource = x.ToList();
            transitBarangControl.comboBoxFilterGudangTujuan.DataSource = x.Prepend(new CommonResultDto()
            {
                Kode = null, Nama = "Semua Tujuan"
            }).ToList();
        });
        await FetchMastergudangBoleTransitOption.Run();
    }

    public async void SubscribeMastergudangOption()
    {
        FetchMastergudangOptionSubs = FetchMastergudangOption.Subscribe(x =>
        {
            transitBarangControl.comboBoxMastergudang.DataSource = x.ToList();
            transitBarangControl.comboBoxFilterGudang.DataSource = x.Prepend(new MastergudangOptionDto()
            {
                Kode = null, Nama = "Semua Tujuan"
            }).ToList();
        });
        await FetchMastergudangOption.Run();
    }

    public async void SubscribePenyiapOrder()
    {
        FetchPenyiaporderSubs = FetchPenyiaporder.Subscribe(x =>
        {
            transitBarangControl.comboBoxPenyiapOrder.DataSource = x.ToList();
            transitBarangControl.comboBoxFilterPenyiap.DataSource = x.ToList().Prepend(new Utils.Penyiaporder()
            {
                Kode = null, Nama = "Semua Penyiap"
            }).ToList();
        });
        await FetchPenyiaporder.Run();
    }


    private void TransitForm_Load(object sender, EventArgs e)
    {
        // Create UserControls
        transitBarangControl = new TransitBarangControl();
        permintaanSalesControl = new PermintaanSalesControl();
        transitBarangControl.Dock = DockStyle.Fill;
        permintaanSalesControl.Dock = DockStyle.Fill;
        // Add controls, set properties, etc. for each UserControl as needed

        // Create TabPages and assign UserControls to them
        TabPage tabPage1 = new TabPage("Transit Barang");
        tabPage1.Controls.Add(transitBarangControl);
        tabPage1.Tag =
            transitBarangControl; // Optionally, associate the UserControl with the TabPage for easy access later

        TabPage tabPage2 = new TabPage("Permintaan Sales");
        tabPage2.Controls.Add(permintaanSalesControl);
        tabPage2.Tag = permintaanSalesControl;

        // Add TabPages to the TabControl
        tabControl1.TabPages.Add(tabPage1);
        tabControl1.TabPages.Add(tabPage2);

        SubscribeMasterbarangOptionWithSn();
        SubscribePenyiapOrder();
        SubscribeMastergudangOption();
        SubscribeMastergudangBoleTransitOption();
    }

    private void TransitForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        FetchPenyiaporderSubs?.Dispose();
        FetchMastergudangOptionSubs?.Dispose();
        FetchMastergudangBoleTransitOptionSubs?.Dispose();
        FetchMasterbarangOptionWithSnSubs?.Dispose();
    }
}