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

        private TransaksiData _transaksiData = new TransaksiData();
        private List<MasterbarangOption> _masterbarangOptions = new List<MasterbarangOption>();
        private List<Masterpengeluaran> _ekspedisiOption = new List<Masterpengeluaran>();
        private List<SalesOption> _salesOptions = new List<SalesOption>();
        private List<MasterpelangganOption> _masterpelangganOptions = new List<MasterpelangganOption>();
        private List<Mastergudang> _mastergudangOptions = new List<Mastergudang>();
        private List<Setlevelharga> _setlevelhargaOptions = new List<Setlevelharga>();

        public OrderInputForm()
        {
            InitializeComponent();
        }

        private void OrderInputForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DoubleBuffered(true);
            dataGridView2.DoubleBuffered(true);
            dataGridView3.DoubleBuffered(true);
            comboTempo.SelectedIndex = 0;
            FetchSales();
            FetchPelanggan();
            FetchLevelHarga();
            FetchEkspedisi();
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
    }
}
