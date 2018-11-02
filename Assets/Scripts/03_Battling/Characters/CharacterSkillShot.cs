using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillShot : MonoBehaviour {
	public GameObject explodeParticlePref;
	public Sprite shotSprtIcon;
	public float shotSpeed;
	public float coolDownSpeed;
	public string skillName;

	public int shotPower;
	public bool bNotInterferedByStageShot;
	private Rigidbody2D rg2D;

    //---------------------------------------------------------------
    // Use this for initialization
    void Start () {
        ConvertPowerToDamage(shotPower);
         
        //TODO replace FindObjectOfType by FindObjectByTag
        Needle needle = GameObject.FindObjectOfType<Needle>();
		if (!needle) {
			return; //exit if no needle found 
		}
		Transform needleTransform = needle.GetComponent<Transform> ();
		transform.rotation = needleTransform.rotation;
		Vector3 shotDerivingPosition = needleTransform.right;
		rg2D = this.GetComponent<Rigidbody2D>() ;
		rg2D.velocity = new Vector2 (shotSpeed * shotDerivingPosition.x,shotSpeed * shotDerivingPosition.y );
	} 
	//---------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D col){
		if(skillName == "Riot"){
			return;
		}

		if(col.gameObject.GetComponent<BlockOfStage>() || col.gameObject.GetComponent<Enemy>() ||
			col.gameObject.GetComponent<EnemyPaddle>()){
			GameObject explosion = Instantiate(explodeParticlePref, transform.position, Quaternion.identity);

			if(explosion.GetComponent<RadiantDamage>()){
				explosion.GetComponent<RadiantDamage>().SetDamage(shotPower);
			}

			Destroy(gameObject);
		} else if(col.gameObject.GetComponent<Vehicle>()){
			Destroy(gameObject);
		}

		if(col.gameObject.GetComponent<ProjectileBall>()){
			if(bNotInterferedByStageShot){return;}
			GameObject explosion = Instantiate(explodeParticlePref, transform.position, Quaternion.identity);

			if(explosion.GetComponent<RadiantDamage>()){
				explosion.GetComponent<RadiantDamage>().SetDamage(shotPower);
			}
			Destroy (gameObject);
		}
	}
	//---------------------------------------------------------------
	public Sprite GetShotSprtIcon(){
		return shotSprtIcon;
	}
	//---------------------------------------------------------------
	public string GetShotSkillName(){
		return skillName;
	}
	//---------------------------------------------------------------
	public void ConvertPowerToDamage(int powerToConvert){
		if(GetComponent<RadiantDamage>()){
			GetComponent<RadiantDamage>().SetDamage(powerToConvert);
		}else{
			explodeParticlePref.GetComponent<RadiantDamage>().SetDamage(powerToConvert);
		}
	}
	//---------------------------------------------------------------
    public void SetShotPower(int powerToSet)
    {
        shotPower = powerToSet;
    }
	//---------------------------------------------------------------
    public int GetShotPower(){
        return shotPower;
	}
	//---------------------------------------------------------------
	public float GetShotCoolDownSpeed(){
		return coolDownSpeed;
	}
	//---------------------------------------------------------------
	public void ActivateEffect(string skillName){
		switch(skillName){
			
		}
	}
} 
