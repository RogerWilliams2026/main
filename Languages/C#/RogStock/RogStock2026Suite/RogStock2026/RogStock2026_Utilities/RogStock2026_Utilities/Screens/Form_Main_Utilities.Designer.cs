using System.Windows.Forms;
using System.Drawing;

namespace RogStock2026_Utilities.Screens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.TMRMenu = new System.Windows.Forms.Timer(this.components);
            this.BTNShowHide = new System.Windows.Forms.Button();
            this.PANOptions = new System.Windows.Forms.Panel();
            this.MNUOpenWindows = new System.Windows.Forms.MenuStrip();
            this.MNUWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.PANMenu = new System.Windows.Forms.Panel();
            this.MNUMainMenu = new System.Windows.Forms.MenuStrip();
            this.MNUMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUSections = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUMenuItems = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUMenuSecurity = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUThemeMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUUserGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.MNULogins = new System.Windows.Forms.ToolStripMenuItem();
            this.MNULoginMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUArrangeWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUExit = new System.Windows.Forms.ToolStripMenuItem();
            this.PANTitle = new System.Windows.Forms.Panel();
            this.BTNClose = new System.Windows.Forms.Button();
            this.LBLTitle = new System.Windows.Forms.Label();
            this.PANLine = new System.Windows.Forms.Panel();
            this.PANOptions.SuspendLayout();
            this.MNUOpenWindows.SuspendLayout();
            this.PANMenu.SuspendLayout();
            this.MNUMainMenu.SuspendLayout();
            this.PANTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // TMRMenu
            // 
            this.TMRMenu.Interval = 4000;
            // 
            // BTNShowHide
            // 
            this.BTNShowHide.BackColor = System.Drawing.Color.CadetBlue;
            this.BTNShowHide.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGreen;
            this.BTNShowHide.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Blue;
            this.BTNShowHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNShowHide.Location = new System.Drawing.Point(0, 36);
            this.BTNShowHide.Name = "BTNShowHide";
            this.BTNShowHide.Size = new System.Drawing.Size(28, 58);
            this.BTNShowHide.TabIndex = 8;
            this.BTNShowHide.UseVisualStyleBackColor = false;
            this.BTNShowHide.Click += new System.EventHandler(this.BTNShowHide_Click);
            this.BTNShowHide.Paint += new System.Windows.Forms.PaintEventHandler(this.BTNShowHide_Paint);
            // 
            // PANOptions
            // 
            this.PANOptions.BackColor = System.Drawing.Color.Teal;
            this.PANOptions.Controls.Add(this.MNUOpenWindows);
            this.PANOptions.Location = new System.Drawing.Point(27, 36);
            this.PANOptions.Name = "PANOptions";
            this.PANOptions.Size = new System.Drawing.Size(153, 27);
            this.PANOptions.TabIndex = 9;
            // 
            // MNUOpenWindows
            // 
            this.MNUOpenWindows.BackColor = System.Drawing.Color.SteelBlue;
            this.MNUOpenWindows.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNUWindows});
            this.MNUOpenWindows.Location = new System.Drawing.Point(0, 0);
            this.MNUOpenWindows.Name = "MNUOpenWindows";
            this.MNUOpenWindows.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.MNUOpenWindows.Size = new System.Drawing.Size(153, 27);
            this.MNUOpenWindows.TabIndex = 0;
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
            this.PANMenu.Location = new System.Drawing.Point(178, 36);
            this.PANMenu.Name = "PANMenu";
            this.PANMenu.Size = new System.Drawing.Size(282, 27);
            this.PANMenu.TabIndex = 10;
            // 
            // MNUMainMenu
            // 
            this.MNUMainMenu.BackColor = System.Drawing.Color.SteelBlue;
            this.MNUMainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MNUMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNUMenu,
            this.MNULogins,
            this.MNUArrangeWindows,
            this.MNUExit});
            this.MNUMainMenu.Location = new System.Drawing.Point(0, 0);
            this.MNUMainMenu.Name = "MNUMainMenu";
            this.MNUMainMenu.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.MNUMainMenu.Size = new System.Drawing.Size(282, 27);
            this.MNUMainMenu.TabIndex = 0;
            this.MNUMainMenu.Text = "menuStrip1";
            // 
            // MNUMenu
            // 
            this.MNUMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNUSections,
            this.MNUGroups,
            this.MNUMenuItems,
            this.MNUMenuSecurity,
            this.MNUThemeMaintenance,
            this.MNUUserGroups});
            this.MNUMenu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.MNUMenu.ForeColor = System.Drawing.Color.White;
            this.MNUMenu.Name = "MNUMenu";
            this.MNUMenu.Size = new System.Drawing.Size(58, 23);
            this.MNUMenu.Tag = "frmSections";
            this.MNUMenu.Text = "Menu";
            // 
            // MNUSections
            // 
            this.MNUSections.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MNUSections.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MNUSections.ForeColor = System.Drawing.Color.White;
            this.MNUSections.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MNUSections.Name = "MNUSections";
            this.MNUSections.Size = new System.Drawing.Size(249, 24);
            this.MNUSections.Tag = "frmSections_Utilities";
            this.MNUSections.Text = "Sections";
            // 
            // MNUGroups
            // 
            this.MNUGroups.ForeColor = System.Drawing.Color.White;
            this.MNUGroups.Name = "MNUGroups";
            this.MNUGroups.Size = new System.Drawing.Size(249, 24);
            this.MNUGroups.Tag = "frmGroups_Utilities";
            this.MNUGroups.Text = "Groups";
            // 
            // MNUMenuItems
            // 
            this.MNUMenuItems.ForeColor = System.Drawing.Color.White;
            this.MNUMenuItems.Name = "MNUMenuItems";
            this.MNUMenuItems.Size = new System.Drawing.Size(249, 24);
            this.MNUMenuItems.Tag = "frmMenuItems_Utilities";
            this.MNUMenuItems.Text = "Menu Items";
            // 
            // MNUMenuSecurity
            // 
            this.MNUMenuSecurity.ForeColor = System.Drawing.Color.White;
            this.MNUMenuSecurity.Name = "MNUMenuSecurity";
            this.MNUMenuSecurity.Size = new System.Drawing.Size(249, 24);
            this.MNUMenuSecurity.Tag = "frmMenuSecurity_Utilities";
            this.MNUMenuSecurity.Text = "Menu Security";
            // 
            // MNUThemeMaintenance
            // 
            this.MNUThemeMaintenance.ForeColor = System.Drawing.Color.White;
            this.MNUThemeMaintenance.Name = "MNUThemeMaintenance";
            this.MNUThemeMaintenance.Size = new System.Drawing.Size(249, 24);
            this.MNUThemeMaintenance.Tag = "frmThemeMaintenance_Utilities";
            this.MNUThemeMaintenance.Text = "Theme Maintenance";
            // 
            // MNUUserGroups
            // 
            this.MNUUserGroups.ForeColor = System.Drawing.Color.White;
            this.MNUUserGroups.Name = "MNUUserGroups";
            this.MNUUserGroups.Size = new System.Drawing.Size(249, 24);
            this.MNUUserGroups.Tag = "frmUserGroups_Utilities";
            this.MNUUserGroups.Text = "User Groups Maintenance";
            // 
            // MNULogins
            // 
            this.MNULogins.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNULoginMaintenance});
            this.MNULogins.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.MNULogins.ForeColor = System.Drawing.Color.White;
            this.MNULogins.Name = "MNULogins";
            this.MNULogins.Size = new System.Drawing.Size(64, 23);
            this.MNULogins.Text = "Logins";
            // 
            // MNULoginMaintenance
            // 
            this.MNULoginMaintenance.ForeColor = System.Drawing.Color.White;
            this.MNULoginMaintenance.Name = "MNULoginMaintenance";
            this.MNULoginMaintenance.Size = new System.Drawing.Size(204, 24);
            this.MNULoginMaintenance.Tag = "frmLoginMaintenance_Utilities";
            this.MNULoginMaintenance.Text = "Login Maintenance";
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
            // 
            // MNUCascade
            // 
            this.MNUCascade.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.MNUCascade.ForeColor = System.Drawing.Color.White;
            this.MNUCascade.Name = "MNUCascade";
            this.MNUCascade.Size = new System.Drawing.Size(149, 24);
            this.MNUCascade.Text = "Cascade";
            this.MNUCascade.Click += new System.EventHandler(this.MNUCascade_Click);
            // 
            // MNUHorizontal
            // 
            this.MNUHorizontal.ForeColor = System.Drawing.Color.White;
            this.MNUHorizontal.Name = "MNUHorizontal";
            this.MNUHorizontal.Size = new System.Drawing.Size(149, 24);
            this.MNUHorizontal.Text = "Horizontal";
            this.MNUHorizontal.Click += new System.EventHandler(this.MNUHorizontal_Click);
            // 
            // MNUVertical
            // 
            this.MNUVertical.ForeColor = System.Drawing.Color.White;
            this.MNUVertical.Name = "MNUVertical";
            this.MNUVertical.Size = new System.Drawing.Size(149, 24);
            this.MNUVertical.Text = "Vertical";
            this.MNUVertical.Click += new System.EventHandler(this.MNUVertical_Click);
            // 
            // MNUExit
            // 
            this.MNUExit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.MNUExit.ForeColor = System.Drawing.Color.White;
            this.MNUExit.Name = "MNUExit";
            this.MNUExit.Size = new System.Drawing.Size(45, 23);
            this.MNUExit.Text = "Exit";
            // 
            // PANTitle
            // 
            this.PANTitle.Controls.Add(this.BTNClose);
            this.PANTitle.Controls.Add(this.LBLTitle);
            this.PANTitle.Location = new System.Drawing.Point(2, 2);
            this.PANTitle.Name = "PANTitle";
            this.PANTitle.Size = new System.Drawing.Size(1180, 34);
            this.PANTitle.TabIndex = 125;
            this.PANTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseDown);
            this.PANTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseMove);
            this.PANTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PANTitle_MouseUp);
            // 
            // BTNClose
            // 
            this.BTNClose.FlatAppearance.BorderSize = 0;
            this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTNClose.Image = ((System.Drawing.Image)(resources.GetObject("BTNClose.Image")));
            this.BTNClose.Location = new System.Drawing.Point(1153, 3);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(22, 22);
            this.BTNClose.TabIndex = 125;
            this.BTNClose.UseVisualStyleBackColor = true;
            this.BTNClose.Click += new System.EventHandler(this.MNUExit_Click);
            // 
            // LBLTitle
            // 
            this.LBLTitle.AutoSize = true;
            this.LBLTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBLTitle.ForeColor = System.Drawing.Color.White;
            this.LBLTitle.Location = new System.Drawing.Point(9, 8);
            this.LBLTitle.Name = "LBLTitle";
            this.LBLTitle.Size = new System.Drawing.Size(147, 17);
            this.LBLTitle.TabIndex = 0;
            this.LBLTitle.Text = "RogStock - Utilities";
            // 
            // PANLine
            // 
            this.PANLine.BackColor = System.Drawing.Color.White;
            this.PANLine.Location = new System.Drawing.Point(34, 62);
            this.PANLine.Name = "PANLine";
            this.PANLine.Size = new System.Drawing.Size(152, 2);
            this.PANLine.TabIndex = 127;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1182, 750);
            this.Controls.Add(this.PANLine);
            this.Controls.Add(this.PANTitle);
            this.Controls.Add(this.PANMenu);
            this.Controls.Add(this.PANOptions);
            this.Controls.Add(this.BTNShowHide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MNUMainMenu;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "RogStock 2026 - Utilities";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.PANOptions.ResumeLayout(false);
            this.PANOptions.PerformLayout();
            this.MNUOpenWindows.ResumeLayout(false);
            this.MNUOpenWindows.PerformLayout();
            this.PANMenu.ResumeLayout(false);
            this.PANMenu.PerformLayout();
            this.MNUMainMenu.ResumeLayout(false);
            this.MNUMainMenu.PerformLayout();
            this.PANTitle.ResumeLayout(false);
            this.PANTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TMRMenu;
        private Button BTNShowHide;
        private Panel PANOptions;
        private Panel PANMenu;
        private MenuStrip MNUMainMenu;
        private ToolStripMenuItem MNUMenu;
        private ToolStripMenuItem MNULogins;
        private ToolStripMenuItem MNUExit;
        private ToolStripMenuItem MNUSections;
        private ToolStripMenuItem MNUGroups;
        private ToolStripMenuItem MNUMenuItems;
        private ToolStripMenuItem MNUMenuSecurity;
        private ToolStripMenuItem MNULoginMaintenance;
        private ToolStripMenuItem MNUThemeMaintenance;
        private ToolStripMenuItem MNUUserGroups;
        private MenuStrip MNUOpenWindows;
        private ToolStripMenuItem MNUWindows;
        private ToolStripMenuItem MNUArrangeWindows;
        private ToolStripMenuItem MNUCascade;
        private ToolStripMenuItem MNUHorizontal;
        private ToolStripMenuItem MNUVertical;
        private Panel PANTitle;
        private Button BTNClose;
        private Label LBLTitle;
        private Panel PANLine;
    }
}