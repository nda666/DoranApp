using DoranApp.Data;
using DoranApp.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp
{

    public partial class FormMasterBarang : Form
    {
        private Pagination pagination { get; set; }
        private BindingSource bindingSource { get; set; }
        private DataTable dataTable { get; set; }
        private BindingSource BindingSource { get; set; }

        public FormMasterBarang()
        {
            InitializeComponent();
        }

        private void FormMasterBarang_Load(object sender, EventArgs e)
        {
            getKategori();
            dKategoriCombo.DisplayMember = "nama";
            dKategoriCombo.ValueMember = "koded";

            hKategoriCombo.DisplayMember = "nama";
            hKategoriCombo.ValueMember = "kodeh";

            gudangCombo.DisplayMember = "nama";
            gudangCombo.ValueMember = "kode";

            dataGridView1.DoubleBuffered(true);

            bindingSource = new BindingSource();
            dataTable = new DataTable();
            dataTable.Columns.AddRange(MasterBarangData.GetColumn());

            bindingSource.DataSource = dataTable;
            dataGridView1.DataSource = bindingSource;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public async void getKategori()
        {
            DKategoriBarangData dKategoriBarangData = new DKategoriBarangData(null);
            HKategoriBarangData hKategoriBarangData = new HKategoriBarangData(new { aktif = "1" });
            MasterGudangData masterGudangData = new MasterGudangData(new { aktif = "1" });
            dKategoriCombo.DataSource = await dKategoriBarangData.GetBindingSource(true);
            gudangCombo.DataSource = await masterGudangData.GetBindingSource(true);
        }

        public async Task getData()
        {
            var brgNama = brgNamaTxt.Text;
            MasterBarangData dMasterBarang = new MasterBarangData(new
            {
                searchJoin = "and",
                search = $"brgNama:{brgNama};" +
                $"brgAktif:1;" +
                $"stok:{stokTxt.Text}" +
                $"stokMinus:{(stokMinusCheck.Checked ? "1" : "0")}" +
                $"showHarga:{(showHargaCheck.Checked ? "1" : "0")}" +
                $"tambahHarga:{tambahHarga.Text}" +
                $"kategoriBrg:{dKategoriCombo.SelectedValue.ToString()}" +
                $"tipebarang:{hKategoriCombo.SelectedValue.ToString()}",

            });
            Console.WriteLine($"brgNama:{brgNama};brgAktif:1;kategoriBrg:{dKategoriCombo.SelectedValue.ToString()}");
            try
            {
                dataGridView1.DataSource = await dMasterBarang.GetBindingSource();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            dataTable.Rows.Clear();
            button1.Enabled = false;
            await getData();
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dKategoriCombo.SelectedValue.ToString());

        }

        private void hKategoriCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tambahHarga_Leave(object sender, EventArgs e)
        {
            Double value;
            if (Double.TryParse(tambahHarga.Text, out value))
                tambahHarga.Text = String.Format("{0:n0}", value);
            else
                tambahHarga.Text = String.Empty;
        }
    }
}
