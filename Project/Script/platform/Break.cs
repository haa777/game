using UnityEngine;
using System.Collections;

public class Break : MonoBehaviour {


	private Rigidbody2D rd;
	// Use this for initialization
	void Start () {
		rd = GetComponent<Rigidbody2D> ();

	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag=="Player") {
			Invoke ("Fall", 1f);
		}
	}

	// Update is called once per frame
	void Fall(){
		rd.isKinematic = false;
	}

}
