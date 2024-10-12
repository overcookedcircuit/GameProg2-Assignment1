using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Vector3 playerVelocity;
    Vector3 move;

    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float jumpHeight = 2;
    public int maxJumpCount = 2;
    public int jumpCount = 0;
    public float gravity = -9.18f;
    public bool isGrounded;
    public bool isRunning;
    private CharacterController controller;
    private Animator animator;

    // New fields for double jump power-up
    public bool canDoubleJump = false;
    private float doubleJumpDuration = 30f; // Double jump enabled for 30 seconds
    private float doubleJumpTimer = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void ProcessMovement()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Turns the player towards the direction they are heading 
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Are you pressing ALT to run
        isRunning = Input.GetButton("Run");

        if (isGrounded)
        {
            jumpCount = 0;
        }

        // Handle JUMPING mechanic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            jumpCount++;
        }

        // Double jump mechanic: Only allow double jump if `canDoubleJump` is true
        if (Input.GetButtonDown("Jump") && !isGrounded && jumpCount == 1 && canDoubleJump)
        {
            // Double jump
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

            // Set counter back to 0 after the double jump
            jumpCount++;
        }

        // Moves the player
        controller.Move(move * Time.deltaTime * ((isRunning) ? runSpeed : walkSpeed));
    }

    public void Update()
    {
        if (animator.applyRootMotion == false)
        {
            ProcessMovement();
        }
        ProcessGravity();

        // Handle the double jump timer
        if (canDoubleJump)
        {
            doubleJumpTimer -= Time.deltaTime;
            if (doubleJumpTimer <= 0f)
            {
                canDoubleJump = false; // Disable double jump after the timer runs out
            }
        }
    }

    public void ProcessGravity()
    {
        // Since there is no physics applied on character controller we apply gravity manually
        if (isGrounded)
        {
            if (playerVelocity.y < 0.0f) // Keep the player grounded when on the ground
            {
                playerVelocity.y = -1.0f;
            }
        }
        else // if not grounded
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        controller.Move(playerVelocity * Time.deltaTime);
        isGrounded = controller.isGrounded;
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        velocity.y = playerVelocity.y * Time.deltaTime;

        controller.Move(velocity);
    }

    public float GetAnimationSpeed()
    {
        if (isRunning && (move != Vector3.zero)) // Left shift to run
        {
            return 1.0f;
        }
        else if (move != Vector3.zero)
        {
            return 0.5f;
        }
        else
        {
            return 0f;
        }
    }

    // Call this method when the player picks up the power-up (e.g., the cube)
    public void EnableDoubleJump()
    {
        canDoubleJump = true;
        doubleJumpTimer = doubleJumpDuration; // Start the 30-second timer
    }
}
