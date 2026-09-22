using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    public partial class frmUOMMaintenance: Form
    {
        /*
           
          Created 05/03/2025 By Roger Williams

          Uses one unbound combobox!

          Writes to: stock_UOM

          - Checks if user wants to delete if any stock items are using that UOM
          - Checks if new UOM if already exists
        

        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBSTKU_Desc";
        string strUOM = string.Empty;
        bool blnOk = false;
        bool blnNew = false;
        Pen penTemp;


        public frmUOMMaintenance()
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
            blnNew = false;
            strUOM = string.Empty;;
        }

        //data
        private void DeleteRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Deletes current record if UOM not used in stock items
              - Clears form 

            */

            SqlTransaction SQLTransaction = null;
            SqlConnection SQLConn;
            SqlCommand SQLCmd;

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    if (Modules.clsData.UOMUsed(this.CMBSTKU_Desc.Text))
                    {
                        MessageBox.Show("Cannot Delete UOM Used In Stock Items\n\nUse List Stock Items - Filter For This UOM To See Affected Items\n\nThen Use Stock Items Screen To Change Affected Items To Another UOM\n\nOr\n\nUse UOM Replace", "UOM In Use", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        SQLConn.Open();
                        //start transaction
                        SQLTransaction = SQLConn.BeginTransaction();
                        //delete from database
                        SQLCmd = new SqlCommand("DELETE FROM " + Modules.clsTables.CNST_STR_STOCK_UOM + " WHERE STKU_Desc = '" + this.CMBSTKU_Desc.Text + "';", SQLConn);
                        SQLCmd.Transaction = SQLTransaction;
                        SQLCmd.ExecuteNonQuery();
                        SQLTransaction.Commit();

                        //reset form
                        ResetForm("", false);
                        Modules.clsData.PopulateComboBoxes(this.CMBSTKU_Desc, Modules.clsTables.CNST_STR_STOCK_UOM, "STKU_Desc", this.CMBSTKU_Desc.Text, "", "", "", false);
                        MessageBox.Show("Record Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
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

              - Saves record to stock_UOM
              - clears form 

            */

            int intBoolean = 0;
            SqlTransaction SQLTransaction = null;
            SqlConnection SQLConn;
            SqlCommand SQLCmd;


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

                        //save data to database
                        if (blnNew)
                        {
                            if (Modules.clsData.CheckUOMExists(this.CMBSTKU_Desc.Text))
                            {
                                MessageBox.Show("Cannot Add " + this.CMBSTKU_Desc.Text + " As It Already Exists!", "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                SQLCmd = new SqlCommand("INSERT INTO " + Modules.clsTables.CNST_STR_STOCK_UOM + " (STKU_Desc) " +
                                             "VALUES ('" + this.CMBSTKU_Desc.Text + "');", SQLConn);
                                SQLCmd.Transaction = SQLTransaction;
                                SQLCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            //if existing record check something to save!
                            if (this.CMBSTKU_Desc.Text == strUOM)
                            {
                                MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            //if struom != cmbSTKU_Desc update ALL stock items using old UOM
                            if (this.CMBSTKU_Desc.Text != strUOM)
                            {

                                if (MessageBox.Show("Change UOM:\n\nFrom: " + this.CMBSTKU_Desc.Text + "\nTo\n" + strUOM + "\n\nFor ALL Stock Items?", "Change UOM For Stock Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    SQLCmd = new SqlCommand("UPDATE " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " SET STKI_UOM = '" + this.CMBSTKU_Desc.Text + "' " +
                                                     " WHERE STKI_UOM = '" + strUOM + "';", SQLConn);
                                    SQLCmd.Transaction = SQLTransaction;
                                    SQLCmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        //write data
                        SQLTransaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    SQLTransaction.Rollback();
                    MessageBox.Show("Error Saving Data\n\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                    //reset form
                    ResetForm("", false);
                    Modules.clsData.PopulateComboBoxes(this.CMBSTKU_Desc, Modules.clsTables.CNST_STR_STOCK_UOM, "", "", "", "","", false);
                    MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

       

        //form events

        private void Form_UOMMaintenance_Load(object sender, EventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams

              - Sets pen for paint event
              - Populates combobox 

            */
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;

            ResetForm(CNST_STR_FIRSTCONTROL, false);
            Modules.clsData.PopulateComboBoxes(this.CMBSTKU_Desc, Modules.clsTables.CNST_STR_STOCK_UOM, "", "", "", "", "", false);
            //apply system theme
            Modules.clsView.SetTheme(this, null);
        }

        private void frmUOMMaintenance_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams

              Draws line across screen

            */


            //draw line
            e.Graphics.DrawLine(penTemp, 0, 40, this.Width, 40);
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams

             

            */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams


            */
            SaveRecord();
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams


            */

            this.CMBSTKU_Desc.Text = string.Empty;;
            ResetForm(CNST_STR_FIRSTCONTROL, true);
            blnNew = true;
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams


            */

            //undo changes
            if (blnNew)
            {
                if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    blnNew = false;
                    this.CMBSTKU_Desc.Text = strUOM;
                    strUOM = string.Empty;;
                    Modules.clsData.PopulateComboBoxes(this.CMBSTKU_Desc, Modules.clsTables.CNST_STR_STOCK_ITEMS, "", "", "", "", "", false);
                }
            }
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams


            */

            if (blnNew)
            {
                //if new record just undo
                BTNUndo_Click(sender, e);
            }
            else
            {
                if (MessageBox.Show("Delete Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    DeleteRecord();
                }
            }

            blnNew = false;
        }

        private void CMBSTKU_Desc_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                  Created 05/03/2025 By Roger Williams

                  Enable form after selection

            */
            blnOk = true;
            strUOM = this.CMBSTKU_Desc.Text;
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
        }

        private void CMBSTKU_Desc_Leave(object sender, EventArgs e)
        {
            /*
                   Created 04/03/2025 By Roger Williams

                   If typed value in list enable form

            */
            //check if location already exists

            if (!blnOk)
            { 
                if (blnNew)
                {
                    if (this.CMBSTKU_Desc.Items.Contains(this.CMBSTKU_Desc.Text))
                    {
                        this.CMBSTKU_Desc.Text = string.Empty;;
                        this.CMBSTKU_Desc.Focus();
                        MessageBox.Show("Location Already In List", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    strUOM = this.CMBSTKU_Desc.Text;
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }

            blnOk = false;
        }
    }
}
