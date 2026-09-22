using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    partial class frmAdjustQuantity
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
            this.BTNUndo = new System.Windows.Forms.Button();
            this.CMBSTKI_ItemID = new System.Windows.Forms.ComboBox();
            this.LBLSTKI_ItemID = new System.Windows.Forms.Label();
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.LBLLOC_Name = new System.Windows.Forms.Label();
            this.TXTHidden = new System.Windows.Forms.TextBox();
            this.DGVLocations = new System.Windows.Forms.DataGridView();
            this.DGVLots = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.LBLSTKD_Desc = new System.Windows.Forms.Label();
            this.BTNFind = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLocations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLots)).BeginInit();
            this.SuspendLayout();
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(184, 476);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 4;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // CMBSTKI_ItemID
            // 
            this.CMBSTKI_ItemID.FormattingEnabled = true;
            this.CMBSTKI_ItemID.Location = new System.Drawing.Point(80, 12);
            this.CMBSTKI_ItemID.MaxLength = 50;
            this.CMBSTKI_ItemID.Name = "CMBSTKI_ItemID";
            this.CMBSTKI_ItemID.Size = new System.Drawing.Size(304, 21);
            this.CMBSTKI_ItemID.TabIndex = 0;
            this.CMBSTKI_ItemID.Tag = "1";
            this.CMBSTKI_ItemID.SelectedValueChanged += new System.EventHandler(this.CMBSTKI_ItemID_SelectedValueChanged);
            this.CMBSTKI_ItemID.Leave += new System.EventHandler(this.CMBSTKI_ItemID_Leave);
            // 
            // LBLSTKI_ItemID
            // 
            this.LBLSTKI_ItemID.AutoSize = true;
            this.LBLSTKI_ItemID.ForeColor = System.Drawing.Color.Red;
            this.LBLSTKI_ItemID.Location = new System.Drawing.Point(9, 15);
            this.LBLSTKI_ItemID.Name = "LBLSTKI_ItemID";
            this.LBLSTKI_ItemID.Size = new System.Drawing.Size(41, 13);
            this.LBLSTKI_ItemID.TabIndex = 15;
            this.LBLSTKI_ItemID.Text = "Item ID";
            // 
            // BTNClose
            // 
            this.BTNClose.Location = new System.Drawing.Point(649, 475);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 5;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(13, 476);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 3;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // LBLLOC_Name
            // 
            this.LBLLOC_Name.AutoSize = true;
            this.LBLLOC_Name.ForeColor = System.Drawing.Color.Red;
            this.LBLLOC_Name.Location = new System.Drawing.Point(8, 73);
            this.LBLLOC_Name.Name = "LBLLOC_Name";
            this.LBLLOC_Name.Size = new System.Drawing.Size(56, 13);
            this.LBLLOC_Name.TabIndex = 51;
            this.LBLLOC_Name.Text = "Locations:";
            // 
            // TXTHidden
            // 
            this.TXTHidden.BackColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTHidden.ForeColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.Location = new System.Drawing.Point(0, 0);
            this.TXTHidden.Name = "TXTHidden";
            this.TXTHidden.Size = new System.Drawing.Size(0, 13);
            this.TXTHidden.TabIndex = 52;
            this.TXTHidden.TabStop = false;
            // 
            // DGVLocations
            // 
            this.DGVLocations.AllowUserToAddRows = false;
            this.DGVLocations.AllowUserToDeleteRows = false;
            this.DGVLocations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVLocations.EnableHeadersVisualStyles = false;
            this.DGVLocations.Location = new System.Drawing.Point(12, 99);
            this.DGVLocations.MultiSelect = false;
            this.DGVLocations.Name = "DGVLocations";
            this.DGVLocations.Size = new System.Drawing.Size(722, 150);
            this.DGVLocations.TabIndex = 1;
            this.DGVLocations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVLocations_CellDoubleClick);
            this.DGVLocations.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGVLocations_CellFormatting);
            this.DGVLocations.Click += new System.EventHandler(this.DGVLocations_Click);
            this.DGVLocations.DoubleClick += new System.EventHandler(this.DGVLocations_DoubleClick);
            // 
            // DGVLots
            // 
            this.DGVLots.AllowUserToAddRows = false;
            this.DGVLots.AllowUserToDeleteRows = false;
            this.DGVLots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVLots.EnableHeadersVisualStyles = false;
            this.DGVLots.Location = new System.Drawing.Point(11, 301);
            this.DGVLots.MultiSelect = false;
            this.DGVLots.Name = "DGVLots";
            this.DGVLots.Size = new System.Drawing.Size(722, 150);
            this.DGVLots.TabIndex = 2;
            this.DGVLots.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGVLots_CellFormatting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(7, 275);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "Lots:";
            // 
            // LBLSTKD_Desc
            // 
            this.LBLSTKD_Desc.AutoSize = true;
            this.LBLSTKD_Desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLSTKD_Desc.Location = new System.Drawing.Point(11, 41);
            this.LBLSTKD_Desc.Name = "LBLSTKD_Desc";
            this.LBLSTKD_Desc.Size = new System.Drawing.Size(0, 17);
            this.LBLSTKD_Desc.TabIndex = 56;
            // 
            // BTNFind
            // 
            this.BTNFind.Location = new System.Drawing.Point(395, 11);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(39, 20);
            this.BTNFind.TabIndex = 74;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // frmAdjustQuantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 506);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.LBLSTKD_Desc);
            this.Controls.Add(this.DGVLots);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGVLocations);
            this.Controls.Add(this.TXTHidden);
            this.Controls.Add(this.LBLLOC_Name);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.CMBSTKI_ItemID);
            this.Controls.Add(this.LBLSTKI_ItemID);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmAdjustQuantity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Adjust Quantity";
            this.Load += new System.EventHandler(this.frmAdjustQuantity_Load);
            this.Shown += new System.EventHandler(this.frmAdjustQuantity_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmAdjustQuantity_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.DGVLocations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.ComboBox CMBSTKI_ItemID;
        private System.Windows.Forms.Label LBLSTKI_ItemID;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.Label LBLLOC_Name;
        private System.Windows.Forms.TextBox TXTHidden;
        private System.Windows.Forms.DataGridView DGVLocations;
        private System.Windows.Forms.DataGridView DGVLots;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LBLSTKD_Desc;
        private Button BTNFind;
    }
}