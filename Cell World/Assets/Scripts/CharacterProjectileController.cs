using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class CharacterProjectileController : MonoBehaviour {

	public float speed;

	private Rigidbody2D m_RigidBody2D;
	private PlatformerCharacter2D player;

	private LevelManager lvlManager;

	private bool delayLvlManager;		//This is to avoid null object error
	private int delayEnemyNum;

	// Use this for initialization
	void Start () {
		m_RigidBody2D = GetComponent<Rigidbody2D> ();
		player = FindObjectOfType<PlatformerCharacter2D> ();

		if (player.transform.localScale.x < 0) {
			speed = -speed;
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		lvlManager = FindObjectOfType<LevelManager> ();
		//delayLvlManager = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!lvlManager.GetPause ()) {
			m_RigidBody2D.velocity = new Vector2 (speed, m_RigidBody2D.velocity.y);
			//Debug.Log ("lvlManager: " + lvlManager);
			//Debug.Log ("Speed: " + m_RigidBody2D.velocity.x);

			//Debug.Log ("Update delayLvlManger: " + delayLvlManager);
			if (delayLvlManager) {
				//	Debug.Log ("changing, score death status, and destroying bullet");
				lvlManager.SetBacteriaScore (lvlManager.GetBacteriaScore () + 1);
				Debug.Log ("Hit enemy, lvlManagerScore: " + lvlManager.GetBacteriaScore ());
				lvlManager.setEnemyDeathStatus (delayEnemyNum, true);
				Destroy (gameObject);
			}
		} else {
			m_RigidBody2D.velocity = new Vector2 (0, 0);
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			BacteriaScoreManager.AddPoints(1);
			Debug.Log ("Hit enemy, BacteriaScoreManager: " + BacteriaScoreManager.score);
		//	Debug.Log ("hit enemy; lvlManager: " + lvlManager + "; other: " + other);
			GetEnemyNum enemyNum = other.gameObject.GetComponent<GetEnemyNum> ();
			if (lvlManager != null) {
				lvlManager.SetBacteriaScore (lvlManager.GetBacteriaScore () + 1);
				Debug.Log ("Hit enemy, lvlManagerScore: " + lvlManager.GetBacteriaScore ());
				lvlManager.setEnemyDeathStatus (enemyNum.getNum (), true);
			} else {
		//		Debug.Log ("lvl manager was null");
				delayLvlManager = true;
				delayEnemyNum = enemyNum.getNum ();
		//		Debug.Log ("On Trigger delayLvlManager: " + delayLvlManager);
			}
			Destroy (other.gameObject);
		}
		if(other.tag =="Enemy_Projectile" ) {
			Destroy (other.gameObject);
		//	Debug.Log ("Hit enemy projectile");
		}
		if (!delayLvlManager) {
			Destroy (gameObject);
		}
	}
}
