using UnityEngine;
using System.Collections;

public class Cow : MonoBehaviour {
	//This enemy will pull player down or up
	//every 2 seconds

	private Vector3 target;
	private Vector3 currentPosition;
	public float attackThreshold;
	private float coolDown;
	public float speed;
	public float attackSpeed;
	private bool faceRight;
	private float health;

	DecisionTree root;


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
		
	//Check if player is on the left
	bool CheckUp()
	{
		Debug.Log ("Check Up");
		float playerDist = gameObject.transform.position.y - GameObject.FindWithTag ("Player").transform.position.y;
		//if the value less or equal than 0
		//player is above cow
		if(playerDist < 0)
			return true;
		else
			return false;
	}

	//Check if player is on the left
	bool CheckDown()
	{
		Debug.Log ("Check Down");
		float playerDist = gameObject.transform.position.y - GameObject.FindWithTag ("Player").transform.position.y;
		//if the value less or equal than 0
		//player is below cow
		if(playerDist > 0)
			return true;
		else
			return false;
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
	//Move player up
	void MoveUp(){
		Debug.Log("Check move Up");
		GameObject.Find ("Player").SendMessage ("MoveUp");
		coolDown = Time.time + attackSpeed;
	}
	//Move player down
	void MoveDown(){
		Debug.Log("Check move Down");
		GameObject.Find ("Player").SendMessage ("MoveDown");
		coolDown = Time.time + attackSpeed;
	}
			
	/******  Build Decision Tree  ******/

	void BuildDecisionTree()
	{
		/******  Decision Nodes  ******/

		DecisionTree isInRangeNode = new DecisionTree();
		isInRangeNode.SetDecision(CheckPlayerDistance);

		DecisionTree isUpNode = new DecisionTree();
		isUpNode.SetDecision(CheckUp);

		DecisionTree isAttackNode = new DecisionTree();
		isAttackNode.SetDecision(CheckAttack);

		DecisionTree isAttackNode2 = new DecisionTree();
		isAttackNode2.SetDecision(CheckAttack);

		DecisionTree isDownNode = new DecisionTree();
		isDownNode.SetDecision(CheckDown);

		DecisionTree actUpNode = new DecisionTree();
		actUpNode.SetAction(MoveUp);

		DecisionTree actDownNode = new DecisionTree();
		actDownNode.SetAction(MoveDown);

		/******  Assemble Tree  ******/
		//cow pull player up or push player down
		isInRangeNode.SetLeft(isUpNode);
		isUpNode.SetLeft(isAttackNode); 
		isAttackNode.SetLeft (actDownNode);

		isUpNode.SetRight (isDownNode);

		isDownNode.SetLeft (isAttackNode2);
		isAttackNode2.SetLeft (actUpNode);

		root = isInRangeNode;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "bullet") {
			GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("addScore", 110);
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
