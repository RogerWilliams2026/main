using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
          Created 27/07/2025 By Roger Williams

          Uses one unbound combobox!

          Manually configures report datasource based on filter criteria


     */
    public partial class frmReportLoginsListing : Form
    {
        Pen penTemp;
        bool blnShow = true;
        bool blnOk = true;

        public frmReportLoginsListing()
        {
            InitializeComponent();
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                  Created 27/08/2025 By Roger Williams


             */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void frmReportLoginsListing_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 27/08/2025 By Roger Williams

              Draws line across screen

            */


            //draw line
            e.Graphics.DrawLine(penTemp, 0, 120, this.Width, 120);
        }

        private void frmReportLoginsListing_Load(object sender, EventArgs e)
        {
            /*
                  Created 27/08/2025 By Roger Williams

                  - Sets pen for paint event
                  - Checks if any UOM records
                  - Populates combobox


             */
            if (Modules.clsData.CheckForLogins())
            {
                penTemp = new Pen(Color.Black, 1);
                penTemp.Color = Color.Black;
                Modules.clsData.PopulateComboBoxes(this.CMBLOG_User, Modules.clsTables.CNST_STR_LOGIN, "", "", "", "", "", false);
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
            else
            {
                blnShow = false;
            }
        }

        private void frmReportLoginsListing_Shown(object sender, EventArgs e)
        {
            /*
                  Created 27/08/2025 By Roger Williams

                  If no login records close form


             */

            if (blnShow == false)
            {
                MessageBox.Show("Cannot Continue No Login Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void CMBLOG_User_Leave(object sender, EventArgs e)
        {
            /*
                   Created 27/08/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBLOG_User, this.CMBLOG_User.Text))
            {
                this.CHKAll.Checked = false;
            }

            blnOk = false;
        }

        private void CMBLOG_User_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 27/08/2025 By Roger Williams

                   

            */
            this.CHKAll.Checked = false;
            blnOk = true;


        }

        private void CHKAll_CheckedChanged(object sender, EventArgs e)
        {
            /*
                   Created 27/08/2025 By Roger Williams

                   
            */
            if (this.CHKAll.Checked)
            {
                this.CMBLOG_User.Text = string.Empty; ;
            }
        }

        private void BTNShow_Click(object sender, EventArgs e)
        {
            /*
                       Created 27/08/2025 By Roger Williams

                       "prints" the report based on filter criteria


                  */
            //data table vars
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataAdapter DADUOM;
            DataSet DSTUOM;
            ReportDataSource RDSReportDataSource = new ReportDataSource();


            if (this.CHKAll.Checked == false && this.CMBLOG_User.Text == String.Empty)
            {
                MessageBox.Show("No User Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_LOGIN + Modules.clsTables.CNST_STR_SELECT_LOGIN_ORDERBY, SQLConn);
                    }
                    else
                    {
                        SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_LOGIN + " WHERE LOG_User = '" + this.CMBLOG_User.Text + "' " + Modules.clsTables.CNST_STR_SELECT_LOGIN_ORDERBY, SQLConn);
                    }

                    DADUOM = new SqlDataAdapter(SQLCmd);
                    DADUOM.Fill(DSTUOM, Modules.clsTables.CNST_STR_LOGIN);
                    this.RPTUOM.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_LOGIN;

                    if (DSTUOM.Tables[Modules.clsTables.CNST_STR_LOGIN].Rows.Count != 0)
                    {
                        // Must match the DataSource in the RDLC
                        RDSReportDataSource.Name = "DataSet1";
                        RDSReportDataSource.Value = DSTUOM.Tables[Modules.clsTables.CNST_STR_LOGIN];

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

        private void RPTUOM_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CMBLOG_User_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
