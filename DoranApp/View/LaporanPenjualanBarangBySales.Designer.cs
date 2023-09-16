using System.ComponentModel;

namespace DoranApp.View;

partial class LaporanPenjualanBarangBySales
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
        this.components = new System.ComponentModel.Container();
        this.groupBoxShowMode = new System.Windows.Forms.GroupBox();
        this.radioButton2 = new System.Windows.Forms.RadioButton();
        this.radioButton3 = new System.Windows.Forms.RadioButton();
        this.groupBoxJurnalPenjualan = new System.Windows.Forms.GroupBox();
        this.radioButton4 = new System.Windows.Forms.RadioButton();
        this.radioButton5 = new System.Windows.Forms.RadioButton();
        this.radioButton6 = new System.Windows.Forms.RadioButton();
        this.comboFilterMasterchannelsales = new System.Windows.Forms.ComboBox();
        this.label7 = new System.Windows.Forms.Label();
        this.comboFilterMastertimsales = new System.Windows.Forms.ComboBox();
        this.comboFilterHkategoribarang = new System.Windows.Forms.ComboBox();
        this.comboFilterDkategoribarang = new System.Windows.Forms.ComboBox();
        this.textBoxFilterNamaBarang = new System.Windows.Forms.TextBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.datePickerFilterMax = new System.Windows.Forms.DateTimePicker();
        this.datePickerFilterMin = new System.Windows.Forms.DateTimePicker();
        this.label2 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
        this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
        this.labelJumlahSum = new System.Windows.Forms.ToolStripLabel();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
        this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.labelLoading = new System.Windows.Forms.ToolStripLabel();
        this.button1 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.panel1 = new System.Windows.Forms.Panel();
        this.groupBoxShowMode.SuspendLayout();
        this.groupBoxJurnalPenjualan.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
        this.bindingNavigator1.SuspendLayout();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupBoxShowMode
        // 
        this.groupBoxShowMode.Controls.Add(this.radioButton2);
        this.groupBoxShowMode.Controls.Add(this.radioButton3);
        this.groupBoxShowMode.Location = new System.Drawing.Point(358, 38);
        this.groupBoxShowMode.Name = "groupBoxShowMode";
        this.groupBoxShowMode.Size = new System.Drawing.Size(143, 45);
        this.groupBoxShowMode.TabIndex = 113;
        this.groupBoxShowMode.TabStop = false;
        this.groupBoxShowMode.Text = "Tampilkan By";
        // 
        // radioButton2
        // 
        this.radioButton2.AutoSize = true;
        this.radioButton2.Location = new System.Drawing.Point(90, 19);
        this.radioButton2.Name = "radioButton2";
        this.radioButton2.Size = new System.Drawing.Size(43, 17);
        this.radioButton2.TabIndex = 1;
        this.radioButton2.Tag = "1";
        this.radioButton2.Text = "Pcs";
        this.radioButton2.UseVisualStyleBackColor = true;
        // 
        // radioButton3
        // 
        this.radioButton3.AutoSize = true;
        this.radioButton3.Checked = true;
        this.radioButton3.Location = new System.Drawing.Point(7, 20);
        this.radioButton3.Name = "radioButton3";
        this.radioButton3.Size = new System.Drawing.Size(55, 17);
        this.radioButton3.TabIndex = 0;
        this.radioButton3.TabStop = true;
        this.radioButton3.Tag = "0";
        this.radioButton3.Text = "Omzet";
        this.radioButton3.UseVisualStyleBackColor = true;
        // 
        // groupBoxJurnalPenjualan
        // 
        this.groupBoxJurnalPenjualan.Controls.Add(this.radioButton4);
        this.groupBoxJurnalPenjualan.Controls.Add(this.radioButton5);
        this.groupBoxJurnalPenjualan.Controls.Add(this.radioButton6);
        this.groupBoxJurnalPenjualan.Location = new System.Drawing.Point(358, 88);
        this.groupBoxJurnalPenjualan.Name = "groupBoxJurnalPenjualan";
        this.groupBoxJurnalPenjualan.Size = new System.Drawing.Size(101, 96);
        this.groupBoxJurnalPenjualan.TabIndex = 111;
        this.groupBoxJurnalPenjualan.TabStop = false;
        this.groupBoxJurnalPenjualan.Text = "Jurnal Penjualan";
        // 
        // radioButton4
        // 
        this.radioButton4.AutoSize = true;
        this.radioButton4.Location = new System.Drawing.Point(6, 66);
        this.radioButton4.Name = "radioButton4";
        this.radioButton4.Size = new System.Drawing.Size(52, 17);
        this.radioButton4.TabIndex = 2;
        this.radioButton4.Tag = "0";
        this.radioButton4.Text = "Tidak";
        this.radioButton4.UseVisualStyleBackColor = true;
        // 
        // radioButton5
        // 
        this.radioButton5.AutoSize = true;
        this.radioButton5.Location = new System.Drawing.Point(7, 43);
        this.radioButton5.Name = "radioButton5";
        this.radioButton5.Size = new System.Drawing.Size(38, 17);
        this.radioButton5.TabIndex = 1;
        this.radioButton5.Tag = "1";
        this.radioButton5.Text = "Ya";
        this.radioButton5.UseVisualStyleBackColor = true;
        // 
        // radioButton6
        // 
        this.radioButton6.AutoSize = true;
        this.radioButton6.Checked = true;
        this.radioButton6.Location = new System.Drawing.Point(7, 20);
        this.radioButton6.Name = "radioButton6";
        this.radioButton6.Size = new System.Drawing.Size(58, 17);
        this.radioButton6.TabIndex = 0;
        this.radioButton6.TabStop = true;
        this.radioButton6.Text = "Semua";
        this.radioButton6.UseVisualStyleBackColor = true;
        // 
        // comboFilterMasterchannelsales
        // 
        this.comboFilterMasterchannelsales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterMasterchannelsales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterMasterchannelsales.DisplayMember = "Nama";
        this.comboFilterMasterchannelsales.FormattingEnabled = true;
        this.comboFilterMasterchannelsales.Location = new System.Drawing.Point(230, 194);
        this.comboFilterMasterchannelsales.Name = "comboFilterMasterchannelsales";
        this.comboFilterMasterchannelsales.Size = new System.Drawing.Size(121, 21);
        this.comboFilterMasterchannelsales.TabIndex = 104;
        this.comboFilterMasterchannelsales.Text = "Semua Channel Sales";
        this.comboFilterMasterchannelsales.ValueMember = "Kode";
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label7.Location = new System.Drawing.Point(15, 193);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(96, 21);
        this.label7.TabIndex = 103;
        this.label7.Text = "Sales :";
        this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboFilterMastertimsales
        // 
        this.comboFilterMastertimsales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterMastertimsales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterMastertimsales.DisplayMember = "Nama";
        this.comboFilterMastertimsales.FormattingEnabled = true;
        this.comboFilterMastertimsales.Location = new System.Drawing.Point(117, 194);
        this.comboFilterMastertimsales.Name = "comboFilterMastertimsales";
        this.comboFilterMastertimsales.Size = new System.Drawing.Size(107, 21);
        this.comboFilterMastertimsales.TabIndex = 102;
        this.comboFilterMastertimsales.Text = "Semua Tim Sales";
        this.comboFilterMastertimsales.ValueMember = "Kode";
        // 
        // comboFilterHkategoribarang
        // 
        this.comboFilterHkategoribarang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterHkategoribarang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterHkategoribarang.DisplayMember = "Nama";
        this.comboFilterHkategoribarang.FormattingEnabled = true;
        this.comboFilterHkategoribarang.Location = new System.Drawing.Point(242, 167);
        this.comboFilterHkategoribarang.Name = "comboFilterHkategoribarang";
        this.comboFilterHkategoribarang.Size = new System.Drawing.Size(109, 21);
        this.comboFilterHkategoribarang.TabIndex = 99;
        this.comboFilterHkategoribarang.Text = "Semua Brand";
        this.comboFilterHkategoribarang.ValueMember = "Kodeh";
        // 
        // comboFilterDkategoribarang
        // 
        this.comboFilterDkategoribarang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboFilterDkategoribarang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboFilterDkategoribarang.DisplayMember = "Nama";
        this.comboFilterDkategoribarang.FormattingEnabled = true;
        this.comboFilterDkategoribarang.Location = new System.Drawing.Point(117, 167);
        this.comboFilterDkategoribarang.Name = "comboFilterDkategoribarang";
        this.comboFilterDkategoribarang.Size = new System.Drawing.Size(119, 21);
        this.comboFilterDkategoribarang.TabIndex = 98;
        this.comboFilterDkategoribarang.Text = "Semua Sub Brand";
        this.comboFilterDkategoribarang.ValueMember = "Koded";
        // 
        // textBoxFilterNamaBarang
        // 
        this.textBoxFilterNamaBarang.Location = new System.Drawing.Point(117, 37);
        this.textBoxFilterNamaBarang.Name = "textBoxFilterNamaBarang";
        this.textBoxFilterNamaBarang.Size = new System.Drawing.Size(234, 20);
        this.textBoxFilterNamaBarang.TabIndex = 97;
        // 
        // label4
        // 
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(15, 36);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(96, 21);
        this.label4.TabIndex = 96;
        this.label4.Text = "Nama Brg. :";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label8
        // 
        this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label8.Location = new System.Drawing.Point(12, 9);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(99, 21);
        this.label8.TabIndex = 95;
        this.label8.Text = "Tgl Trans. [F1] :";
        this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(220, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(29, 21);
        this.label1.TabIndex = 94;
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
        this.datePickerFilterMax.TabIndex = 93;
        // 
        // datePickerFilterMin
        // 
        this.datePickerFilterMin.CustomFormat = "dd/MM/yyyy";
        this.datePickerFilterMin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.datePickerFilterMin.Location = new System.Drawing.Point(117, 10);
        this.datePickerFilterMin.Name = "datePickerFilterMin";
        this.datePickerFilterMin.Size = new System.Drawing.Size(97, 20);
        this.datePickerFilterMin.TabIndex = 92;
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(15, 166);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(96, 21);
        this.label2.TabIndex = 114;
        this.label2.Text = "Kategori :";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(117, 63);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(234, 20);
        this.textBox1.TabIndex = 116;
        // 
        // label3
        // 
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(15, 62);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(96, 21);
        this.label3.TabIndex = 115;
        this.label3.Text = "OR :";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // textBox2
        // 
        this.textBox2.Location = new System.Drawing.Point(117, 89);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(234, 20);
        this.textBox2.TabIndex = 118;
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(15, 88);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(96, 21);
        this.label5.TabIndex = 117;
        this.label5.Text = "OR :";
        this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // textBox3
        // 
        this.textBox3.Location = new System.Drawing.Point(117, 115);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(234, 20);
        this.textBox3.TabIndex = 120;
        // 
        // label6
        // 
        this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label6.Location = new System.Drawing.Point(15, 114);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(96, 21);
        this.label6.TabIndex = 119;
        this.label6.Text = "OR :";
        this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // textBox4
        // 
        this.textBox4.Location = new System.Drawing.Point(117, 141);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(234, 20);
        this.textBox4.TabIndex = 122;
        // 
        // label9
        // 
        this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label9.Location = new System.Drawing.Point(15, 140);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(96, 21);
        this.label9.TabIndex = 121;
        this.label9.Text = "OR :";
        this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // dataGridView1
        // 
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeColumns = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Location = new System.Drawing.Point(12, 262);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.Size = new System.Drawing.Size(657, 70);
        this.dataGridView1.TabIndex = 123;
        this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
        // 
        // bindingNavigator1
        // 
        this.bindingNavigator1.AddNewItem = null;
        this.bindingNavigator1.AllowMerge = false;
        this.bindingNavigator1.AutoSize = false;
        this.bindingNavigator1.BackColor = System.Drawing.SystemColors.Control;
        this.bindingNavigator1.CountItem = this.toolStripLabel1;
        this.bindingNavigator1.CountItemFormat = "Varian: {0}";
        this.bindingNavigator1.DeleteItem = null;
        this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.bindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
        this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.labelJumlahSum, this.toolStripSeparator1, this.toolStripLabel2, this.toolStripSeparator3, this.toolStripLabel1, this.toolStripSeparator2, this.labelLoading });
        this.bindingNavigator1.Location = new System.Drawing.Point(0, 337);
        this.bindingNavigator1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
        this.bindingNavigator1.MoveFirstItem = null;
        this.bindingNavigator1.MoveLastItem = null;
        this.bindingNavigator1.MoveNextItem = null;
        this.bindingNavigator1.MovePreviousItem = null;
        this.bindingNavigator1.Name = "bindingNavigator1";
        this.bindingNavigator1.PositionItem = null;
        this.bindingNavigator1.Size = new System.Drawing.Size(681, 40);
        this.bindingNavigator1.TabIndex = 124;
        this.bindingNavigator1.Text = "bindingNavigator1";
        // 
        // toolStripLabel1
        // 
        this.toolStripLabel1.AutoSize = false;
        this.toolStripLabel1.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.toolStripLabel1.Name = "toolStripLabel1";
        this.toolStripLabel1.Size = new System.Drawing.Size(150, 37);
        this.toolStripLabel1.Text = "Varian: {0}";
        this.toolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // labelJumlahSum
        // 
        this.labelJumlahSum.AutoSize = false;
        this.labelJumlahSum.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.labelJumlahSum.Name = "labelJumlahSum";
        this.labelJumlahSum.Size = new System.Drawing.Size(100, 37);
        this.labelJumlahSum.Text = "Total: 0";
        this.labelJumlahSum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // toolStripSeparator1
        // 
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
        // 
        // toolStripLabel2
        // 
        this.toolStripLabel2.AutoSize = false;
        this.toolStripLabel2.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.toolStripLabel2.Name = "toolStripLabel2";
        this.toolStripLabel2.Size = new System.Drawing.Size(100, 37);
        this.toolStripLabel2.Text = "Total: 0";
        this.toolStripLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // toolStripSeparator3
        // 
        this.toolStripSeparator3.Name = "toolStripSeparator3";
        this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
        // 
        // toolStripSeparator2
        // 
        this.toolStripSeparator2.Name = "toolStripSeparator2";
        this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
        // 
        // labelLoading
        // 
        this.labelLoading.Name = "labelLoading";
        this.labelLoading.Size = new System.Drawing.Size(96, 37);
        this.labelLoading.Text = "Mohon tunggu...";
        this.labelLoading.Visible = false;
        // 
        // button1
        // 
        this.button1.Dock = System.Windows.Forms.DockStyle.Left;
        this.button1.Location = new System.Drawing.Point(0, 0);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(107, 36);
        this.button1.TabIndex = 125;
        this.button1.Text = "Cari";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // button3
        // 
        this.button3.Dock = System.Windows.Forms.DockStyle.Right;
        this.button3.Location = new System.Drawing.Point(113, 0);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(121, 36);
        this.button3.TabIndex = 127;
        this.button3.Tag = "";
        this.button3.Text = "Grup By Channel";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(this.button3_Click);
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.button1);
        this.panel1.Controls.Add(this.button3);
        this.panel1.Location = new System.Drawing.Point(117, 220);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(234, 36);
        this.panel1.TabIndex = 128;
        // 
        // LaporanPenjualanBarangBySales
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(681, 377);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.bindingNavigator1);
        this.Controls.Add(this.dataGridView1);
        this.Controls.Add(this.textBox4);
        this.Controls.Add(this.label9);
        this.Controls.Add(this.textBox3);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.textBox2);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.groupBoxShowMode);
        this.Controls.Add(this.groupBoxJurnalPenjualan);
        this.Controls.Add(this.comboFilterMasterchannelsales);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.comboFilterMastertimsales);
        this.Controls.Add(this.comboFilterHkategoribarang);
        this.Controls.Add(this.comboFilterDkategoribarang);
        this.Controls.Add(this.textBoxFilterNamaBarang);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label8);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.datePickerFilterMax);
        this.Controls.Add(this.datePickerFilterMin);
        this.Name = "LaporanPenjualanBarangBySales";
        this.Text = "LaporanPenjualanBarangBySales";
        this.Load += new System.EventHandler(this.LaporanPenjualanBarangBySales_Load);
        this.groupBoxShowMode.ResumeLayout(false);
        this.groupBoxShowMode.PerformLayout();
        this.groupBoxJurnalPenjualan.ResumeLayout(false);
        this.groupBoxJurnalPenjualan.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
        this.bindingNavigator1.ResumeLayout(false);
        this.bindingNavigator1.PerformLayout();
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Panel panel1;

    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button3;

    private System.Windows.Forms.BindingNavigator bindingNavigator1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripLabel labelJumlahSum;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripLabel labelLoading;

    private System.Windows.Forms.DataGridView dataGridView1;

    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.Label label9;

    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBox1;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.GroupBox groupBoxShowMode;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.GroupBox groupBoxJurnalPenjualan;
    private System.Windows.Forms.RadioButton radioButton4;
    private System.Windows.Forms.RadioButton radioButton5;
    private System.Windows.Forms.RadioButton radioButton6;
    private System.Windows.Forms.ComboBox comboFilterMasterchannelsales;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox comboFilterMastertimsales;
    private System.Windows.Forms.ComboBox comboFilterHkategoribarang;
    private System.Windows.Forms.ComboBox comboFilterDkategoribarang;
    private System.Windows.Forms.TextBox textBoxFilterNamaBarang;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DateTimePicker datePickerFilterMax;
    private System.Windows.Forms.DateTimePicker datePickerFilterMin;

    #endregion
}