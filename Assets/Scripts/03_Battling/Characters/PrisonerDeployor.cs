using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerDeployor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject[] charsToPlay = SelectBaseAvarList.GetCharactersToPlay();
        GameObject baseToPlay = SelectBaseAvarList.GetBaseGameObjectToPlay();
        if ((charsToPlay.Length > 0) && baseToPlay) {
            GameObject charBase = Instantiate(baseToPlay, transform.GetChild(0).position, Quaternion.identity) as GameObject;
            charBase.transform.SetParent(transform.GetChild(0));
			
            //temporarily use healthbar component as a index of character slot
            HealthBar[] healthBars = charBase.GetComponentsInChildren<HealthBar>(); 
            Debug.Log("healthBars " + healthBars.Length);
            for (int i = 0; i < healthBars.Length; i++)
            {
                // if character of index i exists, instantiate it and attach it to character slot game object
                if (charsToPlay[i]) {
                    GameObject character = Instantiate(charsToPlay[i], healthBars[i].transform.parent.position, Quaternion.identity) as GameObject;
                    character.transform.SetParent(healthBars[i].transform.parent);
                }
                else { Debug.Log("The position " + i + " is blank"); }
            }
		} else {
			Debug.Log ("there is no character or no vehicle selected");
		}
	}
	
}
