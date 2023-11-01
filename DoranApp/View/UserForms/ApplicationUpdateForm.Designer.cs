using System.ComponentModel;

namespace DoranApp.View.UserForms;

partial class ApplicationUpdateForm
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
        this.label1 = new System.Windows.Forms.Label();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(67, 12);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(359, 52);
        this.label1.TabIndex = 0;
        this.label1.Text = "label1";
        // 
        // pictureBox1
        // 
        this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
        this.pictureBox1.Image = global::DoranApp.Properties.Resources.icons8_update1;
        this.pictureBox1.Location = new System.Drawing.Point(12, 12);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(49, 52);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        this.pictureBox1.TabIndex = 1;
        this.pictureBox1.TabStop = false;
        // 
        // button1
        // 
        this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.button1.Location = new System.Drawing.Point(331, 67);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(95, 38);
        this.button1.TabIndex = 2;
        this.button1.Text = "Update";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // button2
        // 
        this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.button2.Location = new System.Drawing.Point(230, 67);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(95, 38);
        this.button2.TabIndex = 3;
        this.button2.Text = "Keluar";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // ApplicationUpdateForm
        // 
        this.AcceptButton = this.button1;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.button2;
        this.ClientSize = new System.Drawing.Size(438, 114);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.pictureBox1);
        this.Controls.Add(this.label1);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "ApplicationUpdateForm";
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Pembaruan Aplikasi";
        this.TopMost = true;
        this.Load += new System.EventHandler(this.ApplicationUpdateForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pictureBox1;

    #endregion
}