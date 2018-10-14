using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_AimingShot : MonoBehaviour {
	public float shotSpeed;
	public float randomCoolDownTweak;
	public float maxCoolDownTime;
	private float shotCoolDownTime;
	private GameObject enemyShotContainer;
	private GameObject[] Prisoners;
	public GameObject enemyShot;
	public GameObject ShootSpawner;
	private Vector3 shotTarget;
	private float actualRandomCoolDown;


	// Use this for initialization
	void Start () {
		actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
		shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;

		if(!GameObject.Find("Enemy Shot Container")){
			enemyShotContainer = new GameObject("Enemy Shot Container");
			enemyShotContainer.AddComponent<ClearUpChidren>();

		}else{
			enemyShotContainer = GameObject.Find("Enemy Shot Container");
		}

		Prisoners = GameObject.FindGameObjectsWithTag("Prisoner");
	}
	
	// Update is called once per frame
	void Update () {
		if(EnemyWaveController.GetWaveHasStarted()){
			shotCoolDownTime -= Time.deltaTime;
			if(shotCoolDownTime<= 0f && !Prisoner.GetAllPrisonerDead()){
				Fire();
				actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
				shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
			}	
		}
	}

	void Fire(){
		if (!enemyShotContainer) {
			return;
		}

		if(Prisoners.Length<=0){
			Debug.Log("prisoners are not found. now refind the prisoners");
			Prisoners = GameObject.FindGameObjectsWithTag("Prisoner");
		}
		//Generate a Shot
		GameObject shot = Instantiate (enemyShot, ShootSpawner.transform.position, Quaternion.identity) as GameObject;
		shot.transform.parent = enemyShotContainer.transform;
		//Get a random target for the shot

		int rdNum = Random.Range (0, Prisoners.Length);
		Rigidbody2D shotRgBody = shot.GetComponent<Rigidbody2D> ();
		//Set the direction to aim the shot
		if(Prisoners.Length > 0){
			Vector3 dir = (Prisoners [rdNum].transform.position - transform.position).normalized;
			//Actual Shot
			shotRgBody.velocity = dir * shotSpeed;
			//print(Prisoners[rdNum].name + " " + shotRgBody.velocity);
		} else{
			print("no prisoner found");
		}
	}
}
