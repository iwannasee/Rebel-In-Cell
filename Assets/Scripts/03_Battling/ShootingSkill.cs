using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSkill : MonoBehaviour {
	public int tapTimesToUseSkill;
	public GameObject gaugeMeterPrefab;
	public GameObject skillShotPrefab;
	public float maxCoolDownTime = 10;
	public float maxAdjustTime = 4;
	[Range(0.0f, 1.0f)]//delay time to prevent use prisoner skill continously
	public float skillDelayRate; 
	public string skillName;

	//TODO implement skill casting sound using skill clip
	//public AudioClip skillClip;
	static private GameObject prisonerJustUsedSkill;
	static private bool pausingForSkill = false;

	private float skillCoolDownTime;
	private float needleAdjustTime;
	private int tapTimes;
	private bool gaugeIsDisplayed = false;
	private GameObject skillShotContainer;
	private SkillCastingFadeEffect skillCastingEffect;
	Renderer[] standOutRenderers;
	private HealthBar healthBar;
	private SkillBar skillBar;
	//---------------------------------------------------------------
	void Start(){ 
		skillCastingEffect = GameObject.FindGameObjectWithTag("Skill Casting Effect").GetComponent<SkillCastingFadeEffect>();
		
		healthBar = transform.parent.GetComponentInChildren<HealthBar>();
		
		skillBar = transform.parent.GetComponentInChildren<SkillBar>();
		if(!healthBar || !skillBar){
			Debug.Log("no health or skill bar found");
		}

		if(GetComponent<Prisoner>()){
			FindRendersToStandOut();
		}

		skillCoolDownTime = maxCoolDownTime;
		needleAdjustTime = maxAdjustTime;

		//Init no prisoner use skill
		prisonerJustUsedSkill = null;
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
			//visualize cooldown status
			SetSkillBar();
		}

		if (gaugeIsDisplayed) {
			//if Wave UI Text is displayed while gauge is displayed
			if (UITextController.GetUITextStatusType () == UITextController.DISPLAY_TEXT.WAVE) {
				Destroy (GameObject.FindObjectOfType<GaugeMeter> ().gameObject); //hide the gauge (destroying it)
				tapTimes = 0; //reset the tap times
				gaugeIsDisplayed = false;//the flag telling the gauge is NOT displayed to shot skill
				Prisoner.UnSetPrisonIsCasting() ; //the flag telling prisoner is NOT about to shot skill
				return; 
			} 
			needleAdjustTime = needleAdjustTime - Time.unscaledDeltaTime;
			Prisoner.SetPrisonIsCasting();
			if (needleAdjustTime <= 0 ) {
				//TODO refactor this
				//Unpause to shoot
				Time.timeScale = 1;
				skillCastingEffect.EndEffect();
				ExitStandOut();
				SkillShootingFromNeedle ();
				//TODO refactor this
				Destroy (GameObject.FindObjectOfType<GaugeMeter> ().gameObject);
				gaugeIsDisplayed = false;
				needleAdjustTime = maxAdjustTime;
				Prisoner.UnSetPrisonIsCasting();
				prisonerJustUsedSkill = this.gameObject; 
				FindNotYetSkillUsersToDelaySkill();
				pausingForSkill = false;
				PlayerPrefManager.SetUITextStatus(PlayerPrefManager.GUITEXT_STATUS_CHANGING);
				UITextController.SetUITextStatusType(UITextController.DISPLAY_TEXT.SKILL_NAME,skillName);
			}
		}
	}
	//---------------------------------------------------------------
	private void OnMouseDown(){
		//Disable this function if someprisoner is casting skill
		if(Prisoner.GetIsCastingSkill() || 
		//Or the stage is clear
			(UITextController.GetUITextStatusType()== UITextController.DISPLAY_TEXT.CLEAR)||
			(UITextController.GetUITextStatusType()== UITextController.DISPLAY_TEXT.LOSE)){
			return;
		}
		if (skillCoolDownTime <= 0 && (Time.timeScale == 1)) {
			tapTimes++; 
			if (tapTimes >= tapTimesToUseSkill) {
				skillCastingEffect.StartEffect();
				MakeThisCasterStandOut();
				DisplayAimingAngle ();
				tapTimes = 0;
			}
		}
	}
	//---------------------------------------------------------------
	private void DelayAfterOtherPrisonerShot ()
	{
		if (skillCoolDownTime <= 0) {
			skillCoolDownTime = maxCoolDownTime * skillDelayRate;
		}
		else {
			skillCoolDownTime += maxCoolDownTime * skillDelayRate;
		}
	} 
	//---------------------------------------------------------------
	private void SetSkillBar(){ 
		float skillScaleToSet = skillCoolDownTime/maxCoolDownTime;
		skillBar.SetSkillBarAccordingly(skillScaleToSet);
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
	private void DisplayAimingAngle(){
		Vector3 position = GameObject.FindGameObjectWithTag ("Prisoner Paddle").transform.position;
		Instantiate (gaugeMeterPrefab, position, Quaternion.identity);
		gaugeIsDisplayed = true;
		pausingForSkill = true;
		Time.timeScale = 0;
		//PlayerPrefManager.SetSkillPause(PlayerPrefManager.SKILL_PAUSING);
	}
	//---------------------------------------------------------------
	private void FindNotYetSkillUsersToDelaySkill(){
		Prisoner[] prisonerArray = this.GetComponent<Prisoner>().GetPrisonerArray();
		foreach (Prisoner thisPrisoner in prisonerArray) {
		//if the prisoner just shooting is not this prisoner
			if (prisonerJustUsedSkill.name != thisPrisoner.name) {
				//if not this prisoner, increase the waiting time to shot
				ShootingSkill skill = thisPrisoner.GetComponent<ShootingSkill>();
				skill.DelayAfterOtherPrisonerShot ();
			}
		}
	}

	private void MakeThisCasterStandOut(){
		foreach(Renderer renderer in standOutRenderers){
			renderer.sortingLayerName = "StandOut";
		}
	}

	private void ExitStandOut(){
		foreach(Renderer renderer in standOutRenderers){
			renderer.sortingLayerName = "Characters";
		}
	}

	private void FindRendersToStandOut(){		
			Transform thisCharacterBlock = transform.parent;
			standOutRenderers = thisCharacterBlock.GetComponentsInChildren<SpriteRenderer>();
		}
}
