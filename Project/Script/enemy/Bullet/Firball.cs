using UnityEngine;
using System.Collections;

public class Firball : MonoBehaviour {

	[SerializeField]
	private float bulletSpeed;

	private Rigidbody2D rb;

	private Vector2 direction;

	Animator anim;



	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		direction = transform.up;

	}

	// Update is called once per frame
	void Update () 
	{	
		//have initial speed
		rb.AddForce( direction* bulletSpeed);
		 

	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player" ) {
			//Destroy itself when meet player
			Destroy(gameObject);
		}

		if (other.gameObject.tag == "Wall") {
			//Destroy itself when meet wall
			Destroy(gameObject);
		}

		if (other.gameObject.tag == "DeathLine") {
			//Destroy itself when meet DeathLine
			Destroy(gameObject);
		}

	}

	void OnBecameInvisible(){
		//when it is out of vision, then destroy
		//itself
		Destroy (gameObject);
	}
}
