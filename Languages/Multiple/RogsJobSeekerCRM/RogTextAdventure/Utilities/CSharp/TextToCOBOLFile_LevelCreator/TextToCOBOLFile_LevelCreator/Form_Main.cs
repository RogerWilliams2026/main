using System.IO;
using System.Data;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;

namespace TextToCOBOLFile_LevelCreator
{
    /*
     Created 08/08/2024 By Roger Williams

     Creates level file data for the COBOL version of Rog's Text Adventure
     Each level is comprised of a number of rooms

     NOTE: this is a CREATOR not an editor, this is Phase1 so no need to be reinventing the wheel
           (just yet). This is for internal use not public domain. Hence no data validation, as I
           am not going to create a level for my game with incorrect data in it!
           Phase2 would be a full featured EDITOR.
          

     COBOL format is fixed length so:

     number  pic 99
     message pic x(20)

     10
     hello world!

     would be stored as:

     10hello world!        <- plus spaces at end to make a text string of 20 chars

     if number is less than 10 would need 0 added
     if string is less than 20 chars would need padding to be 20 chars long

     The COBOL room data for each level is stored thus (taken from the COBOL source file):

        01 REC_ROOM_INTERNAL.
                03 REC_ROOM OCCURS 40 TIMES.
                  05 INT_ROOMID PIC 99 VALUE ZEROES.
          *    'next 4 propoerties determine which room this one leads to 0=no room!
                  05 INT_NEXTROOMNORTH PIC 99 VALUE ZEROES.
                  05 INT_NEXTROOMSOUTH PIC 99 VALUE ZEROES.
                  05 INT_NEXTROOMEAST PIC 99 VALUE ZEROES.
                  05 INT_NEXTROOMWEST PIC 99 VALUE ZEROES.
          *'used for text to describe room to player
                  05 STR_DESC_INT1 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT2 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT3 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT4 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT5 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT6 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT7 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT8 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT9 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT10 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT11 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT12 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT13 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT14 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT15 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT16 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT17 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT18 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT19 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT20 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT21 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT22 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT23 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT24 PIC X(80) VALUE SPACES.
                  05 STR_DESC_INT25 PIC X(80) VALUE SPACES.
 

      So each data field needs to be filled with 0's/spaces to meet the fixed record length requirements
    */
    public partial class frmMain : Form
    {
        DataSet dsData;     //virtual DataSet for storing the new room data

        public frmMain()
        {
            InitializeComponent();
        }

        private void CreateCOBOLLevelFile()
        {
            /*
              Created 08/08/2024 By Roger Williams

              Creates the COBOL level text file from the dsData rows using the required fixed length
              strings COBOL dictates

            */

            string strOutput = "";
            int intNum = 1;
            StreamWriter stmFile = null;
            System.Windows.Forms.SaveFileDialog dlgSaveFile;

            //remind user of level filename requirements
            MessageBox.Show("Filename must be in format:\n LEVEL<number>_COBOL.txt");

            //get filename
            dlgSaveFile=new System.Windows.Forms.SaveFileDialog();
            dlgSaveFile.Title = "Chose Where To Save And Filename";
            dlgSaveFile.Filter = "Text File|*.txt";
            dlgSaveFile.DefaultExt = "txt";
            dlgSaveFile.FilterIndex = 0;
            dlgSaveFile.ShowDialog();

            if (dlgSaveFile.FileName.Length > 0)
            {

                //set stream to write file
                stmFile = new StreamWriter(dlgSaveFile.FileName);

                //iterate through the dataset
                foreach (DataRow drTemp in dsData.Tables["Rooms"].Rows)
                {
                    strOutput = string.Empty;
                    //write the numeric fields
                    strOutput = strOutput + drTemp.Field<string>("RM_RoomID");
                    strOutput = strOutput + drTemp.Field<string>("RM_NextRoomNorth");
                    strOutput = strOutput + drTemp.Field<string>("RM_NextRoomSouth");
                    strOutput = strOutput + drTemp.Field<string>("RM_NextRoomEast");
                    strOutput = strOutput + drTemp.Field<string>("RM_NextRoomWest");
                    //add room description text
                    for (intNum = 1; intNum != 26; intNum++)
                    {
                        strOutput = strOutput + drTemp.Field<string>("RM_Desc" + intNum.ToString());
                    }

                    //write line to file
                    stmFile.WriteLine(strOutput);
                }

                stmFile.Close();
                stmFile.Dispose();
                MessageBox.Show("Level File Creatd!");
            }

        }
        private void Init()
        {
            /*
              Created 08/08/2024 By Roger Williams

              Creates virtual table to contain the COBOL room record
              this is used to add rooms so the level file can be created

              NOTE: making ALL fields string even though the first 4 columns are numeric
                    this makes handling adding 0 to the start easier!
            */

            DataColumn dcTemp;
            int intNum = 1;

            dsData = new DataSet();
            dsData.Tables.Add("Rooms");
            //create numeric fields
            dcTemp = dsData.Tables["Rooms"].Columns.Add();
            dcTemp.ColumnName = "RM_RoomID";
            dcTemp.MaxLength = 2;
            dcTemp.DataType = typeof(string);

            dcTemp = dsData.Tables["Rooms"].Columns.Add();
            dcTemp.ColumnName = "RM_NextRoomNorth";
            dcTemp.MaxLength = 2;
            dcTemp.DataType = typeof(string);

            dcTemp = dsData.Tables["Rooms"].Columns.Add();
            dcTemp.ColumnName = "RM_NextRoomSouth";
            dcTemp.MaxLength = 2;
            dcTemp.DataType = typeof(string);

            dcTemp = dsData.Tables["Rooms"].Columns.Add();
            dcTemp.ColumnName = "RM_NextRoomEast";
            dcTemp.MaxLength = 2;
            dcTemp.DataType = typeof(string);

            dcTemp = dsData.Tables["Rooms"].Columns.Add();
            dcTemp.ColumnName = "RM_NextRoomWest";
            dcTemp.MaxLength = 2;
            dcTemp.DataType = typeof(string);

            //now add the 25  lines for the room description (what player sees)
            for (intNum = 1; intNum != 26; intNum++)
            {
                dcTemp = dsData.Tables["Rooms"].Columns.Add();
                dcTemp.ColumnName = "RM_Desc" + intNum.ToString();
                dcTemp.MaxLength = 80;   //maximum length COBOL console supportas
                dcTemp.DataType = typeof(string);
            }

            //create primary key for searching later
            dsData.Tables["Rooms"].PrimaryKey = new DataColumn[] { dsData.Tables["Rooms"].Columns["RM_RoomID"] };
        }

        private void PrepareData()
        {
            /*
              Created 08/08/2024 By Roger Williams

              Prepares the data before saving to dsDATA
             
              Specifically:

              prefixes 0 to any number <10
              pads strings to make sure exactly 80 chars long
            */

            string strTemp = "";
            int intNum = 0;

            //look through the forms controls
            foreach (Control CONTemp in this.Controls)
            {
                if (CONTemp is TextBox)
                {
                    //check if room description control
                    if (CONTemp.Name.Contains("TXTDesc"))
                    {
                        //pad to 80 chars long if short
                        if (CONTemp.Text.Length < 80)
                        {
                            //special case if contains nothing at all
                            if (CONTemp.Text.Length == 0)
                            {
                                for (intNum = 1; intNum != 81; intNum++)
                                {
                                    CONTemp.Text = CONTemp.Text + " ";
                                }
                            }
                            else
                            {
                                //pad away!
                                while (CONTemp.Text.Length != 80)
                                {
                                    CONTemp.Text = CONTemp.Text + " ";
                                }
                            }
                        }
                    }
                    else
                    {
                        //process the numeric fields
                        if (CONTemp.Name.Contains("Room"))
                        {
                            if (CONTemp.Text.Length < 2)
                            {
                                CONTemp.Text = "0" + CONTemp.Text;
                            }
                        }
                    }
                }
            }
        }
        private void ClearForm()
        {
            /*
              Created 08/08/2024 By Roger Williams

              Clears the textbox controls
            */

            //can do this as all testboxes contain level data
            foreach (Control CONTemp in this.Controls)
            {
                if (CONTemp is TextBox)
                {
                    CONTemp.Text = string.Empty;
                }
            }
            //focus on first text control
            this.TXTRoomID.Focus();
        }
        private void BTNClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
           //save dsData data to actual text file
            CreateCOBOLLevelFile();
        }

        private void BTNSaveRoom_Click(object sender, EventArgs e)
        {
            /*
              Created 08/08/2024 By Roger Williams

              Adds room to dsData
            */

            DataRow drTemp;
            int intNum = 0;

            //make sure data is padded correctly
            PrepareData();
            //create new row
            drTemp = dsData.Tables["Rooms"].NewRow();
            //populate
            drTemp["RM_RoomID"] = this.TXTRoomID.Text;
            drTemp["RM_NextRoomNorth"] = this.TXTRoomNorthID.Text;
            drTemp["RM_NextRoomSouth"] = this.TXTRoomSouthID.Text;
            drTemp["RM_NextRoomEast"] = this.TXTRoomEastID.Text;
            drTemp["RM_NextRoomWest"] = this.TXTRoomWestID.Text;

            for (intNum = 1; intNum != 26; intNum++)
            {
                drTemp["RM_Desc" + intNum.ToString()] = this.Controls["TXTDesc" + intNum.ToString()].Text;
            }
            //add row to table
            dsData.Tables["Rooms"].Rows.Add(drTemp);
            //tell user saved
            MessageBox.Show("Saved!");
        }

        private void TXTRoomNorthID_Enter(object sender, EventArgs e)
        {
            /*
              Created 08/08/2024 By Roger Williams

             Checks room ID is unique in dsData
             If length <2 adds 0 to start
      
           */
            DataRow drTemp;
            string strTemp = this.TXTRoomID.Text;
            //is number < 10?
            if (strTemp.Length == 1)
            {
                strTemp = "0" + strTemp;
            }
            //look for ID user entered
            drTemp = dsData.Tables[0].Rows.Find(strTemp);
            //if found already exists!
            if (drTemp != null)
            {
                MessageBox.Show("Room ID Exists", "Duplicate Room ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //undo users entry
                this.TXTRoomID.Undo();
            }
        }
    }
}
