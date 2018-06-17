using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnDeath : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (GlobalVariables.PLAYERDEAD) {
			gameObject.SetActive (false);
		}
	}
}
