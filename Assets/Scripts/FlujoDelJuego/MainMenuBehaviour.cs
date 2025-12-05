using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject spanishMainButtons;
    [SerializeField] private GameObject englishMainButtons;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject logInMenu;
    [SerializeField] private FakeLogin fakeLogin;
    [SerializeField] private SliderSync ambientSlider;
    [SerializeField] private SliderSync effectsSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Lenguage"))
        {
            string lang = PlayerPrefs.GetString("Lenguage");
            if (lang == "English")
            {
                changeToEnglish();
            }
            else
            {
                changeToSpanish();
            }
        }
    }

    public void startButton()
    {
        mainMenu.SetActive(false);
        GameManager.applicationController.updateGameState(GameState.encenderLaringoscopio);
        GameManager.applicationController.myDatabase.LogAction("Inicio de la simulación", true, "");
    }

    public void changeToSpanish()
    {
        PlayerPrefs.SetString("Lenguage", "Spanish");
        PlayerPrefs.Save();
        spanishMainButtons.SetActive(true);
        englishMainButtons.SetActive(false);
        GameManager.applicationController.CambiarIdioma(Language.Spanish);
    }

    public void changeToEnglish()
    {
        PlayerPrefs.SetString("Lenguage", "English");
        PlayerPrefs.Save();
        spanishMainButtons.SetActive(false);
        englishMainButtons.SetActive(true);
        GameManager.applicationController.CambiarIdioma(Language.English);
    }

    public void quitButton()
    {
        PlayerPrefs.DeleteKey("user_uid");
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void OnStartButtonPressed()
    {
        fakeLogin.OnFakeLoginButtonPressed();
    }

    public void restartButton()
    {
        GameManager.applicationController.myDatabase.LogAction("La simulación ha sido reiniciada", true, "Tiempo de simulación: " + GameManager.applicationController.timerText);
        reloadScene();
    }

    public void endButton()
    {
        GameManager.applicationController.myDatabase.LogAction("La simulación ha sido completada con éxito", true, "Tiempo de simulación: " + GameManager.applicationController.timerText);
        reloadScene();
    }

    private void reloadScene()
    {
        ambientSlider.SaveSliderValue();
        effectsSlider.SaveSliderValue();
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void logOutButton()
    {
        mainMenu.SetActive(false);
        PlayerPrefs.DeleteKey("user_uid");
        PlayerPrefs.DeleteKey("user_al");
        PlayerPrefs.Save();
        logInMenu.SetActive(true);

    }
}
