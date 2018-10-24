using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSkillShot : MonoBehaviour {

	public GameObject castSkillFXPref;
	public Sprite shotSprtIcon; 
	public float coolDownSpeed;
	public string skillName;
	public bool bIsAutoSkill;
	 
	public int shotPower;


	public Sprite GetShotSprtIcon(){
		return shotSprtIcon;
	}

	public string GetShotSkillName(){
		return skillName;
	}

	public int GetShotPower(){
		return shotPower;
	}

	public float GetShotCoolDownSpeed(){
		return coolDownSpeed;
	}

	public bool GetIsAutoSkill(){
		return bIsAutoSkill;
	}
}
