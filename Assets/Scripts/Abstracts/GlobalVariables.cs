using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables{

	public static bool MENUSHOWING = false; 
	public static bool PAUSED = false; 
	public static int DIFFICULTY = 0;
	public static int STARTINGWAVE = 1;
	public static bool PLAYERDEAD = false; 
	public static bool PLAYERNODAMAGE = true;
	public static bool HEARTNODAMAGE = true;
	public static bool HEARTDEAD = false; 
	public static bool GAMEOVER = false; 


	//ACHEIVMENTS
	public static string NODAMAGE_LEVEL_WAVE_DIFFICULTY_TEMPLATE = "Level{0}Wave{1}Difficulty{2}NoDamage";
	public static string NODAMAGE_HEART_LEVEL_WAVE_DIFFICULTY_TEMPLATE = "Level{0}Wave{1}Difficulty{2}NoDamageHeart";
	public static string NODAMAGE_PLAYER_LEVEL_WAVE_DIFFICULTY_TEMPLATE = "Level{0}Wave{1}Difficulty{2}NoDamagePlayer";

	public static string HEART = "Heart";
	public static string PLAYER = "Player";
	public static string TOTAL = "Total";

	public static string COMPLETE_LEVEL_WAVE_DIFFICULTY_TEMPLATE = "Level{0}Wave{1}Difficulty{2}Complete";

}
