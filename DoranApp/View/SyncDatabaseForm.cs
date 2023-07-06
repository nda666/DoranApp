using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class SyncDatabaseForm : Form
    {
        public SyncDatabaseForm()
        {
            InitializeComponent();
        }

        private void SyncDatabaseForm_Load(object sender, EventArgs e)
        {

        }



        private async void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            var serverOrchestrator = new WebRemoteOrchestrator("https://localhost:44376/api/sync");
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Doran Office";
            var filePath = appDataPath + "\\data.db";
            Console.WriteLine($"Path: {filePath}");
            if (!System.IO.Directory.Exists(appDataPath))
            {
                System.IO.Directory.CreateDirectory(appDataPath);
            }

            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine("Just entered to create Sync DB");
                SQLiteConnection.CreateFile(filePath);
            }
            // Second provider is using plain old Sql Server provider,
            // relying on triggers and tracking tables to create the sync environment
            var clientProvider = new SqliteSyncProvider($"Data Source={filePath};");

            // Creating an agent that will handle all the process
            var agent = new SyncAgent(clientProvider, serverOrchestrator);
            progressBar1.Maximum = 100;
            await agent.LocalOrchestrator.UpdateUntrackedRowsAsync();

            var progress = new Progress<ProgressArgs>(update =>
            {
                progressBar1.Value = (int)Math.Round(update.ProgressPercentage * 100);
                Console.WriteLine("Progress: " + update.ProgressPercentage.ToString());
            });

            // Launch the sync process
            var s1 = await agent.SynchronizeAsync(progress);

            richTextBox1.Text = s1.ToString();

        }
    }
}
