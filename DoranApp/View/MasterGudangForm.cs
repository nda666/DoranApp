using DoranApp.Data;
using DoranApp.Utils;
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
    public partial class MasterGudangForm : Form
    {
        private DataTable _dataTable { get; set; }

        private MastergudangData _mastergudangData = new MastergudangData();

        public MasterGudangForm()
        {
            InitializeComponent();
        }

        private async void MasterGudangForm_Load(object sender, EventArgs e)
        {
            await FetchData();
            dataGridView1.DoubleBuffered(true);
            _dataTable = _mastergudangData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.ClearSelection();
            ResetForm();
        }

        public void ResetForm()
        {
            textBoxNama.Text = "";
            textboxUrut.Text = "";
        }

        private bool? getAktifForm()
        {
            if (radioAktif.Checked)
            {
                return true;
            }
            if (radioTidakAktif.Checked)
            {
                return false;
            }
            return null;
        }

        private bool? getBolehTransitForm()
        {
            if (radioTransitIya.Checked)
            {
                return true;
            }
            if (radioTransitTidak.Checked)
            {
                return false;
            }
            return null;
        }

        public async Task FetchData()
        {

            ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            _mastergudangData.SetQuery(new
            {
                nama = textBoxNama.Text.ToString(),
                aktif = getAktifForm(),
                urut = textboxUrut.Text.ToString(),
                boletransit = getBolehTransitForm(),
            });
            try
            {
                await _mastergudangData.Refresh();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            label3.Text = $"Jumlah Data: {dataGridView1.Rows.Count}";
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await FetchData();
        }
    }
}
