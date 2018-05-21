using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooMonsterScript : MonoBehaviour
{

    public Rigidbody2D rb;
    public float distance;
    public float speedX;
    public float speedY;
    public bool isHorizontal = true;
    public Transform transformMonster;

    private float timeLeft;
    private int direction = 1;

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
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
            Vector3 theScale = transformMonster.localScale;
            theScale.x *= -1;
            transformMonster.localScale = theScale;
            direction *= -1;
        }

        rb.velocity = new Vector2(speedX * direction, speedY * direction);
    }
}