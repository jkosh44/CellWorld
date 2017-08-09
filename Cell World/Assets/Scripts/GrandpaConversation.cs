using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GrandpaConversation : MonoBehaviour {
//	public List<AudioClip> myAudioClipsList = new List<AudioClip>();
	public Image img;

	public Sprite grandpa1;
	public Sprite grandpa2;
	public Sprite grandpa3;
	public Sprite grandpa4;
	public Sprite grandpa5;
	public Sprite grandpa6;
	public Color loadToColor = Color.white;
	private int state;
	private Button but;
	private bool nah;
	private Image buttonimage;

	public AudioClip gpsound1; //Voiceovers for Grandpa Ricky
	public AudioClip gpsound2;
	public AudioClip gpsound3;
	public AudioClip gpsound4;
	public AudioClip gpsound5;
	public AudioClip gpsound6;

	private AudioSource gpsource;

	// Use this for initialization
	void Start () {
		state = 1;
		gpsource = GetComponent<AudioSource> ();
		gpsource.clip = gpsound1;
		gpsource.Play ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("Submit")){
				changeImgRight();
		}
		else if(Input.GetButtonDown("Cancel")){
			changeImgLeft();
		}
	}

	public void changeImgRight() {
		if (state == 1){

			gpsource.clip = gpsound2;
			gpsource.Play ();
			img.sprite = grandpa2;
//			gpsource.clip = gpsound1;
//			gpsource.Play ();


//			foreach (AudioSource audio in 
			state = 2;
			}
		else if (state == 2) {
			gpsource.clip = gpsound3;
			gpsource.Play ();
			img.sprite = grandpa3;
			state = 3;

		} 
		else if (state == 3) {
			gpsource.clip = gpsound4;
			gpsource.Play ();
			img.sprite = grandpa4;
			state = 4;

		}
		else if (state == 4) {
			gpsource.clip = gpsound5;
			gpsource.Play ();
			img.sprite = grandpa5;
			state = 5;

		}
		else if (state == 5) {
			gpsource.clip = gpsound6;
			gpsource.Play();
			img.sprite = grandpa6;
			state = 6;

		}
		else if (state == 6) {
			SceneManager.LoadScene ("Between Scene 1");
		}
	}
	public void changeImgLeft() {
//		but = GetComponent<Button> ();
//		Debug.Log (but.tag);
		if (state == 5) {
			img.sprite = grandpa4;


			state = 4;

		} else if (state == 4) {
			img.sprite = grandpa3;
			state = 3;

		} else if (state == 3) {
			img.sprite = grandpa2;
			state = 2;
	
		}
		 else if (state == 2) {
			img.sprite = grandpa1;
			state = 1;
			gpsource.clip = gpsound1;
			gpsource.Play ();
		}
		else if (state == 1) {
			SceneManager.LoadScene ("Between Scene 1");
		}
//		else if (state == 5) {
//			Initiate.Fade ("Between Scene 1", loadToColor, 1.0f);
//
//		}
		}
}

