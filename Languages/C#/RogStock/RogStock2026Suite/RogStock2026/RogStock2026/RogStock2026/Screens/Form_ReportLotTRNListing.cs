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
using static System.Windows.Forms.AxHost;

namespace RogStock2026.Screens
{
    public partial class frmReportLotTRNListing : Form
    {
        /*
              Created 13/02/2025 By Roger Williams

              "prints" the report based on filter criteria


         */
        bool blnShow = true;
        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd = new SqlCommand();
        SqlDataAdapter DADLotTRN;
        DataSet DSTLotTRN;
        Pen penTemp;

        //for manual mouse move of form
        bool blnDragging = false;
        System.Drawing.Point pntLastLocation;

        public frmReportLotTRNListing()
        {
            InitializeComponent();
        }


        private void PopulateLotNbrComboBox()
        {
            /*
              Created 13/02/2025 By Roger Williams

              Populates the lot nbr combobox with data based on the LOT_TRN table
              itemID column

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            string strData = string.Empty; ;

            //clear combo
            this.CMBLOT_Nbr.Items.Clear();

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();

                    SQLCmd.CommandText = "SELECT DISTINCT LOTT_Nbr FROM " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + " ORDER BY LOTT_Nbr;";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        strData = SQLRead[0].ToString();

                        if (strData.Length != 0)
                        {
                            this.CMBLOT_Nbr.Items.Add(strData);
                        }
                    }

                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Table " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void PopulateLocationComboBox()
        {
            /*
              Created 13/02/2025 By Roger Williams

              Populates the location combobox with data based on the LOT_TRN table
              itemID column

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            string strData = string.Empty; ;

            //clear combo
            this.CMBLOC_Location.Items.Clear();

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();

                    SQLCmd.CommandText = "SELECT DISTINCT LOTT_Location FROM " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + " ORDER BY LOTT_Location;";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        strData = SQLRead[0].ToString();

                        if (strData.Length != 0)
                        {
                            this.CMBLOC_Location.Items.Add(strData);
                        }
                    }

                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Table " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void PopulateItemIDComboBox()
        {
            /*
              Created 13/02/2025 By Roger Williams

              Populates the itemID combobox with data based on the LOT_TRN table
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

                    SQLCmd.CommandText = "SELECT DISTINCT LOTT_ItemID FROM " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + " WHERE LOTT_ItemID IS NOT NULL ORDER BY LOTT_ItemID;";
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
                MessageBox.Show("Error Opening Table " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void PopulateOperationComboBox()
        {
            /*
              Created 13/02/2025 By Roger Williams

              Populates the operations combobox with data based on the LOT_TRN table
              operation column

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;

            //clear combo
            this.CMBLOTT_Operation.Items.Clear();

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();

                    SQLCmd.CommandText = "SELECT DISTINCT LOTT_Operation FROM " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + " WHERE LOTT_Operation IS NOT NULL ORDER BY LOTT_Operation;";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        this.CMBLOTT_Operation.Items.Add(SQLRead[0].ToString());
                    }

                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Table " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        //form events

        private void frmReportLotTRN_Load(object sender, EventArgs e)
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
                    PopulateLotNbrComboBox();
                    PopulateLocationComboBox();
                    PopulateOperationComboBox();
                    PopulateItemIDComboBox();
                    //set event handlers
                    this.LocationChanged += Modules.clsView.FormLocationChanged;
                    penTemp = new Pen(Color.Black, 1);
                    penTemp.Color = Color.Black;
                    //set form title
                    Modules.clsView.SetFormTitle(this);
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

            if (this.CHKALLLots.Checked == false && this.CMBLOT_Nbr.Text == String.Empty)
            {
                MessageBox.Show("No Lot Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (this.CHKALLOperations.Checked == false && this.CMBLOTT_Operation.Text == String.Empty)
            {
                MessageBox.Show("No Operation Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (this.CHKAllDates.Checked == false)
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

            try
            {
                SQLCmd.Connection = SQLConn;
                //get location data
                DSTLotTRN = new DataSet();

                //set report path
                RPTLotTRN.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_LOTTRN;

                //check for filter options

                //check if filtering lot nbr
                if (this.CHKALLLots.Checked)
                {
                    SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOTTRN;
                }
                else
                {
                    SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOTTRN + " AND " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + ".LOTT_Nbr = " + this.CMBLOT_Nbr.Text;
                }

                //check if filtering item ID
                if (this.CHKALLItems.Checked == false)
                {
                    if (SQLCmd.CommandText.Length == 0)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOTTRN + " AND " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + ".LOTT_ItemID = '" + this.CMBSTKI_ItemID.Text + "' ";
                    }
                    else
                    {
                        SQLCmd.CommandText += " AND " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + ".LOTT_ItemID = '" + this.CMBSTKI_ItemID.Text + "' ";
                    }
                }

                //check if filtering location
                if (this.CHKAllLocations.Checked == false)
                {
                    if (SQLCmd.CommandText.Length == 0)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOTTRN + " AND " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + ".LOTT_Location = '" + this.CMBLOC_Location.Text + "' ";
                    }
                    else
                    {
                        SQLCmd.CommandText += " AND " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + ".LOTT_Location = '" + this.CMBLOC_Location.Text + "' ";
                    }
                }

                //check if filtering operation
                if (this.CHKALLOperations.Checked == false)
                {
                    if (SQLCmd.CommandText.Length == 0)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOTTRN + " WHERE " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + ".LOTT_Operation = '" + this.CMBLOTT_Operation.Text + "' ";
                    }
                    else
                    {
                        SQLCmd.CommandText += " AND " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + ".LOTT_Operation = '" + this.CMBLOTT_Operation.Text + "' ";
                    }
                }

                //check if filtering date
                if (this.CHKAllDates.Checked == false)
                {
                    strDateFrom = this.DTEFrom.Value.Month.ToString() + "/" + this.DTEFrom.Value.Day.ToString() + "/" + this.DTEFrom.Value.Year.ToString();
                    strDateTo = this.DTETo.Value.Month.ToString() + "/" + this.DTETo.Value.Day.ToString() + "/" + this.DTETo.Value.Year.ToString();

                    if (SQLCmd.CommandText.Length == 0)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_LOTTRN + " WHERE " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + ".LOTT_DateTime BETWEEN '" + strDateFrom + " 00:00:00' AND '" + strDateTo + " 23:59:59'";
                    }
                    else
                    {
                        SQLCmd.CommandText += " AND " + Modules.clsTables.CNST_STR_TABLE_LOTTRN + ".LOTT_DateTime BETWEEN '" + strDateFrom + " 00:00:00' AND '" + strDateTo + " 23:59:59'";
                    }
                }

                SQLCmd.CommandText += ";"; // Modules.clsTables.CNST_STR_SELECT_LOTTRN_ORDERBY;
                DADLotTRN = new SqlDataAdapter(SQLCmd);
                DADLotTRN.Fill(DSTLotTRN, Modules.clsTables.CNST_STR_TABLE_LOTTRN);

                if (DSTLotTRN.Tables[Modules.clsTables.CNST_STR_TABLE_LOTTRN].Rows.Count != 0)
                {
                    //main report datasource
                    RDSReportDataSource.Name = "DataSet1";
                    RDSReportDataSource.Value = DSTLotTRN.Tables[Modules.clsTables.CNST_STR_TABLE_LOTTRN];

                    RPTLotTRN.LocalReport.DataSources.Clear();
                    RPTLotTRN.LocalReport.DataSources.Add(RDSReportDataSource);
                    RPTLotTRN.RefreshReport();
                    RPTLotTRN.Enabled = true;
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

        private void CHKALLOperations_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams



             */

            if (this.CHKALLOperations.Checked)
            {
                this.CMBLOTT_Operation.Text = string.Empty; ;
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

                  Checks if typed value in list

             */

            if (Modules.clsView.ValueInCombobox(this.CMBLOC_Location, this.CMBLOC_Location.Text))
            {
                this.CHKAllLocations.Checked = false;
            }

        }

        private void CMBLOTT_Operation_Leave(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams

                  Checks if typed value in list

             */
            if (Modules.clsView.ValueInCombobox(this.CMBLOTT_Operation, this.CMBLOTT_Operation.Text))
            {
                this.CHKALLOperations.Checked = false;
            }
        }

        private void CMBLOTT_Operation_SelectedValueChanged(object sender, EventArgs e)
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

        private void CMBLOT_Nbr_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams


             */
            this.CHKALLLots.Checked = false;
        }

        private void CMBLOT_Nbr_Leave(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams

                  Checks if typed value in list

             */
            if (Modules.clsView.ValueInCombobox(this.CMBLOT_Nbr, this.CMBLOT_Nbr.Text))
            {
                this.CHKALLLots.Checked = false;
            }
        }

        private void CHKALLLots_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams


             */
            this.CMBLOT_Nbr.Text = string.Empty; ;
        }

        private void frmReportLotTRN_Shown(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams

                  If no stock items closes form 
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

        private void frmReportLotTRN_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 5/03/2025 By Roger Williams

              Draws line across screen

            */

            //draw line
            e.Graphics.DrawLine(penTemp, 0, 140, this.Width, 140);

            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
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

        private void CMBSTKI_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                  Created 13/02/2025 By Roger Williams

                  Checks if typed value in list

             */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
            {
                this.CHKALLItems.Checked = false;
            }
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
