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


    private static GameObject baseGameObjectToPlay = null;
    private static GameObject[] charactersToPlay;
    // Use this for initialization
    void Start () {
		FilterAvailableBases();

		for(int i = 0; i <baseAvar.Length; i++){
			GameObject thisAvar = Instantiate(baseAvar[i]) as GameObject; 
			thisAvar.transform.SetParent(basesListFrame.transform, false) ;
		}

        //renew this array every time the selection scene start. the number [1] is just a random number
        charactersToPlay = new GameObject[1];
    }
	 
	private void FilterAvailableBases(){
		int dataTotalNumberOfVehs = PlayerProgress.playerData.availableVehicles.Count; 
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

	//Used By button /event trigger
	public void ShowAvailableSlotsInBase(){ 

		//Get position(s) of available slots 
		CharacterSlot[] availableSlots = GetCharSlotOfCurrentBase();

		//Show arrows to click to get character on seat
		foreach (CharacterSlot thisSlot in availableSlots){
			thisSlot.ShowArrow();
		}
	}

    public void HideAllPoppingArrows()
    {
        CharacterSlot[] slots = GetCharSlotOfCurrentBase();
        foreach(CharacterSlot slot in slots)
        {
            slot.HideArrow();
        }
    }

	public void RefreshBase(){
		CharacterSlot[] availableSlots = GetCharSlotOfCurrentBase();
		//Show arrows to click to get character on seat
		foreach (CharacterSlot thisSlot in availableSlots){
			//Hide arrow
			thisSlot.HideArrow();
			//if there is a character in this slot, remove it
			if(thisSlot.GetCharacterPrefabInThisSlot()){
				thisSlot.GetComponent<Image>().sprite = null;
			}
		}

	}


	private CharacterSlot[] GetCharSlotOfCurrentBase(){
		//Get current displaying base
        Transform currentDisplayingBase = GetCurrentBase();

        //Get position(s) of available slots 
        CharacterSlot[] availableSlots = currentDisplayingBase.GetComponentsInChildren<CharacterSlot>();
		return availableSlots;
	}

    private Transform GetCurrentBase()
    {
        int currentBaseIndex = GetComponent<ScrollSnapRectOriginal>().GetCurrentPage();
        Transform baseListTransform = transform.GetChild(0);
        return baseListTransform.GetChild(currentBaseIndex);
    }

    
    public void GoTheBattle()
    {
        //Get the current base
        Transform currentBaseAvar = GetCurrentBase();

        //Set char prefabs to go to battle
        CharacterSlot[] charSlots = GetCharSlotOfCurrentBase();
        charactersToPlay = new GameObject[charSlots.Length];
        for (int i = 0; i < charSlots.Length; i++)
        {
            charactersToPlay[i] = null;
            //ReadyCharAvar readyChar = thisPrisoner.GetComponent<ReadyCharAvar>();
            charactersToPlay[i] = charSlots[i].GetCharacterPrefabInThisSlot();
            if (charactersToPlay[i])
            {
                Debug.Log("Playable Char " + charactersToPlay[i].name);
            }
        }

        // if no character selected in the slot, ignore go to battle
        int actualPrisonerCount = 0;
        for (int i = 0; i < charactersToPlay.Length; i++)
        {
            if (charactersToPlay[i] != null)
            {
                actualPrisonerCount++;
            }
        }
        if (actualPrisonerCount <= 0)
        {
            //TODO display warning message here	
            Debug.Log("actualPrisonerCount " + actualPrisonerCount + " ->You have to select at least one Prisoner");
            return;
        }

        //Set base prefab to go to battle
        Selection_DefenceBaseAvar defenceBaseAvar = currentBaseAvar.GetComponent<Selection_DefenceBaseAvar>();
        baseGameObjectToPlay = defenceBaseAvar.GetBasePrefabToPlay();

        //only start battle if there is at least one character selected and base selected
        if (baseGameObjectToPlay)
        {
            string mapToPlay = GameObject.FindObjectOfType<MapSelectManager>().GetSelectedMap();
            int currentWorld = PlayerProgress.presentWorldIndex;
            LevelManager.SLoadLevel("03 World " + currentWorld + " " + mapToPlay);
        }
        else
        {
            //TODO display warning message here
            Debug.Log("You have to select at least one Vehicle");
        }

    }

    public static GameObject[] GetCharactersToPlay()
    {
        return charactersToPlay;
    }

    public static GameObject GetBaseGameObjectToPlay()
    {
        return baseGameObjectToPlay;
    }
} 
