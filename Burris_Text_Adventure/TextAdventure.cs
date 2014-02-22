/**********************************************************************************************************************
 * Title: TextAdventure
 * Description: Handles the major functions of the text adventure such as prompts, rendering, and the battle system
 * Author: Chris Burris (CB), cburris@chrisbburris.com
 * Date: 5/14/13
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Media;
using System.IO;

namespace PokemonTextAdventure
{
	class TextAdventure
	{
		#region Variables

		const int iOverworldHeight = 20;
		const int iOverworldWidth = 76;

		enum GameState { Overworld, Battle, BattleTransition };
		static GameState gsGameState = GameState.Overworld;

		static bool bBattleBegun = false;
		static bool bBattleOver = true;
		
		static List<Pokemon> lpPokemon = new List<Pokemon>();

		static bool bShowMenu = false;
		static bool bShowMenuInBattle = false;
		static bool bShowHelp = false;
		static bool bShowCredits = false;

		static string sFirstPokemonName = "??????????";
		static string sFirstPokemonCurrentHealth = " ??";
		static string sFirstPokemonMaxHealth = "?? ";
		static string sFirstPokemonLevel = "?? ";
		static string sFirstPokemonType = "?????";
		static string sFirstPokemonAttack = "??";
		static string sFirstPokemonDefense = "??";
		static string sFirstPokemonSpeed = "??";
		static string sFirstPokemonExperience = "????";
		static string sFirstPokemonExpToNextLevel = "???";

		static string sEnemyPoke1 =  "                                               ";
		static string sEnemyPoke2 =  "                                               ";
		static string sEnemyPoke3 =  "                                               ";
		static string sEnemyPoke4 =  "                                               ";
		static string sEnemyPoke5 =  "                                               ";
		static string sEnemyPoke6 =  "                                               ";
		static string sEnemyPoke7 =  "                                               ";
		static string sEnemyPoke8 =  "                                               ";
		static string sEnemyPoke9 =  "                                               ";
		static string sEnemyPoke10 = "                                               ";
		static string sEnemyPoke11 = "                                               ";
		static string sEnemyPoke12 = "                                               ";
		static string sEnemyPoke13 = "                                               ";
		static string sEnemyPoke14 = "                                               ";
		static string sEnemyPoke15 = "                                               ";
		static string sEnemyPoke16 = "                                               ";
		static string sEnemyPoke17 = "                                               ";
		static string sEnemyPoke18 = "                                               ";

		static Pokemon pCurrentEnemyPokemon;
		enum BattleMessageType { Normal, PlayerHealth, EnemyHealth, PlayerLevelUp, BattleEnd, EnemyFainted };
		static Timer tBattleMessageTimer = new Timer(2000.0);
		static bool bDisplayingBattleMessages = false;
		static List<string> lsBattleMessages = new List<string>();
		static List<double> ldBattleMessageDurations = new List<double>();
		static List<BattleMessageType> lmtBattleMessageTypes = new List<BattleMessageType>();
		static bool bDrawBattle = true;

		enum ScreenType { Overworld, Battle, BattleTransition, Menu, Help, Credits };
		static char[,] caOverworld = new char[iOverworldHeight, iOverworldWidth];
		static char[,] caDrawnOverworld = new char[iOverworldHeight, iOverworldWidth];
		
		static char[,] caBattleScreen = new char[iOverworldHeight, iOverworldWidth];

		static char[,] caMenuScreen = new char[iOverworldHeight, iOverworldWidth];

		// Formatting
		const int iMaxBattleMessage = 74; 
		const int iMaxNameLength = 10;
		const int iMaxTypeLength = 5;
		const int iMaxHealthLength = 3;
		const int iMaxLevelLength = 3;
		const int iMaxStatLength = 2;
		const int iMaxExpLength = 4;
		const int iMaxExpToNextLevelLength = 3;

		static string sPlayerPokemonName = "??????????";
		static string sPlayerPokemonCurrentHealth = " ??";
		static string sPlayerPokemonMaxHealth = "?? ";
		static string sPlayerPokemonLevel = "?? ";
		static string sPlayerPokemonType = "?????";
		static bool bUpdatePlayerPokemonCurrentHealth = false;
		static bool bUpdatePlayerPokemonMaxHealth = false;
		static bool bUpdatePlayerPokemonLevel = false;

		static string sEnemyPokemonName = "??????????";
		static string sEnemyPokemonCurrentHealth = " ??";
		static string sEnemyPokemonMaxHealth = "?? ";
		static string sEnemyPokemonLevel = "?? ";
		static string sEnemyPokemonType = "?????";
		static bool bUpdateEnemyPokemonCurrentHealth = false;

		#region Battle Transition Variables

		static char[,] caBattleTransition = new char[iOverworldHeight, iOverworldWidth];
		static Timer tBattleTransitionTimer = new Timer(0.5);
		static bool bStartBattleTransition = false;
		static bool bAllowBattleTransition = false;
		static bool bdrawingBattleTransition = false;
		static int iBattleTransitionStage = 0;
		static int iBattleTransIndex;

		#endregion

		static char cDrawnPlayer = 'P';
		const char cBarrier = '#';
		const char cGrass = '\'';
		const char cNorth = '^';
		const char cSouth = 'v';
		const char cEast = '>';
		const char cWest = '<';

		static int iPlayerXLocation = (iOverworldWidth / 2) - 3;
		static int iPlayerYLocation = iOverworldHeight / 2;
		static int iPreviousXLocation = (iOverworldWidth / 2) - 3;
		static int iPreviousYLocation = iOverworldHeight / 2;

		static Timer tMovementTimer = new Timer(50.0);
		static bool bAllowMovement = false;
		static bool bDrawMovement = true;
		static bool bPokemonEncountered = false;
		const double dChanceOfPokeEncounter = 28.7;

		static bool bshowFaint = false;

		static List<Area> laOverworld = Area.Areas;
		static Area aCurrentArea = laOverworld[0];

		static bool bReset = false;

		#region User Input Variables

		enum Direction { Up, Down, Left, Right };

		const string sUpKey = "W";
		const string sDownKey = "S";
		const string sLeftKey = "A";
		const string sRightKey = "D";
		const string sMenuKey = "P";
		const string sHelpKey = "H";
		const string sQuitKey = "Escape";
		const string sAttackKey = "Enter";
		const string sCreditsKey = "C";
		const string sRunKey = "R";
		const string sBackKey = "B";

		#endregion

        #region Sound Variables

        static SoundPlayer soundOpeningIntro = new SoundPlayer();
        static SoundPlayer soundOpening = new SoundPlayer();
        static SoundPlayer soundWorldOfPokemon = new SoundPlayer();
        static SoundPlayer soundRoute1 = new SoundPlayer();
        static SoundPlayer soundWildPokemonIntro = new SoundPlayer();
        static SoundPlayer soundWildPokemon = new SoundPlayer();
        static SoundPlayer soundWildPokemonVictory = new SoundPlayer();
        static SoundPlayer soundEnding = new SoundPlayer();

        static Timer tOpeningMusicTimer = new Timer(15796.0);
        static Timer tBattleMusicTimer = new Timer(44333.0);

        static bool bOpeningMusicPlayed = false;
        static bool bVictoryMusicReached = false;

        #endregion

        #endregion

        static void Main(string[] args)
		{
			string sInput = "";

            initSounds();

            soundOpeningIntro.Play();
            initOpeningMusicTimer();

			while (true)
			{
				showTitleScreen();
                showIntroText();
				getStarterPokemon();

				init();

				sInput = "";
				bReset = false;

                soundRoute1.PlayLooping();

				while (true)
				{
					if (lpPokemon[0].Level == 10 && gsGameState == GameState.Overworld)
					{
						showVictoryScreen();
						if (bReset) bReset = false; lpPokemon.Clear(); break;
					}

					if (sInput == sQuitKey) Environment.Exit(0);

					#region Overworld State

					if (gsGameState == GameState.Overworld)
					{
						if (!bShowMenu && !bShowHelp && !bShowCredits)
						{
							handleMovement(sInput);

							if (bDrawMovement)
							{
								bDrawMovement = false;
								drawScreen(ScreenType.Overworld);
							}
						}
						else if(bShowMenu)
						{
							drawScreen(ScreenType.Menu);
						}
						else if (bShowHelp)
						{
							drawScreen(ScreenType.Help);
						}
						else if (bShowCredits)
						{
							drawScreen(ScreenType.Credits);
						}

						sInput = Console.ReadKey(true).Key.ToString();

						if (bPokemonEncountered && !bShowMenu)
						{
							bPokemonEncountered = false;
							sInput = " ";
							gsGameState = GameState.BattleTransition;
							bStartBattleTransition = true;
							bdrawingBattleTransition = true;
						}

						if (sInput == sMenuKey && !bShowMenu) bShowMenu = true;
						if (bShowMenu && sInput == sBackKey) bShowMenu = false;

						if (sInput == sHelpKey && !bShowHelp) bShowHelp = true;
						if (bShowHelp && sInput == sBackKey) bShowHelp = false;

						if (sInput == sCreditsKey && !bShowCredits) bShowCredits = true;
						if (bShowCredits && sInput == sBackKey) bShowCredits = false;

					}

					#endregion

					#region Battle Transition State

					else if (gsGameState == GameState.BattleTransition)
					{
						playBattleTransition();
						if (bAllowBattleTransition) drawScreen(ScreenType.BattleTransition);
					}

					#endregion

					#region Battle State

					else if (gsGameState == GameState.Battle)
					{
						manageBattle();
						
						if (bDrawBattle && !bShowMenuInBattle) drawScreen(ScreenType.Battle);
						else if (bDrawBattle && bShowMenuInBattle) drawScreen(ScreenType.Menu);
					}

					#endregion
				}
			}
		}

		#region Menu System

		static void showTitleScreen()
		{
            while (true)
            {
                Console.Clear();
                Console.WriteLine(@"                                                                      ");
                Console.WriteLine(@"                                                                      ");
                Console.WriteLine(@"                                                                      ");
                Console.WriteLine(@"                                        .::.                          ");
                Console.WriteLine(@"                                      .;:**'                          ");
                Console.WriteLine(@"                                      `                               ");
                Console.WriteLine(@"          .:XHHHHk.              db.   .;;.     dH  MX                ");
                Console.WriteLine(@"        oMMMMMMMMMMM       ~MM  dMMP :MMMMMR   MMM  MR      ~MRMN     ");
                Console.WriteLine(@"        QMMMMMb  'MMX       MMMMMMP !MX' :M~   MMM MMM  .oo. XMMM 'MMM");
                Console.WriteLine(@"          `MMMM.  )M> :X!Hk. MMMM   XMM.o'  .  MMMMMMM X?XMMM MMM>!MMP");
                Console.WriteLine(@"           'MMMb.dM! XM M'?M MMMMMX.`MMMMMMMM~ MM MMM XM `' MX MMXXMM ");
                Console.WriteLine(@"            ~MMMMM~ XMM. .XM XM`'MMMb.~*?**~ .MMX M t MMbooMM XMMMMMP ");
                Console.WriteLine(@"             ?MMM>  YMMMMMM! MM   `?MMRb.    `'''   !L'MMMMM XM IMMM  ");
                Console.WriteLine(@"              MMMX   'MMMM'  MM       ~%:           !Mh.''' dMI IMMP  ");
                Console.WriteLine(@"              'MMM.                                             IMX   ");
                Console.WriteLine(@"               ~M!M                                             IMP   ");
                Console.WriteLine(@"                                                                      ");
                Console.WriteLine(@"                                                                      ");
                Console.WriteLine(@"                                 Text Adventure                       ");
                Console.WriteLine(@"                                                                      ");
                Console.WriteLine(@"                                                                      ");
                Console.WriteLine(@"                                                                      ");
                Console.WriteLine(@"                                                                      ");


                // Wait to continue
                Console.Write("                            Press ENTER to continue: ");
                string sInput = Console.ReadKey(true).Key.ToString();

                // Check which class the user chose 
                if (sInput == sAttackKey)
                {
                    break;
                }
            }
		}

		static void showIntroText()
		{
            soundWorldOfPokemon.PlayLooping();
            bOpeningMusicPlayed = true;

            while (true)
            {

                Console.Clear();
                Console.WriteLine(@"                   |||----------------------------------|||");
                Console.WriteLine(@"                   ||| Welcome to the World of Pokemon! |||");
                Console.WriteLine(@"                   |||----------------------------------|||");
                Console.WriteLine("");

                Console.WriteLine("Hello, I'm Professor Joshua Tree, but some people call me the Pokemon Professor. \n\n" +
                                                    "In the world of Pokemon, people and Pokemon live and work together. \n" +
                                                    "They form strong bonds together through training and Pokemon battles.\n" +
                                                    "It seems you've decided to begin your own Pokemon journey. \n");

                Console.WriteLine("Before you can venture out into the world, you must first choose a pokemon \n" +
                                                    "companion. Before you can leave town, you have to prove yourself by \n" +
                                                    "training your Pokemon to level 10. Only then will I recognize you as a \n" +
                                                    "full-fledged Pokemon Trainer and allow you to capture Pokemon.\n");

                Console.WriteLine("I'm letting you into the fields just outside of town. There you will encounter \n" +
                                                    "wild Pokemon to battle so you can raise your Pokemon companion.");
                Console.WriteLine("\nGood luck out there!\n");

                // Wait to continue
                Console.Write("\n\n                          Press ENTER to continue: ");
                string sInput = Console.ReadKey(true).Key.ToString();

                // Check which class the user chose 
                if (sInput == sAttackKey)
                {
                    break;
                }
            }
		}

		static void showVictoryScreen()
		{
            soundEnding.Play();

			// Show this screen until the user either quits or resets
			while (true)
			{
				Console.Clear();
				Console.WriteLine(@"                 |||-----------------------------------------|||");
				Console.WriteLine(@"                 ||| Congrats! You're now a Pokemon Trainer! |||");
				Console.WriteLine(@"                 |||-----------------------------------------|||");

				if (lpPokemon[0].Species == Pokemon.PokeSpecies.Bulbasaur)
				{
					//Console.WriteLine(@"                                   _,.------....___,.' ',.-.");
					Console.WriteLine(@"                                ,-'          _,.--'        |");
					Console.WriteLine(@"                              ,'         _.-'              .");
                    Console.WriteLine(@" BULBASAUR reached Level 10! /   ,     ,'                   `");
					Console.WriteLine(@"                            .   /     /                     ``.");
					Console.WriteLine(@"                            |  |     .                       \.\");
					Console.WriteLine(@"                  ____      |___._.  |       __               \ `.");
					Console.WriteLine(@"                .'    `---''       ``'-.--''`  \               .  \");
					Console.WriteLine(@"               .  ,            __               `              |   .");
					Console.WriteLine(@"               `,'         ,-''  .               \             |    L");
					Console.WriteLine(@"              ,'          '    _.'                -._          /    |");
					Console.WriteLine(@"             ,`-.    ,'.   `--'                      >.      ,'     |");
					Console.WriteLine(@"            . .'\'   `-'       __    ,  ,-.         /  `.__.-      ,'");
					Console.WriteLine(@"            ||:, .           ,'  ;  /  / \ `        `.    .      .'/");
					Console.WriteLine(@"            j|:D  \          `--'  ' ,'_  . .         `.__, \   , /");
					Console.WriteLine(@"           / L:_  |                 .  '' :_;                `.'.'");
					Console.WriteLine(@"           .    '''                  ''''''                    V");
					Console.WriteLine(@"            `.                                 .    `.   _,..  `");
					Console.WriteLine(@"              `,_   .    .                _,-'/    .. `,'   __  `");
					Console.WriteLine(@"               ) \`._        ___....----''  ,'   .'  \ |   '  \  .");
					/*Console.WriteLine(@"              /   `. '`-.--''         _,' ,'     `---' |    `./  |");
					Console.WriteLine(@"             .   _  `'''--.._____..--'   ,             '         |");
					Console.WriteLine(@"             | .' `. `-.                /-.           /          ,");
					Console.WriteLine(@"             | `._.'    `,_            ;  /         ,'          .");
					Console.WriteLine(@"            .'          /| `-.        . ,'         ,           ,");
					Console.WriteLine(@"            '-.__ __ _,','    '`-..___;-...__   ,.'\ ____.___.'");
					Console.WriteLine(@"            `^--'..'   '-`-^-''--    `-^-'`.'''''''`.,^.`.--' mh");*/
				}
				else if (lpPokemon[0].Species == Pokemon.PokeSpecies.Charmander)
				{
					//Console.WriteLine(@"         					    .--''`-..");
					Console.WriteLine(@"                     ,'          `.");
					Console.WriteLine(@"                   ,'          __  `.");
                    Console.WriteLine(@"                  /|          ' __   \   CHARMANDER reached Level 10!");
					Console.WriteLine(@"                 , |           / |.   .");
					Console.WriteLine(@"                 |,'          !_.'|   |");
					Console.WriteLine(@"               ,'             '   |   |");
					Console.WriteLine(@"              /              |`--'|   |");
					Console.WriteLine(@"             |                `---'   |");
					Console.WriteLine(@"              .   ,                   |                       ,'.");
					Console.WriteLine(@"               ._     '           _'  |                    , ' \ `");
					Console.WriteLine(@"           `.. `.`-...___,...---''    |       __,.        ,`'   L,|");
					Console.WriteLine(@"           |, `- .`._        _,-,.'   .  __.-'-. /        .   ,    \");
					Console.WriteLine(@"         -:..     `. `-..--_.,.<       `'      / `.        `-/ |   .");
					Console.WriteLine(@"           `,         """"'     `.              ,'         |   |  ',,");
					Console.WriteLine(@"             `.      '            '            /          '    |'. |/");
					Console.WriteLine(@"               `.   |              \       _,-'           |       ''");
					Console.WriteLine(@"                 `._'               \   ''\                .      |");
					Console.WriteLine(@"                    |                '     \                `._  ,'");
					Console.WriteLine(@"                    |                 '     \                 .'|");
					/*Console.WriteLine(@"                    |                 .      \                | |");
					Console.WriteLine(@"                    |                 |       L              ,' |");
					Console.WriteLine(@"                    `                 |       |             /   '");
					Console.WriteLine(@"                     \                |       |           ,'   /");
					Console.WriteLine(@"                   ,' \               |  _.._ ,-..___,..-'    ,'");
					Console.WriteLine(@"                  /     .             .      `!             ,j'");
					Console.WriteLine(@"                 /       `.          /        .           .'/");
					Console.WriteLine(@"                .          `.       /         |        _.'.'");
					Console.WriteLine(@"                 `.          7`'---'          |------''_.'");
					Console.WriteLine(@"                _,.`,_     _'                ,''-----''");
					Console.WriteLine(@"            _,-_    '       `.     .'      ,\");
					Console.WriteLine(@"            -' /`.         _,'     | _  _  _.|");
					Console.WriteLine(@"             ''--'---''''''        `' '! |! /");
					Console.WriteLine(@"                                     `' ' -' mh");*/
				}
				else if (lpPokemon[0].Species == Pokemon.PokeSpecies.Squirtle)
				{
					//Console.WriteLine(@"          					     _,........__");
					Console.WriteLine(@"                      ,-'            '`-.");
					Console.WriteLine(@"                    ,'                   `-.");
                    Console.WriteLine(@"                  ,'                        \   SQUIRTLE reached Level 10!");
					Console.WriteLine(@"                ,'                           .");
					Console.WriteLine(@"                .'\               ,"".       `");
					Console.WriteLine(@"               ._.'|             / |  `       \");
					Console.WriteLine(@"               |   |            `-.'  ||       `.");
					Console.WriteLine(@"               |   |            '-._,'||       | \");
					Console.WriteLine(@"               .`.,'             `..,'.'       , |`-.");
					Console.WriteLine(@"               l                       .'`.  _/  |   `.");
					Console.WriteLine(@"               `-.._'-   ,          _ _'   -' \  .     `");
					Console.WriteLine(@"          `.''''''-.`-...,---------','         `. `....__.");
					Console.WriteLine(@"          .'        `'-..___      __,'\          \  \     \");
					Console.WriteLine(@"          \_ .          |   `'''''    `.           . \     \");
					Console.WriteLine(@"            `.          |              `.          |  .     L");
					Console.WriteLine(@"              `.        |`--...________.'.        j   |     |");
					Console.WriteLine(@"                `._    .'      |          `.     .|   ,     |");
					Console.WriteLine(@"                   `--,\       .            `7''' |  ,      |");
					Console.WriteLine(@"                      ` `      `            /     |  |      |    _,-''''`-.");
					/*Console.WriteLine(@"                       \ `.     .          /      |  '      |  ,'          `.");
					Console.WriteLine(@"                        \  v.__  .        '       .   \    /| /              \");
					Console.WriteLine(@"                         \/    `''\'''''''`.       \   \  /.''                |");
					Console.WriteLine(@"                          `        .        `._ ___,j.  `/ .-       ,---.     |");
					Console.WriteLine(@"                          ,`-.      \         .'     `.  |/        j     `    |");
					Console.WriteLine(@"                         /    `.     \       /         \ /         |     /    j");
					Console.WriteLine(@"                        |       `-.   7-.._ .          |'          '         /");
					Console.WriteLine(@"                        |          `./_    `|          |            .     _,'");
					Console.WriteLine(@"                        `.           / `----|          |-............`---'");
					Console.WriteLine(@"                          \          \      |          |");
					Console.WriteLine(@"                         ,'           )     `.         |");
					Console.WriteLine(@"                          7____,,..--'      /          |");
					Console.WriteLine(@"                                            `---.__,--.'mh");*/
				}

				// Prompt user for quit or reset
				Console.Write("\n                      Press 'ESC' to quit or 'R' to Reset: ");
				string sInput = Console.ReadKey(true).Key.ToString();

				// Check to quit or reset
				if (sInput == "Escape")
				{
					Environment.Exit(0);
				}
				else if (sInput == "R")
				{
                    soundOpeningIntro.Play();
                    tOpeningMusicTimer.Interval = 15796.0;
                    tOpeningMusicTimer.Start();
                    bOpeningMusicPlayed = false;
                    aCurrentArea = laOverworld[0];
                    iPlayerXLocation = (iOverworldWidth / 2) - 3;
		            iPlayerYLocation = iOverworldHeight / 2;
		            iPreviousXLocation = (iOverworldWidth / 2) - 3;
		            iPreviousYLocation = iOverworldHeight / 2;
					bReset = true;
					break;
				}
			}
		}

		static void getStarterPokemon()
		{
			Pokemon pBulbasaurTemp = new Pokemon(Pokemon.PokeSpecies.Bulbasaur, 5);
			Pokemon pCharmanderTemp = new Pokemon(Pokemon.PokeSpecies.Charmander, 5);
			Pokemon pSquirtleTemp = new Pokemon(Pokemon.PokeSpecies.Squirtle, 5);

			string sBulbasaurName = formatPokeName(pBulbasaurTemp.NickName);
			string sBulbasaurCurrentHealth = formatPokeHealth(pBulbasaurTemp.CurrentHealth.ToString());
			string sBulbasaurMaxHealth = formatPokeMaxHealth(pBulbasaurTemp.MaxHealth.ToString());
			string sBulbasaurLevel = formatPokeLevel(pBulbasaurTemp.Level.ToString());
			string sBulbasaurType = formatePokeType(pBulbasaurTemp.Type.ToString());
			string sBulbasaurAttack = formatPokeStat(pBulbasaurTemp.Attack.ToString());
			string sBulbasaurDefense = formatPokeStat(pBulbasaurTemp.Defense.ToString());
			string sBulbasaurSpeed = formatPokeStat(pBulbasaurTemp.Speed.ToString());
			string sBulbasaurExperience = formatPokeExp(pBulbasaurTemp.Experience.ToString());

			string sCharmanderName = formatPokeName(pCharmanderTemp.NickName);
			string sCharmanderCurrentHealth = formatPokeHealth(pCharmanderTemp.CurrentHealth.ToString());
			string sCharmanderMaxHealth = formatPokeMaxHealth(pCharmanderTemp.MaxHealth.ToString());
			string sCharmanderLevel = formatPokeLevel(pCharmanderTemp.Level.ToString());
			string sCharmanderType = formatePokeType(pCharmanderTemp.Type.ToString());
			string sCharmanderAttack = formatPokeStat(pCharmanderTemp.Attack.ToString());
			string sCharmanderDefense = formatPokeStat(pCharmanderTemp.Defense.ToString());
			string sCharmanderSpeed = formatPokeStat(pCharmanderTemp.Speed.ToString());
			string sCharmanderExperience = formatPokeExp(pCharmanderTemp.Experience.ToString());

			string sSquirtleName = formatPokeName(pSquirtleTemp.NickName);
			string sSquirtleCurrentHealth = formatPokeHealth(pSquirtleTemp.CurrentHealth.ToString());
			string sSquirtleMaxHealth = formatPokeMaxHealth(pSquirtleTemp.MaxHealth.ToString());
			string sSquirtleLevel = formatPokeLevel(pSquirtleTemp.Level.ToString());
			string sSquirtleType = formatePokeType(pSquirtleTemp.Type.ToString());
			string sSquirtleAttack = formatPokeStat(pSquirtleTemp.Attack.ToString());
			string sSquirtleDefense = formatPokeStat(pSquirtleTemp.Defense.ToString());
			string sSquirtleSpeed = formatPokeStat(pSquirtleTemp.Speed.ToString());
			string sSquirtleExperience = formatPokeExp(pSquirtleTemp.Experience.ToString());

			// While the user hasn't chosen a class, loop
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Choose Your Pokemon!                                                        \n");
				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");
				Console.WriteLine(" |     1      | HP: " + sBulbasaurCurrentHealth + "/" + sBulbasaurMaxHealth + " | Atk: " + sBulbasaurAttack + "   | Lv: " + sBulbasaurLevel + "       | Type: " + sBulbasaurType + "         |");
				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");
				Console.WriteLine(" | " + sBulbasaurName + " | Def: " + sBulbasaurDefense + "     | Spd: " + sBulbasaurSpeed + "   | Exp: " + sBulbasaurExperience + "     | High Speed          |");
				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");
				Console.WriteLine("                                                                            ");
				Console.WriteLine("                                                                            ");
				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");
				Console.WriteLine(" |     2      | HP: " + sCharmanderCurrentHealth + "/" + sCharmanderMaxHealth + " | Atk: " + sCharmanderAttack + "   | Lv: " + sCharmanderLevel + "       | Type: " + sCharmanderType + "         |");
				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");
				Console.WriteLine(" | " + sCharmanderName + " | Def: " + sCharmanderDefense + "     | Spd: " + sCharmanderSpeed + "   | Exp: " + sCharmanderExperience + "     | High Attack         |");
				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");
				Console.WriteLine("                                                                            ");
				Console.WriteLine("                                                                            ");
				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");
				Console.WriteLine(" |     3      | HP: " + sSquirtleCurrentHealth + "/" + sSquirtleMaxHealth + " | Atk: " + sSquirtleAttack + "   | Lv: " + sSquirtleLevel + "       | Type: " + sSquirtleType + "         |");
				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");
				Console.WriteLine(" | " + sSquirtleName + " | Def: " + sSquirtleDefense + "     | Spd: " + sSquirtleSpeed + "   | Exp: " + sSquirtleExperience + "     | High Defense        |");
				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");

				// Prompt to get class selection
				Console.Write("\nPress 1 for BULBASAUR, 2 for CHARMANDER, or 3 for SQUIRTLE: ");
				string sInput = Console.ReadKey(true).Key.ToString();

				// Check which class the user chose 
				if (sInput == "D1")
				{
					lpPokemon.Add(pBulbasaurTemp);
					break;
				}
				else if (sInput == "D2")
				{
					lpPokemon.Add(pCharmanderTemp);
					break;
				}
				else if (sInput == "D3")
				{
					lpPokemon.Add(pSquirtleTemp);
					break;
				}
			}
		}

		#endregion

		#region Initialization

		static void init()
		{
			updateOverworld();
			updateBattleScreen();
			updateMenuSetup();
			updateMenuScreen();
			initMovementTimer();
			initBattleTransitionTimer();
			initBattleMessageTimer();
            initBattleMusicTimer();
		}

		static void updateOverworld()
		{
			int iCount = 0;

			for (int i = 0; i < iOverworldHeight; i++)
			{
				for (int j = 0; j < iOverworldWidth; j++)
				{
					caOverworld[i, j] = aCurrentArea.Landscape[iCount];
					if (aCurrentArea.Landscape[iCount] == cNorth || aCurrentArea.Landscape[iCount] == cSouth || aCurrentArea.Landscape[iCount] == cEast || aCurrentArea.Landscape[iCount] == cWest)
					{
						caDrawnOverworld[i, j] = ' ';
					}
					else caDrawnOverworld[i, j] = aCurrentArea.Landscape[iCount];
					iCount++;
				}
			}
		}

		static void updateBattleScreen()
		{
			string sBattleScreen;
			int iCount = 0;

			sBattleScreen = " Your Pokemon | " + sPlayerPokemonName + " | HP: " + sPlayerPokemonCurrentHealth + "/" + sPlayerPokemonMaxHealth + " | Lv: " + sPlayerPokemonLevel + "    | Type: " + sPlayerPokemonType + " |       " +
										"--------------|------------|-------------|------------|-------------|       " +
										"                                                                            " +
										" " + sEnemyPoke2 + "                            " +
										" " + sEnemyPoke3 + "                            " +
										" " + sEnemyPoke4 + "                            " +
										" " + sEnemyPoke5 + "                            " +
										" " + sEnemyPoke6 + "                            " +
										" " + sEnemyPoke7 + "                            " +
										" " + sEnemyPoke8 + "                            " +
										" " + sEnemyPoke9 + "                            " +
										" " + sEnemyPoke10 + "                            " +
										" " + sEnemyPoke11 + "                            " +
										" " + sEnemyPoke12 + "                            " +
										" " + sEnemyPoke13 + " |-------|                  " +
										" " + sEnemyPoke14 + " | Enemy |                  " +
										" " + sEnemyPoke15 + " |--------------------------" +
										" " + sEnemyPoke16 + " | " + sEnemyPokemonName + " | HP: " + sEnemyPokemonCurrentHealth + "/" + sEnemyPokemonMaxHealth + " " +
										" " + sEnemyPoke17 + " |------------|-------------" +
										" " + sEnemyPoke18 + " | Lv: " + sEnemyPokemonLevel + "    | Type: " + sEnemyPokemonType + " ";

			for (int i = 0; i < iOverworldHeight; i++)
			{
				for (int j = 0; j < iOverworldWidth; j++)
				{
					caBattleScreen[i, j] = sBattleScreen[iCount];
					iCount++;
				}
			}
		}

		static void loadPokemon()
		{
			if (bshowFaint)
			{
				sEnemyPoke1 =  @"                                               ";
				sEnemyPoke2 =  @"                                               ";
				sEnemyPoke3 =  @"                                               ";
				sEnemyPoke4 =  @"                                               ";
				sEnemyPoke5 =  @"                                               ";
				sEnemyPoke6 =  @"                                               ";
				sEnemyPoke7 =  @"                                               ";
				sEnemyPoke8 =  @"                                               ";
				sEnemyPoke9 =  @"                                               ";
				sEnemyPoke10 = @"                                               ";
				sEnemyPoke11 = @"                                               ";
				sEnemyPoke12 = @"                                               ";
				sEnemyPoke13 = @"                                               ";
				sEnemyPoke14 = @"                                               ";
				sEnemyPoke15 = @"                                               ";
				sEnemyPoke16 = @"                                               ";
				sEnemyPoke17 = @"                                               ";
				sEnemyPoke18 = @"                                               ";

				return;
			}

			if (pCurrentEnemyPokemon.Species == Pokemon.PokeSpecies.Oddish)
			{
                sEnemyPoke1 =  @"__     `|    |         .   /   _.-'        _,.."; 
                sEnemyPoke2 =  @"  `._   |    |          L,'  ,'       _,.-'   '";
                sEnemyPoke3 =  @"     `. |    |          |  .'     _.-'       , ";
                sEnemyPoke4 =  @"       \|    '          L/    _,-'          /  ";
                sEnemyPoke5 =  @"._      `.    L        /   _,'            ,'   ";
                sEnemyPoke6 =  @"  `-._    \   `       ,' ,'             ,'     ";
                sEnemyPoke7 =  @"      `-.. `   \     /,-'           _.-'       ";
                sEnemyPoke8 =  @"        ,'.-..  .   /_,..---'`+'''''           ";
                sEnemyPoke9 =  @"       /           '           `.              ";
                sEnemyPoke10 = @"      j                          .             ";
                sEnemyPoke11 = @"     .                           |             ";
                sEnemyPoke12 = @"     |   .-.       ,.            |             ";
                sEnemyPoke13 = @"     |    -'       `.'           |             ";
                sEnemyPoke14 = @"     `                           '             ";
                sEnemyPoke15 = @"      `.      .--.             ,'              ";
                sEnemyPoke16 = @"        `.    `._|          ,-'                ";
                sEnemyPoke17 = @"      _.-`   ,..______.. .  `-.                ";
                sEnemyPoke18 = @"    ,'       |          |      `.              ";
			}
			else if (pCurrentEnemyPokemon.Species == Pokemon.PokeSpecies.Psyduck)
			{
                sEnemyPoke1 =  @"           ,...,.''             `--.---'       ";       
                sEnemyPoke2 =  @"         /,..,'                     `.         ";
                sEnemyPoke3 =  @"       ,'  .'                         `.       ";
                sEnemyPoke4 =  @"      j   /                             `.     ";
                sEnemyPoke5 =  @"      |  /,----._           ,.----.       .    ";
                sEnemyPoke6 =  @"     ,  j    _   \        .'  .,   `.     |    ";
                sEnemyPoke7 =  @"   ,'   |        |  ____  |         | .'--+,^. ";
                sEnemyPoke8 =  @"  /     |`-....-',-'    `._`--....-' _/      | ";
                sEnemyPoke9 =  @" /      |     _,'          `--..__  `        ' ";
                sEnemyPoke10 = @"j       | ,-''    `    .'         `. `        `";
                sEnemyPoke11 = @"|        .\                        /  |        ";
                sEnemyPoke12 = @"|         `\                     ,'   |        ";
                sEnemyPoke13 = @"|          |                    |   ,-|        ";
                sEnemyPoke14 = @".         ,'                    |-''  |        ";
                sEnemyPoke15 = @" \       /                      `.    |        ";
                sEnemyPoke16 = @"  ` /  ,'                        |    `        ";
                sEnemyPoke17 = @"   /  /                          |     \       ";
                sEnemyPoke18 = @"  /  |                           |      \      ";
			}
			if (pCurrentEnemyPokemon.Species == Pokemon.PokeSpecies.Growlithe)
			{
			    sEnemyPoke1 =  @"	           _,                                 ";                         
                sEnemyPoke2 =  @"           .',_..,                             ";
                sEnemyPoke3 =  @"         ,'     /,--                           ";
                sEnemyPoke4 =  @"       .'       ,./.__                         ";
                sEnemyPoke5 =  @"       |_,.----/,,'`.  _                       ";
                sEnemyPoke6 =  @"      .'__     //    `...>                     ";
                sEnemyPoke7 =  @" ____//|) |    `      /.'                      ";
                sEnemyPoke8 =  @"(/    `-.-'.._     _,|                 ,.------";
                sEnemyPoke9 =  @".             `.  '   \               /        ";
                sEnemyPoke10 = @"`..---._       |       `.            j         ";
                sEnemyPoke11 = @"  >-,-'`'''    |        |`'+-..__    |         ";
                sEnemyPoke12 = @" ( /|         /____     |  |  |  \''|+-.._     ";
                sEnemyPoke13 = @"  ' `-..._     '--,_    |  |  `   | |   | `.-.'";
                sEnemyPoke14 = @"       ,'        '_>_   j ,'  '. ,` |,  |   `. ";
                sEnemyPoke15 = @"      '.         >       '     | |  ' \ L     .";
                sEnemyPoke16 = @"       '.       /              '|`     \|      ";
                sEnemyPoke17 = @"      / /      /                '       v      ";
                sEnemyPoke18 = @"    ,'  >---+-+.        |   __,..--'`-._       ";

			}
			if (pCurrentEnemyPokemon.Species == Pokemon.PokeSpecies.Vulpix)
			{
				sEnemyPoke1 =  @"	        _,.+-----__,._                        ";
				sEnemyPoke2 =  @"         /  /    ,'     `.                     ";
				sEnemyPoke3 =  @",+._   ./...\_  /   ,..   \                    ";
				sEnemyPoke4 =  @"| `.`+'       `-' .' ,.|  |                    ";
				sEnemyPoke5 =  @"|  |( ,    ,.`,   |  `-',,........_       __.. ";
				sEnemyPoke6 =  @" \ |..`/,-'  ''''' `''''  _,.---'-,  .-+-'     ";
				sEnemyPoke7 =  @"  .'|       /'\`.      ,-'       / .','      , ";
				sEnemyPoke8 =  @" .'-'      |`-'  |    `./       / / /       /  ";
				sEnemyPoke9 =  @"j`v+'      `----'       ,'    ,'./ .'      /   ";
				sEnemyPoke10 = @"|                      |   _,','j  |      /    ";
				sEnemyPoke11 = @" \                     `.-'  j  |  L     F     ";
				sEnemyPoke12 = @"  \ .-.               ,'     |  L   .    /     ";
				sEnemyPoke13 = @"   \ `.|            _/_      '   \  |   /   ,  ";
				sEnemyPoke14 = @"    `.             '   `-.    `.__| |  /  ,'   ";
				sEnemyPoke15 = @"      `'-,.               `----'   `-.' .'     ";
				sEnemyPoke16 = @"         |          ,.                `.  ,-'  ";
				sEnemyPoke17 = @"        /|         /                    \'     ";
				sEnemyPoke18 = @"..--...' +,..-----'                      \---  ";
			}
			if (pCurrentEnemyPokemon.Species == Pokemon.PokeSpecies.Staryu)
			{			
				sEnemyPoke1 =  @"                                               ";
				sEnemyPoke2 =  @"`.`.        `    \ `. _,..-'._   , /   .       ";     
				sEnemyPoke3 =  @" `.`.       \       '        `'' /    '        ";
				sEnemyPoke4 =  @"   . `.      .   '              /    /        _";
				sEnemyPoke5 =  @"    `. `.     .   \            /   .'       .' ";
				sEnemyPoke6 =  @"      `. `.  /`    \          /   , \     .' _,";
				sEnemyPoke7 =  @"        `  `/  \    \        /   /   \  ,' ,'  ";
				sEnemyPoke8 =  @"       ,'`./    \  ,-'''''-./   /     \_.-'    ";
				sEnemyPoke9 =  @"       |._ `.    ,',-'''''`-.` '    _,' `.     ";
				sEnemyPoke10 = @"       |  `-._  . /          \`._.-' _.-'|     ";
				sEnemyPoke11 = @"        `._   `-|.            .|..-''    |     ";
				sEnemyPoke12 = @"        /  \'._ ||            ||     _,-'.     ";
				sEnemyPoke13 = @"       .    \  `.|            ||..-''/   |     ";
				sEnemyPoke14 = @"       '     .   `           .'    ,'    '     ";
				sEnemyPoke15 = @"      .       .  .`._      .'`.  .'       .    ";
				sEnemyPoke16 = @"             .|,'   /'----'\   `._|.      '    ";
				sEnemyPoke17 = @"     '     ,' `. _,'-.    ,''._  .' `.         ";
				sEnemyPoke18 = @"    /    .'  _.''     `..'     `'-.   \    .   ";	                              
			}
			if (pCurrentEnemyPokemon.Species == Pokemon.PokeSpecies.Bellsprout)
			{
				sEnemyPoke1 =  @"  			 .--'''''--._                           ";
                sEnemyPoke2 =  @"       .'             `.                       ";
                sEnemyPoke3 =  @"      /                 .                      ";
                sEnemyPoke4 =  @"     j                   .                     ";
                sEnemyPoke5 =  @"     |                   |                     ";
                sEnemyPoke6 =  @"     |                   |                     ";
                sEnemyPoke7 =  @"     | (')              j                      ";
                sEnemyPoke8 =  @"     `                 ,`.                     ";
                sEnemyPoke9 =  @"      \               ,^. `.                   ";
                sEnemyPoke10 = @"       .             /   \  .                  ";
                sEnemyPoke11 = @"       |            /     . |                  ";
                sEnemyPoke12 = @"       |          ,'      | |                  ";
                sEnemyPoke13 = @"      ,'---..___ /        | |                  ";
                sEnemyPoke14 = @"     ' `--..___ '`.      .  |                  ";
                sEnemyPoke15 = @"      `'--....___.'     /  j__.....__          ";
                sEnemyPoke16 = @"                       /   |         `.        ";
                sEnemyPoke17 = @"                      / _,------._     `.      ";
                sEnemyPoke18 = @"                     /,+_         `.     `.    ";
			}
		}

		static void updateMenuScreen()
		{
			string sMenuScreen;
			int iCount = 0;

			sMenuScreen =
			"------------| HP: " + sFirstPokemonCurrentHealth + "/" + sFirstPokemonMaxHealth + " | Atk: " + sFirstPokemonAttack + "   | Lv: " + sFirstPokemonLevel + "       | Type: " + sFirstPokemonType + "         " +
			" " + sFirstPokemonName + " |-------------|-----------|---------------|---------------------" +
			"------------| Def: " + sFirstPokemonDefense + "     | Spd: " + sFirstPokemonSpeed + "   | Exp: " + sFirstPokemonExperience + "     | Exp to Next Lv: " + sFirstPokemonExpToNextLevel + " " +
			"------------|-------------|-----------|---------------|---------------------";

			if (lpPokemon[0].Species == Pokemon.PokeSpecies.Bulbasaur)
			{
				string sTemp;

				sTemp =
				@"                              ,'         _.-'              .                "+
				@"                             /   ,     ,'                   `               "+
				@"                            .   /     /                     ``.             "+
				@"                            |  |     .                       \.\            "+
				@"                  ____      |___._.  |       __               \ `.          "+
				@"                .'    `---''       ``'-.--''`  \               .  \         "+
				@"               .  ,            __               `              |   .        "+
				@"               `,'         ,-''  .               \             |    L       "+
				@"              ,'          '    _.'                -._          /    |       "+
				@"             ,`-.    ,'.   `--'                      >.      ,'     |       "+
				@"            . .'\'   `-'       __    ,  ,-.         /  `.__.-      ,'       "+
				@"            ||:, .           ,'  ;  /  / \ `        `.    .      .'/        "+
				@"            j|:D  \          `--'  ' ,'_  . .         `.__, \   , /         "+
				@"           / L:_  |                 .  '' :_;                `.'.'          "+
				@"           .    '''                  ''''''                    V            "+
				@"            `.                                 .    `.   _,..  `            ";

				sMenuScreen += sTemp;
			}
			else if (lpPokemon[0].Species == Pokemon.PokeSpecies.Charmander)
			{
				string sTemp;

				sTemp =
				@"                     ,'          `.                                         " +
				@"                   ,'          __  `.                                       " +
				@"                  /|          ' __   \                                      " +
				@"                 , |           / |.   .                                     " +
				@"                 |,'          !_.'|   |                                     " +
				@"               ,'             '   |   |                                     " +
				@"              /              |`--'|   |                                     " +
				@"             |                `---'   |                                     " +
				@"              .   ,                   |                       ,'.           " +
				@"               ._     '           _'  |                    , ' \ `          " +
				@"           `.. `.`-...___,...---''    |       __,.        ,`'   L,|         " +
				@"           |, `- .`._        _,-,.'   .  __.-'-. /        .   ,    \        " +
				@"         -:..     `. `-..--_.,.<       `'      / `.        `-/ |   .        " +
				@"           `,         '''''     `.              ,'         |   |  ',,       " +
				@"             `.      '            '            /          '    |'. |/       " +
				@"               `.   |              \       _,-'           |       ''        ";

				sMenuScreen += sTemp;
			}
			else if (lpPokemon[0].Species == Pokemon.PokeSpecies.Squirtle)
			{
				string sTemp;

				sTemp =
				@"                        ,-'            '`-.                                 " +
				@"                      ,'                   `-.                              " +
				@"                    ,'                        \                             " +
				@"                  ,'                           .                            " +
				@"                  .'\               ,"".       `                            " +
				@"                 ._.'|             / |  `       \                           " +
				@"                 |   |            `-.'  ||       `.                         " +
				@"                 |   |            '-._,'||       | \                        " +
				@"                 .`.,'             `..,'.'       , |`-.                     " +
				@"                 l                       .'`.  _/  |   `.                   " +
				@"                 `-.._'-   ,          _ _'   -' \  .     `                  " +
				@"            `.''''''-.`-...,---------','         `. `....__.                " +
				@"            .'        `'-..___      __,'\          \  \     \               " +
				@"            \_ .          |   `'''''    `.           . \     \              " +
				@"              `.          |              `.          |  .     L             " +
				@"                `.        |`--...________.'.        j   |     |             " +
				@"                `.        |`--...________.'.        j   |     |             ";

				sMenuScreen += sTemp;
			}

			for (int i = 0; i < iOverworldHeight; i++)
			{
				for (int j = 0; j < iOverworldWidth; j++)
				{
					caMenuScreen[i, j] = sMenuScreen[iCount];
					iCount++;
				}
			}
		}

		static void updateMenuSetup()
		{
			sFirstPokemonName = formatPokeName(lpPokemon[0].NickName);
			sFirstPokemonCurrentHealth = formatPokeHealth(lpPokemon[0].CurrentHealth.ToString());
			sFirstPokemonMaxHealth = formatPokeMaxHealth(lpPokemon[0].MaxHealth.ToString());
			sFirstPokemonLevel = formatPokeLevel(lpPokemon[0].Level.ToString());
			sFirstPokemonType = formatePokeType(lpPokemon[0].Type.ToString());
			sFirstPokemonAttack = formatPokeStat(lpPokemon[0].Attack.ToString());
			sFirstPokemonDefense = formatPokeStat(lpPokemon[0].Defense.ToString());
			sFirstPokemonSpeed = formatPokeStat(lpPokemon[0].Speed.ToString());
			sFirstPokemonExperience = formatPokeExp(lpPokemon[0].Experience.ToString());
			sFirstPokemonExpToNextLevel = formatPokeExpToNextLevel(lpPokemon[0].ExpToNextLevel.ToString());
		}

		static void initBattleTransition()
		{
			for (int i = 0; i < iOverworldHeight; i++)
			{
				for (int j = 0; j < iOverworldWidth; j++) caBattleTransition[i, j] = '*';
			}
		}

        static void initSounds()
        {
            soundOpeningIntro.SoundLocation = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Sounds\OpeningIntro.wav");
            soundOpening.SoundLocation = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Sounds\Opening.wav");
            soundWorldOfPokemon.SoundLocation = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Sounds\TheWorldOfPokemon.wav");
            soundRoute1.SoundLocation = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Sounds\Route1.wav");
            soundWildPokemonIntro.SoundLocation = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Sounds\WildPokemonIntro.wav");
            soundWildPokemon.SoundLocation = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Sounds\WildPokemon.wav");
            soundWildPokemonVictory.SoundLocation = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Sounds\WildPokemonVictory.wav");
            soundEnding.SoundLocation = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Sounds\Ending.wav");
        }

		#endregion

		#region Timers
		
		#region Movement Timer

		static void initMovementTimer()
		{
			tMovementTimer.Elapsed += new ElapsedEventHandler(allowMovement);
			tMovementTimer.Enabled = true;
		}

		static void allowMovement(object sender, ElapsedEventArgs e)
		{
			bAllowMovement = true;
		}

		#endregion

        #region Opening Music Timer

        static void initOpeningMusicTimer()
        {
            tOpeningMusicTimer.Elapsed += new ElapsedEventHandler(allowOpeningMusic);
            tOpeningMusicTimer.Enabled = true;
        }

        static void allowOpeningMusic(object sender, ElapsedEventArgs e)
        {
            if (!bOpeningMusicPlayed)
            {
                bOpeningMusicPlayed = true;
                soundOpening.PlayLooping();
            }

            tOpeningMusicTimer.Enabled = false;
        }

        #endregion

        #region Battle Music Timer

        static void initBattleMusicTimer()
        {
            tBattleMusicTimer.Elapsed += new ElapsedEventHandler(allowBattleMusic);
        }

        static void allowBattleMusic(object sender, ElapsedEventArgs e)
        {
            if (gsGameState != GameState.Overworld && !bVictoryMusicReached)
            {
                soundWildPokemon.PlayLooping();
            }

            tBattleMusicTimer.Stop();
        }

        #endregion

        #region Battle Transition Timer

        static void initBattleTransitionTimer()
		{
			tBattleTransitionTimer.Elapsed += new ElapsedEventHandler(allowBattleTransition);
			tBattleTransitionTimer.Enabled = true;
		}

		static void allowBattleTransition(object sender, ElapsedEventArgs e)
		{
			bAllowBattleTransition = true;
		}

		#endregion

		#region Battle Message Timer 

		static void initBattleMessageTimer()
		{
			tBattleMessageTimer.Elapsed += new ElapsedEventHandler(disableBattleMessage);
			tBattleMessageTimer.Enabled = false;
		}

		static void disableBattleMessage(object sender, ElapsedEventArgs e)
		{
            if (lsBattleMessages.Count > 0) lsBattleMessages.RemoveAt(0);

            if (ldBattleMessageDurations.Count > 0) ldBattleMessageDurations.RemoveAt(0);

            if (lmtBattleMessageTypes.Count > 0) lmtBattleMessageTypes.RemoveAt(0);

			if (lsBattleMessages.Count == 0)
			{
				lsBattleMessages.Clear();
				ldBattleMessageDurations.Clear();
				lmtBattleMessageTypes.Clear();
				tBattleMessageTimer.Enabled = false;

				if (!bBattleOver) bDrawBattle = true;
				else
				{
					bDrawBattle = false;
					bshowFaint = false;
					healPlayerPokemon();

	                sEnemyPoke1 =  "                                               ";
		            sEnemyPoke2 =  "                                               ";
		            sEnemyPoke3 =  "                                               ";
		            sEnemyPoke4 =  "                                               ";
		            sEnemyPoke5 =  "                                               ";
		            sEnemyPoke6 =  "                                               ";
		            sEnemyPoke7 =  "                                               ";
		            sEnemyPoke8 =  "                                               ";
		            sEnemyPoke9 =  "                                               ";
		            sEnemyPoke10 = "                                               ";
		            sEnemyPoke11 = "                                               ";
		            sEnemyPoke12 = "                                               ";
		            sEnemyPoke13 = "                                               ";
		            sEnemyPoke14 = "                                               ";
		            sEnemyPoke15 = "                                               ";
		            sEnemyPoke16 = "                                               ";
		            sEnemyPoke17 = "                                               ";
		            sEnemyPoke18 = "                                               ";

					gsGameState = GameState.Overworld;

                    bVictoryMusicReached = false;
                    soundRoute1.PlayLooping();
				}
			}
			else
			{
				tBattleMessageTimer.Interval = ldBattleMessageDurations[0];
				checkBattleMessageType(lmtBattleMessageTypes[0]);
				tBattleMessageTimer.Enabled = true;
				bDrawBattle = true;
			}
		}

		#endregion

		#endregion

		#region Player Movement

		static void handleMovement(string sInput)
		{
			if (bAllowMovement)
			{
				bAllowMovement = false;
				bDrawMovement = true;

				// Up
				if (sInput == sUpKey)
				{
					if (canMove(Direction.Up))
					{
						iPreviousYLocation = iPlayerYLocation;
						iPreviousXLocation = iPlayerXLocation;
						iPlayerYLocation--;

						if (inGrass()) determinePokeEncounter();

						checkForTransition();
					}
				}

				// Down
				else if (sInput == sDownKey)
				{
					if (canMove(Direction.Down))
					{
						iPreviousYLocation = iPlayerYLocation;
						iPreviousXLocation = iPlayerXLocation;
						iPlayerYLocation++;

						if (inGrass()) determinePokeEncounter();

						checkForTransition();
					}
				}

				// Left
				else if (sInput == sLeftKey)
				{
					if (canMove(Direction.Left))
					{
						iPreviousXLocation = iPlayerXLocation;
						iPreviousYLocation = iPlayerYLocation;
						iPlayerXLocation--;

						if (inGrass()) determinePokeEncounter();

						checkForTransition();
					}
				}

				// Right
				else if (sInput == sRightKey)
				{
					if (canMove(Direction.Right))
					{
						iPreviousXLocation = iPlayerXLocation;
						iPreviousYLocation = iPlayerYLocation;
						iPlayerXLocation++;

						if (inGrass()) determinePokeEncounter();

						checkForTransition();
					}
				}
			}
		}

		static bool canMove(Direction dDirection)
		{
			if (dDirection == Direction.Up && spaceIsEmpty(iPlayerXLocation, iPlayerYLocation - 1)) return true;

			else if (dDirection == Direction.Down && spaceIsEmpty(iPlayerXLocation, iPlayerYLocation + 1)) return true;

			else if (dDirection == Direction.Left && spaceIsEmpty(iPlayerXLocation - 1, iPlayerYLocation)) return true;

			else if (dDirection == Direction.Right && spaceIsEmpty(iPlayerXLocation + 1, iPlayerYLocation)) return true;

			return false;
		}

		static void determinePokeEncounter()
		{
			Random rRandom = new Random(DateTime.Now.Second);

			if ((rRandom.NextDouble() * 100) <= dChanceOfPokeEncounter) bPokemonEncountered = true;
		}

		static bool inGrass()
		{
			return caOverworld[iPlayerYLocation, iPlayerXLocation] == cGrass;
		}

		static void checkForTransition()
		{
			if (caOverworld[iPlayerYLocation, iPlayerXLocation] == cNorth)
			{
				if (aCurrentArea.HasNorthernNeighbor)
				{
					aCurrentArea = aCurrentArea.NorthernNeighbor;
					updateOverworld();
					iPlayerYLocation = iOverworldHeight - 2;
				}
			}

			else if (caOverworld[iPlayerYLocation, iPlayerXLocation] == cSouth)
			{
				if (aCurrentArea.HasSouthernNeighbor)
				{
					aCurrentArea = aCurrentArea.SouthernNeighbor;
					updateOverworld();
					iPlayerYLocation = 1;
				}
			}

			else if (caOverworld[iPlayerYLocation, iPlayerXLocation] == cEast)
			{
				if (aCurrentArea.HasEasternNeighbor)
				{
					aCurrentArea = aCurrentArea.EasternNeighbor;
					updateOverworld();
					iPlayerXLocation = 1;
				}
			}

			else if (caOverworld[iPlayerYLocation, iPlayerXLocation] == cWest)
			{
			  if (aCurrentArea.HasWesternNeighbor)
				{
					aCurrentArea = aCurrentArea.WesternNeighbor;
					updateOverworld();
					iPlayerXLocation = iOverworldWidth - 2;
				}
			}
		}

		static bool spaceIsEmpty(int iPossibleNewX, int iPossibleNewY)
		{
			if (coordinateOutOfBounds(iPossibleNewX, iPossibleNewY)) return false;

			if (caOverworld[iPossibleNewY, iPossibleNewX] == cBarrier) return false;

			return true;
		}

		static bool coordinateOutOfBounds(int iPossibleNewX, int iPossibleNewY)
		{
			if (iPossibleNewX < 0 || iPossibleNewX >= iOverworldWidth || iPossibleNewY < 0 || iPossibleNewY >= iOverworldHeight) return true;

			return false;
		}

		#endregion

		#region Draw Screen

		static void drawScreen(ScreenType stScreenType)
		{
			Console.Clear();

			#region Draw Overworld Screen

			if (stScreenType == ScreenType.Overworld)
			{
				drawPlayerInOverworld();

				Console.WriteLine(" |----------------------------------------------------------------------------|");
				Console.Write(prepareScreenForDraw(caDrawnOverworld));
				Console.WriteLine(" |---------------|---------|---------|------------|-----------|---------------|");
				Console.WriteLine(" | Move: W,A,S,D | Poke: P | Help: H | Credits: C | Quit: ESC |               |");
				Console.WriteLine(" |---------------|---------|---------|------------|-----------|---------------|");
			}

			#endregion

			#region Draw Battle Screen

			else if (stScreenType == ScreenType.Battle)
			{
				loadPokemon();
				updatePokemonDraw();
				updateBattleScreen();

				Console.WriteLine(" |--------------|------------|-------------|------------|-------------|-------|");
				Console.Write(prepareScreenForDraw(caBattleScreen));                                             

				if (!tBattleMessageTimer.Enabled)
				{
					Console.WriteLine(" |---------------|---------|--------|--------------|------------|-------------|");
					Console.WriteLine(" | Attack: Enter | Poke: P | Run: R |                                         |");
					Console.WriteLine(" |---------------|---------|--------|-----------------------------------------|");

					bDisplayingBattleMessages = false;
				}
				else if (tBattleMessageTimer.Enabled)
				{
					Console.WriteLine(" |----------------------------------------------------------------------------|");
					Console.WriteLine(" | " + lsBattleMessages[0] + " |");
					Console.WriteLine(" |----------------------------------------------------------------------------|");

					bDrawBattle = false;
				}

				resetAllPokeDraw();
			}

			#endregion

			#region Draw Battle Transition

			else if (stScreenType == ScreenType.BattleTransition)
			{
				Console.WriteLine(" |----------------------------------------------------------------------------|");
				Console.Write(prepareScreenForDraw(caBattleTransition));
				Console.WriteLine(" |----------------------------------------------------------------------------|");
				Console.WriteLine(" | Pokemon Encountered!                                                       |");
				Console.WriteLine(" |----------------------------------------------------------------------------|");
			}

			#endregion

			#region Draw Menu Screen

			else if (stScreenType == ScreenType.Menu)
			{
				updateMenuSetup();
				updateMenuScreen();

				Console.WriteLine(" |------------|-------------|-----------|---------------|---------------------|");
				Console.Write(prepareScreenForDraw(caMenuScreen));
				Console.WriteLine(" |---------|------------------------------------------------------------------|");
				Console.WriteLine(" | Back: B |                                                                  |");
				Console.WriteLine(" |---------|------------------------------------------------------------------|");
			}

			#endregion

			#region Draw Help Screen

			else if (stScreenType == ScreenType.Help)
			{
				Console.WriteLine(" |-----------------------------|||-----------|||------------------------------|");
				Console.WriteLine(" |                             ||| Help Menu |||                              |");
				Console.WriteLine(" |                             |||-----------|||                              |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | Type Advantages: GRASS beats WATER, WATER beats FIRE, FIRE beats GRASS     |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | In battle, a Pokemon with a type advantage over the other will do          |");
				Console.WriteLine(" | 2x damage. If the Pokemon has a type disadvantage, they do 0.5 damage.     |");
				Console.WriteLine(" | Finally, two Pokemon of the same type do normal damage to each other.      |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | After every battle, your Pokemon is automatically healed.                  |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | If your Pokemon faints, the battle ends and your Pokemon is still          |");
				Console.WriteLine(" | automatically healed.                                                      |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | The Atk stat is how much damage a Pokemon does.                            |");
				Console.WriteLine(" | The attacker's Atk stat minus the victim's Def stat determines the actual  |");
				Console.WriteLine(" | damage done to the victim.                                                 |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | The Spd stat determines which Pokemon attacks first in battle.             |");
				Console.WriteLine(" | The Pokemon with the higher Spd stat attacks first.                        |");
				Console.WriteLine(" |---------|------------------------------------------------------------------|");
				Console.WriteLine(" | Back: B |                                                                  |");
				Console.WriteLine(" |---------|------------------------------------------------------------------|");
			}

			#endregion

			#region Draw Credits Screen

			else if (stScreenType == ScreenType.Credits)
			{
				Console.WriteLine(" |-----------------------------|||-----------|||------------------------------|");
				Console.WriteLine(" |                             |||  Credits  |||                              |");
				Console.WriteLine(" |                             |||-----------|||                              |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | Project Creator: Chris Burris (cburris@chrisbburris.com)                   |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | The Pokemon name and all elements thereof are registered trademarks of     |");
				Console.WriteLine(" | The Pokemon Company, a subsidiary of Nintendo Co., Ltd. This is a student  |");
				Console.WriteLine(" | project for the Guildhall at SMU. I do not own anything related to the     |");
				Console.WriteLine(" | Pokemon name. I'm only using it as a part of this project which is purely  |");
				Console.WriteLine(" | for academic and non-profit reasons.                                       |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | I did not do any of the Ascii art, Here are the sources for it:            |");
				Console.WriteLine(" | - http://ascii.co.uk/art/pokemon                                           |");
				Console.WriteLine(" | - http://www.angelfire.com/mn/Maija/pokemon/                               |");
				Console.WriteLine(" |                                                                            |");
                Console.WriteLine(" | All music is from Pokemon Red/Blue. Composer: Junichi Masuda               |");
				Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" | Made using C#                                                              |");
                Console.WriteLine(" |                                                                            |");
				Console.WriteLine(" |---------|------------------------------------------------------------------|");
				Console.WriteLine(" | Back: B |                                                                  |");
				Console.WriteLine(" |---------|------------------------------------------------------------------|");
			}

			#endregion
		}

		static void playBattleTransition()
		{
			if (bStartBattleTransition)
			{
				bStartBattleTransition = false;

				for (int i = 0; i < iOverworldHeight; i++)
				{
					for (int j = 0; j < iOverworldWidth; j++) caBattleTransition[i, j] = caDrawnOverworld[i, j];
				}

                soundRoute1.Stop();
                soundWildPokemonIntro.Play();
                tBattleMusicTimer.Interval = 44333.0;
                tBattleMusicTimer.Start();
			}

			if (bAllowBattleTransition)
			{
				bAllowBattleTransition = false;

				if (iBattleTransitionStage == 0)
				{
					caBattleTransition[0, iBattleTransIndex] = '*';
					caBattleTransition[1, iBattleTransIndex] = '*';
					caBattleTransition[2, iBattleTransIndex] = '*';
					caBattleTransition[3, iBattleTransIndex] = '*';
					caBattleTransition[4, iBattleTransIndex] = '*';
					caBattleTransition[5, iBattleTransIndex] = '*';
					caBattleTransition[6, iBattleTransIndex] = '*';
					caBattleTransition[7, iBattleTransIndex] = '*';
					caBattleTransition[8, iBattleTransIndex] = '*';
					caBattleTransition[9, iBattleTransIndex] = '*';
					caBattleTransition[10, iBattleTransIndex] = '*';
					caBattleTransition[11, iBattleTransIndex] = '*';
					caBattleTransition[12, iBattleTransIndex] = '*';
					caBattleTransition[13, iBattleTransIndex] = '*';
					caBattleTransition[14, iBattleTransIndex] = '*';
					caBattleTransition[15, iBattleTransIndex] = '*';
					caBattleTransition[16, iBattleTransIndex] = '*';
					caBattleTransition[17, iBattleTransIndex] = '*';
					caBattleTransition[18, iBattleTransIndex] = '*';
					caBattleTransition[19, iBattleTransIndex] = '*';
				}
				else if (iBattleTransitionStage == 1)
				{
					caBattleTransition[0, iBattleTransIndex] = caBattleScreen[0, iBattleTransIndex];
					caBattleTransition[1, iBattleTransIndex] = caBattleScreen[1, iBattleTransIndex];
					caBattleTransition[2, iBattleTransIndex] = caBattleScreen[2, iBattleTransIndex];
					caBattleTransition[3, iBattleTransIndex] = caBattleScreen[3, iBattleTransIndex];
					caBattleTransition[4, iBattleTransIndex] = caBattleScreen[4, iBattleTransIndex];
					caBattleTransition[5, iBattleTransIndex] = caBattleScreen[5, iBattleTransIndex];
					caBattleTransition[6, iBattleTransIndex] = caBattleScreen[6, iBattleTransIndex];
					caBattleTransition[7, iBattleTransIndex] = caBattleScreen[7, iBattleTransIndex];
					caBattleTransition[8, iBattleTransIndex] = caBattleScreen[8, iBattleTransIndex];
					caBattleTransition[9, iBattleTransIndex] = caBattleScreen[9, iBattleTransIndex];
					caBattleTransition[10, iBattleTransIndex] = caBattleScreen[10, iBattleTransIndex];
					caBattleTransition[11, iBattleTransIndex] = caBattleScreen[11, iBattleTransIndex];
					caBattleTransition[12, iBattleTransIndex] = caBattleScreen[12, iBattleTransIndex];
					caBattleTransition[13, iBattleTransIndex] = caBattleScreen[13, iBattleTransIndex];
					caBattleTransition[14, iBattleTransIndex] = caBattleScreen[14, iBattleTransIndex];
					caBattleTransition[15, iBattleTransIndex] = caBattleScreen[15, iBattleTransIndex];
					caBattleTransition[16, iBattleTransIndex] = caBattleScreen[16, iBattleTransIndex];
					caBattleTransition[17, iBattleTransIndex] = caBattleScreen[17, iBattleTransIndex];
					caBattleTransition[18, iBattleTransIndex] = caBattleScreen[18, iBattleTransIndex];
					caBattleTransition[19, iBattleTransIndex] = caBattleScreen[19, iBattleTransIndex];
				}

				iBattleTransIndex++;

				if (iBattleTransIndex == iOverworldWidth)
				{
					iBattleTransIndex = 0;

					if (iBattleTransitionStage == 0)
					{
						iBattleTransitionStage++;
						setDefualtPokeInfo();
						updateBattleScreen();
					}
					else if (iBattleTransitionStage == 1)
					{
						iBattleTransitionStage = 0;
						bdrawingBattleTransition = false;
						gsGameState = GameState.Battle;
						bBattleBegun = true;
					}
				}
			}
		}
			
		static string getScreenRow(char[,] caScreen, int iRowNum)
		{
			string sRow = "";

			for (int i = 0; i < iOverworldWidth; i++) sRow += caScreen[iRowNum, i];

			return sRow;
		}

		static string prepareScreenForDraw(char[,] caScreen)
		{
			string sScreen = "";

			for (int i = 0; i < 20; i++) sScreen += (" |" + getScreenRow(caScreen, i) + "|\n");

			return sScreen;
		}

		static void drawPlayerInOverworld()
		{
			caDrawnOverworld[iPreviousYLocation, iPreviousXLocation] = caOverworld[iPreviousYLocation, iPreviousXLocation];
			caDrawnOverworld[iPlayerYLocation, iPlayerXLocation] = cDrawnPlayer;
		}

		#endregion

		#region Battle System

		static void manageBattle()
		{
			string sInput = "";

			if (bBattleBegun)
			{
				bBattleBegun = false;
				bBattleOver = false;
				bDrawBattle = true;

				pCurrentEnemyPokemon = getEnemyPokemon();

				allowAllPokeDraw();

				while (Console.KeyAvailable) Console.ReadKey(false);

				addBattleMessage("A wild " + pCurrentEnemyPokemon.NickName + " appeared!");
				showBattleMessages();

				return;
			}

            if (bBattleOver) return;

			if (tBattleMessageTimer.Enabled || bDisplayingBattleMessages)
			{
				while(Console.KeyAvailable) Console.ReadKey(false);
			}
			if (!tBattleMessageTimer.Enabled && !bDisplayingBattleMessages) sInput = Console.ReadKey(true).Key.ToString();

			#region Attack

			if (sInput == sAttackKey && !bShowMenuInBattle)
			{
				if (lpPokemon[0].Speed > pCurrentEnemyPokemon.Speed || lpPokemon[0].Speed == pCurrentEnemyPokemon.Speed)
				{
					handlePlayerAttacksFirst(ref pCurrentEnemyPokemon);
				}
				else
				{
					handleEnemyAttacksFirst(ref pCurrentEnemyPokemon);
				}
			}

      #endregion
			else if (sInput == sMenuKey && !bShowMenuInBattle) bShowMenuInBattle = true;

			else if (sInput == sBackKey && bShowMenuInBattle) bShowMenuInBattle = false;

			else if (sInput == sRunKey && !bShowMenuInBattle)
			{
				addBattleMessage("Got away safely.", BattleMessageType.BattleEnd);
				showBattleMessages();
			}
		}

		static Pokemon getEnemyPokemon()
		{
			return aCurrentArea.getWildPokemon();
		}

		static void updatePokemonDraw()
		{
			updatePlayerPokemonDraw(lpPokemon[0]);
			updateEnemyPokemonDraw(pCurrentEnemyPokemon);
		}

		static void updatePlayerPokemonDraw(Pokemon pPlayerPokemon)
		{
			sPlayerPokemonName = formatPokeName(pPlayerPokemon.NickName);
			if(bUpdatePlayerPokemonCurrentHealth) sPlayerPokemonCurrentHealth = formatPokeHealth(pPlayerPokemon.CurrentHealth.ToString());
			if(bUpdatePlayerPokemonMaxHealth) sPlayerPokemonMaxHealth = formatPokeMaxHealth(pPlayerPokemon.MaxHealth.ToString());
			if(bUpdatePlayerPokemonLevel) sPlayerPokemonLevel = formatPokeLevel(pPlayerPokemon.Level.ToString());
			sPlayerPokemonType = formatePokeType(pPlayerPokemon.Type.ToString());
		}

		static void updateEnemyPokemonDraw(Pokemon pEnemyPokemon)
		{
			sEnemyPokemonName = formatPokeName(pEnemyPokemon.NickName);
			if(bUpdateEnemyPokemonCurrentHealth) sEnemyPokemonCurrentHealth = formatPokeHealth(pEnemyPokemon.CurrentHealth.ToString());
			sEnemyPokemonMaxHealth = formatPokeMaxHealth(pEnemyPokemon.MaxHealth.ToString());
			sEnemyPokemonLevel = formatPokeLevel(pEnemyPokemon.Level.ToString());
			sEnemyPokemonType = formatePokeType(pEnemyPokemon.Type.ToString());
		}

		static void allowAllPokeDraw()
		{
			bUpdatePlayerPokemonCurrentHealth = true;
			bUpdatePlayerPokemonMaxHealth = true;
			bUpdatePlayerPokemonLevel = true;

			bUpdateEnemyPokemonCurrentHealth = true;
		}

		static void resetAllPokeDraw()
		{
			bUpdatePlayerPokemonCurrentHealth = false;
			bUpdatePlayerPokemonMaxHealth = false;
			bUpdatePlayerPokemonLevel = false;

			bUpdateEnemyPokemonCurrentHealth = false;
		}

		static void setDefualtPokeInfo()
		{
			sPlayerPokemonName = "??????????";
			sPlayerPokemonCurrentHealth = " ??";
			sPlayerPokemonMaxHealth = "?? ";
			sPlayerPokemonLevel = "?? ";
			sPlayerPokemonType = "?????";

			sEnemyPokemonName = "??????????";
			sEnemyPokemonCurrentHealth = " ??";
			sEnemyPokemonMaxHealth = "?? ";
			sEnemyPokemonLevel = "?? ";
			sEnemyPokemonType = "?????";
		}

		static void handlePlayerAttacksFirst(ref Pokemon pEnemyPokemon)
		{
			Pokemon.PokeTypeAdvantage taAdvantage1 = Pokemon.getTypeAdvantage(lpPokemon[0], pEnemyPokemon);
			int iDamage1 = Pokemon.calculateDamage(lpPokemon[0], pEnemyPokemon);

			addBattleMessage(lpPokemon[0].NickName + " attacked first!");

			if (iDamage1 >= pEnemyPokemon.CurrentHealth)
			{
				addBattleMessage("The " + pEnemyPokemon.NickName + " took " + pEnemyPokemon.CurrentHealth + " damage.", BattleMessageType.EnemyHealth);
				pEnemyPokemon.CurrentHealth = 0;
			}
			else
			{
				pEnemyPokemon.CurrentHealth -= iDamage1;
				addBattleMessage("The " + pEnemyPokemon.NickName + " took " + iDamage1 + " damage.", BattleMessageType.EnemyHealth);
			}

			if (taAdvantage1 == Pokemon.PokeTypeAdvantage.Advantage) addBattleMessage("It was Super Effective!");
			else if (taAdvantage1 == Pokemon.PokeTypeAdvantage.Disadvantage) addBattleMessage("It was not very effective.");

			if (pEnemyPokemon.CurrentHealth > 0)
			{
				Pokemon.PokeTypeAdvantage taAdvantage2 = Pokemon.getTypeAdvantage(pEnemyPokemon, lpPokemon[0]);
				int iDamage2 = Pokemon.calculateDamage(pEnemyPokemon, lpPokemon[0]);

				addBattleMessage("The " + pEnemyPokemon.NickName + " attacked.");

				if (iDamage2 >= lpPokemon[0].CurrentHealth)
				{
					addBattleMessage(lpPokemon[0].NickName + " took " + lpPokemon[0].CurrentHealth + " damage.", BattleMessageType.PlayerHealth);
					lpPokemon[0].CurrentHealth = 0;
				}
				else
				{
					lpPokemon[0].CurrentHealth -= iDamage2;
					addBattleMessage(lpPokemon[0].NickName + " took " + iDamage2 + " damage.", BattleMessageType.PlayerHealth);
				}

				if (taAdvantage2 == Pokemon.PokeTypeAdvantage.Advantage) addBattleMessage("It was Super Effective!");
				else if (taAdvantage2 == Pokemon.PokeTypeAdvantage.Disadvantage) addBattleMessage("It was not very effective.");

				if (lpPokemon[0].CurrentHealth == 0)
				{
					addBattleMessage(lpPokemon[0].NickName + " fainted!");
					addBattleMessage("You fled the battle!", BattleMessageType.BattleEnd, 3000.0);
				}
			}
			else
			{
				int iExperience = Pokemon.calculateExperience(lpPokemon[0], pEnemyPokemon);

				lpPokemon[0].Experience += iExperience;

				bool canLevelUp = lpPokemon[0].hasLeveledUp();

				if (!canLevelUp)
				{
					if (lpPokemon[0].Level != Pokemon.LevelCap)
					{
						addBattleMessage("The " + pEnemyPokemon.NickName + " fainted!", BattleMessageType.EnemyFainted);
						addBattleMessage(lpPokemon[0].NickName + " received " + iExperience + " experience!", BattleMessageType.BattleEnd);
					}
					else addBattleMessage("The " + pEnemyPokemon.NickName + " fainted!", BattleMessageType.BattleEnd);
				}
				else
				{
					int iOldHP = lpPokemon[0].MaxHealth;
					int iOldAttack = lpPokemon[0].Attack;
					int iOldDefense = lpPokemon[0].Defense;
					int iOldSpeed = lpPokemon[0].Speed;

					addBattleMessage("The " + pEnemyPokemon.NickName + " fainted!", BattleMessageType.EnemyFainted);

                    addBattleMessage(lpPokemon[0].NickName + " received " + iExperience + " experience!");

					lpPokemon[0].levelUp();

					addBattleMessage(lpPokemon[0].NickName + " reached Level " + lpPokemon[0].Level + "!", BattleMessageType.PlayerLevelUp);
					addBattleMessage("(HP: " + iOldHP + " -> " + lpPokemon[0].MaxHealth + ") " + "(Atk: " + iOldAttack + " -> " + lpPokemon[0].Attack + ") " +
													 "(Def: " + iOldDefense + " -> " + lpPokemon[0].Defense + ") " + "(Spd: " + iOldSpeed + " -> " + lpPokemon[0].Speed + ")", BattleMessageType.BattleEnd, 5000.0);
				}
			}

			showBattleMessages();
		}

		static void handleEnemyAttacksFirst(ref Pokemon pEnemyPokemon)
		{
			Pokemon.PokeTypeAdvantage taAdvantage1 = Pokemon.getTypeAdvantage(pEnemyPokemon, lpPokemon[0]);
			int iDamage1 = Pokemon.calculateDamage(pEnemyPokemon, lpPokemon[0]);

			addBattleMessage("The " + pEnemyPokemon.NickName + " attacked first!");

			if (iDamage1 >= lpPokemon[0].CurrentHealth)
			{
				addBattleMessage(lpPokemon[0].NickName + " took " + lpPokemon[0].CurrentHealth + " damage.", BattleMessageType.PlayerHealth);
				lpPokemon[0].CurrentHealth = 0;
			}
			else
			{
				lpPokemon[0].CurrentHealth -= iDamage1;
				addBattleMessage(lpPokemon[0].NickName + " took " + iDamage1 + " damage.", BattleMessageType.PlayerHealth);
			}

			if (taAdvantage1 == Pokemon.PokeTypeAdvantage.Advantage) addBattleMessage("It was Super Effective!");
			else if (taAdvantage1 == Pokemon.PokeTypeAdvantage.Disadvantage) addBattleMessage("It was not very effective.");

			if (lpPokemon[0].CurrentHealth > 0)
			{
				Pokemon.PokeTypeAdvantage taAdvantage2 = Pokemon.getTypeAdvantage(lpPokemon[0], pEnemyPokemon);
				int iDamage2 = Pokemon.calculateDamage(lpPokemon[0], pEnemyPokemon);

				addBattleMessage(lpPokemon[0].NickName + " attacked.");

				if (iDamage2 >= pEnemyPokemon.CurrentHealth)
				{
					addBattleMessage("The " + pEnemyPokemon.NickName + " took " + pEnemyPokemon.CurrentHealth + " damage.", BattleMessageType.EnemyHealth);
					pEnemyPokemon.CurrentHealth = 0;
				}
				else
				{
					pEnemyPokemon.CurrentHealth -= iDamage2;
					addBattleMessage("The " + pEnemyPokemon.NickName + " took " + iDamage2 + " damage.", BattleMessageType.EnemyHealth);
				}

				if (taAdvantage2 == Pokemon.PokeTypeAdvantage.Advantage) addBattleMessage("It was Super Effective!");
				else if (taAdvantage2 == Pokemon.PokeTypeAdvantage.Disadvantage) addBattleMessage("It was not very effective.");

				if (pEnemyPokemon.CurrentHealth == 0)
				{
					int iExperience = Pokemon.calculateExperience(lpPokemon[0], pEnemyPokemon);

					lpPokemon[0].Experience += iExperience;

					bool canLevelUp = lpPokemon[0].hasLeveledUp();

					if (!canLevelUp)
					{
						if (lpPokemon[0].Level != Pokemon.LevelCap)
						{
							addBattleMessage("The " + pEnemyPokemon.NickName + " fainted!", BattleMessageType.EnemyFainted);
							addBattleMessage(lpPokemon[0].NickName + " received " + iExperience + " experience!", BattleMessageType.BattleEnd);
						}
						else addBattleMessage("The " + pEnemyPokemon.NickName + " fainted!", BattleMessageType.BattleEnd);
					}
					else
					{
						int iOldHP = lpPokemon[0].MaxHealth;
						int iOldAttack = lpPokemon[0].Attack;
						int iOldDefense = lpPokemon[0].Defense;
						int iOldSpeed = lpPokemon[0].Speed;

                        addBattleMessage("The " + pEnemyPokemon.NickName + " fainted!", BattleMessageType.EnemyFainted);

						addBattleMessage(lpPokemon[0].NickName + " received " + iExperience + " experience!");

						lpPokemon[0].levelUp();

						addBattleMessage(lpPokemon[0].NickName + " reached Level " + lpPokemon[0].Level + "!", BattleMessageType.PlayerLevelUp);
						addBattleMessage("(HP: " + iOldHP + " -> " + lpPokemon[0].MaxHealth + ") " + "(Atk: " + iOldAttack + " -> " + lpPokemon[0].Attack + ") " +
														 "(Def: " + iOldDefense + " -> " + lpPokemon[0].Defense + ") " + "(Spd: " + iOldSpeed + " -> " + lpPokemon[0].Speed + ")", BattleMessageType.BattleEnd, 5000.0);
					}
				}
			}
			else
			{
				addBattleMessage(lpPokemon[0].NickName + " fainted!");
				addBattleMessage("You fled the battle!", BattleMessageType.BattleEnd, 3000.0);
			}

			showBattleMessages();
		}

		static void healPlayerPokemon()
		{
			lpPokemon[0].CurrentHealth = lpPokemon[0].MaxHealth;
		}

		static void addBattleMessage(string sMessage, BattleMessageType mtMessageType = BattleMessageType.Normal, double dMessageDuration = 2000.0)
		{
			lsBattleMessages.Add(formatBattleMessage(sMessage));
			ldBattleMessageDurations.Add(dMessageDuration);
			lmtBattleMessageTypes.Add(mtMessageType);
		}

		static void showBattleMessages()
		{
			if (lsBattleMessages.Count > 0)
			{
				tBattleMessageTimer.Interval = ldBattleMessageDurations[0];
				checkBattleMessageType(lmtBattleMessageTypes[0]);
				tBattleMessageTimer.Enabled = true;
				bDisplayingBattleMessages = true;
			}
		}

		static void checkBattleMessageType(BattleMessageType mtMessageType)
		{
			if (mtMessageType == BattleMessageType.PlayerHealth) bUpdatePlayerPokemonCurrentHealth = true;

			else if (mtMessageType == BattleMessageType.EnemyHealth) bUpdateEnemyPokemonCurrentHealth = true;

			else if (mtMessageType == BattleMessageType.PlayerLevelUp)
			{
				bUpdatePlayerPokemonMaxHealth = true;
				bUpdatePlayerPokemonLevel = true;
			}

			else if (mtMessageType == BattleMessageType.EnemyFainted)
			{
				bshowFaint = true;
                bVictoryMusicReached = true;
                soundWildPokemonVictory.Play();
			}

			else if (mtMessageType == BattleMessageType.BattleEnd) bBattleOver = true;
		}

		#region Format Battle Output

		static string formatBattleMessage(string sBattleMessage)
		{
			string sFormatedBattleMessage = sBattleMessage;
			int iBattleMessageLength = sBattleMessage.Length;

			if (iBattleMessageLength < iMaxBattleMessage)
			{
				for (int i = 0; i < (iMaxBattleMessage - iBattleMessageLength); i++)
				{
					sFormatedBattleMessage += " ";
				}
			}

			return sFormatedBattleMessage;
		}

		static string formatPokeName(string sName)
		{
			string sFormatedName = sName;
			int iNameLength = sName.Length;

			if (iNameLength < iMaxNameLength)
			{
				for (int i = 0; i < (iMaxNameLength - iNameLength); i++)
				{
					sFormatedName += " ";
				}
			}

			return sFormatedName;
		}

		static string formatePokeType(string sType)
		{
			string sFormatedType = sType;
			int iTypeLength = sType.Length;

			if (iTypeLength < iMaxTypeLength)
			{
				for (int i = 0; i < (iMaxTypeLength - iTypeLength); i++)
				{
					sFormatedType += " ";
				}
			}

			return sFormatedType;
		}

		static string formatPokeHealth(string sHealth)
		{
			string sFormatedHealth = "";
			int iHealthLength = sHealth.Length;

			if (iHealthLength < iMaxHealthLength)
			{
				for (int i = 0; i < (iMaxHealthLength - iHealthLength); i++)
				{
					sFormatedHealth += " ";
				}

				sFormatedHealth += sHealth;
			}

			return sFormatedHealth;
		}

		static string formatPokeMaxHealth(string sHealth)
		{
			string sFormatedHealth = sHealth;
			int iHealthLength = sHealth.Length;

			if (iHealthLength < iMaxHealthLength)
			{
				for (int i = 0; i < (iMaxHealthLength - iHealthLength); i++)
				{
					sFormatedHealth += " ";
				}
			}

			return sFormatedHealth;
		}

		static string formatPokeLevel(string sLevel)
		{
			string sFormatedLevel = sLevel;
			int iLevelLength = sLevel.Length;

			if (iLevelLength < iMaxLevelLength)
			{
				for (int i = 0; i < (iMaxLevelLength - iLevelLength); i++)
				{
					sFormatedLevel += " ";
				}
			}

			return sFormatedLevel;
		}

		static string formatPokeStat(string sStat)
		{
			string sFormatedStat = sStat;
			int iStatLength = sStat.Length;

			if (iStatLength < iMaxStatLength)
			{
				for (int i = 0; i < (iMaxStatLength - iStatLength); i++)
				{
					sFormatedStat += " ";
				}
			}

			return sFormatedStat;
		}

		static string formatPokeExp(string sExp)
		{
			string sFormatedExp = sExp;
			int iExpLength = sExp.Length;

			if (iExpLength < iMaxExpLength)
			{
				for (int i = 0; i < (iMaxExpLength - iExpLength); i++)
				{
					sFormatedExp += " ";
				}
			}

			return sFormatedExp;
		}

		static string formatPokeExpToNextLevel(string sExpToNextLevel)
		{
			string sFormatedExpToNextLevel = sExpToNextLevel;
			int iExpToNextLevelLength = sExpToNextLevel.Length;

			if (iExpToNextLevelLength < iMaxExpToNextLevelLength)
			{
				for (int i = 0; i < (iMaxExpToNextLevelLength - iExpToNextLevelLength); i++)
				{
					sFormatedExpToNextLevel += " ";
				}
			}

			return sFormatedExpToNextLevel;
		}

		#endregion

		#endregion
	}
}
