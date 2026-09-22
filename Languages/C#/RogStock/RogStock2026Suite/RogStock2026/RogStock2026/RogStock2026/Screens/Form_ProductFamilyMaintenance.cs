using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2026.Screens
{
    public partial class frmProductFamilyMaintenance : Form
    {
        /*
           
          Created 05/03/2025 By Roger Williams

          Uses one unbound combobox!

          Writes to: stock_product families

          - Checks if user wants to delete if any stock items are using that product family
          - Checks if new product family if already exists
        

        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBSTKP_ProductFamily";
        string strDesc = string.Empty;
        string strProductFamily = string.Empty;
        bool blnOk = false;
        bool blnNew = false;
        Pen penTemp;

        //for manual mouse move of form
        bool blnDragging = false;
        System.Drawing.Point pntLastLocation;


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
            strDesc = string.Empty;
        }

        //data
        private void DeleteRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Checks if product family in use by stock item
              - clears form 

            */

            SqlTransaction SQLTransaction = null;
            SqlConnection SQLConn;
            SqlCommand SQLCmd;

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    if (Modules.clsData.ProdFamUsed(this.CMBSTKP_ProductFamily.Text))
                    {
                        MessageBox.Show("Cannot Delete Product Family Used In Stock Items\n\nUse List Stock Items - Filter For This Product Family To See Affected Items\n\nThen Use Stock Items Screen To Change Affected Items To Another Product Family\n\nOr\n\nUse Product Family Replace", "Product Family In Use", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        SQLConn.Open();
                        //start transaction
                        SQLTransaction = SQLConn.BeginTransaction();
                        //delete from database
                        SQLCmd = new SqlCommand("DELETE FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY + " WHERE STKP_ProductFamily = '" + this.CMBSTKP_ProductFamily.Text + "';", SQLConn);
                        SQLCmd.Transaction = SQLTransaction;
                        SQLCmd.ExecuteNonQuery();
                        SQLTransaction.Commit();

                        //reset form
                        ResetForm("", false);
                        Modules.clsView.PopulateComboBoxes(this.CMBSTKP_ProductFamily, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY, "", "", "", "", "", false, false);
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

              - Saves record to stock_uo,
              - clears form 

            */

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
                            if (Modules.clsData.CheckProdFamExists(this.CMBSTKP_ProductFamily.Text))
                            {
                                MessageBox.Show("Cannot Add " + this.CMBSTKP_ProductFamily.Text + " As It Already Exists!", "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                SQLCmd = new SqlCommand("INSERT INTO " + Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY + " (STKP_ProductFamily, STKP_Desc) " +
                                             "VALUES ('" + this.CMBSTKP_ProductFamily.Text + "','" + this.TXTSTKP_Desc.Text + "');", SQLConn);
                                SQLCmd.Transaction = SQLTransaction;
                                SQLCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            //if existing record check something to save!
                            if (this.TXTSTKP_Desc.Text == strDesc)
                            {
                                MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            //update ONLY description
                            SQLCmd = new SqlCommand("UPDATE " + Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY + " SET STKP_Desc = '" + this.TXTSTKP_Desc.Text + "' " +
                                             " WHERE STKP_ProductFamily = '" + this.CMBSTKP_ProductFamily.Text + "';", SQLConn);
                            SQLCmd.Transaction = SQLTransaction;
                            SQLCmd.ExecuteNonQuery();
                            //write data
                            SQLTransaction.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    SQLTransaction.Rollback();
                    MessageBox.Show("Error Saving Data\n\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //reset form
                ResetForm("", false);
                Modules.clsView.PopulateComboBoxes(this.CMBSTKP_ProductFamily, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY, "", "", "", "", "", false, false);
                MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GetProducFamilyDescription()
        {
            /*
                 Created 27/02/2025 By Roger Williams

                 Reads description record from Stock_ProductFamily

            */
            SqlDataReader SQLRead;
            SqlCommand SQLCmdDesc;
            SqlConnection SQLConn;

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmdDesc = new SqlCommand("SELECT STKP_Desc FROM " + Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY + " WHERE STKP_ProductFamily= '" + this.CMBSTKP_ProductFamily.Text + "';", SQLConn);
                    SQLRead = SQLCmdDesc.ExecuteReader();

                    //load from dataset
                    if (SQLRead.HasRows)
                    {
                        SQLRead.Read();
                        this.TXTSTKP_Desc.Text = SQLRead["STKP_Desc"].ToString();
                    }
                    else
                    {
                        this.TXTSTKP_Desc.Text = string.Empty; ;
                    }

                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Accessing Data" + ex.Message, "Read Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //form events

        public frmProductFamilyMaintenance()
        {
            InitializeComponent();
        }

        private void frmProductFamilyMaintenance_Load(object sender, EventArgs e)
        {
            /*
               Created 05/03/2025 By Roger Williams

               - Set pen for paint event
               - Clear form
               - populate combobox


             */
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;
            ResetForm(CNST_STR_FIRSTCONTROL, false);
            Modules.clsView.PopulateComboBoxes(this.CMBSTKP_ProductFamily, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY, "", "", "", "", "", false, false);
            this.LocationChanged += Modules.clsView.FormLocationChanged;
            //set form captions
            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY);

            //apply system theme
            Modules.clsView.SetTheme(this, null);
        }

        private void frmProductFamilyMaintenance_Paint(object sender, PaintEventArgs e)
        {
            /*
               Created 05/03/2025 By Roger Williams

               Draws line across screen

             */

            //draw line
            e.Graphics.DrawLine(penTemp, 0, 120, this.Width, 120);

            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
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
            
            if (this.CMBSTKP_ProductFamily.Text == string.Empty)
            {
                return;
            }

            SaveRecord();
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            /*
               Created 05/03/2025 By Roger Williams

               


             */
            this.CMBSTKP_ProductFamily.Text = string.Empty; ;
            ResetForm(CNST_STR_FIRSTCONTROL, true);
            blnNew = true;
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
               Created 05/03/2025 By Roger Williams


             */

            if (this.CMBSTKP_ProductFamily.Text == string.Empty)
            {
                return;
            }

            if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //undo changes
                if (blnNew)
                {
                    blnNew = false;
                    this.CMBSTKP_ProductFamily.Text = strProductFamily;
                    strProductFamily = string.Empty;
                    strDesc = string.Empty;
                    Modules.clsView.PopulateComboBoxes(this.CMBSTKP_ProductFamily, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY, "", "", "", "", "", false, false);
                }
                else
                {
                    this.CMBSTKP_ProductFamily.Text = strProductFamily;
                    this.TXTSTKP_Desc.Text = strDesc;
                    strProductFamily = string.Empty;
                    strDesc = string.Empty;
                }
            }
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            /*
               Created 05/03/2025 By Roger Williams

               


             */

            if (this.CMBSTKP_ProductFamily.Text == string.Empty)
            {
                return;
            }

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

        private void CMBSTKP_ProductFamily_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 05/03/2025 By Roger Williams


            */

            blnOk = true;
            strProductFamily = this.CMBSTKP_ProductFamily.Text;
            GetProducFamilyDescription();
            strDesc = this.TXTSTKP_Desc.Text;
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
        }

        private void CMBSTKP_ProductFamily_Leave(object sender, EventArgs e)
        {
            /*
                     Created 05/03/2025 By Roger Williams

                     Check if location already exists

             */


            if (!blnOk)
            {
                if (blnNew)
                {
                    if (this.CMBSTKP_ProductFamily.Items.Contains(this.CMBSTKP_ProductFamily.Text))
                    {
                        this.CMBSTKP_ProductFamily.Text = string.Empty; ;
                        this.TXTSTKP_Desc.Text = string.Empty; ;
                        this.CMBSTKP_ProductFamily.Focus();
                        MessageBox.Show("Product Family Already In List", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    strProductFamily = this.CMBSTKP_ProductFamily.Text;
                    GetProducFamilyDescription();
                    strDesc = this.TXTSTKP_Desc.Text;
                }
            }

            blnOk = false;
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
                this.Location = new System.Drawing.Point(
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
