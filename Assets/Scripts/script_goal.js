#pragma strict

private var player : Transform;

function Awake() {
    player = GameObject.Find('Player').transform;
}

function Update() {
	// var lineRenderer : LineRenderer = transform.GetComponent(LineRenderer);
	var enemyA : Transform = GameObject.Find('EnemyA').transform;
	var enemyB : Transform = GameObject.Find('EnemyB').transform;

	var posA : Vector3 = Vector3(enemyA.position.x, enemyA.position.y, 10);
	var posB : Vector3 = Vector3(enemyB.position.x, enemyB.position.y, 10);

	var distance : float = Vector3.Distance(posA, posB);

	transform.localScale = Vector3(distance, 1, 1);

    var angle = Mathf.Atan2(posA.y - posB.y, posA.x - posB.x) * Mathf.Rad2Deg;
    transform.eulerAngles = Vector3(0, 0, angle);


    var x = posB.x + distance / 2 * Mathf.Cos(angle * Mathf.Deg2Rad);
    var y = posB.y + distance / 2 * Mathf.Sin(angle * Mathf.Deg2Rad);

    transform.position = Vector3(x, y, 10);

	// lineRenderer.SetPosition(0, posA);
	// lineRenderer.SetPosition(1, posB);

}

function OnTriggerEnter (other : Collider) {
    if (other.name == 'Player') {
    	transform.position = Vector3(Random.Range(-80.0, 80.0), Random.Range(-60.0, 60.0), transform.position.z);
    	while (Vector3.Distance(player.position, transform.position) < 60) 
	    	transform.position = Vector3(Random.Range(-80.0, 80.0), Random.Range(-60.0, 60.0), transform.position.z);

	    var textMesh : TextMesh = transform.Find('Count').GetComponent(TextMesh);
	    textMesh.text = (parseInt(textMesh.text) + 1).ToString();
    }
}