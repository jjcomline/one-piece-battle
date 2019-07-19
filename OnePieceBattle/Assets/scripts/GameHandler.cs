using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
    
    [SerializeField] private HealthBar HealthBarPlayer; 
    [SerializeField] private HealthBar HealthBarEnemy; 
    [SerializeField] private PowerBar PowerBarPlayer; 
    [SerializeField] private PowerBar PowerBarEnemy; 

    public Text timer;
    private float startTime;

    private int i = 100; 

    private void Start() {
        startTime = Time.time;
    }

    private void Update(){
        Damage(1, true);
        Damage(1, false);
        Power(1, true);
        Power(1, false);

        float t = Time.time - startTime;

        string minutes = ((int)t/60).ToString();
        string seconds = (t % 60).ToString("f2");

        timer.text = minutes + ":" + seconds;
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
		return true;

    }

    private bool isFinalMove (){
		return true;
    }


}