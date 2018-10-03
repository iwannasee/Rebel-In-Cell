using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour {
	public string prisonerName;

	[Tooltip("needs to equal or greater than number of sprites. should be the product of sprites length")]
	public int maxHealth;
	public float maxHitImgInterval = 0.2f;
	[Tooltip("sprite index from weak to healthy")]
	public Sprite[] heathStatusSprites;
    public Sprite hitImg;
    public Sprite deadImg;

    public AudioClip hitClip;
    public AudioClip skillClip;
    public AudioClip dieClip;
 
	private static bool allPrisonerDead;

    private float hitImgInterval;
	private int health; 
	private int lostHeathToChangeSprite;
	private bool isDead = false;

	static private bool prisonerIsCastingSkill = false;

	private Prisoner[] prisonerArray;
	private AudioSource audioSource;
	private HealthBar healthBar;
	private SkillBar skillBar;
	private SpriteRenderer spriteRenderer;
	//---------------------------------------------------------------
	void Start(){
		audioSource = GetComponent<AudioSource>();
		//link to health bar object , assume its index is 2 in transform hierachy
		healthBar = transform.parent.GetChild(2).GetComponent<HealthBar>();
		skillBar = transform.parent.GetChild(3).GetComponent<SkillBar>();
		if(!healthBar || !skillBar){
			Debug.Log("no health or skill bar found");
		}
		hitImgInterval = maxHitImgInterval;
		spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = heathStatusSprites[heathStatusSprites.Length-1];
		if(heathStatusSprites.Length > 0 && maxHealth >= heathStatusSprites.Length){
			lostHeathToChangeSprite = maxHealth / heathStatusSprites.Length;
		}

		allPrisonerDead = false;

		health = maxHealth;

		prisonerArray = GameObject.FindObjectsOfType<Prisoner>();

	}
	//---------------------------------------------------------------
	void OnCollisionEnter2D (Collision2D collision)
	{
		if(health <=0){
			health = 0;
			SetHealthBar();
			return;
		}

		GameObject collidingObject = collision.gameObject;
		if (collidingObject.GetComponent<RadiantDamage> ()) {
			RadiantDamage radiantDamage = collision.gameObject.GetComponent<RadiantDamage>();
			int inflictedDamage = radiantDamage.GetDamage();
			health = health - inflictedDamage;
			SetHealthBar();
	          StartCoroutine("PlayHitSprite");
		}
	}

	private void SetHealthBar(){
		float healthScaleToSet = (float)health/(float)maxHealth;
		healthBar.SetHealthBarAccordingly(healthScaleToSet);
	}

	//TODO change name consistently to playhitspriteandsound
    IEnumerator PlayHitSprite() 
    {
		
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hitImg;
        audioSource.clip = hitClip;
        audioSource.Play();

        yield return new WaitForSeconds(maxHitImgInterval);
		
		if (health <= 0 && !isDead)
        {
            isDead = true;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = deadImg;
            //Check the number of alive prisoners
            CheckAlivePrisonerLeft();
			audioSource.clip = dieClip;
			audioSource.Play(); 
		}else if (lostHeathToChangeSprite != 0){
			ChangeSpriteFollowHealth();
		} 
    }

	private void CheckAlivePrisonerLeft(){
		int alivePrisonerNum = prisonerArray.Length;
		foreach(Prisoner thisPrisoner in prisonerArray){
			if(thisPrisoner.GetIsDead()){
				alivePrisonerNum--;
			}
		}
		if (alivePrisonerNum <=0){
			allPrisonerDead = true;
			//as all the prisoners are down, set the game is lost
		 	WinLoseCondition wlCondition=GameObject.FindGameObjectWithTag("Win Lose Condition").GetComponent<WinLoseCondition>();
			wlCondition.Lose();
		}
	}

	private bool GetIsDead(){
		return isDead;
	}

	public static bool GetAllPrisonerDead(){
		return allPrisonerDead;
	}

	private void ChangeSpriteFollowHealth() {
		if((maxHealth % lostHeathToChangeSprite) == 0){
			int spriteIndex = health/ lostHeathToChangeSprite;
			spriteRenderer.sprite = heathStatusSprites[spriteIndex];
		}
	}

	public Prisoner[] GetPrisonerArray(){
		return prisonerArray;
	}

	public static bool GetIsCastingSkill(){
		return prisonerIsCastingSkill;
	}

	public static void UnSetPrisonIsCasting(){
		prisonerIsCastingSkill = false;
	}

	public static void SetPrisonIsCasting(){
		prisonerIsCastingSkill = true;
	}
}