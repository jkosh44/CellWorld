using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisappearOverTime : MonoBehaviour {

	public Text txt;

	public float startTime;
	public float timeUntilDisappear;
	private float curTime;
	private int intEndTime;
	private int intCurTime;
	private int intStartTime;
	private int intTimeUntilDisappear;
	private string displayText;


	private bool disabled;

	// Use this for initialization
	void Start () {
		disabled = false;
		displayText = txt.text;
		txt.text = "";

	}
	
	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime;
		intCurTime = (int) curTime;
		intStartTime = (int)startTime;
		intTimeUntilDisappear = (int)timeUntilDisappear;

		if (intCurTime == intStartTime) 
		{
			txt.text = displayText;
		}
		intEndTime = intTimeUntilDisappear + intStartTime;
		if (intCurTime > intEndTime) 
		{
			txt.text = "";
		}
	}
}
