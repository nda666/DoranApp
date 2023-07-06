using DoranApp.Data;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class SalesTeamForm : Form
    {
        public SalesTeamForm()
        {
            InitializeComponent();
        }

        private DataTable _dataTable { get; set; }

        private SalesChannelData _salesChannelData = new SalesChannelData();

        private SalesTeamData _salesTeamData = new SalesTeamData();


        public void ResetForm()
        {
            textboxId.Text = "";
            textboxName.Text = "";
            textboxOmzetTarget.Text = "";
            textboxJeteTarget.Text = "";
            checkboxComissionTerms.Checked = true;
            checkboxShowLastYear.Checked = true;
            checkboxActive.Checked = true;
            comboSalesChannel.SelectedValue = true;
        }

        public async Task FetchSalesChannel()
        {
            _salesChannelData.SetQuery(new { active = "true" });
            await _salesChannelData.Refresh();
            var bsComboSalesChannel = new BindingSource();
            bsComboSalesChannel.DataSource = _salesChannelData.GetData();
            comboSalesChannel.DataSource = bsComboSalesChannel;
            comboSalesChannel.DisplayMember = "Name";
            comboSalesChannel.ValueMember = "Id";

            var bsComboFilterChannel = new BindingSource();
            bsComboFilterChannel.DataSource = _salesChannelData.GetData();
            comboFilterChannel.DataSource = bsComboFilterChannel;
            comboFilterChannel.DisplayMember = "Name";
            comboFilterChannel.ValueMember = "Id";
        }

        public async Task FetchData()
        {

            buttonFilter.Enabled = false;
            _salesTeamData.SetQuery(new
            {
                name = textboxFilterUsername.Text.ToString(),
                active = comboboxFilterActive.SelectedValue.ToString(),
                salesChannelId = comboFilterChannel.SelectedValue?.ToString(),
            });
            try
            {
                await _salesTeamData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            buttonFilter.Enabled = true;
        }

        private async Task Delete()
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var rest = new Rest($"salesteams/{textboxId.Text}");
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

        private async void SalesTeamForm_Load(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            buttonSave.Focus();

            Dictionary<string, string> activeOption = new Dictionary<string, string>();
            activeOption.Add("true", "Aktif");
            activeOption.Add("false", "Tidak Aktif");
            activeOption.Add("", "Semua");
            comboboxFilterActive.DataSource = new BindingSource(activeOption, null);
            comboboxFilterActive.DisplayMember = "Value";
            comboboxFilterActive.ValueMember = "Key";

            await FetchSalesChannel();

            comboSalesChannel.Text = "";
            comboSalesChannel.SelectedIndex = -1;

            comboFilterChannel.Text = "";
            comboFilterChannel.SelectedIndex = -1;

            await FetchData();
            dataGridView1.DoubleBuffered(true);
            _dataTable = _salesTeamData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.Sort(dataGridView1.Columns[8], ListSortDirection.Descending);
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.Columns[9].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.ClearSelection();

            ResetForm();
        }

        private async void buttonFilter_Click(object sender, EventArgs e)
        {
            await FetchData();
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;
                var isEdit = textboxId.Text.Length > 0;

                var uri = isEdit ? $"salesteams/{textboxId.Text}" : $"salesteams";
                var rest = new Rest(uri);
                try
                {
                    if (isEdit)
                    {
                        await rest.Put(new
                        {
                            id = textboxId.Text,
                            name = textboxName.Text,
                            salesChannelId = comboSalesChannel.SelectedValue,
                            jeteTarget = textboxJeteTarget.Text,
                            omzetTarget = textboxOmzetTarget.Text,
                            showLastYear = checkboxShowLastYear.Checked,
                            commissionTerms = checkboxShowLastYear.Checked,
                            active = checkboxActive.Checked,
                            
                        });
                    }
                    else
                    {
                        await rest.Post(new
                        {
                            name = textboxName.Text,
                            salesChannelId = comboSalesChannel.SelectedValue,
                            jeteTarget = textboxJeteTarget.Text,
                            omzetTarget = textboxOmzetTarget.Text,
                            showLastYear = checkboxShowLastYear.Checked,
                            commissionTerms = checkboxShowLastYear.Checked,
                            active = checkboxActive.Checked,
                        });
                    }
                    await _salesTeamData.Refresh();
                    if (isEdit)
                    {
                        dataGridView1.Rows[selectedRowIndex].Selected = true;
                    }
                    textboxName.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
            ResetForm();
            totalDataLabel.Text = $"Total Data: {dataGridView1.RowCount}";
        }

        private async void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dataGridView1.SelectedRows.Count > 0)
            {
                await Delete();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                buttonDelete.Enabled = false;
                return;
            }

            var selectedUser = _salesTeamData.GetData().Where(x => x.id == Guid.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).First();

            textboxId.Text = selectedUser.id.ToString();
            textboxName.Text = selectedUser.name;
            textboxOmzetTarget.Text = selectedUser.omzetTarget.ToString();
            textboxJeteTarget.Text = selectedUser.jeteTarget.ToString();
            checkboxComissionTerms.Checked = selectedUser.commissionTerms;
            checkboxShowLastYear.Checked = selectedUser.showLastYear;
            checkboxActive.Checked = selectedUser.active;
            comboSalesChannel.SelectedValue = selectedUser.salesChannelId;

            buttonDelete.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            await Delete();
        }
    }
}
