using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	private float tCycle;
	void Start() {
		
	}

	void Update ()
	{

		transform.Rotate (0, 0, 0.5f);
//		var t = Time.time;
//
//		if (tCycle - t <= 1) { 
//			transform.Rotate (0, 0, 180 * Time.deltaTime);
//		}
//
//		if (t > tCycle) {
//			tCycle = t + 3;
//			if (transform.localEulerAngles.z < 90) {
//				transform.localEulerAngles.z = 0;
//			} else {
//				transform.localEulerAngles.z = 180;
//			}
//		}
	}
}
