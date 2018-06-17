using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcheivementRowController : MonoBehaviour {


	private GameObject leftWave;
	private GameObject rightWave;

	// Use this for initialization
	void Start () {
		
	}

	public void SetData(string lvl, string wave, string diff){

		leftWave = transform.Find ("LeftWave").gameObject;
		rightWave = transform.Find ("RightWave").gameObject;
		foreach (WaveNoDamageAcheivementController ach in leftWave.GetComponentsInChildren<WaveNoDamageAcheivementController>()) {
			ach.SetData (lvl, wave, diff);
		}
		wave = (int.Parse (wave) + 1).ToString();
		foreach (WaveNoDamageAcheivementController ach in rightWave.GetComponentsInChildren<WaveNoDamageAcheivementController>()) {
			ach.SetData (lvl, wave, diff);
		}
	}

}
