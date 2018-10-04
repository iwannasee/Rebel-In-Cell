using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour {
	// Use this for initialization
	public GameObject explodeParticlePref;
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Brick")||
			col.gameObject.CompareTag("Prisoner")||
			col.gameObject.CompareTag("Prisoner Paddle")) {
			Instantiate(explodeParticlePref, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

}
