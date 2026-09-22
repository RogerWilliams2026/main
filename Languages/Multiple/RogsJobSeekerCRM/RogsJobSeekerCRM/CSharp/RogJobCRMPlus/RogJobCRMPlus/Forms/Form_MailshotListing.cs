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
    public partial class frmMailshotListing : Form
    {
        //for manual mouse move of form
        bool blnDragging = false;
        System.Drawing.Point pntLastLocation;

        Pen penTemp;
        string CNST_STR_FIRSTCONTROL = "CMBMailshotID";

        public frmMailshotListing()
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
            this.DTEMSH_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.DTEMSH_DateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //from contact table FROM
            Modules.clsView.PopulateComboBoxes(this.CMBMailshotID, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER, "MSH_ID", "", "", "", "", true, true);
            Modules.clsView.PopulateComboBoxes(this.CMBMSH_MailshotName, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER, "MSH_MailshotName", "", "", "", "", true, false);
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
            bool blnFirstRun = true;
			DateTime dteReport = DateTime.Now;
			string strSort = string.Empty;

			//for word
			Microsoft.Office.Interop.Word.Application appWord;
            Object objMissing = System.Reflection.Missing.Value;

            string strCriteriaReport = string.Empty;
            string strCriteriaFields = string.Empty;
            string strTemp2 = string.Empty;
            int intCount = 1;

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
                //    if (typFields.strTableName == Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER)
                //    {
                //        if ( (typFields.strTableName.IndexOf("_ID") == 0) && (typFields.strTableName != "timestamp"))
                //        { 
                //            strTemp += typFields.strTableName + ", ";
                //        }
                //    }
                //}

                //strTemp = strTemp.Substring(0,strTemp.Length - 1);

                strTemp = "MSH_Date, MSH_MailshotName, MSL_CompanyName, MSL_Email, MSH_Comments ";
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

                if (this.CHKMSH_DateAll.Checked)
                {
                    strCriteriaReport = "All Dates ";
                }
                else
                {
                    dteFrom = DateTime.Parse(this.DTEMSH_Date.Text);
                    dteTo = DateTime.Parse(this.DTEMSH_DateTo.Text);

                    strCriteriaReport = "Between Dates: " + this.DTEMSH_Date.Text + " and " + this.DTEMSH_DateTo.Text + " ";
                    strCriteriaFields = "MSH_Date BETWEEN '" + dteFrom.ToString("MM/dd/yyyy") + "' AND '" + dteTo.ToString("MM/dd/yyyy") + "'  AND ";
                }

                if (this.CHKMSH_MailshotNameAll.Checked)
                {
                    strCriteriaReport += " All Mailshot Names ";
                }
                else
                {
                    strCriteriaReport += "Mailshot Name: " + this.CMBMSH_MailshotName.Text + " ";
                    strCriteriaFields += "MSH_MailshotName = '" + this.CMBMSH_MailshotName.Text + "' AND ";
                }

                if (strCriteriaFields.Substring(strCriteriaFields.Length - 5, 5) == " AND ")
                {
                    //keep end space
                    strCriteriaFields = strCriteriaFields.Substring(0, strCriteriaFields.Length - 4);
                }

                if (this.CHKSort.Checked)
				{
					strSort = "ORDER BY MSH_Date DESC;";
				}
				else
				{
					strSort = "ORDER BY MSH_Date ASC;";
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
                    SQLCmd.CommandText = "SELECT " + GetQueryFieldsList() + " FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER + " INNER JOIN " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES +
                                         " ON " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER + ".MSH_ID = " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES+ ".MSH_ID " +
                                         " WHERE " + strCriteriaFields + " " + strSort;

                }
                else
                {
                    SQLCmd.CommandText = "SELECT " + GetQueryFieldsList() + " FROM " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER + " INNER JOIN " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES +
                                         " ON " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER + ".MSH_ID = " + Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_LINES + ".MSH_ID " + strSort;

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
                    appWord.ActiveWindow.Selection.TypeText("Mailshot Listing Report " + DateTime.Now);
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
                    appWord.ActiveWindow.Selection.TypeText("List Of Mailshots");
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

                    strTemp2 = string.Empty;
                    
                    while (SQLRead.Read())
                    {
                        if (strTemp2 != SQLRead["MSH_Date"].ToString())
                        {
                            strTemp2 = SQLRead["MSH_Date"].ToString();

                            //check if NOT first page if true add blank paragraph
                      //      if (appWord.ActiveWindow.Selection.Range.get_Information(Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber) != 1)
                            if (!blnFirstRun)
                            {
                                appWord.ActiveWindow.Selection.TypeParagraph();
                            }

                            //make sure first part is in bold
                            appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 12;  //when next record adds little space
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 0;
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.Size = 11;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Date: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
							//    appWord.ActiveWindow.Selection.TypeText(SQLRead["MSH_Date"].ToString()); // + Environment.NewLine);
							dteReport = (DateTime)SQLRead["MSH_Date"];
							appWord.ActiveWindow.Selection.TypeText(dteReport.ToString("dd/MM/yyyy"));

							//make sure first part is in bold
							appWord.ActiveWindow.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 0;
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Mailshot Name: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["MSH_MailshotName"].ToString()); // + Environment.NewLine);

                            //make sure first part is in bold
                            //entering a paragraph break "enter"
                            appWord.Selection.TypeParagraph();
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 8;
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 0;
                            appWord.ActiveWindow.Selection.Font.Bold = 1;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                            appWord.ActiveWindow.Selection.TypeText("Comments: ");
                            appWord.ActiveWindow.Selection.Font.Bold = 0;
                            appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                            appWord.ActiveWindow.Selection.TypeText(SQLRead["MSH_Comments"].ToString() + Environment.NewLine);

                            //set rest of report font size
                            appWord.ActiveWindow.Selection.Font.Size = 10;
                            appWord.ActiveWindow.Selection.ParagraphFormat.SpaceAfter = 0;
                            blnFirstRun = false;
                        }

                        //make sure first part is in bold
                        appWord.ActiveWindow.Selection.Font.Bold = 1;
                        appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                        appWord.ActiveWindow.Selection.TypeText("Company Name: ");
                        appWord.ActiveWindow.Selection.Font.Bold = 0;
                        appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                        appWord.ActiveWindow.Selection.TypeText(SQLRead["MSL_CompanyName"].ToString()); // + Environment.NewLine);

                        //make sure first part is in bold
                        appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 0;
                        appWord.ActiveWindow.Selection.Font.Bold = 1;
                        appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                        appWord.ActiveWindow.Selection.TypeText("    Email: ");
                        appWord.ActiveWindow.Selection.Font.Bold = 0;
                        appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                        appWord.ActiveWindow.Selection.TypeText(SQLRead["MSL_Email"].ToString()); // + Environment.NewLine);

                        //entering a paragraph break "enter"
                        appWord.Selection.TypeParagraph();
                        intCount += 1;
                    }

                    //write total
                    //make sure first part is in bold
                    appWord.ActiveWindow.Selection.ParagraphFormat.SpaceBefore = 0;
                    appWord.ActiveWindow.Selection.Font.Bold = 1;
                    appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkBlue;
                    appWord.ActiveWindow.Selection.TypeText("Total Companies Emailed: ");
                    appWord.ActiveWindow.Selection.Font.Bold = 0;
                    appWord.ActiveWindow.Selection.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                    appWord.ActiveWindow.Selection.TypeText(intCount.ToString()); // + Environment.NewLine);

                    appWord.ActiveWindow.Selection.GoTo(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine, Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst);

                    if (blnPrint)
                    {
                        //print
                        appWord.ActiveDocument.PrintOut();
                    }
                    else
                    {
                        appWord.ActiveWindow.Selection.GoTo(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToLine, Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToFirst);
                        //show the document
                        appWord.Visible = true;
                    }

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
            if (strTemp == "MSH_Date")
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

                        if (strTemp == "MSH_Date")
                        {
                            this.DTEMSH_DateTo.Text = this.DTEMSH_Date.Text;
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
            this.CHKMSH_MailshotNameAll.CheckedChanged += CustomCheckedChanged;
            //set form captions
            Modules.clsView.SetFormCaptions(this, Modules.clsTables.CNST_STR_TABLE_CRM_MAILSHOT_HEADER);
            //set label for dateto and salaryto to "from" text
            this.LBLMSH_DateTo.Text = this.LBLMSH_Date.Text + " To";
            this.CHKMSH_DateAll.Checked = true;
            //make combobox read only
            this.CMBMSH_MailshotName.KeyDown += CustomKeyDown;
            this.CMBMailshotID.KeyDown += CustomKeyDown;
            //handle combobox selected item
            this.CMBMSH_MailshotName.SelectedValueChanged += CustomSelectedValueChanged;
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

        private void frmMailshotListing_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(penTemp, 0, 160, this.Width, 160);
            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
        }

        private void frmMailshotListing_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void BTNPrint_Click(object sender, EventArgs e)
        {
            CreateWordDocument(false);
        }

        private void BTNPreview_Click(object sender, EventArgs e)
        {
            CreateWordDocument(false);
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            Modules.clsView.RemoveFromOpenForms(this.LBLTitle.Text);
            this.Close();
        }

        private void DTEMSH_Date_ValueChanged(object sender, EventArgs e)
        {
            this.CHKMSH_DateAll.Checked = false;
        }

        private void DTEMSH_DateTo_ValueChanged(object sender, EventArgs e)
        {
            this.CHKMSH_DateAll.Checked = false;
        }

        //****end class
    }
}
