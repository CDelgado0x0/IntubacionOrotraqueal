using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class ConexionOxigeno : MonoBehaviour
{
    [SerializeField] private Transform connectedObject;
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject Manos;
    [SerializeField] private GameObject initialPosition;

    private Rigidbody rb;
    private bool keepKinematicActive = false;
    private bool ambuConnected = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = initialPosition.transform.position;
        transform.rotation = initialPosition.transform.rotation;

    }

    private void Update()
    {
        if (ambuConnected)
        {
            ComprobarDistancia();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("OxyGate") || other.CompareTag("Support")))
        {
            if (ambuConnected) return;
            keepKinematicActive = true;

            StartCoroutine(ResetDeManos());

            rb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;


            if (other.CompareTag("OxyGate"))
            {
                ambuConnected = true;
                transform.SetParent(other.transform);
                GameManager.applicationController.updateGameState(GameState.conectarOxigeno);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            if (ambuConnected) DesacoplarCable();

            rb.isKinematic = true;
            transform.position = initialPosition.transform.position;
            transform.rotation = initialPosition.transform.rotation;
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
            ambuConnected = false;
            rb.isKinematic = false;
            transform.SetParent(null);
        }
    }

    public void ComprobarDistancia()
    {
        float distance = Vector3.Distance(transform.position, connectedObject.position);

        if (distance > maxDistance)
        {
            DesacoplarCable();
        }
    }

    private void DesacoplarCable()
    {
        ambuConnected = false;
        rb.isKinematic = false;
        transform.SetParent(null);
        StartCoroutine(ResetDeManos());
    }

    IEnumerator ResetDeManos()
    {
        Manos.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Manos.SetActive(true);
    }
}
