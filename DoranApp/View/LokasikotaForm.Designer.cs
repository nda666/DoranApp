using System.ComponentModel;

namespace DoranApp.View;

partial class LokasikotaForm
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
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.button1 = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.textBoxNama = new System.Windows.Forms.TextBox();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.comboBox2 = new System.Windows.Forms.ComboBox();
        this.button2 = new System.Windows.Forms.Button();
        this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Provinsi = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Coa = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        this.SuspendLayout();
        // 
        // dataGridView1
        // 
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Kode, this.Nama, this.Provinsi, this.Coa });
        this.dataGridView1.Location = new System.Drawing.Point(12, 95);
        this.dataGridView1.MultiSelect = false;
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.ReadOnly = true;
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.Size = new System.Drawing.Size(505, 290);
        this.dataGridView1.TabIndex = 25;
        this.dataGridView1.TabStop = false;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(115, 62);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(101, 27);
        this.button1.TabIndex = 24;
        this.button1.Tag = "actionButton";
        this.button1.Text = "Tambah Baru";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(97, 17);
        this.label1.TabIndex = 23;
        this.label1.Text = "Nama Kota :";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // textBoxNama
        // 
        this.textBoxNama.Location = new System.Drawing.Point(115, 9);
        this.textBoxNama.Name = "textBoxNama";
        this.textBoxNama.Size = new System.Drawing.Size(276, 20);
        this.textBoxNama.TabIndex = 22;
        // 
        // comboBox1
        // 
        this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
        this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboBox1.DisplayMember = "Nama";
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(115, 35);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(276, 21);
        this.comboBox1.TabIndex = 26;
        this.comboBox1.ValueMember = "Kode";
        // 
        // label2
        // 
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(12, 39);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(97, 17);
        this.label2.TabIndex = 27;
        this.label2.Text = "Nama Provinsi :";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // label3
        // 
        this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(12, 405);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(97, 17);
        this.label3.TabIndex = 29;
        this.label3.Text = "Set COA";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // comboBox2
        // 
        this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
        this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
        this.comboBox2.DisplayMember = "Nama";
        this.comboBox2.FormattingEnabled = true;
        this.comboBox2.Location = new System.Drawing.Point(115, 401);
        this.comboBox2.Name = "comboBox2";
        this.comboBox2.Size = new System.Drawing.Size(295, 21);
        this.comboBox2.TabIndex = 28;
        this.comboBox2.ValueMember = "Kode";
        // 
        // button2
        // 
        this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.button2.Location = new System.Drawing.Point(416, 397);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(101, 27);
        this.button2.TabIndex = 30;
        this.button2.Tag = "actionButton";
        this.button2.Text = "Set Coa";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // Kode
        // 
        this.Kode.DataPropertyName = "Kode";
        this.Kode.HeaderText = "Kode";
        this.Kode.Name = "Kode";
        this.Kode.ReadOnly = true;
        this.Kode.Width = 57;
        // 
        // Nama
        // 
        this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
        this.Nama.DataPropertyName = "Nama";
        this.Nama.FillWeight = 200F;
        this.Nama.HeaderText = "Nama";
        this.Nama.Name = "Nama";
        this.Nama.ReadOnly = true;
        // 
        // Provinsi
        // 
        this.Provinsi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
        this.Provinsi.DataPropertyName = "NamaProvinsi";
        this.Provinsi.HeaderText = "Provinsi";
        this.Provinsi.Name = "Provinsi";
        this.Provinsi.ReadOnly = true;
        // 
        // Coa
        // 
        this.Coa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
        this.Coa.DataPropertyName = "Coa";
        this.Coa.HeaderText = "Coa";
        this.Coa.Name = "Coa";
        this.Coa.ReadOnly = true;
        this.Coa.Width = 250;
        // 
        // LokasikotaForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(529, 434);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.comboBox2);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.comboBox1);
        this.Controls.Add(this.dataGridView1);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.textBoxNama);
        this.Location = new System.Drawing.Point(15, 15);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "LokasikotaForm";
        this.Text = "Lokasi Kota";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LokasikotaForm_FormClosing_1);
        this.Load += new System.EventHandler(this.LokasikotaForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.DataGridViewTextBoxColumn Coa;

    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.DataGridViewTextBoxColumn Provinsi;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
    private System.Windows.Forms.DataGridViewTextBoxColumn Nama;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxNama;

    #endregion
}