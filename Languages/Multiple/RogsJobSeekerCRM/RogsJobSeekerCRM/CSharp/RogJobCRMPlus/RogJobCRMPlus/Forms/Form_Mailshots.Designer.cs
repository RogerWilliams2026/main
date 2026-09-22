namespace RogJobCRMPlus.Forms
{
    partial class frmMailshots
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMailshots));
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.BTNFind = new System.Windows.Forms.Button();
            this.CMBMSH_MailshotName = new System.Windows.Forms.ComboBox();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.BTNImport = new System.Windows.Forms.Button();
            this.LVLines = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LBLMSH_MailshotName = new System.Windows.Forms.Label();
            this.BTNSave = new System.Windows.Forms.Button();
            this.CMBMailshotID = new System.Windows.Forms.ComboBox();
            this.LBLMSH_ID = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.DTEMSH_Date = new System.Windows.Forms.DateTimePicker();
            this.LBLMSH_Date = new System.Windows.Forms.Label();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.TXTMSH_Comments = new System.Windows.Forms.TextBox();
            this.LBLMSH_Comments = new System.Windows.Forms.Label();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(0, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(589, 34);
            this.PANTitle.TabIndex = 77;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(564, 6);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 79;
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
            this.LBLTitle.Size = new System.Drawing.Size(0, 17);
            this.LBLTitle.TabIndex = 0;
            // 
            // BTNFind
            // 
            this.BTNFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNFind.Location = new System.Drawing.Point(279, 43);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(57, 20);
            this.BTNFind.TabIndex = 2;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // CMBMSH_MailshotName
            // 
            this.CMBMSH_MailshotName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBMSH_MailshotName.FormattingEnabled = true;
            this.CMBMSH_MailshotName.Location = new System.Drawing.Point(137, 70);
            this.CMBMSH_MailshotName.Name = "CMBMSH_MailshotName";
            this.CMBMSH_MailshotName.Size = new System.Drawing.Size(438, 24);
            this.CMBMSH_MailshotName.Sorted = true;
            this.CMBMSH_MailshotName.TabIndex = 3;
            this.CMBMSH_MailshotName.Tag = "1";
            // 
            // BTNUndo
            // 
            this.BTNUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNUndo.Location = new System.Drawing.Point(218, 554);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(85, 34);
            this.BTNUndo.TabIndex = 8;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // BTNImport
            // 
            this.BTNImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNImport.Location = new System.Drawing.Point(486, 554);
            this.BTNImport.Name = "BTNImport";
            this.BTNImport.Size = new System.Drawing.Size(89, 34);
            this.BTNImport.TabIndex = 10;
            this.BTNImport.Text = "Import";
            this.BTNImport.UseVisualStyleBackColor = true;
            this.BTNImport.Click += new System.EventHandler(this.BTNImport_Click);
            // 
            // LVLines
            // 
            this.LVLines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.LVLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LVLines.HideSelection = false;
            this.LVLines.Location = new System.Drawing.Point(14, 255);
            this.LVLines.Name = "LVLines";
            this.LVLines.Size = new System.Drawing.Size(561, 284);
            this.LVLines.TabIndex = 6;
            this.LVLines.TabStop = false;
            this.LVLines.UseCompatibleStateImageBehavior = false;
            this.LVLines.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Who";
            this.columnHeader1.Width = 340;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Email";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "MSL_ID";
            this.columnHeader3.Width = 0;
            // 
            // LBLMSH_MailshotName
            // 
            this.LBLMSH_MailshotName.AutoSize = true;
            this.LBLMSH_MailshotName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLMSH_MailshotName.Location = new System.Drawing.Point(19, 73);
            this.LBLMSH_MailshotName.Name = "LBLMSH_MailshotName";
            this.LBLMSH_MailshotName.Size = new System.Drawing.Size(46, 17);
            this.LBLMSH_MailshotName.TabIndex = 118;
            this.LBLMSH_MailshotName.Text = "label1";
            // 
            // BTNSave
            // 
            this.BTNSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNSave.Location = new System.Drawing.Point(14, 551);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(85, 34);
            this.BTNSave.TabIndex = 7;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // CMBMailshotID
            // 
            this.CMBMailshotID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBMailshotID.FormattingEnabled = true;
            this.CMBMailshotID.Location = new System.Drawing.Point(137, 43);
            this.CMBMailshotID.Name = "CMBMailshotID";
            this.CMBMailshotID.Size = new System.Drawing.Size(57, 24);
            this.CMBMailshotID.TabIndex = 0;
            this.CMBMailshotID.SelectedValueChanged += new System.EventHandler(this.CMBMailshotID_SelectedValueChanged);
            // 
            // LBLMSH_ID
            // 
            this.LBLMSH_ID.AutoSize = true;
            this.LBLMSH_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLMSH_ID.Location = new System.Drawing.Point(18, 47);
            this.LBLMSH_ID.Name = "LBLMSH_ID";
            this.LBLMSH_ID.Size = new System.Drawing.Size(46, 17);
            this.LBLMSH_ID.TabIndex = 120;
            this.LBLMSH_ID.Text = "label1";
            // 
            // BTNNew
            // 
            this.BTNNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNNew.Location = new System.Drawing.Point(202, 44);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(50, 20);
            this.BTNNew.TabIndex = 1;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // DTEMSH_Date
            // 
            this.DTEMSH_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTEMSH_Date.Location = new System.Drawing.Point(137, 97);
            this.DTEMSH_Date.Name = "DTEMSH_Date";
            this.DTEMSH_Date.Size = new System.Drawing.Size(140, 23);
            this.DTEMSH_Date.TabIndex = 4;
            this.DTEMSH_Date.Tag = "";
            // 
            // LBLMSH_Date
            // 
            this.LBLMSH_Date.AutoSize = true;
            this.LBLMSH_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLMSH_Date.Location = new System.Drawing.Point(18, 101);
            this.LBLMSH_Date.Name = "LBLMSH_Date";
            this.LBLMSH_Date.Size = new System.Drawing.Size(46, 17);
            this.LBLMSH_Date.TabIndex = 123;
            this.LBLMSH_Date.Tag = "";
            this.LBLMSH_Date.Text = "label3";
            // 
            // BTNDelete
            // 
            this.BTNDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNDelete.Location = new System.Drawing.Point(323, 554);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(85, 34);
            this.BTNDelete.TabIndex = 9;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // TXTMSH_Comments
            // 
            this.TXTMSH_Comments.Location = new System.Drawing.Point(17, 152);
            this.TXTMSH_Comments.Multiline = true;
            this.TXTMSH_Comments.Name = "TXTMSH_Comments";
            this.TXTMSH_Comments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TXTMSH_Comments.Size = new System.Drawing.Size(558, 97);
            this.TXTMSH_Comments.TabIndex = 152;
            this.TXTMSH_Comments.Tag = "5";
            // 
            // LBLMSH_Comments
            // 
            this.LBLMSH_Comments.AutoSize = true;
            this.LBLMSH_Comments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLMSH_Comments.Location = new System.Drawing.Point(16, 129);
            this.LBLMSH_Comments.Name = "LBLMSH_Comments";
            this.LBLMSH_Comments.Size = new System.Drawing.Size(46, 17);
            this.LBLMSH_Comments.TabIndex = 153;
            this.LBLMSH_Comments.Text = "label3";
            // 
            // frmMailshots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 596);
            this.Controls.Add(this.TXTMSH_Comments);
            this.Controls.Add(this.LBLMSH_Comments);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.DTEMSH_Date);
            this.Controls.Add(this.LBLMSH_Date);
            this.Controls.Add(this.BTNNew);
            this.Controls.Add(this.CMBMailshotID);
            this.Controls.Add(this.LBLMSH_ID);
            this.Controls.Add(this.BTNSave);
            this.Controls.Add(this.LBLMSH_MailshotName);
            this.Controls.Add(this.LVLines);
            this.Controls.Add(this.BTNImport);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.CMBMSH_MailshotName);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.PANTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMailshots";
            this.Text = "Mailshots";
            this.Load += new System.EventHandler(this.frmMailshots_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMailshots_Paint);
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel PANTitle;
        private System.Windows.Forms.Label LBLTitle;
        private System.Windows.Forms.Button BTNFind;
        private System.Windows.Forms.ComboBox CMBMSH_MailshotName;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.Button BTNImport;
        private System.Windows.Forms.ListView LVLines;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label LBLMSH_MailshotName;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.ComboBox CMBMailshotID;
        private System.Windows.Forms.Label LBLMSH_ID;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.DateTimePicker DTEMSH_Date;
        private System.Windows.Forms.Label LBLMSH_Date;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox TXTMSH_Comments;
        private System.Windows.Forms.Label LBLMSH_Comments;
        private System.Windows.Forms.Button BTNClose;
    }
}