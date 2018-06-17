using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectControlMethod : MonoBehaviour {

	public PlayerController thePlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//Detect Mouse Input
		//Click
		if (Input.GetMouseButton (0) || Input.GetMouseButton (1) || Input.GetMouseButton (2)) {
			thePlayer.useController = false;
		}

		//Detect Controller Input
		if (Input.GetAxisRaw ("RHorizontal") != 0.0f || Input.GetAxisRaw ("RVertical") != 0.0f) {
			thePlayer.useController = true;
		}

	}
}
