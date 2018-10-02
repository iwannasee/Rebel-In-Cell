using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour {
	public float fadeOutTime;
	private Image fadePanel;
	private Color currentColor = new Color(0f,0f,0f,0f);
	private bool fadable = false;
	private string levelNameToLoad = "";
	private OptionController optController;
	// Use this for initialization
	void Start () {
		optController = GameObject.FindObjectOfType<OptionController> ();
		fadePanel = GetComponent<Image> ();
		fadePanel.color = currentColor;
	}

	// Update is called once per frame
	void Update () {
		if (fadable) {
			float alphaChange = Time.deltaTime / fadeOutTime;
			currentColor.a += alphaChange; 
			fadePanel.color = currentColor;
			if (fadePanel.color.a >= fadeOutTime ) {
				if(levelNameToLoad == "02 MapSelect"){
					int worldIndex = PlayerProgress.presentWorldIndex;
					SceneManager.LoadScene ("02 MapSelect World "+ MapDictionary.worldList[worldIndex]);
				}else{
					SceneManager.LoadScene (levelNameToLoad);
				}
			}
		}
	}

	public void FadeOutAndLoadLevel(string levelName){
		fadePanel.enabled = true;
		fadable = true;
		levelNameToLoad = levelName;

	}

	public void FadeOutSaveExit(string levelName){
		optController.SaveOption ();
		FadeOutAndLoadLevel (levelName);
	}

}
