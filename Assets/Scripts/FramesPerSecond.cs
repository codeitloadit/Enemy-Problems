using UnityEngine;
using System.Collections;

public class FramesPerSecond : MonoBehaviour {

	public float updateInterval = 0.5f;

	private float accum = 0.0f;
	private int frames = 0;
	private float timeleft;

	private float low = 60.0f;
	private float high = 60.0f;
	private float lastLow = 60.0f;
	private float lastHigh = 60.0f;

	private float totalEllapsed = 0;

	private int lowCount = 0;
	private int highCount = 0;

	void Start () {
		if (!GetComponent<GUIText>()) {
			Debug.Log("FramesPerSecond needs a GUIText component!");
			enabled = false;
			return;
		}
		timeleft = updateInterval;
	}

	void Update () {
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;

		if (timeleft <= 0.0) {
			var fps = accum/frames;
			if (totalEllapsed > 0.5) {
				if (fps < low) {
					low = fps;
				} else if (fps > high) {
					high = fps;
				}

				if (low == lastLow) {
					lowCount += 1;
				}
				if (high == lastHigh) {
					highCount += 1;
				}

				if (lowCount >= 50) {
					lowCount = 0;
					low = 60;
				}
				if (highCount >= 50) {
					highCount = 0;
					high = 60;
				}
				lastLow = low;
				lastHigh = high;
			} else {
				totalEllapsed += Time.deltaTime;
			}
			GetComponent<GUIText>().text = "" + (fps).ToString("n0") + "\n" + (low).ToString("n0") + "\n" + (high).ToString("n0");
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0;
		}
	}
}