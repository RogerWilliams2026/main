using RogStock2025_Utilities.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
/*
   Created 17/02/2025 By Roger Williams

  handles GUI manipulation

*/
namespace RogStock2025_Utilities.Modules
{
    internal static class clsView_Utilities
    {
        //used for theme custom line colour
        static int clrLine = 0;
        //list used to store theme colours
        static string[,,] aryThemeColours = new string[51, 51, 51];
        static int intAryPos = 1;
        static Form frmMainForm = null;

        //used for readonly datagrid columns
        public static Color CNST_INT_READONLYCOLUMNSCOLOUR = Color.LightGray; // .DarkGray;
        public static string CNST_STR_OPENFORMSCONTROL = "MNUOpenWindows";

        //list of colours used by theme maintenance and colour palette forms

        //Note: there are actually 137 solidcolours but some where deleted
        //      as they looked the same as others!
        public static string[] aryColours = {
                "Aqua",
                "Aquamarine",
                "Black",
                "Blue",
                "BlueViolet",
                "Brown",
                "BurlyWood",
                "CadetBlue",
                "Chartreuse",
                "Chocolate",
                "Coral",
                "CornflowerBlue",
                "Crimson",
                "Cyan",
                "DarkBlue",
                "DarkCyan",
                "DarkGoldenrod",
                "DarkGray",
                "DarkGreen",
                "DarkKhaki",
                "DarkMagenta",
                "DarkOliveGreen",
                "DarkOrange",
                "DarkOrchid",
                "DarkRed",
                "DarkSalmon",
                "DarkSeaGreen",
                "DarkSlateBlue",
                "DarkSlateGray",
                "DarkTurquoise",
                "DarkViolet",
                "DeepPink",
                "DeepSkyBlue",
                "DodgerBlue",
                "Firebrick",
                "FloralWhite",
                "ForestGreen",
                "Fuchsia",
                "Gold",
                "Goldenrod",
                "Gray",
                "Green",
                "GreenYellow",
                "HotPink",
                "IndianRed",
                "Indigo",
                "Ivory",
                "Khaki",
                "LawnGreen",
                "LemonChiffon",
                "LightBlue",
                "LightCoral",
                "LightGray",
                "LightGreen",
                "LightPink",
                "LightSalmon",
                "LightSeaGreen",
                "LightSkyBlue",
                "LightSteelBlue",
                "Lime",
                "LimeGreen",
                "Magenta",
                "Maroon",
                "MediumAquamarine",
                "MediumBlue",
                "MediumOrchid",
                "MediumPurple",
                "MediumSeaGreen",
                "MediumSlateBlue",
                "MediumSpringGreen",
                "MediumTurquoise",
                "MediumVioletRed",
                "MidnightBlue",
                "Moccasin",
                "NavajoWhite",
                "Navy",
                "Olive",
                "OliveDrab",
                "Orange",
                "OrangeRed",
                "Orchid",
                "PaleGoldenrod",
                "PaleGreen",
                "PaleTurquoise",
                "PaleVioletRed",
                "PeachPuff",
                "Peru",
                "Pink",
                "Plum",
                "PowderBlue",
                "Purple",
                "Red",
                "RosyBrown",
                "RoyalBlue",
                "SaddleBrown",
                "Salmon",
                "SandyBrown",
                "SeaGreen",
                "Sienna",
                "Silver",
                "SkyBlue",
                "SlateGray",
                "SpringGreen",
                "SteelBlue",
                "Tan",
                "Teal",
                "Thistle",
                "Tomato",
                "Turquoise",
                "Violet",
                "Wheat",
                "White",
                "Yellow",
                "YellowGreen"
        };



        //************* internal class for colour "theme"
        internal class clsMenuColourScheme : ToolStripProfessionalRenderer
        {
            public clsMenuColourScheme() : base(new clsMenuColours()) { }
        }

        public class clsMenuColours : ProfessionalColorTable
        {

            //  Color menu
            /// <summary>
            /// Gets the starting color of the gradient used when 
            /// a top-level System.Windows.Forms.ToolStripMenuItem is pressed.
            /// </summary>

            public override Color MenuItemPressedGradientBegin => Color.DimGray;

            /// <summary>
            /// Gets the end color of the gradient used when a top-level 
            /// System.Windows.Forms.ToolStripMenuItem is pressed.
            /// </summary>
            public override Color MenuItemPressedGradientEnd => Color.DimGray;

            /// <summary>
            /// Gets the border color to use with a 
            /// System.Windows.Forms.ToolStripMenuItem.
            /// </summary>
            public override Color MenuItemBorder => Color.SkyBlue;

            /// <summary>
            /// Gets the starting color of the gradient used when the 
            /// System.Windows.Forms.ToolStripMenuItem is selected.
            /// </summary>
            public override Color MenuItemSelectedGradientBegin => Color.SkyBlue; // Silver;

            /// <summary>
            /// Gets the end color of the gradient used when the 
            /// System.Windows.Forms.ToolStripMenuItem is selected.
            /// </summary>
            public override Color MenuItemSelectedGradientEnd => Color.SkyBlue;

            /// <summary>
            /// Gets the solid background color of the 
            /// System.Windows.Forms.ToolStripDropDown.
            /// </summary>
            public override Color ToolStripDropDownBackground => Color.DarkCyan;

            /// <summary>
            /// Gets the starting color of the gradient used in the image 
            /// margin of a System.Windows.Forms.ToolStripDropDownMenu.
            /// </summary>
            public override Color ImageMarginGradientBegin => Color.DimGray;

            /// <summary>
            /// Gets the middle color of the gradient used in the image 
            /// margin of a System.Windows.Forms.ToolStripDropDownMenu.
            /// </summary>
            public override Color ImageMarginGradientMiddle => Color.DimGray;

            /// <summary>
            /// Gets the end color of the gradient used in the image 
            /// margin of a System.Windows.Forms.ToolStripDropDownMenu.
            /// </summary>
            public override Color ImageMarginGradientEnd => Color.DimGray;

            /// <summary>
            /// Gets the color to use to for shadow effects on 
            /// the System.Windows.Forms.ToolStripSeparator.
            /// </summary>
            public override Color SeparatorDark => Color.White;

        }

        public static int PopulateThemeArraysForForm(ref string[,,] aryThemeColoursNew, ref string[,,] aryThemeColoursOld)
        {
            int intNum = 1;

            while (aryThemeColours[intNum, 0, 0] != null)
            {
                aryThemeColoursNew[intNum, 0, 0] = aryThemeColours[intNum, 0, 0];
                aryThemeColoursNew[0, intNum, 0] = aryThemeColours[0, intNum, 0];
                aryThemeColoursNew[0, 0, intNum] = aryThemeColours[0, 0, intNum];

                aryThemeColoursOld[intNum, 0, 0] = aryThemeColours[intNum, 0, 0];
                aryThemeColoursOld[0, intNum, 0] = aryThemeColours[0, intNum, 0];
                aryThemeColoursOld[0, 0, intNum] = aryThemeColours[0, 0, intNum];
                intNum++;
            }

            return intNum;
        }

        public static void ShowForm(string strWhat)
        {
            /*

                Created 07/08/2025 By Roger Williams
           
                Shows form
            

                VARS

                strWhat  - form caption of form to show

            */

            foreach (Form frmTemp in Application.OpenForms)
            {
                if (frmTemp.Text == strWhat)
                {
                    frmTemp.BringToFront();
                    break;
                }
            }
        }


        public static void ReadThemeData()
        {
            /*
             Created 09/07/2025 By Roger Williams
         
             reads theme colours file: \Resources\rogstock2025theme.thm
             and stores values into theme array            

            */

            string[] aryTemp = null;
            string strTemp = "";
            StreamReader stmRead = null;
            int intNum = 1;

            strTemp = Path.GetDirectoryName(Application.ExecutablePath)+ @"\Resources\rogstock2025theme.thm";

            //if theme file not found just tell user as does not affect application operation
            if (!File.Exists(strTemp))
            {
                MessageBox.Show("Theme File Not Found In Resources Folder!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                intAryPos = 1;
                //fill array with null as this procedure is called when application loads
                //AND when theme maintenance is loaded
                for(intNum =1; intNum != 51;intNum++)
                {
                    aryThemeColours[intNum, 0, 0] = null;
                    aryThemeColours[0, intNum, 0] = null;
                    aryThemeColours[0, 0, intNum] = null;
                }

                stmRead = new StreamReader(strTemp);

                //read file and store in array
                while (stmRead.Peek() != -1)
                {
                    strTemp = stmRead.ReadLine();
                    //split by: ,
                    aryTemp = strTemp.Split(",");

                    aryThemeColours[intAryPos, 0, 0] = aryTemp[0];
                    aryThemeColours[0, intAryPos, 0] = aryTemp[1];
                    aryThemeColours[0, 0, intAryPos] = aryTemp[2];
                    intAryPos++;
                }

                stmRead.Close();
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Accessing Theme File!\n\n" + ex.Message, "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }


        public static void SetTheme(Form frmWhere, string [,,] aryWithThemeColours)
        {
            /*
             Modified 22/07/2025 By Roger Williams

             Redesgined the "apply" function for colours

             Added passed var: aryWithThemeColours
             This is used by theme maintenance to test settings

             Now processes "container" controls like tabcontrol correctly and handles datagridview

             Created 09/07/2025 By Roger Williams
         
             Sets the passed forms controls to the defined theme in the theme array            

             VARS

             frmWhere                 - form to process 
             aryWithThemeColours      - array with theme colours
            
             NOTE: this is also called by theme maintenance form so obviously that will pass a
                   "test" array to preview changes 

            */
            
            List<Control> lstMainControls = frmWhere.Controls.Cast<Control>().ToList(); //gets all controls in frmWhere into a list  
            List<Control> lstTemp = new List<Control>(); //used to search list for control types this handles search results
            Dictionary<string, string> dictPropertyColours = new Dictionary<string, string>(); //used to hold control colour properties for processing

           void ApplyToControl(string strWhat, string strName, string strProperty, string strColour, Form frmFrom, Control ctlFrom)
            {
                /*
                  Modified 21/07/2025 By Roger Williams
                
                  Added vars:
                  frmFrom - form to process
                  crlFrom - control to process

                  NOTE: only one OR the other is passed
                
                  ctlFrom added for "container" controls like tabcontrol


                  Created 11/07/2025 By Roger Williams

                  Sets frmWhere controls colours or frmWhere itself

                  VARS

                  strWhat       - control type
                  strname       - control name
                  strProperty   - property to change
                  strColour     - colour

                 */

                Color clrSelectedColour = Color.FromName(strColour); //get passed colour

                switch (strWhat)
                {
                    case "ColumnHeadersDefaultCellStyle":
                        if (strProperty == "BackColor")
                        {
                            //need to set this boolean variable to false to use custom column header colours
                            ((DataGridView)frmFrom.Controls[strName]).EnableHeadersVisualStyles = false;
                            ((DataGridView)frmFrom.Controls[strName]).ColumnHeadersDefaultCellStyle.BackColor = clrSelectedColour;
                        }

                        if (strProperty == "ForeColor")
                        {
                            //need to set this boolean variable to false to use custom column header colours
                            ((DataGridView)frmFrom.Controls[strName]).EnableHeadersVisualStyles = false;
                            ((DataGridView)frmFrom.Controls[strName]).ColumnHeadersDefaultCellStyle.ForeColor = clrSelectedColour;
                        }

                        break;
                    case "DataGridView":
                        if (strProperty == "BackgroundColor")
                        {
                            ((DataGridView)frmFrom.Controls[strName]).BackgroundColor = clrSelectedColour;
                        }
                        
                        if (strProperty == "GridColor")
                        {
                            ((DataGridView)frmFrom.Controls[strName]).GridColor = clrSelectedColour;
                        }
                        break;
                    case "DefaultCellStyle":
                        if (strProperty == "BackColor")
                        {
                            ((DataGridView)frmFrom.Controls[strName]).DefaultCellStyle.BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            ((DataGridView)frmFrom.Controls[strName]).DefaultCellStyle.ForeColor = clrSelectedColour;
                        }

                        break;
                     case "Form":
                        if (strProperty == "BackColor")
                        {
                            frmFrom.BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmFrom.ForeColor = clrSelectedColour;
                        }
                        break;
                    case "GroupBox":
                        if (strProperty == "BackColor")
                        {
                            frmFrom.Controls[strName].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmFrom.Controls[strName].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "Panel":
                        if (strProperty == "BackColor")
                        {
                            frmFrom.Controls[strName].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            frmFrom.Controls[strName].ForeColor = clrSelectedColour;
                        }
                        break;
                    case "RowsDefaultCellStyle":
                        if (strProperty == "BackColor")
                        {
                            ((DataGridView)frmFrom.Controls[strName]).RowsDefaultCellStyle.BackColor = clrSelectedColour;
                        }

                        if (strProperty == "ForeColor")
                        {
                            ((DataGridView)frmFrom.Controls[strName]).RowsDefaultCellStyle.ForeColor = clrSelectedColour;
                        }

                        break;
                    case "RowHeadersDefaultCellStyle":
                        if (strProperty == "BackColor")
                        {
                            ((DataGridView)frmFrom.Controls[strName]).RowHeadersDefaultCellStyle.BackColor = clrSelectedColour;
                        }

                        if (strProperty == "ForeColor")
                        {
                            ((DataGridView)frmFrom.Controls[strName]).RowHeadersDefaultCellStyle.ForeColor = clrSelectedColour;
                        }


                        break;
                    case "ToolStripStatusLabel":
                        if (strProperty == "BackColor")
                        {
                            ((StatusStrip)ctlFrom).Items[strName].BackColor = clrSelectedColour;
                        }
                        if (strProperty == "ForeColor")
                        {
                            ((StatusStrip)ctlFrom).Items[strName].ForeColor = clrSelectedColour;
                        }
                        break;
                    default:
                        try
                        {
                            if (strProperty == "BackColor")
                            {
                                if (frmFrom != null)
                                {
                                    frmFrom.Controls[strName].BackColor = clrSelectedColour;
                                }
                                else
                                {
                                    ctlFrom.Controls[strName].BackColor = clrSelectedColour;
                                }
                            }
                            if (strProperty == "ForeColor")
                            {
                                if (frmFrom != null)
                                {
                                    frmFrom.Controls[strName].ForeColor = clrSelectedColour;
                                }
                                else
                                {
                                    ctlFrom.Controls[strName].ForeColor = clrSelectedColour;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //do nothing as whatever the passed control is does not support passed property
                            ex = ex;
                        }
                        break;
                }
            }



            void GetFromArray(string strControlType)
            {
                /*
                   Created 21/07/2025 By Roger Williams

                   searches the theme array for passed control TYPE then populates
                   dictionary with all properties and colours for the passed control name


                   VARS


                   strControlType   - control type e.g. Button

                */

                int intNum = 1;

                //clear dictionary
                dictPropertyColours.Clear();

                while (aryWithThemeColours[intNum, 0, 0] != null)
                {
                    while (aryWithThemeColours[intNum, 0, 0] == strControlType) 
                    {
                        //add property and colour 
                        dictPropertyColours.Add(aryWithThemeColours[0, intNum, 0], aryWithThemeColours[0, 0, intNum]);
                        intNum++;
                    }

                    intNum++;
                 }
            }
            //************end of sub funcs************
   
            //if not theme for strange reason just exit
            if (aryThemeColours.Length == 0)
            {
                return;
            }

            //check if array null as forms setting their theme will pass NULL
            if (aryWithThemeColours == null)
            {
                aryWithThemeColours = aryThemeColours;
            }

            //go through mains controls first
            foreach (Control ctlMain in lstMainControls)
            {
                //see if child control i.e. a container e.g. panel
                switch (ctlMain.GetType().Name) 
                {
                    case "DataGridView":
                        GetFromArray(ctlMain.GetType().Name);

                        foreach (var elTemp in dictPropertyColours)
                        {
                            ApplyToControl(ctlMain.GetType().Name, ctlMain.Name, elTemp.Key, elTemp.Value, frmWhere, null);
                        }

                        //set default cell colours
                        GetFromArray("DefaultCellStyle");

                        foreach (var elTemp in dictPropertyColours)
                        {
                            ApplyToControl("DefaultCellStyle", ctlMain.Name, elTemp.Key, elTemp.Value, frmWhere, null);
                        }

                        //set default column header colours
                        GetFromArray("ColumnHeadersDefaultCellStyle");

                        foreach (var elTemp in dictPropertyColours)
                        {
                            ApplyToControl("ColumnHeadersDefaultCellStyle", ctlMain.Name, elTemp.Key, elTemp.Value, frmWhere, null);
                        }

                        //set default row colours
                        GetFromArray("RowsDefaultCellStyle");

                        foreach (var elTemp in dictPropertyColours)
                        {
                            ApplyToControl("RowsDefaultCellStyle", ctlMain.Name, elTemp.Key, elTemp.Value, frmWhere, null);
                        }


                        //get default row select colours
                        GetFromArray("RowHeadersDefaultCellStyle");

                        foreach (var elTemp in dictPropertyColours)
                        {
                            ApplyToControl("RowHeadersDefaultCellStyle", ctlMain.Name, elTemp.Key, elTemp.Value, frmWhere, null);
                        }
                        break;
                    case "GroupBox":
                        GetFromArray(ctlMain.GetType().Name);

                        foreach (var elTemp in dictPropertyColours)
                        {
                            ApplyToControl(ctlMain.GetType().Name, ctlMain.Name, elTemp.Key, elTemp.Value, frmWhere, null);
                        }

                        //process child controls
                        foreach (Control ctlTemp in ctlMain.Controls)
                        {
                            GetFromArray(ctlTemp.GetType().Name);

                            foreach (var elTemp in dictPropertyColours)
                            {
                                ApplyToControl(ctlTemp.GetType().Name, ctlTemp.Name, elTemp.Key, elTemp.Value, null, ctlMain);
                            }
                        }
                        break;
                    case "Panel":
                        GetFromArray(ctlMain.GetType().Name);

                        foreach (var elTemp in dictPropertyColours)
                        {
                            ApplyToControl(ctlMain.GetType().Name, ctlMain.Name, elTemp.Key, elTemp.Value, frmWhere, null);
                        }

                        //process child controls
                        foreach (Control ctlTemp in ctlMain.Controls)
                        {
                            GetFromArray(ctlTemp.GetType().Name);

                            foreach (var elTemp in dictPropertyColours)
                            {
                                ApplyToControl(ctlTemp.GetType().Name, ctlTemp.Name, elTemp.Key, elTemp.Value, null, ctlMain);
                            }
                        }
                        break;

                    case "StatusStrip":
                        GetFromArray(ctlMain.GetType().Name);

                        foreach (var elTemp in dictPropertyColours)
                        {
                            ApplyToControl(ctlMain.GetType().Name, ctlMain.Name, elTemp.Key, elTemp.Value, frmWhere, null);
                        }

                        //process child controls
                        foreach (ToolStripItem TSITemp in ((StatusStrip)ctlMain).Items)
                        {
                            GetFromArray(TSITemp.GetType().Name);

                            foreach (var elTemp in dictPropertyColours)
                            {
                                ApplyToControl(TSITemp.GetType().Name, TSITemp.Name, elTemp.Key, elTemp.Value, null, ctlMain);
                            }
                        }
                        break;

                    case "TabControl":
                        //process child controls
                        foreach (Control ctlTemp in ctlMain.Controls)
                        {
                            GetFromArray(ctlTemp.GetType().Name);

                            foreach (var elTemp in dictPropertyColours)
                            {
                                ApplyToControl(ctlTemp.GetType().Name, ctlTemp.Name, elTemp.Key, elTemp.Value, null, ctlMain);
                            }

                            //check if has child controls i.e. tabpage
                            if (ctlTemp.HasChildren)
                            { 
                                foreach (Control ctlChild in ctlTemp.Controls)
                                {
                                    GetFromArray(ctlChild.GetType().Name);

                                    foreach (var elTemp in dictPropertyColours)
                                    {
                                        ApplyToControl(ctlChild.GetType().Name, ctlChild.Name, elTemp.Key, elTemp.Value, null, ctlTemp);
                                    }
                                }
                            }
                        }
                        break;

                    default:
                        GetFromArray(ctlMain.GetType().Name);

                        foreach (var elTemp in dictPropertyColours)
                        {
                            ApplyToControl(ctlMain.GetType().Name, ctlMain.Name, elTemp.Key, elTemp.Value, frmWhere, null);
                        }

                        break;
                }
            }

            //now apply colour to form
            GetFromArray("Form");

            foreach (var elTemp in dictPropertyColours)
            {
                ApplyToControl("Form", frmWhere.Name, elTemp.Key, elTemp.Value, frmWhere, null);
            }
        }

        public static void RemoveFromOpenForms(string strText)
        {
            /*
             Modified 07/08/2025 By Roger Williams
            
             Now uses menu form open forms
  
            
             Created 10/03/2025 By Roger Williams
         
             remove from open forms combobox on the main form
             hides sidebar
            
             VARS

             strText        - text to remove

             Note: also makes sure not duplicating entires

            */

            Control[] aryTemp;
            ToolStripMenuItem MNUWindows = null;
            object objTemp = null;
            MenuStrip MNUTemp = null;
            Panel PANTemp = null;
            int intNum = 0;

            aryTemp = frmMainForm.Controls.Find(CNST_STR_OPENFORMSCONTROL, true);

            foreach (Control ctlTemp in aryTemp)
            {
                if (ctlTemp != null)
                {
                    //get MNUWindows
                    objTemp = ((MenuStrip)ctlTemp).Items[0];
                    MNUWindows = (ToolStripMenuItem)objTemp;
                    MNUWindows.DropDownItems.Remove(MNUWindows.DropDownItems[strText]);
                    intNum = MNUWindows.DropDownItems.Count;
                }
            }

            if (intNum == 0)
            {
                //hide arrange menu
                PANTemp = (Panel)frmMainForm.Controls["PANMenu"];
                MNUTemp = (MenuStrip)PANTemp.Controls["MNUMainMenu"];
                MNUTemp.Items["MNUArrangeWindows"].Visible = false;
            }
        }
        public static void AddToOpenForms(string strText)
        {
            /*     
             Modified 07/08/2025 By Roger Williams
            
             Now uses menu form open forms

             Created 10/03/2025 By Roger Williams
         
             stores form name in open forms combobox on the main form
             hides sidebar
            
             VARS

             strText        - text to add

             Note: also makes sure not duplicating entires

            */

            Control[] aryTemp;
            ToolStripMenuItem MNUTemp = null;
            ToolStripMenuItem MNUWindows = null;

            object objTemp = null;

            aryTemp = frmMainForm.Controls.Find(CNST_STR_OPENFORMSCONTROL, true);

            foreach (Control ctlTemp in aryTemp)
            {
                if (ctlTemp != null)
                {
                    //get MNUWindows
                    objTemp = ((MenuStrip)ctlTemp).Items[0]; //.DropDownItems[0];
                    MNUWindows = (ToolStripMenuItem)objTemp;

                    //make sure not adding duplicate
                    if (MNUWindows.DropDownItems.IndexOfKey(strText) != -1)
                    {
                        return;
                    }
                    else
                    {
                        //create menu item
                        MNUTemp = new ToolStripMenuItem();
                        MNUTemp.Text = strText;
                        MNUTemp.Name = strText;
                        MNUTemp.BackColor = Color.CadetBlue;
                        MNUTemp.ForeColor = Color.White;
                        MNUTemp.Font = new Font("Segoe UI",10,FontStyle.Bold);
                        MNUTemp.Click += ((frmMain)frmMainForm).WindowMenuClickEvent;
                        //add menu item      
                        MNUWindows.DropDownItems.Add(MNUTemp);
                    }
                }
            }
        }

        public static void OpenForm(string strWhat)
        {
            ///*
            //   Created 17/02/2025 By Roger Williams

            //   opens form adds to open forms combobox sets as MDI child

            //*/

            //Form frmTemp = null;
            //Button BTNTemp = null;

            ////first make sure form not already open
            //foreach (Form frmFind in Application.OpenForms)
            //{
            //    if (frmFind.Text == strWhat)
            //    {
            //        return;
            //    }
            //}

            //switch (strWhat)
            //{
            //    case "Sections":
            //        frmTemp = new frmSections_Utilities();
            //        break;

            //    case "Groups":
            //        frmTemp = new frmGroups_Utilities();
            //        break;

            //    case "Menu Items":
            //        frmTemp= new frmMenuItems_Utilities();
            //        break;
            //    case "Menu Security":
            //        frmTemp= new frmMenuSecurity_Utilities();
            //        break;
            //    case "Login Maintenance":
            //        frmTemp = new frmLoginMaintenance();
            //        break;
            //    case "User Groups Maintenance":
            //        frmTemp = new frmUserGroups();
            //        break;
            //    case "Theme Maintenance":
            //        frmTemp = new frmThemeMaintenance();
            //        break;
            //}

            //if (frmTemp != null)
            //{
            //    if (frmMainForm == null)
            //    {
            //        frmMainForm = Application.OpenForms["frmMain_Utilities"];
            //    }

            //    if (frmMainForm != null)
            //    {
            //        frmTemp.MdiParent = frmMainForm;
            //        //add to open form combobox
            //        AddToOpenForms(frmTemp.Text);
            //        BTNTemp = (Button)frmMainForm.Controls["BTNShowHide"];
            //        BTNTemp.PerformClick();
            //    }
            //    //position form
            //    frmTemp.StartPosition = FormStartPosition.Manual;
            //    frmTemp.Left = 50;
            //    frmTemp.Top = 5;
            //    frmTemp.Visible = true;
            //    SetTheme(frmTemp, aryThemeColours);
            //}


            /*
   Created 17/02/2025 By Roger Williams

   opens form adds to open forms combobox sets as MDI child

*/

            Form frmTemp = null;
            Button BTNTemp = null;
            Panel PANTemp = null;
            MenuStrip MNUTemp = null;
            string strTemp = String.Empty;

            //first make sure form not already open
            foreach (Form frmFind in Application.OpenForms)
            {
                if (frmFind.Text == strWhat)
                {
                    return;
                }
            }

            //get main form
            if (frmMainForm == null)
            {
                frmMainForm = Application.OpenForms["frmMain"];
            }

            //if for some bizarre reason the main form is not open (??)
            if (frmMainForm == null)
            {
                return;
            }

            //get forms 
            //Note: need to pass an existing form e.g. frmMain as type NOT Form
            foreach (Type formType in Assembly.GetAssembly(typeof(frmMain)).GetTypes())
            {
                //Select only the application forms 
                if (formType.BaseType != null)
                {
                    if (formType.BaseType.ToString().ToUpper().Contains("SYSTEM.WINDOWS.FORMS.FORM"))
                    {
                        //get JUST the form name
                        strTemp = formType.Name.ToString();

                        if (strTemp == strWhat)
                        {
                            //create form object from formType 
                            frmTemp = (Form)Activator.CreateInstance(formType);
                            break;
                        }
                    }
                }
            }

            if (frmTemp != null)
            {
                frmTemp.MdiParent = frmMainForm;
                //add to open form menu
                AddToOpenForms(frmTemp.Text);
                BTNTemp = (Button)frmMainForm.Controls["BTNShowHide"];
                BTNTemp.PerformClick();
                //show arrange menu
                PANTemp = (Panel)frmMainForm.Controls["PANMenu"];
                MNUTemp = (MenuStrip)PANTemp.Controls["MNUMainMenu"];
                MNUTemp.Items["MNUArrangeWindows"].Visible = true;
                //position form
                frmTemp.StartPosition = FormStartPosition.Manual;
                frmTemp.Left = 50;
                frmTemp.Top = 50;
                frmTemp.Visible = true;
                SetTheme(frmTemp, aryThemeColours);
            }
        }

        public static List<string> GetAnyTreeNodesSelected(TreeNode ndeParent)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Returns a list for any nodes with tag of 1 AND checked


               VARS

               ndeParent    - where to search

            */

            List<string> lstNodes = new List<string>();

            void CheckAllChildren(TreeNode ndeNewParent)
            {
                foreach (TreeNode ndeTemp in ndeNewParent.Nodes)
                {

                    if (ndeTemp.Tag != null)
                    {
                        if (ndeTemp.Checked)
                        {
                            lstNodes.Add(ndeTemp.Text);
                        }
                    }

                    CheckAllChildren(ndeTemp);
                }
            }

            CheckAllChildren(ndeParent);
            return lstNodes;
        }

        //public static List<string> GetAnyTreeItemSelected(TreeNode ndeParent)
        //{
        //    /*
        //       Created 08/07/2025 By Roger Williams

        //       Returns a list for any item in nodes with tag of 1 AND checked


        //       VARS

        //       ndeParent    - where to search

        //    */

        //    List<string> lstTemp= new List<string>();

        //    void CheckAllChildren(TreeNode ndeParent)
        //    {
        //        foreach (TreeNode ndeTemp in ndeParent.Nodes)
        //        {

        //            if (ndeTemp.Tag != null)
        //            {
        //                if (ndeTemp.Checked)
        //                {
        //                    lstTemp.Add(ndeTemp.Text);
        //                }
        //            }

        //            CheckAllChildren(ndeTemp);
        //        }
        //    }

        //    CheckAllChildren(ndeParent);
        //    return lstTemp;
        //}

        public static void CheckAllGroup(TreeNode ndeParent)
        {
            /*
               Created 01/08/2025 By Roger Williams

               Checks if all nodes in group e.g. forms are checked if so checks children

               VARS

               ndeParent        - parent node (need to filter for type node contents e.g. form)

            */

            int intNum = 0;
            TreeNode ndeGroup = null;

            foreach (TreeNode ndeTemp in ndeParent.Nodes)
            {
                foreach (TreeNode ndeType in ndeTemp.Nodes)
                {
                    //first check if top level (type) is checked if so check all children
                    if (ndeType.Checked)
                    {
                        //check children

                        foreach (TreeNode ndeChild in ndeType.Nodes)
                        {
                            ndeChild.Checked = true;
                        }
                    }
                    else
                    {
                            //now check if all child nodes checked if so checks parent
                            ndeGroup = ndeType;
                            intNum = 0;

                            foreach (TreeNode ndeChild in ndeType.Nodes)
                            {
                                if (ndeChild.Tag != null)
                                {
                                    if (ndeChild.Checked)
                                    {
                                        intNum++;
                                    }
                                }
                            }

                            if (intNum == ndeGroup.Nodes.Count)
                            {
                                //check parent i.e. type e.g. Form
                                ndeGroup.Checked = true;
                            }
                    }
                }
            }
        }

        public static void UnCheckAllGroup(TreeNode ndeParent)
        {
            /*
               Created 01/08/2025 By Roger Williams

               UnChecks/checks children if all group e.g. forms is unchecked 

               VARS

               ndeParent    - parent node (need to filter for type node contents e.g. form)

            */

            TreeNode ndeGroup = null;
            int intNum = 0;

            foreach (TreeNode ndeTemp in ndeParent.Nodes)
            {
                foreach (TreeNode ndeType in ndeTemp.Nodes)
                {
                    //first check if top level (type) is checked if so check all children
                    if (ndeType.Checked)
                    {
                        ndeTemp.Checked = false;

                        //check children
                        ndeGroup = ndeType;
                        intNum = 0;

                        foreach (TreeNode ndeChild in ndeType.Nodes)
                        {
                            ndeChild.Checked = false;
                        }
                    }
                    else
                    {
                        //first check if top level (type) is unchecked if so uncheck all children
                        {
                            //check children
                            foreach (TreeNode ndeChild in ndeType.Nodes)
                            {
                                ndeChild.Checked = false;
                            }
                        }
                    }
                }
            }
        }

        public static void UnCheckAll(TreeNode ndeParent)
        {
            /*
               Created 01/08/2025 By Roger Williams

               UnChecks everything!

               VARS

               ndeParent    - root node

            */

            ndeParent.Checked = false;

            foreach (TreeNode ndeTemp in ndeParent.Nodes)
            {
                foreach (TreeNode ndeType in ndeTemp.Nodes)
                {
                    //first check if top level (type) is checked if so check all children
                    ndeType.Checked = false;
                    
                    //now uncheck if all child nodes
                    foreach (TreeNode ndeChild in ndeType.Nodes)
                    {
                        ndeChild.Checked = false;
                    }
                }
            }
        }

        public static void CheckGroup(TreeNode ndeParent)
        {
            /*
               Created 01/08/2025 By Roger Williams

               Checks if all nodes in group e.g. forms are checked if so checks children

               VARS

               ndeParent        - parent node (need to filter for type node contents e.g. form)

            */

            TreeNode ndeGroup = null;

            //check if has no tag if so is type
            if (ndeParent.Tag == null)
            {
                foreach (TreeNode ndeType in ndeParent.Nodes)
                {
                    ndeType.Checked = true;
                }
            }
        }

        public static void CheckGroupFromChild(TreeNode ndeParent)
        {
            /*
               Created 01/08/2025 By Roger Williams

               Checks if all nodes in group e.g. forms are checked if so checks parent

               VARS

               ndeParent        - child node (need to filter for type node contents e.g. form)

            */

            int intNum = 0;
            TreeNode ndeGroup = null;

                //now check if all child nodes checked if so checks parent
                ndeGroup = ndeParent.Parent;
                intNum = 0;

                foreach (TreeNode ndeChild in ndeParent.Parent.Nodes)
                {
                    if (ndeChild.Tag != null)
                    {
                        if (ndeChild.Checked)
                        {
                            intNum++;
                        }
                    }
                }

                if (intNum == ndeGroup.Nodes.Count)
                {
                    //check parent i.e. type e.g. Form
                    ndeGroup.Checked = true;
                }
        }

        public static void UnCheckGroup(TreeNode ndeParent)
        {
            /*
               Created 01/08/2025 By Roger Williams

               Unchecks if all nodes in group e.g. forms are checked if so unchecks children

               VARS

               ndeParent        - parent node (need to filter for type node contents e.g. form)

            */

            int intNum = 0;
            TreeNode ndeGroup = null;

            foreach (TreeNode ndeType in ndeParent.Nodes)
            {
                ndeType.Checked = false;
            }
        }

        public static void FindValueInTreeAndCheck(TreeNode ndeParent, string strValue)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Checks for any item in nodes with tag of 1 that matches value and sets checked to true

               VARS

               ndeParent    - where to search
               strValue     - value to look for


            */

            void CheckAllChildren(TreeNode ndeParent)
            {
                TreeNode ndeCurChild = null;

                foreach (TreeNode ndeArea in ndeParent.Nodes)
                {
                    if (ndeArea.Nodes.Count != 0)
                    { 
                        foreach (TreeNode ndeType in ndeArea.Nodes)
                        {
                            foreach (TreeNode ndeChild in ndeType.Nodes)
                            {
                                if (ndeChild.Tag != null)
                                {
                                    ndeCurChild = ndeType;

                                    if (ndeChild.Text == strValue)
                                    {
                                        ndeChild.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                  else
                    {
                        if (ndeArea.Text == strValue)
                        {
                            ndeArea.Checked = true;
                        }
                    }


                 }
            }

            CheckAllChildren(ndeParent);
        }

        public static bool CheckAnyTreeItemSelected(TreeNode ndeParent)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Checks if checked = true for any item in nodes with tag of 1 AND checked

               VARS

               ndeParent    - where to search

            */

            bool blnFound = false;

            void CheckAllChildren(TreeNode ndeNewParent)
            {
                foreach (TreeNode ndeTemp in ndeNewParent.Nodes)
                {

                    if (ndeTemp.Tag != null)
                    {
                        if (ndeTemp.Checked)
                        {
                            blnFound = true;
                        }
                    }

                    CheckAllChildren(ndeTemp);
                }
            }

            CheckAllChildren(ndeParent);
            return blnFound;
        }

        public static void ResetTree(TreeNode ndeParent)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Resets checked to false for all items in nodes with tag of 1

               VARS

               ndeParent    - where to search

            */
            void ResetAllChildren(TreeNode ndeNewParent)
            { 
                foreach (TreeNode ndeTemp in ndeNewParent.Nodes)
                {

                    ndeTemp.Checked = false;
                    ResetAllChildren(ndeTemp);
                }
            }

            ResetAllChildren(ndeParent);
        }

        public static void FormLocationChanged(object sender, EventArgs e)
        {
            /*
               Created 13/03/2025 By Roger Williams

               Stops form being moved over show menu button!

            */
            Form frmTemp = (Form)sender;
            
            if (frmTemp.Left < 20)
            {
                frmTemp.Left = 20;
            }
        }

        public static void OpenSelectedForm(string strForm)
        {
            /*
               Created 10/03/2025 By Roger Williams

               shows the form in strForm

            */


            foreach (Form frmTemp in Application.OpenForms)
            {
                if (frmTemp.Text == strForm)
                {
                    frmTemp.BringToFront();
                }
            }

        }
    
        public static void EnableDisableForm(Form frmTemp, string strFirstControl, bool blnEnable)
        {
            /*
               Created 18/02/2025 By Roger Williams

                enables/disables form controls except:
            
                btnNew, btnClose and control name passed in strFirstControl

            */

            foreach (Control ctlTemp in frmTemp.Controls)
            {
                try
                {
                    if (ctlTemp.Name != strFirstControl && ctlTemp.Name != "BTNNew" && ctlTemp.Name != "BTNClose")
                    {
                        ctlTemp.Enabled = blnEnable;
                    }
                }
                catch (Exception ex)
                {
                    //Whoops! - ignore only here if looking for a property a control does not have
                }
            }


        }

        public static void UpdateStatusBar(ToolStripStatusLabel STLSTatus, string strWhat)
        /*
            Created 03/07/2025 By Roger Williams

            updates form status bar with passed text

             VARS
        
            STLStatus   - status bar label
            strWhat     - text

         */


        {
            STLSTatus.Text = strWhat;
        }

        public static void ResetForm(Form frmTemp, string strIgnore)
        {
            /*
             Created 18/02/2025 By Roger Williams

             resets the passed form vars controls except strIgnore

              resets:
                - textbox
                - checkbox
                - listview
                - treeview
                - combobox
                - radiobutton

            */


            int intNum = 0;
 
            foreach (Control ctlTemp in frmTemp.Controls)
            {
                if (ctlTemp is TabControl)
                {
                    //iterate through tab page controls
                    for (intNum = 0; intNum < ((TabControl)ctlTemp).TabPages.Count; intNum++)
                    {
                        foreach (Control ctlTemp2 in ((TabControl)ctlTemp).TabPages[intNum].Controls)
                        {
                            try
                            {
                                if (ctlTemp2.Name != strIgnore)
                                {
                                    if (ctlTemp2 is ComboBox || ctlTemp2 is TextBox)
                                    {
                                        ctlTemp2.Text = "";
                                    }
                                    if (ctlTemp2 is CheckBox)
                                    {
                                        ((CheckBox)ctlTemp2).Checked = false;
                                    }
                                    if (ctlTemp2 is RadioButton)
                                    {
                                        ((RadioButton)ctlTemp2).Checked = false;
                                    }
                                    if (ctlTemp2 is ListView)
                                    {
                                        ((ListView)ctlTemp2).Items.Clear();
                                    }
                                    if (ctlTemp2 is TreeView)
                                    {
                                        ((TreeView)ctlTemp2).Nodes.Clear();
                                    }
                                    if (ctlTemp2 is NumericUpDown)
                                    {
                                        ((NumericUpDown)ctlTemp2).Value = ((NumericUpDown)ctlTemp2).Minimum;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //whoops! - ignore only here if looking for a property a control does not have
                            }
                        }
                    }
                }
                //is it a panel?
                if (ctlTemp is Panel)
                {
                    //iterate through panel controls
                    foreach (Control ctlTemp2 in ((Panel)ctlTemp).Controls)
                    {
                        try
                        {
                            if (ctlTemp2.Name != strIgnore)
                            {
                                if (ctlTemp2 is ComboBox || ctlTemp2 is TextBox)
                                {
                                    ctlTemp2.Text = "";
                                }
                                if (ctlTemp2 is CheckBox)
                                {
                                    ((CheckBox)ctlTemp2).Checked = false;
                                }
                                if (ctlTemp2 is RadioButton)
                                {
                                    ((RadioButton)ctlTemp2).Checked = false;
                                }
                                if (ctlTemp2 is ListView)
                                {
                                    ((ListView)ctlTemp2).Items.Clear();
                                }
                                if (ctlTemp2 is TreeView)
                                {
                                    ((TreeView)ctlTemp2).Nodes.Clear();
                                }
                                if (ctlTemp2 is NumericUpDown)
                                {
                                    ((NumericUpDown)ctlTemp2).Value = ((NumericUpDown)ctlTemp2).Minimum;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //whoops! - ignore only here if looking for a property a control does not have
                        }
                    }
                }

                try
                {
                    if (ctlTemp.Name != strIgnore)
                    { 
                        if (ctlTemp is ComboBox || ctlTemp is TextBox)
                        {
                            ctlTemp.Text = "";
                        }
                        if (ctlTemp is CheckBox)
                        {
                            ((CheckBox)ctlTemp).Checked = false;
                        }
                        if (ctlTemp is RadioButton)
                        {
                            ((RadioButton)ctlTemp).Checked = false;
                        }
                        if (ctlTemp is ListView)
                        {
                            ((ListView)ctlTemp).Items.Clear();
                        }
                        if (ctlTemp is TreeView)
                        {
                            ((TreeView)ctlTemp).Nodes.Clear();
                        }
                        if (ctlTemp is NumericUpDown)
                        {
                            ((NumericUpDown)ctlTemp).Value = ((NumericUpDown)ctlTemp).Minimum;
                        }
                    }

                }
                catch (Exception ex)
                {
                    //whoops! - ignore only here if looking for a property a control does not have
                }
            }
        }

        public static bool ValueInCombobox(ComboBox CMBTemp, string strValue)
        {
            /*
             Created 05/03/2025 By Roger Williams

             Validates contens of passed combobox to see if passed value is in the list

             VARS

             cbmtemp        - combobox to validate
             strvalue       - what to find  

             returns true if ok

            */

            int intNum = 0;

            if (strValue == "")
            {
                return false;
            }

            intNum = CMBTemp.Items.IndexOf(strValue);

            if (intNum == -1)
            {
                CMBTemp.Text = "";
                CMBTemp.Focus();
                MessageBox.Show("Data Not In List", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
        public static bool ValidateRequiredFields(Form frmTemp)
        {
        /*
         Created 17/02/2025 By Roger Williams

         Validates form required fields populated - uses tag if 1 required field

         VARS

         frmTemp        - form to validate

         returns true if ok

        */
            List<string> lstErrors = new List<string>();
            string strTemp = "";
            int intNum = 0;

            foreach (Control ctlTemp in frmTemp.Controls)
            {
                try
                {
                    if (ctlTemp is TabControl)
                    {
                        //iterate through tab page controls
                        for (intNum = 0; intNum < ((TabControl)ctlTemp).TabPages.Count; intNum++)
                        {
                            ((TabControl)ctlTemp).SelectedIndex = intNum;

                            //check each page for required fields
                            foreach (Control ctlTab in ((TabControl)ctlTemp).SelectedTab.Controls)
                            {
                                try
                                {
                                    if (ctlTab.Tag != null)
                                    {
                                        if (ctlTab.Tag.ToString() == "1")
                                        {
                                            strTemp = ctlTab.Name.Substring(3, ctlTab.Name.Length - 3);

                                            if (ctlTab.Text.Length == 0)
                                            {
                                                strTemp = ((TabControl)ctlTemp).SelectedTab.Controls["LBL" + strTemp].Text;
                                                lstErrors.Add(strTemp);
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //whoops! - ignore only here if looking for a property a control does not have
                                }
                            }
                        }
                        //reset tab selected
                        ((TabControl)ctlTemp).SelectedIndex = 0;
                    }

                    //check if panel
                    if (ctlTemp is Panel)
                    {
                        //iterate panel controls
                        //check each page for required fields
                        foreach (Control ctlTab in ((Panel)ctlTemp).Controls)
                        {
                            try
                            {
                                if (ctlTab.Tag != null)
                                {
                                    if (ctlTab.Tag.ToString() == "1")
                                    {
                                        strTemp = ctlTab.Name.Substring(3, ctlTab.Name.Length - 3);

                                        if (ctlTab.Text.Length == 0)
                                        {
                                            strTemp = ((Panel)ctlTemp).Controls["LBL" + strTemp].Text;
                                            lstErrors.Add(strTemp);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //whoops! - ignore only here if looking for a property a control does not have
                            }
                        }
                    }

                    if (ctlTemp.Tag != null)
                    {
                        if (ctlTemp.Tag.ToString() == "1")
                        {
                            strTemp = ctlTemp.Name.Substring(3, ctlTemp.Name.Length - 3);

                            if (ctlTemp.Text.Length == 0)
                            {
                                strTemp = frmTemp.Controls["LBL" + strTemp].Text;
                                lstErrors.Add(strTemp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //whoops! - ignore only here if looking for a property a control does not have
                }
            }

            if (lstErrors.Count != 0)
            {
              strTemp = "";
              //create messagebox to user showing missing required fields
              for (intNum = 0; intNum != lstErrors.Count; intNum++)
              {
                  strTemp = strTemp + lstErrors[intNum].ToString() + "\n";
              }

              MessageBox.Show("These Fields Are Missing Required Data:\n\n" + strTemp, "Required Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }
        }

        //class end
    }
    }
