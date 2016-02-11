using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Much of this code was based on Unity Live tutorial, which can also be found in PlatformerCharacter2D.cs
// Controls motions of player, such as running, jumping, etc. and plays correct animations
// Also, holds code for health, score, experience, etc. - though this may branch into a separate class

public class PlayerController : MonoBehaviour {

	// Motion related variables
	public float max_Speed = 10f;    // Max player velocity
	public float jump_Force = 500f;  // Force for player jumping
	public LayerMask groundLayer;    // Mask describing what layers to check for as ground
	public Transform groundCheck;
	private bool onGround;           // Is the player on the ground?
	private bool facingRight;        // Is the player facing right?

	private float h;
	private bool jump;

	// Physics related variables, such as ground and ceiling checks
	private Rigidbody2D player_RB;
	private float groundRadius = 0.2f;
	private Transform ceilingCheck;
	private float ceilingRadius = 0.01f;

	public float hitDelay = 0.8f; // Delay between two hits;
	public int hitPower = 10;
	private float hitTimer = 0f;
	public int maxHealth = 100;
	private int health;
	float noCanDieTime;
	// private int score; // Player's current score // If more than one life, this player will get destroyed, score needs to be in game controller
	public Text scoreText; // Link to HUD ScoreText UI element
	public Text animText; // Link to AnimationText UI element in canvas attached to player
	public Animator scoreAnim; // Score animator component of canvas attached to player

	private Animator anim;

	private Shoot shotScript;

	// Initialize important variables such as rigidBody and ground and ceiling checks
	void Awake () {
		// score = 0;
		anim = GetComponent<Animator>();
		shotScript = GetComponent<Shoot>();
		health = maxHealth;
		facingRight = true;
		player_RB = GetComponent<Rigidbody2D> ();
		//groundCheck = transform.Find ("GroundCheck");
		ceilingCheck = transform.Find ("CeilingCheck");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	/*	if (onGround == true)
			Debug.Log ("Ground");
		else
			Debug.Log ("hang time");
    */
		if (noCanDieTime > 0) {
			noCanDieTime -= Time.deltaTime;
		}

		onGround = false;

		h = Input.GetAxis ("Horizontal");
		jump = Input.GetKey (KeyCode.Space);

		// Check to see if player is on the ground
		Collider2D[] colliders = Physics2D.OverlapCircleAll (groundCheck.position, groundRadius, groundLayer);
		foreach (Collider2D col in colliders) {
			if (col.gameObject != gameObject)
				onGround = true;
		}
		anim.SetBool ("isWalking", h != 0);
		Move (h, jump);
		jump = false;

		// update the hit timer
		hitTimer += Time.deltaTime;
	}
		
	// Move the player in move direction and scale (if using controller) and a a bool if player is jumping or not
	// TODO: Add run and jump animations for player
	void Move (float move, bool jump) {

		// Set the player's velocity
		player_RB.velocity = new Vector2 (move*max_Speed, player_RB.velocity.y);

		// Get player to face correct direction
		if (move > 0 && !facingRight)
			Flip ();
		if (move < 0 && facingRight)
			Flip ();

		// If player is on the ground and jump was trigger, make player jump --- TODO: play jump animation once we have it
		if(onGround && jump) {
			onGround = false;
			player_RB.AddForce (new Vector2(0f, jump_Force));
		}
	}
		
	// Flip player x scale if facing left to change sprite direction and vice versa
	void Flip () {
		facingRight = !facingRight;
		shotScript.shotDirection.Scale (new Vector2 (-1, 0));
		Vector3 lScale = transform.localScale;
		lScale.x = -lScale.x;
		transform.localScale = lScale;
	}

	// Get the Vector2 describing the player's velocity --- used in CameraControl.cs
	public Vector2 getSpeed () {
		return player_RB.velocity;
	}



	public void ScoreAnimation(int points) {
		animText.text = points + "pts";
		scoreAnim.SetTrigger ("Play");
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.CompareTag ("Enemy")) {
			if (hitTimer > hitDelay && noCanDieTime <= 0) {
				TakeHit ();
			}
		}
		if (coll.gameObject.CompareTag ("Faller")) {
			if (noCanDieTime <= 0) {
				TakeHit ();
				Destroy (coll.gameObject);
			}
		}
	}
	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.CompareTag ("Enemy")) {
			if (hitTimer > hitDelay && noCanDieTime <= 0) {
				TakeHit ();
			}
		}
	}

	void TakeHit() {
		hitTimer = 0f;
		health -= hitPower;

		GameController gc = GameController.GetController ();


		gc.ShowHealth (health);

		if (health <= 0) {
			gc.GameOver ();
		}
	}
		
	public void GainHealth (int heal) {
		if( health + heal <= maxHealth ) {
			health += heal;
			GameController gc = GameController.GetController ();

			gc.ShowHealth (health);
		}
	}

	public void Invulnerability () {
		noCanDieTime = 5.0f;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;

		var pos = groundCheck.position;
		//pos.x += transform.position.x;
		//pos.y += transform.position.y;

		Gizmos.DrawWireSphere (pos, groundRadius);

	}
}
