using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPot : MonoBehaviour {
	public GameObject PoisonEffectPref;
	public int decreaseTimes = 5;
	private int poisonPower;
	// Use this for initialization
	void Start () {
		/*Process for enemy shooting*/
		if(GetComponent<EnemyShot>() && GetComponent<RadiantDamage>()){
			poisonPower = GetComponent<RadiantDamage>().GetDamage();
			return;
		}

		/*Process for player shooting*/
		poisonPower = GetComponent<RadiantDamage>().GetDamage();
		//make the main shot non-damage because the attack damage will be from this poison
		GetComponent<RadiantDamage>().SetDamage(0);
	}

	void OnCollisionEnter2D(Collision2D col){
		/*Process for enemy shooting*/
		if(GetComponent<EnemyShot>() && col.gameObject.GetComponent<Prisoner>()){
			
			// poison effect only has effect on those who are not being poisoned yet
			if(col.transform.GetComponentInChildren<DecreaseHealthOverTime>()){
				return;
			}

			GameObject poison = Instantiate(PoisonEffectPref) as GameObject;
			poison.transform.SetParent(col.transform);
			poison.transform.localPosition = Vector3.zero;
			poison.GetComponent<DecreaseHealthOverTime>().SetHealthDecreaseTime(decreaseTimes);
			poison.GetComponent<DecreaseHealthOverTime>().SetHealthDecreaseRate(poisonPower/decreaseTimes);
			print(poison.name +  " hit prisoner");
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		/*Process for player shooting*/
		if(collider.gameObject.GetComponent<Enemy>() ||
			collider.gameObject.GetComponent<BlockOfStage>() ||
			collider.gameObject.GetComponent<ProjectileBall>()){

			// poison effect only has effect on those who are not being poisoned yet
			if(collider.transform.GetComponentInChildren<DecreaseHealthOverTime>()){
				return;
			}

			GameObject poison = Instantiate(PoisonEffectPref) as GameObject;
			poison.transform.SetParent(collider.transform);
			poison.transform.localPosition = Vector3.zero;
			poison.GetComponent<DecreaseHealthOverTime>().SetHealthDecreaseTime(decreaseTimes);
			poison.GetComponent<DecreaseHealthOverTime>().SetHealthDecreaseRate(poisonPower/decreaseTimes);
		}
	}
}
