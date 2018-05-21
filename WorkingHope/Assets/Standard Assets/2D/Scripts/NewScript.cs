using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class NewScript : MonoBehaviour {

    private GameObject player;

    private Transform playerPos;
    
    private PlatformerCharacter2D otherScript;

    private bool booleanScriptTwo;

    public GameObject prefab;

    public float leftBoxOffset;

    public float rightBoxOffset;

    public float vertBoxOffset;

    private GameObject hitBox;

    private Vector3 boxPos;

    private bool isAttacking;

    private float attackTimer;

    private Animator playerAnim;

    private bool facingRightAttack = false;

    private bool facingLeftAttack = false;

    private bool isAttacking2 = false;

    // Update is called once per frame
    void Update () {

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) {

            playerPos = player.GetComponent<Transform>();

            playerAnim = player.GetComponent<Animator>();

            otherScript = player.GetComponent<PlatformerCharacter2D>();

            playerAnim.SetBool("isAttacking", isAttacking);

            booleanScriptTwo = otherScript.m_FacingRight;

            if (Time.time - 0.16f > attackTimer && isAttacking)
            {
                isAttacking = false;
            }

            if (facingRightAttack && Time.time - 0.12f > attackTimer)
            {
                boxPos = new Vector3(playerPos.position.x + rightBoxOffset, playerPos.position.y - vertBoxOffset, playerPos.position.z);

                hitBox = Instantiate(prefab, boxPos, Quaternion.identity);

                hitBox.transform.SetParent(player.transform);

                facingRightAttack = false;
            }

            if (facingLeftAttack && Time.time - 0.12f > attackTimer)
            {
                boxPos = new Vector3(playerPos.position.x - leftBoxOffset, playerPos.position.y - vertBoxOffset, playerPos.position.z);

                hitBox = Instantiate(prefab, boxPos, Quaternion.identity);

                hitBox.transform.SetParent(player.transform);

                facingLeftAttack = false;
            }

            if (Input.GetButtonDown("Fire1") && !isAttacking2)
            {
                isAttacking = true;

                isAttacking2 = true;

                attackTimer = Time.time;

                if (booleanScriptTwo)
                {
                    facingRightAttack = true;
                }

                else
                {
                    facingLeftAttack = true;
                }
            }
            else if (hitBox != null && Time.time > attackTimer + 0.22f)
            {
                Destroy(hitBox);
                isAttacking2 = false;
            }
        }
	}
}
