using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : MonoBehaviour {
	public float moveSpeed;
	public bool isPlayWithMouse;
	public GameObject inventoryGameObj;
	private Reward rewardToObtain;
	private Inventory inventory;
	private bool bIsConfused = false;
	private float confusionTime;
	private int paddleDirection = 1;
	private bool bCanMove = true;
	private float freezedTime;

	void Start(){
		inventory = inventoryGameObj.GetComponent<Inventory>();
		rewardToObtain = GameObject.FindGameObjectWithTag("Win Lose Condition").GetComponent<WinLoseCondition>().GetRewardWillBeObtained();
	}
	//---------------------------------------------------------------
	void Update () {
		if(!bCanMove){
			print("freezed now");
			freezedTime -= Time.deltaTime;
			if(freezedTime <= 0){
				GetComponent<SpriteRenderer>().color = Color.white;
				bCanMove = true;
			}
			return;
		}

		if(bIsConfused){
			confusionTime -= Time.deltaTime;
			paddleDirection = -1;
			if(confusionTime <= 0){
				GetComponent<SpriteRenderer>().color = Color.white;
				paddleDirection = 1;
				bIsConfused = false;
			}
		}
	//Can control paddle if game is not paused by skill shooting
		if (!Prisoner.GetIsCastingSkill() &&
			 (Time.timeScale == 1)) {
			if(isPlayWithMouse){
				PlayWithMouse ();
			}else{
				PlayWithArrow();
			}
		}
	}
	//---------------------------------------------------------------
	void PlayWithArrow(){
		Vector3 newPos = new Vector3 (transform.position.x + Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime, transform.position.y, 0);
		this.transform.position = newPos;
	}

	//---------------------------------------------------------------
	void PlayWithMouse(){
		float MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		Vector3 paddleShieldPos = new Vector3 (Mathf.Clamp (paddleDirection*MousePos, -2.65f, 2.65f), this.transform.position.y, 0f);
		this.transform.position = paddleShieldPos;
	}

	//---------------------------------------------------------------
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.GetComponent<GoldCoin>()) {
			int goldCoinVal = col.GetComponent<GoldCoin>().GetGoldCoinValue();
			inventory.AddCoin(goldCoinVal);
			col.GetComponent<GoldCoin>().CoinDestroy();
		}
		else if(col.GetComponent<ItemLoot>()){
			ItemLoot droppedLoot = col.GetComponent<ItemLoot>();
			Item itemInLoot = droppedLoot.GetDroppedItem();
			rewardToObtain.AddToUltimateReward(itemInLoot);
			droppedLoot.ItemLootDestroyed();
		}
	}

	public void SetBeingConfused(float timeBeingConfused){
		bIsConfused = true;
		confusionTime = timeBeingConfused;
		GetComponent<SpriteRenderer>().color = Color.black;
	}

	public void SetCannotMove(float timeBeingFreezed){
		print("freezzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz");
		bCanMove = false;
		freezedTime = timeBeingFreezed;
		GetComponent<SpriteRenderer>().color = Color.blue;
	}
}
