using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using static RogStock2025.Modules.clsView;
//using static System.Net.Mime.MediaTypeNames;


namespace RogStock2025.Screens
{
    public partial class frmMain : Form
    {       /*
                   Modified 28/07/2025 By Roger Williams
        
                   Now supports theme colours for forms (not main menu yet)
                   Menu options are dynamically created from users records in:  Menu_UsersGroups

                  
        
                   Created 13/02/2025 By Roger Williams

                   Main menu screen!


                   Created 12/02/2025 By Roger Williams

                   RogStock 2025!
        
                   Simple ISO 9001 stock system written in C#

                    Still in development, purposely posted on GITHub early so people can follow the
                    developemnt of the project via the branches, with the latest branch being the
                    latest version of the project


                    Features Include:
                    - Uses SQL Server Reporting Services for reports
                    - SQL Server backend
                    - custom user colour scheme!
                    - Location/Lot tracking
                    - UOM
                    - Product families
                    - Location renaming


            */


        //used by "mainmenu" button
        Brush bruShowHide1 = new SolidBrush(Color.White);
        Brush bruShowHide2 = new SolidBrush(Color.GreenYellow);
        Brush bruShowHide3 = new SolidBrush(Color.Yellow);
        Brush bruShowHide4 = new SolidBrush(Color.LightBlue);
        Point pntShowHide = new Point(10, 10);
        Font fntShowHide = new Font("Segoe UI", 11, FontStyle.Bold);
        int intColourSwap = 0;

        ////used by "mainmenu"
        bool blnShowMenu = false;
        Brush bruMenu = new SolidBrush(Color.White);
        Point pntMenu = new Point(10, 4);
        Font fntMenu = new Font("Segoe UI", 10, FontStyle.Bold);

        int intPANSectionsheight = 0;




        public frmMain()
        {
            InitializeComponent();
            //apply colour "theme" to menu
            this.MNUMainMenu.Renderer = new Modules.clsView.clsMenuColourScheme();

        }


        //****custom subs/funcs ***********+

        private void Custom_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rctTemp = e.ClipRectangle;

            if (sender is ToolStripMenuItem)
            {
                e.Graphics.FillRectangle(Brushes.SteelBlue, rctTemp);
                e.Graphics.DrawString(((ToolStripMenuItem)sender).Text, fntMenu, bruMenu, pntMenu);
            }
            if (sender is MenuStrip)
            {
                e.Graphics.FillRectangle(Brushes.SteelBlue, rctTemp);
            }
        }

        public void WindowMenuClickEvent(object sender, EventArgs e)
        {
            /*

                  Created 07/08/2025 By Roger Williams
           
                  Opens menu item for Windows menu

            */

            //discover which button

            //            ShowMenuItem(this.MNUItemListing.Text);

            if (sender is ToolStripMenuItem)
            {
                if (((ToolStripMenuItem)sender).Name == "MNUWindows")
                {
                    return;
                }

                Modules.clsView.ShowForm(((ToolStripMenuItem)sender).Text);
            }
        }

        private void MenuClickEvent(object sender, EventArgs e)
        {
            /*

                  Created 05/08/2025 By Roger Williams
           
                  Opens menu item

            */

            //discover which button

            //            ShowMenuItem(this.MNUItemListing.Text);


            if (((ToolStripMenuItem)sender).Tag == null)
            {
                return;
            }

            Modules.clsView.OpenForm(((ToolStripMenuItem)sender).Tag.ToString());

        }

        private void MenuButtonClickEvent(object sender, EventArgs e)
        {
            /*

                  Created 05/08/2025 By Roger Williams
           
                  Populates menus with user menu items
                  Opens first visible menu e.g. Forms so user knows menu items present
            

            */

            bool blnCont = true;
            int intNum = 0;
            ToolStripMenuItem MNUTemp = null;
            string strTemp = String.Empty;

            //clear existing mneu items
            this.MNUForms.DropDownItems.Clear();
            this.MNUReports.DropDownItems.Clear();
            this.MNUOperations.DropDownItems.Clear();

            //go through array and create menu items for each user menu item
            //caption will be menu item name tag will be menu object name


            //create form menu elements
            for (intNum = 1; aryUserMenuItems[0, intNum, 0, 0] != null; intNum++)
            {
                blnCont = true;

                //get forms
                if (aryUserMenuItems[0, intNum, 0, 0] == "Form" && aryUserMenuItems[intNum, 0, 0, 0] == ((Button)sender).Text)
                {
                    //make sure does not already exist!
                    foreach (ToolStripMenuItem MNUFind in this.MNUForms.DropDownItems)
                    {
                        strTemp = aryUserMenuItems[0, 0, intNum, 0];

                        if (MNUFind.Text == aryUserMenuItems[0, 0, intNum, 0])
                        {
                            blnCont = false;
                            break;
                        }
                    }

                    if (blnCont)
                    {
                        MNUTemp = new ToolStripMenuItem();
                        MNUTemp.Text = aryUserMenuItems[0, 0, intNum, 0];
                        MNUTemp.Tag = aryUserMenuItems[0, 0, 0, intNum]; //object name
                        MNUTemp.Click += MenuClickEvent;
                        MNUTemp.ForeColor = Color.White;
                        this.MNUForms.DropDownItems.Add(MNUTemp);
                    }
                }

                //get operations
                if (aryUserMenuItems[0, intNum, 0, 0] == "Operation" && aryUserMenuItems[intNum, 0, 0, 0] == ((Button)sender).Text)
                {
                    //make sure does not already exist!
                    foreach (ToolStripMenuItem MNUFind in this.MNUOperations.DropDownItems)
                    {
                        strTemp = aryUserMenuItems[0, 0, intNum, 0];

                        if (MNUFind.Text == aryUserMenuItems[0, 0, intNum, 0])
                        {
                            blnCont = false;
                            break;
                        }
                    }

                    if (blnCont)
                    {
                        MNUTemp = new ToolStripMenuItem();
                        MNUTemp.Text = aryUserMenuItems[0, 0, intNum, 0];
                        MNUTemp.Tag = aryUserMenuItems[0, 0, 0, intNum]; //object name
                        MNUTemp.Click += MenuClickEvent;
                        MNUTemp.ForeColor = Color.White;
                        this.MNUOperations.DropDownItems.Add(MNUTemp);
                    }
                }

                //get reports
                if (aryUserMenuItems[0, intNum, 0, 0] == "Report" && aryUserMenuItems[intNum, 0, 0, 0] == ((Button)sender).Text)
                {
                    //make sure does not already exist!
                    foreach (ToolStripMenuItem MNUFind in this.MNUReports.DropDownItems)
                    {
                        strTemp = aryUserMenuItems[0, 0, intNum, 0];

                        if (MNUFind.Text == aryUserMenuItems[0, 0, intNum, 0])
                        {
                            blnCont = false;
                            break;
                        }
                    }

                    if (blnCont)
                    {
                        MNUTemp = new ToolStripMenuItem();
                        MNUTemp.Text = aryUserMenuItems[0, 0, intNum, 0];
                        MNUTemp.Tag = aryUserMenuItems[0, 0, 0, intNum]; //object name
                        MNUTemp.Click += MenuClickEvent;
                        MNUTemp.ForeColor = Color.White;
                        this.MNUReports.DropDownItems.Add(MNUTemp);
                    }
                }
            }


            //show form/report/operation menu if items for user contained
            this.MNUForms.Visible = Modules.clsView.HasUserAccessToForms(((Button)sender).Text);
            this.MNUOperations.Visible = Modules.clsView.HasUserAccessToOperations(((Button)sender).Text);
            this.MNUReports.Visible = Modules.clsView.HasUserAccessToReports(((Button)sender).Text);
        }

        private void SetFormColour()
        {
            /*

                  Created 19/08/2025 By Roger Williams
           
                  Taken off the internet sets MDI parent back colour!

            */

            MdiClient cliMDI;
            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    cliMDI = (MdiClient)ctl;
                    // Set the BackColor of the MdiClient control.
                    cliMDI.BackColor = Color.SteelBlue;
                }
                catch (InvalidCastException ex)
                {
                    // Catch and ignore the error if casting failed.
                }
            }
        }
        private void CreateMainMenu()
        {
            /*

                  Created 05/08/2025 By Roger Williams

                  Gets list of AlL menu items user has access too then creates the section/menu items for them  
                  in: PANSections

                  as a series of buttons whose caption is the area e.g.: Inventory
                  the user menu items data is stored in an array: aryUserMenuItems, this contains:

                  area            (e.g. Inventory)    
                  type            (e.g. Form/Report)
                  menu item name  (e.g. Stock Inventory)
                  object name     (e.g. frmStockItems)

                 

            */

            string strSQL = "SELECT Menu_Areas.SEC_Area, Menu_MenuItems.MNU_MenuItemName, Menu_MenuItems.MNU_MenuItemObject, Menu_MenuItems.MNU_Type, " +
                            "Menu_MenuItems.MNU_DisplayWhere, Menu_Groups.GRP_MenuItem, Menu_UsersGroups.USRGRP__ID, Menu_UsersGroups.USRGRP_User " +
                            "FROM Menu_Areas INNER JOIN " +
                            "Menu_MenuItems ON Menu_Areas.SEC_Area = Menu_MenuItems.MNU_DisplayWhere INNER JOIN " +
                            "Menu_Groups ON Menu_MenuItems.MNU_MenuItemName = Menu_Groups.GRP_MenuItem INNER JOIN " +
                            "Menu_UsersGroups ON Menu_Groups.GRP_Group = Menu_UsersGroups.USRGRP_Group " +
                            "WHERE Menu_UsersGroups.USRGRP_User ='" + Modules.clsData.strLoggedInUser + "' " +
                            "ORDER BY Menu_MenuItems.MNU_DisplayWhere, Menu_MenuItems.MNU_Type, Menu_MenuItems.MNU_MenuItemName;";
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SDRTable = null;
            Button BTNTemp = null;
            int intNum = 1;
            int intButtonTop = 2;
            int CNST_INT_BUTTONHEIGHT = 33;
            int CNST_INT_BUTTONLEFT = 9;
            int CNST_INT_BUTTONWIDTH = 124;
            int CNST_INT_BUTTONSPACING = 4;

            //first clear PANSections of all buttons
            foreach (Control ctlTemp in this.PANSections.Controls)
            {
                if (ctlTemp is Button)
                {
                    this.PANSections.Controls.Remove(ctlTemp);
                }
            }

            //hide menu items
            this.MNUForms.Visible = false;
            this.MNUOperations.Visible = false;
            this.MNUReports.Visible = false;

            //first get sections and create menu buttons

            //Microsoft Sans Serif  10
            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = new SqlCommand("SELECT * FROM Menu_Areas ORDER BY SEC_Area", SQLConn);
                    //read records
                    SDRTable = SQLCmd.ExecuteReader();

                    while (SDRTable.Read())
                    {
                        BTNTemp = new Button();
                        BTNTemp.Text = SDRTable["SEC_Area"].ToString();
                        BTNTemp.Name = "BTN" + SDRTable["SEC_Area"].ToString();
                        //set click event
                        BTNTemp.Click += MenuButtonClickEvent;
                        BTNTemp.Left = CNST_INT_BUTTONLEFT;
                        BTNTemp.Width = CNST_INT_BUTTONWIDTH;
                        BTNTemp.Height = CNST_INT_BUTTONHEIGHT;
                        BTNTemp.Top = intButtonTop;
                        intButtonTop = intButtonTop + CNST_INT_BUTTONHEIGHT + CNST_INT_BUTTONSPACING;
                        BTNTemp.Font = new Font("Microsoft Sans Serif", 10);
                        //set button colours
                        BTNTemp.FlatStyle = FlatStyle.Flat;
                        BTNTemp.BackColor = Color.Teal;
                        BTNTemp.ForeColor = Color.White;
                        BTNTemp.FlatAppearance.MouseDownBackColor = Color.LightGreen;
                        BTNTemp.FlatAppearance.MouseOverBackColor = Color.DeepSkyBlue;
                        BTNTemp.Visible = true;
                        this.PANSections.Controls.Add(BTNTemp);

                        //check if need to expand PANSections
                        if (intButtonTop >= 432)
                        {
                            //increase panel size
                            this.PANSections.Height = this.PANSections.Height + CNST_INT_BUTTONHEIGHT + CNST_INT_BUTTONSPACING;
                            this.PANExit.Top = this.PANExit.Top + CNST_INT_BUTTONHEIGHT + CNST_INT_BUTTONSPACING;
                        }
                    }

                    SDRTable.Close();

                    intPANSectionsheight = this.PANSections.Height;
                    //"hide" PANSections
                    this.PANSections.Height = 0;
                    //activate form timer for animation
                    this.TMRMenu.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //now get user menu items
            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = new SqlCommand(strSQL, SQLConn);
                    //read records
                    SDRTable = SQLCmd.ExecuteReader();

                    while (SDRTable.Read())
                    {
                        /*
                            area            (e.g. Inventory)    
                            type            (e.g. Form/Report)
                            menu item name  (e.g. Stock Inventory)
                            object name     (e.g. frmStockItems)
                        */
                        Modules.clsView.aryUserMenuItems[intNum, 0, 0, 0] = SDRTable["MNU_DisplayWhere"].ToString();
                        Modules.clsView.aryUserMenuItems[0, intNum, 0, 0] = SDRTable["MNU_Type"].ToString();
                        Modules.clsView.aryUserMenuItems[0, 0, intNum, 0] = SDRTable["MNU_MenuItemName"].ToString();
                        Modules.clsView.aryUserMenuItems[0, 0, 0, intNum] = SDRTable["MNU_MenuItemObject"].ToString();
                        intNum++;
                    }

                    SDRTable.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SetButtonColours()
        {
            /*
                   Created 18/08/2025 By Roger Williams

                   sets hide/show and exit button colours
                   done here as flatappearance colours  can be fiddly to find!
                   
            */

            //set how/hide button colours
            this.BTNShowHide.BackColor = Color.CadetBlue;
            this.BTNShowHide.ForeColor = Color.SteelBlue;
            this.BTNShowHide.FlatAppearance.MouseOverBackColor = Color.DeepSkyBlue;
            this.BTNShowHide.FlatAppearance.MouseDownBackColor = Color.LightGreen;
            //set exit button colours
            this.BTNExit.BackColor = Color.Teal;
            this.BTNExit.ForeColor = Color.White;
            this.BTNExit.FlatAppearance.MouseDownBackColor = Color.LightGreen;
            this.BTNExit.FlatAppearance.MouseOverBackColor = Color.DeepSkyBlue;
        }

        //*******form events*******
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


                   
            */
            Modules.clsData.DeleteCurrentLoginRecord();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            /*
                   Created 13/02/2025 By Roger Williams

                   Show login screen

            */
            frmLogin frmTemp;
            Rectangle RECTTemp;

            //load theme
            Modules.clsView.ReadThemeData();
            //open login screen
            frmTemp = new frmLogin();
            frmTemp.ShowDialog();


            //init custom sql error data
            if (!Modules.clsData.InitCustomErrorhandler(Modules.clsData.CNST_STR_SQLCUSTOMERRORSPATH))  // Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\Errorlist.res"))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            else
            {
                //set main menu controls
                this.PANMenu.Height = 0;
                this.PANMenu.Top = 0;
                this.PANOptions.Height = 0;
                this.BTNShowHide.Top = 0;
                //set custom paint events
                //this.MNUForms.Paint += this.Custom_Paint;
                //this.MNUReports.Paint += this.Custom_Paint;
                //this.MNUOperations.Paint += this.Custom_Paint;
                //this.MNUMainMenu.Paint += this.Custom_Paint;

                //configure main menu
                CreateMainMenu();
                this.MNUForms.Click += MenuClickEvent;
                this.MNUOperations.Click += MenuClickEvent;
                this.MNUReports.Click += MenuClickEvent;
                this.MNUOpenWindows.Click += WindowMenuClickEvent;
                //set button colours
                SetButtonColours();
                //set form colour
                SetFormColour();
                //get sql table schemas into dictionary
                Modules.clsData.GetSQLSchema();
                //position form
                this.Top = 0;
                this.Left = 0;
                RECTTemp = Screen.PrimaryScreen.Bounds;
                this.Width = RECTTemp.Width;
                this.Height = RECTTemp.Height;
            }
        }

        //other form events
        private void TSCMBOpenForms_KeyDown(object sender, KeyEventArgs e)
        {
            /*
                   Created 10/03/2025 By Roger Williams


                   Stop user entering text
            */
            e.SuppressKeyPress = true;
        }

        private void BTNExit_Click(object sender, EventArgs e)
        {
            /*
                   Created 10/03/2025 By Roger Williams


            */
            this.Close();
        }



        private void BTNShowHide_Click(object sender, EventArgs e)
        {
            /*
               Created 26/06/2025 By Roger Williams

               shows hides the main menu controls
               

            */


            int intNum = 0;

            blnShowMenu = !blnShowMenu;
            /*
      
            menu panel defaults

            x: 0
            y: 0
            h: 50
            
            sectiion panel defaults

            x: 1
            y: 60
            h: 230

            */


            //process
            if (blnShowMenu)
            {
                for (intNum = 0; intNum != 58; intNum++)
                {
                    this.PANMenu.Height++;
                    this.PANMenu.Update();
                    this.PANOptions.Height++;
                    this.PANOptions.Update();
                }
                for (intNum = 0; intNum != intPANSectionsheight; intNum++)
                {
                    this.PANSections.Height++;
                    this.PANSections.Update();
                }

                //remove button border
                this.BTNShowHide.FlatAppearance.BorderSize = 0;
            }
            else
            {
                for (intNum = 0; intNum != 58; intNum++)
                {
                    this.PANMenu.Height--;
                    this.PANMenu.Update();
                    this.PANOptions.Height--;
                    this.PANOptions.Update();
                }
                for (intNum = 0; intNum != intPANSectionsheight; intNum++)
                {
                    this.PANSections.Height--;
                    this.PANSections.Update();
                }

                //reset button border
                this.BTNShowHide.FlatAppearance.BorderSize = 1;
            }
        }

        private void BTNShowHide_Paint(object sender, PaintEventArgs e)
        {
            Graphics graTemp = e.Graphics;
            GraphicsState state = graTemp.Save();  //save state
            Brush bruTemp = null;

            graTemp.ResetTransform();

            // Rotate.
            graTemp.RotateTransform(90);

            // Translate to desired position. Be sure to append
            // the rotation so it occurs after the rotation.
            graTemp.TranslateTransform(27, 8, MatrixOrder.Append);

            switch (intColourSwap)
            {
                case 0:
                    bruTemp = bruShowHide1;
                    break;
                case 1:
                    bruTemp = bruShowHide3;
                    break;
                case 2:
                    bruTemp = bruShowHide2;
                    break;
                case 3:
                    bruTemp = bruShowHide3;
                    break;
                case 4:
                    bruTemp = bruShowHide4;
                    break;
                case 5:
                    intColourSwap = 0;
                    bruTemp = bruShowHide1;
                    break;
            }

            if (blnShowMenu)
            {
                graTemp.DrawString("Hide", fntShowHide, bruTemp, 0, 0);
            }
            else
            {
                graTemp.DrawString("Menu", fntShowHide, bruTemp, 0, 0);
            }

            // Restore the graphics state.
            graTemp.Restore(state);
        }

        private void CMBOpenScreens_KeyDown(object sender, KeyEventArgs e)
        {
            /*
               Created 27/06/2025 By Roger Williams


               Stop user entering text
            */
            e.SuppressKeyPress = true;
        }
        private void TMRMenu_Tick(object sender, EventArgs e)
        {
            //swap menu show button text colour
            intColourSwap++;
            this.BTNShowHide.Refresh();
        }


        private void MNUCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void MNUHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void MNUVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        //class end
    }
}
