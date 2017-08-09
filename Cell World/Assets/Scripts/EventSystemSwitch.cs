using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemSwitch : MonoBehaviour {

	private EventSystem eventSystem;

	private LevelManager lvlManager;

	public GameObject nextLevelButton;

	private bool setNextLevel;

	// Use this for initialization
	void Start () {
		eventSystem = GetComponent<EventSystem> ();

		lvlManager = FindObjectOfType<LevelManager> ();

		setNextLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (lvlManager.GetLevelOver () && !setNextLevel) {
			eventSystem.SetSelectedGameObject (nextLevelButton);
			setNextLevel = true;
		}
	}
}
