using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour {

	private LevelManager lvlManager;

	// Use this for initialization
	void Start () {
		lvlManager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			GetEnemyNum enemyNum = other.gameObject.GetComponent<GetEnemyNum> ();
			Debug.Log ("level manager: " + lvlManager + "; enemyNum: " + enemyNum);
			lvlManager.setEnemyDeathStatus (enemyNum.getNum(), true);
			Destroy (other.gameObject);
		}
	}
}
