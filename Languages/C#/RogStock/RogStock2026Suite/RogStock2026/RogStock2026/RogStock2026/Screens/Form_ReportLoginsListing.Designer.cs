using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2026.Screens
{
    partial class frmReportLoginsListing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportLoginsListing));
            this.RPTUOM = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CHKAll = new System.Windows.Forms.CheckBox();
            this.CMBLOG_User = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.BTNShow = new System.Windows.Forms.Button();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // RPTUOM
            // 
            this.RPTUOM.Enabled = false;
            this.RPTUOM.Location = new System.Drawing.Point(5, 144);
            this.RPTUOM.Name = "RPTUOM";
            this.RPTUOM.ServerReport.BearerToken = null;
            this.RPTUOM.ServerReport.DisplayName = "UOM Listing";
            this.RPTUOM.ServerReport.ReportPath = "/projects/VisualStudio/Csharp/RogStock2026_Reports/RogStock_Reports/Reports\\rptUO" +
    "M";
            this.RPTUOM.ServerReport.ReportServerUrl = new System.Uri("http://DESKTOP-694Q8HR:80/ReportServer", System.UriKind.Absolute);
            this.RPTUOM.Size = new System.Drawing.Size(1017, 538);
            this.RPTUOM.TabIndex = 42;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CHKAll);
            this.panel1.Controls.Add(this.CMBLOG_User);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Location = new System.Drawing.Point(5, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 40);
            this.panel1.TabIndex = 43;
            // 
            // CHKAll
            // 
            this.CHKAll.AutoSize = true;
            this.CHKAll.Checked = true;
            this.CHKAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKAll.Location = new System.Drawing.Point(264, 9);
            this.CHKAll.Name = "CHKAll";
            this.CHKAll.Size = new System.Drawing.Size(37, 17);
            this.CHKAll.TabIndex = 1;
            this.CHKAll.Text = "All";
            this.CHKAll.UseVisualStyleBackColor = true;
            this.CHKAll.CheckedChanged += new System.EventHandler(this.CHKAll_CheckedChanged);
            // 
            // CMBLOG_User
            // 
            this.CMBLOG_User.FormattingEnabled = true;
            this.CMBLOG_User.Location = new System.Drawing.Point(52, 5);
            this.CMBLOG_User.MaxLength = 20;
            this.CMBLOG_User.Name = "CMBLOG_User";
            this.CMBLOG_User.Size = new System.Drawing.Size(200, 21);
            this.CMBLOG_User.TabIndex = 0;
            this.CMBLOG_User.Tag = "1";
            this.CMBLOG_User.SelectedIndexChanged += new System.EventHandler(this.CMBLOG_User_SelectedIndexChanged);
            this.CMBLOG_User.SelectedValueChanged += new System.EventHandler(this.CMBLOG_User_SelectedValueChanged);
            this.CMBLOG_User.Leave += new System.EventHandler(this.CMBLOG_User_Leave);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(9, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(32, 13);
            this.Label1.TabIndex = 41;
            this.Label1.Text = "User:";
            // 
            // BTNShow
            // 
            this.BTNShow.Location = new System.Drawing.Point(926, 38);
            this.BTNShow.Name = "BTNShow";
            this.BTNShow.Size = new System.Drawing.Size(96, 23);
            this.BTNShow.TabIndex = 0;
            this.BTNShow.Text = "Show Report";
            this.BTNShow.UseVisualStyleBackColor = true;
            this.BTNShow.Click += new System.EventHandler(this.BTNShow_Click);
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(1, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(1027, 34);
            this.PANTitle.TabIndex = 82;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(1000, 3);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 80;
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // LBLTitle
            // 
            this.LBLTitle.AutoSize = true;
            this.LBLTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLTitle.Location = new System.Drawing.Point(9, 8);
            this.LBLTitle.Name = "LBLTitle";
            this.LBLTitle.Size = new System.Drawing.Size(0, 17);
            this.LBLTitle.TabIndex = 0;
            // 
            // frmReportLoginsListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 712);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.BTNShow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RPTUOM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmReportLoginsListing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Logins Listing";
            this.Load += new System.EventHandler(this.frmReportLoginsListing_Load);
            this.Shown += new System.EventHandler(this.frmReportLoginsListing_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmReportLoginsListing_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Reporting.WinForms.ReportViewer RPTUOM;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTNShow;
        private System.Windows.Forms.CheckBox CHKAll;
        private System.Windows.Forms.ComboBox CMBLOG_User;
        private System.Windows.Forms.Label Label1;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;

        public FormStartPosition StartPosition { get; private set; }
    }
}