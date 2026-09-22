using System.Windows.Forms;
/*
  Created 05/11/2024 by Roger Williams
            
  reads level datatext file and creates a new text file 
  writing the level data as a javaScript array of objects

*/

namespace ConvertLevelToJavaScriptObject
{
    public partial class frmMain : Form
    {
        string strLevelFileLocation = "";
        string strSaveFileLocation = "";

        public frmMain()
        {
            InitializeComponent();
        }

        private void ConvertFile()
        {
            /*
            Modified 13/11/2024 by Roger Williams
            
            code for upgrade to include which rooms cause win/loss properties in the room object
            NOTE: version one of the level file does not have this hence code disabled
                  BUT did enable to create the new structure for the javaScript version :)
            
            Created 05/11/2024 by Roger Williams
            
            reads level data and creates a new text file writing the level data as a javaScript array of objects
            includes empy first element so romm 1 is element 1

             

            example file excerpt:

1
0
3
0
2
It is a cold Monday morning. The rain beats heavily against the window, turning everything grey and miserable.
You stand by your desk, Windows 11 is chunterring to itself (as it does) and you know it will be at least 15 minutes

            there are 20 lines of level text
            */
            StreamReader srmReader = new StreamReader(strLevelFileLocation);
            StreamWriter srmWrite = new StreamWriter(strSaveFileLocation);
            string       strTemp = "";
            int          intNum = 0;

            if ((strLevelFileLocation.Length == 0 ) || (strSaveFileLocation.Length == 0)) {
                return;
            }

            //write "header"
            srmWrite.WriteLine("export const aryRooms = [");
            srmWrite.WriteLine("{");

            //write blank element so first room is actually array pos 1
            //write room number
            srmWrite.WriteLine("room: 0,");
            //write the four rooms available
            srmWrite.WriteLine("north: 0,");
            srmWrite.WriteLine("south: 0,");
            srmWrite.WriteLine("east:  0,");
            srmWrite.WriteLine("west:  0,");

            // added 13/11/2024 By Roger Williams 
            //for upgrade to include which rooms cause win/loss
            srmWrite.WriteLine("win:   false" + ",");
            srmWrite.WriteLine("lose:  false" + ",");
            //write the 20 lines of room decsription text

            for (intNum = 1; intNum != 21; intNum++)
            {
                srmWrite.WriteLine("line" + intNum.ToString() + ": \"" + strTemp + "\",");
            }
            //end of object
            srmWrite.WriteLine("},");

            //write rooms
            while (!srmReader.EndOfStream)
            {
                if (intNum !=0) srmWrite.WriteLine("{");

                strTemp = srmReader.ReadLine();
                //write room number
                srmWrite.WriteLine("room: " + strTemp + ",");
                //write the four rooms available
                strTemp = srmReader.ReadLine();
                srmWrite.WriteLine("north: " +strTemp + ",");
                strTemp = srmReader.ReadLine();
                srmWrite.WriteLine("south: " + strTemp + ",");
                strTemp = srmReader.ReadLine();
                srmWrite.WriteLine("east:  " + strTemp + ",");
                strTemp = srmReader.ReadLine();
                srmWrite.WriteLine("west:  " + strTemp + ",");

                // added 13/11/2024 By Roger Williams 
                //for upgrade to include which rooms cause win/loss
                //    strTemp = srmReader.ReadLine();
                //    srmWrite.WriteLine("win:   false" + ",");
                //    strTemp = srmReader.ReadLine();
                //    srmWrite.WriteLine("lose:  false" + ",");
                //write the 20 lines of room decsription text

                for (intNum = 1; intNum != 21; intNum++)
                {
                    strTemp = srmReader.ReadLine();
                    srmWrite.WriteLine("line" + intNum.ToString() + ": \"" + strTemp + "\",");
                }
                //end of object
                srmWrite.WriteLine("},");
            }
            //end of array
            srmWrite.WriteLine("];");
            srmReader.Close();
            srmWrite.Close();
            srmReader.Dispose();
            srmWrite.Dispose();
            MessageBox.Show("Converted File!");
        }
        private void BTNClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgFind = new OpenFileDialog();
            SaveFileDialog dlgSave = new SaveFileDialog();  

            dlgFind.Title = "Select Level File";
            dlgFind.ShowPreview= true;
            dlgFind.FileName = "*.txt";

            if (dlgFind.ShowDialog() == DialogResult.OK)
            {
                strLevelFileLocation = dlgFind.FileName;

                //show save to dialog
                dlgSave.Title = "Where To Save Converted File";
                dlgSave.FileName = Path.GetFileName(strLevelFileLocation);

                if (dlgSave.ShowDialog() == DialogResult.OK)
                {
                    strSaveFileLocation = dlgSave.FileName;
                    ConvertFile();
                }

                dlgSave.Dispose();
            }
                dlgFind.Dispose();

            }
        }
}
