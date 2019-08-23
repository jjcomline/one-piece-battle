using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hit_Controller : MonoBehaviour
{
    public int move = 1;
    private List<int> hit_objects;
    public UnityEvent OnMoveFinished;
    public ObjectEvent Move1;
    public ObjectEvent Move2;
    public ObjectEvent Move3;


    void Start(){
        if (OnMoveFinished == null)
            OnMoveFinished = new UnityEvent();
        if (Move1 == null)
            Move1 = new ObjectEvent();
        if (Move2 == null)
            Move2 = new ObjectEvent();
        if (Move3 == null)
            Move3 = new ObjectEvent();

    }
    void OnEnable()
    {
        hit_objects = new List<int>();
        if (move == 2)
            this.GetComponent<Animator>().SetBool("Move2", true);
        else
            this.GetComponent<Animator>().SetBool("Move2", false);
    }
    void OnDisable()
    {
        OnMoveFinished.Invoke();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject gmOb = other.gameObject;
        int id = gmOb.GetInstanceID();
        if (gmOb.layer == 8 && !hit_objects.Contains(id))
        {
            hit_objects.Add(id);
            if (move == 1)
                Move1.Invoke(gmOb);
            else if (move == 2) 
                Move2.Invoke(gmOb);
            else if(move == 3)
                Move3.Invoke(gmOb);
        }
    }

}