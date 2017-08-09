using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class EnemyProjectileController : MonoBehaviour {

	public float speed = 8f;

	public PlatformerCharacter2D player;

	public float rotationSpeed = 0f;//-360f; 

	private float launchForce = 800f;

	private Rigidbody2D m_RigidBody2D;

	private bool fromRight = false;

	private LevelManager lvlManager;

	private float restoreGravity;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlatformerCharacter2D> ();
		m_RigidBody2D = GetComponent<Rigidbody2D> ();
		lvlManager = FindObjectOfType<LevelManager> ();

		if (player.transform.position.x < transform.position.x) {
			speed = -speed;
			rotationSpeed = -rotationSpeed;
			fromRight = true;
		}

		m_RigidBody2D.AddForce (new Vector2 (0f, launchForce));
		restoreGravity = m_RigidBody2D.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (!lvlManager.GetPause ()) {
			m_RigidBody2D.gravityScale = restoreGravity;
			m_RigidBody2D.velocity = new Vector2 (speed, m_RigidBody2D.velocity.y);

			m_RigidBody2D.angularVelocity = rotationSpeed;
			//Debug.Log ("projectile has a speed of " + speed);
		} else {
			m_RigidBody2D.velocity = new Vector2 (0, 0);
			m_RigidBody2D.angularVelocity = 0f;
			m_RigidBody2D.gravityScale = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Character") {
			player.Hurt (fromRight);
		}
		Destroy (gameObject);
	}
}
