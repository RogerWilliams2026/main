using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace RogStock2025.Screens
{
    /*
          Created 05/03/2025 By Roger Williams

          Uses one unbound combobox!

          Manually configures report datasource based on filter criteria


     */
    public partial class frmReportUOMListing : Form
    {
        Pen penTemp;
        bool blnShow = true;
        bool blnOk = true;

        public frmReportUOMListing()
        {
            InitializeComponent();
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                  Created 05/03/2025 By Roger Williams


             */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void frmReportUOMListing_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams

              Draws line across screen

            */


            //draw line
            e.Graphics.DrawLine(penTemp, 0, 120, this.Width, 120);
        }

        private void frmReportUOMListing_Load(object sender, EventArgs e)
        {
            /*
                  Created 05/03/2025 By Roger Williams

                  - Sets pen for paint event
                  - Checks if any UOM records
                  - Populates combobox


             */
            if (Modules.clsData.CheckForUOM())
            {
                penTemp = new Pen(Color.Black, 1);
                penTemp.Color = Color.Black;
                Modules.clsData.PopulateComboBoxes(this.CMBSTKU_Desc, Modules.clsTables.CNST_STR_STOCK_UOM, "", "", "", "", "", false);
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
            else
            {
                blnShow = false;
            }
        }

        private void frmReportUOMListing_Shown(object sender, EventArgs e)
        {
            /*
                  Created 05/03/2025 By Roger Williams

                  If no UOM records close form


             */
            if (blnShow == false)
            {
                MessageBox.Show("Cannot Continue No UOM Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void CMBSTKU_Desc_Leave(object sender, EventArgs e)
        {
            /*
                   Created 27/08/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKU_Desc, this.CMBSTKU_Desc.Text))
            {
                this.CHKAll.Checked = false;
            }

            blnOk = false;
        }

        private void CMBSTKU_Desc_SelectedValueChanged(object sender, EventArgs e)
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
                this.CMBSTKU_Desc.Text = string.Empty; ;
            }
        }

        private void BTNShow_Click(object sender, EventArgs e)
        {
            /*
                       Created 05/03/2025 By Roger Williams

                       "prints" the report based on filter criteria


                  */
            //data table vars
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataAdapter DADUOM;
            DataSet DSTUOM;
            ReportDataSource RDSReportDataSource = new ReportDataSource();

            if (this.CHKAll.Checked == false && this.CMBSTKU_Desc.Text == String.Empty)
            {
                MessageBox.Show("No UOM Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    //get UOM data
                    DSTUOM = new DataSet();

                    if (this.CHKAll.Checked)
                    {
                        SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_STOCK_UOM + Modules.clsTables.CNST_STR_SELECT_STOCK_UOM_ORDERBY, SQLConn);
                    }
                    else
                    {
                        SQLCmd = new SqlCommand(" SELECT * FROM " + Modules.clsTables.CNST_STR_STOCK_UOM + " WHERE STKU_Desc = '" + this.CMBSTKU_Desc.Text + "' " + Modules.clsTables.CNST_STR_SELECT_STOCK_UOM_ORDERBY, SQLConn);
                    }

                    DADUOM = new SqlDataAdapter(SQLCmd);
                    DADUOM.Fill(DSTUOM, Modules.clsTables.CNST_STR_STOCK_UOM);
                    this.RPTUOM.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_UOM;

                    if (DSTUOM.Tables[Modules.clsTables.CNST_STR_STOCK_UOM].Rows.Count != 0)
                    {
                        // Must match the DataSource in the RDLC
                        RDSReportDataSource.Name = "DataSet1";
                        RDSReportDataSource.Value = DSTUOM.Tables[Modules.clsTables.CNST_STR_STOCK_UOM];

                        this.RPTUOM.LocalReport.DataSources.Clear();
                        this.RPTUOM.LocalReport.DataSources.Add(RDSReportDataSource);
                        this.RPTUOM.RefreshReport();
                        this.RPTUOM.Enabled = true;
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


    }
}
