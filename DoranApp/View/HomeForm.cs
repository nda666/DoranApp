﻿using System;
using System.Windows.Forms;
using DoranApp.Data;

namespace DoranApp.View
{
    public partial class HomeForm : Form
    {
        public bool homeStart = false;
        public long timer = 8 * 60 * 60; // 8 hours

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


        private void button3_Click(object sender, EventArgs e)
        {
            ((_Container)MdiParent)._stopSync = false;
            ((_Container)MdiParent)._runSync = false;
            ((_Container)MdiParent).syncDb();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<TransaksiForm>();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<OrderInputForm>();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!homeStart)
            {
                return;
            }

            timer--;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
            var countDown = timeSpan.ToString(@"hh\:mm\:ss");
            //label3.Text = $"WAKTU ANDA TERSISA:\n{countDown}";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<Transit.TransitForm>();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ((_Container)MdiParent).OpenForm<CekStok.CekStokForm>();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var user = new MasteruserData();
            await user.UpdateUserSession();
        }
    }
}