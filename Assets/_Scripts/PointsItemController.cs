using UnityEngine;
using System.Collections;

// Conrtoller for collectible items that give points

public class PointsItemController : MonoBehaviour {
	public int pointsAward = 10; // number of points to award

	// Give player points and pass true to AddScore() to play score animation above player, then destroy the gameObject
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			GameController.GetController().AddScore(pointsAward, true);
			GameObject.Destroy (gameObject);
		}
	}
}
