﻿using DoranApp.Data;
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

    public partial class SalesChannelForm : Form
    {
        private DataTable _dataTable { get; set; }

        private SalesChannelData _salesChannelData = new SalesChannelData();

        public SalesChannelForm()
        {
            InitializeComponent();
        }

        public void ResetForm()
        {
            textboxId.Text = "";
            textboxName.Text = "";
            checkboxActive.Checked = true;
        }

        public async Task FetchData()
        {

            buttonFilter.Enabled = false;
            _salesChannelData.SetQuery(new
            {
                name = textboxFilterUsername.Text.ToString(),
                active = comboboxFilterActive.SelectedValue.ToString()
            });
            try
            {
                await _salesChannelData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            buttonFilter.Enabled = true;
        }

        private async void SalesChannelForm_Load(object sender, EventArgs e)
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

            await FetchData();
            dataGridView1.DoubleBuffered(true);
            _dataTable = _salesChannelData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Descending);
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
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

                var uri = isEdit ? $"saleschannels/{textboxId.Text}" : $"saleschannels";
                var rest = new Rest(uri);
                try
                {
                    if (isEdit)
                    {
                        await rest.Put(new
                        {
                            id = textboxId.Text,
                            name = textboxName.Text,
                            active = checkboxActive.Checked
                        });
                    }
                    else
                    {
                        await rest.Post(new
                        {
                            name = textboxName.Text,
                            active = checkboxActive.Checked
                        });
                    }
                    await _salesChannelData.Refresh();
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

        private async Task Delete()
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var rest = new Rest($"saleschannels/{textboxId.Text}");
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

            var selectedUser = _salesChannelData.GetData().Where(role => role.id == Guid.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).First();
            textboxName.Text = selectedUser.name;
            checkboxActive.Checked = selectedUser.active;
            textboxId.Text = selectedUser.id.ToString();
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
    }
}
