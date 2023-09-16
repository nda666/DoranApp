using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace DoranApp.Utils
{
    public class BarcodeGenerator
    {
        public static string GenerateBase64Barcode(string content, BarcodeFormat format, int width, int height)
        {
            var barcodeBitmap = RunGenerate(content, format, width, height);
            using (MemoryStream ms = new MemoryStream())
            {
                barcodeBitmap.Save(ms, ImageFormat.Png); // Save as PNG or the appropriate format
                byte[] byteImage = ms.ToArray();
                return Convert.ToBase64String(byteImage);
            }
        }
        public static Bitmap GenerateBarcode(string content, BarcodeFormat format, int width, int height)
        {
            return RunGenerate(content, format, width, height);
        }

        private static Bitmap RunGenerate(string content, BarcodeFormat format, int width, int height)
        {
            var barcodeWriter = new BarcodeWriterPixelData
            {
                Format = format,
                Options = new EncodingOptions
                {
                    Width = width,
                    Height = height
                }
            };

            PixelData pixelData = barcodeWriter.Write(content);

            Bitmap barcodeBitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            BitmapData bmpData = barcodeBitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, barcodeBitmap.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            Marshal.Copy(pixelData.Pixels, 0, ptr, pixelData.Pixels.Length);
            barcodeBitmap.UnlockBits(bmpData);

            return barcodeBitmap;
        }
    }
}
