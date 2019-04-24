﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterInfoScreen : MonoBehaviour {
	public Image displayCharImage;
	public Image skillImage;
	public Image equipmentImage;
	public Text charInfoText;
	public Text goldText;

	private string charName;

	void Start(){
		goldText.text = PlayerProgress.playerData.gold.ToString();
	}

	public void SetSelectedCharToInfoScrn(GameObject charPrefToSetInfo, Sprite charSprtToSet, string charInfoToShow){

        if (!charPrefToSetInfo.GetComponent<Prisoner>()){
			print("cannot set to info screen with non-character obj");
			return;
		}

		SetOnScrnCharSprt(charSprtToSet);
		SetInfoTextOfSelectedChar(charInfoToShow);

		string selectedCharName = charPrefToSetInfo.GetComponent<Prisoner>().GetPrisonerName();
		SetCharNameOnScreen(selectedCharName);

		//check the shot type is shooting or supporting
		GameObject latestUsedSkill = charPrefToSetInfo.GetComponent<ShootingSkill>().GetSkillShotToPlay();
        if (!latestUsedSkill) { print("no skillllll"); }

        if (latestUsedSkill.GetComponent<CharacterSkillShot>()){
			SetSkillSprtOfSelectedChar(latestUsedSkill.GetComponent<CharacterSkillShot>().GetShotSprtIcon());
		}else if( latestUsedSkill.GetComponent<SupportSkillShot>()){
			SetSkillSprtOfSelectedChar(latestUsedSkill.GetComponent<SupportSkillShot>().GetShotSprtIcon());
		}
		

		//first load all skill (locked and unlocked) into skill slot
		if(skillImage.GetComponent<SkillSlot>()){
			skillImage.GetComponent<SkillSlot>().LoadSelectedCharSkillsIntoSkillSlot(charPrefToSetInfo);
		}

	}

	public void SetOnScrnCharSprt(Sprite spriteToUpdate){
		displayCharImage.sprite = spriteToUpdate;
        displayCharImage.color = new Color(1, 1, 1, 1);

    }

	public void SetSkillSprtOfSelectedChar(Sprite spriteToUpdate){
		skillImage.sprite = spriteToUpdate;
        skillImage.color = new Color(1, 1, 1, 1);
    }


	public void SetEquipmentSprtOfSelectedChar(Sprite spriteToUpdate){
		equipmentImage.sprite = spriteToUpdate;
	}

	public void SetInfoTextOfSelectedChar(string infoTextToUpdate){
        charInfoText.fontSize = 16;

        charInfoText.text = infoTextToUpdate;
	}

	public void ClickTest(){
		print( "click test " +EventSystem.current.currentSelectedGameObject.name);
	}

	public void SetCharNameOnScreen(string nameToSet){
		charName = nameToSet;
	}

	public string GetCharNameOnScreen(){
		return charName;
	}
}
