using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOfStage : MonoBehaviour {
	// Use this for initialization
	private int WaveBlockCount;

	[Tooltip("Assign sprites from healthy to weak, following the health")]
	public Sprite[] sprites;

	[Tooltip("needs to equal or greater than number of sprites. should be the product of sprites length")]
	public int maxHealth;
	public int health;

	private int lostHeathToChangeSprite;
	private GameObject ItemContainer;
	//---------------------------------------------------------------
	void Start () {
		health = maxHealth;
		int numberOfSprites = sprites.Length;

		if(numberOfSprites > 0 && maxHealth >= numberOfSprites){
			lostHeathToChangeSprite = maxHealth / numberOfSprites;
		}
	}
	//---------------------------------------------------------------
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.GetComponent <RadiantDamage>()) {
			//TODO add quake effect 
			RadiantDamage radiantDamage = collision.gameObject.GetComponent<RadiantDamage>();
			int inflictedDamage = radiantDamage.GetDamage();
			health = health - inflictedDamage;

			if (health <= 0) {
				GetComponent<ItemDropper>().DropItem();
				BlockDestroy ();
			}

			if(lostHeathToChangeSprite != 0){
				ChangeSpriteFollowHealth();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.GetComponent<RadiantDamage> ()){
			print("collider.gameObject " + collider.name);
			RadiantDamage radiantDamage = collider.GetComponent<RadiantDamage>();
			int inflictedDamage = radiantDamage.GetDamage();
			health = health - inflictedDamage;

			if (health <= 0) {
				GetComponent<ItemDropper>().DropItem();
				BlockDestroy ();
				return;
			}

			if(lostHeathToChangeSprite != 0){
				ChangeSpriteFollowHealth();
			}
		}
	}

	//---------------------------------------------------------------
	void BlockDestroy(){
		ParticleManager vfx = GameObject.FindGameObjectWithTag("Particle Manager").GetComponent<ParticleManager>();
		//Find how many block in this current wave
		WaveBlockCount = GameObject.FindObjectsOfType<BlockOfStage>().Length;
		WaveBlockCount--;
		//EXTENDABLE if this wave have block regen, check the isRegen bool of block container 
		//Destroy whole block container if this is the last block
		if(WaveBlockCount<=0){
			Instantiate(vfx.brickBroken, transform.position, Quaternion.identity);
			Destroy(transform.parent.gameObject);
		}
		Instantiate(vfx.brickBroken, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	private void ChangeSpriteFollowHealth() {
		if((health % lostHeathToChangeSprite) == 0){
			SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
			int spriteIndex = health/ lostHeathToChangeSprite;
			spriteRenderer.sprite = sprites[spriteIndex];
		}
	}
	//---------------------------------------------------------------
}
