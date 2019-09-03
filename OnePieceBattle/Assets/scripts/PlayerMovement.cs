using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerMovement : MonoBehaviour
{
    public IChar character;
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed;
    protected Joystick joystick;
    public Joybutton move1;
    public Joybutton move2;
    public Joybutton finalMove;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
	bool attack = false;

    void OnEnable()
    {
        runSpeed = character.RunSpeed;
        controller = this.gameObject.GetComponent<CharacterController2D>();
        controller.OnLandEvent = new UnityEvent();
        controller.OnLandEvent.AddListener(OnLanding);
        controller.OnCrouchEvent = new BoolEvent();
        controller.OnCrouchEvent.AddListener(OnCrouching);
        controller.m_JumpForce = character.JumpForce;
        animator = this.gameObject.GetComponent<Animator>();
        character.GameHandler = GameObject.FindObjectOfType<GameHandler>();
        Hit_Controller hit_Controller = character.Bullet.GetComponent<Hit_Controller>();
        hit_Controller.Move1 = new ObjectEvent();
        hit_Controller.Move1.AddListener(character.Move1);
        hit_Controller.Move2 = new ObjectEvent();
        hit_Controller.Move2.AddListener(character.Move2);
        hit_Controller.Move3 = new ObjectEvent();
        hit_Controller.Move3.AddListener(character.Move3);
        hit_Controller.OnMoveFinished = new UnityEvent();
        hit_Controller.OnMoveFinished.AddListener(OnMoveFinished);
        joystick = FindObjectOfType<Joystick>();
        move1 = GameObject.FindWithTag("move1").GetComponent<Joybutton>();
        move2 = GameObject.FindWithTag("move2").GetComponent<Joybutton>();
        finalMove = GameObject.FindWithTag("finalMove").GetComponent<Joybutton>();
        
    }
    // Update is called once per frame
    public virtual void Update()
    {

        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalMove = joystick.Horizontal * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (joystick.Vertical > 0.5) //Input.GetButtonDown("Jump")
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (joystick.Vertical < -0.5 )  // Input.GetButtonDown("Crouch")
        {
            crouch = true;
        }
        else // if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (move1.Pressed) //Input.GetButtonDown("Fire1")
        {
			if(!attack && !crouch && character.SetAttack(1) )
			{
				animator.SetFloat("Move_n", .2f);
				animator.SetTrigger("Attack");
				attack = true;
			}
        }
        if (move2.Pressed)
        {
            if(!attack && !crouch && character.SetAttack(2))
			{	
				animator.SetFloat("Move_n", .5f);
				animator.SetTrigger("Attack");
				attack = true;
			}
        }
        if (finalMove.Pressed )
        {
            if (!attack && !crouch && character.SetAttack(3))
            {
                animator.SetFloat("Move_n", .9f);
                animator.SetTrigger("Attack");
                attack = true;
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
	public void OnMoveFinished()
	{
		attack = false;
	}
    void FixedUpdate()
    {
        // Move our character
		if(attack)
			return;
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

}