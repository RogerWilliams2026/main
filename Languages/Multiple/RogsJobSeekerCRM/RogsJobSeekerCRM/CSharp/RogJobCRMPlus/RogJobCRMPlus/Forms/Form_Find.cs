using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

/*
    Modified 15/03/2026 By Roger Williams

    added new table: Find_Operators

    Note: tables does NOT contain "between" as this is added exclusively for dates 
 

    Created 12/08/2025 By Roger Williams

    Design taken from Access97 version
    
    Uses the 3 dictionaries (primary key etc) for available columns to search 
    Uses datagrid bound to to result query for result
    Passes back primary key value  to calling forms public var: pubstrFindValue

    Uses constructor to get calling form name and search type to use

*/
namespace RogJobCRMPlus.Forms
{
    public partial class frmFind : Form
    {

        string strSearchName = String.Empty;

        //for datagrid
        BindingSource BDSQuery = null;

        //for line
        Brush BRUTemp = new SolidBrush(Color.Black);
        Pen PENTemp = new Pen(Color.Black);

        //for manual mouse move of form
        bool blnDragging = false;
        Point pntLastLocation;

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


        private void GetFindlabelName()
        {
            /*
               Created 17/03/2026 By Roger Williams

               read newly added field ffr_labelname to set form title

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT " + Modules.clsTables.GetTertiaryField(Modules.clsTables.CNST_STR_TABLE_FIND_RELATIONS) + " FROM " + Modules.clsTables.CNST_STR_TABLE_FIND_RELATIONS
                                       + " WHERE FFR_SearchType = '" + strSearchName + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    SQLRead.Read();
                    this.LBLTitle.Text = SQLRead["FFR_LabelName"].ToString();
                    SQLRead.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        //private void PopulateForm()
        //{
        //    /*
        //      Modified 15/03/2026 By Roger Williams

        //      added new table: Find_Operators

        //      Note: tables does NOT contain "between" as this is added exclusively for dates

        //      populates CMBfieldxComparison with values


        //      Created 12/08/2025 By Roger Williams

        //      Populates <field> controls from find_fieldinfo table

        //      for this application only first 3 (if there are that many) columns are used
        //      that is ignore column 0 (ID)

        //      create data controls dynamically as if numeric/date want to use appropriate control!

        //      also populates cmbfield<number.comparison combobox from modules.clsdata.lstFindOperators

        //    */

        //    string strTemp1 = String.Empty;
        //    string strLabelText = String.Empty;
        //    string strTablename = String.Empty;
        //    string strFieldName = String.Empty;
        //    string strDataType = String.Empty;

        //    SqlConnection SQLConn;
        //    SqlCommand SQLCmd;
        //    SqlDataReader SQLRead;
        //    //form controls for column choice
        //    int intControlNumber = 1;

        //    try
        //    {

        //        //clear comparison comboboxes
        //        this.CMBField1Comparison.Items.Clear();
        //        this.CMBField2Comparison.Items.Clear();
        //        this.CMBField3Comparison.Items.Clear();
        //        this.CMBField4Comparison.Items.Clear();
        //        this.CMBField5Comparison.Items.Clear();
        //        this.CMBField6Comparison.Items.Clear();


        //        using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
        //        {
        //            SQLConn.Open();
        //            SQLCmd = SQLConn.CreateCommand();
        //            SQLCmd.CommandText = "SELECT " +Modules.clsTables.GetSecondaryField(Modules.clsTables.CNST_STR_TABLE_FIND_OPERATORS) + " FROM " + Modules.clsTables.CNST_STR_TABLE_FIND_OPERATORS + ";";
        //            SQLCmd.CommandType = CommandType.Text;
        //            SQLRead = SQLCmd.ExecuteReader();

        //            while (SQLRead.Read())
        //            {
        //                this.CMBField1Comparison.Items.Add(SQLRead["FFC_Operator"].ToString());
        //                this.CMBField2Comparison.Items.Add(SQLRead["FFC_Operator"].ToString());
        //                this.CMBField3Comparison.Items.Add(SQLRead["FFC_Operator"].ToString());
        //                this.CMBField4Comparison.Items.Add(SQLRead["FFC_Operator"].ToString());
        //                this.CMBField5Comparison.Items.Add(SQLRead["FFC_Operator"].ToString());
        //                this.CMBField6Comparison.Items.Add(SQLRead["FFC_Operator"].ToString());
        //            }

        //            SQLRead.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Whoops!
        //        MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        this.Close();
        //    }

        //    try
        //    {
        //        using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
        //        {
        //            SQLConn.Open();
        //            SQLCmd = SQLConn.CreateCommand();
        //            SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_FIND_FIELDINFO + " WHERE " + Modules.clsTables.GetSecondaryField(Modules.clsTables.CNST_STR_TABLE_FIND_FIELDINFO) + " = '" + strSearchName + "' ORDER BY FFI_Order;";
        //            SQLCmd.CommandType = CommandType.Text;
        //            SQLRead = SQLCmd.ExecuteReader();

        //            while (SQLRead.Read())
        //            {
        //                strLabelText=SQLRead["FFI_LabelText"].ToString();
        //                strDataType = SQLRead["FFI_FieldDataType"].ToString();
        //                strFieldName= SQLRead["FFI_FieldName"].ToString();
        //                strTablename = SQLRead["FFI_TableName"].ToString();

        //                //label for telling user what column is available
        //                ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Text
        //                        = strLabelText;

        //                ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left = 10;
        //                ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Top = 10;

        //                //set tag to field name
        //                ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Tag
        //                    = SQLRead["FFI_TableName"].ToString() + "." + strFieldName;

        //                //use datatype to create appropirate value control
        //                switch (strDataType)
        //                {
        //                    //To Do correct text box widths, cmbcomparision position
        //                    case "string":
        //                        {
        //                            //set max text size to fields length
        //                            ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
        //                                     .MaxLength = (int)SQLRead["FFI_FieldLength"];
        //                            //set tag to data type (needed for creating SQl query to get data)
        //                            ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
        //                                     .Tag = strDataType;
        //                            //set width if >= 50 /2 if <50 show as 25 / 10

        //                            if (((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
        //                                  .MaxLength >= 50)
        //                            {
        //                                if (((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
        //                                      .MaxLength == 50)
        //                                {
        //                                    ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
        //                                        .Width =
        //                                        (((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).MaxLength / 2) * 10;
        //                                }
        //                                else
        //                                {
        //                                    //limit to 25 characters
        //                                    ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
        //                                        .Width = 250;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
        //                                    .Width =
        //                                    ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).MaxLength * 10;

        //                            }

        //                            //set position
        //                            ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).Top = 8;
        //                            //position comparison combobox and value control
        //                            CMBFieldOperator.Left
        //                                = ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
        //                                + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;


        //                            ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).Left =
        //                                CMBFieldOperator.Left
        //                                         + CMBFieldOperator.Width + 10;
        //                            //     this.Controls["PANField" + intControlNumber.ToString()].Controls.Add(TXTTemp);
        //                            ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).Visible = true;
        //                            break;
        //                        }
        //                    case "datetime":
        //                        {
        //                            //two datetimepickers are used from and to

        //                            //set tag to data type (needed for creating SQl query to get data)
        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Tag = strDataType;

        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Top = 6;
        //                            //position comparison combobox and value control
        //                            //CMBFieldOperator.Left =
        //                            //    ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
        //                            //    + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;

        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Left
        //                                = this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()].Left
        //                                + this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()].Width + 10;
        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Width = 135;

        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Visible = true;

        //                            //move CMBField1Comparison
        //                            ((ComboBox)CMBFieldOperator).Left =
        //                                       ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Width
        //                                       + 80;

        //                            //set tag to data type (needed for creating SQl query to get data)
        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Tag = strDataType;

        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Top = 6;
        //                            //position comparison combobox
        //                            //CMBFieldOperator.Left =
        //                            //    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Left
        //                            //    + ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Width + 20;

        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Left =
        //                                 ((ComboBox)CMBFieldOperator).Left +
        //                                 80;
        //                            //   = this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"].Left
        //                            //   + this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"].Width + 10;
        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Width = 135;

        //                            ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Visible = true;

        //                            //because it is a date can use BETWEEN comparison so add it!
        //                            ((ComboBox)CMBFieldOperator).Items.Add("Between");
        //                            break;
        //                        }
        //                    case "numeric":
        //                        {
        //                            //set max text size to fields length
        //                            NUDField.Maximum = (int)SQLRead["FFI_FieldLength"];
        //                            NUDField.DecimalPlaces = (int)SQLRead["FFI_DecimalPlaces"];                                    //     NUDTemp.Name = "NUDField" + intControlNumber.ToString() + "Value";
        //                            //set tag to data type (needed for creating SQl query to get data)
        //                            NUDField.Tag = strDataType;
        //                            NUDField.Top = 10;
        //                            NUDField.Width = 100;
        //                            //position comparison combobox and value control
        //                            CMBFieldOperator.Left =
        //                                ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
        //                                + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;

        //                            NUDField.Left = CMBFieldOperator.Left
        //                                         + CMBFieldOperator.Width + 10;
        //                            NUDField.Visible = true;
        //                            break;
        //                        }
        //                    case "bool":
        //                        {
        //                            //set tag to data type (needed for creating SQl query to get data)
        //                            ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Tag = strDataType;
        //                            ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Top = 10;
        //                            ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Width = 15;
        //                            //position comparison combobox and value control
        //                            CMBFieldOperator.Left =
        //                                ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
        //                                + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;

        //                            ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Left = CMBFieldOperator.Left
        //                                         + CMBFieldOperator.Width + 10;
        //                            ((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Visible = true;

        //                            //hide comparison combobox if checkbow shown
        //                            CMBFieldOperator.Visible = false;
        //                            break;
        //                        }
        //                }


        //                //set for next line
        //                intControlNumber++;
        //            }

        //            SQLRead.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Whoops!
        //        MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        this.Close();
        //    }
        //}


        private void PopulateForm()
        {
            /*
              Modified 16/03/2026 By Roger Williams

              now dynamically creates the controls
            
              Modified 15/03/2026 By Roger Williams

              added new table: Find_Operators

              Note: tables does NOT contain "between" as this is added exclusively for dates

              populates CMBfieldxComparison with values

            
              Created 12/08/2025 By Roger Williams

              Populates <field> controls from find_fieldinfo table

              for this application only first 3 (if there are that many) columns are used
              that is ignore column 0 (ID)

              create data controls dynamically as if numeric/date want to use appropriate control!

              also populates cmbfield<number.comparison combobox from modules.clsdata.lstFindOperators
              
            */

            string strTemp1 = String.Empty;
            string strLabelText = String.Empty;
            string strTableName = String.Empty;
            string strFieldName = String.Empty;
            string strDataType = String.Empty;

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            //form controls for column choice
            int intControlNumber = 1;
            int intFieldLength = 0;


            List<string> lstOperators = new List<string>();
            DateTimePicker DTPFieldFrom = new DateTimePicker();
            DateTimePicker DTPFieldTo = new DateTimePicker();
            ComboBox CMBFieldOperator = new ComboBox();
            TextBox TXTField = new TextBox();
            NumericUpDown NUDField = new NumericUpDown();
            CheckBox CHKField = new CheckBox();
            CheckBox CHKPanel = new CheckBox();
            Panel PANField = new Panel();
            Label LBLField = new Label();



            int intCHKPanelY = 50;
            int intPANFieldY = 53;

            void PopulateOperatorList()
            {
                /*
                  Created 17/03/2026 By Roger Williams

                  adds operators into list for adding into form comtron cmboperator<x>


                */
                try
                {
                    using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                    {
                        SQLConn.Open();
                        SQLCmd = SQLConn.CreateCommand();
                        SQLCmd.CommandText = "SELECT " + Modules.clsTables.GetSecondaryField(Modules.clsTables.CNST_STR_TABLE_FIND_OPERATORS) + " FROM " + Modules.clsTables.CNST_STR_TABLE_FIND_OPERATORS + ";";
                        SQLCmd.CommandType = CommandType.Text;
                        SQLRead = SQLCmd.ExecuteReader();

                        while (SQLRead.Read())
                        {
                            lstOperators.Add(SQLRead["FFC_Operator"].ToString());
                        }

                        SQLRead.Close();
                    }
                }
                catch (Exception ex)
                {
                    //Whoops!
                    MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }


            //****end sub func


            PopulateOperatorList();

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_FIND_FIELDINFO + " WHERE " + Modules.clsTables.GetSecondaryField(Modules.clsTables.CNST_STR_TABLE_FIND_FIELDINFO) + " = '" + strSearchName + "' ORDER BY FFI_Order;";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        strLabelText = SQLRead["FFI_LabelText"].ToString();
                        strDataType = SQLRead["FFI_FieldDataType"].ToString();
                        strFieldName = SQLRead["FFI_FieldName"].ToString();
                        strTableName = SQLRead["FFI_TableName"].ToString();
                        intFieldLength = Convert.ToInt16(SQLRead["FFI_FieldLength"]);

                        //create selection panel+controls for each record found

                        //create panel
                        PANField = new Panel();
                        PANField.Left = 40;
                        PANField.Top = intPANFieldY;
                        PANField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        PANField.Name = "PANField" + intControlNumber.ToString();
                        PANField.Size = new System.Drawing.Size(470, 31);

                        //check chkpanel
                        CHKPanel = new CheckBox();
                        CHKPanel.Name = "CHKPanel" + intControlNumber.ToString();
                        CHKPanel.Left = 18;
                        CHKPanel.Top = PANField.Top + 2;
                        CHKPanel.Text = string.Empty;
                        CHKPanel.Width = 15;
                        CHKPanel.CheckedChanged += Custom_CheckedChanged;
                        this.Controls.Add(CHKPanel);

                        intCHKPanelY = intPANFieldY + 2;
                        intPANFieldY = intPANFieldY + 60;

                        CMBFieldOperator = new ComboBox();
                        CMBFieldOperator.FormattingEnabled = true;
                        CMBFieldOperator.Location = new System.Drawing.Point(179, 6);
                        CMBFieldOperator.Name = "CMBFieldOperator" + intControlNumber.ToString();
                        CMBFieldOperator.Size = new System.Drawing.Size(48, 21);
                        CMBFieldOperator.TabIndex = 3;
                        CMBFieldOperator.Text = "=";
                        CMBFieldOperator.KeyDown += CustomKeyDown;
                        //add operators from list
                        foreach (string strItem in lstOperators)
                        {
                            CMBFieldOperator.Items.Add(strItem);
                        }



                        LBLField = new Label();
                        LBLField.AutoSize = true;
                        LBLField.Location = new System.Drawing.Point(5, 7);
                        LBLField.Name = "LBLField" + intControlNumber.ToString();
                        LBLField.Size = new System.Drawing.Size(0, 13);
                        LBLField.TabIndex = 5;
                        LBLField.Text = strLabelText;
                        LBLField.Tag = strFieldName;
                        //LBLField.Parent = PANField;

                        ////add controls to new panel
                        PANField.Controls.Add(LBLField);
                        PANField.Controls.Add(CMBFieldOperator);

                        //use datatype to create appropirate value control
                        switch (strDataType)
                        {
                            //To Do correct text box widths, cmbcomparision position
                            case "string":
                                {
                                    TXTField = new TextBox();
                                    TXTField.Location = new System.Drawing.Point(233, 3);
                                    TXTField.Name = "TXTField" + intControlNumber.ToString();
                                    TXTField.Size = new System.Drawing.Size(91, 20);
                                    TXTField.TabIndex = 6;
                                    TXTField.Visible = false;

                                    //set max text size to fields length
                                    TXTField.MaxLength = intFieldLength;
                                    TXTField.Tag = strDataType;

                                    if (TXTField.MaxLength >= 50)
                                    {
                                        if (TXTField.MaxLength == 50)
                                        {
                                            TXTField.Width = (TXTField.MaxLength / 2) * 10;
                                        }
                                        else
                                        {
                                            TXTField.Width = 250;
                                        }
                                    }
                                    else
                                    {
                                        TXTField.Width = TXTField.MaxLength * 10;
                                    }

                                    //set position
                                    TXTField.Top = 8;
                                    //position comparison combobox and value control
                                    CMBFieldOperator.Left = LBLField.Left + LBLField.Width + 20;

                                    TXTField.Left = CMBFieldOperator.Left + CMBFieldOperator.Width + 10;
                                    //     this.Controls["PANField" + intControlNumber.ToString()].Controls.Add(TXTTemp);
                                    TXTField.Visible = true;

                                    PANField.Controls.Add(TXTField);

                                    //((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                    //         .MaxLength = (int)SQLRead["FFI_FieldLength"];
                                    ////set tag to data type (needed for creating SQl query to get data)
                                    //((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                    //         .Tag = strDataType;
                                    //set width if >= 50 /2 if <50 show as 25 / 10

                                    //if (((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                    //      .MaxLength >= 50)
                                    //{
                                    //    if (((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                    //          .MaxLength == 50)
                                    //    {
                                    //        ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                    //            .Width =
                                    //            (((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).MaxLength / 2) * 10;
                                    //    }
                                    //    else
                                    //    {
                                    //        //limit to 25 characters
                                    //        ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                    //            .Width = 250;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()])
                                    //        .Width =
                                    //        ((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).MaxLength * 10;

                                    //}

                                    //set position
                                    //((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).Top = 8;
                                    ////position comparison combobox and value control
                                    //CMBFieldOperator.Left
                                    //    = ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
                                    //    + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;


                                    //((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).Left =
                                    //    CMBFieldOperator.Left
                                    //             + CMBFieldOperator.Width + 10;
                                    ////     this.Controls["PANField" + intControlNumber.ToString()].Controls.Add(TXTTemp);
                                    //((TextBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["TXTField" + intControlNumber.ToString()]).Visible = true;
                                    break;
                                }
                            case "datetime":
                                {
                                    //two datetimepickers are used from and to

                                    DTPFieldFrom = new DateTimePicker();
                                    //     DTPFieldFrom.Location = new System.Drawing.Point(336, 0);
                                    DTPFieldFrom.Name = "DTPField" + intControlNumber.ToString() + "From";
                                    DTPFieldFrom.Size = new System.Drawing.Size(41, 20);
                                    DTPFieldFrom.TabIndex = 8;
                                    DTPFieldFrom.Visible = false;

                                    DTPFieldTo = new DateTimePicker();
                                    //    DTPFieldTo.Location = new System.Drawing.Point(152, 3);
                                    DTPFieldTo.Name = "DTPField" + intControlNumber.ToString() + "To";
                                    DTPFieldTo.Size = new System.Drawing.Size(41, 20);
                                    DTPFieldTo.TabIndex = 9;
                                    DTPFieldTo.Visible = false;

                                    //set tag to data type (needed for creating SQl query to get data)
                                    DTPFieldFrom.Tag = strDataType;

                                    DTPFieldFrom.Top = 6;
                                    //position comparison combobox and value control
                                    DTPFieldFrom.Left = LBLField.Left + LBLField.Width + 10;
                                    DTPFieldFrom.Width = 135;
                                    DTPFieldFrom.Visible = true;

                                    //move CMBField1Comparison
                                    CMBFieldOperator.Left = DTPFieldFrom.Width + 100;

                                    //set tag to data type (needed for creating SQl query to get data)
                                    DTPFieldTo.Tag = strDataType;
                                    DTPFieldTo.Top = 6;
                                    //position comparison combobox
                                    DTPFieldTo.Left = CMBFieldOperator.Left + 80;
                                    DTPFieldTo.Width = 135;

                                    DTPFieldTo.Visible = true;

                                    //because it is a date can use BETWEEN comparison so add it!
                                    CMBFieldOperator.Items.Add("Between");

                                    PANField.Controls.Add(DTPFieldTo);
                                    PANField.Controls.Add(DTPFieldFrom);

                                    ////set tag to data type (needed for creating SQl query to get data)
                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Tag = strDataType;

                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Top = 6;
                                    ////position comparison combobox and value control
                                    ////CMBFieldOperator.Left =
                                    ////    ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
                                    ////    + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;

                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Left
                                    //    = this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()].Left
                                    //    + this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()].Width + 10;
                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Width = 135;

                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Visible = true;

                                    ////move CMBField1Comparison
                                    //((ComboBox)CMBFieldOperator).Left =
                                    //           ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Width
                                    //           + 80;

                                    ////set tag to data type (needed for creating SQl query to get data)
                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Tag = strDataType;

                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Top = 6;
                                    ////position comparison combobox
                                    ////CMBFieldOperator.Left =
                                    ////    ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Left
                                    ////    + ((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "From"]).Width + 20;

                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Left =
                                    //     ((ComboBox)CMBFieldOperator).Left +
                                    //     80;
                                    ////   = this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"].Left
                                    ////   + this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"].Width + 10;
                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Width = 135;

                                    //((DateTimePicker)this.Controls["PANField" + intControlNumber.ToString()].Controls["DTPField" + intControlNumber.ToString() + "To"]).Visible = true;

                                    ////because it is a date can use BETWEEN comparison so add it!
                                    //((ComboBox)CMBFieldOperator).Items.Add("Between");
                                    break;
                                }
                            case "numeric":
                                {
                                    NUDField = new NumericUpDown();
                                    //      NUDField.Location = new System.Drawing.Point(237, 3);
                                    NUDField.Name = "NUDField" + intControlNumber.ToString();
                                    NUDField.Size = new System.Drawing.Size(62, 20);
                                    NUDField.TabIndex = 7;
                                    NUDField.Visible = false;
                                    //set max text size to fields length
                                    NUDField.Maximum = 10; // (int)SQLRead["FFI_FieldLength"];
                                    NUDField.DecimalPlaces = (int)SQLRead["FFI_DecimalPlaces"];
                                    //set tag to data type (needed for creating SQl query to get data)
                                    NUDField.Tag = strDataType;
                                    NUDField.Top = 10;
                                    NUDField.Width = 100;
                                    //position comparison combobox and value control
                                    CMBFieldOperator.Left = LBLField.Left + LBLField.Width + 20;

                                    NUDField.Left = CMBFieldOperator.Left
                                                 + CMBFieldOperator.Width + 10;
                                    NUDField.Visible = true;

                                    PANField.Controls.Add(NUDField);

                                    //NUDField.Maximum = (int)SQLRead["FFI_FieldLength"];
                                    //NUDField.DecimalPlaces = (int)SQLRead["FFI_DecimalPlaces"];                                    //     NUDTemp.Name = "NUDField" + intControlNumber.ToString() + "Value";
                                    ////set tag to data type (needed for creating SQl query to get data)
                                    //NUDField.Tag = strDataType;
                                    //NUDField.Top = 10;
                                    //NUDField.Width = 100;
                                    ////position comparison combobox and value control
                                    //CMBFieldOperator.Left =
                                    //    ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
                                    //    + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;

                                    //NUDField.Left = CMBFieldOperator.Left
                                    //             + CMBFieldOperator.Width + 10;
                                    //NUDField.Visible = true;
                                    break;
                                }
                            case "bool":
                                {
                                    CHKField = new CheckBox();
                                    CHKField.AutoSize = true;
                                    //       CHKField.Location = new System.Drawing.Point(90, 9);
                                    CHKField.Name = "CHKField" + intControlNumber.ToString();
                                    CHKField.Size = new System.Drawing.Size(15, 14);
                                    CHKField.TabIndex = 4;
                                    CHKField.UseVisualStyleBackColor = true;
                                    CHKField.Visible = false;
                                    //set tag to data type (needed for creating SQl query to get data)
                                    CHKField.Tag = strDataType;
                                    CHKField.Top = 10;
                                    CHKField.Width = 15;
                                    //position comparison combobox and value control
                                    CMBFieldOperator.Left = LBLField.Left + LBLField.Width + 20;

                                    CHKField.Left = CMBFieldOperator.Left + CMBFieldOperator.Width + 10;
                                    CHKField.Visible = true;
                                    PANField.Controls.Add(CHKField);

                                    //((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Tag = strDataType;
                                    //((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Top = 10;
                                    //((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Width = 15;
                                    ////position comparison combobox and value control
                                    //CMBFieldOperator.Left =
                                    //    ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Left
                                    //    + ((Label)this.Controls["PANField" + intControlNumber.ToString()].Controls["LBLField" + intControlNumber.ToString()]).Width + 20;

                                    //((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Left = CMBFieldOperator.Left
                                    //             + CMBFieldOperator.Width + 10;
                                    //((CheckBox)this.Controls["PANField" + intControlNumber.ToString()].Controls["CHKField" + intControlNumber.ToString()]).Visible = true;

                                    //hide comparison combobox if checkbox shown
                                    CMBFieldOperator.Visible = false;
                                    break;
                                }
                        }






                        PANField.Enabled = false;

                        this.Controls.Add(PANField);

                        //set for next line
                        intControlNumber++;
                    }

                    SQLRead.Close();

                    //resize form
                    this.Height = PANField.Top + PANField.Height + 20 + this.DGVResults.Height + 10;
                    //move datagrid
                    this.DGVResults.Top = PANField.Top + PANField.Height + 20;

                    //store next top position of panel (if there was one) to use as height for box
                    this.Tag = intPANFieldY;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private string GetSQL()
        {
            /*
              Created 12/08/2025 By Roger Williams

              converted from Access MRP system

              compiles the SQL string needed from the search datagrid from the SQL Server serach tables

              in find relations table:

              qualifiedjoin = INNER JOIN..
              searchfiled = fields from JOINed table to also show in results

            */


            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;

            string strSQL = "SELECT ";
            string strFields = String.Empty;
            string strFieldValues = String.Empty;

            void GetFieldValues()
            {
                /*
                  Created 13/08/2025 By Roger Williams

                  Creates list of field names and values and comparisons for query stores in strFieldValues

                  Note: checkboxes have no comparision use checked value

                */

                string strTemp1 = String.Empty;
                string strTemp2 = String.Empty;
                string strNum = String.Empty;
                string strLike = " LIKE ";


                foreach (Control ctlTemp in this.Controls)
                {
                    if (ctlTemp is Panel)
                    {
                        if (ctlTemp.Name.Contains("PANField"))
                        {
                            //extract control number
                            strNum = ctlTemp.Name.Substring(8, ctlTemp.Name.Length - 8);

                            if (this.Controls["PANField" + strNum].Enabled)
                            {
                                //add field name
                                strTemp1 = ((Label)this.Controls["PANField" + strNum].Controls["LBLField" + strNum]).Tag + " ";

                                //get operator
                                if (((ComboBox)this.Controls["PANField" + strNum].Controls["CMBFieldOperator" + strNum]).Visible)
                                {
                                    //add operator first check if * (like)
                                    if (((ComboBox)this.Controls["PANField" + strNum].Controls["CMBFieldOperator" + strNum]).Text == "Like")
                                    {
                                        strTemp2 += strLike;
                                    }
                                    else
                                    {
                                        strTemp2 += " " + ((ComboBox)this.Controls["PANField" + strNum].Controls["CMBFieldOperator" + strNum]).Text + " ";
                                    }
                                }

                                //get value 
                                if (this.Controls["PANField" + strNum].Controls.Find("TXTField" + strNum, true).Count() != 0)
                                {
                                    if (strTemp2 == strLike)
                                    {
                                        strTemp1 += strLike + "'%" + ((TextBox)this.Controls["PANField" + strNum].Controls["TXTField" + strNum]).Text
                                                        + "%' AND ";
                                    }
                                    else
                                    {
                                        strTemp1 += strTemp2 + "'" + ((TextBox)this.Controls["PANField" + strNum].Controls["TXTField" + strNum]).Text
                                                        + "' AND ";
                                    }
                                }

                                if (this.Controls["PANField" + strNum].Controls.Find("CHKField" + strNum, true).Count() != 0)
                                {
                                    if ((((CheckBox)this.Controls["PANField" + strNum].Controls["CHKField" + strNum]).Checked))
                                    {
                                        strTemp1 += " = 1 AND ";
                                    }
                                    else
                                    {
                                        strTemp1 += " = 0 AND ";
                                    }
                                }

                                if (this.Controls["PANField" + strNum].Controls.Find("NUDField" + strNum, true).Count() != 0)
                                {
                                    if (strTemp2 == strLike)
                                    {
                                        strTemp1 += strLike + "%" + ((NumericUpDown)this.Controls["PANField" + strNum].Controls["NUDField" + strNum]).Value.ToString()
                                                        + "% AND ";
                                    }
                                    else
                                    {
                                        strTemp1 += strTemp2 + ((NumericUpDown)this.Controls["PANField" + strNum].Controls["NUDField" + strNum]).Value.ToString()
                                                        + " AND ";
                                    }
                                }

                                if (this.Controls["PANField" + strNum].Controls.Find("DTPField" + strNum, true).Count() != 0)
                                {
                                    //Note: TWO datetimepickers are used: from and to
                                    strTemp2 = " BETWEEN '" + ((DateTimePicker)this.Controls["PANField" + strNum].Controls["DTPField" + strNum + "From"]).Text +
                                               "' AND '" + ((DateTimePicker)this.Controls["PANField" + strNum].Controls["DTPField" + strNum + "To"]).Text + "' ";

                                    strTemp1 += strTemp2;
                                }

                                strFieldValues += strTemp1;
                            }
                        }
                    }
                }

                if (strFieldValues.Length > 0)
                { 
                    //strip trailing: AND
                    strFieldValues = strFieldValues.Substring(0, strFieldValues.Length - 4);
                }
            }

            void GetFields()
            {
                /*
                  Created 13/08/2025 By Roger Williams

                  Creates list of fields for query stores in strFields uses LBLField<num> for alias

                  Note: also returns the primary key as regardless of user preference need to pass it back so calling form
                        can find record!
                */

                string strTemp = string.Empty; // Modules.clsTables.GetPrimaryField(SQLRead["FFR_TableName"].ToString()); //get primary key
                string strNum = string.Empty;

                foreach (Control ctlTemp in this.Controls)
                {
                    if (ctlTemp is Panel)
                    {
                        if (ctlTemp.Name.Contains("PANField"))
                        {
                            //extract control number
                            strNum = ctlTemp.Name.Substring(8, ctlTemp.Name.Length - 8);

                            //add field name
                            strFields += ((Label)this.Controls["PANField" + strNum.ToString()].Controls["LBLField" + strNum.ToString()]).Tag + " AS [" +
                                            ((Label)this.Controls["PANField" + strNum.ToString()].Controls["LBLField" + strNum.ToString()]).Text + "], ";
                        }
                    } 
                }

                strFields = strFields.Substring(0, strFields.Length - 2);
            }




            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    //get search details e.g. orderby/distinct etc
                    SQLCmd.CommandText = "SELECT * FROM " + Modules.clsTables.CNST_STR_TABLE_FIND_RELATIONS + " WHERE " + Modules.clsTables.GetSecondaryField(Modules.clsTables.CNST_STR_TABLE_FIND_RELATIONS) + " = '" + strSearchName + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        //get field values
                        GetFieldValues();
                        //get fields list
                        GetFields();

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
                            if (strFieldValues.Length >0)
                            { 
                                strSQL += " WHERE " + strFieldValues;
                            }

                                //add sort if not default ASC
                                if (SQLRead["FFR_Sort"].ToString() != "ASC")
                                {
                                    strSQL += " ORDER BY " + SQLRead["FFR_OrderBy"].ToString() + " " + SQLRead["FFR_Sort"].ToString() + ";";
                                }
                                else
                                {
                                    strSQL += " ORDER BY " + SQLRead["FFR_OrderBy"].ToString() + ";";
                                }
                            }
                            else
                            {
                                strSQL += " HAVING " + strFieldValues;
                                strSQL += " GROUP BY " + SQLRead["FFR_GroupBy"].ToString() + ";";
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

            return strSQL;
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

                    if (DSTStock.Tables[0].Rows.Count == 0)
                    {
                        this.DGVResults.DataSource = "";
                        this.DGVResults.Enabled = false;
                        MessageBox.Show("No Records Found!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    { 
                        this.DGVResults.DataSource = BDSQuery;
                        //set datagrid defaults
                        foreach (DataGridViewRow viewRow in this.DGVResults.Rows)
                        {
                            viewRow.ReadOnly = true;
                        }

                        this.DGVResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        this.DGVResults.Enabled = true;
                    }
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

                    PopulateForm();
                    this.DGVResults.Enabled = false;
                    GetFindlabelName();
                    //apply system theme
                    Modules.clsView.SetTheme(this, null);
                    //set form movement limits
                    this.Move += Modules.clsView.FormLocationChanged;
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


        private void BTNFind_Click(object sender, EventArgs e)
        {
            /*
              Created 13/08/2025 By Roger Williams

              Checks datetimepicker dates are valid if enable then gets query values!


            */

            DateTime DTETemp = DateTime.Now;
            string strNum = string.Empty;

            //make sure if datetimepickers visible that dates valid
            foreach (Control ctlTemp in this.Controls)
            {
                if (ctlTemp is Panel)
                { 
                    if (ctlTemp.Name.Contains("PANField"))
                    { 
                        //extract control number
                        strNum = ctlTemp.Name.Substring(8, ctlTemp.Name.Length - 8);

                        if (this.Controls["PANField" + strNum].Enabled)
                        {
                            //set ok flag (something selected)
                       //     blnOk = true;
                            if (this.Controls["PANField" + strNum.ToString()].Controls.Find("DTPField" + strNum.ToString() + "From", true).Count() != 0)
                            {
                                if (((DateTimePicker)this.Controls["PANField" + strNum.ToString()].Controls["DTPField" + strNum.ToString() + "From"]).Visible)
                                {
                                    if (((DateTimePicker)this.Controls["PANField" + strNum.ToString()].Controls["DTPField" + strNum.ToString() + "From"]).Value >
                                         ((DateTimePicker)this.Controls["PANField" + strNum.ToString()].Controls["DTPField" + strNum.ToString() + "To"]).Value)
                                    {
                                        MessageBox.Show("From Date Greater Than To Date!", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                            }

                            if (this.Controls["PANField" + strNum.ToString()].Controls.Find("TXTField" + strNum.ToString(), true).Count() != 0)
                            {
                                //check if textbox visible and NOT null
                                if (((TextBox)this.Controls["PANField" + strNum.ToString()].Controls["TXTField" + strNum.ToString()]).Visible)
                                {
                                    if (((TextBox)this.Controls["PANField" + strNum.ToString()].Controls["TXTField" + strNum.ToString()]).Text == String.Empty)
                                    {
                                  //      blnOk = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (DoSearch())
            {
                this.DGVResults.Enabled = true;
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


        private void frmFind_Paint(object sender, PaintEventArgs e)
        {
            //draw box around panels
            e.Graphics.DrawRectangle(PENTemp, 12, 44, 505, Convert.ToInt16(this.Tag) -60);
            //fill titlebar with PANTitle back colour
            Modules.clsView.FillTitleBar(e.Graphics, this.PANTitle.BackColor, this.PANTitle.Width, this.Width - this.PANTitle.Width, this.PANTitle.Height);
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
                this.Location = new Point(
                (this.Location.X - pntLastLocation.X) + e.X,
                (this.Location.Y - pntLastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void PANTitle_MouseUp(object sender, MouseEventArgs e)
        {
            blnDragging = false;
        }

        //***end class
    }
}
