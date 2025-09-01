using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class LaryngoscopeConexion : MonoBehaviour
{
    private Collider myCollider;
    private Transform manosGancho;
    private Transform nuevosCollidersGancho;

    [SerializeField] private GameObject laryngoscopeCameraCover;
    [SerializeField] private Collider detectorCollider;

    private void ComprobarActivacionLaringoscopio(GameState state)
    {
        if (state == GameState.encenderLaringoscopio)
        {
            detectorCollider.enabled = true;
        }
        else
        {
            detectorCollider.enabled = false;
        }
    }

    void Start()
    {
        GameManager.onGameStateChanged += ComprobarActivacionLaringoscopio;
        myCollider = GetComponent<Collider>();
        nuevosCollidersGancho = transform.Find("GanchoColliders");
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= ComprobarActivacionLaringoscopio;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gancho"))
        {
            /*
            manosGancho = other.transform.Find("HandGrab");
            Rigidbody otherRigidbody = other.attachedRigidbody;

            if (manosGancho == null || otherRigidbody == null) return;

            myCollider.enabled = false;
            manosGancho.gameObject.SetActive(false);
            StartCoroutine(EsperarUnSegundo());

            otherRigidbody.angularVelocity = Vector3.zero;
            otherRigidbody.linearVelocity = Vector3.zero;

            otherRigidbody.isKinematic = true;
            otherRigidbody.transform.position = transform.position;
            otherRigidbody.transform.rotation = transform.rotation;
            otherRigidbody.isKinematic = false;

            fixedJoint = other.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = GetComponent<Rigidbody>();

            */

            manosGancho = other.transform.Find("HandGrab");
            Transform ganchoColliders = other.transform.Find("Colliders");
            Transform laryngoscopeLight = other.transform.Find("Light");
            Rigidbody otherRigidbody = other.attachedRigidbody;

            if (manosGancho == null || otherRigidbody == null || ganchoColliders == null) return;

            myCollider.enabled = false;
            manosGancho.gameObject.SetActive(false);
            ganchoColliders.gameObject.SetActive(false);
            nuevosCollidersGancho.gameObject.SetActive(true);


            //otherRigidbody.UnlockKinematic();
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;
            otherRigidbody.isKinematic = true;
            //otherRigidbody.LockKinematic();

            laryngoscopeCameraCover.SetActive(false);
            laryngoscopeLight.gameObject.SetActive(true);

            GameManager.applicationController.updateGameState(GameState.inflarBalonTuboOrotraqueal);
        }
    }
}
