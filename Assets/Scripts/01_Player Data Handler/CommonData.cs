using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour {
	//---------------------------------------------------------------
	public const string char_pippo = "Pippo";
	public const string char_johnny = "Johnny"; 
	public const string char_mathial = "Mathial"; 
	public const string char_kolav = "Kolav"; 
	public const string char_maja = "Maja";
	public const string char_bape = "Bape";
	public const string char_vie = "Vie";
	public const string char_lynu = "Lynu";

	public static List <string> charNameList = new List<string>(new string[]{
		char_pippo,
		char_johnny,
		char_mathial,
		char_kolav,
		char_maja,
		char_bape,
		char_vie,
		char_lynu
	});
	//---------------------------------------------------------------
	public const string base_1Slot = "One Slot";
	public const string base_2Slot = "Two Slots";
	public const string base_3slot = "Three Slots";

	public static List <string> vehicleList = new List<string>(new string[]{
        base_1Slot,
        base_2Slot,
        base_3slot
	});


	public const string Pippo_FireBall = "Fire Ball";
	public const string Pippo_Bazooka = "Bazooka";
	public const string Pippo_FireWall = "Fire Wall";
	public static List <string> PippoSkills = new List<string>(new string[]{
		Pippo_FireBall,
		Pippo_Bazooka,
		Pippo_FireWall
	});

	public const string Johnny_Regeneration = "Regeneration";
	public const string Johnny_Epidemic = "Epidemic";
	public const string Johnny_Achemysto = "Achemysto";
	public static List <string> JohnnySkills = new List<string>(new string[]{
		Johnny_Regeneration,
		Johnny_Epidemic,
		Johnny_Achemysto
	});

	public const string Mathial_DragonStance = "Dragon Stance";
	public const string Mathial_PrayingMantisStance = "Praying Mantis Stance";
	public const string Mathial_ReverseBowStance = "Reverse Bow";
	public static List <string> MathialSkills = new List<string>(new string[]{
		Mathial_DragonStance,
		Mathial_PrayingMantisStance,
		Mathial_ReverseBowStance
	});

	public const string Kolav_GangBlade = "Gang Blade";
	public const string Kolav_Riot = "Riot";
	public const string Kolav_SlingGun = "Sling Gun";
	public static List <string> KolavSkills = new List<string>(new string[]{
		Kolav_GangBlade,
		Kolav_Riot,
		Kolav_SlingGun
	});

	public const string Maja_PoisonMagic = "Poison Magic";
	public const string Maja_AchillesHeel = "Achilles' heel Revealer";
	public const string Maja_MajakumaWish = "Majakuma Wish";
	public static List <string> MajaSkills = new List<string>(new string[]{
		Maja_PoisonMagic,
		Maja_AchillesHeel,
		Maja_MajakumaWish
	});

	public const string Bape_GrowingBomb = "Growing Bomb";
	public const string Bape_DynamoArrow = "Dynamo Arrow";
	public const string Bape_TimeHealingBomb = "Healing C4";
	public static List <string> BapeSkills = new List<string>(new string[]{
		Bape_GrowingBomb,
		Bape_DynamoArrow,
		Bape_TimeHealingBomb
	});

	public const string Vie_Reinforcement = "Reinforcement";
	public const string Vie_Degravitation = "Degravitation";
	public const string Vie_Blackholification = "Blackholification";
	public static List <string> VieSkills = new List<string>(new string[]{
		Vie_Reinforcement,
		Vie_Degravitation,
		Vie_Blackholification
	});

	public const string Lynu_PrayOfPower = "Pray Of Power";
	public const string Lynu_PrayOfLongLasting = "Pray Of Eternal";
	public const string Lynu_HolyLight= "Holy Light";
	public static List <string> LynuSkills = new List<string>(new string[]{
		Lynu_PrayOfPower,
		Lynu_PrayOfLongLasting,
		Lynu_HolyLight
	});

	public const string Equip_StrawCloak = "Straw Cloak";
	public const string Equip_LeatherShirt = "Leather Shirt";
	public static List <string> Equipments = new List<string>(new string[]{
		Equip_StrawCloak,
		Equip_LeatherShirt
	});
	//---------------------------------------------------------------
}
