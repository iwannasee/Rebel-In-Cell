using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour {
	public List<Item> items;
	public int rewardGold;
	private bool looted = false;


	void Start(){
		DontDestroyOnLoad(gameObject);
	}

	public void SetHadLooted(){
		looted = true;
	}
	public bool GetHadLooted(){
		return looted;
	}

	public void DestroyReward(){
		DestroyImmediate(gameObject,true);
	}

	public int GetRewardGold(){
		return rewardGold;
	}

	public void AddToUltimateReward(Item rewardToAdd){
		print("number of items = " + items.Count + ". " + rewardToAdd.itemName + " has been added to ulti reward.");
		items.Add(rewardToAdd);
	}

	public List<Item> GetItemsInReward(){
		return items;
	}
}

