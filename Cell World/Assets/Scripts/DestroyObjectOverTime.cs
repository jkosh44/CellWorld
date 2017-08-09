using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOverTime : MonoBehaviour {

	public float lifetime = 2;

	private LevelManager lvlManager;

	// Use this for initialization
	void Start () {
		lvlManager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!lvlManager.GetPause ()) {
			lifetime -= Time.deltaTime;

			if (lifetime < 0) {
				Destroy (gameObject);
			}
		}
	}
}
