using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {

	public float m_MaxSpeed;

	private Rigidbody2D m_Rigidbody2D;

	public Image img;

	public Sprite mitochondria;
	public Sprite centriole;
	public Sprite golgi;
	public Sprite endo;
	public Sprite nucleus;
	public Sprite peroxisome;
	public Sprite lysosome;

	// Use this for initialization
	void Start () {
		m_Rigidbody2D = GetComponent<Rigidbody2D> ();

		img.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		float v = CrossPlatformInputManager.GetAxis ("Vertical");

		Move (h, v);

		//Rotate ();

	}

	private void Move(float moveH, float moveV) {
		m_Rigidbody2D.velocity = new Vector2 (moveH * m_MaxSpeed, moveV * m_MaxSpeed);
	}

	private void Rotate() {
		Vector2 moveDirection = m_Rigidbody2D.velocity;

		if (moveDirection != Vector2.zero) {
			float angle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			m_Rigidbody2D.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			m_Rigidbody2D.transform.Rotate (0, 0, -90);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		img.enabled = true;
		if(other.tag == "Mitochondria") {
			img.sprite = mitochondria;
		}
		else if(other.tag == "Centriole") {
			img.sprite = centriole;
		}
		else if(other.tag == "Golgi") {
			img.sprite = golgi;
		}
		else if(other.tag == "Endoplasmic_Reticulum") {
			img.sprite = endo;
		}
		else if(other.tag == "Nucleus") {
			img.sprite = nucleus;
		}
		else if(other.tag == "Peroxisome") {
			img.sprite = peroxisome;
		}
		else if(other.tag == "Lysosome") {
			img.sprite = lysosome;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		img.enabled = false;
	}
		
		
}
