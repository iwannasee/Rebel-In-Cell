using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_AimingShot : MonoBehaviour {
	public int numberOfShotsPerLaunch;
	public float maxBulletInterval;
	public float shotSpeed;
	[Tooltip("this property is not implemented yet")]
    public int shotPower; //TODO this property is not implemented yet
	public float randomCoolDownTweak;
	public float maxCoolDownTime;
	public GameObject enemyShot;
	public GameObject ShootSpawner;

	private Vector3 shotTarget;
	private float actualRandomCoolDown;
	private float intervalPerBullets;
	private int bulletCount;
	private float shotCoolDownTime;
	private GameObject enemyShotContainer;
	private GameObject[] Prisoners;

	public bool bIsStraightShot = false;
	public bool bCanGenerateOtherShot = false;
	private float randomTimeShotExplode;

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

		if(GetComponent<EnemyShot>() && bCanGenerateOtherShot){
			ShootSpawner = this.gameObject;
			randomTimeShotExplode = Random.Range(0.8f,1.8f);
		}
	}
		
	// Update is called once per frame
	void Update () {
		//If the aiming shot is launched from straight shot
		if(GetComponent<EnemyShot>() && bCanGenerateOtherShot){
			randomTimeShotExplode -= Time.deltaTime;
			if(randomTimeShotExplode <= 0){
				Fire();

				//TODO add explosion effect
				GameObject explodeEffect = GetComponent<EnemyShot>().explodeParticlePref;
				Instantiate(explodeEffect, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
			return;
		}

		//If the aiming shot is launched from enemy
		if(EnemyWaveController.GetWaveHasStarted()){
			shotCoolDownTime -= Time.deltaTime;
			if(shotCoolDownTime<= 0f && !Prisoner.GetAllPrisonerDead()){
				if(GetComponent<EnemyHealingSkill>() && (bulletCount <= 0)){
					
					float randomToss = Random.value;
					if(randomToss >5){
						GetComponent<EnemyHealingSkill>().Heal(shotPower);
						actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
						shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
						return;
					}
				}
				
				if(numberOfShotsPerLaunch <= 1){
					Fire();
					actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
					shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
				}else if(numberOfShotsPerLaunch >1){
					if(bulletCount >= numberOfShotsPerLaunch){
						actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
						shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
						bulletCount = 0;
						return;
					}

					intervalPerBullets -= Time.deltaTime;
					if(intervalPerBullets <= 0){
						Fire();
						bulletCount++;
						intervalPerBullets = maxBulletInterval;
					}
				}
			}	
		}
	}

	void Fire(){
		if (!enemyShotContainer) {
			return;
		}

		//Generate a Shot
		GameObject shot = Instantiate (enemyShot, ShootSpawner.transform.position, Quaternion.identity) as GameObject;
		if(shot.GetComponent<RadiantDamage>()){
			shot.GetComponent<RadiantDamage>().SetDamage(shotPower);
		}

		shot.transform.parent = enemyShotContainer.transform;
		shot.GetComponent<EnemyShot>().SetTheEnemyWhoShot(this.GetComponent<Enemy>());

		Rigidbody2D shotRgBody = shot.GetComponent<Rigidbody2D> ();

		if(bIsStraightShot){
			//Actual Shot
			shotRgBody.velocity = Vector3.down * shotSpeed;
			return;
		}

		if(Prisoners.Length<=0){
			Debug.Log("prisoners are not found. now refind the prisoners");
			Prisoners = GameObject.FindGameObjectsWithTag("Prisoner");
		}

		//Get a random target for the shot
		int rdNum = Random.Range (0, Prisoners.Length);
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
