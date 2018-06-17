using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	private float speed;
	private float damageToGive;
	public float lifetime;
	private bool canDamage;
	public GameObject target;

	// Use this for initialization
	void Start () {
		canDamage = true;
		Destroy(gameObject, lifetime);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (target) {
			Vector3 targetPos = target.transform.position;
			transform.LookAt (targetPos);
		}
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Enemy" && canDamage) {
			canDamage = false;
			other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy (damageToGive);
		}
		if (other.gameObject.tag != "Player") {
			Destroy (gameObject);
		}
	}

	//Set

	public void setSpeed(float newSpeed){
		speed = newSpeed;
	}

	public void setDamage(float newDamage){
		damageToGive = newDamage; 
	}

	public void setTarget(GameObject newTarget){
		target = newTarget; 
	}



}
