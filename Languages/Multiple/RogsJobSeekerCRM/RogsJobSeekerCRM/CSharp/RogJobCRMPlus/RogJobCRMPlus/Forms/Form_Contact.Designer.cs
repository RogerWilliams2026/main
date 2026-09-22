namespace RogJobCRMPlus.Forms
{
    partial class frmContact
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContact));
            this.BTNFind = new System.Windows.Forms.Button();
            this.LBLStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.CMBCNTID = new System.Windows.Forms.ComboBox();
            this.LBLCNT_ID = new System.Windows.Forms.Label();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.CMBCNT_Company = new System.Windows.Forms.ComboBox();
            this.LBLCNT_Company = new System.Windows.Forms.Label();
            this.LBLCNT_Contact = new System.Windows.Forms.Label();
            this.TXTCNT_PhoneNumber = new System.Windows.Forms.TextBox();
            this.LBLCNT_PhoneNumber = new System.Windows.Forms.Label();
            this.CMBCNT_Subject = new System.Windows.Forms.ComboBox();
            this.LBLCNT_Subject = new System.Windows.Forms.Label();
            this.TXTCNT_Comments = new System.Windows.Forms.TextBox();
            this.LBLCNT_Comments = new System.Windows.Forms.Label();
            this.DTECNT_Date = new System.Windows.Forms.DateTimePicker();
            this.LBLCNT_Date = new System.Windows.Forms.Label();
            this.TXTCNT_Contact = new System.Windows.Forms.TextBox();
            this.TXTCNT_Email = new System.Windows.Forms.TextBox();
            this.LBLCNT_Email = new System.Windows.Forms.Label();
            this.CMBCNT_Status = new System.Windows.Forms.ComboBox();
            this.LBLCNT_Status = new System.Windows.Forms.Label();
            this.BTNClose = new System.Windows.Forms.Button();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTNFind
            // 
            this.BTNFind.Location = new System.Drawing.Point(291, 49);
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
            this.LBLStatus.Location = new System.Drawing.Point(461, 52);
            this.LBLStatus.Name = "LBLStatus";
            this.LBLStatus.Size = new System.Drawing.Size(0, 13);
            this.LBLStatus.TabIndex = 129;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(376, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 128;
            this.label2.Text = "Record Status:";
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(203, 49);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(50, 20);
            this.BTNNew.TabIndex = 1;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // CMBCNTID
            // 
            this.CMBCNTID.FormattingEnabled = true;
            this.CMBCNTID.Location = new System.Drawing.Point(128, 48);
            this.CMBCNTID.Name = "CMBCNTID";
            this.CMBCNTID.Size = new System.Drawing.Size(57, 21);
            this.CMBCNTID.TabIndex = 0;
            this.CMBCNTID.SelectedValueChanged += new System.EventHandler(this.CMBCNTID_SelectedValueChanged);
            // 
            // LBLCNT_ID
            // 
            this.LBLCNT_ID.AutoSize = true;
            this.LBLCNT_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLCNT_ID.Location = new System.Drawing.Point(12, 52);
            this.LBLCNT_ID.Name = "LBLCNT_ID";
            this.LBLCNT_ID.Size = new System.Drawing.Size(46, 17);
            this.LBLCNT_ID.TabIndex = 125;
            this.LBLCNT_ID.Text = "label1";
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(2, 2);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(745, 34);
            this.PANTitle.TabIndex = 123;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
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
            // BTNDelete
            // 
            this.BTNDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNDelete.Location = new System.Drawing.Point(369, 459);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(85, 34);
            this.BTNDelete.TabIndex = 13;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNSave.Location = new System.Drawing.Point(15, 459);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(85, 34);
            this.BTNSave.TabIndex = 11;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // BTNUndo
            // 
            this.BTNUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNUndo.Location = new System.Drawing.Point(229, 459);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(85, 34);
            this.BTNUndo.TabIndex = 12;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // CMBCNT_Company
            // 
            this.CMBCNT_Company.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBCNT_Company.FormattingEnabled = true;
            this.CMBCNT_Company.Location = new System.Drawing.Point(128, 107);
            this.CMBCNT_Company.Name = "CMBCNT_Company";
            this.CMBCNT_Company.Size = new System.Drawing.Size(218, 24);
            this.CMBCNT_Company.Sorted = true;
            this.CMBCNT_Company.TabIndex = 4;
            this.CMBCNT_Company.Tag = "1";
            // 
            // LBLCNT_Company
            // 
            this.LBLCNT_Company.AutoSize = true;
            this.LBLCNT_Company.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLCNT_Company.Location = new System.Drawing.Point(12, 110);
            this.LBLCNT_Company.Name = "LBLCNT_Company";
            this.LBLCNT_Company.Size = new System.Drawing.Size(46, 17);
            this.LBLCNT_Company.TabIndex = 143;
            this.LBLCNT_Company.Text = "label3";
            // 
            // LBLCNT_Contact
            // 
            this.LBLCNT_Contact.AutoSize = true;
            this.LBLCNT_Contact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLCNT_Contact.Location = new System.Drawing.Point(12, 135);
            this.LBLCNT_Contact.Name = "LBLCNT_Contact";
            this.LBLCNT_Contact.Size = new System.Drawing.Size(46, 17);
            this.LBLCNT_Contact.TabIndex = 145;
            this.LBLCNT_Contact.Text = "label3";
            // 
            // TXTCNT_PhoneNumber
            // 
            this.TXTCNT_PhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTCNT_PhoneNumber.Location = new System.Drawing.Point(128, 163);
            this.TXTCNT_PhoneNumber.Name = "TXTCNT_PhoneNumber";
            this.TXTCNT_PhoneNumber.Size = new System.Drawing.Size(188, 23);
            this.TXTCNT_PhoneNumber.TabIndex = 6;
            // 
            // LBLCNT_PhoneNumber
            // 
            this.LBLCNT_PhoneNumber.AutoSize = true;
            this.LBLCNT_PhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLCNT_PhoneNumber.Location = new System.Drawing.Point(12, 164);
            this.LBLCNT_PhoneNumber.Name = "LBLCNT_PhoneNumber";
            this.LBLCNT_PhoneNumber.Size = new System.Drawing.Size(46, 17);
            this.LBLCNT_PhoneNumber.TabIndex = 147;
            this.LBLCNT_PhoneNumber.Tag = "1";
            this.LBLCNT_PhoneNumber.Text = "label3";
            // 
            // CMBCNT_Subject
            // 
            this.CMBCNT_Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBCNT_Subject.FormattingEnabled = true;
            this.CMBCNT_Subject.Location = new System.Drawing.Point(128, 217);
            this.CMBCNT_Subject.Name = "CMBCNT_Subject";
            this.CMBCNT_Subject.Size = new System.Drawing.Size(218, 24);
            this.CMBCNT_Subject.Sorted = true;
            this.CMBCNT_Subject.TabIndex = 8;
            this.CMBCNT_Subject.Tag = "1";
            // 
            // LBLCNT_Subject
            // 
            this.LBLCNT_Subject.AutoSize = true;
            this.LBLCNT_Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLCNT_Subject.Location = new System.Drawing.Point(12, 220);
            this.LBLCNT_Subject.Name = "LBLCNT_Subject";
            this.LBLCNT_Subject.Size = new System.Drawing.Size(46, 17);
            this.LBLCNT_Subject.TabIndex = 149;
            this.LBLCNT_Subject.Text = "label3";
            // 
            // TXTCNT_Comments
            // 
            this.TXTCNT_Comments.Location = new System.Drawing.Point(14, 300);
            this.TXTCNT_Comments.Multiline = true;
            this.TXTCNT_Comments.Name = "TXTCNT_Comments";
            this.TXTCNT_Comments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TXTCNT_Comments.Size = new System.Drawing.Size(720, 153);
            this.TXTCNT_Comments.TabIndex = 10;
            this.TXTCNT_Comments.Tag = "1";
            // 
            // LBLCNT_Comments
            // 
            this.LBLCNT_Comments.AutoSize = true;
            this.LBLCNT_Comments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLCNT_Comments.Location = new System.Drawing.Point(13, 277);
            this.LBLCNT_Comments.Name = "LBLCNT_Comments";
            this.LBLCNT_Comments.Size = new System.Drawing.Size(46, 17);
            this.LBLCNT_Comments.TabIndex = 151;
            this.LBLCNT_Comments.Text = "label3";
            // 
            // DTECNT_Date
            // 
            this.DTECNT_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTECNT_Date.Location = new System.Drawing.Point(128, 79);
            this.DTECNT_Date.Name = "DTECNT_Date";
            this.DTECNT_Date.Size = new System.Drawing.Size(140, 23);
            this.DTECNT_Date.TabIndex = 3;
            this.DTECNT_Date.Tag = "1";
            // 
            // LBLCNT_Date
            // 
            this.LBLCNT_Date.AutoSize = true;
            this.LBLCNT_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLCNT_Date.Location = new System.Drawing.Point(13, 83);
            this.LBLCNT_Date.Name = "LBLCNT_Date";
            this.LBLCNT_Date.Size = new System.Drawing.Size(46, 17);
            this.LBLCNT_Date.TabIndex = 153;
            this.LBLCNT_Date.Tag = "1";
            this.LBLCNT_Date.Text = "label3";
            // 
            // TXTCNT_Contact
            // 
            this.TXTCNT_Contact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTCNT_Contact.Location = new System.Drawing.Point(128, 135);
            this.TXTCNT_Contact.Name = "TXTCNT_Contact";
            this.TXTCNT_Contact.Size = new System.Drawing.Size(188, 23);
            this.TXTCNT_Contact.TabIndex = 5;
            // 
            // TXTCNT_Email
            // 
            this.TXTCNT_Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTCNT_Email.Location = new System.Drawing.Point(128, 191);
            this.TXTCNT_Email.Name = "TXTCNT_Email";
            this.TXTCNT_Email.Size = new System.Drawing.Size(188, 23);
            this.TXTCNT_Email.TabIndex = 7;
            // 
            // LBLCNT_Email
            // 
            this.LBLCNT_Email.AutoSize = true;
            this.LBLCNT_Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLCNT_Email.Location = new System.Drawing.Point(12, 192);
            this.LBLCNT_Email.Name = "LBLCNT_Email";
            this.LBLCNT_Email.Size = new System.Drawing.Size(46, 17);
            this.LBLCNT_Email.TabIndex = 156;
            this.LBLCNT_Email.Tag = "1";
            this.LBLCNT_Email.Text = "label3";
            // 
            // CMBCNT_Status
            // 
            this.CMBCNT_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBCNT_Status.FormattingEnabled = true;
            this.CMBCNT_Status.Location = new System.Drawing.Point(128, 246);
            this.CMBCNT_Status.Name = "CMBCNT_Status";
            this.CMBCNT_Status.Size = new System.Drawing.Size(159, 24);
            this.CMBCNT_Status.Sorted = true;
            this.CMBCNT_Status.TabIndex = 9;
            this.CMBCNT_Status.Tag = "1";
            // 
            // LBLCNT_Status
            // 
            this.LBLCNT_Status.AutoSize = true;
            this.LBLCNT_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLCNT_Status.Location = new System.Drawing.Point(13, 249);
            this.LBLCNT_Status.Name = "LBLCNT_Status";
            this.LBLCNT_Status.Size = new System.Drawing.Size(46, 17);
            this.LBLCNT_Status.TabIndex = 158;
            this.LBLCNT_Status.Text = "label3";
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(720, 5);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 125;
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
            // 
            // frmContact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 502);
            this.Controls.Add(this.CMBCNT_Status);
            this.Controls.Add(this.LBLCNT_Status);
            this.Controls.Add(this.TXTCNT_Email);
            this.Controls.Add(this.LBLCNT_Email);
            this.Controls.Add(this.TXTCNT_Contact);
            this.Controls.Add(this.DTECNT_Date);
            this.Controls.Add(this.LBLCNT_Date);
            this.Controls.Add(this.TXTCNT_Comments);
            this.Controls.Add(this.LBLCNT_Comments);
            this.Controls.Add(this.CMBCNT_Subject);
            this.Controls.Add(this.LBLCNT_Subject);
            this.Controls.Add(this.TXTCNT_PhoneNumber);
            this.Controls.Add(this.LBLCNT_PhoneNumber);
            this.Controls.Add(this.LBLCNT_Contact);
            this.Controls.Add(this.CMBCNT_Company);
            this.Controls.Add(this.LBLCNT_Company);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNSave);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.LBLStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BTNNew);
            this.Controls.Add(this.CMBCNTID);
            this.Controls.Add(this.LBLCNT_ID);
            this.Controls.Add(this.PANTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmContact";
            this.Text = "Form_Contact";
            this.Load += new System.EventHandler(this.frmContact_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmContact_Paint);
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTNFind;
        private System.Windows.Forms.Label LBLStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.ComboBox CMBCNTID;
        private System.Windows.Forms.Label LBLCNT_ID;
        private System.Windows.Forms.Panel PANTitle;
        private System.Windows.Forms.Label LBLTitle;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.ComboBox CMBCNT_Company;
        private System.Windows.Forms.Label LBLCNT_Company;
        private System.Windows.Forms.Label LBLCNT_Contact;
        private System.Windows.Forms.TextBox TXTCNT_PhoneNumber;
        private System.Windows.Forms.Label LBLCNT_PhoneNumber;
        private System.Windows.Forms.ComboBox CMBCNT_Subject;
        private System.Windows.Forms.Label LBLCNT_Subject;
        private System.Windows.Forms.TextBox TXTCNT_Comments;
        private System.Windows.Forms.Label LBLCNT_Comments;
        private System.Windows.Forms.DateTimePicker DTECNT_Date;
        private System.Windows.Forms.Label LBLCNT_Date;
        private System.Windows.Forms.TextBox TXTCNT_Contact;
        private System.Windows.Forms.TextBox TXTCNT_Email;
        private System.Windows.Forms.Label LBLCNT_Email;
        private System.Windows.Forms.ComboBox CMBCNT_Status;
        private System.Windows.Forms.Label LBLCNT_Status;
        private System.Windows.Forms.Button BTNClose;
    }
}