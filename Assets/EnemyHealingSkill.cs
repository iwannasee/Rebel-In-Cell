using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealingSkill : MonoBehaviour {
	public GameObject healingParticleOnTarget;
	public GameObject healingCastingParticle;
	public bool bCanHealAll;

	public void Heal(int healingAmount){
		Enemy[] enemiesOfThisWave = transform.parent.GetComponentsInChildren<Enemy>();
		if(bCanHealAll){
			for(int i = 0; i<enemiesOfThisWave.Length; i++){
				enemiesOfThisWave[i].GetComponent<Health>().AddHealth(healingAmount/2);
				Instantiate(healingCastingParticle, transform.position, Quaternion.identity);
				Instantiate(healingParticleOnTarget, enemiesOfThisWave[i].transform.position, Quaternion.identity);
			}
		}else{ //Heal a random enemy
			int randomEnemyIndex = Random.Range(0, enemiesOfThisWave.Length-1);
			enemiesOfThisWave[randomEnemyIndex].GetComponent<Health>().AddHealth(healingAmount);
			Instantiate(healingCastingParticle, transform.position, Quaternion.identity);
			Instantiate(healingParticleOnTarget, enemiesOfThisWave[randomEnemyIndex].transform.position, Quaternion.identity);
		}
	}
}
