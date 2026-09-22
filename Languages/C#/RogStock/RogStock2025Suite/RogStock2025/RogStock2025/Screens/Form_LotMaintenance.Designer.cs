using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    partial class frmLotMaintenance
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
            this.CMBLOT_Nbr = new System.Windows.Forms.ComboBox();
            this.LBLLOT_Nbr = new System.Windows.Forms.Label();
            this.TXTHidden = new System.Windows.Forms.TextBox();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.CHK_LOT_NonNet = new System.Windows.Forms.CheckBox();
            this.NUDLOT_Qty = new System.Windows.Forms.NumericUpDown();
            this.LBLLOT_Qty = new System.Windows.Forms.Label();
            this.CMBLOT_ItemID = new System.Windows.Forms.ComboBox();
            this.LBLLOT_ItemID = new System.Windows.Forms.Label();
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.CMBLOT_Location = new System.Windows.Forms.ComboBox();
            this.LBLLOT_Location = new System.Windows.Forms.Label();
            this.LBLSTKD_Desc = new System.Windows.Forms.Label();
            this.LBLLocationQty = new System.Windows.Forms.Label();
            this.LBLTotalLotQtys = new System.Windows.Forms.Label();
            this.LBLTimeDate = new System.Windows.Forms.Label();
            this.BTNFind = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOT_Qty)).BeginInit();
            this.SuspendLayout();
            // 
            // CMBLOT_Nbr
            // 
            this.CMBLOT_Nbr.FormattingEnabled = true;
            this.CMBLOT_Nbr.Location = new System.Drawing.Point(78, 101);
            this.CMBLOT_Nbr.Name = "CMBLOT_Nbr";
            this.CMBLOT_Nbr.Size = new System.Drawing.Size(200, 21);
            this.CMBLOT_Nbr.TabIndex = 2;
            this.CMBLOT_Nbr.Tag = "1";
            this.CMBLOT_Nbr.SelectedValueChanged += new System.EventHandler(this.CMBLOT_Nbr_SelectedValueChanged);
            this.CMBLOT_Nbr.Leave += new System.EventHandler(this.CMBLOT_Nbr_Leave);
            // 
            // LBLLOT_Nbr
            // 
            this.LBLLOT_Nbr.AutoSize = true;
            this.LBLLOT_Nbr.ForeColor = System.Drawing.Color.Red;
            this.LBLLOT_Nbr.Location = new System.Drawing.Point(6, 105);
            this.LBLLOT_Nbr.Name = "LBLLOT_Nbr";
            this.LBLLOT_Nbr.Size = new System.Drawing.Size(65, 13);
            this.LBLLOT_Nbr.TabIndex = 45;
            this.LBLLOT_Nbr.Text = "Lot Number:";
            // 
            // TXTHidden
            // 
            this.TXTHidden.BackColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTHidden.ForeColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.Location = new System.Drawing.Point(600, 200);
            this.TXTHidden.Name = "TXTHidden";
            this.TXTHidden.Size = new System.Drawing.Size(0, 13);
            this.TXTHidden.TabIndex = 43;
            this.TXTHidden.TabStop = false;
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(269, 219);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 7;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(165, 219);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 6;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // CHK_LOT_NonNet
            // 
            this.CHK_LOT_NonNet.AutoSize = true;
            this.CHK_LOT_NonNet.Checked = true;
            this.CHK_LOT_NonNet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_LOT_NonNet.Location = new System.Drawing.Point(159, 132);
            this.CHK_LOT_NonNet.Name = "CHK_LOT_NonNet";
            this.CHK_LOT_NonNet.Size = new System.Drawing.Size(72, 17);
            this.CHK_LOT_NonNet.TabIndex = 4;
            this.CHK_LOT_NonNet.Text = "Non Net?";
            this.CHK_LOT_NonNet.UseVisualStyleBackColor = true;
            // 
            // NUDLOT_Qty
            // 
            this.NUDLOT_Qty.Location = new System.Drawing.Point(78, 128);
            this.NUDLOT_Qty.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NUDLOT_Qty.Name = "NUDLOT_Qty";
            this.NUDLOT_Qty.Size = new System.Drawing.Size(56, 20);
            this.NUDLOT_Qty.TabIndex = 3;
            this.NUDLOT_Qty.Tag = "1";
            // 
            // LBLLOT_Qty
            // 
            this.LBLLOT_Qty.AutoSize = true;
            this.LBLLOT_Qty.ForeColor = System.Drawing.Color.Red;
            this.LBLLOT_Qty.Location = new System.Drawing.Point(8, 132);
            this.LBLLOT_Qty.Name = "LBLLOT_Qty";
            this.LBLLOT_Qty.Size = new System.Drawing.Size(49, 13);
            this.LBLLOT_Qty.TabIndex = 42;
            this.LBLLOT_Qty.Text = "Quantity:";
            // 
            // CMBLOT_ItemID
            // 
            this.CMBLOT_ItemID.FormattingEnabled = true;
            this.CMBLOT_ItemID.Location = new System.Drawing.Point(78, 12);
            this.CMBLOT_ItemID.MaxLength = 50;
            this.CMBLOT_ItemID.Name = "CMBLOT_ItemID";
            this.CMBLOT_ItemID.Size = new System.Drawing.Size(304, 21);
            this.CMBLOT_ItemID.TabIndex = 0;
            this.CMBLOT_ItemID.Tag = "1";
            this.CMBLOT_ItemID.SelectedValueChanged += new System.EventHandler(this.CMBLOT_ItemID_SelectedValueChanged);
            this.CMBLOT_ItemID.Leave += new System.EventHandler(this.CMBLOT_ItemID_Leave);
            // 
            // LBLLOT_ItemID
            // 
            this.LBLLOT_ItemID.AutoSize = true;
            this.LBLLOT_ItemID.ForeColor = System.Drawing.Color.Red;
            this.LBLLOT_ItemID.Location = new System.Drawing.Point(8, 16);
            this.LBLLOT_ItemID.Name = "LBLLOT_ItemID";
            this.LBLLOT_ItemID.Size = new System.Drawing.Size(41, 13);
            this.LBLLOT_ItemID.TabIndex = 41;
            this.LBLLOT_ItemID.Text = "Item ID";
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(669, 219);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 8;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(26, 219);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 5;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // CMBLOT_Location
            // 
            this.CMBLOT_Location.FormattingEnabled = true;
            this.CMBLOT_Location.Location = new System.Drawing.Point(545, 12);
            this.CMBLOT_Location.MaxLength = 30;
            this.CMBLOT_Location.Name = "CMBLOT_Location";
            this.CMBLOT_Location.Size = new System.Drawing.Size(200, 21);
            this.CMBLOT_Location.TabIndex = 1;
            this.CMBLOT_Location.Tag = "1";
            this.CMBLOT_Location.SelectedValueChanged += new System.EventHandler(this.CMBLOT_Location_SelectedValueChanged);
            this.CMBLOT_Location.Leave += new System.EventHandler(this.CMBLOT_Location_Leave);
            // 
            // LBLLOT_Location
            // 
            this.LBLLOT_Location.AutoSize = true;
            this.LBLLOT_Location.ForeColor = System.Drawing.Color.Red;
            this.LBLLOT_Location.Location = new System.Drawing.Point(460, 16);
            this.LBLLOT_Location.Name = "LBLLOT_Location";
            this.LBLLOT_Location.Size = new System.Drawing.Size(82, 13);
            this.LBLLOT_Location.TabIndex = 47;
            this.LBLLOT_Location.Text = "Location Name:";
            // 
            // LBLSTKD_Desc
            // 
            this.LBLSTKD_Desc.AutoSize = true;
            this.LBLSTKD_Desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLSTKD_Desc.Location = new System.Drawing.Point(10, 37);
            this.LBLSTKD_Desc.Name = "LBLSTKD_Desc";
            this.LBLSTKD_Desc.Size = new System.Drawing.Size(0, 17);
            this.LBLSTKD_Desc.TabIndex = 48;
            // 
            // LBLLocationQty
            // 
            this.LBLLocationQty.AutoSize = true;
            this.LBLLocationQty.Location = new System.Drawing.Point(11, 67);
            this.LBLLocationQty.Name = "LBLLocationQty";
            this.LBLLocationQty.Size = new System.Drawing.Size(0, 13);
            this.LBLLocationQty.TabIndex = 49;
            // 
            // LBLTotalLotQtys
            // 
            this.LBLTotalLotQtys.AutoSize = true;
            this.LBLTotalLotQtys.Location = new System.Drawing.Point(243, 67);
            this.LBLTotalLotQtys.Name = "LBLTotalLotQtys";
            this.LBLTotalLotQtys.Size = new System.Drawing.Size(0, 13);
            this.LBLTotalLotQtys.TabIndex = 50;
            // 
            // LBLTimeDate
            // 
            this.LBLTimeDate.AutoSize = true;
            this.LBLTimeDate.Location = new System.Drawing.Point(422, 67);
            this.LBLTimeDate.Name = "LBLTimeDate";
            this.LBLTimeDate.Size = new System.Drawing.Size(0, 13);
            this.LBLTimeDate.TabIndex = 51;
            // 
            // BTNFind
            // 
            this.BTNFind.Location = new System.Drawing.Point(396, 12);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(39, 20);
            this.BTNFind.TabIndex = 72;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // frmLotMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 250);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.LBLTimeDate);
            this.Controls.Add(this.LBLTotalLotQtys);
            this.Controls.Add(this.LBLLocationQty);
            this.Controls.Add(this.LBLSTKD_Desc);
            this.Controls.Add(this.CMBLOT_Location);
            this.Controls.Add(this.LBLLOT_Location);
            this.Controls.Add(this.CMBLOT_Nbr);
            this.Controls.Add(this.LBLLOT_Nbr);
            this.Controls.Add(this.TXTHidden);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.CHK_LOT_NonNet);
            this.Controls.Add(this.NUDLOT_Qty);
            this.Controls.Add(this.LBLLOT_Qty);
            this.Controls.Add(this.CMBLOT_ItemID);
            this.Controls.Add(this.LBLLOT_ItemID);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmLotMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Lot Maintenance";
            this.Load += new System.EventHandler(this.frmLotMaintenance_Load);
            this.Shown += new System.EventHandler(this.frmLotMaintenance_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmLotMaintenance_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOT_Qty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBLOT_Nbr;
        private System.Windows.Forms.Label LBLLOT_Nbr;
        private System.Windows.Forms.TextBox TXTHidden;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.CheckBox CHK_LOT_NonNet;
        private System.Windows.Forms.NumericUpDown NUDLOT_Qty;
        private System.Windows.Forms.Label LBLLOT_Qty;
        private System.Windows.Forms.ComboBox CMBLOT_ItemID;
        private System.Windows.Forms.Label LBLLOT_ItemID;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.ComboBox CMBLOT_Location;
        private System.Windows.Forms.Label LBLLOT_Location;
        private System.Windows.Forms.Label LBLSTKD_Desc;
        private System.Windows.Forms.Label LBLLocationQty;
        private System.Windows.Forms.Label LBLTotalLotQtys;
        private System.Windows.Forms.Label LBLTimeDate;
        private Button BTNFind;
    }
}