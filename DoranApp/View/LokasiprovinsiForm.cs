using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Data;
using DoranApp.DataGlobal;
using DoranApp.Utils;

namespace DoranApp.View;

public partial class LokasiprovinsiForm : Form
{
    private LokasiprovinsiData _lokasiprovinsiData = new LokasiprovinsiData();

    public LokasiprovinsiForm()
    {
        InitializeComponent();
    }

    private DataTable _dataTable { get; set; }

    private async Task fetchLokasiProvinsi()
    {
        await _lokasiprovinsiData.Refresh();
    }

    private async void LokasiprovinsiForm_Load(object sender, EventArgs e)
    {
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.EnableDoubleBuffered(true);
        _dataTable = _lokasiprovinsiData.GetDataTable();
        dataGridView1.DataSource = _dataTable;
        try
        {
            await fetchLokasiProvinsi();
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(ex);
        }
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(textBoxNama.Text.Trim()))
        {
            MessageBox.Show("Nama harus di isi");
            return;
        }

        if (DialogResult.Yes == MessageBox.Show("Apakah Anda yakin ingin menyimpan data ini?", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
        {
            ButtonToggleHelper.DisableButtonsByTag(this, "actionButton");
            var selectedRowIndex = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : 0;

            var dataToSend = new
            {
                nama = textBoxNama.Text.Trim(),
            };
            var success = false;
            try
            {
                await _lokasiprovinsiData.CreateOrUpdate(null, dataToSend);
                textBoxNama.Clear();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            await _lokasiprovinsiData.Refresh();
            textBoxNama.Focus();
            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
            if (success)
            {
                MessageBox.Show("Lokasi provinsi berhasil ditambah");
                FetchLokasiProvinsiOption.Run();
            }
        }
    }
}