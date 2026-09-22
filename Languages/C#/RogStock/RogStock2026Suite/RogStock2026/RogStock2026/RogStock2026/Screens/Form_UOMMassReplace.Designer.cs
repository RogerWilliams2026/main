using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2026.Screens
{
    partial class frmUOMMassReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUOMMassReplace));
            this.label1 = new System.Windows.Forms.Label();
            this.CMBUOM_DescFrom = new System.Windows.Forms.ComboBox();
            this.LBLUOM_Desc = new System.Windows.Forms.Label();
            this.BTNSave = new System.Windows.Forms.Button();
            this.CMBUOM_DescTo = new System.Windows.Forms.ComboBox();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(15, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "UOM To:";
            // 
            // CMBUOM_DescFrom
            // 
            this.CMBUOM_DescFrom.FormattingEnabled = true;
            this.CMBUOM_DescFrom.Location = new System.Drawing.Point(100, 42);
            this.CMBUOM_DescFrom.MaxLength = 20;
            this.CMBUOM_DescFrom.Name = "CMBUOM_DescFrom";
            this.CMBUOM_DescFrom.Size = new System.Drawing.Size(200, 21);
            this.CMBUOM_DescFrom.TabIndex = 0;
            this.CMBUOM_DescFrom.Tag = "1";
            this.CMBUOM_DescFrom.SelectedValueChanged += new System.EventHandler(this.CMBUOM_DescFrom_SelectedValueChanged);
            this.CMBUOM_DescFrom.Leave += new System.EventHandler(this.CMBUOM_DescFrom_Leave);
            // 
            // LBLUOM_Desc
            // 
            this.LBLUOM_Desc.AutoSize = true;
            this.LBLUOM_Desc.ForeColor = System.Drawing.Color.Red;
            this.LBLUOM_Desc.Location = new System.Drawing.Point(15, 46);
            this.LBLUOM_Desc.Name = "LBLUOM_Desc";
            this.LBLUOM_Desc.Size = new System.Drawing.Size(61, 13);
            this.LBLUOM_Desc.TabIndex = 52;
            this.LBLUOM_Desc.Text = "UOM From:";
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(18, 116);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 2;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // CMBUOM_DescTo
            // 
            this.CMBUOM_DescTo.FormattingEnabled = true;
            this.CMBUOM_DescTo.Location = new System.Drawing.Point(100, 70);
            this.CMBUOM_DescTo.MaxLength = 20;
            this.CMBUOM_DescTo.Name = "CMBUOM_DescTo";
            this.CMBUOM_DescTo.Size = new System.Drawing.Size(200, 21);
            this.CMBUOM_DescTo.TabIndex = 1;
            this.CMBUOM_DescTo.Tag = "1";
            this.CMBUOM_DescTo.SelectedValueChanged += new System.EventHandler(this.CMBUOM_DescTo_SelectedValueChanged);
            this.CMBUOM_DescTo.Leave += new System.EventHandler(this.CMBUOM_DescTo_Leave);
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(0, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(312, 34);
            this.PANTitle.TabIndex = 66;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(287, 4);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 68;
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
            // frmUOMMassReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 145);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.CMBUOM_DescTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CMBUOM_DescFrom);
            this.Controls.Add(this.LBLUOM_Desc);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmUOMMassReplace";
            this.Text = "UOM Mass Replace";
            this.Load += new System.EventHandler(this.frmUOMMassReplace_Load);
            this.Shown += new System.EventHandler(this.frmUOMMassReplace_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmUOMMassReplace_Paint);
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CMBUOM_DescFrom;
        private System.Windows.Forms.Label LBLUOM_Desc;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.ComboBox CMBUOM_DescTo;
        private Panel PANTitle;
        private Label LBLTitle;
        private Button BTNClose;
    }
}