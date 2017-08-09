using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class HealthManagerPic : MonoBehaviour {

	Image img;
	private PlatformerCharacter2D player;
	private int health;

	public Sprite health5;
	public Sprite health4;
	public Sprite health3;
	public Sprite health2;
	public Sprite health1;
	public Sprite health0;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlatformerCharacter2D>();
		img = GetComponent<Image> ();
		health = player.getHealth();
	}

	// Update is called once per frame
	void Update () {
		health = player.getHealth();

		if (health == 5) {
			img.sprite = health5;
		} else if (health == 4) {
			img.sprite = health4;
		} else if (health == 3) {
			img.sprite = health3;
		} else if (health == 2) {
			img.sprite = health2;
		} else if (health == 1) {
			img.sprite = health1;
		} else if (health == 0) {
			img.sprite = health0;
		}

	}
}
