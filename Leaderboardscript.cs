using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
using UnityEngine.SocialPlatforms;

public class Leaderboardscript : MonoBehaviour {

	public static Leaderboardscript mInstance;
	public static bool mConnected;

	public static Text dOUT;

	// Use this for initialization
	void Awake (){
		mInstance = this;

		#if UNITY_ANDROID
		GooglePlayGames.PlayGamesPlatform.Activate ();
		#endif

		if (!Social.localUser.authenticated){
			DOUT ("Not logged in, so try to log in");
			Social.localUser.Authenticate ((bool success) => {
				mConnected = success;
				if (success) {
					Social.CreateLeaderboard();
					Social.CreateLeaderboard().id = "grp.com.whitewhale.abakus.leaderboard";

					DOUT ("Ok! logged in!" + Social.localUser.userName);
				} else {
					DOUT ("Sorry failed to log in.");
				}
			});
		} else {
			DOUT ("Already logged in." + Social.localUser.userName);
		}
	}

	void Start () {
		if (GameObject.Find ("dOut") == null)
			return;
		dOUT = GameObject.Find ("dOut").GetComponent<Text>();
		DOUT ("I love debugging");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnLeaderboard() {
		if (mConnected) {
			Social.ShowLeaderboardUI ();
		}
	}

	public static void DOUT (string str){
		if (dOUT != null)
			dOUT.text = dOUT.text + "\n" + str;
	}


	public void UploadMyScore(long score){
		if (mConnected) {
			#if UNITY_IOS || UNITY_IPHONE
			Social.ReportScore (score, "grp.com.whitewhale.abakus.leaderboard", (bool success) => {
				if (success)
					DOUT("Nice one!");
				else 
					DOUT("NoNO! failed");
			});
			#else
			Social.ReportScore (score, AbakusLeaderboard.leaderboard_abakusleaderboard, (bool success) => {
				if (success)
					DOUT("Nice one!");
				else 
					DOUT("NoNO! failed");
			});
			#endif
		}
	}
}
