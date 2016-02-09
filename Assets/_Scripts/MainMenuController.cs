using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Controller for the main menu
// Only needs to be in main menu scene

public class MainMenuController : MonoBehaviour {

	private static MainMenuController menuController;

	// Make sure there is only one instance of this class
	void Awake () {
		if (menuController == null) {
			DontDestroyOnLoad (gameObject);
			menuController = this;
		}
		else if (menuController != this) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static MainMenuController GetMenuController () {
		return menuController;
	}

	// Starts new game - currently just loads scene 1
	public void NewGame () {
		SceneManager.LoadSceneAsync ("scene01");
	}

	// Load game code here
	public void Load () {

	}

	// Options menu code here
	public void Options () {

	}

	// Calls GameController.Quit() to quit game ---- later will ask if sure you want to quit
	public void Quit () {
		GameController.GetController ().Quit ();
	}
}
