using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    /*//this might be usefull later, with some changes
    public KeyCode left_key;
    public KeyCode right_key;
    public KeyCode jump_key;
    */
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    //state
    bool jump   = false,
         crouch = false;
    //button pressed
    private bool jumpPressed   = false,
                 crouchPressed = false;

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpPressed)
        {
            jump = true;
            animator.SetBool("isJumping", true);
            jumpPressed = false;
        }
        if (crouchPressed)
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    //Input System methods---------------
    //reads the input of the horizontal action (a-> -1 d->1; similar with other controls)
    public void Horizontal(InputAction.CallbackContext context)
    {
        float inputMovement = context.ReadValue<float>();
        horizontalMove = inputMovement * runSpeed;
    }
    //reads the input of the jump action
    public void Jump(InputAction.CallbackContext context)
    {
        jumpPressed = context.performed;

    }
    public void Crouch(InputAction.CallbackContext context)
    {
        crouchPressed = context.action.triggered;
    }


}
