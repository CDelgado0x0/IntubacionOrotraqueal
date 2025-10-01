using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject spanishMainButtons;
    [SerializeField] private GameObject englishMainButtons;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject spanishInGame;
    [SerializeField] private GameObject englishInGame;

    [SerializeField] private FakeLogin fakeLogin;

    private bool isSpanish = false;

    public void startButton()
    {
        mainMenu.SetActive(false);
        GameManager.applicationController.updateGameState(GameState.encenderLaringoscopio);

        inGameMenu.SetActive(true);
        if (isSpanish)
        {
            spanishInGame.SetActive(true);
        }
        else
        {
            englishInGame.SetActive(true);
        }
    }

    public void changeToSpanish()
    {
        spanishMainButtons.SetActive(true);
        englishMainButtons.SetActive(false);
        GameManager.applicationController.CambiarIdioma(Language.Spanish);
        isSpanish = true;
    }

    public void changeToEnglish()
    {
        spanishMainButtons.SetActive(false);
        englishMainButtons.SetActive(true);
        GameManager.applicationController.CambiarIdioma(Language.English);
        isSpanish = false;
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public void OnStartButtonPressed()
    {
        fakeLogin.OnFakeLoginButtonPressed();
    }

    public void endButton()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }
}
