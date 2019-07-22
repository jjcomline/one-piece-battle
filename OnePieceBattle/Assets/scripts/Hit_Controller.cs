using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hit_Controller : MonoBehaviour
{
    private int move = 1;
    private List<int> hit_objects;

    [Header("Events")]
    [Space]
    public UnityEvent OnMoveFinished;
    [System.Serializable]
    public class ObjectEvent : UnityEvent<GameObject> { }
    public ObjectEvent Move1;
    public ObjectEvent Move2;

    void Start(){
        if (OnMoveFinished == null)
            OnMoveFinished = new UnityEvent();
        if (Move1 == null)
            Move1 = new ObjectEvent();
        if (Move2 == null)
            Move2 = new ObjectEvent();

    }
    void OnEnable()
    {
        hit_objects = new List<int>();
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
        }
    }

}