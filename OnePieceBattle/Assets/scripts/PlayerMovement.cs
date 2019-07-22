using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public GameObject Bullet;
	public GameHandler gameHandler;
    public float runSpeed = 20f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
	bool attack = false;
    // Update is called once per frame
    void Update()
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
			if(SetAttack(1))
			{
				animator.SetFloat("Move_n", .1f);
				animator.SetTrigger("Attack");
				attack = true;
			}
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if(SetAttack(2))
			{	
				animator.SetFloat("Move_n", .6f);
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
    public virtual bool SetAttack(int a)
	{
		return false;
	}
}