using UnityEngine;
using System.Collections;

public class rotation : MonoBehaviour {

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
}
