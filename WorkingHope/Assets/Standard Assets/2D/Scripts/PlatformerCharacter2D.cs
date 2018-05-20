using System;
using UnityEngine;


namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        public KeyCode jumpButton;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.
        public Rigidbody2D m_Rigidbody2D;
        Transform playerGraphics;
        private BoxCollider2D boxCol;
        public Vector2 offset;
        private bool directionVar = false;
        public float accelPlayer;
        public float forceLimit;
        public float accelPlayer2;
        public bool wallJumping;
        public float accelplayer3;


        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            playerGraphics = transform.Find("Graphics");
            boxCol = GetComponent<BoxCollider2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
                if (m_Rigidbody2D.velocity.y < 0)
                {
                    m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }

            }

        }


        public void Update()
        {
            if (m_Rigidbody2D.velocity.y > 0 && !Input.GetKey(jumpButton))
            {
                m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            
        }


        public void Move(float move, bool crouch, bool jump)
        {
            
            

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Move the character
                if (move == 1 && m_Rigidbody2D.velocity.x <= m_MaxSpeed)
                {
                    m_Rigidbody2D.AddForce(new Vector2(move * accelPlayer, 0));
                }
                else if (move == 0 && m_Rigidbody2D.velocity.x >= forceLimit && !wallJumping)
                {
                    m_Rigidbody2D.AddForce(new Vector2(-accelPlayer2, 0));
                }
                else if (move == 0 && m_Rigidbody2D.velocity.x <= -forceLimit && !wallJumping)
                {
                    m_Rigidbody2D.AddForce(new Vector2(accelPlayer2, 0));
                }
                else if (move == -1 && m_Rigidbody2D.velocity.x >= -m_MaxSpeed)
                {
                    m_Rigidbody2D.AddForce(new Vector2(move * accelPlayer, 0));
                }
                else if (move == 0 && m_Rigidbody2D.velocity.x >= forceLimit && wallJumping)
                {
                    m_Rigidbody2D.AddForce(new Vector2(accelplayer3, 0));
                }
                else if (move == 0 && m_Rigidbody2D.velocity.x <= -forceLimit && wallJumping)
                {
                    m_Rigidbody2D.AddForce(new Vector2(accelplayer3, 0));
                }
                if (move == 0 && m_Rigidbody2D.velocity.x <= forceLimit && m_Rigidbody2D.velocity.x >= -forceLimit && !wallJumping)
                {
                    m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
                }
                
                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }

        }


        public void Flip()
        {
            if (m_FacingRight)
            {
                boxCol.offset += offset;
                directionVar = true;
            }
            else if (!m_FacingRight && directionVar)
            {
                boxCol.offset -= offset;
            }
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = playerGraphics.localScale;
            theScale.x *= -1;
            playerGraphics.localScale = theScale;
        }
    }
}
