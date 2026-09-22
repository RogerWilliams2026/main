using System.Windows.Forms;
using System.Drawing;

namespace RogStock2026_Utilities.Screens
{
    partial class frmGroups_Utilities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGroups_Utilities));
            this.STBStatus = new System.Windows.Forms.StatusStrip();
            this.STLStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TXTHidden = new System.Windows.Forms.TextBox();
            this.TVMenuItems_old = new System.Windows.Forms.TreeView();
            this.TVMenuItems = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.CMBGRP_Group = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BTNSave = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
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
            this.STBStatus.Location = new System.Drawing.Point(0, 477);
            this.STBStatus.Name = "STBStatus";
            this.STBStatus.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.STBStatus.Size = new System.Drawing.Size(374, 22);
            this.STBStatus.SizingGrip = false;
            this.STBStatus.TabIndex = 11;
            this.STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            this.STLStatus.Name = "STLStatus";
            this.STLStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(100, 451);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 8;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.TXTHidden);
            this.panel1.Controls.Add(this.TVMenuItems_old);
            this.panel1.Controls.Add(this.TVMenuItems);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.BTNNew);
            this.panel1.Controls.Add(this.CMBGRP_Group);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(15, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 397);
            this.panel1.TabIndex = 9;
            this.panel1.TabStop = true;
            // 
            // TXTHidden
            // 
            this.TXTHidden.Location = new System.Drawing.Point(403, 18);
            this.TXTHidden.Name = "TXTHidden";
            this.TXTHidden.Size = new System.Drawing.Size(1, 20);
            this.TXTHidden.TabIndex = 12;
            this.TXTHidden.TabStop = false;
            // 
            // TVMenuItems_old
            // 
            this.TVMenuItems_old.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TVMenuItems_old.CheckBoxes = true;
            this.TVMenuItems_old.Location = new System.Drawing.Point(171, 47);
            this.TVMenuItems_old.Name = "TVMenuItems_old";
            this.TVMenuItems_old.Size = new System.Drawing.Size(85, 81);
            this.TVMenuItems_old.TabIndex = 11;
            this.TVMenuItems_old.Visible = false;
            // 
            // TVMenuItems
            // 
            this.TVMenuItems.CheckBoxes = true;
            this.TVMenuItems.Location = new System.Drawing.Point(19, 62);
            this.TVMenuItems.Name = "TVMenuItems";
            this.TVMenuItems.Size = new System.Drawing.Size(299, 304);
            this.TVMenuItems.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Menu Items:";
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(244, 18);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(39, 20);
            this.BTNNew.TabIndex = 0;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // CMBGRP_Group
            // 
            this.CMBGRP_Group.BackColor = System.Drawing.SystemColors.Window;
            this.CMBGRP_Group.FormattingEnabled = true;
            this.CMBGRP_Group.Location = new System.Drawing.Point(91, 17);
            this.CMBGRP_Group.Name = "CMBGRP_Group";
            this.CMBGRP_Group.Size = new System.Drawing.Size(124, 21);
            this.CMBGRP_Group.Sorted = true;
            this.CMBGRP_Group.TabIndex = 1;
            this.CMBGRP_Group.Tag = "1";
            this.CMBGRP_Group.SelectedIndexChanged += new System.EventHandler(this.CMBGRP_Group_SelectedIndexChanged);
            this.CMBGRP_Group.Leave += new System.EventHandler(this.CMBGroups_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Group:";
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(11, 451);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 7;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(187, 451);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 12;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(-1, 1);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(375, 34);
            this.PANTitle.TabIndex = 124;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(350, 4);
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
            this.LBLTitle.Size = new System.Drawing.Size(158, 17);
            this.LBLTitle.TabIndex = 0;
            this.LBLTitle.Text = "Groups Maintenance";
            // 
            // frmGroups_Utilities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 499);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.STBStatus);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmGroups_Utilities";
            this.Text = "Groups Maintenance";
            this.Load += new System.EventHandler(this.frmGroups_Utilities_Load);
            this.Shown += new System.EventHandler(this.frmGroups_Utilities_Shown);
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
        private Button BTNNew;
        private ComboBox CMBGRP_Group;
        private Label label1;
        private Button BTNSave;
        private Label label2;
        private TreeView TVMenuItems;
        private Button BTNUndo;
        private TreeView TVMenuItems_old;
        private TextBox TXTHidden;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
    }
}