using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

//class to handle inputs
public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    private PlayerConfiguration playerConfig;
    private PlayerControls playerControls; //to handle inputs
    private Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    //state
    bool jump   = false,
         crouch = false;
    
    //button pressed
    private bool jumpPressed   = false,
                 crouchPressed = false;

    //on creation obtain animator component and initialize playerControls
    private void Awake()
    {
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
    }

    //initialize player from playerConfig
    public void Initialize(PlayerConfiguration pc)
    {
        playerConfig = pc;

        //set animator controller based on the character the player selected
        RuntimeAnimatorController AC = GameManager.Instance.personajes[pc.characterIndex].controladorAnimacion;
        animator.runtimeAnimatorController = AC;

        //set listener function for actionTriggered, and change action map to gameplay map
        playerConfig.input.onActionTriggered += Input_onActionTriggered;
        playerConfig.input.SwitchCurrentActionMap("Player");
    }

    //read all actions and redirect to each specific action
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
        //shoot/openMenu/...

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
    // Physics Update
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    //---------------animation methods-----------------
    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    //---------------Input System methods---------------
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
    //reads input of crouch action
    public void Crouch(InputAction.CallbackContext context)
    {
        crouchPressed = context.action.triggered;
    }


}
