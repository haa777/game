using UnityEngine;
using System.Collections;

public class Slash : MonoBehaviour {
	//a script for player slash

	public float speed;
	private Rigidbody2D rg;
	private Vector2 direction;

	private AudioClip fire;
	private AudioSource source;


	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody2D> ();
		direction = Vector2.right;


	}

	//give a upward speed to slash when spawned
	void FixedUpdate(){
		rg.velocity = direction * speed;
	}
	// Update is called once per frame
	void Update () {
		checkDistance ();
	}

	//when the bullet out of screen, destroyed. this make sure 
	//bullet not going infinite to kill enemy that ouside of screen
	void OnBecameInvisible(){
		Destroy (gameObject);
	}

	void checkDistance(){
		float distance = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag ("Player").transform.position);
		if (distance > 3f) {
			//desctroy the slash since it is melee attack
			Destroy (gameObject);
		}
	}
		
	//when hit enemy, the bullet disappear
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Enemy") {
			Destroy (gameObject);
			Destroy (other.gameObject);
			source.PlayOneShot (fire, 1f);
		}
		if (other.gameObject.tag == "Boss") {
			Destroy (gameObject);
			Destroy (other.gameObject);
			source.PlayOneShot (fire, 1f);
		}
	}
}
