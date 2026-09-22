using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    partial class frmRenameLocation
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
            this.CMBLOC_Location = new System.Windows.Forms.ComboBox();
            this.LBLLOC_Location = new System.Windows.Forms.Label();
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TXTLocTo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CMBLOC_Location
            // 
            this.CMBLOC_Location.FormattingEnabled = true;
            this.CMBLOC_Location.Location = new System.Drawing.Point(102, 12);
            this.CMBLOC_Location.MaxLength = 30;
            this.CMBLOC_Location.Name = "CMBLOC_Location";
            this.CMBLOC_Location.Size = new System.Drawing.Size(200, 21);
            this.CMBLOC_Location.TabIndex = 0;
            this.CMBLOC_Location.Tag = "1";
            this.CMBLOC_Location.SelectedValueChanged += new System.EventHandler(this.CMBLOC_Location_SelectedValueChanged);
            this.CMBLOC_Location.Leave += new System.EventHandler(this.CMBLOC_Location_Leave);
            // 
            // LBLLOC_Location
            // 
            this.LBLLOC_Location.AutoSize = true;
            this.LBLLOC_Location.ForeColor = System.Drawing.Color.Red;
            this.LBLLOC_Location.Location = new System.Drawing.Point(17, 16);
            this.LBLLOC_Location.Name = "LBLLOC_Location";
            this.LBLLOC_Location.Size = new System.Drawing.Size(77, 13);
            this.LBLLOC_Location.TabIndex = 46;
            this.LBLLOC_Location.Text = "Location From:";
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(299, 91);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 3;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(15, 92);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 2;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(17, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Location To:";
            // 
            // TXTLocTo
            // 
            this.TXTLocTo.Location = new System.Drawing.Point(103, 45);
            this.TXTLocTo.MaxLength = 30;
            this.TXTLocTo.Name = "TXTLocTo";
            this.TXTLocTo.Size = new System.Drawing.Size(199, 20);
            this.TXTLocTo.TabIndex = 1;
            this.TXTLocTo.Tag = "1";
            // 
            // frmRenameLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 126);
            this.Controls.Add(this.TXTLocTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CMBLOC_Location);
            this.Controls.Add(this.LBLLOC_Location);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmRenameLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rename Location";
            this.Load += new System.EventHandler(this.frmRenameLocation_Load);
            this.Shown += new System.EventHandler(this.frmRenameLocation_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmRenameLocation_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBLOC_Location;
        private System.Windows.Forms.Label LBLLOC_Location;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TXTLocTo;
    }
}