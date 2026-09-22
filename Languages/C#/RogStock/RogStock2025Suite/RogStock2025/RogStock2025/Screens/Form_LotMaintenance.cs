using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    public partial class frmLotMaintenance : Form
    {

        /*
           
          Created 27/02/2025 By Roger Williams

          Uses disconnected recordset to load ONE record into the form 

          User can edit/delete record as only affects dataset

          Note: user cannot create new lot records here that is done automatically
                via adjust quantity screen

          Dataset changes are saved to the database

          Stores item id in hidden control TXTHidden so item id combobox can't be 
          used to rename items! 

          Uses:

           Bindingsource
           Dataset
           DataAdapter (to load record into dataset)
           SQl Command to load/save data

        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBLOT_ItemID";

        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd;
        SqlDataAdapter DADLOT;
        DataSet DSTLOT;
        BindingSource BNSLOT = new BindingSource();
        bool blnLoading = false;
        bool blnOk = false;
        bool blnShow = true;
        Pen penTemp;

        //find form
        frmFind frmTemp = new frmFind(Modules.clsTables.CNST_STR_FINDLOT);

        //holds current item id used if record deleted
        string strItemID = string.Empty;

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
            if (this.DSTLOT != null)
            {
                if (this.DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].GetChanges() != null)
                {
                    this.BNSLOT.CancelEdit();
                }
            }
            //reset form
            Modules.clsView.ResetForm(this, strKeep);
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            strItemID = string.Empty;
            this.BTNFind.Enabled = true;
        }

        private void ActivateLocationComboBox()
        {
            /*
                     Created 27/02/2025 By Roger Williams

                     - Populate combobox
                     - Get item description

             */
            strItemID = this.CMBLOT_ItemID.Text;
            this.CMBLOT_Nbr.Enabled = false;
            this.CMBLOT_Location.Enabled = false;

            //enable location combobox
            this.CMBLOT_Location.Enabled = true;
            this.CMBLOT_Location.Text = String.Empty;
            //populate locations for item list
            Modules.clsData.PopulateComboBoxes(this.CMBLOT_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "LOC_ItemID", this.CMBLOT_ItemID.Text, "", "", "", false);
            //populate locations for item list
            GetStockDescription();
        }

        //data
        private void GetStockDescription()
        {
            /*
                 Created 27/02/2025 By Roger Williams

                 Reads description record from Stock_Description

            */
            SqlDataReader SQLRead;
            SqlCommand SQLCmdDesc;

            SQLCmdDesc = new SqlCommand("SELECT STKD_Desc FROM " + Modules.clsTables.CNST_STR_STOCK_DESCRIPTION + " WHERE STKD_ItemID = '" + this.CMBLOT_ItemID.Text + "'", SQLConn);
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
        private void DeleteRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Deletes current record using a transaction
              - Clears form 

              - Writes TRN record
              - BEFORE doing anything checks if ONLY lot for item/loc if so does NOT allow delete as this would break loc/lot tracking!
              - Else if deleting the lot brings the lot quantity BELOW the loc qty stop processing

            */

            SqlTransaction SQLTransaction = null;
            SqlDataReader SQLRead;
            SqlCommand SQLCmdLot;
            int intQty = 0;

            if (this.DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows.Count == 0)
            {
                return;
            }

            if (this.DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows.Count == 1)
            {
                MessageBox.Show("Cannot Delete As Is ONLY Lot For Item\n\nDeleting Breaks Location/Lot Tracking Cannot Continue", "Must Have One Lot Per Item ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //check if lot qtys WITHOUT lot to delete still = loc qty
            SQLCmdLot = new SqlCommand("SELECT LOT_Qty FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE LOT_ItemID = '" + this.CMBLOT_ItemID.Text + "' AND LOT_Location = '" +
                                       this.CMBLOT_Location.Text + "' AND LOT_ID IS NOT " + this.CMBLOT_Nbr.Text + ";", SQLConn);
            SQLRead = SQLCmdLot.ExecuteReader();

            //load from dataset
            if (SQLRead.HasRows)
            {
                intQty += Convert.ToInt16(SQLRead["LOT_Qty"]);
            }

            SQLRead.Close();

            if (intQty != Convert.ToInt16(this.LBLLocationQty.Text))
            {
                MessageBox.Show("Cannot Delete As Doing So Causes A Location/Lot Mismatch\n\nTOTAL Quantity In Lots Is Less Than The Location Quantity Of: " + this.LBLLocationQty.Text, "Total Lot Quantities Must Match Location Quantity!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                SQLTransaction = SQLConn.BeginTransaction();
                SQLCmd.Transaction = SQLTransaction;
                SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOTTRN + " (LOTT_ItemID, LOTT_Nbr, LOTT_Qty, LOTT_Location, LOTT_Operation) " +
                                     "VALUES ('" + this.CMBLOT_ItemID.Text + "'," + this.CMBLOT_Nbr.Text + "," + this.NUDLOT_Qty.Value + ",'" + this.CMBLOT_Location.Text + "','" + Modules.clsData.CNST_STR_OPERATION_DELETE + "');";
                //write LOT TRN record
                SQLCmd.ExecuteNonQuery();
                //delete lot
                SQLCmd.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE LOT_ItemID = '" + this.CMBLOT_ItemID.Text + "' AND LOT_ID = " + this.CMBLOT_Nbr.Text + ";";
                SQLCmd.ExecuteNonQuery();
                //save changes
                SQLTransaction.Commit();
                //clear dataset record
                BNSLOT.RemoveCurrent();
                //reset form
                ResetForm("", false);
                Modules.clsData.PopulateComboBoxes(this.CMBLOT_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);
                Modules.clsData.PopulateComboBoxes(this.CMBLOT_ItemID, Modules.clsTables.CNST_STR_STOCK_LOT, "", "", "", "", "", false);
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

              - Saves record to stock_lot
              - clears form 
              - clears dataset
              - writes TRN

            */

            SqlTransaction SQLTransaction = null;
            int intBoolean = 0;

            //commit changes to dataset
            this.BNSLOT.EndEdit();

            if (this.DSTLOT != null)
            {
                if (this.DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows.Count == 0)
                {
                    return;
                }
            }

            //check lot qty total matches location qty

            if (Modules.clsData.ValidateLocationQtys(this.CMBLOT_ItemID.Text, this.CMBLOT_Location.Text) == false)
            {
                MessageBox.Show("Lot Quantities Do Not Match Location Quantities", "Mismatch Cannot Save!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //check required fields completed
            if (Modules.clsView.ValidateRequiredFields(this))
            {
                try
                {
                    intBoolean = this.CHK_LOT_NonNet.Checked == true ? -1 : 0;

                    //if existing record check something to save!
                    if (this.DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].GetChanges() == null)
                    {
                        MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //start transaction
                    SQLTransaction = SQLConn.BeginTransaction();
                    SQLCmd.Transaction = SQLTransaction;
                    //update LOT record
                    SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_STOCK_LOT + " SET LOT_Qty = " + this.NUDLOT_Qty.Value + ", LOT_NonNet = " + intBoolean + " WHERE LOT_ID = " + this.CMBLOT_Nbr.Text + ";";
                    SQLCmd.ExecuteNonQuery();
                    //write LOT TRN record
                    SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOTTRN + " (LOTT_ItemID, LOTT_Nbr, LOTT_Qty, LOTT_Location, LOTT_Operation) " +
                                         "VALUES ('" + this.CMBLOT_ItemID.Text + "'," + this.CMBLOT_Nbr.Text + "," + this.NUDLOT_Qty.Value + ",'" + this.CMBLOT_Location.Text + "','" + Modules.clsData.CNST_STR_OPERATION_ADJUST + "');";
                    SQLCmd.ExecuteNonQuery();
                    //commit changes
                    SQLTransaction.Commit();
                    //clear form dataset
                    this.BNSLOT.RemoveCurrent();

                    //reset form
                    ResetForm("", false);
                    Modules.clsData.PopulateComboBoxes(this.CMBLOT_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);
                    // Modules.clsData.PopulateComboBoxes(this.CMBLOT_Nbr, Modules.clsData.CNST_STR_STOCK_LOT, "", "", "", "");
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

        private void BindForm()
        {
            /*
              Created 25/02/2025 By Roger Williams

              binds form to table: 
            
              Stock_LOT

            */

            if (this.BNSLOT.Count > 0)
            {
                this.BNSLOT = new BindingSource();
            }

            this.BNSLOT.DataSource = this.DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT];

            //clear bindings
            this.TXTHidden.DataBindings.Clear();
            this.CHK_LOT_NonNet.DataBindings.Clear();
            this.NUDLOT_Qty.DataBindings.Clear();

            //bind form controls to stock_LOT
            this.TXTHidden.DataBindings.Add("text", this.BNSLOT, "LOT_ItemID", true, DataSourceUpdateMode.OnPropertyChanged);
            this.CHK_LOT_NonNet.DataBindings.Add("checked", this.BNSLOT, "LOT_NonNet", false, DataSourceUpdateMode.OnPropertyChanged);
            this.NUDLOT_Qty.DataBindings.Add("text", this.BNSLOT, "LOT_Qty", false, DataSourceUpdateMode.OnPropertyChanged);
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

              This apporach of load ONE record into the dataset means each loads reads current data
             
            */

            blnLoading = true;
            DataTable tblLot;
            SqlDataReader SQLReadLoc;
            int intLotQty = 0;

            try
            {
                //get LOTation data
                this.DSTLOT = new DataSet();
                this.SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOT + " WHERE LOT_ID ='" + this.CMBLOT_Nbr.Text + "';";
                this.DADLOT = new SqlDataAdapter(SQLCmd);
                this.DADLOT.Fill(this.DSTLOT, Modules.clsTables.CNST_STR_STOCK_LOT);

                //get location qty and total lot qtys
                this.SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_LOC + " WHERE LOC_ItemID ='" + this.CMBLOT_ItemID.Text + "' AND LOC_Location ='" + this.CMBLOT_Location.Text + "';";
                SQLReadLoc = SQLCmd.ExecuteReader();

                if (SQLReadLoc.Read())
                {
                    this.LBLLocationQty.Text = "Total Quantity In Location: " + SQLReadLoc["LOC_Qty"].ToString();
                }

                SQLReadLoc.Close();

                tblLot = DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT];

                foreach (DataRow DARTemp in tblLot.Rows)
                {
                    intLotQty += Convert.ToInt16(DARTemp["LOT_Qty"]);
                }

                this.LBLTotalLotQtys.Text = "Total Quantity In Lots: " + intLotQty.ToString();
                this.LBLTimeDate.Text = DateTime.Now.ToString();

                blnShow = this.CMBLOT_ItemID.Items.Count != 0;
                BindForm();

                //enable form
                Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);

                //check if new record
                if (this.DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows.Count == 0)
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

            blnLoading = false;
        }



        private void InitData()
        {
            /*
              Created 27/02/2025 By Roger Williams

              - Fills DSTLOT with records in:
                - Stock_LOT
      
              - Binds form controls
              - Clears form controls

              Saves faffing about creating the bindingsource if user creates new record without loading anything first
              as no controls would be bound!

            */

            blnLoading = true;
            this.SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);

            try
            {
                //SQLConn and DSTLOT stay open for duration of appplication run 
                SQLConn.Open();
                //get LOTation data
                this.DSTLOT = new DataSet();
                this.SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_STOCK_LOT, SQLConn);
                this.DADLOT = new SqlDataAdapter(SQLCmd);
                this.DADLOT.Fill(this.DSTLOT, Modules.clsTables.CNST_STR_STOCK_LOT);
                //populate combbox with available item ids
                Modules.clsData.PopulateComboBoxes(this.CMBLOT_ItemID, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);

                blnShow = this.CMBLOT_ItemID.Items.Count != 0;
                BindForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Data:\n\n" + ex.Message, "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetForm(CNST_STR_FIRSTCONTROL, false);
            blnLoading = false;
        }

        private void ClearLotControls() 
        {
            /*
                     Created 02/09/2025 By Roger Williams

                     clears lot controls

             */
            this.BNSLOT.CancelEdit();
            this.NUDLOT_Qty.Value = 0;
            this.CMBLOT_Nbr.Text = String.Empty;
            this.CHK_LOT_NonNet.Checked = false;
        }

        //form events
        public frmLotMaintenance()
        {
            InitializeComponent();
        }

        private void frmLotMaintenance_Load(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams

                     - Sets pen for paint event
                     - Inits SQL connection
                     - Populates combobox

             */
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;

            if (Modules.clsData.CheckForStockItems())
            {
                if (Modules.clsData.CheckForLots())
                {
                    ResetForm("", false);
                    InitData();
                    this.LocationChanged += Modules.clsView.FormLocationChanged;
                    //apply system theme
                    Modules.clsView.SetTheme(this, null);
                }
                else
                {
                    //no lots for location!
                    MessageBox.Show("No Lots Records Available - Cannot Continue!", "No Records Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
            }
            else
            {
                blnShow = false;
            }
        }

        private void frmLotMaintenance_Paint(object sender, PaintEventArgs e)
        {
            /*
                Created 25/02/2025 By Roger Williams

                Draws line across screen

              */

            //draw lines
            e.Graphics.DrawLine(penTemp, 0, 60, this.Width, 60);
            e.Graphics.DrawLine(penTemp, 0, 200, this.Width, 200);
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams


             */
            try
            {
                DSTLOT.Clear();
                SQLConn.Close();
            }
            catch (Exception ex)
            {
                //ignore any errors as closing
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
            ResetForm(CNST_STR_FIRSTCONTROL, true);
            //add new blank record
            this.BNSLOT.AddNew();
            //set qty to 0
            this.NUDLOT_Qty.Value = 0;
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams


             */

            //undo changes
            if (this.DSTLOT.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].GetChanges() != null)
            {
                if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.BNSLOT.CancelEdit();
                    strItemID = string.Empty; ;
                }
            }
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams


             */

            if (MessageBox.Show("Delete Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                DeleteRecord();
            }
        }

        private void CMBLOT_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


            */
            blnOk = true;
            ActivateLocationComboBox();
        }

        private void CMBLOT_Nbr_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


            */
            blnOk = true;

            if (this.CMBLOT_Nbr.SelectedIndex != -1)
            {
                if (!blnLoading)
                {
                    //if no records load!
                    LoadRecord();
                }
            }
        }

        private void CMBLOT_Location_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams

                     - Populate combobox
                     - Check for lots
             */

            blnOk = true;
            //clear lot controls
            ClearLotControls();
            this.CMBLOT_Nbr.Text = String.Empty;
            Modules.clsData.PopulateComboBoxes(this.CMBLOT_Nbr, Modules.clsTables.CNST_STR_STOCK_LOT, "LOT_Location", this.CMBLOT_Location.Text, "", "", "", false);

            if (this.CMBLOT_Nbr.Items.Count == 0)
            {
                //no lots for location!
                MessageBox.Show("No Lots Available For Location: " + this.CMBLOT_Location.Text, "No Records Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //enable lot number combobox
                this.CMBLOT_Nbr.Enabled = true;
            }
        }

        private void CMBLOT_Location_Leave(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams

                     If typed value exists in list check for lots
             */

            if (!blnOk)
            {
                if (Modules.clsView.ValueInCombobox(this.CMBLOT_Location, this.CMBLOT_Location.Text))
                {
                    this.CMBLOT_Nbr.Text = String.Empty;
                    Modules.clsData.PopulateComboBoxes(this.CMBLOT_Nbr, Modules.clsTables.CNST_STR_STOCK_LOT, "LOT_Location", this.CMBLOT_Location.Text, "", "", "", false);

                    if (this.CMBLOT_Nbr.Items.Count == 0)
                    {
                        //no lots for location!
                        MessageBox.Show("No Lots Available For Location: " + this.CMBLOT_Location.Text, "No Records Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        //enable lot number combobox
                        this.CMBLOT_Nbr.Enabled = true;
                    }
                }
            }

            blnOk = false;
        }

        private void CMBLOT_Nbr_Leave(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams

                     Check typed value exists in list

             */

            if (!blnOk)
            {
                Modules.clsView.ValueInCombobox(this.CMBLOT_Nbr, this.CMBLOT_Nbr.Text);
            }

            blnOk = false;
        }

        private void CMBLOT_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams

                     Check typed value exists in list

             */
            if (!blnOk)
            {
                if (Modules.clsView.ValueInCombobox(this.CMBLOT_ItemID, this.CMBLOT_ItemID.Text) == false)
                {
                    this.CMBLOT_ItemID.Text = strItemID;
                }
                else
                {
                    this.TXTHidden.Text = this.CMBLOT_ItemID.Text;
                    ActivateLocationComboBox();
                }
            }

            blnOk = false;
        }

        private void frmLotMaintenance_Shown(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams

                     If no lots rcords close form

             */
            if (blnShow == false)
            {
                SQLConn.Close();
                MessageBox.Show("Cannot Continue No Lot Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void BTNFind_Click(object sender, EventArgs e)
        {
            frmTemp.ShowDialog();

            if (Modules.clsData.objFindSelected != null)
            {
                this.CMBLOT_ItemID.Text = Modules.clsData.objFindSelected.ToString();
            }
        }

    }
}
