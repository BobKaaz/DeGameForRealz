using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Rigidbody2D rb;
    public float distance;
    public float speedX;
    public float speedY;
    public bool isHorizontal = true;
    public Rigidbody2D platformRb;

    private float timeLeft;
    private int direction = 1;
    private float nextTimeToSearch = 0;
    private Vector2 lastPos;
    private Vector2 posDif;

    // Use this for initialization
    void Start () {
        if (isHorizontal)
        {
            timeLeft = Time.time + (distance / speedX);
        }
        else
        {
            timeLeft = Time.time + (distance / speedY);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Time.time >= timeLeft)
        {
            if (isHorizontal)
            {
                timeLeft = Time.time + (distance / speedX);
            }
            else
            {
                timeLeft = Time.time + (distance / speedY);
            }
            direction *= -1;
        }

        posDif = platformRb.position - lastPos;
        lastPos = platformRb.position;

        rb.velocity = new Vector2(speedX * direction, speedY * direction);

	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Rigidbody2D>().position += posDif;
    }
}
