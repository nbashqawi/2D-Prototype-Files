﻿using UnityEngine;
using System.Collections;

// Much of this code was based on Unity Live tutorial, which can also be found in PlatformerCharacter2D.cs
// Controls motions of player, such as running, jumping, etc. and plays correct animations
// Also, holds code for health, score, experience, etc. - though this may branch into a separate class

public class PlayerController : MonoBehaviour {

	// Motion related variables
	public float max_Speed = 10f;    // Max player velocity
	public float jump_Force = 400f;  // Force for player jumping
	public LayerMask groundLayer;    // Mask describing what layers to check for as ground
	private bool onGround;           // Is the player on the ground?
	private bool facingRight;        // Is the player facing right?

	private float h;
	private bool jump;

	// Physics related variables, such as ground and ceiling checks
	private Rigidbody2D player_RB;
	private Transform groundCheck;
	private float groundRadius = 0.2f;
	private Transform ceilingCheck;
	private float ceilingRadius = 0.01f;

	// Initialize important variables such as rigidBody and ground and ceiling checks
	void Awake () {
		facingRight = true;
		player_RB = GetComponent<Rigidbody2D> ();
		groundCheck = transform.Find ("GroundCheck");
		ceilingCheck = transform.Find ("CeilingCheck");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		onGround = false;

		h = Input.GetAxis ("Horizontal");
		jump = Input.GetKey (KeyCode.Space);

		// Check to see if player is on the ground
		Collider2D[] colliders = Physics2D.OverlapCircleAll (groundCheck.position, groundRadius);
		foreach (Collider2D col in colliders) {
			if (col.gameObject != gameObject)
				onGround = true;
		}

		Move (h, jump);
		jump = false;
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

		Vector3 lScale = transform.localScale;
		lScale.x = -lScale.x;
		transform.localScale = lScale;
	}

	// Get the Vector2 describing the player's velocity --- used in CameraControl.cs
	public Vector2 getSpeed () {
		return player_RB.velocity;
	}
}