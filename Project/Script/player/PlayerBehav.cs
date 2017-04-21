using UnityEngine;
using System.Collections;

public class PlayerBehav : MonoBehaviour {

	//a script to support player's movement, set animation, several behaviour


	Rigidbody2D rg;
	Animator anim; 
	public float moveSpeed;
	public float jumpForce;
	private bool facingRight;
	private bool isGrounded;
	private bool jump;
	public float force;
	public float attackSpeed = 5f;
	private float coolDown;
	private bool god;

	public GameObject bulletPrefab;
	public GameObject slashPrefab;
	public GameObject inverseSlashPrefab;
	public GameObject platformPrefab;
	public Transform bulletSpawn;
	public Transform slashSpawn;
	public Transform platformTransform;

	[SerializeField]

	private AudioClip dead;
	private AudioSource ad;
	bool death;

	// Use this for initialization
	void Start () {
		ad = GetComponent<AudioSource> ();
		facingRight = true;
		rg = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		death = false;
		god = false;
	}

	void FixedUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		//isGrounded = IsGrounded ();
		Move (horizontal);
		Flip (horizontal);
		resetValue ();
	}

	// Update is called once per frame
	void Update () {
		handleInput ();
		checkPosition ();
	}

	private void Move(float horizontal){
		rg.velocity = new Vector2 (horizontal * moveSpeed, rg.velocity.y);
		if (isGrounded && jump) {
			isGrounded = false;
			rg.AddForce (new Vector2 (0, jumpForce));
		}
		anim.SetFloat ("speed", Mathf.Abs (horizontal));
		if (rg.velocity.y != 0) {
			anim.SetBool ("fly", true);
		}
	}

	private void MoveUp(){
		rg.AddForce (transform.up*force);
		anim.SetBool ("fly", true);
	}

	private void MoveDown(){
		rg.AddForce (transform.up * (-force));
		anim.SetBool ("fly", true);
	}

	private void Flip (float horizontal)
	{
		if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)) {
			facingRight = !facingRight;
			Vector3 Scale = transform.localScale;
			Scale.x *= -1;
			transform.localScale = Scale;
		}
	}

	private void handleInput(){
		if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.K)){
			jump=true;
			//anim.ResetTrigger ("ground");
		}

		//let player to shoot bullet
		if (Input.GetKeyDown (KeyCode.J)) {
			Fire ();
			anim.SetTrigger ("attack");
		}

		//let player to release slash
		if (Input.GetKeyDown (KeyCode.H)) {
			if (facingRight) {
				Slash (0);
			} 
			else {
				Slash (1);
			}
			anim.SetTrigger ("attack");
		}

		//let player to generate a platform after coolDown tiem
		if (Input.GetKeyDown (KeyCode.L)&&Time.time >= coolDown) {
			platform ();
		}

		//when player use cheating mode, make player like god.
		if (Input.GetKeyDown (KeyCode.Alpha3)&& Input.GetKeyDown (KeyCode.Alpha0) && Input.GetKeyDown (KeyCode.Alpha6) &&!god	) {
			//activitate god mode
			godlike ();
		}
	}

	private void resetValue(){
		jump = false;
	}

//	private bool IsGrounded ()
//	{
//		if (rg.velocity.y == 0) {
//			anim.SetBool ("fly", false);
//			return true;
//		}
//		return false;
//	}

	//fire bullet
	public void Fire(){
		Instantiate (bulletPrefab, bulletSpawn.position, Quaternion.identity);
	}

	//generate platform 
	public void platform(){
		Instantiate (platformPrefab, platformTransform.position,Quaternion.identity);
		coolDown = Time.time + attackSpeed;
	}

	//shoot slash as melee attack
	public void Slash(int dir){
		if (dir == 0) {
			Instantiate (slashPrefab, slashSpawn.position, Quaternion.identity);
		}
		if (dir == 1){
			Instantiate (inverseSlashPrefab, slashSpawn.position, Quaternion.identity);
		}	
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Enemy" ) {

			GameObject.Find ("health").SendMessage ("setHealth",   1f);
		}
		if (other.gameObject.tag == "Wolf" ) {

			GameObject.Find ("health").SendMessage ("setHealth",   3f);
		}
		if (other.gameObject.tag == "Platform" ) {
			anim.SetBool ("fly", false);
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Platform" ) {
			anim.SetBool ("fly", true);
			isGrounded = false;
		}
	}

	public void playSound(){
		ad.PlayOneShot (dead);
	}

	//make player  to be god
	void godlike(){
		GameObject.Find ("health").SendMessage ("godlike");
		jumpForce *= 2;
		GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("zero");
		attackSpeed = 0f;
		god = true;
	}

	public void destoryPlayer(int a){


		death = true;
		playSound ();
		Destroy (gameObject,0.5f);
		//Application.LoadLevel (0);
	}
	//make the player transform from left to right or right to left when the player is beyound the screen
	public void checkPosition(){
		
		if(transform.position.x < -10f){
			transform.position = new Vector3 (transform.position.x +18f, transform.position.y, transform.position.z);
		}
		else if
			(transform.position.x > 9f){

			transform.position = new Vector3 (transform.position.x -19f, transform.position.y, transform.position.z);
		}

	}
}
