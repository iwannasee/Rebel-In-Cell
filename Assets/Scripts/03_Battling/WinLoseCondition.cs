using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinLoseCondition : MonoBehaviour {
	public GameObject reward;
	public float regretfulTime = 5;
	private static bool isWinner;

	private MusicManager musicMng;
	private static bool isGameOver = false;

	// Use this for initialization
	void Start () {
		isGameOver = false;
		GameObject MusicManagerObj = GameObject.FindGameObjectWithTag("Music Manager");
		if(!MusicManagerObj){
			print("no music manager object found");
			return;
		}

		musicMng = MusicManagerObj.GetComponent<MusicManager>();

		//Vehicle playerBase = GameObject.FindGameObjectWithTag("Vehicle").GetComponent<Vehicle>();

	}

	void Update(){
		if(isGameOver){
			regretfulTime -= Time.unscaledDeltaTime;
			if(regretfulTime<=0){
				int currentWorldIndex = PlayerProgress.presentWorldIndex;
				string lvName = "02 MapSelect World " + MapDictionary.worldList[currentWorldIndex]; 
				if (reward) {
					reward.GetComponent<Reward> ().DestroyReward ();
				}
				SceneManager.LoadScene(lvName);

			}
		}
	}

	public static bool GetIsWinner(){
		return isWinner;
	}

	public void Lose(){
		isGameOver = true;
		isWinner = false;
		PlayerPrefManager.SetUITextStatus (PlayerPrefManager.GUITEXT_STATUS_CHANGING);
		UITextController.SetUITextStatusType (UITextController.DISPLAY_TEXT.LOSE, "");

		//play gameover sound
		if(!musicMng){return;}
		musicMng.PlayMusic(musicMng.gameOverClip);
	}

	public void Win(){
		//TODO play victory music here 
		// musicMng.PlayMusic(musicMng.victoryClip);
		isWinner = true;
		PlayerPrefManager.SetUITextStatus (PlayerPrefManager.GUITEXT_STATUS_CHANGING);
		UITextController.SetUITextStatusType (UITextController.DISPLAY_TEXT.CLEAR, "");
	}

	/// <summary>
	/// Sets the is winner. For testing. Delete this later.
	/// </summary>
	/// <param name="win">If set to <c>true</c> window.</param>
	public static void SetIsWinner(bool win){
		isWinner = win;
	}

	public static bool GetIsGameOver(){
		return isGameOver;
	}

	public Reward GetRewardWillBeObtained(){
		return reward.GetComponent<Reward>();
	}
}
