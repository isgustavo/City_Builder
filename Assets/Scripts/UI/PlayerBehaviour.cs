using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : UIBehaviour {

	[SerializeField]
	private Text nicknamePlayerText;
    [SerializeField]
    private GameObject moneyPanel;
	[SerializeField]
	private Text moneyText;

	public override void GameStateChanged(GameState gameState)
	{
		switch (gameState)
		{
			case GameState.Login:
			case GameState.LoginError:
				nicknamePlayerText.gameObject.SetActive(false);
                moneyPanel.SetActive(false);
				break;
			case GameState.Game:
				nicknamePlayerText.gameObject.SetActive(true);
                nicknamePlayerText.text = GameManagerBehaviour.instancie.player.nickname;
				moneyPanel.SetActive(true);
                moneyText.text = GameManagerBehaviour.instancie.player.money.ToString();
				break;
		}
	}
}
