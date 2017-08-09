using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class BossProjectileController : MonoBehaviour {

	private float speed;

	private PlatformerCharacter2D player;
	private BossController boss;

	public float rotationSpeed;

	public float launchForce;

	private Rigidbody2D m_RigidBody2D;

	private bool fromRight = false;

	private LayerMask m_WhatIsGround;
	const float k_GroundedRadius = .2f;
	private bool m_Grounded;     
	private Transform m_GroundCheck;

	private LevelManager lvlManager;

	private Vector2 restoreVelocity;
	private float restoreGravity;
	//private bool velocityRestored;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlatformerCharacter2D> ();
		boss = FindObjectOfType<BossController> ();
		m_RigidBody2D = GetComponent<Rigidbody2D> ();

		lvlManager = FindObjectOfType<LevelManager> ();

		speed = boss.projectileSpeed;

		if (player.transform.position.x < transform.position.x) {
			//speed = -speed;
			rotationSpeed = -rotationSpeed;
			fromRight = true;

			m_GroundCheck = transform.Find("Ground Check");
		}

		//Vector2 direction = player.transform.position - transform.position;
		//m_RigidBody2D.AddForce (direction);

		m_RigidBody2D.velocity = (player.transform.position - transform.position).normalized * speed;

		restoreVelocity = m_RigidBody2D.velocity;
		restoreGravity = m_RigidBody2D.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 velocity = m_RigidBody2D.velocity;
		if (lvlManager.GetPause ()) {
			m_RigidBody2D.velocity = new Vector2 (0, 0);
			m_RigidBody2D.gravityScale = 0;
		} else {
			m_RigidBody2D.velocity = restoreVelocity;
			m_RigidBody2D.gravityScale = restoreGravity;
		}
		//m_RigidBody2D.velocity = (player.transform.position - transform.position).normalized * speed;
		//m_RigidBody2D.velocity = new Vector2 (speed, speed);

		//m_RigidBody2D.angularVelocity = rotationSpeed;
		//Debug.Log ("projectile has a speed of " + speed);
	}

	/*private void FixedUpdate()
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}

		if (m_Grounded) {
			m_RigidBody2D.velocity = (player.transform.position - transform.position).normalized * speed;
		}
	}*/

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Character") {
			player.Hurt (fromRight);
			//Destroy (gameObject);
		}
		Destroy (gameObject);
	}
}
