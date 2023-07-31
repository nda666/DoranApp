namespace DoranApp.View
{
    partial class SalesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesForm));
            this.comboFilterIsManager = new System.Windows.Forms.GroupBox();
            this.comboFilterManager = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboFilterSalesTeam = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.textboxFilterUsername = new System.Windows.Forms.TextBox();
            this.totalDataLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboManager = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonRefreshRole = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboSalesTeam = new System.Windows.Forms.ComboBox();
            this.checkboxGetOmzetEmail = new System.Windows.Forms.CheckBox();
            this.checkboxIsManager = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.checkboxActive = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textboxId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxName = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.checkboxEmailJeteterdahsyat = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxEmailTargetTahunan = new System.Windows.Forms.CheckBox();
            this.checkBoxEmailresikiriman = new System.Windows.Forms.CheckBox();
            this.checkBoxBisalihatomzettahunantim = new System.Windows.Forms.CheckBox();
            this.checkBoxSalesOl = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.isManagerComboBox1 = new DoranApp.Components.IsManagerComboBox(this.components);
            this.comboboxFilterActive = new DoranApp.Components.ActiveComboBox(this.components);
            this.textBoxUrutan = new DoranApp.Components.NumericTextbox(this.components);
            this.comboFilterIsManager.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboFilterIsManager
            // 
            this.comboFilterIsManager.Controls.Add(this.isManagerComboBox1);
            this.comboFilterIsManager.Controls.Add(this.comboboxFilterActive);
            this.comboFilterIsManager.Controls.Add(this.comboFilterManager);
            this.comboFilterIsManager.Controls.Add(this.label8);
            this.comboFilterIsManager.Controls.Add(this.label7);
            this.comboFilterIsManager.Controls.Add(this.label4);
            this.comboFilterIsManager.Controls.Add(this.comboFilterSalesTeam);
            this.comboFilterIsManager.Controls.Add(this.label3);
            this.comboFilterIsManager.Controls.Add(this.buttonFilter);
            this.comboFilterIsManager.Controls.Add(this.textboxFilterUsername);
            this.comboFilterIsManager.Location = new System.Drawing.Point(431, 12);
            this.comboFilterIsManager.Name = "comboFilterIsManager";
            this.comboFilterIsManager.Size = new System.Drawing.Size(359, 141);
            this.comboFilterIsManager.TabIndex = 31;
            this.comboFilterIsManager.TabStop = false;
            this.comboFilterIsManager.Text = "Filter";
            this.comboFilterIsManager.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // comboFilterManager
            // 
            this.comboFilterManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterManager.FormattingEnabled = true;
            this.comboFilterManager.Location = new System.Drawing.Point(191, 69);
            this.comboFilterManager.Name = "comboFilterManager";
            this.comboFilterManager.Size = new System.Drawing.Size(155, 21);
            this.comboFilterManager.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(11, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Manager?";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(36, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Team";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(210, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Aktif";
            // 
            // comboFilterSalesTeam
            // 
            this.comboFilterSalesTeam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterSalesTeam.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterSalesTeam.FormattingEnabled = true;
            this.comboFilterSalesTeam.ItemHeight = 13;
            this.comboFilterSalesTeam.Location = new System.Drawing.Point(80, 42);
            this.comboFilterSalesTeam.Name = "comboFilterSalesTeam";
            this.comboFilterSalesTeam.Size = new System.Drawing.Size(124, 21);
            this.comboFilterSalesTeam.TabIndex = 27;
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
            this.buttonFilter.Location = new System.Drawing.Point(80, 96);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(266, 40);
            this.buttonFilter.TabIndex = 8;
            this.buttonFilter.Text = "Cari";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // textboxFilterUsername
            // 
            this.textboxFilterUsername.Location = new System.Drawing.Point(80, 16);
            this.textboxFilterUsername.Name = "textboxFilterUsername";
            this.textboxFilterUsername.Size = new System.Drawing.Size(266, 20);
            this.textboxFilterUsername.TabIndex = 6;
            // 
            // totalDataLabel
            // 
            this.totalDataLabel.AutoSize = true;
            this.totalDataLabel.Location = new System.Drawing.Point(428, 156);
            this.totalDataLabel.Name = "totalDataLabel";
            this.totalDataLabel.Size = new System.Drawing.Size(69, 13);
            this.totalDataLabel.TabIndex = 30;
            this.totalDataLabel.Text = "Total Data: 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxUrutan);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.checkBoxSalesOl);
            this.groupBox1.Controls.Add(this.checkBoxBisalihatomzettahunantim);
            this.groupBox1.Controls.Add(this.checkBoxEmailresikiriman);
            this.groupBox1.Controls.Add(this.checkBoxEmailTargetTahunan);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.checkboxEmailJeteterdahsyat);
            this.groupBox1.Controls.Add(this.comboManager);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.buttonRefreshRole);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboSalesTeam);
            this.groupBox1.Controls.Add(this.checkboxGetOmzetEmail);
            this.groupBox1.Controls.Add(this.checkboxIsManager);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.checkboxActive);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textboxId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textboxName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 397);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tambah/Ubah User";
            // 
            // comboManager
            // 
            this.comboManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboManager.FormattingEnabled = true;
            this.comboManager.ItemHeight = 13;
            this.comboManager.Location = new System.Drawing.Point(105, 156);
            this.comboManager.Name = "comboManager";
            this.comboManager.Size = new System.Drawing.Size(175, 21);
            this.comboManager.TabIndex = 31;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::DoranApp.Properties.Resources.Refresh;
            this.button1.Location = new System.Drawing.Point(286, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 30;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonRefreshRole
            // 
            this.buttonRefreshRole.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRefreshRole.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonRefreshRole.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonRefreshRole.FlatAppearance.BorderSize = 0;
            this.buttonRefreshRole.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonRefreshRole.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonRefreshRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefreshRole.Image = global::DoranApp.Properties.Resources.Refresh;
            this.buttonRefreshRole.Location = new System.Drawing.Point(286, 103);
            this.buttonRefreshRole.Name = "buttonRefreshRole";
            this.buttonRefreshRole.Size = new System.Drawing.Size(25, 25);
            this.buttonRefreshRole.TabIndex = 29;
            this.buttonRefreshRole.TabStop = false;
            this.buttonRefreshRole.UseVisualStyleBackColor = true;
            this.buttonRefreshRole.Click += new System.EventHandler(this.buttonRefreshRole_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(61, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Team";
            // 
            // comboSalesTeam
            // 
            this.comboSalesTeam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboSalesTeam.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboSalesTeam.FormattingEnabled = true;
            this.comboSalesTeam.ItemHeight = 13;
            this.comboSalesTeam.Location = new System.Drawing.Point(105, 106);
            this.comboSalesTeam.Name = "comboSalesTeam";
            this.comboSalesTeam.Size = new System.Drawing.Size(175, 21);
            this.comboSalesTeam.TabIndex = 27;
            // 
            // checkboxGetOmzetEmail
            // 
            this.checkboxGetOmzetEmail.AutoSize = true;
            this.checkboxGetOmzetEmail.Checked = true;
            this.checkboxGetOmzetEmail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxGetOmzetEmail.Location = new System.Drawing.Point(105, 206);
            this.checkboxGetOmzetEmail.Name = "checkboxGetOmzetEmail";
            this.checkboxGetOmzetEmail.Size = new System.Drawing.Size(140, 17);
            this.checkboxGetOmzetEmail.TabIndex = 6;
            this.checkboxGetOmzetEmail.Text = "Email Omzet Terdahsyat";
            this.checkboxGetOmzetEmail.UseVisualStyleBackColor = true;
            // 
            // checkboxIsManager
            // 
            this.checkboxIsManager.AutoSize = true;
            this.checkboxIsManager.Checked = true;
            this.checkboxIsManager.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxIsManager.Location = new System.Drawing.Point(105, 133);
            this.checkboxIsManager.Name = "checkboxIsManager";
            this.checkboxIsManager.Size = new System.Drawing.Size(68, 17);
            this.checkboxIsManager.TabIndex = 5;
            this.checkboxIsManager.Text = "Manager";
            this.checkboxIsManager.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Manager";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(199, 311);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(205, 44);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Simpan";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // checkboxActive
            // 
            this.checkboxActive.AutoSize = true;
            this.checkboxActive.Checked = true;
            this.checkboxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxActive.Location = new System.Drawing.Point(105, 183);
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
            this.button4.Size = new System.Drawing.Size(404, 32);
            this.button4.TabIndex = 9;
            this.button4.TabStop = false;
            this.button4.Text = "Buat Baru";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.Color.Red;
            this.buttonDelete.Location = new System.Drawing.Point(296, 361);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(108, 32);
            this.buttonDelete.TabIndex = 20;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.Text = "Hapus";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(79, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "ID";
            // 
            // textboxId
            // 
            this.textboxId.Location = new System.Drawing.Point(105, 54);
            this.textboxId.Name = "textboxId";
            this.textboxId.ReadOnly = true;
            this.textboxId.Size = new System.Drawing.Size(299, 20);
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
            // textboxName
            // 
            this.textboxName.Location = new System.Drawing.Point(105, 80);
            this.textboxName.Name = "textboxName";
            this.textboxName.Size = new System.Drawing.Size(299, 20);
            this.textboxName.TabIndex = 1;
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
            this.dataGridView1.Location = new System.Drawing.Point(431, 172);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(359, 237);
            this.dataGridView1.TabIndex = 28;
            this.dataGridView1.TabStop = false;
            // 
            // checkboxEmailJeteterdahsyat
            // 
            this.checkboxEmailJeteterdahsyat.AutoSize = true;
            this.checkboxEmailJeteterdahsyat.Checked = true;
            this.checkboxEmailJeteterdahsyat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxEmailJeteterdahsyat.Location = new System.Drawing.Point(105, 229);
            this.checkboxEmailJeteterdahsyat.Name = "checkboxEmailJeteterdahsyat";
            this.checkboxEmailJeteterdahsyat.Size = new System.Drawing.Size(130, 17);
            this.checkboxEmailJeteterdahsyat.TabIndex = 32;
            this.checkboxEmailJeteterdahsyat.Text = "Email Jete Terdahsyat";
            this.checkboxEmailJeteterdahsyat.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(67, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Opsi";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkBoxEmailTargetTahunan
            // 
            this.checkBoxEmailTargetTahunan.AutoSize = true;
            this.checkBoxEmailTargetTahunan.Checked = true;
            this.checkBoxEmailTargetTahunan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEmailTargetTahunan.Location = new System.Drawing.Point(105, 252);
            this.checkBoxEmailTargetTahunan.Name = "checkBoxEmailTargetTahunan";
            this.checkBoxEmailTargetTahunan.Size = new System.Drawing.Size(131, 17);
            this.checkBoxEmailTargetTahunan.TabIndex = 34;
            this.checkBoxEmailTargetTahunan.Text = "Email Target Tahunan";
            this.checkBoxEmailTargetTahunan.UseVisualStyleBackColor = true;
            // 
            // checkBoxEmailresikiriman
            // 
            this.checkBoxEmailresikiriman.AutoSize = true;
            this.checkBoxEmailresikiriman.Checked = true;
            this.checkBoxEmailresikiriman.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEmailresikiriman.Location = new System.Drawing.Point(251, 206);
            this.checkBoxEmailresikiriman.Name = "checkBoxEmailresikiriman";
            this.checkBoxEmailresikiriman.Size = new System.Drawing.Size(112, 17);
            this.checkBoxEmailresikiriman.TabIndex = 35;
            this.checkBoxEmailresikiriman.Text = "Email Resi Kiriman";
            this.checkBoxEmailresikiriman.UseVisualStyleBackColor = true;
            // 
            // checkBoxBisalihatomzettahunantim
            // 
            this.checkBoxBisalihatomzettahunantim.AutoSize = true;
            this.checkBoxBisalihatomzettahunantim.Checked = true;
            this.checkBoxBisalihatomzettahunantim.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBisalihatomzettahunantim.Location = new System.Drawing.Point(251, 229);
            this.checkBoxBisalihatomzettahunantim.Name = "checkBoxBisalihatomzettahunantim";
            this.checkBoxBisalihatomzettahunantim.Size = new System.Drawing.Size(102, 17);
            this.checkBoxBisalihatomzettahunantim.TabIndex = 36;
            this.checkBoxBisalihatomzettahunantim.Text = "Lihat Omzet Tim";
            this.checkBoxBisalihatomzettahunantim.UseVisualStyleBackColor = true;
            // 
            // checkBoxSalesOl
            // 
            this.checkBoxSalesOl.AutoSize = true;
            this.checkBoxSalesOl.Checked = true;
            this.checkBoxSalesOl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSalesOl.Location = new System.Drawing.Point(179, 133);
            this.checkBoxSalesOl.Name = "checkBoxSalesOl";
            this.checkBoxSalesOl.Size = new System.Drawing.Size(69, 17);
            this.checkBoxSalesOl.TabIndex = 37;
            this.checkBoxSalesOl.Text = "Sales OL";
            this.checkBoxSalesOl.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(54, 275);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "Urutan";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // isManagerComboBox1
            // 
            this.isManagerComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.isManagerComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.isManagerComboBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.isManagerComboBox1.DisplayMember = "Value";
            this.isManagerComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.isManagerComboBox1.FormattingEnabled = true;
            this.isManagerComboBox1.Items.AddRange(new object[] {
            ((object)(resources.GetObject("isManagerComboBox1.Items"))),
            ((object)(resources.GetObject("isManagerComboBox1.Items1"))),
            ((object)(resources.GetObject("isManagerComboBox1.Items2")))});
            this.isManagerComboBox1.Location = new System.Drawing.Point(80, 69);
            this.isManagerComboBox1.Name = "isManagerComboBox1";
            this.isManagerComboBox1.Size = new System.Drawing.Size(105, 21);
            this.isManagerComboBox1.TabIndex = 33;
            this.isManagerComboBox1.ValueMember = "Key";
            // 
            // comboboxFilterActive
            // 
            this.comboboxFilterActive.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboboxFilterActive.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboboxFilterActive.DisplayMember = "Value";
            this.comboboxFilterActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxFilterActive.FormattingEnabled = true;
            this.comboboxFilterActive.Items.AddRange(new object[] {
            ((object)(resources.GetObject("comboboxFilterActive.Items"))),
            ((object)(resources.GetObject("comboboxFilterActive.Items1"))),
            ((object)(resources.GetObject("comboboxFilterActive.Items2"))),
            ((object)(resources.GetObject("comboboxFilterActive.Items3"))),
            ((object)(resources.GetObject("comboboxFilterActive.Items4"))),
            ((object)(resources.GetObject("comboboxFilterActive.Items5"))),
            ((object)(resources.GetObject("comboboxFilterActive.Items6"))),
            ((object)(resources.GetObject("comboboxFilterActive.Items7"))),
            ((object)(resources.GetObject("comboboxFilterActive.Items8")))});
            this.comboboxFilterActive.Location = new System.Drawing.Point(250, 42);
            this.comboboxFilterActive.Name = "comboboxFilterActive";
            this.comboboxFilterActive.Size = new System.Drawing.Size(96, 21);
            this.comboboxFilterActive.TabIndex = 32;
            this.comboboxFilterActive.ValueMember = "Key";
            // 
            // textBoxUrutan
            // 
            this.textBoxUrutan.Location = new System.Drawing.Point(105, 275);
            this.textBoxUrutan.Name = "textBoxUrutan";
            this.textBoxUrutan.Size = new System.Drawing.Size(100, 20);
            this.textBoxUrutan.TabIndex = 40;
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 421);
            this.Controls.Add(this.comboFilterIsManager);
            this.Controls.Add(this.totalDataLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SalesForm";
            this.Text = "SalesForm";
            this.Load += new System.EventHandler(this.SalesForm_Load);
            this.comboFilterIsManager.ResumeLayout(false);
            this.comboFilterIsManager.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox comboFilterIsManager;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboFilterSalesTeam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.TextBox textboxFilterUsername;
        private System.Windows.Forms.Label totalDataLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkboxGetOmzetEmail;
        private System.Windows.Forms.CheckBox checkboxIsManager;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkboxActive;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textboxId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboManager;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonRefreshRole;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboSalesTeam;
        private System.Windows.Forms.ComboBox comboFilterManager;
        private System.Windows.Forms.Label label8;
        private Components.ActiveComboBox comboboxFilterActive;
        private Components.IsManagerComboBox isManagerComboBox1;
        private System.Windows.Forms.CheckBox checkboxEmailJeteterdahsyat;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxSalesOl;
        private System.Windows.Forms.CheckBox checkBoxBisalihatomzettahunantim;
        private System.Windows.Forms.CheckBox checkBoxEmailresikiriman;
        private System.Windows.Forms.CheckBox checkBoxEmailTargetTahunan;
        private System.Windows.Forms.Label label9;
        private Components.NumericTextbox textBoxUrutan;
    }
}