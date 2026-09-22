using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    partial class frmUseStock
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
            this.DGVLots = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.TXTHidden = new System.Windows.Forms.TextBox();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.CMBSTKI_ItemID = new System.Windows.Forms.ComboBox();
            this.LBLSTKI_ItemID = new System.Windows.Forms.Label();
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.CMBLOC_Location = new System.Windows.Forms.ComboBox();
            this.LBLLOC_Location = new System.Windows.Forms.Label();
            this.NUDLOC_Qty = new System.Windows.Forms.NumericUpDown();
            this.LBLLOC_Qty = new System.Windows.Forms.Label();
            this.LBLSTKD_Desc = new System.Windows.Forms.Label();
            this.BTNFind = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVLots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOC_Qty)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVLots
            // 
            this.DGVLots.AllowUserToAddRows = false;
            this.DGVLots.AllowUserToDeleteRows = false;
            this.DGVLots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVLots.EnableHeadersVisualStyles = false;
            this.DGVLots.Location = new System.Drawing.Point(12, 144);
            this.DGVLots.MultiSelect = false;
            this.DGVLots.Name = "DGVLots";
            this.DGVLots.Size = new System.Drawing.Size(722, 150);
            this.DGVLots.TabIndex = 3;
            this.DGVLots.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGVLots_CellFormatting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 64;
            this.label1.Text = "Lots:";
            // 
            // TXTHidden
            // 
            this.TXTHidden.BackColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTHidden.ForeColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.Location = new System.Drawing.Point(378, 277);
            this.TXTHidden.Name = "TXTHidden";
            this.TXTHidden.Size = new System.Drawing.Size(10, 13);
            this.TXTHidden.TabIndex = 62;
            this.TXTHidden.TabStop = false;
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(154, 324);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 5;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // CMBSTKI_ItemID
            // 
            this.CMBSTKI_ItemID.FormattingEnabled = true;
            this.CMBSTKI_ItemID.Location = new System.Drawing.Point(96, 13);
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
            this.LBLSTKI_ItemID.Location = new System.Drawing.Point(9, 16);
            this.LBLSTKI_ItemID.Name = "LBLSTKI_ItemID";
            this.LBLSTKI_ItemID.Size = new System.Drawing.Size(41, 13);
            this.LBLSTKI_ItemID.TabIndex = 57;
            this.LBLSTKI_ItemID.Text = "Item ID";
            // 
            // BTNClose
            // 
            this.BTNClose.Location = new System.Drawing.Point(647, 323);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 6;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(15, 324);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 4;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // CMBLOC_Location
            // 
            this.CMBLOC_Location.FormattingEnabled = true;
            this.CMBLOC_Location.Location = new System.Drawing.Point(96, 79);
            this.CMBLOC_Location.MaxLength = 30;
            this.CMBLOC_Location.Name = "CMBLOC_Location";
            this.CMBLOC_Location.Size = new System.Drawing.Size(200, 21);
            this.CMBLOC_Location.TabIndex = 1;
            this.CMBLOC_Location.Tag = "1";
            this.CMBLOC_Location.SelectedValueChanged += new System.EventHandler(this.CMBLOC_Location_SelectedValueChanged);
            this.CMBLOC_Location.Leave += new System.EventHandler(this.CMBLOC_Location_Leave);
            // 
            // LBLLOC_Location
            // 
            this.LBLLOC_Location.AutoSize = true;
            this.LBLLOC_Location.ForeColor = System.Drawing.Color.Red;
            this.LBLLOC_Location.Location = new System.Drawing.Point(9, 82);
            this.LBLLOC_Location.Name = "LBLLOC_Location";
            this.LBLLOC_Location.Size = new System.Drawing.Size(82, 13);
            this.LBLLOC_Location.TabIndex = 67;
            this.LBLLOC_Location.Text = "Location Name:";
            // 
            // NUDLOC_Qty
            // 
            this.NUDLOC_Qty.Location = new System.Drawing.Point(423, 80);
            this.NUDLOC_Qty.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NUDLOC_Qty.Name = "NUDLOC_Qty";
            this.NUDLOC_Qty.Size = new System.Drawing.Size(56, 20);
            this.NUDLOC_Qty.TabIndex = 2;
            this.NUDLOC_Qty.Tag = "1";
            // 
            // LBLLOC_Qty
            // 
            this.LBLLOC_Qty.AutoSize = true;
            this.LBLLOC_Qty.ForeColor = System.Drawing.Color.Red;
            this.LBLLOC_Qty.Location = new System.Drawing.Point(317, 81);
            this.LBLLOC_Qty.Name = "LBLLOC_Qty";
            this.LBLLOC_Qty.Size = new System.Drawing.Size(93, 13);
            this.LBLLOC_Qty.TabIndex = 69;
            this.LBLLOC_Qty.Text = "Location Quantity:";
            // 
            // LBLSTKD_Desc
            // 
            this.LBLSTKD_Desc.AutoSize = true;
            this.LBLSTKD_Desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLSTKD_Desc.Location = new System.Drawing.Point(11, 41);
            this.LBLSTKD_Desc.Name = "LBLSTKD_Desc";
            this.LBLSTKD_Desc.Size = new System.Drawing.Size(0, 17);
            this.LBLSTKD_Desc.TabIndex = 70;
            // 
            // BTNFind
            // 
            this.BTNFind.Location = new System.Drawing.Point(419, 13);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(39, 20);
            this.BTNFind.TabIndex = 71;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // frmUseStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 351);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.LBLSTKD_Desc);
            this.Controls.Add(this.NUDLOC_Qty);
            this.Controls.Add(this.LBLLOC_Qty);
            this.Controls.Add(this.CMBLOC_Location);
            this.Controls.Add(this.LBLLOC_Location);
            this.Controls.Add(this.DGVLots);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TXTHidden);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.CMBSTKI_ItemID);
            this.Controls.Add(this.LBLSTKI_ItemID);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmUseStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Use Stock";
            this.Load += new System.EventHandler(this.frmUseStock_Load);
            this.Shown += new System.EventHandler(this.frmUseStock_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmUseStock_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.DGVLots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOC_Qty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVLots;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TXTHidden;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.ComboBox CMBSTKI_ItemID;
        private System.Windows.Forms.Label LBLSTKI_ItemID;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.ComboBox CMBLOC_Location;
        private System.Windows.Forms.Label LBLLOC_Location;
        private System.Windows.Forms.NumericUpDown NUDLOC_Qty;
        private System.Windows.Forms.Label LBLLOC_Qty;
        private System.Windows.Forms.Label LBLSTKD_Desc;
        private Button BTNFind;
    }
}