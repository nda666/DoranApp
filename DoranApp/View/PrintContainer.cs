using System;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace DoranApp.View
{
    public partial class PrintContainer : Form
    {
        public PrintContainer(dynamic htrans)
        {
            InitializeComponent();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.ReportEmbeddedResource = "DoranApp.NotaTransaksi.rdlc"; // Path to your RDLC file

            // Set parameters (if any)
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("tglTrans", htrans.tglTrans.ToString());
            reportViewer.LocalReport.SetParameters(parameters);

            // Refresh the report
            reportViewer.RefreshReport();
            reportViewer.PrintDialog();

            // Show the report viewer in your form
            reportViewer.Dock = DockStyle.Fill;
            this.Controls.Add(reportViewer);
        }

        private void PrintContainer_Load(object sender, EventArgs e)
        {
        }
    }
}