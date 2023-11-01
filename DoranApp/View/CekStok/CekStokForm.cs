using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DoranApp.DataGlobal;
using DoranApp.Utils;

namespace DoranApp.View.CekStok;

public partial class CekStokForm : Form
{
    public HistoryBarangMasukControl _HistoryBarangMasukControl;
    public MutasiBarangControl _MutasiBarangControl;

    public IDisposable FetchMasterbarangDisposable;

    public IDisposable FetchMastergudangDisposable;
    public List<MasterbarangOptionWithSnDto> MasterbarangOptionWithSns = new List<MasterbarangOptionWithSnDto>();
    public List<MastergudangOptionDto> MastergudangOptions = new List<MastergudangOptionDto>();

    public CekStokForm()
    {
        InitializeComponent();
    }

    public async void FetchMasterbarang()
    {
        FetchMasterbarangDisposable = FetchMasterbarangOptionWithSn.Subscribe(x =>
        {
            _MutasiBarangControl.comboMasterbarang.DataSource = x.ToList();
            _HistoryBarangMasukControl.comboMasterbarang.DataSource = x.ToList();
        });
        await FetchMasterbarangOptionWithSn.Run();
    }

    public async void FetchMastergudang()
    {
        FetchMastergudangDisposable = FetchMastergudangOption.Subscribe(x =>
        {
            _MutasiBarangControl.comboMastergudang.DataSource = x.ToList();
        });
        await FetchMastergudangOption.Run();
    }

    public async void FetchSupplier()
    {
        FetchMastergudangDisposable = FetchMastergudangOption.Subscribe(x =>
        {
            _MutasiBarangControl.comboMastergudang.DataSource = x.ToList();
        });
        await FetchMastergudangOption.Run();
    }

    private void CekStokForm_Load(object sender, EventArgs e)
    {
        _MutasiBarangControl = new MutasiBarangControl();
        _HistoryBarangMasukControl = new HistoryBarangMasukControl();
        _MutasiBarangControl.Dock = DockStyle.Fill;
        _HistoryBarangMasukControl.Dock = DockStyle.Fill;
        // Add controls, set properties, etc. for each UserControl as needed

        // Create TabPages and assign UserControls to them
        TabPage tabPage1 = new TabPage("Mutasi Barang");
        tabPage1.Name = "MutasiBarang";
        tabPage1.Controls.Add(_MutasiBarangControl);
        tabPage1.Tag =
            _MutasiBarangControl; // Optionally, associate the UserControl with the TabPage for easy access later
        tabPage1.Select();
        TabPage tabPage2 = new TabPage("History Barang Masuk");
        tabPage2.Name = "HistoryBarangMasuk";
        tabPage2.Controls.Add(_HistoryBarangMasukControl);
        tabPage2.Tag = _HistoryBarangMasukControl;


        // Add TabPages to the TabControl
        tabControl1.TabPages.Add(tabPage1);
        tabControl1.TabPages.Add(tabPage2);
        FetchMasterbarang();
        FetchMastergudang();
    }

    private void CekStokForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F2 && tabControl1.SelectedTab.Name == "MutasiBarang")
        {
            _MutasiBarangControl.comboMasterbarang.Select();
            _MutasiBarangControl.comboMasterbarang.DroppedDown = true;
        }
    }
}