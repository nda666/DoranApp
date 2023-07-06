﻿using System;
using System.Windows.Forms;

namespace DoranApp.View
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<UserForm>();
        }

        private void HomeForm_Resize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<RolesForm>();
        }
    }
}
