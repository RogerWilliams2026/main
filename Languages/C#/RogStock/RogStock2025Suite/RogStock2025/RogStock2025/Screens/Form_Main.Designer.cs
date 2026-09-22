using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
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
            this.components = new System.ComponentModel.Container();
            this.TMRMenu = new System.Windows.Forms.Timer(this.components);
            this.PANOptions = new System.Windows.Forms.Panel();
            this.MNUOpenWindows = new System.Windows.Forms.MenuStrip();
            this.MNUWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.PANMenu = new System.Windows.Forms.Panel();
            this.MNUMainMenu = new System.Windows.Forms.MenuStrip();
            this.MNUForms = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUReports = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUArrangeWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.PANSections = new System.Windows.Forms.Panel();
            this.PANExit = new System.Windows.Forms.Panel();
            this.BTNExit = new System.Windows.Forms.Button();
            this.BTNShowHide = new System.Windows.Forms.Button();
            this.PANOptions.SuspendLayout();
            this.MNUOpenWindows.SuspendLayout();
            this.PANMenu.SuspendLayout();
            this.MNUMainMenu.SuspendLayout();
            this.PANSections.SuspendLayout();
            this.PANExit.SuspendLayout();
            this.SuspendLayout();
            // 
            // TMRMenu
            // 
            this.TMRMenu.Interval = 4000;
            this.TMRMenu.Tick += new System.EventHandler(this.TMRMenu_Tick);
            // 
            // PANOptions
            // 
            this.PANOptions.BackColor = System.Drawing.Color.Teal;
            this.PANOptions.Controls.Add(this.MNUOpenWindows);
            this.PANOptions.ForeColor = System.Drawing.Color.White;
            this.PANOptions.Location = new System.Drawing.Point(31, 0);
            this.PANOptions.Margin = new System.Windows.Forms.Padding(4);
            this.PANOptions.Name = "PANOptions";
            this.PANOptions.Size = new System.Drawing.Size(268, 59);
            this.PANOptions.TabIndex = 8;
            // 
            // MNUOpenWindows
            // 
            this.MNUOpenWindows.BackColor = System.Drawing.Color.SteelBlue;
            this.MNUOpenWindows.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNUWindows});
            this.MNUOpenWindows.Location = new System.Drawing.Point(0, 0);
            this.MNUOpenWindows.Name = "MNUOpenWindows";
            this.MNUOpenWindows.Size = new System.Drawing.Size(268, 27);
            this.MNUOpenWindows.TabIndex = 1;
            this.MNUOpenWindows.Text = "menuStrip1";
            // 
            // MNUWindows
            // 
            this.MNUWindows.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.MNUWindows.ForeColor = System.Drawing.Color.White;
            this.MNUWindows.Name = "MNUWindows";
            this.MNUWindows.Size = new System.Drawing.Size(122, 23);
            this.MNUWindows.Text = "Open Windows";
            // 
            // PANMenu
            // 
            this.PANMenu.BackColor = System.Drawing.Color.Teal;
            this.PANMenu.Controls.Add(this.MNUMainMenu);
            this.PANMenu.ForeColor = System.Drawing.Color.White;
            this.PANMenu.Location = new System.Drawing.Point(299, 0);
            this.PANMenu.Margin = new System.Windows.Forms.Padding(4);
            this.PANMenu.Name = "PANMenu";
            this.PANMenu.Size = new System.Drawing.Size(388, 59);
            this.PANMenu.TabIndex = 9;
            // 
            // MNUMainMenu
            // 
            this.MNUMainMenu.BackColor = System.Drawing.Color.SteelBlue;
            this.MNUMainMenu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.MNUMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNUForms,
            this.MNUReports,
            this.MNUOperations,
            this.MNUArrangeWindows});
            this.MNUMainMenu.Location = new System.Drawing.Point(0, 0);
            this.MNUMainMenu.Name = "MNUMainMenu";
            this.MNUMainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MNUMainMenu.Size = new System.Drawing.Size(388, 27);
            this.MNUMainMenu.TabIndex = 0;
            this.MNUMainMenu.Text = "menuStrip1";
            // 
            // MNUForms
            // 
            this.MNUForms.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.MNUForms.ForeColor = System.Drawing.Color.White;
            this.MNUForms.Name = "MNUForms";
            this.MNUForms.Size = new System.Drawing.Size(68, 23);
            this.MNUForms.Text = "Forrms";
            // 
            // MNUReports
            // 
            this.MNUReports.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.MNUReports.ForeColor = System.Drawing.Color.White;
            this.MNUReports.Name = "MNUReports";
            this.MNUReports.Size = new System.Drawing.Size(73, 23);
            this.MNUReports.Text = "Reports";
            // 
            // MNUOperations
            // 
            this.MNUOperations.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.MNUOperations.ForeColor = System.Drawing.Color.White;
            this.MNUOperations.Name = "MNUOperations";
            this.MNUOperations.Size = new System.Drawing.Size(95, 23);
            this.MNUOperations.Text = "Operations";
            // 
            // MNUArrangeWindows
            // 
            this.MNUArrangeWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNUCascade,
            this.MNUHorizontal,
            this.MNUVertical});
            this.MNUArrangeWindows.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.MNUArrangeWindows.ForeColor = System.Drawing.Color.White;
            this.MNUArrangeWindows.Name = "MNUArrangeWindows";
            this.MNUArrangeWindows.Size = new System.Drawing.Size(141, 23);
            this.MNUArrangeWindows.Text = "Arrange Windows";
            this.MNUArrangeWindows.Visible = false;
            // 
            // MNUCascade
            // 
            this.MNUCascade.ForeColor = System.Drawing.Color.White;
            this.MNUCascade.Name = "MNUCascade";
            this.MNUCascade.Size = new System.Drawing.Size(180, 24);
            this.MNUCascade.Text = "Cascade";
            this.MNUCascade.Click += new System.EventHandler(this.MNUCascade_Click);
            // 
            // MNUHorizontal
            // 
            this.MNUHorizontal.ForeColor = System.Drawing.Color.White;
            this.MNUHorizontal.Name = "MNUHorizontal";
            this.MNUHorizontal.Size = new System.Drawing.Size(180, 24);
            this.MNUHorizontal.Text = "Horizontal";
            this.MNUHorizontal.Click += new System.EventHandler(this.MNUHorizontal_Click);
            // 
            // MNUVertical
            // 
            this.MNUVertical.ForeColor = System.Drawing.Color.White;
            this.MNUVertical.Name = "MNUVertical";
            this.MNUVertical.Size = new System.Drawing.Size(180, 24);
            this.MNUVertical.Text = "Vertical";
            this.MNUVertical.Click += new System.EventHandler(this.MNUVertical_Click);
            // 
            // PANSections
            // 
            this.PANSections.BackColor = System.Drawing.Color.Teal;
            this.PANSections.Controls.Add(this.PANExit);
            this.PANSections.ForeColor = System.Drawing.Color.White;
            this.PANSections.Location = new System.Drawing.Point(0, 55);
            this.PANSections.Margin = new System.Windows.Forms.Padding(4);
            this.PANSections.Name = "PANSections";
            this.PANSections.Size = new System.Drawing.Size(152, 488);
            this.PANSections.TabIndex = 10;
            // 
            // PANExit
            // 
            this.PANExit.Controls.Add(this.BTNExit);
            this.PANExit.Location = new System.Drawing.Point(1, 432);
            this.PANExit.Name = "PANExit";
            this.PANExit.Size = new System.Drawing.Size(142, 48);
            this.PANExit.TabIndex = 11;
            // 
            // BTNExit
            // 
            this.BTNExit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BTNExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.ForestGreen;
            this.BTNExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.OliveDrab;
            this.BTNExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNExit.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNExit.Location = new System.Drawing.Point(9, 11);
            this.BTNExit.Name = "BTNExit";
            this.BTNExit.Size = new System.Drawing.Size(124, 27);
            this.BTNExit.TabIndex = 1;
            this.BTNExit.Text = "Exit";
            this.BTNExit.UseVisualStyleBackColor = true;
            this.BTNExit.Click += new System.EventHandler(this.BTNExit_Click);
            // 
            // BTNShowHide
            // 
            this.BTNShowHide.BackColor = System.Drawing.Color.CadetBlue;
            this.BTNShowHide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGreen;
            this.BTNShowHide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.BTNShowHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNShowHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNShowHide.ForeColor = System.Drawing.Color.SteelBlue;
            this.BTNShowHide.Location = new System.Drawing.Point(0, 0);
            this.BTNShowHide.Margin = new System.Windows.Forms.Padding(4);
            this.BTNShowHide.Name = "BTNShowHide";
            this.BTNShowHide.Size = new System.Drawing.Size(33, 58);
            this.BTNShowHide.TabIndex = 11;
            this.BTNShowHide.UseVisualStyleBackColor = false;
            this.BTNShowHide.Click += new System.EventHandler(this.BTNShowHide_Click);
            this.BTNShowHide.Paint += new System.Windows.Forms.PaintEventHandler(this.BTNShowHide_Paint);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.DarkKhaki;
            this.ClientSize = new System.Drawing.Size(1698, 923);
            this.Controls.Add(this.BTNShowHide);
            this.Controls.Add(this.PANMenu);
            this.Controls.Add(this.PANSections);
            this.Controls.Add(this.PANOptions);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MNUMainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RogStock 2025";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.PANOptions.ResumeLayout(false);
            this.PANOptions.PerformLayout();
            this.MNUOpenWindows.ResumeLayout(false);
            this.MNUOpenWindows.PerformLayout();
            this.PANMenu.ResumeLayout(false);
            this.PANMenu.PerformLayout();
            this.MNUMainMenu.ResumeLayout(false);
            this.MNUMainMenu.PerformLayout();
            this.PANSections.ResumeLayout(false);
            this.PANExit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TMRMenu;
        private System.Windows.Forms.Panel PANOptions;
        private System.Windows.Forms.Panel PANMenu;
        private System.Windows.Forms.Panel PANSections;
        private System.Windows.Forms.Button BTNShowHide;
        private System.Windows.Forms.ComboBox CMBOpenScreens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip MNUMainMenu;
        private System.Windows.Forms.ToolStripMenuItem MNUForms;
        private System.Windows.Forms.ToolStripMenuItem MNUReports;
        private System.Windows.Forms.ToolStripMenuItem MNUOperations;
        private Panel PANExit;
        private Button BTNExit;
        private MenuStrip MNUOpenWindows;
        private ToolStripMenuItem MNUWindows;
        private ToolStripMenuItem MNUArrangeWindows;
        private ToolStripMenuItem MNUCascade;
        private ToolStripMenuItem MNUHorizontal;
        private ToolStripMenuItem MNUVertical;
    }
}