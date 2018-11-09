using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoot : MonoBehaviour {
	//dropability from low to high
	private Item exactItemToGet;
	public float fallingSpeed;
	
	// Update is called once per frame
	void Update(){
		transform.Translate (Vector3.down * Time.deltaTime * fallingSpeed);
	}

	public void SetItemToDrop(Item itemToSet){
		exactItemToGet = itemToSet;
	}

	public Item GetDroppedItem(){
		return exactItemToGet;
	}

	public void ItemLootDestroyed(){
		Destroy(gameObject);
	}
}
