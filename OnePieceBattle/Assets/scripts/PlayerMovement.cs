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
        character.Bullet = this.transform.GetChild(0).gameObject;
        Hit_Controller hit_Controller = character.Bullet.GetComponent<Hit_Controller>();
        hit_Controller.Move1 = new ObjectEvent();
        hit_Controller.Move1.AddListener(character.Move1);
        hit_Controller.Move2 = new ObjectEvent();
        hit_Controller.Move2.AddListener(character.Move2);
        hit_Controller.Move3 = new ObjectEvent();
        hit_Controller.Move3.AddListener(character.Move3);
        hit_Controller.OnMoveFinished = new UnityEvent();
        hit_Controller.OnMoveFinished.AddListener(OnMoveFinished);
    }
    // Update is called once per frame
    public virtual void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
			if(character.SetAttack(1) && !crouch)
			{
				animator.SetFloat("Move_n", .2f);
				animator.SetTrigger("Attack");
				attack = true;
			}
        }
        if (Input.GetButtonDown("Fire2") && !crouch)
        {
            if(character.SetAttack(2))
			{	
				animator.SetFloat("Move_n", .5f);
				animator.SetTrigger("Attack");
				attack = true;
			}
        }
        if (Input.GetButtonDown("Fire3") && !crouch)
        {
            if (character.SetAttack(3))
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