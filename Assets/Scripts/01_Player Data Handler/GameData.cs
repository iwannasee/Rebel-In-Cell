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

    public List<string> maja_availableskills;
    public string latestUsedSkill_Maja;

    public List<string>bape_availableskills;
    public string latestUsedSkill_Bape;

    public List<string> vie_availableskills;
    public string latestUsedSkill_Vie;

    public List<string> lynu_availableskills;
    public string latestUsedSkill_Lynu;

	public List<string> allAvailableskills;
}
