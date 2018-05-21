using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;


public class WallJump : MonoBehaviour
{
    public float distance = 1f;
    public float speed = 2f;
    private PlatformerCharacter2D script;
    private bool jumping;
    private float timeLeft;
    public float jumpTime = 1;
    private bool jumping2 = false;
    private bool jumping3;

    // Use this for initialization
    void Awake()
    {
        script = GetComponent<PlatformerCharacter2D>();
        jumping = script.wallJumping;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        script.wallJumping = jumping;
        Physics2D.queriesStartInColliders = false;
        if (jumping && Time.time >= timeLeft)
        {
            jumping = false;
        }

        if (jumping3 && script.m_FacingRight)
        {
            GetComponent<Rigidbody2D>().velocity = (new Vector2(-speed, speed));
            jumping3 = false;
            // script.Flip();
        }
        else if (jumping3 && !script.m_FacingRight)
        {
            GetComponent<Rigidbody2D>().velocity = (new Vector2(speed, speed));
            jumping3 = false;
            // script.Flip();
        }

        if (script.wallJumping && script.m_FacingRight && jumping2)
        {
            GetComponent<Rigidbody2D>().velocity = (new Vector2(-speed, speed));
            jumping3 = true;
            jumping2 = false;
        }

        if (script.wallJumping && !script.m_FacingRight && jumping2)
        {
            GetComponent<Rigidbody2D>().velocity = (new Vector2(speed, speed));
            jumping3 = true;
            jumping2 = false;
        }

        

    }
    private void Update()
    {
        if (script.m_FacingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

            if (Input.GetKeyDown(script.jumpButton) && !script.m_Grounded && hit.collider != null)
            {
                timeLeft = Time.time + jumpTime;
                jumping = true;
                jumping2 = true;
            }
        }

        if (!script.m_FacingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * -transform.localScale.x, distance);

            if (Input.GetKeyDown(script.jumpButton) && !script.m_Grounded && hit.collider != null)
            {
                timeLeft = Time.time + jumpTime;
                jumping = true;
                jumping2 = true;
            }
        }

    }
}
    

