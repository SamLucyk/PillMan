using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelInfo
{
	public string name;
	public int levelNum;
	public int numWaves;



	public LevelInfo(string aName, int aLevelNum, int aNumWaves)
	{
		name = aName;
		levelNum = aLevelNum;
		numWaves = aNumWaves;
	}
}

public class MenuScript : MonoBehaviour {

	public GameObject exitMenu;
	public GameObject startMenu;
	public GameObject levelSelectMenu;
	public GameObject achievementsMenu;
	public GameObject settingsMenu;

	public Dropdown levelDropdown;
	public Dropdown difficultyDropdown;
	public Dropdown startingWaveDropdown;
	public Text levelTitle;
	public Button startText;
	public Button exitText;

	private List<LevelInfo> levelInfos;
	private LevelInfo currentLevel;
	private int currentDifficulty;
	private List<string> levelNames = new List<string>();
	private List<string> waveNames = new List<string>();


	// Use this for initialization
	void Start () {
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		SetLevels ();
		SetDropdowns ();
	}

	public void SetDropdowns(){


		startingWaveDropdown.options = new List<Dropdown.OptionData> ();
		waveNames = new List<string> (){ "1" };
		currentLevel = levelInfos [levelDropdown.value];
		currentDifficulty = difficultyDropdown.value;
		levelTitle.text = currentLevel.name;

		string currentKey;
		int currentValue;

		for (int waveNum = 1; waveNum < currentLevel.numWaves; waveNum++) {
			currentKey = string.Format (GlobalVariables.NODAMAGE_LEVEL_WAVE_DIFFICULTY_TEMPLATE, currentLevel.levelNum, waveNum, currentDifficulty);
			currentValue = PlayerPrefs.GetInt(currentKey);

			if (currentValue == 1) {
				waveNames.Add((waveNum+1).ToString());
			}
		}

		startingWaveDropdown.AddOptions (waveNames);
		if (startingWaveDropdown.value + 1 > waveNames.Count) {
			startingWaveDropdown.value = 0;
		}

	}

		
	private void SetLevels(){
		
		LevelInfo level1 = new LevelInfo ("Heart Defense 1", 1, 20);
		//LevelInfo level2 = new LevelInfo ("Lung Defense", 2, 20);

		levelInfos = new List<LevelInfo>(){ level1 };

		foreach (LevelInfo levelInfo in levelInfos) {
			levelNames.Add (levelInfo.name);
		}

		levelDropdown.options = new List<Dropdown.OptionData> ();
		levelDropdown.AddOptions(levelNames);

	}
	
	public void ExitPress(){
		exitMenu.SetActive(true);
		startText.enabled = false;
		exitText.enabled = false;
	}

	public void NoPress(){
		exitMenu.SetActive(false);
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void SelectLevelPress(){
		SetDropdowns ();
		startMenu.SetActive (false);
		levelSelectMenu.SetActive (true);
	}

	public void StartMenuPress(){
		startMenu.SetActive (true);
		settingsMenu.SetActive (false);
		levelSelectMenu.SetActive (false);
		achievementsMenu.SetActive (false);
	}

	public void AcheivementsPress(){
		startMenu.SetActive (false);
		achievementsMenu.SetActive (true);
	}

	public void SettingsPress(){
		startMenu.SetActive (false);
		settingsMenu.SetActive (true);
	}

	public void StartLevel(){
		GlobalVariables.DIFFICULTY = currentDifficulty;
		GlobalVariables.STARTINGWAVE = startingWaveDropdown.value + 1;
		SceneManager.LoadScene (levelDropdown.value + 1);
	}

	public void ExitGame(){
		print ("Exitting");
		Application.Quit ();
	}
}
