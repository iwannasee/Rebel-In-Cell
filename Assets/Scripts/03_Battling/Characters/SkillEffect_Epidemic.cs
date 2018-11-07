using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect_Epidemic : MonoBehaviour {
	public GameObject epidemicInvadePref;
	public GameObject badStatusPref;
	public float statusDuration;
	public int lifeCost;

	void Start(){
		GameObject fx = Instantiate(badStatusPref, transform.position, Quaternion.identity) as GameObject;
	
		statusDuration = badStatusPref.GetComponent<ParticleSystem>().main.duration;
		Destroy(fx, statusDuration);
		Destroy(gameObject, statusDuration);

	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.GetComponent<EnemyShot>()){
			Transform targetEnemy = collider.GetComponent<EnemyShot>().GetShootingEnemy().transform;
			GameObject actualDame = Instantiate(epidemicInvadePref, targetEnemy.position, Quaternion.identity);
			actualDame.GetComponent<RadiantDamage>().SetDamage(GetComponent<SupportSkillShot>().GetShotPower());
			Destroy(gameObject);
		}
	}

	public int GetLifeCost(){
		return lifeCost;
	}
}
