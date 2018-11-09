using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOfStage : MonoBehaviour {

	private int WaveBlockCount;
	private GameObject ItemContainer;
	public GameObject DestroyEffectPrefab;

	//---------------------------------------------------------------
	public void BlockDestroy(){
		//Find how many block in this current wave
		WaveBlockCount = GameObject.FindObjectsOfType<BlockOfStage>().Length;
		WaveBlockCount--;
		//EXTENDABLE if this wave have block regen, check the isRegen bool of block container 
		//Destroy whole block container if this is the last block
		Instantiate(DestroyEffectPrefab, transform.position, Quaternion.identity);
		if(WaveBlockCount<=0){
			if(GetComponent<ItemDropper>()){
				GetComponent<ItemDropper>().DropItem();
			}
			Destroy(transform.parent.gameObject);
		}
		if(GetComponent<ItemDropper>()){
				GetComponent<ItemDropper>().DropItem();
			}
		Destroy(gameObject);
	}
}
