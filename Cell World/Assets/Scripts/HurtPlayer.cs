using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class HurtPlayer : MonoBehaviour {

	public LevelManager levelManager;
	private PlatformerCharacter2D player;
	private BossController boss;


	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		player = FindObjectOfType<PlatformerCharacter2D>();
//		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.name == "Character" && !levelManager.getRespawn() && !player.GetInvincible()) {
			//if we want hazards to kill the player
			//levelManager.ResawnPlayer();

			//if we want hazards to hurt the player
			if (gameObject.name == "Boss") {
				boss = FindObjectOfType<BossController> ();
				if (boss.GetDead ()) {
					return;
				}
			}

			if (other.transform.position.x < transform.position.x) {
				player.Hurt (true);

//				audio = GetComponent<AudioSource>();
//				audio.PlayOneShot(impact, 1.0F);
//				audio.volume = 1.0f;
			} else {
				player.Hurt (false);
			}
		}
	}


}
