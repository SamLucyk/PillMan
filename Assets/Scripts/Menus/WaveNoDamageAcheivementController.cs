using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveNoDamageAcheivementController : MonoBehaviour {

	public enum NoDamageType {Player, Heart, Total};
	public NoDamageType type;
	private string level;
	private string wave;
	private string difficulty;
	private string key;
	private Text acheiveText;
	private Image acheiveImg;
	private Outline acheiveOutline;
	private string templateText;
	private float disappearTime;
	private bool flashing = false;
	private bool pulseGrow;
	private float switchPulse;

	public void Update(){

		if (flashing) {
	
			if (Time.time >= switchPulse) {
				pulseGrow = !pulseGrow;
				switchPulse += 1.0f;
			}

			if (pulseGrow) {
				transform.localScale = new Vector3(transform.localScale.x + .002f*transform.localScale.x, transform.localScale.y + .002f*transform.localScale.y, 1f);

			} 

			else {
				transform.localScale = new Vector3(transform.localScale.x - .002f*transform.localScale.x, transform.localScale.y - .002f*transform.localScale.y, 1f);
			}
		


			if (Time.time > disappearTime) {
				Destroy (gameObject);
			} if (disappearTime - Time.time <= 4.0f) {
				DisplayData ();
			}
		}

	}

	public void SetData(string lvl, string wve, string diff){
		level = lvl;
		wave = wve;
		difficulty = diff;
		DisplayData ();
	}

	public void SetType(string stringType){
		if (stringType == GlobalVariables.HEART) {
			type = NoDamageType.Heart;
		} if (stringType == GlobalVariables.PLAYER) {
			type = NoDamageType.Player;
		} if (stringType == GlobalVariables.TOTAL) {
			type = NoDamageType.Total;
		}
	}

	public void FlashData(){
		DisplayData ();
		flashing = true;
		disappearTime = Time.time + 5.0f;
		switchPulse = Time.time + 1.0f;
	}

	public void DisplayData(){
		acheiveText = transform.Find ("Text").GetComponent<Text>();
		acheiveOutline = transform.Find ("Text").GetComponent<Outline>();
		acheiveImg = GetComponent<Image> ();
		templateText = "Level "+level+"\nWave "+wave+"\nNo {0}\nDamage";

		if (type == NoDamageType.Heart) {
			key = string.Format (GlobalVariables.NODAMAGE_HEART_LEVEL_WAVE_DIFFICULTY_TEMPLATE, level, wave, difficulty);
			acheiveText.text = string.Format (templateText, GlobalVariables.HEART);
		} 

		if (type == NoDamageType.Player) {
			key = string.Format (GlobalVariables.NODAMAGE_PLAYER_LEVEL_WAVE_DIFFICULTY_TEMPLATE, level, wave, difficulty);
			acheiveText.text = string.Format (templateText, GlobalVariables.PLAYER);
		} 

		if (type == NoDamageType.Total) {
			key = string.Format (GlobalVariables.NODAMAGE_LEVEL_WAVE_DIFFICULTY_TEMPLATE, level, wave, difficulty);
			acheiveText.text = string.Format (templateText, GlobalVariables.TOTAL);
		}

		if (PlayerPrefs.GetInt (key) == 1) {
			acheiveText.color = Color.black;
			acheiveOutline.effectColor = Color.red;
			acheiveImg.color = Color.white;
		} else {
			acheiveText.color = Color.grey;
			acheiveOutline.effectColor = Color.black;
			acheiveImg.color = Color.grey;
		}
	}

}
