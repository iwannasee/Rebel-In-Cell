using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBaseAvarList : MonoBehaviour {
	[Tooltip("the list of total playable Bases of the whole game")]
	public GameObject[] totalBasesAvars;

	[Tooltip("the GameObject for holding playable Base avar list")]
	public GameObject basesListFrame;

	//the list of actual playable bases
	private GameObject[] baseAvar;



	// Use this for initialization
	void Start () {
		FilterAvailableBases();

		for(int i = 0; i <baseAvar.Length; i++){
			print( "baseAvar " + baseAvar[i].name);
			GameObject thisAvar = Instantiate(baseAvar[i]) as GameObject; 
			thisAvar.transform.SetParent(basesListFrame.transform, false) ;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	 
	private void FilterAvailableBases(){
		int dataTotalNumberOfVehs = PlayerProgress.playerData.availableVehicles.Count; 
		print("dataTotalNumberOfVehs " + dataTotalNumberOfVehs);
		baseAvar = new GameObject[dataTotalNumberOfVehs]; 
		for (int i = 0; i < dataTotalNumberOfVehs; i++){  
			string nameInData = PlayerProgress.playerData.availableVehicles[i]; 
			for(int j = 0; j < totalBasesAvars.Length; j ++){  
				string nameInList = totalBasesAvars[j].GetComponent<Selection_DefenceBaseAvar>().GetBasePrefabName();
				if(nameInData == nameInList){ 
					baseAvar[i] = totalBasesAvars[j];
				}
			} 
		}
	}

	public void ShowAvailableSeats(){ 
		//Get current displaying base
		int currentBaseIndex = GetComponent<ScrollSnapRectOriginal>().GetCurrentPage();
		Transform baseListTransform = transform.GetChild(0);

		//Get position(s) of available seat

		//Show arrows to click to get character on seat
	}
} 
