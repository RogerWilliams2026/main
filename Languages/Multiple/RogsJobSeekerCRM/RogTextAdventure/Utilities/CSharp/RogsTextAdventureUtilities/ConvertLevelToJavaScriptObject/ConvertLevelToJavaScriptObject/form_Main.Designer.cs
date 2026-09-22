namespace ConvertLevelToJavaScriptObject
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BTNFile = new Button();
            BTNClose = new Button();
            BTNConvert = new Button();
            SuspendLayout();
            // 
            // BTNFile
            // 
            BTNFile.Location = new Point(80, 195);
            BTNFile.Name = "BTNFile";
            BTNFile.Size = new Size(107, 40);
            BTNFile.TabIndex = 0;
            BTNFile.Text = "Select File";
            BTNFile.UseVisualStyleBackColor = true;
            BTNFile.Click += BTNFile_Click;
            // 
            // BTNClose
            // 
            BTNClose.Location = new Point(520, 195);
            BTNClose.Name = "BTNClose";
            BTNClose.Size = new Size(107, 40);
            BTNClose.TabIndex = 1;
            BTNClose.Text = "Close";
            BTNClose.UseVisualStyleBackColor = true;
            BTNClose.Click += BTNClose_Click;
            // 
            // BTNConvert
            // 
            BTNConvert.Location = new Point(318, 195);
            BTNConvert.Name = "BTNConvert";
            BTNConvert.Size = new Size(107, 40);
            BTNConvert.TabIndex = 2;
            BTNConvert.Text = "Convert";
            BTNConvert.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BTNConvert);
            Controls.Add(BTNClose);
            Controls.Add(BTNFile);
            Name = "frmMain";
            Text = "Level File To JavsScript Object";
            ResumeLayout(false);
        }

        #endregion

        private Button BTNFile;
        private Button BTNClose;
        private Button BTNConvert;
    }
}
