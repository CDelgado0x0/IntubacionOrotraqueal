using UnityEngine;

public class CapnographBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject CapnographHands;

    private Rigidbody myRb;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CapnographDetectable"))
        {
            CapnographHands.SetActive(false);
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
        }
    }
}
