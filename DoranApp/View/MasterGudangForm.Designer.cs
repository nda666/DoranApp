namespace DoranApp.View
{
    partial class MasterGudangForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxNama = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioTidakAktif = new System.Windows.Forms.RadioButton();
            this.radioAktif = new System.Windows.Forms.RadioButton();
            this.radioAktifSemua = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioTransitTidak = new System.Windows.Forms.RadioButton();
            this.radioTransitIya = new System.Windows.Forms.RadioButton();
            this.radioTransitSemua = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioUrut = new System.Windows.Forms.RadioButton();
            this.radioUrutNama = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonUbah = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.textboxUrut = new DoranApp.Components.NumericTextbox(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxNama
            // 
            this.textBoxNama.Location = new System.Drawing.Point(57, 12);
            this.textBoxNama.Name = "textBoxNama";
            this.textBoxNama.Size = new System.Drawing.Size(192, 20);
            this.textBoxNama.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(277, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Urut";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioTidakAktif);
            this.groupBox1.Controls.Add(this.radioAktif);
            this.groupBox1.Controls.Add(this.radioAktifSemua);
            this.groupBox1.Location = new System.Drawing.Point(15, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 48);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Aktif";
            // 
            // radioTidakAktif
            // 
            this.radioTidakAktif.AutoSize = true;
            this.radioTidakAktif.Location = new System.Drawing.Point(122, 19);
            this.radioTidakAktif.Name = "radioTidakAktif";
            this.radioTidakAktif.Size = new System.Drawing.Size(76, 17);
            this.radioTidakAktif.TabIndex = 2;
            this.radioTidakAktif.Text = "Tidak Aktif";
            this.radioTidakAktif.UseVisualStyleBackColor = true;
            // 
            // radioAktif
            // 
            this.radioAktif.AutoSize = true;
            this.radioAktif.Checked = true;
            this.radioAktif.Location = new System.Drawing.Point(70, 19);
            this.radioAktif.Name = "radioAktif";
            this.radioAktif.Size = new System.Drawing.Size(46, 17);
            this.radioAktif.TabIndex = 1;
            this.radioAktif.TabStop = true;
            this.radioAktif.Text = "Aktif";
            this.radioAktif.UseVisualStyleBackColor = true;
            // 
            // radioAktifSemua
            // 
            this.radioAktifSemua.AutoSize = true;
            this.radioAktifSemua.Location = new System.Drawing.Point(6, 19);
            this.radioAktifSemua.Name = "radioAktifSemua";
            this.radioAktifSemua.Size = new System.Drawing.Size(58, 17);
            this.radioAktifSemua.TabIndex = 0;
            this.radioAktifSemua.Text = "Semua";
            this.radioAktifSemua.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioTransitTidak);
            this.groupBox2.Controls.Add(this.radioTransitIya);
            this.groupBox2.Controls.Add(this.radioTransitSemua);
            this.groupBox2.Location = new System.Drawing.Point(222, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 48);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Boleh Transit";
            // 
            // radioTransitTidak
            // 
            this.radioTransitTidak.AutoSize = true;
            this.radioTransitTidak.Location = new System.Drawing.Point(115, 19);
            this.radioTransitTidak.Name = "radioTransitTidak";
            this.radioTransitTidak.Size = new System.Drawing.Size(52, 17);
            this.radioTransitTidak.TabIndex = 2;
            this.radioTransitTidak.Text = "Tidak";
            this.radioTransitTidak.UseVisualStyleBackColor = true;
            // 
            // radioTransitIya
            // 
            this.radioTransitIya.AutoSize = true;
            this.radioTransitIya.Checked = true;
            this.radioTransitIya.Location = new System.Drawing.Point(70, 19);
            this.radioTransitIya.Name = "radioTransitIya";
            this.radioTransitIya.Size = new System.Drawing.Size(39, 17);
            this.radioTransitIya.TabIndex = 1;
            this.radioTransitIya.TabStop = true;
            this.radioTransitIya.Text = "Iya";
            this.radioTransitIya.UseVisualStyleBackColor = true;
            // 
            // radioTransitSemua
            // 
            this.radioTransitSemua.AutoSize = true;
            this.radioTransitSemua.Location = new System.Drawing.Point(6, 19);
            this.radioTransitSemua.Name = "radioTransitSemua";
            this.radioTransitSemua.Size = new System.Drawing.Size(58, 17);
            this.radioTransitSemua.TabIndex = 0;
            this.radioTransitSemua.Text = "Semua";
            this.radioTransitSemua.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioUrut);
            this.groupBox3.Controls.Add(this.radioUrutNama);
            this.groupBox3.Location = new System.Drawing.Point(402, 38);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(118, 48);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Urut";
            // 
            // radioUrut
            // 
            this.radioUrut.AutoSize = true;
            this.radioUrut.Checked = true;
            this.radioUrut.Location = new System.Drawing.Point(65, 19);
            this.radioUrut.Name = "radioUrut";
            this.radioUrut.Size = new System.Drawing.Size(45, 17);
            this.radioUrut.TabIndex = 1;
            this.radioUrut.TabStop = true;
            this.radioUrut.Text = "Urut";
            this.radioUrut.UseVisualStyleBackColor = true;
            // 
            // radioUrutNama
            // 
            this.radioUrutNama.AutoSize = true;
            this.radioUrutNama.Location = new System.Drawing.Point(6, 19);
            this.radioUrutNama.Name = "radioUrutNama";
            this.radioUrutNama.Size = new System.Drawing.Size(53, 17);
            this.radioUrutNama.TabIndex = 0;
            this.radioUrutNama.Text = "Nama";
            this.radioUrutNama.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 7;
            this.button1.Tag = "actionButton";
            this.button1.Text = "CARI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 93);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 33);
            this.button2.TabIndex = 8;
            this.button2.Tag = "actionButton";
            this.button2.Text = "SIMPAN";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonUbah
            // 
            this.buttonUbah.Location = new System.Drawing.Point(177, 93);
            this.buttonUbah.Name = "buttonUbah";
            this.buttonUbah.Size = new System.Drawing.Size(75, 33);
            this.buttonUbah.TabIndex = 9;
            this.buttonUbah.Tag = "actionButton";
            this.buttonUbah.Text = "UBAH";
            this.buttonUbah.UseVisualStyleBackColor = true;
            this.buttonUbah.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(258, 93);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 33);
            this.button4.TabIndex = 10;
            this.button4.Tag = "actionButton";
            this.button4.Text = "Aktif/Non";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(339, 93);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 33);
            this.button5.TabIndex = 11;
            this.button5.Tag = "actionButton";
            this.button5.Text = "Transit/Non";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 132);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(506, 112);
            this.dataGridView1.TabIndex = 20;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Jumlah Data: -";
            // 
            // textboxUrut
            // 
            this.textboxUrut.Location = new System.Drawing.Point(314, 12);
            this.textboxUrut.Name = "textboxUrut";
            this.textboxUrut.Size = new System.Drawing.Size(82, 20);
            this.textboxUrut.TabIndex = 13;
            // 
            // MasterGudangForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 278);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textboxUrut);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.buttonUbah);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxNama);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MasterGudangForm";
            this.Text = "MasterGudang";
            this.Load += new System.EventHandler(this.MasterGudangForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioTidakAktif;
        private System.Windows.Forms.RadioButton radioAktif;
        private System.Windows.Forms.RadioButton radioAktifSemua;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioTransitTidak;
        private System.Windows.Forms.RadioButton radioTransitIya;
        private System.Windows.Forms.RadioButton radioTransitSemua;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioUrut;
        private System.Windows.Forms.RadioButton radioUrutNama;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonUbah;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private Components.NumericTextbox textboxUrut;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
    }
}