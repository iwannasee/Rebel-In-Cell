using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadSystem : MonoBehaviour  {

	private static readonly string SAVE_FILE = "playerprogress.json";
	private static string fileName = Path.Combine (Application.persistentDataPath, SAVE_FILE);
	//---------------------------------------------------------------
	public static void SaveGame(GameData dataToSave){
		string json = JsonUtility.ToJson (dataToSave);

		if (File.Exists (fileName)) {
			File.Delete (fileName);
			Debug.Log("Save file exists.");
		}
		File.WriteAllText (fileName, json);
		Debug.Log ("Player saved to " + fileName);
	}
	//---------------------------------------------------------------
	public static GameData LoadGame(){
		if (!File.Exists (fileName)) {
			Debug.Log("No save data to load");
			GameData loadedData= new GameData();
			loadedData.playerName = "ViEt";
			loadedData.gold = 0;
			//Set present world is 1
			loadedData.presentWorld = 0;
			loadedData.maxReachableWorld = 1;
			loadedData.availableMaps = new List<string>();
			loadedData.availableMaps.Add("Bat Cave");
			loadedData.completedMaps = new List<string>();

			loadedData.availableCharacters = new List<string>();
			loadedData.availableCharacters.Add(CommonData.char_pippo);
			loadedData.availableCharacters.Add(CommonData.char_johnny);
			loadedData.availableCharacters.Add(CommonData.char_kolav);

			loadedData.pippo_availableskills = new List<string>();
			loadedData.pippo_availableskills.Add(CommonData.Pippo_FireBall);
			loadedData.pippo_availableskills.Add(CommonData.Pippo_Bazooka);
			loadedData.pippo_availableskills.Add(CommonData.Pippo_Shotgun);
			loadedData.latestUsedSkill_Pippo = CommonData.Pippo_FireBall;

			loadedData.johnny_availableskills = new List<string>();
			loadedData.johnny_availableskills.Add(CommonData.Johnny_Regeneration);
			loadedData.johnny_availableskills.Add(CommonData.Johnny_Achemysto);
			loadedData.latestUsedSkill_Johnny = CommonData.Johnny_Regeneration;

			loadedData.kolav_availableskills = new List<string>();
			loadedData.kolav_availableskills.Add(CommonData.Kolav_LaserGlance);
			loadedData.kolav_availableskills.Add(CommonData.Kolav_KineticArm);
			loadedData.latestUsedSkill_Kolav = CommonData.Kolav_KineticArm;

			loadedData.availableVehicles = new List<string>();
			loadedData.availableVehicles.Add(CommonData.veh_oven);
			loadedData.availableVehicles.Add(CommonData.veh_test);


			SaveGame(loadedData);
			return loadedData;
		}else{
			string jsonFromSaveFile = File.ReadAllText (fileName);
			GameData loadedData = JsonUtility.FromJson<GameData> (jsonFromSaveFile);
			Debug.Log("Load successed from " + fileName);
			return loadedData;
		}
	}
}
