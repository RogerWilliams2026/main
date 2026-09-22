using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

/*
  Created 06/10/2025 By Roger Williams

  moved from clsalien

  alienwave class will handle shooting

*/
namespace RogInvaders2025.Modules
{
    internal class clsAlienShot
    {
        private PictureBox PICAlienShot = new PictureBox();
        private MediaPlayer SNDSoundShot = new System.Windows.Media.MediaPlayer();
        private MediaPlayer SNDSoundShotMissed = new System.Windows.Media.MediaPlayer();

        //private Timer TMRAnimateShot = new Timer();
        //private Timer TMRAnimateShotExplode = new Timer();
        //private Timer TMRMoveShot = new Timer();
        private bool blnAsyncAnimateShot = false;
        private bool blnAsyncAnimateShotExplode = false;
        private bool blnAsyncMoveShot = false;

        //used for animation
        private int intCurShotAni = 1;
        private Bitmap BMPTemp1 = null;
        private Bitmap BMPTemp2 = null;
        private System.Drawing.Color CLRTemp = System.Drawing.Color.Transparent;

        public bool blnExploding = false; //used to make sure frmgame is not repeatedly causing explosions due to collision timer!

        private void InitImages()
        {
            /*
                Created 21/10/2025 By Roger Williams

                called by constructor and remove shot

                init the images!

            */

            //set shot image to picturebox on frmgame
            PICAlienShot = (PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls["PICAlienShot"];

            //get shot image
            PICAlienShot.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENSHOT_1;
            PICAlienShot.Visible = false;
            PICAlienShot.BringToFront();
            PICAlienShot.Load();
            BMPTemp1 = new Bitmap(this.PICAlienShot.Image);

            //get shot explosion image
            BMPTemp2 = new Bitmap(Modules.clsModel.CNST_STR_IMG_ALIENSHOT_EXPLOSION_1);
        }

        public void RemoveShot()
        {
            /*
                Created 29/09/2025 By Roger Williams

                removes shot and stops timer

            */

            //stop timers            
            //TMRAnimateShotExplode.Enabled = false;
            //TMRAnimateShot.Enabled = false;
            //TMRMoveShot.Enabled = false;
            try
            { 
                blnAsyncAnimateShotExplode = true;
                blnAsyncAnimateShot = true;
                blnAsyncMoveShot = true;
                SNDSoundShot.Stop();
                //reset animation number 
                intCurShotAni = 1;

                blnExploding = false;
                InitImages();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
}

        private async void AsyncAnimateShot()
        {
            /*
                Created 01/10/2025 By Roger Williams

                animate alien shot

                changes certain colours in Alien shot for others depending on "animation" number

                colours changed:

                alienshot1:  r: 104  g: 74  b: 74
                alienshot2:  r: 120  g: 145 b: 245
                alienshot3:  r: 174  g: 186 b: 232
                alienshot4:  r: 45   g: 81  b: 210
  
            */

            int intNum = 0;
            int intNum3 = 0;

            try 
            { 
            while (!blnAsyncAnimateShot)
            {
                await Task.Delay(Modules.clsView.CNST_INT_ANIMATIONDELAY_ALIEN_SHOT);

                for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);

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
                                //change shot1 colour - original:  alienshot1:  r: 104  g: 74  b: 74
                                if (CLRTemp == System.Drawing.Color.FromArgb(104, 74, 74))
                                {
                                    //set colour 2  alienshot2:  r: 120  g: 145 b: 245
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(120, 145, 245));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(120, 145, 245))
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(104, 74, 74));
                                    }
                                    else
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }
                                break;
                            case 2:
                                //change shot2 colour - original:  alienshot2:  r: 120  g: 145 b: 245
                                if (CLRTemp == System.Drawing.Color.FromArgb(120, 145, 245))
                                {
                                    //set colour 3   alienshot3:  r: 174  g: 186 b: 232
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(174, 186, 232));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(174, 186, 232))
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(120, 145, 245));
                                    }
                                    else
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 3:
                                //change shot3 colour - original: alienshot3:  r: 174  g: 186 b: 232
                                if (CLRTemp == System.Drawing.Color.FromArgb(174, 186, 232))
                                {
                                    //set colour 4   alienshot4:  r: 45   g: 81  b: 210
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(45, 81, 210));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(45, 81, 210))
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(174, 186, 232));
                                    }
                                    else
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 4:
                                //change shot4 colour - original: alienshot4:  r: 45   g: 81  b: 210
                                if (CLRTemp == System.Drawing.Color.FromArgb(45, 81, 210))
                                {
                                    //set colour 1   alienshot1:  r: 104  g: 74  b: 74
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(104, 74, 74));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(255, 0, 0))
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(104, 74, 74));
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

                PICAlienShot.Image = BMPTemp1;
                PICAlienShot.Invalidate();

                intCurShotAni++;

                if (intCurShotAni > 4)
                {
                    intCurShotAni = 1;
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }


        private async void AsyncAnimateShotExplode()
        {
            /*
              Created 07/10/2025 By Roger Williams

              animate alien shot explosion

              changes certain colours in alien explosion for others depending on "animation" number

            colours changed:

            exp1: r:241    g: 15       b: 15
            exp2: r:248    g: 92       b: 21
            exp3: r:248    g: 181      b: 21
            exp4: r:210    g: 196      b: 164

        */

            int intNum = 0;
            int intNum3 = 0;

            try
            { 
            while (!blnAsyncAnimateShotExplode)
            {

                await Task.Delay(Modules.clsView.CNST_INT_ANIMATIONDELAY_ALIEN_SHOTEXPLOSION);

                for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPTemp2.GetPixel(intNum, intNum3);

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
                                //change exp1 colour - original:  exp1: r:241    g: 15       b: 15
                                if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
                                {
                                    //set colour 2  exp2: r:248    g: 92       b: 21
                                    BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 92, 21));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(248, 92, 21))
                                    {
                                        BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
                                    }
                                    else
                                    {
                                        BMPTemp2.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }
                                break;
                            case 2:
                                //change exp2 colour - original:  exp2: r:248    g: 92       b: 21
                                if (CLRTemp == System.Drawing.Color.FromArgb(248, 92, 21))
                                {
                                    //set colour 3   exp3: r:248    g: 181      b: 21
                                    BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 181, 21));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(248, 181, 21))
                                    {
                                        BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 92, 21));
                                    }
                                    else
                                    {
                                        BMPTemp2.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 3:
                                //change exp3 colour - original: exp3: r:248    g: 181      b: 21
                                if (CLRTemp == System.Drawing.Color.FromArgb(248, 181, 21))
                                {
                                    //set colour 4   exp4: r:210    g: 196      b: 164
                                    BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(210, 196, 164));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(210, 196, 164))
                                    {
                                        BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 181, 21));
                                    }
                                    else
                                    {
                                        BMPTemp2.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 4:
                                //change exp4 colour - original: exp4: r:210    g: 196      b: 164
                                if (CLRTemp == System.Drawing.Color.FromArgb(210, 196, 164))
                                {
                                    //set colour 1   exp1: r:241    g: 15       b: 15
                                    BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
                                    {
                                        BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(210, 196, 164));
                                    }
                                    else
                                    {
                                        BMPTemp2.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }
                                break;
                        }
                    }
                }

                    //PICAlienShotExplosion.Image = BMPTemp2;
                    //PICAlienShotExplosion.Invalidate();
                    PICAlienShot.Image = BMPTemp2;
                    PICAlienShot.Invalidate();

                    intCurShotAni++;

                if (intCurShotAni > 4)
                {
                    //TMRAnimateShotExplode.Enabled = false;
                    blnAsyncAnimateShotExplode = true;
                    RemoveShot();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        private async void AsyncMoveShot(int intCurLevel)
        {
            /*
              Created 06/10/2025 By Roger Williams

              move alien shot remove if reaches bottom of screen

              intlevel determines the speed of the shot
             
              VARS

              intcurlevel - current game level

            */

            try
            { 
                while (!blnAsyncMoveShot)
                {
                    switch (intCurLevel)
                    {
                        case 1:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENSHOT_MOVE_INTERVAL_LEVEL1);
                            break;
                        case 2:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENSHOT_MOVE_INTERVAL_LEVEL2);
                            break;
                        case 3:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENSHOT_MOVE_INTERVAL_LEVEL3);
                            break;
                        case 4:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENSHOT_MOVE_INTERVAL_LEVEL4);
                            break;
                    }

                    //move
                    PICAlienShot.Top += Modules.clsView.CNST_INT_ALIEN_SHOT_MOVEAMOUNT;

                    if (PICAlienShot.Top >= Modules.clsView.CNST_INT_SCREEN_HEIGHT - PICAlienShot.Height - Modules.clsView.CNST_INT_GAMESCREENTOP)
                    {
                        //play missed sound
                        SNDSoundShotMissed.Position = Modules.clsModel.TMSStart;
                        SNDSoundShotMissed.Play();
                        //explode shot
                        ExplodeShot();
                    }
                }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        //private void AnimateShot_Tick(object sender, EventArgs e)
        //{
        //    /*
        //        Created 01/10/2025 By Roger Williams

        //        animate alien shot

        //        changes certain colours in Alien shot for others depending on "animation" number

        //        colours changed:

        //        alienshot1:  r: 104  g: 74  b: 74
        //        alienshot2:  r: 120  g: 145 b: 245
        //        alienshot3:  r: 174  g: 186 b: 232
        //        alienshot4:  r: 45   g: 81  b: 210

        //    */

        //    int intNum = 0;
        //    int intNum3 = 0;


        //    for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
        //    {
        //        for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
        //        {
        //            //get pixel colour
        //            CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);

        //            /*
        //                use this for multiple colours

        //                Note: works thus: 

        //                - intCurShotAni 1: if colour 1 set to colour 2
        //                - intCurShotAni 2: if colour 2 set to colour 3
        //                - intCurShotAni 3: if colour 3 set to colour 4
        //                - intCurShotAni 4: if colour 4 set to colour 1
        //            */

        //            switch (intCurShotAni)
        //            {
        //                case 1:
        //                    //change shot1 colour - original:  alienshot1:  r: 104  g: 74  b: 74
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(104, 74, 74))
        //                    {
        //                        //set colour 2  alienshot2:  r: 120  g: 145 b: 245
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(120, 145, 245));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(120, 145, 245))
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(104, 74, 74));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }
        //                    break;
        //                case 2:
        //                    //change shot2 colour - original:  alienshot2:  r: 120  g: 145 b: 245
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(120, 145, 245))
        //                    {
        //                        //set colour 3   alienshot3:  r: 174  g: 186 b: 232
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(174, 186, 232));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(174, 186, 232))
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(120, 145, 245));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }

        //                    break;
        //                case 3:
        //                    //change shot3 colour - original: alienshot3:  r: 174  g: 186 b: 232
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(174, 186, 232))
        //                    {
        //                        //set colour 4   alienshot4:  r: 45   g: 81  b: 210
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(45, 81, 210));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(45, 81, 210))
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(174, 186, 232));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }

        //                    break;
        //                case 4:
        //                    //change shot4 colour - original: alienshot4:  r: 45   g: 81  b: 210
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(45, 81, 210))
        //                    {
        //                        //set colour 1   alienshot1:  r: 104  g: 74  b: 74
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(104, 74, 74));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(255, 0, 0))
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(104, 74, 74));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }
        //                    break;
        //            }
        //        }
        //    }

        //    PICAlienShot.Image = BMPTemp1;
        //    PICAlienShot.Invalidate();

        //    intCurShotAni++;

        //    if (intCurShotAni > 4)
        //    {
        //        intCurShotAni = 1;
        //    }
        //}


        //private void AnimateShotExplode_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 07/10/2025 By Roger Williams

        //      animate alien shot explosion

        //      changes certain colours in alien explosion for others depending on "animation" number

        //    colours changed:

        //    exp1: r:241    g: 15       b: 15
        //    exp2: r:248    g: 92       b: 21
        //    exp3: r:248    g: 181      b: 21
        //    exp4: r:210    g: 196      b: 164

        //*/

        //    int intNum = 0;
        //    int intNum3 = 0;


        //    for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
        //    {
        //        for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
        //        {
        //            //get pixel colour
        //            CLRTemp = BMPTemp2.GetPixel(intNum, intNum3);

        //            /*
        //                use this for multiple colours

        //                Note: works thus: 

        //                - intCurShotAni 1: if colour 1 set to colour 2
        //                - intCurShotAni 2: if colour 2 set to colour 3
        //                - intCurShotAni 3: if colour 3 set to colour 4
        //                - intCurShotAni 4: if colour 4 set to colour 1
        //            */

        //            switch (intCurShotAni)
        //            {
        //                case 1:
        //                    //change exp1 colour - original:  exp1: r:241    g: 15       b: 15
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
        //                    {
        //                        //set colour 2  exp2: r:248    g: 92       b: 21
        //                        BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 92, 21));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(248, 92, 21))
        //                        {
        //                            BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp2.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }
        //                    break;
        //                case 2:
        //                    //change exp2 colour - original:  exp2: r:248    g: 92       b: 21
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(248, 92, 21))
        //                    {
        //                        //set colour 3   exp3: r:248    g: 181      b: 21
        //                        BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 181, 21));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(248, 181, 21))
        //                        {
        //                            BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 92, 21));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp2.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }

        //                    break;
        //                case 3:
        //                    //change exp3 colour - original: exp3: r:248    g: 181      b: 21
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(248, 181, 21))
        //                    {
        //                        //set colour 4   exp4: r:210    g: 196      b: 164
        //                        BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(210, 196, 164));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(210, 196, 164))
        //                        {
        //                            BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(248, 181, 21));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp2.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }

        //                    break;
        //                case 4:
        //                    //change exp4 colour - original: exp4: r:210    g: 196      b: 164
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(210, 196, 164))
        //                    {
        //                        //set colour 1   exp1: r:241    g: 15       b: 15
        //                        BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
        //                        {
        //                            BMPTemp2.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(210, 196, 164));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp2.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }
        //                    break;
        //            }
        //        }
        //    }

        //    PICAlienShotExplosion.Image = BMPTemp2;
        //    PICAlienShotExplosion.Invalidate();

        //    intCurShotAni++;

        //    if (intCurShotAni > 4)
        //    {
        //        TMRAnimateShotExplode.Enabled = false;
        //        RemoveShot();
        //    }
        //}
        public void ExplodeShot()
        {
            /*
              Created 07/10/2025 By Roger Williams

              animates the shot explosion

              called when frmgame detects alien shot hit player/base
              also called by move timer when shot reaches screen bottom

            */
            //stop shot moving
            // TMRMoveShot.Enabled = false;

            try
            { 
                blnAsyncMoveShot = true;
                //put explosion into position
                //PICAlienShotExplosion.Top = PICAlienShot.Top;
                //PICAlienShotExplosion.Left = PICAlienShot.Left;
                //PICAlienShotExplosion.Visible = true;
                //hide shot
                // PICAlienShot.Visible = false;
                PICAlienShot.Image = BMPTemp2;
                blnExploding = true;
                //reset animation counters
                intCurShotAni = 1;
                //start explosion
                //TMRAnimateShotExplode.Enabled = true;
                blnAsyncAnimateShotExplode = false;
                AsyncAnimateShotExplode();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }
        //private void MoveShot_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 06/10/2025 By Roger Williams

        //      move alien shot remove if reaches bottom of screen

        //    */

        //    //move
        //    PICAlienShot.Top += Modules.clsView.CNST_INT_ALIEN_SHOT_MOVEAMOUNT;

        //    if (PICAlienShot.Top >= Modules.clsView.CNST_INT_SCREEN_HEIGHT - PICAlienShot.Height - Modules.clsView.CNST_INT_GAMESCREENTOP)
        //    {
        //        //play missed sound
        //        SNDSoundShotMissed.Position = Modules.clsModel.TMSStart;
        //        SNDSoundShotMissed.Play();
        //        //explode shot
        //        ExplodeShot();
        //    }
        //}
        public void AlienShoot(int intShotLeft, int intShotTop, int intLevel)
        {
            /*
              Created 01/10/2025 By Roger Williams

              called when frmgame sends shoot command

              moves and animates the shot

              if shot reaches bottom of screen remove and stop timer


              VARS

              intshottop    - alien shot top
              intshotleft   - alien shot left
              intlevel      - current level number

            */

            try
            { 
            //position bullet
            PICAlienShot.Left = intShotLeft;
            PICAlienShot.Top = intShotTop;

            //no visible shot?
            if (PICAlienShot.Visible == false)
            {
                //show bullet
                PICAlienShot.Visible = true;
                //start animation
                //TMRAnimateShot.Enabled = true;
                blnAsyncAnimateShot = false;
                AsyncAnimateShot();
                //start movement
                //TMRMoveShot.Enabled = true;
                blnAsyncMoveShot = false;
                AsyncMoveShot(intLevel);
            }

            //play fire sound
            SNDSoundShot.Position = Modules.clsModel.TMSStart;
            SNDSoundShot.Play();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        public void Destroy()
        {
            RemoveShot();
            PICAlienShot.Visible = false;
        }

        public clsAlienShot()
        {
            InitImages();

            //init sound
            SNDSoundShot.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_ALIEN_SHOT));
            SNDSoundShot.Volume = 1;

            SNDSoundShotMissed.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_ALIEN_SHOTMISSED));
            SNDSoundShotMissed.Volume = 0.7;

            //create timers
            blnAsyncAnimateShot = false;
            blnAsyncAnimateShotExplode = false;
            blnAsyncMoveShot = false;
        }
    }
}
