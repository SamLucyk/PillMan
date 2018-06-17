using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAccessScript : MonoBehaviour {

	private GameObject towerMenu;
	private GameObject menus;
	private TowerMenuController towerMenuController;

	// Use this for initialization
	void Start () {
		menus = GameObject.Find ("Menus");
		towerMenu = menus.FindObject ("TowerMenu");
		towerMenuController = towerMenu.GetComponent<TowerMenuController> ();
	}

	// Update is called once per frame
	void Update () {
		if (GlobalVariables.GAMEOVER && towerMenu.activeSelf) {
			towerMenu.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player"){
			towerMenu.SetActive (true);
			GlobalVariables.MENUSHOWING = true;
			towerMenuController.towerPad = gameObject;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			towerMenu.SetActive (false);
			GlobalVariables.MENUSHOWING = false;
		}
	}
}
