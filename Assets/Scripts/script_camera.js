#pragma strict

private var player : Transform;

function Awake() {
    player = GameObject.Find('Player').transform;
}

function Update () {
	transform.position = Vector3(player.position.x, player.position.y, transform.position.z);

}