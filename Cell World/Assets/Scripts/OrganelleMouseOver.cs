using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OrganelleMouseOver : MonoBehaviour {

	public Image img;
	public Image enterlevelimg;

	public Sprite mitochondria;
	public Sprite centriole;
	public Sprite golgi;
	public Sprite endo;
	public Sprite nucleus;
	public Sprite peroxisome;
	public Sprite lysosome;
	public AudioClip mitosound;
	public AudioClip centrisound;
	public AudioClip golgisound;
	public AudioClip endosound;
	public AudioClip nucleussound;
	public AudioClip peroxsound;
	public AudioClip lysosound;

	private AudioSource source;


	public Sprite mitoimage1;
	public Sprite mitoimage2;


	// Use this for initialization
	void Start () {
		img.enabled = false;
		source = GetComponent<AudioSource> ();
//		enterlevelimg.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//void OnMouseHover() {

	void OnMouseEnter() {
		img.enabled = true;
		enterlevelimg.enabled = true;
		if(gameObject.tag == "Mitochondria") {
			img.sprite = mitochondria;
			source.clip = mitosound;
			source.Play ();
		}
		else if(gameObject.tag == "Centriole") {
			img.sprite = centriole;
		}
		else if(gameObject.tag == "Golgi") {
			img.sprite = golgi;
		}
		else if(gameObject.tag == "Endoplasmic_Reticulum") {
			img.sprite = endo;
		}
		else if(gameObject.tag == "Nucleus") {
			img.sprite = nucleus;
		}
		else if(gameObject.tag == "Peroxisome") {
			img.sprite = peroxisome;
		}
		else if(gameObject.tag == "Lysosome") {
			img.sprite = lysosome;
		}
	}

	void OnMouseExit() {
		//img.enabled = false;
	}
}
