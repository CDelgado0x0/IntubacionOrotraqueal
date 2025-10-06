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
    private bool oxygenAlwaysConnected = false;

    [SerializeField] private AudioSource disconnectSound;
    [SerializeField] private AudioSource connectSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.SetPositionAndRotation(initialPosition.transform.position, initialPosition.transform.rotation);

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
            transform.SetPositionAndRotation(other.transform.position, other.transform.rotation);


            if (other.CompareTag("OxyGate"))
            {
                connectSound.Play();
                oxygenAlwaysConnected = true;
                ambuConnected = true;
                transform.SetParent(other.transform);
                GameManager.applicationController.updateGameState(GameState.acoplarCapnografoYAmbuAlTubo);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            if (ambuConnected) DesacoplarCable();

            rb.isKinematic = true;
            transform.SetPositionAndRotation(initialPosition.transform.position, initialPosition.transform.rotation);
        }
    }

    public void grabConnector() //Se llama desde el event
    {
        disconnectSound.Play();
        keepKinematicActive = false;
    }

    public void releaseConnector() //Se llama desde el event
    {
        if (!keepKinematicActive)
        {
            ambuConnected = false;
            rb.isKinematic = false;
            transform.SetParent(null);

            if (oxygenAlwaysConnected) GameManager.applicationController.updateGameState(GameState.conectarOxigeno);
        }
    }

    public void ComprobarDistancia()
    {
        float distance = Vector3.Distance(transform.position, connectedObject.position);

        Debug.Log("Distance: " + distance);

        if (distance > maxDistance)
        {
            DesacoplarCable();
        }
    }

    private void DesacoplarCable()
    {
        disconnectSound.Play();
        ambuConnected = false;
        rb.isKinematic = false;
        transform.SetParent(null);
        StartCoroutine(ResetDeManos());
        if (oxygenAlwaysConnected) GameManager.applicationController.updateGameState(GameState.conectarOxigeno);
    }

    IEnumerator ResetDeManos()
    {
        Manos.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Manos.SetActive(true);
    }
}
