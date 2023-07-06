using DoranApp.Exceptions;
using DoranApp.Utils;
using System;
using System.Windows.Forms;


namespace DoranApp.View
{
    public partial class Login : Form
    {
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
                MessageBox.Show($"{error.Message}", $"{ error.ErrorCode } {error.StatusText}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception error)
            {
                MessageBox.Show($"{error.Message} {error.StackTrace}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
        }

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
    }
}
