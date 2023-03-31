using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variable type is rigid body and rb is its name
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private SpriteRenderer sprite;
    private float dirX = 0f;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;

    ///Need to create the variable that holds all animation states
    private enum MovementState{idle,running,jumping,falling }

    [SerializeField]private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal");

        //multiplies by dirX because as it can be negative/backwards
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


        if (Input.GetButtonDown("Jump")  && IsGrounded())
        {   
            jumpSoundEffect.Play();
            //rb.velocity.x/y will keep the velocity of the opposing force the same
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        MovementState state;
        //checks to see if player is moving for animations
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        //uses the y velocity to know if our player is jumping
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        //typecasts enum variable to int to use while making connecting in the animator tab 
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        //creates box around player and moves down slightly to see if player can jump
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f ,Vector2.down, .1f, jumpableGround);
    }
}
