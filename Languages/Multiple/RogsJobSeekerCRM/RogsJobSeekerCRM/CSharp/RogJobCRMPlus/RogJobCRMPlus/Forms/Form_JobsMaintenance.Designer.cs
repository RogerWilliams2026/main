namespace RogJobCRMPlus.Forms
{
    partial class frmJobsMaintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJobsMaintenance));
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.LBLJOB_ID = new System.Windows.Forms.Label();
            this.CMBJobID = new System.Windows.Forms.ComboBox();
            this.BTNNew = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.LBLStatus = new System.Windows.Forms.Label();
            this.LBLJOB_DateApplied = new System.Windows.Forms.Label();
            this.DTEJOB_DateApplied = new System.Windows.Forms.DateTimePicker();
            this.LBLJOB_Company = new System.Windows.Forms.Label();
            this.CMBJOB_Company = new System.Windows.Forms.ComboBox();
            this.CMBJOB_TownCity = new System.Windows.Forms.ComboBox();
            this.LBLJOB_TownCity = new System.Windows.Forms.Label();
            this.CMBJOB_Sector = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Sector = new System.Windows.Forms.Label();
            this.CMBJOB_Salary = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Salary = new System.Windows.Forms.Label();
            this.CMBJOB_Title = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Title = new System.Windows.Forms.Label();
            this.CMBJOB_Type = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Type = new System.Windows.Forms.Label();
            this.CMBJOB_Hours = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Hours = new System.Windows.Forms.Label();
            this.CMBJOB_Where = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Where = new System.Windows.Forms.Label();
            this.CHKJOB_Direct = new System.Windows.Forms.CheckBox();
            this.LBLJOB_Direct = new System.Windows.Forms.Label();
            this.CMBJOB_Status = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Status = new System.Windows.Forms.Label();
            this.LBLJOB_Details = new System.Windows.Forms.Label();
            this.TXTJOB_Details = new System.Windows.Forms.TextBox();
            this.BTNFind = new System.Windows.Forms.Button();
            this.TXTJOB_Comments = new System.Windows.Forms.TextBox();
            this.LBLJOB_Comments = new System.Windows.Forms.Label();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(0, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(1095, 34);
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
            this.BTNClose.Location = new System.Drawing.Point(1069, 6);
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
            // BTNUndo
            // 
            this.BTNUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNUndo.Location = new System.Drawing.Point(226, 736);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(85, 34);
            this.BTNUndo.TabIndex = 17;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // BTNSave
            // 
            this.BTNSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNSave.Location = new System.Drawing.Point(19, 736);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(85, 34);
            this.BTNSave.TabIndex = 16;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // BTNDelete
            // 
            this.BTNDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNDelete.Location = new System.Drawing.Point(366, 736);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(85, 34);
            this.BTNDelete.TabIndex = 18;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            this.BTNDelete.Click += new System.EventHandler(this.BTNDelete_Click);
            // 
            // LBLJOB_ID
            // 
            this.LBLJOB_ID.AutoSize = true;
            this.LBLJOB_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_ID.Location = new System.Drawing.Point(18, 52);
            this.LBLJOB_ID.Name = "LBLJOB_ID";
            this.LBLJOB_ID.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_ID.TabIndex = 85;
            this.LBLJOB_ID.Text = "label1";
            // 
            // CMBJobID
            // 
            this.CMBJobID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJobID.FormattingEnabled = true;
            this.CMBJobID.Location = new System.Drawing.Point(162, 45);
            this.CMBJobID.Name = "CMBJobID";
            this.CMBJobID.Size = new System.Drawing.Size(57, 24);
            this.CMBJobID.TabIndex = 0;
            this.CMBJobID.SelectedValueChanged += new System.EventHandler(this.CMBJobID_SelectedValueChanged);
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(237, 46);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(50, 20);
            this.BTNNew.TabIndex = 1;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            this.BTNNew.Click += new System.EventHandler(this.BTNNew_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(410, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 88;
            this.label2.Text = "Record Status:";
            // 
            // LBLStatus
            // 
            this.LBLStatus.AutoSize = true;
            this.LBLStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLStatus.Location = new System.Drawing.Point(495, 49);
            this.LBLStatus.Name = "LBLStatus";
            this.LBLStatus.Size = new System.Drawing.Size(0, 17);
            this.LBLStatus.TabIndex = 89;
            // 
            // LBLJOB_DateApplied
            // 
            this.LBLJOB_DateApplied.AutoSize = true;
            this.LBLJOB_DateApplied.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_DateApplied.Location = new System.Drawing.Point(18, 85);
            this.LBLJOB_DateApplied.Name = "LBLJOB_DateApplied";
            this.LBLJOB_DateApplied.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_DateApplied.TabIndex = 90;
            this.LBLJOB_DateApplied.Tag = "1";
            this.LBLJOB_DateApplied.Text = "label3";
            // 
            // DTEJOB_DateApplied
            // 
            this.DTEJOB_DateApplied.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTEJOB_DateApplied.Location = new System.Drawing.Point(163, 81);
            this.DTEJOB_DateApplied.Name = "DTEJOB_DateApplied";
            this.DTEJOB_DateApplied.Size = new System.Drawing.Size(140, 23);
            this.DTEJOB_DateApplied.TabIndex = 3;
            this.DTEJOB_DateApplied.Tag = "1";
            // 
            // LBLJOB_Company
            // 
            this.LBLJOB_Company.AutoSize = true;
            this.LBLJOB_Company.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Company.Location = new System.Drawing.Point(19, 114);
            this.LBLJOB_Company.Name = "LBLJOB_Company";
            this.LBLJOB_Company.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Company.TabIndex = 93;
            this.LBLJOB_Company.Tag = "1";
            this.LBLJOB_Company.Text = "label3";
            // 
            // CMBJOB_Company
            // 
            this.CMBJOB_Company.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Company.FormattingEnabled = true;
            this.CMBJOB_Company.Location = new System.Drawing.Point(162, 111);
            this.CMBJOB_Company.Name = "CMBJOB_Company";
            this.CMBJOB_Company.Size = new System.Drawing.Size(312, 24);
            this.CMBJOB_Company.TabIndex = 4;
            this.CMBJOB_Company.Tag = "1";
            // 
            // CMBJOB_TownCity
            // 
            this.CMBJOB_TownCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_TownCity.FormattingEnabled = true;
            this.CMBJOB_TownCity.Location = new System.Drawing.Point(162, 191);
            this.CMBJOB_TownCity.Name = "CMBJOB_TownCity";
            this.CMBJOB_TownCity.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_TownCity.Sorted = true;
            this.CMBJOB_TownCity.TabIndex = 8;
            this.CMBJOB_TownCity.Tag = "1";
            // 
            // LBLJOB_TownCity
            // 
            this.LBLJOB_TownCity.AutoSize = true;
            this.LBLJOB_TownCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_TownCity.Location = new System.Drawing.Point(19, 194);
            this.LBLJOB_TownCity.Name = "LBLJOB_TownCity";
            this.LBLJOB_TownCity.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_TownCity.TabIndex = 95;
            this.LBLJOB_TownCity.Text = "label3";
            // 
            // CMBJOB_Sector
            // 
            this.CMBJOB_Sector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Sector.FormattingEnabled = true;
            this.CMBJOB_Sector.Location = new System.Drawing.Point(162, 164);
            this.CMBJOB_Sector.Name = "CMBJOB_Sector";
            this.CMBJOB_Sector.Size = new System.Drawing.Size(312, 24);
            this.CMBJOB_Sector.TabIndex = 7;
            this.CMBJOB_Sector.Tag = "1";
            // 
            // LBLJOB_Sector
            // 
            this.LBLJOB_Sector.AutoSize = true;
            this.LBLJOB_Sector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Sector.Location = new System.Drawing.Point(19, 167);
            this.LBLJOB_Sector.Name = "LBLJOB_Sector";
            this.LBLJOB_Sector.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Sector.TabIndex = 97;
            this.LBLJOB_Sector.Text = "label3";
            // 
            // CMBJOB_Salary
            // 
            this.CMBJOB_Salary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Salary.FormattingEnabled = true;
            this.CMBJOB_Salary.Location = new System.Drawing.Point(162, 218);
            this.CMBJOB_Salary.Name = "CMBJOB_Salary";
            this.CMBJOB_Salary.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Salary.Sorted = true;
            this.CMBJOB_Salary.TabIndex = 9;
            this.CMBJOB_Salary.Tag = "1";
            // 
            // LBLJOB_Salary
            // 
            this.LBLJOB_Salary.AutoSize = true;
            this.LBLJOB_Salary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Salary.Location = new System.Drawing.Point(19, 221);
            this.LBLJOB_Salary.Name = "LBLJOB_Salary";
            this.LBLJOB_Salary.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Salary.TabIndex = 99;
            this.LBLJOB_Salary.Text = "label3";
            // 
            // CMBJOB_Title
            // 
            this.CMBJOB_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Title.FormattingEnabled = true;
            this.CMBJOB_Title.Location = new System.Drawing.Point(162, 138);
            this.CMBJOB_Title.Name = "CMBJOB_Title";
            this.CMBJOB_Title.Size = new System.Drawing.Size(312, 24);
            this.CMBJOB_Title.Sorted = true;
            this.CMBJOB_Title.TabIndex = 6;
            this.CMBJOB_Title.Tag = "1";
            // 
            // LBLJOB_Title
            // 
            this.LBLJOB_Title.AutoSize = true;
            this.LBLJOB_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Title.Location = new System.Drawing.Point(19, 141);
            this.LBLJOB_Title.Name = "LBLJOB_Title";
            this.LBLJOB_Title.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Title.TabIndex = 101;
            this.LBLJOB_Title.Text = "label3";
            // 
            // CMBJOB_Type
            // 
            this.CMBJOB_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Type.FormattingEnabled = true;
            this.CMBJOB_Type.Location = new System.Drawing.Point(162, 245);
            this.CMBJOB_Type.Name = "CMBJOB_Type";
            this.CMBJOB_Type.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Type.Sorted = true;
            this.CMBJOB_Type.TabIndex = 10;
            this.CMBJOB_Type.Tag = "1";
            // 
            // LBLJOB_Type
            // 
            this.LBLJOB_Type.AutoSize = true;
            this.LBLJOB_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Type.Location = new System.Drawing.Point(19, 248);
            this.LBLJOB_Type.Name = "LBLJOB_Type";
            this.LBLJOB_Type.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Type.TabIndex = 103;
            this.LBLJOB_Type.Text = "label3";
            // 
            // CMBJOB_Hours
            // 
            this.CMBJOB_Hours.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Hours.FormattingEnabled = true;
            this.CMBJOB_Hours.Location = new System.Drawing.Point(162, 272);
            this.CMBJOB_Hours.Name = "CMBJOB_Hours";
            this.CMBJOB_Hours.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Hours.Sorted = true;
            this.CMBJOB_Hours.TabIndex = 11;
            this.CMBJOB_Hours.Tag = "1";
            // 
            // LBLJOB_Hours
            // 
            this.LBLJOB_Hours.AutoSize = true;
            this.LBLJOB_Hours.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Hours.Location = new System.Drawing.Point(19, 275);
            this.LBLJOB_Hours.Name = "LBLJOB_Hours";
            this.LBLJOB_Hours.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Hours.TabIndex = 105;
            this.LBLJOB_Hours.Text = "label3";
            // 
            // CMBJOB_Where
            // 
            this.CMBJOB_Where.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Where.FormattingEnabled = true;
            this.CMBJOB_Where.Location = new System.Drawing.Point(162, 299);
            this.CMBJOB_Where.Name = "CMBJOB_Where";
            this.CMBJOB_Where.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Where.Sorted = true;
            this.CMBJOB_Where.TabIndex = 12;
            this.CMBJOB_Where.Tag = "1";
            // 
            // LBLJOB_Where
            // 
            this.LBLJOB_Where.AutoSize = true;
            this.LBLJOB_Where.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Where.Location = new System.Drawing.Point(19, 302);
            this.LBLJOB_Where.Name = "LBLJOB_Where";
            this.LBLJOB_Where.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Where.TabIndex = 107;
            this.LBLJOB_Where.Text = "label3";
            // 
            // CHKJOB_Direct
            // 
            this.CHKJOB_Direct.AutoSize = true;
            this.CHKJOB_Direct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_Direct.Location = new System.Drawing.Point(638, 116);
            this.CHKJOB_Direct.Name = "CHKJOB_Direct";
            this.CHKJOB_Direct.Size = new System.Drawing.Size(15, 14);
            this.CHKJOB_Direct.TabIndex = 5;
            this.CHKJOB_Direct.Tag = "";
            this.CHKJOB_Direct.UseVisualStyleBackColor = true;
            // 
            // LBLJOB_Direct
            // 
            this.LBLJOB_Direct.AutoSize = true;
            this.LBLJOB_Direct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Direct.Location = new System.Drawing.Point(488, 116);
            this.LBLJOB_Direct.Name = "LBLJOB_Direct";
            this.LBLJOB_Direct.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Direct.TabIndex = 111;
            this.LBLJOB_Direct.Tag = "1";
            this.LBLJOB_Direct.Text = "label3";
            // 
            // CMBJOB_Status
            // 
            this.CMBJOB_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Status.FormattingEnabled = true;
            this.CMBJOB_Status.Location = new System.Drawing.Point(162, 327);
            this.CMBJOB_Status.Name = "CMBJOB_Status";
            this.CMBJOB_Status.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Status.Sorted = true;
            this.CMBJOB_Status.TabIndex = 13;
            this.CMBJOB_Status.Tag = "1";
            // 
            // LBLJOB_Status
            // 
            this.LBLJOB_Status.AutoSize = true;
            this.LBLJOB_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Status.Location = new System.Drawing.Point(19, 330);
            this.LBLJOB_Status.Name = "LBLJOB_Status";
            this.LBLJOB_Status.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Status.TabIndex = 112;
            this.LBLJOB_Status.Text = "label3";
            // 
            // LBLJOB_Details
            // 
            this.LBLJOB_Details.AutoSize = true;
            this.LBLJOB_Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Details.Location = new System.Drawing.Point(19, 358);
            this.LBLJOB_Details.Name = "LBLJOB_Details";
            this.LBLJOB_Details.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Details.TabIndex = 114;
            this.LBLJOB_Details.Text = "label3";
            // 
            // TXTJOB_Details
            // 
            this.TXTJOB_Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTJOB_Details.Location = new System.Drawing.Point(18, 385);
            this.TXTJOB_Details.Multiline = true;
            this.TXTJOB_Details.Name = "TXTJOB_Details";
            this.TXTJOB_Details.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TXTJOB_Details.Size = new System.Drawing.Size(1059, 153);
            this.TXTJOB_Details.TabIndex = 14;
            this.TXTJOB_Details.Tag = "1";
            // 
            // BTNFind
            // 
            this.BTNFind.Location = new System.Drawing.Point(325, 46);
            this.BTNFind.Name = "BTNFind";
            this.BTNFind.Size = new System.Drawing.Size(50, 20);
            this.BTNFind.TabIndex = 2;
            this.BTNFind.TabStop = false;
            this.BTNFind.Text = "Find";
            this.BTNFind.UseVisualStyleBackColor = true;
            this.BTNFind.Click += new System.EventHandler(this.BTNFind_Click);
            // 
            // TXTJOB_Comments
            // 
            this.TXTJOB_Comments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTJOB_Comments.Location = new System.Drawing.Point(19, 574);
            this.TXTJOB_Comments.Multiline = true;
            this.TXTJOB_Comments.Name = "TXTJOB_Comments";
            this.TXTJOB_Comments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TXTJOB_Comments.Size = new System.Drawing.Size(1059, 153);
            this.TXTJOB_Comments.TabIndex = 15;
            this.TXTJOB_Comments.Tag = "";
            // 
            // LBLJOB_Comments
            // 
            this.LBLJOB_Comments.AutoSize = true;
            this.LBLJOB_Comments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Comments.Location = new System.Drawing.Point(20, 547);
            this.LBLJOB_Comments.Name = "LBLJOB_Comments";
            this.LBLJOB_Comments.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Comments.TabIndex = 116;
            this.LBLJOB_Comments.Text = "label3";
            // 
            // frmJobsMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 780);
            this.Controls.Add(this.TXTJOB_Comments);
            this.Controls.Add(this.LBLJOB_Comments);
            this.Controls.Add(this.BTNFind);
            this.Controls.Add(this.TXTJOB_Details);
            this.Controls.Add(this.LBLJOB_Details);
            this.Controls.Add(this.CMBJOB_Status);
            this.Controls.Add(this.LBLJOB_Status);
            this.Controls.Add(this.LBLJOB_Direct);
            this.Controls.Add(this.CHKJOB_Direct);
            this.Controls.Add(this.CMBJOB_Where);
            this.Controls.Add(this.LBLJOB_Where);
            this.Controls.Add(this.CMBJOB_Hours);
            this.Controls.Add(this.LBLJOB_Hours);
            this.Controls.Add(this.CMBJOB_Type);
            this.Controls.Add(this.LBLJOB_Type);
            this.Controls.Add(this.CMBJOB_Title);
            this.Controls.Add(this.LBLJOB_Title);
            this.Controls.Add(this.CMBJOB_Salary);
            this.Controls.Add(this.LBLJOB_Salary);
            this.Controls.Add(this.CMBJOB_Sector);
            this.Controls.Add(this.LBLJOB_Sector);
            this.Controls.Add(this.CMBJOB_TownCity);
            this.Controls.Add(this.LBLJOB_TownCity);
            this.Controls.Add(this.CMBJOB_Company);
            this.Controls.Add(this.LBLJOB_Company);
            this.Controls.Add(this.DTEJOB_DateApplied);
            this.Controls.Add(this.LBLJOB_DateApplied);
            this.Controls.Add(this.LBLStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BTNNew);
            this.Controls.Add(this.CMBJobID);
            this.Controls.Add(this.LBLJOB_ID);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNSave);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.PANTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmJobsMaintenance";
            this.Text = "Form_JobsMaintenance";
            this.Load += new System.EventHandler(this.frmJobsMaintenance_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_JobsMaintenance_Paint);
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel PANTitle;
        private System.Windows.Forms.Label LBLTitle;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.Button BTNSave;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Label LBLJOB_ID;
        private System.Windows.Forms.ComboBox CMBJobID;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LBLStatus;
        private System.Windows.Forms.Label LBLJOB_DateApplied;
        private System.Windows.Forms.DateTimePicker DTEJOB_DateApplied;
        private System.Windows.Forms.Label LBLJOB_Company;
        private System.Windows.Forms.ComboBox CMBJOB_Company;
        private System.Windows.Forms.ComboBox CMBJOB_TownCity;
        private System.Windows.Forms.Label LBLJOB_TownCity;
        private System.Windows.Forms.ComboBox CMBJOB_Sector;
        private System.Windows.Forms.Label LBLJOB_Sector;
        private System.Windows.Forms.ComboBox CMBJOB_Salary;
        private System.Windows.Forms.Label LBLJOB_Salary;
        private System.Windows.Forms.ComboBox CMBJOB_Title;
        private System.Windows.Forms.Label LBLJOB_Title;
        private System.Windows.Forms.ComboBox CMBJOB_Type;
        private System.Windows.Forms.Label LBLJOB_Type;
        private System.Windows.Forms.ComboBox CMBJOB_Hours;
        private System.Windows.Forms.Label LBLJOB_Hours;
        private System.Windows.Forms.ComboBox CMBJOB_Where;
        private System.Windows.Forms.Label LBLJOB_Where;
        private System.Windows.Forms.CheckBox CHKJOB_Direct;
        private System.Windows.Forms.Label LBLJOB_Direct;
        private System.Windows.Forms.ComboBox CMBJOB_Status;
        private System.Windows.Forms.Label LBLJOB_Status;
        private System.Windows.Forms.Label LBLJOB_Details;
        private System.Windows.Forms.TextBox TXTJOB_Details;
        private System.Windows.Forms.Button BTNFind;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.TextBox TXTJOB_Comments;
        private System.Windows.Forms.Label LBLJOB_Comments;
    }
}