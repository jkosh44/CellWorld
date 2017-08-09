using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class SugarPickup : MonoBehaviour {

	public int pointsToAdd;
	//private AudioSource sugarsource;
	private LevelManager lvlManager;
	//private AudioSource audio;
	//public AudioClip sugarpickup;


	// Use this for initialization
	void Start () {
		lvlManager = FindObjectOfType<LevelManager> ();
		//sugarsource = GetComponent<AudioSource> ();

	}
		

	void OnTriggerEnter2D(Collider2D other) {

		
		if (other.GetComponent<PlatformerCharacter2D> () == null) {
			return;
		} else {
//		if (other.gameObject.tag == "Player") {
//			sugarsource.Play ();
//		}
//		
//			
//		audio = (other.GetComponent<PlatformerCharacter2D> ()).GetComponent<AudioSource>();
//		audio.clip = sugarpickup;
//		audio.Play ();
//		if (other.GetComponent<Collider2D>().tag == "Player") {
////			sugarsource.Play ();
////		}

			//sugarsource.Play();
			lvlManager.PlayYeahAudio();
			Debug.Log("playedNoise");
			SugarScoreManager.AddPoints (pointsToAdd);

			lvlManager.SetSugarScore (lvlManager.GetSugarScore () + 1);
			Debug.Log ("SugarScore: " + lvlManager.GetSugarScore ());
			//hitSugar = true;
			Destroy (gameObject);
		}

	}
}
