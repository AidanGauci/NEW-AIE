﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

public class EndScreenController : MonoBehaviour {

	public GameObject NameEnterCanvas;
	public Button QuitButton;
	public Button RestartButton;
	public Text textBox;
	public int score;

	private string playerName;
	private string filename;
	private List<string> Names = new List<string>();
	private List<int> Scores = new List<int>();
	private int TopScores = 10;

	// Use this for initialization
	void Start () {
		filename = Application.dataPath + "/highScores.txt";
		NameEnterCanvas.SetActive (true);
		QuitButton.interactable = false;
		RestartButton.interactable = false;
	}

	public void SetName()
	{
		playerName = textBox.text;
		if (playerName == "")
		{
			playerName = "Ghost";
		}
		NameEnterCanvas.SetActive (false);
		QuitButton.interactable = true;
		RestartButton.interactable = true;
		GetHighScores ();
		DisplayScores ();
		SaveHighScores ();
	}

	void DisplayScores()
	{
		SortOrder ();
		string displayNames = "";
		string displayScores = "";
		TopScores = Names.Count;
		if (TopScores > 10)
		{
			TopScores = 10;
		}
		for (int i = 0; i < TopScores; i++) {
			displayNames += Names[i] + "\n";
			displayScores += Scores[i] + "\n";
		}
		Text scoreDisplayBox = (Text) GameObject.Find ("ScoreBox").GetComponent<Text>();
		scoreDisplayBox.text = displayScores;
		Text nameDisplayBox = (Text) GameObject.Find ("NameBox").GetComponent<Text>();
		nameDisplayBox.text = displayNames;
	}

	void GetHighScores()
	{
		for (int i = 0; i < TopScores; i++) {
			string tempName = PlayerPrefs.GetString ("Name" + i, "FailedToLoadAName");

			if (tempName == "FailedToLoadAName")
			{
				break;
			}
			int tempScore = PlayerPrefs.GetInt ("Score" + i);
			Names.Add (tempName);
			Scores.Add (tempScore);
		}

		/*try
		{
			string line;

			StreamReader reader = new StreamReader (filename);

			using (reader)
			{
				do
				{
					line = reader.ReadLine ();
					if (line != null)
					{
						string[] entries = line.Split (',');
						Names.Add(entries[0]);
						Scores.Add(entries[1]);
					}
				} while (line != null);

			}
			reader.Close ();
		}
		catch (Exception ex)
		{
			Console.WriteLine ("{0}\n", ex.Message);
		}*/
	}

	void SortOrder()
	{
		if (Names.Count == 0)
		{
			Names.Add (playerName);
			Scores.Add (score);
		} 
		else
		{
			for (int i = 0; i < Names.Count; i++)
			{
				if (score > Convert.ToInt32 (Scores [i]))
				{
					Names.Insert (i, playerName);
					Scores.Insert (i, score);
					break;
				}
			}
			Names.Add (playerName);
			Scores.Add (score);
		}
	}

	void SaveHighScores()
	{
		for (int i = 0; i < TopScores; i++) {
			PlayerPrefs.SetString ("Name" + i, Names [i]);
			PlayerPrefs.SetInt ("Score" + i, Scores[i]);
		}

		/*try
		{
			StreamWriter writer = new StreamWriter (filename, false);
			for (int i = 0; i < TopScores; i++) {
				writer.WriteLine(Names[i] + "," + Scores[i]);
			}

			writer.Close();
			
		}
		catch (Exception ex)
		{
			Console.WriteLine ("{0}\n", ex.Message);
		}*/
	}
}
