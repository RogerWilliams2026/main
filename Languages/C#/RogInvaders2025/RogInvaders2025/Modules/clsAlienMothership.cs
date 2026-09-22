using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
/*
  Created 29/09/2025 By Roger Williams

  alien mothership class

  - moves ship
  - handles hit

*/
namespace RogInvaders2025.Modules
{
    internal class clsAlienMothership
    {
        private int intShowMotherShipLeft = Modules.clsView.CNST_INT_ALIENMOTHERSHIPINTRO_LEFT;
        private int intAlienMothershipAnimation = 0;
        private int intCurAlienExplosionAni = 1;
        private bool blnAlienMotherShipSound = false;

        private PictureBox PICAlienMotherShip;
        //timers
        //private Timer TMRAlienMotherShipExplosion = new Timer();
        //private Timer TMRAlienMotherShipMove = new Timer();
        //private Timer TMRAlienMotherShipShow = new Timer();
        //private Timer TMRAnimateAlienMotherShip = new Timer();
        private bool blnAsyncAlienMotherShipExplosion = false;
        private bool blnAsyncAlienMotherShipMove = false;
        private bool blnAsyncAlienMotherShipShow = false;
        private bool blnAsyncAlienMotherShipAnimate = false;

        //sounds
        private MediaPlayer SNDSound = new System.Windows.Media.MediaPlayer();
        private MediaPlayer SNDSoundExplosion = new System.Windows.Media.MediaPlayer();

        public bool blnHit = false;

        //mediaplayer event
        private void Custom_MediaEnded(object sender, EventArgs e)
        {
            //tell class music stopped if ship not at left hand edge yet this 
            //makes sure music is played again!
            blnAlienMotherShipSound = false;
        }


        private async void AsyncAlienMotherShipExplosion()
        {
            /*
              Created 05/10/2025 By Roger Williams

              animate alien explosion

            */
            try
            { 
            while (!blnAsyncAlienMotherShipExplosion)
            {
                await Task.Delay(Modules.clsView.CNST_INT_ALIENMOTHERSHIP_EXPLOSION_INTERVAL);

                //animate
                switch (intCurAlienExplosionAni)
                {
                    case 1:
                        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_1;
                        break;
                    case 2:
                        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_2;
                        break;
                    case 3:
                        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_3;
                        break;
                    case 4:
                        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_4;
                        break;
                    case 5:
                        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_5;
                        break;
                    case 6:
                        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_6;
                        break;
                }

                intCurAlienExplosionAni++;

                if (intCurAlienExplosionAni > 6)
                {
                    //stop timer remove alien
                    //TMRAlienMotherShipExplosion.Enabled = false;
                    blnAsyncAlienMotherShipExplosion = true;
                    Destroy();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        private async void AsyncAlienMotherShipMove()
        {
            /*
              Created 08/09/2025 By Roger Williams

              animates the mothership and moves it

            */

            try
            { 
            while (!blnAsyncAlienMotherShipMove)
            {
                await Task.Delay(Modules.clsView.CNST_INT_ALIENMOTHERSHIP_MOVE_INTERVAL);

                //play alien mothership sound
                if (blnAlienMotherShipSound == false)
                {
                    blnAlienMotherShipSound = true;
                    //play sound
                    SNDSound.Position = Modules.clsModel.TMSStart;
                    SNDSound.Play();
                }

                //move mothership
                PICAlienMotherShip.Left = intShowMotherShipLeft;
                intShowMotherShipLeft = intShowMotherShipLeft - 50;

                if (intShowMotherShipLeft == -300)
                {
                    blnAlienMotherShipSound = false;
                    Destroy();
                }

                //animate alien for intro
                if (intAlienMothershipAnimation == 4)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_5;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 3)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_4;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 2)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_3;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 1)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_2;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 0)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_1;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 5)
                {
                    intAlienMothershipAnimation = 0;
                }
                else
                {
                    PICAlienMotherShip.Load();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        private async void AsyncAnimateAlienMotherShip()
        {
            /*
              Created 10/10/2025 By Roger Williams

              animates the mothership
              custom event called by frmgame->showalieninvasion()

            */

            try
            { 
            while (!blnAsyncAlienMotherShipAnimate)
            {
                await Task.Delay(Modules.clsView.CNST_INT_ALIENMOTHERSHIP_MOVE_INTERVAL);

                //play alien mothership sound
                if (blnAlienMotherShipSound == false)
                {
                    blnAlienMotherShipSound = true;
                    //play sound
                    SNDSound.Position = Modules.clsModel.TMSStart;
                    SNDSound.Play();
                }

                //animate alien for intro
                if (intAlienMothershipAnimation == 4)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_5;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 3)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_4;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 2)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_3;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 1)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_2;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 0)
                {
                    PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_1;
                    intAlienMothershipAnimation++;
                }
                if (intAlienMothershipAnimation == 5)
                {
                    intAlienMothershipAnimation = 0;
                }
                else
                {
                    PICAlienMotherShip.Load();
                }
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }
        private async void AsyncAlienMotherShipShow()
        {
            /*
              Created 29/09/2025 By Roger Williams

              shows the mothership and starts movement timer

            */
            try { 
            while (!blnAsyncAlienMotherShipShow)
            { 
                await Task.Delay(Modules.clsView.CNST_INT_ALIENMOTHERSHIP_GAME_INTERVAL);
                //init position and show
                PICAlienMotherShip.Left = intShowMotherShipLeft;
                PICAlienMotherShip.Top = Modules.clsView.CNST_INT_ALIENMOTHERSHIP_TOP;
                PICAlienMotherShip.Visible = true;
                //start movement/animation timer
                //TMRAlienMotherShipMove.Enabled = true;
                blnAsyncAlienMotherShipMove = false;
                //stop this timer
                //TMRAlienMotherShipShow.Enabled = false;
                blnAsyncAlienMotherShipShow = true;
                AsyncAlienMotherShipMove();
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        //private void AlienMotherShipExplosion_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 05/10/2025 By Roger Williams

        //      animate alien explosion

        //    */

        //    //animate
        //    switch (intCurAlienExplosionAni)
        //    {
        //        case 1:
        //            PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_1;
        //            break;
        //        case 2:
        //            PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_2;
        //            break;
        //        case 3:
        //            PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_3;
        //            break;
        //        case 4:
        //            PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_4;
        //            break;
        //        case 5:
        //            PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_5;
        //            break;
        //        case 6:
        //            PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_6;
        //            break;
        //    }

        //    intCurAlienExplosionAni++;

        //    if (intCurAlienExplosionAni > 6)
        //    {
        //        //stop timer remove alien
        //        TMRAlienMotherShipExplosion.Enabled = false;
        //        Destroy();
        //    }
        //}

        //private void TMRAlienMotherShipMove_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 08/09/2025 By Roger Williams

        //      animates the mothership and moves it

        //    */

        //    //play alien mothership sound
        //    if (blnAlienMotherShipSound == false)
        //    {
        //        blnAlienMotherShipSound = true;
        //        //play sound
        //        SNDSound.Position = Modules.clsModel.TMSStart;
        //        SNDSound.Play();
        //    }

        //    //move mothership
        //    PICAlienMotherShip.Left = intShowMotherShipLeft;
        //    intShowMotherShipLeft = intShowMotherShipLeft - 50;

        //    if (intShowMotherShipLeft == -300)
        //    {
        //        blnAlienMotherShipSound = false;
        //        Destroy();
        //    }

        //    //animate alien for intro
        //    if (intAlienMothershipAnimation == 4)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_5;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 3)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_4;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 2)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_3;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 1)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_2;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 0)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_1;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 5)
        //    {
        //        intAlienMothershipAnimation = 0;
        //    }
        //    else
        //    {
        //        PICAlienMotherShip.Load();
        //    }
        //}

        //private void TMRAnimateAlienMotherShip_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 10/10/2025 By Roger Williams

        //      animates the mothership
        //      custom event called by frmgame->showalieninvasion()

        //    */

        //    //play alien mothership sound
        //    if (blnAlienMotherShipSound == false)
        //    {
        //        blnAlienMotherShipSound = true;
        //        //play sound
        //        SNDSound.Position = Modules.clsModel.TMSStart;
        //        SNDSound.Play();
        //    }

        //     //animate alien for intro
        //    if (intAlienMothershipAnimation == 4)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_5;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 3)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_4;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 2)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_3;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 1)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_2;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 0)
        //    {
        //        PICAlienMotherShip.ImageLocation = Modules.clsModel.CNST_STR_IMG_ALIENMOTHERSHIP_1;
        //        intAlienMothershipAnimation++;
        //    }
        //    if (intAlienMothershipAnimation == 5)
        //    {
        //        intAlienMothershipAnimation = 0;
        //    }
        //    else
        //    {
        //        PICAlienMotherShip.Load();
        //    }
        //}
        //private void TMRAlienMotherShipShow_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 29/09/2025 By Roger Williams

        //      shows the mothership and starts movement timer

        //    */

        //    //init position and show
        //    PICAlienMotherShip.Left = intShowMotherShipLeft;
        //    PICAlienMotherShip.Top = Modules.clsView.CNST_INT_ALIENMOTHERSHIP_TOP;
        //    PICAlienMotherShip.Visible = true;
        //    //start movement/animation timer
        //    TMRAlienMotherShipMove.Enabled = true;
        //    //stop this timer
        //    TMRAlienMotherShipShow.Enabled = false;
        //}
        public void MothershipHit()
        {
            /*
              Created 29/09/2025 By Roger Williams

              mothership explosiont

            */
            blnHit = true;
            //stop movement
            //TMRAlienMotherShipMove.Enabled = false;
            blnAsyncAlienMotherShipMove = true;
            SNDSound.Stop();

            //play explosion sound
            SNDSoundExplosion.Volume = 1;
            SNDSoundExplosion.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_ALIENMOTHERSHIP_HIT));
            SNDSoundExplosion.Play();
            //start explosion timer
            //TMRAlienMotherShipExplosion.Enabled = true;
            blnAsyncAlienMotherShipExplosion = false;
            AsyncAlienMotherShipExplosion();
        }

        public void StopMoveShowTimer()
        {
            /*
              Created 10/10/2025 By Roger Williams

              called by frmgame stops movement/show timer

            */
            blnAsyncAlienMotherShipMove = true;
            blnAsyncAlienMotherShipShow = true;
            //TMRAlienMotherShipShow.Enabled = false;
            //TMRAlienMotherShipMove.Enabled = false;
        }
        public void StartAnimationAndSound()
        {
            /*
              Created 10/10/2025 By Roger Williams

              called by frmgame starts custom animation timer used ONLY by frmgame
              plays movement sound

            */

            SNDSound.Position = Modules.clsModel.TMSStart;
            SNDSound.Play();
           // TMRAnimateAlienMotherShip.Enabled = true;
            blnAsyncAlienMotherShipAnimate = false;
            AsyncAnimateAlienMotherShip();
        }
        public void StopAnimationAndSound()
        {
            /*
              Created 10/10/2025 By Roger Williams

              called by frmgame stops custom animation timer used ONLY by frmgame
              stops movement sound

            */

            SNDSound.Stop();
            //TMRAnimateAlienMotherShip.Enabled = false;
            blnAsyncAlienMotherShipAnimate = true;
        }

        public void StopSound()
        {
            /*
              Created 10/10/2025 By Roger Williams

              called by frmgame stops stops movement sound

            */

            SNDSound.Stop();
        }
        public void Destroy()
        {
            /*
              Created 29/09/2025 By Roger Williams

              destroys class elements

            */

            SNDSound.Stop();
            PICAlienMotherShip.Visible = false;
            //stop timers
            //TMRAlienMotherShipMove.Enabled = false;
            //TMRAnimateAlienMotherShip.Enabled = false;
            //TMRAlienMotherShipShow.Enabled = false;
            //TMRAlienMotherShipExplosion.Enabled= false;
            blnAsyncAlienMotherShipAnimate = true;
            blnAsyncAlienMotherShipExplosion = true;
            blnAsyncAlienMotherShipMove = true;
            blnAsyncAlienMotherShipShow = true;
        }
        public clsAlienMothership()
        {
            PICAlienMotherShip = (PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls["PICAlienMotherShip"];
            //init mothership show timer
            blnAsyncAlienMotherShipShow = false;
            AsyncAlienMotherShipShow();

            //init sound
            SNDSound.MediaEnded += Custom_MediaEnded;
            SNDSound.Volume = 0.8;
            SNDSound.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_ALIENMOTHERSHIP));
        }
    }

}
