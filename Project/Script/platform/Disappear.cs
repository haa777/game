using UnityEngine;
using System.Collections;

public class Disappear : MonoBehaviour {
	[SerializeField]
	private SpriteRenderer render;


	private BoxCollider2D box;
	// Use this for initialization
	void Start () {
		//renderer = FindObjectOfType<SpriteRenderer> ();
		box = GetComponent<BoxCollider2D> ();
	}

	// Update is called once per frame
	void Update () {
		int seconds = (int) Time.time / 3;
		bool oddeven = (seconds % 2) == 0;

		render.enabled = oddeven;
		box.enabled = oddeven;
	}
}
