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
    public partial class UserForm : Form
    {
        private DataTable _dataTable { get; set; }

        private UserData _userData = new UserData();

        private RolesData _rolesData = new RolesData();

        public UserForm()
        {
            InitializeComponent();
        }

        public void ResetForm()
        {
            textboxId.Text = "";
            textboxUsername.Text = "";
            textboxPassword.Text = "";
            checkboxActive.Checked = true;
            comboRoles.SelectedIndex = -1;
        }

        public async Task fetchRoleData()
        {

            buttonRefreshRole.Enabled = false;
            buttonRefreshRole.Cursor = Cursors.WaitCursor;

            _rolesData.SetQuery(new { active = "true" });
            await _rolesData.Refresh();
            var bsRoleData = new BindingSource();
            bsRoleData.DataSource = _rolesData.GetData();
            comboRoles.DataSource = bsRoleData;
            comboRoles.DisplayMember = "Name";
            comboRoles.ValueMember = "Id";

            var bsSearchData = new BindingSource();
            bsSearchData.DataSource = _rolesData.GetData();
            comboFilterRole.DataSource = bsSearchData;
            comboFilterRole.DisplayMember = "Name";
            comboFilterRole.ValueMember = "Id";

            comboRoles.Text = "";
            comboRoles.SelectedIndex = -1;

            comboFilterRole.Text = "";
            comboFilterRole.SelectedIndex = -1;

            buttonRefreshRole.Enabled = true;
            buttonRefreshRole.Cursor = Cursors.Default;
            buttonRefreshRole.Focus();


        }

        private async void UserForm_Load(object sender, EventArgs e)
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
            //await fetchRoleData();

            await fetchData();
            comboFilterRole.Text = "";
            comboFilterRole.SelectedIndex = -1;

            dataGridView1.DoubleBuffered(true);
            _dataTable = _userData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Descending);
            dataGridView1.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.ClearSelection();
            ResetForm();
        }

        public async Task fetchData()
        {

            buttonFilter.Enabled = false;
            _userData.SetQuery(new
            {
                username = textboxFilterUsername.Text.ToString(),
                roleId = comboFilterRole.SelectedIndex > -1 ? comboFilterRole.SelectedValue.ToString() : "",
                active = comboboxFilterActive.SelectedValue.ToString()
            });
            try
            {
                await _userData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            buttonFilter.Enabled = true;
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;
                var isEdit = textboxId.Text.Length > 0;

                var uri = isEdit ? $"users/{textboxId.Text}" : $"users";
                var rest = new Rest(uri);
                try
                {
                    if (isEdit)
                    {
                        await rest.Put(new
                        {
                            kodeKu = textboxId.Text,
                            usernameKu = textboxUsername.Text,
                            passwordKu = textboxPassword.Text,
                            akses = comboRoles.SelectedText.ToString(),
                            aktif = checkboxActive.Checked
                        });
                    }
                    else
                    {
                        await rest.Post(new
                        {
                            usernameKu = textboxUsername.Text,
                            passwordKu = textboxPassword.Text,
                            akses = comboRoles.SelectedValue.ToString(),
                            aktif = checkboxActive.Checked
                        });
                    }
                    await _userData.Refresh();
                    if (isEdit)
                    {
                        dataGridView1.Rows[selectedRowIndex].Selected = true;
                    }
                    textboxUsername.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comboRoles_Leave(object sender, EventArgs e)
        {
            if (comboRoles.SelectedIndex == -1)
            {
                comboRoles.Text = "";
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                buttonDelete.Enabled = false;
                return;
            }

            var selectedUser = _userData.GetData().Where(user => user.Kodeku.ToString() == dataGridView1.SelectedRows[0].Cells[0].Value.ToString()).First();
            textboxUsername.Text = selectedUser.Usernameku;
            textboxPassword.Text = selectedUser.Passwordku;
            //comboRoles.SelectedValue = selectedUser.roleId;
            checkboxActive.Checked = selectedUser.Aktif;
            textboxId.Text = selectedUser.Kodeku.ToString();
            buttonDelete.Enabled = true;
        }

        private async Task Delete()
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var rest = new Rest($"users/{textboxId.Text}");
                try
                {
                    buttonDelete.Enabled = false;
                    await rest.Delete();
                    await fetchData();
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

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
            ResetForm();
            totalDataLabel.Text = $"Total Data: {dataGridView1.RowCount}";
        }

        private void comboboxFilterRole_Leave(object sender, EventArgs e)
        {
            if (comboFilterRole.SelectedIndex == -1)
            {
                comboFilterRole.Text = "";
            }
        }

        private async void buttonFilter_Click(object sender, EventArgs e)
        {
            await fetchData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(InternetAvailabilityService.isOnline.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetForm();
            dataGridView1.ClearSelection();
        }

        private async void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dataGridView1.SelectedRows.Count > 0)
            {
                await Delete();
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            await fetchRoleData();
        }

        private void activeComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show("A");
        }

        private void comboRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(comboRoles.SelectedText);
        }
    }
}
