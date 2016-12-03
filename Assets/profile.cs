using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Facebook.MiniJSON;
using Facebook.Unity;

public class profile : MonoBehaviour {

	public GameObject fbname;
	public GameObject fbava;



	public profile(){
		
	}

	void Start(){
		
		fbname = GameObject.Find ("fbname");
		fbava = GameObject.Find("fbava");

		loadUserName();
		loadProfilePicture();
	}

	public void loadProfilePicture(){
		FB.API( "me/picture?width=128&height=128" , HttpMethod.GET, DealWithProfilePicture);
	}
	public void loadUserName(){
		FB.API ("/me?filed=id,first_name", HttpMethod.GET, DealWithUsername);
	}

	private void DealWithProfilePicture(IGraphResult result){
		if (result.Error != null) 
		{
			Debug.Log ("problem with getting profile");
		}
		Image UserAvatar = fbava.GetComponent<Image> ();
		UserAvatar.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 (0, 0));
		return;
	}
	private void DealWithUsername(IGraphResult result){
		if(result.Error != null)
		{
			Debug.Log ("problem with getting username");
			return;
		}

		Debug.Log( result.RawResult );

		Dictionary<string, object> profile =  Json.Deserialize(result.RawResult) as Dictionary<string,object>;

		string name = profile["name"] as string;


		Text Username = fbname.GetComponent<Text> ();
		Username.text = "Hello, " + name;
	}

}
