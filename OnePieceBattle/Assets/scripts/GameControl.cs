using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControl : MonoBehaviour {
	
	//public Text scoreText, gameOverText;
	public int playerScore = 0;
	public int life_char = 100;
	public int life_enemy = 100;
	float time = 100;
	
	public void Start () {
		life_char = 100;
		life_enemy = 100;
		time = 100;

		buildBar();
	}

	public void Update () {
		time -= Time.deltaTime;
	}

	public void Damage(int damage, bool isEnemy) {
		if (isEnemy)
			life_enemy -= damage;
		else 
			life_char -= damage;
	}

	private void buildBar() {
	
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,Color.white);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(new Rect(0,2, 10, 10), GUIContent.none);


	}
	
	
}
