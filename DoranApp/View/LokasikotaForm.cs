using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleDump;
using DoranApp.Data;
using DoranApp.DataGlobal;
using DoranApp.Utils;

namespace DoranApp.View;

public partial class LokasikotaForm : Form
{
    private Client _Client = new Client();

    private Dictionary<string, IDisposable> _disposable = new Dictionary<string, IDisposable>();
    private LokasikotaData _lokasikotaData = new LokasikotaData();

    public LokasikotaForm()
    {
        InitializeComponent();
    }

    private DataTable _dataTable { get; set; }

    private async void LokasikotaForm_Load(object sender, EventArgs e)
    {
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.EnableDoubleBuffered(true);
        _dataTable = _lokasikotaData.GetDataTable();
        dataGridView1.DataSource = _dataTable;
        fetchCoa4();
        subscribeProvinsi();
        fetchLokasiKota();
    }

    private async Task fetchCoa4()
    {
        var data = await _Client.Get_Coa4_OptionsAsync(kodecoa3: 120);
        comboBox2.DataSource = data.Select(e => new Coa4OptionDto
        {
            Kode = e.Kode,
            Nama = $"{e.Nama} {e.Kode}"
        }).ToList();
    }

    private async Task fetchLokasiKota()
    {
        try
        {
            await _lokasikotaData.Refresh();
        }
        catch (Exception ex)
        {
            ex.Dump();
            Helper.ShowErrorMessage(ex);
        }
    }

    private async Task subscribeProvinsi()
    {
        var data = FetchLokasiProvinsiOption.Subscribe(list =>
        {
            comboBox1.DataSource = list.OrderBy(e => e.Nama).ToList();
        });

        _disposable.Add("provinsi", data);
        await FetchLokasiProvinsiOption.Run();
    }

    private void LokasikotaForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        throw new System.NotImplementedException();
    }


    private void LokasikotaForm_FormClosing_1(object sender, FormClosingEventArgs e)
    {
        foreach (var x in _disposable)
        {
            x.Value.Dispose();
        }
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(textBoxNama.Text.Trim()))
        {
            MessageBox.Show("Nama harus di isi");
            return;
        }

        if (comboBox1.SelectedValue == null)
        {
            MessageBox.Show("Provinsi harus di isi");
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
                provisi = comboBox1.SelectedValue,
            };
            var success = false;
            try
            {
                await _lokasikotaData.CreateOrUpdate(null, dataToSend);
                textBoxNama.Clear();
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            await _lokasikotaData.Refresh();
            textBoxNama.Focus();
            ButtonToggleHelper.EnableButtonsByTag(this, "actionButton");
            if (success)
            {
                MessageBox.Show("Lokasi provinsi berhasil ditambah");
                FetchLokasiKota.Run();
            }
        }
    }

    private async void button2_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count != 1)
        {
            return;
        }

        if (comboBox2.SelectedValue == null)
        {
            MessageBox.Show("Kode COA harus di isi");
            return;
        }

        var rowData = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index];
        button2.Enabled = false;
        try
        {
            var res = await _Client.Set_Coa_Lokasi_KotaAsync((int)rowData.Cells[0].Value, new SetCoaLokasiKota
            {
                KodeCoa4 = (int)comboBox2.SelectedValue
            });
            button2.Enabled = true;
        }
        catch (ApiException ex)
        {
            button2.Enabled = true;
            Helper.ShowErrorMessageFromResponse(ex);
            return;
        }

        fetchLokasiKota();
    }

    private void LokasikotaForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            this.Close();
        }
    }
}