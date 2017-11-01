using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBehaviour : MonoBehaviour {

	void Start () {
		GameManagerBehaviour.instancie.GameStateAction += GameStateChanged;
	}

    public abstract void GameStateChanged(GameState gameState);
}
