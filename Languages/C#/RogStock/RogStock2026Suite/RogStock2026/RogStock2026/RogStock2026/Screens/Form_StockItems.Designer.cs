using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2026.Screens
{
    partial class frmStockItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockItems));
            this.BTNSave = new System.Windows.Forms.Button();
            this.LBLSTKI_ItemID = new System.Windows.Forms.Label();
            this.CMBSTKI_ItemID = new System.Windows.Forms.ComboBox();
            this.BTNNew = new System.Windows.Forms.Button();
            this.TABStockItem = new System.Windows.Forms.TabControl();
            this.PGEDesc = new System.Windows.Forms.TabPage();
            this.TXTSTKD_LongDesc = new System.Windows.Forms.TextBox();
            this.LBLSTKD_LongDesc = new System.Windows.Forms.Label();
            this.TXTSTKD_Desc = new System.Windows.Forms.TextBox();
            this.LBLSTKD_Desc = new System.Windows.Forms.Label();
            this.PGEMedia = new System.Windows.Forms.TabPage();
            this.WEBPreview = new System.Windows.Forms.WebBrowser();
            this.label8 = new System.Windows.Forms.Label();
            this.BTNRemoveFile = new System.Windows.Forms.Button();
            this.BTNAddFile = new System.Windows.Forms.Button();
            this.LVMedia = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label7 = new System.Windows.Forms.Label();
            this.CMBSTKI_UOM = new System.Windows.Forms.ComboBox();
            this.LBLSTKU_Desc = new System.Windows.Forms.Label();
            this.CHKSTKI_LocLot = new System.Windows.Forms.CheckBox();
            this.NUDSTKI_Price = new System.Windows.Forms.NumericUpDown();
            this.LBLSTKI_Price = new System.Windows.Forms.Label();
            this.CMBSTKI_ProductFamily = new System.Windows.Forms.ComboBox();
            this.LBLSTKI_ProductFamily = new System.Windows.Forms.Label();
            this.OFDSelectFile = new System.Windows.Forms.OpenFileDialog();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.TXTHidden = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BTNFind = new System.Windows.Forms.Button();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.TABStockItem.SuspendLayout();
            this.PGEDesc.SuspendLayout();
            this.PGEMedia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDSTKI_Price)).BeginInit();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(18, 494);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 7;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // LBLSTKI_ItemID
            // 
            this.LBLSTKI_ItemID.AutoSize = true;
            this.LBLSTKI_ItemID.ForeColor = System.Drawing.Color.Red;
            this.LBLSTKI_ItemID.Location = new System.Drawing.Point(8, 44);
            this.LBLSTKI_ItemID.Name = "LBLSTKI_ItemID";
            this.LBLSTKI_ItemID.Size = new System.Drawing.Size(41, 13);
            this.LBLSTKI_ItemID.TabIndex = 2;
            this.LBLSTKI_ItemID.Text = "Item ID";
            // 
            // CMBSTKI_ItemID
            // 
            this.CMBSTKI_ItemID.FormattingEnabled = true;
            this.CMBSTKI_ItemID.Location = new System.Drawing.Point(58, 41);
            this.CMBSTKI_ItemID.MaxLength = 50;
            this.CMBSTKI_ItemID.Name = "CMBSTKI_ItemID";
            this.CMBSTKI_ItemID.Size = new System.Drawing.Size(304, 21);
            this.CMBSTKI_ItemID.TabIndex = 0;
            this.CMBSTKI_ItemID.Tag = "1";
            this.CMBSTKI_ItemID.TextUpdate += new System.EventHandler(this.CMBSTKI_ItemID_TextUpdate);
            this.CMBSTKI_ItemID.SelectedValueChanged += new System.EventHandler(this.CMBSTKI_ItemID_SelectedValueChanged);
            this.CMBSTKI_ItemID.Enter += new System.EventHandler(this.CMBSTKI_ItemID_Enter);
            this.CMBSTKI_ItemID.Leave += new System.EventHandler(this.CMBSTKI_ItemID_TextUpdate);
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(368, 41);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(39, 20);
            this.BTNNew.TabIndex = 1;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // TABStockItem
            // 
            this.TABStockItem.Controls.Add(this.PGEDesc);
            this.TABStockItem.Controls.Add(this.PGEMedia);
            this.TABStockItem.Location = new System.Drawing.Point(20, 117);
            this.TABStockItem.Name = "TABStockItem";
            this.TABStockItem.SelectedIndex = 0;
            this.TABStockItem.Size = new System.Drawing.Size(732, 370);
            this.TABStockItem.TabIndex = 6;
            // 
            // PGEDesc
            // 
            this.PGEDesc.Controls.Add(this.TXTSTKD_LongDesc);
            this.PGEDesc.Controls.Add(this.LBLSTKD_LongDesc);
            this.PGEDesc.Controls.Add(this.TXTSTKD_Desc);
            this.PGEDesc.Controls.Add(this.LBLSTKD_Desc);
            this.PGEDesc.Location = new System.Drawing.Point(4, 22);
            this.PGEDesc.Name = "PGEDesc";
            this.PGEDesc.Padding = new System.Windows.Forms.Padding(3);
            this.PGEDesc.Size = new System.Drawing.Size(724, 344);
            this.PGEDesc.TabIndex = 0;
            this.PGEDesc.Text = "Description";
            this.PGEDesc.UseVisualStyleBackColor = true;
            // 
            // TXTSTKD_LongDesc
            // 
            this.TXTSTKD_LongDesc.Location = new System.Drawing.Point(21, 149);
            this.TXTSTKD_LongDesc.MaxLength = 4096;
            this.TXTSTKD_LongDesc.Multiline = true;
            this.TXTSTKD_LongDesc.Name = "TXTSTKD_LongDesc";
            this.TXTSTKD_LongDesc.Size = new System.Drawing.Size(678, 173);
            this.TXTSTKD_LongDesc.TabIndex = 3;
            // 
            // LBLSTKD_LongDesc
            // 
            this.LBLSTKD_LongDesc.AutoSize = true;
            this.LBLSTKD_LongDesc.Location = new System.Drawing.Point(18, 127);
            this.LBLSTKD_LongDesc.Name = "LBLSTKD_LongDesc";
            this.LBLSTKD_LongDesc.Size = new System.Drawing.Size(87, 13);
            this.LBLSTKD_LongDesc.TabIndex = 2;
            this.LBLSTKD_LongDesc.Text = "Long Description";
            // 
            // TXTSTKD_Desc
            // 
            this.TXTSTKD_Desc.Location = new System.Drawing.Point(21, 29);
            this.TXTSTKD_Desc.MaxLength = 523;
            this.TXTSTKD_Desc.Multiline = true;
            this.TXTSTKD_Desc.Name = "TXTSTKD_Desc";
            this.TXTSTKD_Desc.Size = new System.Drawing.Size(678, 84);
            this.TXTSTKD_Desc.TabIndex = 1;
            this.TXTSTKD_Desc.Tag = "1";
            // 
            // LBLSTKD_Desc
            // 
            this.LBLSTKD_Desc.AutoSize = true;
            this.LBLSTKD_Desc.ForeColor = System.Drawing.Color.Red;
            this.LBLSTKD_Desc.Location = new System.Drawing.Point(18, 12);
            this.LBLSTKD_Desc.Name = "LBLSTKD_Desc";
            this.LBLSTKD_Desc.Size = new System.Drawing.Size(60, 13);
            this.LBLSTKD_Desc.TabIndex = 0;
            this.LBLSTKD_Desc.Text = "Description";
            // 
            // PGEMedia
            // 
            this.PGEMedia.Controls.Add(this.WEBPreview);
            this.PGEMedia.Controls.Add(this.label8);
            this.PGEMedia.Controls.Add(this.BTNRemoveFile);
            this.PGEMedia.Controls.Add(this.BTNAddFile);
            this.PGEMedia.Controls.Add(this.LVMedia);
            this.PGEMedia.Controls.Add(this.label7);
            this.PGEMedia.Location = new System.Drawing.Point(4, 22);
            this.PGEMedia.Name = "PGEMedia";
            this.PGEMedia.Padding = new System.Windows.Forms.Padding(3);
            this.PGEMedia.Size = new System.Drawing.Size(724, 344);
            this.PGEMedia.TabIndex = 1;
            this.PGEMedia.Text = "Media";
            this.PGEMedia.UseVisualStyleBackColor = true;
            // 
            // WEBPreview
            // 
            this.WEBPreview.Location = new System.Drawing.Point(485, 49);
            this.WEBPreview.MinimumSize = new System.Drawing.Size(20, 20);
            this.WEBPreview.Name = "WEBPreview";
            this.WEBPreview.ScriptErrorsSuppressed = true;
            this.WEBPreview.Size = new System.Drawing.Size(221, 228);
            this.WEBPreview.TabIndex = 6;
            this.WEBPreview.WebBrowserShortcutsEnabled = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(475, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Image Preview";
            // 
            // BTNRemoveFile
            // 
            this.BTNRemoveFile.Location = new System.Drawing.Point(156, 298);
            this.BTNRemoveFile.Name = "BTNRemoveFile";
            this.BTNRemoveFile.Size = new System.Drawing.Size(81, 23);
            this.BTNRemoveFile.TabIndex = 4;
            this.BTNRemoveFile.Text = "Remove File";
            this.BTNRemoveFile.UseVisualStyleBackColor = true;
            this.BTNRemoveFile.Click += new System.EventHandler(this.BTNRemoveFile_Click);
            // 
            // BTNAddFile
            // 
            this.BTNAddFile.Location = new System.Drawing.Point(20, 298);
            this.BTNAddFile.Name = "BTNAddFile";
            this.BTNAddFile.Size = new System.Drawing.Size(75, 23);
            this.BTNAddFile.TabIndex = 2;
            this.BTNAddFile.Text = "Add File";
            this.BTNAddFile.UseVisualStyleBackColor = true;
            this.BTNAddFile.Click += new System.EventHandler(this.BTNAddFile_Click);
            // 
            // LVMedia
            // 
            this.LVMedia.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.LVMedia.HideSelection = false;
            this.LVMedia.Location = new System.Drawing.Point(23, 49);
            this.LVMedia.Name = "LVMedia";
            this.LVMedia.Size = new System.Drawing.Size(434, 228);
            this.LVMedia.TabIndex = 1;
            this.LVMedia.UseCompatibleStateImageBehavior = false;
            this.LVMedia.View = System.Windows.Forms.View.Details;
            this.LVMedia.DoubleClick += new System.EventHandler(this.LVMedia_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Path";
            this.columnHeader1.Width = 600;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "type";
            this.columnHeader2.Width = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Media Maintenance";
            // 
            // CMBSTKI_UOM
            // 
            this.CMBSTKI_UOM.FormattingEnabled = true;
            this.CMBSTKI_UOM.Location = new System.Drawing.Point(58, 68);
            this.CMBSTKI_UOM.MaxLength = 20;
            this.CMBSTKI_UOM.Name = "CMBSTKI_UOM";
            this.CMBSTKI_UOM.Size = new System.Drawing.Size(99, 21);
            this.CMBSTKI_UOM.TabIndex = 2;
            this.CMBSTKI_UOM.Tag = "1";
            this.CMBSTKI_UOM.Leave += new System.EventHandler(this.CMBSTKI_UOM_Leave);
            // 
            // LBLSTKU_Desc
            // 
            this.LBLSTKU_Desc.AutoSize = true;
            this.LBLSTKU_Desc.ForeColor = System.Drawing.Color.Red;
            this.LBLSTKU_Desc.Location = new System.Drawing.Point(8, 74);
            this.LBLSTKU_Desc.Name = "LBLSTKU_Desc";
            this.LBLSTKU_Desc.Size = new System.Drawing.Size(32, 13);
            this.LBLSTKU_Desc.TabIndex = 6;
            this.LBLSTKU_Desc.Text = "UOM";
            // 
            // CHKSTKI_LocLot
            // 
            this.CHKSTKI_LocLot.AutoSize = true;
            this.CHKSTKI_LocLot.Checked = true;
            this.CHKSTKI_LocLot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKSTKI_LocLot.Location = new System.Drawing.Point(558, 69);
            this.CHKSTKI_LocLot.Name = "CHKSTKI_LocLot";
            this.CHKSTKI_LocLot.Size = new System.Drawing.Size(113, 17);
            this.CHKSTKI_LocLot.TabIndex = 5;
            this.CHKSTKI_LocLot.Text = "Loc/Lot Tracked?";
            this.CHKSTKI_LocLot.UseVisualStyleBackColor = true;
            this.CHKSTKI_LocLot.CheckedChanged += new System.EventHandler(this.CHKSTKI_LocLot_CheckedChanged);
            // 
            // NUDSTKI_Price
            // 
            this.NUDSTKI_Price.Location = new System.Drawing.Point(462, 68);
            this.NUDSTKI_Price.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NUDSTKI_Price.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.NUDSTKI_Price.Name = "NUDSTKI_Price";
            this.NUDSTKI_Price.Size = new System.Drawing.Size(80, 20);
            this.NUDSTKI_Price.TabIndex = 4;
            this.NUDSTKI_Price.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // LBLSTKI_Price
            // 
            this.LBLSTKI_Price.AutoSize = true;
            this.LBLSTKI_Price.Location = new System.Drawing.Point(412, 72);
            this.LBLSTKI_Price.Name = "LBLSTKI_Price";
            this.LBLSTKI_Price.Size = new System.Drawing.Size(31, 13);
            this.LBLSTKI_Price.TabIndex = 10;
            this.LBLSTKI_Price.Text = "Price";
            // 
            // CMBSTKI_ProductFamily
            // 
            this.CMBSTKI_ProductFamily.FormattingEnabled = true;
            this.CMBSTKI_ProductFamily.Location = new System.Drawing.Point(266, 68);
            this.CMBSTKI_ProductFamily.MaxLength = 30;
            this.CMBSTKI_ProductFamily.Name = "CMBSTKI_ProductFamily";
            this.CMBSTKI_ProductFamily.Size = new System.Drawing.Size(121, 21);
            this.CMBSTKI_ProductFamily.TabIndex = 3;
            this.CMBSTKI_ProductFamily.Tag = "1";
            this.CMBSTKI_ProductFamily.Leave += new System.EventHandler(this.CMBSTKI_ProductFamily_Leave);
            // 
            // LBLSTKI_ProductFamily
            // 
            this.LBLSTKI_ProductFamily.AutoSize = true;
            this.LBLSTKI_ProductFamily.ForeColor = System.Drawing.Color.Red;
            this.LBLSTKI_ProductFamily.Location = new System.Drawing.Point(180, 74);
            this.LBLSTKI_ProductFamily.Name = "LBLSTKI_ProductFamily";
            this.LBLSTKI_ProductFamily.Size = new System.Drawing.Size(76, 13);
            this.LBLSTKI_ProductFamily.TabIndex = 8;
            this.LBLSTKI_ProductFamily.Text = "Product Family";
            // 
            // OFDSelectFile
            // 
            this.OFDSelectFile.DefaultExt = "*.jpg";
            this.OFDSelectFile.Filter = "JPEG Image|*.jpg|PNG Image|*.png|GIF Image|*.gif|PDF|*.pdf";
            this.OFDSelectFile.InitialDirectory = "C:\\";
            this.OFDSelectFile.RestoreDirectory = true;
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(183, 494);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 8;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(287, 494);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 9;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // TXTHidden
            // 
            this.TXTHidden.BackColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.Location = new System.Drawing.Point(558, 91);
            this.TXTHidden.Name = "TXTHidden";
            this.TXTHidden.Size = new System.Drawing.Size(100, 20);
            this.TXTHidden.TabIndex = 11;
            this.TXTHidden.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(376, 272);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(10, 13);
            this.textBox1.TabIndex = 44;
            this.textBox1.TabStop = false;
            // 
            // BTNFind
            // 
            this.BTNFind.Location = new System.Drawing.Point(424, 41);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(39, 20);
            this.BTNFind.TabIndex = 45;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(0, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(760, 32);
            this.PANTitle.TabIndex = 46;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(729, 4);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 51;
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
            // frmStockItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 525);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.TXTHidden);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.CHKSTKI_LocLot);
            this.Controls.Add(this.NUDSTKI_Price);
            this.Controls.Add(this.LBLSTKI_Price);
            this.Controls.Add(this.CMBSTKI_ProductFamily);
            this.Controls.Add(this.LBLSTKI_ProductFamily);
            this.Controls.Add(this.CMBSTKI_UOM);
            this.Controls.Add(this.LBLSTKU_Desc);
            this.Controls.Add(this.TABStockItem);
            this.Controls.Add(this.BTNNew);
            this.Controls.Add(this.CMBSTKI_ItemID);
            this.Controls.Add(this.LBLSTKI_ItemID);
            this.Controls.Add(this.BTNSave);
            this.Controls.Add(this.PANTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmStockItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Items Maintenance";
            this.Load += new System.EventHandler(this.frmStockItems_Load);
            this.Shown += new System.EventHandler(this.frmStockItems_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmStockItems_Paint);
            this.TABStockItem.ResumeLayout(false);
            this.PGEDesc.ResumeLayout(false);
            this.PGEDesc.PerformLayout();
            this.PGEMedia.ResumeLayout(false);
            this.PGEMedia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDSTKI_Price)).EndInit();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.Label LBLSTKI_ItemID;
        private System.Windows.Forms.ComboBox CMBSTKI_ItemID;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.TabControl TABStockItem;
        private System.Windows.Forms.TabPage PGEDesc;
        private System.Windows.Forms.TextBox TXTSTKD_LongDesc;
        private System.Windows.Forms.Label LBLSTKD_LongDesc;
        private System.Windows.Forms.TextBox TXTSTKD_Desc;
        private System.Windows.Forms.Label LBLSTKD_Desc;
        private System.Windows.Forms.TabPage PGEMedia;
        private System.Windows.Forms.Button BTNRemoveFile;
        private System.Windows.Forms.Button BTNAddFile;
        private System.Windows.Forms.ListView LVMedia;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox CMBSTKI_UOM;
        private System.Windows.Forms.Label LBLSTKU_Desc;
        private System.Windows.Forms.CheckBox CHKSTKI_LocLot;
        private System.Windows.Forms.NumericUpDown NUDSTKI_Price;
        private System.Windows.Forms.Label LBLSTKI_Price;
        protected System.Windows.Forms.ComboBox CMBSTKI_ProductFamily;
        private System.Windows.Forms.Label LBLSTKI_ProductFamily;
        private System.Windows.Forms.WebBrowser WEBPreview;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.OpenFileDialog OFDSelectFile;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.TextBox TXTHidden;
        private System.Windows.Forms.TextBox textBox1;
        private Button BTNFind;
        private Panel PANTitle;
        private Label LBLTitle;
        private Button BTNClose;
    }
}