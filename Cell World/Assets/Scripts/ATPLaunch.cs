using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATPLaunch : MonoBehaviour {

	public float speed;

	//private float rotationSpeed = -360f; 

	public float launchForceX;
	public float launchForceY;

	private Rigidbody2D m_RigidBody2D;

	private LevelManager lvlManager;

	private float restoreGravity;
	private float restoreYSpeed;

	private bool notFirstFrameOfPause;
	private bool notFirstFrameOfUnpause;

	// Use this for initialization
	void Start () {
		m_RigidBody2D = GetComponent<Rigidbody2D> ();

		m_RigidBody2D.AddForce (new Vector2 (launchForceX, launchForceY));

		lvlManager = FindObjectOfType<LevelManager> ();
		restoreGravity = m_RigidBody2D.gravityScale;

		notFirstFrameOfPause = false;
		notFirstFrameOfUnpause = true;
	}

	// Update is called once per frame
	void Update () {
		if (!lvlManager.GetPause ()) {
			notFirstFrameOfPause = false;
			m_RigidBody2D.gravityScale = restoreGravity;
			if (!notFirstFrameOfUnpause) {
				m_RigidBody2D.velocity = new Vector2 (speed, restoreYSpeed);
				notFirstFrameOfUnpause = true;
			} else {
				m_RigidBody2D.velocity = new Vector2 (speed, m_RigidBody2D.velocity.y);
			}
		} else {
			notFirstFrameOfUnpause = false;
			if (!notFirstFrameOfPause) {
				restoreYSpeed = m_RigidBody2D.velocity.y;
				notFirstFrameOfPause = true;
			}
			m_RigidBody2D.gravityScale = 0;
			m_RigidBody2D.velocity = new Vector2 (0, 0);
		}

		//m_RigidBody2D.angularVelocity = rotationSpeed;
		//Debug.Log ("projectile has a speed of " + speed);
	}
}
