using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class wall: MonoBehaviour {
	public GameObject[] tiles;

	public List<Vector3> createTiles;

	public int tileAmount;
	public int tileSize;
	public int tileIndex;
	public float chanceUp;
	public float waitTime;
	public int dir;
	// Use this for initialization
	void Start () {
		StartCoroutine (GenerateLevel());
	}

	IEnumerator GenerateLevel(){
		while(tileAmount>0) {
			//int dir = Random.Range (0, 3);
			int tile = Random.Range (0, tiles.Length);

			createdTile (tile);
			CallMoveGen ();
			yield return new WaitForSeconds (waitTime);
		}
	}
	void CallMoveGen(){
		MoveGen ();
	}

	void MoveGen(){
		transform.position = new Vector3 (transform.position.x, transform.position.y + tileSize, 0);
	}
	// Update is called once per frame
	void Update () {

	}
	void createdTile(int tileIndex){

		GameObject tileObject;
		tileObject = Instantiate (tiles [tileIndex], transform.position, transform.rotation) as GameObject;
		createTiles.Add (tileObject.transform.position);

	}

}
