using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float acceleration = 3000.0f;
	private float maxVelocity = 3000.0f;

	void Awake () {
		Time.timeScale = 0.0f;
	}
	
	void Update () {
		if (Input.anyKeyDown) {
			if (transform.position == Vector3.zero)
				Time.timeScale = 1.0f;

			if (Time.timeScale == 0.0f)
				Application.LoadLevel("Test"); 
		}
	}

	void FixedUpdate () {
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float magnitude = velocity.magnitude;

		// Apply Dynamic Drag.
		GetComponent<Rigidbody>().AddForce(velocity.normalized * -magnitude * acceleration/100);

		var direction = Vector3.zero;
		if (Input.GetKey("w")) 
			direction.y = 1;
		if (Input.GetKey("s")) 
			direction.y = -1;
		if (Input.GetKey("a")) 
			direction.x = -1;
		if (Input.GetKey("d")) 
			direction.x = 1;
		direction.Normalize();

		if (magnitude < maxVelocity)
			GetComponent<Rigidbody>().AddForce(direction * acceleration);
	}

	void OnCollisionEnter(Collision other) {
		if (other.transform.tag == "Enemy")
			Time.timeScale = 0.0f;
	}
}
