using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	public int currentCheckpointNum;

	private bool[] enemyDeathStatus;					// If true enemy is dead, if false enemy is alive

	private PlatformerCharacter2D player;
	private Timer timer;

	public float attackDistance = 20f;				// Distance between player and projectile enemy that will cause them to stop moving and start shooting

	private bool respawn;							// Whether or not the player needs to be respawned
	public float respawnDelay;						// Delay in seconds after player is killed
	private float curRespawnDelay;

	private Vector3 playerLocalScale;

	private bool controlable;

	private int sugarScore;
	private int bacteriaScore;
	private int prevBacteriaScore;
	private float endTime;

	private int score;

	private bool levelOver;
	private bool stopTime;

	private AudioSource sugarsource;

	private bool paused;



	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlatformerCharacter2D>();
		currentCheckpoint = FindObjectOfType<StartCheckpoint>().gameObject;

		enemyDeathStatus = new bool[100];
		for (int i = 0; i < 100; i++) {
			enemyDeathStatus [i] = false;
		}

		respawn = false;
		curRespawnDelay = respawnDelay;

		playerLocalScale = player.transform.localScale;

		controlable = true;

		sugarScore = 0;
		bacteriaScore = 0;
		prevBacteriaScore = 0;

		score = 0;

		levelOver = false;
		stopTime = false;

		timer = FindObjectOfType<Timer> ();

		sugarsource = GetComponent<AudioSource> ();

		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (respawn && !paused) {
			if (curRespawnDelay > 0) {
				curRespawnDelay -= Time.deltaTime;
			} else {
				RespawnPlayer ();
			}
		}
	}

	public void KillPlayer()
	{
		playerLocalScale = player.transform.localScale;
		respawn = true;
		bacteriaScore = prevBacteriaScore;
		BacteriaScoreManager.RevertScore ();
		Debug.Log("Player Died");
	}

	private void RespawnPlayer() {
		player.transform.position = currentCheckpoint.transform.position;
		respawn = false;
		curRespawnDelay = respawnDelay;
		player.setDead (false);
		Debug.Log ("Player Respawned");
	}

	public void SetNewCheckpoint(GameObject checkpoint, int checkpointNumber) {
		currentCheckpoint = checkpoint;
		currentCheckpointNum = checkpointNumber;
		prevBacteriaScore = bacteriaScore;
		BacteriaScoreManager.SetPrevScore (bacteriaScore);
	}

	public void PlayYeahAudio() {
		sugarsource.Play();
	}

	public bool getRespawn() {
		return respawn;
	}

	public bool getControlable() {
		return controlable;
	}

	public void NotControlable() {
		controlable = false;
	}

	public int GetSugarScore() {
		return sugarScore;
	}

	public void SetSugarScore(int score) {
		sugarScore = score;
	}

	public int GetBacteriaScore() {
		return bacteriaScore;
	}

	public void SetBacteriaScore(int score) {
		bacteriaScore = score;
	}

	public int GetScore() {
		return score;
	}

	public bool GetLevelOver() {
		return levelOver;
	}

	public bool GetStopTime() {
		return stopTime;
	}

	public void StopTime() {
		stopTime = true;
	}

	public void StartTime() {
		stopTime = false;
	}

	public void EndLevel() {
		endTime = timer.GetTime ();
		float t = Mathf.Abs(endTime); // get the absolute timer value
//		float seconds = t % 60; // calculate the seconds
//		float minutes = t / 60; // calculate the minutes

		int timePoints = 0;
		if(t >= 0f && t <= 180f) {
			timePoints = 50;
		} else if (t <= 240f) {
			timePoints = 25;
		} else {
			timePoints = 10;
		}

		int sugarPoints = 0;
		if(sugarScore >= 0 && sugarScore <= 19) {
			sugarPoints = 10;
		} else if(sugarScore <= 29) {
			sugarPoints = 25;
		} else if(sugarScore <= 39) {
			sugarPoints = 50;
		} else if(sugarScore <= 49) {
			sugarPoints = 75;
		} else {
			sugarPoints = 100;
		}

		int bacteriaPoints = 0;
		if(bacteriaScore >= 0 && bacteriaScore <= 0) {
			bacteriaPoints = 25;
		} else if(bacteriaScore <= 17) {
			bacteriaPoints = 50;
		} else {
			bacteriaPoints = 75;
		}

		score = timePoints + sugarPoints + bacteriaPoints;
		levelOver = true;
	}

	public float getEndTime() {
		return endTime;
	}

	public bool getEnemyDeathStatus(int enemyNum) {
		return enemyDeathStatus [enemyNum];
	}

	public void setEnemyDeathStatus(int enemyNum, bool dead) {
		enemyDeathStatus [enemyNum] = dead;
	}

	public void SetPause(bool pauseStatus) {
		paused = pauseStatus;
	}

	public bool GetPause() {
		return paused;
	}

}