using System.Drawing;
using System.Windows.Forms;

namespace DoranApp.View
{
    partial class UserForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboboxFilterActive = new DoranApp.Components.ActiveComboBox(this.components);
            this.searchRoleLabel = new System.Windows.Forms.Label();
            this.comboFilterRole = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.textboxFilterUsername = new System.Windows.Forms.TextBox();
            this.totalDataLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textboxPassword = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.checkboxActive = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.createNewBtn = new System.Windows.Forms.Button();
            this.comboRoles = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textboxId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxUsername = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboboxFilterActive);
            this.groupBox2.Controls.Add(this.searchRoleLabel);
            this.groupBox2.Controls.Add(this.comboFilterRole);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonFilter);
            this.groupBox2.Controls.Add(this.textboxFilterUsername);
            this.groupBox2.Location = new System.Drawing.Point(320, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 123);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            // 
            // comboboxFilterActive
            // 
            this.comboboxFilterActive.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboboxFilterActive.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboboxFilterActive.DisplayMember = "Value";
            this.comboboxFilterActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxFilterActive.FormattingEnabled = true;
            this.comboboxFilterActive.Location = new System.Drawing.Point(253, 43);
            this.comboboxFilterActive.Name = "comboboxFilterActive";
            this.comboboxFilterActive.Size = new System.Drawing.Size(93, 21);
            this.comboboxFilterActive.TabIndex = 17;
            this.comboboxFilterActive.ValueMember = "Key";
            // 
            // searchRoleLabel
            // 
            this.searchRoleLabel.AutoSize = true;
            this.searchRoleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchRoleLabel.Location = new System.Drawing.Point(41, 42);
            this.searchRoleLabel.Name = "searchRoleLabel";
            this.searchRoleLabel.Size = new System.Drawing.Size(33, 13);
            this.searchRoleLabel.TabIndex = 17;
            this.searchRoleLabel.Text = "Role";
            // 
            // comboFilterRole
            // 
            this.comboFilterRole.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboFilterRole.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboFilterRole.FormattingEnabled = true;
            this.comboFilterRole.ItemHeight = 13;
            this.comboFilterRole.Location = new System.Drawing.Point(80, 42);
            this.comboFilterRole.Name = "comboFilterRole";
            this.comboFilterRole.Size = new System.Drawing.Size(128, 21);
            this.comboFilterRole.TabIndex = 16;
            this.comboFilterRole.Leave += new System.EventHandler(this.comboboxFilterRole_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(214, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Aktif";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Username";
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(80, 69);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(266, 40);
            this.buttonFilter.TabIndex = 8;
            this.buttonFilter.Tag = "action";
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
            this.totalDataLabel.Location = new System.Drawing.Point(320, 138);
            this.totalDataLabel.Name = "totalDataLabel";
            this.totalDataLabel.Size = new System.Drawing.Size(69, 13);
            this.totalDataLabel.TabIndex = 16;
            this.totalDataLabel.Text = "Total Data: 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textboxPassword);
            this.groupBox1.Controls.Add(this.buttonSave);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.checkboxActive);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.createNewBtn);
            this.groupBox1.Controls.Add(this.comboRoles);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textboxId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textboxUsername);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 297);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tambah/Ubah User";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(26, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Password";
            // 
            // textboxPassword
            // 
            this.textboxPassword.Location = new System.Drawing.Point(93, 103);
            this.textboxPassword.Name = "textboxPassword";
            this.textboxPassword.Size = new System.Drawing.Size(200, 20);
            this.textboxPassword.TabIndex = 2;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(90, 179);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(203, 44);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Tag = "action";
            this.buttonSave.Text = "Simpan";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.Color.Red;
            this.buttonDelete.Location = new System.Drawing.Point(188, 254);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(105, 32);
            this.buttonDelete.TabIndex = 14;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.Text = "Hapus";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // checkboxActive
            // 
            this.checkboxActive.AutoSize = true;
            this.checkboxActive.Checked = true;
            this.checkboxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxActive.Location = new System.Drawing.Point(93, 156);
            this.checkboxActive.Name = "checkboxActive";
            this.checkboxActive.Size = new System.Drawing.Size(47, 17);
            this.checkboxActive.TabIndex = 4;
            this.checkboxActive.Text = "Aktif";
            this.checkboxActive.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(54, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Aktif";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(54, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Role";
            // 
            // createNewBtn
            // 
            this.createNewBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.createNewBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createNewBtn.Location = new System.Drawing.Point(3, 16);
            this.createNewBtn.Name = "createNewBtn";
            this.createNewBtn.Size = new System.Drawing.Size(293, 32);
            this.createNewBtn.TabIndex = 9;
            this.createNewBtn.TabStop = false;
            this.createNewBtn.Text = "Buat Baru";
            this.createNewBtn.UseVisualStyleBackColor = true;
            this.createNewBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboRoles
            // 
            this.comboRoles.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboRoles.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboRoles.FormattingEnabled = true;
            this.comboRoles.ItemHeight = 13;
            this.comboRoles.Items.AddRange(new object[] {
            "Bos",
            "manageroperasional",
            "auditor",
            "gudang",
            "sales",
            "mastersuperadmin",
            "masterkagud",
            "kagud",
            "adminorder",
            "supermasteraudit",
            "pembelian",
            "akuntan",
            "masteradmin",
            "salesonline",
            "seketaris",
            "managerbusiness",
            "dorancare",
            "masteraudit",
            "kgdjakarta",
            "kgdsemarang",
            "pm",
            "auditkhusus",
            "gudangol",
            "managersalesonline",
            "Kepalaakuntan"});
            this.comboRoles.Location = new System.Drawing.Point(93, 129);
            this.comboRoles.Name = "comboRoles";
            this.comboRoles.Size = new System.Drawing.Size(200, 21);
            this.comboRoles.TabIndex = 3;
            this.comboRoles.Leave += new System.EventHandler(this.comboRoles_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(67, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "ID";
            // 
            // textboxId
            // 
            this.textboxId.Location = new System.Drawing.Point(93, 51);
            this.textboxId.Name = "textboxId";
            this.textboxId.ReadOnly = true;
            this.textboxId.Size = new System.Drawing.Size(200, 20);
            this.textboxId.TabIndex = 0;
            this.textboxId.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Username";
            // 
            // textboxUsername
            // 
            this.textboxUsername.Location = new System.Drawing.Point(93, 77);
            this.textboxUsername.Name = "textboxUsername";
            this.textboxUsername.Size = new System.Drawing.Size(200, 20);
            this.textboxUsername.TabIndex = 1;
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
            this.dataGridView1.Location = new System.Drawing.Point(323, 154);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(362, 167);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Help";
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 333);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.totalDataLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "UserForm";
            this.Text = "User";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.TextBox textboxFilterUsername;
        private System.Windows.Forms.Label totalDataLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createNewBtn;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkboxActive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textboxUsername;
        private System.Windows.Forms.TextBox textboxId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboRoles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textboxPassword;
        private System.Windows.Forms.Label searchRoleLabel;
        private System.Windows.Forms.ComboBox comboFilterRole;
        private ToolTip toolTip1;
        private Components.ActiveComboBox activeComboBoxComponent1;
        private Components.ActiveComboBox comboboxFilterActive;
    }
}