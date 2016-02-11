using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

	AudioSource audio;

	private float acceleration = 3000.0f;
	private float maxVelocity = 3000.0f;

	void Awake() {
		audio = GetComponent<AudioSource>();
		Time.timeScale = 0.0f;
	}

	void Update() {
		if(Input.GetMouseButton(0) && GetComponent<CircleCollider2D>().enabled) {
			Time.timeScale = 1.0f;
		} else {
			Time.timeScale = 0.0f;
		}
		if(Input.GetMouseButton(1)) {
			SceneManager.LoadScene("Test");
		}
	}

	void FixedUpdate() {
		Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
		float magnitude = velocity.magnitude;

		// Apply Dynamic Drag.
		GetComponent<Rigidbody2D>().AddForce(velocity.normalized * -magnitude * acceleration / 130);

		var direction = Vector2.zero;
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		direction = mousePosition - new Vector2(transform.position.x, transform.position.y);
		if(direction.magnitude > 1.0f) {
			direction.Normalize();
		}

		if(magnitude < maxVelocity) {
			GetComponent<Rigidbody2D>().AddForce(direction * acceleration);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.transform.tag == "Enemy") {
			audio.Play();
			GetComponent<CircleCollider2D>().enabled = false;
			Time.timeScale = 0.0f;
		}
	}
}
