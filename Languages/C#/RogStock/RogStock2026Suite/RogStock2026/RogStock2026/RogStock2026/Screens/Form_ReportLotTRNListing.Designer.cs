using System.Drawing;
using System.Windows.Forms;
using System;

namespace RogStock2026.Screens
{
    partial class frmReportLotTRNListing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportLotTRNListing));
            this.PANLocLot = new System.Windows.Forms.Panel();
            this.CMBLOT_Nbr = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CHKALLLots = new System.Windows.Forms.CheckBox();
            this.PANProdFam = new System.Windows.Forms.Panel();
            this.CHKAllDates = new System.Windows.Forms.CheckBox();
            this.DTETo = new System.Windows.Forms.DateTimePicker();
            this.DTEFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PANUOM = new System.Windows.Forms.Panel();
            this.CHKALLOperations = new System.Windows.Forms.CheckBox();
            this.CMBLOTT_Operation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PANNoFilter = new System.Windows.Forms.Panel();
            this.CHKALLItems = new System.Windows.Forms.CheckBox();
            this.CMBSTKI_ItemID = new System.Windows.Forms.ComboBox();
            this.LBLSTKI_ItemID = new System.Windows.Forms.Label();
            this.BTNShow = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.CHKAllLocations = new System.Windows.Forms.CheckBox();
            this.CMBLOC_Location = new System.Windows.Forms.ComboBox();
            this.RPTLotTRN = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.PANLocLot.SuspendLayout();
            this.PANProdFam.SuspendLayout();
            this.PANUOM.SuspendLayout();
            this.PANNoFilter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // PANLocLot
            // 
            this.PANLocLot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANLocLot.Controls.Add(this.CMBLOT_Nbr);
            this.PANLocLot.Controls.Add(this.label3);
            this.PANLocLot.Controls.Add(this.CHKALLLots);
            this.PANLocLot.Location = new System.Drawing.Point(12, 46);
            this.PANLocLot.Name = "PANLocLot";
            this.PANLocLot.Size = new System.Drawing.Size(395, 32);
            this.PANLocLot.TabIndex = 73;
            // 
            // CMBLOT_Nbr
            // 
            this.CMBLOT_Nbr.FormattingEnabled = true;
            this.CMBLOT_Nbr.Location = new System.Drawing.Point(110, 6);
            this.CMBLOT_Nbr.Name = "CMBLOT_Nbr";
            this.CMBLOT_Nbr.Size = new System.Drawing.Size(200, 21);
            this.CMBLOT_Nbr.TabIndex = 0;
            this.CMBLOT_Nbr.Tag = "1";
            this.CMBLOT_Nbr.SelectedValueChanged += new System.EventHandler(this.CMBLOT_Nbr_SelectedValueChanged);
            this.CMBLOT_Nbr.Leave += new System.EventHandler(this.CMBLOT_Nbr_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(25, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "Lot:";
            // 
            // CHKALLLots
            // 
            this.CHKALLLots.AutoSize = true;
            this.CHKALLLots.Checked = true;
            this.CHKALLLots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKALLLots.Location = new System.Drawing.Point(321, 11);
            this.CHKALLLots.Name = "CHKALLLots";
            this.CHKALLLots.Size = new System.Drawing.Size(37, 17);
            this.CHKALLLots.TabIndex = 1;
            this.CHKALLLots.Text = "All";
            this.CHKALLLots.UseVisualStyleBackColor = true;
            this.CHKALLLots.Click += new System.EventHandler(this.CHKALLLots_CheckedChanged);
            // 
            // PANProdFam
            // 
            this.PANProdFam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANProdFam.Controls.Add(this.CHKAllDates);
            this.PANProdFam.Controls.Add(this.DTETo);
            this.PANProdFam.Controls.Add(this.DTEFrom);
            this.PANProdFam.Controls.Add(this.label4);
            this.PANProdFam.Controls.Add(this.label2);
            this.PANProdFam.Location = new System.Drawing.Point(12, 135);
            this.PANProdFam.Name = "PANProdFam";
            this.PANProdFam.Size = new System.Drawing.Size(395, 32);
            this.PANProdFam.TabIndex = 72;
            // 
            // CHKAllDates
            // 
            this.CHKAllDates.AutoSize = true;
            this.CHKAllDates.Checked = true;
            this.CHKAllDates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKAllDates.Location = new System.Drawing.Point(350, 8);
            this.CHKAllDates.Name = "CHKAllDates";
            this.CHKAllDates.Size = new System.Drawing.Size(37, 17);
            this.CHKAllDates.TabIndex = 10;
            this.CHKAllDates.Text = "All";
            this.CHKAllDates.UseVisualStyleBackColor = true;
            this.CHKAllDates.Click += new System.EventHandler(this.CHKAllDates_CheckedChanged);
            // 
            // DTETo
            // 
            this.DTETo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTETo.Location = new System.Drawing.Point(243, 6);
            this.DTETo.MinDate = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            this.DTETo.Name = "DTETo";
            this.DTETo.Size = new System.Drawing.Size(93, 20);
            this.DTETo.TabIndex = 9;
            this.DTETo.ValueChanged += new System.EventHandler(this.DTETo_ValueChanged);
            // 
            // DTEFrom
            // 
            this.DTEFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTEFrom.Location = new System.Drawing.Point(89, 5);
            this.DTEFrom.MinDate = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            this.DTEFrom.Name = "DTEFrom";
            this.DTEFrom.Size = new System.Drawing.Size(93, 20);
            this.DTEFrom.TabIndex = 8;
            this.DTEFrom.ValueChanged += new System.EventHandler(this.DTEFrom_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(192, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Date To:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(26, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Date From:";
            // 
            // PANUOM
            // 
            this.PANUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANUOM.Controls.Add(this.CHKALLOperations);
            this.PANUOM.Controls.Add(this.CMBLOTT_Operation);
            this.PANUOM.Controls.Add(this.label1);
            this.PANUOM.Location = new System.Drawing.Point(427, 94);
            this.PANUOM.Name = "PANUOM";
            this.PANUOM.Size = new System.Drawing.Size(395, 32);
            this.PANUOM.TabIndex = 71;
            // 
            // CHKALLOperations
            // 
            this.CHKALLOperations.AutoSize = true;
            this.CHKALLOperations.Checked = true;
            this.CHKALLOperations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKALLOperations.Location = new System.Drawing.Point(322, 8);
            this.CHKALLOperations.Name = "CHKALLOperations";
            this.CHKALLOperations.Size = new System.Drawing.Size(37, 17);
            this.CHKALLOperations.TabIndex = 7;
            this.CHKALLOperations.Text = "All";
            this.CHKALLOperations.UseVisualStyleBackColor = true;
            this.CHKALLOperations.Click += new System.EventHandler(this.CHKALLOperations_CheckedChanged);
            // 
            // CMBLOTT_Operation
            // 
            this.CMBLOTT_Operation.FormattingEnabled = true;
            this.CMBLOTT_Operation.Location = new System.Drawing.Point(110, 7);
            this.CMBLOTT_Operation.MaxLength = 20;
            this.CMBLOTT_Operation.Name = "CMBLOTT_Operation";
            this.CMBLOTT_Operation.Size = new System.Drawing.Size(200, 21);
            this.CMBLOTT_Operation.TabIndex = 6;
            this.CMBLOTT_Operation.Tag = "1";
            this.CMBLOTT_Operation.SelectedValueChanged += new System.EventHandler(this.CMBLOTT_Operation_SelectedValueChanged);
            this.CMBLOTT_Operation.Leave += new System.EventHandler(this.CMBLOTT_Operation_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(26, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Operation:";
            // 
            // PANNoFilter
            // 
            this.PANNoFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANNoFilter.Controls.Add(this.CHKALLItems);
            this.PANNoFilter.Controls.Add(this.CMBSTKI_ItemID);
            this.PANNoFilter.Controls.Add(this.LBLSTKI_ItemID);
            this.PANNoFilter.Location = new System.Drawing.Point(12, 88);
            this.PANNoFilter.Name = "PANNoFilter";
            this.PANNoFilter.Size = new System.Drawing.Size(395, 32);
            this.PANNoFilter.TabIndex = 70;
            // 
            // CHKALLItems
            // 
            this.CHKALLItems.AutoSize = true;
            this.CHKALLItems.Checked = true;
            this.CHKALLItems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKALLItems.Location = new System.Drawing.Point(322, 8);
            this.CHKALLItems.Name = "CHKALLItems";
            this.CHKALLItems.Size = new System.Drawing.Size(37, 17);
            this.CHKALLItems.TabIndex = 3;
            this.CHKALLItems.Text = "All";
            this.CHKALLItems.UseVisualStyleBackColor = true;
            this.CHKALLItems.Click += new System.EventHandler(this.CHKALLItems_CheckedChanged);
            // 
            // CMBSTKI_ItemID
            // 
            this.CMBSTKI_ItemID.FormattingEnabled = true;
            this.CMBSTKI_ItemID.Location = new System.Drawing.Point(110, 6);
            this.CMBSTKI_ItemID.MaxLength = 20;
            this.CMBSTKI_ItemID.Name = "CMBSTKI_ItemID";
            this.CMBSTKI_ItemID.Size = new System.Drawing.Size(200, 21);
            this.CMBSTKI_ItemID.TabIndex = 2;
            this.CMBSTKI_ItemID.Tag = "1";
            this.CMBSTKI_ItemID.SelectedValueChanged += new System.EventHandler(this.CMBSTKI_ItemID_SelectedValueChanged);
            this.CMBSTKI_ItemID.Leave += new System.EventHandler(this.CMBSTKI_ItemID_Leave);
            // 
            // LBLSTKI_ItemID
            // 
            this.LBLSTKI_ItemID.AutoSize = true;
            this.LBLSTKI_ItemID.ForeColor = System.Drawing.Color.Black;
            this.LBLSTKI_ItemID.Location = new System.Drawing.Point(26, 7);
            this.LBLSTKI_ItemID.Name = "LBLSTKI_ItemID";
            this.LBLSTKI_ItemID.Size = new System.Drawing.Size(44, 13);
            this.LBLSTKI_ItemID.TabIndex = 53;
            this.LBLSTKI_ItemID.Text = "Item ID:";
            // 
            // BTNShow
            // 
            this.BTNShow.Location = new System.Drawing.Point(924, 46);
            this.BTNShow.Name = "BTNShow";
            this.BTNShow.Size = new System.Drawing.Size(96, 23);
            this.BTNShow.TabIndex = 0;
            this.BTNShow.Text = "Show Report";
            this.BTNShow.UseVisualStyleBackColor = true;
            this.BTNShow.Click += new System.EventHandler(this.BTNShow_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.CHKAllLocations);
            this.panel1.Controls.Add(this.CMBLOC_Location);
            this.panel1.Location = new System.Drawing.Point(427, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 32);
            this.panel1.TabIndex = 74;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(25, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Location:";
            // 
            // CHKAllLocations
            // 
            this.CHKAllLocations.AutoSize = true;
            this.CHKAllLocations.Checked = true;
            this.CHKAllLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKAllLocations.Location = new System.Drawing.Point(321, 11);
            this.CHKAllLocations.Name = "CHKAllLocations";
            this.CHKAllLocations.Size = new System.Drawing.Size(37, 17);
            this.CHKAllLocations.TabIndex = 5;
            this.CHKAllLocations.Text = "All";
            this.CHKAllLocations.UseVisualStyleBackColor = true;
            this.CHKAllLocations.CheckedChanged += new System.EventHandler(this.CHKAllLocations_CheckedChanged);
            // 
            // CMBLOC_Location
            // 
            this.CMBLOC_Location.FormattingEnabled = true;
            this.CMBLOC_Location.Location = new System.Drawing.Point(109, 7);
            this.CMBLOC_Location.MaxLength = 30;
            this.CMBLOC_Location.Name = "CMBLOC_Location";
            this.CMBLOC_Location.Size = new System.Drawing.Size(200, 21);
            this.CMBLOC_Location.TabIndex = 4;
            this.CMBLOC_Location.Tag = "1";
            this.CMBLOC_Location.SelectedValueChanged += new System.EventHandler(this.CMBLOC_Location_SelectedValueChanged);
            this.CMBLOC_Location.Leave += new System.EventHandler(this.CMBLOC_Location_Leave);
            // 
            // RPTLotTRN
            // 
            this.RPTLotTRN.Enabled = false;
            this.RPTLotTRN.Location = new System.Drawing.Point(12, 186);
            this.RPTLotTRN.Name = "RPTLotTRN";
            this.RPTLotTRN.ServerReport.BearerToken = null;
            this.RPTLotTRN.ServerReport.DisplayName = "Lot TRN Listing";
            this.RPTLotTRN.ServerReport.ReportPath = "/projects/VisualStudio/Csharp/RogStock2026_Reports/RogStock_Reports/Reports\\rptLo" +
    "tTRN";
            this.RPTLotTRN.ServerReport.ReportServerUrl = new System.Uri("http://DESKTOP-694Q8HR:80/ReportServer", System.UriKind.Absolute);
            this.RPTLotTRN.Size = new System.Drawing.Size(1008, 479);
            this.RPTLotTRN.TabIndex = 75;
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(1, 2);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(1027, 34);
            this.PANTitle.TabIndex = 83;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
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
            // frmReportLotTRNListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 679);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.RPTLotTRN);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PANLocLot);
            this.Controls.Add(this.PANProdFam);
            this.Controls.Add(this.PANUOM);
            this.Controls.Add(this.PANNoFilter);
            this.Controls.Add(this.BTNShow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmReportLotTRNListing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report Lot Transactions";
            this.Load += new System.EventHandler(this.frmReportLotTRN_Load);
            this.Shown += new System.EventHandler(this.frmReportLotTRN_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmReportLotTRN_Paint);
            this.PANLocLot.ResumeLayout(false);
            this.PANLocLot.PerformLayout();
            this.PANProdFam.ResumeLayout(false);
            this.PANProdFam.PerformLayout();
            this.PANUOM.ResumeLayout(false);
            this.PANUOM.PerformLayout();
            this.PANNoFilter.ResumeLayout(false);
            this.PANNoFilter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PANLocLot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CHKALLLots;
        private System.Windows.Forms.Panel PANProdFam;
        private System.Windows.Forms.CheckBox CHKAllDates;
        private System.Windows.Forms.DateTimePicker DTETo;
        private System.Windows.Forms.DateTimePicker DTEFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PANUOM;
        private System.Windows.Forms.CheckBox CHKALLOperations;
        private System.Windows.Forms.ComboBox CMBLOTT_Operation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PANNoFilter;
        private System.Windows.Forms.CheckBox CHKALLItems;
        private System.Windows.Forms.ComboBox CMBSTKI_ItemID;
        private System.Windows.Forms.Label LBLSTKI_ItemID;
        private Microsoft.Reporting.WinForms.ReportViewer RPTItems;
        private System.Windows.Forms.Button BTNShow;
        private System.Windows.Forms.ComboBox CMBLOT_Nbr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox CHKAllLocations;
        private System.Windows.Forms.ComboBox CMBLOC_Location;
        private Microsoft.Reporting.WinForms.ReportViewer RPTLotTRN;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
    }
}