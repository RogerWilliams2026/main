using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2026.Screens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLotMaintenance));
            this.CMBLOT_Nbr = new System.Windows.Forms.ComboBox();
            this.LBLLOT_Nbr = new System.Windows.Forms.Label();
            this.TXTHidden = new System.Windows.Forms.TextBox();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.CHKLOT_NonNet = new System.Windows.Forms.CheckBox();
            this.NUDLOT_Qty = new System.Windows.Forms.NumericUpDown();
            this.LBLLOT_Qty = new System.Windows.Forms.Label();
            this.CMBLOT_ItemID = new System.Windows.Forms.ComboBox();
            this.LBLLOT_ItemID = new System.Windows.Forms.Label();
            this.BTNSave = new System.Windows.Forms.Button();
            this.CMBLOT_Location = new System.Windows.Forms.ComboBox();
            this.LBLLOT_Location = new System.Windows.Forms.Label();
            this.LBLItemDesc = new System.Windows.Forms.Label();
            this.LBLLocationQty = new System.Windows.Forms.Label();
            this.LBLTotalLotQtys = new System.Windows.Forms.Label();
            this.LBLTimeDate = new System.Windows.Forms.Label();
            this.BTNFind = new System.Windows.Forms.Button();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOT_Qty)).BeginInit();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // CMBLOT_Nbr
            // 
            this.CMBLOT_Nbr.FormattingEnabled = true;
            this.CMBLOT_Nbr.Location = new System.Drawing.Point(78, 125);
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
            this.LBLLOT_Nbr.Location = new System.Drawing.Point(6, 129);
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
            this.TXTHidden.Location = new System.Drawing.Point(600, 224);
            this.TXTHidden.Name = "TXTHidden";
            this.TXTHidden.Size = new System.Drawing.Size(0, 13);
            this.TXTHidden.TabIndex = 43;
            this.TXTHidden.TabStop = false;
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(269, 243);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 7;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(165, 243);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 6;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // CHKLOT_NonNet
            // 
            this.CHKLOT_NonNet.AutoSize = true;
            this.CHKLOT_NonNet.Checked = true;
            this.CHKLOT_NonNet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKLOT_NonNet.Location = new System.Drawing.Point(159, 156);
            this.CHKLOT_NonNet.Name = "CHKLOT_NonNet";
            this.CHKLOT_NonNet.Size = new System.Drawing.Size(72, 17);
            this.CHKLOT_NonNet.TabIndex = 4;
            this.CHKLOT_NonNet.Text = "Non Net?";
            this.CHKLOT_NonNet.UseVisualStyleBackColor = true;
            // 
            // NUDLOT_Qty
            // 
            this.NUDLOT_Qty.Location = new System.Drawing.Point(78, 152);
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
            this.LBLLOT_Qty.Location = new System.Drawing.Point(8, 156);
            this.LBLLOT_Qty.Name = "LBLLOT_Qty";
            this.LBLLOT_Qty.Size = new System.Drawing.Size(49, 13);
            this.LBLLOT_Qty.TabIndex = 42;
            this.LBLLOT_Qty.Text = "Quantity:";
            // 
            // CMBLOT_ItemID
            // 
            this.CMBLOT_ItemID.FormattingEnabled = true;
            this.CMBLOT_ItemID.Location = new System.Drawing.Point(78, 36);
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
            this.LBLLOT_ItemID.Location = new System.Drawing.Point(8, 40);
            this.LBLLOT_ItemID.Name = "LBLLOT_ItemID";
            this.LBLLOT_ItemID.Size = new System.Drawing.Size(41, 13);
            this.LBLLOT_ItemID.TabIndex = 41;
            this.LBLLOT_ItemID.Text = "Item ID";
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(26, 243);
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
            this.CMBLOT_Location.Location = new System.Drawing.Point(543, 36);
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
            this.LBLLOT_Location.Location = new System.Drawing.Point(458, 40);
            this.LBLLOT_Location.Name = "LBLLOT_Location";
            this.LBLLOT_Location.Size = new System.Drawing.Size(82, 13);
            this.LBLLOT_Location.TabIndex = 47;
            this.LBLLOT_Location.Text = "Location Name:";
            // 
            // LBLItemDesc
            // 
            this.LBLItemDesc.AutoSize = true;
            this.LBLItemDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLItemDesc.Location = new System.Drawing.Point(6, 60);
            this.LBLItemDesc.Name = "LBLItemDesc";
            this.LBLItemDesc.Size = new System.Drawing.Size(0, 13);
            this.LBLItemDesc.TabIndex = 48;
            // 
            // LBLLocationQty
            // 
            this.LBLLocationQty.AutoSize = true;
            this.LBLLocationQty.Location = new System.Drawing.Point(11, 94);
            this.LBLLocationQty.Name = "LBLLocationQty";
            this.LBLLocationQty.Size = new System.Drawing.Size(0, 13);
            this.LBLLocationQty.TabIndex = 49;
            // 
            // LBLTotalLotQtys
            // 
            this.LBLTotalLotQtys.AutoSize = true;
            this.LBLTotalLotQtys.Location = new System.Drawing.Point(243, 91);
            this.LBLTotalLotQtys.Name = "LBLTotalLotQtys";
            this.LBLTotalLotQtys.Size = new System.Drawing.Size(0, 13);
            this.LBLTotalLotQtys.TabIndex = 50;
            // 
            // LBLTimeDate
            // 
            this.LBLTimeDate.AutoSize = true;
            this.LBLTimeDate.Location = new System.Drawing.Point(422, 91);
            this.LBLTimeDate.Name = "LBLTimeDate";
            this.LBLTimeDate.Size = new System.Drawing.Size(0, 13);
            this.LBLTimeDate.TabIndex = 51;
            // 
            // BTNFind
            // 
            this.BTNFind.Location = new System.Drawing.Point(396, 36);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(39, 20);
            this.BTNFind.TabIndex = 72;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(2, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(749, 34);
            this.PANTitle.TabIndex = 76;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(719, 4);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 78;
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
            // frmLotMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 272);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.LBLTimeDate);
            this.Controls.Add(this.LBLTotalLotQtys);
            this.Controls.Add(this.LBLLocationQty);
            this.Controls.Add(this.LBLItemDesc);
            this.Controls.Add(this.CMBLOT_Location);
            this.Controls.Add(this.LBLLOT_Location);
            this.Controls.Add(this.CMBLOT_Nbr);
            this.Controls.Add(this.LBLLOT_Nbr);
            this.Controls.Add(this.TXTHidden);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.CHKLOT_NonNet);
            this.Controls.Add(this.NUDLOT_Qty);
            this.Controls.Add(this.LBLLOT_Qty);
            this.Controls.Add(this.CMBLOT_ItemID);
            this.Controls.Add(this.LBLLOT_ItemID);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmLotMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Lot Maintenance";
            this.Load += new System.EventHandler(this.frmLotMaintenance_Load);
            this.Shown += new System.EventHandler(this.frmLotMaintenance_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmLotMaintenance_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOT_Qty)).EndInit();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBLOT_Nbr;
        private System.Windows.Forms.Label LBLLOT_Nbr;
        private System.Windows.Forms.TextBox TXTHidden;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.CheckBox CHKLOT_NonNet;
        private System.Windows.Forms.NumericUpDown NUDLOT_Qty;
        private System.Windows.Forms.Label LBLLOT_Qty;
        private System.Windows.Forms.ComboBox CMBLOT_ItemID;
        private System.Windows.Forms.Label LBLLOT_ItemID;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.ComboBox CMBLOT_Location;
        private System.Windows.Forms.Label LBLLOT_Location;
        private System.Windows.Forms.Label LBLItemDesc;
        private System.Windows.Forms.Label LBLLocationQty;
        private System.Windows.Forms.Label LBLTotalLotQtys;
        private System.Windows.Forms.Label LBLTimeDate;
        private Button BTNFind;
        private Panel PANTitle;
        private Label LBLTitle;
        private Button BTNClose;
    }
}