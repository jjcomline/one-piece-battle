using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luffy_Moves : PlayerMovement
{
    private int power = 5;
    private int energy = 5;
    Collider2D coll;
    private int move = 1;
    
    public void Move1(GameObject gmOb)
    {
        int facingRight;
        if (transform.localScale.x > 0)
            facingRight = 1;
        else
            facingRight = -1;
        Rigidbody2D rb2d = gmOb.GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(facingRight * 7.5f, 7.5f / 2), ForceMode2D.Impulse);
        gameHandler.Attack(power, energy, true);
    }
    public void Move2(GameObject gmOb)
    {
        int facingRight;
        if (transform.localScale.x > 0)
            facingRight = 1;
        else
            facingRight = -1;
        Rigidbody2D rb2d = gmOb.GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(facingRight * 20f, 20f / 2), ForceMode2D.Impulse);
        gameHandler.Attack(power,energy, true);
    }
    public override bool SetAttack(int a)
    {
        move = a;
        if (coll)
            Destroy(coll);
        BoxCollider2D box;
        if (move == 1)
        {
            power = 5;
            energy = 5;
            if(gameHandler.TryAttack(energy,true))
            {
                box = Bullet.AddComponent<BoxCollider2D>();
                box.size = new Vector2(.02f, .009f);
                box.offset = new Vector2(.006f, .001f);
                coll = box;
            }
            else return false;
        }
        else if (move == 2)
        {
            power = 30;
            energy = -20;
            if (gameHandler.TryAttack(energy, true))
            {
                box = Bullet.AddComponent<BoxCollider2D>();
                box.size = new Vector2(.03f, .02f);
                box.offset = new Vector2(.0014f, .003f);
                coll = box;
            }
            else return false;
        }
        coll.isTrigger = true;
        return true;

    }
}
