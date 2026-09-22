using System.Windows.Forms;
using System.Drawing;

namespace RogStock2026_Utilities.Screens
{
    partial class frmThemeMaintenance_Utilities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemeMaintenance_Utilities));
            this.BTNUndo = new System.Windows.Forms.Button();
            this.STBStatus = new System.Windows.Forms.StatusStrip();
            this.STLStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BTNSave = new System.Windows.Forms.Button();
            this.PANMain = new System.Windows.Forms.Panel();
            this.BTNPaste = new System.Windows.Forms.Button();
            this.BTNCopy = new System.Windows.Forms.Button();
            this.LBLSelectedColour = new System.Windows.Forms.Label();
            this.RBSelectedColour = new System.Windows.Forms.RadioButton();
            this.BTNColour = new System.Windows.Forms.Button();
            this.BTNUndoSingle = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CMBProperty = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CMBControl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DLGColour = new System.Windows.Forms.ColorDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.STBStatus.SuspendLayout();
            this.PANMain.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(187, 426);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 17;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            this.BTNUndo.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // STBStatus
            // 
            this.STBStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.STLStatus,
            this.toolStripStatusLabel1});
            this.STBStatus.Location = new System.Drawing.Point(0, 458);
            this.STBStatus.Name = "STBStatus";
            this.STBStatus.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.STBStatus.Size = new System.Drawing.Size(1021, 22);
            this.STBStatus.SizingGrip = false;
            this.STBStatus.TabIndex = 16;
            this.STBStatus.Text = "statusStrip1";
            // 
            // STLStatus
            // 
            this.STLStatus.Name = "STLStatus";
            this.STLStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(11, 426);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 13;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            this.BTNSave.Click += new System.EventHandler(this.BTNSave_Click);
            // 
            // PANMain
            // 
            this.PANMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PANMain.Controls.Add(this.BTNPaste);
            this.PANMain.Controls.Add(this.BTNCopy);
            this.PANMain.Controls.Add(this.LBLSelectedColour);
            this.PANMain.Controls.Add(this.RBSelectedColour);
            this.PANMain.Controls.Add(this.BTNColour);
            this.PANMain.Controls.Add(this.BTNUndoSingle);
            this.PANMain.Controls.Add(this.label4);
            this.PANMain.Controls.Add(this.CMBProperty);
            this.PANMain.Controls.Add(this.label3);
            this.PANMain.Controls.Add(this.CMBControl);
            this.PANMain.Controls.Add(this.label1);
            this.PANMain.Location = new System.Drawing.Point(11, 53);
            this.PANMain.Name = "PANMain";
            this.PANMain.Size = new System.Drawing.Size(274, 179);
            this.PANMain.TabIndex = 40;
            // 
            // BTNPaste
            // 
            this.BTNPaste.Location = new System.Drawing.Point(140, 115);
            this.BTNPaste.Name = "BTNPaste";
            this.BTNPaste.Size = new System.Drawing.Size(51, 20);
            this.BTNPaste.TabIndex = 54;
            this.BTNPaste.Text = "Paste";
            this.BTNPaste.UseVisualStyleBackColor = true;
            this.BTNPaste.Click += new System.EventHandler(this.BTNPaste_Click);
            // 
            // BTNCopy
            // 
            this.BTNCopy.Location = new System.Drawing.Point(82, 115);
            this.BTNCopy.Name = "BTNCopy";
            this.BTNCopy.Size = new System.Drawing.Size(51, 20);
            this.BTNCopy.TabIndex = 53;
            this.BTNCopy.Text = "Copy";
            this.BTNCopy.UseVisualStyleBackColor = true;
            this.BTNCopy.Click += new System.EventHandler(this.BTNCopy_Click);
            // 
            // LBLSelectedColour
            // 
            this.LBLSelectedColour.AutoSize = true;
            this.LBLSelectedColour.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LBLSelectedColour.Location = new System.Drawing.Point(94, 91);
            this.LBLSelectedColour.Name = "LBLSelectedColour";
            this.LBLSelectedColour.Size = new System.Drawing.Size(13, 19);
            this.LBLSelectedColour.TabIndex = 52;
            this.LBLSelectedColour.Text = " ";
            // 
            // RBSelectedColour
            // 
            this.RBSelectedColour.AutoSize = true;
            this.RBSelectedColour.Location = new System.Drawing.Point(75, 94);
            this.RBSelectedColour.Name = "RBSelectedColour";
            this.RBSelectedColour.Size = new System.Drawing.Size(14, 13);
            this.RBSelectedColour.TabIndex = 51;
            this.RBSelectedColour.TabStop = true;
            this.RBSelectedColour.UseVisualStyleBackColor = true;
            this.RBSelectedColour.Visible = false;
            this.RBSelectedColour.Paint += new System.Windows.Forms.PaintEventHandler(this.RBSelectedColour_Paint);
            // 
            // BTNColour
            // 
            this.BTNColour.Location = new System.Drawing.Point(16, 115);
            this.BTNColour.Name = "BTNColour";
            this.BTNColour.Size = new System.Drawing.Size(51, 20);
            this.BTNColour.TabIndex = 48;
            this.BTNColour.Text = "Select";
            this.BTNColour.UseVisualStyleBackColor = true;
            this.BTNColour.Click += new System.EventHandler(this.BTNColour_Click);
            // 
            // BTNUndoSingle
            // 
            this.BTNUndoSingle.Location = new System.Drawing.Point(207, 115);
            this.BTNUndoSingle.Name = "BTNUndoSingle";
            this.BTNUndoSingle.Size = new System.Drawing.Size(49, 19);
            this.BTNUndoSingle.TabIndex = 47;
            this.BTNUndoSingle.Text = "Undo";
            this.BTNUndoSingle.UseVisualStyleBackColor = true;
            this.BTNUndoSingle.Click += new System.EventHandler(this.BTNUndo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(16, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 19);
            this.label4.TabIndex = 46;
            this.label4.Text = "Colour:";
            // 
            // CMBProperty
            // 
            this.CMBProperty.BackColor = System.Drawing.SystemColors.Window;
            this.CMBProperty.FormattingEnabled = true;
            this.CMBProperty.Location = new System.Drawing.Point(84, 50);
            this.CMBProperty.Name = "CMBProperty";
            this.CMBProperty.Size = new System.Drawing.Size(124, 21);
            this.CMBProperty.Sorted = true;
            this.CMBProperty.TabIndex = 43;
            this.CMBProperty.Tag = "1";
            this.CMBProperty.SelectedIndexChanged += new System.EventHandler(this.CMBProperty_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(16, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 19);
            this.label3.TabIndex = 44;
            this.label3.Text = "Property:";
            // 
            // CMBControl
            // 
            this.CMBControl.BackColor = System.Drawing.SystemColors.Window;
            this.CMBControl.FormattingEnabled = true;
            this.CMBControl.Location = new System.Drawing.Point(84, 20);
            this.CMBControl.Name = "CMBControl";
            this.CMBControl.Size = new System.Drawing.Size(154, 21);
            this.CMBControl.TabIndex = 40;
            this.CMBControl.Tag = "1";
            this.CMBControl.SelectedIndexChanged += new System.EventHandler(this.CMBControl_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 19);
            this.label1.TabIndex = 41;
            this.label1.Text = "Control:";
            // 
            // DLGColour
            // 
            this.DLGColour.FullOpen = true;
            this.DLGColour.SolidColorOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(291, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 19);
            this.label2.TabIndex = 41;
            this.label2.Text = "Theme Preview";
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(-2, 1);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(1023, 34);
            this.PANTitle.TabIndex = 128;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(997, 4);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 125;
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
            this.LBLTitle.Size = new System.Drawing.Size(154, 17);
            this.LBLTitle.TabIndex = 0;
            this.LBLTitle.Text = "Theme Maintenance";
            // 
            // frmThemeMaintenance_Utilities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 480);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PANMain);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.STBStatus);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmThemeMaintenance_Utilities";
            this.Text = "Theme Maintenance";
            this.Load += new System.EventHandler(this.frmThemeMaintenance_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmThemeMaintenance_Paint);
            this.STBStatus.ResumeLayout(false);
            this.STBStatus.PerformLayout();
            this.PANMain.ResumeLayout(false);
            this.PANMain.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button BTNUndo;
        private StatusStrip STBStatus;
        private ToolStripStatusLabel STLStatus;
        private Button BTNSave;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel PANMain;
        private Button BTNUndoSingle;
        private ComboBox CMBColour;
        private Label label4;
        private ComboBox CMBProperty;
        private Label label3;
        private ComboBox CMBControl;
        private Label label1;
        private ColorDialog DLGColour;
        private Button BTNColour;
        private RadioButton RBSelectedColour;
        private Label LBLSelectedColour;
        private Button BTNPaste;
        private Button BTNCopy;
        private Label label2;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
    }
}