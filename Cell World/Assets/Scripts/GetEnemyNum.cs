using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnemyNum : MonoBehaviour {

	public int enemyNum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getNum() {
		return enemyNum;
	}

	public void setNum(int num) {
		enemyNum = num;
	}
}
