using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcheivementScript : MonoBehaviour {

	public GameObject rowTemplate;

	private int currentLvl;
	private int currentWve;
	private int currentDif;
	private Vector3 newPosition;
	private GameObject newRow;
	public Dropdown difficultyDropdown;
	public Dropdown levelDropdown;

	private bool resized;



	void OnEnable() {
		SetAchievements ();
	}

	void Update () {
		if (!resized && GetComponent<VerticalLayoutGroup> ().preferredHeight != 0f ) {
			resized = true;
			GetComponent<RectTransform> ().sizeDelta = new Vector2 (GetComponent<VerticalLayoutGroup> ().preferredWidth, GetComponent<VerticalLayoutGroup> ().preferredHeight);
		}

	}

	public void SetAchievements(){

		int childs = transform.childCount;
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}

		print ("Setting");
		SetDropdownData ();
		currentWve = 1;
		resized = false;

		for (int i = 0; i < 10; i++) {
			newRow = Instantiate (rowTemplate, gameObject.transform);
			newRow.transform.localScale = new Vector3 (1f, 1f, 1f);
			newRow.GetComponent<AcheivementRowController> ().SetData (currentLvl.ToString(), currentWve.ToString(), currentDif.ToString());
			currentWve += 2;
		}

	}

	void SetDropdownData(){
		currentDif = difficultyDropdown.value;
		currentLvl = levelDropdown.value + 1;
	}

}
