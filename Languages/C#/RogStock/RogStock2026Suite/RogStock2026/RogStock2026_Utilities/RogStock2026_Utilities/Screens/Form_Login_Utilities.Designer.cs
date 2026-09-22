using System.Windows.Forms;
using System.Drawing;

namespace RogStock2026_Utilities.Screens
{
    partial class frmLogin_Utilities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin_Utilities));
            this.BTNLogin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TXTPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TXTUser = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTNLogin
            // 
            this.BTNLogin.Location = new System.Drawing.Point(24, 147);
            this.BTNLogin.Name = "BTNLogin";
            this.BTNLogin.Size = new System.Drawing.Size(75, 23);
            this.BTNLogin.TabIndex = 3;
            this.BTNLogin.Text = "Login";
            this.BTNLogin.UseVisualStyleBackColor = true;
            this.BTNLogin.Click += new System.EventHandler(this.BTNLogin_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.TXTPassword);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TXTUser);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(24, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 100);
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = true;
            // 
            // TXTPassword
            // 
            this.TXTPassword.Location = new System.Drawing.Point(100, 59);
            this.TXTPassword.MaxLength = 10;
            this.TXTPassword.Name = "TXTPassword";
            this.TXTPassword.PasswordChar = '*';
            this.TXTPassword.Size = new System.Drawing.Size(100, 20);
            this.TXTPassword.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password:";
            // 
            // TXTUser
            // 
            this.TXTUser.Location = new System.Drawing.Point(100, 22);
            this.TXTUser.MaxLength = 30;
            this.TXTUser.Name = "TXTUser";
            this.TXTUser.Size = new System.Drawing.Size(100, 20);
            this.TXTUser.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "User Name:";
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(0, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(271, 34);
            this.PANTitle.TabIndex = 125;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(240, 4);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 125;
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNCancel_Click);
            // 
            // LBLTitle
            // 
            this.LBLTitle.AutoSize = true;
            this.LBLTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLTitle.ForeColor = System.Drawing.Color.White;
            this.LBLTitle.Location = new System.Drawing.Point(9, 8);
            this.LBLTitle.Name = "LBLTitle";
            this.LBLTitle.Size = new System.Drawing.Size(107, 17);
            this.LBLTitle.TabIndex = 0;
            this.LBLTitle.Text = "Utilities Login";
            // 
            // frmLogin_Utilities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 173);
            this.ControlBox = false;
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BTNLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin_Utilities";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RogStock 20205 Utilities - Login";
            this.Load += new System.EventHandler(this.frmLogin_Utilities_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BTNLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TXTPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TXTUser;
        private System.Windows.Forms.Label label1;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
    }
}