using System.Drawing;
using System.Windows.Forms;
using System;

namespace RogStock2026.Screens
{
    partial class frmReportStockItemsListing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportStockItemsListing));
            this.RPTItems = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BTNShow = new System.Windows.Forms.Button();
            this.PANItemID = new System.Windows.Forms.Panel();
            this.CHKMedia = new System.Windows.Forms.CheckBox();
            this.CHKAllItemID = new System.Windows.Forms.CheckBox();
            this.CMBSTKI_ItemID = new System.Windows.Forms.ComboBox();
            this.LBLSTKI_ItemID = new System.Windows.Forms.Label();
            this.PANUOM = new System.Windows.Forms.Panel();
            this.CHKAllUOM = new System.Windows.Forms.CheckBox();
            this.CMBSTKI_UOM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PANProdFam = new System.Windows.Forms.Panel();
            this.CHKAllProdFam = new System.Windows.Forms.CheckBox();
            this.CMBSTKI_ProductFamily = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PANLocLot = new System.Windows.Forms.Panel();
            this.CHKAllLocLot = new System.Windows.Forms.CheckBox();
            this.CHKLocLotSelect = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CHKLocLot = new System.Windows.Forms.CheckBox();
            this.CHKProdFam = new System.Windows.Forms.CheckBox();
            this.CHKUOM = new System.Windows.Forms.CheckBox();
            this.CHKItemID = new System.Windows.Forms.CheckBox();
            this.CHKNoFilter = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.PANItemID.SuspendLayout();
            this.PANUOM.SuspendLayout();
            this.PANProdFam.SuspendLayout();
            this.PANLocLot.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // RPTItems
            // 
            this.RPTItems.AutoSize = true;
            this.RPTItems.DocumentMapWidth = 46;
            this.RPTItems.Enabled = false;
            this.RPTItems.Location = new System.Drawing.Point(6, 311);
            this.RPTItems.Name = "RPTItems";
            this.RPTItems.ServerReport.BearerToken = null;
            this.RPTItems.ServerReport.DisplayName = "Stock Items Listing";
            this.RPTItems.ServerReport.ReportServerUrl = new System.Uri("http://DESKTOP-694Q8HR:80/ReportServer", System.UriKind.Absolute);
            this.RPTItems.Size = new System.Drawing.Size(1335, 503);
            this.RPTItems.TabIndex = 54;
            // 
            // BTNShow
            // 
            this.BTNShow.Location = new System.Drawing.Point(1245, 85);
            this.BTNShow.Name = "BTNShow";
            this.BTNShow.Size = new System.Drawing.Size(96, 23);
            this.BTNShow.TabIndex = 0;
            this.BTNShow.Text = "Show Report";
            this.BTNShow.UseVisualStyleBackColor = true;
            this.BTNShow.Click += new System.EventHandler(this.BTNShow_Click);
            // 
            // PANItemID
            // 
            this.PANItemID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANItemID.Controls.Add(this.CHKMedia);
            this.PANItemID.Controls.Add(this.CHKAllItemID);
            this.PANItemID.Controls.Add(this.CMBSTKI_ItemID);
            this.PANItemID.Controls.Add(this.LBLSTKI_ItemID);
            this.PANItemID.Enabled = false;
            this.PANItemID.Location = new System.Drawing.Point(166, 117);
            this.PANItemID.Name = "PANItemID";
            this.PANItemID.Size = new System.Drawing.Size(377, 53);
            this.PANItemID.TabIndex = 56;
            // 
            // CHKMedia
            // 
            this.CHKMedia.AutoSize = true;
            this.CHKMedia.Location = new System.Drawing.Point(110, 31);
            this.CHKMedia.Name = "CHKMedia";
            this.CHKMedia.Size = new System.Drawing.Size(154, 17);
            this.CHKMedia.TabIndex = 54;
            this.CHKMedia.Text = "Include Media Information?";
            this.CHKMedia.UseVisualStyleBackColor = true;
            // 
            // CHKAllItemID
            // 
            this.CHKAllItemID.AutoSize = true;
            this.CHKAllItemID.Checked = true;
            this.CHKAllItemID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKAllItemID.Location = new System.Drawing.Point(322, 7);
            this.CHKAllItemID.Name = "CHKAllItemID";
            this.CHKAllItemID.Size = new System.Drawing.Size(37, 17);
            this.CHKAllItemID.TabIndex = 5;
            this.CHKAllItemID.Text = "All";
            this.CHKAllItemID.UseVisualStyleBackColor = true;
            this.CHKAllItemID.CheckedChanged += new System.EventHandler(this.CHKAllItemID_CheckedChanged);
            // 
            // CMBSTKI_ItemID
            // 
            this.CMBSTKI_ItemID.FormattingEnabled = true;
            this.CMBSTKI_ItemID.Location = new System.Drawing.Point(110, 5);
            this.CMBSTKI_ItemID.MaxLength = 20;
            this.CMBSTKI_ItemID.Name = "CMBSTKI_ItemID";
            this.CMBSTKI_ItemID.Size = new System.Drawing.Size(200, 21);
            this.CMBSTKI_ItemID.TabIndex = 4;
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
            // PANUOM
            // 
            this.PANUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANUOM.Controls.Add(this.CHKAllUOM);
            this.PANUOM.Controls.Add(this.CMBSTKI_UOM);
            this.PANUOM.Controls.Add(this.label1);
            this.PANUOM.Enabled = false;
            this.PANUOM.Location = new System.Drawing.Point(166, 176);
            this.PANUOM.Name = "PANUOM";
            this.PANUOM.Size = new System.Drawing.Size(377, 32);
            this.PANUOM.TabIndex = 57;
            // 
            // CHKAllUOM
            // 
            this.CHKAllUOM.AutoSize = true;
            this.CHKAllUOM.Checked = true;
            this.CHKAllUOM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKAllUOM.Location = new System.Drawing.Point(322, 8);
            this.CHKAllUOM.Name = "CHKAllUOM";
            this.CHKAllUOM.Size = new System.Drawing.Size(37, 17);
            this.CHKAllUOM.TabIndex = 7;
            this.CHKAllUOM.Text = "All";
            this.CHKAllUOM.UseVisualStyleBackColor = true;
            this.CHKAllUOM.CheckedChanged += new System.EventHandler(this.CHKAllUOM_CheckedChanged);
            // 
            // CMBSTKI_UOM
            // 
            this.CMBSTKI_UOM.FormattingEnabled = true;
            this.CMBSTKI_UOM.Location = new System.Drawing.Point(110, 7);
            this.CMBSTKI_UOM.MaxLength = 20;
            this.CMBSTKI_UOM.Name = "CMBSTKI_UOM";
            this.CMBSTKI_UOM.Size = new System.Drawing.Size(200, 21);
            this.CMBSTKI_UOM.TabIndex = 6;
            this.CMBSTKI_UOM.Tag = "1";
            this.CMBSTKI_UOM.SelectedValueChanged += new System.EventHandler(this.CMBSTKU_UOM_SelectedValueChanged);
            this.CMBSTKI_UOM.Leave += new System.EventHandler(this.CMBSTKU_UOM_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(26, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "UOM:";
            // 
            // PANProdFam
            // 
            this.PANProdFam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANProdFam.Controls.Add(this.CHKAllProdFam);
            this.PANProdFam.Controls.Add(this.CMBSTKI_ProductFamily);
            this.PANProdFam.Controls.Add(this.label2);
            this.PANProdFam.Enabled = false;
            this.PANProdFam.Location = new System.Drawing.Point(166, 217);
            this.PANProdFam.Name = "PANProdFam";
            this.PANProdFam.Size = new System.Drawing.Size(377, 32);
            this.PANProdFam.TabIndex = 58;
            // 
            // CHKAllProdFam
            // 
            this.CHKAllProdFam.AutoSize = true;
            this.CHKAllProdFam.Checked = true;
            this.CHKAllProdFam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKAllProdFam.Location = new System.Drawing.Point(322, 8);
            this.CHKAllProdFam.Name = "CHKAllProdFam";
            this.CHKAllProdFam.Size = new System.Drawing.Size(37, 17);
            this.CHKAllProdFam.TabIndex = 9;
            this.CHKAllProdFam.Text = "All";
            this.CHKAllProdFam.UseVisualStyleBackColor = true;
            this.CHKAllProdFam.CheckedChanged += new System.EventHandler(this.CHKProdFam_CheckedChanged);
            // 
            // CMBSTKI_ProductFamily
            // 
            this.CMBSTKI_ProductFamily.FormattingEnabled = true;
            this.CMBSTKI_ProductFamily.Location = new System.Drawing.Point(110, 5);
            this.CMBSTKI_ProductFamily.MaxLength = 20;
            this.CMBSTKI_ProductFamily.Name = "CMBSTKI_ProductFamily";
            this.CMBSTKI_ProductFamily.Size = new System.Drawing.Size(200, 21);
            this.CMBSTKI_ProductFamily.TabIndex = 8;
            this.CMBSTKI_ProductFamily.Tag = "1";
            this.CMBSTKI_ProductFamily.SelectedValueChanged += new System.EventHandler(this.CMBSTKP_ProductFamily_SelectedValueChanged);
            this.CMBSTKI_ProductFamily.Leave += new System.EventHandler(this.CMBSTKP_ProductFamily_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(26, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Product Family:";
            // 
            // PANLocLot
            // 
            this.PANLocLot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANLocLot.Controls.Add(this.CHKAllLocLot);
            this.PANLocLot.Controls.Add(this.CHKLocLotSelect);
            this.PANLocLot.Enabled = false;
            this.PANLocLot.Location = new System.Drawing.Point(166, 256);
            this.PANLocLot.Name = "PANLocLot";
            this.PANLocLot.Size = new System.Drawing.Size(377, 36);
            this.PANLocLot.TabIndex = 59;
            // 
            // CHKAllLocLot
            // 
            this.CHKAllLocLot.AutoSize = true;
            this.CHKAllLocLot.Checked = true;
            this.CHKAllLocLot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKAllLocLot.Location = new System.Drawing.Point(225, 8);
            this.CHKAllLocLot.Name = "CHKAllLocLot";
            this.CHKAllLocLot.Size = new System.Drawing.Size(37, 17);
            this.CHKAllLocLot.TabIndex = 13;
            this.CHKAllLocLot.Text = "All";
            this.CHKAllLocLot.UseVisualStyleBackColor = true;
            this.CHKAllLocLot.CheckedChanged += new System.EventHandler(this.CHKLocLot_CheckedChanged);
            // 
            // CHKLocLotSelect
            // 
            this.CHKLocLotSelect.AutoSize = true;
            this.CHKLocLotSelect.Location = new System.Drawing.Point(110, 8);
            this.CHKLocLotSelect.Name = "CHKLocLotSelect";
            this.CHKLocLotSelect.Size = new System.Drawing.Size(109, 17);
            this.CHKLocLotSelect.TabIndex = 12;
            this.CHKLocLotSelect.Text = "Loc/Lot Traking?";
            this.CHKLocLotSelect.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CHKLocLot);
            this.panel1.Controls.Add(this.CHKProdFam);
            this.panel1.Controls.Add(this.CHKUOM);
            this.panel1.Controls.Add(this.CHKItemID);
            this.panel1.Controls.Add(this.CHKNoFilter);
            this.panel1.Location = new System.Drawing.Point(12, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(148, 219);
            this.panel1.TabIndex = 60;
            // 
            // CHKLocLot
            // 
            this.CHKLocLot.AutoSize = true;
            this.CHKLocLot.Location = new System.Drawing.Point(22, 183);
            this.CHKLocLot.Name = "CHKLocLot";
            this.CHKLocLot.Size = new System.Drawing.Size(109, 17);
            this.CHKLocLot.TabIndex = 4;
            this.CHKLocLot.Text = "Loc/Lot Tracking";
            this.CHKLocLot.UseVisualStyleBackColor = true;
            this.CHKLocLot.CheckedChanged += new System.EventHandler(this.CHKLocLot_CheckedChanged);
            // 
            // CHKProdFam
            // 
            this.CHKProdFam.AutoSize = true;
            this.CHKProdFam.Location = new System.Drawing.Point(22, 144);
            this.CHKProdFam.Name = "CHKProdFam";
            this.CHKProdFam.Size = new System.Drawing.Size(95, 17);
            this.CHKProdFam.TabIndex = 3;
            this.CHKProdFam.Text = "Product Family";
            this.CHKProdFam.UseVisualStyleBackColor = true;
            this.CHKProdFam.CheckedChanged += new System.EventHandler(this.CHKProdFam_CheckedChanged);
            // 
            // CHKUOM
            // 
            this.CHKUOM.AutoSize = true;
            this.CHKUOM.Location = new System.Drawing.Point(22, 104);
            this.CHKUOM.Name = "CHKUOM";
            this.CHKUOM.Size = new System.Drawing.Size(51, 17);
            this.CHKUOM.TabIndex = 2;
            this.CHKUOM.Text = "UOM";
            this.CHKUOM.UseVisualStyleBackColor = true;
            this.CHKUOM.CheckedChanged += new System.EventHandler(this.CHKUOM_CheckedChanged);
            // 
            // CHKItemID
            // 
            this.CHKItemID.AutoSize = true;
            this.CHKItemID.Location = new System.Drawing.Point(22, 44);
            this.CHKItemID.Name = "CHKItemID";
            this.CHKItemID.Size = new System.Drawing.Size(60, 17);
            this.CHKItemID.TabIndex = 1;
            this.CHKItemID.Text = "Item ID";
            this.CHKItemID.UseVisualStyleBackColor = true;
            this.CHKItemID.CheckedChanged += new System.EventHandler(this.CHKItemID_CheckedChanged);
            // 
            // CHKNoFilter
            // 
            this.CHKNoFilter.AutoSize = true;
            this.CHKNoFilter.Checked = true;
            this.CHKNoFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKNoFilter.Location = new System.Drawing.Point(22, 12);
            this.CHKNoFilter.Name = "CHKNoFilter";
            this.CHKNoFilter.Size = new System.Drawing.Size(65, 17);
            this.CHKNoFilter.TabIndex = 0;
            this.CHKNoFilter.Text = "No Filter";
            this.CHKNoFilter.UseVisualStyleBackColor = true;
            this.CHKNoFilter.CheckedChanged += new System.EventHandler(this.CHKNoFilter_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 61;
            this.label3.Text = "Filter Options";
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(1, 1);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(1352, 34);
            this.PANTitle.TabIndex = 85;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(1326, 3);
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
            // frmReportStockItemsListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 822);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PANLocLot);
            this.Controls.Add(this.PANProdFam);
            this.Controls.Add(this.PANUOM);
            this.Controls.Add(this.PANItemID);
            this.Controls.Add(this.RPTItems);
            this.Controls.Add(this.BTNShow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmReportStockItemsListing";
            this.Text = "Report Stock Items";
            this.Load += new System.EventHandler(this.frmReportStockItemsListing_Load);
            this.Shown += new System.EventHandler(this.frmReportStockItemsListing_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmReportStockItemsListing_Paint);
            this.PANItemID.ResumeLayout(false);
            this.PANItemID.PerformLayout();
            this.PANUOM.ResumeLayout(false);
            this.PANUOM.PerformLayout();
            this.PANProdFam.ResumeLayout(false);
            this.PANProdFam.PerformLayout();
            this.PANLocLot.ResumeLayout(false);
            this.PANLocLot.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer RPTItems;
        private System.Windows.Forms.Button BTNShow;
        private System.Windows.Forms.Panel PANItemID;
        private System.Windows.Forms.CheckBox CHKAllItemID;
        private System.Windows.Forms.ComboBox CMBSTKI_ItemID;
        private System.Windows.Forms.Label LBLSTKI_ItemID;
        private System.Windows.Forms.Panel PANUOM;
        private System.Windows.Forms.CheckBox CHKAllUOM;
        private System.Windows.Forms.ComboBox CMBSTKI_UOM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PANProdFam;
        private System.Windows.Forms.CheckBox CHKAllProdFam;
        private System.Windows.Forms.ComboBox CMBSTKI_ProductFamily;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PANLocLot;
        private System.Windows.Forms.CheckBox CHKLocLotSelect;
        private System.Windows.Forms.CheckBox CHKAllLocLot;
        private CheckBox CHKMedia;
        private Panel panel1;
        private CheckBox CHKLocLot;
        private CheckBox CHKProdFam;
        private CheckBox CHKUOM;
        private CheckBox CHKItemID;
        private CheckBox CHKNoFilter;
        private Label label3;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
    }
}