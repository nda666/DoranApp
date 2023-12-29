using System.ComponentModel;

namespace DoranApp.View;

partial class LokasiprovinsiForm
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
        this.textBoxNama = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.button1 = new System.Windows.Forms.Button();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.Kode = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Nama = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        this.SuspendLayout();
        // 
        // textBoxNama
        // 
        this.textBoxNama.Location = new System.Drawing.Point(115, 9);
        this.textBoxNama.Name = "textBoxNama";
        this.textBoxNama.Size = new System.Drawing.Size(276, 20);
        this.textBoxNama.TabIndex = 0;
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(97, 17);
        this.label1.TabIndex = 1;
        this.label1.Text = "Nama Provinsi :";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(115, 35);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(101, 27);
        this.button1.TabIndex = 2;
        this.button1.Tag = "actionButton";
        this.button1.Text = "Tambah Baru";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // dataGridView1
        // 
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.Kode, this.Nama });
        this.dataGridView1.Location = new System.Drawing.Point(12, 68);
        this.dataGridView1.MultiSelect = false;
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.ReadOnly = true;
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.Size = new System.Drawing.Size(547, 225);
        this.dataGridView1.TabIndex = 21;
        this.dataGridView1.TabStop = false;
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
        this.Nama.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        this.Nama.DataPropertyName = "Nama";
        this.Nama.HeaderText = "Nama";
        this.Nama.Name = "Nama";
        this.Nama.ReadOnly = true;
        // 
        // LokasiprovinsiForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(571, 305);
        this.Controls.Add(this.dataGridView1);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.textBoxNama);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "LokasiprovinsiForm";
        this.Text = "Master Provinsi";
        this.Load += new System.EventHandler(this.LokasiprovinsiForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.DataGridViewTextBoxColumn Kode;
    private System.Windows.Forms.DataGridViewTextBoxColumn Nama;

    private System.Windows.Forms.DataGridView dataGridView1;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.TextBox textBoxNama;
    private System.Windows.Forms.Label label1;

    #endregion
}