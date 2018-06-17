using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	//ENEMY TYPES:
	public enum enemyType {Germ, BigGerm, Spinner, BigSpinner};
	public enemyType type;
	public int damageToGive;
	private float startingSpeed;
	private List<Renderer> effectRends = new List<Renderer>();
	private List<Color> effectOrginalColors = new List<Color> ();
	private GameObject thePlayer;
	private GameObject theHeart;
	private GameObject target;
	private float playerDist;
	private float heartDist;
	private bool touchingPlayer;
	private bool touchingHeart;
	private HealthManager heartController;
	private HealthManager playerController;
	private EnemyHealthManager enemyHealthController;
	NavMeshAgent agent;

	//State
	private bool frozen;
	private float frozenTill;

	private bool burning;
	private float burningTill;
	private float timeToBurn;
	private float burnDamage;

	private Color orange = new Color (1f, .55f, 0);
	private Color teal = new Color (.8f, .90f, 1f);
	private List<Color> colors = new List<Color>();

	// Use this for initialization
	void Start () {
		GetEffectParts ();
		thePlayer = GameObject.Find("Player");
		theHeart = GameObject.Find("Heart");
		heartController = theHeart.GetComponent<HealthManager> ();
		playerController = thePlayer.GetComponent<HealthManager> ();
		enemyHealthController = GetComponent<EnemyHealthManager> ();
		target = thePlayer;
		touchingPlayer = false;
		touchingHeart = false;
		agent = GetComponent<NavMeshAgent> ();
		startingSpeed = agent.speed;
		colors.Add (orange);
		colors.Add (teal);
		burnDamage = 0;
	}

	void FixedUpdate(){
		// Basic Movement
		if (type == enemyType.Germ || type == enemyType.Germ) {
			if (touchingPlayer) {
				agent.enabled = false;
			} else {
				agent.enabled = true;
			}
			Vector3 targetPos = target.transform.position;
			targetPos.y = transform.position.y;
			transform.LookAt (targetPos);
		} 

		//Spinner Movement
		if (type == enemyType.Spinner || type == enemyType.BigSpinner) {
			Vector3 targetPos = target.transform.position;
			targetPos.y = transform.position.y;
			transform.Rotate (new Vector3 (0, 240, 0) * Time.deltaTime);
		}
	}
	
	// Update is called once per frame
	void Update () {
		playerDist = Vector3.Distance(thePlayer.transform.position, transform.position);
		heartDist = Vector3.Distance(theHeart.transform.position, transform.position);

		if ((playerDist < 10.0f) && (playerDist < heartDist - 15f)) {
			target = thePlayer;
		} else {
			target = theHeart;
		}

		if (GlobalVariables.HEARTDEAD) {
			target = thePlayer;
		} 

		if (frozen && Time.time >= frozenTill) {
			frozen = false;
			SetEffectParts();
			agent.speed = startingSpeed;
		}

		if (burning){
			if (Time.time >= burningTill) {
				burning = false;
				burnDamage = 0f;
				SetEffectParts ();
			} else {
				if (Time.time >= timeToBurn) {
					enemyHealthController.HurtEnemy (burnDamage);
					timeToBurn += 2.0f;
				}
			}
		}

		if (agent.enabled) {
			agent.SetDestination (target.transform.position);
		}

		if (touchingPlayer) {
			playerController.Hurt (damageToGive);
		}

		if (touchingHeart) {
			heartController.Hurt (damageToGive);
		}
	}
		
	public void OnCollisionEnter(Collision other){

		if (other.gameObject.CompareTag("Player")) {
			touchingPlayer = true;
			playerController.Hurt (damageToGive);
		}

		if (other.gameObject.CompareTag("Heart")) {
			touchingHeart = true;
			heartController.Hurt(damageToGive);
		}

	}

	public void OnCollisionExit(Collision other){
		if (other.gameObject.CompareTag("Player")) {
			touchingPlayer = false;
		}

		if (other.gameObject.CompareTag("Heart")) {
			touchingHeart = false;
		}
	}

	public void Freeze(float freezeDuration, float freezeStrength){
		frozen = true;
		frozenTill = Time.time + freezeDuration;
		agent.speed = agent.speed * freezeStrength;
		SetEffectParts ();
	}

	public void Burn(float burnDuration, float burnStrength){
		burning = true;
		burningTill = Time.time + burnDuration;
		timeToBurn = Time.time + 2.0f;
		burnDamage = burnDamage + burnStrength;
		SetEffectParts ();
	}

	private void GetEffectParts(){
		foreach(Transform child in transform){
			if (child.gameObject.CompareTag("ShowEffect")) {
				Renderer rend = child.gameObject.GetComponent<Renderer> ();
				effectRends.Add(rend);
				effectOrginalColors.Add (rend.material.color);
			}
		}
	}

	public void SetEffectParts(){
		
		if (frozen && burning) {
			int index = 0;
			foreach (Renderer rend in effectRends) {
				rend.material.SetColor ("_Color", colors[index]);
				if (index == 0) { index++; } else { index--; }
			}

		}

		else if (frozen) {
			foreach (Renderer rend in effectRends) {
				rend.material.SetColor ("_Color", teal);
			}

		}

		else if (burning) {
			foreach (Renderer rend in effectRends) {
				rend.material.SetColor ("_Color", orange);
			}	
		}

		else {
			for(int i = 0; i < effectRends.Count ; i++) {
				effectRends[i].material.SetColor ("_Color", effectOrginalColors[i]);
			}
		}
	}
}