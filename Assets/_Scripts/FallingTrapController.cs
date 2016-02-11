using UnityEngine;
using System.Collections;

public class FallingTrapController : MonoBehaviour {

	public float height;
	private BoxCollider2D trapCollider;
	public Transform spawner;
	public GameObject faller;
	private bool triggered = false;

	// Use this for initialization
	void Awake () {
		trapCollider = GetComponent<BoxCollider2D> ();
		trapCollider.size = new Vector2 (trapCollider.size.x, height);
		spawner.localPosition = new Vector3 (0f, height, 0f);
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player") && triggered == false) {
			faller = (GameObject) Instantiate (faller, spawner.position, Quaternion.identity);
			triggered = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Faller")) {
			Destroy (other.gameObject);
		}
	}
}
