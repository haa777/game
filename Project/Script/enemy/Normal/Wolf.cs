using UnityEngine;
using System.Collections;

public class Wolf : MonoBehaviour {
	//Wolf will attack player with
	//wave and self destruct

	Rigidbody2D rb;
	private Vector3 target;
	private Vector3 currentPosition;
	public float attackThreshold;
	private float coolDown;
	public float attackSpeed;
	private bool faceRight;
	private float health;
	public Transform bulletSpawn;
	public GameObject wavePrefab;

	DecisionTree root;
	private Animator anim;

	void Awake()
	{
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		faceRight = false;
		BuildDecisionTree ();
	}

	// Update is called once per frame
	void Update () {
		Flip ();
		root.Search();
		deadAfterPlayerPass ();
	}

	//Flip the direction to face player
	void Flip(){
		float direct = GameObject.FindWithTag ("Player").transform.position.x - transform.position.x;
		if (direct > 0 && !faceRight || direct < 0 && faceRight) {
			faceRight = !faceRight;
			Vector3 temp = transform.localScale;
			temp.x *= -1;
			transform.localScale = temp;
		}
	}

	//Get health from player
	void setHealth(float hp){
		health = hp;
	}

	//~~~~~~~~~~~~~Decision~~~~~~~~~~~~~~~//
	//Check if player is within fire range
	bool CheckPlayerDistance()
	{
		Debug.Log("Check pDistance");
		anim.SetTrigger ("Idle");
		float playerDist = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag ("Player").transform.position);
		//if the distance less than attack threshold then player is within range
		if(playerDist < attackThreshold)
			return true;
		else
			return false;
	}

	//Check if this is a safe distance
	bool CheckifSafe(){
		//it is safe when player is out of attack range
		float playerDist = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag ("Player").transform.position);
		float safeDist = 1.5f * attackThreshold;
		if (playerDist >safeDist ) {
			return true;
		} else {
			return false;
		}
	}

	//check if enemy can attack
	/***********still need to be test*****************/
	bool CheckAttack()
	{	
		Debug.Log("Check actAttack");
		//only attack after the last attack
		if (Time.time >= coolDown) {
			return true;
		}
		else{
			return false;
		}	
	}

	//~~~~~~~~~~~~~Action~~~~~~~~~~~~~~~//
	//Attack player with fire ball
	void Attack(){
		Debug.Log("Check Attack");
		anim.SetTrigger ("Attack");
		//calculate angle to fire the fireball
		currentPosition = bulletSpawn.position;
		target.x = GameObject.FindWithTag ("Player").transform.position.x;
		target.y = GameObject.FindWithTag ("Player").transform.position.y;
		target.x = target.x - currentPosition.x;
		target.y = target.y - currentPosition.y;
		float angle = Mathf.Atan2 (target.y, target.x) * Mathf.Rad2Deg;
		bulletSpawn.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
		Instantiate (wavePrefab, bulletSpawn.position, bulletSpawn.rotation);
		coolDown = Time.time + attackSpeed;
	}

	void Boom(){
		anim.SetTrigger ("Boom");
		Destroy (gameObject,1f);
	}

	/******  Build Decision Tree  ******/

	void BuildDecisionTree()
	{
		/******  Decision Nodes  ******/

		DecisionTree isInRangeNode = new DecisionTree();
		isInRangeNode.SetDecision(CheckPlayerDistance);

		DecisionTree isAttackNode = new DecisionTree();
		isAttackNode.SetDecision(CheckAttack);

		DecisionTree actAttackNode = new DecisionTree();
		actAttackNode.SetAction(Attack);

		DecisionTree actBoomNode = new DecisionTree();
		actBoomNode.SetAction(Boom);

		/******  Assemble Tree  ******/
		//wolf will attack player and self destruct
		//to kill player if player is within range
		isInRangeNode.SetLeft(actBoomNode);
		isInRangeNode.SetRight (isAttackNode);

		isAttackNode.SetLeft(actAttackNode); 

		root = isInRangeNode;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "bullet") {
			GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("addScore", 190);
			Destroy (gameObject);
		}
		if (other.gameObject.tag == "DeathLine") {
			//Destroy itself when meet DeathLine
			Destroy(gameObject);
		}
	}
	void deadAfterPlayerPass(){

		float playerDist = gameObject.transform.position.y - GameObject.FindWithTag ("Player").transform.position.y;
		//if the distance less than attack threshold then player is within range
		if(playerDist <= -6f)
			Destroy (gameObject,0.5f);

	}
}
