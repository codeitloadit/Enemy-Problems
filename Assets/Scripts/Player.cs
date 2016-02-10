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
		Vector2 velocity = GetComponent<Rigidbody2D> ().velocity;
		float magnitude = velocity.magnitude;

		// Apply Dynamic Drag.
		GetComponent<Rigidbody2D>().AddForce(velocity.normalized * -magnitude * acceleration/100);

		var direction = Vector2.zero;
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
			GetComponent<Rigidbody2D>().AddForce(direction * acceleration);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.tag == "Enemy")
			Time.timeScale = 0.0f;
	}
}
