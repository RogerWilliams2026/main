using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogRessourceCreator
{
    /*

      Created 09/12/2025 By Roger Williams

        creates custom resource file by adding the selected file into it

        supports:
      
        .txt
        .wav
        .mp3
        .aiff
        .png
        .gif
        .jpeg
        .jpg


        file format:

        header:

        [info]
            internal comments mot used by program
        [/info]

        sections:

        [text]
            ;   <- start of item
            name: 
            controlname:
            length: <in bytes>
            <.....data stored in bytes....>
            /;
        [/text]

        [sound]
            ;   <- start of item
            name: 
            controlname:
            length: <in bytes>
            <.....data stored in bytes....>
            /;
        [/sound]

        [image]
           ;   <- start of item
           name: 
           controlname:
           length: <in bytes>
           <.....data stored in Color data type....>
           /;
        [/image]

    */
    public partial class frmMain : Form
    {
        //******custom titelbar****
        //mouse move of form using custom titlebar
        //create a sub class for ease of use
        public static class User32_DLL
        {
            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        }

        private const int HTCAPTION = 0x2;
        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int CNST_INT_TITLEBARHEIGHT = 40;


        Brush bruTitleBar = new SolidBrush(Color.Olive);
        Brush bruTitleBarText = new SolidBrush(Color.White);
        Font fntTitleBar = new Font("Microsoft Sans Serif", 11); //title bar text font
        Pen penLine = new Pen(Color.Black, 1);
        //******end custom titlebar****

        bool blnIgnoreCheck = false;

        //stores filename and path
        Dictionary<string,string> dictFiles = new Dictionary<string, string> ();
        //stores file path and type
        Dictionary<string, string> dictFileTypes = new Dictionary<string, string>();

        public frmMain()
        {
            InitializeComponent();
        }


        private void UnCheckAllGroup(TreeNode ndeParent)
        {
            /*
               Created 01/08/2025 By Roger Williams

               UnChecks/checks children if all group e.g. forms is unchecked 

               VARS

               ndeParent    - parent node (need to filter for type node contents e.g. form)

            */

            TreeNode ndeGroup = null;
            int intNum = 0;

            ndeParent.Checked = false;

            foreach (TreeNode ndeTemp in ndeParent.Nodes)
            {
                if (ndeTemp.Nodes.Count == 0)
                {
                    ndeTemp.Checked = false;
                }

                foreach (TreeNode ndeType in ndeTemp.Nodes)
                {
                    //first check if top level (type) is checked if so check all children
                    if (ndeType.Checked)
                    {
                        ndeTemp.Checked = false;
                        ndeType.Checked = false;
                        //check children
                        ndeGroup = ndeType;
                        intNum = 0;

                        foreach (TreeNode ndeChild in ndeType.Nodes)
                        {
                            ndeChild.Checked = false;
                        }
                    }
                    else
                    {
                        //first check if top level (type) is unchecked if so uncheck all children
                        {
                            //check children
                            foreach (TreeNode ndeChild in ndeType.Nodes)
                            {
                                ndeChild.Checked = false;
                            }
                        }
                    }
                }
            }
        }

        private void CheckGroupFromChild(TreeNode ndeParent)
        {
            /*
               Created 01/08/2025 By Roger Williams

               Checks all nodes below parent

               VARS

               ndeParent        - child node (need to filter for type node contents e.g. form)

            */

            int intNum = 0;
            TreeNode ndeGroup = null;


            if (ndeParent.Text == this.TVDirs.Nodes[0].Text)
            {
                return;
            }

            foreach (TreeNode ndeChild in ndeParent.Nodes)
            {
                ndeChild.Checked = true;
                CheckGroupFromChild(ndeChild);
            }
        }

        private void RemoveFromResourcesListView()
        {
            /*

              Created 09/12/2025 By Roger Williams

              removes item from lvresources and adds back into listview
              using dictfiles for the tag


            */
            ListViewItem LVTemp = new ListViewItem();

            if (this.LVFiles.SelectedItems.Count == 0)
            {
                return;
            }

            LVTemp.Text = this.LVResources.SelectedItems[0].Text;

            var foundKey = dictFiles.FirstOrDefault(x => x.Value == LVTemp.Text).Key;

            LVTemp.Tag = foundKey;
            this.LVFiles.Items.Add(LVTemp);
            //remove from listbox
            this.LVResources.Items.Remove(this.LVResources.SelectedItems[0]);
            //remove from dictionaries
            dictFiles.Remove(foundKey);
            dictFileTypes.Remove(foundKey);
        }

        private void GetFilesIntoListView()
        {
            /*

              Created 09/12/2025 By Roger Williams

              puts files in selectred treeview folder into listview

              uses tag to store full file path


            */

            ListViewItem LVTemp = null;
            string[] aryFiles;
            string strFolder = string.Empty;

            //clear previous data
            this.LVFiles.Items.Clear();

            strFolder = this.TVDirs.SelectedNode.Tag.ToString();
            aryFiles = Directory.GetFiles(strFolder);
   
            foreach (string strTemp in aryFiles) 
            {
                LVTemp = new ListViewItem();
                LVTemp.Tag = strTemp;
                LVTemp.Text =  Path.GetFileName(strTemp);               
                this.LVFiles.Items.Add(LVTemp);
            }
        }

        private void GetDrives()
        {
            /*
              Created 09/11/2025 By Roger Williams

              populates cmbdrives with drives
            
            */

            DriveInfo[] aryDrives = DriveInfo.GetDrives();

            this.CMBDrives.Items.Clear();

            foreach (DriveInfo driDrive in aryDrives)
            {
                if (driDrive.IsReady)
                {
                    this.CMBDrives.Items.Add(driDrive.Name);
                }
            }
        }
        private void InitStatus(string strText)
        {
            /*
              Created 09/11/2025 By Roger Williams

              sets status bar text to strtext and progress bar to zero

              VARS

              strText   - text for status bar label
              
            */

            this.SPRRGProgress.Value = 0;
            this.SLBLStatus.Text = strText;
            this.STAStatus.Update();
        }

        private void IncrementProgress()
        {
            /*
              Created 09/11/2025 By Roger Williams

              incs progress bar when reaches max resets to 0

            */


            if (this.SPRRGProgress.Value == this.SPRRGProgress.Maximum)
            {
                this.SPRRGProgress.Value = this.SPRRGProgress.Maximum;
                this.SPRRGProgress.Value = 0;
            }
            else
            {
                this.SPRRGProgress.Value++;
            }

            this.STAStatus.Update();
        }

        private void GetAllDirectories()
        {
            /*
              Created 09/11/2025 By Roger Williams

              finds all folders and populates tree with them
            
              done manually to skip the bugs in getdirectories -> all sub dirs

              uses cmbdrives to determine drive to read 

            */

            string[] aryFolders;
            TreeNode ndeRoot = null;
            TreeNode ndeNode = null;

            void GetSubFolders(string strPath, TreeNode ndeTemp)
            {
                string[] arySubFolders;
                TreeNode ndeSubDir;
                int intNum = 0;
                string strSubTemp = string.Empty;

                this.SPRRGProgress.Maximum = 10000;
                this.SPRRGProgress.MarqueeAnimationSpeed = 200;



                try
                {
                    arySubFolders = Directory.GetDirectories(strPath);

                    //add to tree
                    foreach (string strSubDir in arySubFolders)
                    {
                        strSubTemp = strSubDir;

                        while (strSubTemp.Contains(@"\"))
                        {
                            intNum = strSubTemp.IndexOf(@"\");

                            if (intNum >= 0)
                            {
                                strSubTemp = strSubTemp.Substring(intNum + 1, strSubTemp.Length - intNum - 1);
                            }
                        }

                        IncrementProgress();
                        ndeSubDir = new TreeNode(strSubTemp);
                        ndeSubDir.Tag = strSubDir;
                        ndeTemp.Nodes.Add(ndeSubDir);
                        //get sub folders
                        GetSubFolders(strSubDir, ndeSubDir);
                    }
                }
                catch (Exception ex)
                { //do nothing!
                    ex = ex;
                }
            }


            ndeRoot = new TreeNode(this.CMBDrives.Text.Substring(0, 1));
            this.TVDirs.Nodes.Add(ndeRoot);

            //stage 1 get all root folders
            aryFolders = Directory.GetDirectories(this.CMBDrives.Text.Substring(0, 1) + @":\");
            InitStatus("Getting Directory Names");

            //add to tree
            foreach (string strDir in aryFolders)
            {
                IncrementProgress();
                ndeNode = new TreeNode(strDir.Substring(3, strDir.Length - 3));
                ndeNode.Tag = strDir;
                ndeRoot.Nodes.Add(ndeNode);
                //get sub folders
                GetSubFolders(strDir, ndeNode);
            }

            InitStatus("Ready");
        }

        private void AddFileToResources()
            /*

              Created 09/12/2025 By Roger Williams

              adds selected items from listview and REMOVES them from listview
              also adds filename and full path to dictFiles
              and full path and type to dictfiletype 


            */
        {

         ListViewItem LVNew = new ListViewItem();

         foreach (ListViewItem LVTemp in this.LVFiles.Items)
            {
               if (LVTemp.Selected)
                {
                    LVNew.Text = "Control Name";
                    LVNew.SubItems.Add(LVTemp.Text);
                    LVNew.Tag = LVTemp.Tag;
                    this.LVResources.Items.Add(LVNew);
                    //add to dictionaries
                    dictFiles.Add(LVTemp.Tag.ToString(), LVTemp.Text);

                    switch (Path.GetExtension(LVTemp.Tag.ToString().ToLower()))
                    {
                        case ".png":

                        case ".jpg":

                        case ".jpeg":

                        case ".gif":
                            dictFileTypes.Add(LVTemp.Tag.ToString(), "image");
                            break;
                        case ".txt":
                            dictFileTypes.Add(LVTemp.Tag.ToString(), "text");
                            break;
                        case ".mp3":
                        case ".aiff":
                        case ".wav":
                            dictFileTypes.Add(LVTemp.Tag.ToString(), "sound");
                            break;
                    }

                    this.LVFiles.Items.Remove(LVTemp);
                }

            }
        }


        //***************form events******
        private void Form1_Load(object sender, EventArgs e)
        {
            InitStatus("Ready");
            GetDrives();
        }

        private void CMBDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CMBDrives.Text != string.Empty)
            {
                GetAllDirectories();
            }
        }

        private void CMBDrives_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            //if pointer inside "title bar"
            if (e.Y <= CNST_INT_TITLEBARHEIGHT)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //move form
                    User32_DLL.ReleaseCapture();
                    User32_DLL.SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, new IntPtr(0));
                }
            }
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(bruTitleBar, 0, 0, this.Width, 30);
            e.Graphics.DrawString(this.Text, fntTitleBar, bruTitleBarText, 1, 5);
            e.Graphics.DrawLine(penLine, 0, 640, 1384, 640);
        }

        private void PICClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TVDirs_AfterCheck(object sender, TreeViewEventArgs e)
        {
            /*

              Created 09/12/2025 By Roger Williams

              due to yet another flaw in .Net when a treeview has checkboxes and one
              is checked the selecteditem node is NOT set
              so doing here instead!

            */

            this.TVDirs.SelectedNode = e.Node;
            GetFilesIntoListView();
        }

        private void BTNAdd_Click(object sender, EventArgs e)
        {
            //add to list
            AddFileToResources();
        }

            private void BTNRemove_Click(object sender, EventArgs e)
        {
            RemoveFromResourcesListView(); 
        }

        private void BTNCreate_Click(object sender, EventArgs e)
        {
            /*

              Created 09/12/2025 By Roger Williams

              creates the resource file

              stores in folder:

              c:\rogsresourcecreator


              reads each file into the aryData byte array
              writes into the appropriate section e.g. [image]. [text] etc

              uses streamwriter to write text then filestream to append byte data
              
            Note: if image loads into pictemp and appends width/height to end of file size property
                  after |

            */

            byte[] aryFileData;
            FileStream fstrWrite;
            StreamWriter strmWrite;
            string strFile = string.Empty;
            string strSection = string.Empty;
 
            //for images
            PictureBox picTemp = new PictureBox();
            int intWidth = 0;
            int intHeight = 0;
            int intX = 0;
            int intY = 0;

            void WriteFileData()
            {
             /*
                Created 10/12/2025 By Roger Williams

                writes raw byte data to resource file

                if image loads into hidden picturebox then extracts the colour data
                and writes to the resource file

             */


                Color[] aryColours;
                int intData = 0;
                int intA = 0;
                int intR = 0;
                int intG = 0;
                int intB = 0;
                Color clrTemp;
                Bitmap bmpTemp;
                Bitmap bmpOutput;

                //write file data bytes
                fstrWrite = new FileStream(@"C:\RogsResourceCreator\RogResourceFile.RogRes", FileMode.Append);

                if (strSection != "image")
                {
                    foreach (byte bytData in aryFileData)
                    {
                        fstrWrite.WriteByte(bytData);
                    }

                    fstrWrite.Close();
                }
                else
                {
                    bmpTemp = new Bitmap(picTemp.Image);

                    //write colour data to resource file
                    for (intY = 0; intY != intHeight; intY++)
                    {
                        for (intX = 0; intX != intWidth; intX++)
                        {
                            clrTemp = bmpTemp.GetPixel(intX, intY);
                            //write: A R G B
                            fstrWrite.WriteByte(clrTemp.A);
                            fstrWrite.WriteByte(clrTemp.R);
                            fstrWrite.WriteByte(clrTemp.G);
                            fstrWrite.WriteByte(clrTemp.B);
                        }
                    }

                    fstrWrite.Close();
                }
            }

            long GetImageSize()
            {
                /*
                   Created 11/12/2025 By Roger Williams

                   goes through the height/width incrementing inccolourcount to get
                   number of COLOR classes the image will contain as COLOR has
                   4 properties: A R G B

                */


                intWidth = picTemp.Image.Width;
                intHeight = picTemp.Image.Height;
                int intColourCount = 0;

                //write colour data to resource file
                for (intY = 0; intY != intHeight; intY++)
                {
                    for (intX = 0; intX != intWidth; intX++)
                    {
                        //inc by 4 for: A R G B
                        intColourCount++;
                        intColourCount++;
                        intColourCount++;
                        intColourCount++;
                    }
                }

                return intColourCount;
            }
            //***end sub funcs********



            if (this.LVResources.Items.Count == 0)
            {
                return;
            }

            //create resource folder if not exists
            if (!Directory.Exists(@"C:\RogsResourceCreator"))
            {
                Directory.CreateDirectory(@"C:\RogsResourceCreator");
            }

            //set temp image to autosize
            picTemp.SizeMode = PictureBoxSizeMode.AutoSize;

            //first sort dictfiletypes by value (file type)
            var dictFileTypesSorted = dictFileTypes.OrderBy(x => x.Value).ToList();

            //create resource file
            strmWrite = new StreamWriter(@"C:\RogsResourceCreator\RogResourceFile.RogRes");

            //write header
            strmWrite.WriteLine("[info]");
            strmWrite.WriteLine("************************************************");
            strmWrite.WriteLine("Created:" + DateTime.Now.ToString());
            strmWrite.WriteLine("Total Number Of Files:" + dictFileTypes.Count.ToString());
            strmWrite.WriteLine("************************************************");
            strmWrite.WriteLine("[/info]");
            strmWrite.Close();

            //iterate through dictfiletypes creating headers for each file and writing file data
            foreach (var dictData in dictFileTypesSorted)
            {
                strFile = dictData.Key;  //get file path
                aryFileData = File.ReadAllBytes(strFile); //read file bytes

                if (strSection == dictData.Value)
                {
                    //append file data header
                    strmWrite = new StreamWriter(@"C:\RogsResourceCreator\RogResourceFile.RogRes", true);

                    //write item header first get lvresources item with matching file path 
                    foreach (ListViewItem LVTemp in this.LVResources.Items)
                    {
                        if (LVTemp.Tag.ToString() == strFile)
                        {
                            strmWrite.WriteLine("");
                            strmWrite.WriteLine("Name:" + strFile);
                            strmWrite.WriteLine("ControlName:" + LVTemp.Text);

                            if (strSection != "image")
                            {
                                strmWrite.WriteLine("Length:" + aryFileData.Length.ToString());
                            }
                            else
                            {
                                //load image into pictemp
                                picTemp.ImageLocation = strFile;
                                picTemp.Load();
                                //append height/width after size using | as a delimeter e.g. Length:12304|60x60
                                strmWrite.WriteLine("Length:" + GetImageSize() + "|" + picTemp.Image.Height.ToString() + "x" + picTemp.Image.Width.ToString());
                            }
                        }
                    }

                    strmWrite.Close();
                    WriteFileData();
                }

                if (strSection == string.Empty) 
                {
                    //append section header
                    strmWrite = new StreamWriter(@"C:\RogsResourceCreator\RogResourceFile.RogRes",true);
                 
                    switch (dictData.Value)
                    {
                        case "image":
                            strmWrite.Write("[image]");
                            break;
                        case "text":
                            strmWrite.Write("[text]");
                            break;
                        case "sound":
                            strmWrite.Write("[sound]");
                            break;
                    }

                    //write item header first get lvresources item with matching file path 
                    foreach (ListViewItem LVTemp in this.LVResources.Items)
                    {
                        if (LVTemp.Tag.ToString() == strFile)
                        {
                            strmWrite.WriteLine("");
                            strmWrite.WriteLine("Name:" + strFile);
                            strmWrite.WriteLine("ControlName:" + LVTemp.Text);
                         
                            if (dictData.Value != "image")
                            { 
                                strmWrite.WriteLine("Length:" + aryFileData.Length.ToString());
                            }
                            else
                            {
                                //load image into pictemp
                                picTemp.ImageLocation = strFile;
                                picTemp.Load();
                                //append height/width after size using | as a delimeter e.g. Length:12304|60x60
                                strmWrite.WriteLine("Length:" + GetImageSize() +"|" + picTemp.Image.Height.ToString() + "x" + picTemp.Image.Width.ToString());
                            }
                        }
                    }

                    strmWrite.Close();
                    strSection = dictData.Value;
                    WriteFileData();
                }

                if (strSection != dictData.Value)
                {
                    //append section header end
                    strmWrite = new StreamWriter(@"C:\RogsResourceCreator\RogResourceFile.RogRes", true);
                    strmWrite.WriteLine("");

                    switch (strSection)
                    {
                        case "image":
                            strmWrite.Write("[/image]");
                            break;
                        case "text":
                            strmWrite.Write("[/text]");
                            break;
                        case "sound":
                            strmWrite.Write("[/sound]");
                            break;
                    }

                    //write new section header
                    strSection = dictData.Value;
                    strmWrite.WriteLine("");

                    switch (strSection)
                    {
                        case "image":
                            strmWrite.Write("[image]");
                            break;
                        case "text":
                            strmWrite.Write("[text]");
                            break;
                        case "sound":
                            strmWrite.Write("[sound]");
                            break;
                    }

                    //write item header first get lvresources item with matching file path 
                    foreach (ListViewItem LVTemp in this.LVResources.Items)
                    {
                        if (LVTemp.Tag.ToString() == strFile)
                        {
                            strmWrite.WriteLine("");
                            strmWrite.WriteLine("Name:" + strFile);
                            strmWrite.WriteLine("ControlName:" + LVTemp.Text);
                        
                            if (strSection != "image")
                            {
                                strmWrite.WriteLine("Length:" + aryFileData.Length.ToString());
                            }
                            else
                            {
                                //load image into pictemp
                                picTemp.ImageLocation = strFile;
                                picTemp.Load();
                                //append height/width after size using | as a delimeter e.g. Length:12304|60x60
                                strmWrite.WriteLine("Length:" + GetImageSize() + "|" + picTemp.Image.Height.ToString() + "x" + picTemp.Image.Width.ToString());
                            }
                        }
                    }

                    strmWrite.Close();
                    WriteFileData();
                }
            }

            //write end of section
            strmWrite = new StreamWriter(@"C:\RogsResourceCreator\RogResourceFile.RogRes", true);
            strmWrite.WriteLine("");

            switch (strSection)
            {
                case "image":
                    strmWrite.Write("[/image]");
                    break;
                case "text":
                    strmWrite.Write("[/text]");
                    break;
                case "sound":
                    strmWrite.Write("[/sound]");
                    break;
            }

            strmWrite.Close();

            MessageBox.Show("Resource File Created!","Success",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

//*******end of class******
    }
}
