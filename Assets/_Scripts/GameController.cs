using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Controls basic game functions and data
// Place in every scene
public class GameController : MonoBehaviour {

	private static GameController controller;
	public Slider healthSlider;
	public Animator HUDAnimator, IDAnimator;
	public Text scoreText, IDName, IDPower;
	public PlayerController player;
	public AudioSource ghostVoices;
	public Text lifeText;

	private int score;
	private int enemyKillBonus = -10;


	// Make sure there is only one instance of this class
	void Awake () {
		//if (controller == null) {
		//	DontDestroyOnLoad (gameObject);
			controller = this;
	//	}
	//	else if (controller != this) {
	//		Destroy (gameObject);
	//	}
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Returns the single, static GameController object
	public static GameController GetController() {
		return controller;
	}

	// Game save code will go here
	public void Save () {

	}

	// Load game code will go here
	public void Load () {

	}
		
	// If in editor, exit play mode, otherwise quit the application
	public void Quit () {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}

	// Show the given health in the health bar
	public void ShowHealth(int value) {
		healthSlider.value = value;
	}

	public void GameOver() {
		HUDAnimator.SetTrigger ("GameOver");
	}

	public void Win() {
		HUDAnimator.SetTrigger ("Win");
	}

	public void EnemyKilled(string name, string power) {
		score += enemyKillBonus;
		ghostVoices.volume += 0.1f;
		player.ScoreAnimation (enemyKillBonus);
		UpdateScore ();
		PlayID (name, power);
	}

	public void AddScore(int amount, bool anim) {
		score += amount;
		player.ScoreAnimation (enemyKillBonus);
		UpdateScore ();
	}

	void UpdateScore (){
		scoreText.text = "Score: " + score;
	}

	void PlayID (string name, string power) {
		IDName.text = "Employee Name: " + name;
		if (power == "health") {
			IDPower.text = "Harness their work energy! +5 health";
			player.GainHealth (5);
		} else if (power == "inv") {
			IDPower.text = "You feel a wave of power and satisfaction creep over you...";
			player.Invulnerability ();
		} else {
			IDPower.text = "Such a hard worker!";
		}
		IDAnimator.SetTrigger ("Play");
	}

	public void ShowLifes(int n) {
		lifeText.text = "Lives: " + n;
	}
}

// Class that is the container of serializable player data for save and load
[Serializable]
class PlayerData {
	float health, experience;
	Vector3 position;
}

// Class that is the container of serializable world data for save and load
[Serializable]
class WorldData {

}

// Class that is the container of serializable NPC data for save and load
[Serializable]
class NPCData {

}


