using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	private int stageCollectedCoin = 0;
	public int TotalLifeBags;
	public Text goldText;
	public static GameObject[] prisonersToPlay;

	// Update is called once per frame
	void Start () {
		goldText.text = "Golds: " + stageCollectedCoin;
	}
	
	public void AddCoin(int goldCoinToAdd){
		stageCollectedCoin += goldCoinToAdd;
		goldText.text = "Golds: " + stageCollectedCoin;
	}

	public int GetCollectedCoin(){ 
		return stageCollectedCoin;
	}
}
