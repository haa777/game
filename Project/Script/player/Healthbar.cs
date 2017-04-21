using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

	//script for UI to show player health


	private float fillAmount1;
	private float fillAmount2;
	private float fillAmount3;
	private float totalHealth;
	public Image heart1;
	public Image heart2;
	public Image heart3;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		totalHealth = 3f;
		fillAmount1 = fillAmount2 = fillAmount3 = 1f;
	}

	// Update is called once per frame
	void Update () {

		HandleBar ();
		die ();
	}

	private void HandleBar(){
		heart1.fillAmount = fillAmount1;
		heart2.fillAmount = fillAmount2;
		heart3.fillAmount = fillAmount3;
	}



	//calculate player health
	public void setHealth(float damg){
		if (totalHealth > 2f && damg>0) {
			fillAmount3 -= 1f;
			totalHealth -= 1f;
			damg -= 1f;
		}
		if (totalHealth > 1f && totalHealth<=2f && damg>0) {
			fillAmount2 -= 1f;
			totalHealth -= 1f;
			damg -= 1f;
		}
		if (totalHealth > 0 && totalHealth<=1f && damg>0) {
			fillAmount1 -= 1f;
			totalHealth -= 1f;
			damg -= 1f;
		}
	}

	//when player type in cheating code
	//they can be god like
	public void godlike(){
		totalHealth = 5000;
	}

	private void die(){
		//when health hit 0, call destroy function from player 
		if (totalHealth == 0 && player!=null) {
			GameObject.Find ("Player").SendMessage ("destoryPlayer", 1);
		}
	}

}
