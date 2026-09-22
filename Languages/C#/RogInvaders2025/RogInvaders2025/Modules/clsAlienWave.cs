using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

/*
  Created 18/09/2025 By Roger Williams

  class for each alien wave - comprises of multiple alien classes in 2 rows of 6

  2 different alien types each has 4 animations

  moves each alien in the wave!
  also handles destroying the alien if health = 0

  type 1 alien needs TWO hits
  type 2 alien needs ONE hit

  also controls WHICH alien shoots!

*/
namespace RogInvaders2025.Modules
{
    internal class clsAlienWave
    {
        private clsAlien clsAlienType1_Row1_1;
        private clsAlien clsAlienType1_Row1_2;
        private clsAlien clsAlienType1_Row1_3;
        private clsAlien clsAlienType1_Row1_4;
        private clsAlien clsAlienType1_Row1_5;
        private clsAlien clsAlienType1_Row1_6;

        private clsAlien clsAlienType2_Row2_1;
        private clsAlien clsAlienType2_Row2_2;
        private clsAlien clsAlienType2_Row2_3;
        private clsAlien clsAlienType2_Row2_4;
        private clsAlien clsAlienType2_Row2_5;
        private clsAlien clsAlienType2_Row2_6;

        private MediaPlayer SNDSoundMove = new System.Windows.Media.MediaPlayer();
        private int intWaveDirection = 1; //1 = right 2 = left
        private int intGameLevel = 0;

        //  private Timer TMRMoveAliens = new Timer();
        private bool blnAsyncMoveAliens = false;

        public bool blnWaveDestroyed = false;
        public int intAliensRow2 = 6;
        public int intAliensRow1 = 6;

        private async void AsyncMoveAliens()
        {
            /*
              Created 01/10/2025 By Roger Williams

              moves each visible alien in the wave

              alien rows move to the right, then when at right hand edge
              inc top and moves to left, when closest alien to the left
              hand sides left = 0 inc top and repeat process

            */
            int intLeft = 0;
            int intTop = 0;

            int GetFirstAlienLeft()
            {
                /*
                   Created 01/10/2025 By Roger Williams

                   finds first visible alien and get its left   

                */
                int intNum = 0;
                int intLeftFound = 0;

                for (intNum = 1; intNum != 7; intNum++)
                {
                    if (((PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum]).Visible)
                    {
                        intLeftFound = ((PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "1_" + intNum]).Left;
                        break;
                    }

                    if (((PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum]).Visible)
                    {
                        intLeftFound = ((PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum]).Left;
                        break;
                    }
                }

                return intLeftFound;
            }

            int GetLastAlienLeft()
            {
                /*
                   Created 01/10/2025 By Roger Williams

                   finds last visible (right hand side) alien and get its left   

                */
                int intNum = 0;

                //start with row2
                if (clsAlienType2_Row2_1.blnVisible)
                {
                    intNum = clsAlienType2_Row2_1.intLeft;
                }
                if (clsAlienType2_Row2_2.blnVisible)
                {
                    intNum = clsAlienType2_Row2_2.intLeft;
                }
                if (clsAlienType2_Row2_3.blnVisible)
                {
                    intNum = clsAlienType2_Row2_3.intLeft;
                }
                if (clsAlienType2_Row2_4.blnVisible)
                {
                    intNum = clsAlienType2_Row2_4.intLeft;
                }
                if (clsAlienType2_Row2_5.blnVisible)
                {
                    intNum = clsAlienType2_Row2_5.intLeft;
                }
                if (clsAlienType2_Row2_6.blnVisible)
                {
                    intNum = clsAlienType2_Row2_6.intLeft;
                }

                //if no aliens on second row check first!
                if (intNum == 0)
                {
                    if (clsAlienType1_Row1_1.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_1.intLeft;
                    }
                    if (clsAlienType1_Row1_2.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_2.intLeft;
                    }
                    if (clsAlienType1_Row1_3.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_3.intLeft;
                    }
                    if (clsAlienType1_Row1_4.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_4.intLeft;
                    }
                    if (clsAlienType1_Row1_5.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_5.intLeft;
                    }
                    if (clsAlienType1_Row1_6.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_6.intLeft;
                    }
                }
                return intNum;
            }

            int GetLowestAlien()
            {
                /*
                   Created 01/10/2025 By Roger Williams

                   finds last visible alien and get its top   

                */
                int intNum = 0;

                //start with row2
                if (clsAlienType2_Row2_1.blnVisible)
                {
                    intNum = clsAlienType2_Row2_1.intTop;
                }
                if (clsAlienType2_Row2_2.blnVisible)
                {
                    intNum = clsAlienType2_Row2_2.intTop;
                }
                if (clsAlienType2_Row2_3.blnVisible)
                {
                    intNum = clsAlienType2_Row2_3.intTop;
                }
                if (clsAlienType2_Row2_4.blnVisible)
                {
                    intNum = clsAlienType2_Row2_4.intTop;
                }
                if (clsAlienType2_Row2_5.blnVisible)
                {
                    intNum = clsAlienType2_Row2_5.intTop;
                }
                if (clsAlienType2_Row2_6.blnVisible)
                {
                    intNum = clsAlienType2_Row2_6.intTop;
                }

                //if no aliens on second row check first!
                if (intNum == 0)
                {
                    if (clsAlienType1_Row1_1.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_1.intTop;
                    }
                    if (clsAlienType1_Row1_2.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_2.intTop;
                    }
                    if (clsAlienType1_Row1_3.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_3.intTop;
                    }
                    if (clsAlienType1_Row1_4.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_4.intTop;
                    }
                    if (clsAlienType1_Row1_5.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_5.intTop;
                    }
                    if (clsAlienType1_Row1_6.blnVisible)
                    {
                        intNum = clsAlienType1_Row1_6.intTop;
                    }
                }
                return intNum;
            }

            void MoveAliensTopOnly(int intRow1Top, int intRow2Top)
            {
                /*
                   Created 02/10/2025 By Roger Williams

                   moves the alien wave but ONLY alters top positon

                   used when alienwave raches edge of screen

                */

                //row 1
                if (clsAlienType1_Row1_1.blnVisible)
                {
                    clsAlienType1_Row1_1.AlienMove(clsAlienType1_Row1_1.intLeft, intRow1Top);
                }
                if (clsAlienType1_Row1_2.blnVisible)
                {
                    clsAlienType1_Row1_2.AlienMove(clsAlienType1_Row1_2.intLeft, intRow1Top);
                }
                if (clsAlienType1_Row1_3.blnVisible)
                {
                    clsAlienType1_Row1_3.AlienMove(clsAlienType1_Row1_3.intLeft, intRow1Top);
                }
                if (clsAlienType1_Row1_4.blnVisible)
                {
                    clsAlienType1_Row1_4.AlienMove(clsAlienType1_Row1_4.intLeft, intRow1Top);
                }
                if (clsAlienType1_Row1_5.blnVisible)
                {
                    clsAlienType1_Row1_5.AlienMove(clsAlienType1_Row1_5.intLeft, intRow1Top);
                }
                if (clsAlienType1_Row1_6.blnVisible)
                {
                    clsAlienType1_Row1_6.AlienMove(clsAlienType1_Row1_6.intLeft, intRow1Top);
                }
                //row 2
                if (clsAlienType2_Row2_1.blnVisible)
                {
                    clsAlienType2_Row2_1.AlienMove(clsAlienType2_Row2_1.intLeft, intRow2Top);
                }
                if (clsAlienType2_Row2_2.blnVisible)
                {
                    clsAlienType2_Row2_2.AlienMove(clsAlienType2_Row2_2.intLeft, intRow2Top);
                }
                if (clsAlienType2_Row2_3.blnVisible)
                {
                    clsAlienType2_Row2_3.AlienMove(clsAlienType2_Row2_3.intLeft, intRow2Top);
                }
                if (clsAlienType2_Row2_4.blnVisible)
                {
                    clsAlienType2_Row2_4.AlienMove(clsAlienType2_Row2_4.intLeft, intRow2Top);
                }
                if (clsAlienType2_Row2_5.blnVisible)
                {
                    clsAlienType2_Row2_5.AlienMove(clsAlienType2_Row2_5.intLeft, intRow2Top);
                }
                if (clsAlienType2_Row2_6.blnVisible)
                {
                    clsAlienType2_Row2_6.AlienMove(clsAlienType2_Row2_6.intLeft, intRow2Top);
                }
            }

            void MoveAliens(int intRow1Top, int intRow2Top)
            {
                /*
                   Created 02/10/2025 By Roger Williams

                   moves the alien wave left/right depnding on direction

                   VARS


                   introw1top   - row1 top pos
                   introw2top   - row 2 top pos <- always above plus alien height

                */

                if (intWaveDirection == 1)
                {
                    //row 1
                    if (clsAlienType1_Row1_1.blnVisible)
                    {
                        clsAlienType1_Row1_1.AlienMove(clsAlienType1_Row1_1.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_2.blnVisible)
                    {
                        clsAlienType1_Row1_2.AlienMove(clsAlienType1_Row1_2.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_3.blnVisible)
                    {
                        clsAlienType1_Row1_3.AlienMove(clsAlienType1_Row1_3.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_4.blnVisible)
                    {
                        clsAlienType1_Row1_4.AlienMove(clsAlienType1_Row1_4.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_5.blnVisible)
                    {
                        clsAlienType1_Row1_5.AlienMove(clsAlienType1_Row1_5.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_6.blnVisible)
                    {
                        clsAlienType1_Row1_6.AlienMove(clsAlienType1_Row1_6.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    //row 2
                    if (clsAlienType2_Row2_1.blnVisible)
                    {
                        clsAlienType2_Row2_1.AlienMove(clsAlienType2_Row2_1.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_2.blnVisible)
                    {
                        clsAlienType2_Row2_2.AlienMove(clsAlienType2_Row2_2.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_3.blnVisible)
                    {
                        clsAlienType2_Row2_3.AlienMove(clsAlienType2_Row2_3.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_4.blnVisible)
                    {
                        clsAlienType2_Row2_4.AlienMove(clsAlienType2_Row2_4.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_5.blnVisible)
                    {
                        clsAlienType2_Row2_5.AlienMove(clsAlienType2_Row2_5.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_6.blnVisible)
                    {
                        clsAlienType2_Row2_6.AlienMove(clsAlienType2_Row2_6.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                }
                else
                {
                    //row 1
                    if (clsAlienType1_Row1_1.blnVisible)
                    {
                        clsAlienType1_Row1_1.AlienMove(clsAlienType1_Row1_1.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_2.blnVisible)
                    {
                        clsAlienType1_Row1_2.AlienMove(clsAlienType1_Row1_2.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_3.blnVisible)
                    {
                        clsAlienType1_Row1_3.AlienMove(clsAlienType1_Row1_3.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_4.blnVisible)
                    {
                        clsAlienType1_Row1_4.AlienMove(clsAlienType1_Row1_4.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_5.blnVisible)
                    {
                        clsAlienType1_Row1_5.AlienMove(clsAlienType1_Row1_5.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    if (clsAlienType1_Row1_6.blnVisible)
                    {
                        clsAlienType1_Row1_6.AlienMove(clsAlienType1_Row1_6.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
                    }
                    //row 2
                    if (clsAlienType2_Row2_1.blnVisible)
                    {
                        clsAlienType2_Row2_1.AlienMove(clsAlienType2_Row2_1.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_2.blnVisible)
                    {
                        clsAlienType2_Row2_2.AlienMove(clsAlienType2_Row2_2.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_3.blnVisible)
                    {
                        clsAlienType2_Row2_3.AlienMove(clsAlienType2_Row2_3.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_4.blnVisible)
                    {
                        clsAlienType2_Row2_4.AlienMove(clsAlienType2_Row2_4.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_5.blnVisible)
                    {
                        clsAlienType2_Row2_5.AlienMove(clsAlienType2_Row2_5.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                    if (clsAlienType2_Row2_6.blnVisible)
                    {
                        clsAlienType2_Row2_6.AlienMove(clsAlienType2_Row2_6.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
                    }
                }
            }

            try
            {
                while (!blnAsyncMoveAliens)
                {
                    switch (intGameLevel)
                    {
                        case 1:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENMOVE_LEVEL1_INTERVAL);
                            break;
                        case 2:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENMOVE_LEVEL2_INTERVAL);
                            break;
                        case 3:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENMOVE_LEVEL3_INTERVAL);
                            break;
                        case 4:
                            await Task.Delay(Modules.clsView.CNST_INT_ALIENMOVE_LEVEL4_INTERVAL);
                            break;
                    }

                    //play move sound
                    SNDSoundMove.Position = Modules.clsModel.TMSStart;
                    SNDSoundMove.Play();
                    //move aliens
                    if (intWaveDirection == 1)
                    {
                        //find last alien visible at right hand side
                        intLeft = GetLastAlienLeft();

                        //check if at right hand edge
                        if (intLeft + Modules.clsView.CNST_INT_ALIEN_WIDTH + 10 >= Modules.clsView.CNST_INT_SCREEN_WIDTH)
                        {
                            intTop = GetLowestAlien();
                            intWaveDirection = 2;
                            MoveAliensTopOnly(intTop, intTop + Modules.clsView.CNST_INT_ALIEN_HEIGHT);
                        }
                        else
                        {
                            MoveAliens(0, 0);
                        }
                    }
                    else
                    {
                        //find first alien visible at left hand side
                        //start with row 1 as last to get destroyed plus takes two hits to kill each alien!

                        //find first alien visible at right hand side
                        intLeft = GetFirstAlienLeft();

                        //check if at left hand edge
                        if (intLeft <= 0)
                        {
                            intTop = GetLowestAlien();
                            MoveAliensTopOnly(intTop, intTop + Modules.clsView.CNST_INT_ALIEN_HEIGHT);
                            intWaveDirection = 1;
                        }
                        else
                        {
                            MoveAliens(0, 0);
                        }
                    }

                    //check if intTop >= base!
                    if (intTop >= Modules.clsView.CNST_INT_BASE_TOP)
                    {
                        //stop moving aliens
                        //TMRMoveAliens.Enabled = false;
                        blnAsyncMoveAliens = true;
                        //game over for player!
                        ((frmGame)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM]).AliensReachedBase();
                    }
                }
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }


        //private void MoveAliens_Tick(object sender, EventArgs e)
        //{
        //    /*
        //      Created 01/10/2025 By Roger Williams

        //      moves each visible alien in the wave

        //      alien rows move to the right, then when at right hand edge
        //      inc top and moves to left, when closest alien to the left
        //      hand sides left = 0 inc top and repeat process

        //    */
        //    int intLeft = 0;
        //    int intTop = 0;

        //    int GetFirstAlienLeft()
        //    {
        //        /*
        //           Created 01/10/2025 By Roger Williams

        //           finds first visible alien and get its left   

        //        */
        //        int intNum = 0;
        //        int intLeftFound = 0;

        //        for (intNum = 1; intNum !=7;intNum++)
        //        {
        //            if (((PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum]).Visible)
        //            {
        //                intLeftFound = ((PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum]).Left;
        //                break;
        //            }

        //            if (((PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum]).Visible)
        //            {
        //                intLeftFound = ((PictureBox)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM].Controls[Modules.clsModel.CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME + "2_" + intNum]).Left;
        //                break;
        //            }
        //        }

        //        return intLeftFound;
        //    }

        //    int GetLastAlienLeft()
        //    {
        //        /*
        //           Created 01/10/2025 By Roger Williams

        //           finds last visible (right hand side) alien and get its left   

        //        */
        //        int intNum = 0;

        //        //start with row2
        //        if (clsAlienType2_Row2_1.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_1.intLeft;
        //        }
        //        if (clsAlienType2_Row2_2.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_2.intLeft;
        //        }
        //        if (clsAlienType2_Row2_3.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_3.intLeft;
        //        }
        //        if (clsAlienType2_Row2_4.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_4.intLeft;
        //        }
        //        if (clsAlienType2_Row2_5.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_5.intLeft;
        //        }
        //        if (clsAlienType2_Row2_6.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_6.intLeft;
        //        }

        //        //if no aliens on second row check first!
        //        if (intNum == 0)
        //        {
        //            if (clsAlienType1_Row1_1.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_1.intLeft;
        //            }
        //            if (clsAlienType1_Row1_2.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_2.intLeft;
        //            }
        //            if (clsAlienType1_Row1_3.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_3.intLeft;
        //            }
        //            if (clsAlienType1_Row1_4.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_4.intLeft;
        //            }
        //            if (clsAlienType1_Row1_5.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_5.intLeft;
        //            }
        //            if (clsAlienType1_Row1_6.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_6.intLeft;
        //            }
        //        }
        //        return intNum;
        //    }

        //    int GetLowestAlien()
        //    {
        //        /*
        //           Created 01/10/2025 By Roger Williams

        //           finds last visible alien and get its top   

        //        */
        //        int intNum = 0;

        //        //start with row2
        //        if (clsAlienType2_Row2_1.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_1.intTop;
        //        }
        //        if (clsAlienType2_Row2_2.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_2.intTop;
        //        }
        //        if (clsAlienType2_Row2_3.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_3.intTop;
        //        }
        //        if (clsAlienType2_Row2_4.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_4.intTop;
        //        }
        //        if (clsAlienType2_Row2_5.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_5.intTop;
        //        }
        //        if (clsAlienType2_Row2_6.blnVisible)
        //        {
        //            intNum = clsAlienType2_Row2_6.intTop;
        //        }

        //        //if no aliens on second row check first!
        //        if (intNum == 0)
        //        {
        //            if (clsAlienType1_Row1_1.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_1.intTop;
        //            }
        //            if (clsAlienType1_Row1_2.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_2.intTop;
        //            }
        //            if (clsAlienType1_Row1_3.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_3.intTop;
        //            }
        //            if (clsAlienType1_Row1_4.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_4.intTop;
        //            }
        //            if (clsAlienType1_Row1_5.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_5.intTop;
        //            }
        //            if (clsAlienType1_Row1_6.blnVisible)
        //            {
        //                intNum = clsAlienType1_Row1_6.intTop;
        //            }
        //        }
        //        return intNum;
        //    }

        //    void MoveAliensTopOnly(int intRow1Top, int intRow2Top)
        //    {
        //        /*
        //           Created 02/10/2025 By Roger Williams

        //           moves the alien wave but ONLY alters top positon

        //           used when alienwave raches edge of screen

        //        */

        //            //row 1
        //            if (clsAlienType1_Row1_1.blnVisible)
        //            {
        //                clsAlienType1_Row1_1.AlienMove(clsAlienType1_Row1_1.intLeft, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_2.blnVisible)
        //            {
        //                clsAlienType1_Row1_2.AlienMove(clsAlienType1_Row1_2.intLeft, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_3.blnVisible)
        //            {
        //                clsAlienType1_Row1_3.AlienMove(clsAlienType1_Row1_3.intLeft, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_4.blnVisible)
        //            {
        //                clsAlienType1_Row1_4.AlienMove(clsAlienType1_Row1_4.intLeft, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_5.blnVisible)
        //            {
        //                clsAlienType1_Row1_5.AlienMove(clsAlienType1_Row1_5.intLeft, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_6.blnVisible)
        //            {
        //                clsAlienType1_Row1_6.AlienMove(clsAlienType1_Row1_6.intLeft, intRow1Top);
        //            }
        //            //row 2
        //            if (clsAlienType2_Row2_1.blnVisible)
        //            {
        //                clsAlienType2_Row2_1.AlienMove(clsAlienType2_Row2_1.intLeft, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_2.blnVisible)
        //            {
        //                clsAlienType2_Row2_2.AlienMove(clsAlienType2_Row2_2.intLeft, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_3.blnVisible)
        //            {
        //                clsAlienType2_Row2_3.AlienMove(clsAlienType2_Row2_3.intLeft, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_4.blnVisible)
        //            {
        //                clsAlienType2_Row2_4.AlienMove(clsAlienType2_Row2_4.intLeft, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_5.blnVisible)
        //            {
        //                clsAlienType2_Row2_5.AlienMove(clsAlienType2_Row2_5.intLeft, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_6.blnVisible)
        //            {
        //                clsAlienType2_Row2_6.AlienMove(clsAlienType2_Row2_6.intLeft, intRow2Top);
        //            }
        //    }

        //    void MoveAliens(int intRow1Top, int intRow2Top)
        //    {
        //        /*
        //           Created 02/10/2025 By Roger Williams

        //           moves the alien wave left/right depnding on direction

        //           VARS


        //           introw1top   - row1 top pos
        //           introw2top   - row 2 top pos <- always above plus alien height

        //        */

        //        if (intWaveDirection == 1)
        //        { 
        //        //row 1
        //        if (clsAlienType1_Row1_1.blnVisible)
        //        {
        //            clsAlienType1_Row1_1.AlienMove(clsAlienType1_Row1_1.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //        }
        //        if (clsAlienType1_Row1_2.blnVisible)
        //        {
        //            clsAlienType1_Row1_2.AlienMove(clsAlienType1_Row1_2.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //        }
        //        if (clsAlienType1_Row1_3.blnVisible)
        //        {
        //            clsAlienType1_Row1_3.AlienMove(clsAlienType1_Row1_3.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //        }
        //        if (clsAlienType1_Row1_4.blnVisible)
        //        {
        //            clsAlienType1_Row1_4.AlienMove(clsAlienType1_Row1_4.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //        }
        //        if (clsAlienType1_Row1_5.blnVisible)
        //        {
        //            clsAlienType1_Row1_5.AlienMove(clsAlienType1_Row1_5.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //        }
        //        if (clsAlienType1_Row1_6.blnVisible)
        //        {
        //            clsAlienType1_Row1_6.AlienMove(clsAlienType1_Row1_6.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //        }
        //        //row 2
        //        if (clsAlienType2_Row2_1.blnVisible)
        //        {
        //            clsAlienType2_Row2_1.AlienMove(clsAlienType2_Row2_1.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //        }
        //        if (clsAlienType2_Row2_2.blnVisible)
        //        {
        //            clsAlienType2_Row2_2.AlienMove(clsAlienType2_Row2_2.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //        }
        //        if (clsAlienType2_Row2_3.blnVisible)
        //        {
        //            clsAlienType2_Row2_3.AlienMove(clsAlienType2_Row2_3.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //        }
        //        if (clsAlienType2_Row2_4.blnVisible)
        //        {
        //            clsAlienType2_Row2_4.AlienMove(clsAlienType2_Row2_4.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //        }
        //        if (clsAlienType2_Row2_5.blnVisible)
        //        {
        //            clsAlienType2_Row2_5.AlienMove(clsAlienType2_Row2_5.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //        }
        //        if (clsAlienType2_Row2_6.blnVisible)
        //        {
        //            clsAlienType2_Row2_6.AlienMove(clsAlienType2_Row2_6.intLeft + Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //        }
        //    }
        //    else
        //        {
        //            //row 1
        //            if (clsAlienType1_Row1_1.blnVisible)
        //            {
        //                clsAlienType1_Row1_1.AlienMove(clsAlienType1_Row1_1.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_2.blnVisible)
        //            {
        //                clsAlienType1_Row1_2.AlienMove(clsAlienType1_Row1_2.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_3.blnVisible)
        //            {
        //                clsAlienType1_Row1_3.AlienMove(clsAlienType1_Row1_3.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_4.blnVisible)
        //            {
        //                clsAlienType1_Row1_4.AlienMove(clsAlienType1_Row1_4.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_5.blnVisible)
        //            {
        //                clsAlienType1_Row1_5.AlienMove(clsAlienType1_Row1_5.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //            }
        //            if (clsAlienType1_Row1_6.blnVisible)
        //            {
        //                clsAlienType1_Row1_6.AlienMove(clsAlienType1_Row1_6.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow1Top);
        //            }
        //            //row 2
        //            if (clsAlienType2_Row2_1.blnVisible)
        //            {
        //                clsAlienType2_Row2_1.AlienMove(clsAlienType2_Row2_1.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_2.blnVisible)
        //            {
        //                clsAlienType2_Row2_2.AlienMove(clsAlienType2_Row2_2.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_3.blnVisible)
        //            {
        //                clsAlienType2_Row2_3.AlienMove(clsAlienType2_Row2_3.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_4.blnVisible)
        //            {
        //                clsAlienType2_Row2_4.AlienMove(clsAlienType2_Row2_4.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_5.blnVisible)
        //            {
        //                clsAlienType2_Row2_5.AlienMove(clsAlienType2_Row2_5.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //            }
        //            if (clsAlienType2_Row2_6.blnVisible)
        //            {
        //                clsAlienType2_Row2_6.AlienMove(clsAlienType2_Row2_6.intLeft - Modules.clsView.CNST_INT_ALIEN_MOVEAMOUNT, intRow2Top);
        //            }
        //        }
        //    }

        //    //play move sound
        //    SNDSoundMove.Position = Modules.clsModel.TMSStart;
        //    SNDSoundMove.Play();
        //    //move aliens
        //    if (intWaveDirection == 1)
        //    {
        //        //find last alien visible at right hand side
        //        intLeft = GetLastAlienLeft();

        //        //check if at right hand edge
        //        if (intLeft + Modules.clsView.CNST_INT_ALIEN_WIDTH + 10 >= Modules.clsView.CNST_INT_SCREEN_WIDTH)
        //        {
        //            intTop = GetLowestAlien();
        //            intWaveDirection = 2;
        //            MoveAliensTopOnly(intTop, intTop + Modules.clsView.CNST_INT_ALIEN_HEIGHT);
        //        }
        //        else
        //        {
        //            MoveAliens(0,0);
        //        }
        //    }
        //    else
        //    {
        //        //find first alien visible at left hand side
        //        //start with row 1 as last to get destroyed plus takes two hits to kill each alien!

        //        //find first alien visible at right hand side
        //        intLeft = GetFirstAlienLeft();

        //        //check if at left hand edge
        //        if (intLeft <= 0)
        //        {
        //            intTop = GetLowestAlien();
        //            MoveAliensTopOnly(intTop, intTop + Modules.clsView.CNST_INT_ALIEN_HEIGHT);
        //            intWaveDirection = 1;
        //        }
        //        else
        //        {
        //            MoveAliens(0,0);
        //        }
        //    }

        //    //check if intTop >= base!
        //    if (intTop >= Modules.clsView.CNST_INT_BASE_TOP)
        //    {
        //        //stop moving aliens
        //        TMRMoveAliens.Enabled = false;
        //        //game over for player!
        //        ((frmGame)Application.OpenForms[Modules.clsModel.CNST_STR_FRM_MAINGAMEFORM]).AliensReachedBase();
        //    }
        //}


        //*******public******
        public void StopMoveSound()
        {
            /*
               Created 10/10/2025 By Roger Williams

               called by frmgame if need alien to stop move sound

            */


            SNDSoundMove.Stop();
        }

        public void PlayMoveSound()
        {
            /*
               Created 10/10/2025 By Roger Williams

               called by frmgame if need alien move sound

            */

            //play move sound
            SNDSoundMove.Position = Modules.clsModel.TMSStart;
            SNDSoundMove.Play();
        }
        public void UpdateRows()
        {
            /*
              Created 02/10/2025 By Roger Williams

              checks if rows still contain aliens

            */

            //reset current values
            intAliensRow1 = 0;
            intAliensRow2 = 0;


            //row 1
            if (clsAlienType1_Row1_1.intHealth != 0)
            {
                intAliensRow1++;
            }

            if (clsAlienType1_Row1_2.intHealth != 0)
            {
                intAliensRow1++;
            }

            if (clsAlienType1_Row1_3.intHealth != 0)
            {
                intAliensRow1++;
            }

            if (clsAlienType1_Row1_4.intHealth != 0)
            {
                intAliensRow1++;
            }

            if (clsAlienType1_Row1_5.intHealth != 0)
            {
                intAliensRow1++;
            }

            if (clsAlienType1_Row1_6.intHealth != 0)
            {
                intAliensRow1++;
            }

            //row 2
            if (clsAlienType2_Row2_1.intHealth != 0)
            {
                intAliensRow2++;
            }

            if (clsAlienType2_Row2_2.intHealth != 0)
            {
                intAliensRow2++;
            }

            if (clsAlienType2_Row2_3.intHealth != 0)
            {
                intAliensRow2++;
            }

            if (clsAlienType2_Row2_4.intHealth != 0)
            {
                intAliensRow2++;
            }

            if (clsAlienType2_Row2_5.intHealth != 0)
            {
                intAliensRow2++;
            }

            if (clsAlienType2_Row2_6.intHealth != 0)
            {
                intAliensRow2++;
            }


            //try
            //{ 
            //    //row 1
            //    if (clsAlienType1_Row1_1.blnVisible)
            //    {
            //        intAliensRow1++;
            //    }

            //    if (clsAlienType1_Row1_2.blnVisible)
            //    {
            //        intAliensRow1++;
            //    }

            //    if (clsAlienType1_Row1_3.blnVisible)
            //    {
            //        intAliensRow1++;
            //    }

            //    if (clsAlienType1_Row1_4.blnVisible)
            //    {
            //        intAliensRow1++;
            //    }

            //    if (clsAlienType1_Row1_5.blnVisible)
            //    {
            //        intAliensRow1++;
            //    }

            //    if (clsAlienType1_Row1_6.blnVisible)
            //    {
            //        intAliensRow1++;
            //    }

            //    //row 2
            //    if (clsAlienType2_Row2_1.blnVisible)
            //    {
            //        intAliensRow2++;
            //    }

            //    if (clsAlienType2_Row2_2.blnVisible)
            //    {
            //        intAliensRow2++;
            //    }

            //    if (clsAlienType2_Row2_3.blnVisible)
            //    {
            //        intAliensRow2++;
            //    }

            //    if (clsAlienType2_Row2_4.blnVisible)
            //    {
            //        intAliensRow2++;
            //    }

            //    if (clsAlienType2_Row2_5.blnVisible)
            //    {
            //        intAliensRow2++;
            //    }

            //    if (clsAlienType2_Row2_6.blnVisible)
            //    {
            //        intAliensRow2++;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex = ex;
            //}
        }

        public int AlienHealth(int intAlien)
        {
            /*
              Created 01/10/2025 By Roger Williams

              returns alien health, called by main form on alien hit


              VARS

              intalien   - which alien 

            */

            int intAlienHealth = 0;

            try { 
            //get aliens health
            switch (intAlien)
            {
                case 1:
                    intAlienHealth = clsAlienType1_Row1_1.intHealth;
                    break;
                case 2:
                    intAlienHealth = clsAlienType1_Row1_2.intHealth;
                    break;
                case 3:
                    intAlienHealth = clsAlienType1_Row1_3.intHealth;
                    break;
                case 4:
                    intAlienHealth = clsAlienType1_Row1_4.intHealth;
                    break;
                case 5:
                    intAlienHealth = clsAlienType1_Row1_5.intHealth;
                    break;
                case 6:
                    intAlienHealth = clsAlienType1_Row1_6.intHealth;
                    break;
                case 7:
                    intAlienHealth = clsAlienType2_Row2_1.intHealth;
                    break;
                case 8:
                    intAlienHealth = clsAlienType2_Row2_2.intHealth;
                    break;
                case 9:
                    intAlienHealth = clsAlienType2_Row2_3.intHealth;
                    break;
                case 10:
                    intAlienHealth = clsAlienType2_Row2_4.intHealth;
                    break;
                case 11:
                    intAlienHealth = clsAlienType2_Row2_5.intHealth;
                    break;
                case 12:
                    intAlienHealth = clsAlienType2_Row2_6.intHealth;
                    break;
            }
            }
            catch (Exception ex)
            {
                ex = ex;
            }

            return intAlienHealth;
        }
        public void AlienHit(int intAlien)
        {
            /*
              Created 01/10/2025 By Roger Williams

              decrements hit aliens life if 0 removes from wave!


              VARS

              intalien   - which alien hit

            */

            int intAlienHealth = 0;

            try
            { 
                //get aliens health
                switch(intAlien)
                {
                    case 1:
                        intAlienHealth = clsAlienType1_Row1_1.intHealth;
                        break;
                    case 2:
                        intAlienHealth = clsAlienType1_Row1_2.intHealth;
                        break;
                    case 3:
                        intAlienHealth = clsAlienType1_Row1_3.intHealth;
                        break;
                    case 4:
                        intAlienHealth = clsAlienType1_Row1_4.intHealth;
                        break;
                    case 5:
                        intAlienHealth = clsAlienType1_Row1_5.intHealth;
                        break;
                    case 6:
                        intAlienHealth = clsAlienType1_Row1_6.intHealth;
                        break;
                    case 7:
                        intAlienHealth = clsAlienType2_Row2_1.intHealth;
                        break;
                    case 8:
                        intAlienHealth = clsAlienType2_Row2_2.intHealth;
                        break;
                    case 9:
                        intAlienHealth = clsAlienType2_Row2_3.intHealth;
                        break;
                    case 10:
                        intAlienHealth = clsAlienType2_Row2_4.intHealth;
                        break;
                    case 11:
                        intAlienHealth = clsAlienType2_Row2_5.intHealth;
                        break;
                    case 12:
                        intAlienHealth = clsAlienType2_Row2_6.intHealth;
                        break;
                }

                //decrement health
                intAlienHealth--;

                //check if 0
                if (intAlienHealth == 0)
                {
                    //remove alien from wave
                    switch (intAlien)
                    {
                        case 1:
                            clsAlienType1_Row1_1.AlienHit();
                            break;
                        case 2:
                            clsAlienType1_Row1_2.AlienHit();
                            break;
                        case 3:
                            clsAlienType1_Row1_3.AlienHit();
                            break;
                        case 4:
                            clsAlienType1_Row1_4.AlienHit();
                            break;
                        case 5:
                            clsAlienType1_Row1_5.AlienHit();
                            break;
                        case 6:
                            clsAlienType1_Row1_6.AlienHit();
                            break;
                        case 7:
                            clsAlienType2_Row2_1.AlienHit();
                            break;
                        case 8:
                            clsAlienType2_Row2_2.AlienHit();
                            break;
                        case 9:
                            clsAlienType2_Row2_3.AlienHit();
                            break;
                        case 10:
                            clsAlienType2_Row2_4.AlienHit();
                            break;
                        case 11:
                            clsAlienType2_Row2_5.AlienHit();
                            break;
                        case 12:
                            clsAlienType2_Row2_6.AlienHit();
                            break;
                    }
                }
                else
                {
                    //set aliens health
                    switch (intAlien)
                    {
                        case 1:
                             clsAlienType1_Row1_1.intHealth = intAlienHealth;
                            break;
                        case 2:
                             clsAlienType1_Row1_2.intHealth = intAlienHealth;
                            break;
                        case 3:
                             clsAlienType1_Row1_3.intHealth = intAlienHealth;
                            break;
                        case 4:
                             clsAlienType1_Row1_4.intHealth = intAlienHealth;
                            break;
                        case 5:
                             clsAlienType1_Row1_5.intHealth = intAlienHealth;
                            break;
                        case 6:
                             clsAlienType1_Row1_6.intHealth = intAlienHealth;
                            break;
                        case 7:
                             clsAlienType2_Row2_1.intHealth = intAlienHealth;
                            break;
                        case 8:
                             clsAlienType2_Row2_2.intHealth = intAlienHealth;
                            break;
                        case 9:
                             clsAlienType2_Row2_3.intHealth = intAlienHealth;
                            break;
                        case 10:
                             clsAlienType2_Row2_4.intHealth = intAlienHealth;
                            break;
                        case 11:
                             clsAlienType2_Row2_5.intHealth = intAlienHealth;
                            break;
                        case 12:
                             clsAlienType2_Row2_6.intHealth = intAlienHealth;
                            break;
                    }
                }


                //special check: if hit row 2 alien AND still has health just pay hit sound!
                if (intAlienHealth != 0 && intAlien >= 7)
                {
                     clsAlienType2_Row2_1.PlayHitSound();
                }
             
                UpdateRows();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

        public void StopWave()
        {
            /*
              Created 07/10/2025 By Roger Williams

              stops alienwave moving used by frmgame when player hit/resetting game/new level etc.

            */

            // TMRMoveAliens.Enabled = false;
            blnAsyncMoveAliens = true;
            SNDSoundMove.Stop();
        }
        public void Destroy()
        {
            /*
              Created 01/10/2025 By Roger Williams

              hides alien images destroys all classes

            */

            //TMRMoveAliens.Enabled = false;
            blnAsyncMoveAliens = true;
            clsAlienType1_Row1_1.Destroy();
            clsAlienType1_Row1_2.Destroy();
            clsAlienType1_Row1_3.Destroy();
            clsAlienType1_Row1_4.Destroy();
            clsAlienType1_Row1_5.Destroy();
            clsAlienType1_Row1_6.Destroy();

            clsAlienType2_Row2_1.Destroy();
            clsAlienType2_Row2_2.Destroy();
            clsAlienType2_Row2_3.Destroy();
            clsAlienType2_Row2_4.Destroy();
            clsAlienType2_Row2_5.Destroy();
            clsAlienType2_Row2_6.Destroy();
            SNDSoundMove.Stop();
        }

        public clsAlienWave(int intLevel)
        {
            /*
              Created 01/10/2025 By Roger Williams

              creates alien wave via alien class
              level determines how fast aliens move

              VARS

              intlevel  - game level 1-4 

            */
            //create alien wave

            //row 1
            clsAlienType1_Row1_1 = new clsAlien(1, 1, Modules.clsView.CNST_INT_ALIEN_1_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW1_TOP);
            clsAlienType1_Row1_2 = new clsAlien(2, 1, Modules.clsView.CNST_INT_ALIEN_2_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW1_TOP);
            clsAlienType1_Row1_3 = new clsAlien(3, 1, Modules.clsView.CNST_INT_ALIEN_3_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW1_TOP);
            clsAlienType1_Row1_4 = new clsAlien(4, 1, Modules.clsView.CNST_INT_ALIEN_4_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW1_TOP);
            clsAlienType1_Row1_5 = new clsAlien(5, 1, Modules.clsView.CNST_INT_ALIEN_5_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW1_TOP);
            clsAlienType1_Row1_6 = new clsAlien(6, 1, Modules.clsView.CNST_INT_ALIEN_6_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW1_TOP);
            //row 2
            clsAlienType2_Row2_1 = new clsAlien(1, 2, Modules.clsView.CNST_INT_ALIEN_1_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW2_TOP);
            clsAlienType2_Row2_2 = new clsAlien(2, 2, Modules.clsView.CNST_INT_ALIEN_2_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW2_TOP);
            clsAlienType2_Row2_3 = new clsAlien(3, 2, Modules.clsView.CNST_INT_ALIEN_3_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW2_TOP);
            clsAlienType2_Row2_4 = new clsAlien(4, 2, Modules.clsView.CNST_INT_ALIEN_4_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW2_TOP);
            clsAlienType2_Row2_5 = new clsAlien(5, 2, Modules.clsView.CNST_INT_ALIEN_5_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW2_TOP);
            clsAlienType2_Row2_6 = new clsAlien(6, 2, Modules.clsView.CNST_INT_ALIEN_6_ROW1_LEFT, Modules.clsView.CNST_INT_ALIEN_ROW2_TOP);
            //init sound
            SNDSoundMove.Open(new System.Uri(Modules.clsModel.CNST_STR_SND_ALIEN_MOVE));
            SNDSoundMove.Volume = 0.7;
            //store game level
            intGameLevel = intLevel;
            //init timer
            //TMRMoveAliens.Tick += MoveAliens_Tick;
            blnAsyncMoveAliens = false;
            AsyncMoveAliens();
            //switch (intLevel)
            //{ 
            //    case 1:
            //        await Task.Delay(Modules.clsView.CNST_INT_ALIENMOVE_LEVEL1_INTERVAL;
            //        break;
            //    case 2:
            //        await Task.Delay(Modules.clsView.CNST_INT_ALIENMOVE_LEVEL2_INTERVAL;
            //        break;
            //    case 3:
            //        await Task.Delay(Modules.clsView.CNST_INT_ALIENMOVE_LEVEL3_INTERVAL;
            //        break;
            //    case 4:
            //        await Task.Delay(Modules.clsView.CNST_INT_ALIENMOVE_LEVEL4_INTERVAL;
            //        break;
            //}

           // TMRMoveAliens.Enabled = true;
        }
    }
}
