using System.Windows.Forms;
using System.Drawing;

namespace RogStock2026_Utilities.Screens
{
    partial class frmUserGroups_Utilities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserGroups_Utilities));
            this.panel1 = new System.Windows.Forms.Panel();
            this.TVGroups_old = new System.Windows.Forms.TreeView();
            this.TVGroups = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.CMBUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.STBStatus = new System.Windows.Forms.StatusStrip();
            this.STLStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.STBStatus.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.TVGroups_old);
            this.panel1.Controls.Add(this.TVGroups);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.CMBUser);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(11, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 409);
            this.panel1.TabIndex = 6;
            this.panel1.TabStop = true;
            // 
            // TVGroups_old
            // 
            this.TVGroups_old.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TVGroups_old.CheckBoxes = true;
            this.TVGroups_old.Location = new System.Drawing.Point(171, 39);
            this.TVGroups_old.Name = "TVGroups_old";
            this.TVGroups_old.Size = new System.Drawing.Size(85, 81);
            this.TVGroups_old.TabIndex = 14;
            this.TVGroups_old.Visible = false;
            // 
            // TVGroups
            // 
            this.TVGroups.CheckBoxes = true;
            this.TVGroups.Location = new System.Drawing.Point(19, 55);
            this.TVGroups.Name = "TVGroups";
            this.TVGroups.Size = new System.Drawing.Size(299, 304);
            this.TVGroups.TabIndex = 13;
            this.TVGroups.Click += new System.EventHandler(this.TVGroups_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Available Groups:";
            // 
            // CMBUser
            // 
            this.CMBUser.FormattingEnabled = true;
            this.CMBUser.Location = new System.Drawing.Point(100, 9);
            this.CMBUser.Name = "CMBUser";
            this.CMBUser.Size = new System.Drawing.Size(124, 21);
            this.CMBUser.Sorted = true;
            this.CMBUser.TabIndex = 1;
            this.CMBUser.Tag = "1";
            this.CMBUser.SelectedIndexChanged += new System.EventHandler(this.CMBUser_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "User Name:";
            // 
            // STBStatus
            // 
            this.STBStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.STLStatus});
            this.STBStatus.Location = new System.Drawing.Point(0, 502);
            this.STBStatus.Name = "STBStatus";
            this.STBStatus.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.STBStatus.Size = new System.Drawing.Size(376, 22);
            this.STBStatus.SizingGrip = false;
            this.STBStatus.TabIndex = 7;
            this.STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            this.STLStatus.Name = "STLStatus";
            this.STLStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(187, 476);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 16;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(100, 476);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 14;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(11, 476);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 13;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(1, 2);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(375, 34);
            this.PANTitle.TabIndex = 125;
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
            this.LBLTitle.Size = new System.Drawing.Size(188, 17);
            this.LBLTitle.TabIndex = 0;
            this.LBLTitle.Text = "User Grous Maintenance";
            // 
            // frmUserGroups_Utilities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 524);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNSave);
            this.Controls.Add(this.STBStatus);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmUserGroups_Utilities";
            this.Text = "User Groups";
            this.Load += new System.EventHandler(this.Form_UsersGroups_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.STBStatus.ResumeLayout(false);
            this.STBStatus.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private ComboBox CMBUser;
        private Label label1;
        private StatusStrip STBStatus;
        private ToolStripStatusLabel STLStatus;
        private Button BTNUndo;
        private Button BTNDelete;
        private Button BTNSave;
        private TreeView TVGroups_old;
        private TreeView TVGroups;
        private Label label3;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
    }
}