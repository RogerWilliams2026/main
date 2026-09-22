using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace RogStock2025.Screens
{
    public partial class frmAdjustQuantity : Form
    {
        /*

          Created 28/02/2025 By Roger Williams

          Uses disconnected recordset to load ONE record into the form 

          User can edit record as only affects dataset

          Note: user cannot create new Lot records here that is done automatically
                via adjust quantity screen or creating a new Location for a Loc/Lot
                tracked item

          Dataset changes are saved to the database

          Stores item ID in hidden control TXTHidden so item ID combobox can't be 
          used to rename items! 

          Uses:

           Dataset for Loc/Lot records
           DataAdapter (to load record into dataset)
           SQL Command to load/save data

           This form does not have a table so uses data from these:

           - Stock_Items
           - Stock_Loc
           - Stock_Lot

           With Locations/Lots being bound to a dataset->datagrid

           Adjust Qty ONLY allows adjustments, no deletions or additions and in this
           version only one Location can be edited at a time

           Note: Due to setting name columns for datagrid in SQL statements field names
                 such as Lot_Qty are reduced to: Qty 

        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBSTKI_ItemID";

        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd;
        SqlDataAdapter DADLoc;
        //datasets used to bind gridviews
        DataSet DSTLoc;
        DataSet DSTLot;
        bool blnLoading = false;
        bool blnOk = false;
        bool blnShow = true;
        int intDVGLocIndex = 0;
        Pen penTemp;
        int intSelectedLoc = -1;

        //find form
        frmFind frmTemp = new frmFind(Modules.clsTables.CNST_STR_FINDSTOCKITEM);

        //holds current item id used if record deleted
        string strItemID = string.Empty;
        public frmAdjustQuantity()
        {
            InitializeComponent();
        }


        //other
        private void ResetForm(string strKeep, bool blnEnable)
        {
            /*
              Created 28/02/2025 By Roger Williams

             Resets form 
             Enables/Disables form
             Undoes dataset changes

             VARS

             strKeep     - control to leave
             blnEnable   - enable or disable form

            */

            //reset form
            Modules.clsView.ResetForm(this, strKeep);
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            strItemID = string.Empty;
            this.BTNFind.Enabled = true;

            //clear datagrids
            if (this.DSTLoc != null)
            {
                this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].Rows.Clear();
            }
            if (this.DSTLot != null)
            {
                this.DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows.Clear();
            }
        }


        //data

        private List<string> ValidateAllLocationQtys()
        {
            /*
              Created 28/02/2025 By Roger Williams

              Compares edited Location qty with total Lot qtys (if any)

              Returns list of mismatched Locations null if ok

            */

            List<string> lstTemp = new List<string> { "root" };
            int intNum = 0;
            int intQtyLoc = 0;
            int intQtyLotOld = 0;
            int intQtyLotEdit = 0;
            DataTable tblTempLoc;
            DataTable tblTempLotEdit;
            DataTable tblTempLotOld;
            DataRow[] aryLotsEdit = null;
            DataRow[] aryLotsOld = null;
            bool blnLocLot = Modules.clsData.CheckForLotTracking(this.CMBSTKI_ItemID.Text);


            //only one Location record per item ID
            intQtyLoc = Convert.ToInt16(this.DGVLocations.CurrentRow.Cells[1].Value.ToString());
            //reset Lot qty var
            intQtyLotOld = 0;
            intQtyLotEdit = 0;

            /*
                Check Lots (Loc/Lot tracked)

                Conumdrum:

                Need to get total of all NON edited Lots AND edited Lots!


            */

            if (blnLocLot)
            {
                //get total edited qty
                tblTempLotEdit = this.DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].GetChanges();

                if (tblTempLotEdit != null)
                {
                    //get Lot qtys
                    foreach (DataRow DARLot in tblTempLotEdit.Rows)
                    {
                        intQtyLotEdit += Convert.ToInt16(DARLot["Qty"]);
                    }
                }
                //get total unedited qty making sure to ignore Lot_IDs unedited
                tblTempLotOld = this.DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT];

                if (tblTempLotOld != null)
                {

                    //get Lot qtys
                    foreach (DataRow DARLot in tblTempLotOld.Rows)
                    {
                        if (tblTempLotEdit.Select("[Lot Nbr] = " + DARLot["Lot Nbr"]) is null)
                        {
                            intQtyLotOld += Convert.ToInt16(DARLot["Qty"]);
                        }
                    }
                }

                if (intQtyLotEdit + intQtyLotOld != intQtyLoc)
                {
                    lstTemp.Add(this.DGVLocations.CurrentRow.Cells[0].Value.ToString());
                }
            }



            return lstTemp;
        }



        private void GetStockDescription()
        {
            /*
                 Created 27/02/2025 By Roger Williams

                 Reads description record from Stock_Description

            */
            SqlDataReader SQLRead;
            SqlCommand SQLCmdDesc;

            SQLCmdDesc = new SqlCommand("SELECT STKD_Desc FROM " + Modules.clsTables.CNST_STR_STOCK_DESCRIPTION + " WHERE STKD_ItemID = '" + this.CMBSTKI_ItemID.Text + "';", SQLConn);
            SQLRead = SQLCmdDesc.ExecuteReader();

            //load from dataset
            if (SQLRead.HasRows)
            {
                SQLRead.Read();
                this.LBLSTKD_Desc.Text = SQLRead["STKD_Desc"].ToString();
            }
            else
            {
                this.LBLSTKD_Desc.Text = string.Empty; ;
            }

            SQLRead.Close();
        }
        private void SaveRecord()
        {
            /*
              Created 28/02/2025 By Roger Williams

              - Saves record to STOCK_LOC and STOCK_LOT
              - Clears form 
              - Clears dataset
              - Does not allow save if total Lot qtys not same as Loc qty
              - Iterates through datasets changed records and writes Loc TRN for each entry and updates the Loc records
              - Looks for Lot for each Location changed (matching item ID if Loc/Lot tracking on) if found updates qty else creates new Lot 
              - Writes TRN records for Loc/Lot actions

            */

            SqlTransaction SQLTransaction = null;
            //data table vars
            SqlDataAdapter DADLot;
            SqlParameter SQLParam;
            int intLotNbr = 0;
            DataTable TBLChangesLoc = this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].GetChanges();
            DataTable TBLChangesLot = this.DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].GetChanges();
            bool blnLocLot = Modules.clsData.CheckForLotTracking(this.CMBSTKI_ItemID.Text);
            List<string> lstErrors;
            string strTemp = string.Empty; ;
            int intNum = 0;
            bool blnLotCreated = false;

            //validate loc/lot quantites match IF some lots to check!
            if (this.DGVLots.Rows.Count != 0)
            {
                //validate each Location WITH a Lot has Lot(s) EQUAl to the Location qty
                lstErrors = ValidateAllLocationQtys();
                //skip root list element
                if (lstErrors.Count > 1)
                {
                    for (intNum = 1; intNum != lstErrors.Count; intNum++)
                    {
                        strTemp += lstErrors[intNum] + "\n";
                    }

                    MessageBox.Show("Lot Quantities Do Not Match Location Quantities\n\n" +
                                    "Errors In These Locations:\n\n" + strTemp, "Quantity Mismatch Cannot Save!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            //check required fields completed
            if (Modules.clsView.ValidateRequiredFields(this))
            {
                try
                {
                    //start transaction
                    SQLTransaction = SQLConn.BeginTransaction();
                    SQLCmd.Transaction = SQLTransaction;

                    //see if and Lots to update as Loc/Lot tracked item
                    if (blnLocLot)
                    {
                        //     if (TBLChangesLot is null)
                        //if loc/lot tracking ON but NO lots create one for location/qty
                        if (intSelectedLoc != -1)
                        //      if (this.DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows.Count == 0)
                        {
                            //update Location create Lots where necessary
                            //       foreach (DataRow DTRTemp in TBLChangesLoc.Rows)
                            //       {
                            //create new STOCK_LOT record using stored procedure then get @@IDENTITY -> new Lot nbr
                            SQLCmd.CommandType = CommandType.StoredProcedure;
                            SQLCmd.CommandText = "sp_CreateNewLot";
                            //@itemID varchar(50), @qty int, @LotNbr int output
                            SQLCmd.Parameters.Add("@itemID", SqlDbType.VarChar, 50).Value = this.CMBSTKI_ItemID.Text;
                            SQLCmd.Parameters.Add("@Location", SqlDbType.VarChar, 30).Value = this.DGVLocations.Rows[intSelectedLoc].Cells[0].Value;   // DTRTemp["Location"];
                            SQLCmd.Parameters.Add("@qty", SqlDbType.Int, 0).Value = Convert.ToInt32(this.DGVLocations.Rows[intSelectedLoc].Cells[1].Value);  //DTRTemp["Qty"];
                            SQLCmd.Parameters.Add("@LotNbr", SqlDbType.Int).Direction = ParameterDirection.Output;
                            SQLCmd.Parameters.Add("@ErrorCustom", SqlDbType.Int).Direction = ParameterDirection.Output;
                            SQLCmd.ExecuteNonQuery();

                            if (Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value) == 0)
                            {
                                //create TRN Lot
                                SQLCmd.CommandType = CommandType.Text;
                                SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOTTRN + " (LotT_ItemID, LotT_Nbr, LotT_Qty, LotT_Location, LotT_Operation) " +
                                                      "VALUES ('" + this.CMBSTKI_ItemID.Text + "'," + SQLCmd.Parameters["@LotNbr"].Value + "," + this.DGVLocations.Rows[intSelectedLoc].Cells[1].Value + ",'" + this.DGVLocations.Rows[intSelectedLoc].Cells[0].Value + "','" + Modules.clsData.CNST_STR_OPERATION_LOTCREATED + "');";

                                SQLCmd.ExecuteNonQuery();
                                blnLotCreated = true;
                            }
                            else
                            {
                                //show error to user
                                Modules.clsData.dicSQLErrors.TryGetValue(Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value), out strTemp);
                                strTemp = SQLCmd.Parameters["@ErrorCustom"].Value.ToString() + "\n\n" + strTemp;
                                MessageBox.Show("Error: \n\n" + strTemp);
                            }
                            //      }
                        }
                        else
                        {
                            //check if any changes
                            if (TBLChangesLoc is null)
                            {
                                return;
                            }

                            //update each Lot for each modified Lot
                            foreach (DataRow DTRTemp in TBLChangesLot.Rows)
                            {
                                //create TRN Lot
                                SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOTTRN + " (LotT_ItemID, LotT_Nbr, LotT_Qty, LotT_Location, LotT_Operation) " +
                                                      "VALUES ('" + this.CMBSTKI_ItemID.Text + "'," + DTRTemp["Lot Nbr"] + "," + DTRTemp["Qty"] + ",'" + DTRTemp["Location"] + "','" + Modules.clsData.CNST_STR_OPERATION_ADJUST + "');";
                                SQLCmd.ExecuteNonQuery();

                                //update existing Lot
                                SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_STOCK_LOT + " SET Lot_Qty = " + DTRTemp["Qty"] + " WHERE Lot_ID = " + DTRTemp["Lot Nbr"] + ";";
                                //write Lot record
                                SQLCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    if (TBLChangesLoc != null)
                    { 
                        //update Location
                        foreach (DataRow DTRTemp in TBLChangesLoc.Rows)
                        {
                            //if creating lot for new location
                            if (blnLotCreated == false)
                            {
                                //create TRN Loc
                                SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOCTRN + " (LocT_ItemID, LocT_Location, LocT_Qty, LocT_Operation) " +
                                                        "VALUES ('" + this.CMBSTKI_ItemID.Text + "','" + DTRTemp["Location"] + "'," + DTRTemp["Qty"] + ",'" + Modules.clsData.CNST_STR_OPERATION_ADJUST + "');";

                            }
                            else
                            {
                                //create TRN Loc for
                                SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOCTRN + " (LocT_ItemID, LocT_Location, LocT_Qty, LocT_Operation) " +
                                                        "VALUES ('" + this.CMBSTKI_ItemID.Text + "','" + DTRTemp["Location"] + "'," + DTRTemp["Qty"] + ",'" + Modules.clsData.CNST_STR_OPERATION_LOTCREATED + "');";
                            }

                            SQLCmd.ExecuteNonQuery();

                            //update existing Loc
                            SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_STOCK_LOC + " SET Loc_Qty = " + DTRTemp["Qty"] + " WHERE Loc_ItemID = '" + this.CMBSTKI_ItemID.Text +
                                             "' AND Loc_Location = '" + DTRTemp["Location"] + "';";
                            //write Loc record
                            SQLCmd.ExecuteNonQuery();
                        }
                    }

                    //commit changes
                    SQLTransaction.Commit();

                    //reset form
                    ResetForm("", false);
                    Modules.clsData.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);
                    MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Saving Data:\n\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (SQLTransaction != null)
                    {
                        SQLTransaction.Rollback();
                    }
                }
            }
        }

        private void LoadRecord()
        {
            /*
              Created 28/02/2025 By Roger Williams

              - Populates the form
              - Enables the form
              - Binds the gridview to STOCK_LOC records for item
             
            */

            blnLoading = true;

            try
            {
                //get Location data
                this.DSTLoc = new DataSet();
                this.SQLCmd = new SqlCommand("SELECT Loc_Location AS Location, Loc_Qty AS Qty, Loc_Description AS [Desc], Loc_NonNet AS [Non Net] FROM " + Modules.clsTables.CNST_STR_STOCK_LOC + " WHERE Loc_ItemID = '" + this.CMBSTKI_ItemID.Text + "' ORDER BY Loc_Location;", SQLConn);
                this.DADLoc = new SqlDataAdapter(SQLCmd);
                this.DADLoc.Fill(this.DSTLoc, Modules.clsTables.CNST_STR_STOCK_LOC);
                //set datagrid data
                this.DGVLocations.DataSource = this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC];

                //enable form
                Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                //make certain columns readonly
                this.DGVLocations.Columns[0].ReadOnly = true;
                this.DGVLocations.Columns[2].ReadOnly = true;
                this.DGVLocations.Columns[3].ReadOnly = true;
                //set readonly columns colour
                this.DGVLocations.Columns[0].DefaultCellStyle.BackColor = Modules.clsView.CNST_INT_READONLYCOLUMNSCOLOUR;
                //set this columns header to NOT readonly colour
                this.DGVLocations.Columns[1].HeaderCell.Style.BackColor = Modules.clsView.CNST_INT_NORMALCOLUMNSCOLOUR;

                this.DGVLocations.Columns[2].DefaultCellStyle.BackColor = Modules.clsView.CNST_INT_READONLYCOLUMNSCOLOUR;
                this.DGVLocations.Columns[3].DefaultCellStyle.BackColor = Modules.clsView.CNST_INT_READONLYCOLUMNSCOLOUR;
                GetStockDescription();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Data:\n\n" + ex.Message, "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            blnLoading = false;
        }

        private void LoadLotsIntoDataGrid()
        {
            /*
              Created 28/02/2025 By Roger Williams
                   
              Populates Lot gridview with Lots for Location

            */

            this.SQLCmd = new SqlCommand("SELECT Lot_Location AS Location, Lot_ID AS [Lot Nbr], Lot_Qty AS Qty FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE Lot_ItemID = '" + this.CMBSTKI_ItemID.Text + "' AND Lot_Location = '" + this.DGVLocations.CurrentRow.Cells[0].Value.ToString() + "' ORDER BY Lot_ID;", SQLConn);
            this.DADLoc = new SqlDataAdapter(SQLCmd);
            this.DSTLot = new DataSet();
            this.DADLoc.Fill(this.DSTLot, Modules.clsTables.CNST_STR_STOCK_LOT);

            this.DGVLots.DataSource = "";

            //set datagrid data
           // if (this.DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows.Count == 0)
           // {
           ////     MessageBox.Show("No Lot Records Found For The Location", "No Lot Records", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           // //}
            //else
            //{
                this.DGVLots.DataSource = this.DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT];
                //make column1 (Location) readonly
                this.DGVLots.Columns[0].ReadOnly = true;
                //set readonly column colour
                this.DGVLots.Columns[0].DefaultCellStyle.BackColor = Modules.clsView.CNST_INT_READONLYCOLUMNSCOLOUR;
                //make column 2 (Lot nbr) readonly
                this.DGVLots.Columns[1].ReadOnly = true;
                //set readonly column colour
                this.DGVLots.Columns[1].DefaultCellStyle.BackColor = Modules.clsView.CNST_INT_READONLYCOLUMNSCOLOUR;
                //set this columns header to NOT readonly colour
                this.DGVLots.Columns[2].HeaderCell.Style.BackColor = Modules.clsView.CNST_INT_NORMALCOLUMNSCOLOUR;
         //   }
        }

        private void InitData()
        {
            /*
              Created 27/02/2025 By Roger Williams
                   
         
              - Clears form controls

     
            */

            blnLoading = true;
            this.SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);

            try
            {
                //SQLConn and DSTLoc stay open for duration of appplication run 
                SQLConn.Open();
                //populate combbox with available item ids
                Modules.clsData.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);

                blnShow = this.CMBSTKI_ItemID.Items.Count != 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Data:\n\n" + ex.Message, "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetForm(CNST_STR_FIRSTCONTROL, false);
            blnLoading = false;
        }

        private void frmAdjustQuantity_Load(object sender, EventArgs e)
        {
            /*
                 Created 28/02/2025 By Roger Williams

                 - Set pen for paint event
                 - Init SQL connection

            */
            if (Modules.clsData.CheckForStockItems())
            {
                penTemp = new Pen(Color.Black, 1);
                penTemp.Color = Color.Black;
                ResetForm("", false);
                InitData();
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
            else
            {
                blnShow = false;
            }
        }

        private void CMBSTKI_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams

                   Load record from selection
            */

            strItemID = this.CMBSTKI_ItemID.Text;


            if (this.CMBSTKI_ItemID.SelectedIndex != -1)
            {
                //if stock items available
                if (this.CMBSTKI_ItemID.Items.Count > 0)
                {
                    if (!blnLoading)
                    {
                        //if no records load!
                        LoadRecord();
                    }
                }
            }
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams

                   Reject changes

            */
            this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].RejectChanges();
            this.DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].RejectChanges();
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


            */
            try
            {
                SQLConn.Close();
            }
            catch (Exception ex)
            {
                //ignore any errors as closing
            }
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void frmAdjustQuantity_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              Draws line across screen

            */



            //draw lines
            e.Graphics.DrawLine(penTemp, 0, 60, this.Width, 60);
            e.Graphics.DrawLine(penTemp, 0, 268, this.Width, 268);
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


            */
            SaveRecord();

        }

        private void DGVLocations_Click(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams

                   - Checks if used changed lot record before switching to a diffrent one

            */
            if (this.DGVLots.Rows.Count != 0)
            {
                if (this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].GetChanges() != null)
                {
                    //make sure if user REALLY changing rows
                    if (intDVGLocIndex != this.DGVLocations.CurrentRow.Index)
                    {
                        if (MessageBox.Show("Changes To Lots Have Been Made, Lose Changes?", "Changes Made", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            this.DGVLocations.Rows[this.DGVLocations.CurrentRow.Index - 1].Selected = true;
                            this.DGVLocations.CurrentCell = this.DGVLocations.Rows[this.DGVLocations.CurrentRow.Index - 1].Cells[0];
                            intDVGLocIndex = this.DGVLocations.CurrentRow.Index;
                        }
                    }
                }
                else
                {
                    LoadLotsIntoDataGrid();
                    intDVGLocIndex = this.DGVLocations.CurrentRow.Index;
                }
            }
            else
            {
                LoadLotsIntoDataGrid();
                intDVGLocIndex = this.DGVLocations.CurrentRow.Index;
            }
        }

        private void CMBSTKI_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams

                   If typed value in list load reocrd

            */
            if (!blnOk)
            {
                if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
                {
                    strItemID = this.CMBSTKI_ItemID.Text;
                    this.TXTHidden.Text = strItemID;

                    //if stock items available
                    if (!blnLoading)
                    {
                        //if no records load!
                        LoadRecord();
                    }
                }
            }
        }

        private void frmAdjustQuantity_Shown(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams

                   If not stock records close form

            */
            if (blnShow == false)
            {
                SQLConn.Close();
                MessageBox.Show("Cannot Continue No Stock Items Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void DGVLocations_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            /*
               Created 20/08/2025 By Roger Williams

               makes sure editable columns are a differemt colour


            */

            if (e.ColumnIndex == 1)
            {
                e.CellStyle.BackColor = Color.DarkBlue;
            }
        }

        private void DGVLots_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            /*
               Created 20/08/2025 By Roger Williams

               makes sure editable columns are a differemt colour


            */

            if (e.ColumnIndex == 2)
            {
                e.CellStyle.BackColor = Color.DarkBlue;
            }
        }

        private void DGVLocations_DoubleClick(object sender, EventArgs e)
        {
  
            if (this.DGVLots.Rows.Count == 0)
            {
                MessageBox.Show("No Lot Records Found For The Location\n\nClick 'Save' Button To Create One For Selected Location", "No Lot Records", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DGVLocations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            intSelectedLoc = e.RowIndex;
        }
    }
}
