using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectCharAvarList : MonoBehaviour {
	[Tooltip("the list of total playable characters of the whole game")]
	private Selection_CharAvar[] totalCharAvars;
	public Sprite characterLockedImage;
	// Use this for initialization
	void Start () {
		totalCharAvars = GetComponentsInChildren<Selection_CharAvar>();

		FilterAvailableChars();
	}


	private void FilterAvailableChars(){
		//Find And Get the total number of available (playable, unlocked) characters in gamedata
		int dataTotalNumberOfChars = PlayerProgress.playerData.availableCharacters.Count; 
		print(" dataTotalNumberOfChars " + dataTotalNumberOfChars);
		for (int i = 0; i < dataTotalNumberOfChars; i++){
			string nameInData = PlayerProgress.playerData.availableCharacters[i];
			for(int j = 0; j < totalCharAvars.Length; j ++){  
				string nameInList = totalCharAvars[j].GetPrisonerPrefabName();
				if(nameInData == nameInList){ 
					print(nameInList +" found");
					totalCharAvars[j].UnlockCharacterSelection();
				}else{
					totalCharAvars[j].transform.GetChild(0).GetComponent<Image>().sprite = characterLockedImage;
				}
			}
		}
	}

	public Selection_CharAvar GetIsOnInfoScrnCharacter(){
		CharacterInfoScreen charInfo = totalCharAvars[0].FindAndGetCharInfo();
		string nameOnScreen = charInfo.GetCharNameOnScreen();
		for(int i = 0; i< totalCharAvars.Length; i++){
			if(totalCharAvars[i].GetPrisonerPrefabName() == nameOnScreen){
				return totalCharAvars[i];
			}
		}

		return null;
	}
}
