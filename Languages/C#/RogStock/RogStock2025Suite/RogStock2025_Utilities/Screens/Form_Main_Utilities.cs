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


namespace RogStock2025_Utilities.Screens
{
    public partial class frmMain : Form
    {
        /*
             
                   Created 24/06//2025 By Roger Williams

                   Copied the main menu screen to be used here for the external utilities program
               
             
                   Created 13/02/2025 By Roger Williams

                   Main menu screen!

         */

        //used by "mainmenu" button
        Brush bruShowHide1 = new SolidBrush(Color.White);
        Brush bruShowHide2 = new SolidBrush(Color.GreenYellow);
        Brush bruShowHide3 = new SolidBrush(Color.Yellow);

        Brush bruMainFormBackground = new SolidBrush(Color.SteelBlue);
        Pen penMainFormBackground = new Pen(Color.SteelBlue);
        //   Point pntShowHide = new Point(10, 10);
        Font fntShowHide = new Font("Segoe UI", 11, FontStyle.Bold);
        int intColourSwap = 0;

        ////used by "mainmenu"
        bool blnShowMenu = false;
        Brush bruMenu = new SolidBrush(Color.White);
        Point pntMenu = new Point(10, 4);
        Font fntMenu = new Font("Segoe UI", 10, FontStyle.Bold);

        //used by menu items
        Pen PENTemp = new Pen(Color.White);
        Brush BRUTemp1 = new SolidBrush(Color.SteelBlue);
        Brush BRUTemp2 = new SolidBrush(Color.White);
        Font fntTemp = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        Point PNTTemp = new Point(0, 2);

        public frmMain()
        {
            InitializeComponent();
            //apply colour "theme" to menu
            this.MNUMainMenu.Renderer = new Modules.clsView_Utilities.clsMenuColourScheme();
        }


        //*custom sub/funcs**
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
                e.Graphics.FillRectangle(Brushes.DarkCyan, rctTemp);
            }
        }

        private void Custom_MenuItemPaint(object sender, PaintEventArgs e)
        {
            Rectangle RCTTemp = e.ClipRectangle; //get item size

            e.Graphics.DrawRectangle(PENTemp, RCTTemp);
            e.Graphics.FillRectangle(BRUTemp1, RCTTemp);
            PNTTemp.X = 6;
            e.Graphics.DrawString(((ToolStripMenuItem)sender).Text, fntTemp, BRUTemp2, PNTTemp);
        }
        private void Custom_MenuItemClicked(object sender, EventArgs e)
        {
            /*
                   Created 01/07/2025 By Roger Williams

                   Global handler for menu item click event
                   
            */

            if (((ToolStripMenuItem)sender).Name != "MNUMenu" && ((ToolStripMenuItem)sender).Name != "MNULogins" && ((ToolStripMenuItem)sender).Name != "MNUExit")
            {
                if (((ToolStripMenuItem)sender).Tag != null)
                {
                    ShowMenuItem(((ToolStripMenuItem)sender).Tag.ToString());
                }
            }
        }

        public void WindowMenuClickEvent(object sender, EventArgs e)
        {
            /*

                  Created 07/08/2025 By Roger Williams
           
                  Opens menu item for Windows menu

            */

            if (sender is ToolStripMenuItem)
            {
                if (((ToolStripMenuItem)sender).Name == "MNUWindows")
                {
                    return;
                }

                Modules.clsView_Utilities.ShowForm(((ToolStripMenuItem)sender).Text);
            }
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

        private void SetButtonColours()
        {
            /*
                   Created 18/08/2025 By Roger Williams

                   sets hide/show button colours
                   done here as flatappearance colours  can be fiddly to find!
                   
            */

            //set how/hide button colours
            this.BTNShowHide.BackColor = Color.CadetBlue;
            this.BTNShowHide.ForeColor = Color.SteelBlue;
            this.BTNShowHide.FlatAppearance.MouseOverBackColor = Color.DeepSkyBlue;
            this.BTNShowHide.FlatAppearance.MouseDownBackColor = Color.LightGreen;

        }

        //****form events*******
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


                   
            */

            Modules.clsData_Utilities.DeleteCurrentLoginRecord();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            /*
                   Created 13/02/2025 By Roger Williams

                   Show login screen

            */
            frmLogin_Utilities frmTemp;

            //load theme
            Modules.clsView_Utilities.ReadThemeData();

            //open login screen
            frmTemp = new frmLogin_Utilities();
            frmTemp.ShowDialog();

            //init custom sql error data if not found exit
            if (!Modules.clsData_Utilities.InitCustomErrorhandler(Path.GetDirectoryName(Application.ExecutablePath) + @"\Resources\Errorlist.res"))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
                return;
            }

            //set main menu controls
            this.PANMenu.Height = 0;
            this.PANMenu.Top = 0;
            this.PANOptions.Height = 0;
            this.BTNShowHide.Top = 0;
            //custom paint for menu headers
            //this.MNUMenu.Paint += this.Custom_Paint;
            //this.MNULogins.Paint += this.Custom_Paint;
            //this.MNUExit.Paint += this.Custom_Paint;
            //this.MNUMainMenu.Paint += this.Custom_Paint;
            //custom paint for menu items
            //this.MNUGroups.Paint += this.CustomMenuItemPaint;
            //this.MNULoginMaintenance.Paint += this.CustomMenuItemPaint;
            //this.MNUMenuItems.Paint += this.CustomMenuItemPaint;
            //this.MNUMenuSecurity.Paint += this.CustomMenuItemPaint;
            //this.MNUSections.Paint += this.CustomMenuItemPaint;
            //this.MNUThemeMaintenance.Paint += this.CustomMenuItemPaint;
            //this.MNUUserGroups.Paint += this.CustomMenuItemPaint;
            //enable text colour animation timer
            this.TMRMenu.Enabled = true;
            //menu item custom click handler
            this.MNUGroups.Click += Custom_MenuItemClicked;
            this.MNULoginMaintenance.Click += Custom_MenuItemClicked;
            this.MNUMenu.Click += Custom_MenuItemClicked;
            this.MNUMenuItems.Click += Custom_MenuItemClicked;
            this.MNUMenuSecurity.Click += Custom_MenuItemClicked;
            this.MNUSections.Click += Custom_MenuItemClicked;
            this.MNUThemeMaintenance.Click += Custom_MenuItemClicked;
            this.MNUUserGroups.Click += Custom_MenuItemClicked;
            //apply system theme
            //     Modules.clsView_Utilities.SetTheme(this);
            //set hide/show button colours
            SetButtonColours();
            SetFormColour();
            //get sql table schemas into dictionary
            Modules.clsData_Utilities.GetSQLSchema();
        }

        private void ShowMenuItem(string strWhat)
        {
            /*
             Created 13/03/2025 By Roger Williams

             Opens menu item!
             Partially hide the menu strip

            */

            Modules.clsView_Utilities.OpenForm(strWhat);
            blnShowMenu = false;
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
            
            section panel defaults

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

                //reset button border
                this.BTNShowHide.FlatAppearance.BorderSize = 1;
            }
        }

        private void MNUExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void TMRMenu_Tick(object sender, EventArgs e)
        {
            //swap menu show button text colour
            intColourSwap++;
            this.BTNShowHide.Refresh();
        }

        private void CMBOpenScreens_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
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
