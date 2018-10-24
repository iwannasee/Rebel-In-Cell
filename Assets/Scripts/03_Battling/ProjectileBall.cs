using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBall : MonoBehaviour {
	public float startForce;
	public float speedTweakNumber;
	public int maxLimitHit;
	public float maxHitInterval;
	public int totalTimeToHitWallInARow = 6;
    public GameObject hitParticlePref;
	private Rigidbody2D rg2D;
	private int limitHit;
	private float hitInterval;
	private bool justHit = false;

	private int limitTimeToHitWall;

	//---------------------------------------------------------------
	void Start () {
		limitHit = maxLimitHit;
		hitInterval = maxHitInterval;
		limitTimeToHitWall = totalTimeToHitWallInARow;
	}

	//---------------------------------------------------------------
	void Update(){

		//if 5 hits are done within 2 secs, destroy this object
		if (justHit) {
			hitInterval -= Time.deltaTime;
			if (hitInterval > 0 && (limitHit <= 0)) {
				//Critical hit row Function
				PlayerPrefManager.SetUITextStatus(PlayerPrefManager.GUITEXT_STATUS_CHANGING);
				if((UITextController.GetUITextStatusType() != UITextController.DISPLAY_TEXT.CLEAR) ||
					(UITextController.GetUITextStatusType() != UITextController.DISPLAY_TEXT.LOSE)){
					UITextController.SetUITextStatusType(UITextController.DISPLAY_TEXT.CRITICAL_HIT,"");
				}
				//TODO implement critical hit effect
				Destroy (gameObject);
			}
			//if the critical time (hitInterval) passed, recover hitlimit and critical time count
			if (hitInterval <= 0) {
				justHit = false;
				hitInterval = maxHitInterval;
				limitHit = maxLimitHit;
			}
		}
	}
	//---------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D collision){
		//If all the prisoners are down, stop bounding collision of this ball by destroying it
		if(Prisoner.GetAllPrisonerDead()){
			Destroy(gameObject);
		}
        if (collision.gameObject.GetComponent<BlockOfStage>())
        {
            Instantiate(hitParticlePref, transform.position, Quaternion.identity);
        }

		if (collision.gameObject.CompareTag ("Prisoner")) {
			justHit = true;
			SpeedTweaking ();
			limitHit--;
			ResetTimeHitWallInARow();
            Instantiate(hitParticlePref, transform.position, Quaternion.identity);
		}
		if (collision.gameObject.CompareTag("Enemy") ){
			justHit = true;
			SpeedTweaking ();
			limitHit--;
			ResetTimeHitWallInARow();
            Instantiate(hitParticlePref, transform.position, Quaternion.identity);
        }

		//if (collision.gameObject.CompareTag("Tweaking Wall") ){
			//SpeedTweaking ();
			//limitTimeToHitWall = 6;
		//}

		if (collision.gameObject.CompareTag("Wall")){
			limitTimeToHitWall--;
			if(limitTimeToHitWall <=0){
				//TODO print("do change direction of stage ball");
				SpeedTweaking ();
				ResetTimeHitWallInARow();
			}
		}
	}

	//---------------------------------------------------------------
	//Vary speed a bit if collided
	void SpeedTweaking(){
		Vector2 tweak = new Vector2 (Random.Range (-speedTweakNumber, speedTweakNumber), Random.Range (-speedTweakNumber, speedTweakNumber));
		if(rg2D){
			rg2D.velocity += tweak;
		}else{
			Debug.LogError("ball rigid body doesnt exist");
		}
	}

	public void PushBall(){
		rg2D = this.GetComponent<Rigidbody2D>() ;
		rg2D.velocity = new Vector2(Random.onUnitSphere.x * startForce, Random.onUnitSphere.z*startForce);
        print("velocity of ball: " + rg2D.velocity);
	}

	private void ResetTimeHitWallInARow(){
		limitTimeToHitWall = totalTimeToHitWallInARow;
	}
}
