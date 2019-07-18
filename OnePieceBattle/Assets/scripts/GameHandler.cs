using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
    
    [SerializeField] private HealthBar HealthBarPlayer; 
    [SerializeField] private HealthBar HealthBarEnemy; 
    [SerializeField] private PowerBar PowerBarPlayer; 
    [SerializeField] private PowerBar PowerBarEnemy; 

    int i = 100; 

    private void Update(){
        Damage(1, true);
        Damage(1, false);
        Power(1, true);
        Power(1, false);
    }

    private void Damage (int hit, bool isEnemy) {
        if (HealthBarPlayer.getHealth() > 0 && !isEnemy) {
            HealthBarPlayer.SetSize(hit); 
        }
        if (HealthBarEnemy.getHealth() > 0 && isEnemy) {
            HealthBarEnemy.SetSize(hit);
        }
    }

    private void Power (int energy, bool isEnemy) {
        if (PowerBarPlayer.getPower() < 100 && !isEnemy) {
            PowerBarPlayer.SetSize(energy); 
        }
        if (PowerBarEnemy.getPower() < 100 && isEnemy) {
            PowerBarEnemy.SetSize(energy);
        }
    }

    private bool isSpecialMove (int energy) {

    }

    private bool isFinalMove (){

    }


}