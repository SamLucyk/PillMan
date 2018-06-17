using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenuController : MonoBehaviour {

	//Components
	private Button purchaseBodyBruteButton;
	private Button purchaseBodyColdButton;
	private Button purchaseBodyHotButton;
	public GameObject bruteBody;
	public GameObject coldBody;
	public GameObject hotBody;

	private Text bodyBruteCostText;
	private Text bodyColdCostText;
	private Text bodyHotCostText;
	private Text pointsText;

	private PlayerController playerController;

	//State
	private int bodyBruteCost;
	private int bodyColdCost;
	private int bodyHotCost;
	private int points;
	public GameObject towerPad;



	// Use this for initialization
	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		points = playerController.GetPoints ();

		purchaseBodyBruteButton = GameObject.Find ("TowerMenu/Background/BruteBody/Button").GetComponent<Button> ();
		purchaseBodyColdButton = GameObject.Find ("TowerMenu/Background/ColdBody/Button").GetComponent<Button> ();
		purchaseBodyHotButton = GameObject.Find ("TowerMenu/Background/HotBody/Button").GetComponent<Button> ();

		bodyBruteCostText = GameObject.Find ("TowerMenu/Background/BruteBody/Button/Text").GetComponent<Text> ();
		bodyColdCostText = GameObject.Find ("TowerMenu/Background/ColdBody/Button/Text").GetComponent<Text> ();
		bodyHotCostText = GameObject.Find ("TowerMenu/Background/HotBody/Button/Text").GetComponent<Text> ();
		pointsText = GameObject.Find ("TowerMenu/Background/Points").GetComponent<Text> ();

		bodyBruteCost = 100;
		bodyColdCost = 100;
		bodyHotCost = 100;
	}

	// Update is called once per frame
	void Update () {
		points = playerController.GetPoints ();
		pointsText.text = "$" + points;
		SetPurchaseButtons ();
	}
		
	public void PurchaseBody(GameObject body, int cost){
		playerController.SubtractPoints (cost);
		Instantiate (body, towerPad.transform.position, towerPad.transform.rotation);
		Destroy (towerPad);
		gameObject.SetActive (false);
	}

	public void PurchaseBodyBrute(){
		PurchaseBody (bruteBody, bodyBruteCost);
	}

	public void PurchaseColdBrute(){
		PurchaseBody (coldBody, bodyColdCost);
	}

	public void PurchaseHotBrute(){
		PurchaseBody (hotBody, bodyHotCost);
	}

	private void SetPurchaseButtons(){
		SetBodyBruteButton ();
		SetBodyColdButton ();
		SetBodyHotButton ();
	}

	private void SetBodyBruteButton(){

		if (bodyBruteCost > 0) {
			bodyBruteCostText.text = "$" + bodyBruteCost;
		} else {
			bodyBruteCostText.text = "Max Upgrade Reached";
		}

		if (points >= bodyBruteCost) {
			purchaseBodyBruteButton.interactable = true;
		} else {
			purchaseBodyBruteButton.interactable = false;
		}
	}

	private void SetBodyColdButton(){

		if (bodyColdCost > 0) {
			bodyColdCostText.text = "$" + bodyColdCost;
		} else {
			bodyColdCostText.text = "Max Upgrade Reached";
		}
			

		if (points >= bodyColdCost) {
			purchaseBodyColdButton.interactable = true;
		} else {
			purchaseBodyColdButton.interactable = false;
		}
	}

	private void SetBodyHotButton(){

		if (bodyHotCost > 0) {
			bodyHotCostText.text = "$" + bodyHotCost;
		} else {
			bodyHotCostText.text = "Max Upgrade Reached";
		}


		if (points >= bodyHotCost) {
			purchaseBodyHotButton.interactable = true;
		} else {
			purchaseBodyHotButton.interactable = false;
		}
	}
		
}
