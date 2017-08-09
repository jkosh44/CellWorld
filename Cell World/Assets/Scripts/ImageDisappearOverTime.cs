using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisappearOverTime : MonoBehaviour {


	public Image img;

	public float startTime;
	public float timeUntilDisappear;
	private float curTime;
	private int intEndTime;
	private int intCurTime;
	private int intStartTime;
	private int intTimeUntilDisappear;


	private bool disabled;

	// Use this for initialization
	void Start () {
//		img = GetComponent<Image> ();
		disabled = false;
		img.enabled = false;

	}

	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime;
		intCurTime = (int) curTime;
		intStartTime = (int)startTime;
		intTimeUntilDisappear = (int)timeUntilDisappear;

		if (intCurTime == intStartTime) 
		{
			img.enabled = true;
		}
		intEndTime = intTimeUntilDisappear + intStartTime;
		if (intCurTime > intEndTime) 
		{
			img.enabled = false;
		}
		//Debug.Log (intCurTime);
			
	}
}
