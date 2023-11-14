using System.ComponentModel;

namespace DoranApp.View.CekStok;

partial class MutasiBarangControl
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

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.label7 = new System.Windows.Forms.Label();
        this.comboMasterbarang = new System.Windows.Forms.ComboBox();
        this.label1 = new System.Windows.Forms.Label();
        this.comboMastergudang = new System.Windows.Forms.ComboBox();
        this.checkboxDatalama = new System.Windows.Forms.CheckBox();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.button4 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.button9 = new System.Windows.Forms.Button();
        this.button8 = new System.Windows.Forms.Button();
        this.label17 = new System.Windows.Forms.Label();
        this.label16 = new System.Windows.Forms.Label();
        this.label15 = new System.Windows.Forms.Label();
        this.label14 = new System.Windows.Forms.Label();
        this.label13 = new System.Windows.Forms.Label();
        this.label12 = new System.Windows.Forms.Label();
        this.label11 = new System.Windows.Forms.Label();
        this.label10 = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
        this.checkBox2 = new System.Windows.Forms.CheckBox();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.label6 = new System.Windows.Forms.Label();
        this.listBoxStok = new System.Windows.Forms.ListBox();
        this.listBoxGudang = new System.Windows.Forms.ListBox();
        this.button7 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.SuspendLayout();
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label7.Location = new System.Drawing.Point(5, 5);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(140, 21);
        this.label7.TabIndex = 75;
        this.label7.Text = "Nama Barang [F2] :";
        this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboMasterbarang
        // 
        this.comboMasterbarang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboMasterbarang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboMasterbarang.DisplayMember = "BrgNama";
        this.comboMasterbarang.FormattingEnabled = true;
        this.comboMasterbarang.Location = new System.Drawing.Point(151, 8);
        this.comboMasterbarang.Name = "comboMasterbarang";
        this.comboMasterbarang.Size = new System.Drawing.Size(308, 21);
        this.comboMasterbarang.TabIndex = 74;
        this.comboMasterbarang.Text = "Pilih Barang";
        this.comboMasterbarang.ValueMember = "BrgKode";
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(5, 35);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(140, 21);
        this.label1.TabIndex = 77;
        this.label1.Text = "Gudang :";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboMastergudang
        // 
        this.comboMastergudang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboMastergudang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboMastergudang.DisplayMember = "Nama";
        this.comboMastergudang.FormattingEnabled = true;
        this.comboMastergudang.Location = new System.Drawing.Point(151, 35);
        this.comboMastergudang.Name = "comboMastergudang";
        this.comboMastergudang.Size = new System.Drawing.Size(148, 21);
        this.comboMastergudang.TabIndex = 76;
        this.comboMastergudang.Text = "Pilih Gudang";
        this.comboMastergudang.ValueMember = "Kode";
        // 
        // checkboxDatalama
        // 
        this.checkboxDatalama.Location = new System.Drawing.Point(15, 66);
        this.checkboxDatalama.Name = "checkboxDatalama";
        this.checkboxDatalama.Size = new System.Drawing.Size(130, 29);
        this.checkboxDatalama.TabIndex = 78;
        this.checkboxDatalama.Text = "Tampilkan Data Lama";
        this.checkboxDatalama.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.checkboxDatalama.UseVisualStyleBackColor = true;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(151, 64);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(130, 31);
        this.button1.TabIndex = 79;
        this.button1.Text = "Cek Stok [F3]";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(287, 64);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(172, 31);
        this.button2.TabIndex = 80;
        this.button2.Text = "Lihat Detail Mutasi";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click_1);
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(7, 16);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(60, 21);
        this.label2.TabIndex = 82;
        this.label2.Text = "Nama :";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label3
        // 
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(7, 37);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(60, 21);
        this.label3.TabIndex = 83;
        this.label3.Text = "Toko :";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboBox1
        // 
        this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboBox1.DisplayMember = "Nama";
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(73, 46);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(148, 21);
        this.comboBox1.TabIndex = 84;
        this.comboBox1.Text = "Pilih Gudang";
        this.comboBox1.ValueMember = "Kode";
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(73, 20);
        this.textBox1.Name = "textBox1";
        this.textBox1.ReadOnly = true;
        this.textBox1.Size = new System.Drawing.Size(148, 20);
        this.textBox1.TabIndex = 85;
        // 
        // groupBox1
        // 
        this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox1.Controls.Add(this.button4);
        this.groupBox1.Controls.Add(this.button3);
        this.groupBox1.Controls.Add(this.dataGridView1);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.textBox1);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.comboBox1);
        this.groupBox1.Location = new System.Drawing.Point(3, 122);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(461, 553);
        this.groupBox1.TabIndex = 86;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Detail Mutasi Stok Gudang";
        // 
        // button4
        // 
        this.button4.Location = new System.Drawing.Point(297, 42);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(86, 27);
        this.button4.TabIndex = 89;
        this.button4.Text = "Update";
        this.button4.UseVisualStyleBackColor = true;
        // 
        // button3
        // 
        this.button3.Location = new System.Drawing.Point(227, 42);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(64, 27);
        this.button3.TabIndex = 88;
        this.button3.Text = "Filter";
        this.button3.UseVisualStyleBackColor = true;
        // 
        // dataGridView1
        // 
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeColumns = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Location = new System.Drawing.Point(7, 75);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.ReadOnly = true;
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.Size = new System.Drawing.Size(445, 465);
        this.dataGridView1.TabIndex = 86;
        this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
        // 
        // label4
        // 
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(236, 98);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(223, 21);
        this.label4.TabIndex = 87;
        this.label4.Text = "Stok :";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(12, 98);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(217, 21);
        this.label5.TabIndex = 88;
        this.label5.Text = "Stok Gudang Atas + LT3 :";
        this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // splitContainer1
        // 
        this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
        this.splitContainer1.Location = new System.Drawing.Point(0, 0);
        this.splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
        this.splitContainer1.Panel1.Controls.Add(this.label5);
        this.splitContainer1.Panel1.Controls.Add(this.comboMasterbarang);
        this.splitContainer1.Panel1.Controls.Add(this.label4);
        this.splitContainer1.Panel1.Controls.Add(this.label7);
        this.splitContainer1.Panel1.Controls.Add(this.comboMastergudang);
        this.splitContainer1.Panel1.Controls.Add(this.button2);
        this.splitContainer1.Panel1.Controls.Add(this.label1);
        this.splitContainer1.Panel1.Controls.Add(this.button1);
        this.splitContainer1.Panel1.Controls.Add(this.checkboxDatalama);
        // 
        // splitContainer1.Panel2
        // 
        this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
        this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
        this.splitContainer1.Panel2MinSize = 320;
        this.splitContainer1.Size = new System.Drawing.Size(793, 691);
        this.splitContainer1.SplitterDistance = 467;
        this.splitContainer1.SplitterIncrement = 4;
        this.splitContainer1.TabIndex = 89;
        // 
        // groupBox3
        // 
        this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.groupBox3.Controls.Add(this.button9);
        this.groupBox3.Controls.Add(this.button8);
        this.groupBox3.Controls.Add(this.label17);
        this.groupBox3.Controls.Add(this.label16);
        this.groupBox3.Controls.Add(this.label15);
        this.groupBox3.Controls.Add(this.label14);
        this.groupBox3.Controls.Add(this.label13);
        this.groupBox3.Controls.Add(this.label12);
        this.groupBox3.Controls.Add(this.label11);
        this.groupBox3.Controls.Add(this.label10);
        this.groupBox3.Controls.Add(this.label9);
        this.groupBox3.Controls.Add(this.label8);
        this.groupBox3.Controls.Add(this.checkBox2);
        this.groupBox3.Controls.Add(this.checkBox1);
        this.groupBox3.Location = new System.Drawing.Point(9, 435);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(304, 240);
        this.groupBox3.TabIndex = 1;
        this.groupBox3.TabStop = false;
        this.groupBox3.Text = "Inteligent";
        // 
        // button9
        // 
        this.button9.Location = new System.Drawing.Point(9, 206);
        this.button9.Name = "button9";
        this.button9.Size = new System.Drawing.Size(89, 26);
        this.button9.TabIndex = 13;
        this.button9.Text = "Non Aktifkan";
        this.button9.UseVisualStyleBackColor = true;
        this.button9.Click += new System.EventHandler(this.button9_Click);
        // 
        // button8
        // 
        this.button8.Location = new System.Drawing.Point(9, 174);
        this.button8.Name = "button8";
        this.button8.Size = new System.Drawing.Size(75, 26);
        this.button8.TabIndex = 12;
        this.button8.Text = "Cetak";
        this.button8.UseVisualStyleBackColor = true;
        this.button8.Click += new System.EventHandler(this.button8_Click);
        // 
        // label17
        // 
        this.label17.Location = new System.Drawing.Point(151, 149);
        this.label17.Margin = new System.Windows.Forms.Padding(3);
        this.label17.Name = "label17";
        this.label17.Size = new System.Drawing.Size(139, 16);
        this.label17.TabIndex = 11;
        this.label17.Text = "000000";
        // 
        // label16
        // 
        this.label16.Location = new System.Drawing.Point(151, 127);
        this.label16.Margin = new System.Windows.Forms.Padding(3);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(139, 16);
        this.label16.TabIndex = 10;
        this.label16.Text = "000000";
        // 
        // label15
        // 
        this.label15.Location = new System.Drawing.Point(151, 105);
        this.label15.Margin = new System.Windows.Forms.Padding(3);
        this.label15.Name = "label15";
        this.label15.Size = new System.Drawing.Size(139, 16);
        this.label15.TabIndex = 9;
        this.label15.Text = "000000";
        // 
        // label14
        // 
        this.label14.Location = new System.Drawing.Point(151, 83);
        this.label14.Margin = new System.Windows.Forms.Padding(3);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(139, 16);
        this.label14.TabIndex = 8;
        this.label14.Text = "000000";
        // 
        // label13
        // 
        this.label13.Location = new System.Drawing.Point(151, 61);
        this.label13.Margin = new System.Windows.Forms.Padding(3);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(139, 16);
        this.label13.TabIndex = 7;
        this.label13.Text = "000000";
        // 
        // label12
        // 
        this.label12.Location = new System.Drawing.Point(6, 149);
        this.label12.Margin = new System.Windows.Forms.Padding(3);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(139, 16);
        this.label12.TabIndex = 6;
        this.label12.Text = "Jumlah Keluar :";
        this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
        // 
        // label11
        // 
        this.label11.Location = new System.Drawing.Point(6, 127);
        this.label11.Margin = new System.Windows.Forms.Padding(3);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(139, 16);
        this.label11.TabIndex = 5;
        this.label11.Text = "Jumlah Masuk :";
        this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
        // 
        // label10
        // 
        this.label10.Location = new System.Drawing.Point(6, 105);
        this.label10.Margin = new System.Windows.Forms.Padding(3);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(139, 16);
        this.label10.TabIndex = 4;
        this.label10.Text = "+";
        this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
        // 
        // label9
        // 
        this.label9.Location = new System.Drawing.Point(6, 83);
        this.label9.Margin = new System.Windows.Forms.Padding(3);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(139, 16);
        this.label9.TabIndex = 3;
        this.label9.Text = "Total Penjualan (Rp) :";
        this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
        // 
        // label8
        // 
        this.label8.Location = new System.Drawing.Point(6, 61);
        this.label8.Margin = new System.Windows.Forms.Padding(3);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(139, 16);
        this.label8.TabIndex = 2;
        this.label8.Text = "Total Pembelian (Rp) :";
        this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
        // 
        // checkBox2
        // 
        this.checkBox2.Location = new System.Drawing.Point(175, 19);
        this.checkBox2.Name = "checkBox2";
        this.checkBox2.Size = new System.Drawing.Size(115, 23);
        this.checkBox2.TabIndex = 1;
        this.checkBox2.Text = "Tampilkan Data";
        this.checkBox2.UseVisualStyleBackColor = true;
        // 
        // checkBox1
        // 
        this.checkBox1.Location = new System.Drawing.Point(6, 19);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(147, 23);
        this.checkBox1.TabIndex = 0;
        this.checkBox1.Text = "Warnai Transaksi Lunas";
        this.checkBox1.UseVisualStyleBackColor = true;
        // 
        // groupBox2
        // 
        this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox2.Controls.Add(this.label6);
        this.groupBox2.Controls.Add(this.listBoxStok);
        this.groupBox2.Controls.Add(this.listBoxGudang);
        this.groupBox2.Controls.Add(this.button7);
        this.groupBox2.Controls.Add(this.button6);
        this.groupBox2.Controls.Add(this.button5);
        this.groupBox2.Location = new System.Drawing.Point(3, 3);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(316, 426);
        this.groupBox2.TabIndex = 0;
        this.groupBox2.TabStop = false;
        // 
        // label6
        // 
        this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label6.Location = new System.Drawing.Point(157, 404);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(153, 19);
        this.label6.TabIndex = 5;
        this.label6.Text = "Total Stok : 0";
        // 
        // listBoxStok
        // 
        this.listBoxStok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
        this.listBoxStok.DisplayMember = "Text";
        this.listBoxStok.FormattingEnabled = true;
        this.listBoxStok.Location = new System.Drawing.Point(157, 55);
        this.listBoxStok.Name = "listBoxStok";
        this.listBoxStok.Size = new System.Drawing.Size(153, 342);
        this.listBoxStok.TabIndex = 4;
        this.listBoxStok.ValueMember = "Kode";
        // 
        // listBoxGudang
        // 
        this.listBoxGudang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
        this.listBoxGudang.DisplayMember = "Nama";
        this.listBoxGudang.FormattingEnabled = true;
        this.listBoxGudang.Location = new System.Drawing.Point(6, 55);
        this.listBoxGudang.Name = "listBoxGudang";
        this.listBoxGudang.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        this.listBoxGudang.Size = new System.Drawing.Size(145, 342);
        this.listBoxGudang.TabIndex = 3;
        this.listBoxGudang.ValueMember = "Kode";
        // 
        // button7
        // 
        this.button7.Location = new System.Drawing.Point(198, 19);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(80, 30);
        this.button7.TabIndex = 2;
        this.button7.Text = "Cetak";
        this.button7.UseVisualStyleBackColor = true;
        this.button7.Click += new System.EventHandler(this.button7_Click);
        // 
        // button6
        // 
        this.button6.Location = new System.Drawing.Point(96, 19);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(96, 30);
        this.button6.TabIndex = 1;
        this.button6.Text = "Lihat Daftar Stok";
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(this.button6_Click);
        // 
        // button5
        // 
        this.button5.Location = new System.Drawing.Point(6, 19);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(84, 30);
        this.button5.TabIndex = 0;
        this.button5.Text = "Pilih Semua";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(this.button5_Click);
        // 
        // MutasiBarangControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.Controls.Add(this.splitContainer1);
        this.Location = new System.Drawing.Point(15, 15);
        this.Name = "MutasiBarangControl";
        this.Size = new System.Drawing.Size(793, 691);
        this.Load += new System.EventHandler(this.MutasiBarangControl_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
        this.splitContainer1.ResumeLayout(false);
        this.groupBox3.ResumeLayout(false);
        this.groupBox2.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Button button8;
    private System.Windows.Forms.Button button9;

    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Label label17;

    private System.Windows.Forms.Label label13;

    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;

    private System.Windows.Forms.Label label10;

    private System.Windows.Forms.Label label9;

    private System.Windows.Forms.Label label8;

    private System.Windows.Forms.CheckBox checkBox2;

    private System.Windows.Forms.CheckBox checkBox1;

    private System.Windows.Forms.GroupBox groupBox3;

    private System.Windows.Forms.Label label6;

    public System.Windows.Forms.ListBox listBoxStok;

    public System.Windows.Forms.ListBox listBoxGudang;

    private System.Windows.Forms.SplitContainer splitContainer1;

    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Button button4;

    private System.Windows.Forms.Button button3;

    private System.Windows.Forms.DataGridView dataGridView1;

    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.GroupBox groupBox1;

    private System.Windows.Forms.TextBox textBox1;

    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.ComboBox comboBox1;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.CheckBox checkboxDatalama;

    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.ComboBox comboMastergudang;

    private System.Windows.Forms.Label label7;
    public System.Windows.Forms.ComboBox comboMasterbarang;

    #endregion

    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button button7;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Button button5;
}