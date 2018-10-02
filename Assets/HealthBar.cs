using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	/// <summary>
	/// Assume the character will be spawned last in hiearchy of this whole gameobject
	/// Means that: Spawnpoint -> Cart -> (1st) front == (2nd) back == (3rd) health bar (this) == (4th) skill bar == (5th) char
	/// </summary>
	// Use this for initialization
	private Prisoner prisoner;
	Transform actualBarTransform;
	void Start () {
		//remove health bar if no character rides on
		if(transform.parent.childCount<=4){
			Destroy(this.gameObject);
			return;
		}

		prisoner = transform.parent.GetChild(4).GetComponent<Prisoner>();
		if(!prisoner){ 
			Debug.Log("no prisoner bar found");
		}else{
			Debug.Log(prisoner.gameObject.name + "found");
			//Assume actualbar child index is 0. meaning that it is the only child
			actualBarTransform = transform.GetChild(0);
		}

	}

	public void SetHealthBarAccordingly(float healthScaleToSet){
		if(healthScaleToSet < 0f){
			return;
		}
		actualBarTransform.localScale = new Vector3(healthScaleToSet, 1f, 1f);
	}

}
