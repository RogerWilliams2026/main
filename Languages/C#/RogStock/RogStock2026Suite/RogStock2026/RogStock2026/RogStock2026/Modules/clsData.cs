using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using static System.Net.Mime.MediaTypeNames;


namespace RogStock2026.Modules
{
    internal static class clsData
    {
        //SQL server custom error handling
        static string CNST_STR_ERRORFILEPATH = string.Empty;
        public static Dictionary<int, string> dicSQLErrors = new Dictionary<int, string>();

        //find screen comparison operators
        public static List<string> lstFindOperators = new List<string> { "=", "<", ">", "<>", "LIKE" };

        //data source = server name
        public static readonly string CNST_STR_ODBC = "Data Source=DESKTOP-694Q8HR;Initial Catalog=RogStock;Persist Security Info=True;User ID=sa;Password=RogSQLServer1;TrustServerCertificate=true";

        //TRN operation types
        public static readonly string CNST_STR_OPERATION_CREATE = "created";
        public static readonly string CNST_STR_OPERATION_ADJUST = "adjusted";
        public static readonly string CNST_STR_OPERATION_DELETE = "deleted";
        public static readonly string CNST_STR_OPERATION_RENAME = "locationrenamed";
        public static readonly string CNST_STR_OPERATION_LOTCREATED = "lotcreated";

        //rogstock2026 installation folder - this is where the reports are stored!
        public static readonly string CNST_STR_INSTALLATIONPATH = "C:\\RogStock2026";
        public static readonly string CNST_STR_REPORTSPATH = CNST_STR_INSTALLATIONPATH + "\\Reports";
        public static readonly string CNST_STR_RESOURCEPATH = CNST_STR_INSTALLATIONPATH + "\\Resources";

        //resource file names
        public static readonly string CNST_STR_CUSTOMSQLERRORFILE = "Errorlist.res";
        public static readonly string CNST_STR_CUSTOMTHEMEFILE = "rogstock2026theme.thm";

        //resource files locations
        public static readonly string CNST_STR_SQLCUSTOMERRORSPATH = CNST_STR_RESOURCEPATH + "\\Errorlist.res";
        public static readonly string CNST_STR_CUSTOMTHEMEPATH = CNST_STR_RESOURCEPATH + "\\rogstock2026theme.thm";

        //reports
        public static readonly string CNST_STR_REPORT_UOM = CNST_STR_REPORTSPATH + "\\rptUOM.rdl";
        public static readonly string CNST_STR_REPORT_PRODUCTFAMILY = CNST_STR_REPORTSPATH + "\\rptProductFamily.rdl";
        public static readonly string CNST_STR_REPORT_LOCATIONS = CNST_STR_REPORTSPATH + "\\rptLocations.rdl";
        public static readonly string CNST_STR_REPORT_LOTS = CNST_STR_REPORTSPATH + "\\rptLots.rdl";
        public static readonly string CNST_STR_REPORT_LOCTRN = CNST_STR_REPORTSPATH + "\\rptLocTRN.rdl";
        public static readonly string CNST_STR_REPORT_LOTTRN = CNST_STR_REPORTSPATH + "\\rptLotTRN.rdl";

        public static readonly string CNST_STR_REPORT_STOCK_ITEMS_BASIC = CNST_STR_REPORTSPATH + "\\rptStockItems_Basic.rdl";
        public static readonly string CNST_STR_REPORT_STOCK_ITEMS_ALLINFORMATION = CNST_STR_REPORTSPATH + "\\rptStockItems_AllInformation.rdl";
        public static readonly string CNST_STR_REPORT_STOCK_ITEMS_TRANSACTIONHISTORY = CNST_STR_REPORTSPATH + "\\rptStockItems_TRNHistory.rdl";
        public static readonly string CNST_STR_REPORT_STOCK_ITEMS_UOM = CNST_STR_REPORTSPATH + "\\rptStockItems_UOM.rdl";
        public static readonly string CNST_STR_REPORT_STOCK_ITEMS_PRODUCTFAMILY = CNST_STR_REPORTSPATH + "\\rptStockItems_ProductFamily.rdl";
        public static readonly string CNST_STR_REPORT_STOCK_ITEMS_LOCLOT = CNST_STR_REPORTSPATH + "\\rptStockItems_LocLot.rdl";
        //sub reports
        public static readonly string CNST_STR_REPORT_STOCK_ITEMS_SUB = CNST_STR_REPORTSPATH + "\\rptSubStockItems.rdlc";
        public static readonly string CNST_STR_REPORT_STOCK_ITEMS_TRANSACTIONHISTORY_SUB = CNST_STR_REPORTSPATH + "\\rptSubStockItems_TRNHistory_Lot.rdlc";

        public static readonly string CNST_STR_REPORT_LOGIN = CNST_STR_REPORTSPATH + "\\rptLogins.rdl";
        public static readonly string CNST_STR_REPORT_LOGIN_CURRENT = CNST_STR_REPORTSPATH + "\\rptCurrentLogins.rdl";
        public static readonly string CNST_STR_REPORT_USERGROUPS = CNST_STR_REPORTSPATH + "\\rptUserGroups.rdl";


        //global variable for passback selected primary key from frmFind
        public static object objFindSelected = null;

        //struct typPassword
        //{
        //    public int intLength;
        //    public List<char> aryContents;
        //    public List<int> aryOrder;
        //}
        //used in current login functions
        public static string strLoggedInUser = string.Empty;
        private static string strLoggedInIP = string.Empty;

        //custom SQL error handler default values ONLY used if errorslist.res does not exist i.e. first run
        private static List<string> lstSQLErrorsList = new List<string> { "1000,Login Not Found", "1001,User Not Found Or Password Incorrect", "1003, Error Creating LOT Record!", "1004, Error Reading User Password" };

        //default theme created on first run
        private static string CNST_STR_DEFAULTTHEMEDATA =
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




        //private static string EncryptPassword(string strPassword)
        //{
        //    /*
        //     Created 14/02/2025 By Roger Williams

        //     encrypts passed password

        //     VARS

        //     strPassword   - password

        //     returns encrypted password
        //    */

        //    int intNum = 0;
        //    char chrTemp;
        //    typPassword typPwd;
        //    int intLoc = 0;
        //    string strTemp = strPassword;
        //    byte[] arybyteTemp;

        //    //store password length
        //    typPwd.intLength = strTemp.Length;
        //    typPwd.aryContents = new List<char>();

        //    //store chars "as is"
        //    for (intNum = 0; intNum != strTemp.Length;intNum++)
        //    {
        //        typPwd.aryContents.Add(strTemp[intNum]);

        //    }
        //    //resize the order array to match password length
        //    typPwd.aryOrder = new List<int>(intNum);

        //    //encrypt the password by sorting the array by ASC order ASCII value
        //    intNum = 0;
        //    //get ASCII values for the string
        //    arybyteTemp =  Encoding.ASCII.GetBytes(strTemp);

        //    while (intNum != typPwd.aryContents.Count +1)
        //    {
        //        if (intNum != typPwd.aryContents.Count)
        //        {
        //            if (arybyteTemp[intNum] > arybyteTemp[intNum+1]) 
        //            {
        //                chrTemp = typPwd.aryContents[intNum + 1];
        //                typPwd.aryContents[intNum + 1] = typPwd.aryContents[intNum];
        //                typPwd.aryContents[intNum] = chrTemp;
        //                //reset counter
        //                intNum = 1;
        //            }
        //            else
        //            {
        //                intNum++;
        //            }
        //        }
        //        else
        //        {
        //            intNum++;
        //        }
        //    }

        //    intNum = 0;

        //    //store location of where characters where in original string
        //    while (intNum != typPwd.aryContents.Count+1)
        //    {
        //        intLoc = strTemp.IndexOf(typPwd.aryContents[intNum]);
        //        //erase char from strTemp
        //        strTemp.Remove(intLoc, 1);
        //        strTemp.Insert(intLoc, " ");
        //        typPwd.aryOrder.Add(intLoc);
        //        intNum++;
        //    }

        //    //return encrypted string
        //    strTemp = typPwd.aryContents.ToString();
        //    return strTemp;
        //}
        //private static string DecryptPassword(string strPassword)
        //{
        //    /*
        //     Created 14/02/2025 By Roger Williams

        //     decrypts passed password

        //     VARS

        //     strPassword   - password

        //     returns unencrypted password
        //    */

        //    return "w";

        //}


        public static bool InitCustomErrorhandler(string strPath)
        {
            /*
             Created 02/07/2025 By Roger Williams

             Inits custom SQL error resource file path variabel
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
                        MessageBox.Show("Error: \n\n" + strTemp);

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
                    //      SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_TABLE_LOGIN + " WHERE LOG_User ='" + strUser + "';";
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
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_LOGIN + " WHERE LOG_User ='" + strUser + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    if (SQLRead.Read())
                    {
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
                    SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_TABLE_LOGIN_CURRENT + " (LOGC_User, LOGC_PCIP)  VALUES ('" + strUser + "','" + GetLocalIP() + "');";
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
                    SQLCmd.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_TABLE_LOGIN_CURRENT + " WHERE LOGC_User = '" + strLoggedInUser + "' AND LOGC_PCIP = '" + strLoggedInIP + "';";
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


        public static bool IsUserIngroup(string strUser, string strGroup)
        {
            /*
             Created 28/07/2025 By Roger Williams

             checks passed user is in passed security group
          
             VARS

             strUser       - user 
             strGroup      - security group


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
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_USERGROUPS + " WHERE USRGRP_User ='" + strUser + "' AND USRGRP_Group ='" + strGroup + "';";
                    SQLCmd.CommandType = CommandType.Text;

                    if (SQLCmd.ExecuteScalar() != null)
                    {
                        blnOk = true;
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


        //public static bool CheckForChanges(DataSet DSTTemp, string strTable)
        //{
        //    /*
        //      Created 19/02/2025 By Roger Williams

        //      Checks dataset table for changes returns true if modified

        //      DSTTemp   - dataset to work with
        //      STRTable  - table to work with

        //    */

        //    DSTTemp.AcceptChanges();

        //    if (DSTTemp.Tables[strTable].GetChanges() != null)
        //    {
        //        return true;
        //    }
        //    else
        //    { 
        //        return false;
        //    }
        //}
        //public static void ClearTable(DataSet DSTTemp, string STRTable)
        //{
        //    /*
        //      Created 19/02/2025 By Roger Williams

        //      Clears the dataset table contentsrd

        //      VARS

        //      DSTTemp   - dataset to work with
        //      STRTable  - table to work with

        //    */

        //    DSTTemp.Tables[STRTable].Clear();
        //}
        //public static void CreateNewRecord(DataSet DSTTemp, string STRTable, string strPrimaryKey, string strPrimaryKeyValue)
        //{
        //    /*
        //      Created 19/02/2025 By Roger Williams

        //      Creates new record in dataset table 

        //      VARS

        //      DSTTemp            - dataset to work with
        //      STRTable           - table to work with
        //      STRPrimaryKey      - primary key field name
        //      STRPrimaryKeyValue - primary key field name
        //    */
        //    DataRow DARRow;

        //    DARRow = DSTTemp.Tables[STRTable].NewRow();
        //    //set primary key value
        //    DARRow[strPrimaryKey] = strPrimaryKeyValue;

        //    DSTTemp.Tables[STRTable].Rows.Clear();
        //    DSTTemp.Tables[STRTable].Rows.Add(DARRow);
        //}

        public static bool CheckGroupExists(string strGroup)
        {
            /*
             Created 83/07/2025 By Roger Williams

             checks passed group exists

             VARS

             strgroup      - group name


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
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_GROUPS + " WHERE GRP_Group ='" + strGroup + "';";
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
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_MENUITEMS + " WHERE MNU_MenuItemName ='" + strItem + "';";
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
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_USERGROUPS + " WHERE USRGRP_Group ='" + strUserGroup + "';";
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

        public static bool CheckSectionExists(string strArea)
        {
            /*
             Created 83/07/2025 By Roger Williams

             checks passed area exists

             VARS

             strarea      - area name


            Note; calles checkSECTIONexists as in future sales system will have sales area - removes confusion!

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
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_AREAS + " WHERE SEC_Area ='" + strArea + "';";
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

        public static bool CheckForLotsForItemID(string strItemID)
        /*
             Created 04/03/2025 By Roger Williams
         
             Returns true if ANY lot records exist for passed item ID

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOT + " WHERE LOT_ItemID = '" + strItemID + "';", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    intRows = DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_LOT);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool CheckForStockItems()
        /*
             Created 26/02/2025 By Roger Williams
         
             If no stock items no point opening the form!

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {

                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS);

                    //check stock item record exist
                    if (DSTStock.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS].Rows.Count == 0)
                    {
                        {
                            MessageBox.Show("No Stock Items Found!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public static bool CheckForLocations()
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns the true if passed location has locoation records

                 Note: this function used by location rename

            */

            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOC + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC);

                    //return loc/lot tracking setting
                    return DSTLOTStock.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOC].Rows.Count != 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CheckLotsForLocation(string strLocation)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns the true if passed location has lot records

                 Note: this function used by location rename

            */

            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOT + " WHERE LOT_Location ='" + strLocation + "';", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_LOT);

                    //return loc/lot tracking setting
                    return DSTLOTStock.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].Rows.Count != 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckLLocTRNRecords()
        {
            /*
                 Created 29/08/2025 By Roger Williams

                 Returns the true if loc_trn has records


            */

            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_LOCTRN + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_TABLE_LOCTRN);

                    //return loc/lot tracking setting
                    return DSTLOTStock.Tables[Modules.clsTables.CNST_STR_TABLE_LOCTRN].Rows.Count != 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckForLotTracking(string strItemID)
        /*
             Created 28/02/2025 By Roger Williams
         
             Returns the loc/lot tracked value for the item

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + " WHERE STKI_ItemID ='" + strItemID + "';", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS);

                    //return loc/lot tracking setting
                    return (bool)DSTLOTStock.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS].Rows[0]["STKI_LocLot"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckForAreas()
        /*
             Created 07/07/2025 By Roger Williams
         
             Returns true if ANY area records exist

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;
            int intRows = 0;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_AREAS + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    intRows = DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_TABLE_MENU_AREAS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckAreaExists(string strArea)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if area record for passed area

            */
            SqlConnection SQLConnArea;
            SqlCommand SQLCmdArea;
            SqlDataAdapter DADArea;
            DataSet DSTArea;
            int intRows = 0;

            try
            {
                using (SQLConnArea = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnArea.Open();
                    //load Area items
                    SQLCmdArea = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_AREAS + " WHERE SEC_Area = '" + strArea + "';", SQLConnArea);
                    DADArea = new SqlDataAdapter(SQLCmdArea);
                    DSTArea = new DataSet();
                    intRows = DADArea.Fill(DSTArea, Modules.clsTables.CNST_STR_TABLE_MENU_AREAS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckForLots()
        /*
             Created 04/03/2025 By Roger Williams
         
             Returns true if ANY lot records exist

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADLOTStock;
            DataSet DSTLOTStock;
            int intRows = 0;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOT + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    intRows = DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_LOT);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckProdFamExists(string strProdFam)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY product family records exist with passed product family

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;


            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY + " WHERE STKP_ProductFamily = '" + strProdFam + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        public static bool CheckStockItemIDExists(string strItemID)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock item records exist with passed item ID

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + " WHERE STKI_ItemID = '" + strItemID + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CheckLocationExists(string strLocation)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock loc records exist with passed location

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOC + " WHERE LOC_Location = '" + strLocation + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CheckForProductFamily()
        {
            /*
                 Created 07/03/2025 By Roger Williams

                 Returns true if ANY stock prod fam records exist 

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool CheckForUOM()
        {
            /*
                 Created 07/03/2025 By Roger Williams

                 Returns true if ANY stock UOM records exist 

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {

                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_UOM + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_UOM);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckForLogins()
        {
            /*
                 Created 27/08/2025 By Roger Williams

                 Returns true if ANY stock login records exist 

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {

                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_LOGIN + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_LOGIN);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckForCurrentLogins()
        {
            /*
                 Created 27/08/2025 By Roger Williams

                 Returns true if ANY stock login_current records exist 

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {

                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_LOGIN_CURRENT + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_LOGIN_CURRENT);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CheckUOMExists(string strUOM)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock UOM records exist with passed UOM

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_UOM + " WHERE STKU_Desc = '" + strUOM + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_UOM);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }




        //*****************other*****************
        public static int GetMenuItemsCountForArea(string strArea)
        {
            /*
             Created 07/07/2025 By Roger Williams

             returns numbver of records in Menu_MenuItems that have passed area  
          
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
                    SQLCmd.CommandText = "SELECT  COUNT (*) FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_MENUITEMS + " WHERE MNU_DisplayWhere ='" + strArea + "';";
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

             returns numbver of records in Menu_MenuItems that have passed group
          
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
                    SQLCmd.CommandText = "SELECT  COUNT (*) FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_GROUPS + " WHERE GRP_Group ='" + strGroup + "';";
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
            SqlDataReader sqlRead = null;


            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_MENU_MENUITEMS + " WHERE MNU_DisplayWhere ='" + strArea + "' ORDER BY MNU_MenuItemName;";
                    SQLCmd.CommandType = CommandType.Text;

                    sqlRead = SQLCmd.ExecuteReader();

                    while (sqlRead != null)
                    {
                        lstMenuItems.Add(sqlRead.ToString());
                    }

                    sqlRead.Close();
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


        public static int GetLocationQty(string strLocation)
        {
            /*
                 Created 07/03/2025 By Roger Williams

                 Returns qty for passed location

            */
            {
                SqlConnection SQLConnStock;
                SqlCommand SQLCmdStock;
                SqlDataAdapter DADStock;
                DataSet DSTStock;


                try
                {
                    using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                    {
                        SQLConnStock.Open();
                        //load stock items
                        SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOC + " WHERE LOC_Location ='" + strLocation + "';", SQLConnStock);
                        DADStock = new SqlDataAdapter(SQLCmdStock);
                        DSTStock = new DataSet();
                        DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC);

                        //check stock item record exist
                        if (DSTStock.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOC].Rows.Count == 0)
                        {
                            {
                                MessageBox.Show("Cannot Edit locations As No Stock Items Created!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return 0;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }

                return Convert.ToInt16(DSTStock.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOC].Rows[0]["LOC_Qty"]);
            }
        }


        public static string GetStockDescription(string strItemID)
        {
            /*
                 Created 03/03/2026 By Roger Williams

                 Returns description for passed item ID

            */
            {
                SqlDataReader SQLRead;
                SqlCommand SQLCmdDesc;
                SqlConnection SQLConnStock;
                string strDesc = string.Empty;

                try
                {
                    using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                    {
                        SQLConnStock.Open();
                        SQLCmdDesc = new SqlCommand("SELECT STKD_Desc FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_DESCRIPTION + " WHERE STKD_ItemID = '" + strItemID + "'", SQLConnStock);
                        SQLRead = SQLCmdDesc.ExecuteReader();

                        //load from dataset
                        if (SQLRead.HasRows)
                        {
                            SQLRead.Read();
                            strDesc = SQLRead["STKD_Desc"].ToString();
                        }

                        SQLRead.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return string.Empty;
                }

                return strDesc;
            }
        }



  
        public static bool ValidateLocationQtys(string strItemID, string strLoc)
        {
            /*
              Created 02/03/2025 By Roger Williams

              Compares location qty with total lot qtys (if any)

              Returns true if ok

              VARS

              stritemID     - item id to check
              strloc        - location to check

            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            DataSet DSTLOC;
            DataSet DSTLOT;
            SqlDataAdapter DADTemp;

            int intQtyLoc = 0;
            int intQtyLot = 0;
            DataTable tblTempLoc;
            DataTable tblTempLot;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    //get Location data
                    DSTLOC = new DataSet();
                    SQLCmd = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOC + " WHERE LOC_ItemID = '" + strItemID + "' AND LOC_Location = '" + strLoc + "';", SQLConn);
                    DADTemp = new SqlDataAdapter(SQLCmd);
                    DADTemp.Fill(DSTLOC, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC);
                    //get lot data
                    DSTLOT = new DataSet();
                    SQLCmd = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOT + " WHERE LOT_ItemID = '" + strItemID + "' AND LOT_Location = '" + strLoc + "';", SQLConn);
                    DADTemp = new SqlDataAdapter(SQLCmd);
                    DADTemp.Fill(DSTLOT, Modules.clsTables.CNST_STR_TABLE_STOCK_LOT);

                    tblTempLoc = DSTLOC.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOC];

                    //iterate through the changed rows collating location and quantities
                    foreach (DataRow DARTemp in tblTempLoc.Rows)
                    {
                        //only one location record per item ID
                        intQtyLoc = Convert.ToInt16(DARTemp["LOC_Qty"]);
                        //reset lot qty var
                        intQtyLot = 0;

                        //check lots (if any)
                        tblTempLot = DSTLOT.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT];

                        if (tblTempLot is null)
                        {
                            //no records hence no errors!
                            return true;
                        }
                        else
                        {
                            // aryLots = DSTLOT.Tables[CNST_STR_TABLE_STOCK_LOT].Select("LOT_Location = '" + DARTemp["LOC_Location"] + "' AND LOT_ItemID ='" + strItemID + "'");

                            //  if (aryLots.Length == 0)
                            if (DSTLOT.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].Rows.Count == 0)
                            {
                                //no lots found no error!
                                return true;
                            }
                            else
                            {
                                //get lot qtys
                                foreach (DataRow DARLot in DSTLOT.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].Rows) // aryLots)
                                {
                                    intQtyLot += Convert.ToInt16(DARLot["LOT_Qty"]);
                                }

                                if (intQtyLot != intQtyLoc)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static bool ProdFamUsed(string strProdFam)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock item records exist with passed product family

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + " WHERE STKI_ProductFamily = '" + strProdFam + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool UOMUsed(string strUOM)
        {
            /*
                 Created 06/03/2025 By Roger Williams

                 Returns true if ANY stock item records exist with passed UOM

            */
            SqlConnection SQLConnStock;
            SqlCommand SQLCmdStock;
            SqlDataAdapter DADStock;
            DataSet DSTStock;
            int intRows = 0;

            try
            {
                using (SQLConnStock = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConnStock.Open();
                    //load stock items
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + " WHERE STKI_UOM = '" + strUOM + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS);

                    //any records?
                    if (intRows == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
             // strDataType = lstTableInfo.Find(res => res.strColumnName == strField).strDataType;
             //find in schema
             foreach (TTYPETableInfo typInfo in lstTableInfo)
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

             using (SQLConn = new SqlConnection(CNST_STR_ODBC))
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
                strDataType = lstTableInfo.Find(res => res.strColumnName == strField).strDataType;

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

                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
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



        public static void GetSchemaData()
        {
            /*
              Modified 12/03/2026 By Roger Williams 
              
              now adds column descriptions!  

              Created 04/03/2026 By Roger Williams

              reads EVERY tables column information into an array of structs

              What is read:

              table name
              column name
              column size
              column data type


            */
            TTYPETableInfo TYPInfo;
            object varTemp;
            int intData = 0;

            using (var SQLConn = new SqlConnection(CNST_STR_ODBC))
            using (var SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_QUERY_GETSCHEMAINFORMATION, SQLConn))
            {
                SQLConn.Open();

                using (var SQLRead = SQLCmd.ExecuteReader())
                {
                    while (SQLRead.Read())
                    {
                        TYPInfo = new TTYPETableInfo();
                        TYPInfo.strTableName = SQLRead["TABLENAME"].ToString();
                        TYPInfo.strColumnName = SQLRead["COLUMNNAME"].ToString();
                        TYPInfo.strDescription = SQLRead["DESCRIPTION"].ToString();
                        TYPInfo.intLength = Convert.ToInt16(SQLRead["MAXLENGTH"]);

                        intData = Convert.ToInt16(SQLRead["DATATYPE"]);
                        //store .Net equivalent of SQL datatype
                        switch (intData)
                        {
                            case 35:
                            case 167:
                                {
                                    TYPInfo.strDataType = "string";
                                    break;
                                }
                            case 40:
                                {
                                    TYPInfo.strDataType = "date";
                                    break;
                                }
                            case 42:
                                {
                                    TYPInfo.strDataType = "datetime2";
                                    break;
                                }
                            case 61:
                                {
                                    TYPInfo.strDataType = "datetime";
                                    break;
                                }
                            case 60:
                                {
                                    TYPInfo.strDataType = "money";
                                    break;
                                }
                            case 104:
                                {
                                    TYPInfo.strDataType = "bool";
                                    break;
                                }
                            case 106:
                                {
                                    TYPInfo.strDataType = "decimal";
                                    break;
                                }
                        }

                        TYPInfo.strDataType = SQLRead["DATATYPE"].ToString();
                        varTemp = SQLRead["MAXLength"];

                        if (varTemp.ToString() != string.Empty)
                        {
                            TYPInfo.intLength = Convert.ToInt16(varTemp);
                        }
                        else
                        {
                            TYPInfo.intLength = 0;
                        }
                        lstTableInfo.Add(TYPInfo);
                    }
                }
            }
        }


        //get sql table schemas into dictionary
        public static void GetSQLSchemaTableKeys()
        {
            /*
                 Created 03/08/2025 By Roger Williams

                 Reads SQL table schemas, specifically table names and key field into dictionary
                 for use by other functions

                 Uses: INFORMATION_SCHEMA.KEY_COLUMN_USAGE  for primary key
                       INFORMATION_SCHEMA.TABLES  for "secondary keys" and "tertiary keys"

                 Note: "secondary/tertiary keys" also needs data controls to access actual table columns
                       "secondary keys" are in fact column 2 "tertiary keys" column 3 
                       not all 3rd columns are tertiary keys but why not add them anyway!?
                       avoids using timestamp columns as "keys"
                       due to a luaghable limitation in .NET only ONE reader can be user PER connection!!
                       in the world of .NET pure ADO doesn't exist!  

                       intColNbr = start column to look at, if ignoring ID fields set first if (intColNbr == 0.....
                       to if (intColNbr == 0
                       

            */
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SDRTable = null;
            int intColNbr = 0;
            string strTemp = String.Empty;

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();

                    //********keep just in case need it********
                    //get list of tables primary keys from SQL Server
                    //SQLCmd = new SqlCommand("SELECT TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE ORDER BY TABLE_NAME;", SQLConn);
                    //SDRTable = SQLCmd.ExecuteReader();

                    //if (SDRTable != null)
                    //{
                    //    dicSQLSchema_PrimaryKey.Clear();

                    //    while (SDRTable.Read())
                    //    {
                    //        dicSQLSchema_PrimaryKey.Add(SDRTable["TABLE_NAME"].ToString(), SDRTable["COLUMN_NAME"].ToString());
                    //    }

                    //    SDRTable.Close();
                    //}

                    //SDRTable = null;
                    //get "secondary/tertiary keys" these are the 2nd and 3rd columns (as long as column is not timestamp!)
                    //from SQL Server
                    SQLCmd = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.COLUMNS ORDER BY TABLE_NAME;", SQLConn);
                    SDRTable = SQLCmd.ExecuteReader();

                    if (SDRTable != null)
                    {
                        Modules.clsTables.dicSQLSchema_PrimaryKey.Clear();
                        Modules.clsTables.dicSQLSchema_SecondaryKey.Clear();
                        Modules.clsTables.dicSQLSchema_TertiaryKey.Clear();

                        //iterate through tables list
                        while (SDRTable.Read())
                        {
                            if (strTemp != SDRTable["TABLE_NAME"].ToString())
                            {
                                //point to next table
                                strTemp = SDRTable["TABLE_NAME"].ToString();
                                //reset col number counter
                                intColNbr = 0;
                            }

                            //add to dictionaries
                            if (intColNbr == 0)
                            {
                                Modules.clsTables.dicSQLSchema_PrimaryKey.Add(SDRTable["TABLE_NAME"].ToString(), SDRTable["COLUMN_NAME"].ToString());
                            }

                            if (intColNbr == 1)
                            {
                                Modules.clsTables.dicSQLSchema_SecondaryKey.Add(SDRTable["TABLE_NAME"].ToString(), SDRTable["COLUMN_NAME"].ToString());
                            }

                            //technically this is not ideal, but add anyway!
                            if (intColNbr == 2)
                            {
                                //make surenot trying to add timestamp or text as these cannot be key types in SQL Server!
                                if (SDRTable["DATA_TYPE"].ToString() != "timestamp" && SDRTable["DATA_TYPE"].ToString() != "text")
                                {
                                    Modules.clsTables.dicSQLSchema_TertiaryKey.Add(SDRTable["TABLE_NAME"].ToString(), SDRTable["COLUMN_NAME"].ToString());
                                }
                            }

                            intColNbr++;
                        }

                        SDRTable.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void CreateCustomSQLErrorFile()
        {
            /*
                 Created 10/04/2026 By Roger Williams

                 creates default custom SQL error file ran after install


            */

            StreamWriter strmTemp = null;

            strmTemp = new StreamWriter(CNST_STR_CUSTOMSQLERRORFILE, false);

            foreach (string strTemp in lstSQLErrorsList)
            {
               strmTemp.WriteLine(strTemp); 
            }

            strmTemp.Close();
        }


        public static void CreateDefaultTheme()
        {
            /*
                 Created 10/04/2026 By Roger Williams

                 creates default theme file ran after install


            */

            StreamWriter strmTemp = null;

            strmTemp = new StreamWriter(CNST_STR_CUSTOMTHEMEFILE, false);
            strmTemp.WriteLine(CNST_STR_DEFAULTTHEMEDATA);
            strmTemp.Close();
        }
        //class end
    }
}
