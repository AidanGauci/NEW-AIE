using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private int RoundScore = 0;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
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
		Invoke("SendScore", 0.05f);
	}

	public void LoadControls()
	{
		SceneManager.LoadScene ("ControlsScreen");
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

	void SendScore()
	{
		GameObject scriptHolder = (GameObject)GameObject.Find ("MainCanvas");
		scriptHolder.GetComponent<EndScreenController> ().score = RoundScore;
	}
}
