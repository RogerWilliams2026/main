using System.Windows.Forms;
using System.Drawing;

namespace RogStock2026_Utilities.Screens
{
    partial class frmLoginMaintenance_Utilities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoginMaintenance_Utilities));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTNShowPassword = new System.Windows.Forms.Button();
            this.BTNNew = new System.Windows.Forms.Button();
            this.CMBUser = new System.Windows.Forms.ComboBox();
            this.TXTPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BTNSave = new System.Windows.Forms.Button();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.STBStatus = new System.Windows.Forms.StatusStrip();
            this.STLStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.STBStatus.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.BTNShowPassword);
            this.panel1.Controls.Add(this.BTNNew);
            this.panel1.Controls.Add(this.CMBUser);
            this.panel1.Controls.Add(this.TXTPassword);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 83);
            this.panel1.TabIndex = 5;
            this.panel1.TabStop = true;
            // 
            // BTNShowPassword
            // 
            this.BTNShowPassword.Location = new System.Drawing.Point(244, 46);
            this.BTNShowPassword.Name = "BTNShowPassword";
            this.BTNShowPassword.Size = new System.Drawing.Size(44, 21);
            this.BTNShowPassword.TabIndex = 9;
            this.BTNShowPassword.Text = "Show";
            this.BTNShowPassword.UseVisualStyleBackColor = true;
            this.BTNShowPassword.Click += new System.EventHandler(this.BTNShowPassword_Click);
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(244, 15);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(44, 20);
            this.BTNNew.TabIndex = 0;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // CMBUser
            // 
            this.CMBUser.FormattingEnabled = true;
            this.CMBUser.Location = new System.Drawing.Point(100, 14);
            this.CMBUser.Name = "CMBUser";
            this.CMBUser.Size = new System.Drawing.Size(124, 21);
            this.CMBUser.Sorted = true;
            this.CMBUser.TabIndex = 1;
            this.CMBUser.Tag = "1";
            this.CMBUser.SelectedIndexChanged += new System.EventHandler(this.CMBUser_SelectedIndexChanged);
            this.CMBUser.Leave += new System.EventHandler(this.CMBUser_Leave);
            // 
            // TXTPassword
            // 
            this.TXTPassword.Location = new System.Drawing.Point(100, 46);
            this.TXTPassword.MaxLength = 10;
            this.TXTPassword.Name = "TXTPassword";
            this.TXTPassword.PasswordChar = '*';
            this.TXTPassword.Size = new System.Drawing.Size(100, 20);
            this.TXTPassword.TabIndex = 2;
            this.TXTPassword.Tag = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "User Name:";
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(12, 146);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 3;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(101, 146);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 4;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // STBStatus
            // 
            this.STBStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.STLStatus});
            this.STBStatus.Location = new System.Drawing.Point(0, 174);
            this.STBStatus.Name = "STBStatus";
            this.STBStatus.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.STBStatus.Size = new System.Drawing.Size(330, 22);
            this.STBStatus.SizingGrip = false;
            this.STBStatus.TabIndex = 6;
            this.STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            this.STLStatus.Name = "STLStatus";
            this.STLStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(1, 2);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(329, 34);
            this.PANTitle.TabIndex = 126;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(303, 4);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 125;
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // LBLTitle
            // 
            this.LBLTitle.AutoSize = true;
            this.LBLTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLTitle.ForeColor = System.Drawing.Color.White;
            this.LBLTitle.Location = new System.Drawing.Point(9, 8);
            this.LBLTitle.Name = "LBLTitle";
            this.LBLTitle.Size = new System.Drawing.Size(145, 17);
            this.LBLTitle.TabIndex = 0;
            this.LBLTitle.Text = "Login Maintenance";
            // 
            // frmLoginMaintenance_Utilities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 196);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.STBStatus);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmLoginMaintenance_Utilities";
            this.Text = "Login Maintenance";
            this.Load += new System.EventHandler(this.frmLoginMaintenance_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.STBStatus.ResumeLayout(false);
            this.STBStatus.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TXTPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.ComboBox CMBUser;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNNew;
        private Button BTNShowPassword;
        private StatusStrip STBStatus;
        private ToolStripStatusLabel STLStatus;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
    }
}