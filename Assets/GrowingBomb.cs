using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBomb : MonoBehaviour {
	public float scaleSpeed;
	private RadiantDamage damage;
	private CharacterSkillShot skillShot;
	private int initDamage;

	void Start(){
		skillShot = GetComponent<CharacterSkillShot>();
		//get initial damage of this shot
		//because this is a range-damage shot, get the damage from the explosion particle
		GameObject explosionParticle = GetComponent<CharacterSkillShot>().explodeParticlePref;
		damage = explosionParticle.GetComponent<RadiantDamage>();
		initDamage = damage.GetDamage();
		print("Init dame " + initDamage);


	}
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(
			transform.localScale.x + Time.deltaTime*scaleSpeed,
			transform.localScale.y + Time.deltaTime*scaleSpeed,
			transform.localScale.z);

		float growingDamage = initDamage * transform.transform.localScale.x;
		skillShot.SetShotPower((int)growingDamage);
		if(damage.GetDamage() >= (initDamage * 3)){
			GameObject damager = Instantiate(GetComponent<CharacterSkillShot>().explodeParticlePref, transform.position, Quaternion.identity);

			//damager.GetComponent<RadiantDamage>().SetDamage(growingDamage);
			Destroy(gameObject);
		}
	}
}
