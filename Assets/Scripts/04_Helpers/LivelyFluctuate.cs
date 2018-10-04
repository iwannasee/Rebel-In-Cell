using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivelyFluctuate : MonoBehaviour {
	public float fluctuatingRange =0.06f;
	public float fluctuatingTime = 0.14f;
	// Use this for initialization
	private bool upDownToggle = false;
	private float initialPos;
	void Start () {
		initialPos = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		if(upDownToggle){
			if(transform.position.y <= initialPos + fluctuatingRange){
				transform.Translate(0f, Time.deltaTime* fluctuatingTime, 0f);
			}else{
				upDownToggle = false;
			}
		}else{ 
			if(transform.position.y >= initialPos - fluctuatingRange){
				transform.Translate(0f, -Time.deltaTime *fluctuatingTime, 0f);
			}else{
				upDownToggle = true;
			}
		}

	}
}
