using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour {
	//---------------------------------------------------------------
	public const string char_pippo = "Pippo";
	public const string char_johnny = "Johnny"; 
	public const string char_mathial = "Mathial"; 
	public const string char_kolav = "Kolav"; 

	public static List <string> charNameList = new List<string>(new string[]{
		char_pippo,
		char_johnny,
		char_mathial,
		char_kolav
	});
	//---------------------------------------------------------------
	public const string veh_oven = "Oven";
	public const string veh_test = "Test";

	public static List <string> vehicleList = new List<string>(new string[]{
		veh_oven,
		veh_test
	});


	public const string Pippo_FireBall = "Fire Ball";
	public const string Pippo_Bazooka = "Bazooka";
	public const string Pippo_Shotgun = "Shotgun";
	public static List <string> PippoSkills = new List<string>(new string[]{
		Pippo_FireBall,
		Pippo_Bazooka,
		Pippo_Shotgun
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

	public const string Kolav_PlasmaBomb = "Plasma Bomb";
	public const string Kolav_LaserGlance = "Laser Glance";
	public const string Kolav_KineticArm = "Kinetic Arm";
	public static List <string> KolavSkills = new List<string>(new string[]{
		Kolav_PlasmaBomb,
		Kolav_LaserGlance,
		Kolav_KineticArm
	});

	public const string Equip_StrawCloak = "Straw Cloak";
	public const string Equip_LeatherShirt = "Leather Shirt";
	public static List <string> Equipments = new List<string>(new string[]{
		Equip_StrawCloak,
		Equip_LeatherShirt
	});
	//---------------------------------------------------------------




}
