using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
/*
  Created 29/09/2025 By Roger Williams

  creates the base unit 

  also handles:

  - baseunit hit sound
  - baseunit picture (damage/no damage)

  each baseunit uses is two baseunit images:

  base_L_normal
  base_R_normal

*/

namespace RogInvaders2025.Modules
{
    internal class clsBaseUnit
    {
        private PictureBox PICBaseUnitLeft = new PictureBox();
        private PictureBox PICBaseUnitRight = new PictureBox();
        private MediaPlayer SNDSound = new System.Windows.Media.MediaPlayer();

        //public
        public int intBaseUnitNumber;
        public int intBaseUnitLeft_Left = 0;
        public int intBaseUnitRight_Left = 0;
        public int intBaseUnitDamageLeft = 1;  //inc when hit 1 = normal
        public int intBaseUnitDamageRight = 1;  //inc when hit 1 = normal
        public bool blnVisibleLeft = true;  //if destroyed this set to hidden - used by clsbaseall
        public bool blnVisibleRight = true;  //if destroyed this set to hidden - used by clsbaseall

        public void Destroy()
        {
            /*
              Created 29/09/2025 By Roger Williams

              destroys the class i.e. hides the image!

            */

            PICBaseUnitLeft.Visible = false;
            PICBaseUnitRight.Visible = false;
            blnVisibleLeft = false;
            blnVisibleRight = false;
            SNDSound.Stop();
        }
        public void BaseHit(int intSide)
        {
            /*
              Created 29/09/2025 By Roger Williams

              processes hit:

              - plays hit sound
              - changes picture to damage
              - dec baseunit health

              VARS

              intSide = 1 to left 2 = right


            */

            SNDSound.Position = Modules.clsModel.TMSStart;
            SNDSound.Play();

            if (intSide == 1)
            {
                intBaseUnitDamageLeft++;

                if (intBaseUnitDamageLeft == 7)
                {
                    PICBaseUnitLeft.Visible = false;
                    blnVisibleLeft = false;
                }
                else
                {
                    //change image
                    switch (intBaseUnitDamageLeft)
                    {
                        case 2:
                            PICBaseUnitLeft.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_L1;
                            break;
                        case 3:
                            PICBaseUnitLeft.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_L2;
                            break;
                        case 4:
                            PICBaseUnitLeft.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_L3;
                            break;
                        case 5:
                            PICBaseUnitLeft.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_L4;
                            break;
                        case 6:
                            PICBaseUnitLeft.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_L5;
                            break;
                    }
                }
            }
            else
            {
                intBaseUnitDamageRight++;

                if (intBaseUnitDamageRight == 7)
                {
                    PICBaseUnitRight.Visible = false;
                    blnVisibleRight = false;
                }
                else
                {
                    //change image
                    switch (intBaseUnitDamageRight)
                    {
                        case 2:
                            PICBaseUnitRight.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_R1;
                            break;
                        case 3:
                            PICBaseUnitRight.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_R2;
                            break;
                        case 4:
                            PICBaseUnitRight.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_R3;
                            break;
                        case 5:
                            PICBaseUnitRight.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_R4;
                            break;
                        case 6:
                            PICBaseUnitRight.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASEDAMAGE_R5;
                            break;
                    }
                }
            }

            if ((intBaseUnitDamageLeft == 7) && (intBaseUnitDamageRight == 7))
            {
                Destroy();
            }
        }

        public clsBaseUnit(int intNumber)
        { 
            //set public unit number
            intBaseUnitNumber = intNumber;
            //load image
            PICBaseUnitLeft = (PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNumber + "_L"];
            PICBaseUnitRight = (PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME + intNumber + "_R"];
            PICBaseUnitLeft.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASENORMAL_L;
            PICBaseUnitRight.ImageLocation = Modules.clsModel.CNST_STR_IMG_BASENORMAL_R;
            PICBaseUnitLeft.Width = Modules.clsView.CNST_INT_BASE_WIDTH;
            PICBaseUnitRight.Width = Modules.clsView.CNST_INT_BASE_WIDTH;
            PICBaseUnitLeft.Height= Modules.clsView.CNST_INT_BASE_HEIGHT;
            PICBaseUnitRight.Height = Modules.clsView.CNST_INT_BASE_HEIGHT;
            PICBaseUnitLeft.SizeMode = PictureBoxSizeMode.AutoSize;
            PICBaseUnitRight.SizeMode = PictureBoxSizeMode.AutoSize;

            //position image
            PICBaseUnitLeft.Top = Modules.clsView.CNST_INT_BASE_TOP;
            PICBaseUnitRight.Top = Modules.clsView.CNST_INT_BASE_TOP;

            switch (intNumber)
            {
                case 1:
                    PICBaseUnitLeft.Left = Modules.clsView.CNST_INT_BASE1_LEFT;
                    PICBaseUnitRight.Left = PICBaseUnitLeft.Left + Modules.clsView.CNST_INT_BASE_WIDTH;
                    break;
                case 2:
                    PICBaseUnitLeft.Left = Modules.clsView.CNST_INT_BASE2_LEFT + Modules.clsView.CNST_INT_BASE_SPACING;
                    PICBaseUnitRight.Left = PICBaseUnitLeft.Left + Modules.clsView.CNST_INT_BASE_WIDTH;
                    break;
                case 3:
                    PICBaseUnitLeft.Left = Modules.clsView.CNST_INT_BASE3_LEFT + Modules.clsView.CNST_INT_BASE_SPACING;
                    PICBaseUnitRight.Left = PICBaseUnitLeft.Left + Modules.clsView.CNST_INT_BASE_WIDTH;
                    break;
                case 4:
                    PICBaseUnitLeft.Left = Modules.clsView.CNST_INT_BASE4_LEFT + Modules.clsView.CNST_INT_BASE_SPACING;
                    PICBaseUnitRight.Left = PICBaseUnitLeft.Left + Modules.clsView.CNST_INT_BASE_WIDTH;
                    break;
                case 5:
                    PICBaseUnitLeft.Left = Modules.clsView.CNST_INT_BASE5_LEFT + Modules.clsView.CNST_INT_BASE_SPACING;
                    PICBaseUnitRight.Left = PICBaseUnitLeft.Left + Modules.clsView.CNST_INT_BASE_WIDTH;
                    break;
                case 6:
                    PICBaseUnitLeft.Left = Modules.clsView.CNST_INT_BASE6_LEFT + Modules.clsView.CNST_INT_BASE_SPACING;
                    PICBaseUnitRight.Left = PICBaseUnitLeft.Left + Modules.clsView.CNST_INT_BASE_WIDTH;
                    break;
            }

            intBaseUnitLeft_Left = PICBaseUnitLeft.Left;
            intBaseUnitRight_Left = PICBaseUnitRight.Left;

            //show
            PICBaseUnitLeft.Visible = true;
            PICBaseUnitRight.Visible = true;

            PICBaseUnitLeft.SendToBack();
            PICBaseUnitRight.SendToBack();

            //init sound
            SNDSound.Volume = 0.8;
            SNDSound.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_BASE_HIT));
        }
    }
}
