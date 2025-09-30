using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject spanishMainButtons;
    [SerializeField] private GameObject englishMainButtons;
    [SerializeField] private GameObject mainMenu;

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
}
