using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour {

	public GameObject resetPrompt;
	public GameObject resetConfirm;
	
	public void ResetDataPress(){
		resetPrompt.SetActive (true);
	}

	public void ResetConfirm(){
		PlayerPrefs.DeleteAll ();
		resetPrompt.SetActive (false);
		resetConfirm.SetActive (true);
	}

	public void ResetDeny(){
		resetPrompt.SetActive (false);
	}

	public void ResetConfirmOk(){
		resetConfirm.SetActive (false);
	}

}
