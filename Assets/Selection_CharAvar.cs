using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection_CharAvar : MonoBehaviour {
	public GameObject prisonerPref;
	public string charInfoText;
	private bool bIslocked = true;

	//private bool bIsSelectingToViewInfo = false;

    private bool bIsSelected = false;
    private CharacterInfoScreen charInfo;

	// Use this for initialization
	void Start () {
        //Set not-selecting 
        DeEffectOfSelecting();
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

        GameObject baseAvarList = GameObject.FindGameObjectWithTag("Base Avar List");
        if (bIsSelected) {
            SetCharSelectToInfoScrn();
            baseAvarList.GetComponent<SelectBaseAvarList>().HideAllPoppingArrows();
        } else{
            SetCharSelectToInfoScrn();

            baseAvarList.GetComponent<SelectBaseAvarList>().ShowAvailableSlotsInBase();
        }

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
			//if(!charAvars[i].GetIsCharSelected()){
				charAvars[i].DeEffectOfSelecting();
			//}
		}
    	//show effect when this avar is selecting
		MakeEffectOnSelecting();
    }

	public void SetCharDeSelected(){
    	bIsSelected = false;
    }

	public void SetCharSelected(){
    	bIsSelected = true;
    }

    public bool GetIsCharSelected()
    {
        return bIsSelected;
    }

    public void SetCharAvarImageSprite(){
		Sprite spriteToSet = GetCharPrefabOfThisAvar().GetComponent<Prisoner>().GetPrisonerSprt();
		transform.GetChild(0).GetComponent<Image>().sprite = spriteToSet;
    }

	public void MakeEffectOnSelecting(){
        transform.GetChild(1).gameObject.SetActive(true);
    }
    private void DeEffectOfSelecting()
    {
        transform.GetChild(1).gameObject.SetActive(false);
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
