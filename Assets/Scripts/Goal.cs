using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	private Transform player;
	private Transform score;
	private Transform count;

	AudioSource audio;

	private float distance;

	void Awake() {
		player = GameObject.Find("Player").transform;
		score = GameObject.Find("Score").transform;
		count = GameObject.Find("Count").transform;
		audio = GetComponent<AudioSource>();
	}

	void Update() {
		Transform enemyA = GameObject.Find("EnemyA").transform;
		Transform enemyB = GameObject.Find("EnemyB").transform;

		Vector3 posA = new Vector2(enemyA.position.x, enemyA.position.y);
		Vector3 posB = new Vector2(enemyB.position.x, enemyB.position.y);

		distance = Vector2.Distance(posA, posB);

		transform.localScale = new Vector3(distance, 1, 1);

		float angle = Mathf.Atan2(posA.y - posB.y, posA.x - posB.x) * Mathf.Rad2Deg;
		transform.eulerAngles = new Vector3(0, 0, angle);


		float x = posB.x + distance / 2 * Mathf.Cos(angle * Mathf.Deg2Rad);
		float y = posB.y + distance / 2 * Mathf.Sin(angle * Mathf.Deg2Rad);

		transform.position = new Vector3(x, y, 0);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "Player") {
			audio.Play();
			TextMesh scoreTextMesh = score.GetComponent<TextMesh>();
			scoreTextMesh.text = (int.Parse(scoreTextMesh.text) + 1000 - (int)distance * 4).ToString().PadLeft(8, '0');
			TextMesh countTextMesh = count.GetComponent<TextMesh>();
			countTextMesh.text = (int.Parse(countTextMesh.text) + 1).ToString();
		}
	}
}
