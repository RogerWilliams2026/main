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

namespace SaveImageDataToFileandRead
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*

              stores picoriginal colour data into temp file

              load into picturebox1!


            */

            FileStream fstrWrite;
            FileStream fstrRead;
            Color[] aryColours;
            int intData = 0;
            int intWidth = this.PICOriginal.Image.Width;
            int intHeight = this.PICOriginal.Image.Height;
            int intX = 0;
            int intY = 0;
            int intA = 0;
            int intR = 0;
            int intG = 0;
            int intB = 0;
            Color clrTemp;
            Bitmap bmpTemp = new Bitmap(this.PICOriginal.Image);
            Bitmap bmpOutput;

            fstrWrite = new FileStream(@"C:\RogsResourceCreator\imagedata.dat", FileMode.Create);
            
            for (intY = 0; intY !=intHeight; intY++)
            {
                for (intX = 0;intX !=intWidth; intX++)
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
            //read!
            fstrRead = new FileStream(@"C:\RogsResourceCreator\imagedata.dat", FileMode.Open);
            bmpOutput = new Bitmap(intX, intY);

            while (intA != -1)
            {
                for (intY = 0; intY != intHeight; intY++)
                {
                    for (intX = 0; intX != intWidth; intX++)
                    {
                        //read 4 bytes for colour
                        intA = fstrRead.ReadByte();

                        if (intA != -1)
                        {
                            intR = fstrRead.ReadByte();
                            intG = fstrRead.ReadByte();
                            intB = fstrRead.ReadByte();
                            clrTemp = Color.FromArgb(intA, intR, intG, intB);
                            bmpOutput.SetPixel(intX, intY, clrTemp);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (intA == -1)
                    {
                        break;
                    }
                }
            }

            fstrRead.Close();

            this.pictureBox1.Image = bmpOutput;
        }
    }
}
