using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class BossController : MonoBehaviour {

	private LevelManager levelManager;

	private PlatformerCharacter2D player;

	public Transform spot0;
	public Transform spot1;
	public Transform spot2;
	public Transform spot3;
	public Transform spot4;
	public Transform spot5;

	public GameObject tentacleBR;
	public GameObject tentacleMR;
	public GameObject tentacleTR;
	public GameObject tentacleTL;
	public GameObject tentacleML;
	public GameObject tentacleBL;

	public Sprite redEyeDownSmile;
	public Sprite redEyeUpSmile;
	public Sprite redEyeDownFrown;
	public Sprite redEyeUpFrown;
	public Sprite deadSprite;

	public Sprite orangeEyeDownSmile;
	public Sprite orangeEyeUpSmile;
	public Sprite orangeEyeDownFrown;
	public Sprite orangeEyeUpFrown;

	private SpriteRenderer spriteRenderer;

	private int curSpot;

	private bool doneMoving;
	private bool doneAttacking;

	private bool wait;
	public float waitTimeAfterAttack;
	private float timeSinceLastAttack;

	private Vector3 lastPosit;

	public float speed;
	public float projectileSpeed;
	public float speedScaleUp;

	private bool facingRight;

	public Transform launchPoint;

	public GameObject bossProjectile;

	private int health;
	private bool hurting;

	public float waitTimeAfterHurt;
	private float timeSinceHurt;

	private bool dead;
	private bool levelEnding;

	public float waitTimeToEndLevel;
	private float timeSinceLevelEnded;

	private bool deadWaitingDone;
	private bool deadFlewOff;
	public float waitTimeAfterDeath;
	private float timeSinceDeath;
	private bool addedRigidbody;

	private Rigidbody2D m_RigidBody2D;

	public float deadLaunchForceX;
	public float deadLaunchForceY;
	public float deadRotationForce;

	private bool invincible;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		player = FindObjectOfType<PlatformerCharacter2D> ();

		curSpot = 0;

		doneMoving = true;
		doneAttacking = false;

		wait = false;
		timeSinceLastAttack = 0f;

		lastPosit = transform.position;

		facingRight = false;

		health = 7;
		hurting = false;

		timeSinceHurt = 0f;

		dead = false;
		levelEnding = false;

		deadWaitingDone = false;
		deadFlewOff = false;
		timeSinceDeath = 0;

		addedRigidbody = false;

		timeSinceLevelEnded = 0f;

		spriteRenderer = GetComponent<SpriteRenderer> ();

	}
	
	void FixedUpdate() {
		if (wait) {
			timeSinceLastAttack += Time.deltaTime;

			if (timeSinceLastAttack > waitTimeAfterAttack) {
				doneAttacking = true;
				timeSinceLastAttack = 0f;
				wait = false;
			}
		}
		if (levelEnding) {
			timeSinceLevelEnded += Time.deltaTime;

			if (timeSinceLevelEnded > waitTimeToEndLevel) {
				levelManager.EndLevel ();
			}
		}
		if (dead) {
			timeSinceDeath += Time.deltaTime;
			if (timeSinceDeath > waitTimeAfterDeath) {
				deadWaitingDone = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (!levelManager.GetPause ()) {
			if (hurting) {
				invincible = true;
			}

			if (deadWaitingDone && !deadFlewOff && !addedRigidbody) {
				gameObject.AddComponent<Rigidbody2D> ();
				m_RigidBody2D = GetComponent<Rigidbody2D> ();
				m_RigidBody2D.AddForce (new Vector2 (deadLaunchForceX, deadLaunchForceY));
				levelEnding = true;
				addedRigidbody = true;
			}

			if (deadWaitingDone) {
				m_RigidBody2D.angularVelocity = deadRotationForce;
			}

			if (!dead) {
				if (((facingRight && player.transform.position.x < transform.position.x) || (!facingRight && player.transform.position.x > transform.position.x)) && !hurting) {
					transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
					facingRight = !facingRight;
				}
			

				if (doneMoving && doneAttacking) {
					if (!hurting) {
						curSpot = (curSpot + 1) % 6;
						Move ();
					} else {
						timeSinceHurt += Time.deltaTime;

						if (timeSinceHurt > waitTimeAfterHurt) {
							timeSinceHurt = 0;
							hurting = false;
							changeSprite ();
						}
					}
				} else if (doneMoving && !doneAttacking) {
					invincible = false;
					Attack ();
				} else if (!doneMoving && doneAttacking) {
					Move ();
				} else if (!doneMoving && !doneAttacking) {
					Debug.Log ("This state should never be reached. " +
					"It means the boss is trying to move and attack at the same time.");
				}
			}
		}
		//Debug.Log ("doneMoving: " + doneMoving + "; doneAttacking: " + doneAttacking);
	}

	private Transform GetTarget(int spot) {
		if (spot == 0) {
			return spot0;
		} else if (spot == 1) {
			return spot1;
		} else if (spot == 2) {
			return spot2;
		} else if (spot == 3) {
			return spot3;
		} else if (spot == 4) {
			return spot4;
		} else if (spot == 5) {
			return spot5;
		} else {
			Debug.Log ("curSpot was not [0,5]");
			return spot0;
		}
	}

	private void Move() {
		doneMoving = false;

		Transform target = GetTarget (curSpot);
		lastPosit = transform.position;
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);

		if (transform.position == lastPosit) {
			doneMoving = true;
			doneAttacking = false;
			Debug.Log ("Reached new spot");
		}
	}

	private void ShootAttack() {
		Instantiate (bossProjectile, launchPoint.position, launchPoint.rotation);
	}

	private void StrectchAttack() {
		Vector2 armScale = tentacleBR.transform.localScale;
		armScale.x += 1f;
		tentacleBR.transform.localScale = armScale;

		Vector2 armPosit = tentacleBR.transform.position;
		armPosit.x += 1f;
		tentacleBR.transform.localPosition = armPosit;
	}

	private void Attack() {
		if (!wait) {
			int atk = (int)Random.Range (0, 2);
			Debug.Log ("atk: " + atk);
			//if (atk == 0 || health == 1) {
				ShootAttack ();
			//} else {
			//	StrectchAttack ();
			//}
			wait = true;
		}
		//doneAttacking = true;
	}

	private void Kill() {
		spriteRenderer.sprite = deadSprite;
		//gameObject.AddComponent<Rigidbody2D> ();

		dead = true;
		//levelEnding = true;
	}

	private void RemoveTentacle() {
		if (health == 7) {
			Destroy (tentacleTL);
		} else if (health == 6) {
			Destroy (tentacleML);
		} else if (health == 5) {
			Destroy (tentacleBL);
		} else if (health == 4) {
			Destroy (tentacleTR);
		} else if (health == 3) {
			Destroy (tentacleMR);
		} else if (health == 2) {
			Destroy (tentacleBR);
		}
	}

	private void changeToHurtSprite() {
		if (health == 6 || health == 5 || health == 4) {
			spriteRenderer.sprite = orangeEyeDownFrown;
		} else if (health == 3 || health == 2 || health == 1) {
			spriteRenderer.sprite = orangeEyeUpFrown;
		}
	}

	private void changeSprite() {
		if (health == 6 || health == 5) {
			spriteRenderer.sprite = redEyeDownSmile;
		} else if (health == 4 || health == 3) {
			spriteRenderer.sprite = redEyeDownFrown;
		} else if (health == 2 || health == 1) {
			spriteRenderer.sprite = redEyeUpFrown;
		}
	}

	private void Hurt() {
		hurting = true;
		doneMoving = true;
		doneAttacking = true;

		RemoveTentacle ();

		speed = speed * (1f+speedScaleUp);
		projectileSpeed = projectileSpeed * (1+speedScaleUp);

		health--;
		if (health == 0) {
			Kill ();
		}
		changeToHurtSprite ();
	}

	public bool GetDead() {
		return dead;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Bullet" && !invincible) {
			Hurt();
			Destroy (other.gameObject);
		}
	}
}
