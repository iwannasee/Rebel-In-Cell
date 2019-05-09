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
			GameData loadedData= new GameData();
			loadedData.playerName = "ViEt";
			loadedData.gold =100000;
			//Set present world is 1
			loadedData.presentWorld = 0;
			loadedData.maxReachableWorld = 1;
			loadedData.availableMaps = new List<string>();
			loadedData.availableMaps.Add(MapDictionary.mapOfWorld_0[0]);
			loadedData.completedMaps = new List<string>();

			loadedData.availableCharacters = new List<string>();
			loadedData.availableCharacters.Add(CommonData.char_pippo);
            
			loadedData.availableCharacters.Add(CommonData.char_johnny);
			//loadedData.availableCharacters.Add(CommonData.char_mathial);
            loadedData.availableCharacters.Add(CommonData.char_kolav);
            loadedData.availableCharacters.Add(CommonData.char_maja);
            loadedData.availableCharacters.Add(CommonData.char_bape);
            //loadedData.availableCharacters.Add(CommonData.char_vie);
            //loadedData.availableCharacters.Add(CommonData.char_lynu);
            

			loadedData.allAvailableskills = new List<string>();
            loadedData.pippo_availableskills = new List<string>();
			loadedData.pippo_availableskills.Add(CommonData.Pippo_FireBall);
			loadedData.pippo_availableskills.Add(CommonData.Pippo_Bazooka);
			loadedData.pippo_availableskills.Add(CommonData.Pippo_FireWall);
			loadedData.latestUsedSkill_Pippo = CommonData.Pippo_FireBall;
			for(int i=0; i< loadedData.pippo_availableskills.Count; i++){
				loadedData.allAvailableskills.Add(loadedData.pippo_availableskills[i]);
			}

			loadedData.johnny_availableskills = new List<string>();
			loadedData.johnny_availableskills.Add(CommonData.Johnny_Regeneration);
            loadedData.johnny_availableskills.Add(CommonData.Johnny_Epidemic);
            loadedData.johnny_availableskills.Add(CommonData.Johnny_Achemysto);
			loadedData.latestUsedSkill_Johnny = CommonData.Johnny_Regeneration;
			for(int i=0; i< loadedData.johnny_availableskills.Count; i++){
				loadedData.allAvailableskills.Add(loadedData.johnny_availableskills[i]);
			}

            loadedData.mathial_availableskills = new List<string>();
            loadedData.mathial_availableskills.Add(CommonData.Mathial_DragonStance);
            loadedData.mathial_availableskills.Add(CommonData.Mathial_PrayingMantisStance);
            loadedData.mathial_availableskills.Add(CommonData.Mathial_ReverseBowStance);
            loadedData.latestUsedSkill_Mathial = CommonData.Mathial_DragonStance;
			for(int i=0; i< loadedData.mathial_availableskills.Count; i++){
				loadedData.allAvailableskills.Add(loadedData.mathial_availableskills[i]);
			}

            loadedData.kolav_availableskills = new List<string>();
			loadedData.kolav_availableskills.Add(CommonData.Kolav_GangBlade);
			loadedData.kolav_availableskills.Add(CommonData.Kolav_Riot);
            loadedData.kolav_availableskills.Add(CommonData.Kolav_SlingGun);
            loadedData.latestUsedSkill_Kolav = CommonData.Kolav_GangBlade;
			for(int i=0; i< loadedData.kolav_availableskills.Count; i++){
				loadedData.allAvailableskills.Add(loadedData.kolav_availableskills[i]);
			}

            loadedData.maja_availableskills = new List<string>();
            loadedData.maja_availableskills.Add(CommonData.Maja_PoisonMagic);
            loadedData.maja_availableskills.Add(CommonData.Maja_AchillesHeel);
            loadedData.maja_availableskills.Add(CommonData.Maja_MajakumaWish);
            loadedData.latestUsedSkill_Maja = CommonData.Maja_PoisonMagic;
			for(int i=0; i< loadedData.maja_availableskills.Count; i++){
				loadedData.allAvailableskills.Add(loadedData.maja_availableskills[i]);
			}

            loadedData.bape_availableskills = new List<string>();
            loadedData.bape_availableskills.Add(CommonData.Bape_GrowingBomb);
            loadedData.bape_availableskills.Add(CommonData.Bape_DynamoArrow);
            loadedData.bape_availableskills.Add(CommonData.Bape_TimeHealingBomb);
            loadedData.latestUsedSkill_Bape = CommonData.Bape_GrowingBomb;
			for(int i=0; i< loadedData.bape_availableskills.Count; i++){
				loadedData.allAvailableskills.Add(loadedData.bape_availableskills[i]);
			}

            loadedData.vie_availableskills = new List<string>();
            loadedData.vie_availableskills.Add(CommonData.Vie_Degravitation);
            loadedData.vie_availableskills.Add(CommonData.Vie_Blackholification);
            loadedData.vie_availableskills.Add(CommonData.Vie_Reinforcement);
            loadedData.latestUsedSkill_Vie = CommonData.Vie_Degravitation;
			for(int i=0; i< loadedData.vie_availableskills.Count; i++){
				loadedData.allAvailableskills.Add(loadedData.vie_availableskills[i]);
			}

            loadedData.lynu_availableskills = new List<string>();
            loadedData.lynu_availableskills.Add(CommonData.Lynu_PrayOfPower);
            loadedData.lynu_availableskills.Add(CommonData.Lynu_PrayOfLongLasting);
            loadedData.lynu_availableskills.Add(CommonData.Lynu_HolyLight);
            loadedData.latestUsedSkill_Lynu = CommonData.Lynu_PrayOfPower;
			for(int i=0; i< loadedData.lynu_availableskills.Count; i++){
				loadedData.allAvailableskills.Add(loadedData.lynu_availableskills[i]);
			}

            loadedData.availableVehicles = new List<string>();
			loadedData.availableVehicles.Add(CommonData.base_1Slot);
			loadedData.availableVehicles.Add(CommonData.base_2Slot);
            loadedData.availableVehicles.Add(CommonData.base_3slot);

            SaveGame(loadedData);
			return loadedData;
		}else{
			string jsonFromSaveFile = File.ReadAllText (fileName);
			GameData loadedData = JsonUtility.FromJson<GameData> (jsonFromSaveFile);
			Debug.LogError("Load successed from " + fileName);
			return loadedData;
		}
	}
}
