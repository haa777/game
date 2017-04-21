using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	//procedural generate enemy
	public Transform player;
	float playerY;

	public Transform cow; //cow enemy
	public Transform bird; //bird enemy
	public Transform frog; //frog enemy
	public Transform wolf; //worlf enemy
	public Transform skull; //skull enemy
	public Transform wizard; //wizard enemy

	private float check;
	private float spawn;
	private int enemyIndex;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		//player height
		playerY = player.position.y;
		if (playerY > check) {
			platformGenerate ();
		}

	}

	void platformGenerate()
	{
		check = player.position.y + 5;
		spawnEnemy (check + 10);
	}
				
	void spawnEnemy(float spawnPoint){
		float y = spawn;
		while (y <= spawnPoint) {
			enemyIndex = Random.Range (1, 60);
			float x = Random.Range (-5f, 5f);
			switch (enemyIndex) {
			case  20:
				Instantiate (cow, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  15:
				Instantiate (wolf, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  43:
				Instantiate (bird, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  31:
				if (GameObject.FindGameObjectsWithTag ("Boss").Length < 1) {
					Instantiate (wizard, new Vector3 (x, y, 0), Quaternion.identity);
				}
				break;
			case  8:
				if (GameObject.FindGameObjectsWithTag ("Boss").Length < 1) {
					Instantiate (skull, new Vector3 (x, y, 0), Quaternion.identity);
				}
				break;	
			case  57:
				Instantiate (frog, new Vector3 (x, y, 0), Quaternion.identity);
				break;	
			}
			y = y + Random.Range (1f, 5f);
		}
		spawn = spawnPoint;
	}
}
