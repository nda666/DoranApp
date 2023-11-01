using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ConsoleDump;
using Microsoft.Reporting.WinForms;

namespace DoranApp.Utils
{
    public class DirectPrint : IDisposable
    {
        private int m_currentPageIndex;
        private List<Stream> m_streams;

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType,
            bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        public DirectPrint Export(LocalReport report)
        {
            string deviceInfo = @"
                <DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>21cm</PageWidth>
                    <PageHeight>14cm</PageHeight>
                    <MarginTop>0in</MarginTop>
                    <MarginLeft>0in</MarginLeft>
                    <MarginRight>0in</MarginRight>
                    <MarginBottom>0in</MarginBottom>
                </DeviceInfo>";

            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out _);

            foreach (Stream stream in m_streams)
                stream.Position = 0;

            return this;
        }

        private void OnEndPrint(object sender, PrintEventArgs e)
        {
            foreach (Stream stream in m_streams)
                stream.Position = 0;
            m_currentPageIndex = 0; // Reset the page index when printing is complete
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            m_streams?.Dump();
            if (m_currentPageIndex >= m_streams.Count)
            {
                ev.HasMorePages = false;
                return;
            }

            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void ShowPreview(string paperSizeName, int? width = null, int? height = null)
        {
            using (var printDoc = CreatePrintDocument(paperSizeName, width, height))
            {
                var printPreview = new PrintPreviewDialog();
                m_currentPageIndex = 0;
                printPreview.Document = printDoc;
                m_currentPageIndex = 0;
                printPreview.TopMost = true;

                Screen screen = Screen.PrimaryScreen;
                Rectangle screenBounds = screen.Bounds;

                // Calculate the new window size
                int newWidth = (int)Math.Ceiling(screenBounds.Width / 1.4);
                int newHeight = (int)Math.Ceiling(screenBounds.Height / 1.4);

                printPreview.Size = new Size(newWidth, newHeight);
                printPreview.StartPosition = FormStartPosition.CenterScreen;
                printPreview.MinimizeBox = false;
                printPreview.ShowDialog();
            }
        }

        public void Print(string paperSizeName, int? width = null, int? height = null)
        {
            using (var printDoc = CreatePrintDocument(paperSizeName, width, height))
            {
                m_currentPageIndex = 0;
                try
                {
                    printDoc.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        protected PrintDocument CreatePrintDocument(string paperSizeName, int? width = null, int? height = null)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");

            PrintDocument printDoc = new PrintDocument();

            PaperSize paperSize = null;
            foreach (PaperSize size in printDoc.PrinterSettings.PaperSizes)
            {
                if (size.PaperName.Equals(paperSizeName, StringComparison.OrdinalIgnoreCase))
                {
                    paperSize = size;
                    break;
                }
            }

            if (paperSize != null)
            {
                printDoc.DefaultPageSettings.PaperSize = paperSize;
            }
            else if (width != null && height != null)
            {
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("CustomSize", width.Value, height.Value);
            }
            else
            {
                throw new Exception("Error: specified paper size not found, and custom size not provided.");
            }

            Margins margins = new Margins(0, 0, 0, 0);
            printDoc.DefaultPageSettings.Margins = margins;

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }

            printDoc.EndPrint += new PrintEventHandler(OnEndPrint);
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            return printDoc;
        }
    }
}