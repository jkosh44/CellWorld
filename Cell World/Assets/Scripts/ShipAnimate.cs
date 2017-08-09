using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipAnimate : MonoBehaviour {

	public Transform target;
	public float speed;

	private Vector3 newPos;

	private EnterLevel lvlManager;

	void Start () {

		lvlManager = FindObjectOfType<EnterLevel> ();
	}

	void Update() {

//		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
//		Debug.Log ("this is lastPosit" + lastPosit);
//		Debug.Log ("this is newPos" + newPos);
		if (transform.position == target.position) {
			lvlManager.DoneMoving ();
		}


	}
		
}

