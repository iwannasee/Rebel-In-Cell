using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardLooter : MonoBehaviour {
	private List<Item> items;
	private List<Item> newItems;
	private List<Item> alreadyExistItems;
	/// <summary>
	/// The is winner for test. Delete this later
	/// </summary>
	public bool isWinnerForTest;

	private Reward reward;
	private int acquiredGold;
	private string[] acquiredChars;
	private string[] acquiredVehs;
	private Sprite[] acquiredCharSprts;
	private Sprite[] acquiredVehSprts;
	// Use this for initialization
	void Start () {
		items = new List<Item>();
		newItems = new List<Item>();
		alreadyExistItems = new List<Item>();

		WinLoseCondition.SetIsWinner(isWinnerForTest);
		//Check whether the reward loot exists or not
		reward = FindObjectOfType<Reward>();
		if(!reward){
			Debug.LogError("Cannot find reward Object to loot");
			return;
		}
		//If there is , then check if game won or lost
		if(!WinLoseCondition.GetIsWinner()){
			Debug.Log("Well. Look like you lost. you cannot loot the reward");
			return;
		}
		//if you won, then check whether it has been looted or not
		if(reward.GetHadLooted()){
			Debug.LogError("You completed this map before. Only gold could be collected");
			acquiredGold = reward.GetRewardGold();
			PlayerProgress.playerData.gold += acquiredGold;
			return;
		}

		//If has ever not been looted
		acquiredGold = reward.GetRewardGold();
		PlayerProgress.playerData.gold += reward.GetRewardGold();

		List<Item> itemsInRewardLoot = reward.GetItemsInReward();

		if(itemsInRewardLoot != null){
			for (int i = 0; i < itemsInRewardLoot.Count; i++){
				switch (itemsInRewardLoot[i].itemType){
					case Item.TYPE.CHARACTER:
						if(PlayerProgress.playerData.availableCharacters.Contains(itemsInRewardLoot[i].itemName)){
							Debug.Log("This character is already in your group. You will receive gold instead.");
							itemsInRewardLoot[i].isAlreadyInStock = true;
							PlayerProgress.playerData.gold += itemsInRewardLoot[i].itemValue;
							alreadyExistItems.Add(itemsInRewardLoot[i]);
							items.Add(itemsInRewardLoot[i]);
						}else if(CommonData.charNameList.Contains(itemsInRewardLoot[i].itemName)) {
							Debug.Log(itemsInRewardLoot[i].itemName + " is found in datatabase");
							newItems.Add(itemsInRewardLoot[i]);
							items.Add(itemsInRewardLoot[i]);
							PlayerProgress.playerData.availableCharacters.Add(itemsInRewardLoot[i].itemName);
							//Do stuff here
						}else{
							Debug.Log("this prisoner name :" + itemsInRewardLoot[i].itemName + " is not existing in the database");
						}
						break;
					case Item.TYPE.VEHICLE:
						if(PlayerProgress.playerData.availableVehicles.Contains(itemsInRewardLoot[i].itemName)){
							Debug.Log("This vehicle is already in your group. You will receive gold instead.");
							itemsInRewardLoot[i].isAlreadyInStock = true;
							PlayerProgress.playerData.gold += itemsInRewardLoot[i].itemValue;
							alreadyExistItems.Add(itemsInRewardLoot[i]);
							items.Add(itemsInRewardLoot[i]);
						}else if(CommonData.vehicleList.Contains(itemsInRewardLoot[i].itemName)) {
							Debug.Log(itemsInRewardLoot[i].itemName + " is found in datatabase");
							PlayerProgress.playerData.availableVehicles.Add(itemsInRewardLoot[i].itemName);
							newItems.Add(itemsInRewardLoot[i]);
							items.Add(itemsInRewardLoot[i]);
							//Do stuff here
						}else{
							Debug.Log("this vehicle name :" + itemsInRewardLoot[i].itemName + " is not existing in the database");
						}
						break;
					case Item.TYPE.SHOT:

						break;
					case Item.TYPE.MAP:
						break;
					case Item.TYPE.GOLD:
						PlayerProgress.playerData.gold += itemsInRewardLoot[i].itemValue;
						items.Add(itemsInRewardLoot[i]);
						break;
					default: break;
				}
			}
		}
		SaveLoadSystem.SaveGame(PlayerProgress.playerData);
	}


	public int GetAcquiredGold(){
		return acquiredGold;
	}

	public List<Item> GetNewItemList(){
		return newItems;
	}

	public List<Item> GetAlreadyExistItemList(){
		return alreadyExistItems;
	}

	public List<Item> GetItemList(){
		return items;
	}


}
