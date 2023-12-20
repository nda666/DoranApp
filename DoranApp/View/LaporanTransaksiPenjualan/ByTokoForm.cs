using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleDump;
using DoranApp.Utils;
using OfficeOpenXml;

namespace DoranApp.View.LaporanTransaksiPenjualan;

public partial class ByTokoForm : Form
{
    public delegate Task<List<GetLaporanTransaksiPenjualanGroupTokoDto>> LaporanTransaksiDelegate();

    private Client _Client = new Client();

    private BindingList<GetLaporanTransaksiPenjualanGroupTokoDto> _data =
        new BindingList<GetLaporanTransaksiPenjualanGroupTokoDto>();

    private DateTime Date1 = new DateTime();
    private DateTime Date2 = new DateTime();

    private LaporanTransaksiDelegate RefreshData;
    private ICollection<Sales> Sales = new List<Sales>();

    public ByTokoForm(List<GetLaporanTransaksiPenjualanGroupTokoDto> data, DateTime date1, DateTime date2,
        LaporanTransaksiDelegate laporanTransaksiDelegate)
    {
        InitializeComponent();
        Date1 = date1;
        Date2 = date2;

        _data = new BindingList<GetLaporanTransaksiPenjualanGroupTokoDto>(data);
        dataGridView1.EnableDoubleBuffered(true);
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.DataSource = _data;
        RefreshData = new LaporanTransaksiDelegate(laporanTransaksiDelegate);
    }

    private void ByTokoForm_Load(object sender, EventArgs e)
    {
        FetchSalesThatHadEmail();
        var akses = Session.GetUser()?.Akses?.ToLower();
        if (akses == "bos" ||
            akses == "masteradmin" ||
            akses == "masteradmin2" ||
            akses == "mastersuperadmin" ||
            akses == "seketaris")
        {
            PANEL_Footer.Visible = true;
        }

        if (akses == "bos")
        {
            BTN_SetSales.Enabled = true;
            dataGridView1.Columns[6].Visible = true;
            dataGridView1.Columns[7].Visible = true;
        }
    }

    private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {
        Helper.CreateDatagridviewRowNumber(dataGridView1, this.Font, e);
    }

    private void ByTokoForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            Close();
        }
    }

    private async Task FetchSalesThatHadEmail()
    {
        comboSales.ValueMember = "Kode";
        comboSales.DisplayMember = "Nama";
        Sales = await _Client.Get_Sales_That_Had_EmailAsync();
        comboSales.DataSource = Sales.Select(e => new CommonResultDto
        {
            Kode = e.Kode,
            Nama = e.Nama
        }).Prepend(new CommonResultDto
        {
            Kode = null,
            Nama = "Semua Sales"
        }).ToList();
    }

    private void ByTokoForm_FormClosing(object sender, FormClosingEventArgs e)
    {
    }

    // Function to export DataGridView to Excel
    private MemoryStream ExportToExcel(DataGridView dataGridView)
    {
        MemoryStream memoryStream = new MemoryStream();

        // Create a new Excel package for Excel 97-2003 format
        using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
        {
            // Create a new worksheet
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            // Set headers for specific columns
            string[] headers = { "No", "Nama", "Jumlahnya" };
            if (checkBoxCetakEmail.Checked)
            {
                headers = headers.Append("Email").ToArray();
            }

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
            }

            // Set cell values for the specific columns
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = (i + 1).ToString(); // Incrementing number in No column
                worksheet.Cells[i + 2, 2].Value = dataGridView.Rows[i].Cells["Nama"].Value?.ToString(); // Nama column
                if (dataGridView.Rows[i].Cells["Jumlahnya"].Value != null &&
                    double.TryParse(dataGridView.Rows[i].Cells["Jumlahnya"].Value.ToString(), out double jumlah))
                {
                    worksheet.Cells[i + 2, 3].Value = jumlah; // Set the numeric value in the cell
                }

                if (checkBoxCetakEmail.Checked)
                {
                    worksheet.Cells[i + 2, 4].Value =
                        dataGridView.Rows[i].Cells["Email"].Value?.ToString(); // Email column
                }

                worksheet.Cells[i + 2, 3].Style.Numberformat.Format =
                    "#,##0"; // Apply thousand separator without decimals

                if (checkBoxWarnaiTokoBlok.Checked)
                {
                    worksheet.Cells[i + 2, 1, i + 2, worksheet.Dimension.End.Column].Style.Fill.PatternType =
                        OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[i + 2, 1, i + 2, worksheet.Dimension.End.Column].Style.Fill.BackgroundColor
                        .SetColor(System.Drawing.Color.Red);
                }
            }

            // Auto-fit columns for better visibility
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Save the Excel package (no need to explicitly save to a file)
            excelPackage.Save();
        }

        return memoryStream;
    }


    private void button1_Click(object sender, EventArgs e)
    {
        MemoryStream excelMemoryStream = ExportToExcel(dataGridView1);

        // Generate timestamp for the file name
        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

        // Save the Excel file to disk and open it
        if (excelMemoryStream != null)
        {
            using (excelMemoryStream)
            {
                string fileName = $"Export_{timestamp}.xlsx";
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    excelMemoryStream.WriteTo(fileStream);
                }
            }

            System.Diagnostics.Process.Start($"Export_{timestamp}.xlsx");
        }
    }

    private async void button2_Click(object sender, EventArgs e)
    {
        List<int> salesIdList = new List<int>();
        if (comboSales.SelectedValue == null)
        {
            var confirmResult = MessageBox.Show("Yakin Kirim ke Semua Sales?", "Confirmation", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            salesIdList = Sales.Where(e => e.Jenis).Select(e => e.Kode).ToList();
        }
        else
        {
            if (comboSales.SelectedValue != null &&
                int.TryParse(comboSales.SelectedValue.ToString(), out int selectedValue))
            {
                salesIdList = new List<int> { selectedValue };
            }
        }

        if (salesIdList.Count == 0)
        {
            MessageBox.Show("Pilih sales terlebih dahulu");
        }

        try
        {
            var omzetBelumLunas = await _Client.Omzet_Belum_Lunas_By_SalesAsync(
                dateMin: Date1,
                dateMax: Date2,
                kodeSales: salesIdList.ToArray()
            );
            var listStr = new List<string>();
            var sendedMessage = false;
            omzetBelumLunas?.Dump();
            foreach (var omzet in omzetBelumLunas)
            {
                var sales = Sales.Where(e => e.Kode == omzet.Kodesales).FirstOrDefault();
                if (sales == null)
                {
                    continue;
                }

                var subject = $"TARGET Tagihan yang BELUM LUNAS SALES {sales.Nama}";
                var message = $"Kepada {sales.Nama}\r\n";
                message += "TARGET Tagihan yang BELUM LUNAS :\r\n";
                message += $"{omzet.JumlahNya.ToString("N0")}\r\n";
                message += "Harap Segera Dikejar Tagihannya. Terima Kasih.";
                listStr.Add(message);
                string recipientEmail = "adhabakhtiar@gmail.com"; // Replace with recipient's email
                bool isEmailSent = EmailClient.SendEmail(recipientEmail, subject, message);

                if (isEmailSent)
                {
                    sendedMessage = true;
                }
            }


            MessageBox.Show(sendedMessage ? "Email berhasil dikirim" : "Email gagal dikirim");
            listStr.Dump();
        }
        catch (Exception ex)
        {
            ex.Dump();
        }
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 1)
        {
            buttonBlok.Enabled = true;
            buttonAturPelg.Enabled = true;

            var index = dataGridView1.SelectedRows[0].Index;
            var isBlok = dataGridView1.Rows[index].Cells["Blok"].Value.ToString() == "1";


            buttonBlok.Text = isBlok ? "UNBLOK" : "BLOK";
        }
        else
        {
            buttonBlok.Enabled = false;
            buttonAturPelg.Enabled = false;
        }
    }

    private async void buttonBlok_Click(object sender, EventArgs e)
    {
        if (dataGridView1.SelectedRows.Count == 1)
        {
            var index = dataGridView1.SelectedRows[0].Index;
            var isBlok = dataGridView1.Rows[index].Cells["Blok"].Value.ToString() == "1";
            if (!int.TryParse(dataGridView1.Rows[index].Cells["KodePelanggan"].Value.ToString(), out int cellValue))
            {
                MessageBox.Show("[CLIENT] Kode pelanggan tidak ditemukan");
                return;
            }

            if (isBlok)
            {
                await _Client.Un_Blok_MasterpelangganAsync(cellValue);
            }
            else
            {
                await _Client.Blok_MasterpelangganAsync(cellValue);
            }

            var newData = await RefreshData.Invoke();
            _data = new BindingList<GetLaporanTransaksiPenjualanGroupTokoDto>(newData);
            var selection = dataGridView1.SelectedRows[0].Index;
            dataGridView1.DataSource = _data;
            dataGridView1.Rows[selection].Selected = true;
        }
    }

    private void buttonAturPelg_Click(object sender, EventArgs e)
    {
        // TODO: kerjakan ini
    }
}