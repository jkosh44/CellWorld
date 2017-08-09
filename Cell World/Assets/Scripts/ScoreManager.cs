using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static int score;

	private LevelManager levelManager;

	Text text;

	void Start()
	{
		levelManager = FindObjectOfType<LevelManager> ();

		text = GetComponent<Text> ();

		score = levelManager.GetScore();
	}

	void Update()
	{
		score = levelManager.GetScore ();
		text.text = "" + score;
	}

	public static void Reset()
	{
		score = 0;
	}
}
