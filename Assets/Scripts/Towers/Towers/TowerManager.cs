using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour {

	public enum shotType {Shoot, Spray};
	public shotType type;
	public string title;
	public Material material;
	private int level;

	public Sprite image1;
	public Sprite image2;
	public Sprite image3;

	private GameObject level1;
	private GameObject level2;
	private GameObject level3;

	private Sprite[] imageList = new Sprite[3];
	private GameObject[] levelList = new GameObject[3];

	public void Start(){
		level = 1;
		level1 = transform.Find ("Level1").gameObject;
		level2 = transform.Find ("Level2").gameObject;
		level3 = transform.Find ("Level3").gameObject;
		levelList [0] = level1;
		levelList [1] = level2;
		levelList [2] = level3;
		imageList [0] = image1;
		imageList [1] = image2;
		imageList [2] = image3;
		SetLevelAppearance ();
	}

	public void Upgrade(){
		level++;
		SetLevelAppearance ();
		if (type == shotType.Spray) {
			GetComponent<SprayZoneController> ().Upgrade();
		} if (type == shotType.Shoot) {
			GetComponent<TowerController> ().Upgrade();
		}
	}

	public string GetTitle(){
		return title;
	}

	public int GetLevel(){
		return level;
	}

	public Sprite GetImage(){
		return imageList[level-1];
	}

	public Material GetMaterial(){
		return material;
	}

	private void SetLevelAppearance(){
		level1.SetActive (false);
		level2.SetActive (false);
		level3.SetActive (false);
		levelList [level - 1].SetActive (true);
	}

}
