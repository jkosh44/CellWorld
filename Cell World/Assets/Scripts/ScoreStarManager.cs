using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreStarManager : MonoBehaviour {

	Image img;
	private LevelManager levelManager;
	private int score;

	public Sprite stars3;
	public Sprite stars2;
	public Sprite stars1;
	public Sprite stars0;


	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
		img = GetComponent<Image> ();
		score = levelManager.GetScore ();
	}

	// Update is called once per frame
	void Update () {
		score = levelManager.GetScore ();

		if (score >= 175) {
			if(Mathf.Abs(levelManager.getEndTime()) <= 240) {
				img.sprite = stars3;
			}
			else {
				img.sprite = stars1;
			}
		} else if (score >= 125) {
			if(levelManager.GetSugarScore() >= 20) {
				img.sprite = stars2;
			} else {
				img.sprite = stars1;
			}
		} else {
			img.sprite = stars1;
		}

	}
}
