namespace RogStock2025_Utilities.Screens
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
            panel1 = new Panel();
            TVGroups_old = new TreeView();
            TVGroups = new TreeView();
            label3 = new Label();
            CMBUser = new ComboBox();
            label1 = new Label();
            STBStatus = new StatusStrip();
            STLStatus = new ToolStripStatusLabel();
            BTNUndo = new Button();
            BTNDelete = new Button();
            BTNSave = new Button();
            BTNClose = new Button();
            panel1.SuspendLayout();
            STBStatus.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(TVGroups_old);
            panel1.Controls.Add(TVGroups);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(CMBUser);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(13, 12);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(411, 472);
            panel1.TabIndex = 6;
            panel1.TabStop = true;
            // 
            // TVGroups_old
            // 
            TVGroups_old.BackColor = SystemColors.InactiveCaption;
            TVGroups_old.CheckBoxes = true;
            TVGroups_old.Location = new Point(199, 45);
            TVGroups_old.Name = "TVGroups_old";
            TVGroups_old.Size = new Size(98, 93);
            TVGroups_old.TabIndex = 14;
            TVGroups_old.Visible = false;
            // 
            // TVGroups
            // 
            TVGroups.CheckBoxes = true;
            TVGroups.Location = new Point(22, 63);
            TVGroups.Name = "TVGroups";
            TVGroups.Size = new Size(348, 350);
            TVGroups.TabIndex = 13;
            TVGroups.NodeMouseClick += TVGroups_NodeMouseClick;
            TVGroups.Click += TVGroups_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 45);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(99, 15);
            label3.TabIndex = 12;
            label3.Text = "Available Groups:";
            // 
            // CMBUser
            // 
            CMBUser.FormattingEnabled = true;
            CMBUser.Location = new Point(117, 10);
            CMBUser.Margin = new Padding(4, 3, 4, 3);
            CMBUser.Name = "CMBUser";
            CMBUser.Size = new Size(144, 23);
            CMBUser.Sorted = true;
            CMBUser.TabIndex = 1;
            CMBUser.Tag = "1";
            CMBUser.SelectedIndexChanged += CMBUser_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 16);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 7;
            label1.Text = "User Name:";
            // 
            // STBStatus
            // 
            STBStatus.Items.AddRange(new ToolStripItem[] { STLStatus });
            STBStatus.Location = new Point(0, 544);
            STBStatus.Name = "STBStatus";
            STBStatus.Size = new Size(439, 22);
            STBStatus.SizingGrip = false;
            STBStatus.TabIndex = 7;
            STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            STLStatus.Name = "STLStatus";
            STLStatus.Size = new Size(0, 17);
            // 
            // BTNUndo
            // 
            BTNUndo.Location = new Point(218, 512);
            BTNUndo.Margin = new Padding(4, 3, 4, 3);
            BTNUndo.Name = "BTNUndo";
            BTNUndo.Size = new Size(88, 27);
            BTNUndo.TabIndex = 16;
            BTNUndo.Text = "Undo";
            BTNUndo.UseVisualStyleBackColor = true;
            BTNUndo.Click += BTNUndo_Click;
            // 
            // BTNDelete
            // 
            BTNDelete.Location = new Point(117, 512);
            BTNDelete.Margin = new Padding(4, 3, 4, 3);
            BTNDelete.Name = "BTNDelete";
            BTNDelete.Size = new Size(88, 27);
            BTNDelete.TabIndex = 14;
            BTNDelete.Text = "Delete";
            BTNDelete.UseVisualStyleBackColor = true;
            BTNDelete.Click += BTNDelete_Click;
            // 
            // BTNSave
            // 
            BTNSave.Location = new Point(13, 512);
            BTNSave.Margin = new Padding(4, 3, 4, 3);
            BTNSave.Name = "BTNSave";
            BTNSave.Size = new Size(88, 27);
            BTNSave.TabIndex = 13;
            BTNSave.Text = "Save";
            BTNSave.UseVisualStyleBackColor = true;
            BTNSave.Click += BTNSave_Click;
            // 
            // BTNClose
            // 
            BTNClose.DialogResult = DialogResult.Cancel;
            BTNClose.Location = new Point(336, 512);
            BTNClose.Margin = new Padding(4, 3, 4, 3);
            BTNClose.Name = "BTNClose";
            BTNClose.Size = new Size(88, 27);
            BTNClose.TabIndex = 15;
            BTNClose.Text = "Close";
            BTNClose.UseVisualStyleBackColor = true;
            BTNClose.Click += BTNClose_Click;
            // 
            // frmUserGroups_Utilities
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 566);
            Controls.Add(BTNUndo);
            Controls.Add(BTNDelete);
            Controls.Add(BTNSave);
            Controls.Add(BTNClose);
            Controls.Add(STBStatus);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmUserGroups_Utilities";
            Text = "User Groups";
            Load += Form_UsersGroups_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            STBStatus.ResumeLayout(false);
            STBStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private Button BTNClose;
        private TreeView TVGroups_old;
        private TreeView TVGroups;
        private Label label3;
    }
}