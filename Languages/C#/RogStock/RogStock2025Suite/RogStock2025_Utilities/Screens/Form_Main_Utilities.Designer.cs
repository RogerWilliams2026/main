namespace RogStock2025_Utilities.Screens
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
            components = new System.ComponentModel.Container();
            TMRMenu = new System.Windows.Forms.Timer(components);
            BTNShowHide = new Button();
            PANOptions = new Panel();
            MNUOpenWindows = new MenuStrip();
            MNUWindows = new ToolStripMenuItem();
            PANMenu = new Panel();
            MNUMainMenu = new MenuStrip();
            MNUMenu = new ToolStripMenuItem();
            MNUSections = new ToolStripMenuItem();
            MNUGroups = new ToolStripMenuItem();
            MNUMenuItems = new ToolStripMenuItem();
            MNUMenuSecurity = new ToolStripMenuItem();
            MNUThemeMaintenance = new ToolStripMenuItem();
            MNUUserGroups = new ToolStripMenuItem();
            MNULogins = new ToolStripMenuItem();
            MNULoginMaintenance = new ToolStripMenuItem();
            MNUArrangeWindows = new ToolStripMenuItem();
            MNUCascade = new ToolStripMenuItem();
            MNUHorizontal = new ToolStripMenuItem();
            MNUVertical = new ToolStripMenuItem();
            MNUExit = new ToolStripMenuItem();
            PANOptions.SuspendLayout();
            MNUOpenWindows.SuspendLayout();
            PANMenu.SuspendLayout();
            MNUMainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // TMRMenu
            // 
            TMRMenu.Interval = 4000;
            TMRMenu.Tick += TMRMenu_Tick;
            // 
            // BTNShowHide
            // 
            BTNShowHide.BackColor = Color.DarkTurquoise;
            BTNShowHide.FlatAppearance.MouseDownBackColor = Color.LightGreen;
            BTNShowHide.FlatAppearance.MouseOverBackColor = Color.Blue;
            BTNShowHide.FlatStyle = FlatStyle.Flat;
            BTNShowHide.Location = new Point(0, 0);
            BTNShowHide.Name = "BTNShowHide";
            BTNShowHide.Size = new Size(33, 58);
            BTNShowHide.TabIndex = 8;
            BTNShowHide.UseVisualStyleBackColor = false;
            BTNShowHide.Click += BTNShowHide_Click;
            BTNShowHide.Paint += BTNShowHide_Paint;
            // 
            // PANOptions
            // 
            PANOptions.BackColor = Color.Teal;
            PANOptions.Controls.Add(MNUOpenWindows);
            PANOptions.Location = new Point(31, 0);
            PANOptions.Name = "PANOptions";
            PANOptions.Size = new Size(178, 59);
            PANOptions.TabIndex = 9;
            // 
            // MNUOpenWindows
            // 
            MNUOpenWindows.BackColor = Color.SteelBlue;
            MNUOpenWindows.Items.AddRange(new ToolStripItem[] { MNUWindows });
            MNUOpenWindows.Location = new Point(0, 0);
            MNUOpenWindows.Name = "MNUOpenWindows";
            MNUOpenWindows.Size = new Size(178, 27);
            MNUOpenWindows.TabIndex = 0;
            MNUOpenWindows.Text = "menuStrip1";
            // 
            // MNUWindows
            // 
            MNUWindows.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MNUWindows.ForeColor = Color.White;
            MNUWindows.Name = "MNUWindows";
            MNUWindows.Size = new Size(122, 23);
            MNUWindows.Text = "Open Windows";
            // 
            // PANMenu
            // 
            PANMenu.BackColor = Color.Teal;
            PANMenu.Controls.Add(MNUMainMenu);
            PANMenu.Location = new Point(208, 0);
            PANMenu.Name = "PANMenu";
            PANMenu.Size = new Size(329, 59);
            PANMenu.TabIndex = 10;
            // 
            // MNUMainMenu
            // 
            MNUMainMenu.BackColor = Color.SteelBlue;
            MNUMainMenu.BackgroundImageLayout = ImageLayout.None;
            MNUMainMenu.Items.AddRange(new ToolStripItem[] { MNUMenu, MNULogins, MNUArrangeWindows, MNUExit });
            MNUMainMenu.Location = new Point(0, 0);
            MNUMainMenu.Name = "MNUMainMenu";
            MNUMainMenu.Size = new Size(329, 27);
            MNUMainMenu.TabIndex = 0;
            MNUMainMenu.Text = "menuStrip1";
            // 
            // MNUMenu
            // 
            MNUMenu.DropDownItems.AddRange(new ToolStripItem[] { MNUSections, MNUGroups, MNUMenuItems, MNUMenuSecurity, MNUThemeMaintenance, MNUUserGroups });
            MNUMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MNUMenu.ForeColor = Color.White;
            MNUMenu.Name = "MNUMenu";
            MNUMenu.Size = new Size(58, 23);
            MNUMenu.Tag = "frmSections";
            MNUMenu.Text = "Menu";
            // 
            // MNUSections
            // 
            MNUSections.BackgroundImageLayout = ImageLayout.None;
            MNUSections.DisplayStyle = ToolStripItemDisplayStyle.Text;
            MNUSections.ForeColor = Color.White;
            MNUSections.ImageScaling = ToolStripItemImageScaling.None;
            MNUSections.Name = "MNUSections";
            MNUSections.Size = new Size(249, 24);
            MNUSections.Tag = "frmSections_Utilities";
            MNUSections.Text = "Sections";
            // 
            // MNUGroups
            // 
            MNUGroups.ForeColor = Color.White;
            MNUGroups.Name = "MNUGroups";
            MNUGroups.Size = new Size(249, 24);
            MNUGroups.Tag = "frmGroups_Utilities";
            MNUGroups.Text = "Groups";
            // 
            // MNUMenuItems
            // 
            MNUMenuItems.ForeColor = Color.White;
            MNUMenuItems.Name = "MNUMenuItems";
            MNUMenuItems.Size = new Size(249, 24);
            MNUMenuItems.Tag = "frmMenuItems_Utilities";
            MNUMenuItems.Text = "Menu Items";
            // 
            // MNUMenuSecurity
            // 
            MNUMenuSecurity.ForeColor = Color.White;
            MNUMenuSecurity.Name = "MNUMenuSecurity";
            MNUMenuSecurity.Size = new Size(249, 24);
            MNUMenuSecurity.Tag = "frmMenuSecurity_Utilities";
            MNUMenuSecurity.Text = "Menu Security";
            // 
            // MNUThemeMaintenance
            // 
            MNUThemeMaintenance.ForeColor = Color.White;
            MNUThemeMaintenance.Name = "MNUThemeMaintenance";
            MNUThemeMaintenance.Size = new Size(249, 24);
            MNUThemeMaintenance.Tag = "frmThemeMaintenance_Utilities";
            MNUThemeMaintenance.Text = "Theme Maintenance";
            // 
            // MNUUserGroups
            // 
            MNUUserGroups.ForeColor = Color.White;
            MNUUserGroups.Name = "MNUUserGroups";
            MNUUserGroups.Size = new Size(249, 24);
            MNUUserGroups.Tag = "frmUserGroups_Utilities";
            MNUUserGroups.Text = "User Groups Maintenance";
            // 
            // MNULogins
            // 
            MNULogins.DropDownItems.AddRange(new ToolStripItem[] { MNULoginMaintenance });
            MNULogins.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MNULogins.ForeColor = Color.White;
            MNULogins.Name = "MNULogins";
            MNULogins.Size = new Size(64, 23);
            MNULogins.Text = "Logins";
            // 
            // MNULoginMaintenance
            // 
            MNULoginMaintenance.ForeColor = Color.White;
            MNULoginMaintenance.Name = "MNULoginMaintenance";
            MNULoginMaintenance.Size = new Size(204, 24);
            MNULoginMaintenance.Tag = "frmLoginMaintenance_Utilities";
            MNULoginMaintenance.Text = "Login Maintenance";
            // 
            // MNUArrangeWindows
            // 
            MNUArrangeWindows.DropDownItems.AddRange(new ToolStripItem[] { MNUCascade, MNUHorizontal, MNUVertical });
            MNUArrangeWindows.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MNUArrangeWindows.ForeColor = Color.White;
            MNUArrangeWindows.Name = "MNUArrangeWindows";
            MNUArrangeWindows.Size = new Size(141, 23);
            MNUArrangeWindows.Text = "Arrange Windows";
            // 
            // MNUCascade
            // 
            MNUCascade.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MNUCascade.ForeColor = Color.White;
            MNUCascade.Name = "MNUCascade";
            MNUCascade.Size = new Size(180, 24);
            MNUCascade.Text = "Cascade";
            MNUCascade.Click += MNUCascade_Click;
            // 
            // MNUHorizontal
            // 
            MNUHorizontal.ForeColor = Color.White;
            MNUHorizontal.Name = "MNUHorizontal";
            MNUHorizontal.Size = new Size(180, 24);
            MNUHorizontal.Text = "Horizontal";
            MNUHorizontal.Click += MNUHorizontal_Click;
            // 
            // MNUVertical
            // 
            MNUVertical.ForeColor = Color.White;
            MNUVertical.Name = "MNUVertical";
            MNUVertical.Size = new Size(180, 24);
            MNUVertical.Text = "Vertical";
            MNUVertical.Click += MNUVertical_Click;
            // 
            // MNUExit
            // 
            MNUExit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MNUExit.ForeColor = Color.White;
            MNUExit.Name = "MNUExit";
            MNUExit.Size = new Size(45, 23);
            MNUExit.Text = "Exit";
            MNUExit.Click += MNUExit_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(1379, 865);
            Controls.Add(PANMenu);
            Controls.Add(PANOptions);
            Controls.Add(BTNShowHide);
            IsMdiContainer = true;
            MainMenuStrip = MNUMainMenu;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "frmMain";
            Text = "RogStock 2025 - Utilities";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            PANOptions.ResumeLayout(false);
            PANOptions.PerformLayout();
            MNUOpenWindows.ResumeLayout(false);
            MNUOpenWindows.PerformLayout();
            PANMenu.ResumeLayout(false);
            PANMenu.PerformLayout();
            MNUMainMenu.ResumeLayout(false);
            MNUMainMenu.PerformLayout();
            ResumeLayout(false);

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
    }
}