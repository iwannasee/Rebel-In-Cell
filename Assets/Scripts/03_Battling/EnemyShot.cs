using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour {
	// Use this for initialization
	public GameObject explodeParticlePref;
	public bool bNotInterferedByStageShot;
	private Enemy ShootingEnemy;


	public bool bCanFreezePaddle;
	public bool bCanFreezePrisoner;
	public float freezingTime;


	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.GetComponent<BlockOfStage>()||
			col.gameObject.GetComponent<Prisoner>()||
			col.gameObject.GetComponent<PlayerPaddle>()) {
	
			if(col.gameObject.GetComponent<Prisoner>() && bCanFreezePrisoner){
				col.gameObject.GetComponent<ShootingSkill>().SetSilence(freezingTime);
				GameObject fx = Instantiate(GetComponent<EnemyShot>().explodeParticlePref, col.transform.position, Quaternion.identity);
				fx.GetComponent<DetroyAfterSecond>().lastingTime = freezingTime;
			}else if(col.gameObject.GetComponent<PlayerPaddle>() && bCanFreezePaddle){
				col.gameObject.GetComponent<PlayerPaddle>().SetCannotMove(freezingTime);
				GameObject fx = Instantiate(GetComponent<EnemyShot>().explodeParticlePref, col.transform.position, Quaternion.identity);
				fx.GetComponent<DetroyAfterSecond>().lastingTime = freezingTime;

			}else{
				Instantiate(explodeParticlePref, transform.position, Quaternion.identity);
			}

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
