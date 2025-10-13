using UnityEngine;

public class CapnographBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject CapnographHands;
    [SerializeField] private AmbuPositions ambuController;

    private Rigidbody myRb;

    [SerializeField] private AudioSource connectSound;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CapnographDetectable"))
        {
            connectSound.Play();
            CapnographHands.SetActive(false);
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            ambuController.capnographConnected = true;
        }
    }
}
