using UnityEngine;
using System.Collections;

public class ControllerHandler : MonoBehaviour {

	public GameObject GameController;

	// Use this for initialization
	void Awake () {
		GameObject control = GameObject.FindGameObjectWithTag ("GameController");

		if (!control)
		{
			Instantiate (GameController);
		}
	}
}
