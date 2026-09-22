using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace RogStock2025.Screens
{
    /*
       Modifed 18/02/2025 By Roger Williams

       Now uses data binding except form media as 1-many

 
       Created 18/02/2025 By Roger Williams

       CRUD form for stock items

       uses tables:

       Stock_Items
       Stock_Description
       Stock_Media

       Uses an old skool approach to data handling no data binding!:)
       Partially for fun but also because listviews cannot be data bound (BOO!)

    */
    public partial class frmStockItems : Form
    {
        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBSTKI_ItemID";
        //datasets used to checking if data changed
        DataSet DSTDataItems = new DataSet();
        DataSet DSTDataDescription = new DataSet();
        DataSet DSTDataMedia = new DataSet();
        //used globally as bound controls need active datasets
        SqlConnection SQLConn = null;

        bool blnNew = false;
        bool blnOk = false;
        bool blnLoading = false;
        bool blnShow = true;
        Pen penTemp;
        //binding sources to link tables to controls
        BindingSource BNSStock_Item = new BindingSource();
        BindingSource BNSStock_Description = new BindingSource();
        //media list populated when loaded
        List<string> lstMediaOld = new List<string>();
        //compared to above to check for changes
        List<string> lstMediaCur = new List<string>();

        //find form
        frmFind frmTemp = new frmFind(Modules.clsTables.CNST_STR_FINDSTOCKITEM);


        public frmStockItems()
        {
            InitializeComponent();
        }

        //****************custom funcs/subs************


        private bool InitData()
        {
            /*
              Created 18/08/2025 By Roger Williams
                
              Fills:

              DSTDataItems;
              DSTDataDescription
              DSTDataMedia

              needed for data bound controls to work!

            */

            SqlCommand SQLCmd;
            SqlDataAdapter DADTemp;

            try
            {
                //use open SQL connection
                this.SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
                //stay open for duration of appplication run 
                SQLConn.Open();
                //get location data
                DSTDataItems = new DataSet();
                SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_BASIC, SQLConn);
                DADTemp = new SqlDataAdapter(SQLCmd);
                DADTemp.Fill(this.DSTDataItems, Modules.clsTables.CNST_STR_STOCK_ITEMS);
                DADTemp.Fill(this.DSTDataItems, Modules.clsTables.CNST_STR_STOCK_ITEMS);

                DSTDataDescription = new DataSet();
                SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_STOCK_DESCRIPTION, SQLConn);
                DADTemp = new SqlDataAdapter(SQLCmd);
                DADTemp.Fill(this.DSTDataDescription, Modules.clsTables.CNST_STR_STOCK_DESCRIPTION);

                DSTDataMedia = new DataSet();
                SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_STOCK_MEDIA, SQLConn);
                DADTemp = new SqlDataAdapter(SQLCmd);
                DADTemp.Fill(this.DSTDataMedia, Modules.clsTables.CNST_STR_STOCK_MEDIA);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Data:\n\n" + ex.Message, "Database Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                blnShow = false;
                return false;
            }
        }

        private void BindForm()
        {
            /*
              Created 13/06/2025 By Roger Williams

              binds form to tables: 
            
              Stock_Items
              Stock_Description

              Note: cant add media as listviews do not support binding, plus 1-to-many relationship!

            */

            if (this.BNSStock_Item.Count > 0)
            {
                BNSStock_Item = new BindingSource();
                BNSStock_Description = new BindingSource();
            }

            BNSStock_Item.DataSource = DSTDataItems.Tables[Modules.clsTables.CNST_STR_STOCK_ITEMS];
            BNSStock_Description.DataSource = DSTDataDescription.Tables[Modules.clsTables.CNST_STR_STOCK_DESCRIPTION];


            //clear bindings
            this.TXTHidden.DataBindings.Clear();
            this.CMBSTKI_ProductFamily.DataBindings.Clear();
            this.CMBSTKI_UOM.DataBindings.Clear();
            this.NUDSTKI_Price.DataBindings.Clear();
            this.CHKSTKI_LocLot.DataBindings.Clear();
            this.TXTSTKD_Desc.DataBindings.Clear();
            this.TXTSTKD_LongDesc.DataBindings.Clear();

            //bind form controls to stock_LOT
            this.TXTHidden.DataBindings.Add("text", BNSStock_Item, "STKI_ItemID", true, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBSTKI_ProductFamily.DataBindings.Add("text", BNSStock_Item, "STKI_ProductFamily", true, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBSTKI_UOM.DataBindings.Add("text", BNSStock_Item, "STKI_UOM", true, DataSourceUpdateMode.OnPropertyChanged);
            this.NUDSTKI_Price.DataBindings.Add("value", BNSStock_Item, "STKI_Price", true, DataSourceUpdateMode.OnPropertyChanged);
            this.CHKSTKI_LocLot.DataBindings.Add("checked", BNSStock_Item, "STKI_LocLot", true, DataSourceUpdateMode.OnPropertyChanged);

            this.TXTSTKD_Desc.DataBindings.Add("text", BNSStock_Description, "STKD_Desc",true, DataSourceUpdateMode.OnPropertyChanged);
            this.TXTSTKD_LongDesc.DataBindings.Add("text", BNSStock_Description, "STKD_LongDesc", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        private void ResetForm(string strKeep, bool blnEnable)
        {
            /*
             Created 25/02/2025 By Roger Williams

             Resets form 
             Enables/Disables form

            VARS

            strKeep     - control to leave
            blnEnable   - enable or disable form

            */

            //reset form
            Modules.clsView.ResetForm(this, strKeep);
            Modules.clsView.EnableDisableForm(this, this.CNST_STR_FIRSTCONTROL, blnEnable);
            blnNew = false;
            //clear media listviews
            lstMediaCur.Clear();
            lstMediaOld.Clear();
            //mnake sure find button visible!
            this.BTNFind.Enabled = true;
        }

        //data
        private bool CheckForUOMProdFam()
        /*
             Created 06/03/2025 By Roger Williams
         
             Returns true if UOM AND product family records exist in the database

        */
        {
            int intNum = 0;

            if (Modules.clsData.CheckForProductFamily()) intNum++;
            if (Modules.clsData.CheckForUOM()) intNum++;

            if (intNum == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadRecord()
        {
            /*
              Modified 16/06/2025 By Roger Williams

              Added code to populate list: lstMediaold/cur with stockitem_media
              this is used during save to see if anything has changed

              
              Created 18/02/2025 By Roger Williams
              
              - Populates the form
              - Populates dataset with table and a COPY of the table
              - Enables the form

            Note: recads from these tables:
                  - Stock_Items
                  - Stock_Description
                  - Stock_Media
                  - Stock_Vendors <- implement in version 2

            */
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            DataRowCollection DRCRows;
            ListViewItem LVITemp;
            DataRow DARRow;
            SqlDataAdapter DADTemp;

            string strSQL1 = "SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_STOCK_ITEMS) + " ='" + this.CMBSTKI_ItemID.Text + "';";
            string strSQL2 = "SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_DESCRIPTION + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_STOCK_DESCRIPTION) + " ='" + this.CMBSTKI_ItemID.Text + "';";
            string strSQL3 = "SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_MEDIA + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_STOCK_MEDIA) + " ='" + this.CMBSTKI_ItemID.Text + "';";

            blnLoading = true;
            //reset form except item id combobox
            ResetForm("CMBSTKI_ItemID", false);


 //           try
 //           {
//                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
//                {
//                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = strSQL1;
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    if (SQLRead != null)
                    {
                        SQLRead.Close();
                        //clear existing records
                        if (DSTDataItems.Tables.Count > 0)
                        {
                            DSTDataDescription.Tables[Modules.clsTables.CNST_STR_STOCK_DESCRIPTION].Clear();
                            DSTDataItems.Tables[Modules.clsTables.CNST_STR_STOCK_ITEMS].Clear();
                            DSTDataMedia.Tables[Modules.clsTables.CNST_STR_STOCK_MEDIA].Clear();
                        }
                        ////get record into into dataset
                        DADTemp = new SqlDataAdapter(strSQL1, SQLConn);
                        DADTemp.SelectCommand.CommandText = strSQL1;
                        DADTemp.Fill(DSTDataItems, Modules.clsTables.CNST_STR_STOCK_ITEMS);
                        DADTemp.SelectCommand.CommandText = strSQL2;
                        DADTemp.Fill(DSTDataDescription, Modules.clsTables.CNST_STR_STOCK_DESCRIPTION);
                        DADTemp.SelectCommand.CommandText = strSQL3;
                        DADTemp.Fill(DSTDataMedia, Modules.clsTables.CNST_STR_STOCK_MEDIA);

                //bindform
                BindForm();

                        //populate form 
                        //DARRow = DSTDataItems.Tables[Modules.clsTables.CNST_STR_STOCK_ITEMS].Rows[0];
                        //this.CMBSTKI_ProductFamily.Text = DARRow["STKI_ProductFamily"].ToString();
                        //this.CMBSTKI_UOM.Text = DARRow["STKI_UOM"].ToString();
                        //this.CHKSTKI_LocLot.Checked = (bool)DARRow["STKI_LocLot"];
                        //this.NUDSTKI_Price.Value = (decimal)DARRow["STKI_Price"];
                        ////get description data from datasetcur
                        ////Note: only need to read first record as 1 - 1 relationship
                        //DARRow = DSTDataDescription.Tables[Modules.clsTables.CNST_STR_STOCK_DESCRIPTION].Rows[0];

                        //this.TXTSTKD_Desc.Text = DARRow["STKD_Desc"].ToString();
                        //this.TXTSTKD_LongDesc.Text = DARRow["STKD_LongDesc"].ToString();

                        ////read media into listview
                        DRCRows = DSTDataMedia.Tables[Modules.clsTables.CNST_STR_STOCK_MEDIA].Rows;
                        //clear listview
                        this.LVMedia.Items.Clear();

                        foreach (DataRow DARTemp in DRCRows)
                        {
                            LVITemp = new ListViewItem();
                            LVITemp.Text = DARTemp["STKM_Path"].ToString();
                            LVITemp.SubItems.Add(DARTemp["STKM_Type"].ToString());
                            this.LVMedia.Items.Add(LVITemp);
                            //add to media lists for comparison on save
                            lstMediaOld.Add(DARTemp["STKM_Path"].ToString());
                            lstMediaCur.Add(DARTemp["STKM_Path"].ToString());
                        }

                    //    SQLConn.Close();
                        //enable form

                        ////bindform
                        //BindForm();
                        Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                        //update hidden text control with item id
                        this.TXTHidden.Text = this.CMBSTKI_ItemID.Text;
                    }
                    else
                    {
                        MessageBox.Show("No Records Found", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
  //              }
  //          }
  //          catch (Exception ex)
  //          {
  //              //Whoops!
  //              MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
  //          }
            blnLoading = false;
        }
        private void DeleteRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Deletes current record using a transaction
              - clears form 
              - clears datasets

            */

       //     SqlConnection SQLConn;
            SqlConnection SQLConnLot;
            SqlDataAdapter DADLot;
            DataSet DSTLot;
            SqlCommand SQLCmdLots;
            SqlCommand SQLCmdItems;
            SqlCommand SQLCmdDesc;
            SqlCommand SQLCmdMedia;
            SqlTransaction SQLTransaction;
      //      int intWritten = 0;

            try
            {
                //using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                //{
                //    SQLConn.Open();

                    //create command objects for each table
                    SQLCmdItems = new SqlCommand("DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_STOCK_ITEMS) + " = '" + this.CMBSTKI_ItemID.Text + "';", SQLConn);
                    SQLCmdDesc = new SqlCommand("DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_DESCRIPTION + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_STOCK_DESCRIPTION) + " = '" + this.CMBSTKI_ItemID.Text + "';", SQLConn);
                    SQLCmdMedia = new SqlCommand("DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_MEDIA + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_STOCK_MEDIA) + " = '" + this.CMBSTKI_ItemID.Text + "';", SQLConn);
                    //start transction
                    SQLTransaction = SQLConn.BeginTransaction();
                    //assign commands to the transaction
                    SQLCmdDesc.Transaction = SQLTransaction;
                    SQLCmdItems.Transaction = SQLTransaction;
                    SQLCmdMedia.Transaction = SQLTransaction;


                    try
                    {
                        //delete existing
                        SQLCmdItems.CommandType = CommandType.Text;
                        SQLCmdMedia.ExecuteNonQuery();

                        SQLCmdDesc.CommandType = CommandType.Text;
                        SQLCmdMedia.ExecuteNonQuery();

                        SQLCmdMedia.CommandType = CommandType.Text;
                        SQLCmdMedia.ExecuteNonQuery();

                        //check for loc/lot tracking
                        if (this.CHKSTKI_LocLot.Checked)
                        {
                            //get lot rows into dstlot
                            using (SQLConnLot = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                            {
                                DADLot = new SqlDataAdapter("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_STOCK_LOT) + " = '" + this.CMBSTKI_ItemID.Text + "';", SQLConnLot);
                                DSTLot = new DataSet();
                                DADLot.Fill(DSTLot, Modules.clsTables.CNST_STR_STOCK_LOT);
                                SQLCmdLots = new SqlCommand();
                                SQLCmdLots.Connection = SQLConnLot;
                                SQLCmdLots.Transaction = SQLTransaction;

                                //get lot info write TRN delete
                                foreach (DataRow DTRTemp in DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows)
                                {
                                    //create TRN lot
                                    SQLCmdLots.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOTTRN + " (LOTT_ItemID, LOTT_Nbr, LOTT_Qty, LOTT_Location, LOTT_Operation) " +
                                                        "VALUES ('" + this.CMBSTKI_ItemID.Text + "'," + DTRTemp["LOT_Nbr"] + "," + DTRTemp["LOT_Qty"] + ",'" + DTRTemp["LOT_Location"] + "','" + Modules.clsData.CNST_STR_OPERATION_DELETE + "');";
                                    //write LOT TRN record
                                    SQLCmdLots.ExecuteNonQuery();

                                    //delete existing lot
                                    SQLCmdLots.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + "  WHERE " + Modules.clsTables.GetSecondaryField(Modules.clsTables.CNST_STR_STOCK_LOT) + " = " + DTRTemp[Modules.clsTables.GetSecondaryField(Modules.clsTables.CNST_STR_STOCK_LOT)] + ";";
                                    //write LOT record
                                    SQLCmdLots.ExecuteNonQuery();
                                }
                            }
                        }

                        //write changes
                        SQLTransaction.Commit();
                        //remove from binding source
                        BNSStock_Description.RemoveCurrent();
                        BNSStock_Item.RemoveCurrent();

                        Modules.clsData.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);
                        ResetForm("", false);
                        MessageBox.Show("Record Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        SQLTransaction.Rollback();
                        MessageBox.Show("Error Deleting Data:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                //    SQLConn.Close();
                //}
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Accessing Database:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveRecord()
        {
            /*
              Modified 13/06/2025 By Roger Williams
            
              Using databinds except for media table

             
              Created 18/02/2025 By Roger Williams

              - Saves record to stock_teims and stock_description (stock_vendors -> v2)
              - Clears form 
              - If new just writes form contents if existing post dataset table changes
              - If loc/lot tracking set to off check no existing lots for item if so DELETES them

            */

        //    SqlConnection SQLConn;
            SqlCommand SQLCmdItems;
            SqlCommand SQLCmdDesc;
            SqlCommand SQLCmdMedia;
            SqlTransaction SQLTrans;
            int intNum = 0;
            int intBoolean = 0;
            bool blnOk = false;
            string strSQLError = String.Empty;

            //commit changes to dataset
            this.BNSStock_Item.EndEdit();
            this.BNSStock_Description.EndEdit();

            //check required fields completed
            if (Modules.clsView.ValidateRequiredFields(this))
            {

                //check if already exists
                if (blnNew)
                {
                    if (Modules.clsData.CheckStockItemIDExists(this.CMBSTKI_ItemID.Text))
                    {
                        MessageBox.Show("Cannot Add " + this.CMBSTKI_ItemID.Text + " As It Already Exists!", "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (!blnNew)
                {
                    //if existing record check something to save!
                    if (CheckForChanges() == false)
                    {
                        MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                try
                {
                    //using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                    //{
                    //    SQLConn.Open();
                        //create command objects for each table
                        SQLCmdItems = new SqlCommand("SELECT;", SQLConn);
                        SQLCmdDesc = new SqlCommand("SELECT;", SQLConn);
                        SQLCmdMedia = new SqlCommand("SELECT;", SQLConn);

                        //start transction
                        SQLTrans = SQLConn.BeginTransaction();
                        //assign commands to the transaction
                        SQLCmdDesc.Transaction = SQLTrans;
                        SQLCmdItems.Transaction = SQLTrans;
                        SQLCmdMedia.Transaction = SQLTrans;

                        try
                        {
                            if (!blnNew)
                            {
                                if (DSTDataItems.Tables[Modules.clsTables.CNST_STR_STOCK_ITEMS].Rows[0].RowState == DataRowState.Modified)
                                {
                                    //Update records!
                                    intBoolean = this.CHKSTKI_LocLot.Checked == true ? -1 : 0;
                                    SQLCmdItems.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " " +
                                                              "SET STKI_UOM = '" + this.CMBSTKI_UOM.Text + "', STKI_ProductFamily ='" + this.CMBSTKI_ProductFamily.Text + "', STKI_Price =" + this.NUDSTKI_Price.Value +
                                                              ", STKI_LocLot =" + intBoolean + " WHERE STKI_ItemID ='" + this.CMBSTKI_ItemID.Text + "';";
                                    SQLCmdItems.ExecuteNonQuery();

                                    //    strSQLError = Modules.clsData.SaveRecord(SQLConn, Modules.clsTables.CNST_STR_STOCK_ITEMS, this, true, SQLTrans);

                                    if (strSQLError != String.Empty)
                                    {
                                        throw new Exception(strSQLError);
                                    }
                                }

                                if (DSTDataDescription.Tables[Modules.clsTables.CNST_STR_STOCK_DESCRIPTION].Rows[0].RowState != DataRowState.Unchanged)
                                {
                                    if (DSTDataDescription.Tables[Modules.clsTables.CNST_STR_STOCK_DESCRIPTION].Rows[0].RowState == DataRowState.Modified)
                                    {
                                        SQLCmdDesc.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_STOCK_DESCRIPTION + " " +
                                                                 "SET STKD_Desc = '" + this.TXTSTKD_Desc.Text + "', STKD_LongDesc ='" + this.TXTSTKD_LongDesc.Text + "'" +
                                                                 " WHERE STKD_ItemID ='" + this.CMBSTKI_ItemID.Text + "';";
                                    }

                                    SQLCmdDesc.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                //add new records 
                                intBoolean = this.CHKSTKI_LocLot.Checked == true ? -1 : 0;
                                SQLCmdItems.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " (STKI_ItemID, STKI_UOM, STKI_ProductFamily, STKI_Price, STKI_LocLot) " +
                                                          "VALUES('" + this.CMBSTKI_ItemID.Text + "','" + this.CMBSTKI_UOM.Text + "','" + this.CMBSTKI_ProductFamily.Text + "'," + this.NUDSTKI_Price.Value + "," + intBoolean + ");";
                                SQLCmdDesc.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_STOCK_DESCRIPTION + " (STKD_ItemID, STKD_Desc, STKD_LongDesc) " +
                                                          "VALUES('" + this.CMBSTKI_ItemID.Text + "','" + this.TXTSTKD_Desc.Text + "','" + this.TXTSTKD_LongDesc.Text + "');";

                                SQLCmdDesc.ExecuteNonQuery();
                            }

                            //store media files if any handled differently as 1-many relationship
                            if (blnNew)
                            {
                                //add everything in the listview
                                for (intNum = 0; intNum != this.LVMedia.Items.Count; intNum++)
                                {                                                                                                                                                                                                                    //subitems start at 1
                                    SQLCmdMedia.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_STOCK_MEDIA + " (STKM_ItemID, STKM_Path, STKM_Type) VALUES ('" + this.CMBSTKI_ItemID.Text + "','" + this.LVMedia.Items[intNum].Text + "','" + this.LVMedia.Items[intNum].SubItems[1].Text + "');";
                                    SQLCmdMedia.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                //MANUALLY check if new/deleted/edited!

                                //check 1: is anything in lstmediacur not in lstmediaold?
                                foreach (string strTemp in lstMediaCur)
                                {
                                    if (!lstMediaOld.Contains(strTemp))
                                    {
                                        //get listview item
                                        foreach (ListViewItem LVITemp in this.LVMedia.Items)
                                        {
                                            if (LVITemp.Text == strTemp)
                                            {
                                                //add
                                                SQLCmdMedia.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_STOCK_MEDIA + " (STKM_ItemID, STKM_Path, STKM_Type) VALUES ('" + this.CMBSTKI_ItemID.Text + "','" + LVITemp.Text + "','" + LVITemp.SubItems[1].Text + "');";
                                                SQLCmdMedia.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }

                                //check 2: is anything in the lstmediaold and not in listview?
                                foreach (string strTemp in lstMediaOld)
                                {
                                    if (!lstMediaCur.Contains(strTemp))
                                    {
                                        //add
                                        SQLCmdMedia.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_MEDIA + " WHERE STKM_ItemID ='" + this.CMBSTKI_ItemID.Text + "' AND STKM_Path ='" + strTemp + "';";
                                        SQLCmdMedia.ExecuteNonQuery();
                                    }
                                }
                            }

                            //check lots for item ID and loc/lot if tracking switched OFF
                            if (this.CHKSTKI_LocLot.Checked == false)
                            {
                                if (Modules.clsData.CheckForLotsForItemID(this.CMBSTKI_ItemID.Text))
                                {
                                    SQLCmdItems.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE LOT_ItemID = '" + this.CMBSTKI_ItemID.Text + "';";
                                    SQLCmdItems.ExecuteNonQuery();
                                }
                            }

                            //write changes
                            SQLTrans.Commit();
                            //clear form dataset
                            this.BNSStock_Description.RemoveCurrent();
                            this.BNSStock_Item.RemoveCurrent();

                            //reset form
                            ResetForm("", false);
                            Modules.clsData.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);
                            MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            if (SQLTrans != null)
                            {
                                SQLTrans.Rollback();
                            }

                            MessageBox.Show("Error Saving Data:\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    //    SQLConn.Close();
                    //}
                }
                catch (Exception ex)
                {
                    //Whoops!                
                    MessageBox.Show("Error Accessing Database:\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CheckForChanges()
        {
            /*
              Modified 18/06/2025 By Roger Williams
            
              Now uses lstmediacur/old to check for media changes


              Created 19/02/2025 By Roger Williams

              Checks datasets and compares values to controls to see if any changes


            */

            int intChangesMade = 0;
 
            //update binding sources
            BNSStock_Description.EndEdit();
            BNSStock_Item.EndEdit();

            if (DSTDataDescription.Tables[Modules.clsTables.CNST_STR_STOCK_DESCRIPTION].GetChanges() != null)
            {
                intChangesMade  ++;
            }

            if (DSTDataItems.Tables[Modules.clsTables.CNST_STR_STOCK_ITEMS].GetChanges() != null)
            {
                intChangesMade++;
            }

            //check media

            //check 1: is anything in lstmediacur not in lstmediaold?
            foreach (string strTemp in lstMediaCur)
            {
                if (!lstMediaOld.Contains(strTemp))
                {
                    intChangesMade++;
                }
            }

            //check 2: is anything in the lstmedia and not in listview?
            foreach (string strTemp in lstMediaOld)
            {
                if (!lstMediaCur.Contains(strTemp))
                {
                    intChangesMade++;
                }
            }

            if (intChangesMade != 0)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        private void AddFile()
        {
            /*

            Modified 17/06/2025 By Roger Williams

            Adds to lstMediaCur as well


            Created 17/02/2025 By Roger Williams

            Adds selected file to list

            */

            ListViewItem LVITemp;
            string strTemp1 = string.Empty; ;
            int intNum1 = 0;
            string[] aryFilter;

            this.OFDSelectFile.Title = "Select File To Associate With " + this.CMBSTKI_ItemID.Text;
            this.OFDSelectFile.FileName = string.Empty; ;
            this.OFDSelectFile.FilterIndex = 1;
            this.OFDSelectFile.ShowDialog();

            if (this.OFDSelectFile.FileName != string.Empty)
            {
                //first make sure file not in list!
                if (lstMediaCur.Contains(this.OFDSelectFile.FileName))
                {
                    MessageBox.Show("File Already In List!", "Duplicate Filename", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                LVITemp = new ListViewItem();
                LVITemp.Text = this.OFDSelectFile.FileName;
                LVITemp.Tag = "STKM_Path";
                intNum1 = this.OFDSelectFile.FilterIndex;
                lstMediaCur.Add(this.OFDSelectFile.FileName);

                //extract filter first part
                if (intNum1 == 1)
                {
                    strTemp1 = this.OFDSelectFile.Filter.Substring(0, this.OFDSelectFile.Filter.IndexOf("|"));
                }
                else
                {
                    aryFilter = this.OFDSelectFile.Filter.Split('|');
                    intNum1 = intNum1 * 2 - 2;
                    strTemp1 = aryFilter[intNum1];
                }

                LVITemp.SubItems.Add(strTemp1);
                this.LVMedia.Items.Add(LVITemp);
                LVITemp.Tag = "STKM_Type";
            }
        }

        //other
        private void RemoveFile()
        {
            /*
               Modified 17/06/2025 By Roger Williams

               Removes from lstMediaCur as well
               

               Created 17/02/2025 By Roger Williams

               Removes selectred item

             */
            if (this.LVMedia.SelectedItems.Count != 0)
            {
                lstMediaCur.Remove(this.LVMedia.SelectedItems[0].Text);
                this.LVMedia.Items.RemoveAt(this.LVMedia.SelectedItems[0].Index);
            }
        }

        //form events
        private void frmStockItems_Load(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              Populates the comboboxes with table values and disables form 

              First checks if any UOM and product family records, if not wont open!

            */

            if (CheckForUOMProdFam() == false)
            {
                //no lots for location!
                MessageBox.Show("No Product Family or UOM Records Available - Cannot Continue!\n\nPlease Check Stock UOM Maintenance and Stock Product Family Maintenance Screens For Data ", "No Records Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                blnShow = false;
            }

            if (InitData())
            { 
                Modules.clsData.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);
                Modules.clsData.PopulateComboBoxes(this.CMBSTKI_ProductFamily, Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY, "", "", "", "", "", false);
                Modules.clsData.PopulateComboBoxes(this.CMBSTKI_UOM, Modules.clsTables.CNST_STR_STOCK_UOM, "", "", "", "", "", false);
                Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, false);
                //create pen for line drawing
                penTemp = new Pen(Color.Black, 1);
                penTemp.Color = Color.Black;
                //stop form being moved over menu button 
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                this.BTNFind.Enabled = true;
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
        }

        private void frmStockItems_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              Draws line across screen
            */
            //draw line
            e.Graphics.DrawLine(penTemp, 0, 80, this.Width, 80);
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
            */
            SaveRecord();
        }

        private void BTNAddFile_Click(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams

               Allows user to add mdeia file  
            */

            AddFile();
        }

        private void LVMedia_DoubleClick(object sender, EventArgs e)
        {
            //previews the file (if possible)

            this.WEBPreview.Navigate(this.LVMedia.SelectedItems[0].Text);
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
            */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void BTNRemoveFile_Click(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              Remove selected media file
            */
            RemoveFile();
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
            */
            blnNew = true;
            //enable form
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
            //reset form
            Modules.clsView.ResetForm(this, "");
        }

        private void CMBSTKI_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams

                   Load selected record

            */

            blnOk = true;

            if (blnNew)
            {
                //if new record do nothing
                return;
            }

            if (this.CMBSTKI_ItemID.SelectedIndex != -1)
            {

                if (this.TXTHidden.Text == this.CMBSTKI_ItemID.Text)
                {
                    return;
                }

                if (!blnLoading)
                {
                    //if no records load!
                    LoadRecord();
                }
            }
        }
        private void CMBSTKI_ItemID_TextUpdate(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
            */
            if (blnNew)
            {
                //if new record do nothing
                this.TXTHidden.Text = this.CMBSTKI_ItemID.Text;
            }
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
            Created 19/02/2025 By Roger Williams

            undoes changes if made

            */

            if (blnNew || CheckForChanges())
            {
                if (MessageBox.Show("Lose Changes?", "Changes Made", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    SaveRecord();
                }
                else
                {
                    if (!blnNew)
                    {
                        BNSStock_Description.CancelEdit();
                        BNSStock_Item.CancelEdit();
                        LoadRecord();
                    }
                    else
                    {
                        ResetForm("", false);
                    }
                }
            }
        }

        private void CMBSTKI_ItemID_TextChanged(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
            */

            //if new record set txthidden and its changed property
            if (blnNew)
            {
                this.TXTHidden.Text = this.CMBSTKI_ItemID.Text;
                this.TXTHidden.Modified = true;
            }

        }

        private void CMBSTKI_ItemID_Enter(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              Updates hidden text box
            */
            this.TXTHidden.Text = this.CMBSTKI_ItemID.Text;
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              Delete item
            */
            string strMessage = string.Empty; ;


            if (blnNew)
            {
                BTNUndo_Click(sender, e);
                return;
            }

            if (this.CHKSTKI_LocLot.Checked == false)
            {
                strMessage = "Delete Stock Item?";
            }
            else
            {
                strMessage = "Delete Stock Item?\n\nThis Item Is Location and Lot Tracked\nDeleting This Item Will Delete ALL Associated Locations AND Lots\n\nAre You Sure?";
            }

            if (MessageBox.Show(strMessage, "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //delete record 
                DeleteRecord();
            }
        }

        private void CHKSTKI_LocLot_CheckedChanged(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              Checks for existing lots if switched off

            */


            if (blnNew || blnLoading)
            {
                return;
            }

            if (this.CHKSTKI_LocLot.Checked == false)
            {
                //check no lots for item id
                if (Modules.clsData.CheckForLotsForItemID(this.CMBSTKI_ItemID.Text))
                {
                    MessageBox.Show("Clearing This Will Delete ALL Associated Lots When Saved", "Lot Deletion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void CMBSTKI_ItemID_Leave(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              If not new item check if type value exists in list

            */
            if (!blnOk)
            {
                if (blnNew)
                {
                    //if new record do nothing
                    return;
                }

                if (this.CMBSTKI_ItemID.SelectedIndex != -1)
                {
                    if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
                    {
                        if (this.TXTHidden.Text == this.CMBSTKI_ItemID.Text)
                        {
                            return;
                        }


                        if (!blnLoading)
                        {
                            //if no records load!
                            LoadRecord();
                        }
                    }
                }
            }

            blnOk = false;
        }

        private void CMBSTKI_UOM_Leave(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams

              Checked type value in list
              
            */
            Modules.clsView.ValueInCombobox(this.CMBSTKI_UOM, this.CMBSTKI_UOM.Text);
        }

        private void CMBSTKI_ProductFamily_Leave(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams

              Checked type value in list
              
            */
            Modules.clsView.ValueInCombobox(this.CMBSTKI_ProductFamily, this.CMBSTKI_ProductFamily.Text);
        }

        private void frmStockItems_Shown(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams

              If missing records in UOM/Product Family tables close form!
              
            */
            if (blnShow == false)
            {
                BTNClose_Click(sender, e);
            }
        }

        private void BTNFind_Click(object sender, EventArgs e)
        {
            frmTemp.ShowDialog();

            if (Modules.clsData.objFindSelected != null)
            { 
                this.CMBSTKI_ItemID.Text = Modules.clsData.objFindSelected.ToString();
            }
        }

        //class end
    }
}
