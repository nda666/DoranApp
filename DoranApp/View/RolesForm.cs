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
    public partial class RolesForm : Form
    {
        private DataTable _dataTable { get; set; }

        private RolesData _rolesData = new RolesData();
        public RolesForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;
                var isEdit = textBox3.Text.Length > 0;

                var uri = isEdit ? $"roles/{textBox3.Text}" : $"roles";
                var rest = new Rest(uri);
                try
                {
                    if (isEdit)
                    {
                        await rest.Put(new { name = textBox1.Text, active = checkBox1.Checked });
                    }
                    else
                    {
                        await rest.Post(new { name = textBox1.Text, active = checkBox1.Checked });
                    }
                    await _rolesData.Refresh();
                    if (isEdit)
                    {
                        dataGridView1.Rows[selectedRowIndex].Selected = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private async void RolesForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            buttonSave.Focus();

            dataGridView1.DoubleBuffered(true);
            _dataTable = _rolesData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending);
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.ClearSelection();

            Dictionary<string, string> activeOption = new Dictionary<string, string>();
            activeOption.Add("true", "Aktif");
            activeOption.Add("false", "Tidak Aktif");
            activeOption.Add("", "Semua");
            comboBox1.DataSource = new BindingSource(activeOption, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            await fetchRole();
        }

        public async Task fetchRole()
        {
            _rolesData.SetQuery(new
            {
                name = searchNameTBox.Text,
                active = comboBox1.SelectedValue.ToString()
            });
            try
            {
                await _rolesData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            buttonCari.Enabled = false;
            await fetchRole();
            buttonCari.Enabled = true;
        }

        private void resetForm()
        {
            textBox1.Clear();
            textBox3.Clear();
            checkBox1.Checked = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                button3.Enabled = false;
                return;
            }

            var selectedRole = _rolesData.GetData().Where(role => role.id == Guid.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).First();
            textBox1.Text = selectedRole.name;
            checkBox1.Checked = selectedRole.active;
            textBox3.Text = selectedRole.id.ToString();
            button3.Enabled = true;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
            resetForm();
            totalDataLabel.Text = $"Total Data: {dataGridView1.RowCount}";
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            // ClearSelectionAndResetSuppression();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            deleteRole();
        }

        private async void deleteRole()
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var rest = new Rest($"roles/{textBox3.Text}");
                try
                {
                    await rest.Delete();
                    _rolesData.Refresh();
                    resetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dataGridView1.SelectedRows.Count > 0)
            {
                deleteRole();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length <= 0)
            {
                buttonSave.Text = "Simpan";
            }
            else
            {
                buttonSave.Text = "Ubah";
            }
        }
    }
}
