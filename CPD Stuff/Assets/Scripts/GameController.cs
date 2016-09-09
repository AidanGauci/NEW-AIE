using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private int RoundScore = 0;

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


	public void LoadEndMenu(int Score)
	{
		SceneManager.LoadScene ("EndMenu");
		RoundScore = Score;
		Invoke("DisplayScores", 0.1f);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

	void DisplayScores()
	{
		Text scoreDisplayBox = (Text) GameObject.Find ("ScoreBox").GetComponent<Text>();
		scoreDisplayBox.text = RoundScore.ToString ();
	}
}
