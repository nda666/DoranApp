using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DoranApp.Utils;

namespace DoranApp.View
{
    public partial class LoginForm : Form
    {
        private HomeForm _homeForm;

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(HomeForm homeForm)
        {
            _homeForm = homeForm;
            InitializeComponent();
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            login();
        }

        public async void login()
        {
            try
            {
                var client = new Client();
                var response = await client.Post_LoginAsync(new LoginDto
                {
                    Username = textBox1.Text,
                    Password = textBox2.Text
                });
                Session.SetUser(response.Masteruser);
                Properties.Settings.Default.AuthToken = $"BEARER {response.ApiToken}";
                _homeForm.label2.Text = $"Hello {response.Masteruser.Usernameku}, Semangat Selalu!!";

                Console.WriteLine($"Auth TOken: {Properties.Settings.Default.AuthToken}");
                Hide();
                if (_homeForm != null)
                {
                    _homeForm.homeStart = true;
                }
            }
            catch (ApiException error)
            {
                if (error.StatusCode == 401)
                {
                    MessageBox.Show(this, $"Username / password tidak cocok", $"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(this, $"{error.StatusCode} {error.Message}", "Error!", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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

#if DEBUG
            textBox1.Text = "JT";
            textBox2.Text = "a";
#endif
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