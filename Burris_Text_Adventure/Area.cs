/**********************************************************************************************************************
 * Title: Area
 * Description: Creates the areas the player can explore and battle Pokemon
 * Author: Chris Burris (CB), cburris@chrisbburris.com
 * Date: 5/14/13
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextAdventure
{
	class Area
	{
		private class AreaPokeInfo
		{
			public enum PokeRarity {Common, Uncommon, Rare};

			private Pokemon.PokeSpecies psSpecies;
			public Pokemon.PokeSpecies Species { get { return psSpecies; } }

			private int iLevelMin;
			public int LevelMin { get { return iLevelMin; } }

			private int iLevelMax;
			public int LevelMax { get { return iLevelMax; } }

			private PokeRarity prRarity;
			public PokeRarity Rarity { get { return prRarity; } }

			public AreaPokeInfo (Pokemon.PokeSpecies psSpecies, int iLevelMin, int iLevelMax, PokeRarity prRarity = PokeRarity.Common)
			{
				this.psSpecies = psSpecies;
				this.iLevelMin = iLevelMin;
				this.iLevelMax = iLevelMax;
				this.prRarity = prRarity;
			}
		}

		#region Areas

		#region Area 1

		private static string sLandscape1 = 
		"#############################^^^^^^^^^^^^###################################" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"<                                                                          >" +
		"<                                                                          >" +
		"<                                                                          >" +
		"<                                                                          >" +
		"#'''''''''''''''''''''''######################'''''''''''''''''''''''''''''#" +
		"#'''''''''''''''''''''''######################'''''''''''''''''''''''''''''#" +
		"#'''''''''''''''''''''''######################'''''''''''''''''''''''''''''#" +
		"#'''''''''''''''''''''''######################'''''''''''''''''''''''''''''#" +
		"#'''''''''''''''''''''''######################'''''''''''''''''''''''''''''#" +
		"#'''''''''''''''''''''''######################'''''''''''''''''''''''''''''#" +
		"############################################################################";

		private static List<AreaPokeInfo> lpArea1Pokemon = new List<AreaPokeInfo>()
		{
			new AreaPokeInfo(Pokemon.PokeSpecies.Bellsprout, 2, 2),
 			new AreaPokeInfo(Pokemon.PokeSpecies.Psyduck, 2, 2),
			new AreaPokeInfo(Pokemon.PokeSpecies.Growlithe, 2, 2)
		};

		private static Area aArea1 = new Area(sLandscape1, lpArea1Pokemon);

		#endregion

		#region Area 2

		private static string sLandscape2 =
		"############################################################################" +
		"#############################'''''''''''''''''##############################" +
		"#############################'''''''''''''''''##############################" +
		"#############################'''''''''''''''''##############################" +
		"#############################'''''''''''''''''##############################" +
		"#############################'''''''''''''''''##############################" +
		"#############################'''''''''''''''''##############################" +
		"#'''''''''''''''''''''''''''''''''''''''''''''##############################" +
		"#'''''''''''''''''''''''''''''''''''''''''''''##############################" +
		"#'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''               >" +
		"#'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''               >" +
		"#'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''               >" +
		"#'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''               >" +
		"#'''''''''''''''''''''''''''''''''''''''''''''##############################" +
		"#'''''''''''''''''''''''''''''''''''''''''''''##############################" +
		"#############################'''''''''''''''''##############################" +
		"#############################'''''''''''''''''##############################" +
		"#############################'''''''''''''''''##############################" +
		"#############################'''''''''''''''''##############################" +
		"############################################################################";

		private static List<AreaPokeInfo> lpArea2Pokemon = new List<AreaPokeInfo>()
		{
			new AreaPokeInfo(Pokemon.PokeSpecies.Vulpix, 2, 2),
			new AreaPokeInfo(Pokemon.PokeSpecies.Staryu, 2, 2),
			new AreaPokeInfo(Pokemon.PokeSpecies.Oddish, 2, 2)
		};

		private static Area aArea2 = new Area(sLandscape2, lpArea2Pokemon);

		#endregion

		#region Area 3

		private static string sLandscape3 =
		"############################################################################" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''######'''''''''''''''''''''''''''######'''''''''''''''''''''#" +
		"#''''''''''''''######'''''''''''''''''''''''''''######'''''''''''''''''''''#" +
		"#''''''''''''''######'''''''''''''''''''''''''''######'''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''#################          #######################''''''''''''#" +
		"#''''''''''''                                                  ''''''''''''#" +
		"#''''''''''''                                                  ''''''''''''#" +
		"#''''''''''''#################          #######################''''''''''''#" +
		"#''''''''''''''''''''''''''''#          #''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''#          #''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''#          #''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''#          #''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
		"#############################vvvvvvvvvvvv###################################";

		private static List<AreaPokeInfo> lpArea3Pokemon = new List<AreaPokeInfo>()
		{
 			new AreaPokeInfo(Pokemon.PokeSpecies.Bellsprout, 4, 7),
			new AreaPokeInfo(Pokemon.PokeSpecies.Vulpix, 4, 7),
			new AreaPokeInfo(Pokemon.PokeSpecies.Staryu, 4, 7)
		};

		private static Area aArea3 = new Area(sLandscape3, lpArea3Pokemon);

		#endregion

		#region Area 4

		private static string sLandscape4 =
		"############################################################################" +
		"###############################                ''''''''''''''''''''''''''''#" +
		"###############################                ''''''''''''''''''''''''''''#" +
		"###############################                ''''''''#############'''''''#" +
		"###############################                ''''''''#############'''''''#" +
		"###############################                ''''''''#############'''''''#" +
		"###############################                ''''''''#############'''''''#" +
		"###############################                ''''''''#############'''''''#" +
		"###############################                ''''''''''''''''''''''''''''#" +
		"<                                              ''''''''''''''''''''''''''''#" +
		"<                                              ''''''''''''''''''''''''''''#" +
		"<                                              ''''''''''''''''''''''''''''#" +
		"<                                              ''''''''#############'''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''#############'''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''#############'''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''#############'''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''#############'''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''#############'''''''#" +
		"#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''#" +
		"############################################################################";

		private static List<AreaPokeInfo> lpArea4Pokemon = new List<AreaPokeInfo>()
		{
 			new AreaPokeInfo(Pokemon.PokeSpecies.Psyduck, 2, 3),
			new AreaPokeInfo(Pokemon.PokeSpecies.Growlithe, 2, 3),
			new AreaPokeInfo(Pokemon.PokeSpecies.Oddish, 2, 3)
		};

		private static Area aArea4 = new Area(sLandscape4, lpArea4Pokemon);

		#endregion

        #region Area 5

        private static string sLandscape5 =
        "#############################^^^^^^^^^^^^###################################" +
        "#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
        "#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
        "#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
        "#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
        "#'''''''''''''''''''#########            #########'''''''''''''''''''''''''#" +
        "#'''''''''''''''''''#                            #'''''''''''''''''''''''''#" +
        "#'''''''''''''''''''#                            #'''''''''''''''''''''''''#" +
        "#######     #########                            #############     #########" +
        "<                                ''''                                      >" +
        "<                               ''''''                                     >" +
        "<                               ''''''                                     >" +
        "<                                ''''                                      >" +
        "#######     #########                            #############     #########" +
        "#'''''''''''''''''''#                            #'''''''''''''''''''''''''#" +
        "#'''''''''''''''''''#                            #'''''''''''''''''''''''''#" +
        "#'''''''''''''''''''#########            #########'''''''''''''''''''''''''#" +
        "#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
        "#''''''''''''''''''''''''''''            ''''''''''''''''''''''''''''''''''#" +
        "#############################vvvvvvvvvvvv###################################";

        private static List<AreaPokeInfo> lpArea5Pokemon = new List<AreaPokeInfo>()
		{
            new AreaPokeInfo(Pokemon.PokeSpecies.Staryu, 2, 3),
			new AreaPokeInfo(Pokemon.PokeSpecies.Bellsprout, 2, 3),
			new AreaPokeInfo(Pokemon.PokeSpecies.Growlithe, 2, 3)
		};

        private static Area aArea5 = new Area(sLandscape5, lpArea5Pokemon);

        #endregion

        #region Area 6

        private static string sLandscape6 =
        "############################################################################" +
        "#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''#" +
        "#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''#" +
        "#''''''                                  ''''''''''''''''''''''''''''''''''#" +
        "#''''''      #################           ''''''''''''''''''''''''''''''''''#" +
        "#''''''      #'''''''''''''''#           ''''''''''''''''''''''''''''''''''#" +
        "#''''''      #'''''''''''''''#           ''''''''''''''''''''''''''''''''''#" +
        "#''''''      #'''''''''''''''#           ''''''''''''''''''''''''''''''''''#" +
        "#''''''      #####'''''''#####           ''''''''''''''''''''''''''''''''''#" +
        "#''''''                                                                    >" +
        "#''''''                                                                    >" +
        "#''''''                                                                    >" +
        "#''''''                                                                    >" +
        "#''''''''''''''''''''''''''''''''''''''''        ###'''''''###       ''''''#" +
        "#''''''''''''''''''''''''''''''''''''''''        #'''''''''''#       ''''''#" +
        "#''''''''''''''''''''''''''''''''''''''''        #'''''''''''#       ''''''#" +
        "#''''''''''''''''''''''''''''''''''''''''        #############       ''''''#" +
        "#''''''''''''''''''''''''''''''''''''''''                            ''''''#" +
        "#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''#" +
        "############################################################################";

        private static List<AreaPokeInfo> lpArea6Pokemon = new List<AreaPokeInfo>()
		{
            new AreaPokeInfo(Pokemon.PokeSpecies.Vulpix, 3, 4),
 			new AreaPokeInfo(Pokemon.PokeSpecies.Psyduck, 3, 4),
			new AreaPokeInfo(Pokemon.PokeSpecies.Oddish, 3, 4)
		};

        private static Area aArea6 = new Area(sLandscape6, lpArea6Pokemon);

        #endregion

        #region Area 7

        private static string sLandscape7 =
        "############################################################################" +
        "#''''''''''''''''''''''''''''############''''''''''''''''''''''''''''''''''#" +
        "#''''''''''''''''''''''''''''############''''''''''''''''''''''''''''''''''#" +
        "#''''''''######''''''''''''''#######################              '''''''''#" +
        "#''''''''######''''''''''''''#######################              '''''''''#" +
        "#''''''''######''''''''''''''############'''''''''''              '''''''''#" +
        "#''''''''######''''''''''''''############'''''''''''              '''''''''#" +
        "#''''''''######''''''''''''''############'''''''''''              '''''''''#" +
        "#''''''''######''''''''''''''############'''''''''''    ##########''''''####" +
        "<                                                       ##########''''''####" +
        "<                                                       ##########''''''####" +
        "<                                                       ##########''''''####" +
        "<                                        '''''''''''              '''''''''#" +
        "#''''##''''''''''''''''                  '''''''''''              '''''''''#" +
        "#''''##''''''''''''''''                  '''''''''''              '''''''''#" +
        "#''''##''''''''''''''''                  '''''''''''              '''''''''#" +
        "#''''##''''''''''''''''''''####          ###########              '''''''''#" +
        "#''''##''''''''''''''''''''####''''''''''###########'''''''''''''''''''''''#" +
        "#''''##########################''''''''''###########'''''''''''''''''''''''#" +
        "############################################################################";

        private static List<AreaPokeInfo> lpArea7Pokemon = new List<AreaPokeInfo>()
		{
            new AreaPokeInfo(Pokemon.PokeSpecies.Oddish, 3, 5),
 			new AreaPokeInfo(Pokemon.PokeSpecies.Psyduck, 3, 5),
			new AreaPokeInfo(Pokemon.PokeSpecies.Growlithe, 3, 5)	
		};

        private static Area aArea7 = new Area(sLandscape7, lpArea7Pokemon);

        #endregion

		#endregion

		#region Variables

		private static List<Area> laAreas = new List<Area>();
		public static List<Area> Areas { get { initAreas(); return laAreas; } }

		private string sLandscape;
		public string Landscape { get { return sLandscape; } }

		private List<AreaPokeInfo> lpPokemonInThisArea = new List<AreaPokeInfo>();

		private Area aNorthernNeighbor;
		public Area NorthernNeighbor { get { return aNorthernNeighbor; } }
		private Area SetNorthernNeighbor { set { aNorthernNeighbor = value; bHasNorthernNeighbor = true; } }

		private Area aSouthernNeighbor;
		public Area SouthernNeighbor { get { return aSouthernNeighbor; } }
		private Area SetSouthernNeighbor { set { aSouthernNeighbor = value; bHasSouthernNeighbor = true; } }

		private Area aEasternNeighbor;
		public Area EasternNeighbor { get { return aEasternNeighbor; } }
		private Area SetEasternNeighbor { set { aEasternNeighbor = value; bHasEasternNeighbor = true; } }

		private Area aWesternNeighbor;
		public Area WesternNeighbor { get { return aWesternNeighbor; } }
		private Area SetWesternNeighbor { set { aWesternNeighbor = value; bHasWesternNeighbor = true; } }

		private bool bHasNorthernNeighbor = false;
		public bool HasNorthernNeighbor { get { return bHasNorthernNeighbor; } }

		private bool bHasSouthernNeighbor = false;
		public bool HasSouthernNeighbor { get { return bHasSouthernNeighbor; } }

		private bool bHasEasternNeighbor = false;
		public bool HasEasternNeighbor { get { return bHasEasternNeighbor; } }

		private bool bHasWesternNeighbor = false;
		public bool HasWesternNeighbor { get { return bHasWesternNeighbor; } }

		#endregion

		private Area (string sLandscape, List<AreaPokeInfo> lpPokemonInArea)
		{
			this.sLandscape = sLandscape;
			lpPokemonInThisArea = lpPokemonInArea;
		}

		private static void initAreas()
		{
			assignAreaNeighbors();

			laAreas.Add(aArea1);
			laAreas.Add(aArea2);
			laAreas.Add(aArea3);
			laAreas.Add(aArea4);
            laAreas.Add(aArea5);
            laAreas.Add(aArea6);
            laAreas.Add(aArea7);
		}

		private static void assignAreaNeighbors()
		{
			aArea1.SetNorthernNeighbor = aArea5;
			aArea1.SetWesternNeighbor = aArea2;
			aArea1.SetEasternNeighbor = aArea4;

			aArea2.SetEasternNeighbor = aArea1;

			aArea3.SetSouthernNeighbor = aArea5;

			aArea4.SetWesternNeighbor = aArea1;

            aArea5.SetNorthernNeighbor = aArea3;
            aArea5.SetSouthernNeighbor = aArea1;
            aArea5.SetWesternNeighbor = aArea6;
            aArea5.SetEasternNeighbor = aArea7;

            aArea6.SetEasternNeighbor = aArea5;

            aArea7.SetWesternNeighbor = aArea5;
		}

		public Pokemon getWildPokemon()
		{
			Random rRandom = new Random(DateTime.Now.Second);
			int iPokeIndex = rRandom.Next(0, lpPokemonInThisArea.Count);
			
			Pokemon.PokeSpecies psPokeSpecies = lpPokemonInThisArea[iPokeIndex].Species;
			int iPokeLevel = rRandom.Next(lpPokemonInThisArea[iPokeIndex].LevelMin, (lpPokemonInThisArea[iPokeIndex].LevelMax + 1));

			return new Pokemon(psPokeSpecies, iPokeLevel);
		}
	}
}
