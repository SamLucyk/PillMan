using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenuController : MonoBehaviour {

	public GameObject helpMenu;
	public GameObject gameOverMenu;
	public GameObject pauseMenu;


	private bool reset;

	// Use this for initialization
	void Start () {
		reset = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey ("h")) {
			helpMenu.SetActive (true);
		} else {
			helpMenu.SetActive (false);
		}
		if (GlobalVariables.GAMEOVER) {
			gameOverMenu.SetActive(true);
		} 
		if (reset && !GlobalVariables.GAMEOVER) {
			SceneManager.LoadScene(1);
		}

		if (Input.GetKeyDown ("p") && !GlobalVariables.GAMEOVER) {
			if (GlobalVariables.PAUSED) {
				GlobalVariables.MENUSHOWING = false; 
				Time.timeScale = 1.0f;
				GlobalVariables.PAUSED = false;
				pauseMenu.SetActive (false);
			} else {
				GlobalVariables.MENUSHOWING = true; 
				Time.timeScale = 0.0f;
				GlobalVariables.PAUSED = true;
				pauseMenu.SetActive (true);
			}

		} 
	}

	public void ResetGame(){
		print ("Resetting");
		GlobalVariables.MENUSHOWING = false; 
		GlobalVariables.PLAYERDEAD = false; 
		GlobalVariables.HEARTDEAD = false; 
		GlobalVariables.GAMEOVER = false; 
		reset = true;
	}



}
