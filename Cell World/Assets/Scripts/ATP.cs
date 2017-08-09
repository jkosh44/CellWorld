using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATP : MonoBehaviour {

	public Transform firePoint;			// Location where sugar is fired from
	public GameObject atp;			// Sugar

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DropATP() {
		Instantiate (atp, firePoint.position, firePoint.rotation);
	}
}
