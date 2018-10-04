using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {
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

	public void ChangeVolume(float volume)
	{
		audioSource.volume = volume;
	}

	public void PlayMusic(AudioClip musicToPlay){
		audioSource.clip = musicToPlay;
		audioSource.loop = false;
		audioSource.Play ();
	}
}
