using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour {
	public int coinValue;
	public float fallingSpeed;

	void Update(){
		transform.Translate (Vector3.down * Time.deltaTime * fallingSpeed);
	}

	public void CoinDestroy(){
		Destroy(gameObject);
	}

	public int GetGoldCoinValue(){
		return coinValue;
	}
}
