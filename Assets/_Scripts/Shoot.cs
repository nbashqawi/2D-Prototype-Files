using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public Transform shotSpawn;
	public GameObject projectile;
	public Vector2 shotDirection = new Vector2(1,0);
	public float shotPower = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			GameObject proj = (GameObject) Instantiate (projectile, shotSpawn.position, Quaternion.identity);
			proj.GetComponent<Rigidbody2D> ().velocity = shotDirection.normalized * shotPower;
		}
	}
}
