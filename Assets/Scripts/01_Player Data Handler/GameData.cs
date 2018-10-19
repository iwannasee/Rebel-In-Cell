using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
	public string playerName;
	public int gold;
	public int presentWorld;
	public int maxReachableWorld;
	public List<string> availableMaps;
	public List<string> completedMaps;
	public List<string> availableCharacters;
	public List<string> availableVehicles;

	public List<string> pippo_availableskills;
	public string latestUsedSkill_Pippo;

	public List<string> johnny_availableskills;
	public string latestUsedSkill_Johnny;

	public List<string> mathial_availableskills;
	public string latestUsedSkill_Mathial;

	public List<string> kolav_availableskills;
	public string latestUsedSkill_Kolav;
}
