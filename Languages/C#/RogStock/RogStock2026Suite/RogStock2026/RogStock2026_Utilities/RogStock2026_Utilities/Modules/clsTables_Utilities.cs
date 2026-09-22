using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace RogStock2026_Utilities.Modules
{
    internal class clsTables_Utilities
    {
        //tables
        public static readonly string CNST_STR_TABLE_LOCTRN = "Loc_TRN";
        public static readonly string CNST_STR_TABLE_LOTTRN = "Lot_TRN";

        public static readonly string CNST_STR_TABLE_LOGIN = "Login_Logins";
        public static readonly string CNST_STR_TABLE_LOGIN_CURRENT = "Login_Current";
        public static readonly string CNST_STR_TABLE_USERGROUPS = "Menu_UsersGroups";

        public static readonly string CNST_STR_TABLE_FIND_RELATIONS = "Find_Relations";
        public static readonly string CNST_STR_TABLE_FIND_FIELDINFO = "Find_FieldInfo";
        public static readonly string CNST_STR_TABLE_FIND_OPERATORS = "Find_Operators";

        public static readonly string CNST_STR_TABLE_MENU_GROUPS = "Menu_Groups";
        public static readonly string CNST_STR_TABLE_MENU_MENUITEMS = "Menu_MenuItems";
        public static readonly string CNST_STR_TABLE_MENU_AREAS = "Menu_Areas";
        public static readonly string CNST_STR_TABLE_MENU_USERGROUPS = "Menu_UsersGroups";

        public static readonly string CNST_STR_TABLE_STOCK_LOC = "Stock_Loc";
        public static readonly string CNST_STR_TABLE_STOCK_LOT = "Stock_Lot";
        public static readonly string CNST_STR_TABLE_STOCK_ITEMS = "Stock_Items";
        public static readonly string CNST_STR_TABLE_STOCK_VENDORS = "Stock_Vendors";
        public static readonly string CNST_STR_TABLE_STOCK_DESCRIPTION = "Stock_Description";
        public static readonly string CNST_STR_TABLE_STOCK_MEDIA = "Stock_Media";
        public static readonly string CNST_STR_TABLE_STOCK_PRODUCTFAMILY = "Stock_ProductFamily";
        public static readonly string CNST_STR_TABLE_STOCK_UOM = "Stock_UOM";

        public static readonly string CNST_STR_TABLE_PURCHASEORDER_DELIVERIES = "PurchaseOrder_Deliveries";
        public static readonly string CNST_STR_TABLE_PURCHASEORDER_INSPECTION = "PurchaseOrder_Inspection";
        public static readonly string CNST_STR_TABLE_PURCHASEORDER_LINES = "PurchaseOrder_Lines";
        public static readonly string CNST_STR_TABLE_PURCHASEORDER_ONTIMESTATUS = "PurchaseOrder_OnTimeStatus";
        public static readonly string CNST_STR_TABLE_PURCHASEORDER_PURCHASEORDERS = "PurchaseOrder_PurchaseOrders";
        public static readonly string CNST_STR_TABLE_PURCHASEORDER_RECEIPTS = "PurchaseOrder_Receipts";
        public static readonly string CNST_STR_TABLE_VENDOR_BILLINGADDRESSES = "Vendor_BillingAddresses";
        public static readonly string CNST_STR_TABLE_ENDOR_VENDORS = "Vendor_Vendors";

        public static readonly string CNST_STR_TABLE_SETTINGSEXPLORER = "SettingsExplorer";

        public static readonly string CNST_STR_TABLE_TRN_LOC = "TRN_Loc";
        public static readonly string CNST_STR_TABLE_TRN_LOGIN_LOGINS = "TRN_Login_Logins";
        public static readonly string CNST_STR_TABLE_TRN_LOT = "TRN_Lot";
        public static readonly string CNST_STR_TABLE_TRN_PURCHASEORDER_DELIVERIES = "TRN_PurchaseOrder_Deliveries";
        public static readonly string CNST_STR_TABLE_TRN_PURCHASEORDER_INSPECTION = "TRN_PurchaseOrder_Inspection";
        public static readonly string CNST_STR_TABLE_TRN_PURCHASEORDER_LINES = "TRN_PurchaseOrder_Lines";
        public static readonly string CNST_STR_TABLE_TRN_PURCHASEORDER_ONTIMESTATUS = "TRN_PurchaseOrder_OnTimeStatus";
        public static readonly string CNST_STR_TABLE_TRN_PURCHASEORDER_PURCHASEORDERS = "TRN_PurchaseOrder_PurchaseOrders";
        public static readonly string CNST_STR_TABLE_TRN_PURCHASEORDER_RECEIPTS = "TRN_PurchaseOrder_Receipts";
        public static readonly string CNST_STR_TABLE_TRN_STOCK_DESCRIPTION = "TRN_Stock_Description";
        public static readonly string CNST_STR_TABLE_TRN_STOCK_ITEMS = "TRN_Stock_Items";
        public static readonly string CNST_STR_TABLE_TRN_STOCK_MEDIA = "TRN_Stock_Media";
        public static readonly string CNST_STR_TABLE_TRN_STOCK_PRODUCTFAMILY = "TRN_Stock_ProductFamily";
        public static readonly string CNST_STR_TABLE_TRN_STOCK_UOM = "TRN_Stock_UOM";
        public static readonly string CNST_STR_TABLE_TRN_VENDOR_BILLINGADDRESSES = "TRN_Vendor_BillingAddresses";
        public static readonly string CNST_STR_TABLE_TRN_VENDOR_VENDORS = "TRN_Vendor_Vendors";

        //select queries
        public static readonly string CNST_STR_QUERY_GETSCHEMAINFORMATION = "SELECT sys.tables.name AS TableName, sys.tables.object_id, sys.columns.name AS ColumnName, " +
                                                                            "sys.columns.system_type_id AS DataType, sys.columns.max_length AS MAXLength, " +
                                                                            "sys.extended_properties.value AS Description FROM sys.tables INNER JOIN sys.columns ON " +
                                                                            "sys.tables.object_id = sys.columns.object_id INNER JOIN sys.extended_properties ON " +
                                                                            "sys.columns.object_id = sys.extended_properties.major_id AND sys.columns.column_id = sys.extended_properties.minor_id " +
                                                                            "WHERE sys.tables.type_desc = 'USER_TABLE' ORDER BY sys.tables.name;";


        public static readonly string CNST_STR_SELECT_LOGIN = "SELECT " + CNST_STR_TABLE_LOGIN + ".LOG_User FROM " + CNST_STR_TABLE_LOGIN + " ";
        public static readonly string CNST_STR_SELECT_LOGIN_ORDERBY = " ORDER BY " + CNST_STR_TABLE_LOGIN + ".LOG_User;";
        public static readonly string CNST_STR_SELECT_LOGIN_CURRENT = "SELECT " + CNST_STR_TABLE_LOGIN_CURRENT + ".LOGC_User, " + CNST_STR_TABLE_LOGIN_CURRENT + ".LOGC_DateTime, " + CNST_STR_TABLE_LOGIN_CURRENT + ".LOGC_PCIP FROM " + CNST_STR_TABLE_LOGIN_CURRENT + " ";
        public static readonly string CNST_STR_SELECT_LOGIN_CURRENT_ORDERBY = " ORDER BY " + CNST_STR_TABLE_LOGIN_CURRENT + ".LOGC_DateTime, " + CNST_STR_TABLE_LOGIN_CURRENT + ".LOGC_User;";
        public static readonly string CNST_STR_SELECT_USERGROUPS = "SELECT  " + CNST_STR_TABLE_USERGROUPS + ".USRGRP_User,  " + CNST_STR_TABLE_USERGROUPS + ".USRGRP_Group, " + CNST_STR_TABLE_MENU_GROUPS + ".GRP_MenuItem, " + CNST_STR_TABLE_MENU_MENUITEMS + ".MNU_Type, " + CNST_STR_TABLE_MENU_MENUITEMS + ".MNU_DisplayWhere " +
                                                                   "FROM  " + CNST_STR_TABLE_USERGROUPS + " " +
                                                                   "INNER JOIN " + CNST_STR_TABLE_MENU_GROUPS + " ON " + CNST_STR_TABLE_USERGROUPS + ".USRGRP_Group = " + CNST_STR_TABLE_MENU_GROUPS + ".GRP_Group " +
                                                                   "INNER JOIN " + CNST_STR_TABLE_MENU_MENUITEMS + " ON " + CNST_STR_TABLE_MENU_GROUPS + ".GRP_MenuItem = " + CNST_STR_TABLE_MENU_MENUITEMS + ".MNU_MenuItemName ";
        public static readonly string CNST_STR_SELECT_USERGROUPS_ORDERBY = " ORDER BY " + CNST_STR_TABLE_USERGROUPS + ".USRGRP_User, " + CNST_STR_TABLE_USERGROUPS + ".USRGRP_Group, " + CNST_STR_TABLE_MENU_MENUITEMS + ".MNU_DisplayWhere, " + CNST_STR_TABLE_MENU_MENUITEMS + ".MNU_Type, " + CNST_STR_TABLE_MENU_MENUITEMS + ".MNU_MenuItemName;";

        //find types
        public static readonly string CNST_STR_FINDSTOCKITEM = "FindStockItem";
        public static readonly string CNST_STR_FINDSTOCKDESCRIPTION = "FindStockDescription";
        public static readonly string CNST_STR_FINDLOCTRN = "FindLocTransaction";
        public static readonly string CNST_STR_FINDLOTTRN = "FindLotTransaction";
        public static readonly string CNST_STR_FINDLOC = "FindLocation";
        public static readonly string CNST_STR_FINDLOT = "FindLot";
        public static readonly string CNST_STR_FINDMEDIA = "FindMedia";
        public static readonly string CNST_STR_FINDUOM = "FindUOM";



        //SQL tables schema data - name, key field name
        //primary key list
        public static Dictionary<string, string> dicSQLSchema_PrimaryKey = new Dictionary<string, string>();
        //"secondary key" (most commonly used in form searches is actually second column in table)
        public static Dictionary<string, string> dicSQLSchema_SecondaryKey = new Dictionary<string, string>();
        //"tertiary" key (least commonly used in form searches is actually third column in table)
        public static Dictionary<string, string> dicSQLSchema_TertiaryKey = new Dictionary<string, string>();
        //form titles
        public static Dictionary<string, string> dicFormTitles = new Dictionary<string, string>();

        public static string GetPrimaryField(string strWhat)
        {
            /*
                 Created 07/08/2025 By Roger Williams

                 Looks through dicSQLSchema_PrimaryKey for the passed table then returns primary key

            */

            string strTemp = String.Empty;

            Modules.clsTables_Utilities.dicSQLSchema_PrimaryKey.TryGetValue(strWhat, out strTemp);

            return strTemp;

        }

        public static string GetSecondaryField(string strWhat)
        {
            /*
                 Created 07/08/2025 By Roger Williams

                 Looks through dicSQLSchema_SecondaryKey for the passed table then returns primary key

            */

            string strTemp = String.Empty;

            Modules.clsTables_Utilities.dicSQLSchema_SecondaryKey.TryGetValue(strWhat, out strTemp);

            return strTemp;

        }

        public static string GetTertiaryField(string strWhat)
        {
            /*
                 Created 07/08/2025 By Roger Williams

                 Looks through dicSQLSchema_TertiaryKey for the passed table then returns primary key

            */

            string strTemp = String.Empty;

            Modules.clsTables_Utilities.dicSQLSchema_TertiaryKey.TryGetValue(strWhat, out strTemp);

            return strTemp;

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
            Modules.clsData_Utilities.TTYPETableInfo TYPInfo;
            int intData = 0;
            SqlDataReader SQLRead;

            using (var SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
            {
                using (var SQLCmd = new SqlCommand(CNST_STR_QUERY_GETSCHEMAINFORMATION, SQLConn))
                {
                    SQLConn.Open();
                    SQLRead = SQLCmd.ExecuteReader();

                        while (SQLRead.Read())
                        {
                            TYPInfo = new Modules.clsData_Utilities.TTYPETableInfo();
                            TYPInfo.strTableName = SQLRead["TABLENAME"].ToString();
                            TYPInfo.strColumnName = SQLRead["COLUMNNAME"].ToString();
                            TYPInfo.strDescription = SQLRead["DESCRIPTION"].ToString();
                            intData = Convert.ToInt16(SQLRead["DATATYPE"]);

                            //store .Net equivalent of SQL datatype
                            switch (intData)
                            {
                                case 35: //text
                                    {
                                        TYPInfo.strDataType = "text";
                                        //manually set text maximum to 4096
                                        TYPInfo.intLength = 4096;
                                        break;
                                    }
                                case 167:  //varchar
                                    {
                                        TYPInfo.strDataType = "string";
                                        TYPInfo.intLength = Convert.ToInt16(SQLRead["MAXLENGTH"]);
                                        break;
                                    }
                                case 40:
                                    {
                                        TYPInfo.strDataType = "date";
                                        TYPInfo.intLength = 0;
                                        break;
                                    }
                                case 42:
                                    {
                                        TYPInfo.strDataType = "datetime2";
                                        TYPInfo.intLength = 0;
                                        break;
                                    }
                                case 61:
                                    {
                                        TYPInfo.strDataType = "datetime";
                                        TYPInfo.intLength = 0;
                                        break;
                                    }
                                case 60:
                                    {
                                        TYPInfo.strDataType = "money";
                                        TYPInfo.intLength = 0;
                                        break;
                                    }
                                case 104:
                                    {
                                        TYPInfo.strDataType = "bool";
                                        TYPInfo.intLength = 0;
                                        break;
                                    }
                                case 106:
                                    {
                                        TYPInfo.strDataType = "decimal";
                                        TYPInfo.intLength = 0;
                                        break;
                                    }
                            }

                            Modules.clsData_Utilities.lstTableInfo.Add(TYPInfo);
                        }

                    SQLRead.Close();
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
                using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
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
                        dicSQLSchema_PrimaryKey.Clear();
                        dicSQLSchema_SecondaryKey.Clear();
                        dicSQLSchema_TertiaryKey.Clear();

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
                                dicSQLSchema_PrimaryKey.Add(SDRTable["TABLE_NAME"].ToString(), SDRTable["COLUMN_NAME"].ToString());
                            }

                            if (intColNbr == 1)
                            {
                                dicSQLSchema_SecondaryKey.Add(SDRTable["TABLE_NAME"].ToString(), SDRTable["COLUMN_NAME"].ToString());
                            }

                            //technically this is not ideal, but add anyway!
                            if (intColNbr == 2)
                            {
                                //make surenot trying to add timestamp or text as these cannot be key types in SQL Server!
                                if (SDRTable["DATA_TYPE"].ToString() != "timestamp" && SDRTable["DATA_TYPE"].ToString() != "text")
                                {
                                    dicSQLSchema_TertiaryKey.Add(SDRTable["TABLE_NAME"].ToString(), SDRTable["COLUMN_NAME"].ToString());
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


        public static void GetFormTitles()
        {
            /*
                Created 14/03/2026 By Roger Williams

                gets form title from Menu_MenuItems
                puts into dicFormTitles


            */

            SqlDataReader SQLRead;
            SqlCommand SQLCmdDesc;
            SqlConnection SQLConn;

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmdDesc = new SqlCommand("SELECT * FROM " + Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_MENUITEMS + " ORDER BY MNU_MenuItemName;", SQLConn);
                    SQLRead = SQLCmdDesc.ExecuteReader();

                    //load from dataset
                    if (SQLRead.HasRows)
                    {
                        while (SQLRead.Read())
                        {
                            dicFormTitles.Add(SQLRead["MNU_MenuItemObject"].ToString(), SQLRead["MNU_MenuItemName"].ToString());
                        }

                    }

                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //****end class
    }
}
