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

namespace RogStock2026.Screens
{
    public partial class frmUseStock : Form
    {
        /*

          Created 25/02/2025 By Roger Williams

          Consumes stock from tables:
        
          - Stock_Loc
          - Stocl_Lot
          - Writes TRN for adjustments

          Uses unbound comboboxes for itemID/loc

          Uses dataset bound datagrid for lots (if any)


        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBSTKI_ItemID";

        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd;
        SqlDataAdapter DADTemp;
        DataSet DSTTemp;
        bool blnNew = false;
        bool blnLoading = false;
        bool blnOk = false;
        bool blnShow = true;
        string strLocation = string.Empty;
        Pen penTemp;
        //holds current item ID used if record deleted
        string strItemID = string.Empty;

        //find form
        frmFind frmTemp = new frmFind(Modules.clsTables.CNST_STR_FINDSTOCKITEM);

        //for manual mouse move of form
        private bool blnDragging = false;
        private Point pntLastLocation;

        public frmUseStock()
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
             Clears dataset

            VARS

            strKeep     - control to leave
            blnEnable   - enable or disable form

            */

            //undo changes
            if (this.DSTTemp != null)
            {
                try
                {
                    if (this.DSTTemp.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].GetChanges() != null)
                    {
                        this.DSTTemp.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].Rows.Clear();
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
            strItemID = string.Empty; ;
            this.BTNFind.Enabled = true;
        }

        //data

        private bool ValidateLotQtys(DataTable tblLots)
        {
            /*
              Created 28/02/2025 By Roger Williams
                   
              Checks if total lots qtys in dstlot = location qty

            */
            int intQtyLot = 0;

            //get lot qtys
            foreach (DataRow DARLot in tblLots.Rows)
            {
                intQtyLot += Convert.ToInt16(DARLot["Qty"]);
            }

            if (intQtyLot != this.NUDLOC_Qty.Value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void LoadLotsIntoDataGrid()
        {
            /*
              Created 28/02/2025 By Roger Williams
                   
              Populates lot gridview with lots for location

            */

            if (this.CMBLOC_Location.Text.Length == 0)
            {
                return;
            }

            this.SQLCmd = new SqlCommand("SELECT LOT_ID AS [Lot Nbr], LOT_Qty AS Qty FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOT + " WHERE LOT_ItemID = '" + this.CMBSTKI_ItemID.Text + "' AND LOT_Location = '" + this.CMBLOC_Location.Text + "' ORDER BY LOT_ID;", SQLConn);
            this.DADTemp = new SqlDataAdapter(SQLCmd);
            this.DSTTemp = new DataSet();
            this.DADTemp.Fill(this.DSTTemp, Modules.clsTables.CNST_STR_TABLE_STOCK_LOT);

            //set datagrid data
            if (this.DSTTemp.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].Rows.Count == 0)
            {
                MessageBox.Show("No Lot Records Found For The Location", "No Lot Records", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.DGVLots.DataSource = "";
                this.DGVLots.DataSource = this.DSTTemp.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT];
                //make column 1 (lot nbr) readonly
                this.DGVLots.Columns[0].ReadOnly = true;
                //set readonly column colour
                this.DGVLots.Columns[0].DefaultCellStyle.BackColor = Modules.clsView.CNST_INT_READONLYCOLUMNSCOLOUR;
                //set this columns header to white a NOT readonly
                this.DGVLots.Columns[1].HeaderCell.Style.BackColor = Modules.clsView.CNST_INT_NORMALCOLUMNSCOLOUR;
            }
        }

        private void SaveRecord()
        {
            /*
              Created 07/03/2025 By Roger Williams

              - Checks loc/lot quantities match
              - clears form 
              - write loc TRN

              If loc/lot tracked create new lot record in stock_lot writes TRN

            */

            SqlTransaction SQLTransaction = null;
            bool blnLocLot = Modules.clsData.CheckForLotTracking(this.CMBSTKI_ItemID.Text);
            DataTable tblLots = this.DSTTemp.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].GetChanges();

            //Disabled - used to force lot has to exist before save!
            //if (this.DSTTemp != null)
            //{
            //    if (this.DSTTemp.Tables[Modules.clsData.CNST_STR_TABLE_STOCK_LOT].Rows.Count == 0)
            //    {
            //        return;
            //    }

            //    if (this.DSTTemp.Tables[Modules.clsData.CNST_STR_TABLE_STOCK_LOT].Rows.Count == 0 && !blnNew)
            //    {
            //        return;
            //    }
            //}

            //any lots?
            if (this.DSTTemp != null)
            {
                //if lots check qtys match loc qty
                if (this.DSTTemp.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].Rows.Count != 0)
                {
                    if (ValidateLotQtys(tblLots) == false)
                    {
                        MessageBox.Show("Lot Quantities Do Not Match Location Quantity!", "Mismatch Cannot Save!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
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

                    SQLCmd.ExecuteNonQuery();
                    SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOC + " SET LOC_Qty = " + this.NUDLOC_Qty.Value + " WHERE LOC_ItemID = '" + this.CMBSTKI_ItemID.Text + "' AND LOC_Location ='" + this.CMBLOC_Location.Text + "';";
                    SQLCmd.ExecuteNonQuery();

                    //create TRN loc
                    SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_TABLE_LOCTRN + " (LOCT_ItemID, LOCT_Location, LOCT_Qty, LOCT_Operation) " +
                                            "VALUES ('" + this.CMBSTKI_ItemID.Text + "','" + this.CMBLOC_Location.Text + "'," + this.NUDLOC_Qty.Value + ",'" + Modules.clsData.CNST_STR_OPERATION_ADJUST + "');";
                    SQLCmd.ExecuteNonQuery();

                    //any lots records to save?
                    if (tblLots != null)
                    {
                        foreach (DataRow DARTemp in tblLots.Rows)
                        {
                            //update LOT record
                            SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOT + " SET LOT_Qty = " + DARTemp["Qty"] + " WHERE LOT_ID = '" + DARTemp["Lot Nbr"] + "';";
                            SQLCmd.ExecuteNonQuery();
                            //write LOT TRN record
                            SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + " (LOTT_ItemID, LOTT_Nbr, LOTT_Qty, LOTT_Location, LOTT_Operation) " +
                                                 "VALUES ('" + this.CMBSTKI_ItemID.Text + "'," + DARTemp["Lot Nbr"] + "," + DARTemp["Qty"] + ",'" + this.CMBLOC_Location.Text + "','" + Modules.clsData.CNST_STR_OPERATION_ADJUST + "');";
                            SQLCmd.ExecuteNonQuery();
                        }
                    }
                    //write data
                    SQLTransaction.Commit();

                    //reset form
                    ResetForm("", false);
                    Modules.clsView.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS, "", "", "", "", "", false, false);
                    Modules.clsView.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC, "LOC_ItemID", this.CMBSTKI_ItemID.Text, "", "", "", false, false);
                    MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    SQLTransaction.Rollback();
                    MessageBox.Show("Error Saving Data:\n\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Populates the location combobox
              - Enables the form
              - Gets stock item description
              - Loads any available lots into a datagrid

             
            */

            blnLoading = true;
            this.NUDLOC_Qty.Value = 0;

            try
            {
                if (this.TXTHidden.Text.Length == 0)
                {
                    Modules.clsView.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC, "LOC_ItemID", this.CMBSTKI_ItemID.Text, "", "", "", false, false);
                }
                else
                {

                    this.CMBSTKI_ItemID.Text = this.TXTHidden.Text;
                }

                //get stock item description
                this.LBLItemDesc.Text = Modules.clsData.GetStockDescription(this.CMBSTKI_ItemID.Text);
                LoadLotsIntoDataGrid();

                //enable form
                Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);

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

              - Fills DSTTemp with records in:
                - Stock_Lot
              - Clears form controls


            */

            blnLoading = true;
            this.SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);

            try
            {
                //SQLConn and DSTTemp stay open for duration of appplication run 
                SQLConn.Open();
                SQLCmd = new SqlCommand();
                SQLCmd.Connection = SQLConn;
                //populate combbox with available item ids
                Modules.clsView.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS, "", "", "", "", "", false, false);

                blnShow = this.CMBSTKI_ItemID.Items.Count != 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Data:\n\n" + ex.Message, "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetForm(CNST_STR_FIRSTCONTROL, false);
            blnLoading = false;
        }

       
        //********form events

        private void frmUseStock_Load(object sender, EventArgs e)
        {
            /*
                Created 25/02/2025 By Roger Williams

                - Sets Pen for paint event
                - Makes sure stock items exist before continuing
                - Inits the form
               

              */
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;

            //any stock items in the database?
            if (Modules.clsData.CheckForStockItems())
            {
                ResetForm("", false);
                InitData();
                //set form move handler
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //set form captions
                Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS);
                //set form captions
                Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC);
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
            else
            {
                blnShow = false;
            }
        }


        private void BTNClose_Click(object sender, EventArgs e)
        {
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

        private void BTNSave_Click(object sender, EventArgs e)
        {
            /*
                   Created 19/02/2025 By Roger Williams


            */
            SaveRecord();
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
                   Created 19/02/2025 By Roger Williams


            */
            //undo changes
            if (this.DSTTemp.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].GetChanges() != null)
            {
                if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    blnNew = false;
                    strItemID = string.Empty; ;

                    if (this.DSTTemp.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].GetChanges() != null)
                    {
                        this.DSTTemp.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].RejectChanges();
                    }
                }
            }
        }

        private void CMBSTKI_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


            */
            blnOk = true;
            strItemID = this.CMBSTKI_ItemID.Text;

            if (blnNew)
            {
                //if new record do nothing
                return;
            }

            this.CMBLOC_Location.Enabled = false;

            if (this.CMBSTKI_ItemID.SelectedIndex != -1)
            {
                //if stock items available
                if (this.CMBSTKI_ItemID.Items.Count > 0)
                {
                    //enable location combobox
                    this.CMBLOC_Location.Enabled = true;
                    this.CMBLOC_Location.Text = String.Empty;
                    this.DGVLots.DataSource = "";
                    //populate locations for item list
                    Modules.clsView.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC, "LOC_ItemID", this.CMBSTKI_ItemID.Text, "", "", "", false, false);
                    //get stock item description
                    this.LBLItemDesc.Text = Modules.clsData.GetStockDescription(this.CMBSTKI_ItemID.Text);
                    LoadRecord();
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
                    LoadLotsIntoDataGrid();
                    //enable form
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                    this.NUDLOC_Qty.Value = Modules.clsData.GetLocationQty(this.CMBLOC_Location.Text);
                }
            }
        }

        private void CMBSTKI_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams


            */
            if (!blnOk)
            {
                if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
                {
                    if (!blnLoading)
                    {
                        strItemID = this.CMBSTKI_ItemID.Text;
                        this.TXTHidden.Text = strItemID;
                        //enable location combobox
                        this.CMBLOC_Location.Enabled = true;
                        this.CMBLOC_Location.Text = String.Empty;
                        this.DGVLots.DataSource = "";
                        //populate locations for item list
                        Modules.clsView.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC, "LOC_ItemID", this.CMBSTKI_ItemID.Text, "", "", "", false, false);
                        //get stock item description
                        this.LBLItemDesc.Text = Modules.clsData.GetStockDescription(this.CMBSTKI_ItemID.Text);
                        LoadRecord();
                    }
                }
            }
        }

        private void CMBLOC_Location_Leave(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams


            */
            if (!blnOk)
            {
                strLocation = this.CMBLOC_Location.Text;

                if (blnNew)
                {
                    //if new record do nothing
                    return;
                }

                //does typed value exist in the list?
                if (Modules.clsView.ValueInCombobox(this.CMBLOC_Location, this.CMBLOC_Location.Text))
                {
                    if (!blnLoading)
                    {
                        LoadLotsIntoDataGrid();
                        //enable form
                        Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                        this.NUDLOC_Qty.Value = Modules.clsData.GetLocationQty(this.CMBLOC_Location.Text);
                    }
                }
            }

            blnOk = false;
        }

        private void frmUseStock_Shown(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams


                   If no stock items halt form
            */
            if (blnShow == false)
            {
                SQLConn.Close();
                MessageBox.Show("Cannot Continue No Stock Item Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void DGVLots_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void frmUseStock_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              Draws lines across screen

            */

            //draw lines
            e.Graphics.DrawLine(penTemp, 0, 104, this.Width, 104);
            e.Graphics.DrawLine(penTemp, 0, 320, this.Width, 320);
            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
        }

        private void PANTitle_MouseDown(object sender, MouseEventArgs e)
        {
            blnDragging = true;
            pntLastLocation = e.Location;
        }

        private void PANTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (blnDragging)
            {
                this.Location = new Point(
                (this.Location.X - pntLastLocation.X) + e.X,
                (this.Location.Y - pntLastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void PANTitle_MouseUp(object sender, MouseEventArgs e)
        {
            blnDragging = false;
        }
    }
}