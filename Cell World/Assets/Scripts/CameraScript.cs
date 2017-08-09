using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		offset = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!levelManager.getRespawn ()) {
			transform.position = player.transform.position + offset;
		}
	}
}
