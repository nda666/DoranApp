using DoranApp.Data;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class SalesTeamControl : UserControl
    {
        public SalesTeamControl()
        {
            InitializeComponent();
        }

        private DataTable _dataTable { get; set; }

        private MasterchannelsalesData _salesChannelData = new MasterchannelsalesData();

        private MastertimsalesData _salesTeamData = new MastertimsalesData();


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
            comboSalesChannel.DisplayMember = "Nama";
            comboSalesChannel.ValueMember = "Kode";

            var bsComboFilterChannel = new BindingSource();
            bsComboFilterChannel.DataSource = _salesChannelData.GetData();
            comboFilterChannel.DataSource = bsComboFilterChannel;
            comboFilterChannel.DisplayMember = "Nama";
            comboFilterChannel.ValueMember = "Kode";
        }

        public async Task FetchData()
        {

            buttonFilter.Enabled = false;
            _salesTeamData.SetQuery(new
            {
                nama = textboxFilterUsername.Text.ToString(),
                aktif = comboboxFilterActive.SelectedValue.ToString(),
                kodesales = comboFilterChannel.SelectedValue?.ToString(),
            });
            try
            {
                await _salesTeamData.Refresh();
            }
            catch (Exception ex)
            {
                //ConsoleDump.Extensions.Dump(ex);
                MessageBox.Show(ex.Message);
            }
            buttonFilter.Enabled = true;
        }

        private async Task Delete()
        {
            if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                var rest = new Rest($"mastertimsales/{textboxId.Text}");
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



        private async void SalesTeamControl_Load(object sender, EventArgs e)
        {
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
                try
                {
                    var dataToSend = new
                    {
                        nama = textboxName.Text,
                        kodechannel = comboSalesChannel.SelectedValue,
                        targetjete = textboxJeteTarget.Text,
                        targetomzet = textboxOmzetTarget.Text,
                        tampiltahunlalu = checkboxShowLastYear.Checked,
                        syaratKomisi = checkboxShowLastYear.Checked,
                        aktif = checkboxActive.Checked,

                    };
                    var re = await _salesTeamData.CreateOrUpdate(textboxId.Text, dataToSend);

                    ConsoleDump.Extensions.Dump(re);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                await _salesTeamData.Refresh();
                if (isEdit && dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[selectedRowIndex].Selected = true;
                }
                textboxName.Focus();

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

            var selectedUser = _salesTeamData.GetData().Where(x => x.Kode.ToString() == dataGridView1.SelectedRows[0].Cells[0].Value.ToString()).First();

            textboxId.Text = selectedUser.Kode.ToString();
            textboxName.Text = selectedUser.Nama;
            textboxOmzetTarget.Text = selectedUser.Targetomzet.ToString();
            textboxJeteTarget.Text = selectedUser.Targetjete.ToString();
            checkboxComissionTerms.Checked = selectedUser.SyaratKomisi;
            checkboxShowLastYear.Checked = selectedUser.Tampiltahunlalu;
            checkboxActive.Checked = selectedUser.Aktif;
            comboSalesChannel.SelectedValue = selectedUser.Kodechannel;

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
