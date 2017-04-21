using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class moveUP : MonoBehaviour {

	//this script is for deathline on screen, to force player move upward, if player move downward and
	//touch the deathline, player died

	public Transform player;
	private Rigidbody2D rg;
	private Transform to;
	private Vector2 direction;
	private float speed;

	// Use this for initialization
	void Start () {
		speed = 0.3f;
		//player = GameObject.Find ("Player").transform;
		rg = GetComponent<Rigidbody2D> ();
		direction = Vector2.up;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//deathline move speed increase 0.1 every 20s;
		if (Time.time % 20 == 0) {
			speed += 0.1f;
		}
		rg.velocity = direction * speed;
		//if player too far from deathline, then move dethline close to player.
		if (player.transform.position.y - transform.position.y > 15f) {
			
			transform.position = new Vector3 (transform.position.x, player.position.y-10f, transform.position.z);

		}
			
	}

	void OnCollisionEnter2D(Collision2D other){
		//deathline ignore everything except player, destroy player when hit
		if (other.gameObject.name == "Player" ) {
			GameObject.Find ("health").SendMessage ("setHealth",   3f);
		}

	}

}
