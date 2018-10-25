using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour {
	public enum SkillType{
		SKILL_NONE,
		SKILL_SHOOT,
		SKILL_SUPPORT
	}

	public GameObject[] skillsToShow;
	public GameObject skillPanelObj;
	public Button skillPanelOKBtn;
	public Button skillPanelCancelBtn;

	private string charName = "";
	SkillType skillType;
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

		switch (charName){

			case CommonData.char_pippo:
				skillsToShow = charPrefToShowSkill.GetComponent<ShootingSkill>().skillShotPrefabs;
				skillType = SkillType.SKILL_SHOOT;
				break;
			case CommonData.char_johnny:
				skillsToShow = charPrefToShowSkill.GetComponent<ShootingSkill>().skillShotPrefabs;
				skillType = SkillType.SKILL_SHOOT;
				break;
			case CommonData.char_mathial:
				skillsToShow = charPrefToShowSkill.GetComponent<ShootingSkill>().skillShotPrefabs;
				skillType = SkillType.SKILL_SHOOT;
				break; 
			case CommonData.char_kolav:
				skillsToShow = charPrefToShowSkill.GetComponent<ShootingSkill>().skillShotPrefabs;
				skillType = SkillType.SKILL_SHOOT;
				break;
			default:
				print("nothing is selecting");
				break;
		}
	}

	public void ShowSkillsOfCharacter(){
		print("show skill");
		ShowSkillPanel();
		skillPanel.ShowSkillInfo(charName, skillsToShow, skillType);
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
		default:
			break;
		}

		SaveLoadSystem.SaveGame(PlayerProgress.playerData);
	}

}
