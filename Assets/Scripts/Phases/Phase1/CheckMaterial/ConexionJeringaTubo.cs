using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class ConexionJeringaTubo : MonoBehaviour
{
    private Rigidbody myRb;
    private Transform manosValvula;
    private FixedJoint fixedJoint;
    private Transform nuevaPosicion;

    //Hacer que cuando la distancia de la valvula sea superior al tamaño de la cuerda se desconecte de la jeringa y se quite de la mano
    private Transform PosicionPadre;
    [SerializeField] private float maxDistance = 0f; // Distancia máxima permitida para mantener la conexión

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        manosValvula = transform.Find("Manos");
        nuevaPosicion = transform.Find("Transform");
        PosicionPadre = transform.parent;
    }


    private void Update()
    {
        float distance = Vector3.Distance(transform.position, PosicionPadre.position);

        // Si la distancia es mayor al límite, soltar el objeto
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


            manosValvula.gameObject.SetActive(false);
            StartCoroutine(EsperarUnSegundo());

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
        StartCoroutine(EsperarUnSegundo());
    }

    IEnumerator EsperarUnSegundo()
    {
        yield return new WaitForSeconds(1f);

        manosValvula.gameObject.SetActive(true);
    }
}
