using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogStock2026_Utilities.Screens
{
    public partial class frmUserGroups_Utilities : Form
    {
        bool blnNew = false;


        //for manual mouse move of form
        bool blnDragging = false;
        Point pntLastLocation;

        public frmUserGroups_Utilities()
        {
            InitializeComponent();
        }

        //************custom code*************

        private void ResetForm(string strKeep, bool blnEnable)
        {
            /*
             Created 18/06/2026 By Roger Williams

             Resets form 
             Enables/Disables form

            VARS

            strKeep     - control to leave
            blnEnable   - enable or disable form

            */

            //reset form

            //load users comnbo with current users
            Modules.clsView_Utilities.PopulateComboBoxes(this.CMBUser, Modules.clsTables_Utilities.CNST_STR_TABLE_LOGIN, "", "", "", "", "", false, false);
            Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
            //reset checkboxes
            Modules.clsView_Utilities.ResetTree(this.TVGroups.Nodes[0]);
            Modules.clsView_Utilities.ResetTree(this.TVGroups_old.Nodes[0]);
            blnNew = false;
        }


        private void SaveRecord()
        {
            /*
              Created 03/07/2026 By Roger Williams

              Creates/updates MENU_Group record  
              Iterates through treeview writing checked menuitems

              Note: checks first if any treeview items checked if none selected tells user and exits
                    checks if not new if anything has changed if not tells user and exits

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlTransaction SQLTrans = null;
            bool blnFound = false;
            List<string> LSTUserGroups = new List<string>();

            //no group name don't process
            if (this.TVGroups.Nodes.Count == 0) return;

            //check something selected i.e. at least one treeview node checked
            blnFound = Modules.clsView_Utilities.CheckAnyTreeItemSelected(this.TVGroups.Nodes[0]);

            if (!blnFound)
            {
                MessageBox.Show("No Menu Items Selected!", "Nothing To Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //open database
            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConn.Open();
                    SQLCmd = SQLConn.CreateCommand();
                    SQLTrans = SQLConn.BeginTransaction();
                    SQLCmd.Transaction = SQLTrans;

                    //get list of selected menu items from treeview
                    LSTUserGroups = Modules.clsView_Utilities.GetAnyTreeNodesSelected(this.TVGroups.Nodes[0]);

                    //create group
                    foreach (string strTemp in LSTUserGroups)
                    {
                        if (!blnNew)
                        {
                            //for quickness DELETE usergroup and re-create from combobox and treeview
                            SQLCmd.CommandText = "DELETE" + Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_USERGROUPS + " WHERE USRGRP_Group = '" + strTemp + "' AND USRGRP_User ='" + this.CMBUser.Text + "';";
                            SQLCmd.ExecuteNonQuery();
                        }

                        //filter for JUST menuitems as these havw a tag of 1
                        SQLCmd.CommandText = "INSERT INTO " + Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_USERGROUPS + " (USRGRP_Group, USRGRP_User) VALUES ('" + strTemp + "','" + this.CMBUser.Text + "');";
                        SQLCmd.ExecuteNonQuery();
                    }

                    SQLTrans.Commit();
                    //reset form and update menuitem combobox
                    ResetForm("", true);
                    MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SQLConn.Close();
            }
            catch (Exception ex)
            {
                if (SQLTrans.Connection != null)
                {
                    SQLTrans.Rollback();
                }

                MessageBox.Show("Error Saving Data:\n\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteRecord()
        {
            /*
              Created 28/07/2026 By Roger Williams

              deletes usergroup record using a transaction 

            */

            SqlConnection SQLConn;
            SqlCommand SQLCmd;
            SqlTransaction SQLTrans = null;
            List<string> lstUsergroups = new List<string>();

            if (blnNew)
            {
                //if new just clear form
                ResetForm("", false);
                return;
            }

            try
            {
                using (SQLConn = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    try
                    {
                        SQLConn.Open();
                        //start transction
                        SQLTrans = SQLConn.BeginTransaction();

                        SQLCmd = new SqlCommand();
                        //assign commands to the transaction
                        SQLCmd.Transaction = SQLTrans;

                        //get list of selected menu items from treeview
                        lstUsergroups = Modules.clsView_Utilities.GetAnyTreeNodesSelected(this.TVGroups.Nodes[0]);

                        //create group
                        foreach (string strTemp in lstUsergroups)
                        {
                            //for quickness DELETE usergroup and re-create from combobox and treeview
                            SQLCmd.CommandText = "DELETE" + Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_USERGROUPS + " WHERE USRGRP_Group = '" + strTemp + "' AND USRGRP_User ='" + this.CMBUser.Text + "';";
                            SQLCmd.ExecuteNonQuery();
                        }

                        //write changes
                        SQLTrans.Commit();
                        ResetForm("", false);
                        MessageBox.Show("Record Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        if (SQLTrans.Connection != null)
                        {
                            SQLTrans.Rollback();
                        }
                        MessageBox.Show("Error Deleting Data:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    SQLConn.Close();

                    //reset form and update users combobox
                    ResetForm("", true);
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Accessing Database:\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateTree()
        {
            /*
              Created 28/07/2026 By Roger Williams

              Populates tree with ALL records from Menu_Groups

            */

            TreeNode ndeNew = null;
            TreeNode ndeRoot = null;
            TreeNode ndeOld = null;
            TreeNode ndeRoot_old = null;
            SqlConnection SQLConnMenuItems;
            SqlCommand SQLCmdMenuItems;
            SqlDataAdapter DADMenuItems;
            DataSet DSTMenuItems;
            int intRows = 0;

            try
            {
                using (SQLConnMenuItems = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConnMenuItems.Open();
                    //load MenuItems items
                    SQLCmdMenuItems = new SqlCommand("SELECT DISTINCT GRP_Group FROM " + Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_GROUPS + " ORDER BY GRP_Group;", SQLConnMenuItems);
                    DADMenuItems = new SqlDataAdapter(SQLCmdMenuItems);
                    DSTMenuItems = new DataSet();
                    intRows = DADMenuItems.Fill(DSTMenuItems, Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_GROUPS);

                    //any records?
                    if (intRows == 0)
                    {
                        MessageBox.Show("Error Reading Data - No Records!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //clear tree
                        this.TVGroups.Nodes.Clear();
                        this.TVGroups_old.Nodes.Clear();

                        ndeRoot = this.TVGroups.Nodes.Add("Groups");
                        ndeRoot_old = this.TVGroups_old.Nodes.Add("Groups");

                        //populate visible and hidden treeviews
                        foreach (DataRow DARTemp in DSTMenuItems.Tables[Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_GROUPS].Rows)
                        {
                            ndeNew = ndeRoot.Nodes.Add(DARTemp["GRP_Group"].ToString());
                            ndeNew.Tag = 1;
                            ndeOld = ndeRoot_old.Nodes.Add(DARTemp["GRP_Group"].ToString());
                            ndeOld.Tag = 1;
                        }

                        this.TVGroups.Nodes[0].Expand();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LoadRecord()
        {
            /*
              Created 08/07/2026 By Roger Williams

              Loads record -populates treeview with menuitems selected i.e. sets checkbox to true

            */
            SqlConnection SQLConnMenuItems;
            SqlCommand SQLCmdMenuItems;
            SqlDataAdapter DADLOTMenuItems;
            DataSet DSTLOTMenuItems;
            int intRows = 0;

            
            try
            {
                using (SQLConnMenuItems = new SqlConnection(Modules.clsData_Utilities.CNST_STR_ODBC))
                {
                    SQLConnMenuItems.Open();
                    //load MenuItems items
                    SQLCmdMenuItems = new SqlCommand("SELECT * FROM " + Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_USERGROUPS + " WHERE USRGRP_User ='" + this.CMBUser.Text + "';", SQLConnMenuItems);
                    DADLOTMenuItems = new SqlDataAdapter(SQLCmdMenuItems);
                    DSTLOTMenuItems = new DataSet();
                    intRows = DADLOTMenuItems.Fill(DSTLOTMenuItems, Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_USERGROUPS);

                    //reset checkboxes
                    Modules.clsView_Utilities.ResetTree(this.TVGroups.Nodes[0]);
                    Modules.clsView_Utilities.ResetTree(this.TVGroups_old.Nodes[0]);

                    //any records?
                    if (intRows == 0)
                    {
                        MessageBox.Show("No Records Found!\n\nSystem Set To Add New Records", "No Matching Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: New");
                        blnNew = true;
                        return;
                    }
                    else
                    {
                        blnNew = false;
                        Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");

                        foreach (DataRow DARTemp in DSTLOTMenuItems.Tables[Modules.clsTables_Utilities.CNST_STR_TABLE_MENU_USERGROUPS].Rows)
                        {
                            //select in both treeviews
                            Modules.clsView_Utilities.FindValueInTreeAndCheck(this.TVGroups.Nodes[0], DARTemp["USRGRP_Group"].ToString());
                            Modules.clsView_Utilities.FindValueInTreeAndCheck(this.TVGroups_old.Nodes[0], DARTemp["USRGRP_Group"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //*********** form events *************



        private void Form_UsersGroups_Load(object sender, EventArgs e)
        {
            //load users comnbo with current users
            Modules.clsView_Utilities.PopulateComboBoxes(this.CMBUser, Modules.clsTables_Utilities.CNST_STR_TABLE_LOGIN, "", "", "", "", "", false, false);

            //check if no users
            if (this.CMBUser.Items.Count == 0)
            {
                MessageBox.Show("No Users Found!", "Error Accessing User Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                Modules.clsView_Utilities.UpdateStatusBar(this.STLStatus, "Mode: Edit");
                PopulateTree();
                //stop form being moved over show menu button
                this.LocationChanged += Modules.clsView_Utilities.FormLocationChanged;
                //apply system theme
                Modules.clsView_Utilities.SetTheme(this, null);
            }
        }

        private void CMBUser_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            Modules.clsView_Utilities.RemoveFromOpenForms(this.Text);
            this.Close();
        }

        private void CMBGRP_Group_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
              Created 28/07/2026 By Roger Williams

              undoes changes if new just clear form and reset treeview else set treeview to copies checkbox status 

            */

            List<string> lstNodes = new List<string>();

            {
                if (MessageBox.Show("Changes Made Undo?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }

                if (!blnNew)
                {
                    //reset treeview
                    Modules.clsView_Utilities.ResetTree(this.TVGroups.Nodes[0]);
                    //get checked nodes from old treeview
                    lstNodes = Modules.clsView_Utilities.GetAnyTreeNodesSelected(this.TVGroups_old.Nodes[0]);

                    foreach (string strTemp in lstNodes)
                    {
                        Modules.clsView_Utilities.FindValueInTreeAndCheck(this.TVGroups.Nodes[0], strTemp);
                    }
                }
                else
                {
                    ResetForm("", true);
                }
            }
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            if (blnNew)
            {
                if (MessageBox.Show("Delete Record?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show("Delete Record?", "Erase Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }
            }
            //delete usergroup
            DeleteRecord();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            //save data
            SaveRecord();
        }

        private void CMBUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void TVGroups_Click(object sender, EventArgs e)
        {

        }

        private void TVGroups_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                if (e.Node.Checked) 
                { 
                    Modules.clsView_Utilities.CheckGroup(e.Node);
                }
                else
                {
                    Modules.clsView_Utilities.UnCheckGroup(e.Node);
                }
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
    }
}
