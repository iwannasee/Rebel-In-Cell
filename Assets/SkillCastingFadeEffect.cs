using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCastingFadeEffect : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	bool bCanStartEffect = false;
	bool bIsEndEffect = false;
	public float effectSpeed;
	[Tooltip("the alpha property of this sprite when player adjust gauge meter")]
	public float alphaForEffect;

	private float alphaChannel;
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		alphaChannel = spriteRenderer.color.a;
	}
	
	// Update is called once per frame
	void Update () {

		if(bCanStartEffect){
			
			alphaChannel+= Time.unscaledDeltaTime *effectSpeed;
			spriteRenderer.color = new Color (spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b, alphaChannel);
			if(alphaChannel >= (alphaForEffect/255)){
				bCanStartEffect = false;
			}
			return;
		}
		  
		if(bIsEndEffect){
			print("is effecting " + spriteRenderer.color);
			alphaChannel -= Time.unscaledDeltaTime *effectSpeed;
			spriteRenderer.color = new Color (spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b, alphaChannel);
			if(alphaChannel <= 0){
				bIsEndEffect = false;
			}
			return;
		}
	}

	public void StartEffect(){
		bCanStartEffect = true;
		//StartCoroutine("CastingEffectStart");
	}


	public void EndEffect(){
		bIsEndEffect = true;
	}
}
