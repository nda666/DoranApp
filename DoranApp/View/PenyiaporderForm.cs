using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;

namespace DoranApp.View
{
    public partial class PenyiaporderForm : MdiChildForm, INotifyPropertyChanged
    {
        private string _itemId;

        private PenyiaporderData _mastergudangData = new PenyiaporderData();

        public PenyiaporderForm()
        {
            InitializeComponent();
        }

        public string ItemId
        {
            get { return _itemId; }
            set
            {
                if (_itemId != value)
                {
                    _itemId = value;
                    OnPropertyChanged(nameof(ItemId));
                }
            }
        }

        private DataTable _dataTable { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void PenyiaporderForm_Load(object sender, EventArgs e)
        {
            await FetchData();
            dataGridView1.EnableDoubleBuffered(true);
            _dataTable = _mastergudangData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.ClearSelection();
            ResetForm();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == nameof(ItemId))
            {
                if (string.IsNullOrEmpty(ItemId))
                {
                    buttonUbah.Text = "UBAH";
                }
                else
                {
                    buttonUbah.Text = "BATAL";
                }
            }
        }

        public void ResetForm()
        {
            textBoxNama.Text = "";
        }

        private void setAktifForm(bool? val)
        {
            if (val == true)
            {
                radioAktif.Checked = true;
                return;
            }

            if (val == false)
            {
                radioTidakAktif.Checked = true;
                return;
            }

            radioAktifSemua.Checked = true;
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


        public async Task FetchData()
        {
            ItemId = null;
            ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            _mastergudangData.SetQuery(new
            {
                nama = textBoxNama.Text.ToString(),
                aktif = getAktifForm(),
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (buttonUbah.Text == "BATAL")
            {
                ItemId = null;
                textBoxNama.Text = "";
                return;
            }

            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selected = _mastergudangData.GetData().Where(x =>
                    x.Kode.ToString() == dataGridView1.SelectedRows[0].Cells[0].Value.ToString()).First();
                ItemId = selected.Kode.ToString();
                textBoxNama.Text = selected.Nama.ToString();
                setAktifForm(selected.Aktif);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
                var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;
                var isEdit = String.IsNullOrWhiteSpace(ItemId);
                var dataToSend = new
                {
                    nama = textBoxNama.Text.ToString(),
                    aktif = getAktifForm(),
                };
                try
                {
                    await _mastergudangData.CreateOrUpdate(ItemId, dataToSend);
                    ItemId = null;
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                await _mastergudangData.Refresh();

                if (isEdit && dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[selectedRowIndex].Selected = true;
                }

                textBoxNama.Focus();


                ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
            }
        }
    }
}