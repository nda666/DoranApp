using System.ComponentModel;

namespace DoranApp.View.LaporanTransaksiPenjualan;

partial class ByTokoForm
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.JumlahNya = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Limitnya = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Blok = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Salespemilik = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.NamaSales = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Untungnya = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.PersenUntung = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.KodePelanggan = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.button1 = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.comboSales = new System.Windows.Forms.ComboBox();
        this.button2 = new System.Windows.Forms.Button();
        this.BTN_SetSales = new System.Windows.Forms.Button();
        this.buttonBlok = new System.Windows.Forms.Button();
        this.buttonAturPelg = new System.Windows.Forms.Button();
        this.PANEL_Footer = new System.Windows.Forms.Panel();
        this.checkBoxWarnaiTokoBlok = new System.Windows.Forms.CheckBox();
        this.checkBoxCetakEmail = new System.Windows.Forms.CheckBox();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        this.PANEL_Footer.SuspendLayout();
        this.SuspendLayout();
        // 
        // dataGridView1
        // 
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeColumns = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Nama, this.JumlahNya, this.Limitnya, this.Blok, this.Salespemilik, this.NamaSales, this.Email, this.Untungnya, this.PersenUntung, this.KodePelanggan });
        dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
        this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
        this.dataGridView1.Location = new System.Drawing.Point(12, 12);
        this.dataGridView1.MultiSelect = false;
        this.dataGridView1.Name = "dataGridView1";
        dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.Size = new System.Drawing.Size(746, 326);
        this.dataGridView1.TabIndex = 3;
        this.dataGridView1.VirtualMode = true;
        this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
        this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
        // 
        // Nama
        // 
        this.Nama.DataPropertyName = "Nama";
        this.Nama.HeaderText = "Nama";
        this.Nama.Name = "Nama";
        this.Nama.Width = 200;
        // 
        // JumlahNya
        // 
        this.JumlahNya.DataPropertyName = "JumlahNya";
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
        dataGridViewCellStyle2.Format = "N0";
        dataGridViewCellStyle2.NullValue = "0";
        this.JumlahNya.DefaultCellStyle = dataGridViewCellStyle2;
        this.JumlahNya.HeaderText = "Total";
        this.JumlahNya.Name = "JumlahNya";
        // 
        // Limitnya
        // 
        this.Limitnya.DataPropertyName = "Limitnya";
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
        dataGridViewCellStyle3.Format = "N0";
        this.Limitnya.DefaultCellStyle = dataGridViewCellStyle3;
        this.Limitnya.HeaderText = "Limit";
        this.Limitnya.Name = "Limitnya";
        // 
        // Blok
        // 
        this.Blok.DataPropertyName = "Blok";
        this.Blok.HeaderText = "Blok";
        this.Blok.Name = "Blok";
        this.Blok.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.Blok.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // Salespemilik
        // 
        this.Salespemilik.DataPropertyName = "SalesPemilik";
        this.Salespemilik.HeaderText = "Sales";
        this.Salespemilik.Name = "Salespemilik";
        this.Salespemilik.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.Salespemilik.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // NamaSales
        // 
        this.NamaSales.DataPropertyName = "NamaSales";
        this.NamaSales.HeaderText = "Nama Sales Peml";
        this.NamaSales.Name = "NamaSales";
        // 
        // Email
        // 
        this.Email.DataPropertyName = "Email";
        this.Email.HeaderText = "Email";
        this.Email.Name = "Email";
        // 
        // Untungnya
        // 
        this.Untungnya.DataPropertyName = "Untungnya";
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
        dataGridViewCellStyle4.Format = "N0";
        this.Untungnya.DefaultCellStyle = dataGridViewCellStyle4;
        this.Untungnya.HeaderText = "Profit";
        this.Untungnya.Name = "Untungnya";
        this.Untungnya.Visible = false;
        // 
        // PersenUntung
        // 
        this.PersenUntung.DataPropertyName = "PersenUntung";
        dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
        this.PersenUntung.DefaultCellStyle = dataGridViewCellStyle5;
        this.PersenUntung.HeaderText = "% Profit";
        this.PersenUntung.Name = "PersenUntung";
        this.PersenUntung.Visible = false;
        // 
        // KodePelanggan
        // 
        this.KodePelanggan.DataPropertyName = "KodePelanggan";
        this.KodePelanggan.HeaderText = "KodePelanggan";
        this.KodePelanggan.Name = "KodePelanggan";
        this.KodePelanggan.Visible = false;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(6, 3);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(120, 34);
        this.button1.TabIndex = 4;
        this.button1.Text = "Export Ke Excel";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(8, 43);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(50, 23);
        this.label1.TabIndex = 5;
        this.label1.Text = "Sales :";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboSales
        // 
        this.comboSales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
        this.comboSales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboSales.FormattingEnabled = true;
        this.comboSales.Location = new System.Drawing.Point(64, 45);
        this.comboSales.Name = "comboSales";
        this.comboSales.Size = new System.Drawing.Size(147, 21);
        this.comboSales.TabIndex = 6;
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(217, 43);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(120, 34);
        this.button2.TabIndex = 7;
        this.button2.Text = "Email Ke Salesnya";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // BTN_SetSales
        // 
        this.BTN_SetSales.Enabled = false;
        this.BTN_SetSales.Location = new System.Drawing.Point(343, 43);
        this.BTN_SetSales.Name = "BTN_SetSales";
        this.BTN_SetSales.Size = new System.Drawing.Size(81, 34);
        this.BTN_SetSales.TabIndex = 8;
        this.BTN_SetSales.Text = "Set Sales";
        this.BTN_SetSales.UseVisualStyleBackColor = true;
        // 
        // buttonBlok
        // 
        this.buttonBlok.Enabled = false;
        this.buttonBlok.Location = new System.Drawing.Point(430, 43);
        this.buttonBlok.Name = "buttonBlok";
        this.buttonBlok.Size = new System.Drawing.Size(81, 34);
        this.buttonBlok.TabIndex = 9;
        this.buttonBlok.Text = "BLOK";
        this.buttonBlok.UseVisualStyleBackColor = true;
        this.buttonBlok.Click += new System.EventHandler(this.buttonBlok_Click);
        // 
        // buttonAturPelg
        // 
        this.buttonAturPelg.Enabled = false;
        this.buttonAturPelg.Location = new System.Drawing.Point(517, 43);
        this.buttonAturPelg.Name = "buttonAturPelg";
        this.buttonAturPelg.Size = new System.Drawing.Size(97, 34);
        this.buttonAturPelg.TabIndex = 10;
        this.buttonAturPelg.Text = "Atur Pelg";
        this.buttonAturPelg.UseVisualStyleBackColor = true;
        this.buttonAturPelg.Click += new System.EventHandler(this.buttonAturPelg_Click);
        // 
        // PANEL_Footer
        // 
        this.PANEL_Footer.Controls.Add(this.checkBoxWarnaiTokoBlok);
        this.PANEL_Footer.Controls.Add(this.checkBoxCetakEmail);
        this.PANEL_Footer.Controls.Add(this.button1);
        this.PANEL_Footer.Controls.Add(this.buttonAturPelg);
        this.PANEL_Footer.Controls.Add(this.label1);
        this.PANEL_Footer.Controls.Add(this.buttonBlok);
        this.PANEL_Footer.Controls.Add(this.comboSales);
        this.PANEL_Footer.Controls.Add(this.BTN_SetSales);
        this.PANEL_Footer.Controls.Add(this.button2);
        this.PANEL_Footer.Location = new System.Drawing.Point(12, 344);
        this.PANEL_Footer.Name = "PANEL_Footer";
        this.PANEL_Footer.Size = new System.Drawing.Size(746, 86);
        this.PANEL_Footer.TabIndex = 11;
        this.PANEL_Footer.Visible = false;
        // 
        // checkBoxWarnaiTokoBlok
        // 
        this.checkBoxWarnaiTokoBlok.Location = new System.Drawing.Point(242, 9);
        this.checkBoxWarnaiTokoBlok.Name = "checkBoxWarnaiTokoBlok";
        this.checkBoxWarnaiTokoBlok.Size = new System.Drawing.Size(131, 24);
        this.checkBoxWarnaiTokoBlok.TabIndex = 12;
        this.checkBoxWarnaiTokoBlok.Text = "Warnai Toko Blok";
        this.checkBoxWarnaiTokoBlok.UseVisualStyleBackColor = true;
        // 
        // checkBoxCetakEmail
        // 
        this.checkBoxCetakEmail.Location = new System.Drawing.Point(132, 9);
        this.checkBoxCetakEmail.Name = "checkBoxCetakEmail";
        this.checkBoxCetakEmail.Size = new System.Drawing.Size(104, 24);
        this.checkBoxCetakEmail.TabIndex = 11;
        this.checkBoxCetakEmail.Text = "Cetak Email";
        this.checkBoxCetakEmail.UseVisualStyleBackColor = true;
        // 
        // ByTokoForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(770, 435);
        this.Controls.Add(this.PANEL_Footer);
        this.Controls.Add(this.dataGridView1);
        this.KeyPreview = true;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "ByTokoForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Top Omzet Penjualan By Toko";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ByTokoForm_FormClosing);
        this.Load += new System.EventHandler(this.ByTokoForm_Load);
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ByTokoForm_KeyDown);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        this.PANEL_Footer.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.DataGridViewTextBoxColumn KodePelanggan;

    private System.Windows.Forms.DataGridViewTextBoxColumn NamaSales;

    private System.Windows.Forms.CheckBox checkBoxWarnaiTokoBlok;

    private System.Windows.Forms.CheckBox checkBoxCetakEmail;

    private System.Windows.Forms.Panel PANEL_Footer;

    private System.Windows.Forms.DataGridViewTextBoxColumn JumlahNya;
    private System.Windows.Forms.DataGridViewTextBoxColumn Limitnya;
    private System.Windows.Forms.DataGridViewTextBoxColumn Blok;
    private System.Windows.Forms.DataGridViewTextBoxColumn Salespemilik;
    private System.Windows.Forms.DataGridViewTextBoxColumn Email;
    private System.Windows.Forms.DataGridViewTextBoxColumn Untungnya;
    private System.Windows.Forms.DataGridViewTextBoxColumn PersenUntung;

    private System.Windows.Forms.DataGridViewTextBoxColumn Nama;

    private System.Windows.Forms.Button buttonAturPelg;

    private System.Windows.Forms.Button buttonBlok;

    private System.Windows.Forms.Button BTN_SetSales;

    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.ComboBox comboSales;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.DataGridView dataGridView1;

    #endregion
}