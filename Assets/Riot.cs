using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riot : MonoBehaviour {
	public int healPowerWhenComeBack ;
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.GetComponent<BlockOfStage>() || 
			col.gameObject.GetComponent<Enemy>()){
			GameObject explosion = Instantiate(GetComponent<CharacterSkillShot>().explodeParticlePref, transform.position, Quaternion.identity);
		}else if(col.gameObject.GetComponent<Prisoner>()){
			if(col.gameObject.GetComponent<Prisoner>().GetPrisonerName() == CommonData.char_kolav){
				col.gameObject.GetComponent<Health>().AddHealth(healPowerWhenComeBack);
			}
			Destroy(gameObject);
		}else if(col.gameObject.GetComponent<Vehicle>()){
			Destroy(gameObject);
		}

	}
}
