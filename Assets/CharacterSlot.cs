using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour {
	private Transform arrow;

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

	private void SelectingCharacterToSlot(){
		//Get selecting character
		SelectCharAvarList charAvarList = GameObject.FindGameObjectWithTag("Select Char Avar List").GetComponent<SelectCharAvarList>();
		Selection_CharAvar selectingCharacter = charAvarList.GetIsOnInfoScrnCharacter();
		 
		if(selectingCharacter == null){return;}
		//get the selecting character sprite
		Transform actualImgSprtObj = selectingCharacter.transform.GetChild(0);
		Sprite selectingCharSprite = actualImgSprtObj.GetComponent<Image>().sprite;

		//set that to the image sprite in the slot
		GetComponent<Image>().sprite = selectingCharSprite;
		GetComponent<Button>().interactable = true;
		HideArrow();
	}

	private void RemoveCharacterFromSlot(){
		ShowArrow();
		GetComponent<Image>().sprite = null;
	}
}
