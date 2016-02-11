using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.CompareTag ("Enemy")) {
			Destroy (coll.gameObject);
			GameController.GetController ().EnemyKilled ();
		} 
		Destroy (gameObject);
	}
}
