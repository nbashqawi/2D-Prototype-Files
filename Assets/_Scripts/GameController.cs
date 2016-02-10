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

	// Make sure there is only one instance of this class
	void Awake () {
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;
		}
		else if (controller != this) {
			Destroy (gameObject);
		}
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


