using RogInvaders2025.Modules;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
  Created 29/09/2025 By Roger Williams

  creates the six base units using clsBaseUnit

  also handles:

  - baseunit hit 


*/
namespace RogInvaders2025.Modules
{
    internal class clsBaseAll
    {
        private clsBaseUnit clsBaseUnit1;
        private clsBaseUnit clsBaseUnit2;
        private clsBaseUnit clsBaseUnit3;
        private clsBaseUnit clsBaseUnit4;
        private clsBaseUnit clsBaseUnit5;
        private clsBaseUnit clsBaseUnit6;

        public int intBaseUnitsleft = 12;

        public int GetBaseUnitLeft(int intUnit)
        {
            /*
              Created 30/09/2025 By Roger Williams

              called by frmgame to get baseunit (number) left position

              VARS

              intUnit - baseunit number to test

              RETURNS

              if left image ONLY visible - left image left
              if right image ONLY visible - right image left
              if BOTH visible - left image left
              if NONE visible - ZERO (nevr handled externally)

            */
            int intLeft = 0;

            switch (intUnit)
            {
                case 1:
                    if (clsBaseUnit1.blnVisibleLeft)
                    {
                        intLeft = clsBaseUnit1.intBaseUnitLeft_Left;
                    }
                    if (clsBaseUnit1.blnVisibleRight)
                    {
                        if (!clsBaseUnit1.blnVisibleLeft)
                        {
                            intLeft = clsBaseUnit1.intBaseUnitRight_Left;
                        }
                    }
                    break;
                case 2:
                    if (clsBaseUnit2.blnVisibleLeft)
                    {
                        intLeft = clsBaseUnit2.intBaseUnitLeft_Left;
                    }
                    if (clsBaseUnit2.blnVisibleRight)
                    {
                        if (!clsBaseUnit2.blnVisibleLeft)
                        {
                            intLeft = clsBaseUnit2.intBaseUnitRight_Left;
                        }
                    }
                    break;
                case 3:
                    if (clsBaseUnit3.blnVisibleLeft)
                    {
                        intLeft = clsBaseUnit3.intBaseUnitLeft_Left;
                    }
                    if (clsBaseUnit3.blnVisibleRight)
                    {
                        if (!clsBaseUnit3.blnVisibleLeft)
                        {
                            intLeft = clsBaseUnit3.intBaseUnitRight_Left;
                        }
                    }
                    break;
                case 4:
                    if (clsBaseUnit4.blnVisibleLeft)
                    {
                        intLeft = clsBaseUnit4.intBaseUnitLeft_Left;
                    }
                    if (clsBaseUnit4.blnVisibleRight)
                    {
                        if (!clsBaseUnit4.blnVisibleLeft)
                        {
                            intLeft = clsBaseUnit4.intBaseUnitRight_Left;
                        }
                    }
                    break;
                case 5:
                    if (clsBaseUnit5.blnVisibleLeft)
                    {
                        intLeft = clsBaseUnit5.intBaseUnitLeft_Left;
                    }
                    if (clsBaseUnit5.blnVisibleRight)
                    {
                        if (!clsBaseUnit5.blnVisibleLeft)
                        {
                            intLeft = clsBaseUnit5.intBaseUnitRight_Left;
                        }
                    }
                    break;
                case 6:
                    if (clsBaseUnit6.blnVisibleLeft)
                    {
                        intLeft = clsBaseUnit6.intBaseUnitLeft_Left;
                    }
                    if (clsBaseUnit6.blnVisibleRight)
                    {
                        if (!clsBaseUnit6.blnVisibleLeft)
                        {
                            intLeft = clsBaseUnit6.intBaseUnitRight_Left;
                        }
                    }
                    break;
            }
            return intLeft;
        }
        public int IsVisible(int intUnit)
        {
            /*
              Created 30/09/2025 By Roger Williams

              determines if baseunit (number) is visible

              VARS

              intUnit - baseunit number to test

              RETURNS

              int - 1 = left 2 = right 3 = both 0 = none

            */
            int intSide = 0;

            switch (intUnit)
            {
                case 1:
                    if (clsBaseUnit1.blnVisibleLeft)
                    {
                       intSide = 1;
                    }
                    if (clsBaseUnit1.blnVisibleRight)
                    {
                       intSide += 2;
                    }
                break;
                case 2:
                    if (clsBaseUnit2.blnVisibleLeft)
                    {
                        intSide = 1;
                    }
                    if (clsBaseUnit2.blnVisibleRight)
                    {
                        intSide += 2;
                    }
                    break;
                case 3:
                    if (clsBaseUnit3.blnVisibleLeft)
                    {
                        intSide = 1;
                    }
                    if (clsBaseUnit3.blnVisibleRight)
                    {
                        intSide += 2;
                    }
                    break;
                case 4:
                    if (clsBaseUnit4.blnVisibleLeft)
                    {
                        intSide = 1;
                    }
                    if (clsBaseUnit4.blnVisibleRight)
                    {
                        intSide += 2;
                    }
                    break;
                case 5:
                    if (clsBaseUnit5.blnVisibleLeft)
                    {
                        intSide = 1;
                    }
                    if (clsBaseUnit5.blnVisibleRight)
                    {
                        intSide += 2;
                    }
                    break;
                case 6:
                    if (clsBaseUnit6.blnVisibleLeft)
                    {
                        intSide = 1;
                    }
                    if (clsBaseUnit6.blnVisibleRight)
                    {
                        intSide += 2;
                    }
                    break;
            }
            return intSide;
        }

        public void BaseUnitHit(int intSide, int intBaseUnit)
        {
            /*
              Created 01/10/2025 By Roger Williams

              runs BaseHit() in correct class for baseunit hit

              VARS

              intside       - left or right
              intbaseunit   - baseunit number 1-6
            */

            switch (intBaseUnit)
            {
                case 1:
                    clsBaseUnit1.BaseHit(intSide);

                    if (intSide == 1)
                    { 
                        if (clsBaseUnit1.intBaseUnitDamageLeft == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    else
                    {
                        if (clsBaseUnit1.intBaseUnitDamageRight == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    break;
                case 2:
                    clsBaseUnit2.BaseHit(intSide);

                    if (intSide == 1)
                    {
                        if (clsBaseUnit2.intBaseUnitDamageLeft == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    else
                    {
                        if (clsBaseUnit2.intBaseUnitDamageRight == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    break;
                case 3:
                    clsBaseUnit3.BaseHit(intSide);

                    if (intSide == 1)
                    {
                        if (clsBaseUnit3.intBaseUnitDamageLeft == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    else
                    {
                        if (clsBaseUnit3.intBaseUnitDamageRight == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    break;
                case 4:
                    clsBaseUnit4.BaseHit(intSide);

                    if (intSide == 1)
                    {
                        if (clsBaseUnit4.intBaseUnitDamageLeft == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    else
                    {
                        if (clsBaseUnit4.intBaseUnitDamageRight == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    break;
                case 5:
                    clsBaseUnit5.BaseHit(intSide);

                    if (intSide == 1)
                    {
                        if (clsBaseUnit5.intBaseUnitDamageLeft == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    else
                    {
                        if (clsBaseUnit5.intBaseUnitDamageRight == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    break;
                case 6:
                    clsBaseUnit6.BaseHit(intSide);

                    if (intSide == 1)
                    {
                        if (clsBaseUnit6.intBaseUnitDamageLeft == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    else
                    {
                        if (clsBaseUnit6.intBaseUnitDamageRight == 7)
                        {
                            intBaseUnitsleft--;
                        }
                    }
                    break;
            }
        }

        public bool BaseDestroyed()
        {
            /*
              Created 06/10/2025 By Roger Williams

              checks if ALL baseunits are invisible

            */

            int intNum = 0;
            int intVisible = 0;

            for (intNum = 0; intNum != 7; intNum++)
            {
                if (IsVisible(intNum) != 0)
                {
                    intVisible++;
                }
            }

            return intVisible == 0;
        }
        public void Destroy()
        {
            /*
              Created 01/10/2025 By Roger Williams

              hides base images destroys all classes

            */

            clsBaseUnit1.Destroy();
            clsBaseUnit2.Destroy();
            clsBaseUnit3.Destroy();
            clsBaseUnit4.Destroy();
            clsBaseUnit5.Destroy();
            clsBaseUnit6.Destroy();
        }
        public clsBaseAll() 
        {
            //create baseunits
            clsBaseUnit1 = new clsBaseUnit(1);
            clsBaseUnit2 = new clsBaseUnit(2);
            clsBaseUnit3 = new clsBaseUnit(3);
            clsBaseUnit4 = new clsBaseUnit(4);
            clsBaseUnit5 = new clsBaseUnit(5);
            clsBaseUnit6 = new clsBaseUnit(6);
        }
    }
}
