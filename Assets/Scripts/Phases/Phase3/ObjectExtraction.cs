using UnityEngine;

public class ObjectExtraction : MonoBehaviour
{
    [SerializeField] private GameObject canulaHands;
    [SerializeField] private Rigidbody canulaRb;

    private Rigidbody myRb;
    private bool activeScript = false;

    private void ExtraerCanulaYAmbu(GameState state)
    {
        if (state == GameState.extraerAmbuYCanula)
        {
            activeScript = true;
        }
        else
        {
            activeScript = false;
        }
    }

    void Start()
    {
        GameManager.onGameStateChanged += ExtraerCanulaYAmbu;
        myRb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= ExtraerCanulaYAmbu;
    }

    public void AmbuGrab()
    {
        if (activeScript)
        {
            canulaHands.SetActive(true);
        }
    }

    public void AmbuRelease()
    {
        if (activeScript)
        {
            myRb.isKinematic = false;
        }
    }

    public void CanulaRelease()
    {
        if (activeScript)
        {
            canulaRb.isKinematic = false;
            GameManager.applicationController.updateGameState(GameState.introducirLaringoscopio);
        }
    }
}
