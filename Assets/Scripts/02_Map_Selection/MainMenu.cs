using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {

	public Text playerName;
	public Text playerGold;

	void Awake(){

	}

	void Start(){
		playerName.text = PlayerProgress.playerData.playerName;
		playerGold.text = PlayerProgress.playerData.gold.ToString();
	}
}
