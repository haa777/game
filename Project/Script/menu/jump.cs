using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour {

	//simple script for start screen, make the model jump

	private Rigidbody2D rg;
	// Use this for initialization
	void Start () {

		rg = gameObject.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D other){
		rg.AddForce (new Vector2 (0, 500));
	}
}
