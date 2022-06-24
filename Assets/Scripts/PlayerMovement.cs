using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

//class to handle inputs
public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public GameObject hand;
    private PlayerConfiguration playerConfig;
    private PlayerControls playerControls; //to handle inputs
    private Animator animator;


    public float runSpeed = 80f;
    float horizontalMove = 0f;

    int maxHealth = 0;
    int currentHealth = 0;

    //state
    bool jump   = false,
         crouch = false;
    
    //button pressed
    private bool jumpPressed   = false,
                 crouchPressed = false;

    //
    Vector3 aimedPos = new Vector3(0,0,0);


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
        gameObject.layer = LayerMask.NameToLayer("Player" + pc.playerIndex.ToString());

        //---- scripted object (Personaje) settings ----
        //set animator controller based on the character the player selected
        RuntimeAnimatorController AC = GameManager.Instance.personajes[pc.characterIndex].controladorAnimacion;
        animator.runtimeAnimatorController = AC;

        //set max health
        maxHealth = GameManager.Instance.personajes[pc.characterIndex].maxHealth;
        currentHealth = maxHealth;

        //default weapon, instantiated in the hand, as a child of that gameObject
        var weapon = GameManager.Instance.personajes[pc.characterIndex].defaultWeapon;
        Instantiate(weapon, hand.transform.position, hand.transform.rotation, hand.transform);
        

        //set listener function for actionTriggered, and change action map to gameplay map
        playerConfig.input.onActionTriggered += Input_onActionTriggered;
        playerConfig.input.SwitchCurrentActionMap("Player");
        
        if (hand == null)
        {
            hand = transform.GetChild(2).gameObject;
        }
        Debug.Log("Init player" + playerConfig.playerIndex);

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
        //shoot
        if (context.action.name == playerControls.Player.Shoot.name)
        {
            Shoot(context);
        }
        //shoot
        if (context.action.name == playerControls.Player.Aim.name)
        {
            //world pos of the target
            if (context.action.triggered)
                aimedPos = context.ReadValue<Vector2>();

            if (context.control.device.name.Contains("Mouse"))
            {
                aimedPos = Camera.main.ScreenToWorldPoint(aimedPos);
                aimedPos.z = 0f;
            }
            else
            {
                if (hand)
                    aimedPos = hand.transform.position + aimedPos*2;
                else Debug.Log("por alguna razon se le ha caido la mano ªªªªªª te me cuidas");

            }
        }
        


        //openMenu/...

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
        if (Time.timeScale != 0)
        {
            RotateGun();

            SetCrossHairPos();
        }
    }
    // Physics Update
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void TakeDamage(int dmg)
    {
        Debug.Log(playerConfig.playerIndex + " health " + currentHealth);
        currentHealth -= dmg;
        if (currentHealth<=0)
        {
            GameManager.Instance.playerDied(playerConfig.playerIndex);
        }
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
        //this only affects controller input as it is analog, keyboard is already -1.0 or 1.0
        float inputMovement = Mathf.Clamp( context.ReadValue<float>(), -1.0f, 1.0f  );

        horizontalMove = inputMovement * runSpeed;
    }
    //reads the input of the jump action
    public void Jump(InputAction.CallbackContext context)
    {
        jumpPressed = context.action.triggered;

    }
    //reads input of crouch action
    public void Crouch(InputAction.CallbackContext context)
    {
        crouchPressed = context.action.triggered;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            WeaponController w=null;
            if (hand)
            {
                w = hand.GetComponentInChildren<WeaponController>();
                Debug.Log("por alguna razon se le ha caido la mano ª te me cuidas");

            }
            if (w)
                w.Shoot(gameObject.layer);
            else
            {
                Debug.Log("no weapon hehe");
            }
        }
    }

    private void RotateGun()
    {
        //aimedPos = Mouse.current.position.ReadValue();
        Vector3 aimedDir = (aimedPos - hand.transform.position).normalized;
        float anglez = Mathf.Atan2(aimedDir.y, aimedDir.x) * Mathf.Rad2Deg;
        float anglex = 0f;
        //Vector3 rotation = new Vector3(0,0, Vector2.SignedAngle(hand.transform.position, aimedPos));
        //float angle = Vector3.SignedAngle(aimedPos, hand.transform.right, Vector3.forward);
           //anglez = anglez * transform.right.x;
        //Debug.Log("angle " + anglez);

        if (transform.right.x<0)
        {
            anglex -= 180f;
            anglez -= 180f;
        }

        //if (Mathf.Abs(anglez) > 90)
        //{
        //    anglex += 180f;
        //    anglez += -anglez;
        //}

        hand.transform.localEulerAngles = new Vector3(-anglex, 0,anglez);
    }
    private void SetCrossHairPos()
    {
        //aimedPos = (aimedPos - transform.position);
        
        //transform.GetChild(3).transform.position = aimedPos;
        transform.GetChild(3).position = aimedPos ;

        //Debug.Log("aimedPos " + aimedPos.ToString());


    }
}
