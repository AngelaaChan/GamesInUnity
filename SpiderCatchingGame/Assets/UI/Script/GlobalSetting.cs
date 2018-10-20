using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalSetting : MonoBehaviour {

	[Range (10, 20)]
	public static int difficulty = 10;
	public static bool isPause = false;
	public Image pauseScreen;
	public Image gameOverScreen;
	public bool invincible = false;

	public BugGeneration generator0;
	public GroundBugGenerator generator1;
	public TextMeshProUGUI LevelWarning;
	private GameObject canvas;

	// a counter that is reset on each replay
	// this allows for the count since the game started
	// will cause overflow error
	private float timeSinceStart;
	public static int level = 0;
	// seconds between each level
	[Range (10, 120)]
	public int timeBetweenLevels = 60;
	[Range (3, 10)]
	public int timeBetweenEnemgyGeneration = 4;

	private float creationTimer = 0;



	public void Update () {
		if (isPause) {
			return;
		}

		timeSinceStart += Time.deltaTime;
		creationTimer -= Time.deltaTime;

		// create enemies continously
		if (creationTimer < 0) {
			// progess level
			// progess level and generate enemies at the same time
			// this is more responsive
			if (timeSinceStart > timeBetweenLevels * GlobalSetting.level) {
				LevelProgresses ();
			}
			GenerateEnemy ();
			creationTimer = timeBetweenEnemgyGeneration;
		}

	}

	public void GenerateEnemy () {
		// generate both ground and flying
		for (int i = 0; i < level; i++) {
			generator0.GenerateEnemy ();
			generator1.GenerateEnemy ();
		}
	}

	public void LevelProgresses () {
		// Debug.Log (level);
		level++;
		// show warning on screen
		canvas = GameObject.Find ("Canvas");
		TextMeshProUGUI instance = Instantiate (LevelWarning, canvas.transform);
	}

	void Start () {
		if (invincible) {
			Debug.Log ("Invincible Mode Is On, turn off in Global Setting.cs");
		}
		// set frame rate
		Application.targetFrameRate = 30;

		// not visible
		isPause = true;
		pauseScreen.gameObject.SetActive (true);
		gameOverScreen.gameObject.SetActive (false);
	}

	public void Quite () {
		Application.Quit ();
	}

	public void GameOver () {
		if (invincible) {
			return;
		}
		isPause = true;
		Time.timeScale = 0;
		gameOverScreen.gameObject.SetActive (true);
	}

	public void Pause () {
		// Pause
		isPause = true;
		Time.timeScale = 0;
		// show pause screen
		pauseScreen.gameObject.SetActive (true);
	}

	public void Resume () {
		Time.timeScale = 1;
		isPause = false;
		// hide all screens
		pauseScreen.gameObject.SetActive (false);
		gameOverScreen.gameObject.SetActive (false);
	}

	public void Restart () {
		// reload sceen
		isPause = false;
		Time.timeScale = 1;
		// reset timer
		timeSinceStart = 0;
		// reset level
		level = 0;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}