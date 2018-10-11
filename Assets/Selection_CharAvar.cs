﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection_CharAvar : MonoBehaviour {
	public GameObject prisonerPref;
	public string charInfoText;
	private Sprite thisPrisonerSprt;
	private bool bIslocked = true;


	private CharacterInfoScreen charInfo;

	// Use this for initialization
	void Start () {
		thisPrisonerSprt = transform.GetChild(0).GetComponent<Image>().sprite;
		charInfo = FindAndGetCharInfo();
	}

	public CharacterInfoScreen FindAndGetCharInfo(){
		Transform charPanel = transform.parent.parent;
		return charPanel.GetComponentInChildren<CharacterInfoScreen>();
	}

	public string GetPrisonerPrefabName(){
		return prisonerPref.GetComponent<Prisoner>().prisonerName;
	}

	public void CharacterIsSelecting(){
		if(bIslocked){return;}

		charInfo.SetOnScrnCharSprt(thisPrisonerSprt);
		charInfo.SetInfoTextOfSelectedChar(charInfoText);
		charInfo.SetCharNameOnScreen(GetPrisonerPrefabName());	
		//TODO update equipment and skill 
	}

	public void UnlockCharacterSelection(){
		bIslocked = false;
	}

    public GameObject GetCharPrefabOfThisAvar()
    {
        return prisonerPref;
    }
}
