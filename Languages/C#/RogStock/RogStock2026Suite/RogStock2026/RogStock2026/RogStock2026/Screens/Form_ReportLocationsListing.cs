using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace RogStock2026.Screens
{
    public partial class frmReportLocationsListing : Form
    {


        /*
              Created 10/03/2025 By Roger Williams

              "prints" the report based on filter criteria


         */

        bool blnShow = true;
        Pen penTemp;

        //for manual mouse move of form
        bool blnDragging = false;
        System.Drawing.Point pntLastLocation;

        public frmReportLocationsListing()
        {
            InitializeComponent();
        }

        private void CHKALLItems_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  Created 10/03/2025 By Roger Williams



             */
            if (this.CHKALLItems.Checked)
            {
                this.CMBSTKI_ItemID.Text = string.Empty; ;
            }
        }

        private void CHKAllLocations_CheckedChanged(object sender, EventArgs e)
        {
            /*
                  Created 10/03/2025 By Roger Williams



             */
            if (this.CHKAllLocations.Checked)
            {
                this.CMBLOC_Location.Text = string.Empty; ;
            }
        }

        private void CMBSTKI_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                  Created 10/03/2025 By Roger Williams

                  Checks if typed value in list

             */

            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
            {
                //populate locations for item list
                Modules.clsView.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC, "LOC_ItemID", this.CMBSTKI_ItemID.Text, "", "", "", false, false);
                this.CHKALLItems.Checked = false;
            }
        }

        private void CMBSTKI_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                  Created 10/03/2025 By Roger Williams


            */

            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
            {
                //populate locations for item list
                Modules.clsView.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC, "LOC_ItemID", this.CMBSTKI_ItemID.Text, "", "", "", false, false);
                this.CHKALLItems.Checked = false;
            }
        }

        private void CMBLOC_Location_Leave(object sender, EventArgs e)
        {
            /*
                  Created 10/03/2025 By Roger Williams

                  Check if typed value in list

             */
            if (Modules.clsView.ValueInCombobox(this.CMBLOC_Location, this.CMBLOC_Location.Text))
            {
                this.CHKAllLocations.Checked = false;
            }
        }

        private void CMBLOC_Location_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                  Created 10/03/2025 By Roger Williams



             */
            if (Modules.clsView.ValueInCombobox(this.CMBLOC_Location, this.CMBLOC_Location.Text))
            {
                this.CHKAllLocations.Checked = false;
            }
        }

        private void frmReportLocations_Load(object sender, EventArgs e)
        {
            /*
              Created 10/03/2025 By Roger Williams

              If no locations closes form!
              Else populates comboboxes and opens sql connection

            */
            if (Modules.clsData.CheckForLocations())
            {
                try
                {
                    Modules.clsView.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC, "", "", "", "", "", false, false);
                    Modules.clsView.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS, "", "", "", "", "", false, false);
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
        }



        private void BTNShow_Click(object sender, EventArgs e)
        {
            /*
              Created 10/03/2025 By Roger Williams

              Sets datasource for report sets the main report datasource based on filter criteria

            */

            ReportDataSource RDSReportDataSource = new ReportDataSource();
            SqlCommand SQLCmd = null;
            SqlConnection SQLConn;
            SqlDataAdapter DADLoc;
            DataSet DSTLoc;
            DataTable TBLTemp;
           
            //make sure something selected!
            if (this.CMBSTKI_ItemID.Text.Length == 0 && this.CHKALLItems.Checked == false)
            {
                MessageBox.Show("Need Item ID Or Check ALL Box!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (this.CMBLOC_Location.Text.Length == 0 && this.CHKAllLocations.Checked == false)
            {
                MessageBox.Show("Need Location Or Check ALL Box!!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    //get location data
                    DSTLoc = new DataSet();
                    SQLCmd = SQLConn.CreateCommand();

                    //check for filter options
                    if (this.CHKALLItems.Checked)
                    {
                        //check if filtering item ID
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_LOC;
                    }
                    else
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_LOC + " AND " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOC + ".LOC_ItemID = '" + this.CMBSTKI_ItemID.Text + "' ";
                    }

                    if (this.CHKAllLocations.Checked == false)
                    {

                        SQLCmd.CommandText += " AND " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOC + ".LOC_Location = '" + this.CMBLOC_Location.Text + "'";
                    }

                    SQLCmd.CommandText += ";"; // Modules.clsTables.CNST_STR_SELECT_STOCK_LOC_ORDERBY;

                    //set report path
                    this.RPTLocations.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_LOCATIONS;

                    DSTLoc = new DataSet();
                    DADLoc = new SqlDataAdapter(SQLCmd);
                    DADLoc.Fill(DSTLoc, Modules.clsTables.CNST_STR_TABLE_STOCK_LOC);

                    if (DSTLoc.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOC].Rows.Count != 0)
                    {
                        //link to reports dataset
                      //  TBLTemp = new DSTLocations.Stock_LocDataTable().Clone();
                        TBLTemp = new DataTable();
                        TBLTemp.Columns.Clear();

                        //re-create using fields in query
                        foreach (DataColumn colTemp in DSTLoc.Tables[0].Columns)
                        {
                            TBLTemp.Columns.Add(colTemp.ColumnName);
                        }

                        //populate new table!
                        foreach (DataRow DTRTemp in DSTLoc.Tables[0].Rows)
                        {
                            TBLTemp.Rows.Add(DTRTemp.ItemArray);
                        }

                        this.RPTLocations.LocalReport.DataSources.Clear();
                        //main report datasource = TBLTemp
                        this.RPTLocations.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", TBLTemp));
                        this.RPTLocations.RefreshReport();
                        this.RPTLocations.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No Records Found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error Cannot Retrieve Data!", "Report Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                  Created 10/03/2025 By Roger Williams



             */
            Modules.clsView.RemoveFromOpenForms(this.Text);
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


        private void frmReportLocationsListing_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 5/03/2025 By Roger Williams

              Draws line across screen

            */

            //draw line
            e.Graphics.DrawLine(penTemp, 0, 108, this.Width, 108);
            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
        }
    }
}
