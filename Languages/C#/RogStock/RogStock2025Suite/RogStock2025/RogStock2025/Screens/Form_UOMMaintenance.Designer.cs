using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
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
            this.CMBSTKU_Desc = new System.Windows.Forms.ComboBox();
            this.LBLSTKU_Desc = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CMBSTKU_Desc
            // 
            this.CMBSTKU_Desc.FormattingEnabled = true;
            this.CMBSTKU_Desc.Location = new System.Drawing.Point(51, 7);
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
            this.LBLSTKU_Desc.Location = new System.Drawing.Point(8, 10);
            this.LBLSTKU_Desc.Name = "LBLSTKU_Desc";
            this.LBLSTKU_Desc.Size = new System.Drawing.Size(35, 13);
            this.LBLSTKU_Desc.TabIndex = 33;
            this.LBLSTKU_Desc.Text = "UOM:";
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(261, 7);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(39, 20);
            this.BTNNew.TabIndex = 1;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(260, 82);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 4;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(156, 82);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 3;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(421, 82);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 5;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(17, 82);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 2;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // frmUOMMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 112);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.BTNSave);
            this.Controls.Add(this.CMBSTKU_Desc);
            this.Controls.Add(this.LBLSTKU_Desc);
            this.Controls.Add(this.BTNNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmUOMMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock UOM Maintenance";
            this.Load += new System.EventHandler(this.Form_UOMMaintenance_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmUOMMaintenance_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBSTKU_Desc;
        private System.Windows.Forms.Label LBLSTKU_Desc;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Button BTNSave;
    }
}