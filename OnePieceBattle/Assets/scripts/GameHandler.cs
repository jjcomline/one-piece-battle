using System;
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
    public RectTransform mPanelGameOver;
    public Text mTxtGameOver;
    public Text timer;
    public float time = 100;
    public bool gameOver;

    void Start() {
        gameOver = false;
        GameObject player = Instantiate(PlayerPrefab, new Vector2(-7.5f, 0f), Quaternion.identity);
        player.tag = "player";
        GameObject enemy = Instantiate(EnemyPrefab, new Vector2(7.5f, 0f), Quaternion.identity);
        enemy.tag = "enemy";
        player.SetActive(false);
        enemy.SetActive(false);
        IChar character = player.GetComponent<IChar>();
        IChar opponent = enemy.GetComponent<IChar>();
        PlayerMovement mover = player.AddComponent(typeof(PlayerMovement)) as PlayerMovement;
        mover.character = character;
        HealthBarPlayer.resetHealth(character.Health);
        HealthBarEnemy.resetHealth(opponent.Health);
        PowerBarPlayer.resetPower(character.Power);
        PowerBarEnemy.resetPower(opponent.Power);
        character.IsPlayer = true;
        opponent.IsPlayer = false;
        player.SetActive(true);
        enemy.SetActive(true);

        mPanelGameOver.gameObject.SetActive(false);
        mTxtGameOver.gameObject.SetActive(false);

        InvokeRepeating("Count", 0.0f, 1.0f);
       
    }

    void Update()
    {
       

    }

    private void Count() {
        if (timer.text == "50") {
            setColorTimer("yellow");
        }

        if (timer.text == "20") {
            setColorTimer("red");
        }

        if (timer.text == "0") {
            gameOver = true;
            showWinOrLoose(gameOver);
            return;

        }
        time --;
        timer.text = ((int)time).ToString();
    }

    public bool TryAttack(int energy, bool isPlayer)
    {
        if (isPlayer)
            return PowerBarPlayer.setPower(energy);
        else 
            return PowerBarEnemy.setPower(energy);
    }
    public void Attack(int damage, int energy, bool isPlayer){
        if(energy > 0)
            TryAttack(energy, isPlayer);

        if (isPlayer) {
            if(!SetHealthBar(damage, !isPlayer)){
                gameOver = false;
                showWinOrLoose(gameOver);
            }    
        }
        else {
            if(!SetHealthBar(damage, isPlayer)){
                gameOver = true;
                showWinOrLoose(gameOver);
            }    
        }     
    }

    private bool SetHealthBar (int damage, bool isPlayer) {
        if (isPlayer)
            return HealthBarPlayer.setHealth(damage); 
        else
            return HealthBarEnemy.setHealth(damage);
    }

    private void setColorTimer (string color) {
        if (color == "yellow")
            timer.color = Color.yellow;
        if (color == "red")
            timer.color = Color.red;
    }

    private void showWinOrLoose(bool GameOver) {
        mPanelGameOver.gameObject.SetActive(true);
        mTxtGameOver.gameObject.SetActive(true);
        mTxtGameOver.text = GameOver ? "You Loose" : "You Win";
        CancelInvoke("Count");
        Time.timeScale = 0;
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}

