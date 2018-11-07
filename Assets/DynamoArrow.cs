using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamoArrow : MonoBehaviour {
	public int boostPow = 4;
	private int boost = 1;
	int powerToSet = 0;
	// Use this for initialization


	void OnTriggerEnter2D(Collider2D collider){

		if(collider.GetComponent<EnemyShot>()){
			collider.GetComponent<CircleCollider2D>().enabled = false;
			collider.GetComponent<Rigidbody2D>().isKinematic = true;
			collider.transform.SetParent(transform, true);
			boost *= boostPow; 
		}

		if(collider.GetComponent<BlockOfStage>() || collider.GetComponent<Enemy>() ||
			collider.GetComponent<EnemyPaddle>()){
			GameObject explosion = Instantiate(GetComponent<CharacterSkillShot>().explodeParticlePref, transform.position, Quaternion.identity);

			int originPower = GetComponent<CharacterSkillShot>().GetShotPower();
			powerToSet = boost*originPower;
			GetComponent<RadiantDamage>().SetDamage(powerToSet);
			Destroy(gameObject);
		}	
	}
}


