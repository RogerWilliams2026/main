namespace RogJobCRMPlus.Forms
{
    partial class frmJobsAppliedFor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJobsAppliedFor));
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.CMBJOB_Status = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Status = new System.Windows.Forms.Label();
            this.LBLJOB_Direct = new System.Windows.Forms.Label();
            this.CHKJOB_Direct = new System.Windows.Forms.CheckBox();
            this.CMBJOB_Where = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Where = new System.Windows.Forms.Label();
            this.CMBJOB_Hours = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Hours = new System.Windows.Forms.Label();
            this.CMBJOB_Type = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Type = new System.Windows.Forms.Label();
            this.CMBJOB_Title = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Title = new System.Windows.Forms.Label();
            this.CMBJOB_Salary = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Salary = new System.Windows.Forms.Label();
            this.CMBJOB_Sector = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Sector = new System.Windows.Forms.Label();
            this.CMBJOB_TownCity = new System.Windows.Forms.ComboBox();
            this.LBLJOB_TownCity = new System.Windows.Forms.Label();
            this.CMBJOB_Company = new System.Windows.Forms.ComboBox();
            this.LBLJOB_Company = new System.Windows.Forms.Label();
            this.DTEJOB_DateApplied = new System.Windows.Forms.DateTimePicker();
            this.LBLJOB_DateApplied = new System.Windows.Forms.Label();
            this.CMBJobID = new System.Windows.Forms.ComboBox();
            this.LBLJOB_ID = new System.Windows.Forms.Label();
            this.BTNPreview = new System.Windows.Forms.Button();
            this.BTNPrint = new System.Windows.Forms.Button();
            this.DTEJOB_DateAppliedTo = new System.Windows.Forms.DateTimePicker();
            this.LBLJOB_DateAppliedTo = new System.Windows.Forms.Label();
            this.CHKJOB_CompanyAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_DateAppliedAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_TownCityAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_SectorAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_TypeAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_HoursAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_WhereAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_StatusAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_TitleAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_SalaryAll = new System.Windows.Forms.CheckBox();
            this.CHKJOB_DirectAll = new System.Windows.Forms.CheckBox();
            this.CMBJOB_SalaryTo = new System.Windows.Forms.ComboBox();
            this.LBLJOB_SalaryTo = new System.Windows.Forms.Label();
            this.CHKSummary = new System.Windows.Forms.CheckBox();
            this.CHKSort = new System.Windows.Forms.CheckBox();
            this.CHKIExcludeApplied = new System.Windows.Forms.CheckBox();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(0, 1);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(632, 34);
            this.PANTitle.TabIndex = 81;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(605, 6);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 83;
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
            // CMBJOB_Status
            // 
            this.CMBJOB_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Status.FormattingEnabled = true;
            this.CMBJOB_Status.Location = new System.Drawing.Point(150, 379);
            this.CMBJOB_Status.Name = "CMBJOB_Status";
            this.CMBJOB_Status.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Status.Sorted = true;
            this.CMBJOB_Status.TabIndex = 124;
            this.CMBJOB_Status.Tag = "1";
            this.CMBJOB_Status.SelectedValueChanged += new System.EventHandler(this.CMBJOB_Status_SelectedValueChanged);
            // 
            // LBLJOB_Status
            // 
            this.LBLJOB_Status.AutoSize = true;
            this.LBLJOB_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Status.Location = new System.Drawing.Point(12, 382);
            this.LBLJOB_Status.Name = "LBLJOB_Status";
            this.LBLJOB_Status.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Status.TabIndex = 138;
            this.LBLJOB_Status.Text = "label3";
            // 
            // LBLJOB_Direct
            // 
            this.LBLJOB_Direct.AutoSize = true;
            this.LBLJOB_Direct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Direct.Location = new System.Drawing.Point(12, 171);
            this.LBLJOB_Direct.Name = "LBLJOB_Direct";
            this.LBLJOB_Direct.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Direct.TabIndex = 137;
            this.LBLJOB_Direct.Tag = "1";
            this.LBLJOB_Direct.Text = "label3";
            // 
            // CHKJOB_Direct
            // 
            this.CHKJOB_Direct.AutoSize = true;
            this.CHKJOB_Direct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_Direct.Location = new System.Drawing.Point(202, 172);
            this.CHKJOB_Direct.Name = "CHKJOB_Direct";
            this.CHKJOB_Direct.Size = new System.Drawing.Size(15, 14);
            this.CHKJOB_Direct.TabIndex = 118;
            this.CHKJOB_Direct.Tag = "";
            this.CHKJOB_Direct.UseVisualStyleBackColor = true;
            this.CHKJOB_Direct.CheckedChanged += new System.EventHandler(this.CHKJOB_Direct_CheckedChanged);
            // 
            // CMBJOB_Where
            // 
            this.CMBJOB_Where.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Where.FormattingEnabled = true;
            this.CMBJOB_Where.Location = new System.Drawing.Point(150, 320);
            this.CMBJOB_Where.Name = "CMBJOB_Where";
            this.CMBJOB_Where.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Where.Sorted = true;
            this.CMBJOB_Where.TabIndex = 123;
            this.CMBJOB_Where.Tag = "1";
            // 
            // LBLJOB_Where
            // 
            this.LBLJOB_Where.AutoSize = true;
            this.LBLJOB_Where.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Where.Location = new System.Drawing.Point(12, 323);
            this.LBLJOB_Where.Name = "LBLJOB_Where";
            this.LBLJOB_Where.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Where.TabIndex = 136;
            this.LBLJOB_Where.Text = "label3";
            // 
            // CMBJOB_Hours
            // 
            this.CMBJOB_Hours.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Hours.FormattingEnabled = true;
            this.CMBJOB_Hours.Location = new System.Drawing.Point(150, 293);
            this.CMBJOB_Hours.Name = "CMBJOB_Hours";
            this.CMBJOB_Hours.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Hours.Sorted = true;
            this.CMBJOB_Hours.TabIndex = 122;
            this.CMBJOB_Hours.Tag = "1";
            // 
            // LBLJOB_Hours
            // 
            this.LBLJOB_Hours.AutoSize = true;
            this.LBLJOB_Hours.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Hours.Location = new System.Drawing.Point(12, 296);
            this.LBLJOB_Hours.Name = "LBLJOB_Hours";
            this.LBLJOB_Hours.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Hours.TabIndex = 135;
            this.LBLJOB_Hours.Text = "label3";
            // 
            // CMBJOB_Type
            // 
            this.CMBJOB_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Type.FormattingEnabled = true;
            this.CMBJOB_Type.Location = new System.Drawing.Point(150, 266);
            this.CMBJOB_Type.Name = "CMBJOB_Type";
            this.CMBJOB_Type.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Type.Sorted = true;
            this.CMBJOB_Type.TabIndex = 121;
            this.CMBJOB_Type.Tag = "1";
            // 
            // LBLJOB_Type
            // 
            this.LBLJOB_Type.AutoSize = true;
            this.LBLJOB_Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Type.Location = new System.Drawing.Point(12, 269);
            this.LBLJOB_Type.Name = "LBLJOB_Type";
            this.LBLJOB_Type.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Type.TabIndex = 134;
            this.LBLJOB_Type.Text = "label3";
            // 
            // CMBJOB_Title
            // 
            this.CMBJOB_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Title.FormattingEnabled = true;
            this.CMBJOB_Title.Location = new System.Drawing.Point(151, 144);
            this.CMBJOB_Title.Name = "CMBJOB_Title";
            this.CMBJOB_Title.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Title.Sorted = true;
            this.CMBJOB_Title.TabIndex = 125;
            this.CMBJOB_Title.Tag = "1";
            // 
            // LBLJOB_Title
            // 
            this.LBLJOB_Title.AutoSize = true;
            this.LBLJOB_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Title.Location = new System.Drawing.Point(13, 146);
            this.LBLJOB_Title.Name = "LBLJOB_Title";
            this.LBLJOB_Title.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Title.TabIndex = 133;
            this.LBLJOB_Title.Text = "label3";
            // 
            // CMBJOB_Salary
            // 
            this.CMBJOB_Salary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Salary.FormattingEnabled = true;
            this.CMBJOB_Salary.Location = new System.Drawing.Point(150, 349);
            this.CMBJOB_Salary.Name = "CMBJOB_Salary";
            this.CMBJOB_Salary.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_Salary.Sorted = true;
            this.CMBJOB_Salary.TabIndex = 126;
            this.CMBJOB_Salary.Tag = "1";
            // 
            // LBLJOB_Salary
            // 
            this.LBLJOB_Salary.AutoSize = true;
            this.LBLJOB_Salary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Salary.Location = new System.Drawing.Point(12, 353);
            this.LBLJOB_Salary.Name = "LBLJOB_Salary";
            this.LBLJOB_Salary.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Salary.TabIndex = 132;
            this.LBLJOB_Salary.Text = "label3";
            // 
            // CMBJOB_Sector
            // 
            this.CMBJOB_Sector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Sector.FormattingEnabled = true;
            this.CMBJOB_Sector.Location = new System.Drawing.Point(150, 228);
            this.CMBJOB_Sector.Name = "CMBJOB_Sector";
            this.CMBJOB_Sector.Size = new System.Drawing.Size(312, 24);
            this.CMBJOB_Sector.TabIndex = 120;
            this.CMBJOB_Sector.Tag = "1";
            // 
            // LBLJOB_Sector
            // 
            this.LBLJOB_Sector.AutoSize = true;
            this.LBLJOB_Sector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Sector.Location = new System.Drawing.Point(12, 231);
            this.LBLJOB_Sector.Name = "LBLJOB_Sector";
            this.LBLJOB_Sector.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Sector.TabIndex = 131;
            this.LBLJOB_Sector.Text = "label3";
            // 
            // CMBJOB_TownCity
            // 
            this.CMBJOB_TownCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_TownCity.FormattingEnabled = true;
            this.CMBJOB_TownCity.Location = new System.Drawing.Point(150, 198);
            this.CMBJOB_TownCity.Name = "CMBJOB_TownCity";
            this.CMBJOB_TownCity.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_TownCity.Sorted = true;
            this.CMBJOB_TownCity.TabIndex = 119;
            this.CMBJOB_TownCity.Tag = "1";
            // 
            // LBLJOB_TownCity
            // 
            this.LBLJOB_TownCity.AutoSize = true;
            this.LBLJOB_TownCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_TownCity.Location = new System.Drawing.Point(12, 201);
            this.LBLJOB_TownCity.Name = "LBLJOB_TownCity";
            this.LBLJOB_TownCity.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_TownCity.TabIndex = 130;
            this.LBLJOB_TownCity.Text = "label3";
            // 
            // CMBJOB_Company
            // 
            this.CMBJOB_Company.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_Company.FormattingEnabled = true;
            this.CMBJOB_Company.Location = new System.Drawing.Point(150, 117);
            this.CMBJOB_Company.Name = "CMBJOB_Company";
            this.CMBJOB_Company.Size = new System.Drawing.Size(312, 24);
            this.CMBJOB_Company.TabIndex = 117;
            this.CMBJOB_Company.Tag = "1";
            // 
            // LBLJOB_Company
            // 
            this.LBLJOB_Company.AutoSize = true;
            this.LBLJOB_Company.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_Company.Location = new System.Drawing.Point(12, 120);
            this.LBLJOB_Company.Name = "LBLJOB_Company";
            this.LBLJOB_Company.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_Company.TabIndex = 129;
            this.LBLJOB_Company.Tag = "1";
            this.LBLJOB_Company.Text = "label3";
            // 
            // DTEJOB_DateApplied
            // 
            this.DTEJOB_DateApplied.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTEJOB_DateApplied.Location = new System.Drawing.Point(151, 79);
            this.DTEJOB_DateApplied.Name = "DTEJOB_DateApplied";
            this.DTEJOB_DateApplied.Size = new System.Drawing.Size(140, 23);
            this.DTEJOB_DateApplied.TabIndex = 116;
            this.DTEJOB_DateApplied.Tag = "1";
            this.DTEJOB_DateApplied.ValueChanged += new System.EventHandler(this.DTEJOB_DateApplied_ValueChanged);
            // 
            // LBLJOB_DateApplied
            // 
            this.LBLJOB_DateApplied.AutoSize = true;
            this.LBLJOB_DateApplied.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_DateApplied.Location = new System.Drawing.Point(11, 83);
            this.LBLJOB_DateApplied.Name = "LBLJOB_DateApplied";
            this.LBLJOB_DateApplied.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_DateApplied.TabIndex = 128;
            this.LBLJOB_DateApplied.Tag = "1";
            this.LBLJOB_DateApplied.Text = "label3";
            // 
            // CMBJobID
            // 
            this.CMBJobID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJobID.FormattingEnabled = true;
            this.CMBJobID.Location = new System.Drawing.Point(150, 41);
            this.CMBJobID.Name = "CMBJobID";
            this.CMBJobID.Size = new System.Drawing.Size(57, 24);
            this.CMBJobID.TabIndex = 115;
            // 
            // LBLJOB_ID
            // 
            this.LBLJOB_ID.AutoSize = true;
            this.LBLJOB_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_ID.Location = new System.Drawing.Point(11, 45);
            this.LBLJOB_ID.Name = "LBLJOB_ID";
            this.LBLJOB_ID.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_ID.TabIndex = 127;
            this.LBLJOB_ID.Text = "label1";
            // 
            // BTNPreview
            // 
            this.BTNPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNPreview.Location = new System.Drawing.Point(120, 458);
            this.BTNPreview.Name = "BTNPreview";
            this.BTNPreview.Size = new System.Drawing.Size(85, 34);
            this.BTNPreview.TabIndex = 141;
            this.BTNPreview.Text = "Preview";
            this.BTNPreview.UseVisualStyleBackColor = true;
            this.BTNPreview.Visible = false;
            this.BTNPreview.Click += new System.EventHandler(this.BTNPreview_Click);
            // 
            // BTNPrint
            // 
            this.BTNPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNPrint.Location = new System.Drawing.Point(15, 458);
            this.BTNPrint.Name = "BTNPrint";
            this.BTNPrint.Size = new System.Drawing.Size(85, 34);
            this.BTNPrint.TabIndex = 140;
            this.BTNPrint.Text = "Print";
            this.BTNPrint.UseVisualStyleBackColor = true;
            this.BTNPrint.Click += new System.EventHandler(this.BTNPrint_Click);
            // 
            // DTEJOB_DateAppliedTo
            // 
            this.DTEJOB_DateAppliedTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTEJOB_DateAppliedTo.Location = new System.Drawing.Point(422, 78);
            this.DTEJOB_DateAppliedTo.Name = "DTEJOB_DateAppliedTo";
            this.DTEJOB_DateAppliedTo.Size = new System.Drawing.Size(140, 23);
            this.DTEJOB_DateAppliedTo.TabIndex = 160;
            this.DTEJOB_DateAppliedTo.Tag = "1";
            this.DTEJOB_DateAppliedTo.ValueChanged += new System.EventHandler(this.DTEJOB_DateAppliedTo_ValueChanged);
            // 
            // LBLJOB_DateAppliedTo
            // 
            this.LBLJOB_DateAppliedTo.AutoSize = true;
            this.LBLJOB_DateAppliedTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_DateAppliedTo.Location = new System.Drawing.Point(302, 82);
            this.LBLJOB_DateAppliedTo.Name = "LBLJOB_DateAppliedTo";
            this.LBLJOB_DateAppliedTo.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_DateAppliedTo.TabIndex = 161;
            this.LBLJOB_DateAppliedTo.Tag = "1";
            this.LBLJOB_DateAppliedTo.Text = "label3";
            // 
            // CHKJOB_CompanyAll
            // 
            this.CHKJOB_CompanyAll.AutoSize = true;
            this.CHKJOB_CompanyAll.Checked = true;
            this.CHKJOB_CompanyAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_CompanyAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_CompanyAll.Location = new System.Drawing.Point(577, 120);
            this.CHKJOB_CompanyAll.Name = "CHKJOB_CompanyAll";
            this.CHKJOB_CompanyAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_CompanyAll.TabIndex = 162;
            this.CHKJOB_CompanyAll.Text = "All";
            this.CHKJOB_CompanyAll.UseVisualStyleBackColor = true;
            // 
            // CHKJOB_DateAppliedAll
            // 
            this.CHKJOB_DateAppliedAll.AutoSize = true;
            this.CHKJOB_DateAppliedAll.Checked = true;
            this.CHKJOB_DateAppliedAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_DateAppliedAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_DateAppliedAll.Location = new System.Drawing.Point(577, 82);
            this.CHKJOB_DateAppliedAll.Name = "CHKJOB_DateAppliedAll";
            this.CHKJOB_DateAppliedAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_DateAppliedAll.TabIndex = 164;
            this.CHKJOB_DateAppliedAll.Text = "All";
            this.CHKJOB_DateAppliedAll.UseVisualStyleBackColor = true;
            this.CHKJOB_DateAppliedAll.CheckedChanged += new System.EventHandler(this.CHKJOB_DateAppliedAll_CheckedChanged);
            // 
            // CHKJOB_TownCityAll
            // 
            this.CHKJOB_TownCityAll.AutoSize = true;
            this.CHKJOB_TownCityAll.Checked = true;
            this.CHKJOB_TownCityAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_TownCityAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_TownCityAll.Location = new System.Drawing.Point(578, 199);
            this.CHKJOB_TownCityAll.Name = "CHKJOB_TownCityAll";
            this.CHKJOB_TownCityAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_TownCityAll.TabIndex = 165;
            this.CHKJOB_TownCityAll.Text = "All";
            this.CHKJOB_TownCityAll.UseVisualStyleBackColor = true;
            // 
            // CHKJOB_SectorAll
            // 
            this.CHKJOB_SectorAll.AutoSize = true;
            this.CHKJOB_SectorAll.Checked = true;
            this.CHKJOB_SectorAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_SectorAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_SectorAll.Location = new System.Drawing.Point(578, 227);
            this.CHKJOB_SectorAll.Name = "CHKJOB_SectorAll";
            this.CHKJOB_SectorAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_SectorAll.TabIndex = 166;
            this.CHKJOB_SectorAll.Text = "All";
            this.CHKJOB_SectorAll.UseVisualStyleBackColor = true;
            // 
            // CHKJOB_TypeAll
            // 
            this.CHKJOB_TypeAll.AutoSize = true;
            this.CHKJOB_TypeAll.Checked = true;
            this.CHKJOB_TypeAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_TypeAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_TypeAll.Location = new System.Drawing.Point(578, 268);
            this.CHKJOB_TypeAll.Name = "CHKJOB_TypeAll";
            this.CHKJOB_TypeAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_TypeAll.TabIndex = 167;
            this.CHKJOB_TypeAll.Text = "All";
            this.CHKJOB_TypeAll.UseVisualStyleBackColor = true;
            // 
            // CHKJOB_HoursAll
            // 
            this.CHKJOB_HoursAll.AutoSize = true;
            this.CHKJOB_HoursAll.Checked = true;
            this.CHKJOB_HoursAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_HoursAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_HoursAll.Location = new System.Drawing.Point(578, 296);
            this.CHKJOB_HoursAll.Name = "CHKJOB_HoursAll";
            this.CHKJOB_HoursAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_HoursAll.TabIndex = 168;
            this.CHKJOB_HoursAll.Text = "All";
            this.CHKJOB_HoursAll.UseVisualStyleBackColor = true;
            // 
            // CHKJOB_WhereAll
            // 
            this.CHKJOB_WhereAll.AutoSize = true;
            this.CHKJOB_WhereAll.Checked = true;
            this.CHKJOB_WhereAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_WhereAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_WhereAll.Location = new System.Drawing.Point(578, 323);
            this.CHKJOB_WhereAll.Name = "CHKJOB_WhereAll";
            this.CHKJOB_WhereAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_WhereAll.TabIndex = 169;
            this.CHKJOB_WhereAll.Text = "All";
            this.CHKJOB_WhereAll.UseVisualStyleBackColor = true;
            // 
            // CHKJOB_StatusAll
            // 
            this.CHKJOB_StatusAll.AutoSize = true;
            this.CHKJOB_StatusAll.Checked = true;
            this.CHKJOB_StatusAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_StatusAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_StatusAll.Location = new System.Drawing.Point(578, 381);
            this.CHKJOB_StatusAll.Name = "CHKJOB_StatusAll";
            this.CHKJOB_StatusAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_StatusAll.TabIndex = 170;
            this.CHKJOB_StatusAll.Text = "All";
            this.CHKJOB_StatusAll.UseVisualStyleBackColor = true;
            // 
            // CHKJOB_TitleAll
            // 
            this.CHKJOB_TitleAll.AutoSize = true;
            this.CHKJOB_TitleAll.Checked = true;
            this.CHKJOB_TitleAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_TitleAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_TitleAll.Location = new System.Drawing.Point(578, 145);
            this.CHKJOB_TitleAll.Name = "CHKJOB_TitleAll";
            this.CHKJOB_TitleAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_TitleAll.TabIndex = 171;
            this.CHKJOB_TitleAll.Text = "All";
            this.CHKJOB_TitleAll.UseVisualStyleBackColor = true;
            // 
            // CHKJOB_SalaryAll
            // 
            this.CHKJOB_SalaryAll.AutoSize = true;
            this.CHKJOB_SalaryAll.Checked = true;
            this.CHKJOB_SalaryAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_SalaryAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_SalaryAll.Location = new System.Drawing.Point(578, 353);
            this.CHKJOB_SalaryAll.Name = "CHKJOB_SalaryAll";
            this.CHKJOB_SalaryAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_SalaryAll.TabIndex = 172;
            this.CHKJOB_SalaryAll.Text = "All";
            this.CHKJOB_SalaryAll.UseVisualStyleBackColor = true;
            // 
            // CHKJOB_DirectAll
            // 
            this.CHKJOB_DirectAll.AutoSize = true;
            this.CHKJOB_DirectAll.Checked = true;
            this.CHKJOB_DirectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKJOB_DirectAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKJOB_DirectAll.Location = new System.Drawing.Point(578, 170);
            this.CHKJOB_DirectAll.Name = "CHKJOB_DirectAll";
            this.CHKJOB_DirectAll.Size = new System.Drawing.Size(42, 21);
            this.CHKJOB_DirectAll.TabIndex = 173;
            this.CHKJOB_DirectAll.Text = "All";
            this.CHKJOB_DirectAll.UseVisualStyleBackColor = true;
            this.CHKJOB_DirectAll.CheckedChanged += new System.EventHandler(this.CHKJOB_DirectAll_CheckedChanged);
            // 
            // CMBJOB_SalaryTo
            // 
            this.CMBJOB_SalaryTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBJOB_SalaryTo.FormattingEnabled = true;
            this.CMBJOB_SalaryTo.Location = new System.Drawing.Point(403, 352);
            this.CMBJOB_SalaryTo.Name = "CMBJOB_SalaryTo";
            this.CMBJOB_SalaryTo.Size = new System.Drawing.Size(159, 24);
            this.CMBJOB_SalaryTo.Sorted = true;
            this.CMBJOB_SalaryTo.TabIndex = 174;
            this.CMBJOB_SalaryTo.Tag = "1";
            // 
            // LBLJOB_SalaryTo
            // 
            this.LBLJOB_SalaryTo.AutoSize = true;
            this.LBLJOB_SalaryTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLJOB_SalaryTo.Location = new System.Drawing.Point(319, 355);
            this.LBLJOB_SalaryTo.Name = "LBLJOB_SalaryTo";
            this.LBLJOB_SalaryTo.Size = new System.Drawing.Size(46, 17);
            this.LBLJOB_SalaryTo.TabIndex = 175;
            this.LBLJOB_SalaryTo.Text = "label3";
            // 
            // CHKSummary
            // 
            this.CHKSummary.AutoSize = true;
            this.CHKSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKSummary.Location = new System.Drawing.Point(251, 459);
            this.CHKSummary.Name = "CHKSummary";
            this.CHKSummary.Size = new System.Drawing.Size(86, 21);
            this.CHKSummary.TabIndex = 176;
            this.CHKSummary.Text = "Summary";
            this.CHKSummary.UseVisualStyleBackColor = true;
            // 
            // CHKSort
            // 
            this.CHKSort.AutoSize = true;
            this.CHKSort.Checked = true;
            this.CHKSort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKSort.Location = new System.Drawing.Point(367, 459);
            this.CHKSort.Name = "CHKSort";
            this.CHKSort.Size = new System.Drawing.Size(158, 21);
            this.CHKSort.TabIndex = 224;
            this.CHKSort.Text = "Sort Date By Latest?";
            this.CHKSort.UseVisualStyleBackColor = true;
            // 
            // CHKIExcludeApplied
            // 
            this.CHKIExcludeApplied.AutoSize = true;
            this.CHKIExcludeApplied.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKIExcludeApplied.Location = new System.Drawing.Point(322, 382);
            this.CHKIExcludeApplied.Name = "CHKIExcludeApplied";
            this.CHKIExcludeApplied.Size = new System.Drawing.Size(160, 21);
            this.CHKIExcludeApplied.TabIndex = 225;
            this.CHKIExcludeApplied.Text = "Exclude Applied For?";
            this.CHKIExcludeApplied.UseVisualStyleBackColor = true;
            this.CHKIExcludeApplied.CheckedChanged += new System.EventHandler(this.CHKIExcludeApplied_CheckedChanged);
            // 
            // frmJobsAppliedFor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 503);
            this.Controls.Add(this.CHKIExcludeApplied);
            this.Controls.Add(this.CHKSort);
            this.Controls.Add(this.CHKSummary);
            this.Controls.Add(this.CMBJOB_SalaryTo);
            this.Controls.Add(this.LBLJOB_SalaryTo);
            this.Controls.Add(this.CHKJOB_DirectAll);
            this.Controls.Add(this.CHKJOB_SalaryAll);
            this.Controls.Add(this.CHKJOB_TitleAll);
            this.Controls.Add(this.CHKJOB_StatusAll);
            this.Controls.Add(this.CHKJOB_WhereAll);
            this.Controls.Add(this.CHKJOB_HoursAll);
            this.Controls.Add(this.CHKJOB_TypeAll);
            this.Controls.Add(this.CHKJOB_SectorAll);
            this.Controls.Add(this.CHKJOB_TownCityAll);
            this.Controls.Add(this.CHKJOB_DateAppliedAll);
            this.Controls.Add(this.CHKJOB_CompanyAll);
            this.Controls.Add(this.DTEJOB_DateAppliedTo);
            this.Controls.Add(this.LBLJOB_DateAppliedTo);
            this.Controls.Add(this.BTNPreview);
            this.Controls.Add(this.BTNPrint);
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
            this.Controls.Add(this.CMBJobID);
            this.Controls.Add(this.LBLJOB_ID);
            this.Controls.Add(this.PANTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmJobsAppliedFor";
            this.Text = "Form_JobsAppliedFor";
            this.Load += new System.EventHandler(this.frmJobsAppliedFor_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmJobsAppliedFor_Paint);
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel PANTitle;
        private System.Windows.Forms.Label LBLTitle;
        private System.Windows.Forms.ComboBox CMBJOB_Status;
        private System.Windows.Forms.Label LBLJOB_Status;
        private System.Windows.Forms.Label LBLJOB_Direct;
        private System.Windows.Forms.CheckBox CHKJOB_Direct;
        private System.Windows.Forms.ComboBox CMBJOB_Where;
        private System.Windows.Forms.Label LBLJOB_Where;
        private System.Windows.Forms.ComboBox CMBJOB_Hours;
        private System.Windows.Forms.Label LBLJOB_Hours;
        private System.Windows.Forms.ComboBox CMBJOB_Type;
        private System.Windows.Forms.Label LBLJOB_Type;
        private System.Windows.Forms.ComboBox CMBJOB_Title;
        private System.Windows.Forms.Label LBLJOB_Title;
        private System.Windows.Forms.ComboBox CMBJOB_Salary;
        private System.Windows.Forms.Label LBLJOB_Salary;
        private System.Windows.Forms.ComboBox CMBJOB_Sector;
        private System.Windows.Forms.Label LBLJOB_Sector;
        private System.Windows.Forms.ComboBox CMBJOB_TownCity;
        private System.Windows.Forms.Label LBLJOB_TownCity;
        private System.Windows.Forms.ComboBox CMBJOB_Company;
        private System.Windows.Forms.Label LBLJOB_Company;
        private System.Windows.Forms.DateTimePicker DTEJOB_DateApplied;
        private System.Windows.Forms.Label LBLJOB_DateApplied;
        private System.Windows.Forms.ComboBox CMBJobID;
        private System.Windows.Forms.Label LBLJOB_ID;
        private System.Windows.Forms.Button BTNPreview;
        private System.Windows.Forms.Button BTNPrint;
        private System.Windows.Forms.DateTimePicker DTEJOB_DateAppliedTo;
        private System.Windows.Forms.Label LBLJOB_DateAppliedTo;
        private System.Windows.Forms.CheckBox CHKJOB_CompanyAll;
        private System.Windows.Forms.CheckBox CHKJOB_DateAppliedAll;
        private System.Windows.Forms.CheckBox CHKJOB_TownCityAll;
        private System.Windows.Forms.CheckBox CHKJOB_SectorAll;
        private System.Windows.Forms.CheckBox CHKJOB_TypeAll;
        private System.Windows.Forms.CheckBox CHKJOB_HoursAll;
        private System.Windows.Forms.CheckBox CHKJOB_WhereAll;
        private System.Windows.Forms.CheckBox CHKJOB_StatusAll;
        private System.Windows.Forms.CheckBox CHKJOB_TitleAll;
        private System.Windows.Forms.CheckBox CHKJOB_SalaryAll;
        private System.Windows.Forms.CheckBox CHKJOB_DirectAll;
        private System.Windows.Forms.ComboBox CMBJOB_SalaryTo;
        private System.Windows.Forms.Label LBLJOB_SalaryTo;
        private System.Windows.Forms.CheckBox CHKSummary;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.CheckBox CHKSort;
        private System.Windows.Forms.CheckBox CHKIExcludeApplied;
    }
}