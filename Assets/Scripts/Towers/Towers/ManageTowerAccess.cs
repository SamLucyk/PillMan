using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTowerAccess : MonoBehaviour {

	private GameObject menus;
	private GameObject manageTowerMenu;
	private ManageTowerMenuController manageTowerMenuController;

	// Use this for initialization
	void Start () {
		menus = GameObject.Find ("Menus");
		manageTowerMenu = menus.FindObject ("ManageTowerMenu");
		manageTowerMenuController = manageTowerMenu.GetComponent<ManageTowerMenuController> ();
	}

	// Update is called once per frame
	void Update () {
		if (GlobalVariables.GAMEOVER && manageTowerMenu.activeSelf) {
			manageTowerMenu.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player"){
			manageTowerMenu.SetActive (true);
			GameObject towerParent = transform.parent.gameObject;
			GameObject tower = towerParent.transform.Find ("Sphere").gameObject;
			manageTowerMenuController.SetTower (tower);
			GlobalVariables.MENUSHOWING = true;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			manageTowerMenu.SetActive (false);
			GlobalVariables.MENUSHOWING = false;
		}
	}
}
