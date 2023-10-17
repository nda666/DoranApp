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
        this.groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        this.SuspendLayout();
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label7.Location = new System.Drawing.Point(3, 3);
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
        this.comboMasterbarang.Location = new System.Drawing.Point(155, 3);
        this.comboMasterbarang.Name = "comboMasterbarang";
        this.comboMasterbarang.Size = new System.Drawing.Size(308, 21);
        this.comboMasterbarang.TabIndex = 74;
        this.comboMasterbarang.Text = "Pilih Barang";
        this.comboMasterbarang.ValueMember = "BrgKode";
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(3, 30);
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
        this.comboMastergudang.Location = new System.Drawing.Point(155, 30);
        this.comboMastergudang.Name = "comboMastergudang";
        this.comboMastergudang.Size = new System.Drawing.Size(148, 21);
        this.comboMastergudang.TabIndex = 76;
        this.comboMastergudang.Text = "Pilih Gudang";
        this.comboMastergudang.ValueMember = "Kode";
        // 
        // checkboxDatalama
        // 
        this.checkboxDatalama.Location = new System.Drawing.Point(13, 55);
        this.checkboxDatalama.Name = "checkboxDatalama";
        this.checkboxDatalama.Size = new System.Drawing.Size(130, 29);
        this.checkboxDatalama.TabIndex = 78;
        this.checkboxDatalama.Text = "Tampilkan Data Lama";
        this.checkboxDatalama.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.checkboxDatalama.UseVisualStyleBackColor = true;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(155, 57);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(130, 31);
        this.button1.TabIndex = 79;
        this.button1.Text = "Cek Stok [F3]";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(297, 57);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(166, 31);
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
        this.groupBox1.Location = new System.Drawing.Point(0, 126);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(620, 165);
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
        this.dataGridView1.Size = new System.Drawing.Size(607, 84);
        this.dataGridView1.TabIndex = 86;
        this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
        // 
        // label4
        // 
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(240, 102);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(223, 21);
        this.label4.TabIndex = 87;
        this.label4.Text = "Stok :";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label5
        // 
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(17, 102);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(217, 21);
        this.label5.TabIndex = 88;
        this.label5.Text = "Stok Gudang Atas + LT3 :";
        this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // MutasiBarangControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.label5);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.checkboxDatalama);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.comboMastergudang);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.comboMasterbarang);
        this.Name = "MutasiBarangControl";
        this.Size = new System.Drawing.Size(620, 291);
        this.Load += new System.EventHandler(this.MutasiBarangControl_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        this.ResumeLayout(false);
    }

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
}