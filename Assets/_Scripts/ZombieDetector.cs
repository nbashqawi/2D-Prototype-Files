using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ZombieController))]
public class ZombieDetector : MonoBehaviour {

	ZombieController zombie; // ZombieController of this zombie

	void Awake () {
		zombie = GetComponent<ZombieController> ();
	}

	// When detecting player, stop search coroutine and begin chase
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			Debug.Log ("Enter");
			StopCoroutine (zombie.Search (other.gameObject.transform));
			zombie.Chase (other.gameObject.transform);
		}
	}

	// When player exits detection, start search coroutine in zombie
	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			Debug.Log ("Exit");
			StartCoroutine (zombie.Search (other.gameObject.transform));
		}
	}

}
