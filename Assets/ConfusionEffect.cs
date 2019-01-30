using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionEffect : MonoBehaviour {
	public float confusionTime;

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.GetComponent<Prisoner>()){
			//Do nothing if there is no time of confusion
			if(confusionTime <= 0){return;}

			//Find player paddle
			PlayerPaddle playerPaddle =	GameObject.FindGameObjectWithTag("Prisoner Paddle").GetComponent<PlayerPaddle>();
			playerPaddle.SetBeingConfused(confusionTime);
		}
	}
}
