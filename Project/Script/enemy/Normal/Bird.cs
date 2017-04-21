using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {
	//This enemy will attack player only when player can be
	//killed with one shot or there are allies nearby. 
	//It can crash player with highest speed among all monsters
	Rigidbody2D rb;

	private Vector3 target;
	private Vector3 currentPosition;
	public float attackThreshold;

	public float speed;
	private bool faceRight;
	private float health;

	DecisionTree root;

	void Awake()
	{
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
		float playerDist = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag ("Player").transform.position);
		//if the distance less than attack threshold then player is within range
		if(playerDist < attackThreshold)
			return true;
		else
			return false;
	}

	//Check if player's health is safe
	//return true if it is safe
	bool CheckPlayerHealth()
	{
		Debug.Log("Check phealth");
		//player can be killed by one shot
		if (health >= 1f) {
			return true;
		} else {
			return false;
		}
	}

	//Check if there are at least one allies
	//return true if there are one
	bool CheckAllies(){
		//Get the close allies distance
		float alliesDist = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag ("Enemy").transform.position);
		if (alliesDist <= 3f) {
			return true;
		} else {
			return false;
		}
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

	//~~~~~~~~~~~~~Action~~~~~~~~~~~~~~~//
	//Move to player
	void Move(){
		Debug.Log("Check Move");
		//move toward player
		rb.transform.position = Vector3.MoveTowards (rb.transform.position, GameObject.FindWithTag ("Player").transform.position, speed * Time.deltaTime);
	}

	//Attack player with fire ball
	void Attack(){
		Debug.Log("Check Attack");
		//crash player with 1.5 times speed
		rb.transform.position = Vector3.MoveTowards (rb.transform.position, GameObject.FindWithTag ("Player").transform.position, 1.5f*speed * Time.deltaTime);
	}

	//Flee from player
	void Flee(){
		Debug.Log("Check Flee");
		//flee from player with 1.2 times speed
		rb.transform.position = Vector3.MoveTowards (rb.transform.position, GameObject.FindWithTag ("Player").transform.position, 1.2f*speed * Time.deltaTime);
	}

	/******  Build Decision Tree  ******/

	void BuildDecisionTree()
	{
		/******  Decision Nodes  ******/

		DecisionTree isInRangeNode = new DecisionTree();
		isInRangeNode.SetDecision(CheckPlayerDistance);

		DecisionTree isInRangeNode2 = new DecisionTree();
		isInRangeNode.SetDecision(CheckPlayerDistance);

		DecisionTree isPlayerHealthyNode = new DecisionTree();
		isPlayerHealthyNode.SetDecision(CheckPlayerHealth);

		DecisionTree isAlliesNearNode = new DecisionTree ();
		isAlliesNearNode.SetDecision (CheckAllies);

		DecisionTree isSafe = new DecisionTree();
		isSafe.SetDecision(CheckifSafe);

		DecisionTree actApproachNode = new DecisionTree();
		actApproachNode.SetAction(Move);

		DecisionTree actAttackNode = new DecisionTree();
		actAttackNode.SetAction(Attack);

		DecisionTree actFleeNode = new DecisionTree();
		actFleeNode.SetAction(Flee);


		/******  Assemble Tree  ******/
		//bird will first check if player's health is low
		//if it is low, then try to decide if it is ok to attack
		isPlayerHealthyNode.SetLeft(isAlliesNearNode);
		isPlayerHealthyNode.SetRight(isInRangeNode);

		isInRangeNode.SetLeft(actAttackNode); 
		isInRangeNode.SetRight(actApproachNode);

		isAlliesNearNode.SetLeft (isInRangeNode2);
		isAlliesNearNode.SetRight (isSafe);
		isSafe.SetRight (actFleeNode);

		isInRangeNode2.SetLeft(actAttackNode); 
		isInRangeNode2.SetRight(actApproachNode);


		root = isPlayerHealthyNode;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "bullet") {
			GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("addScore", 100);
			Destroy (gameObject);
		}
		if (other.gameObject.tag == "Player") {
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
