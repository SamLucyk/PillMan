using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour
{
	private PlayerController player;
	private int currentWave;
	private int levelNum;
	private bool betweenWaves = true;
	private int startWave;
	private float waveFraction;
	private Vector2 waveBarSize;
	private string waveTemplate = "Wave ";
	private GameObject[] currentEnemies;
	public GameObject winMenu;
	private float waveEnemies;
	private float spawnedEnemies;
	private AchievementAlertController achievementController;

	//
	// Enemy Prefabs
	//----------------------------------
	public GameObject Enemy1;//Germ
	public GameObject Enemy2;//Spinner
	public GameObject Enemy3;//BigGerm
	public GameObject Enemy4;//BigSpinner
	public GameObject Enemy5;//FastGerm
	public GameObject Enemy6;//FastSpinner
	//
	// Spawners
	//
	private GameObject Spawner1;
	private GameObject Spawner2;
	private GameObject Spawner3;
	private GameObject Spawner4;
	private GameObject Spawner5;
	private GameObject Spawner6;

	private int enemy1Num; 
	private int enemy2Num;
	private int enemy3Num; 
	private int enemy4Num;
	private int enemy5Num;
	private int enemy6Num;

	private float waitBetweenSpawns;
	private float waitCounter;

	public GameObject waveBar;
	private Text waveNameDisplay;
	private RectTransform waveDisplay;
	private Text alert;

	private Dictionary<int, List<int>> waveEnemyPairs = new Dictionary<int, List<int>>();


	void Start()
	{
		startWave = GlobalVariables.STARTINGWAVE;
		levelNum = 1;
		GlobalVariables.HEARTNODAMAGE = true;
		GlobalVariables.PLAYERNODAMAGE = true;

		achievementController = GameObject.Find ("HUD/Achievements").GetComponent<AchievementAlertController> ();
		player = GameObject.Find ("Player").GetComponent<PlayerController> ();
		alert = GameObject.Find ("HUD/Alert").GetComponent<Text> ();
		waveDisplay = waveBar.transform.Find ("Health").GetComponent<RectTransform> ();
		waveNameDisplay = waveBar.transform.Find ("Wave Name").GetComponent<Text> ();

		waitCounter = 1.0f;
		waitBetweenSpawns = 1.0f;
		currentWave = startWave - 1;
		spawnedEnemies = 0.0f;
		waveBarSize = new Vector2 (0, 10);

		GetSpawners ();

		initWaves ();
		SetPoints();
		getCurrentEnemies ();
		nextWave ();
		getWaveEnemies ();
		setWaveBar ();
	}

	void Update ()
	{
		getCurrentEnemies ();
		setWaveBar ();

		if (currentEnemies.Length <= 0 && waveEnemies == spawnedEnemies) {
			nextWave ();
		}

		if (betweenWaves) {
			if (Input.GetKeyDown ("r")) {
				betweenWaves = false;
				alert.text = "";
			}
		}

		if (waitCounter > 0 && !betweenWaves) {
			waitCounter -= Time.deltaTime;
			if (waitCounter <= 0) {
				if (enemy1Num > 0) {
					spawnEnemy1 ();
					enemy1Num -= 1;
				} if (enemy2Num > 0) {
					spawnEnemy2 ();
					enemy2Num -= 1;
				}
				if (enemy3Num > 0) {
					spawnEnemy3 ();
					enemy3Num -= 1;
				}
				if (enemy4Num > 0) {
					spawnEnemy4 ();
					enemy4Num -= 1;
				}
				if (enemy5Num > 0) {
					spawnEnemy5 ();
					enemy5Num -= 1;
				}

				if (enemy6Num > 0) {
					spawnEnemy6 ();
					enemy6Num -= 1;
				}
				waitCounter = waitBetweenSpawns;
			}
		}


	}
	// spawns an enemy based on the enemy level that you selected
	private void spawnEnemy1()
	{
		spawnedEnemies++;
		float randomXOffset = Random.Range (-6, 10);
		Vector3 spawnLocation = new Vector3 (Spawner1.gameObject.transform.position.x + randomXOffset, Spawner1.gameObject.transform.position.y, Spawner1.gameObject.transform.position.z);
		Instantiate(Enemy1, spawnLocation, Quaternion.identity);
	}

	private void spawnEnemy2()
	{
		spawnedEnemies++;
		Instantiate(Enemy2, Spawner2.gameObject.transform.position, Quaternion.identity);
	}

	private void spawnEnemy3()
	{
		spawnedEnemies++;
		float randomXOffset = Random.Range (-3, 4);
		Vector3 spawnLocation = new Vector3 (Spawner3.gameObject.transform.position.x + randomXOffset, Spawner1.gameObject.transform.position.y, Spawner1.gameObject.transform.position.z);
		Instantiate(Enemy3, spawnLocation, Quaternion.identity);
	}

	private void spawnEnemy4()
	{
		spawnedEnemies++;
		Instantiate(Enemy4, Spawner4.gameObject.transform.position, Quaternion.identity);
	}

	private void spawnEnemy5()
	{
		spawnedEnemies++;
		Instantiate(Enemy5, Spawner5.gameObject.transform.position, Quaternion.identity);
	}

	private void spawnEnemy6()
	{
		spawnedEnemies++;
		Instantiate(Enemy6, Spawner6.gameObject.transform.position, Quaternion.identity);
	}

	private void getWaveEnemies(){
		waveEnemies = enemy1Num + enemy2Num + enemy3Num + enemy4Num + enemy5Num + enemy6Num;
	}

	private void getCurrentEnemies(){
		currentEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}

	private void setWaveBar(){
		waveFraction = (float)currentEnemies.Length / spawnedEnemies;
		waveBarSize.x = waveFraction * 100;
		waveDisplay.sizeDelta = waveBarSize;
	}

	private void initWaves(){

			// { Germ, Spinner, BigGerm, BigSpinner, Fast Germ, Fast Spinner}
		List<int> wave1 = new List<int>{ 3, 0, 0, 0, 0, 0 };
		List<int> wave2 = new List<int>{ 5, 0, 0, 0, 0, 0 };
		List<int> wave3 = new List<int>{ 3, 1, 0, 0, 0, 0 };
		List<int> wave4 = new List<int>{ 6, 1, 0, 0, 0, 0 };
		List<int> wave5 = new List<int>{ 10, 2, 0, 0, 0, 0 };
		List<int> wave6 = new List<int>{ 6, 1, 0, 0, 2, 0 };
		List<int> wave7 = new List<int>{ 3, 0, 4, 0, 0, 0 };
		List<int> wave8 = new List<int>{ 5, 2, 5, 0, 0, 0 };
		List<int> wave9 = new List<int>{ 10, 0, 5, 0, 5, 0 };
		List<int> wave10 = new List<int>{ 10, 3, 7, 0, 3, 0 };
		List<int> wave11 = new List<int>{ 15, 0, 3, 0, 5, 0 };
		List<int> wave12 = new List<int>{ 0, 0, 10, 0, 0, 9 };
		List<int> wave13 = new List<int>{ 0, 0, 2, 0, 5, 5 };
		List<int> wave14 = new List<int>{ 5, 5, 5, 0, 5, 5 };
		List<int> wave15 = new List<int>{ 0, 3, 0, 3, 0, 3 };
		List<int> wave16 = new List<int>{ 3, 3, 3, 3, 3, 3 };
		List<int> wave17 = new List<int>{ 7, 7, 7, 7, 7, 7 };
		List<int> wave18 = new List<int>{ 0, 0, 15, 15, 0, 0 };
		List<int> wave19 = new List<int>{ 0, 0, 1, 1, 20, 20 };
		List<int> wave20 = new List<int>{ 15, 15, 15, 15, 15, 15 };


		if (GlobalVariables.DIFFICULTY == 1) {
			// { Germ, Spinner, BigGerm, BigSpinner, Fast Germ, Fast Spinner}
			wave1 = new List<int>{ 5, 0, 0, 0, 0, 0 };
			wave2 = new List<int>{ 8, 0, 0, 0, 0, 0 };
			wave3 = new List<int>{ 5, 1, 0, 0, 0, 0 };
			wave4 = new List<int>{ 15, 2, 0, 0, 0, 0 };
			wave5 = new List<int>{ 20, 3, 0, 0, 0, 0 };
			wave6 = new List<int>{ 10, 1, 0, 0, 4, 0 };
			wave7 = new List<int>{ 5, 0, 7, 0, 0, 0 };
			wave8 = new List<int>{ 10, 3, 8, 0, 0, 0 };
			wave9 = new List<int>{ 20, 0, 10, 0, 10, 0 };
			wave10 = new List<int>{ 20, 6, 15, 0, 5, 0 };
			wave11 = new List<int>{ 30, 0, 5, 0, 10, 0 };
			wave12 = new List<int>{ 0, 0, 20, 0, 0, 18 };
			wave13 = new List<int>{ 0, 0, 4, 0, 10, 10 };
			wave14 = new List<int>{ 10, 10, 10, 0, 10, 10 };
			wave15 = new List<int>{ 0, 6, 0, 6, 0, 6 };
			wave16 = new List<int>{ 6, 6, 6, 6, 6, 6 };
			wave17 = new List<int>{ 10, 10, 10, 10, 10, 10 };
			wave18 = new List<int>{ 0, 0, 20, 20, 0, 0 };
			wave19 = new List<int>{ 0, 0, 1, 1, 30, 30 };
			wave20 = new List<int>{ 25, 25, 25, 25, 25, 25 };	
		} 

		if (GlobalVariables.DIFFICULTY == 2) {
			// { Germ, Spinner, BigGerm, BigSpinner, Fast Germ, Fast Spinner}
			wave1 = new List<int>{ 5, 0, 0, 0, 0, 0 };
			wave2 = new List<int>{ 9, 0, 0, 0, 0, 0 };
			wave3 = new List<int>{ 6, 1, 0, 0, 0, 0 };
			wave4 = new List<int>{ 15, 2, 0, 0, 2, 0 };
			wave5 = new List<int>{ 20, 3, 0, 0, 0, 1 };
			wave6 = new List<int>{ 14, 2, 0, 0, 6, 0 };
			wave7 = new List<int>{ 8, 0, 9, 0, 0, 0 };
			wave8 = new List<int>{ 13, 5, 10, 0, 0, 0 };
			wave9 = new List<int>{ 22, 0, 13, 0, 10, 0 };
			wave10 = new List<int>{ 20, 7, 17, 0, 6, 0 };
			wave11 = new List<int>{ 30, 0, 8, 0, 10, 0 };
			wave12 = new List<int>{ 0, 0, 25, 0, 0, 22 };
			wave13 = new List<int>{ 0, 0, 6, 0, 12, 12 };
			wave14 = new List<int>{ 12, 12, 12, 0, 12, 12 };
			wave15 = new List<int>{ 0, 8, 0, 8, 0, 8 };
			wave16 = new List<int>{ 8, 8, 8, 8, 8, 8 };
			wave17 = new List<int>{ 12, 12, 12, 12, 12, 12 };
			wave18 = new List<int>{ 0, 0, 22, 22, 0, 0 };
			wave19 = new List<int>{ 0, 0, 3, 3, 30, 30 };
			wave20 = new List<int>{ 31, 31, 31, 31, 31, 31 };	
		}

		waveEnemyPairs.Add (1, wave1);
		waveEnemyPairs.Add (2, wave2);
		waveEnemyPairs.Add (3, wave3);
		waveEnemyPairs.Add (4, wave4);
		waveEnemyPairs.Add (5, wave5);
		waveEnemyPairs.Add (6, wave6);
		waveEnemyPairs.Add (7, wave7);
		waveEnemyPairs.Add (8, wave8);
		waveEnemyPairs.Add (9, wave9);
		waveEnemyPairs.Add (10, wave10);
		waveEnemyPairs.Add (11, wave11);
		waveEnemyPairs.Add (12, wave12);
		waveEnemyPairs.Add (13, wave13);
		waveEnemyPairs.Add (14, wave14);
		waveEnemyPairs.Add (15, wave15);
		waveEnemyPairs.Add (16, wave16);
		waveEnemyPairs.Add (17, wave17);
		waveEnemyPairs.Add (18, wave18);
		waveEnemyPairs.Add (19, wave19);
		waveEnemyPairs.Add (20, wave20);
	}

	private void nextWave(){
		SaveProgress ();
		if (currentWave == 20) {
			PlayerPrefs.SetInt ("Level1", 1);
			winMenu.SetActive (true);
		} else {
			betweenWaves = true;
			spawnedEnemies = 0;
			currentWave++;
			waveNameDisplay.text = waveTemplate + currentWave;
			List<int> waveEnemyNums = waveEnemyPairs [currentWave];
			enemy1Num = waveEnemyNums [0];
			enemy2Num = waveEnemyNums [1];
			enemy3Num = waveEnemyNums [2];
			enemy4Num = waveEnemyNums [3];
			enemy5Num = waveEnemyNums [4];
			enemy6Num = waveEnemyNums [5];
			getWaveEnemies ();
			if (!(currentWave == 1)) {
				alert.text = "Press \"R\" to begin Wave " + currentWave + ".";
			}
		}
	}  

	private void SaveProgress (){
		if (!(currentWave == 0)) {


			PlayerPrefs.SetInt (string.Format (GlobalVariables.COMPLETE_LEVEL_WAVE_DIFFICULTY_TEMPLATE, levelNum, currentWave, GlobalVariables.DIFFICULTY), 1);

			if (GlobalVariables.PLAYERNODAMAGE && GlobalVariables.HEARTNODAMAGE) {
				achievementController.SetAchievement (GlobalVariables.NODAMAGE_LEVEL_WAVE_DIFFICULTY_TEMPLATE, GlobalVariables.TOTAL, levelNum.ToString (), currentWave.ToString (), GlobalVariables.DIFFICULTY.ToString ());
			} 

			if (GlobalVariables.HEARTNODAMAGE) {
				achievementController.SetAchievement (GlobalVariables.NODAMAGE_HEART_LEVEL_WAVE_DIFFICULTY_TEMPLATE, GlobalVariables.HEART, levelNum.ToString (), currentWave.ToString (), GlobalVariables.DIFFICULTY.ToString ());
			} 

			if (GlobalVariables.PLAYERNODAMAGE) {
				achievementController.SetAchievement (GlobalVariables.NODAMAGE_PLAYER_LEVEL_WAVE_DIFFICULTY_TEMPLATE, GlobalVariables.PLAYER, levelNum.ToString (), currentWave.ToString (), GlobalVariables.DIFFICULTY.ToString ());
			}

		}


	}


	private void SetPoints (){
		int totalPointsToAdd = 0;
		for (int i = 1; i < startWave; i++) {
			totalPointsToAdd += GetPointsOfWave (i);
		}

		player.AddPoints (totalPointsToAdd);
	}

	private int GetPointsOfWave(int waveNum){
		int result = 0;
		List<int> wave = waveEnemyPairs[waveNum];
		result += wave[0] * 15;
		result += wave[1] * 60;
		result += wave[2] * 90;
		result += wave[3] * 300;
		result += wave[4] * 30;
		result += wave[5] * 60;
		return result;
	}

	private void GetSpawners(){
		Spawner1 = GameObject.Find ("Spawners/Spawner1");
		Spawner2 = GameObject.Find ("Spawners/Spawner2");
		Spawner3 = GameObject.Find ("Spawners/Spawner3");
		Spawner4 = GameObject.Find ("Spawners/Spawner4");
		Spawner5 = GameObject.Find ("Spawners/Spawner5");
		Spawner6 = GameObject.Find ("Spawners/Spawner6");
	}
}