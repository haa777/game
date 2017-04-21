using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	//script for UI to show player score
	//based on height, will modify to combine height and the number enemies killed

	Rigidbody2D rg;
	public Text scoreText;
	public Text finalScore;
	private float currentHeight;
	private float newHeight;
	private int score;
	private int diff;
	private bool godlike;
	public GameObject player;

	// Use this for initialization
	void Start () {
		score = 0;
		godlike = false; 
		currentHeight = player.transform.position.y;
		newHeight = player.transform.position.y;

		rg = player.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		if (!godlike) {
			//show score when playing
			scoreText.text = "Score: " + score;

			//show the final score when game over
			finalScore.text = "" + score;

			if (player == null) {
				return;
			}
			if (rg.velocity.y == 0) {
				getNewheight ();
			}
		} else {
			score = 0;
			scoreText.text = "Score: " + score;

			//show the final score when game over
			finalScore.text = "" + score;

			if (player == null) {
				return;
			}
			if (rg.velocity.y == 0) {
				getNewheight ();
			}
		}
	}

	//update the new height
	void getNewheight(){
		if (player.transform.position.y > currentHeight) {
			newHeight = player.transform.position.y;
			calculateScore ();
			currentHeight = newHeight;
		}
	}

	//get different score base on the different height player reach by once
	void calculateScore(){
		diff = (int)(newHeight - currentHeight);
		if (diff > 0 && diff <= 2) {
			score += 100;
		}

		if (diff > 2 && diff <= 4) {
			score += 200;
		}
		if (diff > 4) {
			score += 500;
		}
	}

	//add score to current score
	void addScore(int number){
		score = score + number;
	}

	//make the score to be zero
	void zero(){
		godlike = true;
	}
}
