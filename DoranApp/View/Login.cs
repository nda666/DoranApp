using DoranApp.Exceptions;
using DoranApp.Utils;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace DoranApp.View
{
    public partial class Login : Form
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            login();
        }

        public async void login()
        {

            Rest rest = new Rest("/login");
            try
            {
                var response = await rest.Post(new
                {
                    username = textBox1.Text,
                    password = textBox2.Text
                });

                var result = response.Response;

                Properties.Settings.Default.AuthToken = $"BEARER {result.api_token}";
                Console.WriteLine($"Auth TOken: { Properties.Settings.Default.AuthToken}");
                this.Hide();

            }
            catch (RestException error)
            {
                if (error.ErrorCode == 401)
                {
                    MessageBox.Show($"Username / password tidak cocok", $"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"{error.ErrorCode} {error.StatusText}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            IntPtr iconHandle = ExtractIcon(IntPtr.Zero, "shell32.dll", 316);

            if (iconHandle != IntPtr.Zero)
            {
                Icon icon = Icon.FromHandle(iconHandle);
                Bitmap bitmap = new Bitmap(16, 16);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.DrawIcon(icon, new Rectangle(Point.Empty, bitmap.Size));
                }
                // Display the icon in a PictureBox
                linkLabel1.Image = bitmap;
              

                // Don't forget to release the icon handle
                DestroyIcon(iconHandle);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr hIcon);

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            linkLabel1.Image?.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
        }
    }
}
