using System.ComponentModel;

namespace DoranApp.View.UserForms;

partial class LimitTransaksiConfirmationForm
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
        this.label2 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.label1.Location = new System.Drawing.Point(12, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(329, 72);
        this.label1.TabIndex = 0;
        this.label1.Text = "label1";
        // 
        // label2
        // 
        this.label2.Location = new System.Drawing.Point(12, 89);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(70, 20);
        this.label2.TabIndex = 1;
        this.label2.Text = "Token";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(88, 89);
        this.textBox1.Multiline = true;
        this.textBox1.Name = "textBox1";
        this.textBox1.ReadOnly = true;
        this.textBox1.Size = new System.Drawing.Size(188, 20);
        this.textBox1.TabIndex = 2;
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(282, 89);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(59, 23);
        this.button1.TabIndex = 3;
        this.button1.Text = "Copy";
        this.button1.UseVisualStyleBackColor = true;
        // 
        // textBox2
        // 
        this.textBox2.Location = new System.Drawing.Point(88, 115);
        this.textBox2.Multiline = true;
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(228, 20);
        this.textBox2.TabIndex = 5;
        this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
        // 
        // label3
        // 
        this.label3.Location = new System.Drawing.Point(12, 115);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(70, 20);
        this.label3.TabIndex = 4;
        this.label3.Text = "Key Limit";
        this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(261, 145);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(80, 32);
        this.button2.TabIndex = 6;
        this.button2.Tag = "actionButton";
        this.button2.Text = "Simpan";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // button3
        // 
        this.button3.Location = new System.Drawing.Point(175, 145);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(80, 32);
        this.button3.TabIndex = 7;
        this.button3.Tag = "actionButton";
        this.button3.Text = "Batal";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(this.button3_Click);
        // 
        // errorProvider1
        // 
        this.errorProvider1.ContainerControl = this;
        // 
        // toolTip1
        // 
        this.toolTip1.AutomaticDelay = 0;
        this.toolTip1.AutoPopDelay = 10000;
        this.toolTip1.InitialDelay = 0;
        this.toolTip1.ReshowDelay = 0;
        this.toolTip1.ShowAlways = true;
        this.toolTip1.StripAmpersands = true;
        // 
        // LimitTransaksiConfirmationForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(353, 189);
        this.Controls.Add(this.button3);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.textBox2);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "LimitTransaksiConfirmationForm";
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Konfirmasi Penyetujuan INPUT PAKSA";
        this.TopMost = true;
        this.Load += new System.EventHandler(this.LimitTransaksiConfirmationForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.ToolTip toolTip1;

    private System.Windows.Forms.ErrorProvider errorProvider1;

    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;

    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Button button1;

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBox1;

    private System.Windows.Forms.Label label1;

    #endregion
}