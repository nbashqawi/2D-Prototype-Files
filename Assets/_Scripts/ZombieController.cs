using UnityEngine;
using System.Collections;

// Controls zombie motions, basic AI, damage, health, etc. and plays correct animations --- some code may branch into another class

public class ZombieController : MonoBehaviour {

	// Important zombie business
	Rigidbody2D zRigBod;
	public float sentryRadius; // How far zombie will march about sentry point on x axis
	public float speed;        // Zombie speed
	private float sentryPos;   // Point about which zombie marches
	private float dir;         // Variable for direction of zombie (1 is right, -1 is left)

	Animator anim; // Animator for this zombie

	// Get zombie ready for Monday morning work
	void Awake () {
		anim = GetComponent<Animator> ();
		zRigBod = GetComponent<Rigidbody2D> ();
		sentryPos = transform.position.x;
		dir = 1;
	}
	
	// TODO: set up move, sentry, follow, etc. efficiently; add all animations
	void FixedUpdate () {
		zRigBod.velocity = new Vector2 (speed*dir, 0f);

		anim.SetFloat ("Speed", Mathf.Abs(dir)); // Trigger walk animation if moving

		// Make zombie face correct direction
		if (dir == 1 && Mathf.Abs (transform.position.x - sentryPos) >= sentryRadius)
			Flip ();
		if (dir == -1 && transform.position.x - sentryPos <= -sentryRadius)
			Flip ();
	}

	// Flip zombie x scale if facing left to change sprite direction and vice versa
	void Flip () {
		dir *= -1;

		Vector3 lScale = transform.localScale;
		lScale.x = -lScale.x;
		transform.localScale = lScale;
	}
}
