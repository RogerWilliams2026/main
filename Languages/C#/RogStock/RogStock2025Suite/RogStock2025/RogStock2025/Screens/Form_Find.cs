using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
/*
    Created 12/08/2025 By Roger Williams

    Design taken from Access97 version
    
    Uses the 3 dictionaries (primary key etc) for available columns to search 
    Uses datagrid bound to to result query for result
    Passes back primary key value  to calling forms public var: pubstrFindValue

    Uses constructor to get calling form name and search type to use

*/
namespace RogStock2025.Screens
{
    public partial class frmFind : Form
    {

        private string strSearchName  = String.Empty;

        //for datagrid
        BindingSource BDSQuery = null;

        //for line
        Brush BRUTemp = new SolidBrush(Color.Black);
        Pen PENTemp = new Pen(Color.Black);

        public frmFind(string strSearch)
        {
            InitializeComponent();
            strSearchName = strSearch;
        }

        /*
          
           To Do:

           limit TXTField to char width of field
           
        */

        //**custom subs/funcs

        private void Custom_CheckedChanged(object sender, EventArgs e)
        {
            /*
              Created 12/08/2025 By Roger Williams

              used by checkboxes for selecting panels
              
            */
            string strTemp = String.Empty;

            strTemp = ((CheckBox)sender).Name.Substring(((CheckBox)sender).Name.Length - 1, 1);

            if (((CheckBox)sender).Checked)
            {
                if (this.Controls["PANField" + strTemp].Controls["LBLField" + strTemp].Tag != null)
                {
                    this.Controls["PANField" + strTemp].Enabled = ((CheckBox)sender).Checked;
                }
                else
                {
                    ((CheckBox)sender).Checked = false;
                }
            }
            else
            {
                ((CheckBox)sender).Checked = false;
                this.Controls["PANField" + strTemp].Enabled = false;
            }
        }

        private void CustomKeyDown(object sender, KeyEventArgs e)
        {
            /*
              Created 12/08/2025 By Roger Williams

              used by string based controls to stop text entry

              
              
            */

            e.SuppressKeyPress = true;
        }

        private void PopulateForm()
        {
            /*
              Created 12/08/2025 By Roger Williams

              Populates <field> controls from find_fieldinfo table

              for this application only first 3 (if therer are that many) columns are used
              that is ignore column 0 (ID)

              create data controls dynamically as if numeric/date want to use appropriate control!

              also populates cmbfield<number.comparison combobox from modules.clsdata.lstFindOperators
              
            */

            string strTemp1 = String.Empty;
            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            //form controls for column choice
            int intControlNumber = 1;

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_FIND_FIELDINFO + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_FIND_FIELDINFO) + " = '" + strSearchName + "' ORDER BY FFI_Order;";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        //label for telling user what column is available
                        ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Text
                                = SQLRead["FFI_LabelText"].ToString();

                        ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left = 10;
                        ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Top = 10;

                        //set tag to field name
                        ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Tag
                            = SQLRead["FFI_TableName"].ToString() + "." + SQLRead["FFI_FieldName"].ToString();

                        //use datatype to create appropirate value control
                        switch (SQLRead["FFI_FieldDataType"].ToString())
                        {
                            //To Do correct text box widths, cmbcomparision position
                            case "string":
                                {
                                    //set max text size to fields length
                                    ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                             .MaxLength = (int)SQLRead["FFI_FieldLength"];
                                    //set tag to data type (needed for creating SQl query to get data)
                                    ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                             .Tag = SQLRead["FFI_FieldDataType"].ToString();
                                    //set width if >= 50 /2 if <50 show as 25 / 10

                                    if (((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                          .MaxLength >= 50)
                                    {
                                        if (((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                              .MaxLength == 50)
                                        {
                                            ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                                .Width =
                                                (((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).MaxLength / 2) * 10;
                                        }
                                        else
                                        {
                                            //limit to 25 characters
                                            ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                                .Width = 250;
                                        }
                                    }
                                    else
                                    {
                                        ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                            .Width =
                                            ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).MaxLength * 10;

                                    }

                                    //set position
                                    ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).Top = 8;
                                    //position comparison combobox and value control
                                    this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Left
                                        = ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
                                        + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;


                                    ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).Left =
                                        this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Left
                                                 + this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Width + 10;
                                    //     this.Controls["PANField" + intControlNumber.ToString()].Controls.Add(TXTTemp);
                                    ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).Visible = true;
                                    break;
                                }
                            case "datetime":
                                {
                                    //two datetimepickers are used from and to

                                    //set tag to data type (needed for creating SQl query to get data)
                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString()]).Tag = SQLRead["FFI_FieldDataType"].ToString();

                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString()]).Top = 8;
                                    //position comparison combobox and value control
                                    //this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Left =
                                    //    ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
                                    //    + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;

                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString()]).Left
                                        = this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString() + "Comparison"].Left
                                        + this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString() + "Comparison"].Width + 10;
                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString()]).Width = 280;

                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString()]).Visible = true;


                                    //set tag to data type (needed for creating SQl query to get data)
                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Tag = SQLRead["FFI_FieldDataType"].ToString();

                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Top = 8;
                                    //position comparison combobox
                                    //this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Left =
                                    //    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Left
                                    //    + ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Width + 20;

                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Left
                                        = this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"].Left
                                        + this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"].Width + 10;
                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Width = 280;

                                    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Visible = true;
                                    break;
                                }
                            case "numeric":
                                {
                                    //set max text size to fields length
                                    ((NumericUpDown)this.Controls["PANField" + intControlNumber.ToString()].Controls["NUDField" + intControlNumber.ToString()]).Maximum = (int)SQLRead["FFI_FieldLength"];
                                    ((NumericUpDown)this.Controls["PANField" + intControlNumber.ToString()].Controls["NUDField" + intControlNumber.ToString()]).DecimalPlaces = (int)SQLRead["FFI_DecimalPlaces"];                                    //     NUDTemp.Name = "NUDField" + intControlNumber.ToString() + "Value";
                                    //set tag to data type (needed for creating SQl query to get data)
                                    ((NumericUpDown)this.Controls["PANField" + intControlNumber.ToString()].Controls["NUDField" + intControlNumber.ToString()]).Tag = SQLRead["FFI_FieldDataType"].ToString();
                                    ((NumericUpDown)this.Controls["PANField" + intControlNumber.ToString()].Controls["NUDField" + intControlNumber.ToString()]).Top = 10;
                                    ((NumericUpDown)this.Controls["PANField" + intControlNumber.ToString()].Controls["NUDField" + intControlNumber.ToString()]).Width = 100;
                                    //position comparison combobox and value control
                                    this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Left =
                                        ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
                                        + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;

                                    ((NumericUpDown)this.Controls["PANField" + intControlNumber.ToString()].Controls["NUDField" + intControlNumber.ToString()]).Left = this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Left
                                                 + this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Width + 10;
                                    ((NumericUpDown)this.Controls["PANField" + intControlNumber.ToString()].Controls["NUDField" + intControlNumber.ToString()]).Visible = true;
                                    break;
                                }
                            case "bool":
                                {
                                    //set tag to data type (needed for creating SQl query to get data)
                                    ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Tag = SQLRead["FFI_FieldDataType"].ToString();
                                    ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Top = 10;
                                    ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Width = 15;
                                    //position comparison combobox and value control
                                    this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Left =
                                        ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
                                        + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;

                                    ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Left = this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Left
                                                 + this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Width + 10;
                                    ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Visible = true;

                                    //hide comparison combobox if checkbow shown
                                    this.Controls["PANField" + intControlNumber.ToString()].Controls["CMBField" + intControlNumber.ToString() + "Comparison"].Visible = false;
                                    break;
                                }
                        }


                        //set for next line
                        intControlNumber++;
                    }

                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //populate comboboxes with search operators
            this.CMBField1Comparison.Items.Clear();
            this.CMBField2Comparison.Items.Clear();
            this.CMBField3Comparison.Items.Clear();
            this.CMBField4Comparison.Items.Clear();
            this.CMBField5Comparison.Items.Clear();
            this.CMBField6Comparison.Items.Clear();
            //get comparison operators
            this.CMBField1Comparison.Items.AddRange(Modules.clsData.lstFindOperators.ToArray());
            this.CMBField2Comparison.Items.AddRange(Modules.clsData.lstFindOperators.ToArray());
            this.CMBField3Comparison.Items.AddRange(Modules.clsData.lstFindOperators.ToArray());
            this.CMBField4Comparison.Items.AddRange(Modules.clsData.lstFindOperators.ToArray());
            this.CMBField5Comparison.Items.AddRange(Modules.clsData.lstFindOperators.ToArray());
            this.CMBField6Comparison.Items.AddRange(Modules.clsData.lstFindOperators.ToArray());
        }

        private string GetSQL()
        {
            /*
              Created 12/08/2025 By Roger Williams

              converted from Access MRP system

              compiles the SQL string needed from the search datagrid from the SQL Server serach tables

              in find realtions table:

              qualifiedjoin = INNER JOIN..
              searchfiled = fields from JOINed table to also show in results

            */


            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;

            string strSQL = "SELECT ";
            string strFields = String.Empty;
            string strFieldValues = String.Empty;
            bool blnOk = false;

            void GetFieldValues()
            {
                /*
                  Created 13/08/2025 By Roger Williams

                 Creates list of field names and values and comparisons for query stores in strFieldValues

                 Note: checkboxes have no comparision use checked value

                */

                string strTemp1 = String.Empty;
                string strTemp2 = String.Empty;
                string strLike = " LIKE ";
                int intNum = 1;

                for (intNum = 1; intNum != 6; intNum++)
                {
                    if (this.Controls["PANField" + intNum.ToString()].Enabled)
                    {
                        //add field name
                        strTemp1 = ((Label)this.Controls["PANField" + intNum.ToString()].Controls["LBLField" + intNum.ToString()]).Tag + " ";

                        //get operator
                        if (((ComboBox)this.Controls["PANField" + intNum.ToString()].Controls["CMBField" + intNum.ToString() + "Comparison"]).Visible)
                        {
                            //add operator first check if * (like)
                            if (((ComboBox)this.Controls["PANField" + intNum.ToString()].Controls["CMBField" + intNum.ToString() + "Comparison"]).Text == "LIKE")
                            {
                                strTemp2 += strLike;
                            }
                            else
                            {
                                strTemp2 += " " + ((ComboBox)this.Controls["PANField" + intNum.ToString()].Controls["CMBField" + intNum.ToString() + "Comparison"]).Text + " ";
                            }
                        }

                        //get value 
                        if (((TextBox)this.Controls["PANField" + intNum.ToString()].Controls["TXTField" + intNum.ToString()]).Visible)
                        {

                            if (strTemp2 == strLike)
                            {
                                strTemp1 += strLike + "'%" + ((TextBox)this.Controls["PANField" + intNum.ToString()].Controls["TXTField" + intNum.ToString()]).Text
                                             + "%' AND ";
                            }
                            else
                            {
                                strTemp1 += strTemp2 + "'" + ((TextBox)this.Controls["PANField" + intNum.ToString()].Controls["TXTField" + intNum.ToString()]).Text
                                             + "' AND ";
                            }
                        }
                        if (((CheckBox)this.Controls["PANField" + intNum.ToString()].Controls["CHKField" + intNum.ToString()]).Visible)
                        {
                            if ((((CheckBox)this.Controls["PANField" + intNum.ToString()].Controls["CHKField" + intNum.ToString()]).Checked))
                            {
                                strTemp1 += " = 1 AND ";
                            }
                            else
                            {
                                strTemp1 += " = 0 AND ";
                            }
                        }
                        if (((NumericUpDown)this.Controls["PANField" + intNum.ToString()].Controls["NUDField" + intNum.ToString()]).Visible)
                        {
                            if (strTemp2 == strLike)
                            {
                                strTemp1 += strLike + "%" + ((NumericUpDown)this.Controls["PANField" + intNum.ToString()].Controls["NUDField" + intNum.ToString()]).Value.ToString()
                                             + "% AND ";
                            }
                            else
                            {
                                strTemp1 += strTemp2 + ((NumericUpDown)this.Controls["PANField" + intNum.ToString()].Controls["NUDField" + intNum.ToString()]).Value.ToString()
                                             + " AND ";
                            }
                        }
                        if (((DateTimePicker)this.Controls["PANField" + intNum.ToString()].Controls["DTPField" + intNum.ToString() + "From"]).Visible)
                        {
                            //Note: TWO datetimepickers are used: from and to

                            strTemp2 = " BETWEEN '" + ((DateTimePicker)this.Controls["PANField" + intNum.ToString()].Controls["DTPField" + intNum.ToString() + "From"]).Text +
                                       "' AND '" + ((DateTimePicker)this.Controls["PANField" + intNum.ToString()].Controls["DTPField" + intNum.ToString() + "To"]).Text + "' ";

                            strTemp1 += strTemp2;
                        }

                        strFieldValues += strTemp1;
                    }
                }

                //strip trailing: AND
                strFieldValues = strFieldValues.Substring(0, strFieldValues.Length - 4);
            }

            bool GetFields()
            {
                /*
                  Created 13/08/2025 By Roger Williams

                  Creates list of fields for query stores in strFields uses LBLField<num> for alias

                  Note: also returns the primary key as regardless of user preference need to pass it back so calling form
                        can find record!
                */

                string strTemp = Modules.clsTables.GetPrimaryField(SQLRead["FFR_TableName"].ToString()); //get primary key
                int intNum = 1;
                bool blnOk1 = false;

                for (intNum = 1; intNum != 6; intNum++)
                {
                    if (this.Controls["PANField" + intNum.ToString()].Enabled)
                    {
                        //set ok
                        blnOk1= true;
                        //add field name
                        strFields += ((Label)this.Controls["PANField" + intNum.ToString()].Controls["LBLField" + intNum.ToString()]).Tag + " AS [" +
                                     ((Label)this.Controls["PANField" + intNum.ToString()].Controls["LBLField" + intNum.ToString()]).Text + "], ";
                    }
                }

                strFields = strFields.Substring(0, strFields.Length - 2);

                //check primary key in fields list
                if (!strFields.Contains(strTemp))
                {
                    //add it!
                    strFields = strTemp + " AS [" +
                                ((Label)this.Controls["PANField1"].Controls["LBLField1"]).Text + "], " +  strFields;
                }

                return blnOk1;
            }




            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    //get search details e.g. orderby/distinct etc
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_FIND_RELATIONS + " WHERE " + Modules.clsTables.GetPrimaryField(Modules.clsTables.CNST_STR_FIND_RELATIONS) + " = '" + strSearchName + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        //get fields list
                        blnOk = GetFields();

                        if (blnOk)
                        {
                            //get field values
                            GetFieldValues();

                            if ((bool)SQLRead["FFR_Distinct"] == true)
                            {
                                strSQL += " DISTINCT " + strFields;
                            }
                            else
                            {
                                strSQL += strFields;
                            }

                            //any other fields from other tables to add?
                            if (SQLRead["FFR_QualifiedJoin"].ToString().Length != 0)
                            {
                                strSQL += ", " + SQLRead["FFR_OtherTableFields"].ToString();
                            }

                            //add table
                            strSQL += " FROM " + SQLRead["FFR_TableName"].ToString() + " ";

                            if (SQLRead["FFR_QualifiedJoin"].ToString().Length != 0)
                            {
                                strSQL += " " + SQLRead["FFR_QualifiedJoin"].ToString() + " ";
                            }

                            //add values and orderby/groupby
                            if (SQLRead["FFR_OrderBy"].ToString().Length != 0)
                            {
                                strSQL += " WHERE " + strFieldValues;
                                strSQL += " ORDER BY " + SQLRead["FFR_OrderBy"].ToString() + ";";
                            }
                            else
                            {
                                strSQL += " HAVING " + strFieldValues;
                                strSQL += " GROUP BY " + SQLRead["FFR_GroupBy"].ToString() + ";";
                            }
                        }
                    }

                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (blnOk)
            {
                return strSQL;
            }
            else
            {
                return ""; 
            }

            //If Me.CMBField2.Enabled Then strField = strField & "|" & Me.CMBField2
            //If Me.CMBField3.Enabled Then strField = strField & "|" & Me.CMBField3
            //If Me.CMBField4.Enabled Then strField = strField & "|" & Me.CMBField4
            //If CheckUse2(fndFind.FindName, strField) = False Then
            //rstTemp.Open "SELECT * FROM UsysFindRelations WHERE searchtype='" & fndFind
            //.FindName & "'", adoConn, adOpenDynamic
            //Else
            //rstTemp.Open "SELECT * FROM UsysFindRelations WHERE searchtype='" & fndFind
            //.FindName & "2'", adoConn, adOpenDynamic
            //End If
            //'modified 06/02/2018 added top 5000 filter
            //'If rstTemp!distinctsearch Then
            //' strTemp = "SELECT DISTINCT TOP 5000 " & rstTemp.Fields("searchfields").Val
            //ue & " FROM "
            //'Else
            //' strTemp = "SELECT TOP 5000 " & rstTemp.Fields("searchfields").Value & " FR
            //OM "
            //'End If
            //'If rstTemp!distinctsearch Then
            //' strTemp = "SELECT DISTINCT TOP 5000 " & rstTemp.Fields("searchfields").Val
            //ue & " FROM "
            //'Else
            //strTemp = "SELECT " & rstTemp.Fields("searchfields").Value & " FROM "
            //'End If
            //If rstTemp!groupby Then
            //'set group by
            //blnGroupBy = True
            //strGroupbyFields = rstTemp.Fields("searchfields").Value
            //End If
            //If Nz(rstTemp.Fields("qualifiedjoin").Value, "")  = String.Empty; Then
            //strTemp = strTemp & " " & rstTemp.Fields("tablename").Value
            //Else
            //strTemp = strTemp & " " & rstTemp.Fields("qualifiedjoin").Value
            //End If
            //If Nz(rstTemp.Fields("searchcondition").Value, "") <> "" Then
            //If InStr(1, rstTemp.Fields("searchcondition").Value, "%") = 0 Then
            //strTemp = strTemp & " WHERE " & rstTemp.Fields("searchcondition").Value
            //& " "
            //Else
            //strTemp2 = rstTemp.Fields("searchcondition").Value
            //If InStr(1, strTemp2, "%1%") <> 0 Then
            //strTemp2 = Mid$(strTemp2, 1, InStr(1, strTemp2, "%1%") - 1) & Chr(34)
            //& fndFind.strLiteral1 & Chr(34) & " " & Mid$(strTemp2, InStr(1, strTemp2, "%1
            //%") + 3, Len(strTemp2))
            //End If
            //If InStr(1, strTemp2, "%2%") <> 0 Then
            //strTemp2 = Mid$(strTemp2, 1, InStr(1, strTemp2, "%2%") - 1) & Chr(34)
            //& fndFind.strLiteral2 & Chr(34) & " " & Mid$(strTemp2, InStr(1, strTemp2, "%2
            //%") + 3, Len(strTemp2))
            //End If
            //If InStr(1, strTemp2, "%3%") <> 0 Then
            //strTemp2 = Mid$(strTemp2, 1, InStr(1, strTemp2, "%3%") - 1) & Chr(34)
            //& fndFind.strLiteral3 & Chr(34) & " " & Mid$(strTemp2, InStr(1, strTemp2, "%3
            //%") + 3, Len(strTemp2))
            //End If
            //strTemp = strTemp & " WHERE " & strTemp2
            //End If
            //End If
            //'added 02/03/2018 stores any custom orderby caluse
            //strOrderBy = rstTemp!OrderBy
            //GetSQL = strTemp
            //rstTemp.Close
            //adoConn.Close
            //Set adoConn = Nothing

        }

        private bool DoSearch()
        {
            /*
              Created 13/08/2025 By Roger Williams

              Gets SQL query and populates datagrid from it

              returns TRUE if records found

            */

            string strSQL = string.Empty;
            SqlConnection SQLConn = null;
            SqlCommand SQLCmd = null;
            SqlDataAdapter DADQuery = null;
            DataSet DSTStock = null;

            //clear last find result
            Modules.clsData.objFindSelected = null;
            //get query SQl
            strSQL = GetSQL();

            using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
            {
                try
                {
                    SQLConn.Open();
                    SQLCmd = new SqlCommand(strSQL, SQLConn);
                    DADQuery = new SqlDataAdapter(SQLCmd);
                    DSTStock = new DataSet();
                    DADQuery.Fill(DSTStock);

                    BDSQuery = new BindingSource();
                    BDSQuery.DataSource = DSTStock;
                    BDSQuery.DataMember = DSTStock.Tables[0].TableName; //ALWAYS specify this else grid does not work!
                    SQLConn.Close();

                    this.DGVResults.DataSource = BDSQuery;
                    //set column headers
                    //this.DGVResults.DefaultCellStyle.BackColor = Color.MediumBlue;
                    //this.DGVResults.Columns[0].HeaderText = "ItemID";
                    //this.DGVResults.Columns[1].HeaderText = "UOM";
                    //this.DGVResults.Columns[2].HeaderText = "Product Family";
                    //this.DGVResults.Columns[3].HeaderText = "Price";
                    //this.DGVResults.Columns[4].HeaderText = "Loc/Lot Tracked?";
                    //this.DGVResults.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
                    //this.DGVResults.EnableHeadersVisualStyles = false;

                    //this.DGVResults.RowHeadersDefaultCellStyle.BackColor = Color.Green;
                    //test
                   // this.DGVResults.AutoResizeRow(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Accessing Data\n\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return this.DGVResults.Rows.Count != 0;
        }

        //**********form events**********
        private void Form_Find_Load(object sender, EventArgs e)
        {
            //if nothing passed close
            if (strSearchName == String.Empty)
            {
                this.Close();
            }
            else
            {
                if (this.DGVResults.Rows.Count == 0)
                { 
                //set control events
                this.CHKPanel1.CheckedChanged += Custom_CheckedChanged;
                this.CHKPanel2.CheckedChanged += Custom_CheckedChanged;
                this.CHKPanel3.CheckedChanged += Custom_CheckedChanged;
                this.CHKPanel4.CheckedChanged += Custom_CheckedChanged;
                this.CHKPanel5.CheckedChanged += Custom_CheckedChanged;
                this.CHKPanel6.CheckedChanged += Custom_CheckedChanged;
                this.CMBField1Comparison.KeyDown += CustomKeyDown;
                this.CMBField2Comparison.KeyDown += CustomKeyDown;
                this.CMBField3Comparison.KeyDown += CustomKeyDown;
                this.CMBField4Comparison.KeyDown += CustomKeyDown;
                this.CMBField5Comparison.KeyDown += CustomKeyDown;
                this.CMBField6Comparison.KeyDown += CustomKeyDown;
                PopulateForm();
                this.DGVResults.Enabled = false;
                this.Text = "Find Data Using Search: " + strSearchName;
                //apply system theme
                Modules.clsView.SetTheme(this, null);
                }
        }
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            //always hide as calling form will close on exit
            this.Hide();
        }

        private void CMBField1Comparison_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void frmFind_Paint(object sender, PaintEventArgs e)
        {
            Rectangle RCTTemp = e.ClipRectangle;

            //draw line
            e.Graphics.DrawLine(PENTemp, 1, 290, this.Width, 290);
            //draw box around panels
            e.Graphics.DrawRectangle(PENTemp, 12, 12, 505, 262);
        }

        private void BTNFind_Click(object sender, EventArgs e)
        {
            /*
              Created 13/08/2025 By Roger Williams

              Checks datetimepicker dates are valid if enable then gets query values!


            */

            DateTime DTETemp = DateTime.Now;
            int intNum = 1;
            bool blnOk = false;

            //make sure if datetimepickers visible that dates valid
            for (intNum = 1; intNum != 6; intNum++)
            {
                if (this.Controls["PANField" + intNum.ToString()].Enabled)
                {
                    //set ok flag (something selected)
                    blnOk = true;

                    if (((DateTimePicker)this.Controls["PANField" + intNum.ToString()].Controls["DTPField" + intNum.ToString() + "From"]).Visible)
                    {
                        if (((DateTimePicker)this.Controls["PANField" + intNum.ToString()].Controls["DTPField" + intNum.ToString() + "From"]).Value >
                             ((DateTimePicker)this.Controls["PANField" + intNum.ToString()].Controls["DTPField" + intNum.ToString() + "To"]).Value)
                        {
                            MessageBox.Show("From Date Greater Than To Date!", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (((ComboBox)this.Controls["PANField" + intNum.ToString()].Controls["CMBField" + intNum.ToString() + "Comparison"]).Visible)
                    { 
                        //check cmbField<number>Comparison is NOT null
                        if (((ComboBox)this.Controls["PANField" + intNum.ToString()].Controls["CMBField" + intNum.ToString() + "Comparison"]).Text == String.Empty)
                        {
                            blnOk = false;
                            break;
                        }
                    }
                    //check if textbox visible and NOT null
                    if (((TextBox)this.Controls["PANField" + intNum.ToString()].Controls["TXTField" + intNum.ToString()]).Visible)
                    {
                        if (((TextBox)this.Controls["PANField" + intNum.ToString()].Controls["TXTField" + intNum.ToString()]).Text == String.Empty)
                        {
                            blnOk = false;
                            break;
                        }
                    }
                }
            }

            if (blnOk)
            { 
                if (DoSearch())
                {
                    this.DGVResults.Enabled = true;
                }
                else
                {
                    this.DGVResults.Enabled = false;
                    MessageBox.Show("No Records Found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void DGVResults_DoubleClick(object sender, EventArgs e)
        {
            //returns first field back to calling form and hides this form

            if (this.DGVResults.SelectedRows.Count == 0)
            {
                return;
            }

            Modules.clsData.objFindSelected = this.DGVResults.SelectedRows[0].Cells[0].Value;
            this.Hide();
        }
    }
}

//************Access find form code******

/*

form: find

Property Let OpenFindObject(ByVal fndFindObj As Object)
Set fndFind = fndFindObj
Set Me.SUBCriteria.Form.fndFind = fndFind
End Property
Property Get GetFindType() As String
'Created 07/11/02 by Roger Williams for Orbik Electronics
'
'returns the find type
'
GetFindType = Me.Tag
End Property
Property Get GetHostHWND() As Double
'Created 07/11/02 by Roger Williams for Orbik Electronics
'
'returns the creators hwnd
'
GetHostHWND = lngParentHWND
End Property
Property Let SetHostHWND(varValue As Double)
'Created 07/11/02 by Roger Williams for Orbik Electronics
'
'stores the parents (creators) HWND - used by criteria form
'
lngParentHWND = varValue
End Property
Property Let MultiSelect(varValue As Integer)
'Created 04/11/02 by Roger Williams for Orbik Electronics
'
'value set when class creates the object, can also changed
'by the class
'
'allows/disallows multi row select
'
blnMulti = varValue
End Property
Private Sub PopulateCriteria()
Dim rstTemp2 As ADODB.Recordset
Dim rstTemp3 As ADODB.Recordset
Dim strTemp As String
Dim adoConn As ADODB.Connection
Dim ADOconn2 As ADODB.Connection
Set adoConn = New ADODB.Connection
With adoConn
.Provider = "Microsoft.Jet.OLEDB.4.0"
.Open CodeDb.Name
End With
Set ADOconn2 = New ADODB.Connection
With ADOconn2
.Provider = "Microsoft.Jet.OLEDB.4.0"
.Open CodeDb.Name
End With
Set rstTemp2 = New ADODB.Recordset
Set rstTemp3 = New ADODB.Recordset
rstTemp2.ActiveConnection = adoConn
'modified 12/07/12
'notice in itemavail uses fields from FindItemCard2 but DEFAULT search is Find
ItemCard
rstTemp2.Open "SELECT * FROM UsysFindSearchField WHERE searchtype='" & Me.Tag
& "2' ORDER BY searchorder", adoConn, adOpenDynamic
If rstTemp2.EOF Then 'if no "2" variant do as asked
rstTemp2.Close
rstTemp2.Open "SELECT * FROM UsysFindSearchField WHERE searchtype='" & Me.T
ag & "3' ORDER BY searchorder", adoConn, adOpenDynamic
If rstTemp2.EOF Then 'if no "3" variant do as asked
rstTemp2.Close
rstTemp2.Open "SELECT * FROM UsysFindSearchField WHERE searchtype='" & M
e.Tag & "' ORDER BY searchorder", adoConn, adOpenDynamic
End If
End If
strTemp  = String.Empty;
Me.SUBCriteria.Form!CMBField.ColumnCount = 4
Me.SUBCriteria.Form!CMBField.BoundColumn = 2
Me.SUBCriteria.Form!CMBField.ColumnWidths = "0 cm;0 cm;4 cm;0 cm"
'
While Not rstTemp2.EOF
On Error Resume Next
ADOconn2.Open
rstTemp3.Open "SELECT * FROM UsysFindFieldInfo WHERE fieldname='" & rstTemp
2!FieldName & "'", ADOconn2
strTemp = strTemp & rstTemp3!fielddatatype & ";" & rstTemp3!FieldName & ";"
& rstTemp3!Control_Name & ";" & rstTemp3!fieldlength & ";"
rstTemp2.MoveNext
ADOconn2.Close
Wend
'populate aryFindCriteria array with data
'1: basic search SQL 6: field
'2: hwnd 7: field human name
'3: search name 8:
'4: operator 9 :
'5: value 10:
'
strTemp = Mid$(strTemp, 1, Len(strTemp) - 1)
lngParentHWND = Me.hwnd
Me.SUBCriteria.Form!CMBField.RowSource = strTemp
Me.SUBCriteria.Form!CMBField.Requery
Me.SUBCriteria.Form!CMBField = Me.SUBCriteria.Form!CMBField.ItemData(0)
Me.SUBCriteria.Form!CMBOperation = Me.SUBCriteria.Form!CMBOperation.ItemDat
a(0)
Me.SUBCriteria.Form!TXTValue = "*"
Me.SUBCriteria.Form!CMBField2.RowSource = strTemp
Me.SUBCriteria.Form!CMBField2.Requery
Me.SUBCriteria.Form!CMBField3.RowSource = strTemp
Me.SUBCriteria.Form!CMBField3.Requery
Me.SUBCriteria.Form!CMBField4.RowSource = strTemp
Me.SUBCriteria.Form!CMBField4.Requery
Me.SUBCriteria.Form.Refresh
SendKeys "{TAB}", False
adoConn.Close
Set ADOconn2 = Nothing
Set rstTemp2 = Nothing
Set rstTemp3 = Nothing
End Sub
Private Sub cmdClose_Click()
'dont close just hide
Me.Visible = False
End Sub
Private Sub DoSearch()
Dim bytNum As Byte
Dim strTemp As String
Dim strSQL As String
Dim strWhere As String
Dim bytNum2 As Byte
Dim bytWhereLen As Byte
Dim qryTemp As DAO.QueryDef
Dim intField1Size As Byte
Dim intField2Size As Byte
Dim rstTemp2 As ADODB.Recordset
Dim rstTemp3 As ADODB.Recordset
Dim adoConn As ADODB.Connection
Dim dblColWidths As Double
Set rstTemp2 = New ADODB.Recordset
Set rstTemp3 = New ADODB.Recordset
Set adoConn = New ADODB.Connection
'get search criteria from sub form
strSearchSQL = Me.SUBCriteria.Form.UpdateSQLString
'update search query with search
Set qryTemp = CodeDb.QueryDefs("qryFindSQLPassThrough")
qryTemp.ReturnsRecords = True
qryTemp.SQL = strSearchSQL
qryTemp.Close
'set results forms control sources
With adoConn
.Provider = "Microsoft.Jet.OLEDB.4.0"
.Open CodeDb.Name
End With
If blnUse2 = False Then
rstTemp2.Open "SELECT * FROM UsysFindSearchField WHERE searchtype='" & Me.T
ag & "' ORDER BY searchorder", adoConn, adOpenForwardOnly
Else
rstTemp2.Open "SELECT * FROM UsysFindSearchField WHERE searchtype='" & Me.T
ag & "2' ORDER BY searchorder", adoConn, adOpenForwardOnly
End If
Stop
If rstTemp2.EOF Then Exit Sub
rstTemp3.Open "SELECT * FROM UsysFindFieldInfo WHERE fieldname='" & rstTemp2!F
ieldName & "'", adoConn, adOpenForwardOnly
If InStr(1, rstTemp3!FieldName, ".") = 0 Then
Me.SUBResults.Form!Text0.ControlSource = rstTemp3!FieldName
Else
Me.SUBResults.Form!Text0.ControlSource = Mid$(rstTemp3!FieldName, InStr(1,
rstTemp3!FieldName, ".") + 1, Len(rstTemp3!FieldName))
End If
Me.SUBResults.Form!Label0.Caption = rstTemp3!Control_Name
intField1Size = rstTemp3!fieldlength
rstTemp2.MoveNext
rstTemp3.Close
rstTemp3.Open "SELECT * FROM UsysFindFieldInfo WHERE fieldname='" & rstTemp2!F
ieldName & "'", adoConn, adOpenForwardOnly
Me.SUBResults.Form!Text1.ControlSource = rstTemp3!FieldName
Me.SUBResults.Form!Label1.Caption = rstTemp3!Control_Name
intField2Size = rstTemp3!fieldlength
'setup any extra controls used in criteria
bytNum = 2
While Not rstTemp3.EOF
'if not compulsary fields set control in results form to view it
'set up for cmbfield controls 1-4
If InStr(1, Me.SUBCriteria!CMBField.Column(1), Me.SUBResults.Form!Text0.Contr
olSource) = 0 And Me.SUBCriteria!CMBField.Column(1) <> Me.SUBResults.Form!Text
1.ControlSource Then
'set text box control source
Me.SUBResults.Form.Controls("Text" & bytNum).ControlSource = Me.SUBCriteri
a!CMBField.Column(1)
'set label caption - this becomes the column caption in datasheet view
Me.SUBResults.Form.Controls("Label" & bytNum).Caption = Me.SUBCriteria!CMB
Field.Column(2)
bytNum = bytNum + 1
End If
If Me.SUBCriteria!CMBField2.Enabled Then
If InStr(1, Me.SUBCriteria!CMBField2.Column(1), Me.SUBResults.Form!Text0.Cont
rolSource) = 0 And Me.SUBCriteria!CMBField2.Column(1) <> Me.SUBResults.Form!Te
xt1.ControlSource Then
'set text box control source
Me.SUBResults.Form.Controls("Text" & bytNum).ControlSource = Me.SUBCriteri
a!CMBField2.Column(1)
'set label caption - this becomes the column caption in datasheet view
Me.SUBResults.Form.Controls("Label" & bytNum).Caption = Me.SUBCriteria!CMB
Field2.Column(2)
bytNum = bytNum + 1
End If
End If
If Me.SUBCriteria!CMBField3.Enabled Then
If InStr(1, Me.SUBCriteria!CMBField3.Column(1), Me.SUBResults.Form!Text0.Cont
rolSource) = 0 And Me.SUBCriteria!CMBField3.Column(1) <> Me.SUBResults.Form!Te
xt1.ControlSource Then
'set text box control source
Me.SUBResults.Form.Controls("Text" & bytNum).ControlSource = Me.SUBCriteri
a!CMBField3.Column(1)
'set label caption - this becomes the column caption in datasheet view
Me.SUBResults.Form.Controls("Label" & bytNum).Caption = Me.SUBCriteria!CMB
Field3.Column(2)
bytNum = bytNum + 1
End If
End If
If Me.SUBCriteria!CMBField4.Enabled Then
If InStr(1, Me.SUBCriteria!CMBField4.Column(1), Me.SUBResults.Form!Text0.Cont
rolSource) = 0 And Me.SUBCriteria!CMBField4.Column(1) <> Me.SUBResults.Form!Te
xt1.ControlSource Then
'set text box control source
Me.SUBResults.Form.Controls("Text" & bytNum).ControlSource = Me.SUBCriteri
a!CMBField4.Column(1)
'set label caption - this becomes the column caption in datasheet view
Me.SUBResults.Form.Controls("Label" & bytNum).Caption = Me.SUBCriteria!CMB
Field4.Column(2)
bytNum = bytNum + 1
End If
End If
rstTemp3.MoveNext
Wend
rstTemp2.Close
rstTemp3.Close
'set up default controls
Me.SUBResults.Form!Text0.Visible = True
Me.SUBResults.Form!Label1.Visible = True
'store default field names for later
strField1 = Me.SUBResults.Form!Text0.ControlSource
strField2 = Me.SUBResults.Form!Text1.ControlSource
bytWhereLen = 0
If InStr(1, strTemp, "GROUP BY") = 0 Then
strWhere = " WHERE "
bytWhereLen = 7
Else
If InStr(1, strTemp, "HAVING") = 0 Then
strWhere = " HAVING "
Else
strWhere = " "
End If
bytWhereLen = 9
End If
If InStr(1, strTemp, "WHERE") <> 0 Then strWhere  = String.Empty;
On Error Resume Next
'hide everything
For bytNum2 = 0 To Me.SUBResults.Form.Controls.Count - 1
Me.SUBResults.Form.Controls(bytNum2).ColumnHidden = True
Next bytNum2
'show previously hidden columns
For bytNum2 = 0 To bytNum - 1
Me.SUBResults.Form.Controls("Text" & bytNum2).ColumnHidden = False
Next bytNum2
'hide unused columns
For bytNum2 = bytNum To 9
Me.SUBResults.Form.Controls("Text" & bytNum2).ColumnHidden = True
Next bytNum2
'resize populated columns to fit data
If intField1Size < 100 Then
Me.SUBResults.Form.Controls("Text0").ColumnWidth = intField1Size * 140
Else
Me.SUBResults.Form.Controls("Text0").ColumnWidth = (intField1Size / 2) * 10
0
End If
If Me.SUBCriteria.Form!CMBField.Column(3) < 100 Then
Me.SUBResults.Form.Controls("Text0").ColumnWidth = Me.SUBCriteria.Form!CMBF
ield.Column(3) * 140
Else
Me.SUBResults.Form.Controls("Text0").ColumnWidth = Me.SUBCriteria.Form!CMBF
ield.Column(3) / 2 * 100
End If
dblColWidths = Me.SUBResults.Form.Controls("Text0").ColumnWidth
If intField2Size < 100 Then
Me.SUBResults.Form.Controls("Text1").ColumnWidth = intField2Size * 140
Else
Me.SUBResults.Form.Controls("Text1").ColumnWidth = intField2Size / 2 * 100
End If
dblColWidths = dblColWidths + Me.SUBResults.Form.Controls("Text1").ColumnWidth
If Me.SUBCriteria.Form!CMBField3.Enabled Then
If Me.SUBCriteria.Form!CMBField3.Column(3) < 100 Then
Me.SUBResults.Form.Controls("Text2").ColumnWidth = Me.SUBCriteria.Form!C
MBField3.Column(3) * 140
Else
Me.SUBResults.Form.Controls("Text2").ColumnWidth = Me.SUBCriteria.Form!C
MBField3.Column(3) / 2 * 100
End If
dblColWidths = dblColWidths + Me.SUBResults.Form.Controls("Text2").ColumnWidth
End If
If Me.SUBCriteria.Form!CMBField4.Enabled Then
If Me.SUBCriteria.Form!CMBField4.Column(3) < 100 Then
Me.SUBResults.Form.Controls("Text3").ColumnWidth = Me.SUBCriteria.Form!C
MBField4.Column(3) * 140
Else
Me.SUBResults.Form.Controls("Text3").ColumnWidth = Me.SUBCriteria.Form!C
MBField4.Column(3) / 2 * 100
End If
dblColWidths = dblColWidths + Me.SUBResults.Form.Controls("Text3").ColumnWidt
h
End If
adoConn.Close
Set rstTemp2 = Nothing
Set rstTemp3 = Nothing
If Me.Width < dblColWidths + 200 Then
lngWidth = dblColWidths + 1200
'perform dummy resize to trigger OnResize event
DoCmd.SelectObject acForm, Me.Name
DoCmd.MoveSize , , 10000
Else
lngWidth = CNST_INT_WIDTH
lngWidth = dblColWidths + 1200
'perform dummy resize to trigger OnResize event
DoCmd.SelectObject acForm, Me.Name
DoCmd.MoveSize , , 10000
End If
End Sub
Private Sub CMDfind_Click()
blnStart = True
'Stop
DoSearch
'reset results form
Me.SUBResults.Form.RecordSource = "qryFindSQLPassThrough"
Me.SUBResults.Form.Refresh
Me.SUBResults.Form.Requery
If Me.SUBResults.Form.RecordsetClone.RecordCount = 0 Then
MsgBox "Record Not Found", vbCritical, "No Records To Show"
Else
Me.SUBResults.Height = 2700
Me.InsideHeight = 2880 * 2
blnStart = False
End If
lngHeight = 6225
End Sub
Private Sub CMDSelect_Click()
'Modified 08/11/02 by Roger Williams for Orbik Electronics
'
'added code to store the results in a new property of the
'find class: FindResults()
'
Dim strFields As String
Dim intNum As Integer
Dim strSQL As String
Dim strValues As String
Dim intRows As Integer
Dim strValue As String
'if no data found exit
If Me.InsideHeight = 2880 Then Exit Sub
'if sub form has multiple rows selected use subforms custom property
'to get the selected row count
intRows = Me.SUBResults.Form.SelectedRows
If intRows <> 0 Or Me.SUBResults.Form.SelTop <> 0 Then
Me.SUBResults.Form.RecordsetClone.Bookmark = Me.SUBResults.Form.Bookmark
If intRows <> 0 Then 'multi
For intNum = 1 To intRows
fndFind.AddFindResults intNum, Me.SUBResults.Form.RecordsetClone.Field
s(0).Value, Me.SUBResults.Form.RecordsetClone.Fields(1).Value 'Me.SUBResults.F
orm.Controls(strField2)
Me.SUBResults.Form.RecordsetClone.MoveNext
Next intNum
Else
fndFind.AddFindResults 1, Me.SUBResults.Form.RecordsetClone.Fields(0).Valu
e, Me.SUBResults.Form.RecordsetClone.Fields(1).Value 'Me.SUBResults.Form.Contr
ols(strField2)
Me.SUBResults.Form.RecordsetClone.MoveNext
End If
End If
'if sub form has multiple rows selected use subforms custom property
'to get the selected row count
fndFind.Selected = Me.SUBResults.Form.SelTop <> 0 'fndFind.GetSelectedCount <>
0
Me.TimerInterval = 0
Me.Visible = False
End Sub
Private Sub Form_Activate()
Me.TimerInterval = 120000
End Sub
Private Sub Form_Close()
'Created 08/11/02 by Roger Williams for Orbik Electronics
'
Set fndFind = Nothing
End Sub
Private Sub Form_Load()
blnStart = True
'make sure form always does NOT show subform if not records in it
If intRows = 0 Then Me.InsideHeight = 2880
'added 27/09/07 By Roger Williams
blnStart = True
lngWidth = Me.WindowWidth
lngHeight = 3345 '6225 Me.WindowHeight
End Sub
Private Sub Form_Resize()
Dim dblLeft As Double
dblLeft = 0
If lngWidth = 0 Then Exit Sub
DoCmd.Echo False
Me.Painting = False
If Me.SUBResults.Height <> 2700 And blnStart Then 'if no results fix size
DoCmd.MoveSize , , CNST_INT_WIDTH, CNST_INT_HEIGHT
GoTo Exit_Sub
End If
'enforce size constraints
If lngWidth > Me.WindowWidth Then
If tmpRECT.Right = 804 Then
'enforce maximum screen width - 200
'11925 is width of 800 pixels in twips
If lngWidth > 11725 Then
lngWidth = 11725
dblLeft = 200
End If
End If
If tmpRECT.Right = 1028 Then
'enforce maximum screen width - 200
'15270 is 1025 pixels in twips
If lngWidth > 15170 Then
lngWidth = 15070
dblLeft = 200
End If
End If
'if left position needs adjusting
If dblLeft = 0 Then
DoCmd.MoveSize (lngWidth - Me.WindowWidth) / 100 + tmpRECT.Left, 200, ln
gWidth
Else
'if left does not need adjusting as the form fills the screen
DoCmd.MoveSize 200, 200, lngWidth
End If
Me.SUBResults.Width = Me.WindowWidth - (Me.SUBResults.Left + 160)
End If
If lngHeight > Me.WindowHeight Then
DoCmd.MoveSize , -500, , lngHeight
Me.SUBResults.Height = (Me.WindowHeight - Me.SUBResults.Top)
Exit Sub
End If
If Me.WindowHeight <> CNST_INT_HEIGHT Then
If (tmpRECT.Bottom * 10) < Me.WindowHeight Then
DoCmd.MoveSize , Me.WindowHeight - (tmpRECT.Bottom * 10)
End If
End If
'resize results forms
Me.SUBResults.Width = Me.WindowWidth - (Me.SUBResults.Left + 160)
Me.SUBResults.Height = (Me.WindowHeight - Me.SUBResults.Top - 500)
Exit_Sub:
Me.Painting = True
DoCmd.Echo True
End Sub
Private Sub Form_Timer()
If Me.TimerInterval <> 120000 Then
'get access window size before any searching is done
GetWindowRect Application.hWndAccessApp, tmpRECT
tmpRECT.Left = 200
Me.TimerInterval = 120000
If Me.Tag <> "" And Not IsNull(Me.Tag) Then
If InStr(1, Me.Tag, "Find") = 1 Then
PopulateCriteria
End If
End If
Else
If Me.Visible = True Then cmdClose_Click 'if on for a minute self "close"
End If
End Sub
Private Sub SUBCriteria_Exit(Cancel As Integer)
'get search criteria from sub form
strSearchSQL = Me.SUBCriteria.Form.UpdateSQLString
End Sub







form: findcriteria


Private Function GetSQL(ByRef strOrderBy As String) As String
Dim strTemp As String
Dim adoConn As ADODB.Connection
Dim rstTemp As ADODB.Recordset
Dim strTemp2 As String
Dim strField As String
Set adoConn = New ADODB.Connection
Set rstTemp = New ADODB.Recordset
With adoConn
.Provider = "Microsoft.Jet.OLEDB.4.0"
.Open CodeDb.Name
End With
On Error Resume Next
strField = Me.CMBField
If Me.CMBField2.Enabled Then strField = strField & "|" & Me.CMBField2
If Me.CMBField3.Enabled Then strField = strField & "|" & Me.CMBField3
If Me.CMBField4.Enabled Then strField = strField & "|" & Me.CMBField4
If CheckUse2(fndFind.FindName, strField) = False Then
rstTemp.Open "SELECT * FROM UsysFindRelations WHERE searchtype='" & fndFind
.FindName & "'", adoConn, adOpenDynamic
Else
rstTemp.Open "SELECT * FROM UsysFindRelations WHERE searchtype='" & fndFind
.FindName & "2'", adoConn, adOpenDynamic
End If
'modified 06/02/2018 added top 5000 filter
'If rstTemp!distinctsearch Then
' strTemp = "SELECT DISTINCT TOP 5000 " & rstTemp.Fields("searchfields").Val
ue & " FROM "
'Else
' strTemp = "SELECT TOP 5000 " & rstTemp.Fields("searchfields").Value & " FR
OM "
'End If
'If rstTemp!distinctsearch Then
' strTemp = "SELECT DISTINCT TOP 5000 " & rstTemp.Fields("searchfields").Val
ue & " FROM "
'Else
strTemp = "SELECT " & rstTemp.Fields("searchfields").Value & " FROM "
'End If
If rstTemp!groupby Then
'set group by
blnGroupBy = True
strGroupbyFields = rstTemp.Fields("searchfields").Value
End If
If Nz(rstTemp.Fields("qualifiedjoin").Value, "")  = String.Empty; Then
strTemp = strTemp & " " & rstTemp.Fields("tablename").Value
Else
strTemp = strTemp & " " & rstTemp.Fields("qualifiedjoin").Value
End If
If Nz(rstTemp.Fields("searchcondition").Value, "") <> "" Then
If InStr(1, rstTemp.Fields("searchcondition").Value, "%") = 0 Then
strTemp = strTemp & " WHERE " & rstTemp.Fields("searchcondition").Value
& " "
Else
strTemp2 = rstTemp.Fields("searchcondition").Value
If InStr(1, strTemp2, "%1%") <> 0 Then
strTemp2 = Mid$(strTemp2, 1, InStr(1, strTemp2, "%1%") - 1) & Chr(34)
& fndFind.strLiteral1 & Chr(34) & " " & Mid$(strTemp2, InStr(1, strTemp2, "%1
%") + 3, Len(strTemp2))
End If
If InStr(1, strTemp2, "%2%") <> 0 Then
strTemp2 = Mid$(strTemp2, 1, InStr(1, strTemp2, "%2%") - 1) & Chr(34)
& fndFind.strLiteral2 & Chr(34) & " " & Mid$(strTemp2, InStr(1, strTemp2, "%2
%") + 3, Len(strTemp2))
End If
If InStr(1, strTemp2, "%3%") <> 0 Then
strTemp2 = Mid$(strTemp2, 1, InStr(1, strTemp2, "%3%") - 1) & Chr(34)
& fndFind.strLiteral3 & Chr(34) & " " & Mid$(strTemp2, InStr(1, strTemp2, "%3
%") + 3, Len(strTemp2))
End If
strTemp = strTemp & " WHERE " & strTemp2
End If
End If
'added 02/03/2018 stores any custom orderby caluse
strOrderBy = rstTemp!OrderBy
GetSQL = strTemp
rstTemp.Close
adoConn.Close
Set adoConn = Nothing
End Function
Private Function GetWhere(strFieldType As String, strValue As String, strOpera
tion As String, strField As String) As String
Dim strWhere As String
If strOperation = "Like" Then 'And InStr(1, strValue, "*") <> 0 Then
If Len(strValue) > 1 Then
'make sure value surrounded with *
If InStr(1, strValue, "*") = 0 Then 'if no * set for right side
strValue = strValue & "%"
End If
Else
strValue  = String.Empty;
End If
End If
If strOperation <> "Like" Then
Select Case strFieldType 'rstTemp!FIND_Type
Case "10"
strWhere = strWhere & Chr(34) & strValue & Chr(34)
Case "8"
strValue = Format(strValue, "yyyy/mm/dd") 'get correct format
strWhere = strWhere & Chr(34) & strValue & Chr(34)
Case "1"
If strValue = 0 Then
strWhere = strWhere & "False"
Else
strWhere = strWhere & "True"
End If
Case Else
strWhere = strWhere & strValue
End Select
Else
If strValue = "*" Then
strWhere  = String.Empty; ' Chr(34) & "%" & Chr(34)
Else
'if not a date
If Len(strValue) > 1 Then
If IsDate(Mid$(strValue, 1, Len(strValue) - 1)) Then
'if date strip *
If InStr(1, strValue, "%") <> 0 Then
If Left$(strValue, 1) = "%" Then strValue = Mid$(strValue, 2, Le
n(strValue))
If Right$(strValue, 1) = "%" Then strValue = Mid$(strValue, 1, L
en(strValue) - 1)
End If
strValue = Format(strValue, "yyyy/mm/dd") 'get correct format
strValue = "(" & Chr(34) & strValue & Chr(34) & ")"
strOperation = "In"
strWhere = strValue
Else
While InStr(1, strValue, "*") <> 0
If Left$(strValue, 1) = "*" Then
strValue = "%" & Mid$(strValue, 2, Len(strValue))
Else
strValue = Mid$(strValue, 1, InStr(1, strValue, "*") - 1) & "%
" & Mid$(strValue, InStr(1, strValue, "*") + 1, Len(strValue))
End If
Wend
strWhere = Chr(34) & strValue & Chr(34)
End If
Else
strWhere = Chr(34) & strValue & Chr(34)
End If
End If
End If
'Stop
If strValue <> "" Then GetWhere = strField & " " & strOperation & " " & strWhe
re
End Function
Public Function UpdateSQLString() As String
'Modified 23/11/2017 By Roger Williams
'
'Added new code to sort the query
Dim strSQL As String
Dim strFilter As String
Dim strOrderBy As String
strSQL = GetSQL(strOrderBy)
strFilter = GetWhere(Me.CMBField.Column(0), Me.TXTValue, Me.CMBOperation, Me.
CMBField.Column(1))
Me.Refresh
If Me.CMBField4.Enabled Then
If strFilter <> "" Then
strFilter = strFilter & " AND " & GetWhere(Me.CMBField4.Column(0), Me.T
XTValue4, Me.CMBOperation4, Me.CMBField4.Column(1))
Else
strFilter = GetWhere(Me.CMBField4.Column(0), Me.TXTValue4, Me.CMBOperat
ion4, Me.CMBField4.Column(1))
End If
End If
If Me.CMBField3.Enabled Then
If strFilter <> "" Then
strFilter = strFilter & " AND " & GetWhere(Me.CMBField3.Column(0), Me.T
XTValue3, Me.CMBOperation3, Me.CMBField3.Column(1))
Else
strFilter = GetWhere(Me.CMBField3.Column(0), Me.TXTValue3, Me.CMBOperat
ion3, Me.CMBField3.Column(1))
End If
End If
If Me.CMBField2.Enabled Then
If strFilter <> "" Then
strFilter = strFilter & " AND " & GetWhere(Me.CMBField2.Column(0), Me.T
XTValue2, Me.CMBOperation2, Me.CMBField2.Column(1))
Else
strFilter = GetWhere(Me.CMBField2.Column(0), Me.TXTValue2, Me.CMBOperat
ion2, Me.CMBField2.Column(1))
End If
End If
' If Me.CMBField4.Enabled Then strFilter = strFilter & " AND " & GetWhere(Me.C
MBField4.Column(0), Me.TXTValue4, Me.CMBOperation4, Me.CMBField4.Column(1))
' If Me.CMBField3.Enabled Then strFilter = strFilter & " AND " & GetWhere(Me.C
MBField3.Column(0), Me.TXTValue3, Me.CMBOperation3, Me.CMBField3.Column(1))
' If Me.CMBField2.Enabled Then strFilter = strFilter & " AND " & GetWhere(Me.C
MBField2.Column(0), Me.TXTValue2, Me.CMBOperation2, Me.CMBField2.Column(1))
If InStr(1, strSQL, "having") = 0 And InStr(1, strFilter, "having") = 0 Then
If strFilter <> "" Then
If InStr(1, strSQL, "WHERE") = 0 Then
strSQL = strSQL & " WHERE " & strFilter
Else
strSQL = strSQL & " AND " & strFilter
End If
End If
End If
If Right$(strSQL, 4) = "AND " Then strSQL = Mid$(strSQL, 1, Len(strSQL) - 4)
'check if group by
If blnGroupBy Then
strSQL = strSQL & " GROUP BY " & strGroupbyFields
End If
'added 23/11/2017 By Roger Williams
'add order by
If strOrderBy  = String.Empty; Then
strSQL = strSQL & " ORDER BY " & Me.CMBField.Column(1)
Else
strSQL = strSQL & " " & strOrderBy
End If
UpdateSQLString = strSQL
End Function
Private Sub CMBField_AfterUpdate()
If Me.CMBField <> "" And Not IsNull(Me.CMBField) Then
Me.CMBOperation.Value = Me.CMBOperation.ItemData(0)
Me.TXTValue = "*"
End If
End Sub
Private Function AlreadySelected(strWhat As String) As Boolean
'uses screen.activecontrol for which control has invalid value
'
Dim bln2 As Boolean
Dim bln3 As Boolean
Dim bln4 As Boolean
Dim blnInvalid As Boolean
'get which controls are enabled
If Me.CMBField2.Enabled Then
bln2 = True
End If
If Me.CMBField3.Enabled Then
bln3 = True
End If
If Me.CMBField4.Enabled Then
bln4 = True
End If
'now check for invalid value
If bln2 Then
If Me.CMBField2 = Me.CMBField Then blnInvalid = True
End If
If bln3 Then
If Me.CMBField3 = Me.CMBField Then blnInvalid = True
End If
If bln4 Then
If Me.CMBField4 = Me.CMBField Then blnInvalid = True
End If
If blnInvalid Then
MsgBox CMBField.Value & " Already Selected!", vbExclamation, "Unique Fields
Only"
SendKeys "{ESC}", True
End If
AlreadySelected = blnInvalid
End Function
Private Sub CMBField2_AfterUpdate()
If Me.CMBField2 <> "" And Not IsNull(Me.CMBField2) Then
Me.CMBOperation2.Value = Me.CMBOperation2.ItemData(0)
Me.TXTValue2 = "*"
End If
End Sub
Private Sub CMBField2_BeforeUpdate(Cancel As Integer)
Cancel = AlreadySelected(Me.CMBField2.Value)
Cancel = IsNull(Screen.ActiveControl.Value)
End Sub
Private Sub CMBField3_AfterUpdate()
If Me.CMBField3 <> "" And Not IsNull(Me.CMBField3) Then
Me.CMBOperation3.Value = Me.CMBOperation3.ItemData(0)
Me.TXTValue3 = "*"
End If
End Sub
Private Sub CMBField3_BeforeUpdate(Cancel As Integer)
Cancel = AlreadySelected(Me.CMBField3.Value)
End Sub
Private Sub CMBField4_AfterUpdate()
If Me.CMBField4 <> "" And Not IsNull(Me.CMBField4) Then
Me.CMBOperation4.Value = Me.CMBOperation4.ItemData(0)
Me.TXTValue4 = "*"
End If
End Sub
Private Sub CMBField4_BeforeUpdate(Cancel As Integer)
Cancel = AlreadySelected(Me.CMBField4.Value)
End Sub
Private Sub CMDRecSelect2_Click()
Me.CMBField2.Value = Me.CMBField2.ItemData(1)
Me.TXTValue2 = "*"
Me.CMBOperation2 = Me.CMBOperation2.ItemData(0)
Me.CMBField2.Enabled = Not Me.CMBField2.Enabled
Me.CMBOperation2.Enabled = Not Me.CMBOperation2.Enabled
Me.TXTValue2.Enabled = Not Me.TXTValue2.Enabled
If Me.CMBField2.Enabled = False Then
Me.CMBField3.Enabled = False
Me.CMBOperation3.Enabled = False
Me.TXTValue3.Enabled = False
Me.CMBField4.Enabled = False
Me.CMBOperation4.Enabled = False
Me.TXTValue4.Enabled = False
End If
End Sub
Private Sub CMDRecSelect3_Click()
If Me.CMBField.ListCount <= 1 Then Exit Sub
Me.CMBField3.Value = Me.CMBField3.ItemData(2)
Me.TXTValue3 = "*"
Me.CMBOperation3 = Me.CMBOperation3.ItemData(0)
If Me.CMBField2.Enabled = False Then
Me.CMBField4.Enabled = False
Me.CMBOperation4.Enabled = False
Me.TXTValue4.Enabled = False
Me.CMBField3.Enabled = False
Me.CMBOperation3.Enabled = False
Me.TXTValue3.Enabled = False
Else
Me.CMBField3.Enabled = Not Me.CMBField3.Enabled
Me.CMBOperation3.Enabled = Not Me.CMBOperation3.Enabled
Me.TXTValue3.Enabled = Not Me.TXTValue3.Enabled
If Me.CMBField3.Enabled = False Then
Me.CMBField4.Enabled = False
Me.CMBOperation4.Enabled = False
Me.TXTValue4.Enabled = False
End If
End If
End Sub
Private Sub CMDRecSelect4_Click()
If Me.CMBField.ListCount <= 1 Then Exit Sub
Me.CMBField4.Value = Me.CMBField4.ItemData(3)
Me.TXTValue4 = "*"
Me.CMBOperation4 = Me.CMBOperation4.ItemData(0)
If Me.CMBField2.Enabled = False Then
Me.CMBField4.Enabled = False
Me.CMBOperation4.Enabled = False
Me.TXTValue4.Enabled = False
Me.CMBField3.Enabled = False
Me.CMBOperation3.Enabled = False
Me.TXTValue3.Enabled = False
Else
Me.CMBField4.Enabled = Not Me.CMBField4.Enabled
Me.CMBOperation4.Enabled = Not Me.CMBOperation4.Enabled
Me.TXTValue4.Enabled = Not Me.TXTValue4.Enabled
End If
End Sub




Form: findcreator


'Modified 20/01/2022 By Roger Williams
'
'Now uses new tables:
'
'FIND_Relations
'FIND_SearchField
'FIND_FieldInfo
'
'
'Created 25/03/02 by Roger Williams for Orbik Electronics
'
'Allows the user to create custom find dialog search types
'for any table and test them in MRP9000
'NOTE: User must make sure any custom tables used are in: UsysTablesList
'
'Tables Written To:
'
'It is important to know this incase anything goes wrong you will know
'where to look to delete your 'find' safely
'
'UsysFindFieldInfo 'find info
'UsysFindRelations 'stored in these
'UsysSearchField '3 tables
'dbo_Captions_English 'Display names for fields stored here
'
'INSTALLATION PREREQUISITES
'==========================
'
'Make sure the following tables are in your UsysTablesList with these values:
'
'TableName TargetName SourceType Reattach Refresh
'-----------------------------------------------------------------------
'UsysFindFieldInfo UsysFindFieldInfo L_ims.accdb Yes Yes
'UsysFindRelations UsysFindRelations L_ims.accdb Yes Yes
'UsysFindSearchField UsysFindSearchField L_ims.accdb Yes Yes
'
'Please make a backup copy of UsysTablesList before adding these records!
'
'Having done this run MRP9000/ERP and let it reattach the tables, now you are
'ready!
'
'NOTE: IMS prefixes all find types with: Find
' Please make sure you follow this convention
' Create and test your new finds on a client machine first
' your find dialog will NOT appear on any other machine
' when you are happy with it, compact, compile and push the client
'
'
'Things To do:
'
'Ability to delete finds (not implemented because there is no way of
'detecting which are 'standard' and which are 'custom')
'
'Reason Why I Did It:
'
'I am doing a HUGE project in MRP9000 which requires we to write a complete
'custom sales cycle, obviously I want IMS complicancy in the form design
'and functionality, ergo I will need to use the find dialog for custom
'table searches, without this form I will have to manually create about
'12 find dialogs and you can guarantee at least 1 will have an error in it.
'So I decided to automate the process to make implementation quicker.
'Then I thought: 'I wonder if any one else could use this?'
'So here it is!
'
'Please modify as you require this code is FREEWARE and thus donated freely
'to the MRP9000/ERP modifications comunity
'At this time this modification is in no way endorsed or officially
'approved by IMS so use is at own risk.
'
'FAQ
'
'Q: Why can't I type in my SQL statement?
'
'A: I could let you, but not many people are fluent enough to type SQL stateme
nts
' that work first time, so I insist you use Access's QBE form because its fo
ol
' proof
'
'Q: Why can't I have a table which contains the information about my created f
inds
' so I can delete the right data if I am uncertain, and also allow me to see
' exactly what custom finds have been created?
'
'A: Well I have given you source use it! If you want it put it in!
'
'Q: Whats the atomic mass of iridium?
'
'A: Easy, 192
'
'
'Modified 27/03/02 by Roger Williams for Orbik Electronics
'
'Removed recordset objects and replaced with null querydef
'to increase speed
'
Dim objFind As Object
Dim qryDummy As DAO.QueryDef
Dim qryFind As DAO.QueryDef
Private Sub CMDClear_Click()
'Created 25/03/02 by Roger Williams for Orbik Electronics
'
'clears the dummy query
'
Set qryFind = CodeDb.QueryDefs("qryORBIKFindQuery")
qryFind.SQL = "SELECT ThisIsADummyQuery FROM PleaseEnterYourSQLInHere"
Me.TXTSql  = String.Empty;
qryFind.Close
Set qryFind = Nothing
MsgBox "Temp Query Used For SQL Creation Reset", vbInformation, "Success!"
End Sub
Private Sub cmdClose_Click()
'Created 25/03/02 by Roger Williams for Orbik Electronics
'
DoCmd.Close
End Sub
Private Sub CMDQuery_Click()
'Created 25/03/02 by Roger Williams for Orbik Electronics
'
'allows the user to create a query to use in the Access 97 QBE form
'
Dim qryFind As DAO.QueryDef
'make sure user has given find a name
If Me.txtFindName  = String.Empty; Or IsNull(Me.txtFindName) Then
MsgBox "Cannot Create A 'Find' That Has No NAME", vbCritical, "No Find Name
"
Exit Sub
End If
'check if in MRP
If SysCmd(acSysCmdGetObjectState, acForm, "frm_MainMenu") <> 0 Then
MsgBox "You Must Be In Access Design Mode!", vbCritical, "Cannot Create Que
ry"
Exit Sub
End If
'make sure warn user of overwrite
If Me.TXTSql <> "" Or Not IsNull(Me.TXTSql) Then
If MsgBox("You Have Already Created A Query, Overwrite?", vbQuestion + vbYe
sNo, "Query Exists") = vbNo Then Exit Sub
End If
MsgBox "Make sure query starts with: SELECT DISTINCT", vbInformation, "Search
Requirement"
'show QBE so user can create query
DoCmd.OpenQuery "qryORBIKFindQuery", acViewDesign
'Wait until QBE is closed
Do While SysCmd(acSysCmdGetObjectState, acQuery, "qryORBIKFindQuery") <> 0
DoEvents
Loop
'process query
Set qryFind = CodeDb.QueryDefs("qryORBIKFindQuery")
If qryFind.SQL  = String.Empty; Or IsNull(qryFind.SQL) Or qryFind.SQL = "SELECT;" & vbCrLf
Then
MsgBox "Cannot Create A 'Find' That Has NO SQL Statement", vbCritical, "No
SQL"
Exit Sub
End If
Me.TXTSql = qryFind.SQL
qryFind.Close
Set qryFind = Nothing
End Sub
Private Function GetHumanName(strField As String) As String
'Created 25/03/02 by Roger Williams for Orbik Electronics
'
'due to the way the find dialog tables are constructed there is no direct
'link between a field name and its display name so I have to ask the user
'what they want to call it!
'
GetHumanName = InputBox("Enter Display Name For: " & strField, "Display Name
For Field Required", "")
End Function
Private Sub CMDsave_Click()
'Modified 20/01/2022 By Roger Williams
'
'Now writes to new tables, ignore captions_english table
'uses codedb.execute apposed to createquerydef
'
'
'Created 25/03/02 by Roger Williams for Orbik Electronics
'
'saves the query as a working find dialog type
'
'splits the users query into:
'
' - Field names with tables
' - Table names
' - WHERE clause
' - Core sql text
'
Dim aryTables() As String
Dim aryFieldNames() As String
Dim aryPos As Byte
Dim aryPosTables As Byte
Dim strWhere As String
Dim strTemp As String
Dim strTemp2 As String
Dim strCoreSql As String
Dim aryPosFieldName As Byte
Dim aryFieldType() As Integer
Dim aryFieldSize() As Integer
Dim intNum As Integer
Dim tblTemp As DAO.TableDef
Dim intNum2 As Integer
Dim intNum3 As Integer
Dim rstTemp As DAO.Recordset
Dim dbTemp As DAO.Database
Dim strFieldList As String
'make sure user has given find a name
If Me.txtFindName  = String.Empty; Or IsNull(Me.txtFindName) Then
MsgBox "Cannot Create A 'Find' That Has No NAME", vbCritical, "No Find Name
"
Exit Sub
End If
'make sure query is SELECT only
If InStr(Me.TXTSql, "into") <> 0 Or InStr(Me.TXTSql, "update") <> 0 Or InStr(M
e.TXTSql, "delete") <> 0 Then
MsgBox "SQL Statement MUST Be A SELECT Statement", vbCritical, "Wrong Query
Type"
Exit Sub
End If
'make sure find has SQL
If Me.TXTSql  = String.Empty; Or IsNull(Me.TXTSql) Or Me.TXTSql = "SELECT;" & vbCrLf Then
MsgBox "Cannot Create A 'Find' That Has NO SQL Statement", vbCritical, "No
SQL"
Exit Sub
End If
strTemp = Mid$(Me.TXTSql, 1, InStr(1, Me.TXTSql, "FROM") - 1)
'strip SELECT
strTemp = Mid$(strTemp, 7, Len(strTemp))
strTemp = LTrim(strTemp)
strTemp = RTrim(strTemp)
If InStr(1, strTemp, "distinctrow") <> 0 Then
strTemp = Mid$(strTemp, 12, Len(strTemp))
End If
If InStr(1, strTemp, "distinct") <> 0 Then
strTemp = Mid$(strTemp, 10, Len(strTemp))
End If
Set dbTemp = CodeDb
aryPos = 1
aryPosFieldName = 1
'process field names
While InStr(1, strTemp, ",") <> 0
'store field name
ReDim Preserve aryFieldNames(aryPosFieldName + 1)
aryFieldNames(aryPosFieldName) = LTrim(RTrim(Mid$(strTemp, 1, InStr(1, strTem
p, ",") - 1)))
'strip crlf if exists
If InStr(1, aryFieldNames(aryPosFieldName), vbCrLf) <> 0 Then
aryFieldNames(aryPosFieldName) = Mid$(aryFieldNames(aryPosFieldName), InSt
r(1, aryFieldNames(aryPosFieldName), vbCrLf) - 1, Len(aryFieldNames(aryPosFiel
dName)))
End If
'get field info
If InStr(1, aryFieldNames(aryPosFieldName), ".") <> 0 Then
Set tblTemp = dbTemp.TableDefs(Mid$(strTemp, 1, InStr(strTemp, ".") - 1))
'store field size
ReDim Preserve aryFieldSize(aryPos + 1)
aryFieldSize(aryPos) = tblTemp.Fields(Mid$(aryFieldNames(aryPosFieldName),
InStr(1, aryFieldNames(aryPosFieldName), ".") + 1, Len(aryFieldNames(aryPosFi
eldName)))).Size
'store field type
ReDim Preserve aryFieldType(aryPos + 1)
aryFieldType(aryPos) = tblTemp.Fields(Mid$(aryFieldNames(aryPosFieldName),
InStr(1, aryFieldNames(aryPosFieldName), ".") + 1, Len(aryFieldNames(aryPosFi
eldName)))).Type
aryPos = aryPos + 1
Set tblTemp = Nothing
aryPosFieldName = aryPosFieldName + 1
End If
strTemp = LTrim(Mid$(strTemp, InStr(1, strTemp, ",") + 1, Len(strTemp)))
Wend
'strip crlf if exists
If InStr(1, strTemp, vbCrLf) <> 0 Then strTemp = Mid$(strTemp, 1, InStr(1, str
Temp, vbCrLf) - 1)
If strTemp <> "" And Not IsNull(strTemp) Then
Set tblTemp = dbTemp.TableDefs(Mid$(strTemp, 1, InStr(strTemp, ".") - 1))
'store field name
ReDim Preserve aryFieldNames(aryPosFieldName + 1)
aryFieldNames(aryPosFieldName) = strTemp
'store field size
ReDim Preserve aryFieldSize(aryPos + 1)
aryFieldSize(aryPos) = tblTemp.Fields(Mid$(aryFieldNames(aryPosFieldName), I
nStr(1, aryFieldNames(aryPosFieldName), ".") + 1, Len(aryFieldNames(aryPosFiel
dName)))).Size
'store field type
ReDim Preserve aryFieldType(aryPos + 1)
aryFieldType(aryPos) = tblTemp.Fields(Mid$(aryFieldNames(aryPosFieldName), I
nStr(1, aryFieldNames(aryPosFieldName), ".") + 1, Len(aryFieldNames(aryPosFiel
dName)))).Type
aryPos = aryPos + 1
aryPosFieldName = aryPosFieldName + 1
Set tblTemp = Nothing
End If
'close temp database object
dbTemp.Close
'free objects
Set tblTemp = Nothing
Set dbTemp = Nothing
strTemp = Me.TXTSql
aryPosTables = 1
'extract table names from sql
strTemp = Mid$(Me.TXTSql, InStr(1, Me.TXTSql, "FROM"), Len(Me.TXTSql))
'strip ORDER BY if any
If InStr(1, strTemp, "ORDER") <> 0 Then
strTemp = Mid$(strTemp, 1, InStr(1, strTemp, "ORDER") - 1)
'strip crlf if exists
If InStr(1, strTemp, vbCrLf) <> 0 Then strTemp = Mid$(strTemp, 1, InStr(1,
strTemp, vbCrLf) - 1)
End If
'strip up to 'where' (if any)
If InStr(1, strTemp, "where") <> 0 Then
'store where clause
strWhere = LTrim(Mid$(strTemp, InStr(1, strTemp, "where") + 6, Len(strTemp
)))
strWhere = Mid$(strWhere, 1, InStr(1, strWhere, ";") - 1)
'strip where clause
strTemp = Mid$(strTemp, 1, InStr(1, strTemp, "where") - 1)
Else
'strip trailing ; if any
If InStr(1, strTemp, ";") <> 0 Then strTemp = Mid$(strTemp, 1, InStr(1, st
rTemp, ";") - 1)
End If
'core sql does not require FROM clause so strip it out
strCoreSql = LTrim(Mid$(strTemp, 5, Len(strTemp)))
'strip JOIN tables out if any
If InStr(1, strTemp, "join") = 0 Then 'if no JOINs
'strip crlf if exists
If InStr(1, strTemp, vbCrLf) <> 0 Then strTemp = Mid$(strTemp, 1, InStr(1,
strTemp, vbCrLf) - 1)
'save table name
ReDim Preserve aryTables(aryPosTables + 1)
aryTables(aryPosTables) = Mid$(strTemp, InStr(1, strTemp, " ") + 1, Len(str
Temp))
aryPosTables = aryPosTables + 1
strTemp = Mid$(strTemp, InStr(1, strTemp, " ") + 1, Len(strTemp))
Else 'if JOINs
strTemp = LTrim(Mid$(strTemp, 5, Len(strTemp)))
ReDim Preserve aryTables(aryPosTables + 1)
'save table name
aryTables(aryPosTables) = Mid$(strTemp, 1, InStr(1, strTemp, " ") - 1)
aryPosTables = aryPosTables + 1
strTemp = Mid$(strTemp, InStr(1, strTemp, " ") + 1, Len(strTemp))
'strip JOIN types and ON clauses
While InStr(1, strTemp, "join") <> 0
If Mid$(strTemp, 1, InStr(1, strTemp, " ") - 1) = "left" Then
strTemp = Mid$(strTemp, 11, Len(strTemp))
End If
If Mid$(strTemp, 1, InStr(1, strTemp, " ") - 1) = "right" Then
strTemp = Mid$(strTemp, 12, Len(strTemp))
End If
If Mid$(strTemp, 1, InStr(1, strTemp, " ") - 1) = "on" Then
strTemp = Mid$(strTemp, 4, Len(strTemp))
End If
'save table name
If aryPosTables = 1 Then
ReDim Preserve aryTables(aryPosTables + 1)
aryTables(aryPosTables) = Mid$(strTemp, 1, InStr(1, strTemp, " ") - 1)
aryPosTables = aryPosTables + 1
Else
For intNum2 = 1 To aryPosTables - 1
If aryTables(intNum2) = Mid$(strTemp, 1, InStr(1, strTemp, " ") - 1)
Then
intNum3 = 1
Exit For
End If
Next intNum2
'if table name unique store in array
If intNum3 <> 1 Then
ReDim Preserve aryTables(aryPosTables + 1)
aryTables(aryPosTables) = Mid$(strTemp, 1, InStr(1, strTemp, " ") - 1)
aryPosTables = aryPosTables + 1
End If
End If
strTemp = Mid$(strTemp, InStr(1, strTemp, " ") + 1, Len(strTemp))
Wend
End If
'write data to tables
strTemp  = String.Empty;
For intNum = 1 To aryPosFieldName - 1
'save explicit field info
strTemp = GetHumanName(Mid$(aryFieldNames(intNum), InStr(aryFieldNames(int
Num), ".") + 1, Len(aryFieldNames(intNum))))
If strTemp  = String.Empty; Then
MsgBox "You Must Specify A Display Name For Each Field!" & vbCrLf & _
"WARNING: Partial Data May Be Saved In: UsysFindFieldInfo" & vbC
rLf & _
"Please Check Table For Your Custom Field Names" & vbCrLf & _
"BEWARE: There Is A High Chance Some Of Your Field Names" & vbCr
Lf & _
"Are Standard OrbikMRP Fields Names And Should Not Be Deleted!",
vbCritical, "Error Save Failed"
Exit Sub
Else
strTemp2 = strTemp
End If
CodeDb.Execute "INSERT INTO FIND_FieldInfo (FND_FieldName,FND_TableName,FN
D_FieldDataType,FND_FieldLength,FND_ControlName)" & _
" VALUES ('" & Mid$(aryFieldNames(intNum), InStr(aryFieldNa
mes(intNum), ".") + 1, Len(aryFieldNames(intNum))) & "'," & _
"'" & Mid$(aryFieldNames(intNum), 1, InStr(aryFieldNames(in
tNum), ".") - 1) & "'," & _
"'" & aryFieldType(intNum) & "'," & aryFieldSize(intNum) &
",'" & strTemp & "')", dbSeeChanges + dbFailOnError
CodeDb.Execute "INSERT INTO FIND_SearchField (FND_searchtype,FND_FieldName
,FND_TableName,FND_searchorder) " & _
"VALUES ('" & Me.txtFindName & "','" & Mid$(aryFieldNames(i
ntNum), InStr(aryFieldNames(intNum), ".") + 1, Len(aryFieldNames(intNum))) & "
','" & _
Mid$(aryFieldNames(intNum), 1, InStr(aryFieldNames(intNum),
".") - 1) & "'," & intNum - 1 & ")"
Next intNum
'make tables list from array
strTemp  = String.Empty;
For intNum2 = 1 To aryPosTables - 1
strTemp = strTemp & aryTables(intNum2) & ","
Next intNum2
If Right$(strTemp, 1) = "," Then strTemp = Mid$(strTemp, 1, Len(strTemp) - 1)
'create field list for field: Searchfields
strFieldList  = String.Empty;
For intNum = 1 To UBound(aryFieldNames)
If Not NothingIn(aryFieldNames(intNum)) Then
If InStr(1, aryFieldNames(intNum), ".") <> 0 Then
If InStr(1, aryFieldNames(intNum), "[") = 0 Then
strFieldList = strFieldList & Mid$(aryFieldNames(intNum), InStr(aryF
ieldNames(intNum), ".") + 1, Len(aryFieldNames(intNum))) & ", "
Else
'strip []
strTemp2  = String.Empty;
strTemp2 = Mid$(aryFieldNames(intNum), InStr(aryFieldNames(intNum),
".") + 2, Len(aryFieldNames(intNum)) - 1)
strFieldList = strFieldList & Mid$(strTemp2, 1, Len(strTemp2) - 1) &
", "
End If
Else
strFieldList = strFieldList & aryFieldNames(intNum) & ", "
End If
End If
Next intNum
strFieldList = Mid$(strFieldList, 1, Len(strFieldList) - 2)
'save tables list
If strWhere <> "" Then
CodeDb.Execute "INSERT INTO FIND_Relations (FND_searchtype,FND_TableName,FN
D_qualifiedjoin,FND_searchcondition, FND_searchfields) " & _
"VALUES (" & Chr(34) & Me.txtFindName & Chr(34) & "," & Chr(
34) & strTemp & Chr(34) & "," & Chr(34) & strCoreSql & Chr(34) & "," & Chr(34)
& strWhere & Chr(34) & "," & Chr(34) & strFieldList & Chr(34) & ")"
Else
CodeDb.Execute "INSERT INTO FIND_Relations (FND_searchtype,FND_TableName,FN
D_qualifiedjoin, FND_searchfields) " & _
"VALUES (" & Chr(34) & Me.txtFindName & Chr(34) & "," & Chr(
34) & strTemp & Chr(34) & "," & Chr(34) & strCoreSql & Chr(34) & "," & Chr(34)
& strFieldList & Chr(34) & ")"
End If
'populate temp query with jibberish
Set qryFind = CodeDb.QueryDefs("qryORBIKFindQuery")
qryFind.SQL = "SELECT ThisIsADummyQuery FROM PleaseEnterYourSQLInHere"
qryFind.Close
Set qryFind = Nothing
'clear sql text box
Me.TXTSql  = String.Empty;
'clear find name
Me.txtFindName = vbNullString
'ha ha success!
MsgBox "Created New Find: " & Me.txtFindName & vbCrLf & "You can test it using
Form1 in lFind.accdb", vbInformation, "Success!"
'close objects
Set rstTemp = Nothing
Exit Sub
ERR_Error:
MsgBox "This Unexpected Error Has Occured:" & vbCrLf & Err.Description, "Fat
al Error"
End Sub
Private Sub Form_Close()
'Created 25/03/02 by Roger Williams for Orbik Electronics
'
On Error Resume Next
objFind.CloseFind
Set objFind = Nothing
End Sub
Private Sub txtFindName_BeforeUpdate(Cancel As Integer)
'Created 25/03/02 by Roger Williams for Orbik Electronics
'
'checks find name does not already exist
'
Dim rstRelations As DAO.Recordset
If Me.txtFindName  = String.Empty; Or IsNull(Me.txtFindName) Then Exit Sub
'Set qryDummy = CodeDb.CreateQueryDef("", "SELECT * FROM UsysFindRelations WHE
RE searchtype='" & Me.txtFindName & "'")
Set rstRelations = CodeDb.OpenRecordset("SELECT * FROM FIND_Relations WHERE FN
D_searchtype='" & Me.txtFindName & "'", dbOpenSnapshot, dbSeeChanges + dbFailO
nError)
'find already exists?
If Not rstRelations.EOF Then
MsgBox Me.txtFindName & ": This Find Already Exists", vbCritical, "Unique N
ame Required"
Cancel = True
SendKeys "{ESC}"
End If
rstRelations.Close
Set rstRelations = Nothing
End Sub

*/

