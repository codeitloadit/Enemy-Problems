#pragma strict

private var acceleration : float = 150.0;

private var lastDistance : float;
private var movingAway : boolean = false;

private var player : Transform;
private var target : Transform;

function Awake() {
    player = GameObject.Find('Player').transform;
    target = transform.Find('Target').transform;

    lastDistance = Vector3.Distance(player.position, transform.position);
}

function Update() {
	var playerVelocity = player.rigidbody.velocity;
	var playerMagnitude = playerVelocity.magnitude;
	var distance = Vector3.Distance(player.position, transform.position);

    var angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
    transform.eulerAngles = Vector3(0, 0, angle);

	if (lastDistance < distance)
		movingAway = true;
	else
		movingAway = false;
	lastDistance = distance;

    target.position = player.position + (playerVelocity.normalized * (playerMagnitude)/2) * (distance/50);
}

function FixedUpdate() {

    var velocity = rigidbody.velocity;
	var magnitude = velocity.magnitude;

	// Apply Dynamic Drag.
	if (movingAway)
		rigidbody.AddForce(velocity.normalized * -magnitude);

    rigidbody.AddForce(transform.right * acceleration);

}
