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

namespace RogStock2026.Screens
{
    public partial class frmReportStockItemTRNHistoryListing : Form
    {
    /*
     
      Created 29/08/2025 By Roger Williams

      "prints" the report based on filter criteria

    */

        bool blnShow = true;
        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd = new SqlCommand();
        SqlCommand SQLCmdLocTRN = new SqlCommand();
        SqlDataAdapter DADStockItem;
        SqlDataAdapter DADLocTRN;
        DataSet DSTStockItem;
        DataSet DSTLocTRN;
        
        Pen penTemp;
        //for sub report
        DataTable TBLTemp;
        // Must match the name of the DataSource in the report
        ReportDataSource RDSReportDataSource = new ReportDataSource("DataSet1");

        //for manual mouse move of form
        bool blnDragging = false;
        System.Drawing.Point pntLastLocation;

        public frmReportStockItemTRNHistoryListing()
        {
            InitializeComponent();
        }

        private void SubReportEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            /*
                   Created 12/03/2025 By Roger Williams

                   This event is called by EACH subreport record
                   Sets the datasource

                   Note: By default report dataset is set to DataSet1 in report designer

            */

            string strItemID = String.Empty;

            strItemID = e.Parameters[0].Values[0];
            TBLTemp.Rows.Clear();

            foreach (DataRow DARTemp in DSTLocTRN.Tables["DataSet1"].Rows)
            {
                //if date filtered filter recordset
                if (this.CHKAllDates.Checked == false)
                {
                    if (this.DTEFrom.Value >= (DateTime)DARTemp["LOTT_DateTime"] && (this.DTETo.Value >= (DateTime)DARTemp["LOTT_DateTime"]))
                    {
                        TBLTemp.ImportRow(DARTemp);
                    }
                }
                else
                { 
                    if (String.Equals(strItemID, DARTemp["LOTT_ItemID"].ToString()))
                    {
                        TBLTemp.ImportRow(DARTemp);
                    }
                }
            }

            RDSReportDataSource.Value = TBLTemp;
            e.DataSources.Clear();
            e.DataSources.Add(RDSReportDataSource);
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

                    SQLCmd.CommandText = "SELECT DISTINCT LOCT_ItemID FROM " + Modules.clsTables.CNST_STR_TABLE_LOCTRN + " WHERE LOCT_ItemID IS NOT NULL ORDER BY LOCT_ItemID;";
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
                MessageBox.Show("Error Opening Table " + Modules.clsTables.CNST_STR_TABLE_LOCTRN + " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    SQLCmd.CommandText = "SELECT DISTINCT LOCT_Operation FROM " + Modules.clsTables.CNST_STR_TABLE_LOCTRN + " WHERE LOCT_Operation IS NOT NULL ORDER BY LOCT_Operation;";
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
                MessageBox.Show("Error Opening Table " + Modules.clsTables.CNST_STR_TABLE_LOCTRN + " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //form events

        private void frmReportStockItemTRNHistoryListing_Load(object sender, EventArgs e)
        {
            /*
              Created 10/03/2025 By Roger Williams

              If no stock items closes form!
              Else populates comboboxes and sets combobox eventhandlers
              and opens sql connection

            */
            if (Modules.clsData.CheckForStockItems() && Modules.clsData.CheckLLocTRNRecords())
            {
                try
                {
                    SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
                    SQLConn.Open();
                    Modules.clsView.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_TABLE_LOCTRN, "", "", "", "", "", false, false);
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
                  Created 29/08/2025 By Roger Williams


             */

            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void BTNShow_Click(object sender, EventArgs e)
        {
            /*
                Created 29/08/2025 By Roger Williams

                Sets datasource for report sets the main report datasource based on filter criteria

              */
            ReportDataSource RDSReportDataSource = new ReportDataSource();
            string strDateFrom = string.Empty; ;
            string strDateTo = string.Empty; ;

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

            if (this.CHKALLOperations.Checked == false)
            {
                if (this.CMBLOCT_Operation.Text == String.Empty)
                {
                    MessageBox.Show("No Operation Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }



            try
            {
                SQLCmd.Connection = SQLConn;
                //check for filter options

                //check if filtering item ID
                if (this.CHKALLItems.Checked)
                {
                    SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY;
                }
                else
                {
                    SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY + " WHERE STKI_ItemID = '" + this.CMBSTKI_ItemID.Text + "' ";
                }

                //check if filtering operation
                if (this.CHKALLOperations.Checked == false)
                {
                    if (SQLCmd.CommandText.Length == 0)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY + " WHERE LOCT_Operation = '" + this.CMBLOCT_Operation.Text + "' ";
                    }
                    else
                    {
                        SQLCmd.CommandText += " AND LOCT_Operation = '" + this.CMBLOCT_Operation.Text + "' ";
                    }
                }

                //check if filtering date
                if (this.CHKAllDates.Checked == false)
                {
                    strDateFrom = this.DTEFrom.Value.Month.ToString() + "/" + this.DTEFrom.Value.Day.ToString() + "/" + this.DTEFrom.Value.Year.ToString();
                    strDateTo = this.DTETo.Value.Month.ToString() + "/" + this.DTETo.Value.Day.ToString() + "/" + this.DTETo.Value.Year.ToString();

                    if (SQLCmd.CommandText.Length == 0)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY + " WHERE LOCT_DateTime BETWEEN '" + strDateFrom + " 00:00:00' AND '" + strDateTo + " 23:59:59'";
                    }
                    else
                    {
                        SQLCmd.CommandText += " AND LOCT_DateTime BETWEEN '" + strDateFrom + " 00:00:00' AND '" + strDateTo + " 23:59:59'";
                    }
                }

                SQLCmd.CommandText += ";"; 
                DADStockItem = new SqlDataAdapter(SQLCmd);

                //get location data
                DSTStockItem = new DataSet();
                DADStockItem.Fill(DSTStockItem, Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY);

                if (DSTStockItem.Tables[Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY].Rows.Count != 0)
                {
                    //set report path
                    RPTStockItemTRN.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_STOCK_ITEMS_TRANSACTIONHISTORY;

                    //sub report datasource
                    SQLCmdLocTRN.Connection = SQLConn;

                    if (this.CMBSTKI_ItemID.Text.Length > 0)
                    {
                        SQLCmdLocTRN.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY_LOTS + " WHERE LOTT_ItemID ='" + this.CMBSTKI_ItemID.Text + "'; "; // + Modules.clsTables.CNST_STR_SELECT_STOCK_MEDIA_ORDERBY;
                    }
                    else
                    {
                        SQLCmdLocTRN.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY_LOTS + ";"; //Modules.clsTables.CNST_STR_SELECT_LOCTRN_ORDERBY;
                    }

                    this.DSTLocTRN = new DataSet();
                    this.DADLocTRN = new SqlDataAdapter(SQLCmdLocTRN);
                    this.DADLocTRN.Fill(DSTLocTRN, "DataSet1");

                    //configure tbltemp for rows
                    TBLTemp = new DataTable();

                    foreach (DataColumn DACCol in DSTLocTRN.Tables["DataSet1"].Columns)
                    {
                        TBLTemp.Columns.Add(DACCol.ColumnName, DACCol.DataType);
                    }

                    this.RPTStockItemTRN.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubReportEventHandler);


                    //main report datasource
                    RDSReportDataSource.Name = "DataSet1";
                    RDSReportDataSource.Value = DSTStockItem.Tables[Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_TRANSACTIONHISTORY];

                    RPTStockItemTRN.LocalReport.DataSources.Clear();
                    RPTStockItemTRN.LocalReport.DataSources.Add(RDSReportDataSource);
                    RPTStockItemTRN.RefreshReport();
                    RPTStockItemTRN.Enabled = true;
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
                  Created 29/08/2025 By Roger Williams



             */
            if (this.CHKALLOperations.Checked)
            {
                this.CMBLOCT_Operation.Text = string.Empty; ;
            }
        }

        private void CHKAllDates_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  Created 29/08/2025 By Roger Williams



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
                  Created 29/08/2025 By Roger Williams


            */

            if (this.CHKALLItems.Checked)
            {
                this.CMBSTKI_ItemID.Text = string.Empty; ;
            }
        }

        private void CMBSTKI_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                   Created 29/08/2025 By Roger Williams

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
                   Created 29/08/2025 By Roger Williams

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
                   Created 29/08/2025 By Roger Williams


            */

            this.CHKALLOperations.Checked = false;
        }

        private void CMBSTKI_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 29/08/2025 By Roger Williams


            */

            this.CHKALLItems.Checked = false;
        }

        private void frmReportStockItemTRNHistoryListing_Shown(object sender, EventArgs e)
        {
            /*
                  Created 29/08/2025 By Roger Williams


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
                  Created 29/08/2025 By Roger Williams



             */

            this.CHKAllDates.Checked = false;
        }

        private void DTETo_ValueChanged(object sender, EventArgs e)
        {
            /*
                  Created 29/08/2025 By Roger Williams



             */

            this.CHKAllDates.Checked = false;
        }

        private void frmReportStockItemTRNHistoryListing_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 29/08/2025 By Roger Williams

              Draws line across screen

            */

            //draw line
            e.Graphics.DrawLine(penTemp, 0, 140, this.Width, 140);

            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
        }

        private void RPTStockItemTRN_ReportError(object sender, ReportErrorEventArgs e)
        {
            e = e;
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
