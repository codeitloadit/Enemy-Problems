using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AdsRevMob : MonoBehaviour {
	private static readonly Dictionary<String, String> REVMOB_APP_IDS = new Dictionary<String, String>() {
		{ "Android", "56bc43f91c64102a6106ac10" },
		{ "IOS", "copy your iOS RevMob Media ID here" }
	};
	private RevMob revmob;

	void Awake() {
		revmob = RevMob.Start(REVMOB_APP_IDS, "AdsRevMob");
	}

	void Start() {
		#if UNITY_ANDROID || UNITY_IPHONE
		RevMobBanner banner = revmob.CreateBanner(RevMob.Position.BOTTOM, 0, 0, Screen.width, (int)Screen.width / 7);
		banner.Show();
		#endif
	}
}