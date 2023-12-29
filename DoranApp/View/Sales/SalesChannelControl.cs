using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;

namespace DoranApp.View
{
    public partial class SalesChannelControl : UserControl
    {
        private MasterchannelsalesData _salesChannelData = new MasterchannelsalesData();

        public SalesChannelControl()
        {
            InitializeComponent();
        }

        private DataTable _dataTable { get; set; }

        public void ResetForm()
        {
            textboxId.Text = "";
            textboxName.Text = "";
            checkboxActive.Checked = true;
        }

        public async Task FetchData()
        {
            ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            _salesChannelData.SetQuery(new
            {
                nama = textboxFilterUsername.Text.ToString(),
                aktif = comboboxFilterActive.SelectedValue.ToString()
            });
            try
            {
                await _salesChannelData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        }

        private async void buttonFilter_Click(object sender, EventArgs e)
        {
            await FetchData();
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
                buttonDelete.Enabled = false;
                var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;
                var isEdit = textboxId.Text.Length > 0;
                var dataToSend = new
                {
                    nama = textboxName.Text,
                    aktif = checkboxActive.Checked
                };
                try
                {
                    await _salesChannelData.CreateOrUpdate(textboxId.Text, dataToSend);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                await _salesChannelData.Refresh();

                if (isEdit && dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[selectedRowIndex].Selected = true;
                }

                textboxName.Focus();


                ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                buttonDelete.Enabled = true;
            }
        }

        private async Task Delete()
        {
            if (String.IsNullOrWhiteSpace(textboxId.Text))
            {
                return;
            }

            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var rest = new Rest($"masterchannelsales/{textboxId.Text}");
                try
                {
                    buttonDelete.Enabled = false;
                    await rest.Delete();
                    await FetchData();
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    buttonDelete.Enabled = true;
                }
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            await Delete();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                buttonDelete.Enabled = false;
                return;
            }

            var selectedUser = _salesChannelData.GetData().Where(role =>
                role.Kode.ToString() == dataGridView1.SelectedRows[0].Cells[0].Value.ToString()).First();
            textboxName.Text = selectedUser.Nama;
            textboxId.Text = selectedUser.Kode.ToString();
            buttonDelete.Enabled = true;
        }

        private async void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dataGridView1.SelectedRows.Count > 0)
            {
                await Delete();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
            ResetForm();
            totalDataLabel.Text = $"Total Data: {dataGridView1.RowCount}";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private async void SalesChannelControl_Load(object sender, EventArgs e)
        {
            buttonSave.Focus();

            Dictionary<string, string> activeOption = new Dictionary<string, string>();
            activeOption.Add("true", "Aktif");
            activeOption.Add("false", "Tidak Aktif");
            activeOption.Add("", "Semua");
            comboboxFilterActive.DataSource = new BindingSource(activeOption, null);
            comboboxFilterActive.DisplayMember = "Value";
            comboboxFilterActive.ValueMember = "Key";

            await FetchData();
            dataGridView1.EnableDoubleBuffered(true);
            _dataTable = _salesChannelData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            // dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
            dataGridView1.ClearSelection();

            ResetForm();
        }
    }
}