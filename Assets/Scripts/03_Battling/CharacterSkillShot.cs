using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillShot : MonoBehaviour {
	public GameObject explodeParticlePref;
	public Sprite shotSprtIcon;
	public float shotSpeed;
	public float coolDownSpeed;
	public string skillName;

	private int shotPower;

	private Rigidbody2D rg2D;


	//---------------------------------------------------------------
	// Use this for initialization
	void Start () {
		shotPower = GetShotPower();

		//TODO replace FindObjectOfType by FindObjectByTag
		Needle needle = GameObject.FindObjectOfType<Needle>();
		if (!needle) {
			return; //exit if no needle found 
		}
		Vector3 shotDerivingPosition = needle.GetComponent<Transform> ().right;
		rg2D = this.GetComponent<Rigidbody2D>() ;
		rg2D.velocity = new Vector2 (shotSpeed * shotDerivingPosition.x,shotSpeed * shotDerivingPosition.y );
	} 

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.GetComponent<BlockOfStage>() || col.gameObject.GetComponent<Enemy>() ||
			col.gameObject.GetComponent<EnemyPaddle>()){
			Instantiate(explodeParticlePref, transform.position, Quaternion.identity);
			 
			Destroy(gameObject);
		}
	}

	public Sprite GetShotSprtIcon(){
		return shotSprtIcon;
	}

	public string GetShotSkillName(){
		return skillName;
	}

	public int GetShotPower(){
		return explodeParticlePref.GetComponent<RadiantDamage>().GetDamage();
	}

	public float GetShotCoolDownSpeed(){
		return coolDownSpeed;
	}

} 
