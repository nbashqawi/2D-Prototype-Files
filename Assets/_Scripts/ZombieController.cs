using UnityEngine;
using System.Collections;

// Controls zombie motions, basic AI, damage, health, etc. and plays correct animations --- some code may branch into another class

public class ZombieController : MonoBehaviour {

	// Important zombie business
	Rigidbody2D zRigBod;
	public float sentryRadius; // How far zombie will march about sentry point on x axis
	public float speed;        // Zombie speed
	public float chaseSpeed;
	private float sentryPos;   // Point about which zombie marches
	private float dir;         // Variable for direction of zombie (1 is right, -1 is left)
	public float attackStr = 10.0f; // Attack strength for dealing damage to player --- may make a range
	public Transform vision;

	private RaycastHit2D visionHit; //RaycastHit2D of obstacle and zombie detection RayCast2D commented below
	private bool isChasing = false; // Are we chasing the player?
	private Transform player; // Transform of player for chase
	private float dirToPlayer; // Sign of the direction to the player
	private bool atEdge = false; // Are we at an edge?

	Animator anim; // Animator for this zombie
	AudioClip clip; // Sound clip for source to play
	AudioSource source; // Holds this objects audio source

	public string name;
	public enum Drop
	{
		None, Health, Invulnerable
	};
	public Drop dropDown;

	// Zombie audio clip loop variables
	float timeToPlay;
	float timeSinceLastPlay;

	// Get zombie ready for Monday morning work
	void Awake () {
		source = GetComponent<AudioSource> ();
		clip = GetComponent<AudioSource> ().clip;
		anim = GetComponent<Animator> ();
		zRigBod = GetComponent<Rigidbody2D> ();
		sentryPos = transform.position.x;
		dir = transform.localScale.x;
		timeSinceLastPlay = 0f;
		timeToPlay = 3.0f;
	}
	
	// TODO: set up move, sentry, follow, etc. efficiently; add all animations
	void FixedUpdate () {

		// Loop the zombie sound every timeToPlay seconds
		timeSinceLastPlay += Time.deltaTime;
		if (timeSinceLastPlay >= timeToPlay) {
			timeSinceLastPlay = 0.0f;
			source.Play ();
		}


		/*
		// Raycast that will be used for detecting other zombies and obstacles
		//visionHit = Physics2D.Raycast (new Vector2(vision.position.x, vision.position.y), dir*Vector2.right, 1f, 10);
		visionHit = Physics2D.Raycast (new Vector2(vision.position.x+1.0f, vision.position.y), transform.forward, 1f, 10);
		//Debug.DrawRay (new Vector3(vision.position.x, vision.position.y, vision.position.z), dir*Vector3.right, Color.red);
		if (visionHit.collider != null) {
			if (visionHit.collider.CompareTag ("Enemy") && !isChasing) {
				Debug.Log ("Zombie: Obstacle Hit");
				Flip ();
			}
		}*/
			
		// If not chasing the player, just perform sentry behavior
		if (!isChasing) {
			Sentry ();
		}
		// Otherwise, chase the player
		else {
			dirToPlayer = Mathf.Sign(player.position.x - transform.position.x); // get player direction
			if (dir != dirToPlayer) {
				Flip (); // Flip direction if not facing the player
			}

			// If zombie is not at an edge, move toward player. Otherwise, stop --- still working on this bit as well
			if (!atEdge) {
				zRigBod.velocity = new Vector2 (chaseSpeed * dir, 0f); 
			}
			else {
				zRigBod.velocity = new Vector2 (0f, 0f);
			}
		}
	}

	// If at the edge of a platform, stop and set atEdge to true --- Ive got work to do with this system too
	void OnCollisionEnter2D (Collision2D col) {
		if (col.collider.gameObject.CompareTag("Edge") ) {
			atEdge = true;
			zRigBod.velocity = new Vector2 (0f, 0f);
		}

		if ((col.collider.gameObject.CompareTag("Obstacle") || col.collider.gameObject.CompareTag("Enemy")) && !isChasing) {
			Flip ();
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		

	}

	public string[] DropOnDie() {
		string power = "none";
		switch (dropDown) {
		case Drop.None:
			power = "none";
			break;
		case Drop.Health:
			power = "health";
			break;
		case Drop.Invulnerable:
			power = "inv";
			break;
		}
		return new string[] {name, power};
	}

	// Set the player to chase to p and set isChasing to true
	public void Chase (Transform p) {
		//source.clip = clip; <- will be playing a chase noise clip here
		player = p;
		isChasing = true;
	}

	// Still working on this one, I have to change it up a bit
	public IEnumerator Search (Transform player) {
		isChasing = false;
		sentryPos = transform.position.x;
		Debug.Log ("Start search");
		yield return new WaitForSeconds (3.0f);
		Debug.Log ("End search");
	}

	// Sentry behaviour code --- move back and forth about a iven x position
	void Sentry () {
		zRigBod.velocity = new Vector2 (speed*dir, 0f);

		// Make zombie face correct direction
		if (dir == 1 && Mathf.Abs (transform.position.x - sentryPos) >= sentryRadius)
			Flip ();
		if (dir == -1 && transform.position.x - sentryPos <= -sentryRadius)
			Flip ();
		if (atEdge) {
			atEdge = false;
			Flip ();
		}

		// Trigger walk animation if moving
		anim.SetFloat ("Speed", Mathf.Abs(dir));
	}

	// Flip zombie x scale if facing left to change sprite direction and vice versa
	void Flip () {
		dir *= -1;

		Vector3 lScale = transform.localScale;
		lScale.x = -lScale.x;
		transform.localScale = lScale;
		transform.position = new Vector3 (transform.position.x + dir*0.2f, transform.position.y, transform.position.z);
	}
		
}
