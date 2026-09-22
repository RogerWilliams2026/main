using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogJobCRMPlus.Modules
{
    internal class clsData
    {
        //data source = server name
        public static readonly string CNST_STR_ODBC = "Data Source=DESKTOP-694Q8HR;Initial Catalog=RogJobCRMPlus;Persist Security Info=True;User ID=sa;Password=RogSQLServer1;TrustServerCertificate=true";

        //rogjobcrmplus installation folder - this is where the reports are stored!
        public static readonly string CNST_STR_INSTALLATIONPATH = "C:\\RogJobCRMPlus";
        public static readonly string CNST_STR_MAILSHOTPATH = CNST_STR_INSTALLATIONPATH + "\\Mailshots";
        public static readonly string CNST_STR_RESOURCEPATH = CNST_STR_INSTALLATIONPATH + "\\Resources";

        //resource file names
        public static readonly string CNST_STR_CUSTOMSQLERRORFILE = "Errorlist.res";
        public static readonly string CNST_STR_CUSTOMTHEMEFILE = "rogjobcrmplustheme.thm";

        //resource files locations
        public static readonly string CNST_STR_SQLCUSTOMERRORSPATH = CNST_STR_RESOURCEPATH + "\\Errorlist.res";
        public static readonly string CNST_STR_CUSTOMTHEMEPATH = CNST_STR_RESOURCEPATH + "\\rogjobcrmplustheme.thm";

        //SQL server custom error handling
        static string CNST_STR_ERRORFILEPATH = string.Empty;
        public static Dictionary<int, string> dicSQLErrors = new Dictionary<int, string>();

        //global variable for passback selected primary key from frmFind
        public static object objFindSelected = null;

        public static string strLoggedInUser = string.Empty;
        private static string strLoggedInIP = string.Empty;

        //custom SQL error handler default values ONLY used if errorslist.res does not exist i.e. first run
        private static List<string> lstSQLErrorsList = new List<string> { "1000,Login Not Found", "1001,User Not Found Or Password Incorrect", "1003, Error Creating LOT Record!", "1004, Error Reading User Password" };
        //default theme created on first run
        private static string strDefaultTheme =
                "Button, BackColor, Ivory\n" +
                "Button, ForeColor, Blue\n" +
                "CheckBox,BackColor,DarkCyan\n" +
                "CheckBox, ForeColor, White\n" +
                "ComboBox,BackColor,DarkCyan\n" +
                "ComboBox, ForeColor, White\n" +
                "DataGridView,BackgroundColor,DarkCyan\n" +
                "DataGridView, GridColor, Wheat\n" +
                "DefaultCellStyle,BackColor,MediumBlue\n" +
                "DefaultCellStyle, ForeColor, White\n" +
                "ColumnHeadersDefaultCellStyle,BackColor,DarkCyan\n" +
                "ColumnHeadersDefaultCellStyle, ForeColor, White\n" +
                "RowsDefaultCellStyle,BackColor,DarkCyan\n" +
                "RowsDefaultCellStyle, ForeColor, White\n" +
                "RowHeadersDefaultCellStyle,BackColor,SteelBlue\n" +
                "RowHeadersDefaultCellStyle, ForeColor, DeepSkyBlue\n" +
                "Form,BackColor,CadetBlue\n" +
                "Form, ForeColor, White\n" +
                "GroupBox,BackColor,DarkCyan\n" +
                "GroupBox, ForeColor, White\n" +
                "Label,BackColor,DarkCyan\n" +
                "Label, ForeColor, White\n" +
                "ListBox,BackColor,DarkCyan\n" +
                "ListBox, ForeColor, White\n" +
                "ListView,BackColor,DarkCyan\n" +
                "ListView, ForeColor, White\n" +
                "NumericUpDown,BackColor,DarkCyan\n" +
                "NumericUpDown, ForeColor, White\n" +
                "Panel,BackColor,DarkCyan\n" +
                "Panel, ForeColor, White\n" +
                "RadioButton,BackColor,DarkCyan\n" +
                "RadioButton, ForeColor, White\n" +
                "RadioButton,Font.Color,Black\n" +
                "StatusStrip, BackColor, RoyalBlue\n" +
                "StatusStrip,ForeColor,White\n" +
                "TabPage, BackColor, DarkCyan\n" +
                "TabPage,ForeColor,White\n" +
                "TextBox, BackColor, DarkCyan\n" +
                "TextBox,ForeColor,White\n" +
                "ToolStripStatusLabel, BackColor, DarkCyan\n" +
                "ToolStripStatusLabel,ForeColor,White\n" +
                "TreeView, BackColor, DarkCyan\n" +
                "TreeView,ForeColor,White";

        //for reading scheme table data for use with savrecord etc.
        public struct TTYPETableInfo
        {
            public string strTableName;
            public string strColumnName;
            public string strDescription;
            public string strDataType;
            public int intLength;
        }

        public static List<TTYPETableInfo> lstTableInfo = new List<TTYPETableInfo>();


        public static bool InitCustomErrorhandler(string strPath)
        {
            /*
             Created 02/07/2025 By Roger Williams

             Inits custom SQL error resource file path variable
             Then loads it into dictionary: dicSQLErrors

             Checks if passed path is null or file does not exist

             VAR

             strpath    - location of resource file

             RETURNS

             true if ok

            */

            StreamReader strmTemp = null;
            string strTemp = string.Empty; ;
            string strError = string.Empty; ;
            string strMsg = string.Empty; ;

            if (strPath.Length == 0 || !File.Exists(strPath))
            {
                return false;
            }

            CNST_STR_ERRORFILEPATH = strPath;

            strmTemp = new StreamReader(CNST_STR_ERRORFILEPATH);

            while (!strmTemp.EndOfStream)
            {
                strTemp = strmTemp.ReadLine();
                //split into error number and error message

                //purposely add,
                strError = strTemp.Substring(0, strTemp.IndexOf(",") + 1);
                strMsg = strTemp.Remove(0, strError.Length);
                //remove it
                strError = strError.Remove(strError.Length - 1);

                dicSQLErrors.Add(Convert.ToInt32(strError), strMsg);
            }

            strmTemp.Close();
            strmTemp.Dispose();
            return true;
        }

        public static string GetPassword(string strUser)
        {
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            string strTemp;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SP_GetPassword";
                    SQLCmd.CommandType = CommandType.StoredProcedure;
                    SQLCmd.Parameters.Add("@User", SqlDbType.VarChar, 30).Value = strUser;
                    SQLCmd.Parameters.Add("@Password", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                    SQLCmd.Parameters.Add("@ErrorCustom", SqlDbType.Int).Direction = ParameterDirection.Output;
                    SQLCmd.ExecuteNonQuery();

                    if (Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value) == 0)
                    {
                        return (SQLCmd.Parameters["@Password"].Value.ToString());
                    }
                    else
                    {
                        //show error to user
                        dicSQLErrors.TryGetValue(Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value), out strTemp);
                        strTemp = SQLCmd.Parameters["@ErrorCustom"].Value.ToString() + "\n\n" + strTemp;
                        MessageBox.Show("Error: /n\n" + strTemp);

                        //return nothing to signify error to calling procedure
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public static bool CheckLoginExists(string strUser)
        {
            /*
             Created 18/06/2025 By Roger Williams

             checks passed user exists using: SP_CheckLoginExists

             VARS

             strUser       - user name


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;
            string strTemp = string.Empty; ;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    //      SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_LOGIN + " WHERE LOG_User ='" + strUser + "';";
                    //      SQLCmd.CommandType = CommandType.Text;

                    SQLCmd.CommandText = "SP_CheckLoginExists";
                    SQLCmd.CommandType = CommandType.StoredProcedure;
                    SQLCmd.Parameters.Add("@User", SqlDbType.VarChar, 30).Value = strUser;
                    SQLCmd.Parameters.Add("@ErrorCustom", SqlDbType.Int).Direction = ParameterDirection.Output;
                    SQLCmd.ExecuteNonQuery();

                    if (Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value) == 0)
                    {
                        blnOk = true;
                    }
                    else
                    {
                        //dont tel user be silent as this function only needs to say yah or nah!
                        blnOk = false;
                    }

                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckLogin(string strUser, string strPassword)
        {
            /*
             Created 14/02/2025 By Roger Williams

             checks passed user and password are correct

             VARS

             strUser       - user name
             strPassword   - password


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            string strTemp = string.Empty; ;
            bool blnOk = false;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_LOGIN + " WHERE LOG_User ='" + strUser + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    if (SQLRead != null)
                    {
                        SQLRead.Read();
                        //get password
                        strTemp = SQLRead["LOG_Password"].ToString();
                        //decrypt
                        //    strTemp = EncryptPassword(strTemp);
                        //compare with strPassword
                        if (strTemp == strPassword)
                        {
                            blnOk = true;
                        }
                        else
                        {
                            blnOk = false;
                        }
                    }
                    else
                    {
                        blnOk = false;
                    }

                    SQLRead.Close();
                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void CreateCurrentLoginRecord(string strUser)
        {
            /*
             Created 18/02/2025 By Roger Williams

             creates user logged in record in login_current


             VARS

             struser    - name of user

             stores struser in var strLoggedInUser for use by other functions

            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;

            string GetLocalIP()
            {
                /*
                 Created 18/02/2025 By Roger Williams

                 Gets PCs IP address

                 Modified VB code copied from the internet!

                */
                string strIP = string.Empty; ;
                string strHostName = string.Empty; ;
                IPHostEntry IPHost;

                strHostName = Dns.GetHostName();
                IPHost = Dns.GetHostEntry(strHostName);

                foreach (IPAddress IPATemp in IPHost.AddressList)
                {
                    //look for IP4 address only
                    if (IPATemp.AddressFamily == AddressFamily.InterNetwork)
                    {
                        strIP = IPATemp.ToString();
                        //store for later use
                        strLoggedInIP = IPATemp.ToString();
                        return strIP;
                    }
                }
                return strIP;
            }

            try
            {
                //store for later use elsewhere
                strLoggedInUser = strUser;

                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOGIN_CURRENT + " (LOGC_User, LOGC_PCIP)  VALUES ('" + strUser + "','" + GetLocalIP() + "');";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLCmd.ExecuteNonQuery();
                    SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void DeleteCurrentLoginRecord()
        {
            /*
              Created 18/02/2025 By Roger Williams

              deletes user logged in record in login_current

              uses var strLoggedInUser for delete
             */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_LOGIN_CURRENT + " WHERE LOGC_User = '" + strLoggedInUser + "' AND LOGC_PCIP = '" + strLoggedInIP + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLCmd.ExecuteNonQuery();
                    SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        //public static bool IsUserIngroup(string strUser, string strGroup)
        //{
        //    /*
        //     Created 28/07/2025 By Roger Williams

        //     checks passed user is in passed security group

        //     VARS

        //     strUser       - user 
        //     strGroup      - security group


        //    */
        //    SqlConnection SQLConn;
        //    SqlCommand SQLCmd;
        //    bool blnOk = false;

        //    try
        //    {
        //        using (SQLConn = new SqlConnection(CNST_STR_ODBC))
        //        {
        //            SQLConn.Open();
        //            SQLCmd = SQLConn.CreateCommand();
        //            SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_USERGROUPS + " WHERE USRGRP_User ='" + strUser + "' AND USRGRP_Group ='" + strGroup + "';";
        //            SQLCmd.CommandType = CommandType.Text;

        //            if (SQLCmd.ExecuteScalar() != null)
        //            {
        //                blnOk = true;
        //            }

        //            SQLConn.Close();
        //            return blnOk;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Whoops!
        //        MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}


        //public static bool CheckGroupExists(string strGroup)
        //{
        //    /*
        //     Created 83/07/2025 By Roger Williams

        //     checks passed group exists

        //     VARS

        //     strgroup      - group name


        //    */
        //    SqlConnection SQLConn;
        //    SqlCommand SQLCmd;
        //    bool blnOk = false;

        //    try
        //    {
        //        using (SQLConn = new SqlConnection(CNST_STR_ODBC))
        //        {
        //            SQLConn.Open();
        //            SQLCmd = SQLConn.CreateCommand();
        //            SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_GROUPS + " WHERE GRP_Group ='" + strGroup + "';";
        //            SQLCmd.CommandType = CommandType.Text;

        //            if (SQLCmd.ExecuteScalar() != null)
        //            {
        //                blnOk = true;
        //            }
        //            else
        //            {
        //                //dont tel user be silent as this function only needs to say yah or nah!
        //                blnOk = false;
        //            }

        //            SQLConn.Close();
        //            return blnOk;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Whoops!
        //        MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}

        public static bool CheckMenuItemExists(string strItem)
        {
            /*
             Created 84/07/2025 By Roger Williams

             checks passed menu exists

             VARS

             strgroup      - menu item name


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_MENUITEMS + " WHERE MNU_MenuItemName ='" + strItem + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    if (SQLCmd.ExecuteScalar() != null)
                    {
                        blnOk = true;
                    }
                    else
                    {
                        //dont tel user be silent as this function only needs to say yah or nah!
                        blnOk = false;
                    }

                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckUserGroupExists(string strUserGroup)
        {
            /*
             Created 07/07/2025 By Roger Williams

             checks passed usergroup exists 

             VARS

             strUserGroup       - usergroup name


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            bool blnOk = false;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_USERGROUPS + " WHERE USRGRP_Group ='" + strUserGroup + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    if (SQLCmd.ExecuteScalar() != null)
                    {
                        blnOk = true;
                    }
                    else
                    {
                        //dont tel user be silent as this function only needs to say yah or nah!
                        blnOk = false;
                    }

                    SQLConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




        //*****************other*****************
        public static int GetMenuItemsCountForArea(string strArea)
        {
            /*
             Created 07/07/2025 By Roger Williams

             returns number of records in Menu_MenuItems that have passed area  

             VARS

             strArea       - area


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            int intNum = 0;


            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT  COUNT (*) FROM " + Modules.clsTables.CNST_STR_MENU_MENUITEMS + " WHERE MNU_DisplayWhere ='" + strArea + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    intNum = (int)SQLCmd.ExecuteScalar();

                    SQLConn.Close();
                    return intNum;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return intNum;
            }
        }


        public static int GetMenuItemsCountForGroup(string strGroup)
        {
            /*
             Created 07/07/2025 By Roger Williams

             returns number of records in Menu_Groups that have passed group

             VARS

             strGroup       - group


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            int intNum = 0;


            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT  COUNT (*) FROM " + Modules.clsTables.CNST_STR_MENU_GROUPS + " WHERE GRP_Group ='" + strGroup + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    intNum = (int)SQLCmd.ExecuteScalar();

                    SQLConn.Close();
                    return intNum;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return intNum;
            }
        }

        public static List<string> GetMenuItemsForArea(string strArea)
        {
            /*
             Created 07/07/2025 By Roger Williams

             returns list of records in Menu_MenuItems that have passed area

             VARS

             strArea       - area


            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            List<string> lstMenuItems = new List<string>();
            SqlDataReader SQLRead = null;


            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_MENUITEMS + " WHERE MNU_DisplayWhere ='" + strArea + "' ORDER BY MNU_MenuItemName;";
                    SQLCmd.CommandType = CommandType.Text;

                    SQLRead = SQLCmd.ExecuteReader();

                    if (SQLRead != null)
                    {
                        while (SQLRead.Read())
                        { 
                            lstMenuItems.Add(SQLRead.ToString());
                        }
                    }

                    SQLRead.Close();
                    SQLConn.Close();
                    return lstMenuItems;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n" + ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return lstMenuItems;
            }
        }




        public static bool CheckForRecords(string strTable)
        {
            /*
                 Created 03/03/2026 By Roger Williams

                 Checks for records in passed table

                 VARS

                 strTable - table to check

            */
            SqlDataReader SQLRead;
            SqlCommand SQLCmdDesc;
            SqlConnection SQLConn;
            bool blnOk = false;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmdDesc = new SqlCommand("SELECT * FROM " + strTable + ";", SQLConn);
                    SQLRead = SQLCmdDesc.ExecuteReader();

                    //load from dataset
                    if (SQLRead != null)
                    {
                        blnOk = true;
                    }

                    SQLRead.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return blnOk;
            }
        }




        //public static Dictionary<string, string> GetSQLColumnDescriptions(string strTable)
        ///*
        //  Created 02/03/2026 By Roger Williams

        //  Copied and adapted from CoPilot

        //  unusually for CoPilot this actually works, so far it have been right 1/5 questions!

        //*/

        //{
        //    Dictionary<string, string> DICTDesc = new Dictionary<string, string>();

        //    using (var SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
        //    using (var SQLCmd = new SqlCommand(@"
        //            SELECT 
        //                c.name AS ColumnName,
        //                ep.value AS Description
        //            FROM sys.columns c
        //            LEFT JOIN sys.extended_properties ep 
        //                ON ep.major_id = c.object_id
        //                AND ep.minor_id = c.column_id
        //                AND ep.name = 'MS_Description'
        //            WHERE c.object_id = OBJECT_ID(@tableName)
        //            ORDER BY c.column_id;", SQLConn))
        //    {
        //        SQLCmd.Parameters.AddWithValue("@tableName", "dbo." + strTable);
        //        SQLConn.Open();

        //        using (var reader = SQLCmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                var VARColumn = reader["ColumnName"].ToString();
        //                var VARDesc = reader["Description"]?.ToString();

        //                if (VARDesc != string.Empty)
        //                {
        //                    DICTDesc.Add(VARColumn, VARDesc);
        //                }
        //            }
        //        }
        //    }

        //    return DICTDesc;
        //}


        public static void CreateCustomSQLErrorFile()
        {
            /*
              Created 04/03/2026 By Roger Williams

              creates the default SQL custom error message file

            */
            StreamWriter strmFile;

            strmFile = new StreamWriter(Modules.clsData.CNST_STR_RESOURCEPATH + "\\" + Modules.clsData.CNST_STR_CUSTOMSQLERRORFILE);

            foreach (string strTemp in lstSQLErrorsList)
            {
                strmFile.WriteLine(strTemp);
            }

            strmFile.Close();
        }

        public static void CreateDefaultTheme()
        {
            /*
              Created 04/03/2026 By Roger Williams

              creates the default theme file

            */
            StreamWriter strmFile;

            strmFile = new StreamWriter(CNST_STR_RESOURCEPATH + "\\" + CNST_STR_CUSTOMTHEMEFILE);
            strmFile.WriteLine(strDefaultTheme);
            strmFile.Close();
        }

    
        public static string SaveRecord(string strTableName_One, Form frmFrom, bool blnEdit, string strID)
        {
         /*
             Modified 09/03/2026 By Roger Williams
            
             added new parameters so one->many table data can be saved:

             VARS added

             strTableName_Many  - "lines" table name
             
             
            
            
             Modified 05/03/2026 By Roger Williams

             Removed some parameters, list is now:

             VARS

             strTableName   - table name
             frmFrom        - form to read data from
             boolEdit       - false if new record else true
             strID          - ID of record


             Created 07/08/2025 By Roger Williams

             Saves record to passed table, using passed form for the data AND the column names
             as controls with data follow naming convention: <control type><column name>

             VARS

             SQLConn        - open SQL connection
             strTableName   - table name
             frmFrom        - form to read data from
             boolEdit       - false if new record else true
             SQLTrans       - SQl transaction to use

            */
            string strTemp = String.Empty;
            string strFieldName = String.Empty;
            string strControlType = String.Empty;
            string strControlName = String.Empty;
            string strSQL = String.Empty;
            string strAllFieldNames = String.Empty;
            string strAllFieldValues = String.Empty;
            string strWhere = " WHERE ";
            string strError = String.Empty;
            SqlCommand SQLCmd = null;
            SqlTransaction SQLTrans = null;
            SqlConnection SQLConn;

            string FormatValue(string strField, string strValue)
            {
                /*
                     Created 07/08/2025 By Roger Williams

                     Looks through passed tag for data type then returns the passed value in the formattin
                     e.g.: FormatValue(hello,string) returns:

                     "hello"

                     Note: sometimes there will be a | in the tag this is ignored

                */


                string strReturn = String.Empty;
                string strDataType = String.Empty;
                DateTime dteTemp;

                //get datatype for passed field
                // strDataType = clsData.lstTableInfo.Find(res => res.strColumnName == strField).strDataType;
                //find in schema
                foreach (Modules.clsData.TTYPETableInfo typInfo in Modules.clsData.lstTableInfo)
                {
                    if ((typInfo.strColumnName == strField))
                    {
                        strDataType=typInfo.strDataType;

                        switch (strDataType)
                        {
                            case "text":
                            case "string":
                                strReturn = "'" + strValue + "'";
                                break;
                            case "date":
                            case "datetime2":
                            case "datetime":
                                dteTemp = Convert.ToDateTime(strValue);
                                strReturn = "'" + dteTemp.Month.ToString() + "/" + dteTemp.Day.ToString() + "/" + dteTemp.Year.ToString() + "'";
                                break;
                            case "money":
                            case "decimal":
                            case "float":
                            case "int":
                                strReturn = strValue;
                                break;
                            case "bool":
                                if (strValue == "true")
                                {
                                    strReturn = "-1";
                                }
                                else
                                {
                                    strReturn = "0";
                                }

                                break;
                        }
                    }
                }

                return strReturn;
            }

            //func start
            if (blnEdit)
            {
                strSQL = " UPDATE " + strTableName_One + " SET ";
                //get table primarykey
                strWhere += Modules.clsTables.GetPrimaryField(strTableName_One) + " =" + strID;
            }
            else
            {
                strSQL = "INSERT INTO " + strTableName_One + " (";
            }

            //create SQl value strings from controls
            foreach (Control ctlTemp in frmFrom.Controls)
            {
                //every "data bound" control has _ in it as this is part of the naming
                //convention for columns e.g. STKI_ItemID
                if (ctlTemp.Name.Contains("_"))
                {
                    strControlType = ctlTemp.GetType().Name;

                    if (strControlType != "Label")
                    { 
                        //skip first 3 chars as they are control type
                        strFieldName = ctlTemp.Name.Substring(3, ctlTemp.Name.Length - 3);

                        if (blnEdit == false)
                        {
                            //add new record
                            switch (strControlType)
                            {
                                case "DateTimePicker":
                                    strAllFieldNames += strFieldName + ", ";
                                    strAllFieldValues += FormatValue(strFieldName, ((DateTimePicker)ctlTemp).Text) + ",";
                                    break;
                                case "TextBox":
                                    strAllFieldNames += strFieldName + ", ";
                                    strAllFieldValues += FormatValue(strFieldName, ((TextBox)ctlTemp).Text) + ",";
                                    break;
                                case "ComboBox":
                                    strAllFieldNames += strFieldName + ", ";
                                    strAllFieldValues += FormatValue(strFieldName, ((ComboBox)ctlTemp).Text) + ",";
                                    break;
                                case "NumericUpDown":
                                    strAllFieldNames += strFieldName + ", ";
                                    strAllFieldValues += FormatValue(strFieldName, ((NumericUpDown)ctlTemp).Value.ToString()) + ",";
                                    break;
                                case "CheckBox":
                                    strAllFieldNames += strFieldName + ", ";

                                    if (((CheckBox)ctlTemp).Checked)
                                    {
                                        strAllFieldValues += "1" + ",";
                                    }
                                    else
                                    {
                                        strAllFieldValues += "0" + ",";
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            //edit record
                            switch (strControlType)
                            {
                                case "DateTimePicker":
                                    strAllFieldValues += strFieldName + "= " + FormatValue(strFieldName, ((DateTimePicker)ctlTemp).Text) + ",";
                                    break;
                                case "TextBox":
                                    strAllFieldValues += strFieldName + "= " + FormatValue(strFieldName, ((TextBox)ctlTemp).Text) + ",";
                                    break;
                                case "ComboBox":
                                    strAllFieldValues += strFieldName + "= " + FormatValue(strFieldName, ((ComboBox)ctlTemp).Text) + ",";
                                    break;
                                case "NumericUpDown":
                                    strAllFieldValues += strFieldName + "= " + FormatValue(strFieldName, ((NumericUpDown)ctlTemp).Value.ToString()) + ",";
                                    break;
                                case "CheckBox":
                                    if (((CheckBox)ctlTemp).Checked)
                                    {
                                        strAllFieldValues += strFieldName + "= " + "1" + ",";
                                    }
                                    else
                                    {
                                        strAllFieldValues += strFieldName + "= " + "0" + ",";
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            //save data!
            try
            {
                //trim trailing ,
                if (strAllFieldNames.Length > 0)
                {
                    strAllFieldNames = strAllFieldNames.Substring(0, strAllFieldNames.Length - 2);
                }
               
                strAllFieldValues = strAllFieldValues.Substring(0, strAllFieldValues.Length - 1);

                if (blnEdit)
                {
                    strSQL += strAllFieldValues + strWhere;
                }
                else
                {
                    //new record
                    strAllFieldNames += ") ";
                    strAllFieldValues = " VALUES (" + strAllFieldValues + ")";
                    strSQL += strAllFieldNames + strAllFieldValues + ";";
                }

                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLTrans = SQLConn.BeginTransaction();

                    //save record
                    SQLCmd = new SqlCommand(strSQL, SQLConn);
                    SQLCmd.Transaction = SQLTrans;

                    try
                    {
                        SQLCmd.ExecuteNonQuery();
                        SQLTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        //Whoops!                
                        strError = "Error Accessing Database:\n\n" + ex.Message;

                        if (SQLTrans != null)
                        {
                            SQLTrans.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Whoops!                
                strError = "Error Accessing Database:\n\n" + ex.Message;
            }

            return strError;
        }


        public static string SaveRecordMany(string strTableName_One, Form frmFrom, bool blnEdit, string strID, string strTableName_Many)
        {
           /*
                Created 09/03/2026 By Roger Williams

                Saves record to passed table, using passed form for the data AND the column names
                as controls with data follow naming convention: <control type><column name>
                Does same for "lines" table as well

                extracts first 3 characters from each tables primary key then uses that data to
                ensure when iterating through the forms controls that the correct fields are added
                to strAllFields one/many

                VARS

                strTableName         - table name
                frmFrom              - form to read data from
                boolEdit             - false if new record else true
                strID                - ID of record
                strTableName_Many    - "lines" table name


            */
            string strTemp = String.Empty;
            string strFieldName = String.Empty;
            string strControlType = String.Empty;
            string strControlName = String.Empty;
            
            string strPrimaryKey_One = String.Empty;
            string strFieldBaseName_One = String.Empty;
            string strAllFieldNames_One = String.Empty;
            string strAllFieldValues_One = String.Empty;
            string strWhere_One = " WHERE ";
            string strSQL_One = String.Empty;
            string strPrimaryKey_Many = String.Empty;
            string strFieldBaseName_Many = String.Empty;
            string strAllFieldNames_Many = String.Empty;
            string strAllFieldValues_Many = String.Empty;
            string strWhere_Many = " WHERE ";
            string strSQL_Many = String.Empty;

            string strError = String.Empty;
            SqlCommand SQLCmd = null;
            SqlTransaction SQLTrans = null;
            SqlConnection SQLConn;

            string FormatValue(string strField, string strValue)
            {
                /*
                     Modified 05/03/2026  By Roger Williams
                
                     now uses a list in clsdata to get dataypes for passed strValue     


                     Created 07/08/2025 By Roger Williams

                     Looks through passed tag for data type then returns the passed value in the formattin
                     e.g.: FormatValue(hello,string) returns:

                     "hello"

                     Note: sometimes there will be a | in the tag this is ignored

                */


                string strReturn = String.Empty;
                string strDataType = String.Empty;

                //get datatype for passed field
                strDataType = clsData.lstTableInfo.Find(res => res.strColumnName == strField).strDataType;

                switch (strDataType)
                {
                    case "string":
                    case "date":
                    case "datetime2":
                    case "datetime":
                        strReturn = "'" + strValue + "'";
                        break;
                    case "money":
                    case "decimal":
                    case "float":
                    case "int":
                        strReturn = strValue;
                        break;
                    case "bit":
                        if (strValue == "true")
                        {
                            strReturn = "-1";
                        }
                        else
                        {
                            strReturn = "0";
                        }

                        break;
                }

                return strReturn;
            }




            //****func start

            //extract chars till _ found to denote the table column base name e.g. SKTI_ from STKI_ItemID
            strFieldBaseName_One = Modules.clsTables.GetPrimaryField(strTableName_One);
            strFieldBaseName_One = strFieldBaseName_One.Substring(0, strFieldBaseName_One.IndexOf("_")+1);
            strFieldBaseName_Many = Modules.clsTables.GetPrimaryField(strTableName_Many);
            strFieldBaseName_Many = strFieldBaseName_Many.Substring(0, strFieldBaseName_Many.IndexOf("_")+1);
            strPrimaryKey_One = Modules.clsTables.GetPrimaryField(strTableName_One);
            strPrimaryKey_Many = Modules.clsTables.GetPrimaryField(strTableName_Many);

            if (blnEdit)
            {
                strSQL_One = " UPDATE " + strTableName_One + " SET ";
                strSQL_Many = " UPDATE " + strTableName_Many + " SET ";
                //get table primary key
                strWhere_One = Modules.clsTables.GetPrimaryField(strTableName_One) + " =" + strID;
                //get table primary key
                strWhere_Many = Modules.clsTables.GetPrimaryField(strTableName_Many) + " =" + strID;
            }
            else
            {
                strSQL_One = "INSERT INTO " + strTableName_One + " (";
                strSQL_Many = "INSERT INTO " + strTableName_Many+ " (";
            }

            //create SQL value strings from controls
            foreach (Control ctlTemp in frmFrom.Controls)
            {
                //every "data bound" control has _ in it as this is part of the naming
                //convention for columns e.g. STKI_ItemID
                if (ctlTemp.Name.Contains("_"))
                {
                    strControlType = ctlTemp.GetType().Name;
                    //extract whole field name
                    strFieldName = ctlTemp.Name.Substring(3, ctlTemp.Name.Length - 3);

                    if ( (strFieldName != strPrimaryKey_One) && (strFieldName != strPrimaryKey_Many) )
                    { 
                        if (blnEdit == false)
                        {
                            if (ctlTemp.Name.Contains(strFieldBaseName_One))
                            {
                                //add new record
                                switch (strControlType)
                                {
                                    case "TextBox":
                                        strAllFieldNames_One += strFieldName + ", ";
                                        strAllFieldValues_One += FormatValue(strFieldName, ((TextBox)ctlTemp).Text) + ",";
                                        break;
                                    case "ComboBox":
                                        strAllFieldNames_One += strFieldName + ", ";
                                        strAllFieldValues_One += FormatValue(strFieldName, ((ComboBox)ctlTemp).Text) + ",";
                                        break;
                                    case "NumericUpDown":
                                        strAllFieldNames_One += strFieldName + ", ";
                                        strAllFieldValues_One += FormatValue(strFieldName, ((NumericUpDown)ctlTemp).Value.ToString()) + ",";
                                        break;
                                    case "CheckBox":
                                        strAllFieldNames_One += strFieldName + ", ";

                                        if (((CheckBox)ctlTemp).Checked)
                                        {
                                            strAllFieldValues_One += "1" + ",";
                                        }
                                        else
                                        {
                                            strAllFieldValues_One += "0" + ",";
                                        }
                                        break;
                                }
                            }

                            if (ctlTemp.Name.Contains(strFieldBaseName_Many))
                            {
                                //add new record
                                switch (strControlType)
                                {
                                    case "TextBox":
                                        strAllFieldNames_Many += strFieldName + ", ";
                                        strAllFieldValues_Many += FormatValue(strFieldName, ((TextBox)ctlTemp).Text) + ",";
                                        break;
                                    case "ComboBox":
                                        strAllFieldNames_Many += strFieldName + ", ";
                                        strAllFieldValues_Many += FormatValue(strFieldName, ((ComboBox)ctlTemp).Text) + ",";
                                        break;
                                    case "NumericUpDown":
                                        strAllFieldNames_Many += strFieldName + ", ";
                                        strAllFieldValues_Many += FormatValue(strFieldName, ((NumericUpDown)ctlTemp).Value.ToString()) + ",";
                                        break;
                                    case "CheckBox":
                                        strAllFieldNames_Many += strFieldName + ", ";

                                        if (((CheckBox)ctlTemp).Checked)
                                        {
                                            strAllFieldValues_Many += "1" + ",";
                                        }
                                        else
                                        {
                                            strAllFieldValues_Many += "0" + ",";
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (ctlTemp.Name.Contains(strFieldBaseName_One))
                            {
                                //edit record
                                switch (strControlType)
                                {
                                    case "TextBox":
                                        strAllFieldValues_One += strFieldName + "= " + FormatValue(strFieldName, ((TextBox)ctlTemp).Text) + ",";
                                        break;
                                    case "ComboBox":
                                        strAllFieldValues_One += strFieldName + "= " + FormatValue(strFieldName, ((ComboBox)ctlTemp).Text) + ",";
                                        break;
                                    case "NumericUpDown":
                                        strAllFieldValues_One += strFieldName + "= " + FormatValue(strFieldName, ((NumericUpDown)ctlTemp).Value.ToString()) + ",";
                                        break;
                                    case "CheckBox":
                                        if (((CheckBox)ctlTemp).Checked)
                                        {
                                            strAllFieldValues_One += strFieldName + "= " + "1" + ",";
                                        }
                                        else
                                        {
                                            strAllFieldValues_One += strFieldName + "= " + "0" + ",";
                                        }
                                        break;
                                }
                            }

                            if (ctlTemp.Name.Contains(strFieldBaseName_Many))
                            {
                                //edit record
                                switch (strControlType)
                                {
                                    case "TextBox":
                                        strAllFieldValues_Many += strFieldName + "= " + FormatValue(strFieldName, ((TextBox)ctlTemp).Text) + ",";
                                        break;
                                    case "ComboBox":
                                        strAllFieldValues_Many += strFieldName + "= " + FormatValue(strFieldName, ((ComboBox)ctlTemp).Text) + ",";
                                        break;
                                    case "NumericUpDown":
                                        strAllFieldValues_Many += strFieldName + "= " + FormatValue(strFieldName, ((NumericUpDown)ctlTemp).Value.ToString()) + ",";
                                        break;
                                    case "CheckBox":
                                        if (((CheckBox)ctlTemp).Checked)
                                        {
                                            strAllFieldValues_Many += strFieldName + "= " + "1" + ",";
                                        }
                                        else
                                        {
                                            strAllFieldValues_Many += strFieldName + "= " + "0" + ",";
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            //save data!
            try
            {
                //trim trailing ,
                if (strAllFieldNames_One.Length > 0)
                {
                    strAllFieldNames_One = strAllFieldNames_One.Substring(0, strAllFieldNames_One.Length - 2);
                }

                strAllFieldValues_One = strAllFieldValues_One.Substring(0, strAllFieldValues_One.Length - 1);

                //trim trailing ,
                if (strAllFieldNames_Many.Length > 0)
                {
                    strAllFieldNames_Many = strAllFieldNames_Many.Substring(0, strAllFieldNames_Many.Length - 2);
                }

                strAllFieldValues_Many = strAllFieldValues_Many.Substring(0, strAllFieldValues_Many.Length - 1);



                if (blnEdit)
                {
                    strSQL_One += strAllFieldValues_One + strWhere_One;
                    strSQL_Many += strAllFieldValues_Many + strWhere_Many;
                }
                else
                {
                    //new record
                    strAllFieldNames_One += ") ";
                    strAllFieldValues_One = " VALUES (" + strAllFieldValues_One + ")";
                    strSQL_One += strAllFieldNames_One + strAllFieldValues_One + ";";

                    strAllFieldNames_Many += ") ";
                    strAllFieldValues_Many = " VALUES (" + strAllFieldValues_Many + ")";
                    strSQL_Many += strAllFieldNames_Many + strAllFieldValues_Many + ";";
                }

                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLTrans = SQLConn.BeginTransaction();

                    try
                    {
                        SQLCmd.Transaction = SQLTrans;

                        //save record
                        SQLCmd = new SqlCommand(strSQL_One, SQLConn);
                        SQLCmd.ExecuteNonQuery();
                        //save record
                        SQLCmd = new SqlCommand(strSQL_Many, SQLConn);
                        SQLCmd.ExecuteNonQuery();

                        SQLTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        //Whoops!                
                        strError = "Error Accessing Database:\n\n" + ex.Message;

                        if (SQLTrans != null)
                        {
                            SQLTrans.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Whoops!                
                strError = "Error Accessing Database:\n\n" + ex.Message;
            }

            return strError;
        }




        //class end
    }
}

