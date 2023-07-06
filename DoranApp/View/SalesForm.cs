using DoranApp.Data;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class SalesForm : Form
    {
        private DataTable _dataTable { get; set; }

        private SalesData _salesData = new SalesData();

        private SalesTeamData _salesTeamData = new SalesTeamData();

        public SalesForm()
        {
            InitializeComponent();
        }

        public async Task FetchManager()
        {
            _salesData.SetQuery(new { active = "true", isManager = "true" });
            await _salesData.Refresh();
            var bsComboManager = new BindingSource();
            bsComboManager.DataSource = _salesData.GetData();
            comboManager.DataSource = bsComboManager;
            comboManager.DisplayMember = "Name";
            comboManager.ValueMember = "Id";

            var bsComboFilterManager = new BindingSource();
            bsComboFilterManager.DataSource = _salesData.GetData();
            comboFilterManager.DataSource = bsComboFilterManager;
            comboFilterManager.DisplayMember = "Name";
            comboFilterManager.ValueMember = "Id";

        }

        public async Task FetchSalesTeam()
        {
            _salesTeamData.SetQuery(new { active = "true" });
            await _salesTeamData.Refresh();
            var bsComboSalesTeam = new BindingSource();
            bsComboSalesTeam.DataSource = _salesTeamData.GetData();
            comboSalesTeam.DataSource = bsComboSalesTeam;
            comboSalesTeam.DisplayMember = "Name";
            comboSalesTeam.ValueMember = "Id";

            var bsFilterSalesTeam = new BindingSource();
            bsFilterSalesTeam.DataSource = _salesTeamData.GetData();
            comboFilterSalesTeam.DataSource = bsFilterSalesTeam;
            comboFilterSalesTeam.DisplayMember = "Name";
            comboFilterSalesTeam.ValueMember = "Id";

            
        }

        public async Task FetchData()
        {
            buttonFilter.Enabled = false;
            _salesData.SetQuery(new
            {
                name = textboxFilterUsername.Text.ToString() ?? "",
                active = comboboxFilterActive.SelectedValue?.ToString() ?? "",
                salesTeamId = comboFilterSalesTeam.SelectedValue?.ToString() ?? "",
                isManager = comboFilterIsManager.SelectedValue?.ToString() ?? "",
                managerId = comboFilterManager.SelectedValue?.ToString() ?? ""
            });
            try
            {
                await _salesData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            buttonFilter.Enabled = true;
        }

        private async void SalesForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            buttonSave.Focus();

            await FetchManager();
            await FetchSalesTeam();

            comboManager.Text = "";
            comboManager.SelectedIndex = -1;

            comboFilterSalesTeam.Text = "";
            comboFilterSalesTeam.SelectedIndex = -1;

            await FetchData();
            dataGridView1.DoubleBuffered(true);
            _dataTable = _salesData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.Sort(dataGridView1.Columns[8], ListSortDirection.Descending);
            dataGridView1.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.ClearSelection();

            //ResetForm();
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;
                var isEdit = textboxId.Text.Length > 0;

                var uri = isEdit ? $"sales/{textboxId.Text}" : $"sales";
                var rest = new Rest(uri);
                try
                {
                    if (isEdit)
                    {
                        await rest.Put(new
                        {
                            id = textboxId.Text,
                            name = textboxName.Text,
                            salesTeamId = comboSalesTeam.SelectedValue,
                            isManager = checkboxIsManager.Checked,
                            managerId = comboManager.SelectedValue,
                            getOmzetEmail = checkboxGetOmzetEmail.Checked,
                            active = checkboxActive.Checked,

                        });
                    }
                    else
                    {
                        await rest.Post(new
                        {
                            name = textboxName.Text,
                            salesTeamId = comboSalesTeam.SelectedValue,
                            isManager = checkboxIsManager.Checked,
                            managerId = comboManager.SelectedValue,
                            getOmzetEmail = checkboxGetOmzetEmail.Checked,
                            active = checkboxActive.Checked,
                        });
                    }
                    await _salesData.Refresh();
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

        private async void buttonFilter_Click(object sender, EventArgs e)
        {
            await FetchData();
        }

        private async void buttonRefreshRole_Click(object sender, EventArgs e)
        {
            buttonRefreshRole.Enabled = false;
            buttonRefreshRole.Cursor = Cursors.WaitCursor;
            await FetchSalesTeam();
            buttonRefreshRole.Enabled = true;
            buttonRefreshRole.Cursor = Cursors.Default;
        }

    }
}
