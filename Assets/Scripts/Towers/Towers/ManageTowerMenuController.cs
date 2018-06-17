using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageTowerMenuController : MonoBehaviour {
	
	//Components
	private Button upgradeButton;
	private Text upgradeCostText;
	private Text towerTitle;
	private Text levelText;
	private Text pointsText;
	private Image towerImage;
	private Image progressPicture;
	private RectTransform progressBar;

	private PlayerController playerController;
	private int maxLevel;

	//State
	private int upgradeCost;
	private int points;
	private int level;
	public GameObject tower;
	public TowerManager towerManager;

	// Use this for initialization
	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		points = playerController.GetPoints ();

		upgradeButton = GameObject.Find ("ManageTowerMenu/Background/Body/Button").GetComponent<Button> ();
		upgradeCostText = GameObject.Find ("ManageTowerMenu/Background/Body/Button/Text").GetComponent<Text> ();
		towerTitle = GameObject.Find ("ManageTowerMenu/Background/Body").GetComponent<Text> ();
		levelText = GameObject.Find ("ManageTowerMenu/Background/Level").GetComponent<Text> ();
		pointsText = GameObject.Find ("ManageTowerMenu/Background/Points").GetComponent<Text> ();
		towerImage = GameObject.Find ("ManageTowerMenu/Background/Body/Image").GetComponent<Image> ();
		progressBar = GameObject.Find ("ManageTowerMenu/Background/Progress/Bar").GetComponent<RectTransform> ();
		progressPicture = GameObject.Find ("ManageTowerMenu/Background/Progress/Bar").GetComponent<Image> ();

		maxLevel = 3;
	}

	// Update is called once per frame
	void Update () {
		points = playerController.GetPoints ();
		SetMenu ();
	}

	void SetMenu() {
		pointsText.text = "$" + points;
		towerManager = tower.GetComponent<TowerManager> ();
		towerTitle.text = towerManager.GetTitle ();
		towerImage.sprite = towerManager.GetImage ();
		level = towerManager.GetLevel ();
		SetCost ();
		levelText.text = "Level " + level;
		SetButton ();
		SetProgressBar ();
	}
		
	void SetCost(){
		if (level == 1) {
			upgradeCost = 300;
		} else if (level == 2) {
			upgradeCost = 600;
		} else {
			upgradeCost = -1;
		}

		if (upgradeCost > 0) {
			upgradeCostText.text = "Upgrade $" + upgradeCost;
		} else {
			upgradeCostText.text = "Max Level Reached";
		}
	}

	void SetButton(){
		if (upgradeCost > 0 && points >= upgradeCost) {
			upgradeButton.interactable = true;
		} else {
			upgradeButton.interactable = false;
		}
	}

	public void SetTower(GameObject newTower){
		tower = newTower;
	}

	private void SetProgressBar(){
		progressPicture.material = towerManager.GetMaterial ();
		float fraction = ((float)level) / ((float)maxLevel);
		progressBar.sizeDelta = new Vector2 (fraction * 195, 22);
	}

	public void Upgrade(){
		towerManager.Upgrade ();
		playerController.SubtractPoints (upgradeCost);
	}

}
