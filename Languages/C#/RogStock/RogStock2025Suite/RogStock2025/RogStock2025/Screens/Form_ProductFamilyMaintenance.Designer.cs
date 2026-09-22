using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    partial class frmProductFamilyMaintenance
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
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.CMBSTKP_ProductFamily = new System.Windows.Forms.ComboBox();
            this.LBLSTKP_ProductFamily = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.TXTSTKP_Desc = new System.Windows.Forms.TextBox();
            this.LBLLOC_Desc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(255, 95);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 5;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(151, 95);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 4;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(416, 95);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 6;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(12, 95);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 3;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // CMBSTKP_ProductFamily
            // 
            this.CMBSTKP_ProductFamily.FormattingEnabled = true;
            this.CMBSTKP_ProductFamily.Location = new System.Drawing.Point(103, 12);
            this.CMBSTKP_ProductFamily.MaxLength = 30;
            this.CMBSTKP_ProductFamily.Name = "CMBSTKP_ProductFamily";
            this.CMBSTKP_ProductFamily.Size = new System.Drawing.Size(200, 21);
            this.CMBSTKP_ProductFamily.TabIndex = 0;
            this.CMBSTKP_ProductFamily.Tag = "1";
            this.CMBSTKP_ProductFamily.SelectedValueChanged += new System.EventHandler(this.CMBSTKP_ProductFamily_SelectedValueChanged);
            this.CMBSTKP_ProductFamily.Leave += new System.EventHandler(this.CMBSTKP_ProductFamily_Leave);
            // 
            // LBLSTKP_ProductFamily
            // 
            this.LBLSTKP_ProductFamily.AutoSize = true;
            this.LBLSTKP_ProductFamily.ForeColor = System.Drawing.Color.Red;
            this.LBLSTKP_ProductFamily.Location = new System.Drawing.Point(12, 15);
            this.LBLSTKP_ProductFamily.Name = "LBLSTKP_ProductFamily";
            this.LBLSTKP_ProductFamily.Size = new System.Drawing.Size(79, 13);
            this.LBLSTKP_ProductFamily.TabIndex = 40;
            this.LBLSTKP_ProductFamily.Text = "Product Family:";
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(313, 13);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(39, 20);
            this.BTNNew.TabIndex = 1;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // TXTSTKP_Desc
            // 
            this.TXTSTKP_Desc.Location = new System.Drawing.Point(103, 39);
            this.TXTSTKP_Desc.MaxLength = 30;
            this.TXTSTKP_Desc.Multiline = true;
            this.TXTSTKP_Desc.Name = "TXTSTKP_Desc";
            this.TXTSTKP_Desc.Size = new System.Drawing.Size(369, 22);
            this.TXTSTKP_Desc.TabIndex = 2;
            this.TXTSTKP_Desc.Tag = "0";
            // 
            // LBLLOC_Desc
            // 
            this.LBLLOC_Desc.AutoSize = true;
            this.LBLLOC_Desc.ForeColor = System.Drawing.Color.Black;
            this.LBLLOC_Desc.Location = new System.Drawing.Point(13, 43);
            this.LBLLOC_Desc.Name = "LBLLOC_Desc";
            this.LBLLOC_Desc.Size = new System.Drawing.Size(60, 13);
            this.LBLLOC_Desc.TabIndex = 46;
            this.LBLLOC_Desc.Text = "Description";
            // 
            // frmProductFamilyMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 128);
            this.Controls.Add(this.TXTSTKP_Desc);
            this.Controls.Add(this.LBLLOC_Desc);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.BTNSave);
            this.Controls.Add(this.CMBSTKP_ProductFamily);
            this.Controls.Add(this.LBLSTKP_ProductFamily);
            this.Controls.Add(this.BTNNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmProductFamilyMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stock Product Family Maintenance";
            this.Load += new System.EventHandler(this.frmProductFamilyMaintenance_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmProductFamilyMaintenance_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.ComboBox CMBSTKP_ProductFamily;
        private System.Windows.Forms.Label LBLSTKP_ProductFamily;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.TextBox TXTSTKP_Desc;
        private System.Windows.Forms.Label LBLLOC_Desc;
    }
}