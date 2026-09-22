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
    public partial class frmJobsMaintenance : Form
    {
        //for manual mouse move of form
        bool blnDragging = false;
        Point pntLastLocation;

        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd = new SqlCommand();
        SqlDataAdapter DADJobs;
        BindingSource BNSJobs = new BindingSource();
        DataSet DSTJobs = new DataSet();

        bool blnNew = false;

        string CNST_STR_FIRSTCONTROL = "CMBJobID";
        //find form
        frmFind frmTemp = new frmFind(Modules.clsTables.CNST_STR_FINDJOB);

        public frmJobsMaintenance()
        {
            InitializeComponent();
        }

        private void SuppressKeyPressEventKey(object sender, KeyEventArgs e)
        {
            /*
              Created 03/03/2026 By Roger Williams

              used by comboboxes that has readonly data! 

            */
            e.SuppressKeyPress = true;
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
            if (this.DSTJobs.Tables.Count != 0)
            {
                if (this.DSTJobs.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_JOBS].GetChanges() != null)
                {
                    this.BNSJobs.CancelEdit();
                }
            }
            //reset form
            Modules.clsView.ResetForm(this, strKeep);
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            this.BTNFind.Enabled = true;
            this.DTEJOB_DateApplied.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //populate comboboxes
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Type, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_CONTRACTTYPE, "", "", "", "", "", false, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Hours, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_HOURS, "", "", "", "", "", false, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Status, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_STATUS, "", "", "", "", "", false, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Where, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_WHERE, "", "", "", "", "", false, false);
            //from jobs table
            Modules.clsView.PopulateComboBoxes(this.CMBJobID, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_ID", "", "", "", "", true,true);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Company, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Company", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Salary, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Salary", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Title, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Title", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_TownCity, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_TownCity", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Sector, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Sector", "", "", "", "", true, false);
        }


        //****binding***
        private void BindForm()
        {
            /*
              Created 25/02/2025 By Roger Williams

              binds form to table: 
            
              Stock_LOT

            */

            if (this.BNSJobs.Count > 0)
            {
                this.BNSJobs = new BindingSource();
            }

            this.BNSJobs.DataSource = this.DSTJobs.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_JOBS];

            //clear bindings
        //    this.CMBJobID.DataBindings.Clear();
            this.CMBJOB_Company.DataBindings.Clear();
            this.CMBJOB_Hours.DataBindings.Clear();
            this.CMBJOB_Salary.DataBindings.Clear();
            this.CMBJOB_Sector.DataBindings.Clear();
            this.CMBJOB_Status.DataBindings.Clear();
            this.CMBJOB_Title.DataBindings.Clear();
            this.CMBJOB_TownCity.DataBindings.Clear();
            this.CMBJOB_Type.DataBindings.Clear();
            this.CMBJOB_Where.DataBindings.Clear();
            this.DTEJOB_DateApplied.DataBindings.Clear();
            this.TXTJOB_Details.DataBindings.Clear();
            this.CHKJOB_Direct.DataBindings.Clear();

            //bind form controls 
        //    this.CMBJobID.DataBindings.Add("text", this.BNSJobs, "", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBJOB_Company.DataBindings.Add("text",this.BNSJobs, "JOB_Company", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBJOB_Hours.DataBindings.Add("text",this.BNSJobs, "JOB_Hours", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBJOB_Salary.DataBindings.Add("text",this.BNSJobs, "JOB_Salary", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBJOB_Sector.DataBindings.Add("text",this.BNSJobs, "JOB_Sector", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBJOB_Status.DataBindings.Add("text",this.BNSJobs, "JOB_Status", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBJOB_Title.DataBindings.Add("text",this.BNSJobs, "JOB_Title", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBJOB_TownCity.DataBindings.Add("text",this.BNSJobs, "JOB_TownCity", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBJOB_Type.DataBindings.Add("text",this.BNSJobs, "JOB_Type", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CMBJOB_Where.DataBindings.Add("text",this.BNSJobs, "JOB_Where", false, DataSourceUpdateMode.OnPropertyChanged);
            this.DTEJOB_DateApplied.DataBindings.Add("text",this.BNSJobs, "JOB_DateApplied", false, DataSourceUpdateMode.OnPropertyChanged);
            this.TXTJOB_Details.DataBindings.Add("text",BNSJobs, "JOB_Details", false, DataSourceUpdateMode.OnPropertyChanged);
            this.CHKJOB_Direct.DataBindings.Add("checked",this.BNSJobs, "JOB_Direct", false, DataSourceUpdateMode.OnPropertyChanged);
     //       this.TXTHidden.DataBindings.Add("text", this.BNSJobs, "LOT_ItemID", true, DataSourceUpdateMode.OnPropertyChanged);
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
                this.DSTJobs = new DataSet();
                this.SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_JOBS + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_TABLE_CRM_JOBS) + " = " + this.CMBJobID.Text + ";";
                this.DADJobs = new SqlDataAdapter(SQLCmd);
                this.DADJobs.Fill(this.DSTJobs, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS);
                BindForm();

                //enable form
                Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);

                //check if new record
                if (this.DSTJobs.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_JOBS].Rows.Count == 0)
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

            if (this.DSTJobs.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_JOBS].Rows.Count == 0)
            {
                return;
            }

            try
            {
                SQLTransaction = SQLConn.BeginTransaction();
                SQLCmd.Transaction = SQLTransaction;
                //delete record
                SQLCmd.CommandText = "DELETE FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_JOBS + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_TABLE_CRM_JOBS) + " = " + this.CMBJobID.Text + ";";
                SQLCmd.ExecuteNonQuery();
                //save changes
                SQLTransaction.Commit();
                //clear dataset record
                BNSJobs.RemoveCurrent();
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
                    strError = Modules.clsData.SaveRecord(Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, this, false,"");
                }
                else
                {
                    strError = Modules.clsData.SaveRecord(Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, this, true, this.CMBJobID.Text);
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
            BNSJobs.EndEdit();

            if (DSTJobs.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_JOBS].GetChanges() != null)
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

            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS);
            Modules.clsView.SetFormDataEntryMax(this, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS);
            //set form movement limits
            this.Move += Modules.clsView.FormLocationChanged;
            //lock combobox
            this.CMBJobID.KeyDown += SuppressKeyPressEventKey;
            ResetForm(CNST_STR_FIRSTCONTROL, false);

            //configure global SQL connection
            SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
            SQLConn.Open();
            SQLCmd.Connection = SQLConn;
        }

        //****form events etc**
        private void BTNClose_Click(object sender, EventArgs e)
        {
            SQLConn.Close();
            Modules.clsView.RemoveFromOpenForms(this.LBLTitle.Text);
            this.Close();
        }

        private void Form_JobsMaintenance_Paint(object sender, PaintEventArgs e)
        {
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

        private void frmJobsMaintenance_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            /*
             Created 05/03/2025 By Roger Williams


           */
            this.CMBJobID.Text = string.Empty; ;
            ResetForm(CNST_STR_FIRSTCONTROL, true);
            blnNew = true;
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
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
                        BNSJobs.CancelEdit();
                        LoadRecord();
                    }
                    else
                    {
                        ResetForm("", false);
                    }
                }
            }
        }

        private void CMBJobID_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete Record?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteRecord();
            }
        }

        private void BTNFind_Click(object sender, EventArgs e)
        {
            frmTemp.ShowDialog();

            if (Modules.clsData.objFindSelected != null)
            {
                this.CMBJobID.Text = Modules.clsData.objFindSelected.ToString();
                LoadRecord();
            }

        }



        //****end class
    }
}
