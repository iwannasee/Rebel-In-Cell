using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportSkillShot : MonoBehaviour {

	public GameObject explodeParticlePref;
	public Sprite shotSprtIcon;
	public float shotSpeed;
	public float coolDownSpeed;
	public string skillName;

	private int shotPower;

	private Rigidbody2D rg2D;


	//---------------------------------------------------------------
	// Use this for initialization
	void Start () {
		shotPower = GetShotPower();
		
	} 

	public Sprite GetShotSprtIcon(){
		return shotSprtIcon;
	}

	public string GetShotSkillName(){
		return skillName;
	}

	public int GetShotPower(){
		return explodeParticlePref.GetComponent<RadiantDamage>().GetDamage();
	}

	public float GetShotCoolDownSpeed(){
		return coolDownSpeed;
	}

}
