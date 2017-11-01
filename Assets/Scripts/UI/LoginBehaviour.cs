using UnityEngine;
using UnityEngine.UI;

public class LoginBehaviour : UIBehaviour {

    [SerializeField]
    private InputField usernameField;
    [SerializeField]
    private InputField passwordField;
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject errorMessage;

	public override void GameStateChanged(GameState gameState)
	{
        switch (gameState)
        {
            case GameState.Login:
                usernameField.gameObject.SetActive(true);
                passwordField.gameObject.SetActive(true);
                button.SetActive(true);
                errorMessage.SetActive(false);
                break;
            case GameState.LoginError:
                errorMessage.SetActive(true);
                break;
            case GameState.Game:
				usernameField.gameObject.SetActive(false);
				passwordField.gameObject.SetActive(false);
                button.SetActive(false);
				errorMessage.SetActive(false);
                break;
        }
    }

    public void LoginButtonClicked ()
    {
        Login login = new Login(usernameField.text, passwordField.text);
        GameManagerBehaviour.instancie.TryLogIn(login);
    }
}
