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


/*
      Created 27/07/2025 By Roger Williams

      Uses one unbound combobox!

      Manually configures report datasource based on filter criteria


 */

namespace RogStock2025.Screens
{
    public partial class frmReportCurrentLoginsListing : Form
    {
        bool blnOk = false;
        bool blnShow = true;
        Pen penTemp;

        public frmReportCurrentLoginsListing()
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

            if (this.CHKAll.Checked == false)
            {
                if (this.CMBLOGC_User.Text == String.Empty)
                {
                    MessageBox.Show("No UOM Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
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
                        SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_LOGIN_CURRENT + Modules.clsTables.CNST_STR_SELECT_LOGIN_CURRENT_ORDERBY, SQLConn);
                    }
                    else
                    {                            //SELECT * FROM Login_Current ORDER BY LOGC_User;
                        SQLCmd = new SqlCommand(" SELECT * FROM " + Modules.clsTables.CNST_STR_LOGIN_CURRENT + " WHERE LOGC_User = '" + this.CMBLOGC_User.Text + "' " + Modules.clsTables.CNST_STR_SELECT_LOGIN_CURRENT_ORDERBY, SQLConn);
                    }

                    DADUOM = new SqlDataAdapter(SQLCmd);
                    DADUOM.Fill(DSTUOM, Modules.clsTables.CNST_STR_LOGIN_CURRENT);
                    this.RPTUOM.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_LOGIN_CURRENT;

                    if (DSTUOM.Tables[Modules.clsTables.CNST_STR_LOGIN_CURRENT].Rows.Count != 0)
                    {
                        // Must match the DataSource in the RDLC
                        RDSReportDataSource.Name = "DataSet1";
                        RDSReportDataSource.Value = DSTUOM.Tables[Modules.clsTables.CNST_STR_LOGIN_CURRENT];

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

        private void CHKAll_CheckedChanged(object sender, EventArgs e)
        {
            /*
                   Created 27/08/2025 By Roger Williams

                   
            */
            if (this.CHKAll.Checked)
            {
                this.CMBLOGC_User.Text = string.Empty; ;
            }
        }

        private void CMBLOGC_User_Leave(object sender, EventArgs e)
        {
            /*
                   Created 27/08/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBLOGC_User, this.CMBLOGC_User.Text))
            {
                this.CHKAll.Checked = false;
            }

            blnOk = false;
        }

        private void CMBLOGC_User_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 27/08/2025 By Roger Williams

                   

            */
            this.CHKAll.Checked = false;
            blnOk = true;
        }

        private void frmReportCurrentLoginsListing_Load(object sender, EventArgs e)
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
                Modules.clsData.PopulateComboBoxes(this.CMBLOGC_User, Modules.clsTables.CNST_STR_LOGIN_CURRENT, "LOGC_User", "", "", "", "", true);
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
            else
            {
                blnShow = false;
            }
        }

        private void frmReportCurrentLoginsListing_Shown(object sender, EventArgs e)
        {
            /*
                  Created 27/08/2025 By Roger Williams

                  If no curent login records close form


             */

            if (blnShow == false)
            {
                MessageBox.Show("Cannot Continue No Login Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void frmReportCurrentLoginsListing_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 27/08/2025 By Roger Williams

              Draws line across screen

            */


            //draw line
            e.Graphics.DrawLine(penTemp, 0, 100, this.Width, 100);
        }
    }
}
