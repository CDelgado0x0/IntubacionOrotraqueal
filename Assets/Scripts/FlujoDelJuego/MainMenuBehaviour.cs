using UnityEngine;
using System.Collections;

public class MainMenuBehaviour : MonoBehaviour
{
    public FakeLogin fakeLogin; // Asigna en el inspector

    public void OnStartButtonPressed()
    {
        StartCoroutine(WaitAndStartGame(3f));
    }

    private IEnumerator WaitAndStartGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (fakeLogin != null && fakeLogin.LoginExitoso)
        {
            // Inicializa la partida despues de esperar 3 segundos
            GameManager.applicationController.updateGameState(GameState.encenderLaringoscopio);
            Debug.Log("Partida iniciada.");

        }
        else
        {
            // Opcional: muestra mensaje de error
            Debug.LogWarning("Debes iniciar sesión antes de comenzar la partida.");

        }
    }
}
