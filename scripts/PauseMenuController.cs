using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuController : MonoBehaviour {

	private static PauseMenuController menuController;
	public Canvas canvas;

	// Use this for initialization
	void Awake () {
		if (menuController == null) {
			DontDestroyOnLoad (gameObject);
			canvas = Instantiate (canvas);
			DontDestroyOnLoad (canvas);
			canvas.enabled = false;
			menuController = this;
		}
		else if (menuController != this) {
			Destroy (gameObject);
		}
	}

	void OnLevelWasLoaded (int level) {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !(SceneManager.GetActiveScene ().name == "main_menu"))
		{
			canvas.enabled = !canvas.enabled;
			Cursor.visible = !Cursor.visible;
			PauseGame ();
		}
	}

	public static PauseMenuController GetMenuController () {
		return menuController;
	}

	void PauseGame () {
		Debug.Log (Cursor.lockState);
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		if (Time.timeScale == 0) 
		{
			Cursor.lockState = CursorLockMode.Confined;
		} 

		else 
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		// LowPass ();
	}

	public void Save () {
		
	}

	public void Load () {

	}

	public void Options () {

	}

	public void Quit () {
		GameController.GetController ().Quit ();
	}
}