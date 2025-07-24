using System.Collections;
using UnityEngine;

public class ConexionJeringaTubo : MonoBehaviour
{
    private Rigidbody myRb;
    private Transform manosValvula;
    private FixedJoint fixedJoint;
    private Transform nuevaPosicion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        manosValvula = transform.Find("Manos");
        nuevaPosicion = transform.Find("Transform");
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

    IEnumerator EsperarUnSegundo()
    {
        yield return new WaitForSeconds(1f);

        manosValvula.gameObject.SetActive(true);
    }
}
