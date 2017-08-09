using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : MonoBehaviour {

	private bool levelOver;

	public GameObject creditScreenCanvas;

	private LevelManager levelManager;
	private Timer timer;

	public Image endOfLevelImg;
	public Sprite thanksForPlaying;
	public Sprite credits;

	public float creditsDelay;
	private float currCreditDelayTime;

	// Use this for initialization
	void Start () {
		levelOver = false;

		levelManager = FindObjectOfType<LevelManager> ();
		timer = FindObjectOfType<Timer> ();

		currCreditDelayTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		levelOver = levelManager.GetLevelOver ();

		if (levelOver) {
			endOfLevelImg.sprite = thanksForPlaying;
			creditScreenCanvas.SetActive (true);
			//Time.timeScale = 0f;
			float t = Mathf.Abs(timer.GetTime()); // get the absolute timer value
			float seconds = t % 60; // calculate the seconds
			float minutes = t / 60; // calculate the minutes

			float totalSeconds = minutes * 60 + seconds;

			currCreditDelayTime += Time.deltaTime;
			if (currCreditDelayTime >= creditsDelay) {
				endOfLevelImg.sprite = credits;
			}

		} else {
			creditScreenCanvas.SetActive (false);
			Time.timeScale = 1f;
		}
	}
}
