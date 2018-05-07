using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour {

	public float speed;
	public float maxSpeed;
	public float nitroMaxSpeed;

	public float rotation;

	private Rigidbody rb;
	private bool canJump = true;

	string player;


	void Start ()
	{
		rb = GetComponent<Rigidbody>();


		if (rb.gameObject.name == "PlayerA")
			player = "A";
		else
			player = "B";
		
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal" + player);
		float moveVertical = Input.GetAxis ("Vertical" + player);
	
		/*
		float speedLimiter;
		if (speedPerSec >= maxSpeed) 
			speedLimiter = 0f;
		else 
			speedLimiter = 1f;

		float nitroSpeedLimiter;
		if (speedPerSec >= maxSpeed) 
			nitroSpeedLimiter = 0f;
		else 
			nitroSpeedLimiter = 1f;
			*/

		
		if ( Input.GetAxis("Fire3"  + player) > 0 ) // if nitro
		    rb.AddRelativeForce (Vector3.forward * moveVertical * speed * 3f, ForceMode.VelocityChange);
		else 
			rb.AddRelativeForce (Vector3.forward * moveVertical * speed, ForceMode.VelocityChange);



		if (moveVertical >= 0f) {
			transform.Rotate(Vector3.up, rotation * moveHorizontal);
		} else if (moveVertical < 0f)
			transform.Rotate(Vector3.up, rotation * moveHorizontal * -1f);

		// JUMP

		/*
		if (Input.GetAxis ("Jump") > 0 && canJump) {
			rb.AddRelativeForce (Vector3.up * 10f, ForceMode.Impulse);
			canJump = false;

			Invoke("allowJump", 2);
		}
		*/

		// SPEED CALCULATION

		Vector3 currentPosition = transform.position;
		currentPosition = new Vector3(currentPosition.x, 0, currentPosition.z);
		speedPerSec = Vector3.Distance(oldPosition, currentPosition) / Time.deltaTime;
		oldPosition = transform.position;
		oldPosition = new Vector3(oldPosition.x, 0, oldPosition.z);
		print("speed: " + Mathf.Round(speedPerSec) + " ");


		// FLOATING

		float reverseGravity = 9.81f;
		if (transform.position.y <= 1.5f) {
			rb.AddForce (Vector3.up * (reverseGravity + 10f));
		} 
		//else if (transform.position.y <= 6f) {
		//	rb.AddForce (Vector3.up * ( (reverseGravity)  *  ((transform.position.y - 1) / 2) ));
		//}



	}

	private void allowJump() {
		canJump = true;
	}


	public float speedPerSec { get; set; }
	Vector3 oldPosition;
}
