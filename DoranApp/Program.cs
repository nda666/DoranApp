using System;
using System.Windows.Forms;
using DoranApp.View;
using OfficeOpenXml;

namespace DoranApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new _Container());
            //Application.Run(new SyncDatabaseForm());
        }
    }
}