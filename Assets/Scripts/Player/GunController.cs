using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public bool isFiring;
	public BulletController bullet;
	private int damageToGive;
	private float bulletSpeed;
	private float timeBetweenShots;
	private float shotCounter;
	public Transform firePoint;
	private AudioSource shootSound;

	// Use this for initialization
	void Start () {
		shootSound = GetComponent<AudioSource> ();
		damageToGive = 1;
		bulletSpeed = 30;
		timeBetweenShots = .5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (isFiring) {
			shotCounter -= Time.deltaTime;
			if (shotCounter <= 0) {
				shootSound.Play ();
				shotCounter = timeBetweenShots;
				BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation) as BulletController;
				newBullet.setSpeed(bulletSpeed);
				newBullet.setDamage(damageToGive);
			}
		} else {
			shotCounter -= Time.deltaTime;;
		}
	}

	//GET
	public float getShotSpeed(){
		return bulletSpeed;
	}

	public float getShotRate(){
		return timeBetweenShots;
	}

	public int getShotDamage(){
		return damageToGive;
	}

	//SET
	public void upgradeShotSpeed(){
		bulletSpeed += 15f;
	}

	public void upgradeShotRate(){
		timeBetweenShots -= .05f;
	}

	public void upgradeShotDamage(){
		damageToGive += 2;
	}
}
