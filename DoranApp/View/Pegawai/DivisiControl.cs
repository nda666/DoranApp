using DoranApp.Data;
using DoranApp.Utils;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.View.Pegawai
{
    public partial class DivisiControl : UserControl
    {
        private DataTable _dataTable { get; set; }
        private MasterdivisiData _masterdivisiData = new MasterdivisiData();
        public DivisiControl()
        {
            InitializeComponent();
        }

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
            dataGridView1.DoubleBuffered(true);
           
            _dataTable = _masterdivisiData.GetDataTable();
            dataGridView1.DataSource = _dataTable;
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 60;
            //dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            //dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
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
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                buttonDelete.Enabled = false;
                return;
            }

            var selectedUser = _masterdivisiData.GetData().Where(x => x.Kode.ToString() == dataGridView1.SelectedRows[0].Cells[0].Value.ToString()).First();
            textBoxNama.Text = selectedUser.Nama;
            textBoxKode.Text = selectedUser.Kode.ToString();
            buttonDelete.Enabled = true;
        }

        private async void SimpanBtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
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
    }
}
