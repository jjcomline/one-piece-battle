using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luffy_Moves : MonoBehaviour,IChar
{
    int damage = 5;
    int energy = 5;
    int move = 1;
    GameHandler gameHandler;
    public GameObject bullet;
    Collider2D coll;
    const float runSpeed = 30f;
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
        gameHandler.Attack(damage, energy, isPlayer);
        StartCoroutine(Delay(0.6f));
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
        gameHandler.Attack(damage, energy, isPlayer);
        StartCoroutine(Delay(7));
    }
    public bool SetAttack(int a)
    {
        move = a;
        if (move == 1)
        {
            damage = 3;
            energy = 8;
        }
        if (move == 2)
        {
            if (!gameHandler.TryAttack(-20, isPlayer))
            {
                return false;
            }
            damage = 20;
            energy = -30;
        }

        if (move == 3)
        {   
            if (!gameHandler.TryAttack(-70, isPlayer))
            {   
                return false;
            }
            damage = 70;
            energy = -70;
        }
        return true;

    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        
    }
}
