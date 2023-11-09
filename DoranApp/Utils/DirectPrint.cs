using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace DoranApp.Utils
{
    public class DirectPrint : IDisposable
    {
        private int m_currentPageIndex;
        private List<Stream> m_streams;
        private int? PaperHeight = null;
        private string PaperSizeName;
        private int PaperWidth;
        private PrintDocument PrintDoc;

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

        public DirectPrint Export(LocalReport report, string paperSizeName, int paperWidth, int? paperHeight = null)
        {
            PaperSizeName = paperSizeName;
            PaperWidth = paperWidth;
            PaperHeight = paperHeight;
            var paperHeightString = "";
            if (paperHeight != null)
            {
                paperHeightString = $"<PageHeight>{paperHeight}cm</PageHeight>";
            }

            string deviceInfo = @$"
                <DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>{paperWidth}cm</PageWidth>
                    {paperHeightString}
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
            if (m_currentPageIndex >= m_streams.Count)
            {
                ev.HasMorePages = false;
                return;
            }

            PrintDoc.OriginAtMargins = true;
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            Rectangle adjustedRect = new Rectangle(
                ev.MarginBounds.Left, ev.MarginBounds.Top, ev.MarginBounds.Width, ev.MarginBounds.Height
            );

            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void ShowPreview()
        {
            using (var printDoc = CreatePrintDocument())
            {
                var printPreview = new PrintPreviewDialog();
                m_currentPageIndex = 0;
                printPreview.Document = printDoc;
                m_currentPageIndex = 0;

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

        public void Print()
        {
            using (var printDoc = CreatePrintDocument())
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

        protected PrintDocument CreatePrintDocument()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");

            PrintDoc = new PrintDocument();

            PaperSize paperSize = null;
            foreach (PaperSize size in PrintDoc.PrinterSettings.PaperSizes)
            {
                if (size.PaperName.Equals(PaperSizeName, StringComparison.OrdinalIgnoreCase))
                {
                    paperSize = size;
                    break;
                }
            }

            if (paperSize != null)
            {
                PrintDoc.DefaultPageSettings.PaperSize = paperSize;
            }
            else if (PaperWidth != null && PaperHeight != null)
            {
                var width = (int)(PaperWidth * 100 / 2.54);
                var height = (int)(PaperHeight * 100 / 2.54);
                PrintDoc.DefaultPageSettings.PaperSize = new PaperSize(PaperSizeName, width, height);
            }
            else
            {
                throw new Exception("Error: specified paper size not found, and custom size not provided.");
            }

            Margins margins = new Margins(0, 0, 0, 0);
            PrintDoc.DefaultPageSettings.Margins = margins;

            if (!PrintDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }

            PrintDoc.EndPrint += new PrintEventHandler(OnEndPrint);
            PrintDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            return PrintDoc;
        }
    }
}