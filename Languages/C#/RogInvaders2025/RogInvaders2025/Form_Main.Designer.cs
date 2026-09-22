namespace RogInvaders2025
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
            this.PICTitle1 = new System.Windows.Forms.PictureBox();
            this.PICTitle1a = new System.Windows.Forms.PictureBox();
            this.TXTIntroText = new System.Windows.Forms.TextBox();
            this.PICAlienAni = new System.Windows.Forms.PictureBox();
            this.PICAlienMotherShipIntro = new System.Windows.Forms.PictureBox();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.PICClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PICTitle1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTitle1a)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICAlienAni)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICAlienMotherShipIntro)).BeginInit();
            this.PANTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PICClose)).BeginInit();
            this.SuspendLayout();
            // 
            // PICTitle1
            // 
            this.PICTitle1.BackColor = System.Drawing.Color.Black;
            this.PICTitle1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PICTitle1.ImageLocation = "";
            this.PICTitle1.Location = new System.Drawing.Point(655, 123);
            this.PICTitle1.Name = "PICTitle1";
            this.PICTitle1.Size = new System.Drawing.Size(356, 135);
            this.PICTitle1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PICTitle1.TabIndex = 20;
            this.PICTitle1.TabStop = false;
            // 
            // PICTitle1a
            // 
            this.PICTitle1a.BackColor = System.Drawing.Color.Black;
            this.PICTitle1a.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PICTitle1a.ImageLocation = "";
            this.PICTitle1a.Location = new System.Drawing.Point(200, 308);
            this.PICTitle1a.Name = "PICTitle1a";
            this.PICTitle1a.Size = new System.Drawing.Size(825, 228);
            this.PICTitle1a.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PICTitle1a.TabIndex = 21;
            this.PICTitle1a.TabStop = false;
            // 
            // TXTIntroText
            // 
            this.TXTIntroText.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.TXTIntroText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTIntroText.Font = new System.Drawing.Font("Tw Cen MT Condensed Extra Bold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTIntroText.ForeColor = System.Drawing.Color.Gold;
            this.TXTIntroText.Location = new System.Drawing.Point(5, 685);
            this.TXTIntroText.Name = "TXTIntroText";
            this.TXTIntroText.Size = new System.Drawing.Size(1172, 35);
            this.TXTIntroText.TabIndex = 38;
            this.TXTIntroText.Visible = false;
            // 
            // PICAlienAni
            // 
            this.PICAlienAni.ImageLocation = "";
            this.PICAlienAni.Location = new System.Drawing.Point(516, 470);
            this.PICAlienAni.Name = "PICAlienAni";
            this.PICAlienAni.Size = new System.Drawing.Size(166, 107);
            this.PICAlienAni.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PICAlienAni.TabIndex = 39;
            this.PICAlienAni.TabStop = false;
            // 
            // PICAlienMotherShipIntro
            // 
            this.PICAlienMotherShipIntro.ErrorImage = null;
            this.PICAlienMotherShipIntro.Image = ((System.Drawing.Image)(resources.GetObject("PICAlienMotherShipIntro.Image")));
            this.PICAlienMotherShipIntro.ImageLocation = "";
            this.PICAlienMotherShipIntro.InitialImage = null;
            this.PICAlienMotherShipIntro.Location = new System.Drawing.Point(550, 780);
            this.PICAlienMotherShipIntro.Name = "PICAlienMotherShipIntro";
            this.PICAlienMotherShipIntro.Size = new System.Drawing.Size(100, 40);
            this.PICAlienMotherShipIntro.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PICAlienMotherShipIntro.TabIndex = 40;
            this.PICAlienMotherShipIntro.TabStop = false;
            this.PICAlienMotherShipIntro.Visible = false;
            // 
            // PANTitle
            // 
            this.PANTitle.BackColor = System.Drawing.Color.Black;
            this.PANTitle.Controls.Add(this.PICClose);
            this.PANTitle.Location = new System.Drawing.Point(1122, 0);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(54, 52);
            this.PANTitle.TabIndex = 41;
            // 
            // PICClose
            // 
            this.PICClose.Image = ((System.Drawing.Image)(resources.GetObject("PICClose.Image")));
            this.PICClose.InitialImage = null;
            this.PICClose.Location = new System.Drawing.Point(5, 10);
            this.PICClose.Name = "PICClose";
            this.PICClose.Size = new System.Drawing.Size(45, 29);
            this.PICClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PICClose.TabIndex = 1;
            this.PICClose.TabStop = false;
            this.PICClose.Click += new System.EventHandler(this.PICClose_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1180, 860);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.PICAlienMotherShipIntro);
            this.Controls.Add(this.PICAlienAni);
            this.Controls.Add(this.TXTIntroText);
            this.Controls.Add(this.PICTitle1a);
            this.Controls.Add(this.PICTitle1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.PICTitle1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICTitle1a)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICAlienAni)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICAlienMotherShipIntro)).EndInit();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PICClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox PICTitle1;
        private System.Windows.Forms.PictureBox PICTitle1a;
        private System.Windows.Forms.TextBox TXTIntroText;
        private System.Windows.Forms.PictureBox PICAlienAni;
        private System.Windows.Forms.PictureBox PICAlienMotherShipIntro;
        private System.Windows.Forms.Panel PANTitle;
        private System.Windows.Forms.PictureBox PICClose;
    }
}

