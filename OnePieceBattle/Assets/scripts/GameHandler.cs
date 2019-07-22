using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHandler : MonoBehaviour {
    
    [SerializeField] private HealthBar HealthBarPlayer; 
    [SerializeField] private HealthBar HealthBarEnemy; 
    [SerializeField] private PowerBar PowerBarPlayer; 
    [SerializeField] private PowerBar PowerBarEnemy; 

    public Text timer;
    public float time = 100;
    public bool gameOver;

    void Start() {
        gameOver = false;
        HealthBarPlayer.resetHealth(100);
        HealthBarEnemy.resetHealth(100);
        PowerBarPlayer.resetPower(100);
        PowerBarEnemy.resetPower(100);
    }

    void Update(){
        time -= Time.deltaTime;
        timer.text = ((int)time).ToString();
    }
    public bool TryAttack(int energy, bool isPlayer)
    {
        if (isPlayer)
        {
            return PowerBarPlayer.hasEnoughPower(energy);
        }
        else
        {
            return PowerBarEnemy.hasEnoughPower(energy);
        }
    }
    public void Attack(int damage, int energy, bool isPlayer){
        SetPowerBar(energy, isPlayer);
        if(!SetHealthBar(damage, !isPlayer)){
            Time.timeScale = 0;
            gameOver = true;
        }            
    }

    private bool SetHealthBar (int damage, bool isPlayer) {
        if (isPlayer)
        {
            return HealthBarPlayer.setHealth(damage); 
        }
        else{
            return HealthBarEnemy.setHealth(damage);
        }
    }

    private void SetPowerBar (int energy, bool isPlayer) {
        if (isPlayer) {
            PowerBarPlayer.setPower(energy); 
        }
        else{
            PowerBarEnemy.setPower(energy);
        }
    }
}