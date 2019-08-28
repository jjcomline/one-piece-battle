using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnemyMovement : MonoBehaviour
{
    public IChar character;
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed;
    public GameObject player;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
	bool attack = false;
    private int right;
    private int action = 0; 

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

        InvokeRepeating("Action", 0.0f, 1.0f);
    }
    // Update is called once per frame
    public virtual void Update()
    {
    }

    private void Action () {
        if (action%5 == 0 && !attack){
            crouch = true;
        }
        else crouch = false;

        if (action%3 == 0 && !attack){
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        Moving();
        action++;
    }

    public void Moving() {
        Vector2 position_player = player.transform.position;
        Vector2 position_enemy = this.transform.position;
        float distance = position_enemy.x - position_player.x;
        
        right = position_enemy.x<position_player.x ? 1 : -1;
        if (Math.Abs(distance) < 1) {
            horizontalMove = right*0.001f;
            if (UnityEngine.Random.Range(0f, 1f)<0.1f && character.SetAttack(2) && !crouch && !jump)
            {
                animator.SetFloat("Move_n", .5f);
                animator.SetTrigger("Attack");
                attack = true;
            }
            else if (character.SetAttack(3) && !crouch && !jump)
            {
                animator.SetFloat("Move_n", .9f);
                animator.SetTrigger("Attack");
                attack = true;
            }
            else if (character.SetAttack(1) && !crouch)
			{
				animator.SetFloat("Move_n", .2f);
				animator.SetTrigger("Attack");
				attack = true;
			}
        }
        else  
            horizontalMove = right * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
