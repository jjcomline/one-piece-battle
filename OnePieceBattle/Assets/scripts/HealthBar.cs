using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;
    public int health = 100;
    public int maxHealth = 100;

    public Animator animator;


    void SetSize(float sizeNormalized)
    {
        animator.SetInteger("Danger_1", (int)(sizeNormalized*100));
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
    public void resetHealth(int maxHealth) => health = this.maxHealth = maxHealth;
    
    public bool setHealth(int damage)
    {
        int newHealth = health - damage;
        if (newHealth > maxHealth)
            health = maxHealth;
        else 
            if(newHealth < 0) 
                health = 0;
        else 
            health = newHealth;
        SetSize((float)health / maxHealth);
        return (health > 0);
    }
}