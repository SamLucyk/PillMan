using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementAlertController : MonoBehaviour {

	public GameObject noDamageAchievement;
	private GameObject currentAchievement;
	private WaveNoDamageAcheivementController currentNoDamageController;
	private string currentKey;

	public void DisplayNoDamageAchievement(string type, string lvl, string wave, string diff){

		currentAchievement = Instantiate (noDamageAchievement, transform);
		currentNoDamageController = currentAchievement.GetComponent<WaveNoDamageAcheivementController> ();
		currentAchievement.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		currentNoDamageController.SetType (type);
		currentNoDamageController.SetData (lvl, wave, diff);
		currentNoDamageController.FlashData ();

	}

	public void SetAchievement(string template, string type, string lvl, string wave, string diff){

		DisplayNoDamageAchievement (type, lvl, wave, diff);
		currentKey = string.Format (template, lvl, wave, diff);
		PlayerPrefs.SetInt (currentKey, 1);

	}


}
