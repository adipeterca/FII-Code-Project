using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // The speed for horizontal movement.
    public float speed;
    // This adjusts the height of the jump.
    public float jumpPower;
    // Jump sprite.
    public Sprite jumpSprite;
    // Duck sprite.
    public Sprite duckSprite;
    // Default sprite.
    public Sprite defaultSprite;

    // Horizontal movement variable.
    float moveH;
    // The Y axis value from the frame before the current one.
    float lastY;
    // Variabile holding a private reference to the Rigidbody2D component of this game object.
    Rigidbody2D playerRb;
    // Reference to the Sprite Renderer component.
    SpriteRenderer playerSprite;
    // A necessary parameter for the SmoothDamp function.
    Vector3 refVelocity;
    // Was the "Jump" button pressed?
    bool jump;
    // Was the "Duck" button pressed?
    bool duck;
    // Can the player jump? (Is him on a ground surface?)
    bool canJump;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        lastY = transform.position.y;
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
        
        if (Input.GetButtonDown("Jump") && canJump)
        {
            jump = true;
            playerSprite.sprite = jumpSprite;
        }
        else if (Input.GetButtonDown("Duck") && canJump)
        {
            duck = true;
            playerRb.velocity = Vector3.zero;
            playerSprite.sprite = duckSprite;
        }

        if (Input.GetButtonUp("Duck") && duck)
        {
            duck = false;
            if (canJump)
                playerSprite.sprite = defaultSprite;
            else
                playerSprite.sprite = jumpSprite;
        }
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
        lastY = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            playerSprite.sprite = defaultSprite;
            canJump = true;
            jump = false;
            duck = false;
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
