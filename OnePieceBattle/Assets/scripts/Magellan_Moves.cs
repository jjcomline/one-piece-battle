using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magellan_Moves : MonoBehaviour, IChar
{
    int damage = 5;
    int energy = 5;
    int move = 1;
    GameHandler gameHandler;
    public GameObject bullet;
    Collider2D coll;
    const float runSpeed = 15f;
    const float jumpForce = 350f;
    const int maxHealth = 250;
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
       StartCoroutine(Delay(1f));
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
        StartCoroutine(Delay(1.2f));
    }
    public void Move3(GameObject gmOb)
    {   
        StartCoroutine(Delay(2.8f));
    }
    public bool SetAttack(int a)
    {
        move = a;
        if (move == 1)
        {
            damage = 3;
            energy = 5;
        }
        else if (move == 2)
        {
            damage = 15;
            energy = -20;
            if (!gameHandler.TryAttack(energy, isPlayer))
            {
                return false;
            }
        }
        else if (move == 3)
        {   
            string who;
            damage = 35;
            energy = -70;

            who = isPlayer ? "enemy" : "player"; 

            if (gameHandler.TryAttack(energy, isPlayer))
            {   
                Move3(GameObject.FindWithTag(who));
            }
            else return false;

        }
        return true;

    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        gameHandler.Attack(damage, energy, isPlayer);
    }
}
