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
    public partial class frmReportLotsListing : Form
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

        public frmReportLotsListing()
        {
            InitializeComponent();
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
            SqlDataAdapter DADLot;
            DataSet DSTLot;


            if (this.CHKALLItems.Checked == false && this.CMBSTKI_ItemID.Text == String.Empty)
            {
                MessageBox.Show("No Item Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (this.CHKAllLots.Checked == false && this.CMBLOT_ID.Text == String.Empty)
            {
                MessageBox.Show("No Lot Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    //get Lotation data
                    DSTLot = new DataSet();

                    //check for filter options
                    if (this.CHKALLItems.Checked)
                    {
                        //check if filtering item ID
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_LOT;
                    }
                    else
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_LOT + " AND " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOT + ".LOT_ItemID = '" + this.CMBSTKI_ItemID.Text + "' ";
                    }


                    if (this.CHKAllLots.Checked == false)
                    {
                        SQLCmd.CommandText += " AND " + Modules.clsTables.CNST_STR_TABLE_STOCK_LOT + ".LOT_Nbr = " + this.CMBLOT_ID.Text;
                    }

                    SQLCmd.CommandText += ";"; // Modules.clsTables.CNST_STR_SELECT_STOCK_LOT_ORDERBY;

                    //set report path
                    this.RPTLots.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_LOTS;

                    DSTLot = new DataSet();
                    DADLot = new SqlDataAdapter(SQLCmd);
                    DADLot.Fill(DSTLot, Modules.clsTables.CNST_STR_TABLE_STOCK_LOT);

                    if (DSTLot.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_LOT].Rows.Count != 0)
                    {
                        this.RPTLots.LocalReport.DataSources.Clear();
                        //main report datasource = TBLTemp
                        this.RPTLots.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", DSTLot.Tables[0]));
                        this.RPTLots.RefreshReport();
                        this.RPTLots.Enabled = true;
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

                  "prints" the report based on filter criteria


             */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

 
        private void CMBSTKI_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                   Created 10/03/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
            {
                this.CHKALLItems.Checked = false;
            }
        }



        private void CMBLOT_ID_Leave(object sender, EventArgs e)
        {
            /*
                   Created 10/03/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBLOT_ID, this.CMBLOT_ID.Text))
            {
                this.CHKAllLots.Checked = false;
            }
        }

        private void CMBLOT_ID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 10/03/2025 By Roger Williams


            */
            if (Modules.clsView.ValueInCombobox(this.CMBLOT_ID, this.CMBLOT_ID.Text))
            {
                this.CHKAllLots.Checked = false;
            }
        }

        private void CMBSTKI_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 10/03/2025 By Roger Williams


            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
            {
                this.CHKALLItems.Checked = false;
            }
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

        private void CHKAllLots_CheckedChanged(object sender, EventArgs e)
        {
            /*
                   Created 10/03/2025 By Roger Williams


            */
            if (this.CHKAllLots.Checked)
            {
                this.CMBLOT_ID.Text = string.Empty; ;
            }
        }

        private void frmReportLotsListing_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 5/03/2025 By Roger Williams

              Draws line across screen

            */

            //draw line
            e.Graphics.DrawLine(penTemp, 0, 120, this.Width, 120);

            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);

        }

        private void frmReportLotsListing_Load(object sender, EventArgs e)
        {
            /*
               Created 10/03/2025 By Roger Williams

               If no Lot closes form!
               Else populates comboboxes and opens sql connection

             */
            if (Modules.clsData.CheckForStockItems())
            {
                try
                {
                    Modules.clsView.PopulateComboBoxes(this.CMBLOT_ID, Modules.clsTables.CNST_STR_TABLE_STOCK_LOT, "", "", "", "", "", false, false);
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
