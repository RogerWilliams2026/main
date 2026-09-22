namespace RogJobCRMPlus.Forms
{
    partial class frmEvents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEvents));
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.BTNFind = new System.Windows.Forms.Button();
            this.LBLStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.CMBEVTID = new System.Windows.Forms.ComboBox();
            this.LBLEVT_ID = new System.Windows.Forms.Label();
            this.CMBEVT_Name = new System.Windows.Forms.ComboBox();
            this.LBLEVT_Name = new System.Windows.Forms.Label();
            this.DTEEVT_Date = new System.Windows.Forms.DateTimePicker();
            this.LBLEVT_Date = new System.Windows.Forms.Label();
            this.CMBEVT_Where = new System.Windows.Forms.ComboBox();
            this.LBLEVT_Where = new System.Windows.Forms.Label();
            this.LBLEVT_Booked = new System.Windows.Forms.Label();
            this.CHKEVT_Booked = new System.Windows.Forms.CheckBox();
            this.LBLEVT_Attended = new System.Windows.Forms.Label();
            this.CHKEVT_Attended = new System.Windows.Forms.CheckBox();
            this.TXTEVT_Details = new System.Windows.Forms.TextBox();
            this.LBLEVT_Details = new System.Windows.Forms.Label();
            this.LBLEVT_Contact = new System.Windows.Forms.Label();
            this.TXTEVT_Contact = new System.Windows.Forms.TextBox();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.CMBEVT_Website = new System.Windows.Forms.ComboBox();
            this.LBLEVT_Website = new System.Windows.Forms.Label();
            this.TXTEVT_Comments = new System.Windows.Forms.TextBox();
            this.LBLEVT_Comments = new System.Windows.Forms.Label();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(2, 3);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(744, 34);
            this.PANTitle.TabIndex = 79;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(719, 6);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 81;
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
            this.BTNFind.Location = new System.Drawing.Point(291, 50);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(50, 20);
            this.BTNFind.TabIndex = 2;
            this.BTNFind.TabStop = false;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // LBLStatus
            // 
            this.LBLStatus.AutoSize = true;
            this.LBLStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLStatus.Location = new System.Drawing.Point(461, 53);
            this.LBLStatus.Name = "LBLStatus";
            this.LBLStatus.Size = new System.Drawing.Size(0, 17);
            this.LBLStatus.TabIndex = 121;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(376, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 120;
            this.label2.Text = "Record Status:";
            // 
            // BTNNew
            // 
            this.BTNNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNNew.Location = new System.Drawing.Point(203, 50);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(50, 20);
            this.BTNNew.TabIndex = 1;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // CMBEVTID
            // 
            this.CMBEVTID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBEVTID.FormattingEnabled = true;
            this.CMBEVTID.Location = new System.Drawing.Point(128, 49);
            this.CMBEVTID.Name = "CMBEVTID";
            this.CMBEVTID.Size = new System.Drawing.Size(57, 24);
            this.CMBEVTID.TabIndex = 0;
            this.CMBEVTID.SelectedValueChanged += new System.EventHandler(this.CMBEVTID_SelectedValueChanged);
            this.CMBEVTID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CMBEVTID_KeyDown);
            // 
            // LBLEVT_ID
            // 
            this.LBLEVT_ID.AutoSize = true;
            this.LBLEVT_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_ID.Location = new System.Drawing.Point(12, 53);
            this.LBLEVT_ID.Name = "LBLEVT_ID";
            this.LBLEVT_ID.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_ID.TabIndex = 117;
            this.LBLEVT_ID.Text = "label1";
            // 
            // CMBEVT_Name
            // 
            this.CMBEVT_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBEVT_Name.FormattingEnabled = true;
            this.CMBEVT_Name.Location = new System.Drawing.Point(127, 80);
            this.CMBEVT_Name.Name = "CMBEVT_Name";
            this.CMBEVT_Name.Size = new System.Drawing.Size(159, 24);
            this.CMBEVT_Name.Sorted = true;
            this.CMBEVT_Name.TabIndex = 3;
            this.CMBEVT_Name.Tag = "1";
            // 
            // LBLEVT_Name
            // 
            this.LBLEVT_Name.AutoSize = true;
            this.LBLEVT_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_Name.Location = new System.Drawing.Point(12, 83);
            this.LBLEVT_Name.Name = "LBLEVT_Name";
            this.LBLEVT_Name.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_Name.TabIndex = 123;
            this.LBLEVT_Name.Text = "label3";
            // 
            // DTEEVT_Date
            // 
            this.DTEEVT_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTEEVT_Date.Location = new System.Drawing.Point(127, 135);
            this.DTEEVT_Date.Name = "DTEEVT_Date";
            this.DTEEVT_Date.Size = new System.Drawing.Size(140, 23);
            this.DTEEVT_Date.TabIndex = 6;
            this.DTEEVT_Date.Tag = "1";
            // 
            // LBLEVT_Date
            // 
            this.LBLEVT_Date.AutoSize = true;
            this.LBLEVT_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_Date.Location = new System.Drawing.Point(12, 139);
            this.LBLEVT_Date.Name = "LBLEVT_Date";
            this.LBLEVT_Date.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_Date.TabIndex = 125;
            this.LBLEVT_Date.Tag = "1";
            this.LBLEVT_Date.Text = "label3";
            // 
            // CMBEVT_Where
            // 
            this.CMBEVT_Where.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBEVT_Where.FormattingEnabled = true;
            this.CMBEVT_Where.Location = new System.Drawing.Point(127, 107);
            this.CMBEVT_Where.Name = "CMBEVT_Where";
            this.CMBEVT_Where.Size = new System.Drawing.Size(218, 24);
            this.CMBEVT_Where.Sorted = true;
            this.CMBEVT_Where.TabIndex = 4;
            this.CMBEVT_Where.Tag = "1";
            // 
            // LBLEVT_Where
            // 
            this.LBLEVT_Where.AutoSize = true;
            this.LBLEVT_Where.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_Where.Location = new System.Drawing.Point(12, 110);
            this.LBLEVT_Where.Name = "LBLEVT_Where";
            this.LBLEVT_Where.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_Where.TabIndex = 127;
            this.LBLEVT_Where.Text = "label3";
            // 
            // LBLEVT_Booked
            // 
            this.LBLEVT_Booked.AutoSize = true;
            this.LBLEVT_Booked.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_Booked.Location = new System.Drawing.Point(12, 172);
            this.LBLEVT_Booked.Name = "LBLEVT_Booked";
            this.LBLEVT_Booked.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_Booked.TabIndex = 130;
            this.LBLEVT_Booked.Tag = "1";
            this.LBLEVT_Booked.Text = "label3";
            // 
            // CHKEVT_Booked
            // 
            this.CHKEVT_Booked.AutoSize = true;
            this.CHKEVT_Booked.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKEVT_Booked.Location = new System.Drawing.Point(129, 174);
            this.CHKEVT_Booked.Name = "CHKEVT_Booked";
            this.CHKEVT_Booked.Size = new System.Drawing.Size(15, 14);
            this.CHKEVT_Booked.TabIndex = 7;
            this.CHKEVT_Booked.Tag = "";
            this.CHKEVT_Booked.UseVisualStyleBackColor = true;
            // 
            // LBLEVT_Attended
            // 
            this.LBLEVT_Attended.AutoSize = true;
            this.LBLEVT_Attended.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_Attended.Location = new System.Drawing.Point(12, 202);
            this.LBLEVT_Attended.Name = "LBLEVT_Attended";
            this.LBLEVT_Attended.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_Attended.TabIndex = 132;
            this.LBLEVT_Attended.Tag = "1";
            this.LBLEVT_Attended.Text = "label3";
            // 
            // CHKEVT_Attended
            // 
            this.CHKEVT_Attended.AutoSize = true;
            this.CHKEVT_Attended.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKEVT_Attended.Location = new System.Drawing.Point(129, 204);
            this.CHKEVT_Attended.Name = "CHKEVT_Attended";
            this.CHKEVT_Attended.Size = new System.Drawing.Size(15, 14);
            this.CHKEVT_Attended.TabIndex = 8;
            this.CHKEVT_Attended.Tag = "";
            this.CHKEVT_Attended.UseVisualStyleBackColor = true;
            // 
            // TXTEVT_Details
            // 
            this.TXTEVT_Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTEVT_Details.Location = new System.Drawing.Point(14, 307);
            this.TXTEVT_Details.Multiline = true;
            this.TXTEVT_Details.Name = "TXTEVT_Details";
            this.TXTEVT_Details.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TXTEVT_Details.Size = new System.Drawing.Size(720, 153);
            this.TXTEVT_Details.TabIndex = 10;
            this.TXTEVT_Details.Tag = "1";
            // 
            // LBLEVT_Details
            // 
            this.LBLEVT_Details.AutoSize = true;
            this.LBLEVT_Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_Details.Location = new System.Drawing.Point(12, 280);
            this.LBLEVT_Details.Name = "LBLEVT_Details";
            this.LBLEVT_Details.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_Details.TabIndex = 133;
            this.LBLEVT_Details.Text = "label3";
            // 
            // LBLEVT_Contact
            // 
            this.LBLEVT_Contact.AutoSize = true;
            this.LBLEVT_Contact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_Contact.Location = new System.Drawing.Point(12, 229);
            this.LBLEVT_Contact.Name = "LBLEVT_Contact";
            this.LBLEVT_Contact.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_Contact.TabIndex = 135;
            this.LBLEVT_Contact.Tag = "1";
            this.LBLEVT_Contact.Text = "label3";
            // 
            // TXTEVT_Contact
            // 
            this.TXTEVT_Contact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTEVT_Contact.Location = new System.Drawing.Point(129, 228);
            this.TXTEVT_Contact.Name = "TXTEVT_Contact";
            this.TXTEVT_Contact.Size = new System.Drawing.Size(188, 23);
            this.TXTEVT_Contact.TabIndex = 9;
            // 
            // BTNDelete
            // 
            this.BTNDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNDelete.Location = new System.Drawing.Point(369, 651);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(85, 34);
            this.BTNDelete.TabIndex = 14;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNSave.Location = new System.Drawing.Point(15, 651);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(85, 34);
            this.BTNSave.TabIndex = 12;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNUndo.Location = new System.Drawing.Point(229, 651);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(85, 34);
            this.BTNUndo.TabIndex = 13;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // CMBEVT_Website
            // 
            this.CMBEVT_Website.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBEVT_Website.FormattingEnabled = true;
            this.CMBEVT_Website.Location = new System.Drawing.Point(460, 110);
            this.CMBEVT_Website.Name = "CMBEVT_Website";
            this.CMBEVT_Website.Size = new System.Drawing.Size(159, 24);
            this.CMBEVT_Website.Sorted = true;
            this.CMBEVT_Website.TabIndex = 5;
            this.CMBEVT_Website.Tag = "";
            // 
            // LBLEVT_Website
            // 
            this.LBLEVT_Website.AutoSize = true;
            this.LBLEVT_Website.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_Website.Location = new System.Drawing.Point(381, 113);
            this.LBLEVT_Website.Name = "LBLEVT_Website";
            this.LBLEVT_Website.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_Website.TabIndex = 140;
            this.LBLEVT_Website.Text = "label3";
            // 
            // TXTEVT_Comments
            // 
            this.TXTEVT_Comments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTEVT_Comments.Location = new System.Drawing.Point(14, 495);
            this.TXTEVT_Comments.Multiline = true;
            this.TXTEVT_Comments.Name = "TXTEVT_Comments";
            this.TXTEVT_Comments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TXTEVT_Comments.Size = new System.Drawing.Size(720, 153);
            this.TXTEVT_Comments.TabIndex = 11;
            this.TXTEVT_Comments.Tag = "0";
            // 
            // LBLEVT_Comments
            // 
            this.LBLEVT_Comments.AutoSize = true;
            this.LBLEVT_Comments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLEVT_Comments.Location = new System.Drawing.Point(12, 468);
            this.LBLEVT_Comments.Name = "LBLEVT_Comments";
            this.LBLEVT_Comments.Size = new System.Drawing.Size(46, 17);
            this.LBLEVT_Comments.TabIndex = 142;
            this.LBLEVT_Comments.Text = "label3";
            // 
            // frmEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 689);
            this.Controls.Add(this.TXTEVT_Comments);
            this.Controls.Add(this.LBLEVT_Comments);
            this.Controls.Add(this.CMBEVT_Website);
            this.Controls.Add(this.LBLEVT_Website);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNSave);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.TXTEVT_Contact);
            this.Controls.Add(this.LBLEVT_Contact);
            this.Controls.Add(this.TXTEVT_Details);
            this.Controls.Add(this.LBLEVT_Details);
            this.Controls.Add(this.LBLEVT_Attended);
            this.Controls.Add(this.CHKEVT_Attended);
            this.Controls.Add(this.LBLEVT_Booked);
            this.Controls.Add(this.CHKEVT_Booked);
            this.Controls.Add(this.CMBEVT_Where);
            this.Controls.Add(this.LBLEVT_Where);
            this.Controls.Add(this.DTEEVT_Date);
            this.Controls.Add(this.LBLEVT_Date);
            this.Controls.Add(this.CMBEVT_Name);
            this.Controls.Add(this.LBLEVT_Name);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.LBLStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BTNNew);
            this.Controls.Add(this.CMBEVTID);
            this.Controls.Add(this.LBLEVT_ID);
            this.Controls.Add(this.PANTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEvents";
            this.Text = "Form_Events";
            this.Load += new System.EventHandler(this.frmEvents_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmEvents_Paint);
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel PANTitle;
        private System.Windows.Forms.Label LBLTitle;
        private System.Windows.Forms.Button BTNFind;
        private System.Windows.Forms.Label LBLStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.ComboBox CMBEVTID;
        private System.Windows.Forms.Label LBLEVT_ID;
        private System.Windows.Forms.ComboBox CMBEVT_Name;
        private System.Windows.Forms.Label LBLEVT_Name;
        private System.Windows.Forms.DateTimePicker DTEEVT_Date;
        private System.Windows.Forms.Label LBLEVT_Date;
        private System.Windows.Forms.ComboBox CMBEVT_Where;
        private System.Windows.Forms.Label LBLEVT_Where;
        private System.Windows.Forms.Label LBLEVT_Booked;
        private System.Windows.Forms.CheckBox CHKEVT_Booked;
        private System.Windows.Forms.Label LBLEVT_Attended;
        private System.Windows.Forms.CheckBox CHKEVT_Attended;
        private System.Windows.Forms.TextBox TXTEVT_Details;
        private System.Windows.Forms.Label LBLEVT_Details;
        private System.Windows.Forms.Label LBLEVT_Contact;
        private System.Windows.Forms.TextBox TXTEVT_Contact;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.ComboBox CMBEVT_Website;
        private System.Windows.Forms.Label LBLEVT_Website;
        private System.Windows.Forms.TextBox TXTEVT_Comments;
        private System.Windows.Forms.Label LBLEVT_Comments;
        private System.Windows.Forms.Button BTNClose;
    }
}