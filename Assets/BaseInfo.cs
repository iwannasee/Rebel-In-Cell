using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInfo : MonoBehaviour {
	public Text baseName;
	public Text baseHealth;

	// Use this for initialization
	void Start () { 
		/*Transform currentBase = GetComponentInParent<SelectBaseAvarList>().GetCurrentBase();
		string initBaseName = currentBase.GetComponent<Selection_DefenceBaseAvar>().GetBasePrefabName();
		SetDisplayBaseName(initBaseName);
		int initHealth = currentBase.GetComponent<Selection_DefenceBaseAvar>().GetBaseHealth();
		SetDisplayBaseHealth(initHealth);*/
	}


	public void SetDisplayBaseName(string nameToDisplay){
		baseName.text = nameToDisplay;
	}

	public void SetDisplayBaseHealth(int healthToDisplay){
		baseHealth.text = healthToDisplay.ToString();
	}
}
