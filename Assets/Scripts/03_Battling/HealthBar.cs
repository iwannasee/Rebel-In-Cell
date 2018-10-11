using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	// Use this for initialization
	private Prisoner prisoner;
	Transform actualBarTransform;
	void Start () {
        //remove health bar if no character rides on
        prisoner = transform.parent.GetComponentInChildren<Prisoner>();
        if (!prisoner)
        {
            Debug.Log("no prisoner bar found");
            Destroy(this.gameObject);
			return;
		}

		Debug.Log(prisoner.gameObject.name + "found");
		//Assume actualbar child index is 0. meaning that it is the only child
		actualBarTransform = transform.GetChild(0);
	}

	public void SetHealthBarAccordingly(float healthScaleToSet){
		if(healthScaleToSet < 0f){
			actualBarTransform.localScale = new Vector3(0f, 1f, 1f);
			return;
		}
		actualBarTransform.localScale = new Vector3(healthScaleToSet, 1f, 1f);
	}

}
