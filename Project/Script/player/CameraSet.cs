using UnityEngine;
using System.Collections;

public class CameraSet : MonoBehaviour {


	//set camera follow player


	private Transform player;
	private float highest;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){

		if (player != null) {

			transform.position = new Vector3 (-0.79f, player.position.y+2f, transform.position.z);
		}
	}
}
