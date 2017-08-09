using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
	//This needs to be on the Button Prefab
	//Don't forget to populate it with the LevelText and the Stars
	public Text LevelText;
	public int unlocked;
	public GameObject Star1;
	public GameObject Star2;
	public GameObject Star3;
}
