using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class ConexionOxigeno : MonoBehaviour
{
    private Grabbable myGrab;
    private Rigidbody rb;
    private FixedJoint fixedJoint;
    private bool done = false;

    private bool ICanEnter = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        myGrab = GetComponent<Grabbable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("ConectorCateter") || other.CompareTag("Gancho")) && ICanEnter)
        {

            if (fixedJoint != null) Destroy(fixedJoint);

            myGrab.enabled = false;
            StartCoroutine(EsperarUnSegundo());

            ICanEnter = false;

            // Opcional: bloquear rotación si quieres que no gire más
            rb.angularVelocity = Vector3.zero;
            rb.linearVelocity = Vector3.zero;

            rb.isKinematic = true;
            //Ubicarlo en la posicion del cateter
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            rb.isKinematic = false;


            // Crear joint fijo entre este objeto y el conector
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = other.attachedRigidbody;

            done = false;

            if (other.CompareTag("Gancho")) return;

            GameManager.applicationController.updateGameState(GameState.conectarOxigeno);
        }
    }

    public void isMoving()
    {
        if (fixedJoint != null)
        {
            Destroy(fixedJoint);
            ICanEnter = true;

        }
    }

    IEnumerator EsperarUnSegundo()
    {
        yield return new WaitForSeconds(1f);
        myGrab.enabled = true;
    }
}
