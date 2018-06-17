using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainAccessScript : MonoBehaviour {

	public GameObject brainMenu;

	void Update(){
		if (GlobalVariables.GAMEOVER && brainMenu.activeSelf) {
			brainMenu.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player"){
			brainMenu.SetActive (true);
			GlobalVariables.MENUSHOWING = true;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			brainMenu.SetActive (false);
			GlobalVariables.MENUSHOWING = false;
		}
	}
}
