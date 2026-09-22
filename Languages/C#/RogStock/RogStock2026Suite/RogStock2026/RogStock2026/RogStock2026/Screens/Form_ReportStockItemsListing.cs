using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace RogStock2026.Screens
{
    public partial class frmReportStockItemsListing : Form
    {
        /*
              Created 12/03/2025 By Roger Williams

              Uses one unbound comboboxes!

              Manually configures report datasource based on filter criteria


         */

        Pen penTemp;
        bool blnShow = true;
        bool blnOk = true;
        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLCmd = new SqlCommand();
        SqlCommand SQLCmdMedia = new SqlCommand();
        SqlDataAdapter DADItems;
        DataSet DSTItems;
        SqlDataAdapter DADMedia;
        DataSet DSTMedia;

        //for sub report
        DataTable TBLTemp;
        // Must match the name of the DataSource in the report
        ReportDataSource RDSReportDataSource = new ReportDataSource("DataSet1");

        //for manual mouse move of form
        bool blnDragging = false;
        System.Drawing.Point pntLastLocation;


        public frmReportStockItemsListing()
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

            foreach (DataRow DARTemp in DSTMedia.Tables["DataSet1"].Rows)
            {
                if (String.Equals(strItemID, DARTemp["STKM_ItemID"].ToString())) 
                {
                    TBLTemp.ImportRow(DARTemp);
                }
            }

            RDSReportDataSource.Value = TBLTemp;  
            e.DataSources.Clear();
            e.DataSources.Add(RDSReportDataSource);
        }
        //private void EnableSelection(string strWhat)
        //{
        //    /*
        //     Modified 28/08/2025 By Roger Williams

        //     now uses checkboxes

        //     Created 12/03/2025 By Roger Williams

        //      Used by radio buttons to enable filter options
        //      Disables all the others

        //    */


        //    //disable all
        //    this.PANNoFilter.Enabled = false;
        //    this.PANLocLot.Enabled = false;
        //    this.PANProdFam.Enabled = false;
        //    this.PANUOM.Enabled = false;

        //    switch (strWhat)
        //    {
        //        case "nofilter":
        //            this.PANNoFilter.Enabled = true;
        //            break;
        //        case "uom":
        //            this.PANUOM.Enabled = true;
        //            break;
        //        case "prodfam":
        //            this.PANProdFam.Enabled = true;
        //            break;
        //        case "loclot":
        //            this.PANLocLot.Enabled = true;
        //            break;
        //    }

        //}

        private void CustomLeave(object sender, EventArgs e)
        {
            /*
                   Created 12/03/2025 By Roger Williams

                   Event handler for ALL comboboxes makes sure entries not in list
                   are not accepted
                   If ok sets "ALL" checkbox to false

            */
            ComboBox CMBTemp;

            //data already exists as limited to list values
            CMBTemp = (ComboBox)sender;
            //if in list
            if (Modules.clsView.ValueInCombobox(CMBTemp, CMBTemp.Text))
            {
                switch (CMBTemp.Name)
                {
                    case "CMBSTKI_ItemID":
                        this.CHKAllItemID.Checked = false;
                        break;
                    case "CMBSTKU_Desc":
                        this.CHKAllUOM.Checked = false;
                        break;
                    case "CMBSTKP_ProductFamily":
                        this.CHKAllProdFam.Checked = false;
                        break;
                    case "CMBSTKI_ItemIDLocLot":
                        this.CHKAllLocLot.Checked = false;
                        break;
                }
            }

        }

        private void CheckNoFilter()
        {
            /*
               Created 29/08/2025 By Roger Williams

               check in case no filter selected if so use check chknofilter
            */
            byte bytNum = 0;

            bytNum += Convert.ToByte(this.CHKItemID.Checked);
            bytNum += Convert.ToByte(this.CHKUOM.Checked);
            bytNum += Convert.ToByte(this.CHKProdFam.Checked);
            bytNum += Convert.ToByte(this.CHKLocLot.Checked);

            this.CHKNoFilter.Checked = bytNum == 0;
        }

        //**********form events********

        private void BTNShow_Click(object sender, EventArgs e)
        {
            /*
              Modified 28/08/2025 By Roger Williams
            
              Now uses the checkboxes to select filters
              if multiple filters used uses basic/allinfo stock report
              if ONE filter used uses dedicated report

              report list: 

              rptStockItems_UOM                - UOM
              rptStockItems_ProductFamily      - Product Family
              rptStockItems_LocLot             - LocLot
              rptStockItems_Basic              - item ID
              rptStockItems_AllInformation     - item ID

               

              added: chkMedia 

             
              Created 10/03/2025 By Roger Williams

              Sets datasource for report IF no filter configures the media sub report
              Else just sets the main report datasource based on filter criteria

            */
            ReportDataSource RDSReportDataSource = new ReportDataSource();
            bool blnAllInfo = false;
            bool blnUniqueFilter = false;

            void CheckUniqueFilter()
            {
                /*
                   Created 29/08/2025 By Roger Williams

                   check in case ONLY one filter selected if so use THAT dedicated report instead

                   returns

                   true if only only filter active

                */
                byte bytNum = 0;

                bytNum += Convert.ToByte(this.CHKItemID.Checked);
                bytNum += Convert.ToByte(this.CHKUOM.Checked);
                bytNum += Convert.ToByte(this.CHKProdFam.Checked);
                bytNum += Convert.ToByte(this.CHKLocLot.Checked);

                blnUniqueFilter = bytNum == 1;
            }

            if (this.CHKAllItemID.Checked == false && this.CMBSTKI_ItemID.Text == String.Empty)
            {
                MessageBox.Show("No Item Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (this.CHKAllProdFam.Checked == false && this.CMBSTKI_ProductFamily.Text == String.Empty)
            {
                MessageBox.Show("No Product Family Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (this.CHKAllUOM.Checked == false && this.CMBSTKI_UOM.Text == String.Empty)
            {
                MessageBox.Show("No UOM Selected!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            try
            {
                SQLCmd.Connection = SQLConn;
                //get location data
                DSTItems = new DataSet();
                int intBool = 0;
                string strCriteria = String.Empty;

                //get whether to use media info in report or not
                blnAllInfo = this.CHKMedia.Checked;
                //check for just one filter
                CheckUniqueFilter();

                SQLCmd.CommandText = String.Empty;

                //check for filter options
                if (this.CHKNoFilter.Checked)
                {
                    if (blnAllInfo)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_ALLINFORMATION;
                        this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_STOCK_ITEMS_ALLINFORMATION;
                    }
                    else
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_BASIC;
                        this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_STOCK_ITEMS_BASIC;
                    }
                }

                //item ID
                if (this.CHKItemID.Checked)
                {
                    if (blnAllInfo)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_ALLINFORMATION;

                        //if (blnUniqueFilter)
                        //{
                            this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_STOCK_ITEMS_ALLINFORMATION;
                      //  }
                    }
                    else
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_BASIC;

                        //if (blnUniqueFilter)
                        //{
                            this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_STOCK_ITEMS_BASIC;
                       // }
                    }
                    //check if filtering item ID
                    if (this.CHKAllItemID.Checked == false)
                    {
                        strCriteria += " AND " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + ".STKI_ItemID = '" + this.CMBSTKI_ItemID.Text + "' ";
                    }


                }

                if (this.CHKUOM.Checked)
                {
                    if (blnUniqueFilter)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_UOM;
                        this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_UOM;

                        if (this.CHKAllUOM.Checked == false)
                        {
                            strCriteria += " " + Modules.clsTables.CNST_STR_TABLE_STOCK_UOM + ".STKU_Desc = '" + this.CMBSTKI_UOM.Text + "' ";
                        }
                    }
                    else
                    {
                        if (SQLCmd.CommandText.Length == 0)
                        {
                            if (blnAllInfo)
                            {
                                SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_ALLINFORMATION;
                            }
                            else
                            {
                                SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_BASIC;
                            }
                        }

                        if (this.CHKAllUOM.Checked == false)
                        {
                            strCriteria += " AND " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + ".STKI_UOM = '" + this.CMBSTKI_UOM.Text + "' ";
                        }
                    }
                }

                if (this.CHKProdFam.Checked)
                {
                    if (blnUniqueFilter)
                    {
                        SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_PRODUCTFAMILY;
                        this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_PRODUCTFAMILY;

                        if (this.CHKAllProdFam.Checked == false)
                        {
                            strCriteria += " " + Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY + ".STKP_ProductFamily = '" + this.CMBSTKI_ProductFamily.Text + "' ";
                        }
                    }
                    else
                    {
                        if (SQLCmd.CommandText.Length == 0)
                        {
                            if (blnAllInfo)
                            {
                                SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_ALLINFORMATION;
                            }
                            else
                            {
                                SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_BASIC;
                            }
                        }

                        if (this.CHKAllProdFam.Checked == false)
                        {
                            strCriteria += " AND " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + ".STKI_ProductFamily = '" + this.CMBSTKI_ProductFamily.Text + "' ";
                        }
                    }
                }


                if (this.CHKLocLot.Checked)
                {
                    //if (blnUniqueFilter)
                    //{
                    //    SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_LOCLOTTRACKING;
                    //    this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_STOCK_ITEMS_LOCLOT;

                    //    if (this.CHKAllLocLot.Checked == false)
                    //    {
                    //        intBool = this.CHKLocLotSelect.Checked == true ? 1 : 0;

                    //        strCriteria += " " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + ".STKI_LocLot = " + intBool + " ";
                    //    }
                    //}
                    //else
                    //{
                        if (SQLCmd.CommandText.Length == 0)
                        {
                            if (blnAllInfo)
                            {
                                SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_ALLINFORMATION;
                                this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_STOCK_ITEMS_ALLINFORMATION;
                            }
                            else
                            {
                                SQLCmd.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_ITEMS_BASIC;
                                this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_STOCK_ITEMS_BASIC;
                            }
                        }

                        if (this.CHKAllLocLot.Checked == false)
                        {
                            intBool = this.CHKLocLotSelect.Checked == true ? 1 : 0;

                            strCriteria += " AND " + Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS + ".STKI_LocLot = " + intBool + " ";
                        }
             //       }
                }

                //if (SQLCmd.CommandText.IndexOf("WHERE") > 0)
                //{
                //    if (strCriteria.Length > 0)
                //    {
                ////        strCriteria = " AND " + strCriteria;
                //    }
                //}
                //else

                if (SQLCmd.CommandText.IndexOf("WHERE") == -1)
                {
                    {
                        if (strCriteria.Length > 0)
                        {
                            strCriteria = " WHERE " + strCriteria;
                        }
                    }
                }

                    //append ;
                    strCriteria += ";";

                //add filter if none adds ;
                SQLCmd.CommandText += strCriteria;

                //set report path to either with or without media info
                if (this.CHKMedia.Checked)
                {
                    this.RPTItems.LocalReport.ReportPath = Modules.clsData.CNST_STR_REPORT_STOCK_ITEMS_ALLINFORMATION;
                    //sub report datasource
                    this.DSTMedia = new DataSet();
                    SQLCmdMedia.Connection = SQLConn;

                    if (this.CMBSTKI_ItemID.Text.Length > 0)
                    {
                        SQLCmdMedia.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_MEDIA + " WHERE STKM_ItemID ='" + this.CMBSTKI_ItemID.Text + "' " + Modules.clsTables.CNST_STR_SELECT_STOCK_MEDIA_ORDERBY;
                    }
                    else
                    {
                        SQLCmdMedia.CommandText = Modules.clsTables.CNST_STR_SELECT_STOCK_MEDIA + Modules.clsTables.CNST_STR_SELECT_STOCK_MEDIA_ORDERBY;
                    }

                    this.DADMedia = new SqlDataAdapter(SQLCmdMedia);
                    this.DADMedia.Fill(DSTMedia, "DataSet1");


                    //configure tbltemp for rows
                    TBLTemp = new DataTable();

                    foreach (DataColumn DACCol in DSTMedia.Tables["DataSet1"].Columns)
                    {
                        TBLTemp.Columns.Add(DACCol.ColumnName, DACCol.DataType);
                    }

                    this.RPTItems.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubReportEventHandler);
                }

                //main report query
                DADItems = new SqlDataAdapter(SQLCmd);
                DADItems.Fill(DSTItems, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS);

                if (DSTItems.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS].Rows.Count != 0)
                {
                    //main report datasource
                    RDSReportDataSource.Name = "DataSet1";
                    RDSReportDataSource.Value = DSTItems.Tables[Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS];

                    this.RPTItems.LocalReport.DataSources.Clear();
                    this.RPTItems.LocalReport.DataSources.Add(RDSReportDataSource);
                    this.RPTItems.RefreshReport();
                    this.RPTItems.Enabled = true;
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

        private void BTNClose_Click(object sender, EventArgs e)
        {
            /*
                  Created 10/03/2025 By Roger Williams



             */
            Modules.clsView.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void frmReportStockItemsListing_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 05/03/2025 By Roger Williams

              Draws line across screen

            */

            //draw line
            e.Graphics.DrawLine(penTemp, 0, 240, this.Width, 240);

            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
        }

        private void frmReportStockItemsListing_Shown(object sender, EventArgs e)
        {
            /*
              Created 10/03/2025 By Roger Williams

              If no stock items closes form!
     

            */
            if (blnShow == false)
            {
                MessageBox.Show("Cannot Continue No Stock Item Records Found", "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BTNClose_Click(sender, e);
            }
        }

        private void CMBSTKI_ItemID_Leave(object sender, EventArgs e)
        {
            /*
                   Created 12/03/2025 By Roger Williams

                   If typed value in list enable form

            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
            {
                this.CHKAllItemID.Checked = false;
            }

            blnOk = false;
        }

        private void CMBSTKI_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            this.CHKAllItemID.Checked = false;
            blnOk = true;
        }

        private void CMBSTKI_ItemID_SelectedValueChanged_1(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ItemID, this.CMBSTKI_ItemID.Text))
            {
                this.CHKAllItemID.Checked = false;
            }
        }

        private void CHKNoFilter_CheckedChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   if checked disable all filters

            */
            if (this.CHKNoFilter.Checked)
            {
                this.CHKItemID.Checked = false;
                this.CHKLocLot.Checked = false;
                this.CHKProdFam.Checked = false;
                this.CHKUOM.Checked = false;
                this.PANLocLot.Enabled = false;
                this.PANProdFam.Enabled = false;
                this.PANUOM.Enabled = false;
                this.PANItemID.Enabled = false;
            }
        }

        private void CHKUOM_CheckedChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            if (this.CHKUOM.Checked)
            {
                this.PANUOM.Enabled = true;
                this.CHKNoFilter.Checked = false;
            }
            else
            {
                this.PANUOM.Enabled = false;
                CheckNoFilter();
            }

        }

        private void CHKProdFam_CheckedChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            if (this.CHKProdFam.Checked)
            {
                this.CHKNoFilter.Checked = false;
                this.PANProdFam.Enabled = true;
            }
            else
            {
                this.PANProdFam.Enabled = false;
                CheckNoFilter();
            }
        }

        private void CHKItemID_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CHKItemID.Checked)
            {
                this.CHKNoFilter.Checked = false;
                this.PANItemID.Enabled = true;
            }
            else
            {
                this.PANItemID.Enabled = false;
                CheckNoFilter();
            }
        }

        private void CHKLocLot_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CHKLocLot.Checked)
            {
                this.CHKNoFilter.Checked = false;
                this.PANLocLot.Enabled = true;
            }
            else
            {
                this.PANLocLot.Enabled = false;
                CheckNoFilter();
            }
        }

        private void CMBSTKU_UOM_Leave(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_UOM, this.CMBSTKI_UOM.Text))
            {
                this.CHKAllUOM.Checked = false;
            }
        }

        private void CMBSTKP_ProductFamily_Leave(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ProductFamily, this.CMBSTKI_ProductFamily.Text))
            {
                this.CHKAllProdFam.Checked = false;
            }
        }

        private void CMBSTKU_UOM_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_UOM, this.CMBSTKI_UOM.Text))
            {
                this.CHKAllUOM.Checked = false;
            }
        }

        private void CMBSTKP_ProductFamily_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            if (Modules.clsView.ValueInCombobox(this.CMBSTKI_ProductFamily, this.CMBSTKI_ProductFamily.Text))
            {
                this.CHKAllProdFam.Checked = false;
            }
        }

        private void frmReportStockItemsListing_Load(object sender, EventArgs e)
        {
            /*
               Created 10/03/2025 By Roger Williams

               If no stock items closes form!
               Else populates comboboxes and sets combobox eventhandlers
               and opens sql connection

             */
            if (Modules.clsData.CheckForStockItems())
            {
                penTemp = new Pen(Color.Black, 1);
                penTemp.Color = Color.Black;


                try
                {
                    SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
                    SQLConn.Open();
                    Modules.clsView.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsTables.CNST_STR_TABLE_STOCK_ITEMS, "", "", "", "", "", false, false);
                    Modules.clsView.PopulateComboBoxes(this.CMBSTKI_UOM, Modules.clsTables.CNST_STR_TABLE_STOCK_UOM, "", "", "", "", "", false, false);
                    Modules.clsView.PopulateComboBoxes(this.CMBSTKI_ProductFamily, Modules.clsTables.CNST_STR_TABLE_STOCK_PRODUCTFAMILY, "", "", "", "", "", false, false);
                    //set event handlers
                    this.CMBSTKI_ItemID.Leave += CustomLeave;
                    this.CMBSTKI_ItemID.SelectedValueChanged += CustomLeave;
                    this.CMBSTKI_ProductFamily.Leave += CustomLeave;
                    this.CMBSTKI_ProductFamily.SelectedValueChanged += CustomLeave;
                    this.CMBSTKI_UOM.Leave += CustomLeave;
                    this.CMBSTKI_UOM.SelectedValueChanged += CustomLeave;
                    this.LocationChanged += Modules.clsView.FormLocationChanged;
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


        private void CHKAllItemID_CheckedChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            if (this.CHKAllItemID.Checked)
            {
                this.CMBSTKI_ItemID.Text = string.Empty;
            }
        }

        private void CHKAllUOM_CheckedChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams



            */
            if (this.CHKAllUOM.Checked)
            {
                this.CMBSTKI_UOM.Text = string.Empty;
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
