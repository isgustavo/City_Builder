using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Login
{
	public string username;
    public string password;

	public Login(string _username, string _password)
	{
		username = _username;

		byte[] bytes = Encoding.UTF8.GetBytes(_password);
		SHA256 _SHA256 = SHA256.Create();
		byte[] hashpass = _SHA256.ComputeHash(bytes);
		string hashString = string.Empty;
		foreach (byte x in hashpass)
		{
			hashString += String.Format("{0:x2}", x);
		}

		password = hashString;
	}
}

[Serializable]
public class LoginToken
{
	public string token;
}

public class LoginHelper {

    private const string LOGIN_URL = "http://dev.pushstart.com.br/desafio/public/api/auth/login";

    //private bool requestFinished = false;

    public IEnumerator LoginRequest(Login login, Action<bool, LoginToken> OnResult)
    {
        //requestFinished = false;
        string JSON_Body = JsonUtility.ToJson(login);

		var request = new UnityWebRequest(LOGIN_URL, "POST");

		byte[] bodyRaw = new UTF8Encoding().GetBytes(JSON_Body);
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");

		yield return request.Send();
        //requestFinished = true;

        if (request.isHttpError)
        {
            OnResult (false, null);
        }else
        {
            if(request.responseCode == 200)
            {
                string textResult = request.downloadHandler.text;
                LoginToken token = JsonUtility.FromJson<LoginToken>(textResult);
                OnResult(true, token);
            } else 
            {
                OnResult(false, null); 
            }
        }
    }
}
