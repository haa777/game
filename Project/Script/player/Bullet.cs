using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	//a script for player bullet

	public float speed;
	private Rigidbody2D rg;
	private Vector2 direction;

	private AudioClip fire;
	private AudioSource source;


	// Use this for initialization
	void Start () {
		rg = GetComponent<Rigidbody2D> ();
		direction = Vector2.up;


	}

	//give a upward speed to bullet when spawned
	void FixedUpdate(){
		rg.velocity = direction * speed;
	}
	// Update is called once per frame
	void Update () {

	}

	//	public void Initialize(Vector2 direction){
	//		this.direction = direction;
	//	}

	//when the bullet out of screen, destroyed. this make sure 
	//bullet not going infinite to kill enemy that ouside of screen
	void OnBecameInvisible(){
		Destroy (gameObject);
	}


	//when hit enemy, the bullet disappear
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Enemy") {
			Destroy (gameObject);
			source.PlayOneShot (fire, 1f);
		}
		if (other.gameObject.tag == "Boss") {
			Destroy (gameObject);
			source.PlayOneShot (fire, 1f);
		}
	}
}
