using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody myRigidBody;
	private int points;
	private Vector3 moveInput;
	private Camera mainCamera;
	private Text pointsText;
	private Text brainPointsText;
	private int startingPoints;
	public Texture2D cursorTexture;

	public GunController theGun;
	private bool playerKilled = false;
	public bool useController;
	public float gravity = 20.0F;
	public GameObject brainPoints;

	void Awake(){
		SetStartingPoints ();
		points = startingPoints;
		pointsText = GameObject.Find ("HUD/PlayerPoints").GetComponent<Text>();
		brainPointsText = brainPoints.GetComponent<Text>();
		GlobalVariables.PLAYERDEAD = false;
		GlobalVariables.GAMEOVER = false;
	}

	// Use this for initialization
	void Start () {
		Cursor.SetCursor (cursorTexture, new Vector2(16,-16), CursorMode.Auto);
		myRigidBody = GetComponent<Rigidbody> ();
		mainCamera = FindObjectOfType<Camera> ();
	}

	void Update (){
		if (!playerKilled && GlobalVariables.PLAYERDEAD) {
			print ("Killing Player");
			KillPlayer ();
		}
	}

	// Fixed Update called once per frame for physics update
	void FixedUpdate () {
		if (!GlobalVariables.PLAYERDEAD) {
			if (!useController) {
				Ray cameraRay = mainCamera.ScreenPointToRay (Input.mousePosition);
				Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
				float rayLength;

				if (groundPlane.Raycast (cameraRay, out rayLength)) {
					Vector3 pointToLook = cameraRay.GetPoint (rayLength);
					Debug.DrawLine (cameraRay.origin, pointToLook, Color.blue);
					transform.LookAt (new Vector3 (pointToLook.x, transform.position.y, pointToLook.z));
				}

				if (!GlobalVariables.HEARTDEAD) {
					// 0 is left click
					if (Input.GetMouseButtonDown (0)) {
						theGun.isFiring = true;
					}
					if (Input.GetMouseButtonUp (0)) {
						theGun.isFiring = false;
					}
				} else {
					theGun.isFiring = false;
				}

			}

			// Rotate with Controller 
			if (useController) {

				Vector3 playerDirection = Vector3.right * Input.GetAxisRaw ("RHorizontal") + Vector3.forward * -Input.GetAxisRaw ("RVertical");
				if (playerDirection.sqrMagnitude > 0.0f) {
					transform.rotation = Quaternion.LookRotation (playerDirection, Vector3.up);
				}

				// 0 is left click
				if (Input.GetKeyDown (KeyCode.Joystick1Button7)) {
					theGun.isFiring = true;
				}
				if (Input.GetKeyUp (KeyCode.Joystick1Button7)) {
					theGun.isFiring = false;
				}

			}


			moveInput = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			moveInput = Camera.main.transform.TransformDirection (moveInput);
			myRigidBody.velocity = moveInput * Time.deltaTime * moveSpeed * 7;
		} 

	}

	private void KillPlayer(){
		playerKilled = true;
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = false;
		rb.constraints = RigidbodyConstraints.None;
		rb.mass = .5f;

		GameObject[] beads = GameObject.FindGameObjectsWithTag("FallOffPlayer");

		foreach (GameObject bead in beads)
		{
			bead.AddComponent<SphereCollider> ();
			bead.AddComponent<Rigidbody> ();
		}
	}

	public void AddPoints(int amount){
		points += amount;
		pointsText.text = "$" + points;
		brainPointsText.text = "$" + points;
	}

	public void SubtractPoints(int amount){
		points -= amount;
		pointsText.text = "$" + points;
		brainPointsText.text = "$" + points;
	}

	public int GetPoints(){
		return points;
	}

	private void SetStartingPoints(){
		if (GlobalVariables.DIFFICULTY == 0) {
			startingPoints = 80;
		}
		if (GlobalVariables.DIFFICULTY == 1) {
			startingPoints = 25;
		}
		if (GlobalVariables.DIFFICULTY == 2) {
			startingPoints = 0;
		}
	}
}
