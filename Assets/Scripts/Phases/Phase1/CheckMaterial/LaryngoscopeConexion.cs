using Oculus.Interaction;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LaryngoscopeConexion : MonoBehaviour
{
    private Collider myCollider;
    private Transform manosGancho;
    private Transform nuevosCollidersGancho;
    private bool activeScript;

    [SerializeField] private GameObject laryngoscopeCameraCover;
    [SerializeField] private GameObject completeLaryngoscopeVisual;
    [SerializeField] private GameObject completeLaryngoscopeHands;
    [SerializeField] private GameObject firstPhaseCanvas;
    [SerializeField] private GameObject secondPhaseCanvas;
    [SerializeField] private Collider ganchoCollider;

    private void ComprobarActivacionLaringoscopio(GameState state)
    {
        if (state == GameState.encenderLaringoscopio || state == GameState.introducirLaringoscopio)
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
        GameManager.onGameStateChanged += ComprobarActivacionLaringoscopio;
        myCollider = GetComponent<Collider>();
        nuevosCollidersGancho = transform.Find("GanchoColliders");
        completeLaryngoscopeVisual.SetActive(false);
        completeLaryngoscopeHands.SetActive(false);
        firstPhaseCanvas.SetActive(true);
        secondPhaseCanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= ComprobarActivacionLaringoscopio;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!activeScript) return;

        if (other.CompareTag("Gancho"))
        {
            manosGancho = other.transform.Find("HandGrab");
            Transform ganchoColliders = other.transform.Find("Colliders");
            Transform laryngoscopeLight = other.transform.Find("Light");
            Rigidbody otherRigidbody = other.attachedRigidbody;

            if (manosGancho == null || otherRigidbody == null || ganchoColliders == null) return;

            ganchoCollider.enabled = false;
            myCollider.enabled = false;
            manosGancho.gameObject.SetActive(false);
            ganchoColliders.gameObject.SetActive(false);
            nuevosCollidersGancho.gameObject.SetActive(true);


            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;
            otherRigidbody.isKinematic = true;

            laryngoscopeCameraCover.SetActive(false);
            laryngoscopeLight.gameObject.SetActive(true);

            GameManager.applicationController.updateGameState(GameState.inflarBalonTuboOrotraqueal);
        }
        else if (other.CompareTag("LaryngoscopeDetectable"))
        {
            completeLaryngoscopeVisual.SetActive(true);
            completeLaryngoscopeHands.SetActive(true);
            firstPhaseCanvas.SetActive(false);
            secondPhaseCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    
}