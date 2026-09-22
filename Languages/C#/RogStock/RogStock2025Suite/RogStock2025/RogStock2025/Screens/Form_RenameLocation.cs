using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    public partial class frmRenameLocation: Form
    {
        /*
           
          Created 07/03/2025 By Roger Williams

          Uses disconnected combobox for location
          Allows user to rename locations IF new name is unique
          If loc/lot tracked renames locations in lots

          Note: Due to the nature of this function no undo is available!

        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBLOC_Location";

        //data table vars
        bool blnLoading = false;
        bool blnOk = false;
        bool blnShow = true;
        Pen penTemp;

        public frmRenameLocation()
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


            VARS

            strKeep     - control to leave
            blnEnable   - enable or disable form

            */

            //reset form
            Modules.clsView.ResetForm(this, strKeep);
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
        }

        //data



        private void SaveRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Renames location from too to
              - Clears form 
              - Write loc TRN -> item ID = old location qty as is
              - If loc/lot tracked create new writes TRN for each lot 

            */

            int intBoolean = 0;
            SqlConnection SQLConn;
            SqlTransaction SQLTransaction = null;
            SqlCommand SQLCmd;
            SqlDataAdapter DADTemp;
            DataSet DSTTemp;
            bool blnLocLot = Modules.clsData.CheckLotsForLocation(this.CMBLOC_Location.Text);

            if (this.CMBLOC_Location.Text.Length == 0)
            {
                return;
            }

            //check required fields completed
            if (Modules.clsView.ValidateRequiredFields(this))
            {
                try
                {
                    using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                    {
                        SQLConn.Open();
                        //start transaction
                        SQLTransaction = SQLConn.BeginTransaction();
                        SQLCmd = new SqlCommand();
                        SQLCmd.Connection = SQLConn;
                        SQLCmd.Transaction = SQLTransaction;

                        //save data to database
                        //if existing record check something to save!
                        if (this.TXTLocTo.Text.Length == 0 || this.TXTLocTo.Text == this.CMBLOC_Location.Text)
                        {
                            MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_STOCK_LOC + " SET LOC_Location = '" + this.TXTLocTo.Text + "' WHERE LOC_Location = '" + this.CMBLOC_Location.Text + "';";
                        SQLCmd.ExecuteNonQuery();

                        //create TRN loc for old location NULL for item ID as multiple items can use same location       (LOCT_ItemID,
                        SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOCTRN + " (LOCT_Location, LOCT_OldLocation LOCT_Qty, LOCT_Operation) " +
                                                "VALUES ('" + this.TXTLocTo.Text + "','" + this.CMBLOC_Location.Text + "',0,'" + Modules.clsData.CNST_STR_OPERATION_RENAME + "');";
                        SQLCmd.ExecuteNonQuery();

                        if (blnLocLot)
                        {

                            SQLCmd.CommandText = "SELECT * FROM Stock_Lot WHERE LOT_Location ='" + this.CMBLOC_Location.Text + "';";
                            DADTemp = new SqlDataAdapter(SQLCmd);
                            DSTTemp = new DataSet();
                            DADTemp.Fill(DSTTemp, Modules.clsTables.CNST_STR_STOCK_LOT);

                            foreach (DataRow DARTemp in DSTTemp.Tables[Modules.clsTables.CNST_STR_STOCK_LOT].Rows)
                            {
                                //write LOT TRN record
                                SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_LOTTRN + " (LOTT_ItemID, LOTT_Nbr, LOTT_Qty, LOTT_Location, LOTT_Operation) " +
                                                     "VALUES ('" + DARTemp["LOT_ItemID"] + "'," + DARTemp["LOT_ID"] + "," + DARTemp["LOT_Qty"] + ",'" + this.CMBLOC_Location.Text + "','" + Modules.clsData.CNST_STR_OPERATION_RENAME + "');";
                                SQLCmd.ExecuteNonQuery();
                            }

                            //update LOT records
                            SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_STOCK_LOT + " SET LOT_Location = '" + this.TXTLocTo.Text + "' WHERE LOT_Location = '" + this.CMBLOC_Location.Text + "';";
                            SQLCmd.ExecuteNonQuery();
                        }


                        //write data
                        SQLTransaction.Commit();

                        //reset form
                        ResetForm("", false);
                        Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "", "", "", "", "", false);
                        MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                catch (Exception ex)
                {
                    SQLTransaction.Rollback();
                    MessageBox.Show("Error Saving Data:\n\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    
        //form events
        private void frmRenameLocation_Load(object sender, EventArgs e)
        {
            /*
                  Created 25/02/2025 By Roger Williams

                  - Sets pen for paint event
                  - Checks for location records
                  - Populates combobox

             */
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;

            if (Modules.clsData.CheckForLocations() == false)
            {
                blnShow = false;
            }
            else
            {
                Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "", "", "", "", "", false);
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
        }

        private void frmRenameLocation_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              Draws line across screen

            */

            //draw line
            e.Graphics.DrawLine(penTemp, 0, 80, this.Width, 80);
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                  Created 25/02/2025 By Roger Williams



             */
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

        private void CMBLOC_Location_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   load record

            */
            blnOk = true;

            if (this.CMBLOC_Location.SelectedIndex != -1)
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }
        }

        private void CMBLOC_Location_Leave(object sender, EventArgs e)
        {
            /*
                  Created 25/02/2025 By Roger Williams

                  Enable form if typed value in list

             */
            if (Modules.clsView.ValueInCombobox(this.CMBLOC_Location, this.CMBLOC_Location.Text))
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }

            blnOk = false;
        }

        private void frmRenameLocation_Shown(object sender, EventArgs e)
        {
            /*
                  Created 25/02/2025 By Roger Williams

                  If no location records close form

             */
            if (blnShow == false)
            {
                MessageBox.Show("Cannot Continue No Location Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void TXTLocTo_Leave(object sender, EventArgs e)
        {
            /*
                  Created 25/02/2025 By Roger Williams

                  Enable form if typed value in list

             */
            if (Modules.clsData.CheckLocationExists(this.TXTLocTo.Text))
            {
                this.TXTLocTo.Focus();
                MessageBox.Show("Location Already Exists", "Duplicate Location Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
