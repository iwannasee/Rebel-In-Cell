using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropper : MonoBehaviour {
	public GameObject CoinPref;
	public GameObject itemLootPref;
	public Item[] getableItems;

	public float rateForDropGold;

	public float rateForDropItem;
	//---------------------------------------------------------------
	public void DropItem(){
		float randomNumber = Random.value*100;

		if(randomNumber >= (100-rateForDropItem)){
			print("random number : "+ randomNumber);
			if(itemLootPref){
				GameObject drop = Instantiate(itemLootPref, transform.position, Quaternion.identity) as GameObject;
				drop.GetComponent<ItemLoot>().SetItemToDrop(GetDropItem());
			}
		}else if (randomNumber >= (100-rateForDropGold)){
			print("random number : "+ randomNumber);
			if(CoinPref){
				Instantiate(CoinPref, transform.position, Quaternion.identity);
			}
		}
	}
	//---------------------------------------------------------------
	private Item GetDropItem(){
		float total = 0;
		foreach (Item elem in getableItems) {
            total += elem.dropDate;
        }

		float randomPoint = Random.value * total;

		for (int i= 0; i < getableItems.Length; i++) {
			if (randomPoint < getableItems[i].dropDate) {
				return getableItems[i];
            }
            else {
				randomPoint -= getableItems[i].dropDate;
            }
        }
		return getableItems[getableItems.Length - 1];
	}
}
