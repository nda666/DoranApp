using DoranApp.Data;
using DoranApp.Utils;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class HKategoribarangForm : Form
    {
        private DataTable _dataTable { get; set; }

        private HKategoriBarangData _hkategoribarangData = new HKategoriBarangData();

        public HKategoribarangForm()
        {
            InitializeComponent();
        }

        public async Task FetchData()
        {
            ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            _hkategoribarangData.SetQuery(new
            {
                nama = textboxFilterUsername.Text.ToString(),
                aktif = comboFilterActive.SelectedValue.ToString()
            });
            try
            {
                await _hkategoribarangData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        }

        public async Task DeleteData()
        {
            if (String.IsNullOrWhiteSpace(textboxId.Text))
            {
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {

                try
                {
                    buttonDelete.Enabled = false;
                    await _hkategoribarangData.Delete(textboxId.Text);
                    await FetchData();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                buttonDelete.Enabled = true;

            }
        }
        private async void HKategoribarangForm_Load(object sender, EventArgs e)
        {
            await FetchData();
            dataGridView1.DoubleBuffered(true);
            _dataTable = _hkategoribarangData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            dataGridView1.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.ClearSelection();
            ResetForm();
        }

        private async void buttonFilter_Click(object sender, EventArgs e)
        {
            await FetchData();
        }

        public void ResetForm()
        {
            textboxId.Text = "";
            textboxNama.Text = "";
            checkboxActive.Checked = true;
            checkboxCekTahunan.Checked = true;
            checkboxPerluSetHarga.Checked = true;
            checkBoxHargaKhusus.Checked = true;
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
                buttonDelete.Enabled = false;
                var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;
                var isEdit = textboxId.Text.Length > 0;
                var dataToSend = new
                {
                    nama = textboxNama.Text,
                    perlusetharga = checkboxPerluSetHarga.Checked.ToString(),
                    cektahunan = checkboxCekTahunan.Checked.ToString(),
                    hargakhusus = checkBoxHargaKhusus.Checked.ToString(),
                    aktif = checkboxActive.Checked.ToString(),
                };
                try
                {
                    await _hkategoribarangData.CreateOrUpdate(textboxId.Text, dataToSend);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                await FetchData();

                if (isEdit && dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[selectedRowIndex].Selected = true;
                }
                textboxNama.Focus();



                ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                buttonDelete.Enabled = true;
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            await DeleteData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                buttonDelete.Enabled = false;
                ResetForm();
                return;
            }

            var selected = _hkategoribarangData.GetData().Where(role => role.Kodeh.ToString() == dataGridView1.SelectedRows[0].Cells[0].Value.ToString()).First();
            textboxNama.Text = selected.Nama;
            textboxId.Text = selected.Kodeh.ToString();
            checkboxActive.Checked = selected.Aktif;
            checkboxCekTahunan.Checked = selected.Cektahunan;
            checkBoxHargaKhusus.Checked = selected.Hargakhusus;
            checkboxPerluSetHarga.Checked = selected.Perlusetharga;
            buttonDelete.Enabled = true;
        }

        private async void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dataGridView1.SelectedRows.Count > 0)
            {
                await DeleteData();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
            ResetForm();
            totalDataLabel.Text = $"Total Data: {dataGridView1.RowCount}";
        }
    }
}
