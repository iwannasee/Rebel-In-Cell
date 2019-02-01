using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombardSkill : MonoBehaviour {
	public GameObject fallBombPref;
	public int releaseBombTimes;
	public float moveSpeed;
	public float shotSpeed;
	bool bCanBombbar = false;
	public Vector2 startPos;
	public Vector2 endPos;
	private Vector2 originalPos;
	public float acceptDistance;
	private float bombingRate;
	private bool bReachedStartPoint = false;
	private bool bFinishedBombing = false;
	private GameObject enemyShotContainer;
	// Use this for initialization
	void Start () {
		if(!GameObject.Find("Enemy Shot Container")){
			enemyShotContainer = new GameObject("Enemy Shot Container");
			enemyShotContainer.AddComponent<ClearUpChidren>();
		}else{
			enemyShotContainer = GameObject.Find("Enemy Shot Container");
		}

		originalPos = transform.position;
		bombingRate = ((Vector3.Distance(startPos,endPos))/releaseBombTimes);

	}

	void Update(){
		if(bCanBombbar){
			//If not reach the start point
			if(!bReachedStartPoint){
				//move to start point
				transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime*moveSpeed);
				//When reached within an acceptable radius, mark that "has reached"
				if(Vector3.Distance(transform.position, startPos) <= acceptDistance){
					bReachedStartPoint = true;
				}
			}
			//If reach the start point 
			else{
				//move to the end point
				transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime*moveSpeed);

				if(Vector3.Distance(transform.position, startPos) >= bombingRate){
					FireFromBomBing();
					bombingRate += bombingRate;
				}
				print("bombingRate " + bombingRate);
				// If reach the end point, come back to 
				if(Vector3.Distance(transform.position, endPos) <= acceptDistance){
					bombingRate = ((Vector3.Distance(startPos,endPos))/releaseBombTimes);
					bCanBombbar = false;
					bReachedStartPoint = false;
					bFinishedBombing = true;
				}
			}
		}

		if(bFinishedBombing){
			transform.position = Vector3.MoveTowards(transform.position, originalPos, Time.deltaTime*moveSpeed);
			if(Vector3.Distance(transform.position, originalPos) <= acceptDistance){
				bFinishedBombing = false;
			}
		}
	}

	public void Bombbard(){
	print(" now bomb");
		bCanBombbar = true;
	}

	void FireFromBomBing(){
		GameObject bomb = Instantiate(fallBombPref, transform.position, Quaternion.identity);
		bomb.transform.SetParent(enemyShotContainer.transform);
		bomb.GetComponent<Rigidbody2D>().velocity = Vector3.down * shotSpeed;
	}
}
