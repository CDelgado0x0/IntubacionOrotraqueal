using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class ConexionOxigeno : MonoBehaviour
{
    [SerializeField] private GameObject Manos;

    private Rigidbody rb;
    private bool keepKinematicActive = false;
    private bool connectedToAmbu = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("OxyGate") || other.CompareTag("Support")))
        {
            keepKinematicActive = true;

            StartCoroutine(ResetDeManos());

            rb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;


            if (other.CompareTag("OxyGate"))
            {
                connectedToAmbu = true;
                transform.SetParent(other.transform);
                GameManager.applicationController.updateGameState(GameState.conectarOxigeno);
            }
        }
    }

    public void grabConnector() //Se llama desde el event
    {
        keepKinematicActive = false;
    }

    public void releaseConnector() //Se llama desde el event
    {
        if (!keepKinematicActive)
        {
            rb.isKinematic = false;
        }
    }

    IEnumerator ResetDeManos()
    {
        Manos.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Manos.SetActive(true);
    }
}
