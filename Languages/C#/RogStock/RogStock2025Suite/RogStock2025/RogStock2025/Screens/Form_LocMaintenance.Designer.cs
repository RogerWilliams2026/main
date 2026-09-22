using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    partial class frmLocMaintenance
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
            this.TXTHidden = new System.Windows.Forms.TextBox();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.CHK_LOC_NonNet = new System.Windows.Forms.CheckBox();
            this.NUDLOC_Qty = new System.Windows.Forms.NumericUpDown();
            this.LBLLOC_Qty = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.CMBLOC_ItemID = new System.Windows.Forms.ComboBox();
            this.LBLLOC_ItemID = new System.Windows.Forms.Label();
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.TXTLOC_Desc = new System.Windows.Forms.TextBox();
            this.LBLLOC_Desc = new System.Windows.Forms.Label();
            this.LBLLOC_Location = new System.Windows.Forms.Label();
            this.CMBLOC_Location = new System.Windows.Forms.ComboBox();
            this.LBLSTKD_Desc = new System.Windows.Forms.Label();
            this.BTNFind = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOC_Qty)).BeginInit();
            this.SuspendLayout();
            // 
            // TXTHidden
            // 
            this.TXTHidden.BackColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTHidden.ForeColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.Location = new System.Drawing.Point(507, 198);
            this.TXTHidden.Name = "TXTHidden";
            this.TXTHidden.Size = new System.Drawing.Size(0, 13);
            this.TXTHidden.TabIndex = 27;
            this.TXTHidden.TabStop = false;
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(254, 233);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 8;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(150, 233);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 7;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // CHK_LOC_NonNet
            // 
            this.CHK_LOC_NonNet.AutoSize = true;
            this.CHK_LOC_NonNet.Checked = true;
            this.CHK_LOC_NonNet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_LOC_NonNet.Location = new System.Drawing.Point(153, 194);
            this.CHK_LOC_NonNet.Name = "CHK_LOC_NonNet";
            this.CHK_LOC_NonNet.Size = new System.Drawing.Size(72, 17);
            this.CHK_LOC_NonNet.TabIndex = 5;
            this.CHK_LOC_NonNet.Text = "Non Net?";
            this.CHK_LOC_NonNet.UseVisualStyleBackColor = true;
            // 
            // NUDLOC_Qty
            // 
            this.NUDLOC_Qty.Location = new System.Drawing.Point(73, 193);
            this.NUDLOC_Qty.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NUDLOC_Qty.Name = "NUDLOC_Qty";
            this.NUDLOC_Qty.Size = new System.Drawing.Size(56, 20);
            this.NUDLOC_Qty.TabIndex = 4;
            this.NUDLOC_Qty.Tag = "1";
            // 
            // LBLLOC_Qty
            // 
            this.LBLLOC_Qty.AutoSize = true;
            this.LBLLOC_Qty.ForeColor = System.Drawing.Color.Red;
            this.LBLLOC_Qty.Location = new System.Drawing.Point(11, 193);
            this.LBLLOC_Qty.Name = "LBLLOC_Qty";
            this.LBLLOC_Qty.Size = new System.Drawing.Size(49, 13);
            this.LBLLOC_Qty.TabIndex = 25;
            this.LBLLOC_Qty.Text = "Quantity:";
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(715, 14);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(39, 20);
            this.BTNNew.TabIndex = 2;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // CMBLOC_ItemID
            // 
            this.CMBLOC_ItemID.FormattingEnabled = true;
            this.CMBLOC_ItemID.Location = new System.Drawing.Point(59, 12);
            this.CMBLOC_ItemID.MaxLength = 50;
            this.CMBLOC_ItemID.Name = "CMBLOC_ItemID";
            this.CMBLOC_ItemID.Size = new System.Drawing.Size(304, 21);
            this.CMBLOC_ItemID.TabIndex = 0;
            this.CMBLOC_ItemID.Tag = "1";
            this.CMBLOC_ItemID.SelectedValueChanged += new System.EventHandler(this.CMBLOC_ItemID_SelectedValueChanged);
            this.CMBLOC_ItemID.Leave += new System.EventHandler(this.CMBLOC_ItemID_Leave);
            // 
            // LBLLOC_ItemID
            // 
            this.LBLLOC_ItemID.AutoSize = true;
            this.LBLLOC_ItemID.ForeColor = System.Drawing.Color.Red;
            this.LBLLOC_ItemID.Location = new System.Drawing.Point(9, 15);
            this.LBLLOC_ItemID.Name = "LBLLOC_ItemID";
            this.LBLLOC_ItemID.Size = new System.Drawing.Size(44, 13);
            this.LBLLOC_ItemID.TabIndex = 15;
            this.LBLLOC_ItemID.Text = "Item ID:";
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(674, 232);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 9;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(11, 233);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 6;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // TXTLOC_Desc
            // 
            this.TXTLOC_Desc.Location = new System.Drawing.Point(11, 79);
            this.TXTLOC_Desc.MaxLength = 523;
            this.TXTLOC_Desc.Multiline = true;
            this.TXTLOC_Desc.Name = "TXTLOC_Desc";
            this.TXTLOC_Desc.Size = new System.Drawing.Size(743, 84);
            this.TXTLOC_Desc.TabIndex = 3;
            this.TXTLOC_Desc.Tag = "0";
            // 
            // LBLLOC_Desc
            // 
            this.LBLLOC_Desc.AutoSize = true;
            this.LBLLOC_Desc.ForeColor = System.Drawing.Color.Black;
            this.LBLLOC_Desc.Location = new System.Drawing.Point(9, 63);
            this.LBLLOC_Desc.Name = "LBLLOC_Desc";
            this.LBLLOC_Desc.Size = new System.Drawing.Size(60, 13);
            this.LBLLOC_Desc.TabIndex = 28;
            this.LBLLOC_Desc.Text = "Description";
            // 
            // LBLLOC_Location
            // 
            this.LBLLOC_Location.AutoSize = true;
            this.LBLLOC_Location.ForeColor = System.Drawing.Color.Red;
            this.LBLLOC_Location.Location = new System.Drawing.Point(423, 17);
            this.LBLLOC_Location.Name = "LBLLOC_Location";
            this.LBLLOC_Location.Size = new System.Drawing.Size(82, 13);
            this.LBLLOC_Location.TabIndex = 30;
            this.LBLLOC_Location.Text = "Location Name:";
            // 
            // CMBLOC_Location
            // 
            this.CMBLOC_Location.FormattingEnabled = true;
            this.CMBLOC_Location.Location = new System.Drawing.Point(507, 13);
            this.CMBLOC_Location.MaxLength = 30;
            this.CMBLOC_Location.Name = "CMBLOC_Location";
            this.CMBLOC_Location.Size = new System.Drawing.Size(200, 21);
            this.CMBLOC_Location.Sorted = true;
            this.CMBLOC_Location.TabIndex = 1;
            this.CMBLOC_Location.Tag = "1";
            this.CMBLOC_Location.SelectedValueChanged += new System.EventHandler(this.CMBLOC_Location_SelectedValueChanged);
            this.CMBLOC_Location.Leave += new System.EventHandler(this.CMBLOC_Location_Leave);
            // 
            // LBLSTKD_Desc
            // 
            this.LBLSTKD_Desc.AutoSize = true;
            this.LBLSTKD_Desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLSTKD_Desc.Location = new System.Drawing.Point(11, 40);
            this.LBLSTKD_Desc.Name = "LBLSTKD_Desc";
            this.LBLSTKD_Desc.Size = new System.Drawing.Size(0, 17);
            this.LBLSTKD_Desc.TabIndex = 31;
            // 
            // BTNFind
            // 
            this.BTNFind.Location = new System.Drawing.Point(373, 12);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(39, 20);
            this.BTNFind.TabIndex = 73;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // frmLocMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 270);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.LBLSTKD_Desc);
            this.Controls.Add(this.CMBLOC_Location);
            this.Controls.Add(this.LBLLOC_Location);
            this.Controls.Add(this.TXTLOC_Desc);
            this.Controls.Add(this.LBLLOC_Desc);
            this.Controls.Add(this.TXTHidden);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.CHK_LOC_NonNet);
            this.Controls.Add(this.NUDLOC_Qty);
            this.Controls.Add(this.LBLLOC_Qty);
            this.Controls.Add(this.BTNNew);
            this.Controls.Add(this.CMBLOC_ItemID);
            this.Controls.Add(this.LBLLOC_ItemID);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmLocMaintenance";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Location Maintenance";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_LocMaintenance_Load);
            this.Shown += new System.EventHandler(this.frmLocMaintenance_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_LocMaintenance_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOC_Qty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXTHidden;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.CheckBox CHK_LOC_NonNet;
        private System.Windows.Forms.NumericUpDown NUDLOC_Qty;
        private System.Windows.Forms.Label LBLLOC_Qty;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.ComboBox CMBLOC_ItemID;
        private System.Windows.Forms.Label LBLLOC_ItemID;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.TextBox TXTLOC_Desc;
        private System.Windows.Forms.Label LBLLOC_Desc;
        private System.Windows.Forms.Label LBLLOC_Location;
        private System.Windows.Forms.ComboBox CMBLOC_Location;
        private System.Windows.Forms.Label LBLSTKD_Desc;
        private Button BTNFind;
    }
}