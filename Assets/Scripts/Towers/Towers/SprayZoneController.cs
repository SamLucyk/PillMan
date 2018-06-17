using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayZoneController : MonoBehaviour {

	private Transform sprayZone;
	private float sprayZoneDuration;
	private float sprayZoneCoolDown;
	private ParticleSystem sprayParticles;
	private float timeToStart;
	private float timeToStop;
	private float targetDist;
	private float thisTargetDist;
	private GameObject target;
	private float zoneLength;
	private GameObject firePoint;
	private int level;

	private bool isPlaying;
	private bool isTargetInRange;
	private GameObject[] enemies;

	// Use this for initialization
	void Start () {
		level = 1;
		SetSprayZone ();
		firePoint = transform.Find ("FirePoint").gameObject;

		sprayParticles = firePoint.GetComponent<ParticleSystem> ();
		sprayZoneDuration = 4.0f;
		sprayZoneCoolDown = 5.0f;

		isPlaying = false;
		StopSpray ();
	}

	// Update is called once per frame
	void Update () {

		findTarget ();

		if (isPlaying && Time.time >= timeToStop) {
			StopSpray ();
		}

		else if (!isPlaying && Time.time >= timeToStart && isTargetInRange) {
			StartSpray ();
		}

	}

	private void findTarget(){
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

			if (targetDist < zoneLength + 5) {
				isTargetInRange = true;
			} 
		}
	}

	private void StopSpray(){
		isPlaying = false;
		sprayParticles.Stop ();
		sprayZone.gameObject.SetActive (false);
		timeToStart = Time.time + sprayZoneCoolDown;
	}

	private void StartSpray(){
		isPlaying = true;
		sprayParticles.Play ();
		sprayZone.gameObject.SetActive (true);
		timeToStop = Time.time + sprayZoneDuration;
	}

	public void Upgrade(){
		level++;
		sprayZoneCoolDown *= .8f;
		sprayZoneDuration *= 1.25f;
		IncreaseSprayZone ();
	}

	private void IncreaseSprayZone(){
		firePoint.transform.localScale = new Vector3 (firePoint.transform.localScale.x, firePoint.transform.localScale.y, firePoint.transform.localScale.z * 1.25f);
	}

	private void SetSprayZone(){
		sprayZone = transform.Find("Level"+ level + "/"+"SprayZone");
		zoneLength = sprayZone.localScale.z;
	}


}
