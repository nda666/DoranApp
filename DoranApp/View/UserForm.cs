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
    public partial class UserForm : Form
    {
        private MasteruserData _userData = new MasteruserData();

        public UserForm()
        {
            InitializeComponent();
        }

        private DataTable _dataTable { get; set; }

        public void ResetForm()
        {
            textboxId.Text = "";
            textboxUsername.Text = "";
            textboxPassword.Text = "";
            checkboxActive.Checked = true;
            comboRoles.SelectedIndex = -1;
        }

        private async void UserForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            buttonSave.Focus();


            //await fetchRoleData();

            await fetchData();
            comboFilterRole.Text = "";
            comboFilterRole.SelectedIndex = -1;

            dataGridView1.DoubleBuffered(true);
            _dataTable = _userData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);

            dataGridView1.ClearSelection();
            ResetForm();
        }

        public async Task fetchData()
        {
            ButtonToggleHelper.DisableButtonsByTag(this, "action");
            _userData.SetQuery(new
            {
                Username = textboxFilterUsername.Text.ToString(),
                Akses = comboFilterRole.SelectedIndex > -1 ? comboFilterRole.SelectedItem.ToString() : "",
                aktif = comboboxFilterActive.SelectedValue
            });
            try
            {
                await _userData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ButtonToggleHelper.EnableButtonsByTag(this, "action");
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                this.toolTip1.SetToolTip(this.buttonSave, "This is a tooltip for the button.");
                ButtonToggleHelper.DisableButtonsByTag(this, "action");
                var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;
                var isEdit = textboxId.Text.Length > 0;

                try
                {
                    await _userData.CreateOrUpdate(textboxId.Text, new
                    {
                        usernameKu = textboxUsername.Text,
                        passwordKu = textboxPassword.Text,
                        akses = comboRoles.SelectedItem.ToString(),
                        aktif = checkboxActive.Checked
                    });
                    ;
                    await _userData.Refresh();
                    if (isEdit && dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows[selectedRowIndex].Selected = true;
                    }

                    textboxUsername.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                ButtonToggleHelper.EnableButtonsByTag(this, "action");

                this.toolTip1.SetToolTip(this.buttonSave, null);
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

            var selectedUser = _userData.GetData().Where(user =>
                user.Kodeku.ToString() == dataGridView1.SelectedRows[0].Cells[0].Value.ToString()).First();
            textboxUsername.Text = selectedUser.Usernameku;
            textboxPassword.Text = selectedUser.Passwordku;
            comboRoles.SelectedItem = selectedUser.Akses;
            checkboxActive.Checked = selectedUser.Aktif ?? false;
            textboxId.Text = selectedUser.Kodeku.ToString();
            buttonDelete.Enabled = true;
        }

        private async Task Delete()
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                e.FormattingApplied = true; // <===VERY, VERY important tell it you've taken care of it.
                string temp = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (String.IsNullOrEmpty(temp))
                {
                    e.Value = "Sudah";
                }
                else
                {
                    e.Value = "Belum";
                }
            }
        }
    }
}