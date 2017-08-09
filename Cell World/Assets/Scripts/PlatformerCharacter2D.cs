using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
		public LevelManager levelManager;									// LevelManager 
		[SerializeField] public float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        //[SerializeField] private float m_JumpForce = 800f;                // Amount of force added when the player jumps.
		public float m_JumpForce;											//was 1350f
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

		private int health;					// Keeps track of the players health

		public float knockback;		// Distance that the player gets knocked back every second; was 15
		public float knockbackLength;		// The amount of time the player is in knockback was .3
		private float knockbackCount;		// Keeps track of how much longer the player will be in knockback
		public float kbx; //Knockbackspeed in x
		public float kby; //Knockbackspeed in y
		private bool knockFromRight;		// Keeps track of what direction the player gets knocked back in

		public Transform firePoint;			// Location where projectile is fired from
		public GameObject projectile;		// Object that character shoots
		public float fireRate;
		private float nextFire; 

		public AudioClip walksound;
		public AudioClip deniedsound;
		public AudioClip hurtsound;
		public AudioClip jumpsound;
		public AudioClip shotsound;

		private AudioSource source;




		private bool invincible;
		public float invicibilityTime;
		private float timeSinceInvincible;
		public float invicibilityTransparency;

		private bool dead;
		//public bool mitochondria;

		private float restoreGravity;

        private void Awake()
        {
            // Setting up references.
		//	if (mitochondria) {
				levelManager = FindObjectOfType<LevelManager> ();
		//	} else {
		//		levelManager = FindObjectOfType<LevelManagerNucleus> ();
		//	}
			m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

			//starts the player off with 5 health
			health = 5;

			timeSinceInvincible = 0;

			dead = false;
			source = GetComponent<AudioSource> ();

			restoreGravity = m_Rigidbody2D.gravityScale;
        }


        private void FixedUpdate()
        {
			if (!levelManager.GetPause ()) {
				m_Rigidbody2D.gravityScale = restoreGravity;
				m_Anim.enabled = true;

				m_Grounded = false;

				// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
				// This can be done using layers instead but Sample Assets will not overwrite your project settings.
				Collider2D[] colliders = Physics2D.OverlapCircleAll (m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
					if (colliders [i].gameObject != gameObject)
						m_Grounded = true;
				}
				m_Anim.SetBool ("Ground", m_Grounded);

				// Set the vertical animation
				m_Anim.SetFloat ("vSpeed", m_Rigidbody2D.velocity.y);

				m_Anim.SetBool ("Dead", dead);

				if (invincible) {
					Color charColor = GetComponent<SpriteRenderer> ().color;
					charColor = new Color (charColor.r, charColor.g, charColor.b, invicibilityTransparency);
					GetComponent<SpriteRenderer> ().color = charColor;

					gameObject.layer = 15;

					timeSinceInvincible += Time.deltaTime;
					if (timeSinceInvincible > invicibilityTime) {
						invincible = false;
						timeSinceInvincible = 0;
					}
				} else {
					Color charColor = GetComponent<SpriteRenderer> ().color;
					charColor = new Color (charColor.r, charColor.g, charColor.b, 1f);
					GetComponent<SpriteRenderer> ().color = charColor;

					gameObject.layer = 14;

				}
			} else {
				m_Anim.enabled = false;
				m_Rigidbody2D.gravityScale = 0;
				m_Rigidbody2D.velocity = new Vector2 (0, 0);
			}
        }


        public void Move(float move, bool crouch, bool jump)
        {
			if (!levelManager.GetPause ()) {
				if (!levelManager.getRespawn ()) {
					// If crouching, check to see if the character can stand up
					if (!crouch && m_Anim.GetBool ("Crouch")) {
						// If the character has a ceiling preventing them from standing up, keep them crouching
						if (Physics2D.OverlapCircle (m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) {
							crouch = true;
						}
					}

					// Set whether or not the character is crouching in the animator
					m_Anim.SetBool ("Crouch", crouch);

					//only control the player if grounded or airControl is turned on and the player is not in knockback
					if ((m_Grounded || m_AirControl) && knockbackCount <= 0) {
						// Reduce the speed if crouching by the crouchSpeed multiplier
						move = (crouch ? move * m_CrouchSpeed : move);

						// The Speed animator parameter is set to the absolute value of the horizontal input.
						m_Anim.SetFloat ("Speed", Mathf.Abs (move));

						// Move the character
						m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
//					source.clip = walksound;
//					source.Play ();
						// If the input is moving the player right and the player is facing left...
						if (move > 0 && !m_FacingRight) {
							// ... flip the player.
							Flip ();
						}
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight) {
							// ... flip the player.
							Flip ();
						}
//					walksound = (AudioClip)Resources.Load ("WalkingSound");
//					source.PlayOneShot (walksound);
					}
					// If the player should jump...
					if (m_Grounded && jump && m_Anim.GetBool ("Ground")) {
						// Add a vertical force to the player.
						m_Grounded = false;
						m_Anim.SetBool ("Ground", false);
						m_Anim.SetTrigger ("Jump");
						//m_Rigidbody2D.velocity = new Vector2 (0, m_JumpForce);
						m_Rigidbody2D.AddForce (new Vector2 (0f, m_JumpForce));
						source.clip = jumpsound;
			
						source.Play ();
						//Debug.Log ("Player just jumped with a force of " + m_JumpForce.ToString());
					}

					//handles knockback
					if (knockbackCount > 0) {
						if (knockFromRight) {
							m_Rigidbody2D.velocity = new Vector2 (-knockback * kbx, knockback * kby);
						} else {
							m_Rigidbody2D.velocity = new Vector2 (knockback * kbx, knockback * kby);
						}
						knockbackCount -= Time.deltaTime;
					}
				}
			}
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

		//method in charge of respanwing the character
		public void Respawn()
		{
			source.clip = deniedsound;
			source.Play ();
			//cancel knockback
			knockbackCount = 0;

			//refills health
			health = 5;

			//calls the LevelManagers Respawn function
			levelManager.KillPlayer();
		}

		//method for lowering health
		public void Hurt(bool fromRight)
		{
			if(!invincible) {
				knockFromRight = fromRight;
				knockbackCount = knockbackLength;

				health--;
				source.clip = hurtsound;
				source.volume = 0.4f;
				source.priority = 50;
				source.Play ();
				invincible = true;
				Debug.Log("Character hurt, health is " + health.ToString());
				if (health == 0) {
//					source.clip = deniedsound;
//					source.Play ();
					m_Rigidbody2D.velocity.Set (0, 0);
					m_Anim.SetTrigger ("Die");

//					deniedsound = (AudioClip)Resources.Load ("Denied Sound Effect");
//					source.PlayOneShot (deniedsound);
//					dead = true;
//					if (dead) {
//						source.clip = deniedsound;
//						source.Play ();
//					}
					Respawn();				
					//Animation.Play("DyingAnimation");
				}
			}
		}

		public int getHealth() {
			return health;
		}

		public void setDead(bool status) {
			dead = status;
		}

		public void setDieAnim() {
			m_Anim.SetTrigger ("Die");
		}

		public void Shoot() {
			if (!levelManager.getRespawn() && Time.time > nextFire) 
			{
				nextFire = Time.time + fireRate;
				Instantiate (projectile, firePoint.position, firePoint.rotation);
				source.clip = shotsound;
				source.Play ();
			} 

		}

		public bool GetInvincible() {
			return invincible;
		}

		public void MakeInvincible() {
			invincible = true;
		}
	}
}
