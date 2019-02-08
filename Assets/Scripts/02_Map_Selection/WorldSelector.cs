using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldSelector : MonoBehaviour {
	//TODO now if open any Map Select WORLD scene,the Meadow title and its maps will be load.
	// Use this for initialization
	void Start () {
		//int selectableWorldNum = PlayerProgress.playerData.maxReachableWorld;
		int currentWorldNum =PlayerProgress.presentWorldIndex;
		for(int i = 0; i < transform.childCount; i++){
			Text worldText = transform.GetChild(i).GetComponentInChildren<Text>();
			worldText.text = MapDictionary.worldList[i];
		}

	}



}
