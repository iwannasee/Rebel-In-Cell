﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSkillShot : MonoBehaviour {

	public GameObject skillParticlePref;
	public Sprite shotSprtIcon;
	public float coolDownSpeed;
	public string skillName;

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

}