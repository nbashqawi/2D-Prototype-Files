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
			string name = coll.gameObject.GetComponent<ZombieController> ().DropOnDie()[0];
			string power = coll.gameObject.GetComponent<ZombieController> ().DropOnDie()[1];
			Destroy (coll.gameObject);
			GameController.GetController ().EnemyKilled (name, power);
		} 
		Destroy (gameObject);
	}
}
