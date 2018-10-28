using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFromFireWall : MonoBehaviour {
	public GameObject explodeParticlePref;
	public float shotSpeed;
	// Use this for initialization
	void Start () {
		Rigidbody2D rg2D = this.GetComponent<Rigidbody2D>() ;
		rg2D.velocity = new Vector2 (0, shotSpeed );
	}
            
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.GetComponent<BlockOfStage>()  || col.gameObject.GetComponent<Enemy>() ||
			col.gameObject.GetComponent<EnemyPaddle>()){
			Instantiate(explodeParticlePref, transform.position, Quaternion.identity);
			 
			Destroy(gameObject);
		}
	}
}
