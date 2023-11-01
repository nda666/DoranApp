using System.ComponentModel;

namespace DoranApp.View.CekStok;

partial class HistoryBarangMasukControl
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
        this.comboSupplier = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
        this.currencyTextBox1 = new DoranApp.Components.CurrencyTextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.currencyTextBox2 = new DoranApp.Components.CurrencyTextBox();
        this.SuspendLayout();
        // 
        // label7
        // 
        this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label7.Location = new System.Drawing.Point(3, 3);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(140, 21);
        this.label7.TabIndex = 77;
        this.label7.Text = "Nama Barang [F2] :";
        this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboMasterbarang
        // 
        this.comboMasterbarang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboMasterbarang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboMasterbarang.DisplayMember = "BrgNama";
        this.comboMasterbarang.FormattingEnabled = true;
        this.comboMasterbarang.Location = new System.Drawing.Point(149, 4);
        this.comboMasterbarang.Name = "comboMasterbarang";
        this.comboMasterbarang.Size = new System.Drawing.Size(308, 21);
        this.comboMasterbarang.TabIndex = 76;
        this.comboMasterbarang.Text = "Pilih Barang";
        this.comboMasterbarang.ValueMember = "BrgKode";
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(3, 31);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(140, 21);
        this.label1.TabIndex = 79;
        this.label1.Text = "Gudang :";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboSupplier
        // 
        this.comboSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
        this.comboSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboSupplier.DisplayMember = "Nama";
        this.comboSupplier.FormattingEnabled = true;
        this.comboSupplier.Location = new System.Drawing.Point(149, 31);
        this.comboSupplier.Name = "comboSupplier";
        this.comboSupplier.Size = new System.Drawing.Size(148, 21);
        this.comboSupplier.TabIndex = 78;
        this.comboSupplier.Text = "Pilih Gudang";
        this.comboSupplier.ValueMember = "Kode";
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(3, 57);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(140, 21);
        this.label2.TabIndex = 81;
        this.label2.Text = "Tanggal Masuk :";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // dateTimePicker1
        // 
        this.dateTimePicker1.CustomFormat = "d/M/yyyy";
        this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dateTimePicker1.Location = new System.Drawing.Point(149, 58);
        this.dateTimePicker1.Name = "dateTimePicker1";
        this.dateTimePicker1.Size = new System.Drawing.Size(148, 20);
        this.dateTimePicker1.TabIndex = 82;
        // 
        // currencyTextBox1
        // 
        this.currencyTextBox1.Location = new System.Drawing.Point(149, 84);
        this.currencyTextBox1.Name = "currencyTextBox1";
        this.currencyTextBox1.Size = new System.Drawing.Size(148, 20);
        this.currencyTextBox1.TabIndex = 83;
        // 
        // label3
        // 
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(3, 84);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(140, 21);
        this.label3.TabIndex = 84;
        this.label3.Text = "Harga :";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label4
        // 
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(3, 110);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(140, 21);
        this.label4.TabIndex = 86;
        this.label4.Text = "Jumlah Masuk :";
        this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // currencyTextBox2
        // 
        this.currencyTextBox2.Location = new System.Drawing.Point(149, 110);
        this.currencyTextBox2.Name = "currencyTextBox2";
        this.currencyTextBox2.Size = new System.Drawing.Size(106, 20);
        this.currencyTextBox2.TabIndex = 85;
        // 
        // HistoryBarangMasukControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.label4);
        this.Controls.Add(this.currencyTextBox2);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.currencyTextBox1);
        this.Controls.Add(this.dateTimePicker1);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.comboSupplier);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.comboMasterbarang);
        this.Name = "HistoryBarangMasukControl";
        this.Size = new System.Drawing.Size(646, 394);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.Label label4;
    private DoranApp.Components.CurrencyTextBox currencyTextBox2;

    private DoranApp.Components.CurrencyTextBox currencyTextBox1;
    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;

    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.ComboBox comboSupplier;

    private System.Windows.Forms.Label label7;
    public System.Windows.Forms.ComboBox comboMasterbarang;

    #endregion
}