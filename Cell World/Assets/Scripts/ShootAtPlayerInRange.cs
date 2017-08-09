using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class ShootAtPlayerInRange : MonoBehaviour {

	private float playerRange;

	public GameObject enemyStar;

	private PlatformerCharacter2D player;
	private LevelManager lvlManager;

	public Transform launchPoint;

	private float waitBetweenShots = 2;
	private float shotCounter;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlatformerCharacter2D> ();
		lvlManager = FindObjectOfType<LevelManager> ();
		playerRange = lvlManager.attackDistance;
		shotCounter = waitBetweenShots;
	}
	
	// Update is called once per frame
	void Update () {
		if (!lvlManager.GetPause ()) {
			Debug.DrawLine (new Vector3 (transform.position.x - playerRange, transform.position.y, transform.position.z), new Vector3 (transform.position.x + playerRange, transform.position.y, transform.position.z));
			shotCounter -= Time.deltaTime;

			if (transform.localScale.x < 0 && player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRange && shotCounter < 0) {
				Instantiate (enemyStar, launchPoint.position, launchPoint.rotation);
				shotCounter = waitBetweenShots;
			}

			if (transform.localScale.x > 0 && player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRange && shotCounter < 0) {
				Instantiate (enemyStar, launchPoint.position, launchPoint.rotation);
				shotCounter = waitBetweenShots;
			}
		}
	}
}
