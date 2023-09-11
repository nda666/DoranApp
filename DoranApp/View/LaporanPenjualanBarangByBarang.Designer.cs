namespace DoranApp.View
{
    partial class LaporanPenjualanBarangByBarang
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
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.datePickerFilterMax = new System.Windows.Forms.DateTimePicker();
            this.datePickerFilterMin = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFilterNamaBarang = new System.Windows.Forms.TextBox();
            this.comboFilterDkategoribarang = new System.Windows.Forms.ComboBox();
            this.comboFilterHkategoribarang = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboFilterMastertimsales = new System.Windows.Forms.ComboBox();
            this.comboFilterSales = new System.Windows.Forms.ComboBox();
            this.comboFilterMasterchannelsales = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboFilterPelanggan = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboFilterLokasiKota = new System.Windows.Forms.ComboBox();
            this.comboFilterProvinsi = new System.Windows.Forms.ComboBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 21);
            this.label8.TabIndex = 62;
            this.label8.Text = "Tgl Trans. [F1] :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 21);
            this.label1.TabIndex = 61;
            this.label1.Text = "s/d";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // datePickerFilterMax
            // 
            this.datePickerFilterMax.CustomFormat = "dd/MM/yyyy";
            this.datePickerFilterMax.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerFilterMax.Location = new System.Drawing.Point(255, 12);
            this.datePickerFilterMax.Name = "datePickerFilterMax";
            this.datePickerFilterMax.Size = new System.Drawing.Size(96, 20);
            this.datePickerFilterMax.TabIndex = 60;
            // 
            // datePickerFilterMin
            // 
            this.datePickerFilterMin.CustomFormat = "dd/MM/yyyy";
            this.datePickerFilterMin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerFilterMin.Location = new System.Drawing.Point(117, 12);
            this.datePickerFilterMin.Name = "datePickerFilterMin";
            this.datePickerFilterMin.Size = new System.Drawing.Size(97, 20);
            this.datePickerFilterMin.TabIndex = 59;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 21);
            this.label4.TabIndex = 64;
            this.label4.Text = "Nama Brg. :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxFilterNamaBarang
            // 
            this.textBoxFilterNamaBarang.Location = new System.Drawing.Point(117, 39);
            this.textBoxFilterNamaBarang.Name = "textBoxFilterNamaBarang";
            this.textBoxFilterNamaBarang.Size = new System.Drawing.Size(234, 20);
            this.textBoxFilterNamaBarang.TabIndex = 65;
            // 
            // comboFilterDkategoribarang
            // 
            this.comboFilterDkategoribarang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterDkategoribarang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterDkategoribarang.DisplayMember = "Nama";
            this.comboFilterDkategoribarang.FormattingEnabled = true;
            this.comboFilterDkategoribarang.Location = new System.Drawing.Point(357, 39);
            this.comboFilterDkategoribarang.Name = "comboFilterDkategoribarang";
            this.comboFilterDkategoribarang.Size = new System.Drawing.Size(112, 21);
            this.comboFilterDkategoribarang.TabIndex = 66;
            this.comboFilterDkategoribarang.Text = "Semua Sub Brand";
            this.comboFilterDkategoribarang.ValueMember = "Koded";
            // 
            // comboFilterHkategoribarang
            // 
            this.comboFilterHkategoribarang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterHkategoribarang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterHkategoribarang.DisplayMember = "Nama";
            this.comboFilterHkategoribarang.FormattingEnabled = true;
            this.comboFilterHkategoribarang.Location = new System.Drawing.Point(475, 39);
            this.comboFilterHkategoribarang.Name = "comboFilterHkategoribarang";
            this.comboFilterHkategoribarang.Size = new System.Drawing.Size(112, 21);
            this.comboFilterHkategoribarang.TabIndex = 67;
            this.comboFilterHkategoribarang.Text = "Semua Brand";
            this.comboFilterHkategoribarang.ValueMember = "Kodeh";
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(593, 39);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(112, 21);
            this.comboBox2.TabIndex = 68;
            this.comboBox2.Text = "Semua Gudang";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 21);
            this.label7.TabIndex = 71;
            this.label7.Text = "Sales :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboFilterMastertimsales
            // 
            this.comboFilterMastertimsales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterMastertimsales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterMastertimsales.DisplayMember = "Nama";
            this.comboFilterMastertimsales.FormattingEnabled = true;
            this.comboFilterMastertimsales.Location = new System.Drawing.Point(232, 65);
            this.comboFilterMastertimsales.Name = "comboFilterMastertimsales";
            this.comboFilterMastertimsales.Size = new System.Drawing.Size(119, 21);
            this.comboFilterMastertimsales.TabIndex = 70;
            this.comboFilterMastertimsales.Text = "Semua Tim Sales";
            this.comboFilterMastertimsales.ValueMember = "Kode";
            // 
            // comboFilterSales
            // 
            this.comboFilterSales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterSales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterSales.DisplayMember = "Nama";
            this.comboFilterSales.FormattingEnabled = true;
            this.comboFilterSales.Location = new System.Drawing.Point(117, 65);
            this.comboFilterSales.Name = "comboFilterSales";
            this.comboFilterSales.Size = new System.Drawing.Size(109, 21);
            this.comboFilterSales.TabIndex = 69;
            this.comboFilterSales.Text = "Semua Sales";
            this.comboFilterSales.ValueMember = "Kode";
            // 
            // comboFilterMasterchannelsales
            // 
            this.comboFilterMasterchannelsales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterMasterchannelsales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterMasterchannelsales.DisplayMember = "Nama";
            this.comboFilterMasterchannelsales.FormattingEnabled = true;
            this.comboFilterMasterchannelsales.Location = new System.Drawing.Point(357, 66);
            this.comboFilterMasterchannelsales.Name = "comboFilterMasterchannelsales";
            this.comboFilterMasterchannelsales.Size = new System.Drawing.Size(129, 21);
            this.comboFilterMasterchannelsales.TabIndex = 72;
            this.comboFilterMasterchannelsales.Text = "Semua Channel Sales";
            this.comboFilterMasterchannelsales.ValueMember = "Kode";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 21);
            this.label2.TabIndex = 74;
            this.label2.Text = "Nama Toko :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboFilterPelanggan
            // 
            this.comboFilterPelanggan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterPelanggan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterPelanggan.FormattingEnabled = true;
            this.comboFilterPelanggan.Location = new System.Drawing.Point(117, 92);
            this.comboFilterPelanggan.Name = "comboFilterPelanggan";
            this.comboFilterPelanggan.Size = new System.Drawing.Size(184, 21);
            this.comboFilterPelanggan.TabIndex = 73;
            this.comboFilterPelanggan.Text = "Semua Sales";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(307, 96);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(53, 17);
            this.checkBox1.TabIndex = 75;
            this.checkBox1.Text = "Show";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(366, 96);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(52, 17);
            this.checkBox2.TabIndex = 76;
            this.checkBox2.Text = "Retur";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 21);
            this.label3.TabIndex = 78;
            this.label3.Text = "Lokasi :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboFilterLokasiKota
            // 
            this.comboFilterLokasiKota.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterLokasiKota.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterLokasiKota.FormattingEnabled = true;
            this.comboFilterLokasiKota.Location = new System.Drawing.Point(117, 119);
            this.comboFilterLokasiKota.Name = "comboFilterLokasiKota";
            this.comboFilterLokasiKota.Size = new System.Drawing.Size(109, 21);
            this.comboFilterLokasiKota.TabIndex = 77;
            this.comboFilterLokasiKota.Text = "Semua Kota";
            // 
            // comboFilterProvinsi
            // 
            this.comboFilterProvinsi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterProvinsi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterProvinsi.FormattingEnabled = true;
            this.comboFilterProvinsi.Location = new System.Drawing.Point(232, 119);
            this.comboFilterProvinsi.Name = "comboFilterProvinsi";
            this.comboFilterProvinsi.Size = new System.Drawing.Size(109, 21);
            this.comboFilterProvinsi.TabIndex = 79;
            this.comboFilterProvinsi.Text = "Semua Provinsi";
            // 
            // comboBox7
            // 
            this.comboBox7.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox7.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Location = new System.Drawing.Point(347, 119);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(109, 21);
            this.comboBox7.TabIndex = 80;
            this.comboBox7.Text = "Semua Filter";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 182);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(701, 218);
            this.dataGridView1.TabIndex = 81;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(117, 146);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 82;
            this.button1.Text = "Lihat [F3]";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(198, 146);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 30);
            this.button2.TabIndex = 83;
            this.button2.Text = "Omzet By Brand";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(307, 146);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 30);
            this.button3.TabIndex = 84;
            this.button3.Text = "Omzet By Sub Brand";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(507, 146);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 30);
            this.button4.TabIndex = 85;
            this.button4.Text = "Cetak";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(602, 146);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 30);
            this.button5.TabIndex = 86;
            this.button5.Text = "Cetak LR";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // LaporanPenjualanBarangByBarang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 412);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox7);
            this.Controls.Add(this.comboFilterProvinsi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboFilterLokasiKota);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboFilterPelanggan);
            this.Controls.Add(this.comboFilterMasterchannelsales);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboFilterMastertimsales);
            this.Controls.Add(this.comboFilterSales);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboFilterHkategoribarang);
            this.Controls.Add(this.comboFilterDkategoribarang);
            this.Controls.Add(this.textBoxFilterNamaBarang);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datePickerFilterMax);
            this.Controls.Add(this.datePickerFilterMin);
            this.Name = "LaporanPenjualanBarangByBarang";
            this.Text = "LaporanPenjualanBarangByBarang";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LaporanPenjualanBarangByBarang_FormClosing);
            this.Load += new System.EventHandler(this.LaporanPenjualanBarangByBarang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datePickerFilterMax;
        private System.Windows.Forms.DateTimePicker datePickerFilterMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFilterNamaBarang;
        private System.Windows.Forms.ComboBox comboFilterDkategoribarang;
        private System.Windows.Forms.ComboBox comboFilterHkategoribarang;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboFilterMastertimsales;
        private System.Windows.Forms.ComboBox comboFilterSales;
        private System.Windows.Forms.ComboBox comboFilterMasterchannelsales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboFilterPelanggan;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboFilterLokasiKota;
        private System.Windows.Forms.ComboBox comboFilterProvinsi;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}