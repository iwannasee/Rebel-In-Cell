using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour {
	// Use this for initialization
	public GameObject explodeParticlePref;
	public bool bNotInterferedByStageShot;
	private Enemy ShootingEnemy;


	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.GetComponent<BlockOfStage>()||
			col.gameObject.GetComponent<Prisoner>()||
			col.gameObject.GetComponent<PlayerPaddle>()) {

			Instantiate(explodeParticlePref, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}

		if(col.gameObject.GetComponent<ProjectileBall>()){
			if(bNotInterferedByStageShot){return;}

			Instantiate(explodeParticlePref, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	public Enemy GetShootingEnemy(){
		return ShootingEnemy;
	}

	public void SetTheEnemyWhoShot(Enemy Instigator){
		ShootingEnemy = Instigator;
	}
}
