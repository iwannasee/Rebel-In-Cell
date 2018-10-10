using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterInfoScreen : MonoBehaviour {
	public Image displayCharImage;
	public Image skillImage;
	public Image equipmentImage;
	public Text charInfoText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetOnScrnCharSprt(Sprite spriteToUpdate){
		displayCharImage.sprite = spriteToUpdate;
	}

	public void SetSkillSprtOfSelectedChar(Sprite spriteToUpdate){
		skillImage.sprite = spriteToUpdate;
	}

	public void SetEquipmentSprtOfSelectedChar(Sprite spriteToUpdate){
		equipmentImage.sprite = spriteToUpdate;
	}

	public void SetInfoTextOfSelectedChar(string infoTextToUpdate){
		charInfoText.text = infoTextToUpdate;
	}

	public void ClickTest(){
		print( "click test " +EventSystem.current.currentSelectedGameObject.name);
	}
}
