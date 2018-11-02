using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Radiant damage. attach this script to the destroy-particle effect caused by a shot to apply spreading damage
/// Else, attach to a shot prefab to make it damagable
/// </summary>
public class RadiantDamage : MonoBehaviour {

	private int damage;
	public float damageSpreadSpeed;
    public int damageIfNotSet;
    private CircleCollider2D rangeCollider;
	private float initRadius;
	private bool bIsRangeDamage;
    private bool damageIsSet = false;

	// Use this for initialization
	void Start () {
		if(GetComponent<ParticleSystem>()){
			bIsRangeDamage = true;
			rangeCollider = GetComponent<CircleCollider2D>();
			initRadius = rangeCollider.radius;

			rangeCollider.radius = 0.1f;
		}
	}   
	 
	void Update(){
		if(bIsRangeDamage){
			rangeCollider.radius += Time.deltaTime* damageSpreadSpeed;
			if(rangeCollider.radius >= initRadius){
				bIsRangeDamage = false;
			}
		} 
	}

	public int GetDamage(){
        if (!damageIsSet)
        {
            return damageIfNotSet;
        }
		return damage;
	}

	public void SetDamage(int damageToSet){
		damage = damageToSet;
        damageIsSet = true;
    }
}

