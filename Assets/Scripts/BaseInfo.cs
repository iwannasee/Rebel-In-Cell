using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInfo : MonoBehaviour {
	public Text baseName;
	public Text baseHealth;


	public void SetDisplayBaseName(string nameToDisplay){
		baseName.text = nameToDisplay;
	}

	public void SetDisplayBaseHealth(int healthToDisplay){
		baseHealth.text = healthToDisplay.ToString();
	}
}
