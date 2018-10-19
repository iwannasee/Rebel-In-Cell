using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSkill : MonoBehaviour {
	public int tapTimesToUseSkill;
	public GameObject gaugeMeterPrefab;

	//TODO Make sure skill shot prefab number is equal to the equivalent char skill shot number in Common Data
	public GameObject[] skillShotPrefabs;
	public float maxAdjustTime = 4;

	private GameObject skillShotToPlay;
	[Range(0.0f, 1.0f)]//delay time to prevent use prisoner skill continously
	public float skillDelayRate;
	private float maxCoolDownTime;
	private string playingSkillName;

	//TODO implement skill casting sound using skill clip
	//public AudioClip skillClip;
	static private GameObject prisonerJustUsedSkill;
	static private bool pausingForSkill = false;

	private float skillCoolDownTime; 
	private float needleAdjustTime;
	private int tapTimes;
	private bool bIsSkillCastEffectShowing = false;
	private GameObject skillShotContainer;
	private SkillCastingFadeEffect skillCastingEffect;
	Renderer[] standOutRenderers;
	private HealthBar healthBar;
	private SkillBar skillBar;

	private bool bIsShootingSkill = false;
	private bool bIsSkillUsedThisCharge = false;

	//---------------------------------------------------------------
	void Start(){ 
		skillShotToPlay = GetSkillShotToPlay();
		bIsShootingSkill = (skillShotToPlay.GetComponent<CharacterSkillShot>() != null);

		if(bIsShootingSkill){
			playingSkillName = skillShotToPlay.GetComponent<CharacterSkillShot>().GetShotSkillName();
			maxCoolDownTime = skillShotToPlay.GetComponent<CharacterSkillShot>().GetShotCoolDownSpeed();
		}else if(skillShotToPlay.GetComponent<SupportSkillShot>()){
			playingSkillName = skillShotToPlay.GetComponent<SupportSkillShot>().GetShotSkillName();
			maxCoolDownTime = skillShotToPlay.GetComponent<SupportSkillShot>().GetShotCoolDownSpeed();
		}

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

		if (bIsSkillCastEffectShowing) {
			//if Wave UI Text is displayed while gauge is displayed
			if (UITextController.GetUITextStatusType () == UITextController.DISPLAY_TEXT.WAVE) {
				Destroy (GameObject.FindObjectOfType<GaugeMeter> ().gameObject); //hide the gauge (destroying it)
				tapTimes = 0; //reset the tap times
				bIsSkillCastEffectShowing = false;//the flag telling the gauge is NOT displayed to shot skill
				Prisoner.UnSetPrisonIsCasting() ; //the flag telling prisoner is NOT about to shot skill
				return; 
			} 

			SkillCasting_Regeneration();

			needleAdjustTime = needleAdjustTime - Time.unscaledDeltaTime;

			Prisoner.SetPrisonIsCasting();
			if (needleAdjustTime <= 0 ) {
				//TODO refactor this
				//Unpause to shoot
				Time.timeScale = 1;
				skillCastingEffect.EndEffect();
				ExitStandOut();

				if(!bIsSkillUsedThisCharge){
					//if the skill is the shooting skill
					if(bIsShootingSkill){
						SkillShootingFromNeedle ();
					// else if the skill is the supporting skill
					}else{
						CastSupportSkill();
					}
				}

				bIsSkillCastEffectShowing = false;
				needleAdjustTime = maxAdjustTime;
				Prisoner.UnSetPrisonIsCasting();
				prisonerJustUsedSkill = this.gameObject; 
				FindNotYetSkillUsersToDelaySkill();
				pausingForSkill = false;
				PlayerPrefManager.SetUITextStatus(PlayerPrefManager.GUITEXT_STATUS_CHANGING);
				UITextController.SetUITextStatusType(UITextController.DISPLAY_TEXT.SKILL_NAME,playingSkillName);
			}
		}
	}
	//---------------------------------------------------------------
	private void OnMouseUp(){

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
				ShowSkillCastEffect ();
				tapTimes = 0;
			}
		}
	}

	private void OnMouseDown(){
		
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
		GameObject shot = Instantiate (skillShotToPlay) as GameObject;
		shot.transform.position = needleGameObjTrans.GetChild(0).position;
		if(skillShotContainer){
			shot.transform.parent = skillShotContainer.transform;
		}else {
			Debug.LogWarning("No shot container exists");
		}
		//reset cooldown for limitting use of skill
		skillCoolDownTime = maxCoolDownTime;
		bIsSkillUsedThisCharge = true;
		Destroy (GameObject.FindObjectOfType<GaugeMeter> ().gameObject);
	}

	//---------------------------------------------------------------
	//if the skill is shooting skill
	private void DisplayAimingAngle(){
		Vector3 position = GameObject.FindGameObjectWithTag ("Prisoner Paddle").transform.position;
		Instantiate (gaugeMeterPrefab, position, Quaternion.identity);
		/*
		bIsSkillCastEffectShowing = true;
		pausingForSkill = true;
		Time.timeScale = 0;*/
		//PlayerPrefManager.SetSkillPause(PlayerPrefManager.SKILL_PAUSING);
	}
	private void ShowSkillCastEffect(){
		bIsSkillUsedThisCharge = false;
		bIsSkillCastEffectShowing = true;
		pausingForSkill = true;
		Time.timeScale = 0;
		if(skillShotToPlay.GetComponent<CharacterSkillShot>()){
			DisplayAimingAngle();
		}else if (skillShotToPlay.GetComponent<SupportSkillShot>()){
			print("show vfx of skill casting");
		}
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

	public GameObject GetSkillShotToPlay(){
		if(!GetComponent<Prisoner>()){
			print("The actor using this shooting skill script is not character player");
			return null;
		}
		string charName = GetComponent<Prisoner>().GetPrisonerName();
		string lastUsedSkillName = Prisoner.GetCharacterLastUsedSkill(charName);

		for(int i = 0 ; i< skillShotPrefabs.Length; i++){
			if(skillShotPrefabs[i].GetComponent<CharacterSkillShot>()){
				if(skillShotPrefabs[i].GetComponent<CharacterSkillShot>().GetShotSkillName() == lastUsedSkillName){
					return skillShotPrefabs[i];
				}
			}else if(skillShotPrefabs[i].GetComponent<SupportSkillShot>()){
				if(skillShotPrefabs[i].GetComponent<SupportSkillShot>().GetShotSkillName() == lastUsedSkillName){
					return skillShotPrefabs[i];
				}
			}
		}

		return null;
	}

	private void CastSupportSkill(){
		
		switch(playingSkillName){
			case CommonData.Johnny_Regeneration:
				print("cast supporting now");
			break;
			case CommonData.Johnny_Epidemic:

			break;
			case CommonData.Johnny_Achemysto:

			break;
			case CommonData.Mathial_DragonStance:

			break;
			case CommonData.Mathial_PrayingMantisStance:

			break;
			case CommonData.Mathial_ReverseBowStance:

			break;

			default:
			break;
		}

		skillCoolDownTime = maxCoolDownTime;

	}

	private void SkillCasting_Regeneration(){
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		print (needleAdjustTime);

		if(Input.GetMouseButtonDown(0)){
			if(hit.collider != null)
			{
			//TODO replace the magic number
				hit.transform.gameObject.GetComponent<Health>().HealthUp(30);
				needleAdjustTime = 0f;
				bIsSkillUsedThisCharge = true;
				bIsSkillCastEffectShowing = false;
			}
		}
	}
}
