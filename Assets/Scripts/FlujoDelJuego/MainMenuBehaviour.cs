using UnityEngine;
using System.Collections;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject spanishMainButtons;
    [SerializeField] private GameObject englishMainButtons;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private FakeLogin fakeLogin;

    public void startButton()
    {
        mainMenu.SetActive(false);
        GameManager.applicationController.updateGameState(GameState.encenderLaringoscopio);
    }

    public void changeToSpanish()
    {
        spanishMainButtons.SetActive(true);
        englishMainButtons.SetActive(false);
        GameManager.applicationController.CambiarIdioma(Language.Spanish);
    }

    public void changeToEnglish()
    {
        spanishMainButtons.SetActive(false);
        englishMainButtons.SetActive(true);
        GameManager.applicationController.CambiarIdioma(Language.English);
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public void OnStartButtonPressed()
    {
        StartCoroutine(StartGameAfterLogin());
    }

    private IEnumerator StartGameAfterLogin()
    {
        fakeLogin.OnFakeLoginButtonPressed();

        yield return new WaitForSeconds(2f);

        if (fakeLogin.LoginExitoso) GameManager.applicationController.updateGameState(GameState.encenderLaringoscopio);
    }
}
