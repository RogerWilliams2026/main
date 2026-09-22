using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    public partial class frmProductFamilyMassReplace: Form
    {
        /*
           
          Created 05/03/2025 By Roger Williams

          Uses one unbound combobox!

          Writes to: stock_PRODUCTFAMILY

          - Checks if user wants to delete if any stock items are using that ProductFamily
          - Checks if new ProductFamily if already exists
        

        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBProdFamFrom";
        bool blnLoading = false;
        bool blnOk = false;
        bool blnShow = true;
        Pen penTemp;




        public frmProductFamilyMassReplace()
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

              - Renames ProductFamily from too to
              - Clears form 

            */

            int intBoolean = 0;
            SqlConnection SQLConn;
            SqlTransaction SQLTransaction = null;
            SqlCommand SQLCmd;
            SqlDataAdapter DADTemp;
            DataSet DSTTemp;

            if (this.CMBProdFamFrom.Text.Length == 0)
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
                        if (this.CMBProdFamTo.Text.Length == 0 || this.CMBProdFamTo.Text == this.CMBProdFamFrom.Text)
                        {
                            MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_STOCK_ITEMS + " SET STKI_ProductFamily = '" + this.CMBProdFamTo.Text + "' WHERE STKI_PRODUCTFAMILY = '" + this.CMBProdFamFrom.Text + "';";
                        SQLCmd.ExecuteNonQuery();

                        //write data
                        SQLTransaction.Commit();

                        //reset form
                        ResetForm("", false);
                        Modules.clsData.PopulateComboBoxes(this.CMBProdFamFrom, Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY, "", "", "", "", "", false);
                        Modules.clsData.PopulateComboBoxes(this.CMBProdFamTo, Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY, "", "", "", "", "", false);
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

        private void frmProductFamilyMassReplace_Load(object sender, EventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Sets pen for paint event
              - Clears form 
              - Populates comboboxes

            */
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;

            if (Modules.clsData.CheckForProductFamily() == false)
            {
                blnShow = false;
            }
            else
            {
                Modules.clsData.PopulateComboBoxes(this.CMBProdFamFrom, Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY, "", "", "", "", "", false);
                Modules.clsData.PopulateComboBoxes(this.CMBProdFamTo, Modules.clsTables.CNST_STR_STOCK_PRODUCTFAMILY, "", "", "", "", "", false);
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              Closes form

            */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              Closes form

            */
            SaveRecord();
        }

        private void frmProductFamilyMassReplace_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              Draws line across screen

            */


            //draw line
            e.Graphics.DrawLine(penTemp, 0, 100, this.Width, 100);
        }

        private void CMBProdFamFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   Enable form after selection

            */
            blnOk = true;

            if (this.CMBProdFamFrom.SelectedIndex != -1)
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }
        }

        private void CMBProdFamFrom_Leave(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBProdFamFrom, this.CMBProdFamFrom.Text))
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }

            blnOk = false;
        }

        private void CMBProdFamTo_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   Enable form after selection

            */
            blnOk = true;

            if (this.CMBProdFamTo.SelectedIndex != -1)
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }
        }

        private void CMBProdFamTo_Leave(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBProdFamTo, this.CMBProdFamTo.Text))
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }

            blnOk = false;
        }

        private void frmProductFamilyMassReplace_Shown(object sender, EventArgs e)
        {
            /*
                     Created 25/02/2025 By Roger Williams

                     If no lots rcords close form

             */
            if (blnShow == false)
            {
                MessageBox.Show("Cannot Continue No Product Family Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }
    }
}
