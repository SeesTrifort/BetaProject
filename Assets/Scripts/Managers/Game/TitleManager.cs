using UnityEngine;
using System.Collections;

public class TitleManager : GameManager 
{
	public override void Initialized ()
	{
		Debug.Log("Initialized");

		Social.localUser.Authenticate(AuthenticateCallBack);
	}

	void AuthenticateCallBack (bool success) 
	{
		Debug.Log("AuthenticateCallBack " + success);
	}
}
