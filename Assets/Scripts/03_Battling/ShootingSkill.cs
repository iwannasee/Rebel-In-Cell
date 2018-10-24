﻿using System.Collections;
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
    private int shotPower;
	private bool bIsSkillCastEffectShowing = false;
	private GameObject castSkillFX;
	private GameObject skillShotContainer;
	private SkillCastingFadeEffect skillCastingEffect;
	Renderer[] standOutRenderers;
	private HealthBar healthBar;
	private SkillBar skillBar;

	private bool bIsShootingSkill = false;
	private bool bIsSkillUsedThisCharge = false;
    private bool mouseIsDown = false;
    private bool bIsAutoSkill = false;
	//---------------------------------------------------------------
	void Start(){ 
		skillShotToPlay = GetSkillShotToPlay();
		bIsShootingSkill = (skillShotToPlay.GetComponent<CharacterSkillShot>() != null);

		if(bIsShootingSkill){
			playingSkillName = skillShotToPlay.GetComponent<CharacterSkillShot>().GetShotSkillName();
			maxCoolDownTime = skillShotToPlay.GetComponent<CharacterSkillShot>().GetShotCoolDownSpeed();
            shotPower = skillShotToPlay.GetComponent<CharacterSkillShot>().GetShotPower();

        }
        else if(skillShotToPlay.GetComponent<SupportSkillShot>()){
			playingSkillName = skillShotToPlay.GetComponent<SupportSkillShot>().GetShotSkillName();
			maxCoolDownTime = skillShotToPlay.GetComponent<SupportSkillShot>().GetShotCoolDownSpeed();
            shotPower = skillShotToPlay.GetComponent<SupportSkillShot>().GetShotPower();
			bIsAutoSkill = skillShotToPlay.GetComponent<SupportSkillShot>().GetIsAutoSkill();
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
		if(WinLoseCondition.GetIsGameOver() == true){
			return; 
		}

		if(EnemyWaveController.GetWaveHasStarted()){
			//cooldown skill over time
			skillCoolDownTime = skillCoolDownTime - Time.deltaTime;

			//visualize cooldown status 
			SetSkillBar();

			if(skillCoolDownTime <= 0){
				if(bIsAutoSkill){
					CastAutoPlaySkill();
				}
			}
		}

		if (bIsSkillCastEffectShowing) {
			//if Wave UI Text is displayed while gauge is displayed
			if (UITextController.GetUITextStatusType () == UITextController.DISPLAY_TEXT.WAVE) {
                if (bIsShootingSkill){
                    Destroy(GameObject.FindObjectOfType<GaugeMeter>().gameObject); //hide the gauge (destroying it)
                }
				
				tapTimes = 0; //reset the tap times
				bIsSkillCastEffectShowing = false;//the flag telling the gauge is NOT displayed to shot skill
				Prisoner.UnSetPrisonIsCasting() ; //the flag telling prisoner is NOT about to shot skill
				return; 
			}

            //Only support skills can be activated right on the adjusting time
            CastSupportSkillWhenAdjTimeIs(true);

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
                        CastSupportSkillWhenAdjTimeIs(false);
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
				ShowSkillCastEffect ();
				tapTimes = 0;
			}
		}
        mouseIsDown = true;
    }

    private void OnMouseUpAsButton()
    {
        mouseIsDown = false;
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

		if(skillCoolDownTime > maxCoolDownTime){
			skillCoolDownTime = maxCoolDownTime;
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

	//Play manual skill
	private void CastSupportSkillWhenAdjTimeIs(bool isCastWhenAdjustTime){
		
		switch(playingSkillName){
			case CommonData.Johnny_Regeneration:
                SkillCasting_Regeneration(isCastWhenAdjustTime);

            break;
			case CommonData.Johnny_Epidemic:
                SkillCasting_Epidemic(isCastWhenAdjustTime);
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

	//Play auto-skill
	private void CastAutoPlaySkill(){
		
		switch(playingSkillName){
		case CommonData.Johnny_Achemysto:
				SkillCasting_Achemysto();
			break;
			default:
			break;
		}
		prisonerJustUsedSkill = this.gameObject; 
		FindNotYetSkillUsersToDelaySkill();
		PlayerPrefManager.SetUITextStatus(PlayerPrefManager.GUITEXT_STATUS_CHANGING);
		UITextController.SetUITextStatusType(UITextController.DISPLAY_TEXT.SKILL_NAME,playingSkillName);
		skillCoolDownTime = maxCoolDownTime;

	}


    /// <summary>
    /// Skill Name: Regeneration
    /// Effect: select a character and recover health point for that character
    /// *Note: if player does not select any character ( tap on), then when adjustment time is over, heal the char who casts
    /// Heath recovery power: temporarily equal the power of the skill shot
    /// </summary>
    /// <param name="isCastWhenAdjustTime"></param>
	private void SkillCasting_Regeneration(bool isCastWhenAdjustTime)
    {
        if (mouseIsDown) { return; }
        if (!isCastWhenAdjustTime){
            GetComponent<Health>().AddHealth(shotPower);
            return;
        }
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if(Input.GetMouseButtonDown(0)){
            if (hit.collider != null){
                hit.transform.gameObject.GetComponent<Health>().AddHealth(shotPower);
                needleAdjustTime = 0f;
                bIsSkillUsedThisCharge = true;
                bIsSkillCastEffectShowing = false;
            }
        }
    }

    /// <summary>
    /// Skill Name: Epidemic
    /// Effect:
    /// *Note: shot power is not used in this skill
    /// </summary>
    /// <param name="isCastWhenAdjustTime"></param>
    private void SkillCasting_Epidemic(bool isCastWhenAdjustTime)
    {
		if (mouseIsDown) { return; }
		int heathCost = skillShotToPlay.GetComponent<SkillEffect_Epidemic>().GetLifeCost();
		if(GetComponent<Health>().GetHealth() < heathCost){
			return;
		}

        if (!isCastWhenAdjustTime)
        {
			GetComponent<Health>().AddHealth(-heathCost);
			GameObject shot = Instantiate (skillShotToPlay) as GameObject;
			shot.transform.position = transform.position;
            return;
        }

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if(Input.GetMouseButtonDown(0)){
            if (hit.collider != null){
				hit.transform.gameObject.GetComponent<Health>().AddHealth(-heathCost);
				GameObject shot = Instantiate (skillShotToPlay) as GameObject;
				shot.transform.position = hit.collider.transform.position;
				needleAdjustTime = 0f;
                bIsSkillUsedThisCharge = true;
                bIsSkillCastEffectShowing = false;
            }
        }
    }

	/// <summary>
    /// Skill Name: Reinforcement
    /// Effect: lengthen the paddle
    /// *Note: this effect can use up eternally.
    /// </summary>
    /// <param name="isCastWhenAdjustTime"></param>
	private void SkillCasting_Reinforcement(bool isCastWhenAdjustTime){
		GameObject playerPaddle = GameObject.FindGameObjectWithTag("Prisoner Paddle");
		float scaleRate = (float) shotPower * 0.1f;
		playerPaddle.transform.localScale = new Vector3(1 + scaleRate,1,1);
	}

	/// <summary>
    /// Skill Name: Achemysto
    /// Effect: Increase 15% hp for all character
    /// *Note: auto skill
    /// </summary>
	private void SkillCasting_Achemysto(){
        //Implement here
        Prisoner[] AllChar = GetComponent<Prisoner>().GetPrisonerArray();
        for(int i = 0; i < AllChar.Length; i++){
        	AllChar[i].GetComponent<Health>().AddHealth(shotPower);
        }
	} 

	/// <summary>
    /// Skill Name: Locomotion
	/// Effect: Use up 40% base durability to make paddle in fire. when paddle in fire, if the stage shot hit it, lauch a shot vertically towards enemy side.
    /// *Note: 
    /// </summary>
    /// <param name="isCastWhenAdjustTime"></param>

}
