using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogInvaders2025.Modules
{

    internal static class clsModel
    {
        //global zero timespan to replay music/effects
        public static TimeSpan TMSStart = new TimeSpan(0, 0, 0);
        //player start lives
        public const int CNST_INT_STARTLIVES = 3;
        public const int CNST_INT_MAXLIVES = 4;
        //max levels
        public static readonly int CNST_INT_MAXLEVELS = 4;
        //alien health defaults
        public static readonly int CNST_INT_ALIEN1_HEALTH = 1;
        public static readonly int CNST_INT_ALIEN2_HEALTH = 2;
        //scores for alien types
        public static readonly int CNST_INT_ALIEN1_SCORE = 100;
        public static readonly int CNST_INT_ALIEN2_SCORE = 200;
        public static readonly int CNST_INT_ALIENMOTHERSHIP_SCORE = 1000;

        //main application location
        public static readonly string CNST_STR_PATH_ROGINVADERSLOCATION = @"C:\RogInvaders\";
        //main game form
        public static readonly string CNST_STR_FRM_MAINGAMEFORM = "frmGame";
        //control name templates
        public static readonly string CNST_STR_CTL_ALIEN_IMAGECONTROL_BASENAME = "PICAlien";
        public static readonly string CNST_STR_CTL_ALIENSHOT_IMAGECONTROL_BASENAME = "PICAlienShot";
        public static readonly string CNST_STR_CTL_BASE_IMAGECONTROL_BASENAME = "PICBase";

        //sound locations
        public static readonly string CNST_STR_SND_PLAYER_SHOT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\Arturia_MODV_Aswad_Playershot.wav";
        public static readonly string CNST_STR_SND_PLAYER_MOVE = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\RetroSynth_PlayerMove.wav";
        public static readonly string CNST_STR_SND_PLAYER_LOSE_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\Playerlose.wav";
        public static readonly string CNST_STR_SND_PLAYER_LOSE_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\YouLoseHuman.wav.wav";
        public static readonly string CNST_STR_SND_PLAYER_GAMEOVER = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\GameOver.wav";
        public static readonly string CNST_STR_SND_PLAYER_HIT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\Arturia_ARP2600_PetitCoucou_PlayerHit.wav";
        public static readonly string CNST_STR_SND_PLAYER_WINS = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\YouWin_Crowd Applause Cheering.wav";
        public static readonly string CNST_STR_SND_PLAYER_GAMECOMPLETED = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\GameCompleted_This-is-not-another-flying-saucer-scare.wav";

        public static readonly string CNST_STR_SND_ALIENMOTHERSHIP = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\Arturia_CS80_UFOFlute_Mothership.wav";
        public static readonly string CNST_STR_SND_ALIENMOTHERSHIP_HIT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\Arturia_SEMV_WildClick_MothershipHit.wav";
        public static readonly string CNST_STR_SND_ALIEN_MOVE = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\Arturia_ARP2600_Wowak_AlienMove.wav";
        public static readonly string CNST_STR_SND_ALIEN_SHOT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\MonoFury_AlienSubversion_AlienShot.wav";
        public static readonly string CNST_STR_SND_ALIEN_SHOTMISSED = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\Aruria_ARP2600_Baujolpif_AlienShotMissed.wav";
        public static readonly string CNST_STR_SND_BEAM = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\beam.wav";
        public static readonly string CNST_STR_SND_ALIEN_INVASION = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\AlienInvasion.wav";
        public static readonly string CNST_STR_SND_ALIEN_HIT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\RETROSynth_AlienHit.wav";
        public static readonly string CNST_STR_SND_BASE_HIT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\Arturia_JUP8_RogExplosion1_BaseHit.wav";
        public static readonly string CNST_STR_SND_BASE_DESTROYED = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\Arturia_JUP8_RogExplosion1_BaseDestroyed.wav";
        public static readonly string CNST_STR_SND_INTROTHEME = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\RogInvaders1.wav";
        public static readonly string CNST_STR_SND_MAINTHEME = CNST_STR_PATH_ROGINVADERSLOCATION + @"Sounds\RogInvaders_MainTheme.wav";


        //image locations
        public static readonly string CNST_STR_IMG_MAINGAMETITLE = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\MainGameTitle.png";
        public static readonly string CNST_STR_IMG_BLANKIMAGE = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\BlankImage.png";
        public static readonly string CNST_STR_IMG_ALIEN1_1 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Aliens\Alien1_1.png";
        public static readonly string CNST_STR_IMG_ALIEN1_2 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Aliens\Alien1_2.png";
        public static readonly string CNST_STR_IMG_ALIEN1_3 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Aliens\Alien1_3.png";
        public static readonly string CNST_STR_IMG_ALIEN1_4 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Aliens\Alien1_4.png";
        public static readonly string CNST_STR_IMG_ALIEN2_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien2_1.png";
        public static readonly string CNST_STR_IMG_ALIEN2_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien2_2.png";
        public static readonly string CNST_STR_IMG_ALIEN2_3 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien2_3.png";
        public static readonly string CNST_STR_IMG_ALIEN2_4 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien2_4.png";
        public static readonly string CNST_STR_IMG_ALIEN_INVADER_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\AlienInvader_1.png";
        public static readonly string CNST_STR_IMG_ALIEN_INVADER_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\AlienInvader_2.png";
        public static readonly string CNST_STR_IMG_ALIEN_INVADER_3 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\AlienInvader_3.png";
        public static readonly string CNST_STR_IMG_ALIEN_INVADER_4 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\AlienInvader_4.png";
        public static readonly string CNST_STR_IMG_BEAM_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Beam1.png";
        public static readonly string CNST_STR_IMG_BEAM_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Beam2.png";
        public static readonly string CNST_STR_IMG_BEAM_3 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Beam3.png";
        public static readonly string CNST_STR_IMG_BEAM_4 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Beam4.png";

        public static readonly string CNST_STR_IMG_ALIEN2_1_HIT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien2_1_Hit.png";
        public static readonly string CNST_STR_IMG_ALIEN2_2_HIT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien2_2_Hit.png";
        public static readonly string CNST_STR_IMG_ALIEN2_3_HIT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien2_3_Hit.png";
        public static readonly string CNST_STR_IMG_ALIEN2_4_HIT = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien2_4_Hit.png";

        public static readonly string CNST_STR_IMG_ALIEN_EXPLOSION_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Explosion_1.png";
//        public static readonly string CNST_STR_IMG_ALIEN_EXPLOSION_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Explosion_2.png";
//        public static readonly string CNST_STR_IMG_ALIEN_EXPLOSION_3 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Explosion_3.png";
//        public static readonly string CNST_STR_IMG_ALIEN_EXPLOSION_4 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Explosion_4.png";
        public static readonly string CNST_STR_IMG_ALIENSHOT_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Shot1.png";
        //public static readonly string CNST_STR_IMG_ALIENSHOT_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Shot2.png";
        //public static readonly string CNST_STR_IMG_ALIENSHOT_3 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Shot3.png";
        //public static readonly string CNST_STR_IMG_ALIENSHOT_4 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Shot4.png";
        public static readonly string CNST_STR_IMG_ALIENSHOT_EXPLOSION_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\AlienShot_Explosion_1.png";
    //    public static readonly string CNST_STR_IMG_ALIENSHOT_EXPLOSION_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\AlienShot_Explosion_2.png";
     //   public static readonly string CNST_STR_IMG_ALIENSHOT_EXPLOSION_3 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\AlienShot_Explosion_3.png";
    //    public static readonly string CNST_STR_IMG_ALIENSHOT_EXPLOSION_4 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\AlienShot_Explosion_4.png";

        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_1 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Aliens\Alien_mothership_1.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_2 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Aliens\Alien_mothership_2.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_3 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Aliens\Alien_mothership_3.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_4 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Aliens\Alien_mothership_4.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_5 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Aliens\Alien_mothership_5.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Mothership_Explosion_1.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Mothership_Explosion_2.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_3 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Mothership_Explosion_3.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_4 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Mothership_Explosion_4.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_5 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Mothership_Explosion_5.png";
        public static readonly string CNST_STR_IMG_ALIENMOTHERSHIP_EXPLOSION_6 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Aliens\Alien_Mothership_Explosion_6.png";

        public static readonly string CNST_STR_IMG_PLAYER_1 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Ship\Ship_1.png";
//        public static readonly string CNST_STR_PLAYER_2 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Ship\Ship_2.png";
//        public static readonly string CNST_STR_PLAYER_3 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Ship\Ship_3.png";
//        public static readonly string CNST_STR_PLAYER_4 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Ship\Ship_4.png";
        public static readonly string CNST_STR_IMG_PLAYERSHOT_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Ship\ShipShot_1.png";
 //       public static readonly string CNST_STR_PLAYERSHOT_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Ship\ShipShot_2.png";
  //      public static readonly string CNST_STR_PLAYERSHOT_3 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Ship\ShipShot_3.png";
   //     public static readonly string CNST_STR_PLAYERSHOT_4 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Ship\ShipShot_4.png";

        public static readonly string CNST_STR_IMG_PLAYER_EXPLOSION_1 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Ship\Player_Explosion_1.png";
    //    public static readonly string CNST_STR_PLAYER_EXPLOSION_2 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Ship\Player_Explosion_2.png";
    //    public static readonly string CNST_STR_PLAYER_EXPLOSION_3 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Ship\Player_Explosion_3.png";
     //   public static readonly string CNST_STR_PLAYER_EXPLOSION_4 = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Ship\Player_Explosion_4.png";

        public static readonly string CNST_STR_IMG_PLAYER_GAMEOVER = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\GameOver_1.png";
        public static readonly string CNST_STR_IMG_PLAYER_WINS = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\YouWin_1.png";
        public static readonly string CNST_STR_IMG_PLAYER_LOST = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\YouLose_1.png";
        public static readonly string CNST_STR_IMG_PLAYER_COMPLETEDGAME = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\GameCompleted.png";
        public static readonly string CNST_STR_IMG_PLAYER_PLAYAGAIN = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\PlayAgain_1.png";


        public static readonly string CNST_STR_IMG_BASENORMAL_L = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_L_Normal.png";
        public static readonly string CNST_STR_IMG_BASENORMAL_R = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_R_Normal.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_L1 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_L_Damage1.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_L2 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_L_Damage2.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_L3 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_L_Damage3.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_L4 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_L_Damage4.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_L5 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_L_Damage5.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_R1 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_R_Damage1.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_R2 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_R_Damage2.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_R3 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_R_Damage3.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_R4 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_R_Damage4.png";
        public static readonly string CNST_STR_IMG_BASEDAMAGE_R5 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Background\Base_R_Damage5.png";

        public static readonly string CNST_STR_IMG_TITLEHEADER_1 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Other\Title1.png";
        public static readonly string CNST_STR_IMG_TITLEFOOTER_1 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Other\Title1a.png";
        public static readonly string CNST_STR_IMG_TITLEHEADER_2 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Other\Title2.png";
        public static readonly string CNST_STR_IMG_TITLEFOOTER_2 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Other\Title2a.png";
        public static readonly string CNST_STR_IMG_TITLEHEADER_3 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Other\Title3.png";
        public static readonly string CNST_STR_IMG_TITLEFOOTER_3 = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Images\Other\Title3a.png";

        //player keys
        public const int CNST_INT_PLAYERMOVEMENT_LEFT = 65;
        public const int CNST_INT_PLAYERMOVEMENT_RIGHT = 68;
        public const int CNST_INT_PLAYERMOVEMENT_SHOT = 32;


        //title bar icons
        public static readonly string CNST_STR_IMG_TITLE_CONTROLBOX_MINIMISE = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\Minimise.png";
        public static readonly string CNST_STR_IMG_TITLE_CONTROLBOX_MAXIMISE = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\Maximise.png";
        public static readonly string CNST_STR_IMG_TITLE_CONTROLBOX_CLOSE = CNST_STR_PATH_ROGINVADERSLOCATION + @"Images\Other\Close.png";

        //intro text location
        public static readonly string CNST_STR_PATH_INTROTEXTLOCATION = CNST_STR_PATH_ROGINVADERSLOCATION +  @"Text\IntroText.txt";

        public static bool InRange(int intStart, int intEnd, int intValue)
        {
            /*
              Created 05/10/2025 By Roger Williams

              checks if intValue is in passed number range

              VARS

              intstart      - start of range
              intend        - end of range
              intvalue      - number to find   


              RETURNS

              true if it is

            */

            bool blnOk = false;
            int intNum = 0;

            //if value less than end check value just exit!
            if (intValue < intStart)
            {
                return blnOk;
            }

            for (intNum = intStart; intNum != intEnd + 1; intNum++)
            {
                if (intNum == intValue)
                {
                    blnOk = true;
                    break;
                }
            }

            return blnOk;
        }
    }
}
