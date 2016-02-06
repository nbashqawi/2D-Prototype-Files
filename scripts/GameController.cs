using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameController : MonoBehaviour {

	private static GameController controller;

	// Use this for initialization
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

	public static GameController GetController() {
		return controller;
	}

	public void Save () {

	}

	public void Load () {

	}

	public void Quit () {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}

class PlayerData {
	float health, experience;
	Vector3 position;
}

class WorldData {

}

class NPCData {

}


