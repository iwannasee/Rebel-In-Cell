using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private int enemyCount;
	private int lostHeathToChangeSprite;
	private float stampScale = 1;
	private bool isDead = false;
	//---------------------------------------------------------------
	public void EnemyDestroy(){
		enemyCount = transform.parent.GetComponentsInChildren<Enemy>().Length;
		enemyCount--;
		//EXTENDABLE if this wave have Enemy regen, check the isRegen bool of Enemy container 
		//Destroy whole Enemy container if this is the last Enemy
		if(enemyCount<=0){
			if(GetComponent<ItemDropper>()){
				GetComponent<ItemDropper>().DropItem();
			}
			//Destroy all blocks
			Transform BlockContainer = transform.parent.parent.GetChild(0);
			BlockContainer.SetParent(null);

			for(int i = 0; i < BlockContainer.childCount; i++){
				BlockOfStage block = BlockContainer.GetChild(i).GetComponent<BlockOfStage>();
				if(block){
					block.DestroyBlockToEnterNextWave();
				}
			}
			//Detach the parent
			transform.SetParent(null);

			GameObject waveController = GameObject.FindGameObjectWithTag("Wave Controller");
			waveController.GetComponent<EnemyWaveController>().ClearPresentWave();

			//Destroy(transform.parent.gameObject);
		}

		if(GetComponent<ItemDropper>()){
			GetComponent<ItemDropper>().DropItem();
		}

		isDead = true;
		if(GetComponent<BoxCollider2D>()){
			GetComponent<BoxCollider2D>().enabled = false;
		}else if(GetComponent<BoxCollider2D>()){
			GetComponent<BoxCollider2D>().enabled = false;
		}else if(GetComponent<CapsuleCollider2D>()){
			GetComponent<CapsuleCollider2D>().enabled = false;
		}
	}
	//---------------------------------------------------------------

	void Update(){
		if(isDead){
			stampScale -= Time.deltaTime;
			transform.localScale = new Vector3(stampScale,1,1);
			if(stampScale <= 0){
				Destroy(gameObject);
			}
		}
	}

}
