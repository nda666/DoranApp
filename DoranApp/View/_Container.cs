using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using DoranApp.View.Pegawai;
using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;

namespace DoranApp.View
{
    public partial class _Container : Form
    {
        public HomeForm _homeForm;
        public bool _runSync = false;
        public bool _stopSync = false;
        private int dragTabIndex;

        public _Container()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab && tabForms == null)
            {
                // Disable Tab key for changing focus between controls
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void tabForms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && tabForms == null)
            {
                // Disable Tab key for switching TabControl pages
                e.Handled = true;
            }
        }

        public T OpenForm<T>() where T : Form, new()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(T))
                {
                    form.WindowState = FormWindowState.Maximized;
                    tabForms.SelectedTab = tabForms.TabPages[form.Text];
                    form.Activate();
                    return (T)form;
                }
            }

            var openForm = new T();
            // Set the Parent Form of the Child window.
            openForm.MdiParent = this;
            openForm.MinimizeBox = false;
            openForm.MaximizeBox = true;
            // Display the new form.
            openForm.Show();
            openForm.WindowState = FormWindowState.Maximized;
            openForm.Activate();
            return openForm;
        }

        private void SetInternetStatusText(string status)
        {
            //if (toolStripStatusLabel1.GetCurrentParent() == null)
            //{
            //    return;
            //}
            //if (toolStripStatusLabel1.GetCurrentParent().InvokeRequired)
            //{
            //    SetStatusInternetCallback d = new SetStatusInternetCallback(SetInternetStatusText);
            //    this.Invoke(d, new object[] { status });
            //}
            //else
            //{
            //    this.toolStripStatusLabel1.Text = status;
            //    toolStripStatusLabel1.Image = status == "Online" ? Resources.Connect : Resources.Disconnect;
            //}
        }

        public async void syncDb()
        {
            if (_runSync || _stopSync)
            {
                return;
            }

            _runSync = true;
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

            try
            {
                // Second provider is using plain old Sql Server provider,
                // relying on triggers and tracking tables to create the sync environment
                var clientProvider = new SqliteSyncProvider($"Data Source={filePath};");
                var localOrchestrator = new LocalOrchestrator(clientProvider);
                var sScopeInfo = await serverOrchestrator.GetScopeInfoAsync();
                var sScopeClientInfo = await localOrchestrator.GetScopeInfoClientAsync();

                await localOrchestrator.ProvisionAsync(sScopeInfo);

                // Creating an agent that will handle all the process
                var agent = new SyncAgent(clientProvider, serverOrchestrator);

                var progress = new Progress<ProgressArgs>(update =>
                {
                    Console.WriteLine("Progress: " + update.ProgressPercentage.ToString());
                });

                // Launch the sync process
                var s1 = await agent.SynchronizeAsync(scopeName: "v2", progress: progress);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            _runSync = false;
            _stopSync = true;
        }

        private void _Container_Load(object sender, EventArgs e)
        {
            var internetAvailability = Utils.InternetAvailabilityService.CheckInternetAvailability();
            internetAvailability.Subscribe(isAvailable =>
            {
                SetInternetStatusText(isAvailable ? "Online" : "Offline");
                if (!isAvailable) syncDb();
            });

            HomeForm homeForm = new HomeForm();
            _homeForm = homeForm;
            homeForm.MdiParent = this;
            homeForm.ControlBox = false;
            homeForm.TopLevel = false;
            homeForm.WindowState = FormWindowState.Maximized;
            homeForm.Show();

            if (string.IsNullOrEmpty(Properties.Settings.Default.AuthToken))
            {
                var login = new Login(_homeForm);

                DialogResult loginModal = login.ShowDialog();
                login.MaximizeBox = true;
                if (loginModal == DialogResult.None)
                {
                    this.Close();
                }
            }
        }

        private void syncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncDatabaseForm newMDIChild = new SyncDatabaseForm();
            // Set the Parent Form of the Child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void _Container_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
                tabForms.Visible = false;
            // If no any child form, hide tabControl
            else
            {
                this.ActiveMdiChild.WindowState =
                    FormWindowState.Maximized;
                // Child form always maximized

                // If child form is new and no has tabPage,
                // create new tabPage
                if (this.ActiveMdiChild.Tag == null)
                {
                    // Add a tabPage to tabControl with child
                    // form caption
                    TabPage tp = new TabPage(this.ActiveMdiChild.Text);
                    tp.Name = this.ActiveMdiChild.Text;
                    tp.Tag = this.ActiveMdiChild;
                    tp.Parent = tabForms;
                    tabForms.SelectedTab = tp;

                    this.ActiveMdiChild.Tag = tp;
                    this.ActiveMdiChild.FormClosed +=
                        new FormClosedEventHandler(
                            _Container_FormClosed);
                }

                if (!tabForms.Visible) tabForms.Visible = true;
            }
        }

        private void _Container_FormClosed(object sender, FormClosedEventArgs e)
        {
            var tabPage = ((sender as Form).Tag as TabPage);
            if (tabPage != null)
            {
                tabPage.Dispose();
            }
        }

        private void tabForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((tabForms.SelectedTab != null) &&
                (tabForms.SelectedTab.Tag != null))
                (tabForms.SelectedTab.Tag as Form).Select();
        }

        private void _Container_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tabForms_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TabControl tabControl = (TabControl)sender;
                dragTabIndex = tabControl.SelectedIndex;
                tabControl.DoDragDrop(dragTabIndex, DragDropEffects.Move);
            }
        }

        private void tabForms_DragOver(object sender, DragEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            Point clientPoint = tabControl.PointToClient(new Point(e.X, e.Y));
            int hoverTabIndex = GetHoverTabIndex(tabControl, clientPoint);
            if (hoverTabIndex != dragTabIndex)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void tabForms_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void tabForms_DragDrop(object sender, DragEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            tabControl.SelectedIndex = dragTabIndex;
            Point clientPoint = tabControl.PointToClient(new Point(e.X, e.Y));
            int hoverTabIndex = GetHoverTabIndex(tabControl, clientPoint);
            if (hoverTabIndex != dragTabIndex)
            {
                TabPage tabPage = tabControl.TabPages[dragTabIndex];
                tabControl.TabPages.RemoveAt(dragTabIndex);
                tabControl.TabPages.Insert(hoverTabIndex, tabPage);
                tabControl.SelectedIndex = hoverTabIndex;
            }
        }

        private int GetHoverTabIndex(TabControl tabControl, Point clientPoint)
        {
            for (int i = 0; i < tabControl.TabCount; i++)
            {
                if (tabControl.GetTabRect(i).Contains(clientPoint))
                {
                    return i;
                }
            }

            return dragTabIndex;
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<RolesForm>();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<UserForm>();
        }

        private void syncDBManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<SyncDatabaseForm>();
        }

        private void tesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<UserForm>();
        }

        private void roleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<RolesForm>();
        }

        private void salesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm<SalesForm>();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            if (Utils.InternetAvailabilityService.forceStatus.HasValue)
            {
                Utils.InternetAvailabilityService.forceStatus = null;
            }
            else
            {
                Utils.InternetAvailabilityService.forceStatus = !Utils.InternetAvailabilityService.isOnline;
            }
        }

        private void brandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<HKategoribarangForm>();
        }

        private void pegawaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<PegawaiForm>();
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<SalesForm>();
        }

        private void gudangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<MasterGudangForm>();
        }

        private void brandToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm<HKategoribarangForm>();
        }

        private void laporanTransaksiPenjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<LaporanTransaksiPenjualan>();
        }

        private void penjualanByBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<LaporanPenjualanBarangByBarang>();
        }

        private void penjualanByTokoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<LaporanPenjualanBarangByToko>();
        }

        private void penjualanBySalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<LaporanPenjualanBarangBySales>();
        }


        private void tabForms_DrawItem(object sender, DrawItemEventArgs e)
        {
        }

        private void penyiapOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm<PenyiaporderForm>();
        }

        private delegate void SetStatusInternetCallback(string text);
    }

    public class MdiChildForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~0x00020000; // WS_MAXIMIZEBOX
                return cp;
            }
        }
    }
}