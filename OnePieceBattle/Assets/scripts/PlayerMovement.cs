using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	private Hit_Controller Hit_Controller;

	public float runSpeed = 40f;

	private float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	void Start()
	{
        Hit_Controller = gameObject.GetComponentInChildren<Hit_Controller>(true);
	}
	// Update is called once per frame
	void Update () {

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
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
		
		if (Input.GetButtonDown("Fire1")){
			Hit_Controller.SetAttack(1,(transform.localScale.x > 0));
			animator.SetFloat("Move_n",.1f);
			animator.SetTrigger("Attack");
		}
        if (Input.GetButtonDown("Fire2"))
        {
            Hit_Controller.SetAttack(2,(transform.localScale.x > 0));
            animator.SetFloat("Move_n", .6f);
            animator.SetTrigger("Attack");
        }
	}

	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
