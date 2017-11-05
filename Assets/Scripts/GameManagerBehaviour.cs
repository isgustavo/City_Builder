using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Login,
    LoginError,
    Game,
    Pause
}

public class GameManagerBehaviour : MonoBehaviour {

    public static GameManagerBehaviour instancie;
    public Action<GameState> GameStateAction;

    public Player player
    {
        get;
        private set;
    }

    private GameState gameState;

    void Awake ()
    {
        if(instancie == null)
        {
            instancie = this;
            DontDestroyOnLoad(instancie);
        } else
        {
            Destroy(instancie);
        }
    }

	void Start () {

        if (player == null)
        {
            GameStateChanged(GameState.Game);
        }
	}

    public void GameStateChanged (GameState state)
    {
        gameState = state;

        if (GameStateAction != null) 
        {
            GameStateAction (gameState);
        }
    }

    public void TryLogIn (Login login)
    {
        LoginHelper helper = new LoginHelper();
        StartCoroutine(helper.LoginRequest(login, (loginResult, token) => {
            
            if (loginResult)
            {
                StartCoroutine(new PlayerHelper().LoadRequest(token, (loadResult, _player) => {

                    if (loadResult)
                    {
                        player = _player;
                        GameStateChanged(GameState.Game);
                    } else 
                    {
                        GameStateChanged(GameState.LoginError);
                    }
                }));
            } else
            {
                GameStateChanged(GameState.LoginError);
            }
        }));
    }
}
