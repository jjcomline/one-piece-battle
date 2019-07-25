using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHandler : MonoBehaviour {
    
    [SerializeField] private HealthBar HealthBarPlayer; 
    [SerializeField] private HealthBar HealthBarEnemy; 
    [SerializeField] private PowerBar PowerBarPlayer; 
    [SerializeField] private PowerBar PowerBarEnemy; 
    public GameObject PlayerPrefab, EnemyPrefab;
    public Text timer;
    public float time = 100;
    public bool gameOver;

    void Start() {
        gameOver = false;
        GameObject player = Instantiate(PlayerPrefab, new Vector2(-1f, 0f), Quaternion.identity);
        player.tag = "player";
        GameObject enemy = Instantiate(EnemyPrefab, new Vector2(1f, 0f), Quaternion.identity);
        enemy.tag = "enemy";
        player.SetActive(false);
        enemy.SetActive(false);
        IChar character = player.GetComponent<IChar>();
        PlayerMovement mover = player.AddComponent(typeof(PlayerMovement)) as PlayerMovement;
        mover.character = character;
        HealthBarPlayer.resetHealth(character.Health);
        HealthBarEnemy.resetHealth(100);
        PowerBarPlayer.resetPower(character.Power);
        PowerBarEnemy.resetPower(100);
        player.SetActive(true);
        enemy.SetActive(true);
    }

    void Update(){
        time -= Time.deltaTime;
        timer.text = ((int)time).ToString();
    }
    public bool TryAttack(int energy, bool isPlayer)
    {
        if (isPlayer)
        {
            return PowerBarPlayer.setPower(energy);
        }
        else
        {
            return PowerBarEnemy.setPower(energy);
        }
    }
    public void Attack(int damage, int energy, bool isPlayer){
        if(energy > 0)
            TryAttack(energy, isPlayer);
        if(!SetHealthBar(damage, !isPlayer)){
            // Time.timeScale = 0;
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
}