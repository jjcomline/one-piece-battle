using UnityEngine;
using System.Collections;

public class CharControl : MonoBehaviour {
	public float playerSpeed = 10f;
	//public GameControl gameController;
	//public GameObject bulletPrefab;
	//public float reloadTime = 0.5f; 	
	private float elapsedTime = 0;
	
	void Update () {
		// Keeping track of time for bullet firing
		elapsedTime += Time.deltaTime; 

		// Move the player left and right (keeping it on screen)
		float xMovement = Input.GetAxis ("Horizontal") * playerSpeed * Time.deltaTime;
		if (transform.position.x + xMovement > 7f) xMovement = 7f - transform.position.x;
		if (transform.position.x + xMovement < -7f) xMovement = -7f - transform.position.x; 
		transform.Translate (xMovement, 0f, 0f);
		
		// Spacebar fires. Only happens if enough time has elapsed 
		// since last firing
		if (Input.GetButtonDown("Jump") && transform.position.y < -5f  ) {
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,7), ForceMode2D.Impulse);
			// Instantiate the bullet in front of the player
			//Vector3 spawnPos = transform.position;
			//spawnPos += new Vector3(0, 1.2f, 0);
			//Instantiate(bulletPrefab, spawnPos , Quaternion.identity);
			//elapsedTime = 0f;
		}
	}
	
	// If a meteor hits the player
	/* void OnTriggerEnter2D (Collider2D other) {
		gameController.PlayerDied();
	}*/
} 
