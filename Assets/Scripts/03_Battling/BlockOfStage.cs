using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOfStage : MonoBehaviour {

	private int WaveBlockCount;
	private GameObject ItemContainer;
	private bool isDead = false;
	private float stampScale = 1;
	void Update(){
		if(isDead){
			stampScale -= Time.deltaTime*2;
			transform.localScale = new Vector3(stampScale,1,1);
			if(stampScale <= 0){
				Destroy(gameObject);
			}
		}
	}
	//---------------------------------------------------------------
	public void BlockDestroy(){
		if(GetComponent<BoxCollider2D>()){
			GetComponent<BoxCollider2D>().enabled = false;
		}else if(GetComponent<BoxCollider2D>()){
			GetComponent<BoxCollider2D>().enabled = false;
		}else if(GetComponent<CapsuleCollider2D>()){
			GetComponent<CapsuleCollider2D>().enabled = false;
		}
		isDead = true;
		/*
		//Find how many block in this current wave
		WaveBlockCount = GameObject.FindObjectsOfType<BlockOfStage>().Length;
		WaveBlockCount--;
		//EXTENDABLE if this wave have block regen, check the isRegen bool of block container 
		//Destroy whole block container if this is the last block

		if(WaveBlockCount<=0){
			if(GetComponent<ItemDropper>()){
				GetComponent<ItemDropper>().DropItem();
			}
			Destroy(transform.parent.gameObject);
		}
		Destroy(gameObject);
		*/
		if(GetComponent<ItemDropper>()){
			GetComponent<ItemDropper>().DropItem();
		}


	}

	public void DestroyBlockToEnterNextWave(){
		Instantiate(GetComponent<Health>().DieEffectPrefab, transform.position, Quaternion.identity);
		isDead = true;
	}
}

