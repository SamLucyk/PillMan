using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour {

	private bool heartKilled;
	public GameObject brokenHeart;

	// Use this for initialization
	void Awake () {
		heartKilled = false;
		GlobalVariables.HEARTDEAD = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GlobalVariables.HEARTDEAD) {
			transform.Rotate (new Vector3 (0, 50, 0) * Time.deltaTime);
		} else if (!heartKilled) {
			BreakHeart ();
		}
	}


	private void BreakHeart(){
		Instantiate (brokenHeart, gameObject.transform.position, gameObject.transform.rotation);
		gameObject.SetActive (false);
	}
}
