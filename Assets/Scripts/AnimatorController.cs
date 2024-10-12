using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    public void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
    }
    public void LateUpdate()
    {
       UpdateAnimator();
    }

    // TODO Fill this in with your animator calls
    void UpdateAnimator()
    {
        animator.SetFloat("CharacterSpeed", movement.GetAnimationSpeed());
        animator.SetBool("isFalling", !movement.isGrounded);
        if (movement.jumpCount == 2)
        {
            animator.SetTrigger("doFlip");
            movement.jumpCount = 0;
        }
    }
}
