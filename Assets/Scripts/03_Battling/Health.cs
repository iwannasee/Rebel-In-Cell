using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	[Tooltip("needs to equal or greater than number of sprites. should be the product of sprites length")]
	public int maxHealth;

	[Tooltip("sprite index from weak to healthy")]
	public Sprite[] heathStatusSprites;

	public Sprite hitImg;
    public Sprite deadImg;

	public AudioClip hitClip;
    public AudioClip dieClip;

	public float maxHitImgInterval = 0.2f;

	private float hitImgInterval;
	public int health; 
	private int lostHeathToChangeSprite;
	private HealthBar healthBar;

	private SpriteRenderer spriteRenderer;
	private AudioSource audioSource;
	//---------------------------------------------------------------
	// Use this for initialization
	//---------------------------------------------------------------
	void Start () {
		audioSource = GetComponent<AudioSource>();
		//Only active health bar function if the owner of this script is prisoner
		if(GetComponent<Prisoner>()){
			//link to health bar object , assume its index is 2 in transform hierachy
			healthBar = transform.parent.GetComponentInChildren<HealthBar>();
			if(!healthBar){
				Debug.Log("no health bar found");
			}
		}

		hitImgInterval = maxHitImgInterval;
		spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

		if(heathStatusSprites.Length > 0 && maxHealth >= heathStatusSprites.Length){
			lostHeathToChangeSprite = maxHealth / heathStatusSprites.Length;
		}
		health = maxHealth;
	}
	//---------------------------------------------------------------
	void OnTriggerEnter2D(Collider2D collider){
		if(GetComponent<Enemy>() || GetComponent<BlockOfStage>()){
			if(collider.GetComponent<RadiantDamage> ()){
				RadiantDamage radiantDamage = collider.GetComponent<RadiantDamage>();
				int inflictedDamage = radiantDamage.GetDamage();
				health = health - inflictedDamage;

				if(health <= 0){
					if(GetComponent<BlockOfStage>()){
						GetComponent<BlockOfStage>().BlockDestroy();
						return;
					}

					if(GetComponent<Enemy>()){ 
						GetComponent<Enemy>().EnemyDestroy();
						return;
					}
				}

				if(lostHeathToChangeSprite != 0){
					ChangeSpriteFollowHealth();
				}
			}
		}
	}
	//---------------------------------------------------------------
	void OnCollisionEnter2D (Collision2D collision)
	{
		if(health <=0){return;}

		GameObject collidingObject = collision.gameObject;
		if (collidingObject.GetComponent<RadiantDamage> ()) {
			RadiantDamage radiantDamage = collidingObject.GetComponent<RadiantDamage>();
			int inflictedDamage = radiantDamage.GetDamage();
			health = health - inflictedDamage;

	

			if(lostHeathToChangeSprite != 0){
				ChangeSpriteFollowHealth();
			}

			//Handle when health is <= 0
			if(health <= 0){
				//if this health is of block
				if(GetComponent<BlockOfStage>()){
					GetComponent<BlockOfStage>().BlockDestroy();
					return;
				}
				//if this health is of enemy
				if(GetComponent<Enemy>()){
					GetComponent<Enemy>().EnemyDestroy();
					return;
				}
			}

			//if this health is of prisoner
			if(GetComponent<Prisoner>()){
				SetHealthBar(); 
				StartCoroutine("PlayHitSpriteAndHitSound");
			}
		}
	}
	//---------------------------------------------------------------
	IEnumerator PlayHitSpriteAndHitSound() 
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hitImg;
        audioSource.clip = hitClip;
        audioSource.Play();

        yield return new WaitForSeconds(maxHitImgInterval);


		if (health <= 0 && (audioSource.clip!= dieClip))
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = deadImg;
            //Check the number of alive prisoners
			if(GetComponent<Prisoner>()){
                print("run this");
				GetComponent<Prisoner>().CheckAlivePrisonerLeft();
			}
			audioSource.clip = dieClip;
			audioSource.Play(); 
		}else if (lostHeathToChangeSprite != 0){ 
			ChangeSpriteFollowHealth(); 
		} 
    }
	//---------------------------------------------------------------
	//(for prisoner) set health bar based on remaining health
	private void SetHealthBar(){
		float healthScaleToSet = (float)health/(float)maxHealth;
		healthBar.SetHealthBarAccordingly(healthScaleToSet);
	}
	//---------------------------------------------------------------
	private void ChangeSpriteFollowHealth() {
		if((maxHealth % lostHeathToChangeSprite) == 0){
			int spriteIndex = health/ lostHeathToChangeSprite;
			spriteRenderer.sprite = heathStatusSprites[spriteIndex];
		}else{
			Debug.Log("Warning! the health point of " + this.gameObject.name + " is not set properly. Resulting in sprite reset after hit cannot be done");
		}
	}
	//---------------------------------------------------------------
	public int GetHealth(){
		return health;
	}
	//---------------------------------------------------------------
}
