using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	private bool isPaused;

	public GameObject pauseMenuCanvas;

	public string level;

	private LevelManager lvlManager;

	// Use this for initialization
	void Start () {
		isPaused = false;

		lvlManager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) {
			pauseMenuCanvas.SetActive (true);
			lvlManager.StopTime ();
			Time.timeScale = 0f;
			Debug.Log ("Game paused. Time Scale = " + Time.timeScale);
		} else {
			pauseMenuCanvas.SetActive (false);
			lvlManager.StartTime ();
			Time.timeScale = 1f;
			Debug.Log ("Game unpaused. Time scale = " + Time.timeScale);
		}

		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown ("Pause")) {
			isPaused = !isPaused;
			lvlManager.SetPause (isPaused);
		}
	}

	public void Resume() {
		isPaused = false;
		lvlManager.SetPause (isPaused);
	}

	public void LevelSelect() {
		SceneManager.LoadScene ("Between Scene " + level);
	}

	public void Quit() {
		SceneManager.LoadScene ("Main Menu");
	}
}
