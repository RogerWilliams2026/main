using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2026_Utilities.Screens
{
    public partial class Form_ThemePreview : Form
    {
        BindingSource BDSStock = null;

        public Form_ThemePreview()
        {
            InitializeComponent();
        }

        private void LoadLotsIntoDataGrid()
        {
            /*
              Created 22/07/2026 By Roger Williams

              Populates lot gridview with lone stock item using bindingsource

            */

            SqlConnection SQLConn = null;
            SqlCommand SQLCmd = null;
            SqlDataAdapter DADStock = null;
            DataSet DSTStock = null;

            SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC);
            SQLCmd = new SqlCommand("SELECT TOP 1 STKI_ItemID, STKI_UOM, STKI_ProductFamily, STKI_Price ,STKI_LocLot FROM Stock_Items ORDER BY STKI_ItemID;", SQLConn);

            try
            {
                SQLConn.Open();
                DADStock = new SqlDataAdapter(SQLCmd);
                DSTStock = new DataSet();
                DADStock.Fill(DSTStock);

                BDSStock = new BindingSource();
                BDSStock.DataSource = DSTStock;
                BDSStock.DataMember = DSTStock.Tables[0].TableName; //ALWAYS specify this else grid does not work!
                SQLConn.Close();
              
                this.TestDataGridView.DataSource = BDSStock;
                //set column headers as default is field names!
                this.TestDataGridView.Columns[0].HeaderText = "Item ID";
                this.TestDataGridView.Columns[1].HeaderText = "UOM";
                this.TestDataGridView.Columns[2].HeaderText = "Product Family";
                this.TestDataGridView.Columns[3].HeaderText = "Price";
                this.TestDataGridView.Columns[4].HeaderText = "Loc/Lot Tracked?";

                //test
                this.TestDataGridView.AutoResizeRow(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error\n\n" + ex.Message);
            }

            SQLConn.Dispose();
        }
        private void Form_ThemePreview_Load(object sender, EventArgs e)
        {
            this.TestTreeView.Nodes[0].ExpandAll();
            LoadLotsIntoDataGrid();
        }
    }
}
