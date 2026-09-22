using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;

/*
  Modified 15/11/2025 By Roger Williams

  Converted all timers to async functions 

  Modified 14/09/2025 By Roger Williams

  Added:

  - NCHHITTEST implementation - custom title bar!


  had strange flickering partial border around title pictureboxes so added into the apint this line:
  ControlPaint.DrawBorder(e.Graphics, PICTitle1.ClientRectangle, System.Drawing.Color.White, ButtonBorderStyle.Solid);

  custom draws a border in this case white as the form transparency is set to white, however setting the background colour
  of the images to BLACK fixed the issue, but left code here for future reference!

  in paint event had to change:

   
  //draw fake title bar
  using (System.Drawing.Brush brush = new SolidBrush(CNST_INT_TITLEBAR_BACKCOLOUR))
  {
      e.Graphics.FillRectangle(brush, 0, 0, this.Width-2, CNST_INT_TITLEBARHEIGHT);
  }
  

  To:

  //draw fake title bar
  using (System.Drawing.Brush brush = new SolidBrush(CNST_INT_TITLEBAR_BACKCOLOUR))
  {
      e.Graphics.FillRectangle(brush, 1, 2, this.Width-2, CNST_INT_TITLEBARHEIGHT);
  }

  To stop right hand line being obscured






 
  Created 06/09/2025 By Roger Williams

  RogInvaders!

  An exploration into the possibilities of doing Space Invaders in C#

  Using mediaplayer asn soundplayer cannot allows two sounds at a time soo..

  added refernces:

  Presentation Core
  WindowsBase

  That gives:

  System.Windows.Media -> MediaPlayer


*/

namespace RogInvaders2025
{
    public partial class frmMain : Form
    {


        bool blnFirstRun = true;

        //for title animation
        int intTitleAnimation = 0;

        //for rest of intro
        bool blnIntroFinished = false;
        bool blnAlienMotherShipSound = false;
        int intAlienAnimation = 0;
        int intAlienMothershipAnimation = 0;
        int intIntroTextLine = 0;
        int intCurDisplayChar = 0;
        int intShowMotherShipDelay = Modules.clsView.CNST_INT_ALIENMOTHERSHIP_INTRO_INTERVAL; //how long to wait before showing
        int intShowMotherShipLeft = Modules.clsView.CNST_INT_ALIENMOTHERSHIPINTRO_LEFT;
        int intTimePassed = 0;
        string strIntroLine = String.Empty;
        string strIntroLineDisplay = String.Empty;
        frmGame frmGameForm = new frmGame();

        //timers
        //Timer TMRAlienMotherShip = null;
        //Timer TMRAlienMotherShipMove = null;
        //Timer TMRIntroText = null;
        bool blnAlienMotherShip = false;
        bool blnAlienMotherShipMove = false;
        bool blnIntroText = false;

        //music/soundeffects use media player to get two sounds to play at once!
        MediaPlayer SNDMusic = new System.Windows.Media.MediaPlayer();
        MediaPlayer SNDSound = new System.Windows.Media.MediaPlayer();
        bool blnFadeMusic = false;

        //for custom title bar
        Font fntTemp = new Font("Microsoft Sans Serif", 10); //title bar text font
        const int HTCAPTION = 0x2;
        const int WM_NCLBUTTONDOWN = 0x00A1;
        const int CNST_INT_TITLEBARHEIGHT = 24;
        readonly System.Drawing.Color CNST_INT_TITLEBAR_BACKCOLOUR = System.Drawing.Color.Black;
        readonly System.Drawing.Color CNST_INT_TITLEBAR_TEXTCOLOUR = System.Drawing.Color.Yellow;
        readonly System.Drawing.Color CNST_INT_TITLEBAR_UNDERLINECOLOUR = System.Drawing.Color.Black;
 

        //create a sub class for ease of use
        public static class User32_DLL
        {
            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        }



        public frmMain()
        {
            InitializeComponent();
        }
        //***custom sub/funcs********


        //mediaplayer event
        private void Custom_MediaEnded(object sender, EventArgs e)
        {
            blnAlienMotherShipSound = false;
        }


        //timer events
        private async void AsyncAlienMotherShipMove()
        {
            while (!blnAlienMotherShipMove)
            {
                await Task.Delay(Modules.clsView.CNST_INT_ALIENMOTHERSHIP_INTRO_MOVE_INTERVAL);

                //move mothership
                this.PICAlienMotherShipIntro.Left = intShowMotherShipLeft;
                intShowMotherShipLeft = intShowMotherShipLeft - 50;

                if (intShowMotherShipLeft == -300)
                {
                    this.PICAlienMotherShipIntro.Visible = false;
                    blnAlienMotherShipMove = true;
                    SNDSound.Stop();
                    blnAlienMotherShipSound = false;
                }

                //animate alien for intro
                if (intAlienMothershipAnimation == 4)
                {
                    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_5;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 3)
                {
                    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_4;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 2)
                {
                    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_3;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 1)
                {
                    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_2;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 0)
                {
                    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_1;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 5)
                {
                    intAlienMothershipAnimation = 0;
                }
                else
                {
                    this.PICAlienMotherShipIntro.Load();
                }
            }
        }
        private void AlienMotherShip()
        {
            /*
              Created 08/09/2025 By Roger Williams

              animates the mothership and moves it

            */

            while (!blnAlienMotherShip)
            { 
                blnAlienMotherShip = true;

                this.PICAlienMotherShipIntro.Left = intShowMotherShipLeft;
                this.PICAlienMotherShipIntro.Top = Modules.clsView.CNST_INT_ALIENMOTHERSHIPINTRO_TOP;
                this.PICAlienMotherShipIntro.Visible = true;

                //play alien mothership sound
                blnAlienMotherShipSound = true;
                //play sound
                SNDSound.Position = new TimeSpan(0, 0, 0);
                SNDSound.Play();

                //TMRAlienMotherShipMove.Enabled = true;
                blnAlienMotherShipMove = false;
                AsyncAlienMotherShipMove();
            }
        }

        private async void AsyncAlienMotherShip()
        {
            //wait till time to show mothership
            await Task.Delay(Modules.clsView.CNST_INT_ALIENMOTHERSHIP_INTRO_INTERVAL);
            blnAlienMotherShip = false;
            AlienMotherShip();
        }
        private async void AsyncIntroText()
        {
            /*
              Created 08/09/2025 By Roger Williams

              animates the intro text

              adds one letter at a time to TXTIntroText un

            */

            while (!blnIntroFinished)
            {
                await Task.Delay(Modules.clsView.CNST_INT_INTROTEXT_INTERVAL);

                if (blnFadeMusic)
                {
                    SNDMusic.Volume = SNDMusic.Volume - 0.01;
                }

                //check if music reached point where intro ends: 00:01:50.100000
                if (blnFadeMusic == false)
                {
                    //this.label1.Text = SNDMusic.Position.ToString();
                    //this.label1.Invalidate();
                                                                   //"00:01:50.100000"
                    if (String.Compare(SNDMusic.Position.ToString(), "00:01:35.100000") != -1)
                    {
                        blnFadeMusic = true;
                    }
                }

                //if volume near zero stop intro
                if (SNDMusic.Volume <= 0.05)
                {
                    SNDMusic.Stop();
                    blnIntroFinished = true;
                }

                if (intIntroTextLine != Modules.clsView.LSTIntroText.Count)
                {
                    if (strIntroLine == String.Empty)
                    {
                        strIntroLine = Modules.clsView.LSTIntroText[intIntroTextLine];
                        intCurDisplayChar = 0;
                    }

                    //show line chars
                    if (intCurDisplayChar != strIntroLine.Length)
                    {
                        strIntroLineDisplay = strIntroLine.Substring(0, intCurDisplayChar + 1);
                        intCurDisplayChar++;
                    }
                    else
                    {
                        intIntroTextLine++;
                        strIntroLineDisplay = String.Empty;
                        strIntroLine = String.Empty;
                    }
                }

                this.TXTIntroText.Text = strIntroLineDisplay;
            }
        }


        private async void AsyncAniTitle()
        {
            while (!blnIntroFinished)
            {
                intTimePassed += 1000;

                if (blnFirstRun && blnIntroText == false)
                {
                    // TMRIntroText.Enabled = true;
                    blnIntroText = true;
                    AsyncIntroText();
                    this.TXTIntroText.Visible = true;
                }

                if (intTimePassed == intShowMotherShipDelay)
                {
                    //TMRAlienMotherShip.Enabled = true;
                    blnAlienMotherShip = false;
                    AsyncAlienMotherShip();
                }


                //animate alien for intro
                if (intAlienAnimation == 3)
                {
                    this.PICAlienAni.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_4;
                    intAlienAnimation++;
                }
                if (intAlienAnimation == 2)
                {
                    this.PICAlienAni.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_3;
                    intAlienAnimation++;
                }
                if (intAlienAnimation == 1)
                {
                    this.PICAlienAni.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_2;
                    intAlienAnimation++;
                }
                if (intAlienAnimation == 0)
                {
                    this.PICAlienAni.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_1;
                    intAlienAnimation++;
                }
                if (intAlienAnimation == 4)
                {
                    intAlienAnimation = 0;
                }
                else
                {
                    this.PICAlienAni.Load();
                }

                //animates title images
                if (intTitleAnimation == 2)
                {
                    this.PICTitle1.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEHEADER_3;
                    this.PICTitle1a.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEFOOTER_3;

                    intTitleAnimation += 1;
                }
                if (intTitleAnimation == 1)
                {
                    this.PICTitle1.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEHEADER_2;
                    this.PICTitle1a.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEFOOTER_2;
                    //this.PICTitle1.Load();
                    intTitleAnimation += 1;
                }
                if (intTitleAnimation == 0)
                {
                    this.PICTitle1.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEHEADER_1;
                    this.PICTitle1a.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEFOOTER_1;
                    // this.PICTitle1.Load();
                    intTitleAnimation += 1;
                }
                if (intTitleAnimation == 3)
                {
                    intTitleAnimation = 0;

                }
                else
                {
                    this.PICTitle1.Load();
                }

                await Task.Delay(1000);
            }

            if (blnIntroFinished)
            {
                //check if intro ended
                    //start main game
                    //TMRAniTitle.Enabled = false;
                    //TMRIntroText.Enabled = false;
                    //TMRAlienMotherShip.Enabled = false;
                    blnAlienMotherShip = true;
                    blnAlienMotherShipMove = true;
                    blnIntroText = false;
                    SNDMusic.Stop();
                    SNDSound.Stop();
                    this.Hide();
                    //start main game
                    frmGameForm.Show();
            }
        }


        //private void TMRAniTitle_Tick(object sender, EventArgs e)
        //{
        //    frmGame frmGameForm = new frmGame();

        //    intTimePassed += TMRAniTitle.Interval;

        //    //check if intro ended
        //    if (blnIntroFinished)
        //    {
        //        //start main game
        //        TMRAniTitle.Enabled = false;
        //        TMRIntroText.Enabled = false;
        //        TMRAlienMotherShip.Enabled = false;
        //        SNDMusic.Stop();
        //        SNDSound.Stop();
        //        this.Hide();
        //        //start main game
        //        frmGameForm.Show();
        //    }

        //    if (blnFirstRun)
        //    {
        //        TMRIntroText.Enabled = true;
        //        this.TXTIntroText.Visible = true;
        //    }

        //    if (intTimePassed == intShowMotherShipDelay)
        //    {
        //        TMRAlienMotherShip.Enabled = true;
        //    }


        //    //animate alien for intro
        //    if (intAlienAnimation == 3)
        //    {
        //        this.PICAlienAni.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_4;
        //        intAlienAnimation++;
        //    }
        //    if (intAlienAnimation == 2)
        //    {
        //        this.PICAlienAni.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_3;
        //        intAlienAnimation++;
        //    }
        //    if (intAlienAnimation == 1)
        //    {
        //        this.PICAlienAni.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_2;
        //        intAlienAnimation++;
        //    }
        //    if (intAlienAnimation == 0)
        //    {
        //        this.PICAlienAni.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_1;
        //        intAlienAnimation++;
        //    }
        //    if (intAlienAnimation == 4)
        //    {
        //        intAlienAnimation = 0;
        //    }
        //    else
        //    {
        //        this.PICAlienAni.Load();
        //    }

        //    //animates title images
        //    if (intTitleAnimation == 2)
        //    {
        //        this.PICTitle1.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEHEADER_3;
        //        this.PICTitle1a.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEFOOTER_3;

        //        intTitleAnimation += 1;
        //    }
        //    if (intTitleAnimation == 1)
        //    {
        //        this.PICTitle1.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEHEADER_2;
        //        this.PICTitle1a.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEFOOTER_2;
        //        //this.PICTitle1.Load();
        //        intTitleAnimation += 1;
        //    }
        //    if (intTitleAnimation == 0)
        //    {
        //        this.PICTitle1.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEHEADER_1;
        //        this.PICTitle1a.ImageLocation = Modules.clsModel.CNST_STR_IMG_TITLEFOOTER_1;
        //        // this.PICTitle1.Load();
        //        intTitleAnimation += 1;
        //    }
        //    if (intTitleAnimation == 3)
        //    {
        //        intTitleAnimation = 0;

        //    }
        //    else
        //    {
        //        this.PICTitle1.Load();
        //    }
        //}

        //private void TMRAlienMotherShipMove_Tick(object sender, EventArgs e)
        //{
        //    //move mothership
        //    this.PICAlienMotherShipIntro.Left = intShowMotherShipLeft;
        //    intShowMotherShipLeft = intShowMotherShipLeft - 50;

        //    if (intShowMotherShipLeft == -300)
        //    {
        //        this.PICAlienMotherShipIntro.Visible = false;
        //        this.TMRAlienMotherShipMove.Enabled = false;
        //        SNDSound.Stop();
        //        blnAlienMotherShipSound = false;
        //    }

        //    //animate alien for intro
        //    if (intAlienMothershipAnimation == 4)
        //    {
        //        this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_5;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 3)
        //    {
        //        this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_4;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 2)
        //    {
        //        this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_3;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 1)
        //    {
        //        this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_2;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 0)
        //    {
        //        this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_1;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 5)
        //    {
        //        intAlienMothershipAnimation = 0;
        //    }
        //    else
        //    {
        //        this.PICAlienMotherShipIntro.Load();
        //    }
        //}
        //private void TMRAlienMotherShip_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 08/09/2025 By Roger Williams

        //      animates the mothership and moves it

        //    */

        //    TMRAlienMotherShip.Enabled = false;

        //    this.PICAlienMotherShipIntro.Left = intShowMotherShipLeft;
        //    this.PICAlienMotherShipIntro.Top = Modules.clsView.CNST_INT_ALIENMOTHERSHIPINTRO_TOP;
        //    this.PICAlienMotherShipIntro.Visible = true;

        //    //play alien mothership sound
        //    blnAlienMotherShipSound = true;
        //    //play sound
        //    SNDSound.Position = new TimeSpan(0, 0, 0);
        //    SNDSound.Play();

        //    TMRAlienMotherShipMove.Enabled = true;

        //    //if (this.PICAlienMotherShipIntro.Visible == false)
        //    //{
        //    //    this.PICAlienMotherShipIntro.Left = intShowMotherShipLeft;
        //    //    this.PICAlienMotherShipIntro.Top = Modules.clsView.CNST_INT_ALIENMOTHERSHIPINTRO_TOP;
        //    //    this.PICAlienMotherShipIntro.Visible = true;

        //    //}

        //    ////play alien mothership sound
        //    //if (blnAlienMotherShipSound == false)
        //    //{
        //    //    blnAlienMotherShipSound = true;
        //    //    //play sound
        //    //    SNDSound.Position = new TimeSpan(0, 0, 0);
        //    //    SNDSound.Play();
        //    //}

        //    ////move mothership
        //    //this.PICAlienMotherShipIntro.Left = intShowMotherShipLeft;
        //    //intShowMotherShipLeft = intShowMotherShipLeft - 50;

        //    //if (intShowMotherShipLeft == -300)
        //    //{
        //    //    this.PICAlienMotherShipIntro.Visible = false;
        //    //    this.TMRAlienMotherShip.Enabled = false;
        //    //    SNDSound.Stop();
        //    //    blnAlienMotherShipSound = false;
        //    //}

        //    ////animate alien for intro
        //    //if (intAlienMothershipAnimation == 4)
        //    //{
        //    //    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_5;
        //    //    intAlienMothershipAnimation++;
        //    //}
        //    //if (intAlienMothershipAnimation  == 3)
        //    //{
        //    //    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_4;
        //    //    intAlienMothershipAnimation ++;
        //    //}
        //    //if (intAlienMothershipAnimation  == 2)
        //    //{
        //    //    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_3;
        //    //    intAlienMothershipAnimation ++;
        //    //}
        //    //if (intAlienMothershipAnimation  == 1)
        //    //{
        //    //    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_2;
        //    //    intAlienMothershipAnimation ++;
        //    //}
        //    //if (intAlienMothershipAnimation  == 0)
        //    //{
        //    //    this.PICAlienMotherShipIntro.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_1;
        //    //    intAlienMothershipAnimation ++;
        //    //}
        //    //if (intAlienMothershipAnimation  == 5)
        //    //{
        //    //    intAlienMothershipAnimation  = 0;
        //    //}
        //    //else
        //    //{
        //    //    this.PICAlienMotherShipIntro.Load();
        //    //}

        //}

        //private void TMRIntroText_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 08/09/2025 By Roger Williams

        //      animates the intro text

        //      adds one letter at a time to TXTIntroText un

        //    */

        //    if (blnFadeMusic)
        //    {
        //        SNDMusic.Volume = SNDMusic.Volume - 0.05;
        //    }

        //    //check if music reached point where intro ends: 00:01:50.100000
        //    if (blnFadeMusic == false)
        //    { 
        //        if (String.Compare(SNDMusic.Position.ToString(), "00:01:50.100000") != -1)
        //        {
        //            blnFadeMusic = true;
        //        }
        //    }



        //    //if volume near zero stop intro
        //    if (SNDMusic.Volume <= 0.1)
        //    {
        //        blnIntroFinished = true;
        //    }

        //    if (intIntroTextLine != Modules.clsView.LSTIntroText.Count)
        //    {
        //       if (strIntroLine == String.Empty)
        //       {
        //            strIntroLine = Modules.clsView.LSTIntroText[intIntroTextLine];
        //            intCurDisplayChar = 0;
        //       }

        //        //show line chars
        //        if (intCurDisplayChar != strIntroLine.Length)
        //        {
        //            strIntroLineDisplay = strIntroLine.Substring(0, intCurDisplayChar + 1);
        //            intCurDisplayChar++;
        //        }
        //        else
        //        {
        //            intIntroTextLine++;
        //            strIntroLineDisplay = String.Empty;
        //            strIntroLine = String.Empty;
        //        }
        //    }

        //    this.TXTIntroText.Text = strIntroLineDisplay;
        //}



        //private void CreateTimers()
        //{
        //    /*
        //      Created 08/09/2025 By Roger Williams

        //      creates timers for game events but does NOT enable them!


        //    */

        //    TMRAlienMotherShip = new Timer();
        //    TMRAlienMotherShip.Interval = ;
        //    TMRAlienMotherShip.Tick += TMRAlienMotherShip_Tick;
        //    TMRAlienMotherShip.Enabled = false;

        //    TMRAlienMotherShipMove = new Timer();
        //    TMRAlienMotherShipMove.Interval = Modules.clsView.CNST_INT_ALIENMOTHERSHIP_INTRO_MOVE_INTERVAL;
        //    TMRAlienMotherShipMove.Tick += TMRAlienMotherShipMove_Tick;
        //    TMRAlienMotherShipMove.Enabled = false;

        //    TMRIntroText = new Timer();
        //    TMRIntroText.Interval = Modules.clsView.CNST_INT_INTROTEXT_INTERVAL;
        //    TMRIntroText.Tick += TMRIntroText_Tick;
        //    TMRIntroText.Enabled = false;
        //}

        //end timer events
        private void Init()
        {
            /*
              Created 08/09/2025 By Roger Williams

              initialises various game elements e.g. timers and starts music!


            */

            //load intro text
            if (Modules.clsView.ReadIntroText() == false)
            {
                MessageBox.Show("Error Reading Intro Text File. Please Check File Exists In:\n\n" + Modules.clsModel.CNST_STR_PATH_INTROTEXTLOCATION, "Error Reading File",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            
            //show intro
            this.PICTitle1.Visible = true;
            this.PICTitle1a.Visible = true;
            //this.TMRAniTitle.Enabled = true;
            AsyncAniTitle();
//            AsyncIntroText();
            blnFirstRun = true;

            if (File.Exists(Modules.clsModel.CNST_STR_SND_ALIENMOTHERSHIP))
            {
                SNDSound.MediaEnded += Custom_MediaEnded;
                SNDSound.Volume = 1;
                SNDSound.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_ALIENMOTHERSHIP));
            }
            else
            {
                MessageBox.Show("Error Reading Alien File. Please Check File Exists:\n\n" + Modules.clsModel.CNST_STR_SND_ALIENMOTHERSHIP, "Error Reading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(Modules.clsModel.CNST_STR_SND_INTROTHEME))
                {
                SNDMusic.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_INTROTHEME));
                SNDMusic.Volume = 0.5;
                SNDMusic.Play();
            }
            else
            {
                MessageBox.Show("Error Reading Intro Music File. Please Check File Exists:\n\n" + Modules.clsModel.CNST_STR_SND_INTROTHEME, "Error Reading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void SetImageSizes()
        {
            /*
              Created 08/09/2025 By Roger Williams

              sets image sizes

              WHY?

              During design might decided to resize certain or all images ensures avoid missing
              anything in the designer!

            */

            this.PICTitle1.Height = Modules.clsView.CNST_INT_TITLEHEADER_HEIGHT;
            this.PICTitle1a.Height = Modules.clsView.CNST_INT_TITLEFOOTER_HEIGHT;
            this.PICTitle1.Width = Modules.clsView.CNST_INT_TITLEHEADER_WIDTH;
            this.PICTitle1a.Width = Modules.clsView.CNST_INT_TITLEFOOTER_WIDTH;
        }

        private void CentreTitleImages()
        {
            /*
              Created 08/09/2025 By Roger Williams

              sets title image positions
          
            */

            //centre title iaages
            this.PICTitle1.Left = this.Width / 2 - Modules.clsView.CNST_INT_TITLEHEADER_WIDTH / 2;
            this.PICTitle1a.Left = this.Width / 2 - Modules.clsView.CNST_INT_TITLEFOOTER_WIDTH / 2;
            //set top
            this.PICTitle1.Top = Modules.clsView.CNST_INT_TITLEHEADER_TOP;
            this.PICTitle1a.Top = Modules.clsView.CNST_INT_TITLEFOOTER_TOP;
        }



        //***form events****

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            /*
               Created 14/09/2025 By Roger Williams

               draws form with custom title bar!

             
             */

            //round form corners
            Rectangle rctTemp = e.ClipRectangle;

       
            //draw fake title bar
            using (System.Drawing.Brush brush = new SolidBrush(CNST_INT_TITLEBAR_BACKCOLOUR))
            {
                e.Graphics.FillRectangle(brush, 1, 2, this.Width-2, CNST_INT_TITLEBARHEIGHT);
            }

            using (System.Drawing.Pen penTemp = new System.Drawing.Pen(CNST_INT_TITLEBAR_UNDERLINECOLOUR))
            {
                //draw line underneath
                e.Graphics.DrawLine(penTemp, 0, CNST_INT_TITLEBARHEIGHT, this.Width, CNST_INT_TITLEBARHEIGHT);
            }

            using (System.Drawing.Brush brush = new SolidBrush(CNST_INT_TITLEBAR_TEXTCOLOUR))
            {
                e.Graphics.DrawString(this.Text, fntTemp, brush, 2, 5);
            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            /*
              Created 08/09/2025 By Roger Williams

              animates title
          
            */

            //size images
            SetImageSizes();
            //centre title images
            CentreTitleImages();

            Init();
            //testing only
         //   blnIntroFinished = true;
        }

    
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if manual stop
            //TMRAniTitle.Enabled = false;
            //TMRIntroText.Enabled = false;
            //TMRAlienMotherShip.Enabled = false;
            blnAlienMotherShip = true;
            blnAlienMotherShipMove = true;
            blnIntroFinished = true;
            SNDMusic.Stop();
            SNDSound.Stop();
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

        private void PICClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
