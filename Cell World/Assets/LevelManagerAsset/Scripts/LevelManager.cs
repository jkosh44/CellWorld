using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManagerFake : MonoBehaviour {

	//Drag this on a Empty GameObject
	//Popoluate it with the Spacer GameObject and the Button
	//set The amount of Levels
	//set the 1st one to be unlocked and interactable
	//give all levels a name of the number of the level(1, 2, 3, 4 ... and so on)
	[System.Serializable]
	public class Level
	{
		public string LevelText;
		public int UnLocked;
		public bool IsInteractable;
	}
	public GameObject levelButton;//the button prefab needs to be dragged in
	public Transform Spacer;//create a gameobject in the canvas and drag it into
	public List<Level> LevelList;
	//Score - all level use the same score, so don't give to much score in higher levels
	//Can also be changed in the levelManager GameObject
	public int Star1Points = 5000;//the score the player needs to unlock the first star
	public int Star2Points = 10000;//the score the player needs to unlock the second star
	public int Star3Points = 20000;//the score the player needs to unlock the third star

	void Start () 
	{
		FillList ();
	}

	void FillList()
	{
		foreach(var level in LevelList)
		{
			GameObject newbutton = Instantiate(levelButton) as GameObject;//create the button depend on the given prefab
			LevelButton button = newbutton.GetComponent<LevelButton>();//get the levebutton component of the created button
			button.LevelText.text = level.LevelText;//set the leveltext set in the levelmanager onto the button
			//if the current looped button has a saved value of 1 (is unlocked), then set it to be unlocked and interactable
			if(PlayerPrefs.GetInt("Level" + button.LevelText.text) == 1)
			{
				level.UnLocked = 1;
				level.IsInteractable = true;
			}
			//set unlocked state
			button.unlocked = level.UnLocked;
			//set interactable state
			button.GetComponent<Button>().interactable = level.IsInteractable;
			//add a listener with a function on it to load the right level when the button is clicked
			button.GetComponent<Button>().onClick.AddListener(() => loadLevels("Level" + button.LevelText.text));
			//check stars depending on score
			if(PlayerPrefs.GetInt("Level"+ button.LevelText.text + "_score") >= Star1Points)
			{
				button.Star1.SetActive(true);
			}

			if(PlayerPrefs.GetInt("Level"+ button.LevelText.text + "_score") >= Star2Points)
			{
				button.Star2.SetActive(true);
			}

			if(PlayerPrefs.GetInt("Level"+ button.LevelText.text + "_score") >= Star3Points)
			{
				button.Star3.SetActive(true);
			}
			//set the parent to be the spacer which needs to be in the canvas
			newbutton.transform.SetParent(Spacer,false);
		}
		SaveAll ();//perform a save only the first time the game has been started
	
	}

	void SaveAll()
	{
		if(PlayerPrefs.HasKey("Level1"))//if it has been saved already
		{
			return;// don't do anything
		}
		else//if not
		{
			//go through all existing buttons and save
			GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
			foreach (GameObject buttons in allButtons)
			{
				LevelButton button = buttons.GetComponent<LevelButton>();
				PlayerPrefs.SetInt("Level" + button.LevelText.text, button.unlocked);
			}
		}
	}
	//if you want to delete all saved values use this
	public void DeleteAll()
	{
		PlayerPrefs.DeleteAll ();
	}
	//thats the function to load the right level once clicked on a level button
	void loadLevels(string value)
	{
		//Application.LoadLevel (value);
		SceneManager.LoadScene(value);
	}
}
