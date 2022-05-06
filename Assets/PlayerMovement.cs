using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public KeyCode left_key;
    public KeyCode right_key;
    public KeyCode jump_key;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(left_key))
        {
            horizontalMove = -runSpeed;
        }else if (Input.GetKey(right_key))
        {
            horizontalMove = runSpeed;
        }else
        {
            horizontalMove = 0f;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKey(jump_key))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

}
