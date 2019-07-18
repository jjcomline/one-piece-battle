using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Controller : MonoBehaviour
{
    public float power = 5f;
    public Collider2D coll;
    private int move = 1;
    private int facingRight = 1;
    private List<int> hit_objects;
    private void move1(GameObject gmOb){
        power = 7.5f;
        Rigidbody2D rb2d = gmOb.GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(facingRight * power, power / 2), ForceMode2D.Impulse);
    }
    private void move2(GameObject gmOb)
    {
        power = 20f;
        Rigidbody2D rb2d = gmOb.GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(facingRight * power, power / 2), ForceMode2D.Impulse);
    }
    private void SetCollider()
    {
        if(coll)
            Destroy(coll);
        BoxCollider2D box;
        if (move == 1){
            box = gameObject.AddComponent<BoxCollider2D>();
            box.size = new Vector2(.02f, .009f);
            box.offset = new Vector2(.006f, .001f);
            coll = box;
        }
        else if (move == 2){
            box = gameObject.AddComponent<BoxCollider2D>();
            box.size = new Vector2(.03f, .02f);
            box.offset = new Vector2(.0014f, .003f);
            coll = box;
        }
        coll.isTrigger = true;
    }
    void OnEnable()
    {
        hit_objects = new List<int>();
    } 

    void OnTriggerEnter2D(Collider2D other)
    {   
        GameObject gmOb = other.gameObject;
        int id =gmOb.GetInstanceID();
        if(gmOb.layer == 8 && !hit_objects.Contains(id)){
            hit_objects.Add(id);
            if( move == 1)
                move1(gmOb);
            else if( move == 2)
                move2(gmOb);
        }
    }

    public void SetAttack(int a, bool f){
        if (f)
            facingRight = 1;
        else
            facingRight = -1;
        move = a;
        SetCollider();
    }
}
