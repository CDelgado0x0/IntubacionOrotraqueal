using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class ConexionJeringaTubo : MonoBehaviour
{
    private Rigidbody myRb;
    [SerializeField] private Transform manosValvula;
    private FixedJoint fixedJoint;
    [SerializeField] private Transform nuevaPosicion;

    //Hacer que cuando la distancia de la valvula sea superior al tamaño de la cuerda se desconecte de la jeringa y se quite de la mano
    private Transform puntoReferencia;
    [SerializeField] private float maxDistance = 0f; // Distancia máxima permitida para mantener la conexión

    private Coroutine reactivarManos;

    private SyringeController controladorJeringa;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        puntoReferencia = transform.parent;

        if (manosValvula == null)
            Debug.LogWarning("manosValvula no está asignado en el Inspector.");

        if (nuevaPosicion == null)
            Debug.LogWarning("nuevaPosicion no está asignado en el Inspector.");
    }


    private void Update()
    {
        Transform referencia = fixedJoint != null ? fixedJoint.transform : transform;

        float distance = Vector3.Distance(referencia.position, puntoReferencia.position);
        if (distance > maxDistance)
        {
            soltarConexion();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jeringa"))
        {
            if (fixedJoint != null) Destroy(fixedJoint);

            controladorJeringa = other.GetComponentInChildren<SyringeController>();
            if (controladorJeringa != null)
            {
                controladorJeringa.isConected = true;
            }

            manosValvula.gameObject.SetActive(false);

            IniciarReactivacion();

            myRb.isKinematic = true;
            other.transform.position = nuevaPosicion.position;
            other.transform.rotation = nuevaPosicion.rotation;
            myRb.isKinematic = false;

            fixedJoint = other.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = myRb;
        }
    }

    private void soltarConexion()
    {
        if (fixedJoint != null) Destroy(fixedJoint);
        manosValvula.gameObject.SetActive(false);

        IniciarReactivacion();

        if (controladorJeringa != null)
        {
            controladorJeringa.isConected = false;
        }

    }

    private void IniciarReactivacion()
    {
        if (reactivarManos != null)
            StopCoroutine(reactivarManos);

        reactivarManos = StartCoroutine(EsperarUnSegundo());
    }

    IEnumerator EsperarUnSegundo()
    {
        yield return new WaitForSeconds(1f);

        manosValvula.gameObject.SetActive(true);

        reactivarManos = null;
    }
}
