using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class KillPlayer : MonoBehaviour {

	public LevelManager levelManager;
	private PlatformerCharacter2D player;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		player = FindObjectOfType<PlatformerCharacter2D>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Character" && !levelManager.getRespawn()) {
			//if we want hazards to kill the player
			player.setDieAnim();
			player.setDead(true);
			player.Respawn();

		}
	}
}
