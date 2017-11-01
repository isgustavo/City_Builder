using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Player
{
	public string name;
	public int coins;

	public Player(string _name, int _coins)
	{
        name = _name;
        coins = _coins;
	}
}

public class PlayerHelper {

    private const string Load_Player_URL = "http://dev.pushstart.com.br/desafio/public/api/status";

    private bool requestFinished = false;

    public IEnumerator LoadRequest(LoginToken token, Action<bool, Player> OnResult)
    {
        requestFinished = false;
		UnityWebRequest request = UnityWebRequest.Get(Load_Player_URL);
		request.SetRequestHeader("X-Authorization", token.token);

		yield return request.Send();
        requestFinished = true;

		if (request.isHttpError)
		{
			Debug.Log("Error: " + request.responseCode);
			OnResult(false, null);
		}
		else
		{
			if (request.responseCode == 200)
			{
				Debug.Log("Result: " + request.responseCode);
				string textResult = request.downloadHandler.text;
                Player player = JsonUtility.FromJson<Player>(textResult);
				OnResult(true, player);
			}
			else
			{
				Debug.Log("Error: " + request.responseCode);
				OnResult(false, null);
			}
		}

    }

	
}
