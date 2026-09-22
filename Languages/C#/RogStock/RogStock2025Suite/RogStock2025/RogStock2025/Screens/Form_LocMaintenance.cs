using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq.Expressions;

namespace RogStock2025.Screens
{
    public partial class frmLocMaintenance : Form
    {
        /*
         Modified 07/03/2025 By Roger Williams

         Changed to allow desc modification ONLY if item loc/lot tracked

           
          Created 25/02/2025 By Roger Williams

          Uses disconnected recordset to load ONE record into the form 

          User can create new record/edit/delete as only affects dataset

          Dataset changes are saved to the database

          Stores item id in hidden control TXTHidden so item id combobox can't be 
          used to rename items! 

          Uses:

           Bindingsource
           Dataset
           DataAdapter (to load record into dataset)
           SQl Command to load/save data

           Note: this form ONLY allows changes to location qtys where the stock item
                 does NOT have loc/lot tracking! 

        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBLOC_ItemID";

        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd;
        SqlDataAdapter DADLoc;
        DataSet DSTLoc;
        BindingSource BNSLoc = new BindingSource();
        bool blnNew = false;
        bool blnLoading = false;
        bool blnOk = false;
        bool blnShow = true;
        Pen penTemp;


        //holds current item id used if record deleted
        string strItemID = string.Empty;
        //holds current location used if renaming an existing location
        string strLocation = string.Empty;

        //find form
        frmFind frmTemp = new frmFind(Modules.clsTables.CNST_STR_FINDLOC);

        public frmLocMaintenance()
        {
            InitializeComponent();
        }

        //other
        private void ResetForm(string strKeep, bool blnEnable)
        {
            /*
              Created 25/02/2025 By Roger Williams

             Resets form 
             Enables/Disables form
             Undoes dataset changes

            VARS

            strKeep     - control to leave
            blnEnable   - enable or disable form

            */

            //undo changes
            if (this.DSTLoc != null)
            {
                try
                {
                    if (this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].GetChanges() != null)
                    {
                        this.BNSLoc.CancelEdit();
                    }
                }
                catch (Exception ex)
                {
                    //should never get here!
                }
            }

            //reset form
            Modules.clsView.ResetForm(this, strKeep);
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            blnNew = false;
            strItemID = string.Empty;
            this.BTNFind.Enabled = true;

            if (blnShow)
            {
                this.NUDLOC_Qty.Enabled = true;
            }
            else
            {
                this.NUDLOC_Qty.Enabled = false;
            }
        }

        //data
        private void DeleteRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Deletes current record using a transaction
              - clears form 
              - clears dataset

              Deletes the location AND if lot tracked deletes all lots
              Writes TRN records for the operation 

            */
            DataSet DSTLot;
            SqlDataAdapter DADLot;
            bool blnLocLot = Modules.clsData.CheckForLotTracking(this.CMBLOC_ItemID.Text);
            SqlTransaction SQLTransaction = null;
            SqlConnection SQLConnLot;

            if (this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].Rows.Count == 0)
            {
                return;
            }

            try
            {
                //start transaction
                SQLTransaction = SQLConn.BeginTransaction();
                SQLCmd.Transaction = SQLTransaction;
                //delete from database
                SQLCmd.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_LOC + " WHERE LOC_ItemID = '" + this.CMBLOC_ItemID.Text + "' AND LOC_Location = '" + this.CMBLOC_Location.Text + "';";
                SQLCmd.ExecuteNonQuery();
                //write TRN
                SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOCTRN + " (LOCT_ItemID, LOCT_Location, LOCT_Qty, LOCT_Operation) " +
                                        "VALUES ('" + this.CMBLOC_ItemID.Text + "','" + this.CMBLOC_Location.Text + "'," + this.NUDLOC_Qty.Value + ",'" + Modules.clsData.CNST_STR_OPERATION_DELETE + "');";
                SQLCmd.ExecuteNonQuery();

                //check for loc/lot tracking
                if (blnLocLot)
                {
                    //get lot rows into dstlot
                    using (SQLConnLot = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                    {
                        DADLot = new SqlDataAdapter("SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE LOT_ItemID = '" + this.CMBLOC_ItemID.Text + "' AND LOT_Location = '" + this.CMBLOC_Location.Text + "';", SQLConnLot);
                        DSTLot = new DataSet();
                        DADLot.Fill(DSTLot, Modules.clsTables.CNST_STR_STOCK_LOT);

                        //get lot info write TRN delete
                        foreach (DataRow DTRTemp in DSTLot.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows)
                        {
                            //create TRN lot
                            SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOTTRN + " (LOTT_ItemID, LOTT_Nbr, LOTT_Qty, LOTT_Location, LOTT_Operation) " +
                                                "VALUES ('" + this.CMBLOC_ItemID.Text + "'," + DTRTemp["LOT_ID"] + "," + DTRTemp["LOT_Qty"] + ",'" + DTRTemp["LOT_Location"] + "','" + Modules.clsData.CNST_STR_OPERATION_DELETE + "');";
                            //write LOT TRN record
                            SQLCmd.ExecuteNonQuery();

                            //delete existing lot
                            SQLCmd.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + "  WHERE LOT_ID = " + DTRTemp["LOT_ID"] + ";";
                            //write LOT record
                            SQLCmd.ExecuteNonQuery();
                        }
                    }
                }

                SQLTransaction.Commit();

                BNSLoc.RemoveCurrent();
                //reset form
                ResetForm("", false);
                Modules.clsData.PopulateComboBoxes(this.CMBLOC_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "WHERE STKI_LocLot = 0", false);
                Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "LOC_ItemID", this.CMBLOC_ItemID.Text, "", "", "", false);
                MessageBox.Show("Record Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                SQLTransaction.Rollback();
                MessageBox.Show("Error Deleting Data\n\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Saves record to stock_loc
              - clears form 
              - clears dataset
              - write loc TRN

              If loc/lot tracked create new lot record in stock_lot writes TRN

            */

            int intBoolean = 0;
            SqlTransaction SQLTransaction = null;
            SqlParameter SQLParam;
            bool blnLocLot = Modules.clsData.CheckForLotTracking(this.CMBLOC_ItemID.Text);
            string strTemp = String.Empty;

            void CreateLot()
            {
                /*
                  Created 29/02/2025 By Roger Williams

                  if loc/lot tracked and no lots this creates one for the selected
                  location/qty

                */

                //loc/lot tracked create new lot record and write TRN
                if (blnLocLot)
                {
                    //create new stock_Lot record using stored procedure then get @@IDENTITY -> new lot nbr
                    SQLCmd.CommandType = CommandType.StoredProcedure;
                    SQLCmd.CommandText = "sp_CreateNewLot";
                    //@itemID varchar(50), @qty int, @lotNbr int output
                    SQLCmd.Parameters.Add("@itemID", SqlDbType.VarChar, 50).Value = this.CMBLOC_ItemID.Text;
                    SQLCmd.Parameters.Add("@location", SqlDbType.VarChar, 30).Value = this.CMBLOC_Location.Text;
                    SQLCmd.Parameters.Add("@qty", SqlDbType.Int, 0).Value = this.NUDLOC_Qty.Value;
                    SQLCmd.Parameters.Add("@LotNbr", SqlDbType.Int).Direction = ParameterDirection.Output;
                    SQLCmd.Parameters.Add("@ErrorCustom", SqlDbType.Int).Direction = ParameterDirection.Output;
                    SQLCmd.ExecuteScalar();

                    if (Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value) == 0)
                    {
                        //write LOT TRN record
                        SQLCmd.CommandType = CommandType.Text;
                        SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOTTRN + " (LOTT_ItemID, LOTT_Nbr, LOTT_Location, LOTT_Qty, LOTT_Operation) " +
                                             "VALUES ('" + this.CMBLOC_ItemID.Text + "'," + SQLCmd.Parameters["@lotNbr"].Value + ",'" + this.CMBLOC_Location.Text + "'," + this.NUDLOC_Qty.Value + ",'" + Modules.clsData.CNST_STR_OPERATION_LOTCREATED + "');";
                        SQLCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        //show error to user
                        Modules.clsData.dicSQLErrors.TryGetValue(Convert.ToInt32(SQLCmd.Parameters["@ErrorCustom"].Value), out strTemp);
                        strTemp = SQLCmd.Parameters["@ErrorCustom"].Value.ToString() + "\n\n" + strTemp;
                        MessageBox.Show("Error: \n\n" + strTemp);
                    }
                }
            }


            //commit changes to dataset
            this.BNSLoc.EndEdit();

            if (this.DSTLoc != null)
            {
                if (this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].Rows.Count == 0)
                {
                    return;
                }

                if (this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].Rows.Count == 0 && !blnNew)
                {
                    return;
                }
            }

            //check required fields completed
            if (Modules.clsView.ValidateRequiredFields(this))
            {
                try
                {
                    intBoolean = this.CHK_LOC_NonNet.Checked == true ? -1 : 0;

                    //start transaction
                    SQLTransaction = SQLConn.BeginTransaction();
                    SQLCmd.Transaction = SQLTransaction;

                    //save data to database
                    if (blnNew)
                    {
                        if (Modules.clsData.CheckLocationExists(this.CMBLOC_Location.Text))
                        {
                            MessageBox.Show("Cannot Add " + this.CMBLOC_Location.Text + " As It Already Exists!", "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_STOCK_LOC + " (LOC_ItemID, LOC_Location, LOC_Qty, LOC_Description, LOC_NonNet) " +
                                                 "VALUES ('" + this.CMBLOC_ItemID.Text + "','" + this.CMBLOC_Location.Text + "'," + this.NUDLOC_Qty.Value + ",'" + this.TXTLOC_Desc.Text + "'," + intBoolean + ");";
                            SQLCmd.ExecuteNonQuery();

                            //create TRN loc
                            SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOCTRN + " (LOCT_ItemID, LOCT_Location, LOCT_Qty, LOCT_Operation) " +
                                                    "VALUES ('" + this.CMBLOC_ItemID.Text + "','" + this.CMBLOC_Location.Text + "'," + this.NUDLOC_Qty.Value + ",'" + Modules.clsData.CNST_STR_OPERATION_CREATE + "');";
                            SQLCmd.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        //if existing record check something to save!
                        if (this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].GetChanges() != null)
                        {
                        //    MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}

                            SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_STOCK_LOC + " SET LOC_Qty = " + this.NUDLOC_Qty.Value + ", LOC_Description = '" + this.TXTLOC_Desc.Text + "', LOC_NonNet = " + intBoolean + " WHERE LOC_ItemID = '" + this.CMBLOC_ItemID.Text +
                                                 "' AND LOC_Location = '" + strLocation + "';";
                            SQLCmd.ExecuteNonQuery();

                            //create TRN loc
                            SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOCTRN + " (LOCT_ItemID, LOCT_Location, LOCT_Qty, LOCT_Operation) " +
                                                    "VALUES ('" + this.CMBLOC_ItemID.Text + "','" + this.CMBLOC_Location.Text + "'," + this.NUDLOC_Qty.Value + ",'" + Modules.clsData.CNST_STR_OPERATION_ADJUST + "');";
                            SQLCmd.ExecuteNonQuery();
                        }
                   
                    }

                    if (Modules.clsData.CheckLotsForLocation(this.CMBLOC_Location.Text) == false)
                    {
                        //create lot if required
                        CreateLot();
                    }

                    //write data
                    SQLTransaction.Commit();

                    //clear form bindings
                    this.BNSLoc.RemoveCurrent();
                    
                    //reset form
                    ResetForm("", false);
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "LOC_ItemID", this.CMBLOC_ItemID.Text, "", "", "", false);
                    MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    SQLTransaction.Rollback();
                    MessageBox.Show("Error Saving Data:\n\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BindForm()
        {
            /*
              Created 25/02/2025 By Roger Williams

              binds form to table: 
            
              Stock_Loc

            */
            if (this.BNSLoc.Count > 0)
            {
                this.BNSLoc = new BindingSource();
            }

            this.BNSLoc.DataSource = this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC];

            //clear bindings
            this.LBLSTKD_Desc.DataBindings.Clear();
            this.TXTHidden.DataBindings.Clear();
            this.TXTLOC_Desc.DataBindings.Clear();
            this.CMBLOC_Location.DataBindings.Clear();
            this.CHK_LOC_NonNet.DataBindings.Clear();
            this.NUDLOC_Qty.DataBindings.Clear();

            //bind form controls to stock_loc
            this.TXTHidden.DataBindings.Add("text", this.BNSLoc, "LOC_ItemID", true, DataSourceUpdateMode.OnPropertyChanged);
            this.TXTLOC_Desc.DataBindings.Add("text", this.BNSLoc, "LOC_Description", true, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBLOC_Location.DataBindings.Add("text", this.BNSLoc, "LOC_Location", true, DataSourceUpdateMode.OnPropertyChanged);
            this.CHK_LOC_NonNet.DataBindings.Add("checked", this.BNSLoc, "LOC_NonNet", false, DataSourceUpdateMode.OnPropertyChanged);
            this.NUDLOC_Qty.DataBindings.Add("text", this.BNSLoc, "LOC_Qty", false, DataSourceUpdateMode.OnPropertyChanged);
        }
        private void LoadRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Populates the form
              - Enables the form
              - Binds the form to table fields

              Why?

              If data is changed elsewhere searching through a disconnected dataset for data could find the user
              editing a record someone else has deleted!

              This approach of load ONE record into the dataset means each loads reads current data
             
            */

            blnLoading = true;

            try
            {
                //get location data
                this.DSTLoc = new DataSet();
                this.SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOC + " WHERE LOC_ItemID = '" + this.CMBLOC_ItemID.Text + "' AND LOC_Location ='" + this.CMBLOC_Location.Text + "' ORDER BY LOC_Location;";
                this.DADLoc = new SqlDataAdapter(SQLCmd);
                this.DADLoc.Fill(this.DSTLoc, Modules.clsTables.CNST_STR_STOCK_LOC);

                if (this.TXTHidden.Text.Length == 0)
                {
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "", "", "", "", "", false);
                }
                else
                {
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "LOC_ItemID", this.TXTHidden.Text, "", "", "", false);
                    this.CMBLOC_ItemID.Text = this.TXTHidden.Text;
                }

                BindForm();
                GetStockDescription();

                //enable form
                Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);

                //check if new record
                if (this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].Rows.Count == 0)
                {
                    if (MessageBox.Show("No Records Found Create New Record?", "No Matching Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.BTNNew_Click(this, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Data:\n\n" + ex.Message, "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            blnShow = Modules.clsData.CheckLotsForLocation(this.CMBLOC_Location.Text);

            if (blnShow == false)
            {
                this.NUDLOC_Qty.Enabled = true;
            }
            else
            {
                this.NUDLOC_Qty.Enabled = false;
            }

            blnLoading = false;
        }



        private bool InitData()
        {
            /*
              Created 27/02/2025 By Roger Williams

              - Fills DSTLoc with records in:
                - Stock_Loc

              Fills item ID combobox with locations

            */

            blnLoading = true;
            this.SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);

            try
            {
                //SQLConn and DSTLoc stay open for duration of application run 
                SQLConn.Open();
                //get location data
                this.DSTLoc = new DataSet();
                this.SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_STOCK_LOC, SQLConn);
                this.DADLoc = new SqlDataAdapter(SQLCmd);
                this.DADLoc.Fill(this.DSTLoc, Modules.clsTables.CNST_STR_STOCK_LOC);
                //populate combbox with available item ids
                Modules.clsData.PopulateComboBoxes(this.CMBLOC_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);  // "WHERE STKI_LocLot = 0");

                blnShow = this.CMBLOC_ItemID.Items.Count != 0;
                BindForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Data:\n\n" + ex.Message, "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            ResetForm(CNST_STR_FIRSTCONTROL, false);
            blnLoading = false;
            return true;
        }

        private void GetStockDescription()
        {
            /*
                 Created 27/02/2025 By Roger Williams

                 Reads description record from Stock_Description

            */
            SqlDataReader SQLRead;
            SqlCommand SQLCmdDesc;

            SQLCmdDesc = new SqlCommand("SELECT STKD_Desc FROM " + Modules.clsTables.CNST_STR_STOCK_DESCRIPTION + " WHERE STKD_ItemID = '" + this.CMBLOC_ItemID.Text + "';", SQLConn);
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



        //form events
        private void Form_LocMaintenance_Load(object sender, EventArgs e)
        {
            /*
                 Created 25/02/2025 By Roger Williams

                 - Sets pen for paint event
                 - resset form

            */
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;
            ResetForm("", false);
            this.LocationChanged += Modules.clsView.FormLocationChanged;
            //apply system theme
            Modules.clsView.SetTheme(this, null);

            if (Modules.clsData.CheckForStockItems())
            {
                if (InitData() == false)
                {
                    blnShow = false;
                }
                else
                {
                    this.BTNNew.Enabled = false;
                }
            }
            else
            {
                blnShow = false;
            }
        }

        private void Form_LocMaintenance_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              Draws line across screen
            
            */

            //draw lines
            e.Graphics.DrawLine(penTemp, 0, 70, this.Width, 70);
            e.Graphics.DrawLine(penTemp, 0, 210, this.Width, 210);
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

            
            */
            try
            {
                DSTLoc.Clear();
                SQLConn.Close();
            }
            catch (Exception ex)
            {
            }
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

            
            */
            SaveRecord();
        }


        private void BTNNew_Click(object sender, EventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

            
            */
            //Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
            ResetForm(CNST_STR_FIRSTCONTROL, true);
            //add new blank record
            this.BNSLoc.AddNew();
            blnNew = true;
            //set qty to 0
            this.NUDLOC_Qty.Value = 0;
            GetStockDescription();
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

            
            */
            //undo changes
            if (this.DSTLoc.Tables[Modules.clsTables.CNST_STR_STOCK_LOC].GetChanges() != null)
            {
                if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.BNSLoc.CancelEdit();
                    blnNew = false;
                    strItemID = string.Empty; ;
                }
            }
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

            
            */
            string strMessage = string.Empty; ;
            DialogResult resChoice;

            if (blnNew)
            {
                //if new record just undo
                BTNUndo_Click(sender, e);
            }
            else
            {
                if (Modules.clsData.CheckForLotTracking(this.CMBLOC_ItemID.Text))
                {
                    strMessage = "DeleteLocation?\n\nThis Location Is Location and Lot Tracked\n\nDeleting This Item Will Delete ALL Associated Lots\n\nAre You Sure?";
                }
                else
                {
                    strMessage = "DeleteLocation?";
                }

                if (MessageBox.Show(strMessage, "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    DeleteRecord();
                }
            }

            blnNew = false;
        }

        private void CMBLOC_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


            */
            blnOk = true;
            strItemID = this.CMBLOC_ItemID.Text;

            if (blnNew)
            {
                //if new record do nothing
                return;
            }

            this.CMBLOC_Location.Enabled = false;
            this.BTNNew.Enabled = false;

            if (this.CMBLOC_ItemID.SelectedIndex != -1)
            {
                //if stock items available
                if (this.CMBLOC_ItemID.Items.Count > 0)
                {
                    //enable location combobox
                    this.CMBLOC_Location.Enabled = true;
                    this.CMBLOC_Location.Text = String.Empty;
                    //populate locations for item list
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "LOC_ItemID", this.CMBLOC_ItemID.Text, "", "", "", false);
                    this.BTNNew.Enabled = true;
                }
            }
        }
        private void CMBLOC_Location_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   load record

            */
            blnOk = true;

            strLocation = this.CMBLOC_Location.Text;

            if (blnNew)
            {
                //if new record do nothing
                return;
            }

            if (this.CMBLOC_Location.SelectedIndex != -1)
            {
                if (!blnLoading)
                {
                    //if records load!
                    LoadRecord();
                }
            }
        }

        private void CMBLOC_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                 Created 17/02/2025 By Roger Williams

                 Checks typed value in list
            
            */
            if (!blnOk)
            {
                if (Modules.clsView.ValueInCombobox(this.CMBLOC_ItemID, this.CMBLOC_ItemID.Text))
                {
                    if (!blnLoading)
                    {
                        strItemID = this.CMBLOC_ItemID.Text;
                        this.TXTHidden.Text = strItemID;
                        //enable location combobox
                        this.CMBLOC_Location.Enabled = true;
                        //populate locations for item list
                        this.CMBLOC_Location.Text = String.Empty;
                        Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "LOC_ItemID", this.CMBLOC_ItemID.Text, "", "", "", false);
                        this.BTNNew.Enabled = true;
                    }
                }
            }

            blnOk = false;
        }

        private void CMBLOC_Location_Leave(object sender, EventArgs e)
        {
            /*
                 Created 17/02/2025 By Roger Williams

                 Checks typed value in list
            
            */
            strLocation = this.CMBLOC_Location.Text;

            if (blnNew)
            {
                //if new record do nothing
                return;
            }

            if (Modules.clsView.ValueInCombobox(this.CMBLOC_Location, this.CMBLOC_Location.Text))
            {
                if (!blnLoading)
                {
                    //if records load!
                    LoadRecord();
                }
            }

            blnOk = false;
        }

        private void frmLocMaintenance_Shown(object sender, EventArgs e)
        {
            /*
                 Created 17/02/2025 By Roger Williams

                 If no stock items close form
            
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
                this.CMBLOC_ItemID.Text = Modules.clsData.objFindSelected.ToString();
            }
        }
    }
}
