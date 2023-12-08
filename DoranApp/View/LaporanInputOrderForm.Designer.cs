using System.ComponentModel;

namespace DoranApp.View;

partial class LaporanInputOrderForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        this.comboFilterDkategoribarang = new System.Windows.Forms.ComboBox();
        this.textBoxFilterNamaBarang = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.comboFilterSales = new System.Windows.Forms.ComboBox();
        this.comboFilterMastertimsales = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.comboFilterPelanggan = new System.Windows.Forms.ComboBox();
        this.comboFilterProvinsi = new System.Windows.Forms.ComboBox();
        this.label3 = new System.Windows.Forms.Label();
        this.comboFilterLokasiKota = new System.Windows.Forms.ComboBox();
        this.label5 = new System.Windows.Forms.Label();
        this.comboFilterPenyiap = new System.Windows.Forms.ComboBox();
        this.textBoxFilterSeriOnline = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.textBoxFilterBarcode = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.label10 = new System.Windows.Forms.Label();
        this.comboFilterGudang = new System.Windows.Forms.ComboBox();
        this.groupBoxBayar = new System.Windows.Forms.GroupBox();
        this.radioButton5 = new System.Windows.Forms.RadioButton();
        this.radioButton4 = new System.Windows.Forms.RadioButton();
        this.radioButton3 = new System.Windows.Forms.RadioButton();
        this.radioButton2 = new System.Windows.Forms.RadioButton();
        this.radioButton1 = new System.Windows.Forms.RadioButton();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.radioButton8 = new System.Windows.Forms.RadioButton();
        this.radioButton9 = new System.Windows.Forms.RadioButton();
        this.radioButton10 = new System.Windows.Forms.RadioButton();
        this.label11 = new System.Windows.Forms.Label();
        this.label12 = new System.Windows.Forms.Label();
        this.button1 = new System.Windows.Forms.Button();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.tabPage3 = new System.Windows.Forms.TabPage();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.dataGridView2 = new System.Windows.Forms.DataGridView();
        this.groupBoxBayar.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.tabControl1.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
        this.SuspendLayout();
        // 
        // label8
        // 
        this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label8.Location = new System.Drawing.Point(12, 9);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(99, 21);
        this.label8.TabIndex = 66;
        this.label8.Text = "Tgl Trans. [F1] :";
        this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(220, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(29, 21);
        this.label1.TabIndex = 65;
        this.label1.Text = "s/d";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // datePickerFilterMax
        // 
        this.datePickerFilterMax.CustomFormat = "dd/MM/yyyy";
        this.datePickerFilterMax.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.datePickerFilterMax.Location = new System.Drawing.Point(255, 10);
        this.datePickerFilterMax.Name = "datePickerFilterMax";
        this.datePickerFilterMax.Size = new System.Drawing.Size(96, 20);
        this.datePickerFilterMax.TabIndex = 64;
        // 
        // datePickerFilterMin
        // 
        this.datePickerFilterMin.CustomFormat = "dd/MM/yyyy";
        this.datePickerFilterMin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.datePickerFilterMin.Location = new System.Drawing.Point(117, 10);
        this.datePickerFilterMin.Name = "datePickerFilterMin";
        this.datePickerFilterMin.Size = new System.Drawing.Size(97, 20);
        this.datePickerFilterMin.TabIndex = 63;
        // 
        // comboFilterDkategoribarang
        // 
        this.comboFilterDkategoribarang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterDkategoribarang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterDkategoribarang.DisplayMember = "Nama";
        this.comboFilterDkategoribarang.FormattingEnabled = true;
        this.comboFilterDkategoribarang.Location = new System.Drawing.Point(255, 36);
        this.comboFilterDkategoribarang.Name = "comboFilterDkategoribarang";
        this.comboFilterDkategoribarang.Size = new System.Drawing.Size(96, 21);
        this.comboFilterDkategoribarang.TabIndex = 69;
        this.comboFilterDkategoribarang.Text = "Semua Sub Brand";
        this.comboFilterDkategoribarang.ValueMember = "Koded";
        // 
        // textBoxFilterNamaBarang
        // 
        this.textBoxFilterNamaBarang.Location = new System.Drawing.Point(117, 36);
        this.textBoxFilterNamaBarang.Name = "textBoxFilterNamaBarang";
        this.textBoxFilterNamaBarang.Size = new System.Drawing.Size(132, 20);
        this.textBoxFilterNamaBarang.TabIndex = 68;
        // 
        // label4
        // 
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(15, 35);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(96, 21);
        this.label4.TabIndex = 67;
        this.label4.Text = "Nama Brg. :";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label7.Location = new System.Drawing.Point(15, 61);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(96, 21);
        this.label7.TabIndex = 73;
        this.label7.Text = "Sales :";
        this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboFilterSales
        // 
        this.comboFilterSales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterSales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterSales.DisplayMember = "Nama";
        this.comboFilterSales.FormattingEnabled = true;
        this.comboFilterSales.Location = new System.Drawing.Point(117, 62);
        this.comboFilterSales.Name = "comboFilterSales";
        this.comboFilterSales.Size = new System.Drawing.Size(109, 21);
        this.comboFilterSales.TabIndex = 72;
        this.comboFilterSales.Text = "Semua Sales";
        this.comboFilterSales.ValueMember = "Kode";
        // 
        // comboFilterMastertimsales
        // 
        this.comboFilterMastertimsales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterMastertimsales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterMastertimsales.DisplayMember = "Nama";
        this.comboFilterMastertimsales.FormattingEnabled = true;
        this.comboFilterMastertimsales.Location = new System.Drawing.Point(232, 62);
        this.comboFilterMastertimsales.Name = "comboFilterMastertimsales";
        this.comboFilterMastertimsales.Size = new System.Drawing.Size(119, 21);
        this.comboFilterMastertimsales.TabIndex = 74;
        this.comboFilterMastertimsales.Text = "Semua Tim Sales";
        this.comboFilterMastertimsales.ValueMember = "Kode";
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(15, 88);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(96, 21);
        this.label2.TabIndex = 76;
        this.label2.Text = "Nama Toko :";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboFilterPelanggan
        // 
        this.comboFilterPelanggan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterPelanggan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterPelanggan.FormattingEnabled = true;
        this.comboFilterPelanggan.Location = new System.Drawing.Point(117, 89);
        this.comboFilterPelanggan.Name = "comboFilterPelanggan";
        this.comboFilterPelanggan.Size = new System.Drawing.Size(184, 21);
        this.comboFilterPelanggan.TabIndex = 75;
        this.comboFilterPelanggan.Text = "Semua Sales";
        // 
        // comboFilterProvinsi
        // 
        this.comboFilterProvinsi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterProvinsi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterProvinsi.FormattingEnabled = true;
        this.comboFilterProvinsi.Location = new System.Drawing.Point(232, 116);
        this.comboFilterProvinsi.Name = "comboFilterProvinsi";
        this.comboFilterProvinsi.Size = new System.Drawing.Size(109, 21);
        this.comboFilterProvinsi.TabIndex = 82;
        this.comboFilterProvinsi.Text = "Semua Provinsi";
        // 
        // label3
        // 
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(15, 115);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(96, 21);
        this.label3.TabIndex = 81;
        this.label3.Text = "Lokasi :";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboFilterLokasiKota
        // 
        this.comboFilterLokasiKota.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterLokasiKota.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterLokasiKota.FormattingEnabled = true;
        this.comboFilterLokasiKota.Location = new System.Drawing.Point(117, 116);
        this.comboFilterLokasiKota.Name = "comboFilterLokasiKota";
        this.comboFilterLokasiKota.Size = new System.Drawing.Size(109, 21);
        this.comboFilterLokasiKota.TabIndex = 80;
        this.comboFilterLokasiKota.Text = "Semua Kota";
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(15, 142);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(96, 21);
        this.label5.TabIndex = 84;
        this.label5.Text = "Penyiap :";
        this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboFilterPenyiap
        // 
        this.comboFilterPenyiap.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterPenyiap.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterPenyiap.FormattingEnabled = true;
        this.comboFilterPenyiap.Location = new System.Drawing.Point(117, 143);
        this.comboFilterPenyiap.Name = "comboFilterPenyiap";
        this.comboFilterPenyiap.Size = new System.Drawing.Size(109, 21);
        this.comboFilterPenyiap.TabIndex = 83;
        this.comboFilterPenyiap.Text = "Semua Kota";
        // 
        // textBoxFilterSeriOnline
        // 
        this.textBoxFilterSeriOnline.Location = new System.Drawing.Point(117, 170);
        this.textBoxFilterSeriOnline.Name = "textBoxFilterSeriOnline";
        this.textBoxFilterSeriOnline.Size = new System.Drawing.Size(132, 20);
        this.textBoxFilterSeriOnline.TabIndex = 86;
        // 
        // label6
        // 
        this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label6.Location = new System.Drawing.Point(15, 169);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(96, 21);
        this.label6.TabIndex = 85;
        this.label6.Text = "Seri Online :";
        this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // textBoxFilterBarcode
        // 
        this.textBoxFilterBarcode.Location = new System.Drawing.Point(117, 196);
        this.textBoxFilterBarcode.Name = "textBoxFilterBarcode";
        this.textBoxFilterBarcode.Size = new System.Drawing.Size(132, 20);
        this.textBoxFilterBarcode.TabIndex = 88;
        // 
        // label9
        // 
        this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label9.Location = new System.Drawing.Point(15, 195);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(96, 21);
        this.label9.TabIndex = 87;
        this.label9.Text = "Barcode :";
        this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label10
        // 
        this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label10.Location = new System.Drawing.Point(15, 221);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(96, 21);
        this.label10.TabIndex = 90;
        this.label10.Text = "Gudang :";
        this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboFilterGudang
        // 
        this.comboFilterGudang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterGudang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterGudang.DisplayMember = "Nama";
        this.comboFilterGudang.FormattingEnabled = true;
        this.comboFilterGudang.Location = new System.Drawing.Point(117, 222);
        this.comboFilterGudang.Name = "comboFilterGudang";
        this.comboFilterGudang.Size = new System.Drawing.Size(109, 21);
        this.comboFilterGudang.TabIndex = 89;
        this.comboFilterGudang.Text = "Semua Gudang";
        this.comboFilterGudang.ValueMember = "Kode";
        // 
        // groupBoxBayar
        // 
        this.groupBoxBayar.Controls.Add(this.radioButton5);
        this.groupBoxBayar.Controls.Add(this.radioButton4);
        this.groupBoxBayar.Controls.Add(this.radioButton3);
        this.groupBoxBayar.Controls.Add(this.radioButton2);
        this.groupBoxBayar.Controls.Add(this.radioButton1);
        this.groupBoxBayar.Location = new System.Drawing.Point(117, 249);
        this.groupBoxBayar.Name = "groupBoxBayar";
        this.groupBoxBayar.Size = new System.Drawing.Size(234, 70);
        this.groupBoxBayar.TabIndex = 91;
        this.groupBoxBayar.TabStop = false;
        this.groupBoxBayar.Text = "Bayar";
        // 
        // radioButton5
        // 
        this.radioButton5.AutoSize = true;
        this.radioButton5.Location = new System.Drawing.Point(71, 43);
        this.radioButton5.Name = "radioButton5";
        this.radioButton5.Size = new System.Drawing.Size(64, 17);
        this.radioButton5.TabIndex = 4;
        this.radioButton5.Tag = "3";
        this.radioButton5.Text = "Pending";
        this.radioButton5.UseVisualStyleBackColor = true;
        // 
        // radioButton4
        // 
        this.radioButton4.AutoSize = true;
        this.radioButton4.Location = new System.Drawing.Point(7, 43);
        this.radioButton4.Name = "radioButton4";
        this.radioButton4.Size = new System.Drawing.Size(58, 17);
        this.radioButton4.TabIndex = 3;
        this.radioButton4.Tag = "2";
        this.radioButton4.Text = "Cancel";
        this.radioButton4.UseVisualStyleBackColor = true;
        // 
        // radioButton3
        // 
        this.radioButton3.AutoSize = true;
        this.radioButton3.Location = new System.Drawing.Point(131, 20);
        this.radioButton3.Name = "radioButton3";
        this.radioButton3.Size = new System.Drawing.Size(90, 17);
        this.radioButton3.TabIndex = 2;
        this.radioButton3.Tag = "1";
        this.radioButton3.Text = "Masih Kurang";
        this.radioButton3.UseVisualStyleBackColor = true;
        // 
        // radioButton2
        // 
        this.radioButton2.AutoSize = true;
        this.radioButton2.Location = new System.Drawing.Point(71, 20);
        this.radioButton2.Name = "radioButton2";
        this.radioButton2.Size = new System.Drawing.Size(54, 17);
        this.radioButton2.TabIndex = 1;
        this.radioButton2.Tag = "0";
        this.radioButton2.Text = "Belum";
        this.radioButton2.UseVisualStyleBackColor = true;
        // 
        // radioButton1
        // 
        this.radioButton1.AutoSize = true;
        this.radioButton1.Checked = true;
        this.radioButton1.Location = new System.Drawing.Point(7, 20);
        this.radioButton1.Name = "radioButton1";
        this.radioButton1.Size = new System.Drawing.Size(58, 17);
        this.radioButton1.TabIndex = 0;
        this.radioButton1.TabStop = true;
        this.radioButton1.Tag = "";
        this.radioButton1.Text = "Semua";
        this.radioButton1.UseVisualStyleBackColor = true;
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.radioButton8);
        this.groupBox1.Controls.Add(this.radioButton9);
        this.groupBox1.Controls.Add(this.radioButton10);
        this.groupBox1.Location = new System.Drawing.Point(117, 325);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(234, 49);
        this.groupBox1.TabIndex = 92;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Di Cek";
        // 
        // radioButton8
        // 
        this.radioButton8.AutoSize = true;
        this.radioButton8.Location = new System.Drawing.Point(131, 20);
        this.radioButton8.Name = "radioButton8";
        this.radioButton8.Size = new System.Drawing.Size(56, 17);
        this.radioButton8.TabIndex = 2;
        this.radioButton8.Tag = "1";
        this.radioButton8.Text = "Sudah";
        this.radioButton8.UseVisualStyleBackColor = true;
        // 
        // radioButton9
        // 
        this.radioButton9.AutoSize = true;
        this.radioButton9.Location = new System.Drawing.Point(71, 20);
        this.radioButton9.Name = "radioButton9";
        this.radioButton9.Size = new System.Drawing.Size(54, 17);
        this.radioButton9.TabIndex = 1;
        this.radioButton9.Tag = "0";
        this.radioButton9.Text = "Belum";
        this.radioButton9.UseVisualStyleBackColor = true;
        // 
        // radioButton10
        // 
        this.radioButton10.AutoSize = true;
        this.radioButton10.Checked = true;
        this.radioButton10.Location = new System.Drawing.Point(7, 20);
        this.radioButton10.Name = "radioButton10";
        this.radioButton10.Size = new System.Drawing.Size(58, 17);
        this.radioButton10.TabIndex = 0;
        this.radioButton10.TabStop = true;
        this.radioButton10.Tag = "";
        this.radioButton10.Text = "Semua";
        this.radioButton10.UseVisualStyleBackColor = true;
        // 
        // label11
        // 
        this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label11.Location = new System.Drawing.Point(15, 249);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(96, 21);
        this.label11.TabIndex = 93;
        this.label11.Text = "Bayar :";
        this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.label11.Visible = false;
        // 
        // label12
        // 
        this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label12.Location = new System.Drawing.Point(15, 325);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(96, 21);
        this.label12.TabIndex = 94;
        this.label12.Text = "Dicek :";
        this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.label12.Visible = false;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(117, 380);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(109, 34);
        this.button1.TabIndex = 95;
        this.button1.Text = "Lihat [F3]";
        this.button1.UseVisualStyleBackColor = true;
        // 
        // checkBox1
        // 
        this.checkBox1.Location = new System.Drawing.Point(247, 380);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(104, 24);
        this.checkBox1.TabIndex = 96;
        this.checkBox1.Text = "Show Stock";
        this.checkBox1.UseVisualStyleBackColor = true;
        // 
        // tabControl1
        // 
        this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.tabControl1.Controls.Add(this.tabPage1);
        this.tabControl1.Controls.Add(this.tabPage2);
        this.tabControl1.Controls.Add(this.tabPage3);
        this.tabControl1.Location = new System.Drawing.Point(357, 9);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(381, 437);
        this.tabControl1.TabIndex = 97;
        // 
        // tabPage1
        // 
        this.tabPage1.Location = new System.Drawing.Point(4, 22);
        this.tabPage1.Name = "tabPage1";
        this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage1.Size = new System.Drawing.Size(373, 411);
        this.tabPage1.TabIndex = 0;
        this.tabPage1.Text = "Per Barang";
        this.tabPage1.UseVisualStyleBackColor = true;
        // 
        // tabPage2
        // 
        this.tabPage2.Controls.Add(this.groupBox3);
        this.tabPage2.Controls.Add(this.groupBox2);
        this.tabPage2.Location = new System.Drawing.Point(4, 22);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(373, 411);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "Per Transaksi Order";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // groupBox3
        // 
        this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox3.Controls.Add(this.dataGridView2);
        this.groupBox3.Location = new System.Drawing.Point(6, 294);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(364, 111);
        this.groupBox3.TabIndex = 1;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "Detail Input Order";
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.dataGridView1);
        this.groupBox2.Location = new System.Drawing.Point(6, 6);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(364, 282);
        this.groupBox2.TabIndex = 0;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Laporan Input Order";
        // 
        // tabPage3
        // 
        this.tabPage3.Location = new System.Drawing.Point(4, 22);
        this.tabPage3.Name = "tabPage3";
        this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage3.Size = new System.Drawing.Size(373, 411);
        this.tabPage3.TabIndex = 2;
        this.tabPage3.Text = "Pelunasan";
        this.tabPage3.UseVisualStyleBackColor = true;
        // 
        // dataGridView1
        // 
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dataGridView1.Location = new System.Drawing.Point(3, 16);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.Size = new System.Drawing.Size(358, 263);
        this.dataGridView1.TabIndex = 82;
        // 
        // dataGridView2
        // 
        this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dataGridView2.Location = new System.Drawing.Point(3, 16);
        this.dataGridView2.Name = "dataGridView2";
        this.dataGridView2.RowHeadersVisible = false;
        this.dataGridView2.Size = new System.Drawing.Size(358, 92);
        this.dataGridView2.TabIndex = 82;
        // 
        // LaporanInputOrderForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(740, 450);
        this.Controls.Add(this.tabControl1);
        this.Controls.Add(this.checkBox1);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.label12);
        this.Controls.Add(this.label11);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.groupBoxBayar);
        this.Controls.Add(this.label10);
        this.Controls.Add(this.comboFilterGudang);
        this.Controls.Add(this.textBoxFilterBarcode);
        this.Controls.Add(this.label9);
        this.Controls.Add(this.textBoxFilterSeriOnline);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.comboFilterPenyiap);
        this.Controls.Add(this.comboFilterProvinsi);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.comboFilterLokasiKota);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.comboFilterPelanggan);
        this.Controls.Add(this.comboFilterMastertimsales);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.comboFilterSales);
        this.Controls.Add(this.comboFilterDkategoribarang);
        this.Controls.Add(this.textBoxFilterNamaBarang);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label8);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.datePickerFilterMax);
        this.Controls.Add(this.datePickerFilterMin);
        this.Name = "LaporanInputOrderForm";
        this.Text = "LaporanInputOrderForm";
        this.Load += new System.EventHandler(this.LaporanInputOrderForm_Load);
        this.groupBoxBayar.ResumeLayout(false);
        this.groupBoxBayar.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.tabControl1.ResumeLayout(false);
        this.tabPage2.ResumeLayout(false);
        this.groupBox3.ResumeLayout(false);
        this.groupBox2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.DataGridView dataGridView2;

    private System.Windows.Forms.DataGridView dataGridView1;

    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;

    private System.Windows.Forms.TabPage tabPage3;

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;

    private System.Windows.Forms.CheckBox checkBox1;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radioButton8;
    private System.Windows.Forms.RadioButton radioButton9;
    private System.Windows.Forms.RadioButton radioButton10;

    private System.Windows.Forms.RadioButton radioButton4;
    private System.Windows.Forms.RadioButton radioButton5;

    private System.Windows.Forms.GroupBox groupBoxBayar;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;

    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.ComboBox comboFilterGudang;

    private System.Windows.Forms.TextBox textBoxFilterBarcode;
    private System.Windows.Forms.Label label9;

    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox textBoxFilterSeriOnline;

    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox comboFilterPenyiap;

    private System.Windows.Forms.ComboBox comboFilterProvinsi;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox comboFilterLokasiKota;

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox comboFilterPelanggan;

    private System.Windows.Forms.ComboBox comboFilterMastertimsales;

    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox comboFilterSales;

    private System.Windows.Forms.ComboBox comboFilterDkategoribarang;
    private System.Windows.Forms.TextBox textBoxFilterNamaBarang;
    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DateTimePicker datePickerFilterMax;
    private System.Windows.Forms.DateTimePicker datePickerFilterMin;

    #endregion
}