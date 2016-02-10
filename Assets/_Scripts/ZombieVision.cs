using UnityEngine;
using System.Collections;

// Controls vision for zombies, and is used to detect player, obstacles, etc.

public class ZombieVision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Check if player can be seen
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
		}
			//Debug.Log ("Hooray!");
	}
}
