using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luffy_Moves : MonoBehaviour,IChar
{
    int damage = 5;
    int energy = 5;
    int move = 1;
    GameHandler gameHandler;
    GameObject bullet;
    Collider2D coll;
    const float runSpeed = 30f;
    const float jumpForce = 350f;
    const int maxHealth = 150;
    const int maxPower = 200;
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
        gameHandler.Attack(damage,energy, isPlayer);
    }
    public void Move3(GameObject gmOb)
    {   
        float facingRight;
        if (transform.localScale.x > 0)
            facingRight = 1f;
        else
            facingRight = -1f;
        Vector2 position = gmOb.transform.position;
        this.transform.position = position - new Vector2(facingRight, 0f);
        StartCoroutine(Delay());
    }
    public bool SetAttack(int a)
    {
        move = a;
        if (coll != null)
            Destroy(coll);
        BoxCollider2D box;
        if (move == 1)
        {
            damage = 3;
            energy = 5;
            box = Bullet.AddComponent<BoxCollider2D>();
            box.size = new Vector2(.028f, .01f);
            box.offset = new Vector2(.021f, .032f);
            coll = box;
        }
        else if (move == 2)
        {
            damage = 15;
            energy = -20;
            if (gameHandler.TryAttack(energy, isPlayer))
            {
                box = Bullet.AddComponent<BoxCollider2D>();
                box.size = new Vector2(.03f, .02f);
                box.offset = new Vector2(.0014f, .003f);
                coll = box;
            }
            else return false;
        }
        else if (move == 3)
        {   
            string who;
            damage = 100;
            energy = -70;

            who = isPlayer ? "enemy" : "player"; 

            if (gameHandler.TryAttack(energy, isPlayer))
            {   
                Move3(GameObject.FindWithTag(who));
            }
            else return false;

        }
        coll.isTrigger = true;
        return true;

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(7);
        gameHandler.Attack(damage, energy, isPlayer);
    }
}
