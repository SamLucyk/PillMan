using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour {

	public float health;
	public string displayName;
	private float currentHealth;
	public float flashLength;

	private float flashCounter;
	private Renderer rend;
	private Color storedColor;
	private Color healthColor;
	private List<Renderer> eyeRends = new List<Renderer>();
	private AudioSource hitSound;

	private GameObject healthBar;
	private Text enemyNameDisplay;
	private RectTransform enemyHealthDisplay;

	private PlayerController player;

	// Use this for initialization
	void Start () {
		currentHealth = health;
		rend = GetComponent<Renderer> ();
		storedColor = rend.material.GetColor ("_Color");
		GetEyeRends ();
		player = GameObject.Find ("Player").GetComponent<PlayerController> ();
		hitSound = GetComponent<AudioSource> ();
		healthBar = GameObject.Find ("HUD/EnemyHealthBar");
		enemyHealthDisplay = healthBar.transform.Find ("Health").GetComponent<RectTransform> (); 
		enemyNameDisplay = healthBar.transform.Find ("Enemy Name").GetComponent<Text> (); 
	}
	
	// Update is called once per frame
	void Update () {

		if (currentHealth <= 0) {
			player.AddPoints((int) health * 3);
			Destroy (gameObject);
		}

		if (flashCounter > 0) {
			flashCounter -= Time.deltaTime;
			if (flashCounter <= 0) {
				rend.material.SetColor ("_Color", storedColor);
			}
		}

	}

	public void HurtEnemy(float damage){
		
		currentHealth -= damage;
		flashCounter = flashLength;
		rend.material.SetColor ("_Color", Color.red);
		SetEyes ();
		hitSound.Play();
		SetHealthBar ();

	}

	public void SetHealthColor(){
		float healthFraction = currentHealth / health;
		if (healthFraction < .25){
			healthColor = Color.red;
		} else if (healthFraction < .5){
			healthColor = new Color(1f,.55f,0);
		} else if (healthFraction < .75) {
			healthColor = Color.yellow;
		} else {
			healthColor = Color.white;
		}
	}

	public void SetHealthBar(){
		float healthFraction = currentHealth / health;
		enemyNameDisplay.text = displayName;
		enemyHealthDisplay.sizeDelta = new Vector2 (healthFraction * 100, 10);
	}

	public void GetEyeRends(){
		foreach(Transform child in transform){
			if (child.gameObject.tag == "Eye") {
				eyeRends.Add(child.gameObject.GetComponent<Renderer>());
			}
		}
	}

	public void SetEyes(){
		SetHealthColor ();
		foreach(Renderer eyeRend in eyeRends){
			eyeRend.material.SetColor ("_Color", healthColor);
		}
	}
}
