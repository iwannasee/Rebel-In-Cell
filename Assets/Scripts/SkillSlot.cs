﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour {

	public GameObject[] skillsToShow;
	public GameObject skillPanelObj;
	public Button skillPanelOKBtn;
	public Button skillPanelCancelBtn;

	private string charName = "";

	private SkillPanel skillPanel;
	private Image skillSlotImage;

	void Start(){
		skillPanel = skillPanelObj.GetComponent<SkillPanel>();
		skillSlotImage = GetComponent<Image>();
		skillSlotImage.sprite = null;

		GetComponent<Button>().onClick.AddListener(() => { ShowSkillsOfCharacter();});
		skillPanelOKBtn.onClick.AddListener(() => { HideSkillPanelAndSave();});
		skillPanelCancelBtn.onClick.AddListener(() => { HideSkillPanel();});

		HideSkillPanel();

	}

	public void LoadSelectedCharSkillsIntoSkillSlot(GameObject charPrefToShowSkill){
		charName = charPrefToShowSkill.GetComponent<Prisoner>().GetPrisonerName();
        skillsToShow = charPrefToShowSkill.GetComponent<ShootingSkill>().skillShotPrefabs;
	}

	public void ShowSkillsOfCharacter(){
		ShowSkillPanel();
		skillPanel.ShowSkillInfo(charName, skillsToShow);
	}

	private void ShowSkillPanel(){
		skillPanelObj.SetActive(true);
	}

	private void HideSkillPanel(){
		skillPanelObj.SetActive(false);
	}

	private void HideSkillPanelAndSave(){
		GameObject selectingSkill = skillPanel.GetSelectingSkill();

		if(selectingSkill.GetComponent<CharacterSkillShot>()){
			string skillNameToSave = selectingSkill.GetComponent<CharacterSkillShot>().GetShotSkillName();
			SaveAsLatestUsedSkill (skillNameToSave);
			skillSlotImage.sprite = selectingSkill.GetComponent<CharacterSkillShot>().GetShotSprtIcon();
		}else if(selectingSkill.GetComponent<SupportSkillShot>()){
			string skillNameToSave = selectingSkill.GetComponent<SupportSkillShot>().GetShotSkillName();
			SaveAsLatestUsedSkill (skillNameToSave);
			skillSlotImage.sprite = selectingSkill.GetComponent<SupportSkillShot>().GetShotSprtIcon();
		}

		skillPanelObj.SetActive(false);
		//Get the showing skill sprite to set skill slot image sprite
		//TODO implement save function here
	}

	void SaveAsLatestUsedSkill (string skillNameToSave)
	{
		string charName = skillPanel.GetSkillUsingCharName ();
		switch (charName) {
		    case CommonData.char_pippo:
			    PlayerProgress.playerData.latestUsedSkill_Pippo = skillNameToSave;
			    break;
		    case CommonData.char_johnny:
			    PlayerProgress.playerData.latestUsedSkill_Johnny = skillNameToSave;
			    break;
		    case CommonData.char_mathial:
			    PlayerProgress.playerData.latestUsedSkill_Mathial = skillNameToSave;
			    break;
		    case CommonData.char_kolav:
			    PlayerProgress.playerData.latestUsedSkill_Kolav = skillNameToSave;
			    break;
            case CommonData.char_maja:
                PlayerProgress.playerData.latestUsedSkill_Maja = skillNameToSave;
                break;
            case CommonData.char_bape:
                PlayerProgress.playerData.latestUsedSkill_Bape = skillNameToSave;
                break;
            case CommonData.char_vie:
                PlayerProgress.playerData.latestUsedSkill_Vie = skillNameToSave;
                break;
            case CommonData.char_lynu:
                PlayerProgress.playerData.latestUsedSkill_Lynu = skillNameToSave;
                break;
            default:
			break;
		}

		SaveLoadSystem.SaveGame(PlayerProgress.playerData);
	}

}
