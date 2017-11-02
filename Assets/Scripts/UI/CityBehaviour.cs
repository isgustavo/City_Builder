using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBehaviour : UIBehaviour {

    [SerializeField]
    private GameObject cityObject;

	public override void GameStateChanged(GameState gameState)
	{
		switch (gameState)
		{
			case GameState.Login:
			case GameState.LoginError:
                cityObject.SetActive(false);
				break;
			case GameState.Game:

                cityObject.SetActive(true);
				break;
		}
	}


}
