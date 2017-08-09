using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class RollingEnemyPatrol : MonoBehaviour {

	public float moveSpeed = 5;			// this value determines how fast enemies move
	private bool moveRight = false;			// this value determines whether the enemy moves left or right

	//public Transform wallCheck;				// this is a reference to the game object that collides with walls
	//private float wallCheckRadius = 0.1f;	// this is the radius for wallCheck
	//public LayerMask whatIsWall;			// this is a reference to what should be considered a wall (right now should be everthing)
	//private bool hittingWall;				// this is true when enemy is hitting a wall false otherwise

	//private bool atEdge;					// this is true when enemy is at an edge and false otherwise
	//public Transform edgeCheck;				// this is a reference to the game object that checks if enemy is at edge

	public int timeUntillTurn = 300;		// this is how long before the enemy will turn around on its own
	private int curTimeUntillTurn;

	private Rigidbody2D m_Rigidbody2D;	

	private LevelManager lvlManager;

	// Use this for initialization
	void Start () {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		curTimeUntillTurn = timeUntillTurn;
		lvlManager = FindObjectOfType<LevelManager> ();
	}

	// Update is called once per frame
	void Update () {
		if (!lvlManager.GetPause ()) {
			//hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);

			//atEdge = !Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);

			if (/*hittingWall || atEdge || */curTimeUntillTurn == 0) {
				curTimeUntillTurn = timeUntillTurn;
				moveRight = !moveRight;
				//transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}


			curTimeUntillTurn--;
			if (moveRight) {
				m_Rigidbody2D.velocity = new Vector2 (moveSpeed, m_Rigidbody2D.velocity.y);
				transform.Rotate (Vector3.forward);
			} else {
				m_Rigidbody2D.velocity = new Vector2 (-moveSpeed, m_Rigidbody2D.velocity.y);
				transform.Rotate (Vector3.back);
			}
		} else {
			m_Rigidbody2D.velocity = new Vector2 (0, 0);
			transform.Rotate (Vector3.zero);
		}

	}
}
