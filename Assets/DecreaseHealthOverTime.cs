using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseHealthOverTime : MonoBehaviour {

	public int decreasingTimes;
	public float maxTimeToDecrease;

	private float timeToDecrease;
	private int healthDecreasedPerTime;
	private Health healthOfPoisonedObj;

	// Use this for initialization
	void Start () {
		healthOfPoisonedObj = transform.parent.GetComponent<Health>();
		timeToDecrease = maxTimeToDecrease;
	}
	
	// Update is called once per frame
	void Update () {
		timeToDecrease -= Time.deltaTime;
		if(timeToDecrease <= 0){
			timeToDecrease = maxTimeToDecrease;
			healthOfPoisonedObj.AddHealth(-healthDecreasedPerTime, true);
			decreasingTimes--;
			if(decreasingTimes <= 0){
				Destroy(gameObject);
			}
		}
	}

	public void SetHealthDecreaseRate(int decreaseRate){
		healthDecreasedPerTime = decreaseRate;
	}

	public int GetHealthDecreasingTimes(){
		return decreasingTimes;
	}
}
