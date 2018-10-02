using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[Tooltip("Assign sprites from healthy to weak, following the health")]
	public Sprite[] sprites;
	[Tooltip("needs to equal or greater than number of sprites. should be the product of sprites length")]
	public int maxHealth;

	public GameObject sodierShot;
	public GameObject sentryShooter;
	public float maxCoolDownTime;
	public float randomCoolDownTweak;

	private int enemyCount;
	private float shotCoolDownTime;
	private GameObject enemyShotContainer;
	private GameObject[] Prisoners;
	private Vector3 shotTarget;
	private float actualRandomCoolDown;
	private int health;

	private int lostHeathToChangeSprite;

	//---------------------------------------------------------------
	void Start () {
		health = maxHealth;

		int numberOfSprites = sprites.Length;
		if(numberOfSprites > 0 && maxHealth >= numberOfSprites){
			lostHeathToChangeSprite = maxHealth / numberOfSprites;
		}

		actualRandomCoolDown = Random.Range(0f, randomCoolDownTweak);
		shotCoolDownTime = maxCoolDownTime + actualRandomCoolDown;
		//Create container of Enemy shots
		if(!GameObject.Find("Enemy Shot Container")){
			enemyShotContainer = new GameObject("Enemy Shot Container");
			enemyShotContainer.AddComponent<ClearUpChidren>();
		}else{
			enemyShotContainer = GameObject.Find("Enemy Shot Container");
		}

		Prisoners = GameObject.FindGameObjectsWithTag("Prisoner");
	}
		
	//---------------------------------------------------------------
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

	//---------------------------------------------------------------
	void Fire(){
		if (!enemyShotContainer) {
			return;
		}
		if(Prisoners.Length<=0){
			Debug.Log("prisoners are not found. now refind the prisoners");
			Prisoners = GameObject.FindGameObjectsWithTag("Prisoner");
		}
		//Generate a Shot
		GameObject shot = Instantiate (sodierShot, sentryShooter.transform.position, Quaternion.identity) as GameObject;
		shot.transform.parent = enemyShotContainer.transform;
		//Get a random target for the shot

		int rdNum = Random.Range (0, Prisoners.Length);
		Rigidbody2D shotRgBody = shot.GetComponent<Rigidbody2D> ();
		//Set the direction to aim the shot
		if(Prisoners.Length > 0){
			Vector3 dir = (Prisoners [rdNum].transform.position - transform.position).normalized;
			//Actual Shot
			shotRgBody.velocity = dir * shot.GetComponent<AimingShot> ().speed;
			print(Prisoners[rdNum].name + " " + shotRgBody.velocity);
		} else{
			print("no prisoner found");
		}
	}
	//---------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.GetComponent<RadiantDamage> ()) {
			//TODO add quake effect 
			RadiantDamage radiantDamage = collision.gameObject.GetComponent<RadiantDamage>();
			int inflictedDamage = radiantDamage.GetDamage();
			health = health - inflictedDamage;

			if (health <= 0) {
				EnemyDestroy ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.GetComponent<RadiantDamage> ()){
			print("collider.gameObject " + collider.name);
			RadiantDamage radiantDamage = collider.GetComponent<RadiantDamage>();
			int inflictedDamage = radiantDamage.GetDamage();
			health = health - inflictedDamage;

			if (health <= 0) {
				EnemyDestroy ();
				return;
			}

			if(lostHeathToChangeSprite != 0){
				ChangeSpriteFollowHealth();
			}
		}
	}

	//---------------------------------------------------------------
	void EnemyDestroy(){
		enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		enemyCount--;
		//EXTENDABLE if this wave have Enemy regen, check the isRegen bool of Enemy container 
		//Destroy whole Enemy container if this is the last Enemy
		if(enemyCount<=0){
			GameObject waveController = GameObject.FindGameObjectWithTag("Wave Controller");
			waveController.GetComponent<EnemyWaveController>().ClearPresentWave();
			Destroy(transform.parent.gameObject);
		}
		Destroy(gameObject);
	}
	//---------------------------------------------------------------

	private void ChangeSpriteFollowHealth() {
		if((health % lostHeathToChangeSprite) == 0){
			SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
			int spriteIndex = health/ lostHeathToChangeSprite;
			spriteRenderer.sprite = sprites[spriteIndex];
		}
	}
}
