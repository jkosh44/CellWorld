using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour {

	private bool doneMoving;
	public string levelName;
	public Color loadToColor = Color.white;

	// Use this for initialization
	void Start () {
		doneMoving = false;
	}
	public void DoneMoving() {
		doneMoving = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (doneMoving && (Input.GetKey (KeyCode.Space) || Input.GetButton("OK"))) {
//			Initiate.Fade (levelName, loadToColor, 1.0f);
			SceneManager.LoadScene(levelName);
		}
	}
		
}
