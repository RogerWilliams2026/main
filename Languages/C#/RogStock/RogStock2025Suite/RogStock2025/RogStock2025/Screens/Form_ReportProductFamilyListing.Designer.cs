using System.Drawing;
using System.Windows.Forms;
using System;

namespace RogStock2025.Screens
{
    partial class frmReportProductFamilyListing
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTNShow = new System.Windows.Forms.Button();
            this.CHKAll = new System.Windows.Forms.CheckBox();
            this.CMBSTKP_ProductFamily = new System.Windows.Forms.ComboBox();
            this.LBLSTKP_ProdFam = new System.Windows.Forms.Label();
            this.RPTProdFam = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(927, 53);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(96, 23);
            this.BTNClose.TabIndex = 1;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CHKAll);
            this.panel1.Controls.Add(this.CMBSTKP_ProductFamily);
            this.panel1.Controls.Add(this.LBLSTKP_ProdFam);
            this.panel1.Location = new System.Drawing.Point(15, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 38);
            this.panel1.TabIndex = 49;
            // 
            // BTNShow
            // 
            this.BTNShow.Location = new System.Drawing.Point(927, 12);
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
            this.CHKAll.Location = new System.Drawing.Point(302, 10);
            this.CHKAll.Name = "CHKAll";
            this.CHKAll.Size = new System.Drawing.Size(37, 17);
            this.CHKAll.TabIndex = 1;
            this.CHKAll.Text = "All";
            this.CHKAll.UseVisualStyleBackColor = true;
            this.CHKAll.Click += new System.EventHandler(this.CHKAll_CheckedChanged);
            // 
            // CMBSTKP_ProductFamily
            // 
            this.CMBSTKP_ProductFamily.FormattingEnabled = true;
            this.CMBSTKP_ProductFamily.Location = new System.Drawing.Point(90, 6);
            this.CMBSTKP_ProductFamily.MaxLength = 20;
            this.CMBSTKP_ProductFamily.Name = "CMBSTKP_ProductFamily";
            this.CMBSTKP_ProductFamily.Size = new System.Drawing.Size(200, 21);
            this.CMBSTKP_ProductFamily.TabIndex = 0;
            this.CMBSTKP_ProductFamily.Tag = "1";
            this.CMBSTKP_ProductFamily.SelectedValueChanged += new System.EventHandler(this.CMBSTKP_ProductFamily_SelectedValueChanged);
            this.CMBSTKP_ProductFamily.Leave += new System.EventHandler(this.CMBSTKP_ProductFamily_Leave);
            // 
            // LBLSTKP_ProdFam
            // 
            this.LBLSTKP_ProdFam.AutoSize = true;
            this.LBLSTKP_ProdFam.ForeColor = System.Drawing.Color.Black;
            this.LBLSTKP_ProdFam.Location = new System.Drawing.Point(6, 10);
            this.LBLSTKP_ProdFam.Name = "LBLSTKP_ProdFam";
            this.LBLSTKP_ProdFam.Size = new System.Drawing.Size(79, 13);
            this.LBLSTKP_ProdFam.TabIndex = 48;
            this.LBLSTKP_ProdFam.Text = "Product Family:";
            // 
            // RPTProdFam
            // 
            this.RPTProdFam.Enabled = false;
            this.RPTProdFam.Location = new System.Drawing.Point(15, 130);
            this.RPTProdFam.Name = "RPTProdFam";
            this.RPTProdFam.ServerReport.BearerToken = null;
            this.RPTProdFam.ServerReport.DisplayName = "Product Family Listing";
            this.RPTProdFam.ServerReport.ReportPath = "/projects/VisualStudio/Csharp/RogStock2025_Reports/RogStock_Reports/Reports\\rptPr" +
    "oductFamily";
            this.RPTProdFam.ServerReport.ReportServerUrl = new System.Uri("http://DESKTOP-694Q8HR:80/ReportServer", System.UriKind.Absolute);
            this.RPTProdFam.Size = new System.Drawing.Size(1008, 515);
            this.RPTProdFam.TabIndex = 50;
            // 
            // frmReportProductFamilyListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 657);
            this.Controls.Add(this.BTNShow);
            this.Controls.Add(this.RPTProdFam);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BTNClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmReportProductFamilyListing";
            this.Text = "Product Family Listing";
            this.Load += new System.EventHandler(this.frmReportProductFamilyListing_Load);
            this.Shown += new System.EventHandler(this.frmReportProductFamilyListing_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmReportProductFamilyListing_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer RPTProdFam;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTNShow;
        private System.Windows.Forms.CheckBox CHKAll;
        private System.Windows.Forms.ComboBox CMBSTKP_ProductFamily;
        private System.Windows.Forms.Label LBLSTKP_ProdFam;
    }
}