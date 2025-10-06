using System.Collections;
using UnityEngine;

public class NuevaConexionJeringaTubo : MonoBehaviour
{
    [SerializeField] private Transform connectedObject;
    [SerializeField] private Transform centralPoint; //Punto desde el cual se mide la distancia
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject Manos;
    [SerializeField] private SyringeController syringeBehaviour;

    private Rigidbody rb;
    private bool keepKinematicActive = false;
    private bool syringeConnected = false;
    private Transform myRealParent;
    private bool canTrigger = true; // Evita múltiples triggers rápidos al tratar de soltar la jeringa por distancia

    private bool checkConexion = false;
    private bool checkDesconexion = false;

    [SerializeField] private AudioSource disconnectSound;

    private void ComprobarConexionJeringa(GameState state)
    {
        checkConexion = false;
        checkDesconexion = false;

        if (state == GameState.conectarJeringa)
        {
            checkConexion = true;
        }
        else if (state == GameState.quitarJeringa)
        {
            checkDesconexion = true;
        }
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= ComprobarConexionJeringa;
    }

    private void Start()
    {
        GameManager.onGameStateChanged += ComprobarConexionJeringa;
        rb = GetComponent<Rigidbody>();
        myRealParent = transform.parent;
    }

    private void Update()
    {
        if (syringeConnected)
        {
            ComprobarDistancia();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SyringeGate") && canTrigger)
        {
            syringeBehaviour.isConected = true;
            canTrigger = false;

            keepKinematicActive = true;

            StartCoroutine(ResetDeManos());

            rb.isKinematic = true;
            transform.SetPositionAndRotation(other.transform.position, other.transform.rotation);

            syringeConnected = true;
            transform.SetParent(other.transform);

            if (checkConexion) GameManager.applicationController.updateGameState(GameState.inflarBalon);
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
            syringeBehaviour.isConected = false;
            syringeConnected = false;
            rb.isKinematic = false;
            transform.SetParent(myRealParent);

            if (checkDesconexion) GameManager.applicationController.updateGameState(GameState.desacoplarMascarilla);
        }
    }

    public void ComprobarDistancia()
    {
        float distance = Vector3.Distance(centralPoint.position, connectedObject.position);

        if (distance > maxDistance)
        {
            DesacoplarCable();
        }
    }

    private void DesacoplarCable()
    {
        disconnectSound.Play();
        syringeConnected = false;
        syringeBehaviour.isConected = false;
        rb.isKinematic = false;
        transform.SetParent(myRealParent);
        StartCoroutine(ResetDeManos());

        if (checkDesconexion) GameManager.applicationController.updateGameState(GameState.desacoplarMascarilla);
    }

    IEnumerator ResetDeManos()
    {
        Manos.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Manos.SetActive(true);
        canTrigger = true;
    }
}
