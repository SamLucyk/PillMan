using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

	private GameObject target;
	private bool isFiring;
	private float range;
	private float shotCounter;
	private float timeBetweenShots;
	public Transform firePoint;
	public BulletController bullet;
	private float bulletSpeed;
	private float damageToGive;
	private GameObject[] enemies;
	private float targetDist;
	private float thisTargetDist;
	private bool isTargetInRange;

	// Use this for initialization
	void Start () {
		damageToGive = 1;
		bulletSpeed = 80;
		range = 40;
		timeBetweenShots = 1.25f;
		isFiring = true;
	}

	// Update is called once per frame
	void Update () {

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		FindTarget ();
		if (isTargetInRange) {
			isFiring = true;
		} else {
			isFiring = false;
		}
		shotCounter -= Time.deltaTime;
		if (isFiring && shotCounter <= 0) {
			shotCounter = timeBetweenShots;
			BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
			newBullet.setSpeed(bulletSpeed);
			newBullet.setDamage(damageToGive);
			newBullet.setTarget (target);
		} 
	}

	private void FindTarget(){
		isTargetInRange = false;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (enemies.Length > 0) {
			target = enemies [0];
			targetDist = Vector3.Distance (target.transform.position, transform.position);

			foreach (GameObject enemy in enemies) {
				thisTargetDist = Vector3.Distance (enemy.transform.position, transform.position);
				if (thisTargetDist < targetDist) {
					target = enemy;
					targetDist = thisTargetDist;
				}
			}

			Vector3 targetPos = target.transform.position;
			transform.LookAt (targetPos);

			if (targetDist < range) {
				isTargetInRange = true;
			} 
		}
	}

	public void Upgrade(){
		timeBetweenShots *= .70f;
		damageToGive *= 2f;
		range *= 1.3f;
	}
}
