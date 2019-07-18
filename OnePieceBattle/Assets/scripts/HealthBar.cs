using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;
    private int health = 100;

    public Animator animator;
    
    private void Start() {
        //bar_color = GetComponent<Renderer>();
    }

    public void SetSize(int sizeNormalized) {
        health -= sizeNormalized;            
        if (health < 71 && health > 40) {
            animator.SetInteger("Danger_1", health);
        }
        if (health <= 40 && health > 25 ){
            animator.SetInteger("Danger_1", health);
        }
        if (health <= 25){
            Debug.Log("hhh");
            animator.SetInteger("Danger_1", health);
        }
        bar.localScale = new Vector3 ((float)health/100, 1f);
    }

    public int getHealth() {
        return health;
    } 
}