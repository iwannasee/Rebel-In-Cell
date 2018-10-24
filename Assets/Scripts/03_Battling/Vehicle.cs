using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {
	public string baseBame;

	public string GetBaseName(){
		return baseBame;
	}

	public bool IsDestroyed(){
		bool isDestroyed = false;
		int health = GetComponent<Health>().GetHealth();
		if(health <=0 ){
			isDestroyed = true;
		}

		return isDestroyed;
	}
}
