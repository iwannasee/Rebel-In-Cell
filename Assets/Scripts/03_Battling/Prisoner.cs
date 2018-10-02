using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour {
	public string prisonerName;

	[Tooltip("needs to equal or greater than number of sprites. should be the product of sprites length")]
	public int maxHealth;
	public int tapTimesToUseSkill;
	public GameObject gaugeMeterPrefab;
	public GameObject skillShotPrefab;
	public float maxCoolDownTime = 10;
	public float maxAdjustTime = 4;
	[Range(0.0f, 1.0f)]//delay time to prevent use prisoner skill continously
	public float skillDelayRate; 
	public string skillName;

	[Tooltip("sprite index from weak to healthy")]
	public Sprite[] heathStatusSprites;

    public Sprite hitImg;
    public Sprite deadImg;

    public AudioClip hitClip;
    public AudioClip skillClip;
    public AudioClip dieClip;

    public float maxHitImgInterval = 0.2f;

	static private bool pausingForSkill = false;
	static private bool justShot = false;
	static private bool prisonerIsCastingSkill = false;
	static private GameObject prisonerJustShooting;
	private static bool allPrisonerDead;

    private float hitImgInterval;
	private int health; 
	private int lostHeathToChangeSprite;
	private int tapTimes;
	private float skillCoolDownTime;
	private float needleAdjustTime;

	private bool isDead = false;
	private bool gaugeIsDisplayed = false;
	private GameObject skillShotContainer;
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

		spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = heathStatusSprites[heathStatusSprites.Length-1];
		if(heathStatusSprites.Length > 0 && maxHealth >= heathStatusSprites.Length){
			lostHeathToChangeSprite = maxHealth / heathStatusSprites.Length;
		}

        hitImgInterval = maxHitImgInterval;
		allPrisonerDead = false;
		prisonerJustShooting = null;
		health = maxHealth;
		skillCoolDownTime = maxCoolDownTime;
		needleAdjustTime = maxAdjustTime;
		prisonerArray = GameObject.FindObjectsOfType<Prisoner>();
		//Creat a container for skill shot
		if (!GameObject.Find ("Skill Shot Container")) {
			skillShotContainer = new GameObject ("Skill Shot Container");
			skillShotContainer.AddComponent<ClearUpChidren>();
		}else{
			skillShotContainer = GameObject.Find("Skill Shot Container");
		}
	}
	//---------------------------------------------------------------	
	void Update(){
		if(EnemyWaveController.GetWaveHasStarted()){
			//cooldown skill over time

			skillCoolDownTime = skillCoolDownTime - Time.deltaTime;
			SetSkillBar();
			//TODO implement slider to visualize cooldown status
			//if a skill shot is executed on play
			if(justShot){
				//look through every prisoner to check:
				foreach (Prisoner thisPrisoner in prisonerArray) {
					//if the prisoner just shooting is not this prisoner
					if (prisonerJustShooting.name != thisPrisoner.name) {
						//if not this prisoner, increase the waiting time to shot
						thisPrisoner.DelayAfterOtherPrisonerShot ();
					}
				}
				//after looking through prisoners, handling for that shot is done
				justShot = false;
			}
		}

		if (gaugeIsDisplayed) {
			//if Wave UI Text is displayed while gauge is displayed
			if (UITextController.GetUITextStatusType () == UITextController.DISPLAY_TEXT.WAVE) {
				Destroy (GameObject.FindObjectOfType<GaugeMeter> ().gameObject); //hide the gauge (destroying it)
				tapTimes = 0; //reset the tap times
				gaugeIsDisplayed = false;//the flag telling the gauge is NOT displayed to shot skill
				prisonerIsCastingSkill = false; //the flag telling prisoner is NOT about to shot skill
				return; 
			} 
			needleAdjustTime = needleAdjustTime - Time.unscaledDeltaTime;
			prisonerIsCastingSkill = true;
			if (needleAdjustTime <= 0 ) {
				//Unpause to shoot
				Time.timeScale = 1;
				SkillShootingFromNeedle ();
				Destroy (GameObject.FindObjectOfType<GaugeMeter> ().gameObject);
				gaugeIsDisplayed = false;
				needleAdjustTime = maxAdjustTime;
				prisonerIsCastingSkill = false;
				prisonerJustShooting = this.gameObject;
				justShot = true;
				pausingForSkill = false;
				PlayerPrefManager.SetUITextStatus(PlayerPrefManager.GUITEXT_STATUS_CHANGING);
				UITextController.SetUITextStatusType(UITextController.DISPLAY_TEXT.SKILL_NAME,skillName);
			}
		}
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
	//---------------------------------------------------------------	
	private void OnMouseDown(){
		//Disable this function if someprisoner is casting skill
		if(prisonerIsCastingSkill || 
		//Or the stage is clear
			(UITextController.GetUITextStatusType()== UITextController.DISPLAY_TEXT.CLEAR)||
			(UITextController.GetUITextStatusType()== UITextController.DISPLAY_TEXT.LOSE)){
			return;
		}
		if (skillCoolDownTime <= 0 && (Time.timeScale == 1)) {
			tapTimes++; 
			if (tapTimes >= tapTimesToUseSkill) {
				DisplayAimingAngle ();
				tapTimes = 0;
			}
		}
	}
	//---------------------------------------------------------------
	private void DisplayAimingAngle(){
		Vector3 position = GameObject.FindGameObjectWithTag ("Prisoner Paddle").transform.position;
		Instantiate (gaugeMeterPrefab, position, Quaternion.identity);
		gaugeIsDisplayed = true;
		pausingForSkill = true;
		Time.timeScale = 0;
		//PlayerPrefManager.SetSkillPause(PlayerPrefManager.SKILL_PAUSING);
	}
	//---------------------------------------------------------------
	private void SkillShootingFromNeedle(){
		Needle needle = GameObject.FindObjectOfType<Needle> ();
		if (!needle) {
			return;
		}
		GameObject needleGameObj = needle.gameObject;
		Transform needleGameObjTrans = needleGameObj.GetComponent<Transform> ();

		//Fire shot and attach the shot to the needle parent
		GameObject shot = Instantiate (skillShotPrefab) as GameObject;
		shot.transform.position = needleGameObjTrans.GetChild(0).position;
		if(skillShotContainer){
			shot.transform.parent = skillShotContainer.transform;
		}else {
			Debug.LogWarning("No shot container exists");
		}
		//reset cooldown for limitting use of skill
		skillCoolDownTime = maxCoolDownTime;
	}
	//---------------------------------------------------------------

	private bool GetIsDead(){
		return isDead;
	}

	private void SetHealthBar(){
		float healthScaleToSet = (float)health/(float)maxHealth;
		healthBar.SetHealthBarAccordingly(healthScaleToSet);
	}

	private void SetSkillBar(){ 
		float skillScaleToSet = skillCoolDownTime/maxCoolDownTime;
		skillBar.SetSkillBarAccordingly(skillScaleToSet);
	}
	//TODO make function when one prisoner uses skill, remaining prisoners'cooldown will be reduced by some %
	public void DelayAfterOtherPrisonerShot ()
	{
		if (skillCoolDownTime <= 0) {
			skillCoolDownTime = maxCoolDownTime * skillDelayRate;
		}
		else {
			skillCoolDownTime += maxCoolDownTime * skillDelayRate;
		}
	}
	//---------------------------------------------------------------

	public static bool GetIsCasingSkill(){
		return prisonerIsCastingSkill;
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
}