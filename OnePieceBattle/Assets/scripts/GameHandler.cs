using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHandler : MonoBehaviour {
    
    public GameObject BarPlayer; 
    public GameObject BarEnemy; 
    [SerializeField] private HealthBar HealthBarPlayer; 
    [SerializeField] private HealthBar HealthBarEnemy; 
    [SerializeField] private PowerBar PowerBarPlayer; 
    [SerializeField] private PowerBar PowerBarEnemy; 
    public Image Luffy_Image, Magellan_Image, Zoro_Image;
    public GameObject LuffyPrefab, MagellanPrefab, ZoroPrefab, PlayerPrefab, EnemyPrefab;
    public RectTransform mPanelGameOver;
    public RectTransform PanelStartMenu;
    public Text mTxtGameOver;
    public Text timer;
    public float time = 100;
    public bool gameOver;
    int character;
    bool startMenu;

    void Start() {
        character = 0;
        startMenu = true;
        PanelStartMenu.gameObject.SetActive(true);
        BarPlayer.SetActive(false);
        BarEnemy.SetActive(false);
        timer.gameObject.SetActive(false);
        mPanelGameOver.gameObject.SetActive(false);
        mTxtGameOver.gameObject.SetActive(false);
        setOpacity();
    }

    void Update()
    {
        if (startMenu){
            if(Input.GetButtonDown("Horizontal")){
                character = character + (int)Input.GetAxisRaw("Horizontal");
                if (character == 3)
                    character = 0;
                if (character == -1)
                    character = 2;
                setOpacity();
            }
            if(Input.GetButtonDown("Jump")){
                setPlayer();
            }
        }


    }
    void setOpacity(){
        Color opaque = new Color(1f,1f,1f,.5f);
        Color full = new Color(1f,1f,1f,1f);
        if(character == 1){
            Luffy_Image.color = full;
            Magellan_Image.color = opaque;
            Zoro_Image.color = opaque;
        }
        if(character == 0){
            Magellan_Image.color = full;
            Luffy_Image.color = opaque;
            Zoro_Image.color = opaque;
        }
        if(character == 2){
            Magellan_Image.color = opaque;
            Luffy_Image.color = opaque;
            Zoro_Image.color = full;
        }
    }
    void setPlayer(){
        if (character == 0)
            PlayerPrefab = MagellanPrefab;
        if (character == 1)
            PlayerPrefab = LuffyPrefab;
        if (character == 2)
            PlayerPrefab = ZoroPrefab;
        float rand = UnityEngine.Random.Range(0f, 1f);

        if (rand < 0.33f)
            EnemyPrefab = MagellanPrefab;
        if (rand < 0.66f && rand >= 0.33f)
            EnemyPrefab = LuffyPrefab;
        if  (rand >= 0.66f)
            EnemyPrefab = ZoroPrefab;
        startMenu = false;
        setGame();


    }
    private void setGame() {
        PanelStartMenu.gameObject.SetActive(false);
        BarPlayer.SetActive(true);
        BarEnemy.SetActive(true);
        timer.gameObject.SetActive(true);
        time = 100;
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
        EnemyMovement mover_enemy = enemy.AddComponent(typeof(EnemyMovement)) as EnemyMovement;
        mover_enemy.character = opponent;
        mover_enemy.player = player;
        HealthBarPlayer.resetHealth(character.Health);
        HealthBarEnemy.resetHealth(opponent.Health);
        PowerBarPlayer.resetPower(character.Power);
        PowerBarEnemy.resetPower(opponent.Power);
        character.IsPlayer = true;
        opponent.IsPlayer = false;
        player.SetActive(true);
        enemy.SetActive(true);

        InvokeRepeating("Count", 0.0f, 1.0f);
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

        if(!SetHealthBar(damage, isPlayer)){
            gameOver = !isPlayer;
            showWinOrLoose(gameOver);
        }    

    }

    private bool SetHealthBar (int damage, bool isPlayer) {
        if (isPlayer)
            return HealthBarEnemy.setHealth(damage); 
        else
            return HealthBarPlayer.setHealth(damage);
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

