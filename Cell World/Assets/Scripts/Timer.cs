using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	float timer = 0;

	Text timeText;

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		timeText = GetComponent<Text> ();
	}

	void Update (){

		if(!levelManager.GetStopTime()) {
			timer += Time.deltaTime;

			var t = Mathf.Abs(timer); // get the absolute timer value
			var seconds = t % 60; // calculate the seconds
			var minutes = Mathf.Floor(t / 60); // calculate the minutes

			timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
			//timeText.text = "Time: " + timer;

			//Debug.Log ("Minutes: " + minutes + " Seconds: " + seconds);
		}
	}

	public float GetTime() {
		return timer;
	}

	
}

