using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour {
	public GameObject targetIndicatorPref;
	public GameObject mapPanel;
	public float mapRotateBackSpeed = 1;
	public float mapRotateSpeed = 1;
	Transform mapScroller;

	private ScrollSnapRect scrollSnapRect;
	private Quaternion originRotation = Quaternion.Euler(0,0,0);
	private bool bIsOnTarget = false;
	public string[] dotLocationNames;
	//---------------------------------------------------------------
	// Use this for initialization
	void Start () {
		mapScroller = mapPanel.transform.GetChild(0);
		scrollSnapRect = mapScroller.GetComponent<ScrollSnapRect>();

		dotLocationNames = new string[MapDictionary.GetThisWorldLocations().Length];
		//initialise name of each dot location based on the database map dictionary 
		if(transform.childCount != MapDictionary.GetThisWorldLocations().Length){
			Debug.LogError("the number of location dots is not equal to number of maps stored in database." +
			" Please make sure they are equal");
		}

		for(int i = 0 ; i< transform.childCount; i ++){
			dotLocationNames[i] = MapDictionary.GetThisWorldLocations()[i];
		}

		originRotation = transform.rotation;
		//ShowCorrespondIndicator();
		mapPanel.SetActive(false);

	}
	//---------------------------------------------------------------
	// Update is called once per frame 
	void Update () {
		//Stop rotate map when map panel display and rotation is different from original rot
		if ((mapPanel.activeInHierarchy == true) && (transform.rotation != originRotation)) {
			if(!bIsOnTarget){
					ShowCorrespondIndicator ();
			}
			//If the rotation of the map back nearly to the original rotation( within a threshold)
			if(transform.rotation.eulerAngles.z <= 0.3f){
				bIsOnTarget = true; // state that the indicator now is indicating
				return;
			}
			//If not, continue rotate map back to the original rot
			transform.rotation = Quaternion.Slerp (transform.rotation, originRotation, Time.deltaTime*mapRotateBackSpeed);
			return;

		// rotate map when no panel displayed 
		} else if ((mapPanel.activeInHierarchy == false) && targetIndicatorPref) {
			bIsOnTarget = false;
			targetIndicatorPref.SetActive (false);
		}

		RotateOverTime();
	}
	
	//---------------------------------------------------------------
	void RotateOverTime(){
		transform.Rotate(Vector3.forward, Time.deltaTime*mapRotateSpeed);
	}
	
	//---------------------------------------------------------------
	public void ShowCorrespondIndicator(){
		if(scrollSnapRect.GetMapSetUpEnd() ){
			targetIndicatorPref.SetActive(false);

			string currentMapName = scrollSnapRect.GetCurrentMapName();
			for(int dotNameIndex = 0; dotNameIndex < dotLocationNames.Length; dotNameIndex++){
				if(dotLocationNames[dotNameIndex] == currentMapName){
					targetIndicatorPref.SetActive(true);
					targetIndicatorPref.transform.position = transform.GetChild(dotNameIndex).position;
					return;
				}
			}
		}
	}
	//---------------------------------------------------------------
}
