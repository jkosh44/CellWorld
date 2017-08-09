using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {

	private bool levelOver;

	public GameObject scoreScreenCanvas;

	private LevelManager levelManager;
	private Timer timer;

	public Text time;
	public Color loadToColor = Color.white;
	// Use this for initialization
	void Start () {
		levelOver = false;

		levelManager = FindObjectOfType<LevelManager> ();
		timer = FindObjectOfType<Timer> ();
	}

	// Update is called once per frame
	void Update () {

		levelOver = levelManager.GetLevelOver ();

		if (levelOver) {
			scoreScreenCanvas.SetActive (true);
			Time.timeScale = 0f;
			float t = Mathf.Abs(timer.GetTime()); // get the absolute timer value
			float seconds = t % 60; // calculate the seconds
			float minutes = t / 60; // calculate the minutes

			float totalSeconds = Mathf.Floor(minutes) * 60 + seconds;

			time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		} else {
			scoreScreenCanvas.SetActive (false);
			Time.timeScale = 1f;
		}

	}
		

	public void NextLevel() {
//		Initiate.Fade ("Between Scene 3", loadToColor, 1.0f);
//		if ((Input.GetKey (KeyCode.Space) || Input.GetButton("OK"))) {
//			Initiate.Fade ("Between Scene 4", loadToColor, 1.0f);
//		SceneManager.LoadScene ("Between Scene 2");
		SceneManager.LoadScene ("Pre-Nucleus");

	}

	public void ReplayLevel() {
		//Initiate.Fade ("Mitochondria", loadToColor, 1.0f);
		SceneManager.LoadScene ("Mitochondria");
	
	}

	public void Quit() {
		//Initiate.Fade ("Main Menu", loadToColor, 1.0f);
		SceneManager.LoadScene ("Main Menu");

	}


}
