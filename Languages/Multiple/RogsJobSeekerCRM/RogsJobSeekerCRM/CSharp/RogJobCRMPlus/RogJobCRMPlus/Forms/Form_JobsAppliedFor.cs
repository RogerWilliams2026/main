using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
//
namespace RogJobCRMPlus.Forms
{
    public partial class frmJobsAppliedFor : Form
    {
        //for manual mouse move of form
        bool blnDragging = false;
        System.Drawing.Point pntLastLocation;

        Pen penTemp;
        string CNST_STR_FIRSTCONTROL = "CMBJobID";



        public frmJobsAppliedFor()
        {
            InitializeComponent();
        }


        private void ResetForm(string strKeep, bool blnEnable)
        {
            /*
              Created 25/02/2025 By Roger Williams

             Resets form 
             Enables/Disables form
             Undoes dataset changes

             VARS

             strKeep     - control to leave
             blnEnable   - enable or disable form

            */


            //reset form
         //   Modules.clsView.ResetForm(this, strKeep);
       //     Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            this.DTEJOB_DateApplied.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //populate comboboxes for FROM
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Type, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_CONTRACTTYPE, "", "", "", "", "", false,false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Hours, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_HOURS, "", "", "", "", "", false, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Status, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_STATUS, "", "", "", "", "", false, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Where, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_WHERE, "", "", "", "", "", false, false);
            //from jobs table FROM
            Modules.clsView.PopulateComboBoxes(this.CMBJobID, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_ID", "", "", "", "", true, true);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Company, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Company", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Salary, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Salary", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Title, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Title", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_TownCity, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_TownCity", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Sector, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Sector", "", "", "", "", true, false);

            //populate comboboxes for TO
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Type, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_CONTRACTTYPE, "", "", "", "", "", false, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Hours, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_HOURS, "", "", "", "", "", false, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Status, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_STATUS, "", "", "", "", "", false, false);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_Where, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_WHERE, "", "", "", "", "", false, false);
            //from jobs table TO
       //     Modules.clsView.PopulateComboBoxes(this.CMBJobID, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_ID", "", "", "", "", true);
       //     Modules.clsView.PopulateComboBoxes(this.CMBJOB_Company, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Company", "", "", "", "", true);
            Modules.clsView.PopulateComboBoxes(this.CMBJOB_SalaryTo, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Salary", "", "", "", "", true, false);
            //     Modules.clsView.PopulateComboBoxes(this.CMBJOB_Title, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Title", "", "", "", "", true);
            //     Modules.clsView.PopulateComboBoxes(this.CMBJOB_TownCity, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_TownCity", "", "", "", "", true);
            //     Modules.clsView.PopulateComboBoxes(this.CMBJOB_Sector, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS, "JOB_Sector", "", "", "", "", true);
        }


        private void CreateWordDocument(bool blnPrint)
        {
            /*
              Created 09/03/2026 By Roger Williams

              creates a word document for the data

              VARS

              blnPrint  - print report after creation?

            */

            SqlCommand SQLCmd = new SqlCommand();
            SqlDataReader SQLRead = null;

            //for word
            Microsoft.Office.Interop.Word.Application appWord;
            Object objMissing = System.Reflection.Missing.Value;
            int intLineNumberRange = 0;
            //for Word lines
            // Define line coordinates (in points)
            float fltStartY = 40 * intLineNumberRange; // Vertical start position
            float fltEndY = 40 * intLineNumberRange;   // Vertical end position

            string strCriteriaReport = string.Empty;
            string strCriteriaFields = string.Empty;

            DateTime dteReport = DateTime.Now;
            string strSort = string.Empty;
            string strTemp2 = string.Empty;

            string GetQueryFieldsList()
            {
                /*
                  Created 10/03/2026 By Roger Williams

                  gets full list of fields for use in query from: Modules.clsData.lstTableInfo
                  using a WHERE query. Skips ID and timestamp


                */

                string strTemp = string.Empty;

                //foreach (Modules.clsData.TTYPETableInfo typFields in Modules.clsData.lstTableInfo)
                //{
                //    if (typFields.strTableName == Modules.clsTables.CNST_STR_TABLE_CRM_JOBS)
                //    {
                //        if ( (typFields.strTableName.IndexOf("_ID") == 0) && (typFields.strTableName != "timestamp"))
                //        { 
                //            strTemp += typFields.strTableName + ", ";
                //        }
                //    }
                //}

                //strTemp = strTemp.Substring(0,strTemp.Length - 1);

                strTemp = "JOB_DateApplied, JOB_Company, JOB_Direct, JOB_Title, JOB_TownCity, JOB_Salary, JOB_Sector, JOB_Type, JOB_Hours, JOB_Where, JOB_Status, JOB_Details, JOB_Comments ";
                return strTemp;
            }

            void GetCriteria()
            {
                /*
                  Created 10/03/2026 By Roger Williams

                  puts selected criteria for showing on report into: strCriteriaReport

                  e.g.:

                  Date Range: 01/01/2026 To 12/01/2026 Sector: Technology

                  puts selected critera for query to get data into: strCriteriaFields

                */

                string strTemp = string.Empty;
                DateTime dteFrom = DateTime.Now;
                DateTime dteTo = DateTime.Now;

                strCriteriaReport = string.Empty;
                strCriteriaFields = string.Empty;

                if (this.CHKJOB_DateAppliedAll.Checked)
                {
                    strCriteriaReport = "All Dates ";
                }
                else
                {
                    dteFrom = DateTime.Parse(this.DTEJOB_DateApplied.Text);
                    dteTo= DateTime.Parse(this.DTEJOB_DateAppliedTo.Text);

                    strCriteriaReport = "Between Dates: " + this.DTEJOB_DateApplied.Text + " and " + this.DTEJOB_DateAppliedTo.Text + " ";
                    strCriteriaFields = "JOB_DateApplied BETWEEN '" + dteFrom.ToString("MM/dd/yyyy") + "' AND '" + dteTo.ToString("MM/dd/yyyy") + "'  AND ";
                }

                if (this.CHKJOB_CompanyAll.Checked) 
                {
                    strCriteriaReport += " All Companies ";
                }
                else
                {
                    strCriteriaReport += "Company/Agency: " + this.CMBJOB_Company.Text + " ";
                    strCriteriaFields += "JOB_Company = '" + this.CMBJOB_Company.Text + "' AND ";
                }

                if (this.CHKJOB_DirectAll.Checked)
                {
                    strCriteriaReport += " All Advertised By Company OR Agency ";
                }
                else
                {
                    strCriteriaReport += "All Advertised BY Company NOT Agency ";
                    strCriteriaFields += "JOB_Direct = " + Convert.ToString(this.CHKJOB_Direct.Checked) == "true" ? "1" : "0"  + " AND ";
                }

                if (this.CHKJOB_HoursAll.Checked)
                { 
                    strCriteriaReport += " All Hour Types ";
                }
                    else
                {
                    strCriteriaReport += "Hours: " + this.CMBJOB_Hours.Text + " ";
                    strCriteriaFields += "JOB_Hours = '" + this.CMBJOB_Hours.Text + "' AND ";
                }

                if (this.CHKJOB_SalaryAll.Checked)
                {
                    strCriteriaReport += "All Salaries Between: ";
                }
                else
                {
                    strCriteriaReport += "Salary: " + this.CMBJOB_Salary.Text + " ";
                    strCriteriaFields += "JOB_Salary = BETWEEN '" + this.CMBJOB_Salary.Text + "' AND '" + this.CMBJOB_SalaryTo.Text + "' AND ";
                }

                if (this.CHKJOB_SectorAll.Checked)
                {
                    strCriteriaReport += " All Sectors ";
                }
                else
                {
                    strCriteriaReport += "Sector: " + this.CMBJOB_Sector.Text + " ";
                    strCriteriaFields += "JOB_Sector = '" + this.CMBJOB_Sector.Text + "' AND ";
                }

                if (this.CHKJOB_StatusAll.Checked)
                {
                    strCriteriaReport += " All Statuses ";
                }
                else
                {
                    strCriteriaReport += "Status: " + this.CMBJOB_Status.Text + " ";
                    strCriteriaFields += "JOB_Status = '" + this.CMBJOB_Status.Text + "' AND ";
                }

                if (this.CHKIExcludeApplied.Checked)
                {
                    strCriteriaReport += "Excluding Status: Applied ";
                    strCriteriaFields += "JOB_Status <> 'Applied' AND ";
                }

                if (this.CHKJOB_TitleAll.Checked)
                {
                    strCriteriaReport += " All Job Titles ";
                }
                else
                {
                    strCriteriaReport += "Job Title: " + this.CMBJOB_Title.Text + " ";
                    strCriteriaFields += "JOB_Title = '" + this.CMBJOB_Title.Text + "' AND ";
                }

                if (this.CHKJOB_TownCityAll.Checked)
                {
                    strCriteriaReport += " All Towns/Cities ";
                }
                else
                {
                    strCriteriaReport += "Town/City: " + this.CMBJOB_TownCity.Text + " ";
                    strCriteriaFields += "JOB_TownCity = '" + this.CMBJOB_TownCity.Text + "' AND ";
                }

                if (this.CHKJOB_TypeAll.Checked)
                {
                    strCriteriaReport += " All Job Types ";
                }
                else
                {
                    strCriteriaReport += "Job Type: " + this.CMBJOB_Type.Text + " ";
                    strCriteriaFields += "JOB_Type = '" + this.CMBJOB_Type.Text + "' AND ";
                }

                if (this.CHKJOB_WhereAll.Checked)
                {
                    strCriteriaReport += " All Work Place Types ";
                }
                else
                {
                    strCriteriaReport += "Work Place Type: " + this.CMBJOB_Where.Text + " ";
                    strCriteriaFields += "JOB_Where= '" + this.CMBJOB_Where.Text + "' AND ";
                }

                if (strCriteriaFields.Substring(strCriteriaFields.Length-5,5) == " AND ")
                {
                    //keep end space
                    strCriteriaFields = strCriteriaFields.Substring(0, strCriteriaFields.Length - 4);
                }

                if (this.CHKSort.Checked)
                {
                    strSort = "ORDER BY JOB_DateApplied DESC;";
                }
                else
                {
                    strSort = "ORDER BY JOB_DateApplied ASC;";
                }

            }

            //****end sub/funcs


            GetCriteria();

            if (strCriteriaReport == string.Empty)
            {
                return;
            }

            using (SqlConnection SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
            {

                //if (File.Exists(Modules.clsView.CNST_STR_REPORT_JOBS))
                //{
                //    File.Delete(Modules.clsView.CNST_STR_REPORT_JOBS);
                //}

                SQLConn.Open();
                SQLCmd.Connection = SQLConn;

                //get data
                if (strCriteriaFields != string.Empty)
                {
                    SQLCmd.CommandText = "SELECT " + GetQueryFieldsList() + " FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_JOBS + " WHERE " + strCriteriaFields + " " + strSort;

                }
                else
                { 
                    SQLCmd.CommandText = "SELECT " + GetQueryFieldsList() + " FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_JOBS + " " + strSort;
                }

                SQLRead = SQLCmd.ExecuteReader();

                if (SQLRead != null)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //open word
                    appWord = new Microsoft.Office.Interop.Word.Application();

                    //Create a new document
                    Microsoft.Office.Interop.Word.Document document = appWord.Documents.Add(ref objMissing, ref objMissing, ref objMissing, ref objMissing);

                    //Add header 

                    //setting the focus on the page header
                    appWord.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekCurrentPageHeader;
                    //entering a paragraph break "enter"
                    appWord.Selection.TypeParagraph();
                    //inserting the page numbers centrally aligned in the page footer
                    appWord.Selection.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    appWord.ActiveWindow.Selection.Font.Name = "Arial";
                    appWord.ActiveWindow.Selection.Font.Size = 14;
                    appWord.ActiveWindow.Selection.Font.Bold = 1;
                    appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                    appWord.ActiveWindow.Selection.TypeText("Jobs Applied For Report " + DateTime.Now);
                    //setting focus back to document
                    appWord.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument;


                    //add footer

                    //setting the focus on the page footer
                    appWord.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekCurrentPageFooter;
                    //entering a paragraph break "enter"
                    appWord.Selection.TypeParagraph();
                    //inserting the page numbers centrally aligned in the page footer
                    appWord.Selection.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    appWord.ActiveWindow.Selection.Font.Name = "Arial";
                    appWord.ActiveWindow.Selection.Font.Size = 8;
                    //inserting tab characters
                    appWord.ActiveWindow.Selection.TypeText("\t");
                    appWord.ActiveWindow.Selection.TypeText("\t");
                    appWord.ActiveWindow.Selection.TypeText("Page ");
                    Object CurrentPage = Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage;
                    appWord.ActiveWindow.Selection.Fields.Add(appWord.Selection.Range, ref CurrentPage, ref objMissing, ref objMissing);
                    appWord.ActiveWindow.Selection.TypeText(" of ");
                    Object TotalPages = Microsoft.Office.Interop.Word.WdFieldType.wdFieldNumPages;
                    appWord.ActiveWindow.Selection.Fields.Add(appWord.Selection.Range, ref TotalPages, ref objMissing, ref objMissing);
                    //setting focus back to document
                    appWord.ActiveWindow.ActivePane.View.SeekView = Microsoft.Office.Interop.Word.WdSeekView.wdSeekMainDocument;

                    //add text to document 
                    //Note: skipping TypeParagraph as this inserts new line and start new paragraph
                    appWord.ActiveWindow.Selection.Font.Bold = 1;
                    appWord.ActiveWindow.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineSingle;
                    appWord.ActiveWindow.Selection.Font.Size = 14;
                    appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                    strTemp2 = "List Of Jobs Applied For: ";

                    if (this.CHKSummary.Checked)
                    {
                        strTemp2 += "Summary";
                    }
                    else
                    {
                        strTemp2 += "Detailed";
                    }

                    appWord.ActiveWindow.Selection.TypeText(strTemp2);
                    appWord.ActiveWindow.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;

                    appWord.ActiveWindow.Selection.TypeParagraph();
                    appWord.ActiveWindow.Selection.Font.Bold = 0;
                    appWord.ActiveWindow.Selection.Font.Size = 11;
                    appWord.ActiveWindow.Selection.TypeText("Report Criteria:" + Environment.NewLine);
                    appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                    appWord.ActiveWindow.Selection.TypeText(strCriteriaReport);
                    

                    //no records?
                    if (SQLRead.HasRows == false)
                    {
                        //entering a paragraph break "enter"
                        appWord.Selection.TypeParagraph();
                        //add text to document
                        appWord.ActiveWindow.Selection.TypeParagraph();
                        appWord.ActiveWindow.Selection.Font.Bold = 1;
                        appWord.ActiveWindow.Selection.Font.Size = 14;
                        appWord.ActiveWindow.Selection.TypeText("No Records Found!" + Environment.NewLine);

                    }

                    //make sure nothing else is underlined
                    appWord.ActiveWindow.Selection.Font.Underline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
                    //set rest of report font size
                    appWord.ActiveWindow.Selection.Font.Size = 10;

                    while (SQLRead.Read())
                    {
              //          if (this.CHKSummary.Checked)
              //          {
                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 12;  //when next record adds little space
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 0;
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Date Applied For: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                        //  appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_DateApplied"].ToString()); // + Environment.NewLine);
                            dteReport = (DateTime)SQLRead["JOB_DateApplied"]; 
                            appWord.ActiveWindow.Selection.TypeText(dteReport.ToString("dd/MM/yyyy"));

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 0;
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Direct Contact?: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Direct"].ToString() == "1" ? "Yes" : "No"); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Company/Agency: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Company"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Job Title: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Title"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Town/City: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_TownCity"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Salary: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Salary"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Sector: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Sector"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Type: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Type"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Hours: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Hours"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Work Place Type: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Where"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Status: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Status"].ToString()); // + Environment.NewLine);
                                                                                                       //       }
                                                                                                       //       else
                        if (this.CHKSummary.Checked == false)
                        { 
                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Details:" + Environment.NewLine);
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Details"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Comments:" + Environment.NewLine);
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["JOB_Comments"].ToString()); // + Environment.NewLine);
                                                                                                        //draw separation line
                                                                                                        //intLineNumberRange = appWord.Selection.Range.get_Information(Microsoft.Office.Interop.Word.WdInformation.wdFirstCharacterLineNumber);
                                                                                                        //// Add a line to the document
                                                                                                        //     Microsoft.Office.Interop.Word.Shape line = document.Shapes.AddLine(30, appWord.ActiveWindow.Selection.Range.get_Information(Microsoft.Office.Interop.Word.WdInformation.wdVerticalPositionRelativeToPage), 800, appWord.ActiveWindow.Selection.Range.get_Information(Microsoft.Office.Interop.Word.WdInformation.wdVerticalPositionRelativeToPage));
                                                                                                        //// Optional: Customize the line's appearance
                                                                                                        //    line.Line.Weight = 1; // Thickness of the line
                                                                                                        //    line.Line.ForeColor.RGB = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue); // Line colo
                                                                                                        //entering a paragraph break "enter"
                            appWord.Selection.TypeParagraph();
                        }
                    }

                    appWord.ActiveWindow.Selection.GoTo(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine, Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst);
                    //Save the document
                    appWord.Visible = true;
                    this.Cursor = Cursors.Default;
                }

                SQLRead.Close();
                SQLConn.Close();
                //object filename = Modules.clsView.CNST_STR_REPORT_JOBS;
                //document.SaveAs2(ref filename);
                //document.Close(ref missing, ref missing, ref missing);
                //document = null;
                //appWord.Quit(ref missing, ref missing, ref missing);
                //appWord = null;
            }
        }

        private void CustomCheckedChanged(object sender, EventArgs e)
        {
            /*
              Created 10/03/2026 By Roger Williams

              clears related combox box control if true

            */

            string strTemp = string.Empty;
            Control[] aryTemp;

            strTemp = ((System.Windows.Forms.CheckBox)sender).Name;
            //strip first 3 chars
            strTemp = strTemp.Substring(3, strTemp.Length - 3);
            strTemp = strTemp.Substring(0, strTemp.IndexOf("All"));

            //find combobox with same field name
            if (strTemp == "JOB_DateApplied")
            {
                aryTemp = this.Controls.Find("DTE" + strTemp, true);
            }
            else
            {
                aryTemp = this.Controls.Find("CMB" + strTemp, true);
            }

            //if related control is combobox
            if (aryTemp != null) 
            {
                if (((System.Windows.Forms.CheckBox)sender).Checked)
                {
                    foreach (Control ctlTemp in aryTemp)
                    {
                        if (ctlTemp is ComboBox)
                        {
                            ((ComboBox)ctlTemp).Text = string.Empty;
                        }
                    }
                }
                else
                {
                    //set combobox to first item
                    foreach (Control ctlTemp in aryTemp)
                    {
                        if (ctlTemp is ComboBox)
                        {
                            if (((ComboBox)ctlTemp).Text == string.Empty)
                            { 
                                ((ComboBox)ctlTemp).Text = ((ComboBox)ctlTemp).Items[0].ToString();
                            }
                        }

                        if (strTemp == "JOB_Salary")
                        {
                            this.CMBJOB_SalaryTo.Text = this.CMBJOB_Salary.Text;
                        }

                        if (strTemp == "JOB_DateApplied")
                        {
                            this.DTEJOB_DateAppliedTo.Text = this.DTEJOB_DateApplied.Text;
                        }
                    }
                }
            }
            else
            {
                //in this form if it is NOT a combobox it is a checkbox
                //find combobox with same field name
                aryTemp = this.Controls.Find("CHK" + strTemp, true);

                if (((System.Windows.Forms.CheckBox)sender).Checked)
                {
                    foreach (Control ctlTemp in aryTemp)
                    {
                        ((System.Windows.Forms.CheckBox)ctlTemp).Checked = false;
                    }
                }
                else
                {
                    //set combobox to first item
                    foreach (Control ctlTemp in aryTemp)
                    {
                        ((System.Windows.Forms.CheckBox)ctlTemp).Checked = false;
                    }
                }
            }
        }
        private void CustomKeyDown(object sender, KeyEventArgs e)
        {
            /*
              Created 11/03/2026 By Roger Williams

              makes combobox control read only

            */

            e.SuppressKeyPress = true;
        }

        private void CustomSelectedValueChanged(object sender, EventArgs e)
        {
            /*
              Created 11/03/2026 By Roger Williams

              when combobox item selected set associated checkbox "all" to unchecked

            */

            string strTemp = ((ComboBox)sender).Name.Substring(3, ((ComboBox)sender).Name.Length - 3);
            Control[] aryTemp = null;

            strTemp = "CHK" + strTemp + "All";
            aryTemp = this.Controls.Find(strTemp, true);

            if (aryTemp != null)
            {
                ((System.Windows.Forms.CheckBox)aryTemp[0]).Checked = false;
            }
        }
        private void Init()
        {
            penTemp = new Pen(Color.White);
            ResetForm(CNST_STR_FIRSTCONTROL, false);
            //set each chkALL controls checked changed event
            this.CHKJOB_CompanyAll.CheckedChanged += CustomCheckedChanged;
          //  this.CHKJOB_DateAppliedAll.CheckedChanged += CustomCheckedChanged;
          //  this.CHKJOB_DirectAll.CheckedChanged += CustomCheckedChanged;
            this.CHKJOB_HoursAll.CheckedChanged += CustomCheckedChanged;
            this.CHKJOB_SalaryAll.CheckedChanged += CustomCheckedChanged;
            this.CHKJOB_SectorAll.CheckedChanged += CustomCheckedChanged;
            this.CHKJOB_StatusAll.CheckedChanged += CustomCheckedChanged;
            this.CHKJOB_TitleAll.CheckedChanged += CustomCheckedChanged;
            this.CHKJOB_TownCityAll.CheckedChanged += CustomCheckedChanged;
            this.CHKJOB_TypeAll.CheckedChanged += CustomCheckedChanged;
            this.CHKJOB_WhereAll.CheckedChanged += CustomCheckedChanged;
            //set form captions
            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS);
            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_CONTRACTTYPE);
            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_HOURS);
            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_STATUS);
            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_JOBS_WHERE);
            //set label for dateto and salaryto to "from" text
            this.LBLJOB_DateAppliedTo.Text = this.LBLJOB_DateApplied.Text + " To";
            this.LBLJOB_SalaryTo.Text = this.LBLJOB_Salary.Text + " To";
            this.CHKJOB_DateAppliedAll.Checked = true;
            //make comboboxes read only
            this.CMBJobID.KeyDown += CustomKeyDown;
            this.CMBJOB_Company.KeyDown += CustomKeyDown;
            this.CMBJOB_Hours.KeyDown += CustomKeyDown;
            this.CMBJOB_Salary.KeyDown += CustomKeyDown;
            this.CMBJOB_SalaryTo.KeyDown += CustomKeyDown;
            this.CMBJOB_Sector.KeyDown += CustomKeyDown;
            this.CMBJOB_Status.KeyDown += CustomKeyDown;
            this.CMBJOB_Title.KeyDown += CustomKeyDown;
            this.CMBJOB_Type.KeyDown += CustomKeyDown;
            this.CMBJOB_Where.KeyDown += CustomKeyDown;
            //if combobox item selected uncheck the "all" associated checkbox
            this.CMBJOB_Company.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBJOB_Hours.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBJOB_Salary.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBJOB_SalaryTo.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBJOB_Sector.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBJOB_Status.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBJOB_Title.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBJOB_Type.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBJOB_Where.SelectedValueChanged += CustomSelectedValueChanged;
            //set form movement limits
            this.Move += Modules.clsView.FormLocationChanged;
        }



        //****form events etc***
        private void BTNClose_Click(object sender, EventArgs e)
        {
            Modules.clsView.RemoveFromOpenForms(this.LBLTitle.Text);
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

        private void frmJobsAppliedFor_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void frmJobsAppliedFor_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(penTemp, 0, 420, this.Width, 420);
            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
        }

        private void BTNPrint_Click(object sender, EventArgs e)
        {
            CreateWordDocument(false);
        }

        private void BTNPreview_Click(object sender, EventArgs e)
        {
            CreateWordDocument(false);
        }

        private void CHKJOB_Direct_CheckedChanged(object sender, EventArgs e)
        {
            this.CHKJOB_DirectAll.Checked = !this.CHKJOB_DirectAll.Checked;
        }

        private void CHKJOB_DirectAll_CheckedChanged(object sender, EventArgs e)
        {
            this.CHKJOB_Direct.Checked = !this.CHKJOB_Direct.Checked;
        }

        private void CHKJOB_DateAppliedAll_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void DTEJOB_DateApplied_ValueChanged(object sender, EventArgs e)
        {
            this.CHKJOB_DateAppliedAll.Checked = false;
        }

        private void DTEJOB_DateAppliedTo_ValueChanged(object sender, EventArgs e)
        {
            this.CHKJOB_DateAppliedAll.Checked = false;
        }

        private void CHKIExcludeApplied_CheckedChanged(object sender, EventArgs e)
        {
//            this.CHKJOB_StatusAll.Checked = !this.CHKJOB_StatusAll.Checked;
        }

        private void CMBJOB_Status_SelectedValueChanged(object sender, EventArgs e)
        {
            this.CHKIExcludeApplied.Checked = false;
        }


        //****end class
    }
}
