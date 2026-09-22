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
    public partial class frmUOMMassReplace: Form
    {
        /*
           
          Created 07/03/2025 By Roger Williams

          Uses disconnected combobox for location
          Allows user to replace one UOM with another IF new name is exists

          Note: Due to the nature of this function no undo is available!

        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBUOM_DescFrom";

        //data table vars
        bool blnLoading = false;
        bool blnShow = true;
        Pen penTemp;

        //for manual mouse move of form
        private bool blnDragging = false;
        private Point pntLastLocation;

        bool blnOk = false;

        public frmUOMMassReplace()
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

              - Renames UOM from too to
              - Clears form 

            */

            SqlConnection SQLConn;
            SqlTransaction SQLTransaction = null;
            SqlCommand SQLCmd;

            if (this.CMBUOM_DescFrom.Text.Length == 0)
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
                        if (this.CMBUOM_DescTo.Text.Length == 0 || this.CMBUOM_DescTo.Text == this.CMBUOM_DescFrom.Text)
                        {
                            MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + " SET STKI_UOM = '" + this.CMBUOM_DescTo.Text + "' WHERE STKI_UOM = '" + this.CMBUOM_DescFrom.Text + "';";
                        SQLCmd.ExecuteNonQuery();

                        //write data
                        SQLTransaction.Commit();

                        //reset form
                        ResetForm("", false);
                        Modules.clsView.PopulateComboBoxes(this.CMBUOM_DescFrom, Modules.clsTables.CNST_STR_TABLE_STOCK_UOM, "", "", "", "", "", false, false);
                        Modules.clsView.PopulateComboBoxes(this.CMBUOM_DescTo, Modules.clsTables.CNST_STR_TABLE_STOCK_UOM, "", "", "", "", "", false, false);
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

        private void frmUOMMassReplace_Load(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   - Sets pen for pain event
                   - checks if any UOM records
                   - populates comboboxes
            */
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;

            if (Modules.clsData.CheckForUOM() == false)
            {
                blnShow = false;
            }
            else
            {
                Modules.clsView.PopulateComboBoxes(this.CMBUOM_DescFrom, Modules.clsTables.CNST_STR_TABLE_STOCK_UOM, "", "", "", "", "", false, false);
                Modules.clsView.PopulateComboBoxes(this.CMBUOM_DescTo, Modules.clsTables.CNST_STR_TABLE_STOCK_UOM, "", "", "", "", "", false, false);
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //set form title
                Modules.clsView.SetFormTitle(this);
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
        }



        private void CMBUOM_DescFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   Enables form after selection

            */
            blnOk = true;

            if (this.CMBUOM_DescFrom.SelectedIndex != -1)
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }
        }

        private void CMBUOM_DescFrom_Leave(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   If typed value exists in list load
            */
            if (Modules.clsView.ValueInCombobox(this.CMBUOM_DescFrom, this.CMBUOM_DescFrom.Text))
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }

            blnOk = false;
        }

        private void CMBUOM_DescTo_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   enable form after selection

            */
            blnOk = true;

            if (this.CMBUOM_DescTo.SelectedIndex != -1)
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }
        }

        private void CMBUOM_DescTo_Leave(object sender, EventArgs e)
        {

            /*
                   Created 25/02/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBUOM_DescTo, this.CMBUOM_DescTo.Text))
            {
                if (!blnLoading)
                {
                    Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                }
            }

            blnOk = false;
        }

        private void frmUOMMassReplace_Shown(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   If blnShow false close form

            */
            if (blnShow == false)
            {
                MessageBox.Show("Cannot Continue No UOM Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void frmUOMMassReplace_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              Draws line across screen

            */


            //draw lines
            e.Graphics.DrawLine(penTemp, 0, 134, this.Width, 134);
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
