using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RogInvaders2025.Modules
{
    internal static class clsView
    {
        //screen width/height
        public static readonly int CNST_INT_SCREEN_WIDTH = 1180;
        public static readonly int CNST_INT_SCREEN_HEIGHT = 860;
        //image size constants
        public static readonly int CNST_INT_TITLEHEADER_HEIGHT = 160;
        public static readonly int CNST_INT_TITLEHEADER_WIDTH = 380;
        public static readonly int CNST_INT_TITLEFOOTER_HEIGHT = 160;
        public static readonly int CNST_INT_TITLEFOOTER_WIDTH = 1024;
        public static readonly int CNST_INT_ALIENMOTHERSHIP_HEIGHT = 40;
        public static readonly int CNST_INT_ALIENMOTHERSHIP_WIDTH = 100;
        public static readonly int CNST_INT_ALIEN_HEIGHT = 60;
        public static readonly int CNST_INT_ALIEN_WIDTH = 60;
        public static readonly int CNST_INT_ALIENBULLET_HEIGHT = 23;
        public static readonly int CNST_INT_ALIENBULLET_WIDTH = 19;
        public static readonly int CNST_INT_PLAYER_HEIGHT = 80;
        public static readonly int CNST_INT_PLAYER_WIDTH = 50;
        public static readonly int CNST_INT_PLAYERBULLET_HEIGHT = 30;
        public static readonly int CNST_INT_PLAYERBULLET_WIDTH = 10;
        public static readonly int CNST_INT_BASE_HEIGHT = 63;
        public static readonly int CNST_INT_BASE_WIDTH = 42;
        public static readonly int CNST_INT_LIVES_HEIGHT = 25;
        public static readonly int CNST_INT_LIVES_WIDTH = 21;
        public static readonly int CNST_INT_BASEDAMAGE_L1_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_L1_WIDTH = CNST_INT_BASE_WIDTH /2;
        public static readonly int CNST_INT_BASEDAMAGE_L2_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_L2_WIDTH = CNST_INT_BASE_WIDTH;
        public static readonly int CNST_INT_BASEDAMAGE_L3_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_L3_WIDTH = CNST_INT_BASE_WIDTH;
        public static readonly int CNST_INT_BASEDAMAGE_L4_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_L4_WIDTH = CNST_INT_BASE_WIDTH;
        public static readonly int CNST_INT_BASEDAMAGE_L5_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_L5_WIDTH = CNST_INT_BASE_WIDTH;
        public static readonly int CNST_INT_BASEDAMAGE_R1_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_R1_WIDTH = CNST_INT_BASE_WIDTH / 2;
        public static readonly int CNST_INT_BASEDAMAGE_R2_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_R2_WIDTH = CNST_INT_BASE_WIDTH;
        public static readonly int CNST_INT_BASEDAMAGE_R3_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_R3_WIDTH = CNST_INT_BASE_WIDTH;
        public static readonly int CNST_INT_BASEDAMAGE_R4_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_R4_WIDTH = CNST_INT_BASE_WIDTH;
        public static readonly int CNST_INT_BASEDAMAGE_R5_HEIGHT = CNST_INT_BASE_HEIGHT;
        public static readonly int CNST_INT_BASEDAMAGE_R5_WIDTH = CNST_INT_BASE_WIDTH;

        //top positions
        public static readonly int CNST_INT_TITLEHEADER_TOP = 30;
        public static readonly int CNST_INT_TITLEFOOTER_TOP = 200;
        public static readonly int CNST_INT_ALIENMOTHERSHIP_TOP = 60;
        public static readonly int CNST_INT_ALIENMOTHERSHIPINTRO_TOP = 780;
        public static readonly int CNST_INT_ALIEN_TOP= 220;
        public static readonly int CNST_INT_PLAYER_TOP = 730;
        public static readonly int CNST_INT_BASE_TOP = 630;
        public static readonly int CNST_INT_ALIEN_ROW1_TOP = 100;
        public static readonly int CNST_INT_ALIEN_ROW2_TOP = 170;
        public static readonly int CNST_INT_GAMESCREENTOP = 60; //top of play area (excluding) score area etc.

        //left positions
        public static readonly int CNST_INT_ALIENMOTHERSHIPINTRO_LEFT = 1200;
        public static readonly int CNST_INT_PLAYER_LEFT = 600;  //form width /2
        public static readonly int CNST_INT_BASE1_LEFT = 50;
        public static readonly int CNST_INT_BASE2_LEFT = CNST_INT_BASE1_LEFT + 100;
        public static readonly int CNST_INT_BASE3_LEFT = CNST_INT_BASE2_LEFT + 200;
        public static readonly int CNST_INT_BASE4_LEFT = CNST_INT_BASE3_LEFT + 200;
        public static readonly int CNST_INT_BASE5_LEFT = CNST_INT_BASE4_LEFT + 200;
        public static readonly int CNST_INT_BASE6_LEFT = CNST_INT_BASE5_LEFT + 180;

        //left positions for BOTH rows of aliens are the same
        public static readonly int CNST_INT_ALIEN_1_ROW1_LEFT = 0;
        public static readonly int CNST_INT_ALIEN_2_ROW1_LEFT = 70;
        public static readonly int CNST_INT_ALIEN_3_ROW1_LEFT = CNST_INT_ALIEN_2_ROW1_LEFT + 70;
        public static readonly int CNST_INT_ALIEN_4_ROW1_LEFT = CNST_INT_ALIEN_3_ROW1_LEFT + 70;
        public static readonly int CNST_INT_ALIEN_5_ROW1_LEFT = CNST_INT_ALIEN_4_ROW1_LEFT + 70;
        public static readonly int CNST_INT_ALIEN_6_ROW1_LEFT = CNST_INT_ALIEN_5_ROW1_LEFT + 70;


        //used when drawing baseunits
        public static readonly int CNST_INT_BASE_SPACING = 100;

        //intro text list
        public static List<string> LSTIntroText = new List<string>();
        //intro text line length
        public static readonly int CNST_INT_INTROTEXTLINELENGTH = 103;

        //timer delays
        public static readonly int CNST_INT_ALIENMOTHERSHIP_INTRO_INTERVAL = 20000;  //60000;
        public static readonly int CNST_INT_ALIENMOTHERSHIP_INTRO_MOVE_INTERVAL = 2000;  
        public static readonly int CNST_INT_ALIENMOTHERSHIP_GAME_INTERVAL = 120000;
        public static readonly int CNST_INT_ALIENMOTHERSHIP_EXPLOSION_INTERVAL = 500;
        public static readonly int CNST_INT_ALIENMOTHERSHIP_MOVE_INTERVAL = 2200;
        public static readonly int CNST_INT_INTROTEXT_INTERVAL = 80; //1000;

        public static readonly int CNST_INT_ALIENSHOT_SHOT_INTERVAL_LEVEL1 = 5000;
        public static readonly int CNST_INT_ALIENSHOT_SHOT_INTERVAL_LEVEL2 = 3500;
        public static readonly int CNST_INT_ALIENSHOT_SHOT_INTERVAL_LEVEL3 = 2000;
        public static readonly int CNST_INT_ALIENSHOT_SHOT_INTERVAL_LEVEL4 = 1000;

        public static readonly int CNST_INT_ALIENSHOT_MOVE_INTERVAL_LEVEL1 = 1200;
        public static readonly int CNST_INT_ALIENSHOT_MOVE_INTERVAL_LEVEL2 = 1000;
        public static readonly int CNST_INT_ALIENSHOT_MOVE_INTERVAL_LEVEL3 = 800;
        public static readonly int CNST_INT_ALIENSHOT_MOVE_INTERVAL_LEVEL4 = 600;

        public static readonly int CNST_INT_ALIENMOVE_LEVEL1_INTERVAL = 1400;
        public static readonly int CNST_INT_ALIENMOVE_LEVEL2_INTERVAL = 1200;
        public static readonly int CNST_INT_ALIENMOVE_LEVEL3_INTERVAL = 1000;
        public static readonly int CNST_INT_ALIENMOVE_LEVEL4_INTERVAL = 800;
        public static readonly int CNST_INT_ALIEN_MOVEOFFSCREEN_INTERVAL = 1000;

        public static readonly int CNST_INT_DESTROYBASE_INTERVAL = 1500;
        public static readonly int CNST_INT_PLAYERSHOT_INTERVAL = 800;
        public static readonly int CNST_INT_COLLISION_INTERVAL = 400;
       
        //animation delays
        public static readonly int CNST_INT_ANIMATIONDELAY = 1000;
        public static readonly int CNST_INT_PLAYERANIMATIONDELAY = 500;
        public static readonly int CNST_INT_ANIMATIONDELAY_ALIEN_EXPLOSION = 250;
        public static readonly int CNST_INT_ANIMATIONDELAY_ALIEN_SHOTEXPLOSION = 150;
        public static readonly int CNST_INT_ANIMATIONDELAY_ALIEN_SHOT = 800;

        //alien move amount
        public static readonly int CNST_INT_ALIEN_MOVEAMOUNT = 18; //40
        public static readonly int CNST_INT_ALIEN_SHOT_MOVEAMOUNT = 40; //60;
                                                                        //alien shot offset
        public static readonly int CNST_INT_ALIEN_SHOT_OFFSET = 10;
        //main game screen title text  
        public static readonly string CNST_STR_MAINGAMETITLETEXT = "RogInvaders - Threat From Outer Space!";


        //for custom title bar
        public static Font fntTitlebar = new Font("Microsoft Sans Serif", 12); //title bar text font
        public const int HTCAPTION = 0x2;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int CNST_INT_TITLEBARHEIGHT = 24;
        public static readonly System.Drawing.Color CNST_INT_TITLEBAR_BACKCOLOUR = System.Drawing.Color.Black;
        public static readonly System.Drawing.Color CNST_INT_TITLEBAR_TEXTCOLOUR = System.Drawing.Color.Yellow;
        public static readonly System.Drawing.Color CNST_INT_TITLEBAR_UNDERLINECOLOUR = System.Drawing.Color.Black;

        //for custom panel
        public static Pen PENPanel = new Pen(Color.SkyBlue);
        public static Brush BRUPanel = new SolidBrush(Color.MidnightBlue);

        public static bool ReadIntroText()
        {
            /*
              Created 08/09/2025 By Roger Williams

              reads introtext into LSTIntroText


            */
            StreamReader STRMRead = null;
            string strTemp = String.Empty;

            if (File.Exists(Modules.clsModel.CNST_STR_PATH_INTROTEXTLOCATION))
            {
                STRMRead = new StreamReader(Modules.clsModel.CNST_STR_PATH_INTROTEXTLOCATION);

                if (STRMRead.EndOfStream == true)
                {
                    return false;
                }

                strTemp = STRMRead.ReadLine();

                while (strTemp != null)
                {
                    LSTIntroText.Add(strTemp);
                    strTemp = STRMRead.ReadLine();
                }

                STRMRead.Close();

                return true;
            }
            else
            {
                return false;
            }

        }















    }
}
