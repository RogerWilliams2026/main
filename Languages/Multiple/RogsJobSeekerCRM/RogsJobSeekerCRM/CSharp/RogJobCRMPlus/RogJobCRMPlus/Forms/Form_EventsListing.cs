using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Data.SqlClient;
using System.IO;

namespace RogJobCRMPlus.Forms
{
    public partial class frmEventsListing : Form
    {
        //for manual mouse move of form
        bool blnDragging = false;
        System.Drawing.Point pntLastLocation;

        Pen penTemp;
        string CNST_STR_FIRSTCONTROL = "CMBEventID";

        public frmEventsListing()
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
            this.DTEEVT_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.DTEEVT_DateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //from contact table FROM
            Modules.clsView.PopulateComboBoxes(this.CMBEventID, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, "EVT_ID", "", "", "", "", true,true);
            Modules.clsView.PopulateComboBoxes(this.CMBEVT_Name, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, "EVT_Name", "", "", "", "", true,false);
            Modules.clsView.PopulateComboBoxes(this.CMBEVT_Contact, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, "EVT_Contact", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBEVT_Where, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, "EVT_Where", "", "", "", "", true, false);
            Modules.clsView.PopulateComboBoxes(this.CMBEVT_Website, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS, "EVT_Website", "", "", "", "", true, false);

            this.CHKSummary.Checked = false;
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

            string strCriteriaReport = string.Empty;
            string strCriteriaFields = string.Empty;
            string strSort = string.Empty;
            string strTemp2 = string.Empty;

            DateTime dteReport = DateTime.Now;

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
                //    if (typFields.strTableName == Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS)
                //    {
                //        if ( (typFields.strTableName.IndexOf("_ID") == 0) && (typFields.strTableName != "timestamp"))
                //        { 
                //            strTemp += typFields.strTableName + ", ";
                //        }
                //    }
                //}

                //strTemp = strTemp.Substring(0,strTemp.Length - 1);

                strTemp = "EVT_Date, EVT_Name, EVT_Contact, EVT_Where, EVT_Website, EVT_Booked, EVT_Attended, EVT_Details, EVT_Comments ";
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

                if (this.CHKEVT_DateAll.Checked)
                {
                    strCriteriaReport = "All Dates ";
                }
                else
                {
                    dteFrom = DateTime.Parse(this.DTEEVT_Date.Text);
                    dteTo = DateTime.Parse(this.DTEEVT_DateTo.Text);

                    strCriteriaReport = "Between Dates: " + this.DTEEVT_Date.Text + " and " + this.DTEEVT_DateTo.Text + " ";
                    strCriteriaFields = "EVT_Date BETWEEN '" + dteFrom.Month.ToString() + "/" + dteFrom.Day.ToString() + "/" + dteFrom.Year.ToString() +
                                        "' AND '" + dteTo.Month.ToString() + "/" + dteTo.Day.ToString() + "/" + dteTo.Year.ToString() + "' AND ";
                }

                if (this.CHKEVT_NameAll.Checked)
                {
                    strCriteriaReport += " All Events ";
                }
                else
                {
                    strCriteriaReport += "Name: " + this.CMBEVT_Name.Text + " ";
                    strCriteriaFields += "EVT_Name = '" + this.CMBEVT_Name.Text + "' AND ";
                }

                if (this.CHKEVT_WhereAll.Checked)
                {
                    strCriteriaReport += " All Locations ";
                }
                else
                {
                    strCriteriaReport += "Where: " + this.CMBEVT_Where.Text + " ";
                    strCriteriaFields += "EVT_Where = '" + this.CMBEVT_Where.Text + "' AND ";
                }

                if (this.CHKEVT_ContactAll.Checked)
                {
                    strCriteriaReport += " All Contacts: ";
                }
                else
                {
                    strCriteriaReport += "Contact: " + this.CMBEVT_Contact.Text + " ";
                    strCriteriaFields += "EVT_Contact = '" + this.CMBEVT_Contact.Text + "' AND ";
                }

                if (this.CHKEVT_WebsiteAll.Checked)
                {
                    strCriteriaReport += " All Websites ";
                }
                else
                {
                    strCriteriaReport += "Website: " + this.CMBEVT_Website.Text + " ";
                    strCriteriaFields += "EVT_Website = '" + this.CMBEVT_Website.Text + "' AND ";
                }

                if (this.CHKEVT_AttendedAll.Checked)
                {
                    strCriteriaReport += " All Attended Types ";
                }
                else
                {
                    strTemp = Convert.ToString(this.CHKEVT_Attended.Checked) == "true" ? "1" : "Yes" + "No";
                    strCriteriaReport += "Attended?: " + strTemp + " ";
                    strCriteriaFields += "EVT_Attended = '" + strTemp + "' AND ";
                }


                if (this.CHKEVT_BookedAll.Checked)
                {
                    strCriteriaReport += " All Booked Types ";
                }
                else
                {
                    strTemp = Convert.ToString(this.CHKEVT_Booked.Checked) == "true" ? "1" : "Yes" + "No";
                    strCriteriaReport += "Booked?: " + strTemp + " ";
                    strCriteriaFields += "EVT_Booked = '" + strTemp + "' AND ";
                }

                if (strCriteriaFields.Substring(strCriteriaFields.Length - 5, 5) == " AND ")
                {
                    //keep end space
                    strCriteriaFields = strCriteriaFields.Substring(0, strCriteriaFields.Length - 4);
                }

                if (this.CHKSort.Checked)
                {
                    strSort = "ORDER BY EVT_Date DESC;";
                }
                else
                {
                    strSort = "ORDER BY EVT_Date ASC;";
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

                if (File.Exists(Modules.clsView.CNST_STR_REPORT_JOBS))
                {
                    File.Delete(Modules.clsView.CNST_STR_REPORT_JOBS);
                }

                SQLConn.Open();
                SQLCmd.Connection = SQLConn;

                //get data
                if (strCriteriaFields != string.Empty)
                {
                    SQLCmd.CommandText = "SELECT " + GetQueryFieldsList() + " FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS + " WHERE " + strCriteriaFields + " " + strSort;

                }
                else
                {
                    SQLCmd.CommandText = "SELECT " + GetQueryFieldsList() + " FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS + " " + strSort;
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
                    appWord.ActiveWindow.Selection.TypeText("Events Listing Report " + DateTime.Now);
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
                    strTemp2 = "List Of Events: ";
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
               //         if (this.CHKSummary.Checked)
               //         {
                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 12;  //when next record adds little space
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 0;
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Date: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            dteReport = (DateTime)SQLRead["EVT_Date"];
                            appWord.ActiveWindow.Selection.TypeText(dteReport.ToString("dd/MM/yyyy")); 

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 0;
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Name: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["EVT_Name"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 0;
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.TypeText("Contact: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["EVT_Contact"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Where: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["EVT_Where"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Attended: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            strTemp2 = SQLRead["EVT_Attended"].ToString();
                            strTemp2 = strTemp2 == "false" ? "No" : "Yes";
                            appWord.ActiveWindow.Selection.TypeText(strTemp2); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Booked: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            strTemp2 = SQLRead["EVT_Booked"].ToString();
                            strTemp2 = strTemp2 == "false" ? "No" : "Yes";
                            appWord.ActiveWindow.Selection.TypeText(strTemp2); // + Environment.NewLine);
                                                                               //     }
                                                                               //     else
                        if (this.CHKSummary.Checked == false)
                        {
                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Details:" + Environment.NewLine);
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["EVT_Details"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("What Happened There?:" + Environment.NewLine);
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["EVT_Comments"].ToString()); // + Environment.NewLine);

                            //entering a paragraph break "enter"
                            appWord.Selection.TypeParagraph();
                        }
                    }

                    appWord.ActiveWindow.Selection.GoTo(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine, Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst);
                    //show the document
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
            if (strTemp == "EVT_Date")
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

                        if (strTemp == "EVT_Date")
                        {
                            this.DTEEVT_DateTo.Text = this.DTEEVT_Date.Text;
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

              makes combox box control read only

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
            this.CHKEVT_NameAll.CheckedChanged += CustomCheckedChanged;
            this.CHKEVT_WhereAll.CheckedChanged += CustomCheckedChanged;
            this.CHKEVT_ContactAll.CheckedChanged += CustomCheckedChanged;
            this.CHKEVT_AttendedAll.CheckedChanged += CustomCheckedChanged;
            this.CHKEVT_BookedAll.CheckedChanged += CustomCheckedChanged;
            this.CHKEVT_WebsiteAll.CheckedChanged += CustomCheckedChanged;
            //set form captions
            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_EVENTS);
            //set label for dateto and salaryto to "from" text
            this.LBLEVT_DateTo.Text = this.LBLEVT_Date.Text + " To";
            this.CHKEVT_DateAll.Checked = true;
            //make combobox read only
            this.CMBEVT_Name.KeyDown += CustomKeyDown;
            this.CMBEVT_Contact.KeyDown += CustomKeyDown;
            this.CMBEVT_Website.KeyDown += CustomKeyDown;
            this.CMBEVT_Where.KeyDown += CustomKeyDown;
            this.CMBEventID.KeyDown += CustomKeyDown;
            //handle combobox selected item
            this.CMBEVT_Name.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBEVT_Contact.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBEVT_Website.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBEVT_Where.SelectedValueChanged += CustomSelectedValueChanged;
            this.CMBEventID.SelectedValueChanged += CustomSelectedValueChanged;
            //set form movement limits
            this.Move += Modules.clsView.FormLocationChanged;
        }


        //****form events etc***
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

        private void frmEventsListing_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(penTemp, 0, 300, this.Width, 300);
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

        private void CHKEVT_Booked_CheckedChanged(object sender, EventArgs e)
        {
            this.CHKEVT_BookedAll.Checked = !this.CHKEVT_BookedAll.Checked;
        }

        private void CHKEVT_Attended_CheckedChanged(object sender, EventArgs e)
        {
            this.CHKEVT_AttendedAll.Checked = !this.CHKEVT_AttendedAll.Checked;
        }

        private void frmEventsListing_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            Modules.clsView.RemoveFromOpenForms(this.LBLTitle.Text);
            this.Close();
        }

        private void DTEEVT_Date_ValueChanged(object sender, EventArgs e)
        {
            this.CHKEVT_DateAll.Checked = false;
        }

        private void DTEEVT_DateTo_ValueChanged(object sender, EventArgs e)
        {
            this.CHKEVT_DateAll.Checked = false;
        }


        //****end class
    }
}
