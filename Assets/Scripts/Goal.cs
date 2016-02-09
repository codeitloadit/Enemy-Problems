using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private Transform player;

	void Awake () {
		player = GameObject.Find("Player").transform;
	}
	
	void Update () {
		Transform enemyA = GameObject.Find("EnemyA").transform;
		Transform enemyB = GameObject.Find("EnemyB").transform;

		Vector3 posA = new Vector3(enemyA.position.x, enemyA.position.y, 10);
		Vector3 posB = new Vector3(enemyB.position.x, enemyB.position.y, 10);

		float distance = Vector3.Distance(posA, posB);

		transform.localScale = new Vector3(distance, 1, 1);

		float angle = Mathf.Atan2(posA.y - posB.y, posA.x - posB.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0, 0, angle);


		float x = posB.x + distance / 2 * Mathf.Cos(angle * Mathf.Deg2Rad);
		float y = posB.y + distance / 2 * Mathf.Sin(angle * Mathf.Deg2Rad);

		transform.position = new Vector3(x, y, 0);
	}

	void OnTriggerEnter (Collider other) {
		if (other.name == "Player") {
			TextMesh textMesh = player.Find("Count").GetComponent<TextMesh>();
			textMesh.text = (int.Parse(textMesh.text) + 1).ToString();
		}
	}
}
