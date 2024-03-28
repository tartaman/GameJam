using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPFD_PlayerController : MonoBehaviour
{
    [Header("Player Keybinds")]
    public KeyCode Jump = KeyCode.Space;

    [Header("Player Components")]
    public Rigidbody RB;
    public Animator animator;
    public Animator flipAnimator;
    public SpriteRenderer SR;

    [Header("Movement Data")]
    public float moveSpeed;
    public float jumpForce;
    private Vector2 moveInput;

    [Header("Raycast Checks")]
    public LayerMask whatIsGround;
    public Transform groundPoint;
    public bool isGrounded;

    [Header("Movement Checks")]
    public bool isMovingBackwards;

    void Start()
    {
        RB = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Declares both moveInputs and normalize to preven diagonal movement boost.
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        //We use the moveInput.y as out Z axis indicator, mantaining the velocity that the RB already has on the y axis.
        RB.velocity = new Vector3(moveInput.x * moveSpeed, RB.velocity.y, moveInput.y * moveSpeed);

        //We tell the animator to set the value of moveSpeed to the magnitude of our RB velocity vector (Meaning how fast are we moving).
        animator.SetFloat("moveSpeed", RB.velocity.magnitude);

        //We use a raycast that goes from the groundpoint, downwards to see if we are touching the ground (the 0.3f goes because out groundPoint is at 0.75 or .25f off the ground.)
        RaycastHit hit;
        if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, 0.3f, whatIsGround, QueryTriggerInteraction.Collide))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //If we are groundded and we press the Jump Keybind, apply the force upwards.
        if(Input.GetKeyDown(Jump) && isGrounded)
        {
            RB.velocity += new Vector3(0f, jumpForce, 0f);
        }
        
        //We tell the animator that the Bool "OnGround" should change depending of the value of isGrounded
        animator.SetBool("onGround", isGrounded);

        //Flips the sprite if the player is moving to the right or left.
        if (!SR.flipX && moveInput.x < 0)
        {
            SR.flipX = true;
            //Activates the flip animation.
            flipAnimator.SetTrigger("Flip");
        }
        else if(SR.flipX && moveInput.x > 0)
        {
            SR.flipX = false;
            //Activates the flip animation.
            flipAnimator.SetTrigger("Flip");
        }

        //Changes the animation if the player is moving backwards.
        if(!isMovingBackwards && moveInput.y > 0)
        {
            isMovingBackwards = true;
            //Activates the flip animation.
            flipAnimator.SetTrigger("Flip");
        }
        else if(isMovingBackwards && moveInput.y < 0)
        {
            isMovingBackwards = false;
            //Activates the flip animation.
            flipAnimator.SetTrigger("Flip");
        }

        animator.SetBool("movingBackwards", isMovingBackwards);
    }
}
