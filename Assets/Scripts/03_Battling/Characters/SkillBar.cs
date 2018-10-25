using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBar : MonoBehaviour {
	// Use this for initialization
	private Prisoner prisoner;
	Transform actualBarTransform;
	void Start () {
        //remove skill bar if no character rides on
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

	public void SetSkillBarAccordingly(float skillScaleToSet){
		if(skillScaleToSet < 0f){
			return;
		}

		actualBarTransform.localScale = new Vector3(skillScaleToSet, 1f, 1f);
	}
}