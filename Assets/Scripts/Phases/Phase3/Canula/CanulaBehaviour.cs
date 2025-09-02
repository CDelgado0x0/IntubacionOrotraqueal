using System.Collections;
using UnityEngine;

public class CanulaBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject Manos;
    [SerializeField] private ChangeGrabTransforms myGrabs;

    private Rigidbody myRb;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanulaDetectable"))
        {
            StartCoroutine(HandsChange());
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            myGrabs.JustMovement();
        }
    }

    private IEnumerator HandsChange()
    {
        Manos.SetActive(false);
        yield return new WaitForSeconds(1f);
        Manos.SetActive(true);
    }
}
