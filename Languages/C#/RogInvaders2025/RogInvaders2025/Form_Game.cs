using RogInvaders2025.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Diagnostics;

/*
  modified 06/11/2025 by Roger Williams
  
  replacing timers with async subs as uses less resources now being a control plus its the 21st century!


  modified 13/10/2025 by Roger Williams

  now uses ONE image only and pixel colour swap for colour animation for these images:
  - alien shot
  - player shot
  - player ship
  - you win
  - you lose
  - game over
  - play again
  - mothership beam
  - alien explosion
  - player explosion


  Created 08/09/2025 by Roger Williams

  main game screen


*/



namespace RogInvaders2025
{
    public partial class frmGame : Form
    {

        //create a sub class for ease of use
        public static class User32_DLL
        {
            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        }

        //used if player wins/loses etc
        int intEndgameAni = 1;

        //used by player win/loses/alien invasion etc
        int intCount = 0;
        int intRepeat = 0;

        //used for custom box drawing
        Graphics GRATemp = null;
        Rectangle RECTemp;
        //used for color swap
        System.Drawing.Color CLRTemp = System.Drawing.Color.Transparent;
        Bitmap BMPTemp1 = null;
        Bitmap BMPTemp2 = null;
        //used to dither bmptemp1 into 2
        int intX = 0;
        int intY = 0;
        //used to tell dither timer which animation timer to start when finished
        enum enumScreens { scrYouLost, scrYouWin, scrCompleted, scrGameOver, scrPlayAgain };
        enumScreens enumScreenChosen;

        //music/sound effects use media player to get two sounds to play at once!
        MediaPlayer SNDMusic = new System.Windows.Media.MediaPlayer();
        MediaPlayer SNDBaseDestroyed = new System.Windows.Media.MediaPlayer();
        MediaPlayer SNDOther1 = new System.Windows.Media.MediaPlayer();  //used by gameover/player wins/loses etc
        MediaPlayer SNDOther2 = new System.Windows.Media.MediaPlayer();  //used by player loses as uses two simultaneous sounds

        //classes
        clsPlayer clsActivePlayer;
        clsAlienMothership clsActiveAlienMothership;
        clsAlienWave clsActiveAlienWave;
        clsAlienShot clsActiveAlienShot;
        clsBaseAll clsActiveBaseAll;

        int intCurLevel = 1;  //0 //max of 4 levels set to zero on purpose!
        bool blnCanProgress = false; //if true player completed a level
        bool blnInvasion = false; //show alieninvasion running?
        bool blnPlayAgain = false; //is game in "play again" mode?
        bool blnPlayerWins = false; //player completed game?
        bool blnPlayerLoses = false; //has player lost a life/aliens reached base?

        //used to stop alien shot explosion from continuning to move through the baseunit
        //triggering itself again!
        bool blnPlayerLost = false;
        //used to change nuphonic ray text during shooting
        System.Drawing.Color clrRayColor1 = System.Drawing.Color.GreenYellow;
        System.Drawing.Color clrRayColor2 = System.Drawing.Color.LightGreen;
        System.Drawing.Color clrRayColor3 = System.Drawing.Color.YellowGreen;
        System.Drawing.Color clrRayColor4 = System.Drawing.Color.LimeGreen;
        int intCurRayColour = 1;
        //for custom titlebar
        System.Drawing.Brush BRUTitleBar = new SolidBrush(Modules.clsView.CNST_INT_TITLEBAR_BACKCOLOUR);
        System.Drawing.Pen penTemp = new System.Drawing.Pen(Modules.clsView.CNST_INT_TITLEBAR_UNDERLINECOLOUR);

        //async sub stop running vars
        bool blnAsyncGameOver = false;
        bool blnAsyncYouWin = false;
        bool blnAsyncYouLose = false;
        bool blnAsyncPlayAgain = false;
        bool blnAsyncDither = false;
        bool blnAsyncFadeMusic = false;
        bool blnAsyncCollision = false;
        bool blnAsyncAlienShot = false;
        bool blnAsyncCompletedGame = false;

        public frmGame()
        {
            InitializeComponent();
        }
        //***custom sub/funcs********


        //******public********
   

        //**player ray recharge used by clsPlayer
        public void HideCharge()
        {
            /*
              Created 29/09/2025 By Roger Williams

              called ONCE when clsPlayer shot has either hit something or
              reached top of screen

              hides PANCharge 

            */

            this.PRGCharge.Value = 0;
            this.PANCharge.Visible = false;
        }
        public void IncCharge()
        {
            /*
              Created 29/09/2025 By Roger Williams

              called clsPlayer moves shot

              increments PRGCharge 

            */

            this.PRGCharge.Value += 10;

            switch (intCurRayColour)
            {
                case 1:
                    this.LBLRay.ForeColor = clrRayColor1;
                    break;
                case 2:
                    this.LBLRay.ForeColor = clrRayColor2;
                    break;
                case 3:
                    this.LBLRay.ForeColor = clrRayColor3;
                    break;
                case 4:
                    this.LBLRay.ForeColor = clrRayColor4;
                    break;
            }

            intCurRayColour++;

            if (intCurRayColour > 4)
            {
                intCurRayColour = 1;
            }
        }

        public void ShowCharge()
        {
            /*
              Created 29/09/2025 By Roger Williams

              called ONCE when clsPlayer fires shot (start of animation)

              shows PANCharge 

            */

            this.PRGCharge.Value = 0;
            this.PANCharge.Visible = true;
        }

        //**end player ray recharge

        public void ResetPlayer()
        {
            /*
              Created 11/10/2025 By Roger Williams

              called by clsplayer_playerhit after explosion animation


            */

            blnCanProgress = false;

            if (clsActivePlayer.intLives == 0)
            {
                blnPlayerLoses = true;
                blnPlayerLost = true;
            }
            else
            {
                clsActivePlayer.ResetPlayer();
            }
            //else
            //{
            //    //restart level
            //    Init();
            //}
        }
        public void AliensReachedBase()
        {
            /*
              Created 01/10/2025 By Roger Williams

              called ONCE when by alienwave when alien wave reaches base

            */

            PlayerHit();
        }







        //********async subs*********

        private async void AsyncFadeMusic()
        {
            /*
              Created 06/11/2025 By Roger Williams
              
              replaces timer 


              fades main music


            */

            try
            {      
            while (!blnAsyncFadeMusic)
            { 
                SNDMusic.Volume -= 0.04;
                await Task.Delay(700);

                if (SNDMusic.Volume <= 0.01)
                {
                    blnAsyncFadeMusic = true;
                    SNDMusic.Stop();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        //private void TMRFadeMusic_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 21/10/2025 By Roger Williams

        //      fades main music


        //    */

        //    SNDMusic.Volume -= 0.04;

        //    this.LBLTest.Text = SNDMusic.Volume.ToString();
        //    this.LBLTest.Update();

        //    if (SNDMusic.Volume <= 0.01)
        //    {
        //        TMRFadeMusic.Enabled = false;
        //        SNDMusic.Stop();
        //    }
        //}


        private async void AsyncDither()
        {
            /*
              Created 06/11/2025 By Roger Williams
              
              replaces timer 

              dithers bmptemp2 into bmptemp 1


            */

            Random rndNum = new Random();
            int intNum = 0;
            System.Drawing.Color clrTemp = System.Drawing.Color.Transparent;

            bool ImagesMatch()
            {
                /*
                  Compares bmptemp1 with 2

                */

                int intX = 0;
                int intY = 0;
                bool blnCont = true;
                bool blnAllMatch = true;

                while (blnCont)
                {
                    if (BMPTemp1.GetPixel(intX, intY) != BMPTemp2.GetPixel(intX, intY))
                    {
                        blnCont = false;
                        blnAllMatch = false;
                    }

                    intX++;

                    if (intX >= BMPTemp1.Width)
                    {
                        intX = 0;
                        intY++;

                        if (intY >= BMPTemp1.Height)
                        {
                            blnCont = false;
                        }
                    }
                }

                return blnAllMatch;
            }

            try
            {  
            while (!blnAsyncDither)
            {
                await Task.Delay(350);

                for (intNum = 0; intNum < 40000; intNum++)
                {
                    intX = Math.Abs(rndNum.Next(this.PICBlankImage.Width));
                    intY = Math.Abs(rndNum.Next(this.PICBlankImage.Height));

                    //get pixel colour from second image
                    CLRTemp = BMPTemp2.GetPixel(intX, intY);
                    BMPTemp1.SetPixel(intX, intY, CLRTemp);
                    intCount++;
                }

                this.PICBlankImage.Image = BMPTemp1;
                this.PICBlankImage.Invalidate();

                //dither complete?
                if (ImagesMatch())
                {
                    intCount = 0;
                    //   TMRDitherImage.Enabled = false;
                    blnAsyncDither = true;
                    this.PICBlankImage.Visible = false;

                    //show next screen
                    //  { scrYouLost, scrYouWin, scrCompleted, scrGameOver, scrPlayAgain
                    switch (enumScreenChosen)
                    {
                        case enumScreens.scrYouLost:
                            PlayerLoses();
                            break;
                        case enumScreens.scrYouWin:
                            PlayerCompletedLevel();
                            break;
                        case enumScreens.scrCompleted:
                            PlayerCompletedGame();
                            break;
                        case enumScreens.scrGameOver:
                            GameOver();
                            break;
                        case enumScreens.scrPlayAgain:
                            //     TMRDitherImage.Enabled = false;
                            blnAsyncDither = true;
                            this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_PLAYAGAIN;
                            this.PICGameEnd.Load();
                            this.PICGameEnd.BringToFront();
                            this.PICGameEnd.Visible = true;
                            BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
                            blnPlayAgain = true;
                            //TMRPlayAgain.Enabled = true;
                            blnAsyncPlayAgain = false;
                            AsyncPlayAgain();
                            break;
                    }
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }


        private async void AsyncCompletedGame()
        {
            /*
              Created 06/11/2025 By Roger Williams

              replaces timer


              shows game completed image and animates colours!

              changes colour of title and text!

            title
              colour1: r: 51  g: 255 b: 255
              colour2: r: 0   g: 255 b: 0
              colour3: r: 102 g: 255 b: 0
              colour4: r: 204 g: 255 b: 255

            main text
              colour1: r: 255 g: 255 b: 51
              colour2: r: 255 g: 255 b: 153
              colour3: r: 255 g: 255 b: 204
              colour4: r: 255 g: 255 b: 255


           */

            int intNum = 0;
            int intNum3 = 0;

            try
            { 
            while (!blnAsyncCompletedGame)
            { 
                await Task.Delay(600);

                //change title colour 360 wide 50 tall
                for (intNum3 = 0; intNum3 != 50; intNum3++)
                {
                    for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
                        //change nose colour
                        if (intCount == 1)
                        {
                            //colour1: r: 51  g: 255 b: 255
                            if (CLRTemp == System.Drawing.Color.FromArgb(51, 255, 255))
                            {
                                //colour2: r: 0   g: 255 b: 0
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(0, 255, 0));
                            }
                            else
                            { //if already colour2 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(0, 255, 0))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(51, 255, 255));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 2)
                        {
                            //colour2: r: 0   g: 255 b: 0
                            if (CLRTemp == System.Drawing.Color.FromArgb(0, 255, 0))
                            {
                                //colour3: r: 102 g: 255 b: 0
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(102, 255, 0));
                            }
                            else
                            { //if already colour3 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(102, 255, 0))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(0, 255, 0));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 3)
                        {
                            //colour3: r: 102 g: 255 b: 0
                            if (CLRTemp == System.Drawing.Color.FromArgb(102, 255, 0))
                            {
                                //colour4: r: 204 g: 255 b: 255
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(204, 255, 255));
                            }
                            else
                            { //if already colour4 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(204, 255, 255))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(102, 255, 0));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 4)
                        {
                            //colour4: playagain4 : r: 21   g: 65   b: 177
                            if (CLRTemp == System.Drawing.Color.FromArgb(21, 65, 177))
                            {
                                //colour1: r: 51  g: 255 b: 255
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(51, 255, 255));
                            }
                            else
                            { //if already colour1 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(51, 255, 255))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(21, 65, 177));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                    }
                }

                //main text
                for (intNum3 = 50; intNum3 != BMPTemp1.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
                        //change nose colour
                        if (intCount == 1)
                        {
                            //colour1: r: 255 g: 255 b: 51
                            if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 51))
                            {
                                //colour2: r: 255 g: 255 b: 153
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 153));
                            }
                            else
                            { //if already colour2 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 153))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 51));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 2)
                        {
                            //colour2: r: 255 g: 255 b: 153
                            if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 153))
                            {
                                //colour3: r: 255 g: 255 b: 204
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 204));
                            }
                            else
                            { //if already colour3 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 204))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 153));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 3)
                        {
                            //colour3: r: 255 g: 255 b: 204
                            if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 204))
                            {
                                //colour4: r: 255 g: 255 b: 255
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 255));
                            }
                            else
                            { //if already colour4 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 255))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 204));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 4)
                        {
                            //colour4: r: 255 g: 255 b: 255
                            if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 255))
                            {
                                //colour1: r: 255 g: 255 b: 51
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 51));
                            }
                            else
                            { //if already colour1 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 51))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 255));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                    }
                }


                this.PICGameEnd.Image = BMPTemp1;
                this.PICGameEnd.Invalidate();
                intCount++;

                if (intCount > 4)
                {
                    intRepeat++;
                    intCount = 1;
                }

                if (intRepeat == 8)
                {
                    blnAsyncCompletedGame = true;
                    intRepeat = 1;
                    intCount = 1;
                    this.PICGameEnd.Visible = false;
                    this.PICBlankImage.ImageLocation = Modules.clsModel.CNST_STR_IMG_BLANKIMAGE;
                    this.PICBlankImage.Load();
                    this.PICBlankImage.BringToFront();
                    this.PICBlankImage.Visible = true;
                    blnPlayAgain = true;
                    BMPTemp1 = new Bitmap(this.PICBlankImage.Image);
                    BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_PLAYAGAIN);
                    enumScreenChosen = enumScreens.scrPlayAgain;
                    blnAsyncDither = false;
                    AsyncDither();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        // private void TMRCompletedGame_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 22/10/2025 By Roger Williams

        //      shows game completed image and animates colours!

        //      changes colour of title and text!

        //    title
        //      colour1: r: 51  g: 255 b: 255
        //      colour2: r: 0   g: 255 b: 0
        //      colour3: r: 102 g: 255 b: 0
        //      colour4: r: 204 g: 255 b: 255

        //    main text
        //      colour1: r: 255 g: 255 b: 51
        //      colour2: r: 255 g: 255 b: 153
        //      colour3: r: 255 g: 255 b: 204
        //      colour4: r: 255 g: 255 b: 255


        //   */

        //    int intNum = 0;
        //    int intNum3 = 0;


        //    await Task.Delay(1000);

        //    //change title colour 360 wide 50 tall
        //    for (intNum3 = 0; intNum3 != 50; intNum3++)
        //    {
        //        for (intNum = 0; intNum != BMPTemp1.Width -1; intNum++)
        //        {
        //            //get pixel colour
        //            CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
        //            //change nose colour
        //            if (intCount == 1)
        //            {
        //                //colour1: r: 51  g: 255 b: 255
        //                if (CLRTemp == System.Drawing.Color.FromArgb(51, 255, 255))
        //                {
        //                    //colour2: r: 0   g: 255 b: 0
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(0, 255, 0));
        //                }
        //                else
        //                { //if already colour2 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(0, 255, 0))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(51, 255, 255));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 2)
        //            {
        //                //colour2: r: 0   g: 255 b: 0
        //                if (CLRTemp == System.Drawing.Color.FromArgb(0, 255, 0))
        //                {
        //                    //colour3: r: 102 g: 255 b: 0
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(102, 255, 0));
        //                }
        //                else
        //                { //if already colour3 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(102, 255, 0))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(0, 255, 0));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 3)
        //            {
        //                //colour3: r: 102 g: 255 b: 0
        //                if (CLRTemp == System.Drawing.Color.FromArgb(102, 255, 0))
        //                {
        //                    //colour4: r: 204 g: 255 b: 255
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(204, 255, 255));
        //                }
        //                else
        //                { //if already colour4 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(204, 255, 255))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(102, 255, 0));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 4)
        //            {
        //                //colour4: playagain4 : r: 21   g: 65   b: 177
        //                if (CLRTemp == System.Drawing.Color.FromArgb(21, 65, 177))
        //                {
        //                    //colour1: r: 51  g: 255 b: 255
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(51, 255, 255));
        //                }
        //                else
        //                { //if already colour1 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(51, 255, 255))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(21, 65, 177));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    //main text
        //    for (intNum3 = 50; intNum3 != BMPTemp1.Height -1; intNum3++)
        //    {
        //        for (intNum = 0; intNum != BMPTemp1.Width -1; intNum++)
        //        {
        //            //get pixel colour
        //            CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
        //            //change nose colour
        //            if (intCount == 1)
        //            {
        //                //colour1: r: 255 g: 255 b: 51
        //                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 51))
        //                {
        //                    //colour2: r: 255 g: 255 b: 153
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 153));
        //                }
        //                else
        //                { //if already colour2 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 153))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 51));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 2)
        //            {
        //                //colour2: r: 255 g: 255 b: 153
        //                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 153))
        //                {
        //                    //colour3: r: 255 g: 255 b: 204
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 204));
        //                }
        //                else
        //                { //if already colour3 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 204))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 153));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 3)
        //            {
        //                //colour3: r: 255 g: 255 b: 204
        //                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 204))
        //                {
        //                    //colour4: r: 255 g: 255 b: 255
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 255));
        //                }
        //                else
        //                { //if already colour4 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 255))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 204));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 4)
        //            {
        //                //colour4: r: 255 g: 255 b: 255
        //                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 255))
        //                {
        //                    //colour1: r: 255 g: 255 b: 51
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 51));
        //                }
        //                else
        //                { //if already colour1 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 51))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 255));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //        }
        //    }


        //    this.PICGameEnd.Image = BMPTemp1;
        //    this.PICGameEnd.Invalidate();
        //    intCount++;

        //    if (intCount > 4)
        //    {
        //        intRepeat++;
        //        intCount = 1;
        //    }

        //    if (intRepeat == 16)
        //    {
        //        TMRCompletdGame.Enabled = false;
        //        intRepeat = 1;
        //        intCount = 1;
        //        this.PICGameEnd.Visible = false;
        //        this.PICBlankImage.ImageLocation = Modules.clsModel.CNST_STR_IMG_BLANKIMAGE;
        //        this.PICBlankImage.Load();
        //        this.PICBlankImage.BringToFront();
        //        this.PICBlankImage.Visible = true;
        //        BMPTemp1 = new Bitmap(this.PICBlankImage.Image);
        //        BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_PLAYAGAIN);
        //        enumScreenChosen = enumScreens.scrPlayAgain;
        //        //      TMRDitherImage.Interval = 300;
        //        //      TMRDitherImage.Enabled = true;
        //        blnAsyncDither = false;
        //        AsyncDither();
        //    }
        //}


        //private void TMRDitherImage_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 22/10/2025 By Roger Williams

        //      dithers bmptemp2 into bmptemp 1


        //    */

        //    Random rndNum = new Random();
        //    int intNum = 0;
        //    System.Drawing.Color clrTemp = System.Drawing.Color.Transparent;

        //    bool ImagesMatch()
        //    {
        //        /*
        //          Compares bmptemp1 with 2

        //        */

        //        int intX = 0;
        //        int intY = 0;
        //        bool blnCont = true;
        //        bool blnAllMatch = true;

        //        while (blnCont)
        //        {
        //            if (BMPTemp1.GetPixel(intX, intY) != BMPTemp2.GetPixel(intX, intY))
        //            {
        //                blnCont = false;
        //                blnAllMatch = false;
        //            }

        //            intX++;

        //            if (intX >= BMPTemp1.Width)
        //            {
        //                intX = 0;
        //                intY++;

        //                if (intY >= BMPTemp1.Height)
        //                {
        //                    blnCont = false;
        //                }
        //            }
        //        }

        //        return blnAllMatch;
        //    }




        //    for (intNum = 0; intNum < 40000; intNum++)
        //    {
        //        intX = Math.Abs(rndNum.Next(this.PICBlankImage.Width));
        //        intY = Math.Abs(rndNum.Next(this.PICBlankImage.Height));

        //        //get pixel colour from second image
        //        CLRTemp = BMPTemp2.GetPixel(intX, intY);
        //        BMPTemp1.SetPixel(intX, intY, CLRTemp);
        //        intCount++;
        //    }

        //    this.PICBlankImage.Image = BMPTemp1;
        //    this.PICBlankImage.Invalidate();

        //    //dither complete?
        //    if (ImagesMatch())
        //    {
        //        intCount = 0;
        //        TMRDitherImage.Enabled = false;
        //        this.PICBlankImage.Visible = false;

        //        //show next screen
        //        //  { scrYouLost, scrYouWin, scrCompleted, scrGameOver, scrPlayAgain
        //        switch (enumScreenChosen)
        //        {
        //            case enumScreens.scrYouLost:
        //                PlayerLoses();
        //                break;
        //            case enumScreens.scrYouWin:
        //                PlayerCompletedLevel();
        //                break;
        //            case enumScreens.scrCompleted:
        //                PlayerCompletedGame();
        //                break;
        //            case enumScreens.scrGameOver:
        //                GameOver();
        //                break;
        //            case enumScreens.scrPlayAgain:
        //                TMRDitherImage.Enabled = false;
        //                this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_PLAYAGAIN;
        //                this.PICGameEnd.Load();
        //                this.PICGameEnd.BringToFront();
        //                this.PICGameEnd.Visible = true;
        //                BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
        //                blnPLayAgain = true;
        //                TMRPlayAgain.Enabled = true;
        //                break;
        //        }
        //    }
        //}


        private async void AsyncPlayAgain()
        {
            /*
              Created 06/11/2025 By Roger Williams

              replaces timer               


               play again?
               
               playagain1 : r: 172  g: 187  b: 224
               playagain2 : r: 91   g: 135  b: 247
               playagain3 : r: 28   g: 81   b: 214
               playagain4 : r: 21   g: 65   b: 177


             */

            int intNum = 0;
            int intNum3 = 0;

            try
            { 
            while (!blnAsyncPlayAgain)
            { 
                await Task.Delay(4000);

                for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
                        //change nose colour
                        if (intCount == 1)
                        {
                            //colour1: playagain1 : r: 172  g: 187  b: 224
                            if (CLRTemp == System.Drawing.Color.FromArgb(172, 187, 224))
                            {
                                //colour2: playagain2 : r: 91   g: 135  b: 247
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(91, 135, 247));
                            }
                            else
                            { //if already colour2 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(91, 135, 247))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(172, 187, 224));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 2)
                        {
                            //colour2: playagain2 : r: 91   g: 135  b: 247
                            if (CLRTemp == System.Drawing.Color.FromArgb(91, 135, 247))
                            {
                                //colour3: playagain3 : r: 28   g: 81   b: 214
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(28, 81, 214));
                            }
                            else
                            { //if already colour3 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(28, 81, 214))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(91, 135, 247));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 3)
                        {
                            //colour3: playagain3 : r: 28   g: 81   b: 214
                            if (CLRTemp == System.Drawing.Color.FromArgb(28, 81, 214))
                            {
                                //colour4: playagain4 : r: 21   g: 65   b: 177
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(21, 65, 177));
                            }
                            else
                            { //if already colour4 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(21, 65, 177))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(28, 81, 214));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 4)
                        {
                            //colour4: playagain4 : r: 21   g: 65   b: 177
                            if (CLRTemp == System.Drawing.Color.FromArgb(21, 65, 177))
                            {
                                //colour1: playagain1 : r: 172  g: 187  b: 224
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(172, 187, 224));
                            }
                            else
                            { //if already colour1 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(172, 187, 224))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(21, 65, 177));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                    }
                }

                this.PICGameEnd.Image = BMPTemp1;
                this.PICGameEnd.Invalidate();
                intCount++;

                if (intCount > 4)
                {
                    intRepeat++;
                    intCount = 1;
                }

                if (intRepeat == 16) //?? disable this code?
                {
                    blnAsyncPlayAgain = true; ;
                    this.PICGameEnd.Visible = false;
                    intRepeat = 1;
                    intCount = 1;
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }


        //private void TMRPlayAgain_Tick(object sender, EventArgs e)
        //{
        //    /*

        //       play again?

        //       playagain1 : r: 172  g: 187  b: 224
        //       playagain2 : r: 91   g: 135  b: 247
        //       playagain3 : r: 28   g: 81   b: 214
        //       playagain4 : r: 21   g: 65   b: 177


        //     */

        //    int intNum = 0;
        //    int intNum3 = 0;


        //    await Task.Delay(4000);

        //    for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
        //    {
        //        for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
        //        {
        //            //get pixel colour
        //            CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
        //            //change nose colour
        //            if (intCount == 1)
        //            {
        //                //colour1: playagain1 : r: 172  g: 187  b: 224
        //                if (CLRTemp == System.Drawing.Color.FromArgb(172, 187, 224))
        //                {
        //                    //colour2: playagain2 : r: 91   g: 135  b: 247
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(91, 135, 247));
        //                }
        //                else
        //                { //if already colour2 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(91, 135, 247))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(172, 187, 224));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 2)
        //            {
        //                //colour2: playagain2 : r: 91   g: 135  b: 247
        //                if (CLRTemp == System.Drawing.Color.FromArgb(91, 135, 247))
        //                {
        //                    //colour3: playagain3 : r: 28   g: 81   b: 214
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(28, 81, 214));
        //                }
        //                else
        //                { //if already colour3 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(28, 81, 214))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(91, 135, 247));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 3)
        //            {
        //                //colour3: playagain3 : r: 28   g: 81   b: 214
        //                if (CLRTemp == System.Drawing.Color.FromArgb(28, 81, 214))
        //                {
        //                    //colour4: playagain4 : r: 21   g: 65   b: 177
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(21, 65, 177));
        //                }
        //                else
        //                { //if already colour4 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(21, 65, 177))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(28, 81, 214));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 4)
        //            {
        //                //colour4: playagain4 : r: 21   g: 65   b: 177
        //                if (CLRTemp == System.Drawing.Color.FromArgb(21, 65, 177))
        //                {
        //                    //colour1: playagain1 : r: 172  g: 187  b: 224
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(172, 187, 224));
        //                }
        //                else
        //                { //if already colour1 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(172, 187, 224))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(21, 65, 177));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    this.PICGameEnd.Image = BMPTemp1;
        //    this.PICGameEnd.Invalidate();
        //    intCount++;

        //    if (intCount > 4)
        //    {
        //        intRepeat++;
        //        intCount = 1;
        //    }

        //    if (intRepeat == 16)
        //    {
        //        TMRPlayAgain.Enabled = false;
        //        this.PICGameEnd.Visible = false;
        //        intRepeat = 1;
        //        intCount = 1;
        //    }
        //}

        private async void AsyncYouLost()
        {
            /*
              Created 06/11/2025 By Roger Williams
              
              replaces timer 

            */
            int intNum = 0;
            int intNum3 = 0;

            try
            { 
            while (!blnAsyncYouLose)
            {
                await Task.Delay(4000);

                for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
                        //change nose colour
                        if (intCount == 1)
                        {
                            /*
                              1:
                               r: 40
                               g: 70
                               b: 183
                            */
                            if (CLRTemp.R == 40 && CLRTemp.G == 70 && CLRTemp.B == 183)
                            {
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(86, 108, 188));
                            }
                            else
                            { //if already white change back
                                if (CLRTemp.R == 255 && CLRTemp.B == 255 && CLRTemp.G == 255)
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(40, 70, 183));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 2)
                        {
                            /*
                             r: 86
                             g: 108
                             b: 188
                            */
                            if (CLRTemp.R == 86 && CLRTemp.G == 108 && CLRTemp.B == 188)
                            {
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(40, 70, 183));

                            }
                            else
                            { //if already white change back
                                if (CLRTemp.R == 255 && CLRTemp.B == 255 && CLRTemp.G == 255)
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(86, 108, 188));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 3)
                        {
                            /*
                             3:
                             r: 130
                             g: 148
                             b: 214
                            */
                            if (CLRTemp.R == 130 && CLRTemp.G == 148 && CLRTemp.B == 214)
                            {
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(86, 108, 188));
                            }
                            else
                            { //if already white change back
                                if (CLRTemp.R == 255 && CLRTemp.B == 255 && CLRTemp.G == 255)
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(130, 148, 214));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 4)
                        {
                            /*
                             4:
                             r: 188
                             g: 197
                             b: 232
                            */

                            if (CLRTemp.R != 0)
                            {
                                intCount = intCount;
                            }

                            if (CLRTemp.R == 130 && CLRTemp.G == 148 && CLRTemp.B == 214)
                            {
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.Green);
                            }
                            else
                            { //if already white change back
                                if (CLRTemp.R == 255 && CLRTemp.B == 255 && CLRTemp.G == 255)
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(188, 197, 232));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                    }
                }

                this.PICGameEnd.Image = BMPTemp1;
                this.PICGameEnd.Invalidate();

                intCount++;

                if (intCount == 4)
                {
                    intRepeat++;
                    intCount = 1;
                }

                if (intRepeat == 16)
                {
                    //  TMRYouLose.Enabled = false;
                    blnAsyncYouLose = true;
                    this.PICGameEnd.Visible = false;
                    intRepeat = 1;
                    intCount = 1;
                    this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_PLAYAGAIN;
                    this.PICGameEnd.Load();
                    this.PICGameEnd.BringToFront();
                    this.PICGameEnd.Visible = true;
                    BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
                    blnPlayAgain = true;
                    blnAsyncPlayAgain = false;
                    AsyncPlayAgain();
                    // TMRPlayAgain.Enabled = true;
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }
        //private void TMRYouLose_Tick(object sender, EventArgs e)
        //{
        //    int intNum = 0;
        //    int intNum3 = 0;



        //    await Task.Delay(4000);

        //    for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
        //    {
        //        for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
        //        {
        //            //get pixel colour
        //            CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
        //            //change nose colour
        //            if (intCount == 1)
        //            {
        //                /*
        //                  1:
        //                   r: 40
        //                   g: 70
        //                   b: 183
        //                */
        //                if (CLRTemp.R == 40 && CLRTemp.G == 70 && CLRTemp.B == 183)
        //                {
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(86, 108, 188));
        //                }
        //                else
        //                { //if already white change back
        //                    if (CLRTemp.R == 255 && CLRTemp.B == 255 && CLRTemp.G == 255)
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(40, 70, 183));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 2)
        //            {
        //                /*
        //                 r: 86
        //                 g: 108
        //                 b: 188
        //                */
        //                if (CLRTemp.R == 86 && CLRTemp.G == 108 && CLRTemp.B == 188)
        //                {
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(40, 70, 183));

        //                }
        //                else
        //                { //if already white change back
        //                    if (CLRTemp.R == 255 && CLRTemp.B == 255 && CLRTemp.G == 255)
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(86, 108, 188));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 3)
        //            {
        //                /*
        //                 3:
        //                 r: 130
        //                 g: 148
        //                 b: 214
        //                */
        //                if (CLRTemp.R == 130 && CLRTemp.G == 148 && CLRTemp.B == 214)
        //                {
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(86, 108, 188));
        //                }
        //                else
        //                { //if already white change back
        //                    if (CLRTemp.R == 255 && CLRTemp.B == 255 && CLRTemp.G == 255)
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(130, 148, 214));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 4)
        //            {
        //                /*
        //                 4:
        //                 r: 188
        //                 g: 197
        //                 b: 232
        //                */

        //                if (CLRTemp.R != 0)
        //                {
        //                    intCount = intCount;
        //                }

        //                if (CLRTemp.R == 130 && CLRTemp.G == 148 && CLRTemp.B == 214)
        //                {
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.Green);
        //                }
        //                else
        //                { //if already white change back
        //                    if (CLRTemp.R == 255 && CLRTemp.B == 255 && CLRTemp.G == 255)
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(188, 197, 232));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    this.PICGameEnd.Image = BMPTemp1;
        //    this.PICGameEnd.Invalidate();

        //    intCount++;

        //    if (intCount == 4)
        //    {
        //        intRepeat++;
        //        intCount = 1;
        //    }

        //    if (intRepeat == 16)
        //    {
        //        TMRYouLose.Enabled = false;
        //        this.PICGameEnd.Visible = false;
        //        intRepeat = 1;
        //        intCount = 1;
        //        this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_PLAYAGAIN;
        //        this.PICGameEnd.Load();
        //        this.PICGameEnd.BringToFront();
        //        this.PICGameEnd.Visible = true;
        //        BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
        //        blnPLayAgain = true;
        //        TMRPlayAgain.Enabled = true;
        //    }
        //}

        private async void AsyncGameOver()
        {
            /*
              Created 05/11/2025 By Roger Williams

              used instead of timer
            
              benefits:
              - less resources used as not a control
              
              Note: needs to be async else need to use thread.sleep which will stop everything!

              Game over!!

              gameover1 : r: 60  g: 196  b: 118
              gameover2 : r: 77  g: 235  b: 144
              gameover3 : r: 131 g: 251  b: 182
              gameover4 : r: 172 g: 224  b: 194

            */
            int intNum = 0;
            int intNum3 = 0;


            try
            {  
            while (!blnAsyncGameOver)
            {
                await Task.Delay(1000);

                for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
                        //change nose colour
                        if (intCount == 1)
                        {
                            //colour1: gameover1 : r: 60  g: 196  b: 118
                            if (CLRTemp == System.Drawing.Color.FromArgb(60, 196, 118))
                            {
                                //colour2: gameover2 : r: 77  g: 235  b: 144
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(77, 235, 144));
                            }
                            else
                            { //if already colour2 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(77, 235, 144))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(60, 196, 118));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 2)
                        {
                            //colour2: gameover2 : r: 77  g: 235  b: 144
                            if (CLRTemp == System.Drawing.Color.FromArgb(77, 235, 144))
                            {
                                //colour3: gameover3 : r: 131 g: 251  b: 182
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(131, 251, 182));
                            }
                            else
                            { //if already colour3 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(131, 251, 182))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(77, 235, 144));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 3)
                        {
                            //colour3: gameover3 : r: 131 g: 251  b: 182
                            if (CLRTemp == System.Drawing.Color.FromArgb(131, 251, 182))
                            {
                                //colour4: gameover4 : r: 172 g: 224  b: 194
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(172, 224, 194));
                            }
                            else
                            { //if already colour4 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(172, 224, 194))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(131, 251, 182));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 4)
                        {
                            //colour4: gameover4 : r: 172 g: 224  b: 194
                            if (CLRTemp == System.Drawing.Color.FromArgb(172, 224, 194))
                            {
                                //colour1: gameover1 : r: 60  g: 196  b: 118
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(60, 196, 118));
                            }
                            else
                            { //if already colour1 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(60, 196, 118))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(172, 224, 194));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                    }
                }

                this.PICGameEnd.Image = BMPTemp1;
                this.PICGameEnd.Invalidate();

                intCount++;

                if (intCount > 4)
                {
                    intRepeat++;
                    intCount = 1;
                }

                if (intRepeat == 3)
                {
                    blnAsyncGameOver = true;
                    //  this.PICGameEnd.Visible = false;
                    intRepeat = 1;
                    intCount = 1;
                    blnPlayAgain = true;
                    this.PICBlankImage.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_GAMEOVER;
                    this.PICBlankImage.Load();
                    this.PICBlankImage.BringToFront();
                    this.PICBlankImage.Visible = true;
                    BMPTemp1 = new Bitmap(this.PICBlankImage.Image);
                    BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_PLAYAGAIN);
                    enumScreenChosen = enumScreens.scrPlayAgain;
                    //   TMRDitherImage.Interval = 200;
                    //  TMRDitherImage.Enabled = true;

                    //  TMRPlayAgain.Enabled = true;

                    blnAsyncDither = false;
                    AsyncDither();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }


        //private void TMRGameOver_Tick(object sender, EventArgs e)
        //{
        //    /*

        //      Game over!!

        //      gameover1 : r: 60  g: 196  b: 118
        //      gameover2 : r: 77  g: 235  b: 144
        //      gameover3 : r: 131 g: 251  b: 182
        //      gameover4 : r: 172 g: 224  b: 194

        //    */
        //    int intNum = 0;
        //    int intNum3 = 0;

        //    for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
        //    {
        //        for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
        //        {
        //            //get pixel colour
        //            CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
        //            //change nose colour
        //            if (intCount == 1)
        //            {
        //                //colour1: gameover1 : r: 60  g: 196  b: 118
        //                if (CLRTemp == System.Drawing.Color.FromArgb(60, 196, 118))
        //                {
        //                    //colour2: gameover2 : r: 77  g: 235  b: 144
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(77, 235, 144));
        //                }
        //                else
        //                { //if already colour2 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(77, 235, 144))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(60, 196, 118));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 2)
        //            {
        //                //colour2: gameover2 : r: 77  g: 235  b: 144
        //                if (CLRTemp == System.Drawing.Color.FromArgb(77, 235, 144))
        //                {
        //                    //colour3: gameover3 : r: 131 g: 251  b: 182
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(131, 251, 182));
        //                }
        //                else
        //                { //if already colour3 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(131, 251, 182))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(77, 235, 144));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 3)
        //            {
        //                //colour3: gameover3 : r: 131 g: 251  b: 182
        //                if (CLRTemp == System.Drawing.Color.FromArgb(131, 251, 182))
        //                {
        //                    //colour4: gameover4 : r: 172 g: 224  b: 194
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(172, 224, 194));
        //                }
        //                else
        //                { //if already colour4 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(172, 224, 194))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(131, 251, 182));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //            if (intCount == 4)
        //            {
        //                //colour4: gameover4 : r: 172 g: 224  b: 194
        //                if (CLRTemp == System.Drawing.Color.FromArgb(172, 224, 194))
        //                {
        //                    //colour1: gameover1 : r: 60  g: 196  b: 118
        //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(60, 196, 118));
        //                }
        //                else
        //                { //if already colour1 change back
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(60, 196, 118))
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(172, 224, 194));
        //                    }
        //                    else
        //                    {
        //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    this.PICGameEnd.Image = BMPTemp1;
        //    this.PICGameEnd.Invalidate();

        //    intCount++;

        //    if (intCount > 4)
        //    {
        //        intRepeat++;
        //        intCount = 1;
        //    }

        //    if (intRepeat == 16)
        //    {
        //        TMRGameOver.Enabled = false;
        //      //  this.PICGameEnd.Visible = false;
        //        intRepeat = 1;
        //        intCount = 1;
        //        this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_PLAYAGAIN;
        //        this.PICGameEnd.Load();
        //        this.PICGameEnd.BringToFront();
        //        this.PICGameEnd.Visible = true;
        //        BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
        //        blnPLayAgain = true;
        //        TMRPlayAgain.Enabled = true;
        //    }
        //}

        private async void AsyncYouWin()
        {
            /*

                Created 06/11/2025 By Roger Williams

                used instead of timer

                    Player wins!

                    youwin1: r: 196  g: 166  b: 60
                    youwin2: r: 255  g: 206  b: 89
                    youwin3: r: 242  g: 188  b: 0
                    youwin4: r: 233  g: 189  b: 35

            */

            int intNum = 0;
            int intNum3 = 0;


            try
            { 
            while (!blnAsyncYouWin)
            {
                await Task.Delay(500);

                for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
                        //change nose colour
                        if (intCount == 1)
                        {
                            //colour1: youwin1: r: 196  g: 166  b: 60
                            if (CLRTemp == System.Drawing.Color.FromArgb(196, 166, 60))
                            {
                                //colour2: youwin2: r: 255  g: 206  b: 89
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 206, 89));
                            }
                            else
                            { //if already colour2 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(255, 206, 89))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(196, 166, 60));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 2)
                        {
                            //colour2: youwin2: r: 255  g: 206  b: 89
                            if (CLRTemp == System.Drawing.Color.FromArgb(255, 206, 89))
                            {
                                //colour3: youwin3: r: 242  g: 188  b: 0
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(242, 188, 0));
                            }
                            else
                            { //if already colour3 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(242, 188, 0))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 206, 89));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 3)
                        {
                            //colour3: youwin3: r: 242  g: 188  b: 0
                            if (CLRTemp == System.Drawing.Color.FromArgb(242, 188, 0))
                            {
                                //colour4: youwin4: r: 233  g: 189  b: 35
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(233, 189, 35));
                            }
                            else
                            { //if already colour4 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(233, 189, 35))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(242, 188, 0));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                        if (intCount == 4)
                        {
                            //colour4: youwin4: r: 233  g: 189  b: 35
                            if (CLRTemp == System.Drawing.Color.FromArgb(233, 189, 35))
                            {
                                //colour1: youwin1: r: 196  g: 166  b: 60
                                BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(196, 66, 60));
                            }
                            else
                            { //if already colour1 change back
                                if (CLRTemp == System.Drawing.Color.FromArgb(196, 66, 60))
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(233, 189, 35));
                                }
                                else
                                {
                                    BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                }
                            }
                        }
                    }
                }

                this.PICBlankImage.Image = BMPTemp1;
                this.PICBlankImage.Invalidate();

                intCount++;

                if (intCount > 4)
                {
                    intRepeat++;
                    intCount = 1;
                }

                if (intRepeat == 5)
                {
                    blnAsyncYouWin = true;
                    this.PICBlankImage.Visible = false;
                    intRepeat = 1;
                    intCount = 1;
                    blnCanProgress = true;
                    //if not completed game goto next level
                    //    if (intCurLevel <= Modules.clsModel.CNST_INT_MAXLEVELS)
                    //    {
                    Init();
                    //}
                    //else
                    //{
                    //    //show ending screen before gameover??

                    //    //  TMRGameOver.Enabled = true;
                    //    PlayerCompletedGame();
                    //}
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }



        //private void TMRYouWin_Tick(object sender, EventArgs e)
        //{
        //    /*

            //      Player wins!

            //      youwin1: r: 196  g: 166  b: 60
            //      youwin2: r: 255  g: 206  b: 89
            //      youwin3: r: 242  g: 188  b: 0
            //      youwin4: r: 233  g: 189  b: 35

            //    */

            //    int intNum = 0;
            //    int intNum3 = 0;



            //    await Task.Delay(4000);


            //    for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
            //    {
            //        for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
            //        {
            //            //get pixel colour
            //            CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);
            //            //change nose colour
            //            if (intCount == 1)
            //            {
            //                //colour1: youwin1: r: 196  g: 166  b: 60
            //                if (CLRTemp == System.Drawing.Color.FromArgb(196, 166, 60))
            //                {
            //                    //colour2: youwin2: r: 255  g: 206  b: 89
            //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 206, 89));
            //                }
            //                else
            //                { //if already colour2 change back
            //                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 206, 89))
            //                    {
            //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(196, 166, 60));
            //                    }
            //                    else
            //                    {
            //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
            //                    }
            //                }
            //            }
            //            if (intCount == 2)
            //            {
            //                //colour2: youwin2: r: 255  g: 206  b: 89
            //                if (CLRTemp == System.Drawing.Color.FromArgb(255, 206, 89))
            //                {
            //                    //colour3: youwin3: r: 242  g: 188  b: 0
            //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(242, 188, 0));
            //                }
            //                else
            //                { //if already colour3 change back
            //                    if (CLRTemp == System.Drawing.Color.FromArgb(242, 188, 0))
            //                    {
            //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 206, 89));
            //                    }
            //                    else
            //                    {
            //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
            //                    }
            //                }
            //            }
            //            if (intCount == 3)
            //            {
            //                //colour3: youwin3: r: 242  g: 188  b: 0
            //                if (CLRTemp == System.Drawing.Color.FromArgb(242, 188, 0))
            //                {
            //                    //colour4: youwin4: r: 233  g: 189  b: 35
            //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(233, 189, 35));
            //                }
            //                else
            //                { //if already colour4 change back
            //                    if (CLRTemp == System.Drawing.Color.FromArgb(233, 189, 35))
            //                    {
            //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(242, 188, 0));
            //                    }
            //                    else
            //                    {
            //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
            //                    }
            //                }
            //            }
            //            if (intCount == 4)
            //            {
            //                //colour4: youwin4: r: 233  g: 189  b: 35
            //                if (CLRTemp == System.Drawing.Color.FromArgb(233, 189, 35))
            //                {
            //                    //colour1: youwin1: r: 196  g: 166  b: 60
            //                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(196, 66, 60));
            //                }
            //                else
            //                { //if already colour1 change back
            //                    if (CLRTemp == System.Drawing.Color.FromArgb(196, 66, 60))
            //                    {
            //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(233, 189, 35));
            //                    }
            //                    else
            //                    {
            //                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    this.PICGameEnd.Image = BMPTemp1;
            //    this.PICGameEnd.Invalidate();

            //    intCount++;

            //    if (intCount > 4)
            //    {
            //        intRepeat++;
            //        intCount = 1;
            //    }

            //    if (intRepeat == 16)
            //    {
            //        TMRYouWin.Enabled = false;
            //        this.PICGameEnd.Visible = false;
            //        intRepeat = 1;
            //        intCount = 1;

            //        //if not completed game goto next level
            //        if (intCurLevel <= Modules.clsModel.CNST_INT_MAXLEVELS)
            //        {
            //            Init();
            //        }
            //        else
            //        {
            //            //show ending screen before gameover??

            //            //  TMRGameOver.Enabled = true;
            //            GameOver();
            //        }
            //    }
            //}


        //private async Task TMRAlienShot_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 06/10/2025 By Roger Williams

        //      fire alien shot

        //    */

        //    string strTemp = string.Empty;

        //    //*******sub functions/procedures****


        //    string GetRandomAlien()
        //    {
        //        /*
        //          Created 06/10/2025 By Roger Williams

        //          gets any alien (visible) closest to the player


        //          RETURNS:

        //          alien picturebox name

        //        */

        //        int intNum = 0;
        //        string strWhat = String.Empty;
        //        string strAlien = String.Empty;
        //        Random rndAlien;
        //        bool blnFound = false;

        //        //find ANY visible alien on row 2
        //        rndAlien = new Random();

        //        while (!blnFound)
        //        {
        //            intNum = rndAlien.Next(1, 6);

        //            if (intNum != 0)
        //            {
        //                if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum.ToString()].Visible)
        //                {
        //                    strWhat = Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum.ToString();
        //                    blnFound = true;
        //                }
        //            }
        //        }

        //        //if not found check row 1!
        //        if (!blnFound)
        //        {
        //            while (!blnFound)
        //            {
        //                intNum = rndAlien.Next(1, 6);

        //                if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum.ToString()].Visible)
        //                {
        //                    strWhat = Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum.ToString();
        //                    blnFound = true;
        //                }
        //            }
        //        }

        //        return strWhat;
        //    }

        //    //*********end sub functions/procedures********

        //    while (!blnAsyncAlienShot)
        //    { 
        //        await Task.Delay(Modules.clsView.CNST_INT_GAMECREATEALIENSHOT_INTERVAL);

        //        //make sure shot not already in progress
        //        if (this.PICAlienShot.Visible == false)
        //        {

        //            ////get random alien closest to player 
        //            strTemp = GetRandomAlien();

        //            if (strTemp != String.Empty)
        //            {
        //                clsActiveAlienShot.AlienShoot(this.Controls[strTemp].Left + 6, this.Controls[strTemp].Top + Modules.clsView.CNST_INT_ALIEN_HEIGHT);
        //            }
        //        }
        //    }
        //}

        private async void AsyncAlienShot()
        {
            /*
              Created 06/11/2025 By Roger Williams

              replaces timer 

              fire alien shot

            */

            string strTemp = string.Empty;

            //*******sub functions/procedures****

            int CountVisibleAliensOnRow(int intRow)
            {
                /*
                  Created 18/11/2025 By Roger Williams

                  gets count of aliens on passed row


                  RETURNS:

                  alien count

                */

                int intNum = 0;
                int intCount = 0;

                for (intNum = 1; intNum != 7; intNum++)
                {

                    if (intRow == 1)
                    {
                        if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum.ToString()].Visible)
                        {
                            intCount++;
                        }
                    }
                    else
                    {
                        if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum.ToString()].Visible)
                        {
                            intCount++;
                        }
                    }
                }

                return intCount;
            }

            string GetRandomAlien()
            {
                /*
                  Created 06/10/2025 By Roger Williams

                  gets any alien (visible) closest to the player


                  RETURNS:

                  alien picturebox name

                */

                int intNum = 0;
                int intAliensRow1 = CountVisibleAliensOnRow(1);
                int intAliensRow2 = CountVisibleAliensOnRow(2);

                string strWhat = String.Empty;
                string strAlien = String.Empty;
                Random rndAlien;
                bool blnFound = false;

                //find ANY visible alien on row 2
                rndAlien = new Random();

                if (intAliensRow2 != 0)
                {
                    while (!blnFound)
                    {
                        Application.DoEvents();

                        intNum = rndAlien.Next(1, 6);

                        if (intNum != 0)
                        {
                            if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum.ToString()].Visible)
                            {
                                strWhat = Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum.ToString();
                                blnFound = true;
                            }
                        }
                    }
                }

                //if not found check row 1!
                if (intAliensRow1 != 0)
                { 
                    if (!blnFound)
                    {
                        while (!blnFound)
                        {
                            Application.DoEvents();

                            intNum = rndAlien.Next(1, 6);

                            if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum.ToString()].Visible)
                            {
                                strWhat = Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum.ToString();
                                blnFound = true;
                            }
                        }
                    }
                }

                return strWhat;
            }

            //*********end sub functions/procedures********
            try
            { 
                while (!blnAsyncAlienShot)
                {
                    switch (intCurLevel)
                    {
                        case 1:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENSHOT_SHOT_INTERVAL_LEVEL1);
                            break;
                        case 2:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENSHOT_SHOT_INTERVAL_LEVEL2);
                            break;
                        case 3:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENSHOT_SHOT_INTERVAL_LEVEL3);
                            break;
                        case 4:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENSHOT_SHOT_INTERVAL_LEVEL4);
                            break;
                    }

                    //make sure shot not already in progress
                    if (this.PICAlienShot.Visible == false)
                    {
                        if (!blnInvasion) //don't shoot if aliens won!
                        {
                            ////get random alien closest to player 
                            strTemp = GetRandomAlien();

                            if (strTemp != String.Empty)
                            {
                                clsActiveAlienShot.AlienShoot(this.Controls[strTemp].Left + Modules.clsView.CNST_INT_ALIEN_SHOT_OFFSET, this.Controls[strTemp].Top + Modules.clsView.CNST_INT_ALIEN_HEIGHT, intCurLevel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }


        private async void AsyncCollision()
        {
            /*
             
            Created 06/11/2025 By Roger Williams

            used instead of timer 
             

              checks for any collisons, process player life loss

              checks for collisions:

              - player bullet own base
              - player bullet alien
              - player bullet alien mothership
              - alien bullet base
              - alien bullet player
              - alien hit base (gaem over baby!)

            */
            int intAlienHit = 0;
            int intAlienHitType = 0;
            int intBaseUnitHitSide = 0;
            int intBaseUnitHit = 0;


            //*******sub functions/procedures****

            void GetNearestAlienToPlayerShot()
            {
                /*
                  Created 05/10/2025 By Roger Williams

                  finds the FIRST alien directly above the players shot (if any)

                */

                int intNum = 0;
                int intWhat = 0;

                intAlienHit = 0;
                intAlienHitType = 0;

                for (intNum = 1; intNum != 7; intNum++)
                {
                    //only process VISIBLE aliens
                    if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Visible)
                    {
                        //see if playershot left between alien on row 2 left and left + width
                        if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Left + Modules.clsView.CNST_INT_ALIEN_WIDTH, this.PICPlayerShot.Left))
                        {
                            //make sure playershot is DIRECTLY below the alien
                            //    if (this.Controls[Modules.clsModel.CNST_STR_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT / 2) + 10 >= this.PICPlayerShot.Top - 10)
                            if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT / 2), this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT + 10), this.PICPlayerShot.Top))
                            {
                                intAlienHit = 6 + intNum; //add 6 as 12 aliens in wave and this is SECOND row
                                intAlienHitType = 2;
                                break;
                            }
                        }
                    }
                }


                if (intAlienHit == 0)
                {
                    for (intNum = 1; intNum != 7; intNum++)
                    {
                        //only process VISIBLE aliens
                        if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Visible)
                        {
                            //see if playershot left between alien on row 1 left and left + width
                            if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Left + Modules.clsView.CNST_INT_ALIEN_WIDTH, this.PICPlayerShot.Left))
                            {
                                //make sure playershot is DIRECTLY below the alien
                                //    if (this.Controls[Modules.clsModel.CNST_STR_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT / 2) + 10 >= this.PICPlayerShot.Top - 10)
                                if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT / 2), this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT + 10), this.PICPlayerShot.Top))
                                {
                                    intAlienHit = intNum; //add 6 as 12 aliens in wave and this is SECOND row
                                    intAlienHitType = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            bool CheckIfAlienHitPlayerBase()
            {
                /*
                  Created 06/10/2025 By Roger Williams

                  checks if any alien directly touching the players base

                */

                int intNum = 0;
                bool blnHit = false;

                for (intNum = 1; intNum != 7; intNum++)
                {
                    //only process VISIBLE aliens
                    if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Visible)
                    {
                        //see if alien top+height >= baseunit top +4
                        if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + Modules.clsView.CNST_INT_ALIEN_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Top + 4)
                        {
                            blnHit = true;
                            break;
                        }
                        //see if alien top+height >= baseunit top +4
                        if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + Modules.clsView.CNST_INT_ALIEN_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Top + 4)
                        {
                            blnHit = true;
                            break;
                        }
                    }
                }

                return blnHit;
            }

            bool CheckIfAlienShotHitBase()
            {
                /*
                  Created 06/10/2025 By Roger Williams

                  checks if alien shot directly touching the players base

                */

                int intNum = 0;
                int intWhat = 0;
                bool blnBaseHit = false;

                intBaseUnitHitSide = 0;
                intBaseUnitHit = 0;

                for (intNum = 1; intNum != 7; intNum++)
                {
                    if (this.PICAlienShot.Top + Modules.clsView.CNST_INT_ALIENBULLET_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Top - 10)
                    {
                        if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left + Modules.clsView.CNST_INT_BASE_WIDTH, this.PICAlienShot.Left))
                        {
                            //only register hit if VISIBLE
                            if (this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Visible)
                            { 
                                intBaseUnitHitSide = 1;
                                intBaseUnitHit = intNum;
                                blnBaseHit = true;
                                break;
                            }
                        }
                    }

                    if (this.PICAlienShot.Top + Modules.clsView.CNST_INT_ALIENBULLET_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Top - 10)
                    {
                        //check if left of alien shot < baseunit AND baseunit left INSIDE range alien shot left -> alien shot left + width
                        if (this.PICAlienShot.Left < this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left)
                        {
                            if (Modules.clsModel.InRange(this.PICAlienShot.Left, this.PICAlienShot.Left + Modules.clsView.CNST_INT_ALIENBULLET_WIDTH, this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left))
                            {
                                //only register hit if VISIBLE
                                if (this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Visible)
                                {
                                    intBaseUnitHitSide = 1;
                                    intBaseUnitHit = intNum;
                                    blnBaseHit = true;
                                    break;
                                }
                        }
                        }
                    }

                    if (this.PICAlienShot.Top + Modules.clsView.CNST_INT_ALIENBULLET_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Top + 4)
                    {
                        if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Left + Modules.clsView.CNST_INT_BASE_WIDTH, this.PICAlienShot.Left))
                        {
                            //only register hit if VISIBLE
                            if (this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Visible)
                            {
                                intBaseUnitHitSide = 2;
                                intBaseUnitHit = intNum;
                                blnBaseHit = true;
                                break;
                            }
                        }
                    }
                }

                return blnBaseHit;
            }


            bool CheckIfAlienShotHitPlayer()
            {
                /*
                  Created 06/10/2025 By Roger Williams

                  checks alien shot directly touching the player

                */

                int intNum = 0;
                bool blnHit = false;

                //is alien shot top + height /2 >= player top?
                if (this.PICAlienShot.Top + (Modules.clsView.CNST_INT_ALIENBULLET_HEIGHT / 2) >= this.PICPlayer.Top + 4)
                {
                    //is alien shot slight to the left of the player?
                    if (this.PICAlienShot.Left < this.PICPlayer.Left)
                    {
                        //if the alien shot left + width <= player left + width
                        //                        if (this.PICPlayer.Left + Modules.clsView.CNST_INT_PLAYER_WIDTH <= this.PICAlienShot.Left + Modules.clsView.CNST_INT_ALIENBULLET_WIDTH + 10)
                        if (Modules.clsModel.InRange(this.PICAlienShot.Left, this.PICAlienShot.Left + Modules.clsView.CNST_INT_ALIENBULLET_WIDTH, this.PICPlayer.Left))
                        {
                            blnHit = true;
                        }
                    }
                    //is alien shot left greater or equal than left of the player?
                    if (this.PICAlienShot.Left >= this.PICPlayer.Left)
                    {
                        //if alien shot inside player left to player left + width range
                        if (Modules.clsModel.InRange(this.PICPlayer.Left, this.PICPlayer.Left + Modules.clsView.CNST_INT_PLAYER_WIDTH, this.PICAlienShot.Left))
                        {
                            blnHit = true;
                        }
                    }
                }

                return blnHit;
            }


            bool CheckIfPlayerShotHitMotherShip()
            {
                /*
                  Created 06/10/2025 By Roger Williams

                  checks player shot directly touching the alien mothership

                */

                int intNum = 0;
                bool blnHit = false;

                //is player shot top  -4 >= alien mothership top + height - 4?
                if (this.PICPlayerShot.Top - 4 <= this.PICAlienMotherShip.Top + Modules.clsView.CNST_INT_ALIENMOTHERSHIP_HEIGHT - 4)
                {
                    if (this.PICPlayerShot.Left >= this.PICAlienMotherShip.Left && this.PICPlayerShot.Left + Modules.clsView.CNST_INT_PLAYERBULLET_WIDTH < this.PICAlienMotherShip.Left + +Modules.clsView.CNST_INT_ALIENMOTHERSHIP_WIDTH)
                    {
                        blnHit = true;
                        clsActivePlayer.RemoveShot();
                    }
                }

                return blnHit;
            }
            //*********end sub functions/procedures********

            try
            {

                while (!blnAsyncCollision)
                {
                    await Task.Delay(Modules.clsView.CNST_INT_COLLISION_INTERVAL);

                    this.LBLTest.Text = DateTime.Now.ToString();
                    this.LBLTest.Update();

                    //check if player shot alien
                    if (this.PICPlayerShot.Visible)
                    {
                        //check for player shot -> alien hit
                        GetNearestAlienToPlayerShot();

                        if (intAlienHit != 0)
                        {
                            clsActivePlayer.RemoveShot();
                            clsActiveAlienWave.AlienHit(intAlienHit);

                            if (intAlienHitType == 1)
                            {
                                clsActivePlayer.intScore += Modules.clsModel.CNST_INT_ALIEN1_SCORE;
                            }

                            if (intAlienHitType == 2)
                            {
                                //check if all health removed as type 2 needs TWO shots to destroy
                                if (clsActiveAlienWave.AlienHealth(intAlienHit) == 0)
                                {
                                    clsActivePlayer.intScore += Modules.clsModel.CNST_INT_ALIEN2_SCORE;
                                }
                            }

                            //update player score
                            UpdateStatus();
                           
                            //all aliens destroyed?
                            if ((clsActiveAlienWave.intAliensRow1 == 0) && (clsActiveAlienWave.intAliensRow2 == 0))
                            {
                                PlayerCompletedLevel();
                            }
                        }

                        if (this.PICAlienMotherShip.Visible)
                        {
                            if (!clsActiveAlienMothership.blnHit)
                            {
                                if (CheckIfPlayerShotHitMotherShip())
                                { 
                                    clsActivePlayer.RemoveShot();
                                    clsActiveAlienMothership.MothershipHit();

                                    //if player does not have max lives add one
                                    if (clsActivePlayer.intLives != Modules.clsModel.CNST_INT_MAXLIVES)
                                    {
                                        clsActivePlayer.intLives++;
                                    }
                                    else
                                    {
                                        //if already at max lives add a big score instead!
                                        clsActivePlayer.intScore += Modules.clsModel.CNST_INT_ALIENMOTHERSHIP_SCORE;
                                    }

                                    UpdateStatus();
                                }
                            }
                        }
                    }

                    //check alienwave hit base
                    if (CheckIfAlienHitPlayerBase())
                    { 
                        if (!blnInvasion)
                        {
                            ShowAlienInvasion();
                        }
                    }

                    if (this.PICAlienShot.Visible)
                    {
                        if (!clsActiveAlienShot.blnExploding)
                        {
                            //check alien shot hit base
                            if (CheckIfAlienShotHitBase())
                            {
                                //explode and remove alien shot
                                clsActiveAlienShot.ExplodeShot();
                                clsActiveBaseAll.BaseUnitHit(intBaseUnitHitSide, intBaseUnitHit);

                                if (clsActiveBaseAll.BaseDestroyed())
                                { 
                                    blnAsyncCollision = true;
                                    blnPlayerLost = true;
                                    blnPlayerLoses = true;
                                    clsActiveAlienWave.StopMoveSound();
                                    StopAllTimers();
                                    //play base destroyed sound
                                    SNDBaseDestroyed.Position = Modules.clsModel.TMSStart;
                                    SNDBaseDestroyed.Play();
                                    this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_BLANKIMAGE;
                                    this.PICGameEnd.Load();
                                    this.PICGameEnd.BringToFront();
                                    this.PICGameEnd.Visible = true;
                                    BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
                                    BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_GAMEOVER);
                                    enumScreenChosen = enumScreens.scrGameOver;
                                    blnAsyncDither = false;
                                    AsyncDither();
                                }
                            }


                            //check alien shot hit player
                            if (CheckIfAlienShotHitPlayer())
                            {
                                if (!(clsActivePlayer.blnHit))
                                {
                                    //explode and remove alien shot
                                    clsActiveAlienShot.ExplodeShot();
                                    //process player hit - also run public sub here: resetplayer
                                    clsActivePlayer.PlayerHit(blnInvasion);

                                    while (clsActivePlayer.blnHit)
                                    {
                                         Application.DoEvents(); 
                                    }

                                    clsActivePlayer.ResetPlayer();
                                    //update lives
                                    UpdateStatus();
                                    //player not moving to next level!
                                    blnCanProgress = false;

                                    if (clsActivePlayer.intLives == 0)
                                    {
                                        GameOver();
                                    }

                                }         
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        //private void TMRCollision_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 29/09/2025 By Roger Williams

        //      checks for any collisons, process player life loss

        //      checks for collisions:

        //      - player bullet own base
        //      - player bullet alien
        //      - player bullet alien mothership
        //      - alien bullet base
        //      - alien bullet player
        //      - alien hit base (gaem over baby!)

        //    */
        //    int intAlienHit = 0;
        //    int intAlienHitType = 0;
        //    int intBaseUnitHitSide = 0;
        //    int intBaseUnitHit = 0;


        //    //*******sub functions/procedures****

        //    void GetNearestAlienToPlayerShot()
        //    {
        //        /*
        //          Created 05/10/2025 By Roger Williams

        //          finds the FIRST alien directly above the players shot (if any)

        //        */

        //        int intNum = 0;
        //        int intWhat = 0;


        //        await Task.Delay(Modules.clsView.CNST_INT_COLLISION_INTERVAL);

        //        for (intNum = 1; intNum != 7; intNum++)
        //        {
        //            //only process VISIBLE aliens
        //            if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Visible)
        //            {
        //                //see if playershot left between alien on row 2 left and left + width
        //                if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Left + Modules.clsView.CNST_INT_ALIEN_WIDTH, this.PICPlayerShot.Left))
        //                {
        //                    //make sure playershot is DIRECTLY below the alien
        //                    //    if (this.Controls[Modules.clsModel.CNST_STR_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT / 2) + 10 >= this.PICPlayerShot.Top - 10)
        //                    if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT / 2), this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT + 10), this.PICPlayerShot.Top))
        //                    {
        //                        intAlienHit = 6 + intNum; //add 6 as 12 aliens in wave and this is SECOND row
        //                        intAlienHitType = 2;
        //                        break;
        //                    }
        //                }
        //            }
        //        }


        //        if (intAlienHit == 0)
        //        {
        //            for (intNum = 1; intNum != 7; intNum++)
        //            {
        //                //only process VISIBLE aliens
        //                if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Visible)
        //                {
        //                    //see if playershot left between alien on row 1 left and left + width
        //                    if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Left + Modules.clsView.CNST_INT_ALIEN_WIDTH, this.PICPlayerShot.Left))
        //                    {
        //                        //make sure playershot is DIRECTLY below the alien
        //                        //    if (this.Controls[Modules.clsModel.CNST_STR_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT / 2) + 10 >= this.PICPlayerShot.Top - 10)
        //                        if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT / 2), this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Top + (Modules.clsView.CNST_INT_ALIEN_HEIGHT + 10), this.PICPlayerShot.Top))
        //                        {
        //                            intAlienHit = intNum; //add 6 as 12 aliens in wave and this is SECOND row
        //                            intAlienHitType = 1;
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    bool CheckIfAlienHitPlayerBase()
        //    {
        //        /*
        //          Created 06/10/2025 By Roger Williams

        //          checks if any alien directly touching the players base

        //        */

        //        int intNum = 0;
        //        bool blnHit = false;

        //        for (intNum = 1; intNum != 7; intNum++)
        //        {
        //            //only process VISIBLE aliens
        //            if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Visible)
        //            {
        //                //see if alien top+height >= baseunit top +4
        //                if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + Modules.clsView.CNST_INT_ALIEN_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Top + 4)
        //                {
        //                    blnHit = true;
        //                    break;
        //                }
        //                //see if alien top+height >= baseunit top +4
        //                if (this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top + Modules.clsView.CNST_INT_ALIEN_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Top + 4)
        //                {
        //                    blnHit = true;
        //                    break;
        //                }
        //            }
        //        }

        //        return blnHit;
        //    }

        //    bool CheckIfAlienShotHitBase()
        //    {
        //        /*
        //          Created 06/10/2025 By Roger Williams

        //          checks if alien shot directly touching the players base

        //        */

        //        int intNum = 0;
        //        int intWhat = 0;
        //        bool blnBaseHit = false;

        //        intBaseUnitHitSide = 0;
        //        intBaseUnitHit = 0;

        //        for (intNum = 1; intNum != 7; intNum++)
        //        {
        //            if (this.PICAlienShot.Top + Modules.clsView.CNST_INT_ALIENBULLET_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Top - 10)
        //            {
        //                if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left + Modules.clsView.CNST_INT_BASE_WIDTH, this.PICAlienShot.Left))
        //                {
        //                    intBaseUnitHitSide = 1;
        //                    intBaseUnitHit = intNum;
        //                    blnBaseHit = true;
        //                    break;
        //                }
        //            }

        //            if (this.PICAlienShot.Top + Modules.clsView.CNST_INT_ALIENBULLET_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Top - 10)
        //            {
        //                //check if left of alien shot < baseunit AND baseunit left INSIDE range alien shot left -> alien shot left + width
        //                if (this.PICAlienShot.Left < this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left)
        //                {
        //                    if (Modules.clsModel.InRange(this.PICAlienShot.Left, this.PICAlienShot.Left + Modules.clsView.CNST_INT_ALIENBULLET_WIDTH, this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left))
        //                    {
        //                        intBaseUnitHitSide = 1;
        //                        intBaseUnitHit = intNum;
        //                        blnBaseHit = true;
        //                        break;
        //                    }
        //                }
        //            }

        //            if (this.PICAlienShot.Top + Modules.clsView.CNST_INT_ALIENBULLET_HEIGHT >= this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Top + 4)
        //            {
        //                if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Left + Modules.clsView.CNST_INT_BASE_WIDTH, this.PICAlienShot.Left))
        //                {
        //                    intBaseUnitHitSide = 2;
        //                    intBaseUnitHit = intNum;
        //                    blnBaseHit = true;
        //                    break;
        //                }
        //            }
        //        }

        //        return blnBaseHit;
        //    }


        //    bool CheckIfAlienShotHitPlayer()
        //    {
        //        /*
        //          Created 06/10/2025 By Roger Williams

        //          checks alien shot directly touching the player

        //        */

        //        int intNum = 0;
        //        bool blnHit = false;

        //        //is alien shot top + height /2 >= player top?
        //        if (this.PICAlienShot.Top + (Modules.clsView.CNST_INT_ALIENBULLET_HEIGHT / 2) >= this.PICPlayer.Top + 4)
        //        {
        //            //is alien shot slight to the left of the player?
        //            if (this.PICAlienShot.Left < this.PICPlayer.Left)
        //            {
        //                //if the alien shot left + width <= player left + width
        //                //                        if (this.PICPlayer.Left + Modules.clsView.CNST_INT_PLAYER_WIDTH <= this.PICAlienShot.Left + Modules.clsView.CNST_INT_ALIENBULLET_WIDTH + 10)
        //                if (Modules.clsModel.InRange(this.PICAlienShot.Left, this.PICAlienShot.Left + Modules.clsView.CNST_INT_ALIENBULLET_WIDTH, this.PICPlayer.Left))
        //                {
        //                    blnHit = true;
        //                }
        //            }
        //            //is alien shot left greater or equal than left of the player?
        //            if (this.PICAlienShot.Left >= this.PICPlayer.Left)
        //            {
        //                //if alien shot inside player left to player left + width range
        //                if (Modules.clsModel.InRange(this.PICPlayer.Left, this.PICPlayer.Left + Modules.clsView.CNST_INT_PLAYER_WIDTH, this.PICAlienShot.Left))
        //                {
        //                    blnHit = true;
        //                }
        //            }
        //        }

        //        return blnHit;
        //    }


        //    bool CheckIfPlayerShotHitMotherShip()
        //    {
        //        /*
        //          Created 06/10/2025 By Roger Williams

        //          checks player shot directly touching the alien mothership

        //        */

        //        int intNum = 0;
        //        bool blnHit = false;

        //        //is player shot top  -4 >= alien mothership top + height - 4?
        //        if (this.PICPlayerShot.Top - 4 <= this.PICAlienMotherShip.Top + Modules.clsView.CNST_INT_ALIENMOTHERSHIP_HEIGHT - 4)
        //        {
        //            if (this.PICPlayerShot.Left >= this.PICAlienMotherShip.Left && this.PICPlayerShot.Left + Modules.clsView.CNST_INT_PLAYERBULLET_WIDTH < this.PICAlienMotherShip.Left + +Modules.clsView.CNST_INT_ALIENMOTHERSHIP_WIDTH)
        //            {
        //                blnHit = true;
        //            }
        //        }

        //        return blnHit;
        //    }
        //    //*********end sub functions/procedures********




        //    //check if player shot alien
        //    if (this.PICPlayerShot.Visible)
        //    {
        //        //check for player shot -> alien hit
        //        GetNearestAlienToPlayerShot();

        //        if (intAlienHit != 0)
        //        {
        //            clsActivePlayer.RemoveShot();
        //            clsActiveAlienWave.AlienHit(intAlienHit);

        //            if (intAlienHitType == 1)
        //            {
        //                clsActivePlayer.intScore += Modules.clsModel.CNST_INT_ALIEN1_SCORE;
        //            }

        //            if (intAlienHitType == 2)
        //            {
        //                //check if all health removed as type 2 needs TWO shots to destroy
        //                if (clsActiveAlienWave.AlienHealth(intAlienHit) == 0)
        //                {
        //                    clsActivePlayer.intScore += Modules.clsModel.CNST_INT_ALIEN2_SCORE;
        //                }
        //            }

        //            //update player score
        //            UpdateStatus();

        //            //all aliens destroyed?
        //            if ((clsActiveAlienWave.intAliensRow1 == 0) && (clsActiveAlienWave.intAliensRow2 == 0))
        //            {
        //                TMRCollision.Enabled = false;
        //                this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_BLANKIMAGE;
        //                this.PICGameEnd.Load();
        //                this.PICGameEnd.BringToFront();
        //                this.PICGameEnd.Visible = true;
        //                BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
        //                BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_COMPLETEDGAME);              
        //                enumScreenChosen = enumScreens.scrCompleted;
        //                //     TMRDitherImage.Enabled = true;
        //                //                        PlayerCompletedLevel();
        //                blnAsyncDither = false;
        //                AsyncDither();
        //            }
        //        }

        //        if (this.PICAlienMotherShip.Visible)
        //        {
        //            if (!clsActiveAlienMothership.blnHit)
        //            {
        //                if (CheckIfPlayerShotHitMotherShip())
        //                {
        //                    clsActiveAlienMothership.MothershipHit();

        //                    //if player does not have max lives add one
        //                    if (clsActivePlayer.intLives != Modules.clsModel.CNST_INT_MAXLIVES)
        //                    {
        //                        clsActivePlayer.intLives++;
        //                    }
        //                    else
        //                    {
        //                        //if already at max lives add a big score instead!
        //                        clsActivePlayer.intScore += Modules.clsModel.CNST_INT_ALIENMOTHERSHIP_SCORE;
        //                    }

        //                    UpdateStatus();
        //                }
        //            }
        //        }
        //    }

        //    //check alienwave hit base
        //    if (CheckIfAlienHitPlayerBase())
        //    { 
        //        blnPlayerLost = true;
        //        blnCanProgress = false;
        //        ShowAlienInvasion();
        //        clsActivePlayer.PlayerHit();

        //        if (clsActivePlayer.intLives == 0)
        //        {
        //            TMRCollision.Enabled = false;
        //            //GameOver();
        //            this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_BLANKIMAGE;
        //            this.PICGameEnd.Load();
        //            this.PICGameEnd.BringToFront();
        //            this.PICGameEnd.Visible = true;
        //            BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
        //            BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_GAMEOVER);
        //            enumScreenChosen = enumScreens.scrGameOver;
        //            //    TMRDitherImage.Enabled = true;
        //            blnAsyncDither = false;
        //            AsyncDither();
        //        }
        //        else
        //        {
        //            Init();
        //        }
        //    }

        //    if (this.PICAlienShot.Visible)
        //    {
        //        if (!clsActiveAlienShot.blnExploding)
        //        {
        //            //check alien shot hit base
        //            if (CheckIfAlienShotHitBase())
        //            {
        //                //explode and remove alien shot
        //                clsActiveAlienShot.ExplodeShot();
        //                clsActiveBaseAll.BaseUnitHit(intBaseUnitHitSide, intBaseUnitHit);

        //                if (clsActiveBaseAll.BaseDestroyed())
        //                { 
        //                    StopAllTimers();
        //                    //play base destroyed sound
        //                    SNDBaseDestroyed.Position = Modules.clsModel.TMSStart;
        //                    SNDBaseDestroyed.Play();
        //                    this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_BLANKIMAGE;
        //                    this.PICGameEnd.Load();
        //                    this.PICGameEnd.BringToFront();
        //                    this.PICGameEnd.Visible = true;
        //                    BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
        //                    BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_GAMEOVER);
        //                    enumScreenChosen = enumScreens.scrGameOver;
        //                    //        TMRDitherImage.Enabled = true;
        //                    blnAsyncDither = false;
        //                    AsyncDither();
        //                    //                            GameOver();
        //                }
        //            }


        //            //check alien shot hit player
        //            if (CheckIfAlienShotHitPlayer())
        //            {
        //                if (!(clsActivePlayer.blnHit))
        //                { 
        //                    //explode and remove alien shot
        //                    clsActiveAlienShot.ExplodeShot();
        //                    //process player hit - also run public sub here: resetplayer
        //                    clsActivePlayer.PlayerHit();
        //                    //update lives
        //                    UpdateStatus();
        //                    //player not moving to next level!
        //                    blnCanProgress = false;

        //                    if (clsActivePlayer.intLives == 0)
        //                    {
        //                        blnPlayerLoses = true;
        //                        GameOver();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //***end timers********

        private void StopAllTimers()
        {
            /*
               Modified 06/11/2025 By Roger Williams

               converted timers to async subs so just need to reset bln vars!

            
               Created 05/11/2025 By Roger Williams

               stops all timers called by gameover formclose etc

            */
            //stop ALL timers
            //      TMRAlienShot.Enabled = false;
            //      TMRCollision.Enabled = false;
            //      TMRCompletdGame.Enabled = false;
            //      TMRDitherImage.Enabled = false;
            //      TMRFadeMusic.Enabled = false;
            //     TMRGameOver.Enabled = false;
            //TMRPlayAgain.Enabled = false;
            //TMRYouLose.Enabled = false;
            //TMRYouWin.Enabled = false;
            blnAsyncCollision = true;
            blnAsyncAlienShot = true;
            blnAsyncCompletedGame = true;
            blnAsyncDither = true;
            blnAsyncFadeMusic = true;
            blnAsyncGameOver = true;
            blnAsyncPlayAgain = true;
            blnAsyncYouLose = true;
            blnAsyncYouWin = true;
        }
        private void ShowAlienInvasion()
        {
            /*
              Created 10/10/2025 By Roger Williams

              called when alien hits base

              - fade main music
              - stop wave movement timer
              - stop alien shot timer
              - stop alien mothership timer
              - destroy all baseunits
              - starts timer that moves wave up off screen
              - starts timer that moves mothership into "beam" position
              - starts timer to show beam
              - starts timer to show invaders
              - destroys player ship and dec lives

            */

            int intBeamAni = 1;
            int intInvadersAni = 1;

            System.Windows.Forms.Timer TMRMoveWaveOffScreen = new System.Windows.Forms.Timer();
            System.Windows.Forms.Timer TMRMoveMothership = new System.Windows.Forms.Timer();
            System.Windows.Forms.Timer TMRBeam = new System.Windows.Forms.Timer();
            System.Windows.Forms.Timer TMRMoveInvaders = new System.Windows.Forms.Timer();
            System.Windows.Forms.Timer TMRAnimateInvaders = new System.Windows.Forms.Timer();
            System.Windows.Forms.Timer TMRDestroyBase = new System.Windows.Forms.Timer();

            MediaPlayer SNDBeam = new MediaPlayer();
            MediaPlayer SNDInvasion = new MediaPlayer();

            //********local timer events*********

            void TMRBeam_Tick(object sender, EventArgs e)
            {
                /*
                   Created 10/10/2025 By Roger Williams

                   animates beams and plays sound

                        changes certain colours in beam for others depending on "animation" number

                        colours changed:

                        beam1: r: 20  g: 139  b: 7
                        beam2: r: 97  g: 209  b: 255
                        beam3: r: 147 g: 219  b: 139
                        beam4: r: 202 g: 220  b: 200

                */

                    int intNum = 0;
                    int intNum3 = 0;


                //play hit sound
                SNDBeam.Position = Modules.clsModel.TMSStart;
                SNDBeam.Play();

                    for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
                    {
                        for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                        {
                            //get pixel colour
                            CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);

                            /*
                                use this for multiple colours

                                Note: works thus: 

                                - count 1: if colour 1 set to colour 2
                                - count 2: if colour 2 set to colour 3
                                - count 3: if colour 3 set to colour 4
                                - count 4: if colour 4 set to colour 1
                            */

                            switch (intCount)
                            {
                                case 1:
                                    //change beam1 colour - original: beam1: r: 20  g: 139  b: 7
                                    if (CLRTemp == System.Drawing.Color.FromArgb(20, 139, 7))
                                    {
                                        //set colour 2  beam2: r: 97  g: 209  b: 255
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(97, 209, 255));
                                    }
                                    else
                                    { //if already changed change back
                                        if (CLRTemp == System.Drawing.Color.FromArgb(97, 209, 255))
                                        {
                                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(20, 139, 7));
                                        }
                                        else
                                        {
                                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                        }
                                    }
                                    break;
                                case 2:
                                    //change beam2 colour - original:   beam2: r: 97  g: 209  b: 255
                                    if (CLRTemp == System.Drawing.Color.FromArgb(97, 209, 255))
                                    {
                                        //set colour 3   beam3: r: 147 g: 219  b: 139
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(147, 219, 139));
                                    }
                                    else
                                    { //if already changed change back
                                        if (CLRTemp == System.Drawing.Color.FromArgb(147, 219, 139))
                                        {
                                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(97, 209, 255));
                                        }
                                        else
                                        {
                                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                        }
                                    }

                                    break;
                                case 3:
                                    //change beam3 colour - original: beam3: r: 147 g: 219  b: 139
                                    if (CLRTemp == System.Drawing.Color.FromArgb(147, 219, 139))
                                    {
                                        //set colour 4   beam4: r: 202 g: 220  b: 200
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(202, 220, 200));
                                    }
                                    else
                                    { //if already changed change back
                                        if (CLRTemp == System.Drawing.Color.FromArgb(202, 220, 200))
                                        {
                                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(147, 219, 139));
                                        }
                                        else
                                        {
                                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                        }
                                    }

                                    break;
                                case 4:
                                    //change beam4 colour - original: beam4: r: 202 g: 220  b: 200
                                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 204, 0))
                                    {
                                        //set colour 1   beam1: r: 20  g: 139  b: 7
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(20, 139, 7));
                                    }
                                    else
                                    { //if already changed change back
                                        if (CLRTemp == System.Drawing.Color.FromArgb(20, 139, 7))
                                        {
                                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(202, 220, 200));
                                        }
                                        else
                                        {
                                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                        }
                                    }
                                    break;
                            }
                        }
                    }

                    this.PICBeam.Image = BMPTemp1;
                    this.PICBeam.Invalidate();

                intBeamAni++;

                if (intBeamAni > 4)
                {
                    intBeamAni = 1;
                }
            }

            void TMRMoveInvaders_Tick(object sender, EventArgs e)
            {
                /*
                   Created 10/10/2025 By Roger Williams

                   move alien invaders too left to players ship

                */
                double dblNum = 1;

                //if last alien invader moved stop beam 
                if (this.PICAlienInvader_4.Left != this.PICBeam.Left)
                {
                    TMRBeam.Enabled = false;
                    this.PICBeam.Visible = false;
                }

                //move aliens
                if (this.PICAlienInvader_1.Left <= this.PICPlayer.Left + this.PICPlayer.Width + 20)
                {
                    //first alien invader reached player so stop ALL timers
                    TMRMoveInvaders.Enabled = false;
                    TMRAnimateInvaders.Enabled = false;
                    TMRMoveMothership.Enabled = false;
                    TMRMoveWaveOffScreen.Enabled = false;
                    TMRBeam.Enabled = false;
                    //stop beam sound
                    SNDBeam.Stop();
                    //hide all used images
                    this.PICAlienMotherShip.Visible = false;
                    this.PICAlienInvader_1.Visible = false;
                    this.PICAlienInvader_2.Visible = false;
                    this.PICAlienInvader_3.Visible = false;
                    this.PICAlienInvader_4.Visible = false;


                    this.LBLTest2.Text += " stopped: " + DateTime.Now.ToString();
                    this.LBLTest2.Update();


                    //stop mothership animation and sound
                    clsActiveAlienMothership.StopAnimationAndSound();
                    clsActivePlayer.PlayerHit(blnInvasion);

                    while (clsActivePlayer.blnHit)
                    {
                        Application.DoEvents();
                    }

                    //fade music manually?
                    while (SNDInvasion.Volume != 0)
                    {
                        SNDInvasion.Volume -= 0.01;

                        if (SNDInvasion.Volume == 0)
                        {
                            SNDInvasion.Stop();
                        }
                    }

                    //player not moving to next level!
                    blnCanProgress = false;
                    //@@
                    if (clsActivePlayer.intLives == 0)
                    {
                        blnAsyncCollision = true;
                        //GameOver();
                        this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_BLANKIMAGE;
                        this.PICGameEnd.Load();
                        this.PICGameEnd.BringToFront();
                        this.PICGameEnd.Visible = true;
                        BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
                        BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_GAMEOVER);
                        enumScreenChosen = enumScreens.scrGameOver;
                        //    TMRDitherImage.Enabled = true;
                        blnAsyncDither = false;
                        AsyncDither();
                    }
                    else
                    {
                        Init();
                    }
                }
                else
                {
                    this.PICAlienInvader_1.Left = this.PICAlienInvader_1.Left - (this.PICAlienInvader_1.Width + 12);

                    if (this.PICAlienInvader_1.Left < this.PICAlienInvader_2.Left - (this.PICAlienInvader_1.Width + 24))
                    {
                        this.PICAlienInvader_2.Left = this.PICAlienInvader_2.Left - (this.PICAlienInvader_1.Width + 12);
                    }

                    if (this.PICAlienInvader_2.Left < this.PICAlienInvader_3.Left - (this.PICAlienInvader_1.Width + 24))
                    {
                        this.PICAlienInvader_3.Left = this.PICAlienInvader_3.Left - (this.PICAlienInvader_1.Width + 12);
                    }

                    if (this.PICAlienInvader_3.Left < this.PICAlienInvader_4.Left - (this.PICAlienInvader_1.Width + 24))
                    {
                        this.PICAlienInvader_4.Left = this.PICAlienInvader_4.Left - (this.PICAlienInvader_1.Width + 12);
                    }
                }
            }

            void TMRAnimateInvaders_Tick(object sender, EventArgs e)
            {
                /*
                   Created 10/10/2025 By Roger Williams
                                
                   4 invaders each starts with a different animation

                 */

                switch (intInvadersAni)
                {
                    case 1:
                        this.PICAlienInvader_1.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_1;
                        this.PICAlienInvader_2.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_2;
                        this.PICAlienInvader_3.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_3;
                        break;
                    case 2:
                        this.PICAlienInvader_1.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_2;
                        this.PICAlienInvader_2.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_3;
                        this.PICAlienInvader_3.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_1;
                        this.PICAlienInvader_4.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_2;
                        break;
                    case 3:
                        this.PICAlienInvader_1.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_3;
                        this.PICAlienInvader_2.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_1;
                        this.PICAlienInvader_3.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_2;
                        this.PICAlienInvader_4.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_INVADER_3;
                        break;
                }

                intInvadersAni++;

                if (intInvadersAni > 3)
                {
                    intInvadersAni = 1;
                }
            }


            void TMRMoveAlienMothership_Tick(object sender, EventArgs e)
            {
                /*
                   Created 10/10/2025 By Roger Williams

                   - moves alien mothership into position
                   - shows beam
                   - shows invaders
                   - move invaders too players ship
                */


                //move wave down
                if (this.PICAlienMotherShip.Top + Modules.clsView.CNST_INT_ALIENMOTHERSHIP_HEIGHT < this.Height - 160)
                {
                    if (this.PICAlienMotherShip.Top < 300 && this.PICAlienMotherShip.Left == 180)
                    {
                        this.PICAlienMotherShip.Top += 20;
                    }
                    else
                    {
                        //aliens reached right hand edge?
                        if (this.PICAlienMotherShip.Left + Modules.clsView.CNST_INT_ALIENMOTHERSHIP_WIDTH < 1000)
                        {
                            //if moved to 500 pixels from top move right to edge of screen
                            this.PICAlienMotherShip.Left += 20;
                        }
                        else
                        {
                            //move into "beam" position
                            if (this.PICAlienMotherShip.Top + Modules.clsView.CNST_INT_ALIENMOTHERSHIP_HEIGHT <= this.Height - 160)
                            {
                                this.PICAlienMotherShip.Top += 20;
                            }
                        }
                    }
                }

                //draw beam
                if (this.PICAlienMotherShip.Top + Modules.clsView.CNST_INT_ALIENMOTHERSHIP_HEIGHT >= this.Height - 180)
                {
                    TMRMoveMothership.Enabled = false;
                    //stop alien mothership sound
                    clsActiveAlienMothership.StopSound();
                    //remove alien shot
                    clsActiveAlienShot.RemoveShot();
                    //init beam
                    BMPTemp1 = new Bitmap(this.PICBeam.Image);
                    intCount = 1;
                    SNDBeam.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_BEAM));
                    SNDBeam.Volume = 1;
                    TMRBeam.Interval = 400; // 800;
                    TMRBeam.Tick += TMRBeam_Tick;
                    TMRBeam.Enabled = true;
                    this.PICBeam.Left = this.PICAlienMotherShip.Left + 28;
                    this.PICBeam.Top = this.PICAlienMotherShip.Top + Modules.clsView.CNST_INT_ALIENMOTHERSHIP_HEIGHT;
                    this.PICBeam.Visible = true;

                    //invade!                   
                    this.PICBeam.SendToBack();
                    //position invaders together under beam
                    this.PICAlienInvader_1.Left = this.PICBeam.Left;
                    this.PICAlienInvader_2.Left = this.PICBeam.Left;
                    this.PICAlienInvader_3.Left = this.PICBeam.Left;
                    this.PICAlienInvader_4.Left = this.PICBeam.Left;
                    //show invaders
                    this.PICAlienInvader_1.Visible = true;
                    this.PICAlienInvader_2.Visible = true;
                    this.PICAlienInvader_3.Visible = true;
                    this.PICAlienInvader_4.Visible = true;

                    TMRAnimateInvaders.Tick += TMRAnimateInvaders_Tick;
                    TMRAnimateInvaders.Interval = 800;
                    TMRAnimateInvaders.Enabled = true;

                    TMRMoveInvaders.Tick += TMRMoveInvaders_Tick;
                    TMRMoveInvaders.Interval = 800;
                    TMRMoveInvaders.Enabled = true;
                }
            }

            void TMRMoveWaveOffScreen_Tick(object sender, EventArgs e)
            {
                /*
                   Created 10/10/2025 By Roger Williams

                   moves existing alienwave off screen   

                */


                int intNum = 0;

                //play move sound
                clsActiveAlienWave.PlayMoveSound();

                if (this.PICAlien2_1.Top + Modules.clsView.CNST_INT_ALIEN_HEIGHT - 10 >= 0)
                {
                    for (intNum = 1; intNum != 7; intNum++)
                    {
                        this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum].Top += -10;
                        this.Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum].Top += -10;
                    }
                }
                else
                {
                    //stop if off screen
                    TMRMoveWaveOffScreen.Enabled = false;
                    //stop move sound
                    clsActiveAlienWave.StopMoveSound();
                    //position alien mothership
                    this.PICAlienMotherShip.Top = -100;
                    //set left pos
                    this.PICAlienMotherShip.Left = 180;
                    this.PICAlienMotherShip.Visible = true;
                    this.PICAlienMotherShip.BringToFront();

                    //move alien mothership into position and start sound and animation
                    clsActiveAlienMothership.StartAnimationAndSound();
                    TMRMoveMothership.Interval = 700;
                    TMRMoveMothership.Tick += TMRMoveAlienMothership_Tick;
                    TMRMoveMothership.Enabled = true;
                }
            }


            void TMRDestroyBase_Tick(object sender, EventArgs e)
            {
                /*
                   Created 10/10/2025 By Roger Williams

                   destroys all existing base units with sound!

                */
                int intNum = 0;
                int intHit = 0;
                //isvisible                int - 1 = left 2 = right 3 = both 0 = none
                //BaseUnitHit

                //"hit" each baseunit (if visible)
                for (intNum = 1; intNum != 12; intNum++)
                {
                    intHit = clsActiveBaseAll.IsVisible(intNum);

                    if (intHit != 0)
                    {
                        if (intHit == 3)
                        {
                            clsActiveBaseAll.BaseUnitHit(1, intNum);
                            clsActiveBaseAll.BaseUnitHit(2, intNum);
                        }
                        else
                        {
                            clsActiveBaseAll.BaseUnitHit(intHit, intNum);
                        }
                    }
                }

                //if base destroyed continue with the invasion!
                if (clsActiveBaseAll.intBaseUnitsleft == 0)
                {
                    //stop timer
                    TMRDestroyBase.Enabled = false;
                    //play base destroyed sound
                    SNDBaseDestroyed.Position = Modules.clsModel.TMSStart;
                    SNDBaseDestroyed.Play();
                    //move alien wave off screen and play move sound
                    clsActiveAlienWave.PlayMoveSound();
                    TMRMoveWaveOffScreen.Interval = Modules.clsView.CNST_INT_ALIEN_MOVEOFFSCREEN_INTERVAL;
                    TMRMoveWaveOffScreen.Tick += TMRMoveWaveOffScreen_Tick;
                    TMRMoveWaveOffScreen.Enabled = true;
                }

            }

            void StopInvasionTimers()
            {
                TMRAnimateInvaders.Enabled = false;
                TMRBeam.Enabled = false;
                TMRDestroyBase.Enabled = false;
                TMRMoveInvaders.Enabled = false;
                TMRMoveMothership.Enabled = false;
                TMRMoveWaveOffScreen.Enabled = false;
            }
            //********end local sub/funcs**********

            //stop game timers
            StopAllTimers();
            //fade main music
            blnAsyncFadeMusic = false;
            AsyncFadeMusic();
            //freeze player
            blnPlayerLost = true;
            blnPlayerLoses = true;
            blnCanProgress = false;
            blnInvasion = true;

            clsActiveAlienWave.StopMoveSound();
            //start invasion music
            SNDInvasion.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_ALIEN_INVASION));
            SNDInvasion.Volume = 1;
            SNDInvasion.Play();
            //TMRFadeMusic.Enabled = true;
            //tell frmgame running this timer based routine
            
            //stop wave moving
            clsActiveAlienWave.StopWave();
            //stop alien shot timer
            blnAsyncAlienShot = true;
            //TMRAlienShot.Enabled = false;
            //stop alien mothership timer
            clsActiveAlienMothership.StopMoveShowTimer();

            //this.LBLTest2.Text = "started: " + DateTime.Now.ToString();
            //this.LBLTest2.Update();

            //stop invasion timers
            StopInvasionTimers();

            //destroy baseunits
            TMRDestroyBase.Interval = Modules.clsView.CNST_INT_DESTROYBASE_INTERVAL;
            TMRDestroyBase.Tick += TMRDestroyBase_Tick;
            TMRDestroyBase.Enabled = true;
        }


        //mediaplayer event
        private void Custom_MediaEnded(object sender, EventArgs e)
        {
            if (blnAsyncFadeMusic)
            {
                //repeat
                SNDMusic.Position = Modules.clsModel.TMSStart;
                SNDMusic.Play();
            }
        }

        private void UpdateStatus()
        /*
          Created 29/09/2025 By Roger Williams

          updates player score and lives remaining

          uses PICLives, duplicates for each life over 1 and draws in PANStatus

          public as a test 

        */
        {
            this.PICLives1.Visible = false;
            this.PICLives2.Visible = false;
            this.PICLives3.Visible = false;
            this.PICLives4.Visible = false;

            switch (clsActivePlayer.intLives)
            {
                case 1:
                    this.PICLives1.Visible = true;
                    break;
                case 2:
                    this.PICLives1.Visible = true;
                    this.PICLives2.Visible = true;
                    break;
                case 3:
                    this.PICLives1.Visible = true;
                    this.PICLives2.Visible = true;
                    this.PICLives3.Visible = true;
                    break;
                case 4:
                    this.PICLives1.Visible = true;
                    this.PICLives2.Visible = true;
                    this.PICLives3.Visible = true;
                    this.PICLives4.Visible = true;
                    break;
            }

            //update score
            this.LBLScore.Text = clsActivePlayer.intScore.ToString();
            this.LBLScore.Update();
            //update level
            this.LBLLevelNbr.Text = intCurLevel.ToString();
            this.LBLLevelNbr.Update();
        }

        private void DestroyClasses(bool blnSkipPlayer)
        {
            /*
              Created 17/11/2025 By Roger Williams

              "destroys" game classes 

              blnSkipPlayer = leave player class

            */


            //destroy main classes
            if (!blnSkipPlayer)
            { 
                clsActivePlayer.Destroy();
            }

            clsActiveAlienWave.Destroy();
            clsActiveAlienMothership.Destroy();
            clsActiveBaseAll.Destroy();
            clsActiveAlienShot.Destroy();
        }

        private void CreateClasses(bool blnReset)
        {
            /*
              Created 29/09/2025 By Roger Williams

              creates game classes 

              VARS

              blnreset  - true = new game    

            */

            if (blnReset) //reset player?
            {
                if (clsActivePlayer != null)
                {
                    DestroyClasses(false);
                }

                clsActivePlayer = new clsPlayer();
            }
            else
            {
                if (clsActiveAlienWave != null)
                { 
                    DestroyClasses(true); //skip player reset
                }
            }
                //destroy main classes
                //if (clsActiveAlienWave != null)
                //{ 
                //    clsActiveAlienWave.Destroy();
                //    clsActiveAlienMothership.Destroy();
                //    clsActiveBaseAll.Destroy();
                //}

            clsActiveAlienMothership = new clsAlienMothership();
            clsActiveBaseAll = new clsBaseAll();
            clsActiveAlienWave = new clsAlienWave(intCurLevel);
            clsActiveAlienShot = new clsAlienShot();
        }

        private void ExitGame()
        {
            //stop all timers
            StopAllTimers();
            //destroy main classes
            DestroyClasses(false);
            //stop music
            SNDMusic.Stop();
            SNDOther1.Stop();
            SNDOther2.Stop();  
            Application.Exit();
        }

        private void CreateTimers()
        {
            /*
              Modified 06/11/2025 By Roger Williams

              now uses async subs opposed to timers so just need to set the blnasyc<what> vars
              too true or false. 
            
              true is inactive, false = active
              
              
              Created 08/09/2025 By Roger Williams

              creates timers for game events

              IF called because player loses timers already exist so just restart them when necessary
              
            */


//            if (TMRCollision.Interval != Modules.clsView.CNST_INT_COLLISION_INTERVAL)
//            {
                //TMRCollision = new Timer();
                //TMRCollision.Interval = Modules.clsView.CNST_INT_COLLISION_INTERVAL;
                //TMRCollision.Tick += TMRCollision_Tick;
                //TMRCollision.Enabled = true;

                //TMRAlienShot = new Timer();
                //TMRAlienShot.Interval = Modules.clsView.CNST_INT_GAMECREATEALIENSHOT_INTERVAL;
                //TMRAlienShot.Tick += TMRAlienShot_Tick;
                //TMRAlienShot.Enabled = true;

                //TMRCompletdGame.Interval = 1000;
                //TMRCompletdGame.Tick += TMRCompletedGame_Tick;
                //TMRCompletdGame.Enabled = false;

        //        TMRFadeMusic.Interval = 700;
        //        TMRFadeMusic.Enabled = false;

         //       TMRGameOver.Interval = 1000;
                //TMRPlayAgain.Interval = 4000;
                //TMRYouLose.Interval = 4000;
                //TMRYouWin.Interval = 4000;

                //TMRDitherImage.Interval = 400; //700;
                //TMRDitherImage.Enabled = false;

         //       TMRYouLose.Tick += TMRYouLose_Tick;
         //       TMRGameOver.Tick += TMRGameOver_Tick;
                //TMRPlayAgain.Tick += TMRPlayAgain_Tick;
                //TMRYouWin.Tick += TMRYouWin_Tick;
         //       TMRFadeMusic.Tick += TMRFadeMusic_Tick;
         //       TMRDitherImage.Tick += TMRDitherImage_Tick;
            //}
            //else
            ////timers already configured so just restart what is necessary
            //{
            //    TMRCollision.Enabled = true;
            //    TMRAlienShot.Enabled = true;
            //}

            //reset timers?
            if (blnAsyncCollision == true)
            {
                //activate
                blnAsyncCollision = false;
                blnAsyncAlienShot = false;
                //start timers
                AsyncCollision();
                AsyncAlienShot();
                //deactivate
                blnAsyncCompletedGame = true;
                blnAsyncDither = true;
                blnAsyncFadeMusic = true;
                blnAsyncGameOver = true;
                blnAsyncPlayAgain = true;
                blnAsyncYouLose = true;
                blnAsyncYouWin = true;
             }
            else
            //collision timer already configured so just restart what is necessary
            {
                if (blnAsyncAlienShot)
                {
                    blnAsyncAlienShot = false;
                    AsyncAlienShot();
                }
            }
        }
        private void Init()
        {
            //stop timers
            StopAllTimers(); 
            //hide any gameover etc images
            this.PICGameEnd.Visible = false;
            this.PICBlankImage.Visible = false;
            //hide shot charge controls
            HideCharge();
            //reset game vars
            blnPlayerLost = false;
            blnPlayerLoses = false;
            blnPlayAgain = false;
            blnPlayerWins = false;
    
            //init animation colour var
            CLRTemp = System.Drawing.Color.Transparent;

            //if level already 1 inc
            //if (intCurLevel == 0)
            //{
            //    intCurLevel = 1;
            //    //create classes but don't change player status!
            //    CreateClasses(false);
            //}
            //else
            //{
            if (blnCanProgress) //user completed level?
            {
                intCurLevel++;

                //if player played all levels end game
                if (intCurLevel > Modules.clsModel.CNST_INT_MAXLEVELS)
                {
                    PlayerCompletedGame();
                    return;
                }
                else
                {
                    CreateClasses(false);
                    blnCanProgress = false;
                    clsActivePlayer.ResetPlayer();
                 //   PlayerCompletedLevel();
                }
            }
            else
            {
                //   intCurLevel++;
                if (!blnInvasion)
                { 
                    CreateClasses(true);
                }
                else
                {
                    CreateClasses(false);
                }

                clsActivePlayer.ResetPlayer();
            }
     
            blnInvasion = false;
            this.AllowTransparency = false;
            //show player score and lives
            UpdateStatus();
            this.PICGameEnd.Visible = false;
            //create timers
            CreateTimers();
            //show main game title graphic
            this.PICTitle.ImageLocation = Modules.clsModel.CNST_STR_IMG_MAINGAMETITLE;
            //reset player ship (just in case player lost life)
            //this.PICPlayer.ImageLocation = Modules.clsModel.CNST_STR_PLAYER_1;
            //this.PICPlayer.Visible = true;
            clsActivePlayer.InitImages();

            //load music 
            SNDMusic.MediaEnded -= Custom_MediaEnded;  //in case run second or more times
            SNDMusic.MediaEnded += Custom_MediaEnded;
            SNDMusic.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_MAINTHEME));


            if (File.Exists(Modules.clsModel.CNST_STR_SND_MAINTHEME))
            {
                SNDMusic.Volume = 0.3;
                SNDMusic.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_MAINTHEME));
                SNDMusic.Play();
            }
            else
            {
                MessageBox.Show("Error Reading Intro Music File. Please Check File Exists:\n\n" + Modules.clsModel.CNST_STR_SND_MAINTHEME, "Error Reading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //prepare base destroyed sound          
            if (File.Exists(Modules.clsModel.CNST_STR_SND_BASE_DESTROYED))
            {
                SNDBaseDestroyed.Volume = 1;
                SNDBaseDestroyed.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_BASE_DESTROYED));
            }
            else
            {
                MessageBox.Show("Error Reading Intro Music File. Please Check File Exists:\n\n" + Modules.clsModel.CNST_STR_SND_BASE_DESTROYED, "Error Reading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SNDOther1.Volume = 0.8;
            SNDOther2.Volume = 1;
        }

        private void PlayerLoses()
        {
            /*
              Created 19/10/2025 By Roger Williams

              shows play youlose then play again "screens"

              conditions:

              - if player simply lost a life run init
              - if player lost because aliens reached base goto playagain

            */

            //play youlose music
            SNDOther1.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_LOSE_1));
            SNDOther2.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_LOSE_2));
            SNDOther1.Play();
            SNDOther2.Play();
            //show "youlose"
            this.PICBlankImage.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_LOST;
            this.PICBlankImage.Load();
            this.PICBlankImage.BringToFront();
            this.PICBlankImage.Visible = true;
            BMPTemp1 = new Bitmap(this.PICBlankImage.Image);
            blnAsyncYouLose = false;
            AsyncYouLost();
            //TMRYouLose.Enabled = true;
        }

        private void GameOver()
        {
            /*
              Created 01/10/2025 By Roger Williams

              shows play youlose/gameover then play again "screens"
              stops ALL timers!

              gameover - when player has no lives left
              youlose  - when aliens reach base

            */

            //stop alienwave moving
            clsActiveAlienWave.StopWave();
            //clear it
            clsActiveAlienWave.Destroy();
            //stop mothership
            clsActiveAlienMothership.Destroy();
            //hide shots
            this.PICAlienShot.Visible = false;
            this.PICAlienMotherShip.Visible = false;
            blnPlayerLost = true; //stop player "action" commands
            blnPlayerLoses = true; //stop alien shot
            clsActiveAlienWave.StopMoveSound();
            StopAllTimers();

            //fade main music
            blnAsyncFadeMusic = false;
            AsyncFadeMusic();
            //TMRFadeMusic.Enabled = true;
            //define rectangle to "house" image
            GRATemp = this.CreateGraphics();
            RECTemp = new Rectangle(180, 210, this.PICGameEnd.Width +4, this.PICGameEnd.Height +4);

            //player lost all lives?
            if (clsActivePlayer.intLives == 0)
            {
                //play gameover music
                SNDOther1.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_GAMEOVER));
                SNDOther1.Play();
                this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_GAMEOVER;
                this.PICGameEnd.Load();
                this.PICGameEnd.BringToFront();
                this.PICGameEnd.Visible = true;
                BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
                //TMRGameOver.Enabled = true;
                //TMRGameOver.Start();

                //async test
                blnAsyncGameOver = false;
                AsyncGameOver();
            }
            else
            {
                if (!blnCanProgress)
                {
                    SNDOther1.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_LOSE_1));
                    SNDOther1.Play();
                    SNDOther2.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_LOSE_2));
                    SNDOther2.Play();
                    this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_LOST;
                    this.PICGameEnd.Load();
                    this.PICGameEnd.BringToFront();
                    this.PICGameEnd.Visible = true;
                    BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
                    BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_LOST);
                    enumScreenChosen = enumScreens.scrYouLost;
                    //      TMRDitherImage.Enabled = true;
                    blnAsyncDither = false;
                    AsyncDither();
                }
            }
        }

        private void PlayerCompletedGame()
        {
            /*
              Created 01/10/2025 By Roger Williams

              if player completes all level! 

              shows end screen then game over via TMRCompletedGame

            */

            blnPlayerLost = true; //stop player "action" commands
            blnPlayerLoses = true; //stop alien shot
            clsActiveAlienWave.StopMoveSound();
            clsActiveAlienShot.RemoveShot();
            StopAllTimers();
            DestroyClasses(true);

            //fade main music
            blnAsyncFadeMusic = false;
            AsyncFadeMusic();
            //TMRFadeMusic.Enabled = true;
            SNDOther1.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_WINS));
            SNDOther1.Play();
            SNDOther2.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_GAMECOMPLETED));
            SNDOther2.Play();
            this.PICGameEnd.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_COMPLETEDGAME;
            this.PICGameEnd.Load();
            this.PICGameEnd.BringToFront();
            this.PICGameEnd.Visible = true;
            BMPTemp1 = new Bitmap(this.PICGameEnd.Image);
            blnAsyncCompletedGame = false;
            AsyncCompletedGame();
            //TMRCompletdGame.Enabled = true;
        }

        private void PlayerCompletedLevel()
        {
            /*
              Created 02/10/2025 By Roger Williams

              if player completes level 

            */

            blnPlayerWins = true; //freeze player
            StopAllTimers();
            DestroyClasses(true);

            //stop player moving
            blnPlayerWins = true;
            blnCanProgress = true;

            if (clsActivePlayer.intLives < Modules.clsModel.CNST_INT_MAXLIVES)
            {
                //give player extra life as bonus
                clsActivePlayer.intLives++;
            }
            else
            {
                //if already has max lives give points bonus
                clsActivePlayer.intScore += 2000;
            }

            //fade main music
            blnAsyncFadeMusic = false;
            AsyncFadeMusic();
            //play youwin music
            SNDOther1.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_WINS));
            SNDOther1.Play();
            this.PICBlankImage.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_WINS;
            this.PICBlankImage.Load();
            this.PICBlankImage.BringToFront();
            this.PICBlankImage.Visible = true;
            BMPTemp1 = new Bitmap(this.PICBlankImage.Image);
            blnAsyncYouWin = false;
            AsyncYouWin();
        }
        private void PlayerHit()
        {
            /*
              Created 01/10/2025 By Roger Williams

              called when player hit decrements lives handles if zero

            */

            clsActivePlayer.PlayerHit(blnInvasion);

            //player dead?
            if (clsActivePlayer.intLives == 0)
            {
                clsActivePlayer.Destroy();
                GameOver();
            }
            else
            {
                UpdateStatus();
                clsActivePlayer.ResetPlayer();
            }
        }



        //*******form events*******
        private void frmGame_Paint(object sender, PaintEventArgs e)
        {
            /*
               Created 14/09/2025 By Roger Williams

               draws form with custom title bar!


             */

            Rectangle rctTemp = e.ClipRectangle;


            //draw fake title bar
            e.Graphics.FillRectangle(BRUTitleBar, 1, 2, this.Width - 2, Modules.clsView.CNST_INT_TITLEBARHEIGHT);

            //draw line underneath
            e.Graphics.DrawLine(penTemp, 0, Modules.clsView.CNST_INT_TITLEBARHEIGHT, this.Width, Modules.clsView.CNST_INT_TITLEBARHEIGHT);

            //using (System.Drawing.Brush brush = new SolidBrush(Modules.clsView.CNST_INT_TITLEBAR_TEXTCOLOUR))
            //{
            //    e.Graphics.DrawString(Modules.clsView.CNST_STR_MAINGAMETITLETEXT, Modules.clsView.fntTitlebar, brush, 2, 5);
            //}
        }

        private void frmGame_MouseDown(object sender, MouseEventArgs e)
        {
            //if pointer inside "title bar"
            if (e.Y <= Modules.clsView.CNST_INT_TITLEBARHEIGHT)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //move form
                    User32_DLL.ReleaseCapture();
                    User32_DLL.SendMessage(this.Handle, Modules.clsView.WM_NCLBUTTONDOWN, (IntPtr)Modules.clsView.HTCAPTION, new IntPtr(0));
                }
            }
        }

        private void PICClose_Click(object sender, EventArgs e)
        {
            //close everything!
            ExitGame();
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            /*
              Created 30/09/2025 By Roger Williams

              processes keys

              a     - move left
              d     - move right
              space - fire

              Note: ONLY fires IF player not under a baseunit

            */


            string GetBaseUnitAbovePlayer()
            {
                /*
                  Created 03/10/2025 By Roger Williams

                  returns base unit image directly above player if:

                  - there is one
                  - there is and it is VISIBLE
                  - the player is between its left and width
                  - the player is slightly to the left so could shoot past it
                  - the player is slightly to the right so could shoot past it

                  base unit images are 100 pixels ABOVE player image

                */
                string strWhat = String.Empty;
                int intNum = 0;

                for (intNum = 1; intNum != 7; intNum++)
                {

                    //    //check left
                    if (this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Visible)
                    {
                        //check if player ship half to the left hand side
                        if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left - 34, this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left, this.PICPlayer.Left))
                        {
                            strWhat = this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Name;
                            break;
                        }

                        //check if player between left hand side and width
                        if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Left + Modules.clsView.CNST_INT_BASE_WIDTH, this.PICPlayer.Left))
                        {
                            strWhat = this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Name;
                            break;
                        }

                        //check if player ship half over to the right hand side
                        if (Modules.clsModel.InRange(this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Left, this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_R"].Left + (Modules.clsView.CNST_INT_BASE_WIDTH / 2) + 2, this.PICPlayer.Left))
                        {
                            strWhat = this.Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNum + "_L"].Name;
                            break;
                        }
                    }
                }

                return strWhat;
            }


            bool CheckPlayerShootingBase()
            {
                /*
                  Created 03/10/2025 By Roger Williams

                  checks if player attempting to shoot own base!

                */
                string strWhat = String.Empty;

                //make sure player NOT under a base!
                strWhat = GetBaseUnitAbovePlayer();

                if (strWhat == String.Empty)
                {
                    return true;
                }

                return false;
            }

            //only process player "action" keys if game in progress
            if (!blnPlayerLoses && !blnPlayerWins)
            { 
                if ((e.KeyValue == Modules.clsModel.CNST_INT_PLAYERMOVEMENT_LEFT) || (e.KeyValue == Modules.clsModel.CNST_INT_PLAYERMOVEMENT_RIGHT))
                {
                    clsActivePlayer.PlayerMove(e.KeyValue);
                }
                if (e.KeyValue == Modules.clsModel.CNST_INT_PLAYERMOVEMENT_SHOT)
                {
                    if (CheckPlayerShootingBase())
                    {
                        clsActivePlayer.PlayerShoot();
                    }
                }
            }

            //if playagain showing
            if (blnPlayAgain)
            {
                if (e.KeyCode == Keys.Y)
                {
                    blnAsyncPlayAgain = true;
                    //TMRPlayAgain.Enabled = false;
                    this.PICGameEnd.Visible = false;
                    intRepeat = 1;
                    intCount = 1;
                    Init();
                }

                if (e.KeyCode == Keys.N)
                {
                    ExitGame();
                }
            }

            if (e.KeyCode == Keys.X)
            {
                ExitGame();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  return;
            //blnPlayerLost = true;
            //blnPlayerLoses = true;
            //blnCanProgress = false;
            ShowAlienInvasion();

            //while (blnInvasion)
            //{
            //    Application.DoEvents();
            //}

            //this.LBLTest2.Text = "";
            //this.LBLTest2.Update();
            //    clsActivePlayer.PlayerHit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.PICBlankImage.ImageLocation = Modules.clsModel.CNST_STR_IMG_BLANKIMAGE;
            this.PICBlankImage.Load();
            this.PICBlankImage.BringToFront();
            this.PICBlankImage.Visible = true;
            BMPTemp1 = new Bitmap(this.PICBlankImage.Image);
            BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_PLAYER_GAMEOVER);
            enumScreenChosen = enumScreens.scrGameOver;
            //TMRDitherImage.Interval = 300;
            //TMRDitherImage.Enabled = true;
            blnAsyncDither = false;
            AsyncDither();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (this.NUDEnding.Value)
            {
                case 1:
                    PlayerLoses();
                    break;
                case 2:
                    //stop player moving
                    blnPlayerWins = true;
                    blnAsyncCollision = true;
                    PlayerCompletedLevel();
                    break;
                case 3:
                    //stop player moving
                    blnPlayerWins = true;
                    blnAsyncCollision = true;
                    PlayerCompletedGame();
                    break;
                case 4:
                    GameOver();
                    //blnCanProgress = false;
                    break;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //explode and remove alien shot
            clsActiveAlienShot.ExplodeShot();
            //process player hit - also run public sub here: resetplayer
            clsActivePlayer.PlayerHit(blnInvasion);
            //update lives
            UpdateStatus();
            //check if player dead
            blnCanProgress = false;

            if (clsActivePlayer.intLives == 0)
            {
                blnPlayerLoses = true;
                GameOver();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int intNum = 0;


            for (intNum = 1; intNum != 13; intNum++)
            {
                clsActiveAlienWave.AlienHit(intNum);
            }

            for (intNum = 7; intNum != 13; intNum++)
            {
                clsActiveAlienWave.AlienHit(intNum);
            }

            //all aliens destroyed?
            if ((clsActiveAlienWave.intAliensRow1 == 0) && (clsActiveAlienWave.intAliensRow2 == 0))
            {
                PlayerCompletedLevel();
            }
        }
    }
}
