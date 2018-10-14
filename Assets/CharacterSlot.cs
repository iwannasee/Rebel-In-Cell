using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour {
	private Transform arrow;
    private GameObject prefabOfSelectedCharacter;
	// Use this for initialization
	void Start () {
		//hide the arrow indicating available slot
		arrow = transform.GetChild(0);
		arrow.GetComponent<Button>().onClick.AddListener(() => { SelectingCharacterToSlot();});
		HideArrow();

		GetComponent<Button>().interactable = false;
		GetComponent<Button>().onClick.AddListener(() => { RemoveCharacterFromSlot();});
	}

	public void ShowArrow(){
		arrow.gameObject.SetActive(true);
	}

	public void HideArrow(){
		arrow.gameObject.SetActive(false);
	}

	public bool IsCharAvarSelectedInThisSlot(){
		//TODO promote this variable to member variable later
		SelectCharAvarList charAvarList = GameObject.FindGameObjectWithTag("Select Char Avar List").GetComponent<SelectCharAvarList>();

		string nameInPrefab = "";

		if(GetCharacterPrefabInThisSlot()){
			nameInPrefab = GetCharacterPrefabInThisSlot().GetComponent<Prisoner>().GetPrisonerName();
		}

		for(int i = 0; i< charAvarList.transform.childCount; i++){
			string PrisonerPrefabName = charAvarList.transform.GetChild(0).GetComponent<Selection_CharAvar>().GetPrisonerPrefabName();
			if(nameInPrefab == PrisonerPrefabName){
				print("hy not OK");
				return true;
			}
		}
		return false;
	}

	private void SelectingCharacterToSlot(){
		if(prefabOfSelectedCharacter != null){
			RemoveCharacterFromSlot();
		}
		
		//Get selecting character
		SelectCharAvarList charAvarList = GameObject.FindGameObjectWithTag("Select Char Avar List").GetComponent<SelectCharAvarList>();

		Selection_CharAvar selectingCharacter = charAvarList.GetIsOnInfoScrnCharacter();
		//De-effect of selection in avar list
		//charAvarList.DeselectAllCharacters();

		if(selectingCharacter == null){return;}
		//get the selecting character sprite
		Transform actualImgSprtObj = selectingCharacter.transform.GetChild(0);
		Sprite selectingCharSprite = actualImgSprtObj.GetComponent<Image>().sprite;

		//Show effect of selection character
		selectingCharacter.SetCharSelected();
		selectingCharacter.MakeEffectOnSelected();

        //Get for-playing character prefab
        prefabOfSelectedCharacter = selectingCharacter.GetCharPrefabOfThisAvar();
        //set that to the image sprite in the slot
        GetComponent<Image>().sprite = selectingCharSprite;
		GetComponent<Button>().interactable = true;
		
		HideAllArrows();
	}

	private void RemoveCharacterFromSlot(){
		//Disable interaction of the slot button
		GetComponent<Button>().interactable = false;

		/*	OPTIONAL */
        //UnSelectedCharacter
		string nameOfBeingSelectedChar = prefabOfSelectedCharacter.GetComponent<Prisoner>().GetPrisonerName();
		SelectCharAvarList charAvarList = GameObject.FindGameObjectWithTag("Select Char Avar List").GetComponent<SelectCharAvarList>();
		charAvarList.DeselectCharacterByName(nameOfBeingSelectedChar);


		prefabOfSelectedCharacter = null;
		GetComponent<Image>().sprite = null;

		//Show arrows in every slots
		CharacterSlot[] slots = transform.parent.GetComponentsInChildren<CharacterSlot>();
		for(int i = 0; i < slots.Length; i++){
			slots[i].ShowArrow();
		}
    }

    public GameObject GetCharacterPrefabInThisSlot()
    {
        return prefabOfSelectedCharacter;
    }

	//Hide All Arrows of the base of this slot
    private void HideAllArrows(){
		//Get the slots of the base
		CharacterSlot[] slots = transform.parent.GetComponentsInChildren<CharacterSlot>();
		//Hide every arrows of slots 
		foreach(CharacterSlot slot in slots){
			slot.HideArrow();
		}
    }
}
