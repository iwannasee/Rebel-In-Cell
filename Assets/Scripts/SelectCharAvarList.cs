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
		for(int i = 0; i<totalCharAvars.Length; i ++){
			totalCharAvars[i].transform.GetChild(0).GetComponent<Image>().sprite = characterLockedImage;
		}
		//Find And Get the total number of available (playable, unlocked) characters in gamedata
		int dataTotalNumberOfChars = PlayerProgress.playerData.availableCharacters.Count; 

		for (int i = 0; i < dataTotalNumberOfChars; i++){
			//Get the character name in database
			string nameInData = PlayerProgress.playerData.availableCharacters[i];

			for(int j = 0; j < totalCharAvars.Length; j ++){  
				string nameInList = totalCharAvars[j].GetPrisonerPrefabName();
				if(nameInData == nameInList){ 
					totalCharAvars[j].UnlockCharacterSelection();
					//Set image sprite for the existing characters
					totalCharAvars[j].SetCharAvarImageSprite();
					break;
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

	public void DeselectCharacterByName(string charNameToDeselect){
		for(int i= 0; i< totalCharAvars.Length; i++){
			string charNameOnCharAvar = totalCharAvars[i].GetPrisonerPrefabName();
            
			if(charNameOnCharAvar == charNameToDeselect){

				CharacterInfoScreen charInfo = totalCharAvars[0].FindAndGetCharInfo();
				string nameOnInfoScreen = charInfo.GetCharNameOnScreen();

				/*if(charNameToDeselect == nameOnInfoScreen)
                {
					totalCharAvars[i].MakeEffectOnSelecting();
					totalCharAvars[i].SetCharDeSelected();
					return;
				}*/
				totalCharAvars[i].RemoveEffectOnSelected();
				totalCharAvars[i].SetCharDeSelected();
				return;
			}
		} 
	}

	public void DeselectAllCharacters(){
		for(int i= 0; i< totalCharAvars.Length; i++){
			totalCharAvars[i].SetCharDeSelected();
			totalCharAvars[i].RemoveEffectOnSelected();
		}
	}
}
