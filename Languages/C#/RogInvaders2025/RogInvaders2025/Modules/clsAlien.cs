using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media;

/*
  Created 18/09/2025 By Roger Williams

  class for each alien in wave

  2 different alien types each has 4 animations
  this class creates a single alien

  alien is moved in the alienwave class

*/
namespace RogInvaders2025.Modules
{
    public class clsAlien
    {
        private MediaPlayer SNDSoundHit = new System.Windows.Media.MediaPlayer();
        System.Drawing.Color CLRTemp = System.Drawing.Color.Transparent;

        private int intCurAlienAni = 1;
        private int intCurAlienExplosionAni = 1;
        private int intThisAlienType = 1;

        //private Timer TMRAnimateAlien;
        //private Timer TMRAnimateExplosion;
        private bool blnAsyncAnimateAlien = false;
        private bool blnAsyncAnimateExplosion = false;

        public PictureBox PICAlien; //alienwave class changes this to animate the alien
        private Bitmap BMPTemp1 = null;

        public int intLeft = 0;
        public int intTop = 0;
        public int intHealth = 0;  //type 1 alien = 2 type 2 alien = 1  <- hits to destroy
        public bool blnVisible = true; //used by alienwave class when deciding whether to move alien


        public void PlayHitSound()
        {

            /*
              Created 21/10/2025 By Roger Williams

              called by clsalienwave if type 2 alien hit (row2) BUT alien still has health

            */

            try
            { 
            //play hit sound
            SNDSoundHit.Position = Modules.clsModel.TMSStart;
            SNDSoundHit.Play();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }
        public void Destroy()
        {
            /*
              Created 01/10/2025 By Roger Williams

              called by alienwave class removes alien

            */

            //TMRAnimateAlien.Enabled = false;
            //TMRAnimateExplosion.Enabled = false;
            try
            { 
                blnAsyncAnimateAlien = true;
                blnAsyncAnimateExplosion = true;
                
                PICAlien.Visible = false;
                blnVisible = false;
                intHealth = 0;
                //reset animation numbers
                intCurAlienAni = 1;
                intCurAlienExplosionAni = 1;
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

              animate alien explosion

            */

            int intNum = 0;
            int intNum3 = 0;
            try
            { 
            while (!blnAsyncAnimateExplosion)
            {
                await Task.Delay(Modules.clsView.CNST_INT_ANIMATIONDELAY_ALIEN_EXPLOSION);

                for (intNum3 = 0; intNum3 != BMPTemp1.Height - 1; intNum3++)
                {
                    for (intNum = 0; intNum != BMPTemp1.Width - 1; intNum++)
                    {
                        //get pixel colour
                        CLRTemp = BMPTemp1.GetPixel(intNum, intNum3);

                        /*
                            use this for multiple colours

                            Note: works thus: 

                            - intCurAlienExplosionAni 1: if colour 1 set to colour 2
                            - intCurAlienExplosionAni 2: if colour 2 set to colour 3
                            - intCurAlienExplosionAni 3: if colour 3 set to colour 4
                            - intCurAlienExplosionAni 4: if colour 4 set to colour 1
                        */

                        switch (intCurAlienExplosionAni)
                        {
                            case 1:
                                //change exp1 colour - original:  alienexp1:  r: 241  g: 15  b: 15
                                if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
                                {
                                    //set colour 2  alienexp2:  r: 220  g: 102 b: 102
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(220, 102, 102));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(220, 102, 102))
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
                                    }
                                    else
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }
                                break;
                            case 2:
                                //change exp2 colour - original:  alienexp2:  r: 220  g: 102 b: 102
                                if (CLRTemp == System.Drawing.Color.FromArgb(220, 102, 102))
                                {
                                    //set colour 3   alienexp3:  r: 242  g: 174 b: 44
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(242, 174, 44));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(242, 174, 44))
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(220, 102, 102));
                                    }
                                    else
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 3:
                                //change exp3 colour - original: alienexp3:  r: 242  g: 174 b: 44
                                if (CLRTemp == System.Drawing.Color.FromArgb(242, 174, 44))
                                {
                                    //set colour 4   alienexp4:  r:241    g: 201  b: 103 
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 201, 103));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(241, 201, 103))
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(242, 174, 44));
                                    }
                                    else
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
                                    }
                                }

                                break;
                            case 4:
                                //change exp4 colour - original: alienexp4:  r: 241   g: 201  b: 103
                                if (CLRTemp == System.Drawing.Color.FromArgb(241, 201, 103))
                                {
                                    //set colour 1   alienexp1:  r: 241  g: 15  b: 15
                                    BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
                                }
                                else
                                { //if already changed change back
                                    if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
                                    {
                                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 201, 103));
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

                PICAlien.Image = BMPTemp1;
                PICAlien.Update();  
                intCurAlienExplosionAni++;

                if (intCurAlienExplosionAni > 4)
                {
                    //stop timer remove alien
                    //                TMRAnimateExplosion.Enabled = false;
                    blnAsyncAnimateExplosion = true;
                    Destroy();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }


        private async void AsyncAnimateAlien()
        {
            /*
              Created 01/10/2025 By Roger Williams

              animate alien

            */
            try
            { 
            while (!blnAsyncAnimateAlien)
            {
                await Task.Delay(Modules.clsView.CNST_INT_ANIMATIONDELAY);

                //animate
                switch (intCurAlienAni)
                {
                    case 1:
                        if (intThisAlienType == 1)
                        {
                            PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_1;
                        }
                        else
                        {
                            if (intHealth == 2)
                            {
                                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_1;
                            }
                            else
                            {
                                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_1_HIT;
                            }
                        }

                        break;
                    case 2:
                        if (intThisAlienType == 1)
                        {
                            PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_2;
                        }
                        else
                        {
                            if (intHealth == 2)
                            {
                                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_2;
                            }
                            else
                            {
                                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_2_HIT;
                            }
                        }

                        break;
                    case 3:
                        if (intThisAlienType == 1)
                        {
                            PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_3;
                        }
                        else
                        {
                            if (intHealth == 2)
                            {
                                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_3;
                            }
                            else
                            {
                                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_3_HIT;
                            }
                        }

                        break;
                    case 4:
                        if (intThisAlienType == 1)
                        {
                            PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_4;
                        }
                        else
                        {
                            if (intHealth == 2)
                            {
                                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_4;
                            }
                            else
                            {
                                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_4_HIT;
                            }
                        }
                        break;
                }

                intCurAlienAni++;

                if (intCurAlienAni > 4)
                {
                    intCurAlienAni = 1;
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }
        //private void AnimateExplosion_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 05/10/2025 By Roger Williams

        //      animate alien explosion

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

        //                - intCurAlienExplosionAni 1: if colour 1 set to colour 2
        //                - intCurAlienExplosionAni 2: if colour 2 set to colour 3
        //                - intCurAlienExplosionAni 3: if colour 3 set to colour 4
        //                - intCurAlienExplosionAni 4: if colour 4 set to colour 1
        //            */

        //            switch (intCurAlienExplosionAni)
        //            {
        //                case 1:
        //                    //change exp1 colour - original:  alienexp1:  r: 241  g: 15  b: 15
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
        //                    {
        //                        //set colour 2  alienexp2:  r: 220  g: 102 b: 102
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(220,102,102));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(220, 102, 102))
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }
        //                    break;
        //                case 2:
        //                    //change exp2 colour - original:  alienexp2:  r: 220  g: 102 b: 102
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(220, 102, 102))
        //                    {
        //                        //set colour 3   alienexp3:  r: 242  g: 174 b: 44
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(242, 174, 44));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(242, 174, 44))
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(220, 102, 102));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }

        //                    break;
        //                case 3:
        //                    //change exp3 colour - original: alienexp3:  r: 242  g: 174 b: 44
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(242, 174, 44))
        //                    {
        //                        //set colour 4   alienexp4:  r:241    g: 201  b: 103 
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 201, 103));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(241, 201, 103))
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(242, 174, 44));
        //                        }
        //                        else
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, CLRTemp);
        //                        }
        //                    }

        //                    break;
        //                case 4:
        //                    //change exp4 colour - original: alienexp4:  r: 241   g: 201  b: 103
        //                    if (CLRTemp == System.Drawing.Color.FromArgb(241, 201, 103))
        //                    {
        //                        //set colour 1   alienexp1:  r: 241  g: 15  b: 15
        //                        BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 15, 15));
        //                    }
        //                    else
        //                    { //if already changed change back
        //                        if (CLRTemp == System.Drawing.Color.FromArgb(241, 15, 15))
        //                        {
        //                            BMPTemp1.SetPixel(intNum, intNum3, System.Drawing.Color.FromArgb(241, 201, 103));
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

        //    PICAlien.Image = BMPTemp1;
        //    PICAlien.Invalidate();
        //    intCurAlienExplosionAni++;

        //    if (intCurAlienExplosionAni > 4)
        //    {
        //        //stop timer remove alien
        //        TMRAnimateExplosion.Enabled = false;
        //        Destroy();
        //    }
        //}

        //private void AnimateAlien_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 01/10/2025 By Roger Williams

        //      animate alien

        //    */

        //    //animate
        //    switch (intCurAlienAni)
        //    {
        //        case 1:
        //            if (intThisAlienType == 1)
        //            {
        //                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_1;
        //            }
        //            else
        //            {
        //                if (intHealth == 2)
        //                { 
        //                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_1;
        //                }
        //                else
        //                {
        //                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_1_HIT;
        //                }
        //            }

        //            break;
        //        case 2:
        //            if (intThisAlienType == 1)
        //            {
        //                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_2;
        //            }
        //            else
        //            {
        //                if (intHealth == 2)
        //                {
        //                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_2;
        //                   }
        //                else
        //                {
        //                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_2_HIT;
        //                }
        //             }

        //            break;
        //        case 3:
        //            if (intThisAlienType == 1)
        //            {
        //                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_3;
        //            }
        //            else
        //            {
        //                if (intHealth == 2)
        //                {
        //                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_3;
        //                }
        //                else
        //                {
        //                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_3_HIT;
        //                }
        //            }

        //            break;
        //        case 4:
        //            if (intThisAlienType == 1)
        //            {
        //               PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_4;
        //            }
        //           else
        //            { 
        //                if (intHealth == 2)
        //                {
        //                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_4;
        //                }
        //                else
        //                {
        //                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_4_HIT;
        //                }
        //            }
        //            break;
        //    }

        //    intCurAlienAni++;

        //    if (intCurAlienAni > 4)
        //    {
        //        intCurAlienAni = 1;
        //    }
        //}




        public void AlienMove(int intLeftPos, int intTopPos)
        {
            /*
              Created 01/10/2025 By Roger Williams

              called by alienwave class moves alien

              VARS

              intleft       - start left pos in wave
              inttop        - start top pos in wave

              Note: if top = 0 don't change existing top value
            */

            try
            { 
            PICAlien.Left = intLeftPos;
            intLeft = intLeftPos;

            if (intTopPos != 0)
            { 
                PICAlien.Top = intTopPos;
                intTop = intTopPos;
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }


        public void AlienHit()
        {
            /*
              Created 28/09/2025 By Roger Williams

              processes alien hit

              if alien health = 0 alienwave class handles its destruction

            */

            try
            { 
                intHealth--;

                PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN_EXPLOSION_1;
                PICAlien.Load();
                PICAlien.BringToFront();
                BMPTemp1 = new Bitmap(PICAlien.Image);
                //start explosion timer

                //play hit sound
                SNDSoundHit.Position = Modules.clsModel.TMSStart;
                SNDSoundHit.Play();

                //stop standard animation timer
                //TMRAnimateAlien.Enabled = false;
                //TMRAnimateExplosion.Enabled = true;
                blnAsyncAnimateAlien = true;
                blnAsyncAnimateExplosion = false;
                AsyncAnimateExplosion();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        public clsAlien(int intAlienNbr, int intAlienType, int intLeftPos, int intTopPos) 
        {
            /*
              Created 28/09/2025 By Roger Williams

              creates class for an alien in the wave

              VARS

              intaliennbr   - which alien in the row isit? 
              intalientype  - type 1 or 2
              intleft       - start left pos in wave
              inttop        - start top pos in wave

            */

            intThisAlienType = intAlienType;
            
            switch (intAlienType)
            {
                case 1:
                    PICAlien = (PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + intAlienType + "_" + intAlienNbr];
                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN1_1;
                    intHealth = Modules.clsModel.CNST_INT_ALIEN1_HEALTH;
                    break;
                case 2:
                    PICAlien = (PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + intAlienType + "_" + intAlienNbr];
                    PICAlien.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIEN2_1;
                    intHealth = Modules.clsModel.CNST_INT_ALIEN2_HEALTH;
                    break;
            }

            PICAlien.Left = intLeftPos;
            PICAlien.Top = intTopPos;
            PICAlien.SizeMode = PictureBoxSizeMode.AutoSize;
            PICAlien.BringToFront();
            intLeft = intLeftPos;
            intTop = intTopPos;

            PICAlien.Visible = true;
            //init hit sound
            SNDSoundHit.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_ALIEN_HIT));
            SNDSoundHit.Volume = 1;
            //init timer animate
            blnAsyncAnimateExplosion = true;
            blnAsyncAnimateAlien = false;
            AsyncAnimateAlien();
        }
    }
}
