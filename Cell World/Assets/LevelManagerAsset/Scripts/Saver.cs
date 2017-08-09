using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Saver : MonoBehaviour {
	
	public int score;//score holder
	public string LevelManagerName = "LevelManager";
//!!!!!!!! IMPORTANT
	private int LevelAmount = 3;//change this to the amount of level you have in your levelmanager - at least 1
//!!!!!!!! IMPORTANT END

	private int CurrentLevel;//current level holder

	public void SetScore(int scoreAmount)//call this function once the level is completed and pass in this score
	{
		score = scoreAmount;//stores the score
		CheckCurrentLevel ();//call next function
	}

	void CheckCurrentLevel()
	{
		//this function checks in which level we are depending on the name
		for (int i = 1; i <= LevelAmount; i++)
		{
			//if (Application.loadedLevelName == "Level" + i)//for older versions of unity
			if(SceneManager.GetActiveScene().name == "Level" + i)//unity 5.3+
			{
				CurrentLevel = i;//store the found level 
				SaveMyGame ();// call next function
			}
		}
	}

	void SaveMyGame()
	{
		//in here we started to save all needed data
		int NextLevel = CurrentLevel + 1;// this is needed to unlock the next level
		if (NextLevel < LevelAmount+1) {//to prevent overflow
			PlayerPrefs.SetInt ("Level" + NextLevel.ToString (), 1);//unlock next level
			if (PlayerPrefs.GetInt ("Level" + CurrentLevel.ToString () + "_score") < score) //check if the current score is higher then the already stored
			{
				PlayerPrefs.SetInt ("Level" + CurrentLevel.ToString () + "_score", score);//if so, save
			}
		} 
		else //if it's the last possible level
		{
			if (PlayerPrefs.GetInt ("Level" + CurrentLevel.ToString () + "_score") < score) //check if the current score is higher then the already stored
			{
				PlayerPrefs.SetInt ("Level" + CurrentLevel.ToString () + "_score", score);//if so, save
			}
		}
		BackToLevelSelect ();//call next function
	}

	void BackToLevelSelect()
	{
		//In here we just go back to the Level Select Menu
		//Application.LoadLevel (LevelManagerName);//for older versions of unity
		SceneManager.LoadScene(LevelManagerName);//unity 5.3+
	}
}
