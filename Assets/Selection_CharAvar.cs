using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection_CharAvar : MonoBehaviour {
	public GameObject prisonerPref;
	public string charInfoText;
	private bool bIslocked = true;

	private bool bIsSelected = false;
	private CharacterInfoScreen charInfo;

	// Use this for initialization
	void Start () {
		charInfo = FindAndGetCharInfo();
	}

	public CharacterInfoScreen FindAndGetCharInfo(){
		Transform charPanel = transform.parent.parent;
		return charPanel.GetComponentInChildren<CharacterInfoScreen>();
	}

	public string GetPrisonerPrefabName(){
		string prisonerPrefabName = "";
		if(prisonerPref){
			prisonerPrefabName = prisonerPref.GetComponent<Prisoner>().GetPrisonerName();
		}

		return prisonerPrefabName;
	}

	public void CharacterIsSelecting(){
		if(bIslocked){return;}


		charInfo.SetOnScrnCharSprt(GetThisCharAvarSprt());
		charInfo.SetInfoTextOfSelectedChar(charInfoText);
		charInfo.SetCharNameOnScreen(GetPrisonerPrefabName());
			
		if(bIsSelected){return;}
		SetCharSelectToInfoScrn();
		//TODO update equipment and skill 
	}

	public void UnlockCharacterSelection(){
		bIslocked = false;
	}

    public GameObject GetCharPrefabOfThisAvar()
    {
        return prisonerPref;
    }

	private void SetCharSelectToInfoScrn(){
		//If this avar is selected, ignore

		//Deselecting all the avar first
		Selection_CharAvar[] charAvars = transform.parent.GetComponentsInChildren<Selection_CharAvar>();
		for (int i = 0; i <charAvars.Length; i++){
			if(!charAvars[i].GetIsCharSelected()){
				charAvars[i].RemoveEffectOnSelected();
			}
		}

    	//show effect when this avar is selecting
		MakeEffectOnSelecting();
    }

	public void SetCharDeSelected(){
    	bIsSelected = false;
    }
	public bool GetIsCharSelected(){
    	return bIsSelected;
    }
	public void SetCharSelected(){
    	bIsSelected = true;
    }
    public void SetCharAvarImageSprite(){
		Sprite spriteToSet = GetCharPrefabOfThisAvar().GetComponent<Prisoner>().GetPrisonerSprt();
		transform.GetChild(0).GetComponent<Image>().sprite = spriteToSet;
    }

	public void MakeEffectOnSelecting(){
		GetComponent<Image>().color = new Color(1,0,0,0.5f);
    }

	public void MakeEffectOnSelected(){
		GetComponent<Image>().color = new Color(1,0,0,1);
    }

	public void RemoveEffectOnSelected(){
		GetComponent<Image>().color = new Color(1,1,1,1);
    }

 	private Sprite GetThisCharAvarSprt(){
 		return transform.GetChild(0).GetComponent<Image>().sprite;
 	}
}
