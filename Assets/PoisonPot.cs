using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPot : MonoBehaviour {
	public GameObject PoisonEffectPref;
	private int poisonPower;
	// Use this for initialization
	void Start () {
		poisonPower = GetComponent<RadiantDamage>().GetDamage();
		//make the main shot non-damage because the attack damage will be from this poison
		GetComponent<RadiantDamage>().SetDamage(0);
	}

	void OnTriggerEnter2D(Collider2D collider){
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
				int healthDecreaseTimes = poison.GetComponent<DecreaseHealthOverTime>().GetHealthDecreasingTimes();
				poison.GetComponent<DecreaseHealthOverTime>().SetHealthDecreaseRate(poisonPower/healthDecreaseTimes);
			}
	}
}
