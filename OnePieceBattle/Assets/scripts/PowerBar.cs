using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    public Transform bar;
    private int power = 0;
    private int maxPower = 100;

    public Animator animator;
    
    private void Start() {
    }

    private void SetSize(float sizeNormalized) {
        if (power == 1)
            animator.SetBool("Full", true);
        else animator.SetBool("Full", false);
        bar.localScale = new Vector3 (sizeNormalized, 1f);
    }

    public bool hasEnoughPower(int energy) => power + energy >= 0;

    public void resetPower(int maxPower){
        power = 0;
        this.maxPower = maxPower;
    }

    public void setPower(int energy) {
        if(!hasEnoughPower(energy))
            return;
        int newPower = power + energy;
        if(newPower > maxPower)
            power = maxPower;
        else power = newPower;
        SetSize((float)power/maxPower);
    }
}