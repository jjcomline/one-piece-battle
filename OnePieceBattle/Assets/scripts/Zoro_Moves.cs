﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoro_Moves : MonoBehaviour,IChar
{
    int damage = 5;
    int energy = 5;
    int move = 1;
    GameHandler gameHandler;
    public GameObject bullet;
    Collider2D coll;
    const float runSpeed = 30f;
    const float jumpForce = 400f;
    const int maxHealth = 230;
    const int maxPower = 100;
    bool isPlayer;

    public GameHandler GameHandler {set => gameHandler = value; }
    public float RunSpeed { get => runSpeed;}
    public int Health { get => maxHealth;}
    public int Power { get => maxPower;}
    public GameObject Bullet { get => bullet; set => bullet = value; }
    public float JumpForce { get => jumpForce;}
    public bool IsPlayer { set => isPlayer = value;} 

    public void Move1(GameObject gmOb)
    {
       StartCoroutine(Delay(0.08f));
    }
    public void Move2(GameObject gmOb)
    {
        int facingRight;
        if (transform.localScale.x > 0)
            facingRight = 1;
        else
            facingRight = -1;
        Rigidbody2D rb2d = gmOb.GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(facingRight * 5f, 2f), ForceMode2D.Impulse);
        StartCoroutine(Delay(0.11f));
    }
    public void Move3(GameObject gmOb)
    {   
        int facingRight;
        if (transform.localScale.x > 0)
            facingRight = 1;
        else
            facingRight = -1;
        Rigidbody2D rb2d = gmOb.GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(facingRight * 5f, 2f), ForceMode2D.Impulse);
        StartCoroutine(Delay(3f));
    }
    public bool SetAttack(int a)
    {
        move = a;
        if (move == 1)
        {
            damage = 10;
            energy = 5;
        }
        if (move == 2)
        {
            
            if (!gameHandler.TryAttack(-20, isPlayer))
            {
                return false;
            }
            damage = 15;
            energy = -20;
        }

        if (move == 3)
        {   
            if (!gameHandler.TryAttack(-70, isPlayer))
            {   
                return false;
            }
            damage = 50;
            energy = -50;

        }
        return true;

    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        gameHandler.Attack(damage, energy, isPlayer);
    }
}
