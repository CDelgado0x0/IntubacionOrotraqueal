using Oculus.Interaction;
using UnityEngine;

public class MaskBehaviour : MonoBehaviour
{
    private bool scriptActive = false;
    private Rigidbody myRb;

    [SerializeField] private Grabbable myGrab;
    [SerializeField] private GrabFreeTransformer FreeGrab;

    public BoxCollider myCollider;
    public GameObject parentCollider;

    private void DesacoploMascarilla(GameState state)
    {
        if (state == GameState.desacoplarMascarilla)
        {
            scriptActive = true;
        }
        else
        {
            scriptActive = false;
        }
    }

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        FreeGrab?.Initialize(myGrab);
        GameManager.onGameStateChanged += DesacoploMascarilla;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= DesacoploMascarilla;
    }

    public void disconnectMask()
    {
        if (scriptActive)
        {
            transform.parent = null;
            myGrab.InjectOptionalOneGrabTransformer(FreeGrab);
        }
    }

    public void onMaskRelease()
    {
        if (scriptActive)
        {
            parentCollider.SetActive(false);
            myCollider.isTrigger = false;
            myRb.isKinematic = false;
            GameManager.applicationController.updateGameState(GameState.conectarOxigeno);
        }
    }
}
