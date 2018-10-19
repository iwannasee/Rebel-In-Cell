using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {
	public GameObject shotListPanel;
	public GameObject PowerProp;
	public GameObject CoolDownProp;
	public Text skillNameToShowText;
	public Sprite lockedSkillSprite;
	public Sprite IsSelectingSprite;
	public Sprite nonSelectingSprite;
	private GameObject[] skillsOfPassingInChar;

	private GameObject SelectingSkill;
	private string skillUsingCharName;

	void Start(){
		Button[] skillButtons = shotListPanel.GetComponentsInChildren<Button>();
		print("skillButtons.Length " + skillButtons.Length);

		//Assume there are 
		skillButtons[0].onClick.AddListener(() => { ShowSelectedSkillInfo(0);});
		skillButtons[1].onClick.AddListener(() => { ShowSelectedSkillInfo(1);});
		skillButtons[2].onClick.AddListener(() => { ShowSelectedSkillInfo(2);});

		/*for(int i = 0; i < skillButtons.Length; i++){
			skillButtons[i].onClick.AddListener(() => { ShowSelectedSkillInfo(i);});
		}*/
	}

	//This is done in accordance with the assuming that the order of the skill list in shooting skill script 
	// is the same as order of skills in Common data
	// NOTE: Comparison method based on sprite comparison
	private void ShowSelectedSkillInfo(int selectingSkillIndex){
		print("button " + selectingSkillIndex);
		//Get Selecting skill avar index


		for(int i = 0; i < shotListPanel.transform.childCount; i++){
			shotListPanel.transform.GetChild(i).GetComponent<Image>().sprite = nonSelectingSprite;

			//Set selecting effect on the skill which is equal to the button index 
			if(selectingSkillIndex == i){
				shotListPanel.transform.GetChild(i).GetComponent<Image>().sprite = IsSelectingSprite; 
			}
		}
		GameObject skillToShowInfo = skillsOfPassingInChar[selectingSkillIndex];

		//Set skill name to display
		if(skillToShowInfo.GetComponent<CharacterSkillShot>()){
			SelectingSkill = skillToShowInfo;
			CharacterSkillShot skillShot = skillToShowInfo.GetComponent<CharacterSkillShot>();
			string skillNameToDisplay = skillShot.GetShotSkillName();
			SetSkillNameToDisplay(skillNameToDisplay);

			//Set equivalent property to display
			//Power Property
			int skillPower = skillShot.GetShotPower();
			SetPowerPropertyToDisplay(skillPower);

			float skillReUseSpeed = skillShot.GetShotCoolDownSpeed();
			SetCoolDownSpeedPropertyToDisplay(skillReUseSpeed);
			//Cooldown Speed Property
			//........
		}else{
			print("this is not shooting skill");
		}
	}

	public void ShowSkillInfo(string charName, GameObject[] skills, SkillSlot.SkillType type){
		/*if(skillList.Length != skills.Length){
			print("number of character skills is not relevant to number of skill slots in skill panel");
			return;
		}*/
		//if(!skills[0]){
			
		//}
		skillsOfPassingInChar = skills;
		skillUsingCharName = charName;
		switch(type){
			case SkillSlot.SkillType.SKILL_SHOOT:
				for(int i = 0; i < skills.Length; i++){
					
					CharacterSkillShot thisSkillShot = skills[i].GetComponent<CharacterSkillShot>();
					Sprite shotSprt = thisSkillShot.GetShotSprtIcon();
					string shotName = thisSkillShot.GetShotSkillName();

					Transform shotSlotToSetSprt = shotListPanel.transform.GetChild(i);

					//Check if this skill is available to be used or not
					//if is available, turn interactability of the shot and set sprite onto the slot
					if(IsSkillUnlocked(charName, shotName) == true){
						shotSlotToSetSprt.GetChild(0).GetComponent<Button>().interactable = true;
						shotSlotToSetSprt.GetChild(0).GetComponent<Image>().sprite = shotSprt;

						//if this shot is latest used shot, set it on selection and show all info of the shot
						if(IsLastestUsedSkill(charName, shotName) == true){
							shotSlotToSetSprt.GetComponent<Image>().sprite = IsSelectingSprite;
							SetSkillNameToDisplay(shotName);

							int skillPower = thisSkillShot.GetShotPower();
							SetPowerPropertyToDisplay(skillPower);

							float skillReUseSpeed = thisSkillShot.GetShotCoolDownSpeed();
							SetCoolDownSpeedPropertyToDisplay(skillReUseSpeed);

							SelectingSkill = skills[i];

						//if this shot is not latest used shot, set the sprite indicating the shot being selecting to null
						//TODO set it to a transparent image sprite instead of null
						}else{
							shotSlotToSetSprt.GetComponent<Image>().sprite = nonSelectingSprite;
						}

					//if is unavailable to use, hide it out by setting a "lock" sprite, also hide "selecting" sprite and
					//turn off the button interactability
					}else{
						shotSlotToSetSprt.GetChild(0).GetComponent<Image>().sprite = lockedSkillSprite;
						shotSlotToSetSprt.GetChild(0).GetComponent<Button>().interactable = false;
						shotSlotToSetSprt.GetComponent<Image>().sprite = nonSelectingSprite;
						//shotSlotToSetSprt.GetChild(0).gameObject.SetActive(false);
					}
				}

				break;

			default: break;
		}


	}
	private bool IsLastestUsedSkill(string charName, string skillName){
		bool isLatestUsed = false;
		switch(charName){
		case CommonData.char_pippo:
			if(skillName == PlayerProgress.playerData.latestUsedSkill_Pippo){
				isLatestUsed = true;
			}
			break;

		case CommonData.char_johnny:
			if(skillName == PlayerProgress.playerData.latestUsedSkill_Johnny){
				isLatestUsed = true;
			}
			break;

		case CommonData.char_mathial:
			if(skillName == PlayerProgress.playerData.latestUsedSkill_Mathial){
				isLatestUsed = true;
			}
			break;

		case CommonData.char_kolav:
			if(skillName == PlayerProgress.playerData.latestUsedSkill_Kolav){
				isLatestUsed = true;
			}
			break;

			default: break;
		}
		return isLatestUsed;
	}

	private bool IsSkillUnlocked(string charName, string skillName){
		bool isSkillUnlocked = false;

		switch(charName){
		//Compare skill shot name in prefab with skill in available skill;
		case CommonData.char_pippo:
			if(PlayerProgress.playerData.pippo_availableskills.Contains(skillName)){
				isSkillUnlocked = true;
			}
			break;
		case CommonData.char_johnny:
			if(PlayerProgress.playerData.johnny_availableskills.Contains(skillName)){
				isSkillUnlocked = true;
			}
			break;
		case CommonData.char_mathial:
			if(PlayerProgress.playerData.mathial_availableskills.Contains(skillName)){
				isSkillUnlocked = true;
			}
			break;
		case CommonData.char_kolav:
			if(PlayerProgress.playerData.kolav_availableskills.Contains(skillName)){
				isSkillUnlocked = true;
			}
			break;

			default: break;
		}

		return isSkillUnlocked;
	}

	private void SetSkillNameToDisplay(string skillNameToDisplay){
		skillNameToShowText.text = skillNameToDisplay;
	}

	private void SetPowerPropertyToDisplay(int skillPower){
		string demoPowerBar = "";
		while(skillPower > 0){
			if((skillPower -10) >= 0){
				demoPowerBar = demoPowerBar + "O";
			}
			skillPower -= 10;
		}

		PowerProp.transform.GetChild(0).GetComponent<Text>().text = demoPowerBar;
	}

	private void SetCoolDownSpeedPropertyToDisplay( float skillCoolDownSpeed){
		string demoCoolDownSpeedBar = "";
		while(skillCoolDownSpeed > 0){
			if((skillCoolDownSpeed -1) >= 0){
				demoCoolDownSpeedBar = demoCoolDownSpeedBar + "I";
			}
			skillCoolDownSpeed -= 1;
		}

		CoolDownProp.transform.GetChild(0).GetComponent<Text>().text = demoCoolDownSpeedBar;
	}

	public GameObject GetSelectingSkill(){
		return SelectingSkill;
	}

	public string GetSkillUsingCharName(){
		return skillUsingCharName;
	}
}
