using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	private const float Y_ANGLE_MIN = 15.0f;
	private const float Y_ANGLE_MAX = 30.0f;
	private const float MIN_DIST  = 13f;
	private const float MAX_DIST  = 24f;

	public Transform lookAt;
	public Transform camTransform;

	private float distance = 18.0f;
	private float currentX = 0.0f;
	private float currentY= 0.0f;
	private float sensitivityX = 10.0f;
	private float sensitivityY = 10.0f;

	private void Start(){
		camTransform = transform;
	}

	private void Update(){

		if (!GlobalVariables.MENUSHOWING) {
			distance += Input.GetAxis ("Mouse ScrollWheel");
			distance = Mathf.Clamp (distance, MIN_DIST, MAX_DIST);
		}
		
		if (Input.GetKey ("q")) {
			currentX -= 2.5f;
		} if (Input.GetKey ("e")) {
			currentX += 2.5f;
		}


		if (Input.GetMouseButton (1)) {

			currentX += Input.GetAxis ("Mouse X") * sensitivityX;
			currentY += -Input.GetAxis ("Mouse Y") * sensitivityY;

		}

		currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}

	private void LateUpdate(){
		Vector3 dir = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt (lookAt.position);
	}
}
