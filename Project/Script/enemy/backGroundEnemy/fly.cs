using UnityEngine;
using System.Collections;

public class fly : MonoBehaviour {
	Rigidbody2D rb;
	private Vector2 direction;
	private float speed;
	private bool faceRight;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		speed = 0.5f;
		faceRight = false;
	}
	
	// Update is called once per frame
	void Update () {
		randomFly ();
		deadAfterPlayerPass ();
		Flip ();
	}

	void randomFly(){
		int rand = Random.Range (0, 3);
		if (rand == 3) {
			direction = transform.right;
			rb.AddForce (direction*(-speed));
		}
		if (rand == 2) {
			direction = transform.right;
			rb.AddForce (direction * speed);
		}
	}

	//Flip Direction to face player
	void Flip(){
		float direct = GameObject.FindWithTag ("Player").transform.position.x - transform.position.x;
		if (direct > 0 && !faceRight || direct < 0 && faceRight) {
			faceRight = !faceRight;
			Vector3 temp = transform.localScale;
			temp.x *= -1;
			transform.localScale = temp;
		}
	}

	void deadAfterPlayerPass(){

		float playerDist = gameObject.transform.position.y - GameObject.FindWithTag ("Player").transform.position.y;
		//if the distance less than attack threshold then player is within range
		if(playerDist <= -6f)
			Destroy (gameObject,0.5f);

	}
}
