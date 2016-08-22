# UnityGooglePlayGamesSample
This sample shows how to use GooglePlayGames service in Unity3D.

This code implements GooglePlayGames Leaderboard for android and GameCenter Leaderboard for iOS.

	//import GPG packages provided by google.
	
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif


	//if Android set GooglePlayGames as Social log in.
	
#if UNITY_ANDROID
GooglePlayGames.PlayGamesPlatform.Activate ();
#endif


	//This code logs user to social
	
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

	//Show leaderboard and Upload Scores
	
public void OnLeaderboard() {
	if (mConnected) {
		Social.ShowLeaderboardUI ();
	}
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