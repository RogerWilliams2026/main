using System.Windows.Forms;
using System.Drawing;

namespace RogStock2026_Utilities.Screens
{
    partial class frmMenuItems_Utilities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuItems_Utilities));
            this.STBStatus = new System.Windows.Forms.StatusStrip();
            this.STLStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CMBMNU_MenuItemName = new System.Windows.Forms.ComboBox();
            this.TXTMNU_MenuItemObject = new System.Windows.Forms.TextBox();
            this.CMBMNU_DisplayWhere = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CMBMNU_Type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BTNSave = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.TXTHidden = new System.Windows.Forms.TextBox();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.STBStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // STBStatus
            // 
            this.STBStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.STLStatus});
            this.STBStatus.Location = new System.Drawing.Point(0, 221);
            this.STBStatus.Name = "STBStatus";
            this.STBStatus.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.STBStatus.Size = new System.Drawing.Size(477, 22);
            this.STBStatus.SizingGrip = false;
            this.STBStatus.TabIndex = 15;
            this.STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            this.STLStatus.Name = "STLStatus";
            this.STLStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(101, 189);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 5;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CMBMNU_MenuItemName);
            this.panel1.Controls.Add(this.TXTMNU_MenuItemObject);
            this.panel1.Controls.Add(this.CMBMNU_DisplayWhere);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.CMBMNU_Type);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.BTNNew);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 135);
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = true;
            // 
            // CMBMNU_MenuItemName
            // 
            this.CMBMNU_MenuItemName.FormattingEnabled = true;
            this.CMBMNU_MenuItemName.Location = new System.Drawing.Point(128, 16);
            this.CMBMNU_MenuItemName.Name = "CMBMNU_MenuItemName";
            this.CMBMNU_MenuItemName.Size = new System.Drawing.Size(158, 21);
            this.CMBMNU_MenuItemName.Sorted = true;
            this.CMBMNU_MenuItemName.TabIndex = 0;
            this.CMBMNU_MenuItemName.Tag = "1";
            this.CMBMNU_MenuItemName.SelectedIndexChanged += new System.EventHandler(this.CMBMNU_MenuItemName_SelectedIndexChanged);
            this.CMBMNU_MenuItemName.Leave += new System.EventHandler(this.CMBMNU_MenuItemName_Leave);
            // 
            // TXTMNU_MenuItemObject
            // 
            this.TXTMNU_MenuItemObject.Location = new System.Drawing.Point(128, 42);
            this.TXTMNU_MenuItemObject.MaxLength = 50;
            this.TXTMNU_MenuItemObject.Name = "TXTMNU_MenuItemObject";
            this.TXTMNU_MenuItemObject.Size = new System.Drawing.Size(135, 20);
            this.TXTMNU_MenuItemObject.TabIndex = 1;
            this.TXTMNU_MenuItemObject.Tag = "1";
            // 
            // CMBMNU_DisplayWhere
            // 
            this.CMBMNU_DisplayWhere.FormattingEnabled = true;
            this.CMBMNU_DisplayWhere.Location = new System.Drawing.Point(127, 94);
            this.CMBMNU_DisplayWhere.MaxLength = 50;
            this.CMBMNU_DisplayWhere.Name = "CMBMNU_DisplayWhere";
            this.CMBMNU_DisplayWhere.Size = new System.Drawing.Size(124, 21);
            this.CMBMNU_DisplayWhere.Sorted = true;
            this.CMBMNU_DisplayWhere.TabIndex = 3;
            this.CMBMNU_DisplayWhere.Tag = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Display Where:";
            // 
            // CMBMNU_Type
            // 
            this.CMBMNU_Type.FormattingEnabled = true;
            this.CMBMNU_Type.Items.AddRange(new object[] {
            "Form",
            "Operation",
            "Report"});
            this.CMBMNU_Type.Location = new System.Drawing.Point(127, 68);
            this.CMBMNU_Type.MaxLength = 10;
            this.CMBMNU_Type.Name = "CMBMNU_Type";
            this.CMBMNU_Type.Size = new System.Drawing.Size(124, 21);
            this.CMBMNU_Type.Sorted = true;
            this.CMBMNU_Type.TabIndex = 2;
            this.CMBMNU_Type.Tag = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Menu Item Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Menu Item Object:";
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(300, 18);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(39, 20);
            this.BTNNew.TabIndex = 0;
            this.BTNNew.TabStop = false;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Menu Item Caption:";
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(12, 189);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 4;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(210, 189);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 6;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // TXTHidden
            // 
            this.TXTHidden.Location = new System.Drawing.Point(0, 29);
            this.TXTHidden.Name = "TXTHidden";
            this.TXTHidden.Size = new System.Drawing.Size(1, 20);
            this.TXTHidden.TabIndex = 17;
            this.TXTHidden.TabStop = false;
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(1, -1);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(476, 34);
            this.PANTitle.TabIndex = 127;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(449, 4);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 125;
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // LBLTitle
            // 
            this.LBLTitle.AutoSize = true;
            this.LBLTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLTitle.ForeColor = System.Drawing.Color.White;
            this.LBLTitle.Location = new System.Drawing.Point(9, 8);
            this.LBLTitle.Name = "LBLTitle";
            this.LBLTitle.Size = new System.Drawing.Size(179, 17);
            this.LBLTitle.TabIndex = 0;
            this.LBLTitle.Text = "Menu Item Maintenance";
            // 
            // frmMenuItems_Utilities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 243);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.TXTHidden);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.STBStatus);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmMenuItems_Utilities";
            this.Text = "Menu Items";
            this.Load += new System.EventHandler(this.frmMenuItems_Utilities_Load);
            this.STBStatus.ResumeLayout(false);
            this.STBStatus.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip STBStatus;
        private ToolStripStatusLabel STLStatus;
        private Button BTNDelete;
        private Panel panel1;
        private ComboBox CMBMNU_DisplayWhere;
        private Label label4;
        private ComboBox CMBMNU_Type;
        private Label label3;
        private Label label2;
        private Button BTNNew;
        private Label label1;
        private Button BTNSave;
        private TextBox TXTMNU_MenuItemObject;
        private Button BTNUndo;
        private TextBox TXTHidden;
        private ComboBox CMBMNU_MenuItemName;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
    }
}