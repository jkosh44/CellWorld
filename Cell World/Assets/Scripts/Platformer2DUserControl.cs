using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

		private bool controlable;
		private bool dead;

		public LevelManager levelManager;
		public Animator anim;

		//public bool mitochondria;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
			levelManager = FindObjectOfType<LevelManager> ();
			controlable = true;
			dead = false;
        }


        private void Update()
        {
			if (!levelManager.GetPause ()) {
				if (!m_Jump) {
					// Read the jump input in Update so button presses aren't missed.
					//Commented out m_Jump line assigns space bar to jump
					//m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
					m_Jump = Input.GetButtonDown ("Jump");
//				anim.Play("Jump");
				}
			}
        }


        private void FixedUpdate()
        {
			if (!levelManager.GetPause ()) {
				controlable = levelManager.getControlable ();
				dead = levelManager.getRespawn ();

				// Read the inputs.
				//Used to be left control for crouch, same method.
				bool crouch = false;//Input.GetKey(KeyCode.DownArrow);
				float h = CrossPlatformInputManager.GetAxis ("Horizontal");

				//"H" will hurt the character for testing purposes
				//bool hurt = Input.GetKeyDown (KeyCode.H);
            
				// Pass all parameters to the character control script.
				if (controlable && !dead) {
					m_Character.Move (h, crouch, m_Jump);
				} else {
					m_Character.Move (0, false, false);
				}
				m_Jump = false;

				//Hurt character
				//if(hurt){m_Character.Hurt();}

				if (Input.GetButtonDown ("Fire1") && controlable) {
					m_Character.Shoot ();
				}
			} else {
				m_Character.Move (0, false, false);
			}
        }
    }
}
