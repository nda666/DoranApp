﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Windows.Forms;
using DoranApp.Exceptions;
using DoranApp.Utils;
using Newtonsoft.Json;

namespace DoranApp
{
    public class Helper
    {
        public static void ShowError(String message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void ShowErrorMessageFromResponse(RestException apiException)
        {
            MessageBox.Show(apiException.Message, "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void ShowErrorMessageFromResponse(ApiException apiException)
        {
            try
            {
                dynamic resp = JsonConvert.DeserializeObject<ExpandoObject>(apiException.Response);
                var hadMessage = ((IDictionary<string, object>)resp).ContainsKey("message");
                MessageBox.Show(hadMessage ? resp.message : apiException.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(apiException.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public static string GetSelectedRadioButtonTag(GroupBox groupBox)
        {
            foreach (Control control in groupBox.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    return radioButton.Tag?.ToString();
                }
            }

            return null; // No radio button selected
        }

        public static string TipeTempoToString(sbyte tipeTempo)
        {
            switch (tipeTempo)
            {
                case > 0:
                    return $"{tipeTempo.ToString()} Hari";
                default:
                    return "Cash";
            }
        }

        public static int TipeTempoIndexToValue(int indexTempo)
        {
            // Cash
            // 1 Minggu
            // 2 Minggu
            // 1 Bulan
            // 2 Bulan
            var subDay = 0;
            switch (indexTempo)
            {
                case 0:
                    subDay = 0;
                    break;
                case 1:
                    subDay = 7;
                    break;
                case 2:
                    subDay = 14;
                    break;
                case 3:
                    subDay = 30;
                    break;
                case 4:
                    subDay = 60;
                    break;
                default:
                    subDay = 0;
                    break;
            }

            return subDay;
        }

        public static int TipeTempoValueToIndex(int tipeTempo)
        {
            var subDay = -1;
            switch (tipeTempo)
            {
                case 0:
                    subDay = 0;
                    break;
                case 7:
                    subDay = 1;
                    break;
                case 14:
                    subDay = 2;
                    break;
                case 30:
                    subDay = 3;
                    break;
                case 60:
                    subDay = 4;
                    break;
                default:
                    break;
            }

            return subDay;
        }

        public static string GetKeyLimitTagihan()
        {
            string sHasil = "";
            Random random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                int hasil = random.Next(10) + 65;
                char charNya = Convert.ToChar(hasil);
                sHasil += charNya;
            }

            return sHasil;
        }

        public static string DekripKeyLimitTagihan(string asal)
        {
            string sHasil = "";

            for (int i = 0; i < asal.Length; i++)
            {
                int hasil = (int)asal[i];
                hasil += 5;
                char charNya = (char)hasil;
                sHasil += charNya;
            }

            return sHasil;
        }

        public static void CreateDatagridviewRowNumber(DataGridView dgv, Font font, DataGridViewRowPostPaintEventArgs e)
        {
            string rowNumber = (e.RowIndex + 1).ToString();

            // Mengambil ukuran teks nomor urut
            SizeF size = e.Graphics.MeasureString(rowNumber, font);

            // Mendapatkan koordinat X dan Y untuk menampilkan nomor urut di row header
            float x = (float)(e.RowBounds.Location.X + dgv.RowHeadersWidth - size.Width - 5);
            float y = (float)(e.RowBounds.Location.Y + (e.RowBounds.Height - size.Height) / 2);

            // Menggambar nomor urut di row header
            e.Graphics.DrawString(rowNumber, font, Brushes.Black, x, y);
        }
    }
}