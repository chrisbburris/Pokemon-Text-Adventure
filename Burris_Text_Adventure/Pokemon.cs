/**********************************************************************************************************************
 * Title: Pokemon
 * Description: Defines what a Pokemon is and how it battles and levels up
 * Author: Chris Burris (CB), cburris@chrisbburris.com
 * Date: 5/14/13
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonTextAdventure
{
	class Pokemon
	{
		#region Class Variables

		public enum PokeSpecies { Bulbasaur, Charmander, Squirtle, Oddish, Growlithe, Psyduck, Bellsprout, Vulpix, Staryu };
		public enum PokeType { Grass, Fire, Water };
		public enum PokeTypeAdvantage { Neutral, Advantage, Disadvantage };
		private enum PokeStat { Health, Attack, Defense, Speed, Experience };

		#region Pokemon Stats

		private const int iLevelCap = 10;
		public static int LevelCap { get { return iLevelCap; } }

		private const int iBaseExperience = 10;
		private const int iExperinceCap = 1000;

		#region Bulbasaur's Stats
		
		private static Dictionary<PokeStat, int>[] BulbasaurStats = 
		{
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 10}, {PokeStat.Attack, 4}, {PokeStat.Defense, 1}, {PokeStat.Speed, 3}, {PokeStat.Experience, 100} }, // Level 1
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 12}, {PokeStat.Attack, 5}, {PokeStat.Defense, 1}, {PokeStat.Speed, 6}, {PokeStat.Experience, 200} }, // Level 2
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 15}, {PokeStat.Attack, 6}, {PokeStat.Defense, 2}, {PokeStat.Speed, 9}, {PokeStat.Experience, 300} }, // Level 3
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 17}, {PokeStat.Attack, 7}, {PokeStat.Defense, 2}, {PokeStat.Speed, 12}, {PokeStat.Experience, 400} }, // Level 4
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 20}, {PokeStat.Attack, 8}, {PokeStat.Defense, 3}, {PokeStat.Speed, 15}, {PokeStat.Experience, 500} }, // Level 5
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 22}, {PokeStat.Attack, 9}, {PokeStat.Defense, 3}, {PokeStat.Speed, 18}, {PokeStat.Experience, 600} }, // Level 6
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 25}, {PokeStat.Attack, 10}, {PokeStat.Defense, 4}, {PokeStat.Speed, 21}, {PokeStat.Experience, 700} }, // Level 7
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 27}, {PokeStat.Attack, 11}, {PokeStat.Defense, 4}, {PokeStat.Speed, 24}, {PokeStat.Experience, 800} }, // Level 8
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 30}, {PokeStat.Attack, 12}, {PokeStat.Defense, 5}, {PokeStat.Speed, 27}, {PokeStat.Experience, 900} }, // Level 9
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 32}, {PokeStat.Attack, 13}, {PokeStat.Defense, 5}, {PokeStat.Speed, 30}, {PokeStat.Experience, 1000} } // Level 10
		};

		#endregion

		#region Charmander's Stats

		private static Dictionary<PokeStat, int>[] CharmanderStats = 
		{
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 10}, {PokeStat.Attack, 5}, {PokeStat.Defense, 1}, {PokeStat.Speed, 2}, {PokeStat.Experience, 100} }, // Level 1
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 12}, {PokeStat.Attack, 6}, {PokeStat.Defense, 1}, {PokeStat.Speed, 4}, {PokeStat.Experience, 200} }, // Level 2
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 15}, {PokeStat.Attack, 7}, {PokeStat.Defense, 2}, {PokeStat.Speed, 6}, {PokeStat.Experience, 300} }, // Level 3
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 17}, {PokeStat.Attack, 9}, {PokeStat.Defense, 2}, {PokeStat.Speed, 8}, {PokeStat.Experience, 400} }, // Level 4
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 20}, {PokeStat.Attack, 10},{PokeStat.Defense, 3}, {PokeStat.Speed, 10}, {PokeStat.Experience, 500} }, // Level 5
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 22}, {PokeStat.Attack, 11},{PokeStat.Defense, 3}, {PokeStat.Speed, 12}, {PokeStat.Experience, 600} }, // Level 6
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 25}, {PokeStat.Attack, 13}, {PokeStat.Defense, 4}, {PokeStat.Speed, 14}, {PokeStat.Experience, 700} }, // Level 7
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 27}, {PokeStat.Attack, 14}, {PokeStat.Defense, 4}, {PokeStat.Speed, 16}, {PokeStat.Experience, 800} }, // Level 8
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 30}, {PokeStat.Attack, 15}, {PokeStat.Defense, 5}, {PokeStat.Speed, 18}, {PokeStat.Experience, 900} }, // Level 9
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 32}, {PokeStat.Attack, 16}, {PokeStat.Defense, 5}, {PokeStat.Speed, 20}, {PokeStat.Experience, 1000} }  // Level 10
		};

		#endregion

		#region Squirtle's Stats

		private static Dictionary<PokeStat, int>[] SquirtleStats = 
		{
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 10}, {PokeStat.Attack, 4}, {PokeStat.Defense, 1}, {PokeStat.Speed, 2}, {PokeStat.Experience, 100} }, // Level 1
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 12}, {PokeStat.Attack, 5}, {PokeStat.Defense, 2}, {PokeStat.Speed, 4}, {PokeStat.Experience, 200} }, // Level 2
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 15}, {PokeStat.Attack, 6}, {PokeStat.Defense, 2}, {PokeStat.Speed, 6}, {PokeStat.Experience, 300} }, // Level 3
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 17}, {PokeStat.Attack, 7}, {PokeStat.Defense, 3}, {PokeStat.Speed, 8}, {PokeStat.Experience, 400} }, // Level 4
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 20}, {PokeStat.Attack, 8}, {PokeStat.Defense, 4}, {PokeStat.Speed, 10}, {PokeStat.Experience, 500} }, // Level 5
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 22}, {PokeStat.Attack, 9}, {PokeStat.Defense, 4}, {PokeStat.Speed, 12}, {PokeStat.Experience, 600} }, // Level 6
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 25}, {PokeStat.Attack, 10}, {PokeStat.Defense, 5}, {PokeStat.Speed, 14}, {PokeStat.Experience, 700} }, // Level 7
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 27}, {PokeStat.Attack, 11}, {PokeStat.Defense, 5}, {PokeStat.Speed, 16}, {PokeStat.Experience, 800} }, // Level 8
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 30}, {PokeStat.Attack, 12}, {PokeStat.Defense, 6}, {PokeStat.Speed, 18}, {PokeStat.Experience, 900} }, // Level 9
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 32}, {PokeStat.Attack, 13}, {PokeStat.Defense, 6}, {PokeStat.Speed, 20}, {PokeStat.Experience, 1000} }  // Level 10
		};

		#endregion

		#region Oddish's Stats

		private static Dictionary<PokeStat, int>[] OddishStats = 
		{
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 10}, {PokeStat.Attack, 3}, {PokeStat.Defense, 1}, {PokeStat.Speed, 2}, {PokeStat.Experience, 100} }, // Level 1
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 11}, {PokeStat.Attack, 4}, {PokeStat.Defense, 1}, {PokeStat.Speed, 4}, {PokeStat.Experience, 200} }, // Level 2
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 13}, {PokeStat.Attack, 5}, {PokeStat.Defense, 2}, {PokeStat.Speed, 6}, {PokeStat.Experience, 300} }, // Level 3
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 14}, {PokeStat.Attack, 6}, {PokeStat.Defense, 2}, {PokeStat.Speed, 8}, {PokeStat.Experience, 400} }, // Level 4
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 16}, {PokeStat.Attack, 7}, {PokeStat.Defense, 3}, {PokeStat.Speed, 10}, {PokeStat.Experience, 500} }, // Level 5
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 17}, {PokeStat.Attack, 8}, {PokeStat.Defense, 3}, {PokeStat.Speed, 12}, {PokeStat.Experience, 600} }, // Level 6
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 19}, {PokeStat.Attack, 9},  {PokeStat.Defense, 4}, {PokeStat.Speed, 14}, {PokeStat.Experience, 700} }, // Level 7
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 20}, {PokeStat.Attack, 10}, {PokeStat.Defense, 4}, {PokeStat.Speed, 16}, {PokeStat.Experience, 800} }, // Level 8
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 22}, {PokeStat.Attack, 11}, {PokeStat.Defense, 5}, {PokeStat.Speed, 18}, {PokeStat.Experience, 900} }, // Level 9
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 23}, {PokeStat.Attack, 12}, {PokeStat.Defense, 5}, {PokeStat.Speed, 20}, {PokeStat.Experience, 1000} } // Level 10
		};

		#endregion

		#region Growlithe's Stats

		private static Dictionary<PokeStat, int>[] GrowlitheStats = 
		{
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 10}, {PokeStat.Attack, 5}, {PokeStat.Defense, 1}, {PokeStat.Speed, 1}, {PokeStat.Experience, 100} }, // Level 1
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 11}, {PokeStat.Attack, 6}, {PokeStat.Defense, 1}, {PokeStat.Speed, 3}, {PokeStat.Experience, 200} }, // Level 2
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 13}, {PokeStat.Attack, 7}, {PokeStat.Defense, 2}, {PokeStat.Speed, 5}, {PokeStat.Experience, 300} }, // Level 3
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 14}, {PokeStat.Attack, 8}, {PokeStat.Defense, 2}, {PokeStat.Speed, 7}, {PokeStat.Experience, 400} }, // Level 4
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 16}, {PokeStat.Attack, 9}, {PokeStat.Defense, 3}, {PokeStat.Speed, 9}, {PokeStat.Experience, 500} }, // Level 5
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 17}, {PokeStat.Attack, 10},{PokeStat.Defense, 3}, {PokeStat.Speed, 11}, {PokeStat.Experience, 600} }, // Level 6
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 19}, {PokeStat.Attack, 11}, {PokeStat.Defense, 4}, {PokeStat.Speed, 13}, {PokeStat.Experience, 700} }, // Level 7
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 20}, {PokeStat.Attack, 12}, {PokeStat.Defense, 4}, {PokeStat.Speed, 15}, {PokeStat.Experience, 800} }, // Level 8
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 22}, {PokeStat.Attack, 13}, {PokeStat.Defense, 5}, {PokeStat.Speed, 17}, {PokeStat.Experience, 900} }, // Level 9
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 23}, {PokeStat.Attack, 14}, {PokeStat.Defense, 5}, {PokeStat.Speed, 19}, {PokeStat.Experience, 1000} }  // Level 10
		};

		#endregion

		#region Psyduck's Stats

		private static Dictionary<PokeStat, int>[] PsyduckStats = 
		{
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 10}, {PokeStat.Attack, 4}, {PokeStat.Defense, 1}, {PokeStat.Speed, 1}, {PokeStat.Experience, 100} }, // Level 1
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 11}, {PokeStat.Attack, 5}, {PokeStat.Defense, 2}, {PokeStat.Speed, 3}, {PokeStat.Experience, 200} }, // Level 2
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 13}, {PokeStat.Attack, 6}, {PokeStat.Defense, 2}, {PokeStat.Speed, 5}, {PokeStat.Experience, 300} }, // Level 3
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 14}, {PokeStat.Attack, 7}, {PokeStat.Defense, 3}, {PokeStat.Speed, 7}, {PokeStat.Experience, 400} }, // Level 4
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 16}, {PokeStat.Attack, 8}, {PokeStat.Defense, 4}, {PokeStat.Speed, 9}, {PokeStat.Experience, 500} }, // Level 5
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 17}, {PokeStat.Attack, 9}, {PokeStat.Defense, 4}, {PokeStat.Speed, 11}, {PokeStat.Experience, 600} }, // Level 6
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 19}, {PokeStat.Attack, 10}, {PokeStat.Defense, 5}, {PokeStat.Speed, 13}, {PokeStat.Experience, 700} }, // Level 7
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 20}, {PokeStat.Attack, 11}, {PokeStat.Defense, 5}, {PokeStat.Speed, 15}, {PokeStat.Experience, 800} }, // Level 8
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 22}, {PokeStat.Attack, 12}, {PokeStat.Defense, 6}, {PokeStat.Speed, 17}, {PokeStat.Experience, 900} }, // Level 9
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 23}, {PokeStat.Attack, 13}, {PokeStat.Defense, 6}, {PokeStat.Speed, 19}, {PokeStat.Experience, 1000} }  // Level 10
		};

		#endregion

		#region Bellsprout's Stats

		private static Dictionary<PokeStat, int>[] BellsproutStats = 
		{
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 10}, {PokeStat.Attack, 4}, {PokeStat.Defense, 1}, {PokeStat.Speed, 2}, {PokeStat.Experience, 100} }, // Level 1
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 11}, {PokeStat.Attack, 5}, {PokeStat.Defense, 1}, {PokeStat.Speed, 4}, {PokeStat.Experience, 200} }, // Level 2
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 13}, {PokeStat.Attack, 6}, {PokeStat.Defense, 2}, {PokeStat.Speed, 6}, {PokeStat.Experience, 300} }, // Level 3
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 14}, {PokeStat.Attack, 7}, {PokeStat.Defense, 2}, {PokeStat.Speed, 8}, {PokeStat.Experience, 400} }, // Level 4
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 16}, {PokeStat.Attack, 8}, {PokeStat.Defense, 3}, {PokeStat.Speed, 10}, {PokeStat.Experience, 500} }, // Level 5
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 17}, {PokeStat.Attack, 9}, {PokeStat.Defense, 3}, {PokeStat.Speed, 12}, {PokeStat.Experience, 600} }, // Level 6
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 19}, {PokeStat.Attack, 10}, {PokeStat.Defense, 4}, {PokeStat.Speed, 14}, {PokeStat.Experience, 700} }, // Level 7
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 20}, {PokeStat.Attack, 11}, {PokeStat.Defense, 4}, {PokeStat.Speed, 16}, {PokeStat.Experience, 800} }, // Level 8
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 22}, {PokeStat.Attack, 12}, {PokeStat.Defense, 5}, {PokeStat.Speed, 18}, {PokeStat.Experience, 900} }, // Level 9
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 23}, {PokeStat.Attack, 13}, {PokeStat.Defense, 5}, {PokeStat.Speed, 20}, {PokeStat.Experience, 1000} } // Level 10
		};

		#endregion

		#region Vulpix's Stats

		private static Dictionary<PokeStat, int>[] VulpixStats = 
		{
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 10}, {PokeStat.Attack, 5}, {PokeStat.Defense, 1}, {PokeStat.Speed, 2}, {PokeStat.Experience, 100} }, // Level 1
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 11}, {PokeStat.Attack, 6}, {PokeStat.Defense, 1}, {PokeStat.Speed, 4}, {PokeStat.Experience, 200} }, // Level 2
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 13}, {PokeStat.Attack, 7}, {PokeStat.Defense, 2}, {PokeStat.Speed, 6}, {PokeStat.Experience, 300} }, // Level 3
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 14}, {PokeStat.Attack, 8}, {PokeStat.Defense, 2}, {PokeStat.Speed, 8}, {PokeStat.Experience, 400} }, // Level 4
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 16}, {PokeStat.Attack, 9}, {PokeStat.Defense, 3}, {PokeStat.Speed, 10}, {PokeStat.Experience, 500} }, // Level 5
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 17}, {PokeStat.Attack, 10},{PokeStat.Defense, 3}, {PokeStat.Speed, 12}, {PokeStat.Experience, 600} }, // Level 6
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 19}, {PokeStat.Attack, 11}, {PokeStat.Defense, 4}, {PokeStat.Speed, 14}, {PokeStat.Experience, 700} }, // Level 7
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 20}, {PokeStat.Attack, 12}, {PokeStat.Defense, 4}, {PokeStat.Speed, 16}, {PokeStat.Experience, 800} }, // Level 8
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 22}, {PokeStat.Attack, 13}, {PokeStat.Defense, 5}, {PokeStat.Speed, 18}, {PokeStat.Experience, 900} }, // Level 9
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 23}, {PokeStat.Attack, 14}, {PokeStat.Defense, 5}, {PokeStat.Speed, 20}, {PokeStat.Experience, 1000} }  // Level 10
		};

		#endregion

		#region Staryu's Stats

		private static Dictionary<PokeStat, int>[] StaryuStats = 
		{
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 10}, {PokeStat.Attack, 3}, {PokeStat.Defense, 1}, {PokeStat.Speed, 2}, {PokeStat.Experience, 100} }, // Level 1
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 11}, {PokeStat.Attack, 4}, {PokeStat.Defense, 2}, {PokeStat.Speed, 4}, {PokeStat.Experience, 200} }, // Level 2
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 13}, {PokeStat.Attack, 5}, {PokeStat.Defense, 2}, {PokeStat.Speed, 6}, {PokeStat.Experience, 300} }, // Level 3
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 14}, {PokeStat.Attack, 6}, {PokeStat.Defense, 3}, {PokeStat.Speed, 8}, {PokeStat.Experience, 400} }, // Level 4
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 16}, {PokeStat.Attack, 7}, {PokeStat.Defense, 4}, {PokeStat.Speed, 10}, {PokeStat.Experience, 500} }, // Level 5
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 17}, {PokeStat.Attack, 8}, {PokeStat.Defense, 4}, {PokeStat.Speed, 12}, {PokeStat.Experience, 600} }, // Level 6
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 19}, {PokeStat.Attack, 9},  {PokeStat.Defense, 5},{PokeStat.Speed, 14}, {PokeStat.Experience, 700} }, // Level 7
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 20}, {PokeStat.Attack, 10}, {PokeStat.Defense, 5}, {PokeStat.Speed, 16}, {PokeStat.Experience, 800} }, // Level 8
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 22}, {PokeStat.Attack, 11}, {PokeStat.Defense, 6}, {PokeStat.Speed, 18}, {PokeStat.Experience, 900} }, // Level 9
			new Dictionary<PokeStat, int>() { {PokeStat.Health, 23}, {PokeStat.Attack, 12}, {PokeStat.Defense, 6}, {PokeStat.Speed, 20}, {PokeStat.Experience, 1000} }  // Level 10
		};

		#endregion

		private static Dictionary<PokeSpecies, Dictionary<PokeStat, int>[]> dictPokemonStats = new Dictionary<PokeSpecies,Dictionary<PokeStat,int>[]>()
		{
			{PokeSpecies.Bulbasaur, BulbasaurStats},
			{PokeSpecies.Charmander, CharmanderStats},
			{PokeSpecies.Squirtle, SquirtleStats},
			{PokeSpecies.Oddish, OddishStats},
			{PokeSpecies.Growlithe, GrowlitheStats},
			{PokeSpecies.Psyduck, PsyduckStats},
			{PokeSpecies.Bellsprout, BellsproutStats},
			{PokeSpecies.Vulpix, VulpixStats},
			{PokeSpecies.Staryu, StaryuStats}
		};

		private static Dictionary<PokeSpecies, PokeType> dictPokemonTypes = new Dictionary<PokeSpecies, PokeType>()
		{
			{PokeSpecies.Bulbasaur, PokeType.Grass},
			{PokeSpecies.Charmander, PokeType.Fire},
			{PokeSpecies.Squirtle, PokeType.Water},
			{PokeSpecies.Oddish, PokeType.Grass},
			{PokeSpecies.Growlithe, PokeType.Fire},
			{PokeSpecies.Psyduck, PokeType.Water},
			{PokeSpecies.Bellsprout, PokeType.Grass},
			{PokeSpecies.Vulpix, PokeType.Fire},
			{PokeSpecies.Staryu, PokeType.Water}
		};

		#endregion

		#endregion

		#region Pokemon Variables

		private PokeSpecies psSpecies;
		public PokeSpecies Species { get { return psSpecies; } }

		private PokeType ptType;
		public PokeType Type { get { return ptType; } }

		private string sNickName;
		public string NickName { get { return sNickName; } set { sNickName = value; } }

		private int iLevel;
		public int Level { get { return iLevel; } }

		private int iCurrentHealth;
		public int CurrentHealth { get { return iCurrentHealth; } set { iCurrentHealth = value; } }

		private int iMaxHealth;
		public int MaxHealth { get { return iMaxHealth; } }

		private int iAttackStat;
		public int Attack { get { return iAttackStat; } }

		private int iDefenseStat;
		public int Defense { get { return iDefenseStat; } }

		private int iSpeedStat;
		public int Speed { get { return iSpeedStat; } }

		private int iExperience;
		public int Experience { get { return iExperience; } set { iExperience = value; } }

		public int ExpToNextLevel
		{ 
			get 
			{
				if (iLevel == iLevelCap) return 0;
				else return (((iLevel + 1) * 100) - iExperience);
			} 
		}

		#endregion

		public Pokemon(PokeSpecies psSpecies, int iLevel)
		{
			this.psSpecies = psSpecies;
			
			ptType = dictPokemonTypes[psSpecies];
			
			sNickName = psSpecies.ToString().ToUpper();

			this.iLevel = iLevel;
			if (iLevel < 1) this.iLevel = 1;
			else if (iLevel > iLevelCap) this.iLevel = iLevelCap;

			iCurrentHealth = iMaxHealth = getHealthStat();

			iAttackStat = getAttackStat();

			iDefenseStat = getDefenseStat();

			iSpeedStat = getSpeedStat();

			iExperience = dictPokemonStats[psSpecies][(this.iLevel - 1)][PokeStat.Experience];
		}

		private int getHealthStat()
		{
			return dictPokemonStats[psSpecies][(iLevel - 1)][PokeStat.Health];
		}

		private int getAttackStat()
		{
			return dictPokemonStats[psSpecies][(iLevel - 1)][PokeStat.Attack];
		}

		private int getDefenseStat()
		{
			return dictPokemonStats[psSpecies][(iLevel - 1)][PokeStat.Defense];
		}

		private int getSpeedStat()
		{
			return dictPokemonStats[psSpecies][(iLevel - 1)][PokeStat.Speed];
		}

		public static int calculateDamage(Pokemon pAttacker, Pokemon pAttacked)
		{
			int iDamage = 0;
			PokeTypeAdvantage taAdvantage = getTypeAdvantage(pAttacker, pAttacked);

			if (taAdvantage == PokeTypeAdvantage.Advantage) iDamage = ((pAttacker.Attack * 2) - pAttacked.Defense);

			else if (taAdvantage == PokeTypeAdvantage.Disadvantage) iDamage = ((pAttacker.Attack / 2) - pAttacked.Defense);
			
			else iDamage = (pAttacker.Attack - pAttacked.Defense);

			if (iDamage <= 0) iDamage = 1;
			
			return iDamage;
		}

		public static int calculateExperience(Pokemon pWinner, Pokemon pLoser)
		{
			int iExpereince = iBaseExperience;
			PokeTypeAdvantage taAdvantage = getTypeAdvantage(pWinner, pLoser);

			if (pWinner.Level == iLevelCap) return 0;

			// Loser is lower level
			if(pWinner.Level > pLoser.Level)
			{
				int iLevelDifference = (pWinner.Level - pLoser.Level);

				if (taAdvantage == PokeTypeAdvantage.Advantage) iExpereince = ((iBaseExperience - iLevelDifference) * 2);

				else if (taAdvantage == PokeTypeAdvantage.Disadvantage) iExpereince = ((iBaseExperience - iLevelDifference) * 4);

				else iExpereince = (iBaseExperience - iLevelDifference);
			}
			
			// Loser is higher level
			else if(pWinner.Level < pLoser.Level)
			{
				int iLevelDifference = (pLoser.Level - pWinner.Level);

				if (taAdvantage == PokeTypeAdvantage.Advantage) iExpereince = ((iBaseExperience * 2) * (iLevelDifference + 1));

				else if (taAdvantage == PokeTypeAdvantage.Disadvantage) iExpereince = ((iBaseExperience * 4) * (iLevelDifference + 1));

				else iExpereince = (iBaseExperience * (iLevelDifference + 1));
			}
			
			// Both same level
			else
			{
				if (taAdvantage == PokeTypeAdvantage.Advantage) iExpereince = (iBaseExperience * 2);

				else if (taAdvantage == PokeTypeAdvantage.Disadvantage) iExpereince = (iBaseExperience * 4);

				else iExpereince = iBaseExperience;
			}

            iExpereince += (100 - (pWinner.Level * 10));

			if (pWinner.Experience + iExpereince >= iExperinceCap) iExpereince = (iExperinceCap - pWinner.Experience);

			return iExpereince;
		}

		public static Pokemon.PokeTypeAdvantage getTypeAdvantage(Pokemon pAttacker, Pokemon pAttacked)
		{
			PokeTypeAdvantage taResult = PokeTypeAdvantage.Neutral;

			// Attacker is Grass
			if (pAttacker.Type == PokeType.Grass && pAttacked.Type == PokeType.Grass) taResult = PokeTypeAdvantage.Neutral; // Grass -> Grass
			else if (pAttacker.Type == PokeType.Grass && pAttacked.Type == PokeType.Fire) taResult = PokeTypeAdvantage.Disadvantage; // Grass -> Fire
			else if (pAttacker.Type == PokeType.Grass && pAttacked.Type == PokeType.Water) taResult = PokeTypeAdvantage.Advantage; // Grass -> Water

			// Attacker is Fire
			else if (pAttacker.Type == PokeType.Fire && pAttacked.Type == PokeType.Grass) taResult = PokeTypeAdvantage.Advantage; // Fire -> Grass
			else if (pAttacker.Type == PokeType.Fire && pAttacked.Type == PokeType.Fire) taResult = PokeTypeAdvantage.Neutral; // Fire -> Fire
			else if (pAttacker.Type == PokeType.Fire && pAttacked.Type == PokeType.Water) taResult = PokeTypeAdvantage.Disadvantage; // Fire -> Water

			// Attacker is Water
			else if (pAttacker.Type == PokeType.Water && pAttacked.Type == PokeType.Grass) taResult = PokeTypeAdvantage.Disadvantage; // Water -> Grass
			else if (pAttacker.Type == PokeType.Water && pAttacked.Type == PokeType.Fire) taResult = PokeTypeAdvantage.Advantage; // Water -> Fire
			else if (pAttacker.Type == PokeType.Water && pAttacked.Type == PokeType.Water) taResult = PokeTypeAdvantage.Neutral; //Water -> Water

			return taResult;
		}

		public bool hasLeveledUp()
		{
			return iLevel != iLevelCap && iExperience >= ((iLevel * 100) + 100);
		}

		public void levelUp()
		{
			if (iLevel < iLevelCap)
			{
				iLevel = iExperience / 100;
				iMaxHealth = getHealthStat();
				iAttackStat = getAttackStat();
				iDefenseStat = getDefenseStat();
				iSpeedStat = getSpeedStat();
			}
		}
	}
}
