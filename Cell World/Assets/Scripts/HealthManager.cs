using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class HealthManager : MonoBehaviour {

	private PlatformerCharacter2D player;

	Text text;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlatformerCharacter2D>();
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "" + player.getHealth ();
	}
}
