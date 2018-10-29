using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackholeShield : MonoBehaviour {
	// Use this for initialization
	public float rotateSpeed ;
	public float moveSpeed;
	public Vector2 startPoint;
	public float endPointX;


	void Start(){
		transform.parent.position = startPoint;
		int power = transform.parent.GetComponent<SupportSkillShot>().GetShotPower();
		moveSpeed = 1-(float)power/100; 
	}

	// Update is called once per frame
	void Update () {
		transform.parent.Translate (Vector3.right*moveSpeed*Time.deltaTime);
		transform.Translate (Vector3.right*moveSpeed*Time.deltaTime);
		transform.Rotate(Vector3.forward, Time.deltaTime*rotateSpeed);
		if(transform.parent.position.x >= endPointX){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.GetComponent<EnemyShot>()){
			Destroy(col.gameObject);
		}
	}
}
