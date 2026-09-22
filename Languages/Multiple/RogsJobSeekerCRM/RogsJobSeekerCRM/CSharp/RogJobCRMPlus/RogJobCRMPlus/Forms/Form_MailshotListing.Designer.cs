namespace RogJobCRMPlus.Forms
{
    partial class frmMailshotListing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMailshotListing));
            this.CHKMSH_DateAll = new System.Windows.Forms.CheckBox();
            this.CHKMSH_MailshotNameAll = new System.Windows.Forms.CheckBox();
            this.DTEMSH_DateTo = new System.Windows.Forms.DateTimePicker();
            this.LBLMSH_DateTo = new System.Windows.Forms.Label();
            this.BTNPreview = new System.Windows.Forms.Button();
            this.BTNPrint = new System.Windows.Forms.Button();
            this.LBLMSH_MailshotName = new System.Windows.Forms.Label();
            this.DTEMSH_Date = new System.Windows.Forms.DateTimePicker();
            this.LBLMSH_Date = new System.Windows.Forms.Label();
            this.CMBMailshotID = new System.Windows.Forms.ComboBox();
            this.LBLMSH_ID = new System.Windows.Forms.Label();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.CMBMSH_MailshotName = new System.Windows.Forms.ComboBox();
            this.CHKSort = new System.Windows.Forms.CheckBox();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // CHKMSH_DateAll
            // 
            this.CHKMSH_DateAll.AutoSize = true;
            this.CHKMSH_DateAll.Checked = true;
            this.CHKMSH_DateAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKMSH_DateAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKMSH_DateAll.Location = new System.Drawing.Point(570, 87);
            this.CHKMSH_DateAll.Name = "CHKMSH_DateAll";
            this.CHKMSH_DateAll.Size = new System.Drawing.Size(42, 21);
            this.CHKMSH_DateAll.TabIndex = 3;
            this.CHKMSH_DateAll.Text = "All";
            this.CHKMSH_DateAll.UseVisualStyleBackColor = true;
            // 
            // CHKMSH_MailshotNameAll
            // 
            this.CHKMSH_MailshotNameAll.AutoSize = true;
            this.CHKMSH_MailshotNameAll.Checked = true;
            this.CHKMSH_MailshotNameAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKMSH_MailshotNameAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKMSH_MailshotNameAll.Location = new System.Drawing.Point(570, 125);
            this.CHKMSH_MailshotNameAll.Name = "CHKMSH_MailshotNameAll";
            this.CHKMSH_MailshotNameAll.Size = new System.Drawing.Size(42, 21);
            this.CHKMSH_MailshotNameAll.TabIndex = 5;
            this.CHKMSH_MailshotNameAll.Text = "All";
            this.CHKMSH_MailshotNameAll.UseVisualStyleBackColor = true;
            // 
            // DTEMSH_DateTo
            // 
            this.DTEMSH_DateTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTEMSH_DateTo.Location = new System.Drawing.Point(401, 85);
            this.DTEMSH_DateTo.Name = "DTEMSH_DateTo";
            this.DTEMSH_DateTo.Size = new System.Drawing.Size(140, 23);
            this.DTEMSH_DateTo.TabIndex = 2;
            this.DTEMSH_DateTo.Tag = "1";
            this.DTEMSH_DateTo.ValueChanged += new System.EventHandler(this.DTEMSH_DateTo_ValueChanged);
            // 
            // LBLMSH_DateTo
            // 
            this.LBLMSH_DateTo.AutoSize = true;
            this.LBLMSH_DateTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLMSH_DateTo.Location = new System.Drawing.Point(295, 89);
            this.LBLMSH_DateTo.Name = "LBLMSH_DateTo";
            this.LBLMSH_DateTo.Size = new System.Drawing.Size(46, 17);
            this.LBLMSH_DateTo.TabIndex = 280;
            this.LBLMSH_DateTo.Tag = "1";
            this.LBLMSH_DateTo.Text = "label3";
            // 
            // BTNPreview
            // 
            this.BTNPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNPreview.Location = new System.Drawing.Point(125, 182);
            this.BTNPreview.Name = "BTNPreview";
            this.BTNPreview.Size = new System.Drawing.Size(85, 34);
            this.BTNPreview.TabIndex = 7;
            this.BTNPreview.Text = "Preview";
            this.BTNPreview.UseVisualStyleBackColor = true;
            this.BTNPreview.Visible = false;
            this.BTNPreview.Click += new System.EventHandler(this.BTNPreview_Click);
            // 
            // BTNPrint
            // 
            this.BTNPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNPrint.Location = new System.Drawing.Point(20, 182);
            this.BTNPrint.Name = "BTNPrint";
            this.BTNPrint.Size = new System.Drawing.Size(85, 34);
            this.BTNPrint.TabIndex = 6;
            this.BTNPrint.Text = "Print";
            this.BTNPrint.UseVisualStyleBackColor = true;
            this.BTNPrint.Click += new System.EventHandler(this.BTNPrint_Click);
            // 
            // LBLMSH_MailshotName
            // 
            this.LBLMSH_MailshotName.AutoSize = true;
            this.LBLMSH_MailshotName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLMSH_MailshotName.Location = new System.Drawing.Point(19, 125);
            this.LBLMSH_MailshotName.Name = "LBLMSH_MailshotName";
            this.LBLMSH_MailshotName.Size = new System.Drawing.Size(46, 17);
            this.LBLMSH_MailshotName.TabIndex = 275;
            this.LBLMSH_MailshotName.Tag = "1";
            this.LBLMSH_MailshotName.Text = "label3";
            // 
            // DTEMSH_Date
            // 
            this.DTEMSH_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTEMSH_Date.Location = new System.Drawing.Point(136, 84);
            this.DTEMSH_Date.Name = "DTEMSH_Date";
            this.DTEMSH_Date.Size = new System.Drawing.Size(140, 23);
            this.DTEMSH_Date.TabIndex = 1;
            this.DTEMSH_Date.Tag = "1";
            this.DTEMSH_Date.ValueChanged += new System.EventHandler(this.DTEMSH_Date_ValueChanged);
            // 
            // LBLMSH_Date
            // 
            this.LBLMSH_Date.AutoSize = true;
            this.LBLMSH_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLMSH_Date.Location = new System.Drawing.Point(19, 88);
            this.LBLMSH_Date.Name = "LBLMSH_Date";
            this.LBLMSH_Date.Size = new System.Drawing.Size(46, 17);
            this.LBLMSH_Date.TabIndex = 274;
            this.LBLMSH_Date.Tag = "1";
            this.LBLMSH_Date.Text = "label3";
            // 
            // CMBMailshotID
            // 
            this.CMBMailshotID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBMailshotID.FormattingEnabled = true;
            this.CMBMailshotID.Location = new System.Drawing.Point(136, 46);
            this.CMBMailshotID.Name = "CMBMailshotID";
            this.CMBMailshotID.Size = new System.Drawing.Size(57, 24);
            this.CMBMailshotID.TabIndex = 0;
            // 
            // LBLMSH_ID
            // 
            this.LBLMSH_ID.AutoSize = true;
            this.LBLMSH_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLMSH_ID.Location = new System.Drawing.Point(21, 50);
            this.LBLMSH_ID.Name = "LBLMSH_ID";
            this.LBLMSH_ID.Size = new System.Drawing.Size(46, 17);
            this.LBLMSH_ID.TabIndex = 273;
            this.LBLMSH_ID.Text = "label1";
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(0, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(612, 34);
            this.PANTitle.TabIndex = 271;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(587, 6);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 273;
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
            // CMBMSH_MailshotName
            // 
            this.CMBMSH_MailshotName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBMSH_MailshotName.FormattingEnabled = true;
            this.CMBMSH_MailshotName.Location = new System.Drawing.Point(135, 122);
            this.CMBMSH_MailshotName.Name = "CMBMSH_MailshotName";
            this.CMBMSH_MailshotName.Size = new System.Drawing.Size(312, 24);
            this.CMBMSH_MailshotName.TabIndex = 4;
            this.CMBMSH_MailshotName.Tag = "4";
            // 
            // CHKSort
            // 
            this.CHKSort.AutoSize = true;
            this.CHKSort.Checked = true;
            this.CHKSort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKSort.Location = new System.Drawing.Point(280, 190);
            this.CHKSort.Name = "CHKSort";
            this.CHKSort.Size = new System.Drawing.Size(158, 21);
            this.CHKSort.TabIndex = 281;
            this.CHKSort.Text = "Sort Date By Latest?";
            this.CHKSort.UseVisualStyleBackColor = true;
            // 
            // frmMailshotListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 225);
            this.Controls.Add(this.CHKSort);
            this.Controls.Add(this.CHKMSH_DateAll);
            this.Controls.Add(this.CHKMSH_MailshotNameAll);
            this.Controls.Add(this.DTEMSH_DateTo);
            this.Controls.Add(this.LBLMSH_DateTo);
            this.Controls.Add(this.BTNPreview);
            this.Controls.Add(this.BTNPrint);
            this.Controls.Add(this.CMBMSH_MailshotName);
            this.Controls.Add(this.LBLMSH_MailshotName);
            this.Controls.Add(this.DTEMSH_Date);
            this.Controls.Add(this.LBLMSH_Date);
            this.Controls.Add(this.CMBMailshotID);
            this.Controls.Add(this.LBLMSH_ID);
            this.Controls.Add(this.PANTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMailshotListing";
            this.Text = "FormMailshotListing";
            this.Load += new System.EventHandler(this.frmMailshotListing_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMailshotListing_Paint);
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox CHKMSH_DateAll;
        private System.Windows.Forms.CheckBox CHKMSH_MailshotNameAll;
        private System.Windows.Forms.DateTimePicker DTEMSH_DateTo;
        private System.Windows.Forms.Label LBLMSH_DateTo;
        private System.Windows.Forms.Button BTNPreview;
        private System.Windows.Forms.Button BTNPrint;
        private System.Windows.Forms.Label LBLMSH_MailshotName;
        private System.Windows.Forms.DateTimePicker DTEMSH_Date;
        private System.Windows.Forms.Label LBLMSH_Date;
        private System.Windows.Forms.ComboBox CMBMailshotID;
        private System.Windows.Forms.Label LBLMSH_ID;
        private System.Windows.Forms.Panel PANTitle;
        private System.Windows.Forms.Label LBLTitle;
        private System.Windows.Forms.ComboBox CMBMSH_MailshotName;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.CheckBox CHKSort;
    }
}