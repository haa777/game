using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	//this script is used for menu UI: pause and game over menu
	//privode some control function



	public GameObject pauseUI;
	public GameObject overUI;
	private GameObject player;
	private bool paused = false;

	void Start(){
		pauseUI.SetActive (false);
		overUI.SetActive (false);
		player = GameObject.Find ("Player");
	}

	void Update(){
		//when click escape on keyborad, active pause menu and freeze the screen
		// when player died, active game over screen
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = true;
		}
		if (paused) {
			pauseUI.SetActive (true);
			Time.timeScale = 0;
		}
		if (!paused) {
			pauseUI.SetActive (false);
			Time.timeScale = 1;
		}
		if (player == null) {
			overUI.SetActive (true);

		}
	}
	//function for the resume button on pause menu
	public void Resume(){
		paused = false;
	}
	//function for the play again button on game over menu and retry button on pause menu
	public void Retry(){

		Application.LoadLevel (1);

	}
	//function for the quit button on menu
	public void Quit(){

		Application.LoadLevel (0);
	}
}
