using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BacteriaScoreManager : MonoBehaviour {

	public static int score;
	public static int prevScore;

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();

		score = 0;
		prevScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (score < 0) {
			score = 0;
		}

		text.text = "" + score;
	}

	public static void AddPoints(int pointsToAdd)
	{
		score += pointsToAdd;
	}

	public static void Reset()
	{
		score = 0;
	}

	public static void RevertScore() {
		score = prevScore;
	}

	public static void SetPrevScore(int score) {
		prevScore = score;
	}
}
