using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace RogStock2025.Screens
{
    public partial class frmReportLocTRNListing : Form
    {
        /*
              Created 13/02/2025 By Roger Williams

              "prints" the report based on filter criteria


         */

        bool blnShow = true;
        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd = new SqlCommand();
        SqlDataAdapter DADLocTRN;
        DataSet DSTLocTRN;
        Pen penTemp;


        public frmReportLocTRNListing()
        {
            InitializeComponent();
        }

        private void PopulateItemIDComboBox()
        {
            /*
              Created 13/02/2025 By Roger Williams

              Populates the itemID combobox with data based on the LOC_TRN table
              itemID column

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            string strData = string.Empty; ;

            //clear combo
            this.CMBSTKI_ItemID.Items.Clear();

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();

                    SQLCmd.CommandText = "SELECT DISTINCT LOCT_ItemID FROM " + Modules.clsTables.CNST_STR_LOCTRN + " WHERE LOCT_ItemID IS NOT NULL ORDER BY LOCT_ItemID;";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        strData = SQLRead[0].ToString();

                        if (strData.Length != 0)
                        {
                            this.CMBSTKI_ItemID.Items.Add(strData);
                        }
                    }

                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Table " + Modules.clsTables.CNST_STR_LOCTRN + " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void PopulateOperationComboBox()
        {
            /*
              Created 13/02/2025 By Roger Williams

              Populates the operations combobox with data based on the LOC_TRN table
              operation column

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;

            //clear combo
            this.CMBLOCT_Operation.Items.Clear();

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();

                    SQLCmd.CommandText = "SELECT DISTINCT LOCT_Operation FROM " + Modules.clsTables.CNST_STR_LOCTRN + " WHERE LOCT_Operation IS NOT NULL ORDER BY LOCT_Operation;";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        this.CMBLOCT_Operation.Items.Add(SQLRead[0].ToString());
                    }

                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Table " + Modules.clsTables.CNST_STR_LOCTRN + " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //form events
        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams



             */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void BTNShow_Click(object sender, EventArgs e)
        {
            /*
               Created 13/03/2025 By Roger Williams

               Sets datasource for report sets the main report datasource based on filter criteria

             */
            ReportDataSource RDSReportDataSource = new ReportDataSource();
            string strDateFrom = string.Empty; ;
            string strDateTo = string.Empty; ;

            if (this.CHKAllDates.Checked == false )
            { 
                if (this.DTEFrom.Value == null)
                {
                    MessageBox.Show("No Date Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (this.DTETo.Value == null)
                {
                    MessageBox.Show("No Date Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (this.DTEFrom.Value > this.DTETo.Value)
                {
                    MessageBox.Show("From Date Greater Than To Date!", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (this.CHKALLItems.Checked == false && this.CMBSTKI_ItemID.Text == String.Empty)
            {
                MessageBox.Show("No Item Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (this.CHKAllLocations.Checked == false && this.CMBLOC_Location.Text == String.Empty)
            {
                MessageBox.Show("No Location Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (this.CHKALLOperations.Checked == false && this.CMBLOCT_Operation.Text == String.Empty)
            {
                MessageBox.Show("No Operation Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                SQLCmd.Connection = SQLConn;
                //get location data
                DSTLocTRN = new DataSet();

                //set report path
               // RPTLocTRN = new ReportViewer();
                RPTLocTRN.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_LOCTRN;

                //check for filter options

                //check if filtering item ID
                if (this.CHKALLItems.Checked)
                {
                    SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOCTRN;
                }
                else
                {
                    SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOCTRN + " AND " + Modules.clsTables.CNST_STR_LOCTRN + ".LOCT_ItemID = '" + this.CMBSTKI_ItemID.Text + "' ";
                }

                //check if filtering location
                if (this.CHKAllLocations.Checked == false)
                {
                    if (SQLCmd.CommandText.Length == 0)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOCTRN + " AND " + Modules.clsTables.CNST_STR_LOCTRN + ".LOCT_Location = '" + this.CMBLOC_Location.Text + "' ";
                    }
                    else
                    {
                        //if (SQLCmd.CommandText.Contains("WHERE"))
                        //{
                            SQLCmd.CommandText += " AND " + Modules.clsTables.CNST_STR_LOCTRN + ".LOCT_Location = '" + this.CMBLOC_Location.Text + "' ";
                        //}
                        //else
                        //{
                        //    SQLCmd.CommandText += " WHERE LOCT_Location = '" + this.CMBLOC_Location.Text + "'";
                        //}
                    }
                }

                //check if filtering operation
                if (this.CHKALLOperations.Checked == false)
                {
                    if (SQLCmd.CommandText.Length == 0)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOCTRN + "AND  " + Modules.clsTables.CNST_STR_LOCTRN + ".LOCT_Operation = '" + this.CMBLOCT_Operation.Text + "' ";
                    }
                    else
                    {
                        //if (SQLCmd.CommandText.Contains("WHERE"))
                        //{
                            SQLCmd.CommandText += " AND " + Modules.clsTables.CNST_STR_LOCTRN +".LOCT_Operation = '" + this.CMBLOCT_Operation.Text + "' ";
                        //}
                        //else
                        //{
                        //    SQLCmd.CommandText += " WHERE LOCT_Operation = '" + this.CMBLOCT_Operation.Text + "'";
                        //}
                    }
                }

                //check if filtering date
                if (this.CHKAllDates.Checked == false)
                {
                    strDateFrom = this.DTEFrom.Value.Month.ToString() + "/" + this.DTEFrom.Value.Day.ToString() + "/" + this.DTEFrom.Value.Year.ToString();
                    strDateTo = this.DTETo.Value.Month.ToString() + "/" + this.DTETo.Value.Day.ToString() + "/" + this.DTETo.Value.Year.ToString();

                    if (SQLCmd.CommandText.Length == 0)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOCTRN + " AND " + Modules.clsTables.CNST_STR_LOCTRN + ".LOCT_DateTime BETWEEN '" + strDateFrom + " 00:00:00' AND '" + strDateTo + " 23:59:59'";
                    }
                    else
                    {
                        //if (SQLCmd.CommandText.Contains("WHERE"))
                        //{
                            SQLCmd.CommandText += " AND " + Modules.clsTables.CNST_STR_LOCTRN + ".LOCT_DateTime BETWEEN '" + strDateFrom + " 00:00:00' AND '" + strDateTo + " 23:59:59'";
                        //}
                        //else
                        //{
                        //    SQLCmd.CommandText += " WHERE LOCT_DateTime BETWEEN '" + strDateFrom + "' AND '" + strDateTo + "'";
                        //}
                    }
                }

                SQLCmd.CommandText += ";";   //Modules.clsTables.CNST_STR_SELECT_LOCTRN_ORDERBY;

                DADLocTRN = new SqlDataAdapter(SQLCmd);
                DADLocTRN.Fill(DSTLocTRN, Modules.clsTables.CNST_STR_LOCTRN);

                if (DSTLocTRN.Tables[Modules.clsTables.CNST_STR_LOCTRN].Rows.Count != 0)
                {
                    //main report datasource
                    RDSReportDataSource.Name = "DataSet1";
                    RDSReportDataSource.Value = DSTLocTRN.Tables[Modules.clsTables.CNST_STR_LOCTRN];

                    RPTLocTRN.LocalReport.DataSources.Clear();
                    RPTLocTRN.LocalReport.DataSources.Add(RDSReportDataSource);
                    RPTLocTRN.RefreshReport();
                    RPTLocTRN.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No Records Found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error Cannot Retrieve Data!", "Report Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void frmReportLocTRN_Load(object sender, EventArgs e)
        {
            /*
              Created 10/03/2025 By Roger Williams

              If no stock items closes form!
              Else populates comboboxes and sets combobox eventhandlers
              and opens sql connection

            */
            if (Modules.clsData.CheckForStockItems())
            {
                try
                {
                    SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
                    SQLConn.Open();
                    Modules.clsData.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_LOCTRN, "", "", "", "", "", false);
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_STOCK_LOC, "", "", "", "", "", false);
                    PopulateOperationComboBox();
                    PopulateItemIDComboBox();
                    //set event handlers
                    this.LocationChanged += Modules.clsView.FormLocationChanged;
                    penTemp = new Pen(Color.Black, 1);
                    penTemp.Color = Color.Black;
                    //apply system theme
                    Modules.clsView.SetTheme(this, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error Cannot Retrieve Data!", "Report Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    blnShow = false;
                }
            }
            else
            {
                blnShow = false;
            }

        }

        private void CHKAllLocations_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams



             */
            if (this.CHKAllLocations.Checked)
            {
                this.CMBLOC_Location.Text = string.Empty; ;
            }
        }

        private void CHKALLOperations_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams



             */
            if (this.CHKALLOperations.Checked)
            {
                this.CMBLOCT_Operation.Text = string.Empty; ;
            }
        }

        private void CHKAllDates_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams



             */
            if (this.CHKAllDates.Checked)
            {
                this.DTEFrom.Enabled = false;
                this.DTETo.Enabled = false;
            }
            else
            {
                this.DTEFrom.Enabled = true;
                this.DTETo.Enabled = true;
            }
        }

        private void CHKALLItems_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams



             */
            if (this.CHKALLItems.Checked)
            {
                this.CMBSTKI_ItemID.Text = string.Empty; ;
            }
        }

        private void CMBLOC_Location_Leave(object sender, EventArgs e)
        {
            /*
                   Created 13/02/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBLOC_Location, this.CMBLOC_Location.Text))
            {
                this.CHKAllLocations.Checked = false;
            }

        }

        private void CMBSTKI_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                   Created 13/02/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
            {
                this.CHKALLItems.Checked = false;
            }
        }

        private void CMBLOCT_Operation_Leave(object sender, EventArgs e)
        {
            /*
                   Created 13/02/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBLOCT_Operation, this.CMBLOCT_Operation.Text))
            {
                this.CHKALLOperations.Checked = false;
            }
        }

        private void CMBLOCT_Operation_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 13/02/2025 By Roger Williams


            */
            this.CHKALLOperations.Checked = false;
        }

        private void CMBLOC_Location_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 13/02/2025 By Roger Williams


            */
            this.CHKAllLocations.Checked = false;
        }

        private void CMBSTKI_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 13/02/2025 By Roger Williams


            */
            this.CHKALLItems.Checked = false;
        }

        private void frmReportLocTRN_Shown(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams


                  If no stock items close form
             */
            if (blnShow == false)
            {
                SQLConn.Close();
                MessageBox.Show("Cannot Continue No Stock Items Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void DTEFrom_ValueChanged(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams



             */
            this.CHKAllDates.Checked = false;
        }

        private void DTETo_ValueChanged(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams



             */
            this.CHKAllDates.Checked = false;
        }

        private void frmReportLocTRN_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 5/03/2025 By Roger Williams

              Draws line across screen

            */

            //draw line
            e.Graphics.DrawLine(penTemp, 0, 180, this.Width, 180);
        }

    }
}
