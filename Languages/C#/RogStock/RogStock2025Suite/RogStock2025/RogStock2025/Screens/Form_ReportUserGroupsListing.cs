using Microsoft.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2025.Screens
{
    public partial class frmReportUserGroupsListing : Form
    {
        Pen penTemp;
        bool blnOk = false;
        bool blnShow = true;

        public frmReportUserGroupsListing()
        {
            InitializeComponent();
        }

        private void frmReportUserGroupsListing_Load(object sender, EventArgs e)
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
                Modules.clsData.PopulateComboBoxes(this.CMBUSRGRP_User, Modules.clsTables.CNST_STR_USERGROUPS, Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_USERGROUPS), "", "", "", "", true);
                this.LocationChanged += Modules.clsView.FormLocationChanged;
                //apply system theme
                Modules.clsView.SetTheme(this, null);
            }
            else
            {
                blnShow = false;
            }
        }

        private void frmReportUserGroupsListing_Shown(object sender, EventArgs e)
        {
            /*
                  Created 27/08/2025 By Roger Williams

                  If no curent login records close form


             */

            if (blnShow == false)
            {
                MessageBox.Show("Cannot Continue No User group Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                  Created 27/08/2025 By Roger Williams


             */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void frmReportUserGroupsListing_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 27/08/2025 By Roger Williams

              Draws line across screen

            */


            //draw line
            e.Graphics.DrawLine(penTemp, 0, 120, this.Width, 120);
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
                if (this.CMBUSRGRP_User.Text == String.Empty)
                {
                    MessageBox.Show("No User Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_USERGROUPS + Modules.clsTables.CNST_STR_SELECT_USERGROUPS_ORDERBY, SQLConn);
                    }
                    else
                    {
                        SQLCmd = new SqlCommand(Modules.clsTables.CNST_STR_SELECT_USERGROUPS + " WHERE USRGRP_User = '" + this.CMBUSRGRP_User.Text + "' " + Modules.clsTables.CNST_STR_SELECT_USERGROUPS_ORDERBY, SQLConn);
                    }

                    DADUOM = new SqlDataAdapter(SQLCmd);
                    DADUOM.Fill(DSTUOM, Modules.clsTables.CNST_STR_USERGROUPS);
                    this.RPTUOM.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_USERGROUPS;

                    if (DSTUOM.Tables[Modules.clsTables.CNST_STR_USERGROUPS].Rows.Count != 0)
                    {
                        // Must match the DataSource in the RDLC
                        RDSReportDataSource.Name = "DataSet1";
                        RDSReportDataSource.Value = DSTUOM.Tables[Modules.clsTables.CNST_STR_USERGROUPS];

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
                this.CMBUSRGRP_User.Text = string.Empty; ;
            }
        }

        private void CMBUSRGRP_User_Leave(object sender, EventArgs e)
        {
            /*
                   Created 27/08/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBUSRGRP_User, this.CMBUSRGRP_User.Text))
            {
                this.CHKAll.Checked = false;
            }

            blnOk = false;
        }

        private void CMBUSRGRP_User_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 27/08/2025 By Roger Williams

                   

            */
            this.CHKAll.Checked = false;
            blnOk = true;
        }
    }
}
