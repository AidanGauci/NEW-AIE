using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad (transform.gameObject);
	}

	public void LoadGameScene()
	{
		SceneManager.LoadScene ("BaseScene");
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
	}


	public void LoadEndMenu()
	{
		SceneManager.LoadScene ("EndMenu");
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
