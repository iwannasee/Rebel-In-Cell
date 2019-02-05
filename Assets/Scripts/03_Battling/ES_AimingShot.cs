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
	public GameObject[] enemyShots;
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
	public bool bCannotAttack = false;
	public bool bLookAtTargetWhileShooting = false;
	private float randomTimeShotExplode;
	public float delayForGenerateOtherShot;

	enum PLAYABLE_SKILL{
		FIRE,
		HEAL,
		BOMBBARD,
		NO_SKILL
	}

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
			if(delayForGenerateOtherShot <= 0){
				randomTimeShotExplode = Random.Range(0.8f,1.8f);
			}else{
				randomTimeShotExplode = delayForGenerateOtherShot;
			}
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
				PLAYABLE_SKILL skillToPlayThisTurn;

				if(bulletCount > 0){
					skillToPlayThisTurn = PLAYABLE_SKILL.FIRE;
				}else{
					skillToPlayThisTurn = GetPlaySkills();
				}

				switch(skillToPlayThisTurn){

					case PLAYABLE_SKILL.FIRE:
						if(numberOfShotsPerLaunch <= 1){
							Fire();

							actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
							shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
						}else if(numberOfShotsPerLaunch >1){

							if(bulletCount >= numberOfShotsPerLaunch){
								actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
								shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
								bulletCount = 0;
								break; //TODO check this 
							}

							intervalPerBullets -= Time.deltaTime;
							if(intervalPerBullets <= 0){
								Fire();
								bulletCount++;
								intervalPerBullets = maxBulletInterval;
							}
						}
						break;

					case PLAYABLE_SKILL.HEAL:
						GetComponent<EnemyHealingSkill>().Heal(shotPower);
						actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
						shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
						break;

					case PLAYABLE_SKILL.BOMBBARD:
						GetComponent<EnemyBombardSkill>().Bombbard();
						actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
						shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown + 5;
						break;

					default:
						break;
					}
			}	
		}
	}

	PLAYABLE_SKILL GetPlaySkills(){
		List <PLAYABLE_SKILL> playableSkills = new List<PLAYABLE_SKILL>();

		if(GetComponent<EnemyBombardSkill>() != null){
			playableSkills.Add(PLAYABLE_SKILL.BOMBBARD);
		}
		if(GetComponent<EnemyHealingSkill>() != null){
			playableSkills.Add(PLAYABLE_SKILL.HEAL);
		}
		if(!bCannotAttack){
			playableSkills.Add(PLAYABLE_SKILL.FIRE);
		}
		if(playableSkills.Count <= 0){
			return PLAYABLE_SKILL.NO_SKILL;
		}
		return playableSkills[Random.Range(0, playableSkills.Count)];
	}

	void Fire(){
		if (!enemyShotContainer) {
			return;
		}
		//Guard code
		if(Prisoners.Length<=0){
			Debug.Log("prisoners are not found. now refind the prisoners");
			Prisoners = GameObject.FindGameObjectsWithTag("Prisoner");
		}
		//Get a random number to select a prisoner target
		int rdNum = Random.Range (0, Prisoners.Length);

		if(bLookAtTargetWhileShooting){
			//If this enemy lock focus on prisoner while shooting
			//transform.rotation = Quaternion.LookRotation(Vector3.zero,Vector3.zero);
			Vector3 difference = Prisoners[rdNum].transform.position - transform.position;
			float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90 + rotationZ);
		}

		//Generate a Shot
		GameObject shot = Instantiate (enemyShots[Random.Range(0, enemyShots.Length)], ShootSpawner.transform.position, Quaternion.identity) as GameObject;
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
