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
    public Action<int> PlayerMoneyAction;
    public Action<bool> PauseAction;

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
            player = new Player("Gustav", 400);
            GameStateChanged(GameState.Game);
            //GameStateChanged(GameState.Login);
        }
	}

    public void GameStateChanged (GameState state)
    {
        gameState = state;

		if (PauseAction != null)
		{
            PauseAction(gameState == GameState.Pause ? true : false);
		}

        if (GameStateAction != null) 
        {
            GameStateAction (gameState);
        }
    }

    public void RemoveMoney (int price)
    {
		player.money -= price;

		if (PlayerMoneyAction != null)
		{
			PlayerMoneyAction(player.money);
		}
    }

    public void AddMoney (int profit)
    {
        player.money += profit;

        if(PlayerMoneyAction != null)
        {
            PlayerMoneyAction(player.money);
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
						if (PlayerMoneyAction != null)
						{
							PlayerMoneyAction(player.money);
						}
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
