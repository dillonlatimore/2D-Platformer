using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D myRigidbody;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private bool isGrounded;

    private Animator myAnim;

    public Vector3 respawnPosition;

    public LevelManager theLevelManager;

    public float knockbackXForce;
    public float knockbackLength;
    private float knockbackCounter;

    public AudioSource jumpSound;
	
    // Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        respawnPosition = transform.position;

        theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	
    // Update is called once per frame
	void Update () {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if(knockbackCounter <= 0)
        {
            // This controls player movement left and right as well as stopping the player
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            }

            // This makes the player jump, but only when they are on the ground so they 
            //can't keep jumping in midair
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }
        }

        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.deltaTime;

            if(transform.localScale.x > 0)
            {
                myRigidbody.velocity = new Vector3(-knockbackXForce, myRigidbody.velocity.y, 0f);
            } else
            {
                myRigidbody.velocity = new Vector3(knockbackXForce, myRigidbody.velocity.y, 0f);
            }
            
        }

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);
    }
    
    public void Knockback()
    {
        knockbackCounter = knockbackLength;
    }


    void OnTriggerEnter2D (Collider2D other)
    {
        //If player hits KillPlane they are Respawned at last checkpoint
        if(other.tag == "KillPlane")
        {
            theLevelManager.Respawn();
        }

        //When play collides with Checkpoint, sets this as new checkpoint
        if(other.tag == "Checkpoint")
        {
            respawnPosition = other.transform.position;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        transform.parent = null;
    }
}
