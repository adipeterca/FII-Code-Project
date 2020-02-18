using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /// The speed for horizontal movement.
    public float speed;
    /// This adjusts the height of the jump.
    public float jumpPower;
    /// This restricts the player from jumping to infinity.
    public float jumpTime;
    /// Jump sprite.
    public Sprite jumpSprite;
    /// Duck sprite.
    public Sprite duckSprite;
    /// Default sprite.
    public Sprite defaultSprite;

    /// Horizontal movement variable.
    float moveH;    
    /// Vertical movement variable.
    float moveV;
    /// True if the player is in the duck position or false otherwise.
    bool isDucking;
    /// Variabile holding a private reference to the Rigidbody2D component of this game object.
    Rigidbody2D playerRb;
    /// A counter necessary for the jump time.
    float currentTime;
    ///  Reference to the Sprite Renderer component.
    SpriteRenderer playerSprite;


    bool canJump;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        currentTime = 0;
        isDucking = false;
        canJump = true;
    }

    //private void FixedUpdate()
    //{
    //    moveV = Input.GetAxisRaw("Vertical");

    //    currentTime -= Time.deltaTime;

    //    if (moveV == -1 && currentTime <= 0)
    //    {
    //        playerSprite.sprite = duckSprite;
    //        isDucking = true;
    //        Debug.Log("Duck");
    //    }
    //    else
    //    {
    //        isDucking = false;
    //        if(moveV == 0 && currentTime <=0)
    //            playerSprite.sprite = defaultSprite;
    //        if (currentTime <= 0 && moveV == 1)
    //        {
    //            playerRb.AddForce(new Vector2(0, moveV * jumpPower));
    //            playerSprite.sprite = jumpSprite;
    //            currentTime = jumpTime;
    //            Debug.Log("Jump");
    //        }   
    //    }
    //}

    private void FixedUpdate()
    {
        moveV = Input.GetAxisRaw("Vertical");

        if (moveV != -1)
        {
            isDucking = false;
            if (!canJump)
                playerSprite.sprite = defaultSprite;
            Debug.Log("He is not ducking.");
        }

        if (canJump)
        {
            if (moveV == -1)
            {
                playerSprite.sprite = duckSprite;
                isDucking = true;
                canJump = false;
                Debug.Log("Duck");
            }
            else
            {
                playerSprite.sprite = defaultSprite;
                if (moveV == 1)
                {
                    playerRb.AddForce(new Vector2(0, moveV * jumpPower));
                    playerSprite.sprite = jumpSprite;
                    canJump = false;
                    Debug.Log("Jump");
                }
            }
        }
    }

    private void Update()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        if (moveH != 0 && !isDucking)
        {
            transform.Translate(new Vector3(moveH * speed * Time.deltaTime, 0, 0));
            Debug.Log("Moved along the horizontal axis.");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Platform"))
        {
            canJump = true;
            Debug.Log("Collision detected.");
        }
    }

}
