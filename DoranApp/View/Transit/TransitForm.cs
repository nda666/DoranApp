using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.DataGlobal;
using DoranApp.Utils;

namespace DoranApp.View.Transit;

public partial class TransitForm : Form
{
    public PermintaanSalesControl _PermintaanSalesControl;

    public TransitBarangControl _TransitBarangControl;
    public Dictionary<string, IDisposable> DisposablesDicts = new Dictionary<string, IDisposable>();

    public List<MasterbarangOptionDto> MasterbarangOptionWithSnOptions = new List<MasterbarangOptionDto>();
    public List<CommonResultDto> MastergudangOptions = new List<CommonResultDto>();
    public List<CommonResultDto> MastergudangTujuanOptions = new List<CommonResultDto>();
    public List<Penyiaporder> PenyiapOrderOptions = new List<Penyiaporder>();

    public TransitForm()
    {
        InitializeComponent();
    }


    public async Task SubscribeMasterbarangOptionWithSn()
    {
        DisposablesDicts["masterbarang"] = FetchMasterbarangOptionWithSn.Subscribe(x =>
        {
            _TransitBarangControl.comboBoxTambahMasterbarang.DataSource = x.ToList();
            _PermintaanSalesControl.comboBoxTambahMasterbarang.DataSource = x.ToList();
            _PermintaanSalesControl.comboFilterBarang.DataSource = x.ToList().Prepend(
                new MasterbarangOptionWithSnDto
                {
                    BrgKode = null,
                    Sn = false,
                    BrgNama = "SEMUA BARANG",
                    JurnalBiaya = false
                }).ToList();
        });
        await FetchMasterbarangOptionWithSn.Run();
    }

    public async Task SubscribeMastergudangBoleTransitOption()
    {
        DisposablesDicts["mastergudangboletransit"] = FetchMastergudangBoleTransitOption.Subscribe(x =>
        {
            _TransitBarangControl.comboBoxMastergudangTujuan.DataSource = x.ToList();
            _TransitBarangControl.comboBoxFilterGudangTujuan.DataSource = x.Prepend(new CommonResultDto()
            {
                Kode = null,
                Nama = "SEMUA TUJUAN"
            }).ToList();

            _PermintaanSalesControl.comboGudangTujuan.DataSource = x.ToList().Prepend(new CommonResultDto()
            {
                Kode = null,
                Nama = "PILIH GUDANG TUJUAN"
            }).ToList();
        });
        await FetchMastergudangBoleTransitOption.Run();
    }

    public async Task SubscribeMastergudangOption()
    {
        DisposablesDicts["mastergudang"] = FetchMastergudangOption.Subscribe(x =>
        {
            _TransitBarangControl.comboBoxMastergudang.DataSource = x.ToList();
            _TransitBarangControl.comboBoxFilterGudang.DataSource = x.Prepend(new MastergudangOptionDto
            {
                Kode = null,
                Nama = "Semua Gudang"
            }).ToList();

            // _PermintaanSalesControl.comboGudangAsal.DataSource = x.ToList().Prepend(new MastergudangOptionDto()
            // {
            //     Kode = null,
            //     Nama = "PILIH GUDANG ASAL"
            // }).ToList();
            // _PermintaanSalesControl.comboFilterGudang.DataSource = x.Prepend(new MastergudangOptionDto
            // {
            //     Kode = null,
            //     Nama = "Semua Gudang"
            // }).ToList();
        });
        await FetchMastergudangOption.Run();
    }

    public async Task SubscribePenyiapOrder()
    {
        DisposablesDicts["penyiaporder"] = FetchPenyiaporder.Subscribe(x =>
        {
            _TransitBarangControl.comboBoxPenyiapOrder.DataSource = x.ToList();
            _TransitBarangControl.comboBoxFilterPenyiap.DataSource = x.ToList().Prepend(new CommonResultDto()
            {
                Kode = null,
                Nama = "Semua Penyiap"
            }).ToList();

            _PermintaanSalesControl.comboPenyiapOrder.DataSource = x.ToList();
        });
        await FetchPenyiaporder.Run();
    }

    public async Task SubscribeSales()
    {
        DisposablesDicts["sales"] = FetchSalesOption.Subscribe(x =>
        {
            _PermintaanSalesControl.comboSales.DataSource = x.ToList();
        });
        await FetchSalesOption.Run();
    }

    private async Task SubscribeRequiredData()
    {
        try
        {
            await Task.WhenAll(
                SubscribeMasterbarangOptionWithSn(),
                SubscribePenyiapOrder(),
                SubscribeMastergudangOption(),
                SubscribeMastergudangBoleTransitOption(),
                SubscribeSales()
            );
        }
        catch (Exception ex)
        {
            var checkRetry = MessageBox.Show($"Terjadi kesalahan saat request data.\n{ex.Message}\n\nCoba lagi?",
                "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            if (checkRetry == DialogResult.OK)
            {
                SubscribeRequiredData();
            }
        }
    }

    private void TransitForm_Load(object sender, EventArgs e)
    {
        // Create UserControls
        _TransitBarangControl = new TransitBarangControl();
        _PermintaanSalesControl = new PermintaanSalesControl();
        _TransitBarangControl.Dock = DockStyle.Fill;
        _PermintaanSalesControl.Dock = DockStyle.Fill;
        // Add controls, set properties, etc. for each UserControl as needed

        // Create TabPages and assign UserControls to them
        TabPage tabPage1 = new TabPage("Transit Barang");
        tabPage1.Controls.Add(_TransitBarangControl);
        tabPage1.Tag =
            _TransitBarangControl; // Optionally, associate the UserControl with the TabPage for easy access later

        TabPage tabPage2 = new TabPage("Permintaan Sales");
        tabPage2.Controls.Add(_PermintaanSalesControl);
        tabPage2.Tag = _PermintaanSalesControl;

        // Add TabPages to the TabControl
        tabControl1.TabPages.Add(tabPage1);
        tabControl1.TabPages.Add(tabPage2);

        SubscribeRequiredData();
    }

    private void TransitForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        foreach (var key in DisposablesDicts)
        {
            DisposablesDicts[key.Key]?.Dispose();
        }
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}