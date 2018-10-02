using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

	//public AudioClip[] levelClips;
	public AudioClip gameOverClip;

	private AudioSource audioSource;
	private bool isOptScreen = false;
	GameObject bgMusic;
	void Awake(){
		DontDestroyOnLoad (gameObject);
		audioSource = GetComponent<AudioSource> ();
	}

	void Start () {
		bgMusic = GameObject.FindGameObjectWithTag("BGMusic");
		PlayerPrefManager.SetMasterVolume (audioSource.volume);
	}

	// Update is called once per frame
	void Update () {
		ChangeVolume( PlayerPrefManager.GetMasterVolume ());
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += PlayBGMusic;
	}

	void PlayBGMusic(Scene scene, LoadSceneMode mode){
		int level = scene.buildIndex;
		//check if the transition is between Option Scene & main scene
		if (level == 2) {
			isOptScreen = true;
			return;
		}
		if (level == 1 && isOptScreen) {return;}


		//If not, find the bg music object in scene to play
		bgMusic = GameObject.FindGameObjectWithTag("BGMusic");
		if(bgMusic){
			audioSource.clip = bgMusic.GetComponent<BackGroundMusic>().backgroundMusicClip;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}

	/*
	void MusicPlayer(Scene scene, LoadSceneMode mode)
	{
		int level = scene.buildIndex;

		if (level == 2) {
			isOptScreen = true;
			return;
		}
		if (level == 1 && isOptScreen) {return;}

		//Set music corresponding to level index normally
		AudioClip thisLevelMusic = levelClips [0];
		//set range of gameplay levels
		if (level >= 4 && level <= 8) {
			 thisLevelMusic = levelClips [4];
		} else{
			 thisLevelMusic = levelClips [level];
		} 

		if (thisLevelMusic) {
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		}else{
			Debug.Log("no music found");
		}
	}*/

	public void ChangeVolume(float volume)
	{
		audioSource.volume = volume;
	}

	public void PlayMusic(AudioClip musicToPlay){
		audioSource.clip = musicToPlay;
		audioSource.loop = false;
		audioSource.Play ();
	}

	/*
	void OnLevelWasLoaded(int level)
	{
		//Set music transitting between home and option not interrupted
		if (level == 2) {
			isOptScreen = true;
			return;
		}
		if (level == 1 && isOptScreen) {return;}

		//Set music corresponding to level index normally
		AudioClip thisLevelMusic = levelClips [0];
		//set range of gameplay levels
		if (level >= 4 && level <= 8) {
			 thisLevelMusic = levelClips [4];
		} else{
			 thisLevelMusic = levelClips [level];
		} 
		if (thisLevelMusic) {
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}*/


}
