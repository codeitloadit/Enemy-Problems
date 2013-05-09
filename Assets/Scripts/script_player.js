#pragma strict

private var acceleration : float = 3000.0;
private var maxVelocity : float = 3000.0;

function Awake() {
	Time.timeScale = 0.0;
}

function Update() {
	if (Input.anyKeyDown) {
		if (transform.position == Vector3.zero)
			Time.timeScale = 1.0;

		if (Time.timeScale == 0.0)
	        Application.LoadLevel('Test'); 
	}
}

function FixedUpdate() {

    var velocity = rigidbody.velocity;
	var magnitude = velocity.magnitude;

	// Apply Dynamic Drag.
	rigidbody.AddForce(velocity.normalized * -magnitude * acceleration/100);

	var direction = Vector3.zero;
	if (Input.GetKey('w')) 
		direction.y = 1;
	if (Input.GetKey('s')) 
		direction.y = -1;
	if (Input.GetKey('a')) 
		direction.x = -1;
	if (Input.GetKey('d')) 
		direction.x = 1;
	direction.Normalize();

	if (magnitude < maxVelocity)
		rigidbody.AddForce(direction * acceleration);

    // gameObject.Find('Target').transform.position = transform.position + velocity.normalized * magnitude/1.5;

}

function OnCollisionEnter(other : Collision) {
	if (other.transform.tag == 'Enemy')
		Time.timeScale = 0.0;
}
