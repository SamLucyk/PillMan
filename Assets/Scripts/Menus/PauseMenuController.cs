using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

	public GameObject mainMenuConfirmation;
	public GameObject exitGameConfirmation;

	public void Unpause(){
		Time.timeScale = 1.0f;
		GlobalVariables.PAUSED = false;
		gameObject.SetActive (false);
	}

	public void MainMenuPress(){
		mainMenuConfirmation.SetActive (true);
	}

	public void MainMenuNo(){
		mainMenuConfirmation.SetActive (false);
	}

	public void MainMenu(){
		Time.timeScale = 1.0f;
		SceneManager.LoadScene (0);
	}

	public void ExitGamePress(){
		exitGameConfirmation.SetActive (true);
	}

	public void ExitGameNo(){
		exitGameConfirmation.SetActive (false);
	}

	public void ExitGame(){
		Application.Quit ();
	}


}
