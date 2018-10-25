using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallEffect : MonoBehaviour {
	public GameObject fireBulletPref;

	void OnTriggerEnter2D(Collider2D col){
		if(col.GetComponent<ProjectileBall>() ||
			col.GetComponent<EnemyShot>()){
				Vector2 spawnPoint = new Vector2(col.transform.position.x, transform.position.y);
				Instantiate(fireBulletPref, spawnPoint, Quaternion.identity);
			}
	}
} 
