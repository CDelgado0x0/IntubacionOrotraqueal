using System.Collections;
using UnityEngine;

public class ConexionJeringaTubo : MonoBehaviour
{
    private Rigidbody myRb;
    private Transform manosValvula;
    private FixedJoint fixedJoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = GetComponent<Rigidbody>(); ;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jeringa"))
        {
            if (fixedJoint != null) Destroy(fixedJoint);

            manosValvula = transform.Find("Manos");

            manosValvula.gameObject.SetActive(false);
            StartCoroutine(EsperarUnSegundo());

            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            myRb.isKinematic = false;

            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = other.attachedRigidbody;


        }
    }

    IEnumerator EsperarUnSegundo()
    {
        yield return new WaitForSeconds(1f);

        manosValvula.gameObject.SetActive(true);
    }
}
