using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableNotification : MonoBehaviour {
	float maxScale = 1.3f;
	float minScale = 0.9f;
	bool isGrowing = true;
	// Update is called once per frame
	void Update () {
		if(isGrowing){
			transform.localScale = new Vector2(
				transform.localScale.x + Time.deltaTime, 
				transform.localScale.y + Time.deltaTime
			);
			if(transform.localScale.x >= maxScale){
				isGrowing = false;
			}
		}else{
			transform.localScale = new Vector2(
				transform.localScale.x - Time.deltaTime, 
				transform.localScale.y - Time.deltaTime
			);

			if(transform.localScale.x <= minScale){
				isGrowing = true;
			}
		}
	}
}
