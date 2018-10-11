using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	public void ShowAvailableSlotsInBase(){ 
		//Get position(s) of available slots 
		CharacterSlot[] availableSlots = GetCharSlotOfCurrentBase();

		//Show arrows to click to get character on seat
		foreach (CharacterSlot thisSlot in availableSlots){
			//if there is a character in this slot, dont show anything
			if(thisSlot.GetComponent<Image>().sprite != null){
				continue;
			}
			//else, show the arrow
			else{
				thisSlot.ShowArrow();
			}
		}
	}

	public void RefreshBase(){
		print("refresh arrow");
		CharacterSlot[] availableSlots = GetCharSlotOfCurrentBase();
		//Show arrows to click to get character on seat
		foreach (CharacterSlot thisSlot in availableSlots){
			//Hide arrow
			thisSlot.HideArrow();
			//if there is a character in this slot, remove it
			if(thisSlot.GetComponent<Image>().sprite != null){
				thisSlot.GetComponent<Image>().sprite = null;
			}
		}
	}


	private CharacterSlot[] GetCharSlotOfCurrentBase(){
		//Get current displaying base
		int currentBaseIndex = GetComponent<ScrollSnapRectOriginal>().GetCurrentPage();
		Transform baseListTransform = transform.GetChild(0);
		Transform currentDisplayingBase = baseListTransform.GetChild(currentBaseIndex);

		//Get position(s) of available slots 
		CharacterSlot[] availableSlots = currentDisplayingBase.GetComponentsInChildren<CharacterSlot>();
		return availableSlots;
	}
} 
