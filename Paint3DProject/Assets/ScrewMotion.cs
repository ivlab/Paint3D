using UnityEngine;
using System.Collections;

public class ScrewMotion : MonoBehaviour {

	public float verticalSpeedFactor = 1.0f;
	public float rotationSpeedFactor = 1.0f;


	// state 0 = waiting for action
	// state 1 = fade in
	// state 2 = in use
	// state 3 = fade out
	private int state = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (state == 0) {
			// check to see if the user's head position is on top of the access mark
			// if so, then enter state 1
		}
		else if (state == 1) {
			if (transform.position [1] < 4.0) {
				transform.position += verticalSpeedFactor * Vector3.up;
				transform.rotation *= Quaternion.AngleAxis (rotationSpeedFactor, Vector3.up);
			} else {
				state = 2;
			}
		} else if (state == 2) {
			// do something here to pick items from the menu
			// change cursor mesh
			// etc..

			// should also be some option to dismiss the menu or after you make a selection
			state = 3;
		} else if (state == 3) {
			if (transform.position[1] > 0.0) {
				transform.position += -verticalSpeedFactor * Vector3.up;
				transform.rotation *= Quaternion.AngleAxis(-rotationSpeedFactor, Vector3.up);
			}
			else {
				state = 0;
			}
		}


	}
}
