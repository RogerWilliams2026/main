using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using RogJobCRMPlus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
  Createed 04/03/2026 By Roger Williams

  handles GUI manipulation
  added facility to populate form labels FROM SQL column descriptions!


*/
namespace RogJobCRMPlus.Modules
{
    internal class clsView
    {
        //used for theme custom line colour
        //list used to store theme colours
        static string[,,] aryThemeColours = new string[51, 51, 51];
        static int intAryPos = 1;
        static Form frmMainForm = null;

        //used for readonly datagrid columns
        public static Color CNST_INT_READONLYCOLUMNSCOLOUR = Color.LightGray; // .DarkGray;
        public static Color CNST_INT_NORMALCOLUMNSCOLOUR = Color.SteelBlue;

        //form/menu constants
        public static string CNST_STR_MENU_OPENFORMSCONTROL = "MNUOpenWindows";

        //report constants
        public static string CNST_STR_REPORT_JOBS = @"c:\temp\temp1.docx";

        //array - user menu items
        //contents:
        //
        //          area(e.g.Inventory)
        //          type(e.g.Form/Report)
        //          menu item name(e.g.Stock Inventory)
        //          object name(e.g.frmStockItems)
        public static string[,,,] aryUserMenuItems = new string[100, 100, 100, 100];
        public static readonly int CNST_INT_USERMENUITEMSARRAYSIZE = 101; //add one on as zero based


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

        //for drawing
        private static Brush bruTemp;





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

        public static void ReadThemeData()
        {
            /*
             Created 09/07/2025 By Roger Williams
         
             reads theme colours file: \Resources\rogsjobcrmplustheme.thm
             and stores values into theme array            

            */

            string[] aryTemp = null;
            string strTemp = string.Empty; ;
            StreamReader stmRead = null;
            int intNum = 1;

            strTemp = Modules.clsData.CNST_STR_CUSTOMTHEMEPATH; 

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
                for (intNum = 1; intNum != 51; intNum++)
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
                    aryTemp = strTemp.Split(',');

                    aryThemeColours[intAryPos, 0, 0] = aryTemp[0].Trim();
                    aryThemeColours[0, intAryPos, 0] = aryTemp[1].Trim();
                    aryThemeColours[0, 0, intAryPos] = aryTemp[2].Trim();
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


        public static void SetTheme(Form frmWhere, string[,,] aryWithThemeColours)
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
            MenuStrip MNUTemp = null;
            Panel PANTemp = null;
            object objTemp = null;
            int intNum = 0;
            
            aryTemp = frmMainForm.Controls.Find(CNST_STR_MENU_OPENFORMSCONTROL, true);

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

            //clear main forms current cycle label
           // frmMainForm.Controls["PANCycle"].Controls["LBLCycle"].Text = string.Empty;
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

            aryTemp = frmMainForm.Controls.Find(CNST_STR_MENU_OPENFORMSCONTROL, true);

            foreach (Control ctlTemp in aryTemp)
            {
                if (ctlTemp != null)
                {
                    //get MNUWindows
                    objTemp = ((MenuStrip)ctlTemp).Items[0];
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
                        MNUTemp.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        MNUTemp.Click += ((frmMain)frmMainForm).WindowMenuClickEvent;
                        //add menu item      
                        MNUWindows.DropDownItems.Add(MNUTemp);
                    }
                }
            }
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
                if (frmTemp.Controls["PANTitle"].Controls["LBLTitle"].Text == strWhat)
                {
                    frmTemp.BringToFront();
                    break;
                }
            }
        }

        public static bool HasUserAccessToForms(string strArea)
        {
            /*

                Created 05/08/2025 By Roger Williams
           
                Checks aryUserMenuItems column 2 to see if user has access to any
                forms in that area
            

                VARS

                strArea  - section e.g. Inventory

            */

            int intNum = 1;
            bool blnOk = false;

            while (aryUserMenuItems[intNum, 0, 0, 0] != null)
            {
                if (aryUserMenuItems[0, intNum, 0, 0] == "Form" && aryUserMenuItems[intNum, 0, 0, 0] == strArea)
                {
                    blnOk = true;
                    break;
                }

                intNum++;
            }

            return blnOk;
        }

        public static bool HasUserAccessToReports(string strArea)
        {
            /*

                Created 05/08/2025 By Roger Williams
           
                Checks aryUserMenuItems column 2 to see if user has access to any
                reports in that area
            

                VARS

                strArea  - section e.g. Inventory

            */

            int intNum = 1;
            bool blnOk = false;

            while (aryUserMenuItems[intNum, 0, 0, 0] != null)
            {
                if (aryUserMenuItems[0, intNum, 0, 0] == "Report" && aryUserMenuItems[intNum, 0, 0, 0] == strArea)
                {
                    blnOk = true;
                    break;
                }

                intNum++;
            }

            return blnOk;
        }

        public static bool HasUserAccessToOperations(string strArea)
        {
            /*

                Created 05/08/2025 By Roger Williams
           
                Checks aryUserMenuItems column 2 to see if user has access to any
                operationin that area
            

                VARS

                strArea  - section e.g. Inventory

            */

            int intNum = 1;
            bool blnOk = false;

            while (aryUserMenuItems[intNum, 0, 0, 0] != null)
            {
                if (aryUserMenuItems[0, intNum, 0, 0] == "Operation" && aryUserMenuItems[intNum, 0, 0, 0] == strArea)
                {
                    blnOk = true;
                    break;
                }

                intNum++;
            }

            return blnOk;
        }


        public static void OpenForm(string strWhat)
        {
            /*
               Created 17/02/2025 By Roger Williams

               opens form adds to open forms combobox sets as MDI child

            */

            Form frmTemp = null;
            Button BTNTemp = null;
            string strTemp = String.Empty;

            Panel PANTemp = null;
            MenuStrip MNUTemp = null;

            //first make sure form not already open
            foreach (Form frmFind in Application.OpenForms)
            {
                if (frmFind.Name == strWhat)
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
                BTNTemp = (Button)frmMainForm.Controls["BTNShowHide"];

                //hide menu
                if ( Convert.ToInt16(BTNTemp.Tag) == 1)
                { 
                    BTNTemp = (Button)frmMainForm.Controls["BTNShowHide"];
                    BTNTemp.PerformClick();
                }

                //show arrange menu
                PANTemp = (Panel)frmMainForm.Controls["PANMenu"];
                MNUTemp = (MenuStrip)PANTemp.Controls["MNUMainMenu"];
                MNUTemp.Items["MNUArrangeWindows"].Visible = true;
                //position form
                frmTemp.StartPosition = FormStartPosition.Manual;
                frmTemp.Left = 170;
                frmTemp.Top = 70;
                frmTemp.Visible = true;
                SetFormTitle(frmTemp);
                SetTheme(frmTemp, aryThemeColours);
                //add to open form menu
                AddToOpenForms(frmTemp.Controls["PANTitle"].Controls["LBLTitle"].Text);
            }
        }

        public static void FormLocationChanged(object sender, EventArgs e)
        {
            /*
               Created 13/03/2025 By Roger Williams

               Stops form being moved over show menu button or main menus!

            */
            Form frmTemp = (Form)sender;

            if (frmTemp.Left < 30)
            {
                frmTemp.Left = 30;
            }

            if (frmTemp.Top < 64)
            {
                frmTemp.Top = 64;
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

            void CheckAllChildren(TreeNode ndeNewParentCur)
            {
                foreach (TreeNode ndeTemp in ndeNewParentCur.Nodes)
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

        public static List<string> GetAnyTreeItemSelected(TreeNode ndeParent)
        {
            /*
               Created 08/07/2025 By Roger Williams

               Returns a list for any item in nodes with tag of 1 AND checked


               VARS

               ndeParent    - where to search

            */

            List<string> lstTemp = new List<string>();

            void CheckAllChildren(TreeNode ndeParentCur)
            {
                foreach (TreeNode ndeTemp in ndeParentCur.Nodes)
                {

                    if (ndeTemp.Tag != null)
                    {
                        if (ndeTemp.Checked)
                        {
                            lstTemp.Add(ndeTemp.Text);
                        }
                    }

                    CheckAllChildren(ndeTemp);
                }
            }

            CheckAllChildren(ndeParent);
            return lstTemp;
        }

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

            void CheckAllChildren(TreeNode ndeParentCur)
            {
                TreeNode ndeCurChild = null;

                foreach (TreeNode ndeArea in ndeParentCur.Nodes)
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

            void CheckAllChildren(TreeNode ndeNewParentCur)
            {
                foreach (TreeNode ndeTemp in ndeNewParentCur.Nodes)
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
                        if (ctlTemp.Name != strFirstControl && ctlTemp.Name != "BTNClose" && ctlTemp.Name != "PANTitle")
                        {
                            ctlTemp.Enabled = blnEnable;
                        }
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
                                        ctlTemp2.Text = string.Empty; ;
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
                                    ctlTemp2.Text = string.Empty; ;
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
                            ctlTemp.Text = string.Empty; ;
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

             Validates contents of passed combobox to see if passed value is in the list

             VARS

             cbmtemp        - combobox to validate
             strvalue       - what to find  

             returns true if ok

            */

            int intNum = 0;

            if (strValue == string.Empty)
            {
                return false;
            }

            intNum = CMBTemp.Items.IndexOf(strValue);

            if (intNum == -1)
            {
                CMBTemp.Text = string.Empty;
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
            string strTemp = string.Empty; ;
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
                strTemp = string.Empty; ;
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

        public static void SetFormTitle(Form frmWhat)
        {
            /*
                Created 05/03/2026 By Roger Williams

                Sets form title from clsdata->dicFormTitles

                puts text into PANTitle->LBLTitle

            */

            string strTemp = string.Empty;

            Modules.clsTables.dicFormTitles.TryGetValue(frmWhat.Name, out strTemp);
         
            if (strTemp != string.Empty) 
            {
                frmWhat.Controls["PANTitle"].Controls["LBLTitle"].Text = strTemp;
            }
        }
        public static void SetFormCaptions(Form frmWhat, string strTable)
        {
            /*
             Modified 12/03/2026 By Roger Williams
            
             uses: lstTableInfo instead of dictionary

             tried to use the lists FIND method but it refsued to find items in the list (thanks Microsoft)
             so had to slum with a foreach...
            
            
             Modified 05/03/2026 By Roger Williams

             Now also sets form caption

            
             Created 03/03/2026 By Roger Williams

             sets label control text on passed form to SQL column descrptions
             for the passed table

             each data entry form uses format:
             <control type>SQL column name

             e.g.:

             LBLSTKI_ItemID

             as each control label is always a label only need to remove first 3 letters 
             from control name:

             STKI_ItemID



             VARS

             frmWhat        - form to label
             strTable       - table to get descriptions from

            */

            string strTemp = string.Empty;
            string strResult = string.Empty;

            foreach (Control ctlTemp in frmWhat.Controls)
            {
                if (ctlTemp is Label)
                {
                    strTemp = ctlTemp.Name.Substring(3, ctlTemp.Name.Length - 3);

                    foreach (Modules.clsData.TTYPETableInfo typInfo in Modules.clsData.lstTableInfo)
                    {
                        if (typInfo.strColumnName == strTemp)
                        { 
                            ctlTemp.Text = typInfo.strDescription;
                            ((Label)ctlTemp).AutoSize = true;
                            ctlTemp.Refresh();
                        }
                    }
                }
            }

            //set form title make sure only doing it once!
            if (frmWhat.Controls["PANTitle"].Controls["LBLTitle"].Text == string.Empty) 
            {
                SetFormTitle(frmWhat);
            }
        }


        public static void SetFormDataEntryMax(Form frmWhat, string strTable)
        {
            /*
             Created 12/03/2026 By Roger Williams

             Sets maximum text limit for text/comboboxes based on values in: 
             clsData.lstTableInfo             
            

             VARS

             frmWhat        - form to process
             strTable       - table to get lengths for

            */

            string strTemp = string.Empty;
            string strResult = string.Empty;
            Component[] aryComponents = null;

            foreach (Control ctlTemp in frmWhat.Controls)
            {
                if (ctlTemp is Label)
                {
                    strTemp = ctlTemp.Name.Substring(3, ctlTemp.Name.Length - 3);
                    //find in schema
                    foreach (Modules.clsData.TTYPETableInfo typInfo in Modules.clsData.lstTableInfo)
                    {
                        if (typInfo.strColumnName == strTemp)
                        {
                            //see if combobox exists for that field name
                            aryComponents = frmWhat.Controls.Find("CMB" + strTemp, false);

                            if (aryComponents.Length != 0)
                            {
                                ((ComboBox)aryComponents[0]).MaxLength = typInfo.intLength;
                            }
                            //see if textbox exists for that field name
                            aryComponents = frmWhat.Controls.Find("TXT" + strTemp, false);

                            if (aryComponents.Length != 0)
                            {
                                ((TextBox)aryComponents[0]).MaxLength = typInfo.intLength;
                            }

                            ctlTemp.Text = typInfo.strDescription;
                            ((Label)ctlTemp).AutoSize = true;
                            ctlTemp.Refresh();
                        }
                    }
                }
            }

            //set form title make sure only doing it once!
            if (frmWhat.Controls["PANTitle"].Controls["LBLTitle"].Text == string.Empty)
            {
                SetFormTitle(frmWhat);
            }
        }



        public static void FillTitleBar(Graphics graWhat, Color clrColour, int intX, int intLength, int intY)
        {
            /*
              Created 03/03/2026 By Roger Williams

              fills titlebar with passed colour

              VARS

              graWhat   - where to draw
              clrColour - colour to use
              intX      - start from
              intLength - how long?
              intY      _ how high


            */

            bruTemp = new SolidBrush(clrColour);
            graWhat.FillRectangle(bruTemp, intX, 0, intLength, intY);
        }


        public static void PopulateComboBoxes(ComboBox CMBTemp, string strTable, string strKeyField, string strKeyFieldValue, string strSecondFieldName, string strSecondFieldValue, 
                                              string strWHERE, bool blnDistinct, bool blnDesc)
        {
            /*
             
              Modified 29/03/2025 By Roger Williams

              added sort ability: blnDesc
              sorts descending if true

              
              
              Created 17/02/2025 By Roger Williams

              Populates the comboboxes with table values using first non
              identity seed as column, unless user specifies a key field
              and optional sort value

              VARS

              CMBTemp             - combobox to populate
              strTable            - table to read from

              Optional:

              strKeyField         - key field name 
              strKeyFieldValue    - key field value always handled as text 
                                    in a commercial system would also pass data type
              strSecondFieldName  - another field name 
              strSecondFieldValue - another field value always handled as text 
                                    in a commercial system would also pass data type
              strWHERE             - specify WHERE clause
              blnDistinct          - use DISTINCT 

             Note: if NOT using blnDistinct strKeyField MUST have a value!


            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;

            //clear combo
            CMBTemp.Items.Clear();

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();

                    if (blnDistinct)
                    {
                        SQLCmd.CommandText = "SELECT DISTINCT " + strKeyField + " FROM " + strTable;
                    }
                    else
                    {
                        SQLCmd.CommandText = "SELECT * FROM " + strTable;
                    }
                    if (strKeyField.Length != 0 && blnDistinct == false)
                    {
                        SQLCmd.CommandText += " WHERE " + strKeyField + " = '" + strKeyFieldValue + "'";
                    }

                    if (strSecondFieldName.Length != 0)
                    {
                        SQLCmd.CommandText += " AND " + strSecondFieldName + " = '" + strSecondFieldValue + "'";
                    }

                    if (strWHERE.Length != 0)
                    {
                        if (strKeyField.Length != 0)
                        {
                            strWHERE = strWHERE.ToUpper();
                            strWHERE = strWHERE.Replace("WHERE", " AND ");
                            SQLCmd.CommandText += " " + strWHERE;
                        }
                        else
                        {
                            SQLCmd.CommandText += " " + strWHERE;
                        }
                    }

                    if (strKeyField.Length != 0)
                    {
                        if (blnDesc)
                        {
                            SQLCmd.CommandText += " ORDER BY " + strKeyField + " DESC";
                        }
                    }

                        //add ;
                        SQLCmd.CommandText += ";";

                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    //Note: 0 = ID field
                    while (SQLRead.Read())
                    {
                        if (strTable == Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_CONTRACTTYPE)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_HOURS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_WHERE)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_STATUS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_TABLE_CRM_JOBS)
                        {
                            CMBTemp.Items.Add(SQLRead[0].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS)
                        {
                            CMBTemp.Items.Add(SQLRead[0].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_TABLE_CRM_CONTACTS)
                        {
                            CMBTemp.Items.Add(SQLRead[0].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER)
                        {
                            CMBTemp.Items.Add(SQLRead[0].ToString());
                        }

                        

                        //admin comboboxes
                        if (strTable == Modules.clsTables.CNST_STR_LOGIN)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_MENU_GROUPS)
                        {
                            CMBTemp.Items.Add(SQLRead[0].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_MENU_MENUITEMS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_MENU_AREAS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_MENU_USERGROUPS)
                        {
                            CMBTemp.Items.Add(SQLRead[0].ToString());
                        }
                    }

                    SQLRead.Close();
                    SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Table " + strTable + " - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        static public void ArrangeChildForms(Form frmWhat, int intHow)
        {
            /*
              Created 12/03/2026 By Roger Williams

              arranges the MDI child forms, can be:
              cascade
              horizontal
              vertical

              VARS

              frmWhat - MDI parent to use
              intHo   - 1 = cascade, 2=horizontal, 3=vertical

            */
          
            int intFirstFormX = 130;
            int intFirstFormY = 80;

            switch (intHow)
            {
                case 1:  //cascade
                    {
                        foreach (Form frmTemp in frmWhat.MdiChildren)
                        {
                            frmTemp.Left = intFirstFormX;
                            frmTemp.Top = intFirstFormY;
                            intFirstFormX += 40;
                            intFirstFormY += 40;
                        }
                        break;
                    }
                case 2:  //horizontal
                    {
                        foreach (Form frmTemp in frmWhat.MdiChildren)
                        {
                            frmTemp.Left = intFirstFormX;
                            frmTemp.Top = intFirstFormY;
                            intFirstFormX += 40;
                        }
                        break;
                    }
                case 3:  //vertical
                    {
                        foreach (Form frmTemp in frmWhat.MdiChildren)
                        {
                            frmTemp.Left = intFirstFormX;
                            frmTemp.Top = intFirstFormY;
                            intFirstFormY += 40;
                        }
                        break;
                    }
            }
        }





        //class end
    }
}
