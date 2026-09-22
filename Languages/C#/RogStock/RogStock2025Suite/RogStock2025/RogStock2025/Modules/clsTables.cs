using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogStock2025.Modules
{
    internal class clsTables
    {
        //tables
        public static readonly string CNST_STR_LOCTRN = "Loc_TRN";
        public static readonly string CNST_STR_LOTTRN = "Lot_TRN";

        public static readonly string CNST_STR_LOGIN = "Login";
        public static readonly string CNST_STR_LOGIN_CURRENT = "Login_Current";
        public static readonly string CNST_STR_USERGROUPS = "Menu_UsersGroups";

        public static readonly string CNST_STR_FIND_RELATIONS = "Find_Relations";
        public static readonly string CNST_STR_FIND_FIELDINFO = "Find_FieldInfo";

        public static readonly string CNST_STR_MENU_GROUPS = "Menu_Groups";
        public static readonly string CNST_STR_MENU_MENUITEMS = "Menu_MenuItems";
        public static readonly string CNST_STR_MENU_AREAS = "Menu_Areas";
        public static readonly string CNST_STR_MENU_USERGROUPS = "Menu_UsersGroups";

        public static readonly string CNST_STR_STOCK_LOC = "Stock_Loc";
        public static readonly string CNST_STR_STOCK_LOT = "Stock_Lot";
        public static readonly string CNST_STR_STOCK_ITEMS = "Stock_Items";
        public static readonly string CNST_STR_STOCK_VENDORS = "Stock_Vendors";
        public static readonly string CNST_STR_STOCK_DESCRIPTION = "Stock_Description";
        public static readonly string CNST_STR_STOCK_MEDIA = "Stock_Media";
        public static readonly string CNST_STR_STOCK_PRODUCTFAMILY = "Stock_ProductFamily";
        public static readonly string CNST_STR_STOCK_UOM = "Stock_UOM";

        //select queries

        //stock items
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_BASIC = "SELECT " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID, " + CNST_STR_STOCK_ITEMS + ".STKI_UOM, " + CNST_STR_STOCK_ITEMS + ".STKI_ProductFamily, " + CNST_STR_STOCK_ITEMS + ".STKI_Price, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_LongDesc, " + CNST_STR_STOCK_ITEMS + ".STKI_LocLot " +
                                                                          "FROM " + CNST_STR_STOCK_ITEMS + ", " + CNST_STR_STOCK_DESCRIPTION + " WHERE " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID = " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID ";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_BASIC_ORDERBY = " ORDER BY STKI_ItemID;";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_ALLINFORMATION = "SELECT " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID, " + CNST_STR_STOCK_ITEMS + ".STKI_UOM, " + CNST_STR_STOCK_ITEMS + ".STKI_ProductFamily, " + CNST_STR_STOCK_ITEMS + ".STKI_Price, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_LongDesc, " + CNST_STR_STOCK_ITEMS + ".STKI_LocLot " +
                                                                  "FROM " + CNST_STR_STOCK_ITEMS + ", " + CNST_STR_STOCK_DESCRIPTION + " WHERE " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID = " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID ";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_ALLINFORMATION_ORDERBY = " ORDER BY STKI_ItemID;";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_LOCLOT = "SELECT " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID, " + CNST_STR_STOCK_ITEMS + ".STKI_UOM, " + CNST_STR_STOCK_ITEMS + ".STKI_ProductFamily, " + CNST_STR_STOCK_ITEMS + ".STKI_Price, " + CNST_STR_STOCK_ITEMS + ".STKI_LocLot, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_LongDesc " +
                                                                  "FROM " + CNST_STR_STOCK_ITEMS + ", Stock_Description WHERE " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID = " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID ";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_LOCLOT_ORDERBY = " ORDER BY  " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID;";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_PRODUCTFAMILY = "SELECT " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID, " + CNST_STR_STOCK_ITEMS + ".STKI_UOM, " + CNST_STR_STOCK_ITEMS + ".STKI_ProductFamily, " + CNST_STR_STOCK_ITEMS + ".STKI_Price, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_LongDesc, " + CNST_STR_STOCK_ITEMS + ".STKI_LocLot " +
                                                                  "FROM " + CNST_STR_STOCK_ITEMS + ", " + CNST_STR_STOCK_DESCRIPTION + " WHERE " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID = " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID ";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_PRODUCTFAMILY_ORDERBY = " ORDER BY STKI_ProductFamily, STKI_ItemID;";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_UOM = "SELECT " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID, " + CNST_STR_STOCK_ITEMS + ".STKI_UOM, " + CNST_STR_STOCK_ITEMS + ".STKI_ProductFamily, " + CNST_STR_STOCK_ITEMS + ".STKI_Price, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_LongDesc, " + CNST_STR_STOCK_ITEMS + ".STKI_LocLot " +
                                                                     "FROM " + CNST_STR_STOCK_ITEMS + ", " + CNST_STR_STOCK_DESCRIPTION + " WHERE " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID = " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID ";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_UOM_ORDERBY = " ORDER BY  " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID;";
        //TRN history stock items
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY = "SELECT " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID, " + CNST_STR_STOCK_ITEMS + ".STKI_ProductFamily, " + CNST_STR_STOCK_ITEMS + ".STKI_LocLot, " + CNST_STR_STOCK_ITEMS + ".STKI_UOM, " + CNST_STR_STOCK_ITEMS + ".STKI_Price, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc, " + CNST_STR_LOCTRN + ".LOCT_DateTime, " + CNST_STR_LOCTRN + ".LOCT_Location, " +
                                                          CNST_STR_LOCTRN + ".LOCT_Qty, " + CNST_STR_LOCTRN + ".LOCT_Operation FROM " + CNST_STR_LOCTRN + " INNER JOIN " + CNST_STR_STOCK_DESCRIPTION + " ON Loc_TRN.LOCT_ItemID = " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID INNER JOIN " +
                                                          CNST_STR_STOCK_ITEMS + " ON " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID = " + CNST_STR_STOCK_ITEMS + ".STKI_ItemID";
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY_ORDERBY = " ORDER BY " + CNST_STR_LOCTRN + ".LOCT_Location," + CNST_STR_STOCK_ITEMS + ".STKI_ItemID, " + CNST_STR_LOCTRN + ".LOCT_DateTime;";
        //sub report lots
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY_LOTS = "SELECT " + CNST_STR_LOTTRN + ".LOTT_ItemID, " + CNST_STR_LOTTRN + ".LOTT_Nbr, " + CNST_STR_LOTTRN + ".LOTT_DateTime, " + CNST_STR_LOTTRN + ".LOTT_Qty, " + CNST_STR_LOTTRN + ".LOTT_Location, " + CNST_STR_LOTTRN + ".LOTT_Operation FROM " + CNST_STR_LOTTRN;
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY_LOTS_ORDERBY = " ORDER BY " + CNST_STR_LOTTRN + ".LOTT_DateTime, " + CNST_STR_LOTTRN + ".LOTT_Location," + CNST_STR_LOTTRN + ".LOTT_LotNbr;";

        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_MEDIA = "SELECT " + CNST_STR_STOCK_MEDIA + ".STKM_ItemID, " + CNST_STR_STOCK_MEDIA + ".STKM_Path, " + CNST_STR_STOCK_MEDIA + ".STKM_Type FROM " + CNST_STR_STOCK_MEDIA;
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS_MEDIA_ORDERBY = " ORDER BY " + CNST_STR_STOCK_MEDIA + ".STKM_Type, " + CNST_STR_STOCK_MEDIA + ".STKM_Path";

        //others
        public static readonly string CNST_STR_SELECT_STOCK_DESCRIPTION = "SELECT * FROM " + CNST_STR_STOCK_DESCRIPTION + " ";
        public static readonly string CNST_STR_SELECT_STOCK_DESCRIPTION_ORDERBY = " ORDER BY STKD_ItemID;";
        public static readonly string CNST_STR_SELECT_STOCK_MEDIA = "SELECT " + CNST_STR_STOCK_MEDIA + ".STKM_ItemID, " + CNST_STR_STOCK_MEDIA + ".STKM_Type, " + CNST_STR_STOCK_MEDIA + ".STKM_Path FROM " + CNST_STR_STOCK_MEDIA + " ";
        public static readonly string CNST_STR_SELECT_STOCK_MEDIA_ORDERBY = " ORDER BY STKM_ItemID;";
        public static readonly string CNST_STR_SELECT_STOCK_LOC = "SELECT " + CNST_STR_STOCK_LOC + ".LOC_Location, " + CNST_STR_STOCK_LOC + ".LOC_ItemID, " + CNST_STR_STOCK_LOC + ".LOC_Qty, " + CNST_STR_STOCK_LOC + ".LOC_NonNet, " + CNST_STR_STOCK_LOC + ".LOC_Description, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc " +
                                                                  "FROM " + CNST_STR_STOCK_LOC + ", " + CNST_STR_STOCK_DESCRIPTION + " WHERE " + CNST_STR_STOCK_LOC + ".LOC_ItemID = " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID";
        public static readonly string CNST_STR_SELECT_STOCK_LOC_ORDERBY = " ORDER BY LOC_ItemID;";
        public static readonly string CNST_STR_SELECT_STOCK_LOT = "SELECT " + CNST_STR_STOCK_LOT + ".LOT_Nbr, " + CNST_STR_STOCK_LOT + ".LOT_Qty, " + CNST_STR_STOCK_LOT + ".LOT_NonNet, " + CNST_STR_STOCK_LOT + ".LOT_ItemID, " + CNST_STR_STOCK_LOT + ".LOT_Location, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc " +
                                                                  "FROM " + CNST_STR_STOCK_LOT + ", " + CNST_STR_STOCK_DESCRIPTION + " WHERE " + CNST_STR_STOCK_LOT + ".LOT_ItemID = " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID ";
        public static readonly string CNST_STR_SELECT_STOCK_LOT_ORDERBY = " ORDER BY " + CNST_STR_STOCK_LOT + ".LOT_ItemID, " + CNST_STR_STOCK_LOT + ".LOT_Location,  " + CNST_STR_STOCK_LOT + ".LOT_Nbr ";
        public static readonly string CNST_STR_SELECT_STOCK_UOM = "SELECT " + CNST_STR_STOCK_UOM + ".STKU_Desc FROM " + CNST_STR_STOCK_UOM + " ";
        public static readonly string CNST_STR_SELECT_STOCK_UOM_ORDERBY = " ORDER BY STKU_Desc;";
        public static readonly string CNST_STR_SELECT_STOCK_PRODUCTFAMILY = "SELECT " + CNST_STR_STOCK_PRODUCTFAMILY + ".STKP_ProductFamily, " + CNST_STR_STOCK_PRODUCTFAMILY + ".STKP_Desc FROM " + CNST_STR_STOCK_PRODUCTFAMILY + " ";
        public static readonly string CNST_STR_SELECT_STOCK_PRODUCTFAMILY_ORDERBY = " ORDER BY STKP_ProductFamily;";
        public static readonly string CNST_STR_SELECT_LOCTRN = "SELECT " + CNST_STR_LOCTRN + ".LOCT_ItemID, " + CNST_STR_LOCTRN + ".LOCT_Location, " + CNST_STR_LOCTRN + ".LOCT_Qty, " + CNST_STR_LOCTRN + ".LOCT_DateTime, " + CNST_STR_LOCTRN + ".LOCT_Operation, " + CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc " +
                                                               "FROM " + CNST_STR_LOCTRN + ", " + CNST_STR_STOCK_DESCRIPTION + " WHERE " + CNST_STR_LOCTRN + ".LOCT_ItemID = " + CNST_STR_STOCK_DESCRIPTION + ".STKD_ItemID";
        public static readonly string CNST_STR_SELECT_LOCTRN_ORDERBY = " ORDER BY " + CNST_STR_LOCTRN + "LOCT_ItemID " + CNST_STR_LOCTRN + ".LOCT_DateTime, " + CNST_STR_LOCTRN + ".LOCT_Location;";
        public static readonly string CNST_STR_SELECT_LOTTRN = "SELECT " + CNST_STR_LOTTRN + ".LOTT_Qty, " + CNST_STR_LOTTRN + ".LOTT_DateTime, " + CNST_STR_LOTTRN + ".LOTT_ItemID, " + CNST_STR_LOTTRN + ".LOTT_Location, " + CNST_STR_LOTTRN + ".LOTT_Operation, " + CNST_STR_LOTTRN + ".LOTT_Nbr, " + CNST_STR_STOCK_DESCRIPTION + " .STKD_Desc " +
                                                               "FROM " + CNST_STR_LOTTRN + ", Stock_Description WHERE " + CNST_STR_LOTTRN + ".LOTT_ItemID = " + CNST_STR_STOCK_DESCRIPTION + " .STKD_ItemID";
        public static readonly string CNST_STR_SELECT_LOTTRN_ORDERBY = " ORDER BY" + CNST_STR_LOTTRN + ".LOTT_ItemID;";
        public static readonly string CNST_STR_SELECT_LOGIN = "SELECT " + CNST_STR_LOGIN + ".LOG_User FROM " + CNST_STR_LOGIN + " ";
        public static readonly string CNST_STR_SELECT_LOGIN_ORDERBY = " ORDER BY " + CNST_STR_LOGIN + ".LOG_User;";
        public static readonly string CNST_STR_SELECT_LOGIN_CURRENT = "SELECT " + CNST_STR_LOGIN_CURRENT + ".LOGC_User, " + CNST_STR_LOGIN_CURRENT + ".LOGC_DateTime, " + CNST_STR_LOGIN_CURRENT + ".LOGC_PCIP FROM " + CNST_STR_LOGIN_CURRENT + " ";
        public static readonly string CNST_STR_SELECT_LOGIN_CURRENT_ORDERBY = " ORDER BY " + CNST_STR_LOGIN_CURRENT + ".LOGC_DateTime, " + CNST_STR_LOGIN_CURRENT + ".LOGC_User;";
        public static readonly string CNST_STR_SELECT_USERGROUPS = "SELECT  " + CNST_STR_USERGROUPS + ".USRGRP_User,  " + CNST_STR_USERGROUPS + ".USRGRP_Group, " + CNST_STR_MENU_GROUPS + ".GRP_MenuItem, " + CNST_STR_MENU_MENUITEMS + ".MNU_Type, " + CNST_STR_MENU_MENUITEMS + ".MNU_DisplayWhere " +
                                                                   "FROM  " + CNST_STR_USERGROUPS + " " +
                                                                   "INNER JOIN " + CNST_STR_MENU_GROUPS + " ON " + CNST_STR_USERGROUPS + ".USRGRP_Group = " + CNST_STR_MENU_GROUPS + ".GRP_Group " +
                                                                   "INNER JOIN " + CNST_STR_MENU_MENUITEMS + " ON " + CNST_STR_MENU_GROUPS + ".GRP_MenuItem = " + CNST_STR_MENU_MENUITEMS + ".MNU_MenuItemName ";
        public static readonly string CNST_STR_SELECT_USERGROUPS_ORDERBY = " ORDER BY " + CNST_STR_USERGROUPS + ".USRGRP_User, " + CNST_STR_USERGROUPS + ".USRGRP_Group, " + CNST_STR_MENU_MENUITEMS + ".MNU_DisplayWhere, " + CNST_STR_MENU_MENUITEMS + ".MNU_Type, " + CNST_STR_MENU_MENUITEMS + ".MNU_MenuItemName;";

        //find types
        public static readonly string CNST_STR_FINDSTOCKITEM = "FindStockItem";
        public static readonly string CNST_STR_FINDSTOCKDESCRIPTION = "FindStockDescription";
        public static readonly string CNST_STR_FINDLOCTRN = "FindLocTransaction";
        public static readonly string CNST_STR_FINDLOTTRN = "FindLotTransaction";
        public static readonly string CNST_STR_FINDLOC = "FindLocation";
        public static readonly string CNST_STR_FINDLOT = "FindLot";
        public static readonly string CNST_STR_FINDMEDIA = "FindMedia";
        public static readonly string CNST_STR_FINDUOM = "FindUOM";



        //SQl tables schema data - name, key field name
        //primary key list
        public static Dictionary<string, string> dicSQLSchema_PrimaryKey = new Dictionary<string, string>();
        //"secondary key" (most commonly used in form searches is actually second column in table)
        public static Dictionary<string, string> dicSQLSchema_SecondaryKey = new Dictionary<string, string>();
        //"tertiary" key (least commonly used in form searches is actually third column in table)
        public static Dictionary<string, string> dicSQLSchema_TertiaryKey = new Dictionary<string, string>();


        public static string GetPrimaryField(string strWhat)
        {
            /*
                 Created 07/08/2025 By Roger Williams

                 Looks through dicSQLSchema_PrimaryKey for the passed table then returns primary key

            */

            string strTemp = String.Empty;

            Modules.clsTables.dicSQLSchema_PrimaryKey.TryGetValue(strWhat, out strTemp);

            return strTemp;

        }

        public static string GetSecondaryField(string strWhat)
        {
            /*
                 Created 07/08/2025 By Roger Williams

                 Looks through dicSQLSchema_SecondaryKey for the passed table then returns primary key

            */

            string strTemp = String.Empty;

            Modules.clsTables.dicSQLSchema_SecondaryKey.TryGetValue(strWhat, out strTemp);

            return strTemp;

        }

        public static string GetTertiaryField(string strWhat)
        {
            /*
                 Created 07/08/2025 By Roger Williams

                 Looks through dicSQLSchema_TertiaryKey for the passed table then returns primary key

            */

            string strTemp = String.Empty;

            Modules.clsTables.dicSQLSchema_TertiaryKey.TryGetValue(strWhat, out strTemp);

            return strTemp;

        }
    }
}
