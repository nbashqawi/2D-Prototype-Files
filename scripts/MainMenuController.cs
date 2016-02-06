using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	private static MainMenuController menuController;

	// Use this for initialization
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

	public void NewGame () {
		SceneManager.LoadSceneAsync ("scene02");
	}

	public void Load () {

	}

	public void Options () {

	}

	public void Quit () {
		GameController.GetController ().Quit ();
	}
}
