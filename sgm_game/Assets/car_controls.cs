using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_controls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float vertical = Input.GetAxis ("Vertical");
		if (vertical != 0f ) {
			transform.Translate (vertical * Vector3.forward * 5f * Time.deltaTime);
		}

		float horizontal = Input.GetAxis ("Horizontal");
		if (horizontal != 0f) {
			if (vertical > 0) {
				transform.Rotate (Vector3.up, 50f * Time.deltaTime * horizontal);
			} else if (vertical < 0) {
				transform.Rotate (Vector3.up, 50f * Time.deltaTime * -horizontal);
			}
		}

		if ( Input.GetButtonDown ("Jump") ) { // teleport
			transform.Translate (Vector3.forward * 3f);
		}
	}
}
