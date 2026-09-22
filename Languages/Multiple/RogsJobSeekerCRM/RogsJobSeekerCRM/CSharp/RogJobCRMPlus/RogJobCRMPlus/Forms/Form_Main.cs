using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
   Created 04/03/2026 By Roger Williams

   menu system, most of modules: clsview, clstables, clsdata copied from RogStock2026 

   CRM that handles:

   - job applications
   - job events 
   - mailshots
   - contacts -> if cold called someone

*/

namespace RogJobCRMPlus
{
public partial class frmMain : Form
{
        //used by "mainmenu" button
        Brush bruShowHide1 = new SolidBrush(Color.White);
        Brush bruShowHide2 = new SolidBrush(Color.GreenYellow);
        Brush bruShowHide3 = new SolidBrush(Color.Yellow);
        Brush bruShowHide4 = new SolidBrush(Color.LightBlue);
        Point pntShowHide = new Point(10, 10);
        Font fntShowHide = new Font("Segoe UI", 11, FontStyle.Bold);
        int intColourSwap = 0;
        Brush bruTemp = null;

        //used by button "showhide"
        bool blnShowMenu = false;

        int intPANSectionsheight = 0;

        //for manual mouse move of form
        bool blnDragging = false;
        Point pntLastLocation;

    public frmMain()
    {
        InitializeComponent();
        //apply colour "theme" to menu
        this.MNUMainMenu.Renderer = new Modules.clsView.clsMenuColourScheme();
    }

        //****custom subs/funcs ***********+


        public void WindowMenuClickEvent(object sender, EventArgs e)
        {
            /*

              Created 07/08/2025 By Roger Williams

              Opens menu item in open Windows menu

        */

        //discover which button

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
           
                  Opens menu item based on senders tag

            */

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
                  Creates Form/Report/Operation menus based on what user has access to as listed in: aryUserMenuItems 
                  Sets new menu items tag to its object e.g.: frmJobs

            */

            bool blnCont = true;
            int intNum = 0;
            ToolStripMenuItem MNUTemp = null;
            string strTemp = String.Empty;

            //clear existing menu items
            this.MNUForms.DropDownItems.Clear();
            this.MNUReports.DropDownItems.Clear();
            this.MNUOperations.DropDownItems.Clear();

            //go through array and create menu items for each user menu item
            //caption will be menu item name tag will be menu object name


            //create form menu elements
            for (intNum = 1; Modules.clsView.aryUserMenuItems[0, intNum, 0, 0] != null; intNum++)
            {
                blnCont = true;

                //get forms
                if (Modules.clsView.aryUserMenuItems[0, intNum, 0, 0] == "Form" && Modules.clsView.aryUserMenuItems[intNum, 0, 0, 0] == ((Button)sender).Text)
                {
                    //make sure does not already exist!
                    foreach (ToolStripMenuItem MNUFind in this.MNUForms.DropDownItems)
                    {
                        strTemp = Modules.clsView.aryUserMenuItems[0, 0, intNum, 0];

                        if (MNUFind.Text == Modules.clsView.aryUserMenuItems[0, 0, intNum, 0])
                        {
                            blnCont = false;
                            break;
                        }
                    }

                    if (blnCont)
                    {
                        MNUTemp = new ToolStripMenuItem();
                        MNUTemp.Text = Modules.clsView.aryUserMenuItems[0, 0, intNum, 0];
                        MNUTemp.Tag = Modules.clsView.aryUserMenuItems[0, 0, 0, intNum]; //object name
                        MNUTemp.Click += MenuClickEvent;
                        MNUTemp.ForeColor = Color.White;
                        this.MNUForms.DropDownItems.Add(MNUTemp);
                    }
                }

                //get operations
                if (Modules.clsView.aryUserMenuItems[0, intNum, 0, 0] == "Operation" && Modules.clsView.aryUserMenuItems[intNum, 0, 0, 0] == ((Button)sender).Text)
                {
                    //make sure does not already exist!
                    foreach (ToolStripMenuItem MNUFind in this.MNUOperations.DropDownItems)
                    {
                        strTemp = Modules.clsView.aryUserMenuItems[0, 0, intNum, 0];

                        if (MNUFind.Text == Modules.clsView.aryUserMenuItems[0, 0, intNum, 0])
                        {
                            blnCont = false;
                            break;
                        }
                    }

                    if (blnCont)
                    {
                        MNUTemp = new ToolStripMenuItem();
                        MNUTemp.Text = Modules.clsView.aryUserMenuItems[0, 0, intNum, 0];
                        MNUTemp.Tag = Modules.clsView.aryUserMenuItems[0, 0, 0, intNum]; //object name
                        MNUTemp.Click += MenuClickEvent;
                        MNUTemp.ForeColor = Color.White;
                        this.MNUOperations.DropDownItems.Add(MNUTemp);
                    }
                }

                //get reports
                if (Modules.clsView.aryUserMenuItems[0, intNum, 0, 0] == "Report" && Modules.clsView.aryUserMenuItems[intNum, 0, 0, 0] == ((Button)sender).Text)
                {
                    //make sure does not already exist!
                    foreach (ToolStripMenuItem MNUFind in this.MNUReports.DropDownItems)
                    {
                        strTemp = Modules.clsView.aryUserMenuItems[0, 0, intNum, 0];

                        if (MNUFind.Text == Modules.clsView.aryUserMenuItems[0, 0, intNum, 0])
                        {
                            blnCont = false;
                            break;
                        }
                    }

                    if (blnCont)
                    {
                        MNUTemp = new ToolStripMenuItem();
                        MNUTemp.Text = Modules.clsView.aryUserMenuItems[0, 0, intNum, 0];
                        MNUTemp.Tag = Modules.clsView.aryUserMenuItems[0, 0, 0, intNum]; //object name
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

            //set main mneu lblcycle with choice
            this.LBLCycle.Text = "Current Area: " + ((Button)sender).Text;
        }

        private void SetFormColour()
        {
            /*

                  Created 19/08/2025 By Roger Williams
           
                  Taken off the internet sets MDI child back colour!

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

                  Gets list of ALL menu items user has access too then creates the area menu items for them in: PANSections
                  as a series of buttons whose caption is the area e.g.: Inventory. The user menu items data is stored in an array: aryUserMenuItems
                  this contains:

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


        private bool CheckFolders()
        {
            /*
                   Created 04/03/2026 By Roger Williams

                   checks if resource/mailshots folders exist if not creates them
                   if resources folder does not exist creates the errorslist.res file
                  
                
                   returns TRUE if no folders exist
            */
            bool blnOk = true;
 

            if (!Directory.Exists(Modules.clsData.CNST_STR_INSTALLATIONPATH))
            {
                Directory.CreateDirectory(Modules.clsData.CNST_STR_INSTALLATIONPATH);
                blnOk = false;
            }


            //if (!Directory.Exists(Modules.clsData.CNST_STR_REPORTSPATH))
            //{
            //    Directory.CreateDirectory(Modules.clsData.CNST_STR_REPORTSPATH);
            //}

            if (!Directory.Exists(Modules.clsData.CNST_STR_RESOURCEPATH))
            {
                Directory.CreateDirectory(Modules.clsData.CNST_STR_RESOURCEPATH);
                blnOk = false;

                //create errorslist.res for SQL custom error handler
                Modules.clsData.CreateCustomSQLErrorFile();
                 //create default theme
                Modules.clsData.CreateDefaultTheme();
            }

            //create mailshots folder - used when mailshots are imported via frmmailshots
            if (!Directory.Exists(Modules.clsData.CNST_STR_MAILSHOTPATH))
            {
                Directory.CreateDirectory(Modules.clsData.CNST_STR_MAILSHOTPATH);
                blnOk = false;
            }


            return blnOk;
        }

        private void Init()
        {
            /*
                Created 17/02/2025 By Roger Williams

                creates system folders if missing
                gets theme
                inits SQL custom error handler
                creates user login record
                sets menu form/report/operations click events
                sets mainmenu button/form colours
                gets schema/key data
                gets form labels/titles data
                positions form top left of screen

            */
            //check if first run
            if (CheckFolders())
            {
                //load theme
                Modules.clsView.ReadThemeData();
            }
            //init custom sql error data
            if (!Modules.clsData.InitCustomErrorhandler(Modules.clsData.CNST_STR_SQLCUSTOMERRORSPATH))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            else
            {
                //create record in login_current NOTE: this is temp code till we decide to ACTUALLY have logins
                Modules.clsData.CreateCurrentLoginRecord("admin");

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
                //get sql table keys into dictionary
                Modules.clsTables.GetSQLSchemaTableKeys();
                //get sql table data into list
                Modules.clsTables.GetSchemaData();
                //read form labels/title data into list of typLabels
                Modules.clsTables.GetFormTitles();
                //position form
                this.Top = 0;
                this.Left = 0;

                //set "underline" width
                this.PANLine.Width = this.Width - this.BTNShowHide.Width;
                //start hide/show colour change
                this.TMRShowHide.Enabled = true;
            }
        }



        //********form events etc**
        private void frmMain_Load(object sender, EventArgs e)
        {
            Init();   
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


                   
            */
            Modules.clsData.DeleteCurrentLoginRecord();
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
            Application.Exit();
        }



        private void BTNShowHide_Click(object sender, EventArgs e)
        {
            /*
               Created 26/06/2025 By Roger Williams

               shows/hides the main menu controls by adjusting the height of: PANSections
               

            */


            int intNum = 0;

            blnShowMenu = !blnShowMenu;

            //process
            if (blnShowMenu)
            {
                for (intNum = 0; intNum != intPANSectionsheight; intNum++)
                {
                    this.PANSections.Height++;
                    this.PANSections.Update();
                }

                //remove button border
                this.BTNShowHide.FlatAppearance.BorderSize = 0;
                //set tag to 1
                this.BTNShowHide.Tag = 1;
            }
            else
            {
                for (intNum = 0; intNum != intPANSectionsheight; intNum++)
                {
                    this.PANSections.Height--;
                    this.PANSections.Update();
                }

                //reset button border
                this.BTNShowHide.FlatAppearance.BorderSize = 1;
                //set tag to 0
                this.BTNShowHide.Tag = 0;
            }
        }

        private void BTNShowHide_Paint(object sender, PaintEventArgs e)
        {
            Graphics graTemp = e.Graphics;
            GraphicsState state = graTemp.Save();  //save state
  

            graTemp.ResetTransform();

            // Rotate 90 for button text
            graTemp.RotateTransform(90);

            // Translate to desired position. Be sure to append
            // so it occurs after the rotation.
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

        private void MNUCascade_Click(object sender, EventArgs e)
        {
            //Note: below only works when form has border!
            //this.LayoutMdi(MdiLayout.Cascade);
            Modules.clsView.ArrangeChildForms(this,1);   
        }

        private void MNUHorizontal_Click(object sender, EventArgs e)
        {
            //  this.LayoutMdi(MdiLayout.TileHorizontal);
            Modules.clsView.ArrangeChildForms(this, 2);
        }

        private void MNUVertical_Click(object sender, EventArgs e)
        {
            //  this.LayoutMdi(MdiLayout.TileVertical);
            Modules.clsView.ArrangeChildForms(this, 3);
        }

        private void PICClose_Click(object sender, EventArgs e)
        {
            BTNExit_Click(sender, e);
        }

        private void PANTitle_MouseDown(object sender, MouseEventArgs e)
        {
            blnDragging = true;
            pntLastLocation = e.Location;
        }

        private void PANTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (blnDragging)
            {
                this.Location = new Point(
                (this.Location.X - pntLastLocation.X) + e.X,
                (this.Location.Y - pntLastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void PANTitle_MouseUp(object sender, MouseEventArgs e)
        {
            blnDragging = false;
        }

        private void TMRShowHide_Tick(object sender, EventArgs e)
        {
            /*
               Created 26/06/2025 By Roger Williams

               changes btnshowhide colour

               Note: due to strange issue with .Net cant replace this with an async function..?
               

            */

            Task.Delay(1000);
            //swap menu show button text colour
            intColourSwap++;
            this.BTNShowHide.Refresh();
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            this.BTNExit_Click(sender,e);
        }


        //***end class
    }
}
