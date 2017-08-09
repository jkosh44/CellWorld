using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class ProjectileEnemyPatrol : MonoBehaviour {

	public float moveSpeed = 3;			// this value determines how fast enemies move
	private bool moveRight = false;			// this value determines whether the enemy moves left or right

	public Transform wallCheck;				// this is a reference to the game object that collides with walls
	private float wallCheckRadius = 0.1f;	// this is the radius for wallCheck
	public LayerMask whatIsWall;			// this is a reference to what should be considered a wall (right now should be everthing)
	private bool hittingWall;				// this is true when enemy is hitting a wall false otherwise

	private bool atEdge;					// this is true when enemy is at an edge and false otherwise
	public Transform edgeCheck;				// this is a reference to the game object that checks if enemy is at edge

	public int timeUntillTurn = 50;		// this is how long before the enemy will turn around on its own
	private int curTimeUntillTurn;

	private Rigidbody2D m_Rigidbody2D;		
	//private Animator m_Anim; 

	private PlatformerCharacter2D player;	// this is a reference to the character
	private float attackDist;				// this is the distance that will cause the enemy to shoot
	private float distFromPlayer;			// this is the distance from the player

	private bool walk;						// this tells you wether or not the enemy is walking
	private bool facingRight;				// this tells you wether or not the enemy is facing right

	private LevelManager lvlManager;

	// Use this for initialization
	void Start () {
		//Debug.Log ("Enemy created, moveRight is " + moveRight + " x value is " + transform.localScale.x);
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		//m_Anim = GetComponent<Animator>();
		walk = true;
		facingRight = false;
		curTimeUntillTurn = timeUntillTurn;
		player = FindObjectOfType<PlatformerCharacter2D>();
		lvlManager = FindObjectOfType<LevelManager> ();
		attackDist = lvlManager.attackDistance;
	}
	
	// Update is called once per frame
	void Update () {

		if (!lvlManager.GetPause ()) {
			distFromPlayer = Vector3.Distance (player.transform.position, m_Rigidbody2D.transform.position);

			if (distFromPlayer < attackDist) {
				walk = false;
				if ((facingRight && player.transform.position.x < m_Rigidbody2D.transform.position.x) || (!facingRight && player.transform.position.x > m_Rigidbody2D.transform.position.x)) {
					transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
					facingRight = !facingRight;
					moveRight = !moveRight;
				}
			} else {
				walk = true;
			}

			//m_Anim.SetBool ("Walk", walk);

			hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);

			atEdge = !Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);

			if (hittingWall || atEdge || curTimeUntillTurn == 0) {
				curTimeUntillTurn = timeUntillTurn;
				moveRight = !moveRight;
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
				facingRight = !facingRight;
				//Debug.Log ("Enemy turned around, moveRight is now " + moveRight + " x value of enemy is now " + transform.localScale.x);
			}

			if (walk) {
				curTimeUntillTurn--;
				if (moveRight) {
					m_Rigidbody2D.velocity = new Vector2 (moveSpeed, m_Rigidbody2D.velocity.y);
				} else {
					m_Rigidbody2D.velocity = new Vector2 (-moveSpeed, m_Rigidbody2D.velocity.y);
				}
			} else {
				//what to do when not walking i.e. player is nearby
			}
			
		}
	}
}
