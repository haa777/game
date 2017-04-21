using UnityEngine;
using System.Collections;

public class PlatformSpawn : MonoBehaviour {
	//Proceduarl generate platform
	public Transform player;
	float playerY;

	public Transform normal; //normal platform
	public Transform invisible; //invisible platform
	public Transform bonuspad; //bonuspad platform
	public Transform movePlatform; //move platform
	public Transform spikePlatform; //spibke platform
	public Transform breakPlatform; // break platform
	public Transform hightransparents; // high transparents
	public Transform rotatePlatform; // rotate platform
	public Transform flyFish; //fly fish
	public Transform flyDead; //fly dead

	private int large;
	private float check;
	private float spawn;
	private int platformIndex;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		large = 30;
		Instantiate (normal, new Vector3 (2, -3, 0), Quaternion.identity);
		Instantiate (normal, new Vector3 (5, 1, 0), Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {
		changelevel ();
		//player height
		playerY = player.position.y;
		if (playerY > check) {
			platformGenerate ();
		}
	}

	//Generate platform
	void platformGenerate()
	{
		check = player.position.y + 5;
		spawnPlatform (check + 10);
	}

	//randomly spwan platform
	void spawnPlatform(float spawnPoint){
		float y = spawn;
		while (y <= spawnPoint) {
			platformIndex = Random.Range (1, large);
			float x = Random.Range (-7f, 7f);
			switch (platformIndex) {
			case  1:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  2:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  3:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  4:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  5:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  9:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  8:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  7:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;			
			case  35:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  6:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  11:
				Instantiate (normal, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  12:
				Instantiate (invisible, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  19:
				Instantiate (bonuspad, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  17:
				Instantiate (movePlatform, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  36:
				Instantiate (spikePlatform, new Vector3 (x, y, 0), Quaternion.identity);
				break;		
			case  14:
				Instantiate (hightransparents, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  13:
				Instantiate (breakPlatform, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  16:
				Instantiate (rotatePlatform, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  31:
				Instantiate (spikePlatform, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			case  21:
				Instantiate (flyFish, new Vector3 (x, y, 0), Quaternion.identity);
				break;			
			case  25:
				Instantiate (flyDead, new Vector3 (x, y, 0), Quaternion.identity);
				break;
			}
			y = y + Random.Range (0.7f, 2f);
		}
		spawn = spawnPoint;
	}

	void changelevel(){
		if (GameObject.FindGameObjectWithTag ("Player").transform.position.y > 100f) {
			large = 35;
		}
		if (GameObject.FindGameObjectWithTag ("Player").transform.position.y > 200f) {
			large = 40;
		}
		if (GameObject.FindGameObjectWithTag ("Player").transform.position.y >400f) {
			large = 50;
		}
	}

}
