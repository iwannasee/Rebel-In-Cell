using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour {
	public string prisonerName;
	static private bool prisonerIsCastingSkill = false;
	private Prisoner[] prisonerArray;
	private static bool allPrisonerDead;

	//every renderer components of this character when played
	//include vehicle back and front, health & skill bar and this own char renderer
	//---------------------------------------------------------------
	void Start(){


		allPrisonerDead = false;
		prisonerArray = GameObject.FindObjectsOfType<Prisoner>();
	}
	//---------------------------------------------------------------
	public void CheckAlivePrisonerLeft(){
		int alivePrisonerNum = prisonerArray.Length;
		foreach(Prisoner thisPrisoner in prisonerArray){
			Health thisPrisonerHealth = GetComponent<Health>();
			bool isDead = (thisPrisonerHealth.GetHealth() <= 0);
			if(isDead){
				alivePrisonerNum--;
			}
		}
		if (alivePrisonerNum <=0){
			allPrisonerDead = true;
			//as all the prisoners are down, set the game is lost
		 	WinLoseCondition wlCondition=GameObject.FindGameObjectWithTag("Win Lose Condition").GetComponent<WinLoseCondition>();
			wlCondition.Lose();
		}
	}
	//---------------------------------------------------------------
	public static bool GetAllPrisonerDead(){
		return allPrisonerDead;
	}
	//---------------------------------------------------------------
	public Prisoner[] GetPrisonerArray(){
		return prisonerArray;
	}
	//---------------------------------------------------------------
	public static bool GetIsCastingSkill(){
		return prisonerIsCastingSkill;
	}
	//---------------------------------------------------------------
	public static void UnSetPrisonIsCasting(){
		prisonerIsCastingSkill = false;
	}
	//---------------------------------------------------------------
	public static void SetPrisonIsCasting(){
		prisonerIsCastingSkill = true;
	}

	public string GetPrisonerName(){
		return prisonerName;
	}

	public Sprite GetPrisonerSprt(){
		Sprite prisonerSprt = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
		return prisonerSprt;
	}
}