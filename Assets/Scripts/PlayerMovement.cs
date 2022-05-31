using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    private PlayerConfiguration playerConfig;
    private PlayerControls playerControls;
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

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    public void Initialize(PlayerConfiguration pc)
    {
        playerConfig = pc;
        RuntimeAnimatorController AC = GameManager.Instance.personajes[pc.characterIndex].controladorAnimacion;
        //assign the animator controller based on the character the player selected
        animator.runtimeAnimatorController = AC;

        playerConfig.input.onActionTriggered += Input_onActionTriggered;
        playerConfig.input.SwitchCurrentActionMap("Player");
    }

    private void Input_onActionTriggered(InputAction.CallbackContext context)
    {
        //--------------assign all actions here -------------
        //move horizontally
        if (context.action.name == playerControls.Player.Horizontal.name)
        {
            Horizontal(context);
        }
        //Jump
        if (context.action.name == playerControls.Player.Jump.name)
        {
            Jump(context);
        }//Crouch
        if (context.action.name == playerControls.Player.Crouch.name)
        {
            Crouch(context);
        }
        //...

    }
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
