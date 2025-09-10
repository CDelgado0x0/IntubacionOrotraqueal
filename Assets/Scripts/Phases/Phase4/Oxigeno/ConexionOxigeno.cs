using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class ConexionOxigeno : MonoBehaviour
{
    [SerializeField] private GameObject Manos;

    private Rigidbody rb;
    private FixedJoint fixedJoint;
    private bool keepKinematicActive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("OxyGate") || other.CompareTag("Support")))
        {

            keepKinematicActive = true;

            if (fixedJoint != null) Destroy(fixedJoint);

            StartCoroutine(ResetDeManos());

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

            if (other.CompareTag("OxyGate"))
            {
                GameManager.applicationController.updateGameState(GameState.conectarOxigeno);
            }
        }
    }

    public void grabConnector() //Se llama desde el event
    {
        if (fixedJoint != null)
        {
            Destroy(fixedJoint);
        }
    }

    IEnumerator ResetDeManos()
    {
        Manos.SetActive(false);
        yield return new WaitForSeconds(1f);
        Manos.SetActive(true);
    }
}
