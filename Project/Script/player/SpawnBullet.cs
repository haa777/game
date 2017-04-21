using UnityEngine;
using System.Collections;

public class SpawnBullet : MonoBehaviour {

	//this script not used



	public GameObject normalBullet;
	private GameObject bullet;


	private float time;
	private float cooldown=1f; // shooting cooldown, shoot once every 2 second



	// Use this for initialization
	void Start () {
		bullet = normalBullet;
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if ((Input.GetKeyDown (KeyCode.LeftShift)) && (Time.time >= time)) {
			Shoot ();
		}
	}

	void Shoot(){
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		float z = Mathf.Atan2 (mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg -90;
		bullet.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, z));
		Instantiate (bullet, new Vector3(transform.position.x,transform.position.y,transform.position.z), bullet.transform.rotation);

		time = Time.time + cooldown;
	}

	public void SetBullet(int number){
		if (number == 1) {
			bullet = normalBullet;
		}
		if (number == 2) {

		}
	}
}
