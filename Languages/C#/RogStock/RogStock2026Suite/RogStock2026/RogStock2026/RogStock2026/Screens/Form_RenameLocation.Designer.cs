using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2026.Screens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRenameLocation));
            this.CMBLOC_Location = new System.Windows.Forms.ComboBox();
            this.LBLLOC_Location = new System.Windows.Forms.Label();
            this.BTNSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TXTLocTo = new System.Windows.Forms.TextBox();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // CMBLOC_Location
            // 
            this.CMBLOC_Location.FormattingEnabled = true;
            this.CMBLOC_Location.Location = new System.Drawing.Point(97, 36);
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
            this.LBLLOC_Location.Location = new System.Drawing.Point(12, 40);
            this.LBLLOC_Location.Name = "LBLLOC_Location";
            this.LBLLOC_Location.Size = new System.Drawing.Size(77, 13);
            this.LBLLOC_Location.TabIndex = 46;
            this.LBLLOC_Location.Text = "Location From:";
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(13, 111);
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
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Location To:";
            // 
            // TXTLocTo
            // 
            this.TXTLocTo.Location = new System.Drawing.Point(98, 69);
            this.TXTLocTo.MaxLength = 30;
            this.TXTLocTo.Name = "TXTLocTo";
            this.TXTLocTo.Size = new System.Drawing.Size(199, 20);
            this.TXTLocTo.TabIndex = 1;
            this.TXTLocTo.Tag = "1";
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(0, -2);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(310, 34);
            this.PANTitle.TabIndex = 62;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(284, 4);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 64;
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
            // frmRenameLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 140);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.TXTLocTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CMBLOC_Location);
            this.Controls.Add(this.LBLLOC_Location);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmRenameLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rename Location";
            this.Load += new System.EventHandler(this.frmRenameLocation_Load);
            this.Shown += new System.EventHandler(this.frmRenameLocation_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmRenameLocation_Paint);
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBLOC_Location;
        private System.Windows.Forms.Label LBLLOC_Location;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TXTLocTo;
        private Panel PANTitle;
        private Label LBLTitle;
        private Button BTNClose;
    }
}