using System.ComponentModel;

namespace DoranApp.View.UserForms;

partial class CancelOrderConfirmationForm
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
        this.label1 = new System.Windows.Forms.Label();
        this.textBoxSebab = new System.Windows.Forms.TextBox();
        this.buttonBatal = new System.Windows.Forms.Button();
        this.buttonSimpan = new System.Windows.Forms.Button();
        this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(68, 23);
        this.label1.TabIndex = 0;
        this.label1.Text = "Sebab :";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // textBoxSebab
        // 
        this.textBoxSebab.Location = new System.Drawing.Point(86, 12);
        this.textBoxSebab.Multiline = true;
        this.textBoxSebab.Name = "textBoxSebab";
        this.textBoxSebab.Size = new System.Drawing.Size(278, 20);
        this.textBoxSebab.TabIndex = 1;
        // 
        // buttonBatal
        // 
        this.buttonBatal.Location = new System.Drawing.Point(86, 38);
        this.buttonBatal.Name = "buttonBatal";
        this.buttonBatal.Size = new System.Drawing.Size(75, 33);
        this.buttonBatal.TabIndex = 2;
        this.buttonBatal.Text = "Batal";
        this.buttonBatal.UseVisualStyleBackColor = true;
        this.buttonBatal.Click += new System.EventHandler(this.button1_Click);
        // 
        // buttonSimpan
        // 
        this.buttonSimpan.Location = new System.Drawing.Point(289, 38);
        this.buttonSimpan.Name = "buttonSimpan";
        this.buttonSimpan.Size = new System.Drawing.Size(75, 33);
        this.buttonSimpan.TabIndex = 3;
        this.buttonSimpan.Text = "Simpan";
        this.buttonSimpan.UseVisualStyleBackColor = true;
        this.buttonSimpan.Click += new System.EventHandler(this.button2_Click);
        // 
        // errorProvider1
        // 
        this.errorProvider1.ContainerControl = this;
        // 
        // CancelOrderConfirmationForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ClientSize = new System.Drawing.Size(396, 83);
        this.Controls.Add(this.buttonSimpan);
        this.Controls.Add(this.buttonBatal);
        this.Controls.Add(this.textBoxSebab);
        this.Controls.Add(this.label1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        this.Location = new System.Drawing.Point(15, 15);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "CancelOrderConfirmationForm";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.TopMost = true;
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.ErrorProvider errorProvider1;

    public System.Windows.Forms.Button buttonBatal;
    public System.Windows.Forms.Button buttonSimpan;

    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.TextBox textBoxSebab;

    #endregion
}