using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    public Transform bar;
    private int power = 0;

    public Animator animator;
    
    private void Start() {
    }

    public void SetSize(int sizeNormalized) {
        power += sizeNormalized;
        if (power == 100)
            animator.SetBool("Full", true);
        bar.localScale = new Vector3 ((float)power/100, 1f);
    }

    public int getPower() {
        return power;
    } 

    private void resetPower() {
        power = 0;
    }

    private void minusPower(int energy) {
        power -= energy;
    }
}