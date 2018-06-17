using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainMenuController : MonoBehaviour {

	//Components
	private Button upgradeShotSpeedButton;
	private Button upgradeShotDamageButton;
	private Button upgradeShotRateButton;

	private Text shotSpeedCostText;
	private Text shotDamageCostText;
	private Text shotRateCostText;

	private RectTransform shotSpeedImage;
	private RectTransform shotDamageImage;
	private RectTransform shotRateImage;

	private GunController gunController;
	private GameObject gun;
	private PlayerController playerController;
	private HealthManager playerHealthManager;
	private HealthManager heartHealthManager;

	//Settings
	private int maxShotDamageUpgrades;
	private int maxShotSpeedUpgrades;
	private int maxShotRateUpgrades;

	//State
	private int shotSpeedCost;
	private int shotDamageCost;
	private int shotRateCost;
	private int points;

	private int numShotDamageUpgrades;
	private int numShotSpeedUpgrades;
	private int numShotRateUpgrades;

	private float playerStartingHealth;
	private float heartStartingHealth;
	private float playerHealthFraction;
	private float heartHealthFraction;
	private RectTransform playerHealthDisplay;
	private RectTransform heartHealthDisplay;

	private int playerHeal10Cost;
	private int heartHeal10Cost;
	private int playerHeal100Cost;
	private int heartHeal100Cost;

	private Button playerHealButton10;
	private Button heartHealButton10;
	private Button playerHealButton100;
	private Button heartHealButton100;

	private Text playerHealText10;
	private Text heartHealText10;
	private Text playerHealText100;
	private Text heartHealText100;


	// Use this for initialization
	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		playerHealthManager = GameObject.Find ("Player").GetComponent<HealthManager> ();
		heartHealthManager = GameObject.Find ("Heart").GetComponent<HealthManager> ();

		playerStartingHealth = playerHealthManager.GetStartingHealth ();
		heartStartingHealth = heartHealthManager.GetStartingHealth ();
		playerHealthDisplay = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/HealPlayer/HealthBar/Health").GetComponent<RectTransform> ();
		heartHealthDisplay = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/HealHeart/HealthBar/Health").GetComponent<RectTransform> ();
		heartHealButton10 = GameObject.Find("BrainMenu/Background/ScrollZone/Body/HealHeart/Heal10").GetComponent<Button> ();
		heartHealButton100 = GameObject.Find("BrainMenu/Background/ScrollZone/Body/HealHeart/Heal100").GetComponent<Button> ();
		playerHealButton10 = GameObject.Find("BrainMenu/Background/ScrollZone/Body/HealPlayer/Heal10").GetComponent<Button> ();
		playerHealButton100 = GameObject.Find("BrainMenu/Background/ScrollZone/Body/HealPlayer/Heal100").GetComponent<Button> ();
		heartHealText10 = GameObject.Find("BrainMenu/Background/ScrollZone/Body/HealHeart/Heal10/Text").GetComponent<Text> ();
		heartHealText100 = GameObject.Find("BrainMenu/Background/ScrollZone/Body/HealHeart/Heal100/Text").GetComponent<Text> ();
		playerHealText10 = GameObject.Find("BrainMenu/Background/ScrollZone/Body/HealPlayer/Heal10/Text").GetComponent<Text> ();
		playerHealText100 = GameObject.Find("BrainMenu/Background/ScrollZone/Body/HealPlayer/Heal100/Text").GetComponent<Text> ();

		GetGameState ();

		upgradeShotSpeedButton = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/ShotSpeed/Button").GetComponent<Button> ();
		upgradeShotDamageButton = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/ShotDamage/Button").GetComponent<Button> ();
		upgradeShotRateButton = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/ShotRate/Button").GetComponent<Button> ();

		shotSpeedCostText = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/ShotSpeed/Button/Text").GetComponent<Text> ();
		shotDamageCostText = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/ShotDamage/Button/Text").GetComponent<Text> ();
		shotRateCostText = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/ShotRate/Button/Text").GetComponent<Text> ();

		shotSpeedImage = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/ShotSpeed/Background/ProgressBar").GetComponent<RectTransform> ();
		shotDamageImage = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/ShotDamage/Background/ProgressBar").GetComponent<RectTransform> ();
		shotRateImage = GameObject.Find ("BrainMenu/Background/ScrollZone/Body/ShotRate/Background/ProgressBar").GetComponent<RectTransform> ();

		gun = GameObject.Find ("Player/Gun");
		gunController = gun.GetComponent<GunController> ();

		shotSpeedCost = 150;
		shotDamageCost = 150;
		shotRateCost = 150;

		maxShotSpeedUpgrades = 5;
		maxShotDamageUpgrades = 5;
		maxShotRateUpgrades = 5;

		numShotDamageUpgrades = 0;
		numShotSpeedUpgrades = 0;
		numShotRateUpgrades = 0;
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		GetGameState ();
		SetUpgradeButtons ();
		SetHealButtons ();
		SetHealthBars ();
	}


	/// <summary>
	/// Upgrades the shot speed.
	/// </summary>
	public void UpgradeShotSpeed(){
		numShotSpeedUpgrades++;
		playerController.SubtractPoints (shotSpeedCost);
		gunController.upgradeShotSpeed ();
		shotSpeedCost = GetUpgradeCost (numShotSpeedUpgrades);
	}

	/// <summary>
	/// Upgrades the shot damage.
	/// </summary>
	public void UpgradeShotDamage(){
		numShotDamageUpgrades++;
		playerController.SubtractPoints (shotDamageCost);
		gunController.upgradeShotDamage ();
		shotDamageCost = GetUpgradeCost (numShotDamageUpgrades);
	}

	/// <summary>
	/// Upgrades the shot rate.
	/// </summary>
	public void UpgradeShotRate(){
		numShotRateUpgrades++;
		playerController.SubtractPoints (shotRateCost);
		gunController.upgradeShotRate ();
		shotRateCost = GetUpgradeCost (numShotRateUpgrades);
	}

	/// <summary>
	/// Sets the upgrade buttons.
	/// </summary>
	private void SetUpgradeButtons(){
		SetShotSpeedButton ();
		SetShotDamageButton ();
		SetShotRateButton ();
	}

	/// <summary>
	/// Sets the shot speed button.
	/// </summary>
	private void SetShotSpeedButton(){

		if (shotSpeedCost > 0) {
			shotSpeedCostText.text = "Upgrade $" + shotSpeedCost;
		} else {
			shotSpeedCostText.text = "Max Upgrade Reached";
		}

		float fraction = ((float) numShotSpeedUpgrades )/((float) maxShotSpeedUpgrades);
		shotSpeedImage.sizeDelta = new Vector2 (fraction * 290, 18);

		if (points >= shotSpeedCost && numShotSpeedUpgrades < maxShotSpeedUpgrades) {
			upgradeShotSpeedButton.interactable = true;
		} else {
			upgradeShotSpeedButton.interactable = false;
		}
	}

	/// <summary>
	/// Sets the shot damage button.
	/// </summary>
	private void SetShotDamageButton(){

		if (shotDamageCost > 0) {
			shotDamageCostText.text = "Upgrade $" + shotDamageCost;
		} else {
			shotDamageCostText.text = "Max Upgrade Reached";
		}


		float fraction = ((float)numShotDamageUpgrades) / ((float)maxShotDamageUpgrades);
		shotDamageImage.sizeDelta = new Vector2 (fraction * 290, 18);
		
		if (points >= shotDamageCost && numShotDamageUpgrades < maxShotDamageUpgrades) {
			upgradeShotDamageButton.interactable = true;
		} else {
			upgradeShotDamageButton.interactable = false;
		}
	}

	/// <summary>
	/// Sets the shot rate button.
	/// </summary>
	private void SetShotRateButton(){

		if (shotRateCost > 0) {
			shotRateCostText.text = "Upgrade $" + shotRateCost;
		} else {
			shotRateCostText.text = "Max Upgrade Reached";
		}

		float fraction = ((float)numShotRateUpgrades) / ((float)maxShotRateUpgrades);
		shotRateImage.sizeDelta = new Vector2 (fraction * 290, 18);

		if (points >= shotRateCost && numShotRateUpgrades < maxShotRateUpgrades) {
			upgradeShotRateButton.interactable = true;
		} else {
			upgradeShotRateButton.interactable = false;
		}
	}

	/// <summary>
	/// Gets the upgrade cost.
	/// </summary>
	/// <returns>The upgrade cost.</returns>
	/// <param name="numUpgrades">Number of upgrades.</param>
	private int GetUpgradeCost(int numUpgrades){
		switch ( numUpgrades )
		{
		case 1:
			return 500;
		case 2:
			return 1000;
		case 3:
			return 2000;
		case 4:
			return 3000;
		case 5:
			return -1;
		default:
			return 150;
		}
	}

	/// <summary>
	/// Sets the health bars.
	/// </summary>
	private void SetHealthBars(){
		heartHealthDisplay.sizeDelta = new Vector2 (heartHealthFraction * 100, 20);
		playerHealthDisplay.sizeDelta = new Vector2 (playerHealthFraction * 100, 20);
	}

	private void SetHealCosts(){
		playerHeal10Cost = Mathf.RoundToInt(.1f * playerStartingHealth);
		playerHealText10.text = "Heal 10% $" + playerHeal10Cost;

		playerHeal100Cost = Mathf.RoundToInt(playerStartingHealth - (playerHealthFraction * playerStartingHealth));
		playerHealText100.text = "Heal 100% $" + playerHeal100Cost;

		heartHeal10Cost = Mathf.RoundToInt(.1f * heartStartingHealth);
		heartHealText10.text = "Heal 10% $" + heartHeal10Cost;

		heartHeal100Cost = Mathf.RoundToInt(heartStartingHealth - (heartHealthFraction * heartStartingHealth));
		heartHealText100.text = "Heal 100% $" + heartHeal100Cost;
	}

	/// <summary>
	/// Sets the heal buttons.
	/// </summary>
	private void SetHealButtons(){
		SetHealCosts ();
		if (points >= playerHeal10Cost && playerHealthFraction < .9) {
			playerHealButton10.interactable = true;
		} else {
			playerHealButton10.interactable = false;
		}

		if (points >= playerHeal100Cost && playerHealthFraction < 1) {
			playerHealButton100.interactable = true;
		} else {
			playerHealButton100.interactable = false;
		}

		if (points >= heartHeal10Cost && heartHealthFraction < .9) {
			heartHealButton10.interactable = true;
		}  else {
			heartHealButton10.interactable = false;
		}

		if (points >= heartHeal100Cost && heartHealthFraction < 1) {
			heartHealButton100.interactable = true;
		} else {
			heartHealButton100.interactable = false;
		}
	}

	/// <summary>
	/// Gets the state of the game.
	/// </summary>
	private void GetGameState() {
		points = playerController.GetPoints ();
		playerHealthFraction = playerHealthManager.GetHealthFraction ();
		heartHealthFraction = heartHealthManager.GetHealthFraction ();
	}

	/// <summary>
	/// Heals the heart.
	/// </summary>
	/// <param name="percent">Percent.</param>
	public void HealHeart(float percent){
		
		int cost = heartHeal100Cost;
		if (percent == .1f) {
			cost = heartHeal10Cost;
		}

		playerController.SubtractPoints(cost);
		heartHealthManager.Heal (percent);
	}

	/// <summary>
	/// Heals the player.
	/// </summary>
	/// <param name="percent">Percent.</param>
	public void HealPlayer(float percent){

		int cost = playerHeal100Cost;
		if (percent == .1f) {
			cost = playerHeal10Cost;
		}

		playerController.SubtractPoints(cost);
		playerHealthManager.Heal (percent);
	}
}