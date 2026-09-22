using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;

namespace RogJobCRMPlus.Forms
{
    public partial class frmEvents : Form
    {

        //find form parameter is find to use
        frmFind frmTemp = new frmFind(Modules.clsTables.CNST_STR_FINDEVENT);
        bool blnNew = false;

        //for manual mouse move of form
        bool blnDragging = false;
        Point pntLastLocation;

        Pen penTemp;
        string CNST_STR_FIRSTCONTROL = "CMBEVTID";

        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd = new SqlCommand();
        SqlDataAdapter DADEvents;
        BindingSource BNSEvents = new BindingSource();
        DataSet DSTEvents = new DataSet();


        public frmEvents()
        {
            InitializeComponent();
        }

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
            if (this.DSTEvents.Tables.Count != 0)
            {
                if (this.DSTEvents.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS].GetChanges() != null)
                {
                    this.BNSEvents.CancelEdit();
                }
            }
            //reset form
            Modules.clsView.ResetForm(this, strKeep);
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            this.BTNFind.Enabled = true;
            this.DTEEVT_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //populate comboboxes
            Modules.clsView.PopulateComboBoxes(this.CMBEVTID, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, "EVT_ID", "", "", "", "", true,true);
            Modules.clsView.PopulateComboBoxes(this.CMBEVT_Name, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, "EVT_Name", "", "", "", "", true,false);
            Modules.clsView.PopulateComboBoxes(this.CMBEVT_Where, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, "EVT_Where", "", "", "", "", true, false);
        }


        //****binding***
        private void BindForm()
        {
            /*
              Created 25/02/2025 By Roger Williams

              binds form to table: 
            
              Stock_LOT

            */

            if (this.BNSEvents.Count > 0)
            {
                this.BNSEvents = new BindingSource();
            }

            this.BNSEvents.DataSource = this.DSTEvents.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS];

            //clear bindings
            //    this.CMBEVTID.DataBindings.Clear();
            this.CMBEVT_Name.DataBindings.Clear();
            this.CMBEVT_Where.DataBindings.Clear();
            this.TXTEVT_Contact.DataBindings.Clear();
            this.DTEEVT_Date.DataBindings.Clear();
            this.TXTEVT_Details.DataBindings.Clear();
            this.TXTEVT_Comments.DataBindings.Clear();
            this.CHKEVT_Attended.DataBindings.Clear();
            this.CHKEVT_Booked.DataBindings.Clear();

            //bind form controls 
            //    this.CMBEVTID.DataBindings.Add("text", this.BNSEvents, "", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBEVT_Name.DataBindings.Add("text", this.BNSEvents, "EVT_Name", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBEVT_Where.DataBindings.Add("text", this.BNSEvents, "EVT_Where", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TXTEVT_Contact.DataBindings.Add("text", this.BNSEvents, "EVT_Contact", false, DataSourceUpdateMode.OnPropertyChanged);
            this.DTEEVT_Date.DataBindings.Add("text", this.BNSEvents, "EVT_Date", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TXTEVT_Details.DataBindings.Add("text", BNSEvents, "EVT_Details", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TXTEVT_Comments.DataBindings.Add("text", BNSEvents, "EVT_Comments", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CHKEVT_Attended.DataBindings.Add("checked", this.BNSEvents, "EVT_Attended", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CHKEVT_Booked.DataBindings.Add("checked", this.BNSEvents, "EVT_Booked", false, DataSourceUpdateMode.OnPropertyChanged);
            //       this.TXTHidden.DataBindings.Add("text", this.BNSEvents, "LOT_ItemID", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        private void LoadRecord()
        {
            /*
              Created 05/03/2026 By Roger Williams

              - Populates the form
              - Enables the form
              - Binds the form to table fields

              Why?

              If data is changed elsewhere searching through a disconnected dataset for data could find the user
              editing a record someone else has deleted!

              This approach of load ONE record into the dataset means each loads reads current data
             
            */

            try
            {
                //get data
                this.DSTEvents = new DataSet();
                this.SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS) + " = " + this.CMBEVTID.Text + "; ";
                this.DADEvents = new SqlDataAdapter(SQLCmd);
                this.DADEvents.Fill(this.DSTEvents, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS);
                BindForm();

                //enable form
                Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);

                //check if new record
                if (this.DSTEvents.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS].Rows.Count == 0)
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

        }
        //****end binding***


        //****data***
        private void DeleteRecord()
        {
            /*
              Created 05/03/2026 By Roger Williams

              - Deletes current record using a transaction
              - Clears form 

              - Writes TRN record (future development)
              - BEFORE doing anything checks if ONLY lot for item/loc if so does NOT allow delete as this would break loc/lot tracking!
              - Else if deleting the lot brings the lot quantity BELOW the loc qty stop processing

            */

            SqlTransaction SQLTransaction = null;

            if (this.DSTEvents.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS].Rows.Count == 0)
            {
                return;
            }

            try
            {
                SQLTransaction = SQLConn.BeginTransaction();
                SQLCmd.Transaction = SQLTransaction;
                //delete record
                SQLCmd.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS) + " = " + this.CMBEVTID.Text + ";";
                SQLCmd.ExecuteNonQuery();
                //save changes
                SQLTransaction.Commit();
                //clear dataset record
                BNSEvents.RemoveCurrent();
                //reset form
                ResetForm("", false);
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
              Created 05/03/2026 By Roger Williams

              - saves current record using a transaction
              - Clears form 

              - Writes TRN record (future development)
              - BEFORE doing anything checks if required fields are populated

            */

            string strError = string.Empty;

            if (Modules.clsView.ValidateRequiredFields(this))
            {
                if (blnNew)
                {
                    strError = Modules.clsData.SaveRecord(Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, this, false, "");
                }
                else
                {
                    strError = Modules.clsData.SaveRecord(Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, this, true, this.CMBEVTID.Text);
                }

                if (strError == string.Empty)
                {
                    //reset form
                    ResetForm("", false);
                    blnNew = false;
                    MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record Not Saved!", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //****end data***

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
            BNSEvents.EndEdit();

            //if (DSTMailshotLines.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES].GetChanges() != null)
            //{
            //    intChangesMade++;
            //}

            if (DSTEvents.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS].GetChanges() != null)
            {
                intChangesMade++;
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
        private void Init()
        {
            /*
              Created 03/03/2026 By Roger Williams

              populates comboboxes with data (if any)
              sets form label captions and form title 
              sets keydown event for readonly comboboxes
              creates pen for line drawing

            */

            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS);
            Modules.clsView.SetFormDataEntryMax(this, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS);
            penTemp = new Pen(Color.White);
            ResetForm(CNST_STR_FIRSTCONTROL, false);
            //set form movement limits
            this.Move += Modules.clsView.FormLocationChanged;
            //configure global SQL connection
            SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
            SQLConn.Open();
            SQLCmd.Connection = SQLConn;
        }





        //*****form events etc***
        private void BTNClose_Click(object sender, EventArgs e)
        {
            SQLConn.Close();
            Modules.clsView.RemoveFromOpenForms(this.LBLTitle.Text);
            this.Close();
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

        private void BTNFind_Click(object sender, EventArgs e)
        {
            frmTemp.ShowDialog();

            if (Modules.clsData.objFindSelected != null)
            {
                this.CMBEVTID.Text = Modules.clsData.objFindSelected.ToString();
                LoadRecord();
            }

        }

        private void CMBEVTID_KeyDown(object sender, KeyEventArgs e)
        {
            //lock combobox
            e.SuppressKeyPress=true;
        }

        private void frmEvents_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(penTemp, 0, 260, this.Width, 260);
        }

        private void frmEvents_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            /*
                Created 06/03/2025 By Roger Williams


            */

            this.CMBEVTID.Text = string.Empty; ;
            ResetForm(CNST_STR_FIRSTCONTROL, true);
            blnNew = true;
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void CMBEVTID_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
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
                        BNSEvents.CancelEdit();
                        LoadRecord();
                    }
                    else
                    {
                        ResetForm("", false);
                    }
                }
            }
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete Record?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteRecord();
            }
        }


        //****end class
    }
}
