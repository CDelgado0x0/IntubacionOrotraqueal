using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class LaryngoscopeConexion : MonoBehaviour
{
    private Collider myCollider;
    private Transform manosGancho;
    private Transform nuevosCollidersGancho;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<Collider>();
        nuevosCollidersGancho = transform.Find("GanchoColliders");
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

        }
    }
}
