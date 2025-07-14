using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class LaryngoscopeConexion : MonoBehaviour
{
    private Collider myCollider;
    private FixedJoint fixedJoint;
    private Transform manosGancho;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gancho"))
        {
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

            /*
            Debug.Log("LaryngoscopeConexion: Gancho detected");
            Transform manosGancho = other.transform.Find("HandGrab");
            Rigidbody otherRigidbody = other.attachedRigidbody;

            if (manosGancho == null || otherRigidbody == null) return;

            manosGancho.gameObject.SetActive(false);
            myCollider.enabled = false;

            //otherRigidbody.UnlockKinematic();
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;
            otherRigidbody.isKinematic = true;
            //otherRigidbody.LockKinematic();
            */

        }
    }

    IEnumerator EsperarUnSegundo()
    {
        yield return new WaitForSeconds(1f);
        if (manosGancho != null) manosGancho.gameObject.SetActive(true);
    }
}
