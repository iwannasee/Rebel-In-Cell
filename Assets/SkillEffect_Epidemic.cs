﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect_Epidemic : MonoBehaviour {
	public GameObject epidemicInvadePref;
	public GameObject badStatusPref;
	public float statusDuration;
	public int lifeCost;

	void Start(){
		Instantiate(badStatusPref, transform.position, Quaternion.identity);
		statusDuration = badStatusPref.GetComponent<ParticleSystem>().main.duration;
		Destroy(badStatusPref, statusDuration);
		Destroy(gameObject, statusDuration);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.GetComponent<EnemyShot>()){
			Transform targetEnemy = collider.GetComponent<EnemyShot>().GetShootingEnemy().transform;
			Instantiate(epidemicInvadePref, targetEnemy.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	public int GetLifeCost(){
		return lifeCost;
	}
}
