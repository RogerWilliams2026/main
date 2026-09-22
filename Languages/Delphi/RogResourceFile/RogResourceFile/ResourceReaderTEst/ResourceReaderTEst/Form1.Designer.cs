namespace ResourceReaderTEst
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.TXTIntro = new System.Windows.Forms.TextBox();
            this.PICMothership = new System.Windows.Forms.PictureBox();
            this.PICAlien = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PICMothership)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICAlien)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, 332);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TXTIntro
            // 
            this.TXTIntro.AcceptsReturn = true;
            this.TXTIntro.Location = new System.Drawing.Point(98, 39);
            this.TXTIntro.Multiline = true;
            this.TXTIntro.Name = "TXTIntro";
            this.TXTIntro.Size = new System.Drawing.Size(549, 170);
            this.TXTIntro.TabIndex = 1;
            // 
            // PICMothership
            // 
            this.PICMothership.Location = new System.Drawing.Point(39, 246);
            this.PICMothership.Name = "PICMothership";
            this.PICMothership.Size = new System.Drawing.Size(125, 86);
            this.PICMothership.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PICMothership.TabIndex = 2;
            this.PICMothership.TabStop = false;
            // 
            // PICAlien
            // 
            this.PICAlien.Location = new System.Drawing.Point(390, 231);
            this.PICAlien.Name = "PICAlien";
            this.PICAlien.Size = new System.Drawing.Size(125, 86);
            this.PICAlien.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PICAlien.TabIndex = 3;
            this.PICAlien.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PICAlien);
            this.Controls.Add(this.PICMothership);
            this.Controls.Add(this.TXTIntro);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PICMothership)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PICAlien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TXTIntro;
        private System.Windows.Forms.PictureBox PICMothership;
        private System.Windows.Forms.PictureBox PICAlien;
    }
}

