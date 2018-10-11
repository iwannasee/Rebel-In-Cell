using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Selection_DefenceBaseAvar : MonoBehaviour {
	public GameObject BasePref;
	public Sprite frontImg;
	public Sprite backImg;
	private Image characterSlot;
	// Use this for initialization
	void Start () {
		characterSlot = transform.GetChild(1).GetComponent<Image>();
	}

	public string GetBasePrefabName(){
		return BasePref.GetComponent<Vehicle>().vehileName;
	}
}
