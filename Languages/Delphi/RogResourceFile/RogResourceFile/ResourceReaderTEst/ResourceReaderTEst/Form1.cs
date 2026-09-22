using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResourceReaderTEst
{
    /*

  Created 10/12/2025 By Roger Williams

    reads custom resource file 

    exports sounds into actual files with same name

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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*

             Created 09/12/2025 By Roger Williams

             reads the resource file

             from folder:

             c:\rogsresourcecreator


             uses streamwriter to read text then filestream to read byte data


           */


            StreamReader strmRead;
            long lngFileLen = 0;
            long lngStreamPosition = 0;
            int intNumOfFiles = 0;
            long lngReadLength = 0;
            int intNum = 0;
            int intDataIn = 0;
            int intHeight = 0;
            int intWidth = 0;

            string strFile = string.Empty;
            string strSection = string.Empty;
            string strCurSection = string.Empty;
            string strData = string.Empty;
            string strTemp = string.Empty;
            string strControlName = string.Empty;
            string[] aryImageSize;
            FileStream fstrRead;


            void ReadFileData(long lngPosition, long lngFileSize, string strType)
            {
                long lngNum = 0;
                byte[] aryFileData = new byte[lngFileSize];
                long aryPos = 0;
                int intData = 0;
                byte bytData = 0;
                FileStream fstrWrite;
                //for image data
                Bitmap bmpOutput;
                int intA = 0;
                int intR = 0;
                int intG = 0;
                int intB = 0;
                int intX = 0;
                int intY = 0;
                Color clrTemp;



                for (lngNum = 1; lngNum <= lngFileLen; lngNum++)
                {
                    bytData = Convert.ToByte(fstrRead.ReadByte());
                    aryFileData[aryPos] = bytData;
                    aryPos++;
                }

                //if image
                if (strType == "image")
                {
                    //read!
                    bmpOutput = new Bitmap(intWidth, intHeight);
                    lngNum = 0;

                    for (intY = 0; intY != intHeight; intY++)
                    {
                        for (intX = 0; intX != intWidth; intX++)
                        {
                            //read 4 bytes for colour
                            intA = aryFileData[lngNum];
                            lngNum++;
                            intR = aryFileData[lngNum];
                            lngNum++;
                            intG = aryFileData[lngNum];
                            lngNum++;
                            intB = aryFileData[lngNum];
                            lngNum++;
                            clrTemp = Color.FromArgb(intA, intR, intG, intB);
                            bmpOutput.SetPixel(intX, intY, clrTemp);
                        }
                    }

                    if (strControlName != string.Empty)
                    {
                        ((PictureBox)this.Controls[strControlName]).Image = bmpOutput;
                    }
                }

                //if sound write file
                if (strType == "sound")
                {
                    fstrWrite = new FileStream(@"C:\RogsResourceCreator\" + Path.GetFileName(strFile), FileMode.Create);  //Application.ExecutablePath + "\\" + strFile, FileMode.Create);

                    for (lngNum = 0; lngNum != aryPos; lngNum++)
                    {
                        fstrWrite.WriteByte(aryFileData[lngNum]);
                    }
                }

                if (strType == "text" && strControlName != "Control Name")
                {
                    //write text into control in strcontrolname
                    for (lngNum = 0; lngNum != aryPos; lngNum++)
                    {
                        ((TextBox)this.Controls[strControlName]).Text += (char)aryFileData[lngNum];
                    }
                }
            }

            //*********end sub funcs******


            //create resource file
            fstrRead = new FileStream(@"C:\RogsResourceCreator\RogResourceFile.RogRes", FileMode.Open);
            //read header
            intNum = 0;

            while (intNum < 3)
            {
                intDataIn = fstrRead.ReadByte();

                if (intDataIn == 10)
                {
                    intNum++;
                }
            }

            intDataIn = 0;

            while (intDataIn != 10)
            {
                intDataIn = fstrRead.ReadByte();
             
                if (intDataIn != 10 && intDataIn != 13)
                { 
                    strData += (char)intDataIn;
                }
            }

            intDataIn = 0;

            //get number of files       Total Number Of Files:
            strTemp = strData.Substring(22, strData.Length -22);
            intNumOfFiles = Convert.ToInt16(strTemp);
            intDataIn = 0;

            while (intDataIn != 10)
            {
                intDataIn = fstrRead.ReadByte();
            }

            intDataIn = 0;

            while (intDataIn != 10)
            {
                intDataIn = fstrRead.ReadByte();
            }

            intDataIn = 0;


            while (intDataIn != -1)
            {
                intDataIn = fstrRead.ReadByte();
                strData = string.Empty;

                if (intDataIn == 91)
                {
                    strData += (char)intDataIn;

                    //if [ extract section
                    while (intDataIn != 10)
                    {
                        intDataIn = fstrRead.ReadByte();

                        if (intDataIn != 10 && intDataIn != 13)
                        {
                            strData += (char)intDataIn;
                        }
                    }

                    intDataIn = 0;
                }

                
                //check if starts with [
                if (strData != string.Empty) 
                {
                    if (strData.Substring(0, 1) == "[")
                    {
                        //extract section
                        strSection = strData.Substring(1, strData.Length - 2);
                    }
                }

                    //Read item header          Name: 
                    strData = string.Empty;

                    if (intDataIn == 10)
                    {
                        intDataIn = 0;
                    }


                //first run this gaets the Name propety
                //subsequent runs will find end of current section!
                intDataIn = fstrRead.ReadByte();

                if (intDataIn == 91)
                {
                    //read end of section

                    //if [ extract section
                    while (intDataIn != 10 && intDataIn != -1)
                    {
                        intDataIn = fstrRead.ReadByte();
                    }

                    //check if EOF

                    if (intDataIn == -1)
                    {
                        break;
                    }
                    // strData = string.Empty;
                    intDataIn = 0;
                    //get next char if [ extract next section if -1 exit

                    intDataIn = fstrRead.ReadByte();

                    if (intDataIn == 91)
                    {
                        //read new section
                        strData += (char)intDataIn;

                        //if [ extract section
                        while (intDataIn != 10)
                        {
                            intDataIn = fstrRead.ReadByte();

                            if (intDataIn != 10 && intDataIn != 13)
                            {
                                strData += (char)intDataIn;
                            }
                        }

                        if (strData.Substring(0, 1) == "[")
                        {
                            //extract section
                            strSection = strData.Substring(1, strData.Length - 2);
                        }
                    }

                    intDataIn = fstrRead.ReadByte();
                    //check if EOF

                    if (intDataIn == -1)
                    {
                        break;
                    }

                    strData = string.Empty;
                    //extract Name property
                    while (intDataIn != 10)
                    {
                        if (intDataIn != 10 && intDataIn != 13)
                        {
                            strData += (char)intDataIn;
                        }

                        intDataIn = fstrRead.ReadByte();
                    }
                }
                else  //no section change extract next item
                    while (intDataIn != 10)
                    {
                        if (intDataIn != 10 && intDataIn != 13)
                        {
                            strData += (char)intDataIn;
                        }

                        intDataIn = fstrRead.ReadByte();
                    }


                intDataIn = 0;

                //get filename
                strFile = strData.Substring(5,strData.Length -5);
                //get controlname  ControlName:
                //   strmRead.ReadLine();
                strData = string.Empty;

                while (intDataIn != 10)
                {
                    intDataIn = fstrRead.ReadByte();

                    if (intDataIn != 10 && intDataIn != 13)
                    {
                        strData += (char)intDataIn;
                    }
                }
                //                               ControlName:
                strControlName = strData.Substring(12, strData.Length - 12);
                intDataIn = 0;

                    //get file length  Length: 
                    strData = string.Empty;

                    while (intDataIn != 10)
                    {
                        intDataIn = fstrRead.ReadByte();

                        if (intDataIn != 10 && intDataIn != 13)
                        {
                            strData += (char)intDataIn;
                        }
                    }

                    intDataIn = 0;
   
                //if image handle differently
                if (strSection == "image")
                {
                    //first extract height and width
                    intNum = strData.IndexOf("|");
                    aryImageSize = strData.Split(':');
                    strTemp = aryImageSize[1];

                    //get file length
                    aryImageSize = strTemp.Split('|');
                    lngFileLen = Convert.ToInt32(aryImageSize[0]);

                    //get image height
                    strTemp = aryImageSize[1];
                    intNum = strTemp.IndexOf("x");
                    strTemp = strTemp.Substring(0, intNum);
                    intHeight = Convert.ToInt32(strTemp);

                    //get image width
                    strTemp = aryImageSize[1];
                    //remove height from string
                    strTemp = strTemp.Remove(0, intNum +1);
                    intWidth = Convert.ToInt32(strTemp);
                }
                else
                {
                    strTemp = strData.Substring(7, strData.Length - 7);
                    lngFileLen = Convert.ToInt32(strTemp);

                }

                ReadFileData(lngReadLength, lngFileLen, strSection);
                intDataIn = fstrRead.ReadByte();
            }

            fstrRead.Close();
        }

//*********end of class*********
    }
}
