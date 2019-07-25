using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IChar
{
    void Move1(GameObject gmOb);
    void Move2(GameObject gmOb);
    void Move3(GameObject gmOb);
    bool SetAttack(int a);
    GameHandler GameHandler {set; }
    GameObject Bullet {set; get; }
    float RunSpeed { get;}
    int Health { get;}
    int Power { get;}
    float JumpForce{get;}
}