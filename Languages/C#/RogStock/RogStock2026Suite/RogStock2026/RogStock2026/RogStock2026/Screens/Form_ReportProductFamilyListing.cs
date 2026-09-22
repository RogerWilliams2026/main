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
    public partial class frmReportProductFamilyListing : Form
    {
        /*
              Created 05/03/2025 By Roger Williams

              "prints" the report based on filter criteria


         */

        Pen penTemp;
        bool blnShow = true;
        bool blnOk = true;

        //for manual mouse move of form
        bool blnDragging = false;
        System.Drawing.Point pntLastLocation;

        public frmReportProductFamilyListing()
        {
            InitializeComponent();
        }

        private void frmReportProductFamilyListing_Load(object sender, EventArgs e)
        {
            /*
                  Created 12/03/2025 By Roger Williams

                  - Checks for rpoduct family records
                  - Sets pen for paint event
                  - Populates combobox


             */
            if (Modules.clsData.CheckForProductFamily())
            {
                penTemp = new Pen(Color.Black, 1);
                penTemp.Color = Color.Black;
                Modules.clsView.PopulateComboBoxes(this.CMBSTKP_ProductFamily, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY, "", "", "", "", "", false, false);
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //set form title
                Modules.clsView.SetFormTitle(this);
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
            else
            {
                blnShow = false;
            }
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                  Created 12/03/2025 By Roger Williams



             */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void frmReportProductFamilyListing_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams

              Draws line across screen

            */

            //draw line
            e.Graphics.DrawLine(penTemp, 0, 110, this.Width, 110);
            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
        }

        private void frmReportProductFamilyListing_Shown(object sender, EventArgs e)
        {
            /*
                  Created 12/03/2025 By Roger Williams

                  If no product family records closes form

             */
            if (blnShow == false)
            {
                MessageBox.Show("Cannot Continue No Product Family Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void BTNShow_Click(object sender, EventArgs e)
        {
            /*
                  Created 12/03/2025 By Roger Williams

                  "prints" the report based on filter criteria


             */
            //data table vars
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataAdapter DADProdFam;
            DataSet DSTProdFam;
            ReportDataSource RDSReportDataSource = new ReportDataSource();

            if (this.CHKAll.Checked == false && this.CMBSTKP_ProductFamily.Text == String.Empty)
            {
                MessageBox.Show("No Product Family Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    //get data
                    DSTProdFam = new DataSet();

                    if (this.CHKAll.Checked)
                    {
                        SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_STOCK_PRODUCTFAMILY, SQLConn); // + Modules.clsTables.CNST_STR_SELECT_STOCK_PRODUCTFAMILY_ORDERBY, SQLConn);
                    }
                    else
                    {
                        SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_STOCK_PRODUCTFAMILY + " WHERE STKP_ProductFamily = '" + this.CMBSTKP_ProductFamily.Text + "' " + Modules.clsTables.CNST_STR_SELECT_STOCK_PRODUCTFAMILY_ORDERBY, SQLConn);
                    }

                    DADProdFam = new SqlDataAdapter(SQLCmd);
                    DADProdFam.Fill(DSTProdFam, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY);
                    this.RPTProdFam.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_PRODUCTFAMILY;

                    if (DSTProdFam.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY].Rows.Count != 0)
                    {
                        // Must match the DataSource in the RDLC
                        RDSReportDataSource.Name = "DataSet1";
                        RDSReportDataSource.Value = DSTProdFam.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY];

                        this.RPTProdFam.LocalReport.DataSources.Clear();
                        this.RPTProdFam.LocalReport.DataSources.Add(RDSReportDataSource);
                        this.RPTProdFam.RefreshReport();
                        this.RPTProdFam.Enabled = true;
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

        private void CMBSTKP_ProductFamily_Leave(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams


            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKP_ProductFamily, this.CMBSTKP_ProductFamily.Text))
            {
                this.CHKAll.Checked = false;
            }

            blnOk = false;
        }

        private void CMBSTKP_ProductFamily_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams


            */
            this.CHKAll.Checked = false;
            blnOk = true;
        }

        private void CHKAll_CheckedChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams


            */
            if (this.CHKAll.Checked)
            {
                this.CMBSTKP_ProductFamily.Text = string.Empty; ;
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
