using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour {

	public string levelName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKey (KeyCode.Space) || Input.GetButton ("OK"))) {

			SceneManager.LoadScene (levelName);
		}
	}
}
