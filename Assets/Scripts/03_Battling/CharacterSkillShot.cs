﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillShot : MonoBehaviour {
	public float shotSpeed;
	public GameObject explodeParticlePref;
	private Rigidbody2D rg2D;
	//---------------------------------------------------------------
	// Use this for initialization
	void Start () {
		//TODO replace FindObjectOfType by FindObjectByTag
		Needle needle = GameObject.FindObjectOfType<Needle>();
		if (!needle) {
			return; //exit if no needle found
		}
		Vector3 shotDerivingPosition = needle.GetComponent<Transform> ().right;
		rg2D = this.GetComponent<Rigidbody2D>() ;
		rg2D.velocity = new Vector2 (shotSpeed * shotDerivingPosition.x,shotSpeed * shotDerivingPosition.y );
	} 

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.GetComponent<BlockOfStage>() || col.gameObject.GetComponent<Enemy>() ||
			col.gameObject.GetComponent<EnemyPaddle>()){
			Instantiate(explodeParticlePref, transform.position, Quaternion.identity);
			 
			Destroy(gameObject);
		}
	}
} 
