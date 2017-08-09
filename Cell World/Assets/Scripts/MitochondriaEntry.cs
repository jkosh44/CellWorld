using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MitochondriaEntry : MonoBehaviour {

	private bool touchingMito;

	// Use this for initialization
	void Start () {
		touchingMito = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A) && touchingMito) {
			SceneManager.LoadScene ("Mitochondria");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Spaceship") {
			touchingMito = true;
			Debug.Log ("touching Mitochindria");
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == "Spaceship") {
			touchingMito = false;
			Debug.Log ("not touching Mitochondria");
		}
	}
}
