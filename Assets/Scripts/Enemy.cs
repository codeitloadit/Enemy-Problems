using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float acceleration;

	private float lastDistance;
	private bool movingAway = false;

	private Transform player;
	private Transform target;

	void Awake () {
		player = GameObject.Find("Player").transform;
		target = transform.Find("Target").transform;

		lastDistance = Vector3.Distance(player.position, transform.position);
	}
	
	void Update () {
		Vector3 playerVelocity = player.GetComponent<Rigidbody>().velocity;
		float playerMagnitude = playerVelocity.magnitude;
		float distance = Vector3.Distance(player.position, transform.position);

		float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0, 0, angle);

		if (lastDistance < distance)
			movingAway = true;
		else
			movingAway = false;
		lastDistance = distance;

		target.position = player.position + (playerVelocity.normalized * (playerMagnitude)/2) * (distance/50);
	}

	void FixedUpdate () {

		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		float magnitude = velocity.magnitude;

		// Apply Dynamic Drag.
		if (movingAway)
			GetComponent<Rigidbody>().AddForce(velocity.normalized * -magnitude);

		GetComponent<Rigidbody>().AddForce(transform.right * acceleration);
	}
}
