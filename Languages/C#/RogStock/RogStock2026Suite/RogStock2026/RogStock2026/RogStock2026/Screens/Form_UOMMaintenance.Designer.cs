using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2026.Screens
{
    partial class frmUOMMaintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUOMMaintenance));
            this.CMBSTKU_Desc = new System.Windows.Forms.ComboBox();
            this.LBLSTKU_Desc = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // CMBSTKU_Desc
            // 
            this.CMBSTKU_Desc.FormattingEnabled = true;
            this.CMBSTKU_Desc.Location = new System.Drawing.Point(55, 42);
            this.CMBSTKU_Desc.MaxLength = 20;
            this.CMBSTKU_Desc.Name = "CMBSTKU_Desc";
            this.CMBSTKU_Desc.Size = new System.Drawing.Size(200, 21);
            this.CMBSTKU_Desc.TabIndex = 0;
            this.CMBSTKU_Desc.Tag = "1";
            this.CMBSTKU_Desc.SelectedValueChanged += new System.EventHandler(this.CMBSTKU_Desc_SelectedValueChanged);
            this.CMBSTKU_Desc.Leave += new System.EventHandler(this.CMBSTKU_Desc_Leave);
            // 
            // LBLSTKU_Desc
            // 
            this.LBLSTKU_Desc.AutoSize = true;
            this.LBLSTKU_Desc.ForeColor = System.Drawing.Color.Red;
            this.LBLSTKU_Desc.Location = new System.Drawing.Point(12, 45);
            this.LBLSTKU_Desc.Name = "LBLSTKU_Desc";
            this.LBLSTKU_Desc.Size = new System.Drawing.Size(35, 13);
            this.LBLSTKU_Desc.TabIndex = 33;
            this.LBLSTKU_Desc.Text = "UOM:";
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(265, 42);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(39, 20);
            this.BTNNew.TabIndex = 1;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(260, 111);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 4;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(156, 111);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 3;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(17, 111);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 2;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(0, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(346, 34);
            this.PANTitle.TabIndex = 64;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(321, 3);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 66;
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
            // frmUOMMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 142);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.BTNSave);
            this.Controls.Add(this.CMBSTKU_Desc);
            this.Controls.Add(this.LBLSTKU_Desc);
            this.Controls.Add(this.BTNNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmUOMMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock UOM Maintenance";
            this.Load += new System.EventHandler(this.Form_UOMMaintenance_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmUOMMaintenance_Paint);
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBSTKU_Desc;
        private System.Windows.Forms.Label LBLSTKU_Desc;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.Button BTNSave;
        private Panel PANTitle;
        private Label LBLTitle;
        private Button BTNClose;
    }
}