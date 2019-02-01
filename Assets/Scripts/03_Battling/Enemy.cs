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
		enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		enemyCount--;
		//EXTENDABLE if this wave have Enemy regen, check the isRegen bool of Enemy container 
		//Destroy whole Enemy container if this is the last Enemy
		if(enemyCount<=0){
			if(GetComponent<ItemDropper>()){
				GetComponent<ItemDropper>().DropItem();
			}
			Destroy(transform.parent.gameObject);
		}
		if(GetComponent<ItemDropper>()){
			GetComponent<ItemDropper>().DropItem();
		}
		isDead = true;

	}
	//---------------------------------------------------------------

	void Update(){
		if(isDead){
			stampScale -= Time.deltaTime;
			transform.localScale = new Vector3(stampScale,1,1);
			if(stampScale <= 0){
				Destroy(gameObject);
				GameObject waveController = GameObject.FindGameObjectWithTag("Wave Controller");
				waveController.GetComponent<EnemyWaveController>().ClearPresentWave();
			}
		}
	}

}
