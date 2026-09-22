using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;


/*
  Created 18/09/2025 By Roger Williams

  class for the player

  handles:

  - movement
  - sounds
  - player shot
  - player explodes

*/
namespace RogInvaders2025.Modules
{
    internal class clsPlayer
    {
        //used for animation
        private int intCurPlayerAni = 1;
        private int intCurShotAni = 1;
        private int intCurPlayerExplosionAni = 1;
        private int intCurPlayerExplosionAniCount = 0;
        private Bitmap BMPPlayer = null;
        private Bitmap BMPShot = null;
        private Bitmap BMPExp = null;
        private System.Drawing.Color CLRTemp = System.Drawing.Color.Transparent;

        private PictureBox PICPlayer = new PictureBox();
     //   private PictureBox PICPlayerExplosion = new PictureBox();
        private PictureBox PICPlayerShot = new PictureBox();
        //private Timer TMRAnimateShip = new Timer();
        //private Timer TMRMoveShot = new Timer();
        //private Timer TMRAnimateExplosion = new Timer();
        private bool blnAysncAnimateShip = false;
        private bool blnAsyncMoveShot = false;
        private bool blnAsyncAnimateExplosion = false;
        private MediaPlayer SNDSoundMove = new System.Windows.Media.MediaPlayer();
        private MediaPlayer SNDSoundShot = new System.Windows.Media.MediaPlayer();
        private MediaPlayer SNDSoundHit = new System.Windows.Media.MediaPlayer();
        //used by game mechanics

        public int intScore;
        //give player maximum number of starting lives
        public int intLives = Modules.clsModel.CNST_INT_STARTLIVES;
        public bool blnHit = false;

        private void InitBitmaps()
        {
            /*
                Created 03/11/2025 By Roger Williams

                called by initimages and other external routines such as resetplayer

                laods bitmaps with default images

            */

            BMPShot = new Bitmap(this.PICPlayerShot.Image);
            //BMPExp = new Bitmap(PICPlayerExplosion.Image);
            BMPExp = new Bitmap(((PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls["PICPlayerExplosion"]).Image);
            BMPPlayer = new Bitmap(this.PICPlayer.Image);
        }
        public void InitImages()
        {
            /*
                Created 21/10/2025 By Roger Williams

                called by constructor and other routines such as remove shot and frmgame->init

                init the images!

            */

            //set player image to picturebox on frmgame
            PICPlayer = (PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls["PICPlayer"];
            //set player explosion image to picturebox on frmgame
           // PICPlayerExplosion = (PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls["PICPlayerExplosion"];
            //set shot image to picturebox on frmgame
            PICPlayerShot = (PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls["PICPlayerShot"];


            //init player ship
            PICPlayer.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_1;
            PICPlayer.Load();
            PICPlayer.Visible = true;
            PICPlayer.BringToFront();

            //init player explosion
            //PICPlayerExplosion.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYER_EXPLOSION_1;
            //PICPlayerExplosion.Load();
            //PICPlayerExplosion.Visible = false;

            //init player shot
            PICPlayerShot.ImageLocation = Modules.clsModel.CNST_STR_IMG_PLAYERSHOT_1;
            PICPlayerShot.Load();
            PICPlayerShot.Visible = false;
            PICPlayerShot.BringToFront();

            //init bitmaps
            InitBitmaps();
        }

        public void RemoveShot()
        {
            /*
                Created 29/09/2025 By Roger Williams

                removes shot and stops timer

            */

            try
            { 
                //TMRMoveShot.Enabled = false;
                //TMRAnimateExplosion.Enabled = false;
                blnAsyncAnimateExplosion = true;
                blnAsyncMoveShot = true;
                //PICPlayerShot.Visible = false;
                //hide main form charge graphics;
                ((frmGame)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM]).HideCharge();
                //reset animation numbers
                intCurShotAni = 1;
                intCurPlayerExplosionAni = 1;
                InitImages();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }


        private async void AsyncMoveShot()
        {
            /*
              Created 29/09/2025 By Roger Williams

              animates and moves shot


            changes certain colours in player shot for others depending on "animation" number


            colours changed:

            shot1 r: 255 g: 0   b: 0
            shot2 r: 200 g: 0   b: 0
            shot3 r: 250 g: 100 b: 68
            shot4 r: 250 g: 180 b: 70

        */

            int intNum = 0;
            int intNum3 = 0;

            try
            { 
            while (!blnAsyncMoveShot)
            {
                await Task.Delay(Modules.clsView.CNST_INT_PLAYERSHOT_INTERVAL);
                for (intNum3 = 0; intNum3 != BMPShot.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPShot.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPShot.GetPixel(intNum, intNum3);

                        /*
                            use this for multiple colours

                            Note: works thus: 

                            - intCurShotAni 1: if colour 1 set to colour 2
                            - intCurShotAni 2: if colour 2 set to colour 3
                            - intCurShotAni 3: if colour 3 set to colour 4
                            - intCurShotAni 4: if colour 4 set to colour 1
                        */

                        switch (intCurShotAni)
                        {
                            case 1:
                                //change shot1 colour - original:  shot1 r: 255 g: 0   b: 0
                                if (CLRTemp == System.Drawing.Color.FromArgb(255, 0, 0))
                                {
                                    //set colour 2  shot2 r: 200 g: 0   b: 0
                                    BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(200, 0, 0));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(200, 0, 0))
                                    {
                                        BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 0, 0));
                                    }
                                    else
                                    {
                                        BMPShot.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }
                                break;
                            case 2:
                                //change shot2 colour - original:  shot2 r: 200 g: 0   b: 0
                                if (CLRTemp == System.Drawing.Color.FromArgb(200, 0, 0))
                                {
                                    //set colour 3   shot3 r: 250 g: 100 b: 68
                                    BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(250, 100, 68));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(250, 100, 68))
                                    {
                                        BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(200, 0, 0));
                                    }
                                    else
                                    {
                                        BMPShot.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 3:
                                //change shot3 colour - original: shot3 r: 250 g: 100 b: 68
                                if (CLRTemp == System.Drawing.Color.FromArgb(250, 100, 68))
                                {
                                    //set colour 4  shot4 r: 250 g: 180 b: 70
                                    BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(250, 180, 70));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(250, 180, 70))
                                    {
                                        BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(250, 100, 68));
                                    }
                                    else
                                    {
                                        BMPShot.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 4:
                                //change shot4 colour - original: shot4 r: 250 g: 180 b: 70
                                if (CLRTemp == System.Drawing.Color.FromArgb(250, 180, 70))
                                {
                                    //set colour 1   shot1 r: 255 g: 0   b: 0
                                    BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 0, 0));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 0, 0))
                                    {
                                        BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(250, 180, 70));
                                    }
                                    else
                                    {
                                        BMPShot.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }
                                break;
                        }
                    }
                }

                this.PICPlayerShot.Image = BMPShot;
                this.PICPlayerShot.Invalidate();


                intCurShotAni++;

                if (intCurShotAni > 4)
                {
                    intCurShotAni = 1;
                }

                //move
                PICPlayerShot.Top -= Modules.clsView.CNST_INT_GAMESCREENTOP;

                if (PICPlayerShot.Top <= 20)
                {
                    RemoveShot();
                }
                else
                {
                    ((frmGame)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM]).IncCharge();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }
        private async void AsyncAnimateShip()
        {
            /*
              Created 18/09/2025 By Roger Williams

              animates player ship

            */

            /*

              changes certain colours in ship for others depending on "animation" number

              colours changed:

              ship

                nose:               r: 112      g: 112       b: 112
                hole:               r: 245      g: 245       b:189

                nacel colour 1:     r: 248      g: 244       b: 230
                nacel colour 2:     r: 141      g: 223       b: 203
                nacel colour 3:     r: 220      g: 248       b: 244
                nacel colour 4:     r: 34       g: 246       b: 194
                engine 1:           r: 255      g: 255       b: 0
                engine 2:           r: 231      g: 190       b: 49
                engine 3:           r: 255      g: 0         b: 0
              
            */


            int intNum = 0;
            int intNum3 = 0;


            try
            { 
            while (!blnAysncAnimateShip)
            {
                await Task.Delay(Modules.clsView.CNST_INT_PLAYERANIMATIONDELAY);

                for (intNum3 = 0; intNum3 != BMPPlayer.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPPlayer.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPPlayer.GetPixel(intNum, intNum3);

                        //change nose colour - original:   nose: r: 112  g: 112  b: 112
                        if (CLRTemp == System.Drawing.Color.FromArgb(112, 112, 112))
                        {
                            BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.White);
                        }
                        else
                        { //if already white change back 
                          //Note: DO NOT USE standard colour names as colours obtained from fromargb do NOT have proper names!
                            if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 255))
                            {
                                BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(112, 112, 112));
                            }
                            else
                            {
                                //change hole colour - original: hole: r: 245      g: 245       b: 189
                                if (CLRTemp == System.Drawing.Color.FromArgb(245, 245, 189))
                                {
                                    BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.Navy);
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(0, 0, 128))
                                    {
                                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(245, 245, 189));
                                    }
                                    else
                                    {
                                        BMPPlayer.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }
                            }
                        }

                        /*
                            use this for multiple colours

                            Note: works thus: 

                            - count 1: if colour 1 set to colour 2
                            - count 2: if colour 2 set to colour 3
                            - count 3: if colour 3 set to colour 4
                            - count 4: if colour 4 set to colour 1
                        */

                        switch (intCurPlayerAni)
                        {
                            case 1:
                                //change nacel colour - original:  nacel colour 1:     r: 248      g: 244       b: 230
                                if (CLRTemp == System.Drawing.Color.FromArgb(248, 244, 230))
                                {
                                    //set colour 2  nacel colour 2:     r: 141      g: 223       b: 203
                                    BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(141, 223, 203));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(141, 223, 203))
                                    {
                                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 244, 230));
                                    }
                                }

                                //change engine colour - original: engine 1:           r: 255      g: 255       b: 0
                                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 0))
                                {
                                    //set colour 2  engine 2:           r: 231      g: 190       b: 49
                                    BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(231, 190, 49));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(231, 190, 49))
                                    {
                                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 0));
                                    }
                                }
                                break;
                            case 2:
                                //change nacel colour - original:  nacel colour 2:     r: 141      g: 223       b: 203
                                if (CLRTemp == System.Drawing.Color.FromArgb(141, 223, 203))
                                {
                                    //set colour 3   nacle colour 3:     r: 220      g: 248       b: 244
                                    BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(220, 248, 244));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(220, 248, 244))
                                    {
                                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(141, 223, 203));
                                    }
                                }

                                //change engine colour - original: engine 2:           r: 231      g: 190       b: 49
                                if (CLRTemp == System.Drawing.Color.FromArgb(231, 190, 49))
                                {
                                    //set colour 3 engine 3:           r: 255      g: 0         b: 0
                                    BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 0, 0));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 0, 0))
                                    {
                                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(231, 190, 49));
                                    }
                                }
                                break;
                            case 3:
                                //change nacel colour - original: nacel colour 3:     r: 220      g: 248       b: 244
                                if (CLRTemp == System.Drawing.Color.FromArgb(220, 248, 244))
                                {
                                    //set colour 4   nacel colour 4:     r: 34       g: 246       b: 194
                                    BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(34, 246, 194));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(34, 246, 194))
                                    {
                                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(220, 248, 244));
                                    }
                                }

                                break;
                            case 4:
                                //change nacel colour - original: nacel colour 4:     r: 34       g: 246       b: 194
                                if (CLRTemp == System.Drawing.Color.FromArgb(34, 246, 194))
                                {
                                    //set colour 4   nacel colour 1:     r: 248      g: 244       b: 230
                                    BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 244, 230));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(248, 244, 230))
                                    {
                                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(34, 246, 194));
                                    }
                                }
                                break;
                        }
                    }
                }

                intCurPlayerAni++;

                if (intCurPlayerAni > 4)
                {
                    intCurPlayerAni = 1;
                }

                PICPlayer.Image = BMPPlayer;
                PICPlayer.Invalidate();
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        private async void AsyncAnimateExplosion()
        {
            /*
              Created 05/10/2025 By Roger Williams

              animate player explosion

       
             changes certain colours in player explosion for others depending on "animation" number

             colours changed:

             exp1: r: 224  g: 42  b: 102
             exp2: r: 241  g: 15  b: 15
             exp3: r: 255  g: 102 b: 0
             exp4: r: 255  g: 204 b: 0

         */

            int intNum = 0;
            int intNum3 = 0;

            try
            { 
            while (!blnAsyncAnimateExplosion)
            {    //player and aliens have same explosion delay
                await Task.Delay(Modules.clsView.CNST_INT_ANIMATIONDELAY_ALIEN_EXPLOSION);

                for (intNum3 = 0; intNum3 != BMPExp.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPExp.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPExp.GetPixel(intNum, intNum3);

                        /*
                            use this for multiple colours

                            Note: works thus: 

                            - count 1: if colour 1 set to colour 2
                            - count 2: if colour 2 set to colour 3
                            - count 3: if colour 3 set to colour 4
                            - count 4: if colour 4 set to colour 1
                        */

                        switch (intCurPlayerExplosionAni)
                        {
                            case 1:
                                //change exp1 colour - original:  exp1: r: 224  g: 42  b: 102
                                if (CLRTemp == System.Drawing.Color.FromArgb(224, 42, 102))
                                {
                                    //set colour 2  exp2: r: 241  g: 15  b: 15
                                    BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
                                    {
                                        BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(224, 42, 102));
                                    }
                                    else
                                    {
                                        BMPExp.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }
                                break;
                            case 2:
                                //change exp2 colour - original:  exp2: r: 241  g: 15  b: 15
                                if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
                                {
                                    //set colour 3   exp3: r: 255  g: 102 b: 0
                                    BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 102, 0));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 102, 0))
                                    {
                                        BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
                                    }
                                    else
                                    {
                                        BMPExp.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 3:
                                //change exp3 colour - original: exp3: r: 255  g: 102 b: 0
                                if (CLRTemp == System.Drawing.Color.FromArgb(255, 102, 0))
                                {
                                    //set colour 4   exp4: r: 255  g: 204 b: 0
                                    BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 204, 0));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 204, 0))
                                    {
                                        BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 102, 0));
                                    }
                                    else
                                    {
                                        BMPExp.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 4:
                                //change exp4 colour - original: exp4: r: 255  g: 204 b: 0
                                if (CLRTemp == System.Drawing.Color.FromArgb(255, 204, 0))
                                {
                                    //set colour 1   exp1: r: 224  g: 42  b: 102
                                    BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(224, 42, 182));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(224, 42, 182))
                                    {
                                        BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 204, 0));
                                    }
                                    else
                                    {
                                        BMPExp.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }
                                break;
                        }
                    }
                }

                    //this.PICPlayerExplosion.Image = BMPExp;
                    //this.PICPlayerExplosion.Invalidate();

                    this.PICPlayer.Image = BMPExp;
                    this.PICPlayer.Invalidate();

                    intCurPlayerExplosionAni++;

                if (intCurPlayerExplosionAni > 4)
                {
                    intCurPlayerExplosionAniCount++;
                    intCurPlayerExplosionAni = 1;
                }

                if (intCurPlayerExplosionAniCount > 2)
                {
                    //stop timer remove player
                    //TMRAnimateExplosion.Enabled = false;
                    blnAsyncAnimateExplosion = true;
                    //hide explosion
                   // PICPlayerExplosion.Visible = false;
                    //reset hit
                    blnHit = false;
                    Destroy();
                    //reset game
               //     ((frmGame)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM]).ResetPlayer();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        //private void MoveShotTimer_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 29/09/2025 By Roger Williams

        //      animates and moves shot


        //    changes certain colours in player shot for others depending on "animation" number


        //    colours changed:

        //    shot1 r: 255 g: 0   b: 0
        //    shot2 r: 200 g: 0   b: 0
        //    shot3 r: 250 g: 100 b: 68
        //    shot4 r: 250 g: 180 b: 70

        //*/

        //int intNum = 0;
        //int intNum3 = 0;

        //    for (intNum3 = 0; intNum3 != BMPShot.Height - 1; intNum3++)
        //        {
        //            for (intNum = 0; intNum != BMPShot.Width - 1; intNum++)
        //            {
        //                //get pixel colour
        //                CLRTemp = BMPShot.GetPixel(intNum, intNum3);

        //                /*
        //                    use this for multiple colours

        //                    Note: works thus: 

        //                    - intCurShotAni 1: if colour 1 set to colour 2
        //                    - intCurShotAni 2: if colour 2 set to colour 3
        //                    - intCurShotAni 3: if colour 3 set to colour 4
        //                    - intCurShotAni 4: if colour 4 set to colour 1
        //                */

        //                switch (intCurShotAni)
        //                {
        //                    case 1:
        //                        //change shot1 colour - original:  shot1 r: 255 g: 0   b: 0
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(255, 0, 0))
        //                        {
        //                            //set colour 2  shot2 r: 200 g: 0   b: 0
        //                            BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(200, 0, 0));
        //                        }
        //                        else
        //                        { //if already changed change back
        //                            if (CLRTemp == System.Drawing.Color.FromArgb(200, 0, 0))
        //                            {
        //                                BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 0, 0));
        //                            }
        //                            else
        //                            {
        //                                BMPShot.SetPixel(intNum, intNum3, CLRTemp);
        //                            }
        //                        }
        //                        break;
        //                    case 2:
        //                        //change shot2 colour - original:  shot2 r: 200 g: 0   b: 0
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(200, 0, 0))
        //                        {
        //                            //set colour 3   shot3 r: 250 g: 100 b: 68
        //                            BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(250, 100, 68));
        //                        }
        //                        else
        //                        { //if already changed change back
        //                            if (CLRTemp == System.Drawing.Color.FromArgb(250, 100, 68))
        //                            {
        //                                BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(200, 0, 0));
        //                            }
        //                            else
        //                            {
        //                                BMPShot.SetPixel(intNum, intNum3, CLRTemp);
        //                            }
        //                        }

        //                        break;
        //                    case 3:
        //                        //change shot3 colour - original: shot3 r: 250 g: 100 b: 68
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(250, 100, 68))
        //                        {
        //                            //set colour 4  shot4 r: 250 g: 180 b: 70
        //                            BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(250, 180, 70));
        //                        }
        //                        else
        //                        { //if already changed change back
        //                            if (CLRTemp == System.Drawing.Color.FromArgb(250, 180, 70))
        //                            {
        //                                BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(250, 100, 68));
        //                            }
        //                            else
        //                            {
        //                                BMPShot.SetPixel(intNum, intNum3, CLRTemp);
        //                            }
        //                        }

        //                        break;
        //                    case 4:
        //                        //change shot4 colour - original: shot4 r: 250 g: 180 b: 70
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(250, 180, 70))
        //                        {
        //                            //set colour 1   shot1 r: 255 g: 0   b: 0
        //                            BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 0, 0));
        //                        }
        //                        else
        //                        { //if already changed change back
        //                            if (CLRTemp == System.Drawing.Color.FromArgb(255, 0, 0))
        //                            {
        //                                BMPShot.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(250, 180, 70));
        //                            }
        //                            else
        //                            {
        //                                BMPShot.SetPixel(intNum, intNum3, CLRTemp);
        //                            }
        //                        }
        //                        break;
        //                }
        //            }
        //        }

        //    this.PICPlayerShot.Image = BMPShot;
        //    this.PICPlayerShot.Invalidate();


        //    intCurShotAni++;

        //    if (intCurShotAni > 4)
        //    {
        //        intCurShotAni = 1;
        //    }

        //    //move
        //    PICPlayerShot.Top -= Modules.clsView.CNST_INT_GAMESCREENTOP;

        //    if (PICPlayerShot.Top <= 20)
        //    {
        //         RemoveShot();
        //    }
        //    else
        //    {
        //        ((frmGame)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM]).IncCharge();
        //    }
        //}
        //private void AnimateTimer_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 18/09/2025 By Roger Williams

        //      animates player ship

        //    */

        //    /*

        //      changes certain colours in ship for others depending on "animation" number

        //      colours changed:

        //      ship

        //        nose:               r: 112      g: 112       b: 112
        //        hole:               r: 245      g: 245       b:189

        //        nacel colour 1:     r: 248      g: 244       b: 230
        //        nacel colour 2:     r: 141      g: 223       b: 203
        //        nacel colour 3:     r: 220      g: 248       b: 244
        //        nacel colour 4:     r: 34       g: 246       b: 194
        //        engine 1:           r: 255      g: 255       b: 0
        //        engine 2:           r: 231      g: 190       b: 49
        //        engine 3:           r: 255      g: 0         b: 0

        //    */


        //    int intNum = 0;
        //    int intNum3 = 0;

        //    for (intNum3 = 0; intNum3 != BMPPlayer.Height - 1; intNum3++)
        //    {
        //        for (intNum = 0; intNum != BMPPlayer.Width - 1; intNum++)
        //        {
        //            //get pixel colour
        //            CLRTemp = BMPPlayer.GetPixel(intNum, intNum3);

        //            //change nose colour - original:   nose: r: 112  g: 112  b: 112
        //            if (CLRTemp == System.Drawing.Color.FromArgb(112, 112, 112))
        //            {
        //                BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.White);
        //            }
        //            else
        //            { //if already white change back 
        //                //Note: DO NOT USE standard colour names as colours obtained from fromargb do NOT have proper names!
        //                if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 255))
        //                {
        //                    BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(112, 112, 112));
        //                }
        //                else
        //                {
        //                    //change hole colour - original: hole: r: 245      g: 245       b: 189
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(245, 245, 189))
        //                    {
        //                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.Navy);
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(0, 0, 128))
        //                        {
        //                            BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(245, 245, 189));
        //                        }
        //                        else
        //                        {
        //                            BMPPlayer.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }
        //                }
        //            }

        //            /*
        //                use this for multiple colours

        //                Note: works thus: 

        //                - count 1: if colour 1 set to colour 2
        //                - count 2: if colour 2 set to colour 3
        //                - count 3: if colour 3 set to colour 4
        //                - count 4: if colour 4 set to colour 1
        //            */

        //            switch (intCurPlayerAni)
        //            {
        //                case 1:
        //                    //change nacel colour - original:  nacel colour 1:     r: 248      g: 244       b: 230
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(248, 244, 230))
        //                    {
        //                        //set colour 2  nacel colour 2:     r: 141      g: 223       b: 203
        //                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(141, 223, 203));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(141, 223, 203))
        //                        {
        //                            BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 244, 230));
        //                        }
        //                    }

        //                    //change engine colour - original: engine 1:           r: 255      g: 255       b: 0
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 255, 0))
        //                    {
        //                        //set colour 2  engine 2:           r: 231      g: 190       b: 49
        //                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(231, 190, 49));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(231, 190, 49))
        //                        {
        //                            BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 255, 0));
        //                        }
        //                    }
        //                    break;
        //                case 2:
        //                    //change nacel colour - original:  nacel colour 2:     r: 141      g: 223       b: 203
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(141, 223, 203))
        //                    {
        //                        //set colour 3   nacle colour 3:     r: 220      g: 248       b: 244
        //                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(220, 248, 244));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(220, 248, 244))
        //                        {
        //                            BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(141, 223, 203));
        //                        }
        //                    }

        //                    //change engine colour - original: engine 2:           r: 231      g: 190       b: 49
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(231, 190, 49))
        //                    {
        //                        //set colour 3 engine 3:           r: 255      g: 0         b: 0
        //                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 0, 0));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(255, 0, 0))
        //                        {
        //                            BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(231, 190, 49));
        //                        }
        //                    }
        //                    break;
        //                case 3:
        //                    //change nacel colour - original: nacel colour 3:     r: 220      g: 248       b: 244
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(220, 248, 244))
        //                    {
        //                        //set colour 4   nacel colour 4:     r: 34       g: 246       b: 194
        //                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(34, 246, 194));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(34, 246, 194))
        //                        {
        //                            BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(220, 248, 244));
        //                        }
        //                    }

        //                    break;
        //                case 4:
        //                    //change nacel colour - original: nacel colour 4:     r: 34       g: 246       b: 194
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(34, 246, 194))
        //                    {
        //                        //set colour 4   nacel colour 1:     r: 248      g: 244       b: 230
        //                        BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 244, 230));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(248, 244, 230))
        //                        {
        //                            BMPPlayer.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(34, 246, 194));
        //                        }
        //                    }
        //                    break;
        //            }
        //        }
        //    }

        //    intCurPlayerAni++;

        //    if (intCurPlayerAni > 4)
        //    {
        //        intCurPlayerAni = 1;
        //    }

        //    PICPlayer.Image = BMPPlayer;
        //    PICPlayer.Invalidate();
        //}

        //private void AnimateExplosion_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 05/10/2025 By Roger Williams

        //      animate player explosion


        //     changes certain colours in player explosion for others depending on "animation" number

        //     colours changed:

        //     exp1: r: 224  g: 42  b: 102
        //     exp2: r: 241  g: 15  b: 15
        //     exp3: r: 255  g: 102 b: 0
        //     exp4: r: 255  g: 204 b: 0

        // */

        //    int intNum = 0;
        //    int intNum3 = 0;

        //    for (intNum3 = 0; intNum3 != BMPExp.Height - 1; intNum3++)
        //    {
        //        for (intNum = 0; intNum != BMPExp.Width - 1; intNum++)
        //        {
        //            //get pixel colour
        //            CLRTemp = BMPExp.GetPixel(intNum, intNum3);

        //            /*
        //                use this for multiple colours

        //                Note: works thus: 

        //                - count 1: if colour 1 set to colour 2
        //                - count 2: if colour 2 set to colour 3
        //                - count 3: if colour 3 set to colour 4
        //                - count 4: if colour 4 set to colour 1
        //            */

        //            switch (intCurPlayerExplosionAni)
        //            {
        //                case 1:
        //                    //change exp1 colour - original:  exp1: r: 224  g: 42  b: 102
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(224, 42, 102))
        //                    {
        //                        //set colour 2  exp2: r: 241  g: 15  b: 15
        //                        BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
        //                        {
        //                            BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(224, 42, 102));
        //                        }
        //                        else
        //                        {
        //                            BMPExp.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }
        //                    break;
        //                case 2:
        //                    //change exp2 colour - original:  exp2: r: 241  g: 15  b: 15
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
        //                    {
        //                        //set colour 3   exp3: r: 255  g: 102 b: 0
        //                        BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 102, 0));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(255, 102, 0))
        //                        {
        //                            BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
        //                        }
        //                        else
        //                        {
        //                            BMPExp.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }

        //                    break;
        //                case 3:
        //                    //change exp3 colour - original: exp3: r: 255  g: 102 b: 0
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 102, 0))
        //                    {
        //                        //set colour 4   exp4: r: 255  g: 204 b: 0
        //                        BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 204, 0));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(255, 204, 0))
        //                        {
        //                            BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 102, 0));
        //                        }
        //                        else
        //                        {
        //                            BMPExp.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }

        //                    break;
        //                case 4:
        //                    //change exp4 colour - original: exp4: r: 255  g: 204 b: 0
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 204, 0))
        //                    {
        //                        //set colour 1   exp1: r: 224  g: 42  b: 102
        //                        BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(224, 42, 182));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(224, 42, 182))
        //                        {
        //                            BMPExp.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(255, 204, 0));
        //                        }
        //                        else
        //                        {
        //                            BMPExp.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }
        //                    break;
        //            }
        //        }
        //    }

        //    this.PICPlayerExplosion.Image = BMPExp;
        //    this.PICPlayerExplosion.Invalidate();


        //    intCurPlayerExplosionAni++;

        //    if (intCurPlayerExplosionAni > 4)
        //    {
        //        intCurPlayerExplosionAniCount++;
        //        intCurPlayerExplosionAni = 1;
        //    }

        //    if (intCurPlayerExplosionAniCount > 2)
        //    { 
        //        //stop timer remove player
        //        TMRAnimateExplosion.Enabled = false;
        //        //hide explosion
        //        PICPlayerExplosion.Visible = false;
        //        //reset hit
        //        blnHit = false;
        //        //reset game
        //        ((frmGame)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM]).ResetPlayer();
        //        Destroy();
        //    }
        //}

        private void PositionPlayerStart()
        {
            /*
              Created 29/09/2025 By Roger Williams

              sets player ship to start position
              
              called by constructor AND playerdestroyed

            */

            PICPlayer.Left = Modules.clsView.CNST_INT_PLAYER_LEFT;
            PICPlayer.Top = Modules.clsView.CNST_INT_PLAYER_TOP;
            //show ship
          //  PICPlayer.Visible = true;
        }
        //********public************

        public void ResetPlayer()
        {
            /*
              Created 1/10/2025 By Roger Williams

              reset player position and image and timer values etc

              called by frmgame 

            */

            //reset timer vars
            intCurPlayerExplosionAni = 1;
            intCurPlayerAni = 1;
            intCurShotAni = 1;
            intCurPlayerExplosionAniCount = 0;
            //reset images
            InitImages();
            //position player
            PositionPlayerStart();
            //start amination timer
            blnAysncAnimateShip = false;
            AsyncAnimateShip();
            
        }

        public void Destroy()
        {
            /*
              Created 29/09/2025 By Roger Williams

              called when game form closed by player 
              stops everything, no "you lose" messages simply act as class destructor 

            */

            //TMRAnimateExplosion.Enabled=false;
            //TMRAnimateShip.Enabled = false;
            //TMRMoveShot.Enabled = false;
            blnAysncAnimateShip = true;
            blnAsyncAnimateExplosion = true;
            blnAsyncMoveShot = true;
            SNDSoundMove.Stop();
            SNDSoundShot.Stop();
            SNDSoundHit.Stop();
            PICPlayer.Visible = false;
        //    PICPlayerExplosion.Visible = false;
            PICPlayerShot.Visible = false;
        }
        private void PlayerExplodes()
        {
            /*
              Created 06/10/2025 By Roger Williams

              animates player explosion plays sound
               
              Note: uses same explosion animation as alien

            */


            //start explosion timer
            intCurPlayerExplosionAni = 1;
            intCurPlayerExplosionAniCount = 0;
            //play hit sound
            SNDSoundHit.Position = Modules.clsModel.TMSStart;
            SNDSoundHit.Play();
            //show explosion
            BMPPlayer = BMPExp;
            //PICPlayer.Visible = false;
            //PICPlayerExplosion.Top = PICPlayer.Top;
            //PICPlayerExplosion.Left = PICPlayer.Left;
            //PICPlayerExplosion.Visible = true;
            //PICPlayerExplosion.BringToFront();
            //stop standard animation timer
            //TMRAnimateShip.Enabled = false;
            blnAysncAnimateShip = true;
            //start explosion animation timer
            //TMRAnimateExplosion.Enabled = true;
            blnAsyncAnimateExplosion = false;
            AsyncAnimateExplosion();
        }
        public void PlayerHit(bool blnAlienInvasion)
        {
            /*

               VARS

               blnAlienInvasion = aliens invading? if so don't decrement lives

            */
            blnHit = true;

            if (!blnAlienInvasion)
            { 
                intLives--;
            }

            RemoveShot();
            PlayerExplodes();
        }
       
        public void PlayerShoot()
        {
            /*
              Created 29/09/2025 By Roger Williams

              called when game form receives player shoot command


              moves and animates the shot

              if shot reaches top of screen remove and stop timer

            */

            int intPLayerPos = PICPlayer.Left + Modules.clsView.CNST_INT_PLAYER_WIDTH;
 
            //no visible shot?
            if (PICPlayerShot.Visible == false)
            {
                //play fire sound
                SNDSoundShot.Position = Modules.clsModel.TMSStart;
                SNDSoundShot.Play();
                //position bullet
                PICPlayerShot.Left = PICPlayer.Left + (PICPlayerShot.Width / 2) + (PICPlayer.Width /2) - 10;
                PICPlayerShot.Top = PICPlayer.Top - PICPlayerShot.Height;
                //show bullet
                PICPlayerShot.Visible = true;
                //start movement/animation
                //TMRMoveShot.Enabled = true;
                blnAsyncMoveShot = false;
                AsyncMoveShot();
                ((frmGame)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM]).ShowCharge();
            }
        }
        public void PlayerMove(int intDirection)
        {
            /*
              Created 29/09/2025 By Roger Williams

              called when game form receives movement command and plays player move sound

              vars

              intDirection - keycode of movement key e.g. A = left, D = right

            */

            switch (intDirection)
            {
                case Modules.clsModel.CNST_INT_PLAYERMOVEMENT_LEFT:
                    if (PICPlayer.Left > 0)
                    {
                        PICPlayer.Left -= 5;
                        //SNDSoundMove.Position = Modules.clsModel.TMSStart;
                        //SNDSoundMove.Play();
                    }
                    break;

                case Modules.clsModel.CNST_INT_PLAYERMOVEMENT_RIGHT:
                    if (PICPlayer.Left < Modules.clsView.CNST_INT_SCREEN_WIDTH - Modules.clsView.CNST_INT_PLAYER_WIDTH)
                    {
                        PICPlayer.Left += 5;
                        //SNDSoundMove.Position = Modules.clsModel.TMSStart;
                        //SNDSoundMove.Play();
                    }
                    break;
            }

        }



        public clsPlayer()
        {
            //init images
            InitImages();
            //position player
            PositionPlayerStart();
            //create ship animation timer
            blnAysncAnimateShip = false;
            AsyncAnimateShip();
            //init move shot timer
            blnAsyncMoveShot = true;
            //init timer explosion
            blnAsyncAnimateExplosion = true;
            //init sound
            SNDSoundMove.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_MOVE));
            SNDSoundMove.Volume = 0.7;

            SNDSoundShot.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_SHOT));
            SNDSoundShot.Volume = 1;

            SNDSoundHit.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_PLAYER_HIT));
            SNDSoundHit.Volume = 1;
        }
    }
}
