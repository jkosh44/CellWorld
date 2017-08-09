using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour {

	public LevelManager levelManager;

	public GameObject enemy;

	public int enemyNum;

	private bool playerDied;
	private bool lvlManagerRespawn;

	public int respawnNumber;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();

		playerDied = false; 
		lvlManagerRespawn = false;
	}
	
	// Update is called once per frame
	void Update () {
		lvlManagerRespawn = levelManager.getRespawn ();

		if (lvlManagerRespawn && respawnNumber >= levelManager.currentCheckpointNum && levelManager.getEnemyDeathStatus(enemyNum)) {
			playerDied = true;
		}

		if (!lvlManagerRespawn && playerDied && respawnNumber >= levelManager.currentCheckpointNum && levelManager.getEnemyDeathStatus(enemyNum)) {
			levelManager.setEnemyDeathStatus (enemyNum, false);
			playerDied = false;
			Respawn ();
			Debug.Log ("Enemy respawned. playerDied: " + playerDied + "; lvlManagerRespawn: " + lvlManagerRespawn);
		}
	}

	public void Respawn() {
		GameObject newEnemy = Instantiate (enemy, gameObject.transform.position, gameObject.transform.rotation);
		GetEnemyNum newEnemyNum = newEnemy.GetComponent<GetEnemyNum> ();
		newEnemyNum.setNum (enemyNum);
	}
}
