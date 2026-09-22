namespace RogRessourceCreator
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.CMBDrives = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LVFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TVDirs = new System.Windows.Forms.TreeView();
            this.STAStatus = new System.Windows.Forms.StatusStrip();
            this.SLBLStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.SPRRGProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.BTNRemove = new System.Windows.Forms.Button();
            this.PICClose = new System.Windows.Forms.PictureBox();
            this.BTNAdd = new System.Windows.Forms.Button();
            this.BTNCreate = new System.Windows.Forms.Button();
            this.LVResources = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.STAStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PICClose)).BeginInit();
            this.SuspendLayout();
            // 
            // CMBDrives
            // 
            this.CMBDrives.FormattingEnabled = true;
            this.CMBDrives.Location = new System.Drawing.Point(67, 41);
            this.CMBDrives.Name = "CMBDrives";
            this.CMBDrives.Size = new System.Drawing.Size(155, 21);
            this.CMBDrives.TabIndex = 12;
            this.CMBDrives.SelectedIndexChanged += new System.EventHandler(this.CMBDrives_SelectedIndexChanged);
            this.CMBDrives.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CMBDrives_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Drives:";
            // 
            // LVFiles
            // 
            this.LVFiles.BackColor = System.Drawing.Color.OliveDrab;
            this.LVFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.LVFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVFiles.HideSelection = false;
            this.LVFiles.Location = new System.Drawing.Point(372, 85);
            this.LVFiles.Name = "LVFiles";
            this.LVFiles.Size = new System.Drawing.Size(436, 290);
            this.LVFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LVFiles.TabIndex = 10;
            this.LVFiles.UseCompatibleStateImageBehavior = false;
            this.LVFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File";
            this.columnHeader1.Width = 400;
            // 
            // TVDirs
            // 
            this.TVDirs.BackColor = System.Drawing.Color.OliveDrab;
            this.TVDirs.CheckBoxes = true;
            this.TVDirs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TVDirs.HideSelection = false;
            this.TVDirs.Location = new System.Drawing.Point(9, 85);
            this.TVDirs.Name = "TVDirs";
            this.TVDirs.Size = new System.Drawing.Size(347, 290);
            this.TVDirs.TabIndex = 9;
            this.TVDirs.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TVDirs_AfterCheck);
            this.TVDirs.DoubleClick += new System.EventHandler(this.TVDirs_DoubleClick);
            // 
            // STAStatus
            // 
            this.STAStatus.BackColor = System.Drawing.Color.OliveDrab;
            this.STAStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SLBLStatus,
            this.SPRRGProgress});
            this.STAStatus.Location = new System.Drawing.Point(0, 494);
            this.STAStatus.Name = "STAStatus";
            this.STAStatus.Size = new System.Drawing.Size(1344, 22);
            this.STAStatus.SizingGrip = false;
            this.STAStatus.TabIndex = 13;
            this.STAStatus.Text = "statusStrip1";
            // 
            // SLBLStatus
            // 
            this.SLBLStatus.AutoSize = false;
            this.SLBLStatus.BackColor = System.Drawing.Color.OliveDrab;
            this.SLBLStatus.Name = "SLBLStatus";
            this.SLBLStatus.Size = new System.Drawing.Size(150, 17);
            this.SLBLStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SPRRGProgress
            // 
            this.SPRRGProgress.Maximum = 100000;
            this.SPRRGProgress.Name = "SPRRGProgress";
            this.SPRRGProgress.Size = new System.Drawing.Size(200, 16);
            // 
            // BTNRemove
            // 
            this.BTNRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNRemove.Location = new System.Drawing.Point(830, 219);
            this.BTNRemove.Name = "BTNRemove";
            this.BTNRemove.Size = new System.Drawing.Size(42, 29);
            this.BTNRemove.TabIndex = 14;
            this.BTNRemove.Text = "<<";
            this.BTNRemove.UseVisualStyleBackColor = true;
            this.BTNRemove.Click += new System.EventHandler(this.BTNRemove_Click);
            // 
            // PICClose
            // 
            this.PICClose.Image = ((System.Drawing.Image)(resources.GetObject("PICClose.Image")));
            this.PICClose.Location = new System.Drawing.Point(1320, 2);
            this.PICClose.Name = "PICClose";
            this.PICClose.Size = new System.Drawing.Size(21, 21);
            this.PICClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PICClose.TabIndex = 16;
            this.PICClose.TabStop = false;
            this.PICClose.Click += new System.EventHandler(this.PICClose_Click);
            // 
            // BTNAdd
            // 
            this.BTNAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNAdd.Location = new System.Drawing.Point(830, 161);
            this.BTNAdd.Name = "BTNAdd";
            this.BTNAdd.Size = new System.Drawing.Size(42, 29);
            this.BTNAdd.TabIndex = 17;
            this.BTNAdd.Text = ">>";
            this.BTNAdd.UseVisualStyleBackColor = true;
            this.BTNAdd.Click += new System.EventHandler(this.BTNAdd_Click);
            // 
            // BTNCreate
            // 
            this.BTNCreate.Location = new System.Drawing.Point(914, 403);
            this.BTNCreate.Name = "BTNCreate";
            this.BTNCreate.Size = new System.Drawing.Size(151, 35);
            this.BTNCreate.TabIndex = 18;
            this.BTNCreate.Text = "Create Resource File";
            this.BTNCreate.UseVisualStyleBackColor = true;
            this.BTNCreate.Click += new System.EventHandler(this.BTNCreate_Click);
            // 
            // LVResources
            // 
            this.LVResources.BackColor = System.Drawing.Color.OliveDrab;
            this.LVResources.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.LVResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVResources.HideSelection = false;
            this.LVResources.LabelEdit = true;
            this.LVResources.LabelWrap = false;
            this.LVResources.Location = new System.Drawing.Point(899, 85);
            this.LVResources.MultiSelect = false;
            this.LVResources.Name = "LVResources";
            this.LVResources.Size = new System.Drawing.Size(436, 290);
            this.LVResources.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LVResources.TabIndex = 19;
            this.LVResources.UseCompatibleStateImageBehavior = false;
            this.LVResources.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Control Name";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "File";
            this.columnHeader3.Width = 290;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Olive;
            this.ClientSize = new System.Drawing.Size(1344, 516);
            this.Controls.Add(this.LVResources);
            this.Controls.Add(this.BTNCreate);
            this.Controls.Add(this.BTNAdd);
            this.Controls.Add(this.PICClose);
            this.Controls.Add(this.BTNRemove);
            this.Controls.Add(this.STAStatus);
            this.Controls.Add(this.CMBDrives);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LVFiles);
            this.Controls.Add(this.TVDirs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rog\'s Resource File Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseDown);
            this.STAStatus.ResumeLayout(false);
            this.STAStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PICClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBDrives;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView LVFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TreeView TVDirs;
        private System.Windows.Forms.StatusStrip STAStatus;
        private System.Windows.Forms.ToolStripStatusLabel SLBLStatus;
        private System.Windows.Forms.ToolStripProgressBar SPRRGProgress;
        private System.Windows.Forms.Button BTNRemove;
        private System.Windows.Forms.PictureBox PICClose;
        private System.Windows.Forms.Button BTNAdd;
        private System.Windows.Forms.Button BTNCreate;
        private System.Windows.Forms.ListView LVResources;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}

