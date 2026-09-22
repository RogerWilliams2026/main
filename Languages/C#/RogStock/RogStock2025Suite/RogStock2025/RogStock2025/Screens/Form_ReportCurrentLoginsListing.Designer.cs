namespace RogStock2025.Screens
{
    partial class frmReportCurrentLoginsListing
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
            this.CHKAll = new System.Windows.Forms.CheckBox();
            this.CMBLOGC_User = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.BTNShow = new System.Windows.Forms.Button();
            this.RPTUOM = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BTNClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CHKAll);
            this.panel1.Controls.Add(this.CMBLOGC_User);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Location = new System.Drawing.Point(12, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 40);
            this.panel1.TabIndex = 46;
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
            // CMBLOGC_User
            // 
            this.CMBLOGC_User.FormattingEnabled = true;
            this.CMBLOGC_User.Location = new System.Drawing.Point(52, 5);
            this.CMBLOGC_User.MaxLength = 20;
            this.CMBLOGC_User.Name = "CMBLOGC_User";
            this.CMBLOGC_User.Size = new System.Drawing.Size(200, 21);
            this.CMBLOGC_User.TabIndex = 0;
            this.CMBLOGC_User.Tag = "1";
            this.CMBLOGC_User.SelectedValueChanged += new System.EventHandler(this.CMBLOGC_User_SelectedValueChanged);
            this.CMBLOGC_User.Leave += new System.EventHandler(this.CMBLOGC_User_Leave);
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
            this.BTNShow.Location = new System.Drawing.Point(910, 7);
            this.BTNShow.Name = "BTNShow";
            this.BTNShow.Size = new System.Drawing.Size(93, 23);
            this.BTNShow.TabIndex = 0;
            this.BTNShow.Text = "Show Report";
            this.BTNShow.UseVisualStyleBackColor = true;
            this.BTNShow.Click += new System.EventHandler(this.BTNShow_Click);
            // 
            // RPTUOM
            // 
            this.RPTUOM.Enabled = false;
            this.RPTUOM.Location = new System.Drawing.Point(12, 101);
            this.RPTUOM.Name = "RPTUOM";
            this.RPTUOM.ServerReport.BearerToken = null;
            this.RPTUOM.ServerReport.DisplayName = "UOM Listing";
            this.RPTUOM.ServerReport.ReportPath = "/projects/VisualStudio/Csharp/RogStock2025_Reports/RogStock_Reports/Reports\\rptUO" +
    "M";
            this.RPTUOM.ServerReport.ReportServerUrl = new System.Uri("http://DESKTOP-694Q8HR:80/ReportServer", System.UriKind.Absolute);
            this.RPTUOM.Size = new System.Drawing.Size(998, 544);
            this.RPTUOM.TabIndex = 45;
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(910, 52);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(93, 23);
            this.BTNClose.TabIndex = 1;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // frmReportCurrentLoginsListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 651);
            this.Controls.Add(this.BTNShow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RPTUOM);
            this.Controls.Add(this.BTNClose);
            this.Name = "frmReportCurrentLoginsListing";
            this.Text = "Current Logins Listing";
            this.Load += new System.EventHandler(this.frmReportCurrentLoginsListing_Load);
            this.Shown += new System.EventHandler(this.frmReportCurrentLoginsListing_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmReportCurrentLoginsListing_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTNShow;
        private System.Windows.Forms.CheckBox CHKAll;
        private System.Windows.Forms.ComboBox CMBLOGC_User;
        private System.Windows.Forms.Label Label1;
        private Microsoft.Reporting.WinForms.ReportViewer RPTUOM;
        private System.Windows.Forms.Button BTNClose;
    }
}