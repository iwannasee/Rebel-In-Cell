﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VehicleAvarToSelect : MonoBehaviour {

	public GameObject vehiclePref;
	public Sprite frontImg;
	public Sprite backImg;
	private GameObject readyList;
	private bool selected = false;
	// Use this for initialization
	void Start () {
		readyList = GameObject.FindGameObjectWithTag("Ready List");
	} 

	public void AddToReadyList(){
		if(!CharacterScroller.GetIsDragging() && !selected){
			//Add to the prisoner ready list
			readyList.GetComponent<ReadyList>().AddVehicleImage(backImg,frontImg);
			readyList.GetComponent<ReadyList>().SetVehicleWillPlay(vehiclePref);
			//And hide the plus btn on this object to inactivate it
			UnSelectableEffect();
			 
		}
	}  


	private void UnSelectableEffect(){
		//Disable plus mark image
		transform.GetChild(0).GetComponent<Image>().enabled = false;
		//darken the privimage color 
		Image mainPrisonerImg = GetComponent<Image>();
		mainPrisonerImg.color = new Color(0.5f,0.5f,0.5f,1f);

		selected = true;

	}

	public void SelectableEffect(){
		//Enable plus mark Image
		transform.GetChild(0).GetComponent<Image>().enabled = true;
		//restore the image color 
		Image mainPrisonerImg = transform.GetComponent<Image>();
		mainPrisonerImg.color = new Color(1f,1f,1f,1f);

		selected = false;
	}

	public string GetVehiclePrefabName(){
		return vehiclePref.GetComponent<Vehicle>().vehileName;
	}
}
