using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class MainMenu : MonoBehaviour {

	public string newGame;
	public Color loadToColor = Color.white;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewGame() {
		SceneManager.LoadScene("Grandpa Conversation");

	}
		

	public void QuitGame() {
		Application.Quit ();
	}
}
