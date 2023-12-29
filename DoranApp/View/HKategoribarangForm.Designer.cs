namespace DoranApp.View
{
    partial class HKategoribarangForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HKategoribarangForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.checkboxCekTahunan = new System.Windows.Forms.CheckBox();
            this.checkboxPerluSetHarga = new System.Windows.Forms.CheckBox();
            this.totalDataLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxHargaKhusus = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkboxActive = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textboxId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxNama = new System.Windows.Forms.TextBox();
            this.textboxFilterUsername = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboFilterActive = new DoranApp.Components.ActiveComboBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(332, 154);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(374, 150);
            this.dataGridView1.TabIndex = 28;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // checkboxCekTahunan
            // 
            this.checkboxCekTahunan.AutoSize = true;
            this.checkboxCekTahunan.Checked = true;
            this.checkboxCekTahunan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxCekTahunan.Location = new System.Drawing.Point(217, 106);
            this.checkboxCekTahunan.Name = "checkboxCekTahunan";
            this.checkboxCekTahunan.Size = new System.Drawing.Size(91, 17);
            this.checkboxCekTahunan.TabIndex = 6;
            this.checkboxCekTahunan.Text = "Cek Tahunan";
            this.checkboxCekTahunan.UseVisualStyleBackColor = true;
            // 
            // checkboxPerluSetHarga
            // 
            this.checkboxPerluSetHarga.AutoSize = true;
            this.checkboxPerluSetHarga.Checked = true;
            this.checkboxPerluSetHarga.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxPerluSetHarga.Location = new System.Drawing.Point(105, 106);
            this.checkboxPerluSetHarga.Name = "checkboxPerluSetHarga";
            this.checkboxPerluSetHarga.Size = new System.Drawing.Size(101, 17);
            this.checkboxPerluSetHarga.TabIndex = 5;
            this.checkboxPerluSetHarga.Text = "Perlu Set Harga";
            this.checkboxPerluSetHarga.UseVisualStyleBackColor = true;
            // 
            // totalDataLabel
            // 
            this.totalDataLabel.AutoSize = true;
            this.totalDataLabel.Location = new System.Drawing.Point(332, 138);
            this.totalDataLabel.Name = "totalDataLabel";
            this.totalDataLabel.Size = new System.Drawing.Size(69, 13);
            this.totalDataLabel.TabIndex = 30;
            this.totalDataLabel.Text = "Total Data: 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxHargaKhusus);
            this.groupBox1.Controls.Add(this.checkboxCekTahunan);
            this.groupBox1.Controls.Add(this.checkboxPerluSetHarga);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.checkboxActive);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textboxId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textboxNama);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 292);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tambah/Ubah Brand";
            // 
            // checkBoxHargaKhusus
            // 
            this.checkBoxHargaKhusus.AutoSize = true;
            this.checkBoxHargaKhusus.Checked = true;
            this.checkBoxHargaKhusus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHargaKhusus.Location = new System.Drawing.Point(105, 129);
            this.checkBoxHargaKhusus.Name = "checkBoxHargaKhusus";
            this.checkBoxHargaKhusus.Size = new System.Drawing.Size(93, 17);
            this.checkBoxHargaKhusus.TabIndex = 21;
            this.checkBoxHargaKhusus.Text = "Harga Khusus";
            this.checkBoxHargaKhusus.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(105, 176);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(205, 44);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Tag = "actionButton";
            this.buttonSave.Text = "Simpan";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkboxActive
            // 
            this.checkboxActive.AutoSize = true;
            this.checkboxActive.Checked = true;
            this.checkboxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxActive.Location = new System.Drawing.Point(104, 153);
            this.checkboxActive.Name = "checkboxActive";
            this.checkboxActive.Size = new System.Drawing.Size(47, 17);
            this.checkboxActive.TabIndex = 7;
            this.checkboxActive.Text = "Aktif";
            this.checkboxActive.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Top;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(3, 16);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(308, 32);
            this.button4.TabIndex = 9;
            this.button4.TabStop = false;
            this.button4.Text = "Buat Baru";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.Color.Red;
            this.buttonDelete.Location = new System.Drawing.Point(200, 254);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(108, 32);
            this.buttonDelete.TabIndex = 20;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.Text = "Hapus";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(63, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Kode";
            // 
            // textboxId
            // 
            this.textboxId.Location = new System.Drawing.Point(105, 54);
            this.textboxId.Name = "textboxId";
            this.textboxId.ReadOnly = true;
            this.textboxId.Size = new System.Drawing.Size(205, 20);
            this.textboxId.TabIndex = 6;
            this.textboxId.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nama";
            // 
            // textboxNama
            // 
            this.textboxNama.Location = new System.Drawing.Point(105, 80);
            this.textboxNama.Name = "textboxNama";
            this.textboxNama.Size = new System.Drawing.Size(205, 20);
            this.textboxNama.TabIndex = 1;
            // 
            // textboxFilterUsername
            // 
            this.textboxFilterUsername.Location = new System.Drawing.Point(80, 16);
            this.textboxFilterUsername.Name = "textboxFilterUsername";
            this.textboxFilterUsername.Size = new System.Drawing.Size(266, 20);
            this.textboxFilterUsername.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboFilterActive);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonFilter);
            this.groupBox2.Controls.Add(this.textboxFilterUsername);
            this.groupBox2.Location = new System.Drawing.Point(332, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 123);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            // 
            // comboFilterActive
            // 
            this.comboFilterActive.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterActive.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterActive.DisplayMember = "Value";
            this.comboFilterActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFilterActive.FormattingEnabled = true;
            this.comboFilterActive.Location = new System.Drawing.Point(81, 42);
            this.comboFilterActive.Name = "comboFilterActive";
            this.comboFilterActive.Size = new System.Drawing.Size(121, 21);
            this.comboFilterActive.TabIndex = 9;
            this.comboFilterActive.ValueMember = "Key";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(41, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Aktif";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nama";
            // 
            // buttonFilter
            // 
            this.buttonFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFilter.Location = new System.Drawing.Point(80, 69);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(266, 40);
            this.buttonFilter.TabIndex = 8;
            this.buttonFilter.Tag = "actionButton";
            this.buttonFilter.Text = "Cari";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // HKategoribarangForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 316);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.totalDataLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "HKategoribarangForm";
            this.Text = "Brand";
            this.Load += new System.EventHandler(this.HKategoribarangForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox checkboxCekTahunan;
        private System.Windows.Forms.CheckBox checkboxPerluSetHarga;
        private System.Windows.Forms.Label totalDataLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkboxActive;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textboxId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxNama;
        private System.Windows.Forms.TextBox textboxFilterUsername;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.CheckBox checkBoxHargaKhusus;
        private Components.ActiveComboBox comboFilterActive;
    }
}