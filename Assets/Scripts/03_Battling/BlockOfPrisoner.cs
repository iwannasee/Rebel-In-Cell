using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOfPrisoner : MonoBehaviour {
	// Use this for initialization
	public static int ShelterBlockCount = 0;
	public int maxHealth;
	private int health;
	
	//---------------------------------------------------------------
	void Start () {
		health = maxHealth;	
		ShelterBlockCount++;
	}
	//---------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		TimesHitHandle ();
	}
	//---------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.CompareTag ("Projectile Ball")) {
			health--;
		} else if (collision.gameObject.CompareTag ("Enemy Shot")) {
			health--;
		}
	}
	//---------------------------------------------------------------
	void TimesHitHandle(){ 
		if (health <= 0) {
			Destroy (gameObject);
			ShelterBlockCount--;
		}
	}
}
