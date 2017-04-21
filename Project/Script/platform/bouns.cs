using UnityEngine;
using System.Collections;

public class bouns : MonoBehaviour {

	private BoxCollider2D playerCollider;
	Rigidbody2D rg;
	public float force;
	//[SerializeField]
	//private BoxCollider2D platformCollider;
	//[SerializeField]
	//private BoxCollider2D platformTrigger;
	// Use this for initialization
	void Start () {
		
		rg = GameObject.Find ("Player").GetComponent<Rigidbody2D> ();
		playerCollider = GameObject.Find ("Player").GetComponent<BoxCollider2D> ();
		//Physics2D.IgnoreCollision (platformCollider,platformTrigger, true);
	}

	void FixedUpdate(){
		force = Random.Range (1500f, 2500f);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "Player") {
			rg.AddForce (new Vector2 (0, force));
		}
	}
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			//Physics2D.IgnoreCollision (platformCollider, playerCollider, true);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			//Physics2D.IgnoreCollision (platformCollider, playerCollider, false);
		}
	}
}
