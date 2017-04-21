using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void FixedUpdate () {
		
		if (Time.time % 10 == 0 && Time.time!=0) {
			transform.Rotate (0, 0, 180);
		} else {
			transform.Rotate (0, 0, 0);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "Player" ) {
			GameObject.Find ("health").SendMessage ("setHealth",   1f);
		}
	}
}
