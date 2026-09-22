using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using RogJobCRMPlus.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogJobCRMPlus.Forms
{
    public partial class frmMailshots : Form
    {

        //for manual mouse move of form
        bool blnDragging = false;
        Point pntLastLocation;

        bool blnLoading = false;
        bool blnNew = false;

        //find form parameter is find to use
        frmFind frmTemp = new frmFind(Modules.clsTables.CNST_STR_FINDMAILSHOT);

        public frmMailshots()
        {
            InitializeComponent();
        }

        BindingSource bnsMailshotHeader = new BindingSource();
    //    BindingSource bnsMailshotLines = new BindingSource();

        //datasets used to checking if data changed
        DataSet DSTMailshotHeader = new DataSet();
        DataSet DSTMailshotLines = new DataSet();
        //used globally as bound controls need active datasets
        SqlConnection SQLConn = null;
        SqlCommand SQLCmd = new SqlCommand();

        string CNST_STR_FIRSTCONTROL = "CMBMailshotID";


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
            if (this.DSTMailshotHeader.Tables.Count != 0)
            {
                if (this.DSTMailshotHeader.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS].GetChanges() != null)
                {
                    this.bnsMailshotHeader.CancelEdit();
                  //  this.bnsMailshotLines.CancelEdit();
                }
            }
            //reset form
            Modules.clsView.ResetForm(this, strKeep);
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            this.BTNFind.Enabled = true;
            this.DTEMSH_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //populate comboboxes
            Modules.clsView.PopulateComboBoxes(this.CMBMailshotID, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER, "MSH_ID", "", "", "", "", true, true);
            Modules.clsView.PopulateComboBoxes(this.CMBMSH_MailshotName, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER, "MSH_MailshotName", "", "", "", "", true, false);
        }

 
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
            bnsMailshotHeader.EndEdit();

            //if (DSTMailshotLines.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES].GetChanges() != null)
            //{
            //    intChangesMade++;
            //}

            if (DSTMailshotHeader.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER].GetChanges() != null)
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
              Created 04/03/2026 By Roger Williams
                
              Fills:

              DSTMailshotHeader
              DSTMailshotLines

              needed for data bound controls to work!

            */

            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER);
            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES);
            Modules.clsView.SetFormDataEntryMax(this, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER);
            //penTemp = new Pen(Color.White);
            ResetForm(CNST_STR_FIRSTCONTROL, false);

            //configure global SQL connection
            SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
            SQLConn.Open();
            SQLCmd.Connection = SQLConn;
            //set form movement limits
            this.Move += Modules.clsView.FormLocationChanged;
        }


        //****data
        private void BindForm()
        {
            /*
              Created 13/06/2025 By Roger Williams

              binds form to tables: 
            
              Stock_Items
              Stock_Description

              Note: cant add media as listviews do not support binding, plus 1-to-many relationship!

            */

            if (this.bnsMailshotHeader.Count > 0)
            {
                bnsMailshotHeader = new BindingSource();
            //    bnsMailshotLines = new BindingSource();
            }

            bnsMailshotHeader.DataSource = DSTMailshotHeader.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER];
          //  bnsMailshotLines.DataSource = DSTMailshotLines.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES];

            //clear bindings
            this.CMBMSH_MailshotName.DataBindings.Clear();
            this.TXTMSH_Comments.DataBindings.Clear();

            //bind form controls 
            this.CMBMSH_MailshotName.DataBindings.Add("text", bnsMailshotHeader, "MSH_MailshotName", true, DataSourceUpdateMode.OnPropertyChanged);
            this.TXTMSH_Comments.DataBindings.Add("text", bnsMailshotHeader, "MSH_Comments", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void LoadRecord()
        {
            /*
              Created 04/03/2026 By Roger Williams
  
              - Populates the form
              - Populates dataset with table and a COPY of the table
              - Enables the form

            Note: reads from these tables:
                  - mailshot_header
                  - mailshot_lines

            */
            
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            ListViewItem LVITemp;
            SqlDataAdapter DADTemp;

            string strSQL1 = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER) + " ='" + this.CMBMailshotID.Text + "';";
            string strSQL2 = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES + " WHERE MSH_ID =" + this.CMBMailshotID.Text + ";";

            blnLoading = true;
            //reset form except item id combobox
            ResetForm("CMB_MSH_MailshotName", false);


            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = strSQL1;
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    if (SQLRead.Read())
                    {
                        this.CMBMSH_MailshotName.Text = SQLRead["MSH_MailshotName"].ToString();
                        this.TXTMSH_Comments.Text = SQLRead["MSH_Comments"].ToString();

                        SQLRead.Close();
                        //clear existing records
                        if (DSTMailshotHeader.Tables.Count > 0)
                        {
                     //       DSTMailshotLines.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES].Clear();
                            DSTMailshotHeader.Tables[Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER].Clear();
                        }
                        ////get record into into dataset
                        DADTemp = new SqlDataAdapter(strSQL1, SQLConn);
                        DADTemp.SelectCommand.CommandText = strSQL1;
                        DADTemp.Fill(DSTMailshotHeader, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER);
              //          DADTemp.SelectCommand.CommandText = strSQL2;
              //          DADTemp.Fill(DSTMailshotLines, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES);


                        //populate listview
                        this.LVLines.Items.Clear();
                        //read lines
                        SQLCmd.CommandText = strSQL2;
                        SQLRead = SQLCmd.ExecuteReader();

                        while (SQLRead.Read())
                        {
                            LVITemp = new ListViewItem();
                            LVITemp.Text = SQLRead["MSL_CompanyName"].ToString();
                            LVITemp.SubItems.Add(SQLRead["MSL_Email"].ToString());
                            LVITemp.SubItems.Add(SQLRead["MSH_ID"].ToString());
                            this.LVLines.Items.Add(LVITemp);
                        }

                        SQLRead.Close();
                        
                        //bindform
                        BindForm();

                        Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                    }
                    else
                    {
                        MessageBox.Show("No Records Found", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error: /n" + ex.Message);
            }

            blnLoading = false;
        }

        //******data manipulation subs/procs***
        private void DeleteRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Deletes current record using a transaction
              - clears form 
              - clears datasets

            */

        SqlCommand SQLCmd;
        SqlTransaction SQLTransaction;

            try
            {
                    //create command objects for each table
                    SQLCmd = new SqlCommand("DELETE FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER) + " = " + this.CMBMailshotID.Text + ";", SQLConn);
                    //start transction            
                    SQLTransaction = SQLConn.BeginTransaction();
                    //assign commands to the transaction
                    SQLCmd.Transaction = SQLTransaction;

                    try
                    {
                        //delete existing
                        SQLCmd.ExecuteNonQuery();
                        //write changes
                        SQLTransaction.Commit();
                        //remove from binding source
                        bnsMailshotHeader.RemoveCurrent();
                      //  bnsMailshotLines.RemoveCurrent();
                        //reset form
                        ResetForm("", false);
                        MessageBox.Show("Record Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        SQLTransaction.Rollback();
                        MessageBox.Show("Error Deleting Data:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Accessing Database:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void SaveRecord()
        {
            /*
              Created 09/03/2026 By Roger Williams

              - saves current record using a transaction
              - Clears form 

              - Writes TRN record (future development)
              - BEFORE doing anything checks if required fields are populated

              Note: for this form all done manually and stored procedure: SP_CreateMailshot
                    is used to get MSH_ID for inserting into lines records

            */
            string strError = string.Empty;
            int intMSH_ID = 0;
            SqlTransaction SQLTrans = null;

            if (Modules.clsView.ValidateRequiredFields(this))
            {
                if (blnNew)
                {
                    try
                    {
                        //save header
                        //     SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER + " (MSH_MailshotName, MSH_Date) " +
                        //                          "VALUES ('" + this.CMB_MSH_MailshotName.Text + "','" + this.DTEMSH_Date.Text + "');";
                        SQLTrans = SQLConn.BeginTransaction();
                        SQLCmd.Transaction=SQLTrans;
                        SQLCmd.CommandText = "SP_CreateMailshot";
                        SQLCmd.CommandType = CommandType.StoredProcedure;
                        SQLCmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = this.CMBMSH_MailshotName.Text;
                        SQLCmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = Convert.ToDateTime(this.DTEMSH_Date.Text);
                        SQLCmd.Parameters.Add("@Comments", SqlDbType.Text).Value = this.TXTMSH_Comments.Text;
                        SQLCmd.Parameters.Add("@MSH_ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        SQLCmd.ExecuteNonQuery();
                        SQLCmd.CommandType = CommandType.Text;
                       
                        //get autonumber value
                        intMSH_ID = Convert.ToInt16(SQLCmd.Parameters["@MSH_ID"].Value);
                        SQLCmd.Parameters.Clear();

                        //save lines
                        foreach (ListViewItem LVITemp in this.LVLines.Items)
                        {
                            SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES + " (MSH_ID, MSL_CompanyName, MSL_Email) " +
                                                 "VALUES (" + intMSH_ID +  ",'" + LVITemp.Text + "','" + LVITemp.SubItems[1].Text + "');";
                            SQLCmd.ExecuteNonQuery();
                        }

                        SQLTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        strError = ex.Message;

                        if (SQLTrans != null)
                        {
                            SQLTrans.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        SQLTrans = SQLConn.BeginTransaction();
                        SQLCmd.Transaction = SQLTrans;

                        //save header
                        SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER + " SET MSH_MailshotHeader = '" + this.CMBMSH_MailshotName + "'," +
                                             ", MSH_Date = '" + this.DTEMSH_Date.Text + "' " + "WHERE MSH_ID =" + this.CMBMailshotID.Text + ";";
                        SQLCmd.ExecuteNonQuery();

                        //save lines
                        foreach (ListViewItem LVITemp in this.LVLines.Items)
                        {
                            //update any listview items with column 3 (MSL_ID) populated
                            if (LVITemp.SubItems[2].Text != string.Empty)
                            {
                                SQLCmd.CommandText = "UPDATE " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES + " SET MSL_CompanyName = '" + LVITemp.Text + "'," +
                                                     ", MSL_Email = '" + LVITemp.SubItems[1].Text + "' WHERE MSL_ID =" + LVITemp.SubItems[2].Text + ";";
                                SQLCmd.ExecuteNonQuery();
                            }
                            else
                            {
                                //create any listview items with column 3 (MSL_ID) empty
                                SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES + " (MSH_ID, MSL_CompanyName, MSL_Email) " +
                                                     "VALUES (" + this.CMBMailshotID.Text + ",'" + LVITemp.Text + "','" + LVITemp.SubItems[1].Text + "');";
                                SQLCmd.ExecuteNonQuery();
                            }
                        }

                        SQLTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        strError = ex.Message;

                        if (SQLTrans != null)
                        {
                            SQLTrans.Rollback();
                        }
                    }
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





        //*****form events etc******
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

        private void frmMailshots_Paint(object sender, PaintEventArgs e)
        {
            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            SQLConn.Close();
            Modules.clsView.RemoveFromOpenForms(this.LBLTitle.Text);
            this.Close();
        }

        private void frmMailshots_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*

               Created 04/03/2026 By Roger Williams
               
               undoes changes if made

            */

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
                        bnsMailshotHeader.CancelEdit();
                        LoadRecord();
                    }
                    else
                    {
                        ResetForm("", false);
                    }
                }
            }
        }

        private void BTNImport_Click(object sender, EventArgs e)
        {
            /*

               Created 04/03/2026 By Roger Williams
               
               imports mailshot data from text file used in emailer project

               file data example:

               Name:Alma Personnel
               Addr:info@almapersonnel.co.uk

               copies file from source into mailshots sub folder and appends date/time to end

            */

            StreamReader strmRead;
            OpenFileDialog DLGOpen = new OpenFileDialog();
            string strData = string.Empty;
            string strNow = string.Empty;
            string strFileName = string.Empty;
            ListViewItem LVTemp;
            DateTime dtNow = DateTime.Now;

            DLGOpen.Title = "Select RogsEmailer Mailshot File";
            DLGOpen.Multiselect = false;
            DLGOpen.CheckFileExists = true;
            
            if (DLGOpen.ShowDialog() == DialogResult.OK)
            {
                //open file see if contains correct data 
                strmRead = new StreamReader(DLGOpen.FileName);

                strData = strmRead.ReadLine();
                //check data
                if (!strData.Contains("Name:"))
                {
                    MessageBox.Show("File Not Correct Format!","Error Reading File",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    LVTemp = new ListViewItem();
                    LVTemp.Text = strData.Substring(5, strData.Length - 5);
                    //get email address
                    strData = strmRead.ReadLine();
                    LVTemp.SubItems.Add(strData.Substring(5, strData.Length - 5));
                    this.LVLines.Items.Add(LVTemp);

                    //process rest of file
                    while (strmRead.Peek() != -1)
                    {
                        LVTemp = new ListViewItem();
                        LVTemp.Text = strData.Substring(5, strData.Length - 5);
                        //get email address
                        strData = strmRead.ReadLine();
                        LVTemp.SubItems.Add(strData.Substring(5, strData.Length - 5));
                        this.LVLines.Items.Add(LVTemp);
                    }

                    //copy source file to mailshots folder
                    try
                    {
                        //append date/time to filename
                        strFileName = Path.GetFileNameWithoutExtension(DLGOpen.FileName);

                        //populate strdatetime
                        strNow = dtNow.Day > 9 ? dtNow.Day.ToString() : "0" + dtNow.Day.ToString();
                        strNow += dtNow.Month > 9 ? dtNow.Month.ToString() : "0" + dtNow.Month.ToString();
                        strNow += dtNow.Year.ToString() + "_";
                        strNow += dtNow.Hour > 9 ? dtNow.Hour.ToString() : "0" + dtNow.Hour.ToString();
                        strNow += dtNow.Month > 9 ? dtNow.Month.ToString() : "0" + dtNow.Month.ToString();
                        strNow += dtNow.Second > 9 ? dtNow.Second.ToString() : "0" + dtNow.Second.ToString();

                        strFileName+="_"+strNow+Path.GetExtension(DLGOpen.FileName);
                        File.Copy(DLGOpen.FileName, Modules.clsData.CNST_STR_MAILSHOTPATH+"\\"+ strFileName, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Copying Mailshot Filet", "Error Copying File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

                strmRead.Close();
            }
        }

        private void BTNFind_Click(object sender, EventArgs e)
        {
            frmTemp.ShowDialog();

            if (Modules.clsData.objFindSelected != null)
            {
                this.CMBMSH_MailshotName.Text = Modules.clsData.objFindSelected.ToString();
                LoadRecord();
            }
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            /*
                Created 06/03/2025 By Roger Williams


            */

            this.CMBMailshotID.Text = string.Empty; ;
            ResetForm(CNST_STR_FIRSTCONTROL, true);
            blnNew = true;
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete Record?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteRecord();
            }
        }

        private void CMBMailshotID_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }
    }
}
