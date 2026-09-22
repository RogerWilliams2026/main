using System.Drawing;
using System.Windows.Forms;
using System;

namespace RogStock2026.Screens
{
    partial class frmReportLocationsListing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportLocationsListing));
            this.PANNoFilter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.CHKAllLocations = new System.Windows.Forms.CheckBox();
            this.CMBLOC_Location = new System.Windows.Forms.ComboBox();
            this.CHKALLItems = new System.Windows.Forms.CheckBox();
            this.CMBSTKI_ItemID = new System.Windows.Forms.ComboBox();
            this.LBLSTKI_ItemID = new System.Windows.Forms.Label();
            this.BTNShow = new System.Windows.Forms.Button();
            this.RPTLocations = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.PANNoFilter.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // PANNoFilter
            // 
            this.PANNoFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANNoFilter.Controls.Add(this.label1);
            this.PANNoFilter.Controls.Add(this.CHKAllLocations);
            this.PANNoFilter.Controls.Add(this.CMBLOC_Location);
            this.PANNoFilter.Controls.Add(this.CHKALLItems);
            this.PANNoFilter.Controls.Add(this.CMBSTKI_ItemID);
            this.PANNoFilter.Controls.Add(this.LBLSTKI_ItemID);
            this.PANNoFilter.Location = new System.Drawing.Point(12, 36);
            this.PANNoFilter.Name = "PANNoFilter";
            this.PANNoFilter.Size = new System.Drawing.Size(366, 68);
            this.PANNoFilter.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(26, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 61;
            this.label1.Text = "Location:";
            // 
            // CHKAllLocations
            // 
            this.CHKAllLocations.AutoSize = true;
            this.CHKAllLocations.Checked = true;
            this.CHKAllLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKAllLocations.Location = new System.Drawing.Point(322, 37);
            this.CHKAllLocations.Name = "CHKAllLocations";
            this.CHKAllLocations.Size = new System.Drawing.Size(37, 17);
            this.CHKAllLocations.TabIndex = 3;
            this.CHKAllLocations.Text = "All";
            this.CHKAllLocations.UseVisualStyleBackColor = true;
            this.CHKAllLocations.CheckedChanged += new System.EventHandler(this.CHKAllLocations_CheckedChanged);
            // 
            // CMBLOC_Location
            // 
            this.CMBLOC_Location.FormattingEnabled = true;
            this.CMBLOC_Location.Location = new System.Drawing.Point(110, 33);
            this.CMBLOC_Location.MaxLength = 30;
            this.CMBLOC_Location.Name = "CMBLOC_Location";
            this.CMBLOC_Location.Size = new System.Drawing.Size(200, 21);
            this.CMBLOC_Location.TabIndex = 2;
            this.CMBLOC_Location.Tag = "1";
            this.CMBLOC_Location.SelectedValueChanged += new System.EventHandler(this.CMBLOC_Location_SelectedValueChanged);
            this.CMBLOC_Location.Leave += new System.EventHandler(this.CMBLOC_Location_Leave);
            // 
            // CHKALLItems
            // 
            this.CHKALLItems.AutoSize = true;
            this.CHKALLItems.Checked = true;
            this.CHKALLItems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKALLItems.Location = new System.Drawing.Point(322, 8);
            this.CHKALLItems.Name = "CHKALLItems";
            this.CHKALLItems.Size = new System.Drawing.Size(37, 17);
            this.CHKALLItems.TabIndex = 1;
            this.CHKALLItems.Text = "All";
            this.CHKALLItems.UseVisualStyleBackColor = true;
            this.CHKALLItems.CheckedChanged += new System.EventHandler(this.CHKALLItems_CheckedChanged);
            // 
            // CMBSTKI_ItemID
            // 
            this.CMBSTKI_ItemID.FormattingEnabled = true;
            this.CMBSTKI_ItemID.Location = new System.Drawing.Point(110, 6);
            this.CMBSTKI_ItemID.MaxLength = 20;
            this.CMBSTKI_ItemID.Name = "CMBSTKI_ItemID";
            this.CMBSTKI_ItemID.Size = new System.Drawing.Size(200, 21);
            this.CMBSTKI_ItemID.TabIndex = 0;
            this.CMBSTKI_ItemID.Tag = "1";
            this.CMBSTKI_ItemID.SelectedValueChanged += new System.EventHandler(this.CMBSTKI_ItemID_SelectedValueChanged);
            this.CMBSTKI_ItemID.Leave += new System.EventHandler(this.CMBSTKI_ItemID_Leave);
            // 
            // LBLSTKI_ItemID
            // 
            this.LBLSTKI_ItemID.AutoSize = true;
            this.LBLSTKI_ItemID.ForeColor = System.Drawing.Color.Black;
            this.LBLSTKI_ItemID.Location = new System.Drawing.Point(28, 10);
            this.LBLSTKI_ItemID.Name = "LBLSTKI_ItemID";
            this.LBLSTKI_ItemID.Size = new System.Drawing.Size(44, 13);
            this.LBLSTKI_ItemID.TabIndex = 53;
            this.LBLSTKI_ItemID.Text = "Item ID:";
            // 
            // BTNShow
            // 
            this.BTNShow.Location = new System.Drawing.Point(923, 44);
            this.BTNShow.Name = "BTNShow";
            this.BTNShow.Size = new System.Drawing.Size(96, 23);
            this.BTNShow.TabIndex = 0;
            this.BTNShow.Text = "Show Report";
            this.BTNShow.UseVisualStyleBackColor = true;
            this.BTNShow.Click += new System.EventHandler(this.BTNShow_Click);
            // 
            // RPTLocations
            // 
            this.RPTLocations.Enabled = false;
            this.RPTLocations.Location = new System.Drawing.Point(11, 140);
            this.RPTLocations.Name = "RPTLocations";
            this.RPTLocations.ServerReport.BearerToken = null;
            this.RPTLocations.ServerReport.DisplayName = "Locations Listing";
            this.RPTLocations.ServerReport.ReportPath = "/projects/VisualStudio/Csharp/RogStock2026_Reports/RogStock_Reports/Reports\\rptLo" +
    "cations";
            this.RPTLocations.ServerReport.ReportServerUrl = new System.Uri("http://DESKTOP-694Q8HR:80/ReportServer", System.UriKind.Absolute);
            this.RPTLocations.Size = new System.Drawing.Size(1008, 419);
            this.RPTLocations.TabIndex = 58;
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(1, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(1027, 34);
            this.PANTitle.TabIndex = 80;
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
            // frmReportLocationsListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 565);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.RPTLocations);
            this.Controls.Add(this.BTNShow);
            this.Controls.Add(this.PANNoFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmReportLocationsListing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report Locations";
            this.Load += new System.EventHandler(this.frmReportLocations_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmReportLocationsListing_Paint);
            this.PANNoFilter.ResumeLayout(false);
            this.PANNoFilter.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PANNoFilter;
        private System.Windows.Forms.CheckBox CHKALLItems;
        private System.Windows.Forms.ComboBox CMBSTKI_ItemID;
        private System.Windows.Forms.Label LBLSTKI_ItemID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CHKAllLocations;
        private System.Windows.Forms.ComboBox CMBLOC_Location;
        private System.Windows.Forms.Button BTNShow;
        private Microsoft.Reporting.WinForms.ReportViewer RPTLocations;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
    }
}