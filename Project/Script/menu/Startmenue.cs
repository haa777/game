using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Startmenue : MonoBehaviour {

	//simple script for start screen, give a function to play button to load level


	// Use this for initialization
	void Start () {
		//make sure the screen is not pause
		Time.timeScale = 1;
	}

	// Update is called once per frame
	void Update () {

	}

	//function for the play button on start screen
	public void StartLevel(){

		Application.LoadLevel (1);

	}

}
