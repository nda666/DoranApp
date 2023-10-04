﻿using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.Utils;

namespace DoranApp.View.Pegawai
{
    public partial class DivisiControl : UserControl
    {
        private MasterdivisiData _masterdivisiData = new MasterdivisiData();

        public DivisiControl()
        {
            InitializeComponent();
        }

        private DataTable _dataTable { get; set; }
        private DataTable _dataTablePegawai { get; set; }

        protected override async void OnCreateControl()
        {
            base.OnCreateControl();
            await FetchData();
        }

        public async Task FetchData()
        {
            ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            _masterdivisiData.SetQuery(new
            {
                nama = textBoxFilterNama.Text.ToString(),
            });
            try
            {
                await _masterdivisiData.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
        }

        private async void DivisiControl_Load(object sender, EventArgs e)
        {
            await FetchData();
            dataGridView1.EnableDoubleBuffered(true);

            _dataTable = _masterdivisiData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 60;
            dataGridView1.ClearSelection();


            ResetForm();
        }

        public void ResetForm()
        {
            textBoxKode.Text = "";
            textBoxNama.Text = "";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                buttonDelete.Enabled = false;
                return;
            }

            var selectedUser = _masterdivisiData.GetData()
                .Where(x => x.Kode.ToString() == dataGridView1.SelectedRows[0].Cells[0].Value.ToString()).First();
            textBoxNama.Text = selectedUser.Nama;
            textBoxKode.Text = selectedUser.Kode.ToString();
            buttonDelete.Enabled = true;
            var columnSettingsList = new ColumnSettings<Masterpegawai>
            {
                { "Nama", x => x.Nama },
            };
            var dtGen = new DataTableGenerator(columnSettingsList);
            _dataTablePegawai = dtGen.CreateDataTable(selectedUser.Masterpegawais);
            dataGridView2.DataSource = _dataTablePegawai;
        }

        private async void SimpanBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
                buttonDelete.Enabled = false;
                var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;
                var isEdit = textBoxKode.Text.Length > 0;
                var dataToSend = new
                {
                    nama = textBoxNama.Text,
                };
                try
                {
                    await _masterdivisiData.CreateOrUpdate(textBoxKode.Text, dataToSend);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                await _masterdivisiData.Refresh();

                if (isEdit && dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[selectedRowIndex].Selected = true;
                }

                textBoxNama.Focus();
                ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                buttonDelete.Enabled = true;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await FetchData();
        }


        private async Task DeleteData()
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                buttonDelete.Enabled = false;
                ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
                try
                {
                    await _masterdivisiData.Delete(textBoxKode.Text);
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    buttonDelete.Enabled = true;
                    ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
                    await FetchData();
                }
            }
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            await DeleteData();
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
            dataGridView2.ClearSelection();
            ResetForm();
            labelTotalDivisi.Text = $"Total Data: {dataGridView1.RowCount}";
            labelTotalPegawai.Text = $"Total Pegawai: {dataGridView2.RowCount}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetForm();
            textBoxNama.Focus();
        }
    }
}