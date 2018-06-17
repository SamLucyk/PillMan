using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
	public enum Manage {Heart, Player};
	public Manage type;
	public float startingHealth;
	private float currentHealth;
	private bool invincible;
	private Renderer rend;
	private float healthFraction;
	public float flashLength;
	private float flashCounter;
	private Color storedColor;
	private GameObject healthBar;
	private RectTransform healthDisplay;
	private bool noDamage;

	void Start () {

		noDamage = true;

		if (type == Manage.Heart) {
			StartHeart ();
		} else if (type == Manage.Player) {
			StartPlayer ();
		}

	}

	// Update is called once per frame
	void Update () {
		SetHealthFraction();
		SetHealthBar ();
		if (currentHealth <= 0) {
			EndGame ();
			if (type == Manage.Player && !GlobalVariables.PLAYERDEAD) {
				KillPlayer ();
			} else if (type == Manage.Heart && !GlobalVariables.HEARTDEAD) {
				KillHeart ();
			}

		}

		if (flashCounter > 0) {
			flashCounter -= Time.deltaTime;
			if (flashCounter <= 0) {
				invincible = false;
				rend.material.SetColor ("_Color", storedColor);
			}
		}

	}

	public void Hurt(int damageAmount){
		

		if (!invincible) {
			
			if (noDamage) {
				noDamage = false;
				if (type == Manage.Player) {
					GlobalVariables.PLAYERNODAMAGE = false;
				} else if (type == Manage.Heart) {
					GlobalVariables.HEARTNODAMAGE = false;
				}
			}

			currentHealth -= damageAmount;
			flashCounter = flashLength;
			invincible = true;
			rend.material.SetColor ("_Color", Color.white);
		}
	}

	private void SetHealthBar (){
		healthDisplay.sizeDelta = new Vector2 (healthFraction * 100, 20);
	}

	private void SetHealthFraction(){
		healthFraction = currentHealth / startingHealth;
	}

	public float GetHealthFraction(){
		return healthFraction;
	}

	private void StartHeart () {
		currentHealth = startingHealth;
		rend = transform.Find("Heart").gameObject.GetComponent<Renderer> ();
		storedColor = rend.material.GetColor ("_Color");
		healthBar = GameObject.Find ("HUD/HeartHealthBar");
		healthDisplay = healthBar.transform.Find ("Health").GetComponent<RectTransform> (); 
	}

	// Use this for initialization
	private void StartPlayer () {
		currentHealth = startingHealth;
		rend = GetComponent<Renderer> ();
		storedColor = rend.material.GetColor ("_Color");
		healthBar = GameObject.Find ("HUD/PlayerHealthBar");
		healthDisplay = healthBar.transform.Find ("Health").GetComponent<RectTransform> ();
	}

	public void Heal(float percent) {
		float addHealth = percent * startingHealth;
		currentHealth = Mathf.Clamp(currentHealth + addHealth, 0F, startingHealth);
	}

	public float GetStartingHealth(){
		return startingHealth;
	}

	private void KillPlayer(){
		GlobalVariables.PLAYERDEAD = true;
	}

	private void KillHeart(){
		GlobalVariables.HEARTDEAD = true;
	}

	private void EndGame(){
		GlobalVariables.GAMEOVER = true;
	}

}
