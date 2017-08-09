using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopControls : MonoBehaviour {

	private LevelManager levelManager;
	private ATP atp;

	public Transform firePoint;			// Location where sugar is fired from
	public GameObject sugar;			// Sugar
	private int counter;			// counter inbetween sugars
	public int sugarsLaunched;
	private bool hit;
	private bool scoreCalculated;
	public int sugarLaunchDelay;
	public int atpLaunchDelay;
	public int endLevelDelay;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		atp = FindObjectOfType<ATP> ();
		hit = false;

		scoreCalculated = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (!levelManager.GetPause ()) {
			if (hit && !scoreCalculated) {
				//sugarsCollected = levelManager.GetSugarScore ()/4;
				//sugarsCollected = 3;
				Debug.Log ("Sugars: " + sugarsLaunched);
				counter = (sugarsLaunched * sugarLaunchDelay) + atpLaunchDelay + endLevelDelay;
				Debug.Log ("counter: " + counter);
				scoreCalculated = true;
				levelManager.StopTime ();
			}

			if (hit && counter >= 0) {

				if (sugarsLaunched == 0) {
					levelManager.EndLevel ();
				}

				if (counter % sugarLaunchDelay == 0 && counter > atpLaunchDelay + endLevelDelay) {
					Instantiate (sugar, firePoint.position, firePoint.rotation);
				} else if (counter == endLevelDelay) {
					atp.DropATP ();
				} else if (counter == 0) {
					levelManager.EndLevel ();
				}
				Debug.Log ("" + counter);

				counter--;
			}

		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Character") {
			levelManager.NotControlable ();
			hit = true;

		}
	}
}
