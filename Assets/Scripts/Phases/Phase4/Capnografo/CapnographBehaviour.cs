using UnityEngine;

public class CapnographBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject CapnographHands;
    [SerializeField] private AmbuPositions ambuController;

    private Rigidbody myRb;

    [SerializeField] private AudioSource connectSound;
    [SerializeField] private AudioClip[] connectionClips;


    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CapnographDetectable"))
        {
            PlayConnectedSound();
            CapnographHands.SetActive(false);
            myRb.isKinematic = true;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            ambuController.capnographConnected = true;
        }
    }

    void PlayConnectedSound()
    {
        int index = Random.Range(0, connectionClips.Length);
        connectSound.PlayOneShot(connectionClips[index]);
    }
}
