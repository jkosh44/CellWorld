using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IconStraffe : MonoBehaviour {

	public Transform target;
	public float speed;
	private Vector3 lastPosit;

//	private EnterLevel lvlManager;

	void Start () {
		lastPosit = transform.position;
//		lvlManager = FindObjectOfType<EnterLevel> ();
	}

	void Update() {
		lastPosit = transform.position;
		//		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
	}
}
