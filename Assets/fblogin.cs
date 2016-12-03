using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;
using Facebook.MiniJSON;
using UnityEngine.SceneManagement;

public class fblogin : MonoBehaviour {
	void Awake(){
		FB.Init(SetInit, onHideUnity);
	}
	private void SetInit(){
		if (FB.IsInitialized) {
			FB.ActivateApp ();
		} else {
			Debug.Log ("Failed to Initialized");
		}
	}
	private void onHideUnity(bool isGameShown){
		if (!isGameShown)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}

	}
	public void FBlogin(){
		var perms = new List<string>() { "public_profile", "email", "user_friends" };
		FB.LogInWithReadPermissions(perms, AuthCallback);

	}
	public void AuthCallback(ILoginResult result){
		if (FB.IsLoggedIn){
			LoadStage ();
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			Debug.Log(aToken.UserId);
			foreach (string perm in aToken.Permissions){
				Debug.Log(perm);
			}
		}
		else{
			Debug.Log("User cancelled login");
		}

	}
	public void LoadStage()  {
		
		SceneManager.LoadScene ("playgame");
		//Application.LoadLevel ("playgame");
	}
	public void FBlogout(){
		FB.LogOut ();
		SceneManager.LoadScene ("main");
		//Application.LoadLevel ("main");
	}
}
