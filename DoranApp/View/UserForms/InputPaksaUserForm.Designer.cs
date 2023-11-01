using System.ComponentModel;

namespace DoranApp.View.UserForms;

partial class InputPaksaUserForm
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
        this.button1 = new System.Windows.Forms.Button();
        this.buttonOk = new System.Windows.Forms.Button();
        this.textboxMemo = new System.Windows.Forms.RichTextBox();
        this.textBoxPassword = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
        this.SuspendLayout();
        // 
        // button1
        // 
        this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.button1.Location = new System.Drawing.Point(94, 241);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(87, 39);
        this.button1.TabIndex = 4;
        this.button1.Text = "Batal";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // buttonOk
        // 
        this.buttonOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.buttonOk.Location = new System.Drawing.Point(304, 241);
        this.buttonOk.Name = "buttonOk";
        this.buttonOk.Size = new System.Drawing.Size(87, 39);
        this.buttonOk.TabIndex = 3;
        this.buttonOk.Text = "Simpan";
        this.buttonOk.UseVisualStyleBackColor = true;
        this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
        // 
        // textboxMemo
        // 
        this.textboxMemo.Location = new System.Drawing.Point(12, 12);
        this.textboxMemo.Name = "textboxMemo";
        this.textboxMemo.ReadOnly = true;
        this.textboxMemo.Size = new System.Drawing.Size(412, 174);
        this.textboxMemo.TabIndex = 1;
        this.textboxMemo.Text = "";
        // 
        // textBoxPassword
        // 
        this.textBoxPassword.Location = new System.Drawing.Point(94, 215);
        this.textBoxPassword.Name = "textBoxPassword";
        this.textBoxPassword.PasswordChar = '•';
        this.textBoxPassword.Size = new System.Drawing.Size(297, 20);
        this.textBoxPassword.TabIndex = 2;
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(12, 215);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(76, 23);
        this.label1.TabIndex = 4;
        this.label1.Text = "Password :";
        // 
        // label2
        // 
        this.label2.Location = new System.Drawing.Point(94, 189);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(297, 23);
        this.label2.TabIndex = 5;
        this.label2.Text = "Silahkan masukkan Password";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // errorProvider
        // 
        this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
        this.errorProvider.ContainerControl = this;
        // 
        // toolTip1
        // 
        this.toolTip1.IsBalloon = true;
        this.toolTip1.ShowAlways = true;
        // 
        // InputPaksaUserForm
        // 
        this.AcceptButton = this.buttonOk;
        this.CancelButton = this.button1;
        this.ClientSize = new System.Drawing.Size(436, 290);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.textBoxPassword);
        this.Controls.Add(this.textboxMemo);
        this.Controls.Add(this.buttonOk);
        this.Controls.Add(this.button1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        this.Name = "InputPaksaUserForm";
        this.ShowInTaskbar = false;
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Konfirmasi Penyetujuan INPUT PAKSA";
        this.TopMost = true;
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputPaksaUserForm_FormClosing);
        this.Load += new System.EventHandler(this.InputPaksaForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    public System.Windows.Forms.ToolTip toolTip1;

    public System.Windows.Forms.ErrorProvider errorProvider;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Button button1;
    public System.Windows.Forms.Button buttonOk;
    public System.Windows.Forms.RichTextBox textboxMemo;
    public System.Windows.Forms.TextBox textBoxPassword;
    private System.Windows.Forms.Label label1;
    #endregion
}