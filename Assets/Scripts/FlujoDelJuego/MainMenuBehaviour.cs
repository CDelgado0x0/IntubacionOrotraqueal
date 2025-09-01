using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    public void startButton()
    {
        GameManager.applicationController.updateGameState(GameState.encenderLaringoscopio);
    }
}
