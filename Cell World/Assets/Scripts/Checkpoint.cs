using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public LevelManager levelManager;
	public int checkPointNum;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Character" && checkPointNum >= levelManager.currentCheckpointNum) {
			//levelManager.currentCheckpoint = gameObject;
			//levelManager.currentCheckpointNum = checkPointNum;
			levelManager.SetNewCheckpoint(gameObject, checkPointNum);
			Debug.Log ("Activated Checkpoint " + transform.position);
		}
	}
}
