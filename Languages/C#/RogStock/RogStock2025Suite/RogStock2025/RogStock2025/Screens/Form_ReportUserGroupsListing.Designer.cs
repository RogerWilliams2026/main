namespace RogStock2025.Screens
{
    partial class frmReportUserGroupsListing
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTNShow = new System.Windows.Forms.Button();
            this.CHKAll = new System.Windows.Forms.CheckBox();
            this.CMBUSRGRP_User = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.RPTUOM = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BTNClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CHKAll);
            this.panel1.Controls.Add(this.CMBUSRGRP_User);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Location = new System.Drawing.Point(6, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 40);
            this.panel1.TabIndex = 49;
            // 
            // BTNShow
            // 
            this.BTNShow.Location = new System.Drawing.Point(912, 17);
            this.BTNShow.Name = "BTNShow";
            this.BTNShow.Size = new System.Drawing.Size(96, 23);
            this.BTNShow.TabIndex = 0;
            this.BTNShow.Text = "Show Report";
            this.BTNShow.UseVisualStyleBackColor = true;
            this.BTNShow.Click += new System.EventHandler(this.BTNShow_Click);
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
            // CMBUSRGRP_User
            // 
            this.CMBUSRGRP_User.FormattingEnabled = true;
            this.CMBUSRGRP_User.Location = new System.Drawing.Point(52, 5);
            this.CMBUSRGRP_User.MaxLength = 20;
            this.CMBUSRGRP_User.Name = "CMBUSRGRP_User";
            this.CMBUSRGRP_User.Size = new System.Drawing.Size(200, 21);
            this.CMBUSRGRP_User.TabIndex = 0;
            this.CMBUSRGRP_User.Tag = "1";
            this.CMBUSRGRP_User.SelectedValueChanged += new System.EventHandler(this.CMBUSRGRP_User_SelectedValueChanged);
            this.CMBUSRGRP_User.Leave += new System.EventHandler(this.CMBUSRGRP_User_Leave);
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
            // RPTUOM
            // 
            this.RPTUOM.Enabled = false;
            this.RPTUOM.Location = new System.Drawing.Point(6, 130);
            this.RPTUOM.Name = "RPTUOM";
            this.RPTUOM.ServerReport.BearerToken = null;
            this.RPTUOM.ServerReport.DisplayName = "UOM Listing";
            this.RPTUOM.ServerReport.ReportPath = "/projects/VisualStudio/Csharp/RogStock2025_Reports/RogStock_Reports/Reports\\rptUO" +
    "M";
            this.RPTUOM.ServerReport.ReportServerUrl = new System.Uri("http://DESKTOP-694Q8HR:80/ReportServer", System.UriKind.Absolute);
            this.RPTUOM.Size = new System.Drawing.Size(1008, 520);
            this.RPTUOM.TabIndex = 48;
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(912, 64);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(96, 23);
            this.BTNClose.TabIndex = 1;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // frmReportUserGroupsListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 656);
            this.Controls.Add(this.BTNShow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RPTUOM);
            this.Controls.Add(this.BTNClose);
            this.Name = "frmReportUserGroupsListing";
            this.Text = "User Groups Listing";
            this.Load += new System.EventHandler(this.frmReportUserGroupsListing_Load);
            this.Shown += new System.EventHandler(this.frmReportUserGroupsListing_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmReportUserGroupsListing_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTNShow;
        private System.Windows.Forms.CheckBox CHKAll;
        private System.Windows.Forms.ComboBox CMBUSRGRP_User;
        private System.Windows.Forms.Label Label1;
        private Microsoft.Reporting.WinForms.ReportViewer RPTUOM;
        private System.Windows.Forms.Button BTNClose;
    }
}