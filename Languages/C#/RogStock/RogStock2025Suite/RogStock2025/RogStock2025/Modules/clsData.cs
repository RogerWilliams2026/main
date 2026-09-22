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

//using static System.Runtime.InteropServices.JavaScript.JSType;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RogStock2025.Modules
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

        //rogstock2025 installation folder - this is where the reports are stored!
        public static readonly string CNST_STR_INSTALLATIONPATH = "C:\\RogStock2025";
        public static readonly string CNST_STR_REPORTSPATH = CNST_STR_INSTALLATIONPATH + "\\Reports";

        //resource files locations
        public static readonly string CNST_STR_SQLCUSTOMERRORSPATH = Modules.clsData.CNST_STR_INSTALLATIONPATH + @"\Resources\Errorlist.res";
        public static readonly string CNST_STR_CUSTOMTHEMEPATH = Modules.clsData.CNST_STR_INSTALLATIONPATH + @"\Resources\rogstock2025theme.thm";

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
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_USERGROUPS + " WHERE USRGRP_User ='" + strUser + "' AND USRGRP_Group ='" + strGroup + "';";
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
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_GROUPS + " WHERE GRP_Group ='" + strGroup + "';";
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
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_AREAS + " WHERE SEC_Area ='" + strArea + "';";
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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE LOT_ItemID = '" + strItemID + "';", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    intRows = DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_STOCK_LOT);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_ITEMS + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_ITEMS);

                    //check stock item record exist
                    if (DSTStock.Tables[Modules.clsTables.CNST_STR_STOCK_ITEMS].Rows.Count == 0)
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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOC + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_STOCK_LOC);

                    //return loc/lot tracking setting
                    return DSTLOTStock.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].Rows.Count != 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return false;
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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE LOT_Location ='" + strLocation + "';", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_STOCK_LOT);

                    //return loc/lot tracking setting
                    return DSTLOTStock.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows.Count != 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return false;
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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_LOCTRN + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_LOCTRN);

                    //return loc/lot tracking setting
                    return DSTLOTStock.Tables[Modules.clsTables.CNST_STR_LOCTRN].Rows.Count != 0;
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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " WHERE STKI_ItemID ='" + strItemID + "';", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_STOCK_ITEMS);

                    //return loc/lot tracking setting
                    return (bool)DSTLOTStock.Tables[Modules.clsTables.CNST_STR_STOCK_ITEMS].Rows[0]["STKI_LocLot"];
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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_AREAS + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    intRows = DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_MENU_AREAS);

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
                    SQLCmdArea = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_AREAS + " WHERE SEC_Area = '" + strArea + "';", SQLConnArea);
                    DADArea = new SqlDataAdapter(SQLCmdArea);
                    DSTArea = new DataSet();
                    intRows = DADArea.Fill(DSTArea, Modules.clsTables.CNST_STR_MENU_AREAS);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + ";", SQLConnStock);
                    DADLOTStock = new SqlDataAdapter(SQLCmdStock);
                    DSTLOTStock = new DataSet();
                    intRows = DADLOTStock.Fill(DSTLOTStock, Modules.clsTables.CNST_STR_STOCK_LOT);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY + " WHERE STKP_ProductFamily = '" + strProdFam + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " WHERE STKI_ItemID = '" + strItemID + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_ITEMS);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOC + " WHERE LOC_Location = '" + strLocation + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_LOC);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_UOM + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_UOM);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_LOGIN + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_LOGIN);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_LOGIN_CURRENT + ";", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_LOGIN_CURRENT);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_UOM + " WHERE STKU_Desc = '" + strUOM + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_UOM);

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
            SqlDataReader sqlRead = null;


            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_MENU_MENUITEMS + " WHERE MNU_DisplayWhere ='" + strArea + "' ORDER BY MNU_MenuItemName;";
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
                        SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOC + " WHERE LOC_Location ='" + strLocation + "';", SQLConnStock);
                        DADStock = new SqlDataAdapter(SQLCmdStock);
                        DSTStock = new DataSet();
                        DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_LOC);

                        //check stock item record exist
                        if (DSTStock.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].Rows.Count == 0)
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

                return Convert.ToInt16(DSTStock.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].Rows[0]["LOC_Qty"]);
            }
        }



        public static void PopulateComboBoxes(ComboBox CMBTemp, string strTable, string strKeyField, string strKeyFieldValue, string strSecondFieldName, string strSecondFieldValue, string strWHERE, bool blnDistinct)
        {
            /*
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
                                    in a commercial system would also pass
                                    data type
              strSecondFieldName  - another field name 
              strSecondFieldValue - another field value always handled as text 
                                    in a commercial system would also pass
                                    data type
             strWHERE             - specify WHERE clause
             blnDistinct          - use DISTINCT 
            
             Note: if using blnDistinct strKeyField MUST have a value!


            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;

            //clear combo
            CMBTemp.Items.Clear();

            try
            {
                using (SQLConn = new SqlConnection(CNST_STR_ODBC))
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
                    //add ;
                    SQLCmd.CommandText += ";";

                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        if (strTable == Modules.clsTables.CNST_STR_LOCTRN)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_LOTTRN)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_STOCK_ITEMS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_STOCK_LOT)
                        {
                            CMBTemp.Items.Add(SQLRead[0].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_STOCK_VENDORS)
                        {
                            CMBTemp.Items.Add(SQLRead[2].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_LOGIN)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_STOCK_LOC)
                        {
                            CMBTemp.Items.Add(SQLRead[2].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == Modules.clsTables.CNST_STR_STOCK_UOM)
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
                    SQLCmd = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOC + " WHERE LOC_ItemID = '" + strItemID + "' AND LOC_Location = '" + strLoc + "';", SQLConn);
                    DADTemp = new SqlDataAdapter(SQLCmd);
                    DADTemp.Fill(DSTLOC, Modules.clsTables.CNST_STR_STOCK_LOC);
                    //get lot data
                    DSTLOT = new DataSet();
                    SQLCmd = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE LOT_ItemID = '" + strItemID + "' AND LOT_Location = '" + strLoc + "';", SQLConn);
                    DADTemp = new SqlDataAdapter(SQLCmd);
                    DADTemp.Fill(DSTLOT, Modules.clsTables.CNST_STR_STOCK_LOT);

                    tblTempLoc = DSTLOC.Tables[Modules.clsTables.CNST_STR_STOCK_LOC];

                    //iterate through the changed rows collating location and quantities
                    foreach (DataRow DARTemp in tblTempLoc.Rows)
                    {
                        //only one location record per item ID
                        intQtyLoc = Convert.ToInt16(DARTemp["LOC_Qty"]);
                        //reset lot qty var
                        intQtyLot = 0;

                        //check lots (if any)
                        tblTempLot = DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT];

                        if (tblTempLot is null)
                        {
                            //no records hence no errors!
                            return true;
                        }
                        else
                        {
                            // aryLots = DSTLOT.Tables[CNST_STR_STOCK_LOT].Select("LOT_Location = '" + DARTemp["LOC_Location"] + "' AND LOT_ItemID ='" + strItemID + "'");

                            //  if (aryLots.Length == 0)
                            if (DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows.Count == 0)
                            {
                                //no lots found no error!
                                return true;
                            }
                            else
                            {
                                //get lot qtys
                                foreach (DataRow DARLot in DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows) // aryLots)
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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " WHERE STKI_ProductFamily = '" + strProdFam + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_ITEMS);

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
                    SQLCmdStock = new SqlCommand("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " WHERE STKI_UOM = '" + strUOM + "';", SQLConnStock);
                    DADStock = new SqlDataAdapter(SQLCmdStock);
                    DSTStock = new DataSet();
                    intRows = DADStock.Fill(DSTStock, Modules.clsTables.CNST_STR_STOCK_ITEMS);

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


        //get sql table schemas into dictionary
        public static void GetSQLSchema()
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
                       in thw world of .NET pure ADO don't exist!  

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
                            if (intColNbr == 1)
                            {
                                Modules.clsTables.dicSQLSchema_PrimaryKey.Add(SDRTable["TABLE_NAME"].ToString(), SDRTable["COLUMN_NAME"].ToString());
                            }

                            if (intColNbr == 2)
                            {
                                Modules.clsTables.dicSQLSchema_SecondaryKey.Add(SDRTable["TABLE_NAME"].ToString(), SDRTable["COLUMN_NAME"].ToString());
                            }

                            //technically this is not ideal, but add anyway!
                            if (intColNbr == 3)
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

        public static string SaveRecord(SqlConnection SQLConn, string strTableName, Form frmFrom, bool blnEdit, SqlTransaction SQLTrans)
        {
            /*
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
            string strSQL = String.Empty;
            string strFieldsName = String.Empty;
            string strFieldsValue = String.Empty;
            string strPrimaryKey = String.Empty;
            string strWhere = String.Empty;
            string strError = String.Empty;
            SqlCommand SQLCmd = null;

            string FormatValue(string strValue, string strTag)
            {
                /*
                     Created 07/08/2025 By Roger Williams

                     Looks through passed tag for data type then returns the passed value in the formattin
                     e.g.: FormatValue(hello,string) returns:

                     "hello"

                     Note: sometimes there will be a | in the tag this is ignored

                */


                string strTagTemp = strTag;
                string strReturn = String.Empty;

                if (strTagTemp.IndexOf("|") != -1)
                {
                    //remove |
                    strTagTemp = strTagTemp.Substring(strTagTemp.IndexOf("|"), strTagTemp.Length - 2);
                }
                else
                {
                    strTagTemp = strTag;
                }

                switch (strTagTemp)
                {
                    case "string":
                        strReturn = "'" + strValue + "'";
                        break;
                    case "date":
                        strReturn = "'" + strValue + "'";
                        break;
                    case "number":
                        strReturn = strValue;
                        break;
                    case "boolean":
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

            //func start

            if (SQLConn == null)
            {
                strError = "SQL Connection Not Found!";
                return strError;
            }

            if (blnEdit)
            {
                strSQL = " UPDATE " + strTableName + " SET ";
            }
            else
            {
                strSQL = "INSERT INTO " + strTableName + " (";
            }

            //get table primarykey
            strPrimaryKey = Modules.clsTables.GetPrimaryField(strTableName);

            //create SQl value strings from controls
            foreach (Control ctlTemp in frmFrom.Controls)
            {
                if (ctlTemp.Tag != null)
                {
                    //strip any | from tag
                    if (ctlTemp.Tag.ToString().Contains("|"))
                    {
                        //strip |
                        strTemp = ctlTemp.Tag.ToString().Substring(2, ctlTemp.Tag.ToString().Length - 2);
                    }
                    else
                    {
                        strTemp = ctlTemp.Tag.ToString();
                    }

                    //skip first 3 chars as they are control type
                    strFieldsName += ctlTemp.Name.Substring(3, ctlTemp.Name.Length) + ", ";

                    if (blnEdit == false)
                    {

                        //add new record
                        switch (strTemp)
                        {
                            case "TextBox":
                                //skip first 3 chars as they are control type
                                strFieldsName += ctlTemp.Name.Substring(3, ctlTemp.Name.Length) + ", ";
                                strFieldsValue += FormatValue(((TextBox)ctlTemp).Text, ((TextBox)ctlTemp).Tag.ToString()) + ",";
                                break;
                            case "ComboBox":
                                //skip first 3 chars as they are control type
                                strFieldsName += ctlTemp.Name.Substring(3, ctlTemp.Name.Length) + ", ";
                                strFieldsValue += FormatValue(((ComboBox)ctlTemp).Text, ((ComboBox)ctlTemp).Tag.ToString()) + ",";
                                break;
                            case "NumericUpDown":
                                //skip first 3 chars as they are control type
                                strFieldsName += ctlTemp.Name.Substring(3, ctlTemp.Name.Length) + ", ";
                                strFieldsValue += FormatValue(((NumericUpDown)ctlTemp).Value.ToString(), ((NumericUpDown)ctlTemp).Tag.ToString()) + ",";
                                break;
                            case "CheckBox":
                                //skip first 3 chars as they are control type
                                strFieldsName += ctlTemp.Name.Substring(3, ctlTemp.Name.Length) + ", ";
                                strFieldsValue += FormatValue(((CheckBox)ctlTemp).Checked.ToString(), ((CheckBox)ctlTemp).Tag.ToString()) + ",";
                                break;
                        }
                    }
                    else
                    {
                        //edit record
                        switch (strTemp)
                        {
                            case "TextBox":
                                //skip first 3 chars as they are control type
                                strFieldsValue += ctlTemp.Name.Substring(3, ctlTemp.Name.Length) + "= " + FormatValue(((TextBox)ctlTemp).Text, ((TextBox)ctlTemp).Tag.ToString()) + ",";
                                break;
                            case "ComboBox":
                                //skip first 3 chars as they are control type
                                strFieldsValue += ctlTemp.Name.Substring(3, ctlTemp.Name.Length) + "= " + FormatValue(((ComboBox)ctlTemp).Text, ((ComboBox)ctlTemp).Tag.ToString()) + ",";
                                break;
                            case "NumericUpDown":
                                //skip first 3 chars as they are control type
                                strFieldsValue += ctlTemp.Name.Substring(3, ctlTemp.Name.Length) + "= " + FormatValue(((NumericUpDown)ctlTemp).Value.ToString(), ((NumericUpDown)ctlTemp).Tag.ToString()) + ",";
                                break;
                            case "CheckBox":
                                //skip first 3 chars as they are control type
                                strFieldsValue += ctlTemp.Name.Substring(3, ctlTemp.Name.Length) + "= " + FormatValue(((CheckBox)ctlTemp).Checked.ToString(), ((CheckBox)ctlTemp).Tag.ToString()) + ",";
                                break;
                        }
                    }

                }
            }

            //aave data!
            try
            {
                //trim trailing ,
                strFieldsName = strFieldsName.Substring(0, strFieldsName.Length - 2);
                strFieldsValue = strFieldsValue.Substring(0, strFieldsName.Length - 2);

                if (blnEdit)
                {
                    strSQL += strFieldsValue + strWhere;
                }
                else
                {
                    //new record
                    strFieldsName += ") ";
                    strFieldsValue = " VALUES (" + strFieldsValue + ")";
                    strSQL += strFieldsName + strFieldsValue + ";";
                }

                //save record
                SQLCmd = new SqlCommand(strSQL, SQLConn);
                SQLCmd.Transaction = SQLTrans;
                SQLCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Whoops!                
                strError = "Error Accessing Database:\n\n" + ex.Message;
                // MessageBox.Show("Error Accessing Database:\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return strError;
        }

        public static void SaveRecordFromListbox(SqlConnection SQLConn, string strTableName, string strPrimaryKeyValue, ListView LVFrom, bool blnEdit)
        {
            /*
                 Created 07/08/2025 By Roger Williams

                 Saves record to passed table, using passed listview for the data AND the column names
                 as listview items have column name in tag

                 VARS

                 SQLConn                - open SQL connection
                 strTableName           - table name
                 frmFrom                - form to read data from
                 strPrimaryKeyValue     - pirmary key value
                 LVFrom                 - listview tocheck
                 boolEdit               - false if new record else true


            */

        }
        //class end
    }
}
