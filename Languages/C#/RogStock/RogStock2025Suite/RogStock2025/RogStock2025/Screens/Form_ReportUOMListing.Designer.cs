namespace RogStock2025.Screens
{
    partial class frmReportUOMListing
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
            this.BTNClose = new System.Windows.Forms.Button();
            this.RPTUOM = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CHKAll = new System.Windows.Forms.CheckBox();
            this.CMBSTKU_Desc = new System.Windows.Forms.ComboBox();
            this.LBLSTKU_Desc = new System.Windows.Forms.Label();
            this.BTNShow = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(926, 62);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(96, 23);
            this.BTNClose.TabIndex = 1;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // RPTUOM
            // 
            this.RPTUOM.Enabled = false;
            this.RPTUOM.Location = new System.Drawing.Point(14, 118);
            this.RPTUOM.Name = "RPTUOM";
            this.RPTUOM.ServerReport.BearerToken = null;
            this.RPTUOM.ServerReport.DisplayName = "UOM Listing";
            this.RPTUOM.ServerReport.ReportPath = "/projects/VisualStudio/Csharp/RogStock2025_Reports/RogStock_Reports/Reports\\rptUO" +
    "M";
            this.RPTUOM.ServerReport.ReportServerUrl = new System.Uri("http://DESKTOP-694Q8HR:80/ReportServer", System.UriKind.Absolute);
            this.RPTUOM.Size = new System.Drawing.Size(1008, 538);
            this.RPTUOM.TabIndex = 42;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CHKAll);
            this.panel1.Controls.Add(this.CMBSTKU_Desc);
            this.panel1.Controls.Add(this.LBLSTKU_Desc);
            this.panel1.Location = new System.Drawing.Point(14, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(314, 40);
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
            // CMBSTKU_Desc
            // 
            this.CMBSTKU_Desc.FormattingEnabled = true;
            this.CMBSTKU_Desc.Location = new System.Drawing.Point(52, 5);
            this.CMBSTKU_Desc.MaxLength = 20;
            this.CMBSTKU_Desc.Name = "CMBSTKU_Desc";
            this.CMBSTKU_Desc.Size = new System.Drawing.Size(200, 21);
            this.CMBSTKU_Desc.TabIndex = 0;
            this.CMBSTKU_Desc.Tag = "1";
            this.CMBSTKU_Desc.SelectedValueChanged += new System.EventHandler(this.CMBSTKU_Desc_SelectedValueChanged);
            this.CMBSTKU_Desc.Leave += new System.EventHandler(this.CMBSTKU_Desc_Leave);
            // 
            // LBLSTKU_Desc
            // 
            this.LBLSTKU_Desc.AutoSize = true;
            this.LBLSTKU_Desc.ForeColor = System.Drawing.Color.Black;
            this.LBLSTKU_Desc.Location = new System.Drawing.Point(9, 9);
            this.LBLSTKU_Desc.Name = "LBLSTKU_Desc";
            this.LBLSTKU_Desc.Size = new System.Drawing.Size(35, 13);
            this.LBLSTKU_Desc.TabIndex = 41;
            this.LBLSTKU_Desc.Text = "UOM:";
            // 
            // BTNShow
            // 
            this.BTNShow.Location = new System.Drawing.Point(926, 17);
            this.BTNShow.Name = "BTNShow";
            this.BTNShow.Size = new System.Drawing.Size(96, 23);
            this.BTNShow.TabIndex = 0;
            this.BTNShow.Text = "Show Report";
            this.BTNShow.UseVisualStyleBackColor = true;
            this.BTNShow.Click += new System.EventHandler(this.BTNShow_Click);
            // 
            // frmReportUOMListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 668);
            this.Controls.Add(this.BTNShow);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RPTUOM);
            this.Controls.Add(this.BTNClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmReportUOMListing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UOM Listing";
            this.Load += new System.EventHandler(this.frmReportUOMListing_Load);
            this.Shown += new System.EventHandler(this.frmReportUOMListing_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmReportUOMListing_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BTNClose;
        private Microsoft.Reporting.WinForms.ReportViewer RPTUOM;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTNShow;
        private System.Windows.Forms.CheckBox CHKAll;
        private System.Windows.Forms.ComboBox CMBSTKU_Desc;
        private System.Windows.Forms.Label LBLSTKU_Desc;
    }
}