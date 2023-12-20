using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DoranApp.Utils;
using OfficeOpenXml;

namespace DoranApp.View.LaporanTransaksiPenjualan;

public partial class ByProvinsiForm : Form
{
    public List<LaporanTransaksiPenjualanGroupProvinsiDto>
        _data = new List<LaporanTransaksiPenjualanGroupProvinsiDto>();

    private double Total = 0;

    public ByProvinsiForm(List<LaporanTransaksiPenjualanGroupProvinsiDto> data)
    {
        InitializeComponent();
        _data = data;
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.DataSource = _data;
        Total = 0;
        foreach (DataGridViewRow row in dataGridView1.Rows)
        {
            Total += Convert.ToDouble(row.Cells["Jumlah"].Value);
        }
    }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Persen")
        {
            int columnIndexForTotal = 1; // Replace with your column index for total calculation

            if (dataGridView1.Rows.Count > 0)
            {
                double cellValue = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells["Jumlah"].Value);

                // Calculate and set the percentage value
                double percentage = (cellValue / Total) * 100;
                e.Value = $"{percentage:F2}"; // Format the value as percentage with two decimal places
            }
        }
    }

    private void ByProvinsiForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            this.Close();
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        button1.Enabled = false;
        MemoryStream memoryStream = new MemoryStream();

        using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
        {
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

            // Adding headers from the DataGridView
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = dataGridView1.Columns[i].HeaderText;
            }

            // Adding data from DataGridView to Excel starting from the second row
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Columns[j].Name == "Persen")
                    {
                        double cellValue = Convert.ToDouble(dataGridView1.Rows[i].Cells["Jumlah"].Value);
                        double percentage = (cellValue / Total) * 100;
                        worksheet.Cells[i + 2, j + 1].Value = percentage;
                    }
                    else
                    {
                        worksheet.Cells[i + 2, j + 1].Value =
                            dataGridView1.Rows[i].Cells[j].Value; // For other columns, directly copy the value
                    }
                }
            }

            excelPackage.Save(); // Save the changes to the MemoryStream
        }


        if (memoryStream != null)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            using (memoryStream)
            {
                ;
                string fileName = $"Export_{timestamp}.xlsx";
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fileStream);
                }
            }

            System.Diagnostics.Process.Start($"Export_{timestamp}.xlsx");
        }

        button1.Enabled = true;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}