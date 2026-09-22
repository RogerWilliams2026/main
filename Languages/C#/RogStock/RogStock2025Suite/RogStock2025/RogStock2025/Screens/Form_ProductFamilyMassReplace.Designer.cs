using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    partial class frmProductFamilyMassReplace
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
            this.CMBProdFamTo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CMBProdFamFrom = new System.Windows.Forms.ComboBox();
            this.LBLSTKP_ProductFamilyFrom = new System.Windows.Forms.Label();
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CMBProdFamTo
            // 
            this.CMBProdFamTo.FormattingEnabled = true;
            this.CMBProdFamTo.Location = new System.Drawing.Point(182, 49);
            this.CMBProdFamTo.MaxLength = 20;
            this.CMBProdFamTo.Name = "CMBProdFamTo";
            this.CMBProdFamTo.Size = new System.Drawing.Size(200, 21);
            this.CMBProdFamTo.TabIndex = 1;
            this.CMBProdFamTo.Tag = "1";
            this.CMBProdFamTo.SelectedValueChanged += new System.EventHandler(this.CMBProdFamTo_SelectedValueChanged);
            this.CMBProdFamTo.Leave += new System.EventHandler(this.CMBProdFamTo_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(34, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Product Family To:";
            // 
            // CMBProdFamFrom
            // 
            this.CMBProdFamFrom.FormattingEnabled = true;
            this.CMBProdFamFrom.Location = new System.Drawing.Point(182, 21);
            this.CMBProdFamFrom.MaxLength = 20;
            this.CMBProdFamFrom.Name = "CMBProdFamFrom";
            this.CMBProdFamFrom.Size = new System.Drawing.Size(200, 21);
            this.CMBProdFamFrom.TabIndex = 0;
            this.CMBProdFamFrom.Tag = "1";
            this.CMBProdFamFrom.SelectedValueChanged += new System.EventHandler(this.CMBProdFamFrom_SelectedValueChanged);
            this.CMBProdFamFrom.Leave += new System.EventHandler(this.CMBProdFamFrom_Leave);
            // 
            // LBLSTKP_ProductFamilyFrom
            // 
            this.LBLSTKP_ProductFamilyFrom.AutoSize = true;
            this.LBLSTKP_ProductFamilyFrom.ForeColor = System.Drawing.Color.Red;
            this.LBLSTKP_ProductFamilyFrom.Location = new System.Drawing.Point(34, 25);
            this.LBLSTKP_ProductFamilyFrom.Name = "LBLSTKP_ProductFamilyFrom";
            this.LBLSTKP_ProductFamilyFrom.Size = new System.Drawing.Size(105, 13);
            this.LBLSTKP_ProductFamilyFrom.TabIndex = 58;
            this.LBLSTKP_ProductFamilyFrom.Text = "Product Family From:";
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(345, 100);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 3;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(21, 101);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 2;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // frmProductFamilyMassReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 134);
            this.Controls.Add(this.CMBProdFamTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CMBProdFamFrom);
            this.Controls.Add(this.LBLSTKP_ProductFamilyFrom);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmProductFamilyMassReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Family Mass Replace";
            this.Load += new System.EventHandler(this.frmProductFamilyMassReplace_Load);
            this.Shown += new System.EventHandler(this.frmProductFamilyMassReplace_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmProductFamilyMassReplace_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBProdFamTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CMBProdFamFrom;
        private System.Windows.Forms.Label LBLSTKP_ProductFamilyFrom;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Button BTNSave;
    }
}