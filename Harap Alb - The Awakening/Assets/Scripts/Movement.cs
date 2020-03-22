using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Reference to the PlayerStats script.
    public PlayerStats stats;

    // The speed for horizontal movement.
    float speed;
    // This adjusts the height of the jump.
    float jumpPower;
    // Horizontal movement variable.
    float moveH;
    // Variabile holding a private reference to the Rigidbody2D component of this game object.
    Rigidbody2D playerRb;
    // A necessary parameter for the SmoothDamp function.
    Vector3 refVelocity;
    // Animator reference.
    Animator anim;
    // CapsuleCollider2D reference.
    CapsuleCollider2D capsule2d;
    // CircleCollider2D reference.
    CircleCollider2D circle2d;
    // Was the "Jump" button pressed?
    bool jump;
    // Was the "Duck" button pressed?
    bool duck;
    // Can the player jump? (Is him on a ground surface?)
    bool canJump;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsule2d = GetComponent<CapsuleCollider2D>();
        circle2d = GetComponent<CircleCollider2D>();
        speed = stats.speed;
        jumpPower = stats.jumpPower;
        jump = false;
        duck = false;
        canJump = true;
        refVelocity = Vector3.zero;
    }

    private void Update()
    {
        if (PauseMenu.gameIsPaused)
            return;

        moveH = Input.GetAxisRaw("Horizontal");

        anim.SetBool("Walking", moveH != 0 ? true : false);
        if ((moveH > 0 && transform.localScale.x < 0) || (moveH < 0 && transform.localScale.x > 0))
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        
        if (Input.GetButtonDown("Jump") && canJump)
        {
            jump = true;
            anim.SetBool("Jumping", true);
        }
        else if (Input.GetButtonDown("Duck") && canJump)
        {
            duck = true;
            anim.SetBool("Crouching", true);

            capsule2d.size -= new Vector2(0, 1);
            capsule2d.offset -= new Vector2(0, 0.5f);
            playerRb.velocity = Vector3.zero;
        }

        if (Input.GetButtonUp("Duck") && duck)
        {
            duck = false;
            capsule2d.size += new Vector2(0, 1);
            capsule2d.offset += new Vector2(0, 0.5f);
            anim.SetBool("Crouching", false);
        }

        if (!duck && canJump && !jump && Input.GetButtonDown("Space_Jump"))
            anim.SetTrigger("Attack");
    }

    private void FixedUpdate()
    {
        if (PauseMenu.gameIsPaused)
            return;

        if (!duck)
        {
            Vector3 targetVelocity = new Vector2(moveH * speed, playerRb.velocity.y);
            playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, targetVelocity, ref refVelocity, 0.05f);
        }
        if (jump)
        {
            Vector3 targetVelocity = new Vector2(playerRb.velocity.x, jumpPower);
            playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, targetVelocity, ref refVelocity, 0.05f);
            jump = false;
            canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            canJump = true;
            jump = false;
            duck = false; anim.SetBool("Jumping", false);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            canJump = false;
        }
    }
}
