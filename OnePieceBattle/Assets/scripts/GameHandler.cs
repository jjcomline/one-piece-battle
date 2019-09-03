using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class GameHandler : MonoBehaviour {
    
    public GameObject BarPlayer; 
    public GameObject BarEnemy; 
    [SerializeField] private HealthBar HealthBarPlayer; 
    [SerializeField] private HealthBar HealthBarEnemy; 
    [SerializeField] private PowerBar PowerBarPlayer; 
    [SerializeField] private PowerBar PowerBarEnemy; 
    public Image Luffy_Image, Magellan_Image, Zoro_Image;
    public GameObject LuffyPrefab, MagellanPrefab, ZoroPrefab, PlayerPrefab, EnemyPrefab;
    public RectTransform PanelGameOver;
    public RectTransform PanelStartMenu;
    GameObject player;
    GameObject enemy;
    public Button Retry;
    public Text TxtGameOver;
    public Text timer;
    public float time;
    public bool gameOver;
    int character_number = -1;
    bool startMenu;
    bool theEnd;
    int level = 0; 

    void Start() {

        Time.timeScale = 1;
        setEnemy();
        theEnd = false;
        BarPlayer.SetActive(false);
        BarEnemy.SetActive(false);
        timer.gameObject.SetActive(false);
        PanelGameOver.gameObject.SetActive(false);

        if (level == 0){
            startMenu = true;
            PanelStartMenu.gameObject.SetActive(true);
            //setOpacity();
        }
        if (level == 1 || level == 2)
            setGame();
        if (level == 3) {
            theEnd = true;
        }
        level ++;
    }

    void Update()
    {
        if (startMenu){
            character_number = -1;
            if(Magellan_Image.GetComponent<Joybutton>().Pressed) {  //Input.GetButtonDown("Horizontal")
                Magellan_Image.GetComponent<Joybutton>().Pressed=false;
                character_number = 0;
            }
            if(Luffy_Image.GetComponent<Joybutton>().Pressed) {  //Input.GetButtonDown("Horizontal")
                Luffy_Image.GetComponent<Joybutton>().Pressed=false;
                character_number = 1;
            }
            if(Zoro_Image.GetComponent<Joybutton>().Pressed){   //Input.GetButtonDown("Horizontal")
                Zoro_Image.GetComponent<Joybutton>().Pressed=false;
                character_number = 2;
            }
                //setOpacity();
            if (character_number >  -1){
                Debug.Log(character_number);
                setPlayer();
                startMenu = false;
                setGame();
            }
        }


    }
    void setOpacity(){
        Color opaque = new Color(1f,1f,1f,.5f);
        Color full = new Color(1f,1f,1f,1f);
        if(character_number == 1){
            Luffy_Image.color = full;
            Magellan_Image.color = opaque;
            Zoro_Image.color = opaque;
        }
        if(character_number == 0){
            Magellan_Image.color = full;
            Luffy_Image.color = opaque;
            Zoro_Image.color = opaque;
        }
        if(character_number == 2){
            Magellan_Image.color = opaque;
            Luffy_Image.color = opaque;
            Zoro_Image.color = full;
        }
    }
    void setPlayer(){
        if (character_number == 0)
            PlayerPrefab = MagellanPrefab;
        if (character_number == 1)
            PlayerPrefab = LuffyPrefab;
        if (character_number == 2)
            PlayerPrefab = ZoroPrefab;
    }

    void setEnemy() {
         //float rand = UnityEngine.Random.Range(0f, 1f);
        if (level == 0)
            EnemyPrefab = MagellanPrefab;
        if (level == 1)
            EnemyPrefab = LuffyPrefab;
        if (level == 2)
            EnemyPrefab = ZoroPrefab;
    }

    public void setGame() {
        PanelStartMenu.gameObject.SetActive(false);
        time = 100;
        timer.gameObject.SetActive(true);
        gameOver = false;
        player = Instantiate(PlayerPrefab, new Vector2(-7.5f, 0f), Quaternion.identity);
        player.tag = "player";
        enemy = Instantiate(EnemyPrefab, new Vector2(7.5f, 0f), Quaternion.identity);
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
        BarPlayer.SetActive(true);
        BarEnemy.SetActive(true);
        character.IsPlayer = true;
        opponent.IsPlayer = false;
        player.SetActive(true);
        enemy.SetActive(true);

        InvokeRepeating("Count", 0.0f, 1.0f);
    }

    private void Count() {

        if(time  > 97){
             setColorTimer("black");
        }
        if (time == 50) {
            setColorTimer("yellow");
        }

        if (time == 20) {
            setColorTimer("red");
        }

        if (time == 0) {
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
        if (color == "black")
            timer.color = Color.black;
    }

    private void showWinOrLoose(bool GameOver) {
        if (GameOver || (!GameOver && level == 3)) {
            PanelGameOver.gameObject.SetActive(true);
            TxtGameOver.text = GameOver ? "You Loose" : "You Win";
            theEnd = true;
            CancelInvoke("Count");
            Time.timeScale = 0;
        }
        else {
            Destroy(player);
            Destroy(enemy);
            theEnd = false;
            Start();
            
        }

    }

    void OnGUI() {	
        if (theEnd){
            Rect startButton = new Rect(Screen.width/2 - 220, Screen.height/2 + 70, 440, 140);
            if(GUI.Button(startButton, "<size=40>Click to Play Again</size>")) {
                Destroy(player);
                Destroy(enemy);
                level = 0;
                theEnd = false;
                Start();
            }
        }
	}


}

