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
    public string nickname;
	public int money;

	public Player(string _nickname, int _money)
	{
        nickname = _nickname;
        money = _money;
	}
}

public class PlayerHelper {

    private const string Load_Player_URL = "http://dev.pushstart.com.br/desafio/public/api/status";

    //private bool requestFinished = false;

    public IEnumerator LoadRequest(LoginToken token, Action<bool, Player> OnResult)
    {
        //requestFinished = false;
		UnityWebRequest request = UnityWebRequest.Get(Load_Player_URL);
		request.SetRequestHeader("X-Authorization", token.token);

		yield return request.Send();
        //requestFinished = true;

		if (request.isHttpError)
		{
			OnResult(false, null);
		}
		else
		{
			if (request.responseCode == 200)
			{
				string textResult = request.downloadHandler.text;
                Player player = JsonUtility.FromJson<Player>(textResult);
				OnResult(true, player);
			}
			else
			{
				OnResult(false, null);
			}
		}

    }

	
}
