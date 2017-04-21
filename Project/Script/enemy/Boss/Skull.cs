using UnityEngine;
using System.Collections;

public class Skull : MonoBehaviour {
	//Skull shoot only

	Rigidbody2D rb;

	private Vector3 target;
	private Vector3 currentPosition;
	public float attackThreshold;
	public float attackSpeed;
	public float attackSpeed2;
	private float coolDown;
	public float speed;
	private bool faceRight;
	private float health;
	private float selfHealth = 6f;
	public Transform bulletSpawn;
	public GameObject fireBallPrefab;
	public GameObject flamePrefab;
	private Animator anim;

	DecisionTree root;
	// Use this for initialization

	void Awake()
	{
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		faceRight = false;
		BuildDecisionTree ();
	}
	
	// Update is called once per frame
	void Update () {
		Flip ();
		root.Search();
		if (selfHealth <= 0f) {
			GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("addScore", 500);
			Destroy (gameObject);
		}
		deadAfterPlayerPass ();
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

	//Get health from player
	void setHealth(float hp){
		health = hp;
	}

	//Get own health
	void setSelfHealth(float damage){
		selfHealth = selfHealth - damage;
	}

	//~~~~~~~~~~~~~Decision~~~~~~~~~~~~~~~//
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

	bool CheckAttack2()
	{	
		Debug.Log("Check actAttack");
		//only attack after the last attack
		if (Time.time >= (coolDown + attackSpeed2)) {
			return true;
		}
		else{
			return false;
		}	
	}

	//check if health safe
	//return true if health is safe
	bool CheckHealth()
	{
		Debug.Log("Check shealth");
		if (selfHealth >= 3f) {
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
		float safeDist = attackThreshold + 1f;
		if (playerDist >safeDist ) {
			return true;
		} else {
			return false;
		}
	}

	//~~~~~~~~~~~~~Action~~~~~~~~~~~~~~~//
	//Control other normal enemy move to player
	void AskMove(){
		anim.SetTrigger ("Idle");
		GameObject.FindWithTag ("Enemy").SendMessage ("Move");
	}
		
	//Attack player with Flame
	void Attack(){
		Debug.Log("Check Attack");
		anim.SetTrigger ("Attack");
		//calculate angle to fire the flame
		currentPosition = bulletSpawn.position;
		target.x = GameObject.FindWithTag ("Player").transform.position.x;
		target.y = GameObject.FindWithTag ("Player").transform.position.y;
		target.x = target.x - currentPosition.x;
		target.y = target.y - currentPosition.y;
		float angle = Mathf.Atan2 (target.y, target.x) * Mathf.Rad2Deg;
		bulletSpawn.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));
		Instantiate (flamePrefab, bulletSpawn.position, bulletSpawn.rotation);
		coolDown = Time.time + attackSpeed;
	}

	//Attack player with bullet hell
	void BulletHell(){
		Debug.Log("Check BulletHell");
		//calculate angle to fire the bullethell
		currentPosition = bulletSpawn.position;
		target.x = GameObject.FindWithTag ("Player").transform.position.x;
		target.y = GameObject.FindWithTag ("Player").transform.position.y;
		target.x = target.x - currentPosition.x;
		target.y = target.y - currentPosition.y;
		float angle = Mathf.Atan2 (target.y, target.x) * Mathf.Rad2Deg;
		bulletSpawn.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));
		Instantiate (fireBallPrefab, bulletSpawn.position, bulletSpawn.rotation);
		Instantiate (fireBallPrefab, bulletSpawn.position, bulletSpawn.rotation);
		anim.SetTrigger ("Attack");
		coolDown = Time.time + attackSpeed;
	}

	//Flee from player
	void Flee(){
		Debug.Log("Check Flee");
		//Flee from player with double speed
		rb.transform.position = Vector3.MoveTowards (rb.transform.position, GameObject.FindWithTag ("Player").transform.position, -2*speed);
		anim.SetTrigger ("Move");
	}

	/******  Build Decision Tree  ******/

	void BuildDecisionTree()
	{
		/******  Decision Nodes  ******/
		DecisionTree isHealthyNode = new DecisionTree();
		isHealthyNode.SetDecision(CheckHealth);

		DecisionTree isPlayerHealthyNode = new DecisionTree();
		isPlayerHealthyNode.SetDecision(CheckPlayerHealth);

		DecisionTree isAttackNode = new DecisionTree();
		isAttackNode.SetDecision(CheckAttack);

		DecisionTree isAttackNode2 = new DecisionTree();
		isAttackNode2.SetDecision(CheckAttack2);

		DecisionTree isSafe = new DecisionTree();
		isSafe.SetDecision(CheckifSafe);

		DecisionTree isAlliesNearNode = new DecisionTree ();
		isAlliesNearNode.SetDecision (CheckAllies);

		DecisionTree actAskMoveNode = new DecisionTree();
		actAskMoveNode.SetAction(AskMove);

		DecisionTree actFleeNode = new DecisionTree();
		actFleeNode.SetAction(Flee);

		DecisionTree actBulletHellNode = new DecisionTree();
		actBulletHellNode.SetAction(BulletHell);

		DecisionTree actFlameNode = new DecisionTree ();
		actFlameNode.SetAction (Attack);

		/******  Assemble Tree  ******/
		//Wizard will attack player if playe is within range
		// and will release bullethell if its own health is 
		//lower than safe range
		isHealthyNode.SetLeft(isAttackNode);
		isHealthyNode.SetRight(isPlayerHealthyNode);

		isAttackNode.SetLeft (actBulletHellNode);

		isPlayerHealthyNode.SetLeft(isSafe);
		isPlayerHealthyNode.SetRight(isAttackNode2);
		isAttackNode2.SetLeft (actFlameNode);

		isSafe.SetLeft (isAlliesNearNode);
		isSafe.SetRight (actFleeNode);
		isAlliesNearNode.SetLeft (actAskMoveNode);

		root = isHealthyNode;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "bullet") {
			setSelfHealth (1f);
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
